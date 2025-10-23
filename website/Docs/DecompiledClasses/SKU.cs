#define UNITY_EDITOR
using System;
using System.Diagnostics;
using System.IO;
using TerraTech.Network;
using UnityEngine;

public class SKU
{
	public enum BuildType
	{
		Standard,
		Steam,
		GoGGalaxy,
		NetEase,
		XboxOne_Internal,
		XboxOne_Retail,
		PS4_Internal,
		PS4_EU,
		PS4_US,
		LAN_MP,
		Switch_Internal,
		Switch_Retail,
		PS4_JP_Teyon,
		Switch_Teyon_Internal,
		Switch_Teyon_Retail,
		EpicGameStore
	}

	public enum Configuration
	{
		Debug,
		Release,
		Master
	}

	private enum StateFlag : byte
	{
		NotInitialised,
		True,
		False
	}

	public const bool Demo = false;

	public const bool Show = false;

	public const bool IsGoGGalaxy = false;

	public const bool IsCanary = false;

	public const bool IsStandard = false;

	public const bool TwitterEnabled = false;

	public static string ChangelistVersion = ChangelistProxy.TIMESTAMP;

	private static bool m_LoadedBuildTypeAndVersion;

	private static BuildType m_CurrentBuildType;

	private static string m_DisplayVersion;

	private static StateFlag m_UseLegacyBackend;

	public static BuildType CurrentBuildType
	{
		get
		{
			LoadBuildTypeAndDisplayVersionIfNecessary();
			return m_CurrentBuildType;
		}
	}

	public static bool IsSteam => CurrentBuildType == BuildType.Steam;

	public static bool IsNetEase => CurrentBuildType == BuildType.NetEase;

	public static bool IsLAN_MP => CurrentBuildType == BuildType.LAN_MP;

	public static bool IsEpicGS => CurrentBuildType == BuildType.EpicGameStore;

	public static bool UsesEOS
	{
		get
		{
			if (!IsEpicGS)
			{
				if (IsSteam)
				{
					return !UseSteamLegacyMPBackend;
				}
				return false;
			}
			return true;
		}
	}

	public static bool IsEOSCrossplayPlatform
	{
		get
		{
			if (UsesEOS)
			{
				return !IsEpicGS;
			}
			return false;
		}
	}

	public static bool GenericPCPlatform
	{
		get
		{
			if (!IsSteam && !IsLAN_MP)
			{
				return IsEpicGS;
			}
			return true;
		}
	}

	public static bool SupportsMods => IsSteam;

	public static bool IsTeyon => false;

	public static bool XboxOneUI => false;

	public static bool PS4UI => false;

	public static bool SwitchUI => false;

	public static bool ConsoleUI
	{
		get
		{
			if (!XboxOneUI && !PS4UI)
			{
				return SwitchUI;
			}
			return true;
		}
	}

	public static bool FlipEnterCancelButtons
	{
		get
		{
			if (SwitchUI)
			{
				return true;
			}
			return false;
		}
	}

	public static bool FlipExtraButtons => SwitchUI;

	public static bool GrabItEnabled => false;

	public static bool TwitchEnabled
	{
		get
		{
			if (!ConsoleUI)
			{
				return !IsNetEase;
			}
			return false;
		}
	}

	public static bool AllowsExternalLinks => !ConsoleUI;

	public static bool BansEnabled => !PS4UI;

	public static bool AnnouncerEnabled
	{
		get
		{
			if (!ConsoleUI)
			{
				return !IsNetEase;
			}
			return false;
		}
	}

	public static bool BugReporterEnabled
	{
		get
		{
			if (!ConsoleUI)
			{
				return !IsNetEase;
			}
			return false;
		}
	}

	public static bool AllowTextInput => true;

	public static bool SupportsMultiplayer
	{
		get
		{
			if (Singleton.Manager<ManNetworkLobby>.inst == null || Singleton.Manager<ManNetworkLobby>.inst.LobbySystem == null)
			{
				return false;
			}
			if (!IsSteam && !IsEpicGS && !IsLAN_MP)
			{
				return IsNetEase;
			}
			return true;
		}
	}

