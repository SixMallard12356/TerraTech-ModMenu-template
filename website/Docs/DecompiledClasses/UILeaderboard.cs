using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILeaderboard : MonoBehaviour
{
	public struct ScoreEntry
	{
		public string m_PlayerName;

		public float m_Score;
	}

	private struct PlaceEntry
	{
		public UILeaderboardPlace place;

		public UnityAction replayClickedAction;
	}

	[SerializeField]
	private RectTransform m_PlayerEntries;

	[SerializeField]
	private UILeaderboardPlace m_NonHighlightedPlayerEntryPrefab;

	[SerializeField]
	private UILeaderboardPlace m_HighlightedPlayerEntryPrefab;

	[SerializeField]
	private ScrollRect m_ScrollRect;

	[SerializeField]
	private UITableScroller m_Scroller;

	[SerializeField]
	private UIScrollRectSetter m_ScrollRectSetter;

	public Event<int> ReplayButtonClicked;

	private List<ScoreEntry> m_ScoreEntries;

	private List<PlaceEntry> m_GUIEntries = new List<PlaceEntry>();

	private string m_ScoreTypeName;

	private const int NO_HIGHLIGHT = -1;

	private int m_HighlightedIndex = -1;

	private int m_GhostedIndex = -1;

	public void Setup(List<ScoreEntry> sortedLeaderboard, string scoreTypeName)
	{
		m_ScoreEntries = sortedLeaderboard;
		m_ScoreTypeName = scoreTypeName;
		m_HighlightedIndex = -1;
		m_GhostedIndex = -1;
	}

	public void SetHighlightedIndex(int index)
	{
		m_HighlightedIndex = index;
	}

	public void SetGhostedIndex(int index)
	{
		m_GhostedIndex = index;
	}

	public void SpawnEntries()
	{
		if (m_ScoreEntries != null)
		{
			for (int i = 0; i < m_ScoreEntries.Count; i++)
			{
				UILeaderboardPlace prefab = ((m_HighlightedIndex == i) ? m_HighlightedPlayerEntryPrefab : m_NonHighlightedPlayerEntryPrefab);
				PlaceEntry item = new PlaceEntry
				{
					place = prefab.Spawn()
				};
				(item.place.transform as RectTransform).SetParent(m_PlayerEntries, worldPositionStays: false);
				if (item.place.ReplayButton != null)
				{
					int buttonIndex = i;
					item.replayClickedAction = delegate
					{
						ReplayButtonClicked.Send(buttonIndex);
					};
					item.place.ReplayButton.onClick.AddListener(item.replayClickedAction);
				}
				bool showGhostParts = m_GhostedIndex != -1 && m_GhostedIndex == i;
				item.place.Setup(i + 1, m_ScoreEntries[i].m_PlayerName, m_ScoreTypeName, m_ScoreEntries[i].m_Score, showGhostParts);
				m_GUIEntries.Add(item);
			}
		}
		if ((bool)m_Scroller)
		{
			int index = ((m_HighlightedIndex != -1) ? m_HighlightedIndex : 0);
			m_Scroller.ScrollToEntry(index);
		}
	}

	public void RecycleEntries()
	{
		for (int num = m_GUIEntries.Count - 1; num >= 0; num--)
		{
			if (m_GUIEntries[num].place.ReplayButton != null)
			{
				m_GUIEntries[num].place.ReplayButton.onClick.RemoveListener(m_GUIEntries[num].replayClickedAction);
			}
			m_GUIEntries[num].place.transform.SetParent(null, worldPositionStays: false);
			m_GUIEntries[num].place.Recycle();
			m_GUIEntries.RemoveAt(num);
		}
	}

	public void ScrollToButtonIfValid(GameObject buttonObj)
	{
		if (!m_Scroller)
		{
			return;
		}
		int num = -1;
		for (int i = 0; i < m_GUIEntries.Count; i++)
		{
			if (buttonObj == m_GUIEntries[i].place.ReplayButton.gameObject)
			{
				num = i;
				break;
			}
		}
		if (num >= 0)
		{
			m_Scroller.ScrollToEntry(num);
		}
	}

	private void Update()
	{
		if (m_ScrollRectSetter != null)
		{
			float axis = Singleton.Manager<ManInput>.inst.GetAxis(47);
			m_ScrollRectSetter.Scroll((0f - axis) * Time.deltaTime * Globals.inst.m_StickScrollSensitivity);
		}
	}
}
