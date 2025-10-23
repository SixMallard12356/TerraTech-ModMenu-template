#define UNITY_EDITOR
using System.Collections.Generic;
using System.Diagnostics;

public static class TTAnalytics
{
	public enum BatchOverflowHandling
	{
		Truncate,
		MultiSend
	}

	private static Dictionary<string, BatchOverflowHandling> _BatchedEventNames = new Dictionary<string, BatchOverflowHandling> { 
	{
		"BlockAttached",
		BatchOverflowHandling.MultiSend
	} };

	private static HashSet<string> _DisabledEventNames = new HashSet<string> { "BlockSold", "BlockScrapped", "BlockPurchased", "BlockScavenged", "player_travel_distance" };

	private static Dictionary<string, Dictionary<int, int>> _BatchedEventData = new Dictionary<string, Dictionary<int, int>>();

	private static bool _isSubscribedToModeFinish = false;

	public static int MaxEventDataSize => 500;

	private static bool IsBatchedEvent(string eventName)
	{
		return _BatchedEventNames.ContainsKey(eventName);
	}

	private static bool IsActiveEvent(string eventName)
	{
		return !_DisabledEventNames.Contains(eventName);
	}

	private static bool CanSendAnalyticEvents()
	{
		return false;
	}

	[Conditional("USE_ANALYTICS")]
	public static void CustomEvent(string customEventName)
	{
		if (CanSendAnalyticEvents() && IsActiveEvent(customEventName))
		{
			d.AssertFormat(!IsBatchedEvent(customEventName), "Analytics Event {0} is configured as batch, but batching for this type is not setup!");
		}
	}

	[Conditional("USE_ANALYTICS")]
	public static void CustomEvent(string customEventName, string dataName, object dataObject)
	{
		if (CanSendAnalyticEvents() && IsActiveEvent(customEventName))
		{
			new Dictionary<string, object>().Add(dataName, dataObject);
		}
	}

	[Conditional("USE_ANALYTICS")]
	public static void CustomEvent(string customEventName, IDictionary<string, object> eventData)
	{
		if (CanSendAnalyticEvents())
		{
			IsActiveEvent(customEventName);
		}
	}

	[Conditional("USE_ANALYTICS")]
	public static void BlockEvent(string customEventName, BlockTypes blockType, int count = 1)
	{
		if (!CanSendAnalyticEvents())
		{
			return;
		}
		if (IsBatchedEvent(customEventName))
		{
			SetupBatchSendIfNeeded();
			if (!_BatchedEventData.TryGetValue(customEventName, out var value))
			{
				value = new Dictionary<int, int>();
				_BatchedEventData.Add(customEventName, value);
			}
			if (!value.TryGetValue((int)blockType, out var value2))
			{
				value2 = 0;
			}
			value[(int)blockType] = value2 + count;
		}
		else if (IsActiveEvent(customEventName))
		{
			new Dictionary<string, object>
			{
				{
					"BlockType",
					blockType.ToString()
				},
				{
					"GameMode",
					Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().ToString()
				}
			};
		}
	}

	[Conditional("USE_ANALYTICS")]
	private static void Internal_SendEvent(string customEventName, IDictionary<string, object> eventData, bool logErrors = true)
	{
	}

	public static int GetEventDataSize(IDictionary<string, object> eventData)
	{
		int num = 0;
		foreach (KeyValuePair<string, object> eventDatum in eventData)
		{
			num += GetByteCount(eventDatum.Key);
			num += GetByteCount(eventDatum.Value);
		}
		return num;
	}

	public static int GetByteCount(object obj)
	{
		if (obj is string text)
		{
			return text.Length;
		}
		if (obj is byte || obj is sbyte)
		{
			return 1;
		}
		if (obj is int || obj is short || obj is uint || obj is ushort)
		{
			return 4;
		}
		if (obj is long || obj is ulong)
		{
			return 8;
		}
		if (obj is float || obj is double || obj is decimal)
		{
			return 8;
		}
		return obj.ToString().Length;
	}

	private static void SetupBatchSendIfNeeded()
	{
		if (!_isSubscribedToModeFinish)
		{
			Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(OnModeFinished);
			_isSubscribedToModeFinish = true;
		}
	}

	private static void OnModeFinished(Mode mode)
	{
		if (!CanSendAnalyticEvents())
		{
			return;
		}
		foreach (KeyValuePair<string, Dictionary<int, int>> batchedEventDatum in _BatchedEventData)
		{
			string key = batchedEventDatum.Key;
			_ = batchedEventDatum.Value;
			_BatchedEventNames.TryGetValue(key, out var _);
			new Dictionary<string, object>().Add("GameMode", mode.GetGameType());
		}
		_BatchedEventData.Clear();
	}

	[Conditional("USE_ANALYTICS")]
	public static void ProcessBatch(string eventName, Dictionary<string, object> eventData, string countParamName, Dictionary<int, int> countsByType, BatchOverflowHandling overflowHandling)
	{
		Dictionary<int, int>.Enumerator enumerator = countsByType.GetEnumerator();
		int eventDataSize = GetEventDataSize(eventData);
		eventDataSize += GetByteCount(countParamName);
		int num = eventDataSize;
		List<string> list = new List<string>();
		while (enumerator.MoveNext())
		{
			int key = enumerator.Current.Key;
			int value = enumerator.Current.Value;
			string text = $"{key}|{value}";
			int num2 = GetByteCount(text) + 1;
			if (num + num2 > MaxEventDataSize)
			{
				eventData[countParamName] = string.Join(",", list);
				list.Clear();
				num = eventDataSize;
				if (overflowHandling == BatchOverflowHandling.Truncate)
				{
					d.LogWarningFormat("Truncating Analytics Batch data for {0}, with {1} more entries that won't be sent", eventName, countsByType.Count - list.Count);
					return;
				}
			}
			list.Add(text);
			num += num2;
		}
		if (list.Count > 0)
		{
			eventData[countParamName] = string.Join(",", list);
		}
	}
}
