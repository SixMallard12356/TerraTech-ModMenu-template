#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using DevCommands;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class DebugUtil : Singleton.Manager<DebugUtil>
{
	public enum CheatCode
	{
		ToggleDevelopmentCheats,
		ToggleDevelopmentCheatsPublic,
		TogglePublicFacingCheats1,
		TogglePublicFacingCheats2,
		TogglePublicFacingCheats3,
		TogglePublicFacingCheats4,
		TogglePublicFacingCheats5,
		Skip,
		Attract,
		HideUI,
		ShowUI,
		Replace,
		Reload,
		Currency,
		LargeCurrency,
		HideCursor,
		ShowCursor,
		NoNight,
		SkipToNextDayOrNight,
		Immortal,
		FlipReverseSteering,
		ToggleFPS,
		RandD,
		InvaderInfo,
		DevelopmentBlocks,
		PrintingEnabled,
		SaveSequence,
		AllowLoadAllSaves,
		AddXP_GSO,
		AddXP_GEO,
		AddXP_VEN,
		AddXP_HWK,
		AddXP_BF,
		ToggleXP,
		SpawnAI,
		UnlockBlocks,
		PauseEnemies,
		PauseTime,
		CompleteEncounter,
		ResetAchievements,
		NoScenery,
		ShowTechCost,
		ExclusiveContent,
		SkipMultiplayerTimer,
		MaxPower,
		ThrowException,
		NetDump,
		ToggleBlockLimiter,
		SpawnAllMissions,
		RemoveTechLoaderRestrictions,
		DebugPlacementIntersections,
		NoDamage,
		PauseMissionTimer,
		AddXP_RR,
		GravityNormal,
		FastTalk,
		AllEnemies,
		TogglePopInfo,
		ToggleTradingStationDebug,
		TogglePowerLevels,
		EncounterState,
		AddBigXP_GSO,
		AddBigXP_GEO,
		AddBigXP_VEN,
		AddBigXP_HWK,
		AddBigXP_BF,
		AddBigXP_RR,
		UnlimitedShopBlocks,
		AllBlocksInInventory,
		SyncMap,
		AddXP_SJ,
		AddBigXP_SJ
	}

	public enum CheatType
	{
		PublicFacing,
		Development
	}

	private struct CachedArrow
	{
		public Vector3 from;

		public Vector3 to;

		public Color color;
	}

	public enum DebugControlBlockedReason
	{
		PresetMenu,
		BlockMenu,
		ChunkMenu,
		SceneryMenu,
		DebugBlockNameDisplay
	}

	public class CheatCrashException : Exception
	{
		public CheatCrashException()
			: base("Deliberate crash via cheat code")
		{
		}
	}

	public DebugSettings m_Settings;

	[Header("Population")]
	public bool debugPopulationShowSpawnRange;

	public bool debugPopulationPrintSpawnValues;

	public bool debugPopulationStopIfPoolLow;

	public float debugPopulationStopPoolThreshold = 0.5f;

	[Header("Tech/Visible")]
	public bool newPlayerTankReplacesPrevious = true;

	[Header("AI")]
	public bool aiTanksStartInactive = true;

	public bool debugReportAIState;

	public bool debugAIsDontMove;

	[Header("Effects")]
	public bool lockTank;

	public bool hideShadows;

	public int supersizedScreenshotSize = 2;

	[Header("Other Debug")]
	public bool m_AllowDebugControls = true;

	public bool m_AllowPartialDebug;

	public bool m_OutputSaveDebugInfo;

	public float fpsCounterUpdateInterval = 0.5f;

	public CheatCodesEncryptedAsset m_CheatCodes;

	public CommunityEventData m_CommunityEventData;

	public const bool kCheatsAllowed = true;

	[NonSerialized]
	public bool hideGUI;

	protected bool m_ReRaiseExceptionThrown;

	private Exception m_ReRaiseException;

	private bool previousHideShadows;

	private float normalShadowDistance;

	private bool m_ShowFps;

	private string m_CurrentFps = "";

	private string m_AudioFps = "";

	private bool m_DebugSavesEnabled;

	private bool m_DebugAllowAllSaves;

	private bool m_SpawnAllMissions;

	private bool m_UnlimitedShopBlocks;

	private bool m_ShowAllBlocksInInventory;

	private OnGUICallback m_CheatInputGUICallbackObject;

	private float m_CheatCodeInputStart;

	private List<char> m_CheatInputBuffer = new List<char>();

	private string m_CurrentCheatInputString;

	private string m_CurrentCheatInputHash;

	private MD5 m_MD5Hasher;

	private float m_LongestCheatSequenceDuration;

	private float m_AccumFpsTime;

	private int m_FramesDrawn;

	private bool m_PrevHandheldModeState;

	private bool m_Immortal;

	private Bitfield<DebugControlBlockedReason> m_DebugControlsBlockedReasons = new Bitfield<DebugControlBlockedReason>();

	public EventNoParams OnDebugGUI;

	public Event<CheatCode> CheatCompletedEvent;

	public EventNoParams CheatAccessChangedEvent;

	public static DebugSettings CurrentSettings => Singleton.Manager<DebugUtil>.inst.m_Settings;

	public bool hideTheGUI
	{
		get
		{
			if (!hideGUI)
			{
				return Singleton.Manager<CameraManager>.inst.IsCurrent<FirstPersonFlyCam>();
			}
			return true;
		}
	}

	public bool DisableCheatInput { get; set; }

	public bool DisableNight { get; private set; }

	public bool EnemiesPaused { get; private set; }

	public bool DebugControlBlocked => m_DebugControlsBlockedReasons.AnySet;

	public bool SpawnAllMissions => m_SpawnAllMissions;

	public bool UnlimitedShopBlocks => m_UnlimitedShopBlocks;

	public bool AllBlocksInInventory => m_ShowAllBlocksInInventory;

	public bool RemoveTechLoaderRestrictions { get; private set; }

	public Exception ReRaiseException
	{
		set
		{
			m_ReRaiseException = value;
			m_ReRaiseExceptionThrown = false;
		}
	}

	public static bool ModelPrintingEnabled { get; private set; }

	public static bool DebugSavesEnabled
	{
		get
		{
			if (!Singleton.Manager<DebugUtil>.inst.m_DebugSavesEnabled)
			{
				return Globals.inst.m_DebugSavesEnabled;
			}
			return true;
		}
	}

	public static bool DebugAllowAllSaves => Singleton.Manager<DebugUtil>.inst.m_DebugAllowAllSaves;

	public static string FriendlyAIName { get; set; } = "FTUEAI";

	public static bool isShuttingDown { get; private set; }

	public CommunityEngagementEvent EnagementEvent { get; private set; }

	private void UpdateDebugDelete()
	{
		if (!AreCheatsEnabled(CheatType.Development) || DebugControlBlocked || !(Input.GetKey(KeyCode.Backslash) ? Input.GetMouseButtonUp(0) : Input.GetMouseButton(0)) || (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl)))
		{
			return;
		}
		Visible targetVisible = Singleton.Manager<ManPointer>.inst.targetVisible;
		Transform transform = null;
		if ((bool)targetVisible)
		{
			transform = targetVisible.trans;
		}
		else if ((bool)Singleton.Manager<ManPointer>.inst.targetObject)
		{
			transform = Singleton.Manager<ManPointer>.inst.targetObject.GetTopParent();
		}
		if (!transform || transform.gameObject.IsTerrain() || Singleton.Manager<ManSpawn>.inst.DebugSpawnMenuActive)
		{
			return;
		}
		if ((bool)targetVisible)
		{
			if ((bool)targetVisible.block)
			{
				Singleton.Manager<ManLooseBlocks>.inst.HostDestroyBlock(targetVisible.block, errorOnClientCall: false);
			}
			else if ((bool)targetVisible.pickup)
			{
				Singleton.Manager<ManLooseBlocks>.inst.HostDestroyChunk(targetVisible.pickup);
			}
			else if ((bool)targetVisible.resdisp)
			{
				targetVisible.resdisp.RemoveFromWorld(spawnChunks: false, neverRegrow: true, removeInstant: true, removePersistentDamageStage: true);
			}
			else
			{
				targetVisible.RemoveFromGame();
			}
		}
		else
		{
			transform.Recycle();
		}
	}

	private void UpdateDebugDetatch()
	{
		if (!AreCheatsEnabled(CheatType.Development))
		{
			return;
		}
		Visible targetVisible = Singleton.Manager<ManPointer>.inst.targetVisible;
		if (!targetVisible || !targetVisible.block)
		{
			return;
		}
		if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButtonDown(2))
		{
			targetVisible.block.damage.DebugKillTechBlock();
		}
		else
		{
			if (!Input.GetKey(KeyCode.AltGr) && !Input.GetKey(KeyCode.RightAlt))
			{
				return;
			}
			Tank tank = targetVisible.block.tank;
			if (tank != null && Input.GetMouseButtonDown(2))
			{
				BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tank.blockman.IterateBlocks().GetEnumerator();
				while (enumerator.MoveNext())
				{
					enumerator.Current.damage.DebugKillTechBlock();
				}
			}
		}
	}

	private void UpdateDebugDamage()
	{
		if (!AreCheatsEnabled(CheatType.Development) || Singleton.Manager<ManSpawn>.inst.DebugSpawnMenuActive || DebugControlBlocked || !Input.GetMouseButton(2) || (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl)))
		{
			return;
		}
		Visible targetVisible = Singleton.Manager<ManPointer>.inst.targetVisible;
		if (targetVisible != null)
		{
			Damageable damageable = targetVisible.damageable;
			if (damageable != null)
			{
				float num = damageable.MaxHealth / 1f;
				Tank sourceTank = ((targetVisible.block != null && targetVisible.block.tank != null && ManSpawn.IsPlayerTeam(targetVisible.block.tank.Team)) ? null : Singleton.playerTank);
				Singleton.Manager<ManDamage>.inst.DealDamage(damageable, num * Time.deltaTime, ManDamage.DamageType.Standard, this, sourceTank, targetVisible.centrePosition);
			}
		}
	}

	private void UpdateShadowSettings()
	{
		if (hideShadows != previousHideShadows)
		{
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.AO, !hideShadows);
			if (hideShadows)
			{
				normalShadowDistance = QualitySettings.shadowDistance;
				QualitySettings.shadowDistance = 0f;
			}
			else
			{
				QualitySettings.shadowDistance = normalShadowDistance;
			}
			previousHideShadows = hideShadows;
		}
	}

	public static bool DebugCtrlKeyHeld()
	{
		if (!Application.isEditor && !Input.GetKey(KeyCode.LeftControl))
		{
			return Input.GetKey(KeyCode.RightControl);
		}
		return true;
	}

	public void SetDebugControlBlocked(DebugControlBlockedReason blockReason, bool blocked)
	{
		m_DebugControlsBlockedReasons.Set((int)blockReason, blocked);
	}

	public bool DebugHasDLC(ManDLC.DLC inpDLC, DLCTable table)
	{
		int num = -1;
		for (int i = 0; i < table.DLCPacks.Count; i++)
		{
			if (table.DLCPacks[i] == inpDLC)
			{
				num = i;
				break;
			}
		}
		if (num >= 0)
		{
			int num2 = 1 << num;
			return (m_Settings.m_EnabledDLCs & num2) != 0;
		}
		d.LogErrorFormat("Testing for unknown dlc {0}", inpDLC.DlcName);
		return false;
	}

	public bool AreCheatsEnabled(CheatType cheatType, bool forInput = true)
	{
		bool result = false;
		if (!forInput || !DisableCheatInput)
		{
			switch (cheatType)
			{
			case CheatType.PublicFacing:
				result = m_AllowPartialDebug || m_AllowDebugControls;
				break;
			case CheatType.Development:
				result = m_AllowDebugControls;
				break;
			default:
				d.LogErrorFormat("AreCheatsEnabled - Cheat type {0} not implemented!", cheatType);
				break;
			}
		}
		return result;
	}

	public bool CheatCompleted(CheatCode cheat)
	{
		bool flag = false;
		int num = ((m_CheatInputBuffer != null) ? m_CheatInputBuffer.Count : 0);
		if (num > 0)
		{
			CheatCodesEncryptedAsset.CheatCode cheatCodeData = m_CheatCodes.GetCheatCodeData(cheat);
			if (num == cheatCodeData.m_SourcePassPhraseLength && Time.time - m_CheatCodeInputStart < cheatCodeData.m_InputTime)
			{
				if (m_CurrentCheatInputString == null || m_CurrentCheatInputString.Length != num)
				{
					m_CurrentCheatInputString = new string(m_CheatInputBuffer.ToArray());
					if (m_MD5Hasher == null)
					{
						m_MD5Hasher = MD5.Create();
					}
					m_CurrentCheatInputHash = ManSaveGame.GetMD5Hash(m_MD5Hasher, m_CurrentCheatInputString);
				}
				flag = m_CurrentCheatInputHash == cheatCodeData.m_EncryptedPassPhrase;
				if (flag)
				{
					Singleton.Manager<ManSFX>.inst.PlayMiscSFX(ManSFX.MiscSfxType.CheatCode);
					StopListeningForCheatInput();
					CheatCompletedEvent.Send(cheat);
				}
			}
		}
		return flag;
	}

	private void UpdateCheatInput()
	{
		if (Input.GetKeyDown(KeyCode.Backslash) && !Singleton.Manager<ManDevCommands>.inst.HasInputFocus)
		{
			if (m_CheatInputGUICallbackObject == null)
			{
				m_CheatInputGUICallbackObject = OnGUICallback.AddGUICallback(base.gameObject);
				m_CheatInputGUICallbackObject.OnGUIEvent.Subscribe(ProcessCheatInputOnGUI);
			}
			ResetCheatInput();
			m_CheatCodeInputStart = Time.time;
		}
		else if (m_CheatInputGUICallbackObject != null && Time.time > m_CheatCodeInputStart + m_LongestCheatSequenceDuration)
		{
			StopListeningForCheatInput();
		}
	}

	public void AddTextToCheatInputBuffer(string cheatStr)
	{
		ResetCheatInput();
		m_CheatCodeInputStart = Time.time;
		for (int i = 0; i < cheatStr.Length; i++)
		{
			m_CheatInputBuffer.Add(cheatStr[i]);
		}
	}

	private void ResetCheatInput()
	{
		if (m_CheatInputBuffer != null)
		{
			m_CheatInputBuffer.Clear();
		}
		m_CurrentCheatInputString = null;
		m_CurrentCheatInputHash = null;
	}

	private void StopListeningForCheatInput()
	{
		if (m_CheatInputGUICallbackObject != null)
		{
			m_CheatInputGUICallbackObject.OnGUIEvent.Unsubscribe(ProcessCheatInputOnGUI);
			OnGUICallback.RemoveGUICallback(m_CheatInputGUICallbackObject);
			m_CheatInputGUICallbackObject = null;
		}
		ResetCheatInput();
	}

	private void ProcessCheatInputOnGUI()
	{
		if (Event.current.isKey && Event.current.type == EventType.KeyDown && Event.current.character != 0)
		{
			if (m_CheatInputBuffer == null)
			{
				m_CheatInputBuffer = new List<char>();
			}
			if (m_CheatInputBuffer.Count != 0 || Event.current.character != '\\')
			{
				m_CheatInputBuffer.Add(Event.current.character);
			}
		}
	}

	[Conditional("UNITY_EDITOR")]
	[Conditional("UNITY_ASSERTIONS")]
	public static void AssertRelease(bool condition, string message)
	{
		if (!condition)
		{
			string stackTrace = d.GetStackTrace(2);
			UnityEngine.Debug.LogError($"ASSERT '{message}'\n{stackTrace}");
		}
	}

	[Conditional("USE_ANALYTICS")]
	private static void SendAnalyticsPackage(string message, string stackTrace)
	{
		new Dictionary<string, object>
		{
			{ "Message", message },
			{ "stack_trace", stackTrace }
		};
	}

	public static void GizmosDrawArrow(Vector3 p0, Vector3 p1, float headLength = 0.2f, float headWidthAngle = 30f)
	{
		Gizmos.DrawLine(p0, p1);
		if (p0 != p1)
		{
			float magnitude = (p1 - p0).magnitude;
			if (magnitude < headLength * 2f)
			{
				headLength = magnitude / 2f;
			}
			float num = Mathf.Tan(headWidthAngle * ((float)Math.PI / 180f)) * headLength;
			Matrix4x4 matrix4x = Matrix4x4.TRS(p1, Quaternion.LookRotation(p0 - p1), Vector3.one);
			Vector3 vector = matrix4x.MultiplyPoint(new Vector3(num, num, headLength));
			Vector3 vector2 = matrix4x.MultiplyPoint(new Vector3(0f - num, num, headLength));
			Vector3 vector3 = matrix4x.MultiplyPoint(new Vector3(0f - num, 0f - num, headLength));
			Vector3 vector4 = matrix4x.MultiplyPoint(new Vector3(num, 0f - num, headLength));
			Gizmos.DrawLine(p1, vector);
			Gizmos.DrawLine(p1, vector2);
			Gizmos.DrawLine(p1, vector3);
			Gizmos.DrawLine(p1, vector4);
			Gizmos.DrawLine(vector, vector2);
			Gizmos.DrawLine(vector2, vector3);
			Gizmos.DrawLine(vector3, vector4);
			Gizmos.DrawLine(vector4, vector);
		}
	}

	public void ToggleShowAllBlocksInInventory()
	{
		SetShowAllBlocksInInventory(!m_ShowAllBlocksInInventory);
	}

	public void SetShowAllBlocksInInventory(bool showAllBlocksInInventory)
	{
		if (showAllBlocksInInventory != m_ShowAllBlocksInInventory)
		{
			m_ShowAllBlocksInInventory = showAllBlocksInInventory;
			Singleton.Manager<ManSnapshots>.inst.ResetModeAvailability();
		}
	}

	private void Awake()
	{
		Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
		Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.None);
		d.EnableLogCallstacks = true;
		m_AllowDebugControls = false;
		m_LongestCheatSequenceDuration = m_CheatCodes.GetMaxSequenceDuration();
		EnagementEvent = new CommunityEngagementEvent(m_CommunityEventData);
	}

	private void Update()
	{
		UpdateDebugDelete();
		UpdateDebugDamage();
		UpdateDebugDetatch();
		UpdateShadowSettings();
		UpdateCheatInput();
		if (false)
		{
			VirtualKeyboard.EntryCompleteDelegate onCompleteHandler = delegate(bool accepted, string result)
			{
				if (accepted && !result.NullOrEmpty())
				{
					d.Log("OpenVirtualKeyboard result: " + result);
					if (!Singleton.Manager<ManDevCommands>.inst.TryExecuteInput(result, out var _))
					{
						AddTextToCheatInputBuffer(result);
					}
				}
			};
			VirtualKeyboard.PromptInput("Enter Cheat Code:", string.Empty, string.Empty, onCompleteHandler);
		}
		if (AreCheatsEnabled(CheatType.PublicFacing))
		{
			if (CheatCompleted(CheatCode.HideUI))
			{
				hideGUI = true;
			}
			if (CheatCompleted(CheatCode.ShowUI))
			{
				hideGUI = false;
			}
			if (Input.GetKeyDown(KeyCode.F1))
			{
				hideGUI = !hideGUI;
			}
			if (hideTheGUI == Singleton.Manager<ManHUD>.inst.IsVisible)
			{
				Singleton.Manager<ManHUD>.inst.Show(ManHUD.HideReason.Cheats, !hideTheGUI);
				Singleton.Manager<ManOnScreenMessages>.inst.ShowCanvas(!hideTheGUI);
			}
			if (CheatCompleted(CheatCode.HideCursor))
			{
				Cursor.visible = false;
			}
			if (CheatCompleted(CheatCode.ShowCursor))
			{
				Cursor.visible = true;
			}
			if (CheatCompleted(CheatCode.Currency))
			{
				Singleton.Manager<ManPlayer>.inst.AddMoney(10000);
			}
			if (CheatCompleted(CheatCode.LargeCurrency))
			{
				Singleton.Manager<ManPlayer>.inst.AddMoney(10000000);
			}
			if (CheatCompleted(CheatCode.PauseTime) && ManNetwork.IsHost)
			{
				Singleton.Manager<ManTimeOfDay>.inst.TogglePause();
			}
			if (CheatCompleted(CheatCode.NoNight) && ManNetwork.IsHost)
			{
				DisableNight = !DisableNight;
				Singleton.Manager<ManTimeOfDay>.inst.EnableTimeProgression(!DisableNight);
				if (!DisableNight)
				{
					Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay(11, 0, 0);
				}
			}
			if (CheatCompleted(CheatCode.SkipToNextDayOrNight) && ManNetwork.IsHost)
			{
				Singleton.Manager<ManTimeOfDay>.inst.SkipDayOrNight();
			}
			if (CheatCompleted(CheatCode.SaveSequence))
			{
				m_DebugSavesEnabled = !m_DebugSavesEnabled;
			}
			if (CheatCompleted(CheatCode.AllowLoadAllSaves))
			{
				m_DebugAllowAllSaves = !m_DebugAllowAllSaves;
			}
			if (CheatCompleted(CheatCode.AddXP_GSO))
			{
				Singleton.Manager<ManLicenses>.inst.AddXP(FactionSubTypes.GSO, 500);
			}
			if (CheatCompleted(CheatCode.AddXP_GEO))
			{
				Singleton.Manager<ManLicenses>.inst.AddXP(FactionSubTypes.GC, 500);
			}
			if (CheatCompleted(CheatCode.AddXP_VEN))
			{
				Singleton.Manager<ManLicenses>.inst.AddXP(FactionSubTypes.VEN, 500);
			}
			if (CheatCompleted(CheatCode.AddXP_HWK))
			{
				Singleton.Manager<ManLicenses>.inst.AddXP(FactionSubTypes.HE, 500);
			}
			if (CheatCompleted(CheatCode.AddXP_BF))
			{
				Singleton.Manager<ManLicenses>.inst.AddXP(FactionSubTypes.BF, 500);
			}
			if (CheatCompleted(CheatCode.AddXP_SJ))
			{
				Singleton.Manager<ManLicenses>.inst.AddXP(FactionSubTypes.SJ, 500);
			}
			if (CheatCompleted(CheatCode.AddXP_RR))
			{
				Singleton.Manager<ManLicenses>.inst.AddXP(FactionSubTypes.EXP, 500);
			}
			if (CheatCompleted(CheatCode.AddBigXP_GSO))
			{
				Singleton.Manager<ManLicenses>.inst.AddXP(FactionSubTypes.GSO, 10000);
			}
			if (CheatCompleted(CheatCode.AddBigXP_GEO))
			{
				Singleton.Manager<ManLicenses>.inst.AddXP(FactionSubTypes.GC, 10000);
			}
			if (CheatCompleted(CheatCode.AddBigXP_VEN))
			{
				Singleton.Manager<ManLicenses>.inst.AddXP(FactionSubTypes.VEN, 10000);
			}
			if (CheatCompleted(CheatCode.AddBigXP_HWK))
			{
				Singleton.Manager<ManLicenses>.inst.AddXP(FactionSubTypes.HE, 10000);
			}
			if (CheatCompleted(CheatCode.AddBigXP_BF))
			{
				Singleton.Manager<ManLicenses>.inst.AddXP(FactionSubTypes.BF, 10000);
			}
			if (CheatCompleted(CheatCode.AddBigXP_SJ))
			{
				Singleton.Manager<ManLicenses>.inst.AddXP(FactionSubTypes.SJ, 10000);
			}
			if (CheatCompleted(CheatCode.AddBigXP_RR))
			{
				Singleton.Manager<ManLicenses>.inst.AddXP(FactionSubTypes.EXP, 10000);
			}
			if (CheatCompleted(CheatCode.GravityNormal))
			{
				ManGravity.ManipulatorsEnabled = !ManGravity.ManipulatorsEnabled;
			}
			if (CheatCompleted(CheatCode.FastTalk))
			{
				Singleton.Manager<ManOnScreenMessages>.inst.FastTalk = !Singleton.Manager<ManOnScreenMessages>.inst.FastTalk;
			}
			if (CheatCompleted(CheatCode.AllEnemies))
			{
				foreach (Tank currentTech in Singleton.Manager<ManTechs>.inst.CurrentTechs)
				{
					if (currentTech.IsPlayer)
					{
						currentTech.SetTeam(-2);
						continue;
					}
					currentTech.SetTeam(Singleton.Manager<ManSpawn>.inst.GenerateAutomaticTeamID(-1));
					currentTech.AI.SetOldBehaviour();
				}
			}
			if (CheatCompleted(CheatCode.TogglePopInfo))
			{
				ManPop.s_DebugShowInfo = !ManPop.s_DebugShowInfo;
			}
			if (CheatCompleted(CheatCode.ToggleTradingStationDebug))
			{
				ManWorld.ToggleDebugTradingStations();
			}
			if (CheatCompleted(CheatCode.TogglePowerLevels))
			{
				ModuleEnergyStore.s_DebugShowPowerLevels = !ModuleEnergyStore.s_DebugShowPowerLevels;
			}
			if (CheatCompleted(CheatCode.UnlockBlocks))
			{
				BlockUnlockTable blockUnlockTable = Singleton.Manager<ManLicenses>.inst.GetBlockUnlockTable();
				for (int num = 0; num < Singleton.Manager<ManPurchases>.inst.AvailableCorporations.Count; num++)
				{
					FactionSubTypes corporation = Singleton.Manager<ManPurchases>.inst.AvailableCorporations[num];
					List<BlockTypes> allBlocksForFaction = blockUnlockTable.GetAllBlocksForFaction(corporation);
					for (int num2 = 0; num2 < allBlocksForFaction.Count; num2++)
					{
						Singleton.Manager<ManLicenses>.inst.DiscoverBlock(allBlocksForFaction[num2]);
					}
				}
			}
			if (CheatCompleted(CheatCode.PauseEnemies))
			{
				EnemiesPaused = !EnemiesPaused;
			}
			if (CheatCompleted(CheatCode.CompleteEncounter))
			{
				Singleton.Manager<ManQuestLog>.inst.DebugCompleteActiveEncounter();
			}
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && CheatCompleted(CheatCode.SyncMap))
			{
				Singleton.Manager<ManMap>.inst.RequestMapData();
				Singleton.Manager<ManMap>.inst.UploadMapData();
			}
		}
		if (CheatCompleted(CheatCode.ToggleDevelopmentCheats) || CheatCompleted(CheatCode.ToggleDevelopmentCheatsPublic))
		{
			m_AllowDebugControls = !m_AllowDebugControls;
			CheatAccessChangedEvent.Send();
		}
		if (CheatCompleted(CheatCode.TogglePublicFacingCheats1) || CheatCompleted(CheatCode.TogglePublicFacingCheats2) || CheatCompleted(CheatCode.TogglePublicFacingCheats3) || CheatCompleted(CheatCode.TogglePublicFacingCheats4) || CheatCompleted(CheatCode.TogglePublicFacingCheats5))
		{
			m_AllowPartialDebug = !m_AllowPartialDebug;
			CheatAccessChangedEvent.Send();
		}
		if (AreCheatsEnabled(CheatType.Development))
		{
			if (CheatCompleted(CheatCode.Skip))
			{
				Mode<ModeMain>.inst.TutorialDisableBlockRemoval = false;
				Mode<ModeMain>.inst.TutorialDisableBlockRotation = false;
				Mode<ModeMain>.inst.TutorialLockBeam = false;
				Mode<ModeMain>.inst.TutorialLockBeamMove = false;
				Mode<ModeMain>.inst.DebugSkipTutorial = true;
				Mode<ModeMain>.inst.ReduceBlockDragReleaseSpeed = false;
				Singleton.Manager<ManHUD>.inst.Show(ManHUD.HideReason.Cheats, show: true);
				Singleton.Manager<ManOnScreenMessages>.inst.ClearAllMessages();
				Singleton.Manager<ManTechBuildingTutorial>.inst.FilterPlacementPositions(null);
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.BouncingArrow);
			}
			if (CheatCompleted(CheatCode.PauseMissionTimer))
			{
				Singleton.Manager<ManQuestLog>.inst.DebugPauseMissionTimer = true;
			}
			if (Input.GetKey(KeyCode.RightControl) && (bool)Singleton.playerTank)
			{
				Vector3 vector = Vector3.zero;
				if (Input.GetKeyDown(KeyCode.UpArrow))
				{
					vector = Vector3.forward;
				}
				else if (Input.GetKeyDown(KeyCode.DownArrow))
				{
					vector = Vector3.back;
				}
				else if (Input.GetKeyDown(KeyCode.LeftArrow))
				{
					vector = Vector3.left;
				}
				else if (Input.GetKeyDown(KeyCode.RightArrow))
				{
					vector = Vector3.right;
				}
				if (vector != Vector3.zero)
				{
					Vector3 vector2 = Singleton.cameraTrans.position - Singleton.playerTank.boundsCentreWorld;
					Vector3 input = Singleton.cameraTrans.rotation * vector;
					input = input.SetY(0f).normalized * m_Settings.m_DebugTeleportDistance;
					Vector3 position = Singleton.playerTank.boundsCentreWorld + input;
					Singleton.playerTank.visible.Teleport(position, Singleton.playerTank.trans.rotation);
					position = (Singleton.playerTank ? Singleton.playerTank.boundsCentreWorld : Singleton.playerPos);
					Singleton.Manager<CameraManager>.inst.ResetCamera(position + vector2, Singleton.cameraTrans.rotation);
				}
			}
			if (CheatCompleted(CheatCode.Reload))
			{
				Singleton.Manager<ManGameMode>.inst.RestartCurrentMode(reloadSave: true);
			}
			if (CheatCompleted(CheatCode.Attract))
			{
				if (Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeAttract>())
				{
					Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeMain>();
				}
				else
				{
					Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
				}
			}
			if (CheatCompleted(CheatCode.Replace))
			{
				newPlayerTankReplacesPrevious = !newPlayerTankReplacesPrevious;
				string text = (newPlayerTankReplacesPrevious ? "Replace vehicle" : "Spawn new vehicle");
				ManOnScreenMessages.OnScreenMessage message = new ManOnScreenMessages.OnScreenMessage(new string[1] { text }, ManOnScreenMessages.MessagePriority.Medium);
				Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(message, boolVal: false);
			}
			if (CheatCompleted(CheatCode.Immortal))
			{
				m_Immortal = !m_Immortal;
			}
			if (CheatCompleted(CheatCode.NoDamage))
			{
				Damageable.DamageEnabled = !Damageable.DamageEnabled;
			}
			if (CheatCompleted(CheatCode.EncounterState))
			{
				Encounter.s_DebugState = !Encounter.s_DebugState;
			}
			if (CheatCompleted(CheatCode.FlipReverseSteering))
			{
				Globals.inst.reversingSteerInversion = !Globals.inst.reversingSteerInversion;
			}
			if (CheatCompleted(CheatCode.DevelopmentBlocks))
			{
				Singleton.Manager<ManSpawn>.inst.ToggleSecretBlocksDebugSpawn();
			}
			if (CheatCompleted(CheatCode.ToggleXP))
			{
				Singleton.Manager<ManLicenses>.inst.XpDisabled = !Singleton.Manager<ManLicenses>.inst.XpDisabled;
			}
			if (CheatCompleted(CheatCode.InvaderInfo) && Singleton.Manager<ManInvasion>.inst != null)
			{
				Singleton.Manager<ManInvasion>.inst.ToggleDebugGUI();
			}
			if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
			{
				if (Input.GetKeyDown(KeyCode.KeypadPlus))
				{
					supersizedScreenshotSize++;
				}
				if (Input.GetKeyDown(KeyCode.KeypadMinus))
				{
					supersizedScreenshotSize--;
				}
			}
			if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.E))
			{
				Singleton.Manager<ManPop>.inst.DebugForceSpawn();
			}
			if (CheatCompleted(CheatCode.SpawnAI))
			{
				Singleton.Manager<ManSpawn>.inst.TestSpawnFriendlyTech(FriendlyAIName);
			}
			if (CheatCompleted(CheatCode.ResetAchievements))
			{
				Singleton.Manager<ManAchievements>.inst.ResetAllAchievements();
			}
			if (CheatCompleted(CheatCode.NoScenery))
			{
				Singleton.Manager<ManWorld>.inst.DisableSceneryObjectsForTesting();
			}
			if (CheatCompleted(CheatCode.ShowTechCost) && (bool)Singleton.Manager<HUDTechList>.inst)
			{
				Singleton.Manager<HUDTechList>.inst.gameObject.SetActive(!Singleton.Manager<HUDTechList>.inst.gameObject.activeSelf);
				Singleton.Manager<HUDTechList>.inst.UpdateTechs();
			}
			if (CheatCompleted(CheatCode.SkipMultiplayerTimer) && (bool)Singleton.Manager<ManNetwork>.inst && (bool)Singleton.Manager<ManNetwork>.inst.NetController)
			{
				Singleton.Manager<ManNetwork>.inst.NetController.CheatSkipTimerToEnd();
			}
			if (CheatCompleted(CheatCode.MaxPower))
			{
				ModuleEnergyStore.CheatFillMaxPower = !ModuleEnergyStore.CheatFillMaxPower;
			}
			if (CheatCompleted(CheatCode.ThrowException))
			{
				throw new CheatCrashException();
			}
			if (CheatCompleted(CheatCode.SpawnAllMissions))
			{
				m_SpawnAllMissions = !m_SpawnAllMissions;
			}
			if (CheatCompleted(CheatCode.RemoveTechLoaderRestrictions))
			{
				RemoveTechLoaderRestrictions = !RemoveTechLoaderRestrictions;
				Singleton.Manager<ManSnapshots>.inst.ResetModeAvailability();
			}
			if (CheatCompleted(CheatCode.UnlimitedShopBlocks))
			{
				m_UnlimitedShopBlocks = !m_UnlimitedShopBlocks;
			}
			if (CheatCompleted(CheatCode.AllBlocksInInventory))
			{
				ToggleShowAllBlocksInInventory();
				if (Singleton.playerTank != null)
				{
					Singleton.playerTank.beam.EnableBeam(enable: false);
				}
			}
		}
		if (!DisableCheatInput && m_AllowDebugControls)
		{
			if (CheatCompleted(CheatCode.NetDump))
			{
				ManNetwork.NetDumpObjects();
			}
			if (CheatCompleted(CheatCode.DebugPlacementIntersections))
			{
				ManTechBuilder.DebugIntersections = !ManTechBuilder.DebugIntersections;
				d.Log($"Logging intersections:{ManTechBuilder.DebugIntersections}");
			}
		}
		if (m_Immortal && (bool)Singleton.playerTank)
		{
			Singleton.playerTank.SetInvulnerable(invulnerable: true, forever: false);
			if (!ManNetwork.IsHost)
			{
				NetTech netTech = Singleton.playerTank.netTech;
				if (netTech != null)
				{
					Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.DebugSetInvulnerable, new EmptyMessage(), netTech.netId);
				}
			}
		}
		HandleReRaisingExceptionWhenRequired();
		if (!DisableCheatInput && CheatCompleted(CheatCode.ToggleFPS))
		{
			m_AccumFpsTime = 0f;
			m_ShowFps = !m_ShowFps;
			Singleton.Manager<ManUI>.inst.FpsText.gameObject.SetActive(m_ShowFps);
		}
		if (m_ShowFps)
		{
			UpdateFps();
		}
		EnagementEvent.Update();
	}

	private void HandleReRaisingExceptionWhenRequired()
	{
		if (m_ReRaiseExceptionThrown)
		{
			m_ReRaiseException = null;
			m_ReRaiseExceptionThrown = false;
		}
		else if (m_ReRaiseException != null)
		{
			m_ReRaiseExceptionThrown = true;
			throw m_ReRaiseException;
		}
	}

	private void UpdateFps()
	{
		m_AccumFpsTime += Time.unscaledDeltaTime;
		m_FramesDrawn++;
		if (m_AccumFpsTime >= fpsCounterUpdateInterval)
		{
			float num = (float)m_FramesDrawn / m_AccumFpsTime;
			m_CurrentFps = $"{num:F2} FPS";
			m_AccumFpsTime = 0f;
			m_FramesDrawn = 0;
			Singleton.Manager<ManUI>.inst.FpsText.text = m_CurrentFps;
		}
	}

	private void FixedUpdate()
	{
		if (lockTank && (bool)Singleton.playerTank)
		{
			Vector3 position = new Vector3(0f, 0.75f - Singleton.playerTank.blockBounds.min.y, 0f);
			Singleton.playerTank.trans.position = position;
			Singleton.playerTank.trans.rotation = Quaternion.identity;
			Singleton.playerTank.rbody.velocity = Vector3.zero;
		}
	}

	private void OnApplicationQuit()
	{
		isShuttingDown = true;
	}
}
