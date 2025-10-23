#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class DebugSettings : ScriptableObject
{
	public enum EditorPlatform
	{
		Steam,
		EpicGS,
		LanMp,
		NetEase,
		PS4Emulated,
		PS4TeyonEmulated,
		XboxOneEmulated,
		SwitchEmulated,
		SwitchTeyonEmulated
	}

	public enum VisibleDebugFlags
	{
		Generic,
		Holder,
		Requester,
		TechCentreOfGravity,
		TechCentreOfMass,
		TechInertia,
		BlockGravity,
		BlockMass,
		FilledCellsGravity,
		FilledCellsMass
	}

	public bool m_FastStart;

	public string m_StartMode;

	public AutoSpriteCreate.SpriteCaptureType m_SpriteCaptureType = AutoSpriteCreate.SpriteCaptureType.Disabled;

	public bool m_QuitAfterCapture;

	public List<ManStartup.ModeInitSetting> m_StartModeSettings;

	public string m_StartModeSettingStringCache;

	public Bitfield<VisibleDebugFlags> m_VisibleDebugFlags = new Bitfield<VisibleDebugFlags>();

	public bool m_ShowBiomeInfo;

	public bool m_LiveUpdateWheelParams;

	public bool m_DebugTechBuilder;

	public bool m_DisableMeshMerge;

	public bool m_DisableAllDamage;

	public bool m_EnableTechSpawnRecycleLog;

	public string m_OverrideSpawnFolders = "";

	public WorldGenVersionData OverrideWorldGenVersionData = new WorldGenVersionData(-1, BiomeMap.WorldGenVersioningType.ChangleListVersionInt);

	public bool m_DebugForceFailFreeSpaceFinder;

	public bool m_DisableManualTargeting;

	public bool m_ForceUniformFiring;

	public string m_OverrideLoadFile = "";

	public bool m_DisableEditorTreadmillDistanceOverride;

	public bool m_DisplayWorldTileStates;

	public bool m_AcceleratedBlockAPCollection = true;

	public bool m_UpdatePoolTables;

	public bool m_UpdateConsolePoolOverride;

	public bool m_ShowSelectedNavUI;

	public bool m_ShowAllBlocksInInventory;

	public bool m_AchievementsEnabledInAllModes;

	public bool m_OverridePlatformDLCs;

	public int m_EnabledDLCs = -1;

	public bool m_NintendoFreeCommunicationPermission = true;

	public float m_DebugTeleportDistance = 100f;

	public bool m_CheckForNonRecycledChildren = true;

	public float m_TimeScale = 1f;

	public EditorPlatform m_EditorPlatform;

	public bool m_EditorEmulatedSwitchHandheldMode;

	public bool m_AllowTestingDynamicResolution;

	public bool m_JoystickUserEngagement;

	public bool m_ShowMouseOnConsoleUI;

	public bool m_EditorIgnoresFocusLost = true;

	public bool m_EditorUseBlockLimiter;

	public Event<bool> OnGamepadSetEnabled;

	private bool m_UseGamepadInput;

	public bool m_EpicLogonUseDevAuth;

	public string m_EpicLogonDevAuthHost = "localhost:7777";

	public string m_EpicLogonDevAuthID = "";

	public string m_EditorEpicSandboxID = "";

	public bool m_EditorEOSOptOut;

	public string m_SpoofTimestamp;

	public bool m_LogPoolBufferSizes;

	public bool m_FakeVirtualKeyboard;

	public bool m_VirtualKeyboardSucceed = true;

	public string m_VirtualKeyboardString = "VirtualKeyboard";

	private bool m_ResetDisabledDamageAtModeEnd;

	public bool UseGamepadInput
	{
		get
		{
			return m_UseGamepadInput;
		}
		set
		{
			if (value != m_UseGamepadInput)
			{
				m_UseGamepadInput = value;
				OnGamepadSetEnabled.Send(value);
			}
		}
	}

	public bool IsConsolePlatform
	{
		get
		{
			switch (m_EditorPlatform)
			{
			case EditorPlatform.Steam:
			case EditorPlatform.EpicGS:
			case EditorPlatform.LanMp:
			case EditorPlatform.NetEase:
				return false;
			case EditorPlatform.PS4Emulated:
			case EditorPlatform.PS4TeyonEmulated:
			case EditorPlatform.XboxOneEmulated:
			case EditorPlatform.SwitchEmulated:
			case EditorPlatform.SwitchTeyonEmulated:
				return true;
			default:
				d.LogErrorFormat("Unknown console platform {0}", m_EditorPlatform);
				return false;
			}
		}
	}

	public void SetDisableAllDamage(bool disable, bool currentModeOnly = true)
	{
		if (currentModeOnly && !m_ResetDisabledDamageAtModeEnd)
		{
			m_ResetDisabledDamageAtModeEnd = true;
			Singleton.Manager<ManGameMode>.inst.ModeCleanUpEvent.Subscribe(OnModeCleanupResetDamageFlag);
		}
		else if (!currentModeOnly && m_ResetDisabledDamageAtModeEnd)
		{
			ResetDisabledDamageAndCleanup();
		}
		m_DisableAllDamage = disable;
	}

	private void OnModeCleanupResetDamageFlag(Mode _)
	{
		ResetDisabledDamageAndCleanup();
	}

	private void ResetDisabledDamageAndCleanup()
	{
		m_DisableAllDamage = false;
		m_ResetDisabledDamageAtModeEnd = false;
		if (Singleton.Manager<ManGameMode>.inst != null)
		{
			Singleton.Manager<ManGameMode>.inst.ModeCleanUpEvent.Unsubscribe(OnModeCleanupResetDamageFlag);
		}
	}
}