	public static bool OverrideMpTechNames => ConsoleUI;

	public static string DisplayVersion
	{
		get
		{
			LoadBuildTypeAndDisplayVersionIfNecessary();
			return m_DisplayVersion;
		}
	}

	private static bool UseSteamLegacyMPBackend
	{
		get
		{
			if (m_UseLegacyBackend == StateFlag.NotInitialised)
			{
				m_UseLegacyBackend = StateFlag.False;
				if (CommandLineReader.HasArgument("disable_crossplay"))
				{
					m_UseLegacyBackend = StateFlag.True;
				}
			}
			return m_UseLegacyBackend == StateFlag.True;
		}
	}

	public static int CalcUniqueChangelistVersionIntRepresentation()
	{
		if (!ParseChangeListVersionNumberToInt(ChangelistVersion, out var versionInt))
		{
			d.LogError("Failed to parse changelist number as int");
		}
		return 1342177280 + versionInt;
	}

	[Conditional("UNITY_EDITOR")]
	public static void ErrorIfDateExceeds(DateTime date, string message)
	{
		if (DateTime.Now > date)
		{
			d.LogError($"Date {date.ToShortDateString()} exceeded: {message}\n {Environment.StackTrace.Split('\n')[2]}");
		}
	}

	public static bool IsVersionMoreRecentThanBuild(string version)
	{
		if (ParseChangeListVersionNumberToInt(ChangelistVersion, out var versionInt) && ParseChangeListVersionNumberToInt(version, out var versionInt2) && versionInt2 > versionInt)
		{
			return true;
		}
		return false;
	}

	public static bool ParseChangeListVersionNumberToInt(string version, out int versionInt)
	{
		bool flag = false;
		flag = (version.StartsWith("T") ? int.TryParse(version.Substring(1), out var result) : ((!version.StartsWith("P")) ? int.TryParse(version, out result) : int.TryParse(version.Substring(1), out result)));
		versionInt = (flag ? result : (-1));
		return flag;
	}

	private static bool CheckByteArraysEqual(byte[] a0, byte[] a1)
	{
		bool result;
		if (a0.Length == a1.Length)
		{
			result = true;
			for (int i = 0; i < a0.Length; i++)
			{
				if (a0[i] != a1[i])
				{
					result = false;
					break;
				}
			}
		}
		else
		{
			result = false;
		}
		return result;
	}

	private static void LoadBuildTypeAndDisplayVersionIfNecessary()
	{
		if (m_LoadedBuildTypeAndVersion)
		{
			return;
		}
		m_LoadedBuildTypeAndVersion = true;
		m_CurrentBuildType = BuildType.Standard;
		try
		{
			byte[] a = File.ReadAllBytes(Application.dataPath + "/StreamingAssets/sdf.bin");
			if (CheckByteArraysEqual(a, new byte[10] { 14, 3, 194, 195, 18, 82, 41, 157, 119, 139 }))
			{
				m_CurrentBuildType = BuildType.Steam;
			}
			else if (CheckByteArraysEqual(a, new byte[10] { 155, 141, 12, 41, 224, 153, 102, 146, 237, 230 }))
			{
				m_CurrentBuildType = BuildType.NetEase;
			}
			else if (CheckByteArraysEqual(a, new byte[10] { 33, 142, 134, 204, 99, 13, 228, 190, 60, 113 }))
			{
				m_CurrentBuildType = BuildType.LAN_MP;
			}
			else if (CheckByteArraysEqual(a, new byte[10] { 248, 119, 171, 205, 239, 63, 221, 196, 16, 5 }))
			{
				m_CurrentBuildType = BuildType.EpicGameStore;
			}
		}
		catch (Exception)
		{
		}
		if (m_CurrentBuildType == BuildType.Standard)
		{
			Application.Quit();
		}
		try
		{
			m_DisplayVersion = File.ReadAllText(Application.dataPath + "/StreamingAssets/vdn.bin");
		}
		catch
		{
			m_DisplayVersion = "?";
		}
	}
}
