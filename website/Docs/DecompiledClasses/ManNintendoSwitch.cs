#define UNITY_EDITOR
using UnityEngine;

public class ManNintendoSwitch : MonoBehaviour
{
	public const string k_TeyonProdNameMarker = "_Teyon";

	public static Event<bool> HandheldModeChangedEvent;

	private static int s_FastLoadEnableChannels;

	private int[] m_DLC = new int[0];

	public static bool IsTeyon => Application.productName.Contains("_Teyon");

	public static ulong AppID
	{
		get
		{
			if (!IsTeyon)
			{
				return 72222795873673216uL;
			}
			return 72311856369344512uL;
		}
	}

	public static bool IsHandheldMode { get; private set; }

	public static bool IsFastLoad => s_FastLoadEnableChannels > 0;

	public static void SetIsHandheldMode(bool isHandheld, bool forceEvent = false)
	{
		if (isHandheld != IsHandheldMode || forceEvent)
		{
			IsHandheldMode = isHandheld;
			HandheldModeChangedEvent.Send(isHandheld);
		}
	}

	public static void SetFastLoad(bool fastLoad, SwitchFastLoadChannel channelEnum)
	{
		d.Assert(channelEnum >= SwitchFastLoadChannel.Startup && channelEnum < (SwitchFastLoadChannel)32, $"SetFastLoad Invalid fastLoad channel {channelEnum}");
		bool isFastLoad = IsFastLoad;
		s_FastLoadEnableChannels = Bitfield.Set(s_FastLoadEnableChannels, (int)channelEnum, fastLoad);
		if (isFastLoad != IsFastLoad)
		{
			d.Log((IsFastLoad ? "Starting" : "Ending") + " Fast Load configuration!");
		}
	}

	public static bool CheckFreeCommunicationPermission(bool showUi)
	{
		bool flag = true;
		if ((bool)Singleton.Manager<DebugUtil>.inst && (bool)Singleton.Manager<DebugUtil>.inst.m_Settings)
		{
			flag = Singleton.Manager<DebugUtil>.inst.m_Settings.m_NintendoFreeCommunicationPermission;
			if (!flag)
			{
				d.LogWarning("ManNintendoSwitch.CheckFreeCommunicationPermission - permissions blocked. Check DebugSettings panel.");
			}
		}
		d.Log($"CheckFreeCommunicationPermission returning {flag}");
		return flag;
	}
}
