using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_GSO_1_0_CoOpCampaignInit : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public Transform Bomb;

	public TankPreset BuiltTechPreset;

	public uScript_SetCraterMissionsSpawnPos.CraterSpawnData[] CraterSpawnData = new uScript_SetCraterMissionsSpawnPos.CraterSpawnData[0];

	[Multiline(3)]
	public string DontDisableBombMeshChild = "LogoTerraTechTest";

	public Transform Explosion;

	private PositionWithFacing local_61_PositionWithFacing;

	private bool local_74_System_Boolean = true;

	private bool local_FinishedSpawning_System_Boolean;

	private bool local_LockedPause_System_Boolean;

	private bool local_MoneyGiven_System_Boolean;

	private bool local_PlayedMessage_System_Boolean;

	private bool local_StartSpawning_System_Boolean;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	public float messageStartDelay;

	public uScript_AddMessage.MessageData msgIntro;

	public int startingMoney;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_40;

	private GameObject owner_Connection_51;

	private GameObject owner_Connection_64;

	private GameObject owner_Connection_80;

	private uScript_SpawnBaseBomb logic_uScript_SpawnBaseBomb_uScript_SpawnBaseBomb_1 = new uScript_SpawnBaseBomb();

	private Transform logic_uScript_SpawnBaseBomb_prefab_1;

	private Transform logic_uScript_SpawnBaseBomb_explosionPrefab_1;

	private string logic_uScript_SpawnBaseBomb_dontDisableMeshParent_1 = "";

	private bool logic_uScript_SpawnBaseBomb_Delivered_1 = true;

	private bool logic_uScript_SpawnBaseBomb_PlayingOrFinished_1 = true;

	private uScript_SetPopulation logic_uScript_SetPopulation_uScript_SetPopulation_3 = new uScript_SetPopulation();

	private bool logic_uScript_SetPopulation_automatic_3;

	private bool logic_uScript_SetPopulation_Out_3 = true;

	private uScript_HideHUD logic_uScript_HideHUD_uScript_HideHUD_4 = new uScript_HideHUD();

	private bool logic_uScript_HideHUD_hide_4 = true;

	private bool logic_uScript_HideHUD_Out_4 = true;

	private uScript_BlockDecay logic_uScript_BlockDecay_uScript_BlockDecay_5 = new uScript_BlockDecay();

	private bool logic_uScript_BlockDecay_allow_5;

	private bool logic_uScript_BlockDecay_Out_5 = true;

	private uScript_Save logic_uScript_Save_uScript_Save_6 = new uScript_Save();

	private ManFTUE.SaveStates logic_uScript_Save_state_6 = ManFTUE.SaveStates.Disable;

	private bool logic_uScript_Save_Out_6 = true;

	private uScript_SKU logic_uScript_SKU_uScript_SKU_10 = new uScript_SKU();

	private bool logic_uScript_SKU_Show_10 = true;

	private bool logic_uScript_SKU_Demo_10 = true;

	private bool logic_uScript_SKU_Normal_10 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_11 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_11;

	private bool logic_uScriptCon_CompareBool_True_11 = true;

	private bool logic_uScriptCon_CompareBool_False_11 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_13 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_13;

	private bool logic_uScriptAct_SetBool_Out_13 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_13 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_13 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_15 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_15;

	private bool logic_uScriptAct_SetBool_Out_15 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_15 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_15 = true;

	private uScript_HoldFadeUntilSceneryPopulated logic_uScript_HoldFadeUntilSceneryPopulated_uScript_HoldFadeUntilSceneryPopulated_17 = new uScript_HoldFadeUntilSceneryPopulated();

	private bool logic_uScript_HoldFadeUntilSceneryPopulated_Out_17 = true;

	private uScript_SetCraterMissionsSpawnPos logic_uScript_SetCraterMissionsSpawnPos_uScript_SetCraterMissionsSpawnPos_18 = new uScript_SetCraterMissionsSpawnPos();

	private uScript_SetCraterMissionsSpawnPos.CraterSpawnData[] logic_uScript_SetCraterMissionsSpawnPos_spawnData_18 = new uScript_SetCraterMissionsSpawnPos.CraterSpawnData[0];

	private bool logic_uScript_SetCraterMissionsSpawnPos_Finished_18 = true;

	private uScript_IsSceneryPopulating logic_uScript_IsSceneryPopulating_uScript_IsSceneryPopulating_20 = new uScript_IsSceneryPopulating();

	private bool logic_uScript_IsSceneryPopulating_True_20 = true;

	private bool logic_uScript_IsSceneryPopulating_False_20 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_21;

	private bool logic_uScriptCon_CompareBool_True_21 = true;

	private bool logic_uScriptCon_CompareBool_False_21 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_23 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_23;

	private bool logic_uScriptAct_SetBool_Out_23 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_23 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_23 = true;

	private uScript_CameraFade logic_uScript_CameraFade_uScript_CameraFade_25 = new uScript_CameraFade();

	private bool logic_uScript_CameraFade_enableFade_25 = true;

	private bool logic_uScript_CameraFade_Out_25 = true;

	private uScript_CameraFade logic_uScript_CameraFade_uScript_CameraFade_26 = new uScript_CameraFade();

	private bool logic_uScript_CameraFade_enableFade_26;

	private bool logic_uScript_CameraFade_Out_26 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_27 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_27;

	private bool logic_uScriptAct_SetBool_Out_27 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_27 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_27 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_29;

	private bool logic_uScriptCon_CompareBool_True_29 = true;

	private bool logic_uScriptCon_CompareBool_False_29 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_31 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_31;

	private bool logic_uScriptAct_SetBool_Out_31 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_31 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_31 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_32 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_32;

	private bool logic_uScriptAct_SetBool_Out_32 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_32 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_32 = true;

	private uScript_CameraFade logic_uScript_CameraFade_uScript_CameraFade_35 = new uScript_CameraFade();

	private bool logic_uScript_CameraFade_enableFade_35 = true;

	private bool logic_uScript_CameraFade_Out_35 = true;

	private uScript_AddMoney logic_uScript_AddMoney_uScript_AddMoney_36 = new uScript_AddMoney();

	private int logic_uScript_AddMoney_amount_36;

	private bool logic_uScript_AddMoney_Out_36 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_38 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_38;

	private bool logic_uScript_Wait_repeat_38;

	private bool logic_uScript_Wait_Waited_38 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_41 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_41;

	private bool logic_uScript_FinishEncounter_Out_41 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_42 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_42;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_42;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_42;

	private bool logic_uScript_AddMessage_Out_42 = true;

	private bool logic_uScript_AddMessage_Shown_42 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_46 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_46;

	private bool logic_uScriptAct_SetBool_Out_46 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_46 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_46 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_48;

	private bool logic_uScriptCon_CompareBool_True_48 = true;

	private bool logic_uScriptCon_CompareBool_False_48 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_49 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_49;

	private bool logic_uScriptAct_SetBool_Out_49 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_49 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_49 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_50 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_50 = true;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_50 = ManPauseGame.DisablePauseReason.UScriptNewGameIntro;

	private bool logic_uScript_LockPause_Out_50 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_53 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_53;

	private bool logic_uScriptAct_SetBool_Out_53 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_53 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_53 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_55;

	private bool logic_uScriptCon_CompareBool_True_55 = true;

	private bool logic_uScriptCon_CompareBool_False_55 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_56 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_56;

	private bool logic_uScriptAct_SetBool_Out_56 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_56 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_56 = true;

	private uScript_AddMoney logic_uScript_AddMoney_uScript_AddMoney_58 = new uScript_AddMoney();

	private int logic_uScript_AddMoney_amount_58;

	private bool logic_uScript_AddMoney_Out_58 = true;

	private uScript_SpawnPresetAsPlayer logic_uScript_SpawnPresetAsPlayer_uScript_SpawnPresetAsPlayer_60 = new uScript_SpawnPresetAsPlayer();

	private TankPreset logic_uScript_SpawnPresetAsPlayer_preset_60;

	private PositionWithFacing logic_uScript_SpawnPresetAsPlayer_playerPosition_60;

	private bool logic_uScript_SpawnPresetAsPlayer_Done_60 = true;

	private bool logic_uScript_SpawnPresetAsPlayer_NotDone_60 = true;

	private uScript_GetStartPosition logic_uScript_GetStartPosition_uScript_GetStartPosition_62 = new uScript_GetStartPosition();

	private PositionWithFacing logic_uScript_GetStartPosition_Return_62;

	private bool logic_uScript_GetStartPosition_Out_62 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_65 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_65;

	private bool logic_uScript_FinishEncounter_Out_65 = true;

	private uScript_EnableMusic logic_uScript_EnableMusic_uScript_EnableMusic_66 = new uScript_EnableMusic();

	private bool logic_uScript_EnableMusic_Out_66 = true;

	private uScript_Save logic_uScript_Save_uScript_Save_67 = new uScript_Save();

	private ManFTUE.SaveStates logic_uScript_Save_state_67 = ManFTUE.SaveStates.Normal;

	private bool logic_uScript_Save_Out_67 = true;

	private uScript_ShowPlayerProfile logic_uScript_ShowPlayerProfile_uScript_ShowPlayerProfile_68 = new uScript_ShowPlayerProfile();

	private bool logic_uScript_ShowPlayerProfile_Out_68 = true;

	private uScript_SetVendorsEnabled logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_69 = new uScript_SetVendorsEnabled();

	private bool logic_uScript_SetVendorsEnabled_enableShop_69 = true;

	private bool logic_uScript_SetVendorsEnabled_enableMissionBoard_69 = true;

	private bool logic_uScript_SetVendorsEnabled_enableSelling_69 = true;

	private bool logic_uScript_SetVendorsEnabled_enableSCU_69;

	private bool logic_uScript_SetVendorsEnabled_enableCharging_69 = true;

	private bool logic_uScript_SetVendorsEnabled_Out_69 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_71 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_71 = uScript_ChangeBuildingOptions.BuildingOptions.Detach;

	private bool logic_uScript_ChangeBuildingOptions_allow_71 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_71 = true;

	private uScript_SetPlacementFilter logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_72 = new uScript_SetPlacementFilter();

	private Vector3[] logic_uScript_SetPlacementFilter_placementFilter_72 = new Vector3[0];

	private bool logic_uScript_SetPlacementFilter_Out_72 = true;

	private uScript_SetPopulationActive logic_uScript_SetPopulationActive_uScript_SetPopulationActive_73 = new uScript_SetPopulationActive();

	private bool logic_uScript_SetPopulationActive_active_73;

	private bool logic_uScript_SetPopulationActive_Out_73 = true;

	private uScript_RemoveSceneryAtPosition logic_uScript_RemoveSceneryAtPosition_uScript_RemoveSceneryAtPosition_75 = new uScript_RemoveSceneryAtPosition();

	private Vector3 logic_uScript_RemoveSceneryAtPosition_position_75 = new Vector3(0f, 0f, 0f);

	private float logic_uScript_RemoveSceneryAtPosition_radius_75 = 50f;

	private bool logic_uScript_RemoveSceneryAtPosition_preventChunksSpawning_75 = true;

	private bool logic_uScript_RemoveSceneryAtPosition_Out_75 = true;

	private uScript_SetQuestLogVisible logic_uScript_SetQuestLogVisible_uScript_SetQuestLogVisible_78 = new uScript_SetQuestLogVisible();

	private bool logic_uScript_SetQuestLogVisible_visible_78 = true;

	private bool logic_uScript_SetQuestLogVisible_Out_78 = true;

	private uScript_SetMissionsVisibleInHud logic_uScript_SetMissionsVisibleInHud_uScript_SetMissionsVisibleInHud_79 = new uScript_SetMissionsVisibleInHud();

	private bool logic_uScript_SetMissionsVisibleInHud_visible_79 = true;

	private bool logic_uScript_SetMissionsVisibleInHud_Out_79 = true;

	private uScript_ShowQuestLog logic_uScript_ShowQuestLog_uScript_ShowQuestLog_81 = new uScript_ShowQuestLog();

	private GameObject logic_uScript_ShowQuestLog_owner_81;

	private bool logic_uScript_ShowQuestLog_Out_81 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_82 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_82 = 0.25f;

	private bool logic_uScript_Wait_repeat_82;

	private bool logic_uScript_Wait_Waited_82 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
			if (null != owner_Connection_8)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_8.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_8.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_7;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_7;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_7;
				}
			}
		}
		if (null == owner_Connection_40 || !m_RegisteredForEvents)
		{
			owner_Connection_40 = parentGameObject;
		}
		if (null == owner_Connection_51 || !m_RegisteredForEvents)
		{
			owner_Connection_51 = parentGameObject;
			if (null != owner_Connection_51)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_51.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_51.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_52;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_52;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_52;
				}
			}
		}
		if (null == owner_Connection_64 || !m_RegisteredForEvents)
		{
			owner_Connection_64 = parentGameObject;
		}
		if (null == owner_Connection_80 || !m_RegisteredForEvents)
		{
			owner_Connection_80 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_8)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_8.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_8.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_7;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_7;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_7;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_51)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_51.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_51.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_52;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_52;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_52;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_8)
		{
			uScript_SaveLoad component = owner_Connection_8.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_7;
				component.LoadEvent -= Instance_LoadEvent_7;
				component.RestartEvent -= Instance_RestartEvent_7;
			}
		}
		if (null != owner_Connection_51)
		{
			uScript_EncounterUpdate component2 = owner_Connection_51.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_52;
				component2.OnSuspend -= Instance_OnSuspend_52;
				component2.OnResume -= Instance_OnResume_52;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SpawnBaseBomb_uScript_SpawnBaseBomb_1.SetParent(g);
		logic_uScript_SetPopulation_uScript_SetPopulation_3.SetParent(g);
		logic_uScript_HideHUD_uScript_HideHUD_4.SetParent(g);
		logic_uScript_BlockDecay_uScript_BlockDecay_5.SetParent(g);
		logic_uScript_Save_uScript_Save_6.SetParent(g);
		logic_uScript_SKU_uScript_SKU_10.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_11.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_13.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_15.SetParent(g);
		logic_uScript_HoldFadeUntilSceneryPopulated_uScript_HoldFadeUntilSceneryPopulated_17.SetParent(g);
		logic_uScript_SetCraterMissionsSpawnPos_uScript_SetCraterMissionsSpawnPos_18.SetParent(g);
		logic_uScript_IsSceneryPopulating_uScript_IsSceneryPopulating_20.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_23.SetParent(g);
		logic_uScript_CameraFade_uScript_CameraFade_25.SetParent(g);
		logic_uScript_CameraFade_uScript_CameraFade_26.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_27.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.SetParent(g);
		logic_uScript_CameraFade_uScript_CameraFade_35.SetParent(g);
		logic_uScript_AddMoney_uScript_AddMoney_36.SetParent(g);
		logic_uScript_Wait_uScript_Wait_38.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_41.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_42.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_46.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_50.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_56.SetParent(g);
		logic_uScript_AddMoney_uScript_AddMoney_58.SetParent(g);
		logic_uScript_SpawnPresetAsPlayer_uScript_SpawnPresetAsPlayer_60.SetParent(g);
		logic_uScript_GetStartPosition_uScript_GetStartPosition_62.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_65.SetParent(g);
		logic_uScript_EnableMusic_uScript_EnableMusic_66.SetParent(g);
		logic_uScript_Save_uScript_Save_67.SetParent(g);
		logic_uScript_ShowPlayerProfile_uScript_ShowPlayerProfile_68.SetParent(g);
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_69.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_71.SetParent(g);
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_72.SetParent(g);
		logic_uScript_SetPopulationActive_uScript_SetPopulationActive_73.SetParent(g);
		logic_uScript_RemoveSceneryAtPosition_uScript_RemoveSceneryAtPosition_75.SetParent(g);
		logic_uScript_SetQuestLogVisible_uScript_SetQuestLogVisible_78.SetParent(g);
		logic_uScript_SetMissionsVisibleInHud_uScript_SetMissionsVisibleInHud_79.SetParent(g);
		logic_uScript_ShowQuestLog_uScript_ShowQuestLog_81.SetParent(g);
		logic_uScript_Wait_uScript_Wait_82.SetParent(g);
		owner_Connection_8 = parentGameObject;
		owner_Connection_40 = parentGameObject;
		owner_Connection_51 = parentGameObject;
		owner_Connection_64 = parentGameObject;
		owner_Connection_80 = parentGameObject;
	}

	public void Awake()
	{
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_uScript_SetCraterMissionsSpawnPos_uScript_SetCraterMissionsSpawnPos_18.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_SpawnBaseBomb_uScript_SpawnBaseBomb_1.OnDisable();
		logic_uScript_Save_uScript_Save_6.OnDisable();
		logic_uScript_Wait_uScript_Wait_38.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_42.OnDisable();
		logic_uScript_SpawnPresetAsPlayer_uScript_SpawnPresetAsPlayer_60.OnDisable();
		logic_uScript_Save_uScript_Save_67.OnDisable();
		logic_uScript_Wait_uScript_Wait_82.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
	}

	public void OnDestroy()
	{
	}

	private void Instance_SaveEvent_7(object o, EventArgs e)
	{
		Relay_SaveEvent_7();
	}

	private void Instance_LoadEvent_7(object o, EventArgs e)
	{
		Relay_LoadEvent_7();
	}

	private void Instance_RestartEvent_7(object o, EventArgs e)
	{
		Relay_RestartEvent_7();
	}

	private void Instance_OnUpdate_52(object o, EventArgs e)
	{
		Relay_OnUpdate_52();
	}

	private void Instance_OnSuspend_52(object o, EventArgs e)
	{
		Relay_OnSuspend_52();
	}

	private void Instance_OnResume_52(object o, EventArgs e)
	{
		Relay_OnResume_52();
	}

	private void Relay_In_1()
	{
		logic_uScript_SpawnBaseBomb_prefab_1 = Bomb;
		logic_uScript_SpawnBaseBomb_explosionPrefab_1 = Explosion;
		logic_uScript_SpawnBaseBomb_dontDisableMeshParent_1 = DontDisableBombMeshChild;
		logic_uScript_SpawnBaseBomb_uScript_SpawnBaseBomb_1.In(logic_uScript_SpawnBaseBomb_prefab_1, logic_uScript_SpawnBaseBomb_explosionPrefab_1, logic_uScript_SpawnBaseBomb_dontDisableMeshParent_1);
		if (logic_uScript_SpawnBaseBomb_uScript_SpawnBaseBomb_1.Delivered)
		{
			Relay_Succeed_41();
		}
	}

	private void Relay_In_3()
	{
		logic_uScript_SetPopulation_uScript_SetPopulation_3.In(logic_uScript_SetPopulation_automatic_3);
		if (logic_uScript_SetPopulation_uScript_SetPopulation_3.Out)
		{
			Relay_In_48();
		}
	}

	private void Relay_In_4()
	{
		logic_uScript_HideHUD_uScript_HideHUD_4.In(logic_uScript_HideHUD_hide_4);
		if (logic_uScript_HideHUD_uScript_HideHUD_4.Out)
		{
			Relay_In_6();
		}
	}

	private void Relay_In_5()
	{
		logic_uScript_BlockDecay_uScript_BlockDecay_5.In(logic_uScript_BlockDecay_allow_5);
		if (logic_uScript_BlockDecay_uScript_BlockDecay_5.Out)
		{
			Relay_In_3();
		}
	}

	private void Relay_In_6()
	{
		logic_uScript_Save_uScript_Save_6.In(logic_uScript_Save_state_6);
		if (logic_uScript_Save_uScript_Save_6.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_SaveEvent_7()
	{
	}

	private void Relay_LoadEvent_7()
	{
	}

	private void Relay_RestartEvent_7()
	{
		Relay_False_15();
	}

	private void Relay_In_10()
	{
		logic_uScript_SKU_uScript_SKU_10.In();
		bool show = logic_uScript_SKU_uScript_SKU_10.Show;
		bool demo = logic_uScript_SKU_uScript_SKU_10.Demo;
		bool normal = logic_uScript_SKU_uScript_SKU_10.Normal;
		if (show)
		{
			Relay_In_21();
		}
		if (demo)
		{
			Relay_In_17();
		}
		if (normal)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_11()
	{
		logic_uScriptCon_CompareBool_Bool_11 = local_PlayedMessage_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_11.In(logic_uScriptCon_CompareBool_Bool_11);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_11.False)
		{
			Relay_In_38();
		}
	}

	private void Relay_True_13()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_13.True(out logic_uScriptAct_SetBool_Target_13);
		local_PlayedMessage_System_Boolean = logic_uScriptAct_SetBool_Target_13;
	}

	private void Relay_False_13()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_13.False(out logic_uScriptAct_SetBool_Target_13);
		local_PlayedMessage_System_Boolean = logic_uScriptAct_SetBool_Target_13;
	}

	private void Relay_True_15()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_15.True(out logic_uScriptAct_SetBool_Target_15);
		local_PlayedMessage_System_Boolean = logic_uScriptAct_SetBool_Target_15;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_15.Out)
		{
			Relay_False_31();
		}
	}

	private void Relay_False_15()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_15.False(out logic_uScriptAct_SetBool_Target_15);
		local_PlayedMessage_System_Boolean = logic_uScriptAct_SetBool_Target_15;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_15.Out)
		{
			Relay_False_31();
		}
	}

	private void Relay_In_17()
	{
		logic_uScript_HoldFadeUntilSceneryPopulated_uScript_HoldFadeUntilSceneryPopulated_17.In();
		if (logic_uScript_HoldFadeUntilSceneryPopulated_uScript_HoldFadeUntilSceneryPopulated_17.Out)
		{
			Relay_Succeed_41();
		}
	}

	private void Relay_In_18()
	{
		int num = 0;
		Array craterSpawnData = CraterSpawnData;
		if (logic_uScript_SetCraterMissionsSpawnPos_spawnData_18.Length != num + craterSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SetCraterMissionsSpawnPos_spawnData_18, num + craterSpawnData.Length);
		}
		Array.Copy(craterSpawnData, 0, logic_uScript_SetCraterMissionsSpawnPos_spawnData_18, num, craterSpawnData.Length);
		num += craterSpawnData.Length;
		logic_uScript_SetCraterMissionsSpawnPos_uScript_SetCraterMissionsSpawnPos_18.In(logic_uScript_SetCraterMissionsSpawnPos_spawnData_18);
		if (logic_uScript_SetCraterMissionsSpawnPos_uScript_SetCraterMissionsSpawnPos_18.Finished)
		{
			Relay_True_27();
		}
	}

	private void Relay_In_20()
	{
		logic_uScript_IsSceneryPopulating_uScript_IsSceneryPopulating_20.In();
		if (logic_uScript_IsSceneryPopulating_uScript_IsSceneryPopulating_20.False)
		{
			Relay_True_23();
		}
	}

	private void Relay_In_21()
	{
		logic_uScriptCon_CompareBool_Bool_21 = local_StartSpawning_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.In(logic_uScriptCon_CompareBool_Bool_21);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.False;
		if (num)
		{
			Relay_In_29();
		}
		if (flag)
		{
			Relay_In_35();
		}
	}

	private void Relay_True_23()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_23.True(out logic_uScriptAct_SetBool_Target_23);
		local_StartSpawning_System_Boolean = logic_uScriptAct_SetBool_Target_23;
	}

	private void Relay_False_23()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_23.False(out logic_uScriptAct_SetBool_Target_23);
		local_StartSpawning_System_Boolean = logic_uScriptAct_SetBool_Target_23;
	}

	private void Relay_In_25()
	{
		logic_uScript_CameraFade_uScript_CameraFade_25.In(logic_uScript_CameraFade_enableFade_25);
		if (logic_uScript_CameraFade_uScript_CameraFade_25.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_In_26()
	{
		logic_uScript_CameraFade_uScript_CameraFade_26.In(logic_uScript_CameraFade_enableFade_26);
		if (logic_uScript_CameraFade_uScript_CameraFade_26.Out)
		{
			Relay_In_1();
			Relay_In_11();
		}
	}

	private void Relay_True_27()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_27.True(out logic_uScriptAct_SetBool_Target_27);
		local_FinishedSpawning_System_Boolean = logic_uScriptAct_SetBool_Target_27;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_27.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_False_27()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_27.False(out logic_uScriptAct_SetBool_Target_27);
		local_FinishedSpawning_System_Boolean = logic_uScriptAct_SetBool_Target_27;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_27.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_In_29()
	{
		logic_uScriptCon_CompareBool_Bool_29 = local_FinishedSpawning_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.In(logic_uScriptCon_CompareBool_Bool_29);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.False;
		if (num)
		{
			Relay_In_26();
		}
		if (flag)
		{
			Relay_In_25();
		}
	}

	private void Relay_True_31()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.True(out logic_uScriptAct_SetBool_Target_31);
		local_StartSpawning_System_Boolean = logic_uScriptAct_SetBool_Target_31;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_31.Out)
		{
			Relay_False_32();
		}
	}

	private void Relay_False_31()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.False(out logic_uScriptAct_SetBool_Target_31);
		local_StartSpawning_System_Boolean = logic_uScriptAct_SetBool_Target_31;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_31.Out)
		{
			Relay_False_32();
		}
	}

	private void Relay_True_32()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.True(out logic_uScriptAct_SetBool_Target_32);
		local_FinishedSpawning_System_Boolean = logic_uScriptAct_SetBool_Target_32;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_32.Out)
		{
			Relay_False_46();
		}
	}

	private void Relay_False_32()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.False(out logic_uScriptAct_SetBool_Target_32);
		local_FinishedSpawning_System_Boolean = logic_uScriptAct_SetBool_Target_32;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_32.Out)
		{
			Relay_False_46();
		}
	}

	private void Relay_In_35()
	{
		logic_uScript_CameraFade_uScript_CameraFade_35.In(logic_uScript_CameraFade_enableFade_35);
		if (logic_uScript_CameraFade_uScript_CameraFade_35.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_In_36()
	{
		logic_uScript_AddMoney_amount_36 = startingMoney;
		logic_uScript_AddMoney_uScript_AddMoney_36.In(logic_uScript_AddMoney_amount_36);
		if (logic_uScript_AddMoney_uScript_AddMoney_36.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_38()
	{
		logic_uScript_Wait_seconds_38 = messageStartDelay;
		logic_uScript_Wait_uScript_Wait_38.In(logic_uScript_Wait_seconds_38, logic_uScript_Wait_repeat_38);
		if (logic_uScript_Wait_uScript_Wait_38.Waited)
		{
			Relay_In_42();
		}
	}

	private void Relay_Succeed_41()
	{
		logic_uScript_FinishEncounter_owner_41 = owner_Connection_40;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_41.Succeed(logic_uScript_FinishEncounter_owner_41);
	}

	private void Relay_Fail_41()
	{
		logic_uScript_FinishEncounter_owner_41 = owner_Connection_40;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_41.Fail(logic_uScript_FinishEncounter_owner_41);
	}

	private void Relay_In_42()
	{
		logic_uScript_AddMessage_messageData_42 = msgIntro;
		logic_uScript_AddMessage_speaker_42 = messageSpeaker;
		logic_uScript_AddMessage_Return_42 = logic_uScript_AddMessage_uScript_AddMessage_42.In(logic_uScript_AddMessage_messageData_42, logic_uScript_AddMessage_speaker_42);
		if (logic_uScript_AddMessage_uScript_AddMessage_42.Out)
		{
			Relay_True_13();
		}
	}

	private void Relay_True_46()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_46.True(out logic_uScriptAct_SetBool_Target_46);
		local_MoneyGiven_System_Boolean = logic_uScriptAct_SetBool_Target_46;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_46.Out)
		{
			Relay_False_56();
		}
	}

	private void Relay_False_46()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_46.False(out logic_uScriptAct_SetBool_Target_46);
		local_MoneyGiven_System_Boolean = logic_uScriptAct_SetBool_Target_46;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_46.Out)
		{
			Relay_False_56();
		}
	}

	private void Relay_In_48()
	{
		logic_uScriptCon_CompareBool_Bool_48 = local_MoneyGiven_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.In(logic_uScriptCon_CompareBool_Bool_48);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.False;
		if (num)
		{
			Relay_In_10();
		}
		if (flag)
		{
			Relay_True_49();
		}
	}

	private void Relay_True_49()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.True(out logic_uScriptAct_SetBool_Target_49);
		local_MoneyGiven_System_Boolean = logic_uScriptAct_SetBool_Target_49;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_49.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_False_49()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.False(out logic_uScriptAct_SetBool_Target_49);
		local_MoneyGiven_System_Boolean = logic_uScriptAct_SetBool_Target_49;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_49.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_In_50()
	{
		logic_uScript_LockPause_uScript_LockPause_50.In(logic_uScript_LockPause_lockPause_50, logic_uScript_LockPause_disabledReason_50);
		if (logic_uScript_LockPause_uScript_LockPause_50.Out)
		{
			Relay_In_4();
		}
	}

	private void Relay_OnUpdate_52()
	{
		Relay_In_75();
	}

	private void Relay_OnSuspend_52()
	{
	}

	private void Relay_OnResume_52()
	{
	}

	private void Relay_True_53()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.True(out logic_uScriptAct_SetBool_Target_53);
		local_LockedPause_System_Boolean = logic_uScriptAct_SetBool_Target_53;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_53.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_False_53()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.False(out logic_uScriptAct_SetBool_Target_53);
		local_LockedPause_System_Boolean = logic_uScriptAct_SetBool_Target_53;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_53.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_55()
	{
		logic_uScriptCon_CompareBool_Bool_55 = local_LockedPause_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.In(logic_uScriptCon_CompareBool_Bool_55);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.False;
		if (num)
		{
			Relay_In_4();
		}
		if (flag)
		{
			Relay_True_53();
		}
	}

	private void Relay_True_56()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_56.True(out logic_uScriptAct_SetBool_Target_56);
		local_LockedPause_System_Boolean = logic_uScriptAct_SetBool_Target_56;
	}

	private void Relay_False_56()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_56.False(out logic_uScriptAct_SetBool_Target_56);
		local_LockedPause_System_Boolean = logic_uScriptAct_SetBool_Target_56;
	}

	private void Relay_In_58()
	{
		logic_uScript_AddMoney_amount_58 = startingMoney;
		logic_uScript_AddMoney_uScript_AddMoney_58.In(logic_uScript_AddMoney_amount_58);
		if (logic_uScript_AddMoney_uScript_AddMoney_58.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_In_60()
	{
		logic_uScript_SpawnPresetAsPlayer_preset_60 = BuiltTechPreset;
		logic_uScript_SpawnPresetAsPlayer_playerPosition_60 = local_61_PositionWithFacing;
		logic_uScript_SpawnPresetAsPlayer_uScript_SpawnPresetAsPlayer_60.In(logic_uScript_SpawnPresetAsPlayer_preset_60, logic_uScript_SpawnPresetAsPlayer_playerPosition_60);
	}

	private void Relay_In_62()
	{
		logic_uScript_GetStartPosition_Return_62 = logic_uScript_GetStartPosition_uScript_GetStartPosition_62.In();
		local_61_PositionWithFacing = logic_uScript_GetStartPosition_Return_62;
		if (logic_uScript_GetStartPosition_uScript_GetStartPosition_62.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_Succeed_65()
	{
		logic_uScript_FinishEncounter_owner_65 = owner_Connection_64;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_65.Succeed(logic_uScript_FinishEncounter_owner_65);
	}

	private void Relay_Fail_65()
	{
		logic_uScript_FinishEncounter_owner_65 = owner_Connection_64;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_65.Fail(logic_uScript_FinishEncounter_owner_65);
	}

	private void Relay_In_66()
	{
		logic_uScript_EnableMusic_uScript_EnableMusic_66.In();
		if (logic_uScript_EnableMusic_uScript_EnableMusic_66.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_In_67()
	{
		logic_uScript_Save_uScript_Save_67.In(logic_uScript_Save_state_67);
		if (logic_uScript_Save_uScript_Save_67.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_In_68()
	{
		logic_uScript_ShowPlayerProfile_uScript_ShowPlayerProfile_68.In();
		if (logic_uScript_ShowPlayerProfile_uScript_ShowPlayerProfile_68.Out)
		{
			Relay_In_79();
		}
	}

	private void Relay_In_69()
	{
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_69.In(logic_uScript_SetVendorsEnabled_enableShop_69, logic_uScript_SetVendorsEnabled_enableMissionBoard_69, logic_uScript_SetVendorsEnabled_enableSelling_69, logic_uScript_SetVendorsEnabled_enableSCU_69, logic_uScript_SetVendorsEnabled_enableCharging_69);
		if (logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_69.Out)
		{
			Relay_In_66();
		}
	}

	private void Relay_In_71()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_71.In(logic_uScript_ChangeBuildingOptions_change_71, logic_uScript_ChangeBuildingOptions_allow_71);
	}

	private void Relay_In_72()
	{
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_72.In(logic_uScript_SetPlacementFilter_placementFilter_72);
		if (logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_72.Out)
		{
			Relay_In_71();
		}
	}

	private void Relay_SetActive_73()
	{
		logic_uScript_SetPopulationActive_active_73 = local_74_System_Boolean;
		logic_uScript_SetPopulationActive_uScript_SetPopulationActive_73.SetActive(logic_uScript_SetPopulationActive_active_73);
		if (logic_uScript_SetPopulationActive_uScript_SetPopulationActive_73.Out)
		{
			Relay_In_82();
		}
	}

	private void Relay_In_75()
	{
		logic_uScript_RemoveSceneryAtPosition_uScript_RemoveSceneryAtPosition_75.In(logic_uScript_RemoveSceneryAtPosition_position_75, logic_uScript_RemoveSceneryAtPosition_radius_75, logic_uScript_RemoveSceneryAtPosition_preventChunksSpawning_75);
		if (logic_uScript_RemoveSceneryAtPosition_uScript_RemoveSceneryAtPosition_75.Out)
		{
			Relay_In_58();
		}
	}

	private void Relay_In_78()
	{
		logic_uScript_SetQuestLogVisible_uScript_SetQuestLogVisible_78.In(logic_uScript_SetQuestLogVisible_visible_78);
		if (logic_uScript_SetQuestLogVisible_uScript_SetQuestLogVisible_78.Out)
		{
			Relay_In_81();
		}
	}

	private void Relay_In_79()
	{
		logic_uScript_SetMissionsVisibleInHud_uScript_SetMissionsVisibleInHud_79.In(logic_uScript_SetMissionsVisibleInHud_visible_79);
		if (logic_uScript_SetMissionsVisibleInHud_uScript_SetMissionsVisibleInHud_79.Out)
		{
			Relay_In_78();
		}
	}

	private void Relay_In_81()
	{
		logic_uScript_ShowQuestLog_owner_81 = owner_Connection_80;
		logic_uScript_ShowQuestLog_uScript_ShowQuestLog_81.In(logic_uScript_ShowQuestLog_owner_81);
		if (logic_uScript_ShowQuestLog_uScript_ShowQuestLog_81.Out)
		{
			Relay_SetActive_73();
		}
	}

	private void Relay_In_82()
	{
		logic_uScript_Wait_uScript_Wait_82.In(logic_uScript_Wait_seconds_82, logic_uScript_Wait_repeat_82);
		if (logic_uScript_Wait_uScript_Wait_82.Waited)
		{
			Relay_Succeed_65();
		}
	}
}
