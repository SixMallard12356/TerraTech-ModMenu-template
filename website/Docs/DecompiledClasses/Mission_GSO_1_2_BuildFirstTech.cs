using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_GSO_1_2_BuildFirstTech : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public BlockTypes blockTypeDrill;

	public BlockTypes blockTypeGun;

	public BlockTypes blockTypeLaser;

	public BlockTypes blockTypeStandard;

	public BlockTypes blockTypeWheel;

	public BlockTypes blockTypeWheelStabiliser;

	public GhostBlockSpawnData[] ghostBlockDrill = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] ghostBlockGun = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] ghostBlockStandard = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] ghostBlockWheels = new GhostBlockSpawnData[0];

	private TankBlock[] local_198_TankBlockArray = new TankBlock[0];

	private TankBlock[] local_206_TankBlockArray = new TankBlock[0];

	private TankBlock[] local_212_TankBlockArray = new TankBlock[0];

	private ManOnScreenMessages.OnScreenMessage local_361_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_363_ManOnScreenMessages_OnScreenMessage;

	private bool local_CabInBeam_System_Boolean;

	private bool local_DraggingWheel_System_Boolean;

	private TankBlock local_DrillBlock_TankBlock;

	private TankBlock local_GhostBlockDrill_TankBlock;

	private bool local_GhostBlockDrillSpawned_System_Boolean;

	private TankBlock local_GhostBlockGun_TankBlock;

	private bool local_GhostBlockGunSpawned_System_Boolean;

	private TankBlock local_GhostBlockStandard_TankBlock;

	private bool local_GhostBlockStandardSpawned_System_Boolean;

	private TankBlock local_GhostBlockWheel01_TankBlock;

	private TankBlock local_GhostBlockWheel02_TankBlock;

	private TankBlock local_GhostBlockWheel03_TankBlock;

	private TankBlock local_GhostBlockWheel04_TankBlock;

	private TankBlock[] local_GhostBlockWheels_TankBlockArray = new TankBlock[0];

	private bool local_GhostBlockWheelsSpawned_System_Boolean;

	private TankBlock local_GunBlock_TankBlock;

	private bool local_Init_System_Boolean;

	private float local_InitialMsgTime_System_Single;

	private bool local_Msg4OnScreen_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_MsgSkipTutorialExitBeam_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_MsgSkipTutorialExitBeam_Pad_ManOnScreenMessages_OnScreenMessage;

	private bool local_MsgSkipTutorialIntroShown_System_Boolean;

	private Tank local_PlayerTech_Tank;

	private bool local_SkipTutorialReticule_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private TankBlock local_StandardBlock_TankBlock;

	private TankBlock local_Wheel01_TankBlock;

	private TankBlock local_Wheel02_TankBlock;

	private TankBlock local_Wheel03_TankBlock;

	private TankBlock local_Wheel04_TankBlock;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	public uScript_AddMessage.MessageData msg00aSkipTutorialIntro;

	public uScript_AddMessage.MessageData msg00bSkipTutorialExitBeam;

	public uScript_AddMessage.MessageData msg00bSkipTutorialExitBeam_Pad;

	public uScript_AddMessage.MessageData msg01CabExplanation;

	public uScript_AddMessage.MessageData msg02RotateCamera;

	public uScript_AddMessage.MessageData msg02RotateCamera_Pad;

	public uScript_AddMessage.MessageData msg03AttachBlock;

	public uScript_AddMessage.MessageData msg03AttachBlock_Pad1;

	public uScript_AddMessage.MessageData msg03AttachBlock_Pad2;

	public uScript_AddMessage.MessageData msg04AttachWheels;

	public uScript_AddMessage.MessageData msg05RotateToFindWheels;

	public uScript_AddMessage.MessageData msg05RotateToFindWheels_Pad;

	public uScript_AddMessage.MessageData msg06AttachGun;

	public uScript_AddMessage.MessageData msg07AttachDrill;

	public uScript_AddMessage.MessageData msg08ExitBuildBeam;

	public uScript_AddMessage.MessageData msg08ExitBuildBeam_Pad;

	public float TimeBeforeBeam;

	private GameObject owner_Connection_32;

	private GameObject owner_Connection_33;

	private GameObject owner_Connection_34;

	private GameObject owner_Connection_36;

	private GameObject owner_Connection_39;

	private GameObject owner_Connection_41;

	private GameObject owner_Connection_43;

	private GameObject owner_Connection_64;

	private GameObject owner_Connection_79;

	private GameObject owner_Connection_82;

	private GameObject owner_Connection_87;

	private GameObject owner_Connection_91;

	private GameObject owner_Connection_108;

	private GameObject owner_Connection_265;

	private GameObject owner_Connection_292;

	private GameObject owner_Connection_293;

	private GameObject owner_Connection_295;

	private GameObject owner_Connection_296;

	private GameObject owner_Connection_297;

	private GameObject owner_Connection_302;

	private GameObject owner_Connection_309;

	private GameObject owner_Connection_311;

	private GameObject owner_Connection_315;

	private GameObject owner_Connection_319;

	private GameObject owner_Connection_321;

	private GameObject owner_Connection_330;

	private GameObject owner_Connection_345;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_0 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_0 = uScript_ChangeBuildingOptions.BuildingOptions.Detach;

	private bool logic_uScript_ChangeBuildingOptions_allow_0 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_0 = true;

	private uScript_ClearInfoOverlays logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_1 = new uScript_ClearInfoOverlays();

	private bool logic_uScript_ClearInfoOverlays_Out_1 = true;

	private uScript_SetPlacementFilter logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_2 = new uScript_SetPlacementFilter();

	private Vector3[] logic_uScript_SetPlacementFilter_placementFilter_2 = new Vector3[1]
	{
		new Vector3(0f, 0f, 1f)
	};

	private bool logic_uScript_SetPlacementFilter_Out_2 = true;

	private uScript_ToggleBuildBeam logic_uScript_ToggleBuildBeam_uScript_ToggleBuildBeam_3 = new uScript_ToggleBuildBeam();

	private bool logic_uScript_ToggleBuildBeam_active_3 = true;

	private bool logic_uScript_ToggleBuildBeam_Out_3 = true;

	private uScript_IsPlayerInBeam logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_4 = new uScript_IsPlayerInBeam();

	private bool logic_uScript_IsPlayerInBeam_True_4 = true;

	private bool logic_uScript_IsPlayerInBeam_False_4 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_5 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_5 = uScript_ChangeBuildingOptions.BuildingOptions.ToggleBeam;

	private bool logic_uScript_ChangeBuildingOptions_allow_5 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_5 = true;

	private uScript_ClearInfoOverlays logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_6 = new uScript_ClearInfoOverlays();

	private bool logic_uScript_ClearInfoOverlays_Out_6 = true;

	private uScript_SetPlacementFilter logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_7 = new uScript_SetPlacementFilter();

	private Vector3[] logic_uScript_SetPlacementFilter_placementFilter_7 = new Vector3[4]
	{
		new Vector3(1f, 0f, 0f),
		new Vector3(1f, 0f, -1f),
		new Vector3(-1f, 0f, 0f),
		new Vector3(-1f, 0f, -1f)
	};

	private bool logic_uScript_SetPlacementFilter_Out_7 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_8 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_8 = uScript_ChangeBuildingOptions.BuildingOptions.Detach;

	private bool logic_uScript_ChangeBuildingOptions_allow_8;

	private bool logic_uScript_ChangeBuildingOptions_Out_8 = true;

	private uScript_DoesPlayerTankHaveBlock logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_9 = new uScript_DoesPlayerTankHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerTankHaveBlock_blockType_9;

	private int logic_uScript_DoesPlayerTankHaveBlock_amount_9 = 1;

	private bool logic_uScript_DoesPlayerTankHaveBlock_True_9 = true;

	private bool logic_uScript_DoesPlayerTankHaveBlock_False_9 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_10 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_10;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_10 = "sBlock";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_10;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_10;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_10 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_11 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_11 = uScript_ChangeBuildingOptions.BuildingOptions.ToggleBeam;

	private bool logic_uScript_ChangeBuildingOptions_allow_11;

	private bool logic_uScript_ChangeBuildingOptions_Out_11 = true;

	private uScript_SetPlacementFilter logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_12 = new uScript_SetPlacementFilter();

	private Vector3[] logic_uScript_SetPlacementFilter_placementFilter_12 = new Vector3[1]
	{
		new Vector3(0f, 0f, -1f)
	};

	private bool logic_uScript_SetPlacementFilter_Out_12 = true;

	private uScript_ClearInfoOverlays logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_13 = new uScript_ClearInfoOverlays();

	private bool logic_uScript_ClearInfoOverlays_Out_13 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_14 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_14;

	private bool logic_uScript_ChangeBuildingOptions_allow_14;

	private bool logic_uScript_ChangeBuildingOptions_Out_14 = true;

	private uScript_DoesPlayerTankHaveBlock logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_15 = new uScript_DoesPlayerTankHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerTankHaveBlock_blockType_15;

	private int logic_uScript_DoesPlayerTankHaveBlock_amount_15 = 1;

	private bool logic_uScript_DoesPlayerTankHaveBlock_True_15 = true;

	private bool logic_uScript_DoesPlayerTankHaveBlock_False_15 = true;

	private uScript_DoesPlayerTankHaveBlock logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_16 = new uScript_DoesPlayerTankHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerTankHaveBlock_blockType_16;

	private int logic_uScript_DoesPlayerTankHaveBlock_amount_16 = 4;

	private bool logic_uScript_DoesPlayerTankHaveBlock_True_16 = true;

	private bool logic_uScript_DoesPlayerTankHaveBlock_False_16 = true;

	private uScript_HideHUD logic_uScript_HideHUD_uScript_HideHUD_17 = new uScript_HideHUD();

	private bool logic_uScript_HideHUD_hide_17;

	private bool logic_uScript_HideHUD_Out_17 = true;

	private uScript_DoesPlayerTankHaveBlock logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_18 = new uScript_DoesPlayerTankHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerTankHaveBlock_blockType_18;

	private int logic_uScript_DoesPlayerTankHaveBlock_amount_18 = 1;

	private bool logic_uScript_DoesPlayerTankHaveBlock_True_18 = true;

	private bool logic_uScript_DoesPlayerTankHaveBlock_False_18 = true;

	private uScript_SetPlacementFilter logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_19 = new uScript_SetPlacementFilter();

	private Vector3[] logic_uScript_SetPlacementFilter_placementFilter_19 = new Vector3[0];

	private bool logic_uScript_SetPlacementFilter_Out_19 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_20 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_20 = uScript_ChangeBuildingOptions.BuildingOptions.ReduceDragSpeed;

	private bool logic_uScript_ChangeBuildingOptions_allow_20;

	private bool logic_uScript_ChangeBuildingOptions_Out_20 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_21 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_21 = uScript_ChangeBuildingOptions.BuildingOptions.ReduceDragSpeed;

	private bool logic_uScript_ChangeBuildingOptions_allow_21 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_21 = true;

	private uScript_HasCameraRotated logic_uScript_HasCameraRotated_uScript_HasCameraRotated_22 = new uScript_HasCameraRotated();

	private float logic_uScript_HasCameraRotated_turnCameraMinDegrees_22 = 30f;

	private bool logic_uScript_HasCameraRotated_True_22 = true;

	private bool logic_uScript_HasCameraRotated_False_22 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_24 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_24;

	private bool logic_uScript_ChangeBuildingOptions_allow_24 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_24 = true;

	private uScript_ClearInfoOverlays logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_25 = new uScript_ClearInfoOverlays();

	private bool logic_uScript_ClearInfoOverlays_Out_25 = true;

	private uScript_DoesPlayerTankHaveBlock logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_26 = new uScript_DoesPlayerTankHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerTankHaveBlock_blockType_26;

	private int logic_uScript_DoesPlayerTankHaveBlock_amount_26 = 2;

	private bool logic_uScript_DoesPlayerTankHaveBlock_True_26 = true;

	private bool logic_uScript_DoesPlayerTankHaveBlock_False_26 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_27 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_27;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_27 = "drill";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_27;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_27;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_27 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_28 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_28;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_28 = "gun";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_28;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_28;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_28 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_29;

	private uScript_HasCameraRotated logic_uScript_HasCameraRotated_uScript_HasCameraRotated_30 = new uScript_HasCameraRotated();

	private float logic_uScript_HasCameraRotated_turnCameraMinDegrees_30 = 3f;

	private bool logic_uScript_HasCameraRotated_True_30 = true;

	private bool logic_uScript_HasCameraRotated_False_30 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_31 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_31;

	private bool logic_uScript_RemoveOnScreenMessage_instant_31;

	private bool logic_uScript_RemoveOnScreenMessage_Out_31 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_37 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_37;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_37 = "wheel01";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_37;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_37;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_37 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_38 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_38;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_38 = "wheel02";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_38;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_38;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_38 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_40 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_40;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_40 = "wheel03";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_40;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_40;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_40 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_42 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_42;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_42 = "wheel04";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_42;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_42;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_42 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_44 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_44;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_44 = ManPauseGame.DisablePauseReason.UScriptNewGameIntro;

	private bool logic_uScript_LockPause_Out_44 = true;

	private uScript_EnableMusic logic_uScript_EnableMusic_uScript_EnableMusic_45 = new uScript_EnableMusic();

	private bool logic_uScript_EnableMusic_Out_45 = true;

	private uScript_DisablePlayerInput logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_46 = new uScript_DisablePlayerInput();

	private bool logic_uScript_DisablePlayerInput_disableInput_46;

	private bool logic_uScript_DisablePlayerInput_Out_46 = true;

	private uScript_Save logic_uScript_Save_uScript_Save_47 = new uScript_Save();

	private ManFTUE.SaveStates logic_uScript_Save_state_47 = ManFTUE.SaveStates.Normal;

	private bool logic_uScript_Save_Out_47 = true;

	private uScript_SKU logic_uScript_SKU_uScript_SKU_48 = new uScript_SKU();

	private bool logic_uScript_SKU_Show_48 = true;

	private bool logic_uScript_SKU_Demo_48 = true;

	private bool logic_uScript_SKU_Normal_48 = true;

	private uScript_IsDebugSkip logic_uScript_IsDebugSkip_uScript_IsDebugSkip_49 = new uScript_IsDebugSkip();

	private bool logic_uScript_IsDebugSkip_Out_49 = true;

	private bool logic_uScript_IsDebugSkip_True_49 = true;

	private bool logic_uScript_IsDebugSkip_False_49 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_50 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_50;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_50 = ManPauseGame.DisablePauseReason.UScriptNewGameIntro;

	private bool logic_uScript_LockPause_Out_50 = true;

	private uScript_DisablePlayerInput logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_51 = new uScript_DisablePlayerInput();

	private bool logic_uScript_DisablePlayerInput_disableInput_51;

	private bool logic_uScript_DisablePlayerInput_Out_51 = true;

	private uScript_EnableMusic logic_uScript_EnableMusic_uScript_EnableMusic_52 = new uScript_EnableMusic();

	private bool logic_uScript_EnableMusic_Out_52 = true;

	private uScript_SetGrabDistance logic_uScript_SetGrabDistance_uScript_SetGrabDistance_53 = new uScript_SetGrabDistance();

	private float logic_uScript_SetGrabDistance_dist_53;

	private bool logic_uScript_SetGrabDistance_Out_53 = true;

	private uScript_ShowPlayerProfile logic_uScript_ShowPlayerProfile_uScript_ShowPlayerProfile_54 = new uScript_ShowPlayerProfile();

	private bool logic_uScript_ShowPlayerProfile_Out_54 = true;

	private uScript_SetPlacementFilter logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_55 = new uScript_SetPlacementFilter();

	private Vector3[] logic_uScript_SetPlacementFilter_placementFilter_55 = new Vector3[1]
	{
		new Vector3(0f, 1f, 0f)
	};

	private bool logic_uScript_SetPlacementFilter_Out_55 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_56 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_56 = 5f;

	private bool logic_uScript_Wait_repeat_56;

	private bool logic_uScript_Wait_Waited_56 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_66 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_66;

	private bool logic_uScriptAct_SetBool_Out_66 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_66 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_66 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_68 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_68;

	private bool logic_uScriptAct_SetBool_Out_68 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_68 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_68 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_70 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_70;

	private bool logic_uScriptCon_CompareBool_True_70 = true;

	private bool logic_uScriptCon_CompareBool_False_70 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_72 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_72 = 3f;

	private bool logic_uScript_Wait_repeat_72;

	private bool logic_uScript_Wait_Waited_72 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_73 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_73 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_73;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_73;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_73;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_73 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_73 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_74;

	private bool logic_uScriptCon_CompareBool_True_74 = true;

	private bool logic_uScriptCon_CompareBool_False_74 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_75 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_75;

	private bool logic_uScriptAct_SetBool_Out_75 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_75 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_75 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_81 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_81 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_81;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_81;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_81;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_81 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_81 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_85 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_85;

	private bool logic_uScriptAct_SetBool_Out_85 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_85 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_85 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_86;

	private bool logic_uScriptCon_CompareBool_True_86 = true;

	private bool logic_uScriptCon_CompareBool_False_86 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_89 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_89 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_89;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_89;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_89;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_89 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_89 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_93 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_93;

	private bool logic_uScriptAct_SetBool_Out_93 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_93 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_93 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_95;

	private bool logic_uScriptCon_CompareBool_True_95 = true;

	private bool logic_uScriptCon_CompareBool_False_95 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_96 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_96 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_96;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_96;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_96;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_96 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_96 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_98 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_98 = true;

	private uScript_RemoveAllGhostBlocksOnTech logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_99 = new uScript_RemoveAllGhostBlocksOnTech();

	private Tank logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_99;

	private bool logic_uScript_RemoveAllGhostBlocksOnTech_Out_99 = true;

	private uScript_RemoveAllGhostBlocksOnTech logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_101 = new uScript_RemoveAllGhostBlocksOnTech();

	private Tank logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_101;

	private bool logic_uScript_RemoveAllGhostBlocksOnTech_Out_101 = true;

	private uScript_RemoveAllGhostBlocksOnTech logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_103 = new uScript_RemoveAllGhostBlocksOnTech();

	private Tank logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_103;

	private bool logic_uScript_RemoveAllGhostBlocksOnTech_Out_103 = true;

	private uScript_RemoveAllGhostBlocksOnTech logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_106 = new uScript_RemoveAllGhostBlocksOnTech();

	private Tank logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_106;

	private bool logic_uScript_RemoveAllGhostBlocksOnTech_Out_106 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_109 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_109;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_109 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_109 = "Stage";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_111;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_111 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_111 = "GhostBlockStandardSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_113;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_113 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_113 = "GhostBlockWheelsSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_116;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_116 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_116 = "GhostBlockGunSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_117;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_117 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_117 = "GhostBlockDrillSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_119;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_119 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_119 = "Msg4OnScreen";

	private uScriptAct_SetFloat logic_uScriptAct_SetFloat_uScriptAct_SetFloat_121 = new uScriptAct_SetFloat();

	private float logic_uScriptAct_SetFloat_Value_121;

	private float logic_uScriptAct_SetFloat_Target_121;

	private bool logic_uScriptAct_SetFloat_Out_121 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_123 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_123 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_139 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_139 = true;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_144 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_144;

	private TankBlock logic_uScript_BlockAttachedToTech_block_144;

	private bool logic_uScript_BlockAttachedToTech_True_144 = true;

	private bool logic_uScript_BlockAttachedToTech_False_144 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_147 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_147 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_147 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_147 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_147 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_149 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_149 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_149 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_149 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_149 = true;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_153 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_153;

	private TankBlock logic_uScript_BlockAttachedToTech_block_153;

	private bool logic_uScript_BlockAttachedToTech_True_153 = true;

	private bool logic_uScript_BlockAttachedToTech_False_153 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_154 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_154 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_154 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_154 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_154 = true;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_156 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_156;

	private TankBlock logic_uScript_BlockAttachedToTech_block_156;

	private bool logic_uScript_BlockAttachedToTech_True_156 = true;

	private bool logic_uScript_BlockAttachedToTech_False_156 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_158 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_158 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_158 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_158 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_158 = true;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_163 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_163;

	private TankBlock logic_uScript_BlockAttachedToTech_block_163;

	private bool logic_uScript_BlockAttachedToTech_True_163 = true;

	private bool logic_uScript_BlockAttachedToTech_False_163 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_164 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_164 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_165 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_165 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_166 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_166 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_167 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_167 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_169 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_169 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_169 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_169 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_169 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_170 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_170 = 4f;

	private bool logic_uScript_Wait_repeat_170;

	private bool logic_uScript_Wait_Waited_170 = true;

	private uScript_GetPlayerTank logic_uScript_GetPlayerTank_uScript_GetPlayerTank_172 = new uScript_GetPlayerTank();

	private Tank logic_uScript_GetPlayerTank_Return_172;

	private bool logic_uScript_GetPlayerTank_Returned_172 = true;

	private bool logic_uScript_GetPlayerTank_NotReturned_172 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_173 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_173 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_174 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_174;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_174 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_174 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_174 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_174 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_174 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_176 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_176;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_176 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_176 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_176 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_176 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_176 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_178 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_178;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_178 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_178 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_178 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_178 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_178 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_180 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_180;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_180 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_180 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_180 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_180 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_180 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_181;

	private bool logic_uScriptCon_CompareBool_True_181 = true;

	private bool logic_uScriptCon_CompareBool_False_181 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_185 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_185;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_185 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_185 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_185 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_185 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_185 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_188 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_188;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_188 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_188 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_188 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_188 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_188 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_189 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_189;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_189 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_189 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_189 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_189 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_189 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_190 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_190;

	private bool logic_uScriptAct_SetBool_Out_190 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_190 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_190 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_192 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_192 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_193 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_193 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_194 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_194 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_196 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_196 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_196 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_196 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_196 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_197 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_197 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_197 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_197 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_197 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_200 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_200 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_200;

	private TankBlock logic_uScript_AccessListBlock_value_200;

	private bool logic_uScript_AccessListBlock_Out_200 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_202 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_202 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_202 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_202 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_202 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_204 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_204 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_204 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_204 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_204 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_205 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_205 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_205;

	private TankBlock logic_uScript_AccessListBlock_value_205;

	private bool logic_uScript_AccessListBlock_Out_205 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_208 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_208 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_208 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_208 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_208 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_210 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_210 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_210 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_210 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_210 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_211 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_211 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_211;

	private TankBlock logic_uScript_AccessListBlock_value_211;

	private bool logic_uScript_AccessListBlock_Out_211 = true;

	private uScript_SetGrabDistance logic_uScript_SetGrabDistance_uScript_SetGrabDistance_214 = new uScript_SetGrabDistance();

	private float logic_uScript_SetGrabDistance_dist_214;

	private bool logic_uScript_SetGrabDistance_Out_214 = true;

	private uScript_SetInvadersActive logic_uScript_SetInvadersActive_uScript_SetInvadersActive_215 = new uScript_SetInvadersActive();

	private bool logic_uScript_SetInvadersActive_Out_215 = true;

	private uScript_SKU logic_uScript_SKU_uScript_SKU_216 = new uScript_SKU();

	private bool logic_uScript_SKU_Show_216 = true;

	private bool logic_uScript_SKU_Demo_216 = true;

	private bool logic_uScript_SKU_Normal_216 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_218 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_218 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_218;

	private TankBlock logic_uScript_AccessListBlock_value_218;

	private bool logic_uScript_AccessListBlock_Out_218 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_223 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_223 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_223 = 1;

	private TankBlock logic_uScript_AccessListBlock_value_223;

	private bool logic_uScript_AccessListBlock_Out_223 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_227 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_227 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_227 = 3;

	private TankBlock logic_uScript_AccessListBlock_value_227;

	private bool logic_uScript_AccessListBlock_Out_227 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_228 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_228 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_228 = 2;

	private TankBlock logic_uScript_AccessListBlock_value_228;

	private bool logic_uScript_AccessListBlock_Out_228 = true;

	private uScript_DoesTechHaveBlockAtPosition logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_230 = new uScript_DoesTechHaveBlockAtPosition();

	private Tank logic_uScript_DoesTechHaveBlockAtPosition_tech_230;

	private BlockTypes logic_uScript_DoesTechHaveBlockAtPosition_blockType_230;

	private Vector3 logic_uScript_DoesTechHaveBlockAtPosition_localPosition_230 = new Vector3(1f, 0f, 0f);

	private bool logic_uScript_DoesTechHaveBlockAtPosition_True_230 = true;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_False_230 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_233 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_233 = true;

	private uScript_DoesTechHaveBlockAtPosition logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_235 = new uScript_DoesTechHaveBlockAtPosition();

	private Tank logic_uScript_DoesTechHaveBlockAtPosition_tech_235;

	private BlockTypes logic_uScript_DoesTechHaveBlockAtPosition_blockType_235;

	private Vector3 logic_uScript_DoesTechHaveBlockAtPosition_localPosition_235 = new Vector3(1f, 0f, -1f);

	private bool logic_uScript_DoesTechHaveBlockAtPosition_True_235 = true;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_False_235 = true;

	private uScript_DoesTechHaveBlockAtPosition logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_238 = new uScript_DoesTechHaveBlockAtPosition();

	private Tank logic_uScript_DoesTechHaveBlockAtPosition_tech_238;

	private BlockTypes logic_uScript_DoesTechHaveBlockAtPosition_blockType_238;

	private Vector3 logic_uScript_DoesTechHaveBlockAtPosition_localPosition_238 = new Vector3(-1f, 0f, 0f);

	private bool logic_uScript_DoesTechHaveBlockAtPosition_True_238 = true;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_False_238 = true;

	private uScript_DoesTechHaveBlockAtPosition logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_241 = new uScript_DoesTechHaveBlockAtPosition();

	private Tank logic_uScript_DoesTechHaveBlockAtPosition_tech_241;

	private BlockTypes logic_uScript_DoesTechHaveBlockAtPosition_blockType_241;

	private Vector3 logic_uScript_DoesTechHaveBlockAtPosition_localPosition_241 = new Vector3(-1f, 0f, -1f);

	private bool logic_uScript_DoesTechHaveBlockAtPosition_True_241 = true;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_False_241 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_243 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_243 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_243 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_243 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_243 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_246 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_246 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_246 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_246 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_246 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_249 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_249 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_249 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_249 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_249 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_250 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_250 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_250 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_250 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_250 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_251 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_251 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_252 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_252 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_253 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_253 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_255 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_255;

	private bool logic_uScriptAct_SetBool_Out_255 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_255 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_255 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_257 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_257 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_258 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_258;

	private bool logic_uScriptCon_CompareBool_True_258 = true;

	private bool logic_uScriptCon_CompareBool_False_258 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_260;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_260 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_260 = "DraggingWheel";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_262 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_262 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_263 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_263 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_264 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_264 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_266 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_266;

	private bool logic_uScript_FinishEncounter_Out_266 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_267 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_267;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_267;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_267;

	private bool logic_uScript_AddMessage_Out_267 = true;

	private bool logic_uScript_AddMessage_Shown_267 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_270 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_270;

	private int logic_uScriptAct_AddInt_v2_B_270 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_270;

	private float logic_uScriptAct_AddInt_v2_FloatResult_270;

	private bool logic_uScriptAct_AddInt_v2_Out_270 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_272 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_272;

	private int logic_uScriptAct_AddInt_v2_B_272 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_272;

	private float logic_uScriptAct_AddInt_v2_FloatResult_272;

	private bool logic_uScriptAct_AddInt_v2_Out_272 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_274 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_274;

	private int logic_uScriptAct_AddInt_v2_B_274 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_274;

	private float logic_uScriptAct_AddInt_v2_FloatResult_274;

	private bool logic_uScriptAct_AddInt_v2_Out_274 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_277 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_277;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_277;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_277;

	private bool logic_uScript_AddMessage_Out_277 = true;

	private bool logic_uScript_AddMessage_Shown_277 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_279 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_279;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_279;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_279;

	private bool logic_uScript_AddMessage_Out_279 = true;

	private bool logic_uScript_AddMessage_Shown_279 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_283 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_283;

	private int logic_uScriptAct_AddInt_v2_B_283 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_283;

	private float logic_uScriptAct_AddInt_v2_FloatResult_283;

	private bool logic_uScriptAct_AddInt_v2_Out_283 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_284 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_284;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_284;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_284;

	private bool logic_uScript_AddMessage_Out_284 = true;

	private bool logic_uScript_AddMessage_Shown_284 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_288 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_288;

	private int logic_uScriptAct_AddInt_v2_B_288 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_288;

	private float logic_uScriptAct_AddInt_v2_FloatResult_288;

	private bool logic_uScriptAct_AddInt_v2_Out_288 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_289 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_289;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_289;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_289;

	private bool logic_uScript_AddMessage_Out_289 = true;

	private bool logic_uScript_AddMessage_Shown_289 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_294 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_294;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_294 = "skipBlock12";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_294;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_294;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_294 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_298 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_298;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_298 = "skipBlock11";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_298;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_298;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_298 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_299 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_299;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_299 = "skipBlock09";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_299;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_299;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_299 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_300 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_300;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_300 = "skipBlock02";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_300;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_300;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_300 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_301 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_301;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_301 = "skipBlock03";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_301;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_301;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_301 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_303 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_303;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_303 = "skipBlock04";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_303;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_303;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_303 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_304 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_304;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_304 = "skipBlock10";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_304;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_304;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_304 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_305 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_305;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_305 = "skipBlock01";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_305;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_305;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_305 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_310 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_310;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_310 = "skipBlock08";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_310;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_310;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_310 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_314 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_314;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_314 = "skipBlock06";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_314;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_314;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_314 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_320 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_320;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_320 = "skipBlock05";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_320;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_320;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_320 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_322 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_322;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_322 = "skipBlock07";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_322;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_322;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_322 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_327 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_327 = 6;

	private int logic_uScriptAct_SetInt_Target_327;

	private bool logic_uScriptAct_SetInt_Out_327 = true;

	private uScript_SkipTutorial logic_uScript_SkipTutorial_uScript_SkipTutorial_329 = new uScript_SkipTutorial();

	private bool logic_uScript_SkipTutorial_Yes_329 = true;

	private bool logic_uScript_SkipTutorial_No_329 = true;

	private uScript_IsPlayerInBeam logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_331 = new uScript_IsPlayerInBeam();

	private bool logic_uScript_IsPlayerInBeam_True_331 = true;

	private bool logic_uScript_IsPlayerInBeam_False_331 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_333 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_333;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_333;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_333;

	private bool logic_uScript_AddMessage_Out_333 = true;

	private bool logic_uScript_AddMessage_Shown_333 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_335 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_335;

	private bool logic_uScriptCon_CompareBool_True_335 = true;

	private bool logic_uScriptCon_CompareBool_False_335 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_337 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_337;

	private bool logic_uScriptAct_SetBool_Out_337 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_337 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_337 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_338 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_338 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_339 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_339 = uScript_ChangeBuildingOptions.BuildingOptions.ToggleBeam;

	private bool logic_uScript_ChangeBuildingOptions_allow_339 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_339 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_342 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_342;

	private bool logic_uScript_RemoveOnScreenMessage_instant_342;

	private bool logic_uScript_RemoveOnScreenMessage_Out_342 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_344 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_344;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_344 = "skipBlock13";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_344;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_344;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_344 = true;

	private uScript_SkipTutorial logic_uScript_SkipTutorial_uScript_SkipTutorial_346 = new uScript_SkipTutorial();

	private bool logic_uScript_SkipTutorial_Yes_346 = true;

	private bool logic_uScript_SkipTutorial_No_346 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_347 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_347 = 3f;

	private bool logic_uScript_Wait_repeat_347;

	private bool logic_uScript_Wait_Waited_347 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_350;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_350 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_350 = "MsgSkipTutorialIntroShown";

	private uScript_HideHintFloating logic_uScript_HideHintFloating_uScript_HideHintFloating_351 = new uScript_HideHintFloating();

	private bool logic_uScript_HideHintFloating_Out_351 = true;

	private uScript_HideHintFloating logic_uScript_HideHintFloating_uScript_HideHintFloating_352 = new uScript_HideHintFloating();

	private bool logic_uScript_HideHintFloating_Out_352 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_353 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_353;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_353;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_353;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_353;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_353;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_357 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_357;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_357;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_357;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_357;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_357;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_362 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_362;

	private bool logic_uScript_RemoveOnScreenMessage_instant_362;

	private bool logic_uScript_RemoveOnScreenMessage_Out_362 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_365 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_365;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_365;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_365;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_365;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_365;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_368 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_368;

	private bool logic_uScript_RemoveOnScreenMessage_instant_368;

	private bool logic_uScript_RemoveOnScreenMessage_Out_368 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_373 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_373;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_373;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_373;

	private bool logic_uScript_AddMessage_Out_373 = true;

	private bool logic_uScript_AddMessage_Shown_373 = true;

	private uScript_GetJoypadControlMode logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_375 = new uScript_GetJoypadControlMode();

	private bool logic_uScript_GetJoypadControlMode_Joypad_375 = true;

	private bool logic_uScript_GetJoypadControlMode_MouseAndKeyboard_375 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_380 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_380;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_380;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_380;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_380;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_380;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_381 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_381 = true;

	private SubGraph_ShowHintWithPadSupport logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_382 = new SubGraph_ShowHintWithPadSupport();

	private UIHintFloating.HintFloatTypes logic_SubGraph_ShowHintWithPadSupport_hintControlPad_382 = UIHintFloating.HintFloatTypes.Rotate_Pad;

	private UIHintFloating.HintFloatTypes logic_SubGraph_ShowHintWithPadSupport_hintMouseKeyboard_382;

	private SubGraph_ShowHintWithPadSupport logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_383 = new SubGraph_ShowHintWithPadSupport();

	private UIHintFloating.HintFloatTypes logic_SubGraph_ShowHintWithPadSupport_hintControlPad_383 = UIHintFloating.HintFloatTypes.Buildbeam_Pad;

	private UIHintFloating.HintFloatTypes logic_SubGraph_ShowHintWithPadSupport_hintMouseKeyboard_383 = UIHintFloating.HintFloatTypes.Buildbeam_Keyboard;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_384 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_384 = "";

	private bool logic_uScript_EnableGlow_enable_384;

	private bool logic_uScript_EnableGlow_Out_384 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_386 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_386 = "";

	private bool logic_uScript_EnableGlow_enable_386 = true;

	private bool logic_uScript_EnableGlow_Out_386 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_389 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_389 = "";

	private bool logic_uScript_EnableGlow_enable_389;

	private bool logic_uScript_EnableGlow_Out_389 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_390 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_390 = "";

	private bool logic_uScript_EnableGlow_enable_390 = true;

	private bool logic_uScript_EnableGlow_Out_390 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_391 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_391 = "";

	private bool logic_uScript_EnableGlow_enable_391;

	private bool logic_uScript_EnableGlow_Out_391 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_392 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_392 = "";

	private bool logic_uScript_EnableGlow_enable_392;

	private bool logic_uScript_EnableGlow_Out_392 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_393 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_393;

	private bool logic_uScriptCon_CompareBool_True_393 = true;

	private bool logic_uScriptCon_CompareBool_False_393 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_395 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_395 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_399 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_399 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_401 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_401 = "";

	private bool logic_uScript_EnableGlow_enable_401;

	private bool logic_uScript_EnableGlow_Out_401 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_403 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_403;

	private bool logic_uScriptCon_CompareBool_True_403 = true;

	private bool logic_uScriptCon_CompareBool_False_403 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_404 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_404 = "";

	private bool logic_uScript_EnableGlow_enable_404 = true;

	private bool logic_uScript_EnableGlow_Out_404 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_405 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_405 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_406 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_406 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_410 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_410 = "";

	private bool logic_uScript_EnableGlow_enable_410 = true;

	private bool logic_uScript_EnableGlow_Out_410 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_411 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_411 = "";

	private bool logic_uScript_EnableGlow_enable_411;

	private bool logic_uScript_EnableGlow_Out_411 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_412 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_412 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_414 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_414 = "";

	private bool logic_uScript_EnableGlow_enable_414;

	private bool logic_uScript_EnableGlow_Out_414 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_416 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_416 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_418 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_418 = "";

	private bool logic_uScript_EnableGlow_enable_418 = true;

	private bool logic_uScript_EnableGlow_Out_418 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_419 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_419;

	private bool logic_uScriptCon_CompareBool_True_419 = true;

	private bool logic_uScriptCon_CompareBool_False_419 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_420 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_420 = "";

	private bool logic_uScript_EnableGlow_enable_420 = true;

	private bool logic_uScript_EnableGlow_Out_420 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_422 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_422 = "";

	private bool logic_uScript_EnableGlow_enable_422;

	private bool logic_uScript_EnableGlow_Out_422 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_424 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_424 = "";

	private bool logic_uScript_EnableGlow_enable_424;

	private bool logic_uScript_EnableGlow_Out_424 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_426 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_426 = "";

	private bool logic_uScript_EnableGlow_enable_426;

	private bool logic_uScript_EnableGlow_Out_426 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_429 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_429 = "";

	private bool logic_uScript_EnableGlow_enable_429;

	private bool logic_uScript_EnableGlow_Out_429 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_431 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_431 = "";

	private bool logic_uScript_EnableGlow_enable_431;

	private bool logic_uScript_EnableGlow_Out_431 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_432 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_432 = "";

	private bool logic_uScript_EnableGlow_enable_432 = true;

	private bool logic_uScript_EnableGlow_Out_432 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_433 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_433 = "";

	private bool logic_uScript_EnableGlow_enable_433 = true;

	private bool logic_uScript_EnableGlow_Out_433 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_434 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_434 = "";

	private bool logic_uScript_EnableGlow_enable_434 = true;

	private bool logic_uScript_EnableGlow_Out_434 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_435 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_435 = "";

	private bool logic_uScript_EnableGlow_enable_435 = true;

	private bool logic_uScript_EnableGlow_Out_435 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_436 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_436 = "";

	private bool logic_uScript_EnableGlow_enable_436 = true;

	private bool logic_uScript_EnableGlow_Out_436 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_437 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_437 = "";

	private bool logic_uScript_EnableGlow_enable_437 = true;

	private bool logic_uScript_EnableGlow_Out_437 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_438 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_438 = "";

	private bool logic_uScript_EnableGlow_enable_438 = true;

	private bool logic_uScript_EnableGlow_Out_438 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_439 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_439 = "";

	private bool logic_uScript_EnableGlow_enable_439 = true;

	private bool logic_uScript_EnableGlow_Out_439 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_440 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_440 = "";

	private bool logic_uScript_EnableGlow_enable_440;

	private bool logic_uScript_EnableGlow_Out_440 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_442 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_442 = "";

	private bool logic_uScript_EnableGlow_enable_442;

	private bool logic_uScript_EnableGlow_Out_442 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_445 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_445 = "";

	private bool logic_uScript_EnableGlow_enable_445;

	private bool logic_uScript_EnableGlow_Out_445 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_447 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_447 = "";

	private bool logic_uScript_EnableGlow_enable_447;

	private bool logic_uScript_EnableGlow_Out_447 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_448 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_448 = "";

	private bool logic_uScript_EnableGlow_enable_448;

	private bool logic_uScript_EnableGlow_Out_448 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_451 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_451 = "";

	private bool logic_uScript_EnableGlow_enable_451;

	private bool logic_uScript_EnableGlow_Out_451 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_453 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_453 = "";

	private bool logic_uScript_EnableGlow_enable_453;

	private bool logic_uScript_EnableGlow_Out_453 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_455 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_455 = "";

	private bool logic_uScript_EnableGlow_enable_455;

	private bool logic_uScript_EnableGlow_Out_455 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_457 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_457;

	private bool logic_uScriptCon_CompareBool_True_457 = true;

	private bool logic_uScriptCon_CompareBool_False_457 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_459 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_459 = true;

	private uScript_GetJoypadControlMode logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_467 = new uScript_GetJoypadControlMode();

	private bool logic_uScript_GetJoypadControlMode_Joypad_467 = true;

	private bool logic_uScript_GetJoypadControlMode_MouseAndKeyboard_467 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_468 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_468 = true;

	private uScript_ShowHintFloating logic_uScript_ShowHintFloating_uScript_ShowHintFloating_469 = new uScript_ShowHintFloating();

	private UIHintFloating.HintFloatTypes logic_uScript_ShowHintFloating_hintAnimation_469 = UIHintFloating.HintFloatTypes.PickUp_Pad;

	private bool logic_uScript_ShowHintFloating_Out_469 = true;

	private uScript_HideHintFloating logic_uScript_HideHintFloating_uScript_HideHintFloating_471 = new uScript_HideHintFloating();

	private bool logic_uScript_HideHintFloating_Out_471 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_472 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_472;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_472;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_472;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_472;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_472;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_476 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_476;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_476 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_476 = true;

	private uScript_GetPlayerTank logic_uScript_GetPlayerTank_uScript_GetPlayerTank_478 = new uScript_GetPlayerTank();

	private Tank logic_uScript_GetPlayerTank_Return_478;

	private bool logic_uScript_GetPlayerTank_Returned_478 = true;

	private bool logic_uScript_GetPlayerTank_NotReturned_478 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_480 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_480;

	private bool logic_uScriptCon_CompareBool_True_480 = true;

	private bool logic_uScriptCon_CompareBool_False_480 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_481 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_481;

	private bool logic_uScriptAct_SetBool_Out_481 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_481 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_481 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_483;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_483 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_483 = "CabInBeam";

	private uScript_Wait logic_uScript_Wait_uScript_Wait_485 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_485;

	private bool logic_uScript_Wait_repeat_485;

	private bool logic_uScript_Wait_Waited_485 = true;

	private uScript_EnableInteractionMode logic_uScript_EnableInteractionMode_uScript_EnableInteractionMode_487 = new uScript_EnableInteractionMode();

	private bool logic_uScript_EnableInteractionMode_enableInteractionMode_487 = true;

	private bool logic_uScript_EnableInteractionMode_Out_487 = true;

	private uScript_EnableInteractionMode logic_uScript_EnableInteractionMode_uScript_EnableInteractionMode_488 = new uScript_EnableInteractionMode();

	private bool logic_uScript_EnableInteractionMode_enableInteractionMode_488;

	private bool logic_uScript_EnableInteractionMode_Out_488 = true;

	private uScript_GetJoypadControlMode logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_489 = new uScript_GetJoypadControlMode();

	private bool logic_uScript_GetJoypadControlMode_Joypad_489 = true;

	private bool logic_uScript_GetJoypadControlMode_MouseAndKeyboard_489 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_490 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_490 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_491 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_491 = true;

	private uScript_GetJoypadControlMode logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_492 = new uScript_GetJoypadControlMode();

	private bool logic_uScript_GetJoypadControlMode_Joypad_492 = true;

	private bool logic_uScript_GetJoypadControlMode_MouseAndKeyboard_492 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_493 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_493 = uScript_ChangeBuildingOptions.BuildingOptions.MoveInBeam;

	private bool logic_uScript_ChangeBuildingOptions_allow_493;

	private bool logic_uScript_ChangeBuildingOptions_Out_493 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_494 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_494 = uScript_ChangeBuildingOptions.BuildingOptions.MoveInBeam;

	private bool logic_uScript_ChangeBuildingOptions_allow_494 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_494 = true;

	private uScript_LockInteractionMode logic_uScript_LockInteractionMode_uScript_LockInteractionMode_496 = new uScript_LockInteractionMode();

	private bool logic_uScript_LockInteractionMode_Out_496 = true;

	private uScript_LockInteractionMode logic_uScript_LockInteractionMode_uScript_LockInteractionMode_497 = new uScript_LockInteractionMode();

	private bool logic_uScript_LockInteractionMode_Out_497 = true;

	private uScript_LockInteractionMode logic_uScript_LockInteractionMode_uScript_LockInteractionMode_498 = new uScript_LockInteractionMode();

	private bool logic_uScript_LockInteractionMode_Out_498 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_499 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_499 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_502 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_502 = true;

	private uScript_LockInteractionMode logic_uScript_LockInteractionMode_uScript_LockInteractionMode_503 = new uScript_LockInteractionMode();

	private bool logic_uScript_LockInteractionMode_Out_503 = true;

	private uScript_LockInteractionMode logic_uScript_LockInteractionMode_uScript_LockInteractionMode_504 = new uScript_LockInteractionMode();

	private bool logic_uScript_LockInteractionMode_Out_504 = true;

	private uScript_GetJoypadControlMode logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_505 = new uScript_GetJoypadControlMode();

	private bool logic_uScript_GetJoypadControlMode_Joypad_505 = true;

	private bool logic_uScript_GetJoypadControlMode_MouseAndKeyboard_505 = true;

	private uScript_EnableInteractionMode logic_uScript_EnableInteractionMode_uScript_EnableInteractionMode_506 = new uScript_EnableInteractionMode();

	private bool logic_uScript_EnableInteractionMode_enableInteractionMode_506 = true;

	private bool logic_uScript_EnableInteractionMode_Out_506 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_509 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_509;

	private bool logic_uScriptCon_CompareBool_True_509 = true;

	private bool logic_uScriptCon_CompareBool_False_509 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_510 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_510;

	private bool logic_uScriptAct_SetBool_Out_510 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_510 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_510 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_511;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_511 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_511 = "SkipTutorialReticule";

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_513 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_513 = "tutorial_stage";

	private string logic_uScript_SendAnaliticsEvent_parameterName_513 = "stage_complete";

	private object logic_uScript_SendAnaliticsEvent_parameter_513 = "build_first_tech";

	private bool logic_uScript_SendAnaliticsEvent_Out_513 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_515;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_515 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_515 = "Init";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_517 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_517;

	private bool logic_uScriptCon_CompareBool_True_517 = true;

	private bool logic_uScriptCon_CompareBool_False_517 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_518 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_518;

	private bool logic_uScriptAct_SetBool_Out_518 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_518 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_518 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_519 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_519 = true;

	private uScript_SetBlockLimitUIState logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_520 = new uScript_SetBlockLimitUIState();

	private UIBlockLimit.ShowReason logic_uScript_SetBlockLimitUIState_showReason_520;

	private bool logic_uScript_SetBlockLimitUIState_Out_520 = true;

	private uScript_IsBlockLimitEnabled logic_uScript_IsBlockLimitEnabled_uScript_IsBlockLimitEnabled_521 = new uScript_IsBlockLimitEnabled();

	private bool logic_uScript_IsBlockLimitEnabled_True_521 = true;

	private bool logic_uScript_IsBlockLimitEnabled_False_521 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_522 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_522 = true;

	private uScript_DisableQuickMenu logic_uScript_DisableQuickMenu_uScript_DisableQuickMenu_523 = new uScript_DisableQuickMenu();

	private bool logic_uScript_DisableQuickMenu_disableQuickMenu_523;

	private bool logic_uScript_DisableQuickMenu_Out_523 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_32 || !m_RegisteredForEvents)
		{
			owner_Connection_32 = parentGameObject;
		}
		if (null == owner_Connection_33 || !m_RegisteredForEvents)
		{
			owner_Connection_33 = parentGameObject;
		}
		if (null == owner_Connection_34 || !m_RegisteredForEvents)
		{
			owner_Connection_34 = parentGameObject;
		}
		if (null == owner_Connection_36 || !m_RegisteredForEvents)
		{
			owner_Connection_36 = parentGameObject;
		}
		if (null == owner_Connection_39 || !m_RegisteredForEvents)
		{
			owner_Connection_39 = parentGameObject;
		}
		if (null == owner_Connection_41 || !m_RegisteredForEvents)
		{
			owner_Connection_41 = parentGameObject;
		}
		if (null == owner_Connection_43 || !m_RegisteredForEvents)
		{
			owner_Connection_43 = parentGameObject;
		}
		if (null == owner_Connection_64 || !m_RegisteredForEvents)
		{
			owner_Connection_64 = parentGameObject;
			if (null != owner_Connection_64)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_64.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_64.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_65;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_65;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_65;
				}
			}
		}
		if (null == owner_Connection_79 || !m_RegisteredForEvents)
		{
			owner_Connection_79 = parentGameObject;
		}
		if (null == owner_Connection_82 || !m_RegisteredForEvents)
		{
			owner_Connection_82 = parentGameObject;
		}
		if (null == owner_Connection_87 || !m_RegisteredForEvents)
		{
			owner_Connection_87 = parentGameObject;
		}
		if (null == owner_Connection_91 || !m_RegisteredForEvents)
		{
			owner_Connection_91 = parentGameObject;
		}
		if (null == owner_Connection_108 || !m_RegisteredForEvents)
		{
			owner_Connection_108 = parentGameObject;
			if (null != owner_Connection_108)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_108.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_108.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_107;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_107;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_107;
				}
			}
		}
		if (null == owner_Connection_265 || !m_RegisteredForEvents)
		{
			owner_Connection_265 = parentGameObject;
		}
		if (null == owner_Connection_292 || !m_RegisteredForEvents)
		{
			owner_Connection_292 = parentGameObject;
		}
		if (null == owner_Connection_293 || !m_RegisteredForEvents)
		{
			owner_Connection_293 = parentGameObject;
		}
		if (null == owner_Connection_295 || !m_RegisteredForEvents)
		{
			owner_Connection_295 = parentGameObject;
		}
		if (null == owner_Connection_296 || !m_RegisteredForEvents)
		{
			owner_Connection_296 = parentGameObject;
		}
		if (null == owner_Connection_297 || !m_RegisteredForEvents)
		{
			owner_Connection_297 = parentGameObject;
		}
		if (null == owner_Connection_302 || !m_RegisteredForEvents)
		{
			owner_Connection_302 = parentGameObject;
		}
		if (null == owner_Connection_309 || !m_RegisteredForEvents)
		{
			owner_Connection_309 = parentGameObject;
		}
		if (null == owner_Connection_311 || !m_RegisteredForEvents)
		{
			owner_Connection_311 = parentGameObject;
		}
		if (null == owner_Connection_315 || !m_RegisteredForEvents)
		{
			owner_Connection_315 = parentGameObject;
		}
		if (null == owner_Connection_319 || !m_RegisteredForEvents)
		{
			owner_Connection_319 = parentGameObject;
		}
		if (null == owner_Connection_321 || !m_RegisteredForEvents)
		{
			owner_Connection_321 = parentGameObject;
		}
		if (null == owner_Connection_330 || !m_RegisteredForEvents)
		{
			owner_Connection_330 = parentGameObject;
		}
		if (null == owner_Connection_345 || !m_RegisteredForEvents)
		{
			owner_Connection_345 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_64)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_64.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_64.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_65;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_65;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_65;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_108)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_108.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_108.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_107;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_107;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_107;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_64)
		{
			uScript_EncounterUpdate component = owner_Connection_64.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_65;
				component.OnSuspend -= Instance_OnSuspend_65;
				component.OnResume -= Instance_OnResume_65;
			}
		}
		if (null != owner_Connection_108)
		{
			uScript_SaveLoad component2 = owner_Connection_108.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_107;
				component2.LoadEvent -= Instance_LoadEvent_107;
				component2.RestartEvent -= Instance_RestartEvent_107;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_0.SetParent(g);
		logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_1.SetParent(g);
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_2.SetParent(g);
		logic_uScript_ToggleBuildBeam_uScript_ToggleBuildBeam_3.SetParent(g);
		logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_4.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_5.SetParent(g);
		logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_6.SetParent(g);
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_7.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_8.SetParent(g);
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_9.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_10.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_11.SetParent(g);
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_12.SetParent(g);
		logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_13.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_14.SetParent(g);
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_15.SetParent(g);
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_16.SetParent(g);
		logic_uScript_HideHUD_uScript_HideHUD_17.SetParent(g);
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_18.SetParent(g);
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_19.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_20.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_21.SetParent(g);
		logic_uScript_HasCameraRotated_uScript_HasCameraRotated_22.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_24.SetParent(g);
		logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_25.SetParent(g);
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_26.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_27.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_28.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.SetParent(g);
		logic_uScript_HasCameraRotated_uScript_HasCameraRotated_30.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_31.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_37.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_38.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_40.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_42.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_44.SetParent(g);
		logic_uScript_EnableMusic_uScript_EnableMusic_45.SetParent(g);
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_46.SetParent(g);
		logic_uScript_Save_uScript_Save_47.SetParent(g);
		logic_uScript_SKU_uScript_SKU_48.SetParent(g);
		logic_uScript_IsDebugSkip_uScript_IsDebugSkip_49.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_50.SetParent(g);
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_51.SetParent(g);
		logic_uScript_EnableMusic_uScript_EnableMusic_52.SetParent(g);
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_53.SetParent(g);
		logic_uScript_ShowPlayerProfile_uScript_ShowPlayerProfile_54.SetParent(g);
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_55.SetParent(g);
		logic_uScript_Wait_uScript_Wait_56.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_66.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_68.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_70.SetParent(g);
		logic_uScript_Wait_uScript_Wait_72.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_73.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_75.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_81.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_85.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_89.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_96.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_98.SetParent(g);
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_99.SetParent(g);
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_101.SetParent(g);
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_103.SetParent(g);
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_106.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.SetParent(g);
		logic_uScriptAct_SetFloat_uScriptAct_SetFloat_121.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_123.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_139.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_144.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_147.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_149.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_153.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_154.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_156.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_158.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_163.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_164.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_165.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_166.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_167.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_169.SetParent(g);
		logic_uScript_Wait_uScript_Wait_170.SetParent(g);
		logic_uScript_GetPlayerTank_uScript_GetPlayerTank_172.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_173.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_174.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_176.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_178.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_180.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_185.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_188.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_189.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_190.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_192.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_193.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_194.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_196.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_197.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_200.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_202.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_204.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_205.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_208.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_210.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_211.SetParent(g);
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_214.SetParent(g);
		logic_uScript_SetInvadersActive_uScript_SetInvadersActive_215.SetParent(g);
		logic_uScript_SKU_uScript_SKU_216.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_218.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_223.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_227.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_228.SetParent(g);
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_230.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_233.SetParent(g);
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_235.SetParent(g);
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_238.SetParent(g);
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_241.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_243.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_246.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_249.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_250.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_251.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_252.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_253.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_255.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_257.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_258.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_262.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_263.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_264.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_266.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_267.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_270.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_272.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_274.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_277.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_279.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_283.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_284.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_288.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_289.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_294.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_298.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_299.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_300.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_301.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_303.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_304.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_305.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_310.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_314.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_320.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_322.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_327.SetParent(g);
		logic_uScript_SkipTutorial_uScript_SkipTutorial_329.SetParent(g);
		logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_331.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_333.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_335.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_337.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_338.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_339.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_342.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_344.SetParent(g);
		logic_uScript_SkipTutorial_uScript_SkipTutorial_346.SetParent(g);
		logic_uScript_Wait_uScript_Wait_347.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.SetParent(g);
		logic_uScript_HideHintFloating_uScript_HideHintFloating_351.SetParent(g);
		logic_uScript_HideHintFloating_uScript_HideHintFloating_352.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_353.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_357.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_362.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_365.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_368.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_373.SetParent(g);
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_375.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_380.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_381.SetParent(g);
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_382.SetParent(g);
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_383.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_384.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_386.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_389.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_390.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_391.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_392.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_393.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_395.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_399.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_401.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_403.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_404.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_405.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_406.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_410.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_411.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_412.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_414.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_416.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_418.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_419.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_420.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_422.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_424.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_426.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_429.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_431.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_432.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_433.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_434.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_435.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_436.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_437.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_438.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_439.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_440.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_442.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_445.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_447.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_448.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_451.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_453.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_455.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_457.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_459.SetParent(g);
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_467.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_468.SetParent(g);
		logic_uScript_ShowHintFloating_uScript_ShowHintFloating_469.SetParent(g);
		logic_uScript_HideHintFloating_uScript_HideHintFloating_471.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_472.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_476.SetParent(g);
		logic_uScript_GetPlayerTank_uScript_GetPlayerTank_478.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_480.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_481.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.SetParent(g);
		logic_uScript_Wait_uScript_Wait_485.SetParent(g);
		logic_uScript_EnableInteractionMode_uScript_EnableInteractionMode_487.SetParent(g);
		logic_uScript_EnableInteractionMode_uScript_EnableInteractionMode_488.SetParent(g);
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_489.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_490.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_491.SetParent(g);
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_492.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_493.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_494.SetParent(g);
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_496.SetParent(g);
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_497.SetParent(g);
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_498.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_499.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_502.SetParent(g);
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_503.SetParent(g);
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_504.SetParent(g);
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_505.SetParent(g);
		logic_uScript_EnableInteractionMode_uScript_EnableInteractionMode_506.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_509.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_510.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_513.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_517.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_518.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_519.SetParent(g);
		logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_520.SetParent(g);
		logic_uScript_IsBlockLimitEnabled_uScript_IsBlockLimitEnabled_521.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_522.SetParent(g);
		logic_uScript_DisableQuickMenu_uScript_DisableQuickMenu_523.SetParent(g);
		owner_Connection_32 = parentGameObject;
		owner_Connection_33 = parentGameObject;
		owner_Connection_34 = parentGameObject;
		owner_Connection_36 = parentGameObject;
		owner_Connection_39 = parentGameObject;
		owner_Connection_41 = parentGameObject;
		owner_Connection_43 = parentGameObject;
		owner_Connection_64 = parentGameObject;
		owner_Connection_79 = parentGameObject;
		owner_Connection_82 = parentGameObject;
		owner_Connection_87 = parentGameObject;
		owner_Connection_91 = parentGameObject;
		owner_Connection_108 = parentGameObject;
		owner_Connection_265 = parentGameObject;
		owner_Connection_292 = parentGameObject;
		owner_Connection_293 = parentGameObject;
		owner_Connection_295 = parentGameObject;
		owner_Connection_296 = parentGameObject;
		owner_Connection_297 = parentGameObject;
		owner_Connection_302 = parentGameObject;
		owner_Connection_309 = parentGameObject;
		owner_Connection_311 = parentGameObject;
		owner_Connection_315 = parentGameObject;
		owner_Connection_319 = parentGameObject;
		owner_Connection_321 = parentGameObject;
		owner_Connection_330 = parentGameObject;
		owner_Connection_345 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_353.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_357.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_365.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_380.Awake();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_382.Awake();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_383.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_472.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.Awake();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.Output1 += uScriptCon_ManualSwitch_Output1_29;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.Output2 += uScriptCon_ManualSwitch_Output2_29;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.Output3 += uScriptCon_ManualSwitch_Output3_29;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.Output4 += uScriptCon_ManualSwitch_Output4_29;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.Output5 += uScriptCon_ManualSwitch_Output5_29;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.Output6 += uScriptCon_ManualSwitch_Output6_29;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.Output7 += uScriptCon_ManualSwitch_Output7_29;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.Output8 += uScriptCon_ManualSwitch_Output8_29;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Save_Out += SubGraph_SaveLoadInt_Save_Out_109;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Load_Out += SubGraph_SaveLoadInt_Load_Out_109;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Save_Out += SubGraph_SaveLoadBool_Save_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Load_Out += SubGraph_SaveLoadBool_Load_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.Save_Out += SubGraph_SaveLoadBool_Save_Out_113;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.Load_Out += SubGraph_SaveLoadBool_Load_Out_113;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_113;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Save_Out += SubGraph_SaveLoadBool_Save_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Load_Out += SubGraph_SaveLoadBool_Load_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Save_Out += SubGraph_SaveLoadBool_Save_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Load_Out += SubGraph_SaveLoadBool_Load_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Save_Out += SubGraph_SaveLoadBool_Save_Out_119;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Load_Out += SubGraph_SaveLoadBool_Load_Out_119;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_119;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.Save_Out += SubGraph_SaveLoadBool_Save_Out_260;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.Load_Out += SubGraph_SaveLoadBool_Load_Out_260;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_260;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.Save_Out += SubGraph_SaveLoadBool_Save_Out_350;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.Load_Out += SubGraph_SaveLoadBool_Load_Out_350;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_350;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_353.Out += SubGraph_AddMessageWithPadSupport_Out_353;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_353.Shown += SubGraph_AddMessageWithPadSupport_Shown_353;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_357.Out += SubGraph_AddMessageWithPadSupport_Out_357;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_357.Shown += SubGraph_AddMessageWithPadSupport_Shown_357;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_365.Out += SubGraph_AddMessageWithPadSupport_Out_365;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_365.Shown += SubGraph_AddMessageWithPadSupport_Shown_365;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_380.Out += SubGraph_AddMessageWithPadSupport_Out_380;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_380.Shown += SubGraph_AddMessageWithPadSupport_Shown_380;
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_382.Out += SubGraph_ShowHintWithPadSupport_Out_382;
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_383.Out += SubGraph_ShowHintWithPadSupport_Out_383;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_472.Out += SubGraph_AddMessageWithPadSupport_Out_472;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_472.Shown += SubGraph_AddMessageWithPadSupport_Shown_472;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.Save_Out += SubGraph_SaveLoadBool_Save_Out_483;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.Load_Out += SubGraph_SaveLoadBool_Load_Out_483;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_483;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.Save_Out += SubGraph_SaveLoadBool_Save_Out_511;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.Load_Out += SubGraph_SaveLoadBool_Load_Out_511;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_511;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.Save_Out += SubGraph_SaveLoadBool_Save_Out_515;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.Load_Out += SubGraph_SaveLoadBool_Load_Out_515;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_515;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_353.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_357.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_365.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_380.Start();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_382.Start();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_383.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_472.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_353.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_357.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_365.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_380.OnEnable();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_382.OnEnable();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_383.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_472.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_1.OnDisable();
		logic_uScript_ToggleBuildBeam_uScript_ToggleBuildBeam_3.OnDisable();
		logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_6.OnDisable();
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_9.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_10.OnDisable();
		logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_13.OnDisable();
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_15.OnDisable();
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_16.OnDisable();
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_18.OnDisable();
		logic_uScript_HasCameraRotated_uScript_HasCameraRotated_22.OnDisable();
		logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_25.OnDisable();
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_26.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_27.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_28.OnDisable();
		logic_uScript_HasCameraRotated_uScript_HasCameraRotated_30.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_37.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_38.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_40.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_42.OnDisable();
		logic_uScript_Save_uScript_Save_47.OnDisable();
		logic_uScript_Wait_uScript_Wait_56.OnDisable();
		logic_uScript_Wait_uScript_Wait_72.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.OnDisable();
		logic_uScript_Wait_uScript_Wait_170.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_174.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_176.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_178.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_180.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_185.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_188.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_189.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_267.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_277.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_279.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_284.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_289.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_294.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_298.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_299.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_300.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_301.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_303.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_304.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_305.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_310.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_314.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_320.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_322.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_333.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_344.OnDisable();
		logic_uScript_Wait_uScript_Wait_347.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_353.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_357.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_365.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_373.OnDisable();
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_375.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_380.OnDisable();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_382.OnDisable();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_383.OnDisable();
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_467.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_472.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.OnDisable();
		logic_uScript_Wait_uScript_Wait_485.OnDisable();
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_489.OnDisable();
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_492.OnDisable();
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_505.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_353.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_357.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_365.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_380.Update();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_382.Update();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_383.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_472.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_353.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_357.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_365.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_380.OnDestroy();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_382.OnDestroy();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_383.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_472.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.OnDestroy();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.Output1 -= uScriptCon_ManualSwitch_Output1_29;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.Output2 -= uScriptCon_ManualSwitch_Output2_29;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.Output3 -= uScriptCon_ManualSwitch_Output3_29;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.Output4 -= uScriptCon_ManualSwitch_Output4_29;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.Output5 -= uScriptCon_ManualSwitch_Output5_29;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.Output6 -= uScriptCon_ManualSwitch_Output6_29;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.Output7 -= uScriptCon_ManualSwitch_Output7_29;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.Output8 -= uScriptCon_ManualSwitch_Output8_29;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Save_Out -= SubGraph_SaveLoadInt_Save_Out_109;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Load_Out -= SubGraph_SaveLoadInt_Load_Out_109;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_109;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Save_Out -= SubGraph_SaveLoadBool_Save_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Load_Out -= SubGraph_SaveLoadBool_Load_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.Save_Out -= SubGraph_SaveLoadBool_Save_Out_113;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.Load_Out -= SubGraph_SaveLoadBool_Load_Out_113;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_113;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Save_Out -= SubGraph_SaveLoadBool_Save_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Load_Out -= SubGraph_SaveLoadBool_Load_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Save_Out -= SubGraph_SaveLoadBool_Save_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Load_Out -= SubGraph_SaveLoadBool_Load_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Save_Out -= SubGraph_SaveLoadBool_Save_Out_119;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Load_Out -= SubGraph_SaveLoadBool_Load_Out_119;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_119;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.Save_Out -= SubGraph_SaveLoadBool_Save_Out_260;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.Load_Out -= SubGraph_SaveLoadBool_Load_Out_260;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_260;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.Save_Out -= SubGraph_SaveLoadBool_Save_Out_350;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.Load_Out -= SubGraph_SaveLoadBool_Load_Out_350;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_350;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_353.Out -= SubGraph_AddMessageWithPadSupport_Out_353;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_353.Shown -= SubGraph_AddMessageWithPadSupport_Shown_353;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_357.Out -= SubGraph_AddMessageWithPadSupport_Out_357;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_357.Shown -= SubGraph_AddMessageWithPadSupport_Shown_357;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_365.Out -= SubGraph_AddMessageWithPadSupport_Out_365;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_365.Shown -= SubGraph_AddMessageWithPadSupport_Shown_365;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_380.Out -= SubGraph_AddMessageWithPadSupport_Out_380;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_380.Shown -= SubGraph_AddMessageWithPadSupport_Shown_380;
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_382.Out -= SubGraph_ShowHintWithPadSupport_Out_382;
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_383.Out -= SubGraph_ShowHintWithPadSupport_Out_383;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_472.Out -= SubGraph_AddMessageWithPadSupport_Out_472;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_472.Shown -= SubGraph_AddMessageWithPadSupport_Shown_472;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.Save_Out -= SubGraph_SaveLoadBool_Save_Out_483;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.Load_Out -= SubGraph_SaveLoadBool_Load_Out_483;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_483;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.Save_Out -= SubGraph_SaveLoadBool_Save_Out_511;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.Load_Out -= SubGraph_SaveLoadBool_Load_Out_511;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_511;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.Save_Out -= SubGraph_SaveLoadBool_Save_Out_515;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.Load_Out -= SubGraph_SaveLoadBool_Load_Out_515;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_515;
	}

	private void Instance_OnUpdate_65(object o, EventArgs e)
	{
		Relay_OnUpdate_65();
	}

	private void Instance_OnSuspend_65(object o, EventArgs e)
	{
		Relay_OnSuspend_65();
	}

	private void Instance_OnResume_65(object o, EventArgs e)
	{
		Relay_OnResume_65();
	}

	private void Instance_SaveEvent_107(object o, EventArgs e)
	{
		Relay_SaveEvent_107();
	}

	private void Instance_LoadEvent_107(object o, EventArgs e)
	{
		Relay_LoadEvent_107();
	}

	private void Instance_RestartEvent_107(object o, EventArgs e)
	{
		Relay_RestartEvent_107();
	}

	private void uScriptCon_ManualSwitch_Output1_29(object o, EventArgs e)
	{
		Relay_Output1_29();
	}

	private void uScriptCon_ManualSwitch_Output2_29(object o, EventArgs e)
	{
		Relay_Output2_29();
	}

	private void uScriptCon_ManualSwitch_Output3_29(object o, EventArgs e)
	{
		Relay_Output3_29();
	}

	private void uScriptCon_ManualSwitch_Output4_29(object o, EventArgs e)
	{
		Relay_Output4_29();
	}

	private void uScriptCon_ManualSwitch_Output5_29(object o, EventArgs e)
	{
		Relay_Output5_29();
	}

	private void uScriptCon_ManualSwitch_Output6_29(object o, EventArgs e)
	{
		Relay_Output6_29();
	}

	private void uScriptCon_ManualSwitch_Output7_29(object o, EventArgs e)
	{
		Relay_Output7_29();
	}

	private void uScriptCon_ManualSwitch_Output8_29(object o, EventArgs e)
	{
		Relay_Output8_29();
	}

	private void SubGraph_SaveLoadInt_Save_Out_109(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_109 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_109;
		Relay_Save_Out_109();
	}

	private void SubGraph_SaveLoadInt_Load_Out_109(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_109 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_109;
		Relay_Load_Out_109();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_109(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_109 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_109;
		Relay_Restart_Out_109();
	}

	private void SubGraph_SaveLoadBool_Save_Out_111(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = e.boolean;
		local_GhostBlockStandardSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_111;
		Relay_Save_Out_111();
	}

	private void SubGraph_SaveLoadBool_Load_Out_111(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = e.boolean;
		local_GhostBlockStandardSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_111;
		Relay_Load_Out_111();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_111(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = e.boolean;
		local_GhostBlockStandardSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_111;
		Relay_Restart_Out_111();
	}

	private void SubGraph_SaveLoadBool_Save_Out_113(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_113 = e.boolean;
		local_GhostBlockWheelsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_113;
		Relay_Save_Out_113();
	}

	private void SubGraph_SaveLoadBool_Load_Out_113(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_113 = e.boolean;
		local_GhostBlockWheelsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_113;
		Relay_Load_Out_113();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_113(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_113 = e.boolean;
		local_GhostBlockWheelsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_113;
		Relay_Restart_Out_113();
	}

	private void SubGraph_SaveLoadBool_Save_Out_116(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = e.boolean;
		local_GhostBlockGunSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_116;
		Relay_Save_Out_116();
	}

	private void SubGraph_SaveLoadBool_Load_Out_116(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = e.boolean;
		local_GhostBlockGunSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_116;
		Relay_Load_Out_116();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_116(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = e.boolean;
		local_GhostBlockGunSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_116;
		Relay_Restart_Out_116();
	}

	private void SubGraph_SaveLoadBool_Save_Out_117(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = e.boolean;
		local_GhostBlockDrillSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_117;
		Relay_Save_Out_117();
	}

	private void SubGraph_SaveLoadBool_Load_Out_117(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = e.boolean;
		local_GhostBlockDrillSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_117;
		Relay_Load_Out_117();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_117(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = e.boolean;
		local_GhostBlockDrillSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_117;
		Relay_Restart_Out_117();
	}

	private void SubGraph_SaveLoadBool_Save_Out_119(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_119 = e.boolean;
		local_Msg4OnScreen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_119;
		Relay_Save_Out_119();
	}

	private void SubGraph_SaveLoadBool_Load_Out_119(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_119 = e.boolean;
		local_Msg4OnScreen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_119;
		Relay_Load_Out_119();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_119(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_119 = e.boolean;
		local_Msg4OnScreen_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_119;
		Relay_Restart_Out_119();
	}

	private void SubGraph_SaveLoadBool_Save_Out_260(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_260 = e.boolean;
		local_DraggingWheel_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_260;
		Relay_Save_Out_260();
	}

	private void SubGraph_SaveLoadBool_Load_Out_260(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_260 = e.boolean;
		local_DraggingWheel_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_260;
		Relay_Load_Out_260();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_260(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_260 = e.boolean;
		local_DraggingWheel_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_260;
		Relay_Restart_Out_260();
	}

	private void SubGraph_SaveLoadBool_Save_Out_350(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_350 = e.boolean;
		local_MsgSkipTutorialIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_350;
		Relay_Save_Out_350();
	}

	private void SubGraph_SaveLoadBool_Load_Out_350(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_350 = e.boolean;
		local_MsgSkipTutorialIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_350;
		Relay_Load_Out_350();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_350(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_350 = e.boolean;
		local_MsgSkipTutorialIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_350;
		Relay_Restart_Out_350();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_353(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_353 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_353 = e.messageControlPadReturn;
		Relay_Out_353();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_353(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_353 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_353 = e.messageControlPadReturn;
		Relay_Shown_353();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_357(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_357 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_357 = e.messageControlPadReturn;
		local_361_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_357;
		local_363_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_357;
		Relay_Out_357();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_357(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_357 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_357 = e.messageControlPadReturn;
		local_361_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_357;
		local_363_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_357;
		Relay_Shown_357();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_365(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_365 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_365 = e.messageControlPadReturn;
		local_MsgSkipTutorialExitBeam_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_365;
		local_MsgSkipTutorialExitBeam_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_365;
		Relay_Out_365();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_365(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_365 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_365 = e.messageControlPadReturn;
		local_MsgSkipTutorialExitBeam_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_365;
		local_MsgSkipTutorialExitBeam_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_365;
		Relay_Shown_365();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_380(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_380 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_380 = e.messageControlPadReturn;
		Relay_Out_380();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_380(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_380 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_380 = e.messageControlPadReturn;
		Relay_Shown_380();
	}

	private void SubGraph_ShowHintWithPadSupport_Out_382(object o, SubGraph_ShowHintWithPadSupport.LogicEventArgs e)
	{
		Relay_Out_382();
	}

	private void SubGraph_ShowHintWithPadSupport_Out_383(object o, SubGraph_ShowHintWithPadSupport.LogicEventArgs e)
	{
		Relay_Out_383();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_472(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_472 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_472 = e.messageControlPadReturn;
		Relay_Out_472();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_472(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_472 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_472 = e.messageControlPadReturn;
		Relay_Shown_472();
	}

	private void SubGraph_SaveLoadBool_Save_Out_483(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_483 = e.boolean;
		local_CabInBeam_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_483;
		Relay_Save_Out_483();
	}

	private void SubGraph_SaveLoadBool_Load_Out_483(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_483 = e.boolean;
		local_CabInBeam_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_483;
		Relay_Load_Out_483();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_483(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_483 = e.boolean;
		local_CabInBeam_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_483;
		Relay_Restart_Out_483();
	}

	private void SubGraph_SaveLoadBool_Save_Out_511(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_511 = e.boolean;
		local_SkipTutorialReticule_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_511;
		Relay_Save_Out_511();
	}

	private void SubGraph_SaveLoadBool_Load_Out_511(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_511 = e.boolean;
		local_SkipTutorialReticule_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_511;
		Relay_Load_Out_511();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_511(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_511 = e.boolean;
		local_SkipTutorialReticule_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_511;
		Relay_Restart_Out_511();
	}

	private void SubGraph_SaveLoadBool_Save_Out_515(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_515 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_515;
		Relay_Save_Out_515();
	}

	private void SubGraph_SaveLoadBool_Load_Out_515(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_515 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_515;
		Relay_Load_Out_515();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_515(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_515 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_515;
		Relay_Restart_Out_515();
	}

	private void Relay_In_0()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_0.In(logic_uScript_ChangeBuildingOptions_change_0, logic_uScript_ChangeBuildingOptions_allow_0);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_0.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_In_1()
	{
		logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_1.In();
		if (logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_1.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_2()
	{
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_2.In(logic_uScript_SetPlacementFilter_placementFilter_2);
		if (logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_2.Out)
		{
			Relay_In_178();
		}
	}

	private void Relay_In_3()
	{
		logic_uScript_ToggleBuildBeam_uScript_ToggleBuildBeam_3.In(logic_uScript_ToggleBuildBeam_active_3);
		if (logic_uScript_ToggleBuildBeam_uScript_ToggleBuildBeam_3.Out)
		{
			Relay_In_11();
		}
	}

	private void Relay_In_4()
	{
		logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_4.In();
		bool num = logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_4.True;
		bool flag = logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_4.False;
		if (num)
		{
			Relay_In_357();
		}
		if (flag)
		{
			Relay_In_19();
		}
	}

	private void Relay_In_5()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_5.In(logic_uScript_ChangeBuildingOptions_change_5, logic_uScript_ChangeBuildingOptions_allow_5);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_5.Out)
		{
			Relay_In_4();
		}
	}

	private void Relay_In_6()
	{
		logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_6.In();
		if (logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_6.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_7()
	{
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_7.In(logic_uScript_SetPlacementFilter_placementFilter_7);
		if (logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_7.Out)
		{
			Relay_In_144();
		}
	}

	private void Relay_In_8()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_8.In(logic_uScript_ChangeBuildingOptions_change_8, logic_uScript_ChangeBuildingOptions_allow_8);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_8.Out)
		{
			Relay_In_72();
		}
	}

	private void Relay_In_9()
	{
		logic_uScript_DoesPlayerTankHaveBlock_blockType_9 = blockTypeGun;
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_9.In(logic_uScript_DoesPlayerTankHaveBlock_blockType_9, logic_uScript_DoesPlayerTankHaveBlock_amount_9);
		bool num = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_9.True;
		bool flag = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_9.False;
		if (num)
		{
			Relay_In_103();
		}
		if (flag)
		{
			Relay_In_284();
		}
	}

	private void Relay_In_10()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_10 = blockTypeStandard;
		logic_uScript_SpawnBlockAbovePlayer_owner_10 = owner_Connection_32;
		logic_uScript_SpawnBlockAbovePlayer_Return_10 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_10.In(logic_uScript_SpawnBlockAbovePlayer_blockType_10, logic_uScript_SpawnBlockAbovePlayer_uniqueName_10, logic_uScript_SpawnBlockAbovePlayer_owner_10);
		local_StandardBlock_TankBlock = logic_uScript_SpawnBlockAbovePlayer_Return_10;
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_10.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_11()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_11.In(logic_uScript_ChangeBuildingOptions_change_11, logic_uScript_ChangeBuildingOptions_allow_11);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_11.Out)
		{
			Relay_In_493();
		}
	}

	private void Relay_In_12()
	{
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_12.In(logic_uScript_SetPlacementFilter_placementFilter_12);
		if (logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_12.Out)
		{
			Relay_In_174();
		}
	}

	private void Relay_In_13()
	{
		logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_13.In();
		if (logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_13.Out)
		{
			Relay_In_329();
		}
	}

	private void Relay_In_14()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_14.In(logic_uScript_ChangeBuildingOptions_change_14, logic_uScript_ChangeBuildingOptions_allow_14);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_14.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_In_15()
	{
		logic_uScript_DoesPlayerTankHaveBlock_blockType_15 = blockTypeStandard;
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_15.In(logic_uScript_DoesPlayerTankHaveBlock_blockType_15, logic_uScript_DoesPlayerTankHaveBlock_amount_15);
		if (logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_15.True)
		{
			Relay_In_99();
		}
	}

	private void Relay_In_16()
	{
		logic_uScript_DoesPlayerTankHaveBlock_blockType_16 = blockTypeWheel;
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_16.In(logic_uScript_DoesPlayerTankHaveBlock_blockType_16, logic_uScript_DoesPlayerTankHaveBlock_amount_16);
		bool num = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_16.True;
		bool flag = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_16.False;
		if (num)
		{
			Relay_In_101();
		}
		if (flag)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_17()
	{
		logic_uScript_HideHUD_uScript_HideHUD_17.In(logic_uScript_HideHUD_hide_17);
		if (logic_uScript_HideHUD_uScript_HideHUD_17.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_18()
	{
		logic_uScript_DoesPlayerTankHaveBlock_blockType_18 = blockTypeDrill;
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_18.In(logic_uScript_DoesPlayerTankHaveBlock_blockType_18, logic_uScript_DoesPlayerTankHaveBlock_amount_18);
		bool num = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_18.True;
		bool flag = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_18.False;
		if (num)
		{
			Relay_In_106();
		}
		if (flag)
		{
			Relay_In_289();
		}
	}

	private void Relay_In_19()
	{
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_19.In(logic_uScript_SetPlacementFilter_placementFilter_19);
		if (logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_19.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_20()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_20.In(logic_uScript_ChangeBuildingOptions_change_20, logic_uScript_ChangeBuildingOptions_allow_20);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_20.Out)
		{
			Relay_In_494();
		}
	}

	private void Relay_In_21()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_21.In(logic_uScript_ChangeBuildingOptions_change_21, logic_uScript_ChangeBuildingOptions_allow_21);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_21.Out)
		{
			Relay_SetDefault_53();
		}
	}

	private void Relay_In_22()
	{
		logic_uScript_HasCameraRotated_uScript_HasCameraRotated_22.In(logic_uScript_HasCameraRotated_turnCameraMinDegrees_22);
		bool num = logic_uScript_HasCameraRotated_uScript_HasCameraRotated_22.True;
		bool flag = logic_uScript_HasCameraRotated_uScript_HasCameraRotated_22.False;
		if (num)
		{
			Relay_In_351();
		}
		if (flag)
		{
			Relay_In_353();
		}
	}

	private void Relay_In_24()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_24.In(logic_uScript_ChangeBuildingOptions_change_24, logic_uScript_ChangeBuildingOptions_allow_24);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_24.Out)
		{
			Relay_In_0();
		}
	}

	private void Relay_In_25()
	{
		logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_25.In();
		if (logic_uScript_ClearInfoOverlays_uScript_ClearInfoOverlays_25.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_In_26()
	{
		logic_uScript_DoesPlayerTankHaveBlock_blockType_26 = blockTypeWheel;
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_26.In(logic_uScript_DoesPlayerTankHaveBlock_blockType_26, logic_uScript_DoesPlayerTankHaveBlock_amount_26);
		bool num = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_26.True;
		bool flag = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_26.False;
		if (num)
		{
			Relay_In_16();
		}
		if (flag)
		{
			Relay_In_277();
		}
	}

	private void Relay_In_27()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_27 = blockTypeDrill;
		logic_uScript_SpawnBlockAbovePlayer_owner_27 = owner_Connection_34;
		logic_uScript_SpawnBlockAbovePlayer_Return_27 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_27.In(logic_uScript_SpawnBlockAbovePlayer_blockType_27, logic_uScript_SpawnBlockAbovePlayer_uniqueName_27, logic_uScript_SpawnBlockAbovePlayer_owner_27);
		local_DrillBlock_TankBlock = logic_uScript_SpawnBlockAbovePlayer_Return_27;
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_27.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_28()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_28 = blockTypeGun;
		logic_uScript_SpawnBlockAbovePlayer_owner_28 = owner_Connection_33;
		logic_uScript_SpawnBlockAbovePlayer_Return_28 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_28.In(logic_uScript_SpawnBlockAbovePlayer_blockType_28, logic_uScript_SpawnBlockAbovePlayer_uniqueName_28, logic_uScript_SpawnBlockAbovePlayer_owner_28);
		local_GunBlock_TankBlock = logic_uScript_SpawnBlockAbovePlayer_Return_28;
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_28.Out)
		{
			Relay_In_55();
		}
	}

	private void Relay_Output1_29()
	{
		Relay_In_48();
	}

	private void Relay_Output2_29()
	{
		Relay_In_22();
	}

	private void Relay_Output3_29()
	{
		Relay_In_10();
	}

	private void Relay_Output4_29()
	{
		Relay_In_6();
	}

	private void Relay_Output5_29()
	{
		Relay_In_1();
	}

	private void Relay_Output6_29()
	{
		Relay_In_13();
	}

	private void Relay_Output7_29()
	{
	}

	private void Relay_Output8_29()
	{
	}

	private void Relay_In_29()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_29 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_29.In(logic_uScriptCon_ManualSwitch_CurrentOutput_29);
	}

	private void Relay_In_30()
	{
		logic_uScript_HasCameraRotated_uScript_HasCameraRotated_30.In(logic_uScript_HasCameraRotated_turnCameraMinDegrees_30);
		bool num = logic_uScript_HasCameraRotated_uScript_HasCameraRotated_30.True;
		bool flag = logic_uScript_HasCameraRotated_uScript_HasCameraRotated_30.False;
		if (num)
		{
			Relay_In_70();
		}
		if (flag)
		{
			Relay_In_56();
		}
	}

	private void Relay_In_31()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_31 = local_361_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_31.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_31, logic_uScript_RemoveOnScreenMessage_instant_31);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_31.Out)
		{
			Relay_In_362();
		}
	}

	private void Relay_In_37()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_37 = blockTypeWheel;
		logic_uScript_SpawnBlockAbovePlayer_owner_37 = owner_Connection_36;
		logic_uScript_SpawnBlockAbovePlayer_Return_37 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_37.In(logic_uScript_SpawnBlockAbovePlayer_blockType_37, logic_uScript_SpawnBlockAbovePlayer_uniqueName_37, logic_uScript_SpawnBlockAbovePlayer_owner_37);
		local_Wheel01_TankBlock = logic_uScript_SpawnBlockAbovePlayer_Return_37;
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_37.Out)
		{
			Relay_In_38();
		}
	}

	private void Relay_In_38()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_38 = blockTypeWheel;
		logic_uScript_SpawnBlockAbovePlayer_owner_38 = owner_Connection_39;
		logic_uScript_SpawnBlockAbovePlayer_Return_38 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_38.In(logic_uScript_SpawnBlockAbovePlayer_blockType_38, logic_uScript_SpawnBlockAbovePlayer_uniqueName_38, logic_uScript_SpawnBlockAbovePlayer_owner_38);
		local_Wheel02_TankBlock = logic_uScript_SpawnBlockAbovePlayer_Return_38;
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_38.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_In_40()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_40 = blockTypeWheel;
		logic_uScript_SpawnBlockAbovePlayer_owner_40 = owner_Connection_41;
		logic_uScript_SpawnBlockAbovePlayer_Return_40 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_40.In(logic_uScript_SpawnBlockAbovePlayer_blockType_40, logic_uScript_SpawnBlockAbovePlayer_uniqueName_40, logic_uScript_SpawnBlockAbovePlayer_owner_40);
		local_Wheel03_TankBlock = logic_uScript_SpawnBlockAbovePlayer_Return_40;
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_40.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_42()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_42 = blockTypeWheel;
		logic_uScript_SpawnBlockAbovePlayer_owner_42 = owner_Connection_43;
		logic_uScript_SpawnBlockAbovePlayer_Return_42 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_42.In(logic_uScript_SpawnBlockAbovePlayer_blockType_42, logic_uScript_SpawnBlockAbovePlayer_uniqueName_42, logic_uScript_SpawnBlockAbovePlayer_owner_42);
		local_Wheel04_TankBlock = logic_uScript_SpawnBlockAbovePlayer_Return_42;
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_42.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_44()
	{
		logic_uScript_LockPause_uScript_LockPause_44.In(logic_uScript_LockPause_lockPause_44, logic_uScript_LockPause_disabledReason_44);
		if (logic_uScript_LockPause_uScript_LockPause_44.Out)
		{
			Relay_In_521();
		}
	}

	private void Relay_In_45()
	{
		logic_uScript_EnableMusic_uScript_EnableMusic_45.In();
		if (logic_uScript_EnableMusic_uScript_EnableMusic_45.Out)
		{
			Relay_In_3();
		}
	}

	private void Relay_In_46()
	{
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_46.In(logic_uScript_DisablePlayerInput_disableInput_46);
		if (logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_46.Out)
		{
			Relay_In_45();
		}
	}

	private void Relay_In_47()
	{
		logic_uScript_Save_uScript_Save_47.In(logic_uScript_Save_state_47);
		if (logic_uScript_Save_uScript_Save_47.Out)
		{
			Relay_SetMainDefault_214();
		}
	}

	private void Relay_In_48()
	{
		logic_uScript_SKU_uScript_SKU_48.In();
		bool show = logic_uScript_SKU_uScript_SKU_48.Show;
		bool demo = logic_uScript_SKU_uScript_SKU_48.Demo;
		bool normal = logic_uScript_SKU_uScript_SKU_48.Normal;
		if (show)
		{
			Relay_In_480();
		}
		if (demo)
		{
			Relay_In_123();
		}
		if (normal)
		{
			Relay_In_480();
		}
	}

	private void Relay_In_49()
	{
		logic_uScript_IsDebugSkip_uScript_IsDebugSkip_49.In();
		bool num = logic_uScript_IsDebugSkip_uScript_IsDebugSkip_49.True;
		bool flag = logic_uScript_IsDebugSkip_uScript_IsDebugSkip_49.False;
		if (num)
		{
			Relay_In_338();
		}
		if (flag)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_50()
	{
		logic_uScript_LockPause_uScript_LockPause_50.In(logic_uScript_LockPause_lockPause_50, logic_uScript_LockPause_disabledReason_50);
		if (logic_uScript_LockPause_uScript_LockPause_50.Out)
		{
			Relay_In_98();
		}
	}

	private void Relay_In_51()
	{
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_51.In(logic_uScript_DisablePlayerInput_disableInput_51);
		if (logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_51.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_In_52()
	{
		logic_uScript_EnableMusic_uScript_EnableMusic_52.In();
		if (logic_uScript_EnableMusic_uScript_EnableMusic_52.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_SetMainDefault_53()
	{
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_53.SetMainDefault(logic_uScript_SetGrabDistance_dist_53);
		if (logic_uScript_SetGrabDistance_uScript_SetGrabDistance_53.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_SetDefault_53()
	{
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_53.SetDefault(logic_uScript_SetGrabDistance_dist_53);
		if (logic_uScript_SetGrabDistance_uScript_SetGrabDistance_53.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_Set_53()
	{
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_53.Set(logic_uScript_SetGrabDistance_dist_53);
		if (logic_uScript_SetGrabDistance_uScript_SetGrabDistance_53.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_54()
	{
		logic_uScript_ShowPlayerProfile_uScript_ShowPlayerProfile_54.In();
		if (logic_uScript_ShowPlayerProfile_uScript_ShowPlayerProfile_54.Out)
		{
			Relay_In_216();
		}
	}

	private void Relay_In_55()
	{
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_55.In(logic_uScript_SetPlacementFilter_placementFilter_55);
		if (logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_55.Out)
		{
			Relay_In_176();
		}
	}

	private void Relay_In_56()
	{
		logic_uScript_Wait_uScript_Wait_56.In(logic_uScript_Wait_seconds_56, logic_uScript_Wait_repeat_56);
		if (logic_uScript_Wait_uScript_Wait_56.Waited)
		{
			Relay_In_472();
		}
	}

	private void Relay_OnUpdate_65()
	{
		Relay_In_49();
	}

	private void Relay_OnSuspend_65()
	{
	}

	private void Relay_OnResume_65()
	{
	}

	private void Relay_True_66()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_66.True(out logic_uScriptAct_SetBool_Target_66);
		local_Msg4OnScreen_System_Boolean = logic_uScriptAct_SetBool_Target_66;
	}

	private void Relay_False_66()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_66.False(out logic_uScriptAct_SetBool_Target_66);
		local_Msg4OnScreen_System_Boolean = logic_uScriptAct_SetBool_Target_66;
	}

	private void Relay_True_68()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_68.True(out logic_uScriptAct_SetBool_Target_68);
		local_Msg4OnScreen_System_Boolean = logic_uScriptAct_SetBool_Target_68;
	}

	private void Relay_False_68()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_68.False(out logic_uScriptAct_SetBool_Target_68);
		local_Msg4OnScreen_System_Boolean = logic_uScriptAct_SetBool_Target_68;
	}

	private void Relay_In_70()
	{
		logic_uScriptCon_CompareBool_Bool_70 = local_Msg4OnScreen_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_70.In(logic_uScriptCon_CompareBool_Bool_70);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_70.False)
		{
			Relay_In_279();
		}
	}

	private void Relay_In_72()
	{
		logic_uScript_Wait_uScript_Wait_72.In(logic_uScript_Wait_seconds_72, logic_uScript_Wait_repeat_72);
		if (logic_uScript_Wait_uScript_Wait_72.Waited)
		{
			Relay_In_267();
			Relay_In_170();
		}
	}

	private void Relay_TrySpawnOnTech_73()
	{
		int num = 0;
		Array array = ghostBlockStandard;
		if (logic_uScript_SpawnGhostBlocks_ghostBlockData_73.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_73, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_73, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnGhostBlocks_ownerNode_73 = owner_Connection_79;
		logic_uScript_SpawnGhostBlocks_targetTech_73 = local_PlayerTech_Tank;
		logic_uScript_SpawnGhostBlocks_Return_73 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_73.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_73, logic_uScript_SpawnGhostBlocks_ownerNode_73, logic_uScript_SpawnGhostBlocks_targetTech_73);
		local_198_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_73;
		if (logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_73.OnAlreadySpawned)
		{
			Relay_AtIndex_200();
		}
	}

	private void Relay_In_74()
	{
		logic_uScriptCon_CompareBool_Bool_74 = local_GhostBlockStandardSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.In(logic_uScriptCon_CompareBool_Bool_74);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.False;
		if (num)
		{
			Relay_In_197();
		}
		if (flag)
		{
			Relay_True_75();
		}
	}

	private void Relay_True_75()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_75.True(out logic_uScriptAct_SetBool_Target_75);
		local_GhostBlockStandardSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_75;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_75.Out)
		{
			Relay_TrySpawnOnTech_73();
		}
	}

	private void Relay_False_75()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_75.False(out logic_uScriptAct_SetBool_Target_75);
		local_GhostBlockStandardSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_75;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_75.Out)
		{
			Relay_TrySpawnOnTech_73();
		}
	}

	private void Relay_TrySpawnOnTech_81()
	{
		int num = 0;
		Array array = ghostBlockWheels;
		if (logic_uScript_SpawnGhostBlocks_ghostBlockData_81.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_81, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_81, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnGhostBlocks_ownerNode_81 = owner_Connection_82;
		logic_uScript_SpawnGhostBlocks_targetTech_81 = local_PlayerTech_Tank;
		logic_uScript_SpawnGhostBlocks_Return_81 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_81.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_81, logic_uScript_SpawnGhostBlocks_ownerNode_81, logic_uScript_SpawnGhostBlocks_targetTech_81);
		local_GhostBlockWheels_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_81;
		if (logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_81.OnAlreadySpawned)
		{
			Relay_AtIndex_218();
		}
	}

	private void Relay_True_85()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_85.True(out logic_uScriptAct_SetBool_Target_85);
		local_GhostBlockGunSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_85;
	}

	private void Relay_False_85()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_85.False(out logic_uScriptAct_SetBool_Target_85);
		local_GhostBlockGunSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_85;
	}

	private void Relay_In_86()
	{
		logic_uScriptCon_CompareBool_Bool_86 = local_GhostBlockGunSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86.In(logic_uScriptCon_CompareBool_Bool_86);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_86.False;
		if (num)
		{
			Relay_In_204();
		}
		if (flag)
		{
			Relay_True_85();
		}
	}

	private void Relay_TrySpawnOnTech_89()
	{
		int num = 0;
		Array array = ghostBlockGun;
		if (logic_uScript_SpawnGhostBlocks_ghostBlockData_89.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_89, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_89, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnGhostBlocks_ownerNode_89 = owner_Connection_87;
		logic_uScript_SpawnGhostBlocks_targetTech_89 = local_PlayerTech_Tank;
		logic_uScript_SpawnGhostBlocks_Return_89 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_89.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_89, logic_uScript_SpawnGhostBlocks_ownerNode_89, logic_uScript_SpawnGhostBlocks_targetTech_89);
		local_206_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_89;
	}

	private void Relay_True_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.True(out logic_uScriptAct_SetBool_Target_93);
		local_GhostBlockDrillSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_93;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_93.Out)
		{
			Relay_TrySpawnOnTech_96();
		}
	}

	private void Relay_False_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.False(out logic_uScriptAct_SetBool_Target_93);
		local_GhostBlockDrillSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_93;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_93.Out)
		{
			Relay_TrySpawnOnTech_96();
		}
	}

	private void Relay_In_95()
	{
		logic_uScriptCon_CompareBool_Bool_95 = local_GhostBlockDrillSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.In(logic_uScriptCon_CompareBool_Bool_95);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.False;
		if (num)
		{
			Relay_In_210();
		}
		if (flag)
		{
			Relay_True_93();
		}
	}

	private void Relay_TrySpawnOnTech_96()
	{
		int num = 0;
		Array array = ghostBlockDrill;
		if (logic_uScript_SpawnGhostBlocks_ghostBlockData_96.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_96, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_96, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnGhostBlocks_ownerNode_96 = owner_Connection_91;
		logic_uScript_SpawnGhostBlocks_targetTech_96 = local_PlayerTech_Tank;
		logic_uScript_SpawnGhostBlocks_Return_96 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_96.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_96, logic_uScript_SpawnGhostBlocks_ownerNode_96, logic_uScript_SpawnGhostBlocks_targetTech_96);
		local_212_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_96;
		if (logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_96.OnAlreadySpawned)
		{
			Relay_AtIndex_211();
		}
	}

	private void Relay_In_98()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_98.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_98.Out)
		{
			Relay_In_492();
		}
	}

	private void Relay_In_99()
	{
		logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_99 = local_PlayerTech_Tank;
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_99.In(logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_99);
		if (logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_99.Out)
		{
			Relay_In_389();
		}
	}

	private void Relay_In_101()
	{
		logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_101 = local_PlayerTech_Tank;
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_101.In(logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_101);
		if (logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_101.Out)
		{
			Relay_In_471();
		}
	}

	private void Relay_In_103()
	{
		logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_103 = local_PlayerTech_Tank;
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_103.In(logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_103);
		if (logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_103.Out)
		{
			Relay_In_426();
		}
	}

	private void Relay_In_106()
	{
		logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_106 = local_PlayerTech_Tank;
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_106.In(logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_106);
		if (logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_106.Out)
		{
			Relay_In_429();
		}
	}

	private void Relay_SaveEvent_107()
	{
		Relay_Save_109();
	}

	private void Relay_LoadEvent_107()
	{
		Relay_Load_109();
	}

	private void Relay_RestartEvent_107()
	{
		Relay_Restart_109();
	}

	private void Relay_Save_Out_109()
	{
		Relay_Save_515();
	}

	private void Relay_Load_Out_109()
	{
		Relay_Load_515();
	}

	private void Relay_Restart_Out_109()
	{
		Relay_Set_False_515();
	}

	private void Relay_Save_109()
	{
		logic_SubGraph_SaveLoadInt_integer_109 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_109 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Save(logic_SubGraph_SaveLoadInt_restartValue_109, ref logic_SubGraph_SaveLoadInt_integer_109, logic_SubGraph_SaveLoadInt_intAsVariable_109, logic_SubGraph_SaveLoadInt_uniqueID_109);
	}

	private void Relay_Load_109()
	{
		logic_SubGraph_SaveLoadInt_integer_109 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_109 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Load(logic_SubGraph_SaveLoadInt_restartValue_109, ref logic_SubGraph_SaveLoadInt_integer_109, logic_SubGraph_SaveLoadInt_intAsVariable_109, logic_SubGraph_SaveLoadInt_uniqueID_109);
	}

	private void Relay_Restart_109()
	{
		logic_SubGraph_SaveLoadInt_integer_109 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_109 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_109.Restart(logic_SubGraph_SaveLoadInt_restartValue_109, ref logic_SubGraph_SaveLoadInt_integer_109, logic_SubGraph_SaveLoadInt_intAsVariable_109, logic_SubGraph_SaveLoadInt_uniqueID_109);
	}

	private void Relay_Save_Out_111()
	{
		Relay_Save_113();
	}

	private void Relay_Load_Out_111()
	{
		Relay_Load_113();
	}

	private void Relay_Restart_Out_111()
	{
		Relay_Set_False_113();
	}

	private void Relay_Save_111()
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = local_GhostBlockStandardSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_111 = local_GhostBlockStandardSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Save(ref logic_SubGraph_SaveLoadBool_boolean_111, logic_SubGraph_SaveLoadBool_boolAsVariable_111, logic_SubGraph_SaveLoadBool_uniqueID_111);
	}

	private void Relay_Load_111()
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = local_GhostBlockStandardSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_111 = local_GhostBlockStandardSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Load(ref logic_SubGraph_SaveLoadBool_boolean_111, logic_SubGraph_SaveLoadBool_boolAsVariable_111, logic_SubGraph_SaveLoadBool_uniqueID_111);
	}

	private void Relay_Set_True_111()
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = local_GhostBlockStandardSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_111 = local_GhostBlockStandardSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_111, logic_SubGraph_SaveLoadBool_boolAsVariable_111, logic_SubGraph_SaveLoadBool_uniqueID_111);
	}

	private void Relay_Set_False_111()
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = local_GhostBlockStandardSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_111 = local_GhostBlockStandardSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_111, logic_SubGraph_SaveLoadBool_boolAsVariable_111, logic_SubGraph_SaveLoadBool_uniqueID_111);
	}

	private void Relay_Save_Out_113()
	{
		Relay_Save_116();
	}

	private void Relay_Load_Out_113()
	{
		Relay_Load_116();
	}

	private void Relay_Restart_Out_113()
	{
		Relay_Set_False_116();
	}

	private void Relay_Save_113()
	{
		logic_SubGraph_SaveLoadBool_boolean_113 = local_GhostBlockWheelsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_113 = local_GhostBlockWheelsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.Save(ref logic_SubGraph_SaveLoadBool_boolean_113, logic_SubGraph_SaveLoadBool_boolAsVariable_113, logic_SubGraph_SaveLoadBool_uniqueID_113);
	}

	private void Relay_Load_113()
	{
		logic_SubGraph_SaveLoadBool_boolean_113 = local_GhostBlockWheelsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_113 = local_GhostBlockWheelsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.Load(ref logic_SubGraph_SaveLoadBool_boolean_113, logic_SubGraph_SaveLoadBool_boolAsVariable_113, logic_SubGraph_SaveLoadBool_uniqueID_113);
	}

	private void Relay_Set_True_113()
	{
		logic_SubGraph_SaveLoadBool_boolean_113 = local_GhostBlockWheelsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_113 = local_GhostBlockWheelsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_113, logic_SubGraph_SaveLoadBool_boolAsVariable_113, logic_SubGraph_SaveLoadBool_uniqueID_113);
	}

	private void Relay_Set_False_113()
	{
		logic_SubGraph_SaveLoadBool_boolean_113 = local_GhostBlockWheelsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_113 = local_GhostBlockWheelsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_113.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_113, logic_SubGraph_SaveLoadBool_boolAsVariable_113, logic_SubGraph_SaveLoadBool_uniqueID_113);
	}

	private void Relay_Save_Out_116()
	{
		Relay_Save_117();
	}

	private void Relay_Load_Out_116()
	{
		Relay_Load_117();
	}

	private void Relay_Restart_Out_116()
	{
		Relay_Set_False_117();
	}

	private void Relay_Save_116()
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = local_GhostBlockGunSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_116 = local_GhostBlockGunSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Save(ref logic_SubGraph_SaveLoadBool_boolean_116, logic_SubGraph_SaveLoadBool_boolAsVariable_116, logic_SubGraph_SaveLoadBool_uniqueID_116);
	}

	private void Relay_Load_116()
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = local_GhostBlockGunSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_116 = local_GhostBlockGunSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Load(ref logic_SubGraph_SaveLoadBool_boolean_116, logic_SubGraph_SaveLoadBool_boolAsVariable_116, logic_SubGraph_SaveLoadBool_uniqueID_116);
	}

	private void Relay_Set_True_116()
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = local_GhostBlockGunSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_116 = local_GhostBlockGunSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_116, logic_SubGraph_SaveLoadBool_boolAsVariable_116, logic_SubGraph_SaveLoadBool_uniqueID_116);
	}

	private void Relay_Set_False_116()
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = local_GhostBlockGunSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_116 = local_GhostBlockGunSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_116, logic_SubGraph_SaveLoadBool_boolAsVariable_116, logic_SubGraph_SaveLoadBool_uniqueID_116);
	}

	private void Relay_Save_Out_117()
	{
		Relay_Save_119();
	}

	private void Relay_Load_Out_117()
	{
		Relay_Load_119();
	}

	private void Relay_Restart_Out_117()
	{
		Relay_Set_False_119();
	}

	private void Relay_Save_117()
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = local_GhostBlockDrillSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_117 = local_GhostBlockDrillSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Save(ref logic_SubGraph_SaveLoadBool_boolean_117, logic_SubGraph_SaveLoadBool_boolAsVariable_117, logic_SubGraph_SaveLoadBool_uniqueID_117);
	}

	private void Relay_Load_117()
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = local_GhostBlockDrillSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_117 = local_GhostBlockDrillSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Load(ref logic_SubGraph_SaveLoadBool_boolean_117, logic_SubGraph_SaveLoadBool_boolAsVariable_117, logic_SubGraph_SaveLoadBool_uniqueID_117);
	}

	private void Relay_Set_True_117()
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = local_GhostBlockDrillSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_117 = local_GhostBlockDrillSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_117, logic_SubGraph_SaveLoadBool_boolAsVariable_117, logic_SubGraph_SaveLoadBool_uniqueID_117);
	}

	private void Relay_Set_False_117()
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = local_GhostBlockDrillSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_117 = local_GhostBlockDrillSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_117, logic_SubGraph_SaveLoadBool_boolAsVariable_117, logic_SubGraph_SaveLoadBool_uniqueID_117);
	}

	private void Relay_Save_Out_119()
	{
		Relay_Save_350();
	}

	private void Relay_Load_Out_119()
	{
		Relay_Load_350();
	}

	private void Relay_Restart_Out_119()
	{
		Relay_Set_False_350();
	}

	private void Relay_Save_119()
	{
		logic_SubGraph_SaveLoadBool_boolean_119 = local_Msg4OnScreen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_119 = local_Msg4OnScreen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Save(ref logic_SubGraph_SaveLoadBool_boolean_119, logic_SubGraph_SaveLoadBool_boolAsVariable_119, logic_SubGraph_SaveLoadBool_uniqueID_119);
	}

	private void Relay_Load_119()
	{
		logic_SubGraph_SaveLoadBool_boolean_119 = local_Msg4OnScreen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_119 = local_Msg4OnScreen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Load(ref logic_SubGraph_SaveLoadBool_boolean_119, logic_SubGraph_SaveLoadBool_boolAsVariable_119, logic_SubGraph_SaveLoadBool_uniqueID_119);
	}

	private void Relay_Set_True_119()
	{
		logic_SubGraph_SaveLoadBool_boolean_119 = local_Msg4OnScreen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_119 = local_Msg4OnScreen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_119, logic_SubGraph_SaveLoadBool_boolAsVariable_119, logic_SubGraph_SaveLoadBool_uniqueID_119);
	}

	private void Relay_Set_False_119()
	{
		logic_SubGraph_SaveLoadBool_boolean_119 = local_Msg4OnScreen_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_119 = local_Msg4OnScreen_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_119.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_119, logic_SubGraph_SaveLoadBool_boolAsVariable_119, logic_SubGraph_SaveLoadBool_uniqueID_119);
	}

	private void Relay_In_121()
	{
		logic_uScriptAct_SetFloat_uScriptAct_SetFloat_121.In(logic_uScriptAct_SetFloat_Value_121, out logic_uScriptAct_SetFloat_Target_121);
		local_InitialMsgTime_System_Single = logic_uScriptAct_SetFloat_Target_121;
	}

	private void Relay_In_123()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_123.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_123.Out)
		{
			Relay_In_517();
		}
	}

	private void Relay_In_139()
	{
		logic_uScript_HideArrow_uScript_HideArrow_139.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_139.Out)
		{
			Relay_In_25();
		}
	}

	private void Relay_In_144()
	{
		logic_uScript_BlockAttachedToTech_tech_144 = local_PlayerTech_Tank;
		logic_uScript_BlockAttachedToTech_block_144 = local_Wheel01_TankBlock;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_144.In(logic_uScript_BlockAttachedToTech_tech_144, logic_uScript_BlockAttachedToTech_block_144);
		bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_144.True;
		bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_144.False;
		if (num)
		{
			Relay_In_153();
		}
		if (flag)
		{
			Relay_In_147();
		}
	}

	private void Relay_In_147()
	{
		logic_uScript_PointArrowAtVisible_targetObject_147 = local_Wheel01_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_147.In(logic_uScript_PointArrowAtVisible_targetObject_147, logic_uScript_PointArrowAtVisible_timeToShowFor_147, logic_uScript_PointArrowAtVisible_offset_147);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_147.Out)
		{
			Relay_In_436();
		}
	}

	private void Relay_In_149()
	{
		logic_uScript_PointArrowAtVisible_targetObject_149 = local_Wheel02_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_149.In(logic_uScript_PointArrowAtVisible_targetObject_149, logic_uScript_PointArrowAtVisible_timeToShowFor_149, logic_uScript_PointArrowAtVisible_offset_149);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_149.Out)
		{
			Relay_In_437();
		}
	}

	private void Relay_In_153()
	{
		logic_uScript_BlockAttachedToTech_tech_153 = local_PlayerTech_Tank;
		logic_uScript_BlockAttachedToTech_block_153 = local_Wheel02_TankBlock;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_153.In(logic_uScript_BlockAttachedToTech_tech_153, logic_uScript_BlockAttachedToTech_block_153);
		bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_153.True;
		bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_153.False;
		if (num)
		{
			Relay_In_163();
		}
		if (flag)
		{
			Relay_In_149();
		}
	}

	private void Relay_In_154()
	{
		logic_uScript_PointArrowAtVisible_targetObject_154 = local_Wheel03_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_154.In(logic_uScript_PointArrowAtVisible_targetObject_154, logic_uScript_PointArrowAtVisible_timeToShowFor_154, logic_uScript_PointArrowAtVisible_offset_154);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_154.Out)
		{
			Relay_In_438();
		}
	}

	private void Relay_In_156()
	{
		logic_uScript_BlockAttachedToTech_tech_156 = local_PlayerTech_Tank;
		logic_uScript_BlockAttachedToTech_block_156 = local_Wheel04_TankBlock;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_156.In(logic_uScript_BlockAttachedToTech_tech_156, logic_uScript_BlockAttachedToTech_block_156);
		bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_156.True;
		bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_156.False;
		if (num)
		{
			Relay_In_180();
		}
		if (flag)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_158()
	{
		logic_uScript_PointArrowAtVisible_targetObject_158 = local_Wheel04_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_158.In(logic_uScript_PointArrowAtVisible_targetObject_158, logic_uScript_PointArrowAtVisible_timeToShowFor_158, logic_uScript_PointArrowAtVisible_offset_158);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_158.Out)
		{
			Relay_In_439();
		}
	}

	private void Relay_In_163()
	{
		logic_uScript_BlockAttachedToTech_tech_163 = local_PlayerTech_Tank;
		logic_uScript_BlockAttachedToTech_block_163 = local_Wheel03_TankBlock;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_163.In(logic_uScript_BlockAttachedToTech_tech_163, logic_uScript_BlockAttachedToTech_block_163);
		bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_163.True;
		bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_163.False;
		if (num)
		{
			Relay_In_156();
		}
		if (flag)
		{
			Relay_In_154();
		}
	}

	private void Relay_In_164()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_164.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_164.Out)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_165()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_165.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_165.Out)
		{
			Relay_In_166();
		}
	}

	private void Relay_In_166()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_166.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_166.Out)
		{
			Relay_In_167();
		}
	}

	private void Relay_In_167()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_167.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_167.Out)
		{
			Relay_In_180();
		}
	}

	private void Relay_In_169()
	{
		logic_uScript_PointArrowAtVisible_targetObject_169 = local_PlayerTech_Tank;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_169.In(logic_uScript_PointArrowAtVisible_targetObject_169, logic_uScript_PointArrowAtVisible_timeToShowFor_169, logic_uScript_PointArrowAtVisible_offset_169);
	}

	private void Relay_In_170()
	{
		logic_uScript_Wait_uScript_Wait_170.In(logic_uScript_Wait_seconds_170, logic_uScript_Wait_repeat_170);
		if (logic_uScript_Wait_uScript_Wait_170.Waited)
		{
			Relay_In_172();
		}
	}

	private void Relay_In_172()
	{
		logic_uScript_GetPlayerTank_Return_172 = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_172.In();
		local_PlayerTech_Tank = logic_uScript_GetPlayerTank_Return_172;
		if (logic_uScript_GetPlayerTank_uScript_GetPlayerTank_172.Returned)
		{
			Relay_In_169();
		}
	}

	private void Relay_In_173()
	{
		logic_uScript_HideArrow_uScript_HideArrow_173.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_173.Out)
		{
			Relay_In_382();
		}
	}

	private void Relay_In_174()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_174 = local_StandardBlock_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_174.In(logic_uScript_IsPlayerInteractingWithBlock_block_174);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_174.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_174.NotDragging;
		if (dragging)
		{
			Relay_In_74();
		}
		if (notDragging)
		{
			Relay_In_196();
		}
	}

	private void Relay_In_176()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_176 = local_GunBlock_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_176.In(logic_uScript_IsPlayerInteractingWithBlock_block_176);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_176.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_176.NotDragging;
		if (dragging)
		{
			Relay_In_86();
		}
		if (notDragging)
		{
			Relay_In_202();
		}
	}

	private void Relay_In_178()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_178 = local_DrillBlock_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_178.In(logic_uScript_IsPlayerInteractingWithBlock_block_178);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_178.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_178.NotDragging;
		if (dragging)
		{
			Relay_In_95();
		}
		if (notDragging)
		{
			Relay_In_208();
		}
	}

	private void Relay_In_180()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_180 = local_Wheel01_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_180.In(logic_uScript_IsPlayerInteractingWithBlock_block_180);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_180.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_180.NotDragging;
		if (dragging)
		{
			Relay_In_192();
		}
		if (notDragging)
		{
			Relay_In_185();
		}
	}

	private void Relay_In_181()
	{
		logic_uScriptCon_CompareBool_Bool_181 = local_GhostBlockWheelsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.In(logic_uScriptCon_CompareBool_Bool_181);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.False;
		if (num)
		{
			Relay_In_257();
		}
		if (flag)
		{
			Relay_TrySpawnOnTech_81();
		}
	}

	private void Relay_In_185()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_185 = local_Wheel02_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_185.In(logic_uScript_IsPlayerInteractingWithBlock_block_185);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_185.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_185.NotDragging;
		if (dragging)
		{
			Relay_In_193();
		}
		if (notDragging)
		{
			Relay_In_188();
		}
	}

	private void Relay_In_188()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_188 = local_Wheel03_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_188.In(logic_uScript_IsPlayerInteractingWithBlock_block_188);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_188.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_188.NotDragging;
		if (dragging)
		{
			Relay_In_194();
		}
		if (notDragging)
		{
			Relay_In_189();
		}
	}

	private void Relay_In_189()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_189 = local_Wheel04_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_189.In(logic_uScript_IsPlayerInteractingWithBlock_block_189);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_189.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_189.NotDragging;
		if (dragging)
		{
			Relay_True_255();
		}
		if (notDragging)
		{
			Relay_False_255();
		}
	}

	private void Relay_True_190()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_190.True(out logic_uScriptAct_SetBool_Target_190);
		local_GhostBlockWheelsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_190;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_190.Out)
		{
			Relay_In_233();
		}
	}

	private void Relay_False_190()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_190.False(out logic_uScriptAct_SetBool_Target_190);
		local_GhostBlockWheelsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_190;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_190.Out)
		{
			Relay_In_233();
		}
	}

	private void Relay_In_192()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_192.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_192.Out)
		{
			Relay_In_193();
		}
	}

	private void Relay_In_193()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_193.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_193.Out)
		{
			Relay_In_194();
		}
	}

	private void Relay_In_194()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_194.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_194.Out)
		{
			Relay_True_255();
		}
	}

	private void Relay_In_196()
	{
		logic_uScript_PointArrowAtVisible_targetObject_196 = local_StandardBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_196.In(logic_uScript_PointArrowAtVisible_targetObject_196, logic_uScript_PointArrowAtVisible_timeToShowFor_196, logic_uScript_PointArrowAtVisible_offset_196);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_196.Out)
		{
			Relay_In_386();
		}
	}

	private void Relay_In_197()
	{
		logic_uScript_PointArrowAtVisible_targetObject_197 = local_GhostBlockStandard_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_197.In(logic_uScript_PointArrowAtVisible_targetObject_197, logic_uScript_PointArrowAtVisible_timeToShowFor_197, logic_uScript_PointArrowAtVisible_offset_197);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_197.Out)
		{
			Relay_In_384();
		}
	}

	private void Relay_AtIndex_200()
	{
		int num = 0;
		Array array = local_198_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_200.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_200, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_200, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_200.AtIndex(ref logic_uScript_AccessListBlock_blockList_200, logic_uScript_AccessListBlock_index_200, out logic_uScript_AccessListBlock_value_200);
		local_198_TankBlockArray = logic_uScript_AccessListBlock_blockList_200;
		local_GhostBlockStandard_TankBlock = logic_uScript_AccessListBlock_value_200;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_200.Out)
		{
			Relay_In_197();
		}
	}

	private void Relay_In_202()
	{
		logic_uScript_PointArrowAtVisible_targetObject_202 = local_GunBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_202.In(logic_uScript_PointArrowAtVisible_targetObject_202, logic_uScript_PointArrowAtVisible_timeToShowFor_202, logic_uScript_PointArrowAtVisible_offset_202);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_202.Out)
		{
			Relay_In_404();
		}
	}

	private void Relay_In_204()
	{
		logic_uScript_PointArrowAtVisible_targetObject_204 = local_GhostBlockGun_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_204.In(logic_uScript_PointArrowAtVisible_targetObject_204, logic_uScript_PointArrowAtVisible_timeToShowFor_204, logic_uScript_PointArrowAtVisible_offset_204);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_204.Out)
		{
			Relay_In_411();
		}
	}

	private void Relay_AtIndex_205()
	{
		int num = 0;
		Array array = local_206_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_205.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_205, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_205, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_205.AtIndex(ref logic_uScript_AccessListBlock_blockList_205, logic_uScript_AccessListBlock_index_205, out logic_uScript_AccessListBlock_value_205);
		local_206_TankBlockArray = logic_uScript_AccessListBlock_blockList_205;
		local_GhostBlockGun_TankBlock = logic_uScript_AccessListBlock_value_205;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_205.Out)
		{
			Relay_In_204();
		}
	}

	private void Relay_In_208()
	{
		logic_uScript_PointArrowAtVisible_targetObject_208 = local_DrillBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_208.In(logic_uScript_PointArrowAtVisible_targetObject_208, logic_uScript_PointArrowAtVisible_timeToShowFor_208, logic_uScript_PointArrowAtVisible_offset_208);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_208.Out)
		{
			Relay_In_418();
		}
	}

	private void Relay_In_210()
	{
		logic_uScript_PointArrowAtVisible_targetObject_210 = local_GhostBlockDrill_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_210.In(logic_uScript_PointArrowAtVisible_targetObject_210, logic_uScript_PointArrowAtVisible_timeToShowFor_210, logic_uScript_PointArrowAtVisible_offset_210);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_210.Out)
		{
			Relay_In_422();
		}
	}

	private void Relay_AtIndex_211()
	{
		int num = 0;
		Array array = local_212_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_211.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_211, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_211, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_211.AtIndex(ref logic_uScript_AccessListBlock_blockList_211, logic_uScript_AccessListBlock_index_211, out logic_uScript_AccessListBlock_value_211);
		local_212_TankBlockArray = logic_uScript_AccessListBlock_blockList_211;
		local_GhostBlockDrill_TankBlock = logic_uScript_AccessListBlock_value_211;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_211.Out)
		{
			Relay_In_210();
		}
	}

	private void Relay_SetMainDefault_214()
	{
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_214.SetMainDefault(logic_uScript_SetGrabDistance_dist_214);
		if (logic_uScript_SetGrabDistance_uScript_SetGrabDistance_214.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_SetDefault_214()
	{
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_214.SetDefault(logic_uScript_SetGrabDistance_dist_214);
		if (logic_uScript_SetGrabDistance_uScript_SetGrabDistance_214.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_Set_214()
	{
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_214.Set(logic_uScript_SetGrabDistance_dist_214);
		if (logic_uScript_SetGrabDistance_uScript_SetGrabDistance_214.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_Enable_215()
	{
		logic_uScript_SetInvadersActive_uScript_SetInvadersActive_215.Enable();
		if (logic_uScript_SetInvadersActive_uScript_SetInvadersActive_215.Out)
		{
			Relay_Succeed_266();
		}
	}

	private void Relay_Disable_215()
	{
		logic_uScript_SetInvadersActive_uScript_SetInvadersActive_215.Disable();
		if (logic_uScript_SetInvadersActive_uScript_SetInvadersActive_215.Out)
		{
			Relay_Succeed_266();
		}
	}

	private void Relay_In_216()
	{
		logic_uScript_SKU_uScript_SKU_216.In();
		bool show = logic_uScript_SKU_uScript_SKU_216.Show;
		bool demo = logic_uScript_SKU_uScript_SKU_216.Demo;
		bool normal = logic_uScript_SKU_uScript_SKU_216.Normal;
		if (show)
		{
			Relay_Enable_215();
		}
		if (demo)
		{
			Relay_Succeed_266();
		}
		if (normal)
		{
			Relay_Succeed_266();
		}
	}

	private void Relay_AtIndex_218()
	{
		int num = 0;
		Array array = local_GhostBlockWheels_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_218.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_218, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_218, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_218.AtIndex(ref logic_uScript_AccessListBlock_blockList_218, logic_uScript_AccessListBlock_index_218, out logic_uScript_AccessListBlock_value_218);
		local_GhostBlockWheels_TankBlockArray = logic_uScript_AccessListBlock_blockList_218;
		local_GhostBlockWheel01_TankBlock = logic_uScript_AccessListBlock_value_218;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_218.Out)
		{
			Relay_AtIndex_223();
		}
	}

	private void Relay_AtIndex_223()
	{
		int num = 0;
		Array array = local_GhostBlockWheels_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_223.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_223, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_223, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_223.AtIndex(ref logic_uScript_AccessListBlock_blockList_223, logic_uScript_AccessListBlock_index_223, out logic_uScript_AccessListBlock_value_223);
		local_GhostBlockWheels_TankBlockArray = logic_uScript_AccessListBlock_blockList_223;
		local_GhostBlockWheel02_TankBlock = logic_uScript_AccessListBlock_value_223;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_223.Out)
		{
			Relay_AtIndex_228();
		}
	}

	private void Relay_AtIndex_227()
	{
		int num = 0;
		Array array = local_GhostBlockWheels_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_227.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_227, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_227, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_227.AtIndex(ref logic_uScript_AccessListBlock_blockList_227, logic_uScript_AccessListBlock_index_227, out logic_uScript_AccessListBlock_value_227);
		local_GhostBlockWheels_TankBlockArray = logic_uScript_AccessListBlock_blockList_227;
		local_GhostBlockWheel04_TankBlock = logic_uScript_AccessListBlock_value_227;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_227.Out)
		{
			Relay_True_190();
		}
	}

	private void Relay_AtIndex_228()
	{
		int num = 0;
		Array array = local_GhostBlockWheels_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_228.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_228, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_228, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_228.AtIndex(ref logic_uScript_AccessListBlock_blockList_228, logic_uScript_AccessListBlock_index_228, out logic_uScript_AccessListBlock_value_228);
		local_GhostBlockWheels_TankBlockArray = logic_uScript_AccessListBlock_blockList_228;
		local_GhostBlockWheel03_TankBlock = logic_uScript_AccessListBlock_value_228;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_228.Out)
		{
			Relay_AtIndex_227();
		}
	}

	private void Relay_In_230()
	{
		logic_uScript_DoesTechHaveBlockAtPosition_tech_230 = local_PlayerTech_Tank;
		logic_uScript_DoesTechHaveBlockAtPosition_blockType_230 = blockTypeWheel;
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_230.In(logic_uScript_DoesTechHaveBlockAtPosition_tech_230, logic_uScript_DoesTechHaveBlockAtPosition_blockType_230, logic_uScript_DoesTechHaveBlockAtPosition_localPosition_230);
		bool num = logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_230.True;
		bool flag = logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_230.False;
		if (num)
		{
			Relay_In_235();
		}
		if (flag)
		{
			Relay_In_243();
		}
	}

	private void Relay_In_233()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_233.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_233.Out)
		{
			Relay_In_258();
		}
	}

	private void Relay_In_235()
	{
		logic_uScript_DoesTechHaveBlockAtPosition_tech_235 = local_PlayerTech_Tank;
		logic_uScript_DoesTechHaveBlockAtPosition_blockType_235 = blockTypeWheel;
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_235.In(logic_uScript_DoesTechHaveBlockAtPosition_tech_235, logic_uScript_DoesTechHaveBlockAtPosition_blockType_235, logic_uScript_DoesTechHaveBlockAtPosition_localPosition_235);
		bool num = logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_235.True;
		bool flag = logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_235.False;
		if (num)
		{
			Relay_In_238();
		}
		if (flag)
		{
			Relay_In_246();
		}
	}

	private void Relay_In_238()
	{
		logic_uScript_DoesTechHaveBlockAtPosition_tech_238 = local_PlayerTech_Tank;
		logic_uScript_DoesTechHaveBlockAtPosition_blockType_238 = blockTypeWheel;
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_238.In(logic_uScript_DoesTechHaveBlockAtPosition_tech_238, logic_uScript_DoesTechHaveBlockAtPosition_blockType_238, logic_uScript_DoesTechHaveBlockAtPosition_localPosition_238);
		bool num = logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_238.True;
		bool flag = logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_238.False;
		if (num)
		{
			Relay_In_241();
		}
		if (flag)
		{
			Relay_In_249();
		}
	}

	private void Relay_In_241()
	{
		logic_uScript_DoesTechHaveBlockAtPosition_tech_241 = local_PlayerTech_Tank;
		logic_uScript_DoesTechHaveBlockAtPosition_blockType_241 = blockTypeWheel;
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_241.In(logic_uScript_DoesTechHaveBlockAtPosition_tech_241, logic_uScript_DoesTechHaveBlockAtPosition_blockType_241, logic_uScript_DoesTechHaveBlockAtPosition_localPosition_241);
		bool num = logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_241.True;
		bool flag = logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_241.False;
		if (num)
		{
			Relay_In_26();
		}
		if (flag)
		{
			Relay_In_250();
		}
	}

	private void Relay_In_243()
	{
		logic_uScript_PointArrowAtVisible_targetObject_243 = local_GhostBlockWheel01_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_243.In(logic_uScript_PointArrowAtVisible_targetObject_243, logic_uScript_PointArrowAtVisible_timeToShowFor_243, logic_uScript_PointArrowAtVisible_offset_243);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_243.Out)
		{
			Relay_In_432();
		}
	}

	private void Relay_In_246()
	{
		logic_uScript_PointArrowAtVisible_targetObject_246 = local_GhostBlockWheel02_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_246.In(logic_uScript_PointArrowAtVisible_targetObject_246, logic_uScript_PointArrowAtVisible_timeToShowFor_246, logic_uScript_PointArrowAtVisible_offset_246);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_246.Out)
		{
			Relay_In_433();
		}
	}

	private void Relay_In_249()
	{
		logic_uScript_PointArrowAtVisible_targetObject_249 = local_GhostBlockWheel03_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_249.In(logic_uScript_PointArrowAtVisible_targetObject_249, logic_uScript_PointArrowAtVisible_timeToShowFor_249, logic_uScript_PointArrowAtVisible_offset_249);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_249.Out)
		{
			Relay_In_434();
		}
	}

	private void Relay_In_250()
	{
		logic_uScript_PointArrowAtVisible_targetObject_250 = local_GhostBlockWheel04_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_250.In(logic_uScript_PointArrowAtVisible_targetObject_250, logic_uScript_PointArrowAtVisible_timeToShowFor_250, logic_uScript_PointArrowAtVisible_offset_250);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_250.Out)
		{
			Relay_In_435();
		}
	}

	private void Relay_In_251()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_251.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_251.Out)
		{
			Relay_In_252();
		}
	}

	private void Relay_In_252()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_252.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_252.Out)
		{
			Relay_In_253();
		}
	}

	private void Relay_In_253()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_253.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_253.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_True_255()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_255.True(out logic_uScriptAct_SetBool_Target_255);
		local_DraggingWheel_System_Boolean = logic_uScriptAct_SetBool_Target_255;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_255.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_255.SetFalse;
		if (setTrue)
		{
			Relay_In_181();
		}
		if (setFalse)
		{
			Relay_In_262();
		}
	}

	private void Relay_False_255()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_255.False(out logic_uScriptAct_SetBool_Target_255);
		local_DraggingWheel_System_Boolean = logic_uScriptAct_SetBool_Target_255;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_255.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_255.SetFalse;
		if (setTrue)
		{
			Relay_In_181();
		}
		if (setFalse)
		{
			Relay_In_262();
		}
	}

	private void Relay_In_257()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_257.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_257.Out)
		{
			Relay_In_233();
		}
	}

	private void Relay_In_258()
	{
		logic_uScriptCon_CompareBool_Bool_258 = local_DraggingWheel_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_258.In(logic_uScriptCon_CompareBool_Bool_258);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_258.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_258.False;
		if (num)
		{
			Relay_In_440();
		}
		if (flag)
		{
			Relay_In_457();
		}
	}

	private void Relay_Save_Out_260()
	{
	}

	private void Relay_Load_Out_260()
	{
	}

	private void Relay_Restart_Out_260()
	{
		Relay_In_121();
	}

	private void Relay_Save_260()
	{
		logic_SubGraph_SaveLoadBool_boolean_260 = local_DraggingWheel_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_260 = local_DraggingWheel_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.Save(ref logic_SubGraph_SaveLoadBool_boolean_260, logic_SubGraph_SaveLoadBool_boolAsVariable_260, logic_SubGraph_SaveLoadBool_uniqueID_260);
	}

	private void Relay_Load_260()
	{
		logic_SubGraph_SaveLoadBool_boolean_260 = local_DraggingWheel_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_260 = local_DraggingWheel_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.Load(ref logic_SubGraph_SaveLoadBool_boolean_260, logic_SubGraph_SaveLoadBool_boolAsVariable_260, logic_SubGraph_SaveLoadBool_uniqueID_260);
	}

	private void Relay_Set_True_260()
	{
		logic_SubGraph_SaveLoadBool_boolean_260 = local_DraggingWheel_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_260 = local_DraggingWheel_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_260, logic_SubGraph_SaveLoadBool_boolAsVariable_260, logic_SubGraph_SaveLoadBool_uniqueID_260);
	}

	private void Relay_Set_False_260()
	{
		logic_SubGraph_SaveLoadBool_boolean_260 = local_DraggingWheel_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_260 = local_DraggingWheel_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_260.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_260, logic_SubGraph_SaveLoadBool_boolAsVariable_260, logic_SubGraph_SaveLoadBool_uniqueID_260);
	}

	private void Relay_In_262()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_262.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_262.Out)
		{
			Relay_In_263();
		}
	}

	private void Relay_In_263()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_263.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_263.Out)
		{
			Relay_In_233();
		}
	}

	private void Relay_In_264()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_264.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_264.Out)
		{
			Relay_In_459();
		}
	}

	private void Relay_Succeed_266()
	{
		logic_uScript_FinishEncounter_owner_266 = owner_Connection_265;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_266.Succeed(logic_uScript_FinishEncounter_owner_266);
	}

	private void Relay_Fail_266()
	{
		logic_uScript_FinishEncounter_owner_266 = owner_Connection_265;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_266.Fail(logic_uScript_FinishEncounter_owner_266);
	}

	private void Relay_In_267()
	{
		logic_uScript_AddMessage_messageData_267 = msg01CabExplanation;
		logic_uScript_AddMessage_speaker_267 = messageSpeaker;
		logic_uScript_AddMessage_Return_267 = logic_uScript_AddMessage_uScript_AddMessage_267.In(logic_uScript_AddMessage_messageData_267, logic_uScript_AddMessage_speaker_267);
		if (logic_uScript_AddMessage_uScript_AddMessage_267.Shown)
		{
			Relay_In_270();
		}
	}

	private void Relay_In_270()
	{
		logic_uScriptAct_AddInt_v2_A_270 = local_Stage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_270.In(logic_uScriptAct_AddInt_v2_A_270, logic_uScriptAct_AddInt_v2_B_270, out logic_uScriptAct_AddInt_v2_IntResult_270, out logic_uScriptAct_AddInt_v2_FloatResult_270);
		local_Stage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_270;
	}

	private void Relay_In_272()
	{
		logic_uScriptAct_AddInt_v2_A_272 = local_Stage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_272.In(logic_uScriptAct_AddInt_v2_A_272, logic_uScriptAct_AddInt_v2_B_272, out logic_uScriptAct_AddInt_v2_IntResult_272, out logic_uScriptAct_AddInt_v2_FloatResult_272);
		local_Stage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_272;
	}

	private void Relay_In_274()
	{
		logic_uScriptAct_AddInt_v2_A_274 = local_Stage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_274.In(logic_uScriptAct_AddInt_v2_A_274, logic_uScriptAct_AddInt_v2_B_274, out logic_uScriptAct_AddInt_v2_IntResult_274, out logic_uScriptAct_AddInt_v2_FloatResult_274);
		local_Stage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_274;
	}

	private void Relay_In_277()
	{
		logic_uScript_AddMessage_messageData_277 = msg04AttachWheels;
		logic_uScript_AddMessage_speaker_277 = messageSpeaker;
		logic_uScript_AddMessage_Return_277 = logic_uScript_AddMessage_uScript_AddMessage_277.In(logic_uScript_AddMessage_messageData_277, logic_uScript_AddMessage_speaker_277);
		if (logic_uScript_AddMessage_uScript_AddMessage_277.Out)
		{
			Relay_True_66();
		}
	}

	private void Relay_In_279()
	{
		logic_uScript_AddMessage_messageData_279 = msg04AttachWheels;
		logic_uScript_AddMessage_speaker_279 = messageSpeaker;
		logic_uScript_AddMessage_Return_279 = logic_uScript_AddMessage_uScript_AddMessage_279.In(logic_uScript_AddMessage_messageData_279, logic_uScript_AddMessage_speaker_279);
	}

	private void Relay_In_283()
	{
		logic_uScriptAct_AddInt_v2_A_283 = local_Stage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_283.In(logic_uScriptAct_AddInt_v2_A_283, logic_uScriptAct_AddInt_v2_B_283, out logic_uScriptAct_AddInt_v2_IntResult_283, out logic_uScriptAct_AddInt_v2_FloatResult_283);
		local_Stage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_283;
	}

	private void Relay_In_284()
	{
		logic_uScript_AddMessage_messageData_284 = msg06AttachGun;
		logic_uScript_AddMessage_speaker_284 = messageSpeaker;
		logic_uScript_AddMessage_Return_284 = logic_uScript_AddMessage_uScript_AddMessage_284.In(logic_uScript_AddMessage_messageData_284, logic_uScript_AddMessage_speaker_284);
	}

	private void Relay_In_288()
	{
		logic_uScriptAct_AddInt_v2_A_288 = local_Stage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_288.In(logic_uScriptAct_AddInt_v2_A_288, logic_uScriptAct_AddInt_v2_B_288, out logic_uScriptAct_AddInt_v2_IntResult_288, out logic_uScriptAct_AddInt_v2_FloatResult_288);
		local_Stage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_288;
	}

	private void Relay_In_289()
	{
		logic_uScript_AddMessage_messageData_289 = msg07AttachDrill;
		logic_uScript_AddMessage_speaker_289 = messageSpeaker;
		logic_uScript_AddMessage_Return_289 = logic_uScript_AddMessage_uScript_AddMessage_289.In(logic_uScript_AddMessage_messageData_289, logic_uScript_AddMessage_speaker_289);
	}

	private void Relay_In_294()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_294 = blockTypeLaser;
		logic_uScript_SpawnBlockAbovePlayer_owner_294 = owner_Connection_319;
		logic_uScript_SpawnBlockAbovePlayer_Return_294 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_294.In(logic_uScript_SpawnBlockAbovePlayer_blockType_294, logic_uScript_SpawnBlockAbovePlayer_uniqueName_294, logic_uScript_SpawnBlockAbovePlayer_owner_294);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_294.Out)
		{
			Relay_In_344();
		}
	}

	private void Relay_In_298()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_298 = blockTypeWheel;
		logic_uScript_SpawnBlockAbovePlayer_owner_298 = owner_Connection_293;
		logic_uScript_SpawnBlockAbovePlayer_Return_298 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_298.In(logic_uScript_SpawnBlockAbovePlayer_blockType_298, logic_uScript_SpawnBlockAbovePlayer_uniqueName_298, logic_uScript_SpawnBlockAbovePlayer_owner_298);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_298.Out)
		{
			Relay_In_294();
		}
	}

	private void Relay_In_299()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_299 = blockTypeStandard;
		logic_uScript_SpawnBlockAbovePlayer_owner_299 = owner_Connection_311;
		logic_uScript_SpawnBlockAbovePlayer_Return_299 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_299.In(logic_uScript_SpawnBlockAbovePlayer_blockType_299, logic_uScript_SpawnBlockAbovePlayer_uniqueName_299, logic_uScript_SpawnBlockAbovePlayer_owner_299);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_299.Out)
		{
			Relay_In_304();
		}
	}

	private void Relay_In_300()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_300 = blockTypeWheel;
		logic_uScript_SpawnBlockAbovePlayer_owner_300 = owner_Connection_302;
		logic_uScript_SpawnBlockAbovePlayer_Return_300 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_300.In(logic_uScript_SpawnBlockAbovePlayer_blockType_300, logic_uScript_SpawnBlockAbovePlayer_uniqueName_300, logic_uScript_SpawnBlockAbovePlayer_owner_300);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_300.Out)
		{
			Relay_In_301();
		}
	}

	private void Relay_In_301()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_301 = blockTypeWheel;
		logic_uScript_SpawnBlockAbovePlayer_owner_301 = owner_Connection_295;
		logic_uScript_SpawnBlockAbovePlayer_Return_301 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_301.In(logic_uScript_SpawnBlockAbovePlayer_blockType_301, logic_uScript_SpawnBlockAbovePlayer_uniqueName_301, logic_uScript_SpawnBlockAbovePlayer_owner_301);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_301.Out)
		{
			Relay_In_303();
		}
	}

	private void Relay_In_303()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_303 = blockTypeWheel;
		logic_uScript_SpawnBlockAbovePlayer_owner_303 = owner_Connection_297;
		logic_uScript_SpawnBlockAbovePlayer_Return_303 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_303.In(logic_uScript_SpawnBlockAbovePlayer_blockType_303, logic_uScript_SpawnBlockAbovePlayer_uniqueName_303, logic_uScript_SpawnBlockAbovePlayer_owner_303);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_303.Out)
		{
			Relay_In_320();
		}
	}

	private void Relay_In_304()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_304 = blockTypeWheel;
		logic_uScript_SpawnBlockAbovePlayer_owner_304 = owner_Connection_321;
		logic_uScript_SpawnBlockAbovePlayer_Return_304 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_304.In(logic_uScript_SpawnBlockAbovePlayer_blockType_304, logic_uScript_SpawnBlockAbovePlayer_uniqueName_304, logic_uScript_SpawnBlockAbovePlayer_owner_304);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_304.Out)
		{
			Relay_In_298();
		}
	}

	private void Relay_In_305()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_305 = blockTypeStandard;
		logic_uScript_SpawnBlockAbovePlayer_owner_305 = owner_Connection_292;
		logic_uScript_SpawnBlockAbovePlayer_Return_305 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_305.In(logic_uScript_SpawnBlockAbovePlayer_blockType_305, logic_uScript_SpawnBlockAbovePlayer_uniqueName_305, logic_uScript_SpawnBlockAbovePlayer_owner_305);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_305.Out)
		{
			Relay_In_300();
		}
	}

	private void Relay_In_310()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_310 = blockTypeStandard;
		logic_uScript_SpawnBlockAbovePlayer_owner_310 = owner_Connection_309;
		logic_uScript_SpawnBlockAbovePlayer_Return_310 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_310.In(logic_uScript_SpawnBlockAbovePlayer_blockType_310, logic_uScript_SpawnBlockAbovePlayer_uniqueName_310, logic_uScript_SpawnBlockAbovePlayer_owner_310);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_310.Out)
		{
			Relay_In_299();
		}
	}

	private void Relay_In_314()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_314 = blockTypeGun;
		logic_uScript_SpawnBlockAbovePlayer_owner_314 = owner_Connection_315;
		logic_uScript_SpawnBlockAbovePlayer_Return_314 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_314.In(logic_uScript_SpawnBlockAbovePlayer_blockType_314, logic_uScript_SpawnBlockAbovePlayer_uniqueName_314, logic_uScript_SpawnBlockAbovePlayer_owner_314);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_314.Out)
		{
			Relay_In_322();
		}
	}

	private void Relay_In_320()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_320 = blockTypeWheel;
		logic_uScript_SpawnBlockAbovePlayer_owner_320 = owner_Connection_296;
		logic_uScript_SpawnBlockAbovePlayer_Return_320 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_320.In(logic_uScript_SpawnBlockAbovePlayer_blockType_320, logic_uScript_SpawnBlockAbovePlayer_uniqueName_320, logic_uScript_SpawnBlockAbovePlayer_owner_320);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_320.Out)
		{
			Relay_In_314();
		}
	}

	private void Relay_In_322()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_322 = blockTypeDrill;
		logic_uScript_SpawnBlockAbovePlayer_owner_322 = owner_Connection_330;
		logic_uScript_SpawnBlockAbovePlayer_Return_322 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_322.In(logic_uScript_SpawnBlockAbovePlayer_blockType_322, logic_uScript_SpawnBlockAbovePlayer_uniqueName_322, logic_uScript_SpawnBlockAbovePlayer_owner_322);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_322.Out)
		{
			Relay_In_310();
		}
	}

	private void Relay_In_327()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_327.In(logic_uScriptAct_SetInt_Value_327, out logic_uScriptAct_SetInt_Target_327);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_327;
	}

	private void Relay_In_329()
	{
		logic_uScript_SkipTutorial_uScript_SkipTutorial_329.In();
		bool yes = logic_uScript_SkipTutorial_uScript_SkipTutorial_329.Yes;
		bool no = logic_uScript_SkipTutorial_uScript_SkipTutorial_329.No;
		if (yes)
		{
			Relay_In_51();
		}
		if (no)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_331()
	{
		logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_331.In();
		if (logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_331.False)
		{
			Relay_In_342();
		}
	}

	private void Relay_In_333()
	{
		logic_uScript_AddMessage_messageData_333 = msg00aSkipTutorialIntro;
		logic_uScript_AddMessage_speaker_333 = messageSpeaker;
		logic_uScript_AddMessage_Return_333 = logic_uScript_AddMessage_uScript_AddMessage_333.In(logic_uScript_AddMessage_messageData_333, logic_uScript_AddMessage_speaker_333);
		if (logic_uScript_AddMessage_uScript_AddMessage_333.Shown)
		{
			Relay_True_337();
		}
	}

	private void Relay_In_335()
	{
		logic_uScriptCon_CompareBool_Bool_335 = local_MsgSkipTutorialIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_335.In(logic_uScriptCon_CompareBool_Bool_335);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_335.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_335.False;
		if (num)
		{
			Relay_In_339();
		}
		if (flag)
		{
			Relay_In_333();
		}
	}

	private void Relay_True_337()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_337.True(out logic_uScriptAct_SetBool_Target_337);
		local_MsgSkipTutorialIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_337;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_337.Out)
		{
			Relay_In_339();
		}
	}

	private void Relay_False_337()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_337.False(out logic_uScriptAct_SetBool_Target_337);
		local_MsgSkipTutorialIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_337;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_337.Out)
		{
			Relay_In_339();
		}
	}

	private void Relay_In_338()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_338.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_338.Out)
		{
			Relay_In_51();
		}
	}

	private void Relay_In_339()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_339.In(logic_uScript_ChangeBuildingOptions_change_339, logic_uScript_ChangeBuildingOptions_allow_339);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_339.Out)
		{
			Relay_In_365();
		}
	}

	private void Relay_In_342()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_342 = local_MsgSkipTutorialExitBeam_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_342.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_342, logic_uScript_RemoveOnScreenMessage_instant_342);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_342.Out)
		{
			Relay_In_368();
		}
	}

	private void Relay_In_344()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_344 = blockTypeWheelStabiliser;
		logic_uScript_SpawnBlockAbovePlayer_owner_344 = owner_Connection_345;
		logic_uScript_SpawnBlockAbovePlayer_Return_344 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_344.In(logic_uScript_SpawnBlockAbovePlayer_blockType_344, logic_uScript_SpawnBlockAbovePlayer_uniqueName_344, logic_uScript_SpawnBlockAbovePlayer_owner_344);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_344.Out)
		{
			Relay_In_347();
		}
	}

	private void Relay_In_346()
	{
		logic_uScript_SkipTutorial_uScript_SkipTutorial_346.In();
		bool yes = logic_uScript_SkipTutorial_uScript_SkipTutorial_346.Yes;
		bool no = logic_uScript_SkipTutorial_uScript_SkipTutorial_346.No;
		if (yes)
		{
			Relay_In_305();
		}
		if (no)
		{
			Relay_In_14();
		}
	}

	private void Relay_In_347()
	{
		logic_uScript_Wait_uScript_Wait_347.In(logic_uScript_Wait_seconds_347, logic_uScript_Wait_repeat_347);
		if (logic_uScript_Wait_uScript_Wait_347.Waited)
		{
			Relay_In_505();
		}
	}

	private void Relay_Save_Out_350()
	{
		Relay_Save_511();
	}

	private void Relay_Load_Out_350()
	{
		Relay_Load_511();
	}

	private void Relay_Restart_Out_350()
	{
		Relay_Set_False_511();
	}

	private void Relay_Save_350()
	{
		logic_SubGraph_SaveLoadBool_boolean_350 = local_MsgSkipTutorialIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_350 = local_MsgSkipTutorialIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.Save(ref logic_SubGraph_SaveLoadBool_boolean_350, logic_SubGraph_SaveLoadBool_boolAsVariable_350, logic_SubGraph_SaveLoadBool_uniqueID_350);
	}

	private void Relay_Load_350()
	{
		logic_SubGraph_SaveLoadBool_boolean_350 = local_MsgSkipTutorialIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_350 = local_MsgSkipTutorialIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.Load(ref logic_SubGraph_SaveLoadBool_boolean_350, logic_SubGraph_SaveLoadBool_boolAsVariable_350, logic_SubGraph_SaveLoadBool_uniqueID_350);
	}

	private void Relay_Set_True_350()
	{
		logic_SubGraph_SaveLoadBool_boolean_350 = local_MsgSkipTutorialIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_350 = local_MsgSkipTutorialIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_350, logic_SubGraph_SaveLoadBool_boolAsVariable_350, logic_SubGraph_SaveLoadBool_uniqueID_350);
	}

	private void Relay_Set_False_350()
	{
		logic_SubGraph_SaveLoadBool_boolean_350 = local_MsgSkipTutorialIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_350 = local_MsgSkipTutorialIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_350.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_350, logic_SubGraph_SaveLoadBool_boolAsVariable_350, logic_SubGraph_SaveLoadBool_uniqueID_350);
	}

	private void Relay_In_351()
	{
		logic_uScript_HideHintFloating_uScript_HideHintFloating_351.In();
		if (logic_uScript_HideHintFloating_uScript_HideHintFloating_351.Out)
		{
			Relay_In_489();
		}
	}

	private void Relay_In_352()
	{
		logic_uScript_HideHintFloating_uScript_HideHintFloating_352.In();
		if (logic_uScript_HideHintFloating_uScript_HideHintFloating_352.Out)
		{
			Relay_In_513();
		}
	}

	private void Relay_Out_353()
	{
		Relay_In_173();
	}

	private void Relay_Shown_353()
	{
	}

	private void Relay_In_353()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_353 = msg02RotateCamera;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_353 = msg02RotateCamera_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_353 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_353.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_353, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_353, logic_SubGraph_AddMessageWithPadSupport_speaker_353);
	}

	private void Relay_Out_357()
	{
		Relay_In_383();
	}

	private void Relay_Shown_357()
	{
	}

	private void Relay_In_357()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_357 = msg08ExitBuildBeam;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_357 = msg08ExitBuildBeam_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_357 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_357.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_357, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_357, logic_SubGraph_AddMessageWithPadSupport_speaker_357);
	}

	private void Relay_In_362()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_362 = local_363_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_362.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_362, logic_uScript_RemoveOnScreenMessage_instant_362);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_362.Out)
		{
			Relay_In_352();
		}
	}

	private void Relay_Out_365()
	{
		Relay_In_331();
	}

	private void Relay_Shown_365()
	{
	}

	private void Relay_In_365()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_365 = msg00bSkipTutorialExitBeam;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_365 = msg00bSkipTutorialExitBeam_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_365 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_365.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_365, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_365, logic_SubGraph_AddMessageWithPadSupport_speaker_365);
	}

	private void Relay_In_368()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_368 = local_MsgSkipTutorialExitBeam_Pad_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_368.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_368, logic_uScript_RemoveOnScreenMessage_instant_368);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_368.Out)
		{
			Relay_In_327();
		}
	}

	private void Relay_In_373()
	{
		logic_uScript_AddMessage_messageData_373 = msg03AttachBlock_Pad2;
		logic_uScript_AddMessage_speaker_373 = messageSpeaker;
		logic_uScript_AddMessage_Return_373 = logic_uScript_AddMessage_uScript_AddMessage_373.In(logic_uScript_AddMessage_messageData_373, logic_uScript_AddMessage_speaker_373);
		if (logic_uScript_AddMessage_uScript_AddMessage_373.Out)
		{
			Relay_In_467();
		}
	}

	private void Relay_In_375()
	{
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_375.In();
		bool joypad = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_375.Joypad;
		bool mouseAndKeyboard = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_375.MouseAndKeyboard;
		if (joypad)
		{
			Relay_In_373();
		}
		if (mouseAndKeyboard)
		{
			Relay_In_381();
		}
	}

	private void Relay_Out_380()
	{
		Relay_In_467();
	}

	private void Relay_Shown_380()
	{
	}

	private void Relay_In_380()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_380 = msg03AttachBlock;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_380 = msg03AttachBlock_Pad1;
		logic_SubGraph_AddMessageWithPadSupport_speaker_380 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_380.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_380, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_380, logic_SubGraph_AddMessageWithPadSupport_speaker_380);
	}

	private void Relay_In_381()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_381.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_381.Out)
		{
			Relay_In_467();
		}
	}

	private void Relay_Out_382()
	{
	}

	private void Relay_In_382()
	{
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_382.In(logic_SubGraph_ShowHintWithPadSupport_hintControlPad_382, logic_SubGraph_ShowHintWithPadSupport_hintMouseKeyboard_382);
	}

	private void Relay_Out_383()
	{
	}

	private void Relay_In_383()
	{
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_383.In(logic_SubGraph_ShowHintWithPadSupport_hintControlPad_383, logic_SubGraph_ShowHintWithPadSupport_hintMouseKeyboard_383);
	}

	private void Relay_In_384()
	{
		logic_uScript_EnableGlow_targetObject_384 = local_StandardBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_384.In(logic_uScript_EnableGlow_targetObject_384, logic_uScript_EnableGlow_enable_384);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_384.Out)
		{
			Relay_In_390();
		}
	}

	private void Relay_In_386()
	{
		logic_uScript_EnableGlow_targetObject_386 = local_StandardBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_386.In(logic_uScript_EnableGlow_targetObject_386, logic_uScript_EnableGlow_enable_386);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_386.Out)
		{
			Relay_In_393();
		}
	}

	private void Relay_In_389()
	{
		logic_uScript_EnableGlow_targetObject_389 = local_StandardBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_389.In(logic_uScript_EnableGlow_targetObject_389, logic_uScript_EnableGlow_enable_389);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_389.Out)
		{
			Relay_In_392();
		}
	}

	private void Relay_In_390()
	{
		logic_uScript_EnableGlow_targetObject_390 = local_GhostBlockStandard_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_390.In(logic_uScript_EnableGlow_targetObject_390, logic_uScript_EnableGlow_enable_390);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_390.Out)
		{
			Relay_In_375();
		}
	}

	private void Relay_In_391()
	{
		logic_uScript_EnableGlow_targetObject_391 = local_GhostBlockStandard_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_391.In(logic_uScript_EnableGlow_targetObject_391, logic_uScript_EnableGlow_enable_391);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_391.Out)
		{
			Relay_In_399();
		}
	}

	private void Relay_In_392()
	{
		logic_uScript_EnableGlow_targetObject_392 = local_GhostBlockStandard_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_392.In(logic_uScript_EnableGlow_targetObject_392, logic_uScript_EnableGlow_enable_392);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_392.Out)
		{
			Relay_In_274();
		}
	}

	private void Relay_In_393()
	{
		logic_uScriptCon_CompareBool_Bool_393 = local_GhostBlockStandardSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_393.In(logic_uScriptCon_CompareBool_Bool_393);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_393.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_393.False;
		if (num)
		{
			Relay_In_391();
		}
		if (flag)
		{
			Relay_In_395();
		}
	}

	private void Relay_In_395()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_395.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_395.Out)
		{
			Relay_In_399();
		}
	}

	private void Relay_In_399()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_399.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_399.Out)
		{
			Relay_In_380();
		}
	}

	private void Relay_In_401()
	{
		logic_uScript_EnableGlow_targetObject_401 = local_GhostBlockGun_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_401.In(logic_uScript_EnableGlow_targetObject_401, logic_uScript_EnableGlow_enable_401);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_401.Out)
		{
			Relay_In_406();
		}
	}

	private void Relay_In_403()
	{
		logic_uScriptCon_CompareBool_Bool_403 = local_GhostBlockGunSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_403.In(logic_uScriptCon_CompareBool_Bool_403);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_403.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_403.False;
		if (num)
		{
			Relay_In_401();
		}
		if (flag)
		{
			Relay_In_405();
		}
	}

	private void Relay_In_404()
	{
		logic_uScript_EnableGlow_targetObject_404 = local_GunBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_404.In(logic_uScript_EnableGlow_targetObject_404, logic_uScript_EnableGlow_enable_404);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_404.Out)
		{
			Relay_In_403();
		}
	}

	private void Relay_In_405()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_405.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_405.Out)
		{
			Relay_In_406();
		}
	}

	private void Relay_In_406()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_406.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_406.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_In_410()
	{
		logic_uScript_EnableGlow_targetObject_410 = local_GhostBlockGun_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_410.In(logic_uScript_EnableGlow_targetObject_410, logic_uScript_EnableGlow_enable_410);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_410.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_In_411()
	{
		logic_uScript_EnableGlow_targetObject_411 = local_GunBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_411.In(logic_uScript_EnableGlow_targetObject_411, logic_uScript_EnableGlow_enable_411);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_411.Out)
		{
			Relay_In_410();
		}
	}

	private void Relay_In_412()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_412.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_412.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_In_414()
	{
		logic_uScript_EnableGlow_targetObject_414 = local_GhostBlockDrill_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_414.In(logic_uScript_EnableGlow_targetObject_414, logic_uScript_EnableGlow_enable_414);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_414.Out)
		{
			Relay_In_412();
		}
	}

	private void Relay_In_416()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_416.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_416.Out)
		{
			Relay_In_412();
		}
	}

	private void Relay_In_418()
	{
		logic_uScript_EnableGlow_targetObject_418 = local_DrillBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_418.In(logic_uScript_EnableGlow_targetObject_418, logic_uScript_EnableGlow_enable_418);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_418.Out)
		{
			Relay_In_419();
		}
	}

	private void Relay_In_419()
	{
		logic_uScriptCon_CompareBool_Bool_419 = local_GhostBlockDrillSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_419.In(logic_uScriptCon_CompareBool_Bool_419);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_419.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_419.False;
		if (num)
		{
			Relay_In_414();
		}
		if (flag)
		{
			Relay_In_416();
		}
	}

	private void Relay_In_420()
	{
		logic_uScript_EnableGlow_targetObject_420 = local_GhostBlockDrill_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_420.In(logic_uScript_EnableGlow_targetObject_420, logic_uScript_EnableGlow_enable_420);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_420.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_In_422()
	{
		logic_uScript_EnableGlow_targetObject_422 = local_DrillBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_422.In(logic_uScript_EnableGlow_targetObject_422, logic_uScript_EnableGlow_enable_422);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_422.Out)
		{
			Relay_In_420();
		}
	}

	private void Relay_In_424()
	{
		logic_uScript_EnableGlow_targetObject_424 = local_GhostBlockGun_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_424.In(logic_uScript_EnableGlow_targetObject_424, logic_uScript_EnableGlow_enable_424);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_424.Out)
		{
			Relay_In_288();
		}
	}

	private void Relay_In_426()
	{
		logic_uScript_EnableGlow_targetObject_426 = local_GunBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_426.In(logic_uScript_EnableGlow_targetObject_426, logic_uScript_EnableGlow_enable_426);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_426.Out)
		{
			Relay_In_424();
		}
	}

	private void Relay_In_429()
	{
		logic_uScript_EnableGlow_targetObject_429 = local_DrillBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_429.In(logic_uScript_EnableGlow_targetObject_429, logic_uScript_EnableGlow_enable_429);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_429.Out)
		{
			Relay_In_431();
		}
	}

	private void Relay_In_431()
	{
		logic_uScript_EnableGlow_targetObject_431 = local_GhostBlockDrill_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_431.In(logic_uScript_EnableGlow_targetObject_431, logic_uScript_EnableGlow_enable_431);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_431.Out)
		{
			Relay_In_139();
		}
	}

	private void Relay_In_432()
	{
		logic_uScript_EnableGlow_targetObject_432 = local_GhostBlockWheel01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_432.In(logic_uScript_EnableGlow_targetObject_432, logic_uScript_EnableGlow_enable_432);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_432.Out)
		{
			Relay_In_251();
		}
	}

	private void Relay_In_433()
	{
		logic_uScript_EnableGlow_targetObject_433 = local_GhostBlockWheel02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_433.In(logic_uScript_EnableGlow_targetObject_433, logic_uScript_EnableGlow_enable_433);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_433.Out)
		{
			Relay_In_252();
		}
	}

	private void Relay_In_434()
	{
		logic_uScript_EnableGlow_targetObject_434 = local_GhostBlockWheel03_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_434.In(logic_uScript_EnableGlow_targetObject_434, logic_uScript_EnableGlow_enable_434);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_434.Out)
		{
			Relay_In_253();
		}
	}

	private void Relay_In_435()
	{
		logic_uScript_EnableGlow_targetObject_435 = local_GhostBlockWheel04_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_435.In(logic_uScript_EnableGlow_targetObject_435, logic_uScript_EnableGlow_enable_435);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_435.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_In_436()
	{
		logic_uScript_EnableGlow_targetObject_436 = local_Wheel01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_436.In(logic_uScript_EnableGlow_targetObject_436, logic_uScript_EnableGlow_enable_436);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_436.Out)
		{
			Relay_In_164();
		}
	}

	private void Relay_In_437()
	{
		logic_uScript_EnableGlow_targetObject_437 = local_Wheel02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_437.In(logic_uScript_EnableGlow_targetObject_437, logic_uScript_EnableGlow_enable_437);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_437.Out)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_438()
	{
		logic_uScript_EnableGlow_targetObject_438 = local_Wheel03_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_438.In(logic_uScript_EnableGlow_targetObject_438, logic_uScript_EnableGlow_enable_438);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_438.Out)
		{
			Relay_In_166();
		}
	}

	private void Relay_In_439()
	{
		logic_uScript_EnableGlow_targetObject_439 = local_Wheel04_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_439.In(logic_uScript_EnableGlow_targetObject_439, logic_uScript_EnableGlow_enable_439);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_439.Out)
		{
			Relay_In_180();
		}
	}

	private void Relay_In_440()
	{
		logic_uScript_EnableGlow_targetObject_440 = local_Wheel01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_440.In(logic_uScript_EnableGlow_targetObject_440, logic_uScript_EnableGlow_enable_440);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_440.Out)
		{
			Relay_In_442();
		}
	}

	private void Relay_In_442()
	{
		logic_uScript_EnableGlow_targetObject_442 = local_Wheel02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_442.In(logic_uScript_EnableGlow_targetObject_442, logic_uScript_EnableGlow_enable_442);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_442.Out)
		{
			Relay_In_445();
		}
	}

	private void Relay_In_445()
	{
		logic_uScript_EnableGlow_targetObject_445 = local_Wheel03_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_445.In(logic_uScript_EnableGlow_targetObject_445, logic_uScript_EnableGlow_enable_445);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_445.Out)
		{
			Relay_In_447();
		}
	}

	private void Relay_In_447()
	{
		logic_uScript_EnableGlow_targetObject_447 = local_Wheel04_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_447.In(logic_uScript_EnableGlow_targetObject_447, logic_uScript_EnableGlow_enable_447);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_447.Out)
		{
			Relay_In_230();
		}
	}

	private void Relay_In_448()
	{
		logic_uScript_EnableGlow_targetObject_448 = local_GhostBlockWheel01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_448.In(logic_uScript_EnableGlow_targetObject_448, logic_uScript_EnableGlow_enable_448);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_448.Out)
		{
			Relay_In_451();
		}
	}

	private void Relay_In_451()
	{
		logic_uScript_EnableGlow_targetObject_451 = local_GhostBlockWheel02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_451.In(logic_uScript_EnableGlow_targetObject_451, logic_uScript_EnableGlow_enable_451);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_451.Out)
		{
			Relay_In_453();
		}
	}

	private void Relay_In_453()
	{
		logic_uScript_EnableGlow_targetObject_453 = local_GhostBlockWheel03_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_453.In(logic_uScript_EnableGlow_targetObject_453, logic_uScript_EnableGlow_enable_453);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_453.Out)
		{
			Relay_In_455();
		}
	}

	private void Relay_In_455()
	{
		logic_uScript_EnableGlow_targetObject_455 = local_GhostBlockWheel04_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_455.In(logic_uScript_EnableGlow_targetObject_455, logic_uScript_EnableGlow_enable_455);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_455.Out)
		{
			Relay_In_251();
		}
	}

	private void Relay_In_457()
	{
		logic_uScriptCon_CompareBool_Bool_457 = local_GhostBlockWheelsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_457.In(logic_uScriptCon_CompareBool_Bool_457);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_457.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_457.False;
		if (num)
		{
			Relay_In_448();
		}
		if (flag)
		{
			Relay_In_264();
		}
	}

	private void Relay_In_459()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_459.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_459.Out)
		{
			Relay_In_251();
		}
	}

	private void Relay_In_467()
	{
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_467.In();
		bool joypad = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_467.Joypad;
		bool mouseAndKeyboard = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_467.MouseAndKeyboard;
		if (joypad)
		{
			Relay_In_15();
		}
		if (mouseAndKeyboard)
		{
			Relay_In_468();
		}
	}

	private void Relay_In_468()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_468.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_468.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_469()
	{
		logic_uScript_ShowHintFloating_uScript_ShowHintFloating_469.In(logic_uScript_ShowHintFloating_hintAnimation_469);
		if (logic_uScript_ShowHintFloating_uScript_ShowHintFloating_469.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_471()
	{
		logic_uScript_HideHintFloating_uScript_HideHintFloating_471.In();
		if (logic_uScript_HideHintFloating_uScript_HideHintFloating_471.Out)
		{
			Relay_In_283();
		}
	}

	private void Relay_Out_472()
	{
		Relay_False_68();
	}

	private void Relay_Shown_472()
	{
	}

	private void Relay_In_472()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_472 = msg05RotateToFindWheels;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_472 = msg05RotateToFindWheels_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_472 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_472.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_472, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_472, logic_SubGraph_AddMessageWithPadSupport_speaker_472);
	}

	private void Relay_In_476()
	{
		logic_uScript_LockTech_tech_476 = local_PlayerTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_476.In(logic_uScript_LockTech_tech_476, logic_uScript_LockTech_lockType_476);
		if (logic_uScript_LockTech_uScript_LockTech_476.Out)
		{
			Relay_In_485();
		}
	}

	private void Relay_In_478()
	{
		logic_uScript_GetPlayerTank_Return_478 = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_478.In();
		local_PlayerTech_Tank = logic_uScript_GetPlayerTank_Return_478;
		if (logic_uScript_GetPlayerTank_uScript_GetPlayerTank_478.Returned)
		{
			Relay_In_476();
		}
	}

	private void Relay_In_480()
	{
		logic_uScriptCon_CompareBool_Bool_480 = local_CabInBeam_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_480.In(logic_uScriptCon_CompareBool_Bool_480);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_480.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_480.False;
		if (num)
		{
			Relay_In_123();
		}
		if (flag)
		{
			Relay_In_478();
		}
	}

	private void Relay_True_481()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_481.True(out logic_uScriptAct_SetBool_Target_481);
		local_CabInBeam_System_Boolean = logic_uScriptAct_SetBool_Target_481;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_481.Out)
		{
			Relay_In_517();
		}
	}

	private void Relay_False_481()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_481.False(out logic_uScriptAct_SetBool_Target_481);
		local_CabInBeam_System_Boolean = logic_uScriptAct_SetBool_Target_481;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_481.Out)
		{
			Relay_In_517();
		}
	}

	private void Relay_Save_Out_483()
	{
		Relay_Save_111();
	}

	private void Relay_Load_Out_483()
	{
		Relay_Load_111();
	}

	private void Relay_Restart_Out_483()
	{
		Relay_Set_False_111();
	}

	private void Relay_Save_483()
	{
		logic_SubGraph_SaveLoadBool_boolean_483 = local_CabInBeam_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_483 = local_CabInBeam_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.Save(ref logic_SubGraph_SaveLoadBool_boolean_483, logic_SubGraph_SaveLoadBool_boolAsVariable_483, logic_SubGraph_SaveLoadBool_uniqueID_483);
	}

	private void Relay_Load_483()
	{
		logic_SubGraph_SaveLoadBool_boolean_483 = local_CabInBeam_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_483 = local_CabInBeam_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.Load(ref logic_SubGraph_SaveLoadBool_boolean_483, logic_SubGraph_SaveLoadBool_boolAsVariable_483, logic_SubGraph_SaveLoadBool_uniqueID_483);
	}

	private void Relay_Set_True_483()
	{
		logic_SubGraph_SaveLoadBool_boolean_483 = local_CabInBeam_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_483 = local_CabInBeam_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_483, logic_SubGraph_SaveLoadBool_boolAsVariable_483, logic_SubGraph_SaveLoadBool_uniqueID_483);
	}

	private void Relay_Set_False_483()
	{
		logic_SubGraph_SaveLoadBool_boolean_483 = local_CabInBeam_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_483 = local_CabInBeam_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_483.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_483, logic_SubGraph_SaveLoadBool_boolAsVariable_483, logic_SubGraph_SaveLoadBool_uniqueID_483);
	}

	private void Relay_In_485()
	{
		logic_uScript_Wait_seconds_485 = TimeBeforeBeam;
		logic_uScript_Wait_uScript_Wait_485.In(logic_uScript_Wait_seconds_485, logic_uScript_Wait_repeat_485);
		if (logic_uScript_Wait_uScript_Wait_485.Waited)
		{
			Relay_True_481();
		}
	}

	private void Relay_In_487()
	{
		logic_uScript_EnableInteractionMode_uScript_EnableInteractionMode_487.In(logic_uScript_EnableInteractionMode_enableInteractionMode_487);
		if (logic_uScript_EnableInteractionMode_uScript_EnableInteractionMode_487.Out)
		{
			Relay_Lock_498();
		}
	}

	private void Relay_In_488()
	{
		logic_uScript_EnableInteractionMode_uScript_EnableInteractionMode_488.In(logic_uScript_EnableInteractionMode_enableInteractionMode_488);
		if (logic_uScript_EnableInteractionMode_uScript_EnableInteractionMode_488.Out)
		{
			Relay_In_523();
		}
	}

	private void Relay_In_489()
	{
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_489.In();
		bool joypad = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_489.Joypad;
		bool mouseAndKeyboard = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_489.MouseAndKeyboard;
		if (joypad)
		{
			Relay_Unlock_497();
		}
		if (mouseAndKeyboard)
		{
			Relay_In_490();
		}
	}

	private void Relay_In_490()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_490.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_490.Out)
		{
			Relay_In_499();
		}
	}

	private void Relay_In_491()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_491.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_491.Out)
		{
			Relay_In_523();
		}
	}

	private void Relay_In_492()
	{
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_492.In();
		bool joypad = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_492.Joypad;
		bool mouseAndKeyboard = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_492.MouseAndKeyboard;
		if (joypad)
		{
			Relay_Unlock_496();
		}
		if (mouseAndKeyboard)
		{
			Relay_In_491();
		}
	}

	private void Relay_In_493()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_493.In(logic_uScript_ChangeBuildingOptions_change_493, logic_uScript_ChangeBuildingOptions_allow_493);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_493.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_494()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_494.In(logic_uScript_ChangeBuildingOptions_change_494, logic_uScript_ChangeBuildingOptions_allow_494);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_494.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_Lock_496()
	{
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_496.Lock();
		if (logic_uScript_LockInteractionMode_uScript_LockInteractionMode_496.Out)
		{
			Relay_In_488();
		}
	}

	private void Relay_Unlock_496()
	{
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_496.Unlock();
		if (logic_uScript_LockInteractionMode_uScript_LockInteractionMode_496.Out)
		{
			Relay_In_488();
		}
	}

	private void Relay_Lock_497()
	{
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_497.Lock();
		if (logic_uScript_LockInteractionMode_uScript_LockInteractionMode_497.Out)
		{
			Relay_In_487();
		}
	}

	private void Relay_Unlock_497()
	{
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_497.Unlock();
		if (logic_uScript_LockInteractionMode_uScript_LockInteractionMode_497.Out)
		{
			Relay_In_487();
		}
	}

	private void Relay_Lock_498()
	{
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_498.Lock();
		if (logic_uScript_LockInteractionMode_uScript_LockInteractionMode_498.Out)
		{
			Relay_In_272();
		}
	}

	private void Relay_Unlock_498()
	{
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_498.Unlock();
		if (logic_uScript_LockInteractionMode_uScript_LockInteractionMode_498.Out)
		{
			Relay_In_272();
		}
	}

	private void Relay_In_499()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_499.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_499.Out)
		{
			Relay_In_272();
		}
	}

	private void Relay_In_502()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_502.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_502.Out)
		{
			Relay_In_335();
		}
	}

	private void Relay_Lock_503()
	{
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_503.Lock();
		if (logic_uScript_LockInteractionMode_uScript_LockInteractionMode_503.Out)
		{
			Relay_In_335();
		}
	}

	private void Relay_Unlock_503()
	{
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_503.Unlock();
		if (logic_uScript_LockInteractionMode_uScript_LockInteractionMode_503.Out)
		{
			Relay_In_335();
		}
	}

	private void Relay_Lock_504()
	{
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_504.Lock();
		if (logic_uScript_LockInteractionMode_uScript_LockInteractionMode_504.Out)
		{
			Relay_In_506();
		}
	}

	private void Relay_Unlock_504()
	{
		logic_uScript_LockInteractionMode_uScript_LockInteractionMode_504.Unlock();
		if (logic_uScript_LockInteractionMode_uScript_LockInteractionMode_504.Out)
		{
			Relay_In_506();
		}
	}

	private void Relay_In_505()
	{
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_505.In();
		bool joypad = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_505.Joypad;
		bool mouseAndKeyboard = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_505.MouseAndKeyboard;
		if (joypad)
		{
			Relay_In_509();
		}
		if (mouseAndKeyboard)
		{
			Relay_In_502();
		}
	}

	private void Relay_In_506()
	{
		logic_uScript_EnableInteractionMode_uScript_EnableInteractionMode_506.In(logic_uScript_EnableInteractionMode_enableInteractionMode_506);
		if (logic_uScript_EnableInteractionMode_uScript_EnableInteractionMode_506.Out)
		{
			Relay_Lock_503();
		}
	}

	private void Relay_In_509()
	{
		logic_uScriptCon_CompareBool_Bool_509 = local_SkipTutorialReticule_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_509.In(logic_uScriptCon_CompareBool_Bool_509);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_509.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_509.False;
		if (num)
		{
			Relay_In_502();
		}
		if (flag)
		{
			Relay_True_510();
		}
	}

	private void Relay_True_510()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_510.True(out logic_uScriptAct_SetBool_Target_510);
		local_SkipTutorialReticule_System_Boolean = logic_uScriptAct_SetBool_Target_510;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_510.Out)
		{
			Relay_Unlock_504();
		}
	}

	private void Relay_False_510()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_510.False(out logic_uScriptAct_SetBool_Target_510);
		local_SkipTutorialReticule_System_Boolean = logic_uScriptAct_SetBool_Target_510;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_510.Out)
		{
			Relay_Unlock_504();
		}
	}

	private void Relay_Save_Out_511()
	{
		Relay_Save_260();
	}

	private void Relay_Load_Out_511()
	{
		Relay_Load_260();
	}

	private void Relay_Restart_Out_511()
	{
		Relay_Set_False_260();
	}

	private void Relay_Save_511()
	{
		logic_SubGraph_SaveLoadBool_boolean_511 = local_SkipTutorialReticule_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_511 = local_SkipTutorialReticule_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.Save(ref logic_SubGraph_SaveLoadBool_boolean_511, logic_SubGraph_SaveLoadBool_boolAsVariable_511, logic_SubGraph_SaveLoadBool_uniqueID_511);
	}

	private void Relay_Load_511()
	{
		logic_SubGraph_SaveLoadBool_boolean_511 = local_SkipTutorialReticule_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_511 = local_SkipTutorialReticule_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.Load(ref logic_SubGraph_SaveLoadBool_boolean_511, logic_SubGraph_SaveLoadBool_boolAsVariable_511, logic_SubGraph_SaveLoadBool_uniqueID_511);
	}

	private void Relay_Set_True_511()
	{
		logic_SubGraph_SaveLoadBool_boolean_511 = local_SkipTutorialReticule_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_511 = local_SkipTutorialReticule_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_511, logic_SubGraph_SaveLoadBool_boolAsVariable_511, logic_SubGraph_SaveLoadBool_uniqueID_511);
	}

	private void Relay_Set_False_511()
	{
		logic_SubGraph_SaveLoadBool_boolean_511 = local_SkipTutorialReticule_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_511 = local_SkipTutorialReticule_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_511.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_511, logic_SubGraph_SaveLoadBool_boolAsVariable_511, logic_SubGraph_SaveLoadBool_uniqueID_511);
	}

	private void Relay_In_513()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_513.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_513, logic_uScript_SendAnaliticsEvent_parameterName_513, logic_uScript_SendAnaliticsEvent_parameter_513);
		if (logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_513.Out)
		{
			Relay_In_492();
		}
	}

	private void Relay_Save_Out_515()
	{
		Relay_Save_483();
	}

	private void Relay_Load_Out_515()
	{
		Relay_Load_483();
	}

	private void Relay_Restart_Out_515()
	{
		Relay_Set_False_483();
	}

	private void Relay_Save_515()
	{
		logic_SubGraph_SaveLoadBool_boolean_515 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_515 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.Save(ref logic_SubGraph_SaveLoadBool_boolean_515, logic_SubGraph_SaveLoadBool_boolAsVariable_515, logic_SubGraph_SaveLoadBool_uniqueID_515);
	}

	private void Relay_Load_515()
	{
		logic_SubGraph_SaveLoadBool_boolean_515 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_515 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.Load(ref logic_SubGraph_SaveLoadBool_boolean_515, logic_SubGraph_SaveLoadBool_boolAsVariable_515, logic_SubGraph_SaveLoadBool_uniqueID_515);
	}

	private void Relay_Set_True_515()
	{
		logic_SubGraph_SaveLoadBool_boolean_515 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_515 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_515, logic_SubGraph_SaveLoadBool_boolAsVariable_515, logic_SubGraph_SaveLoadBool_uniqueID_515);
	}

	private void Relay_Set_False_515()
	{
		logic_SubGraph_SaveLoadBool_boolean_515 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_515 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_515.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_515, logic_SubGraph_SaveLoadBool_boolAsVariable_515, logic_SubGraph_SaveLoadBool_uniqueID_515);
	}

	private void Relay_In_517()
	{
		logic_uScriptCon_CompareBool_Bool_517 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_517.In(logic_uScriptCon_CompareBool_Bool_517);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_517.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_517.False;
		if (num)
		{
			Relay_In_519();
		}
		if (flag)
		{
			Relay_True_518();
		}
	}

	private void Relay_True_518()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_518.True(out logic_uScriptAct_SetBool_Target_518);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_518;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_518.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_False_518()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_518.False(out logic_uScriptAct_SetBool_Target_518);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_518;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_518.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_519()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_519.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_519.Out)
		{
			Relay_In_522();
		}
	}

	private void Relay_Show_520()
	{
		logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_520.Show(logic_uScript_SetBlockLimitUIState_showReason_520);
		if (logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_520.Out)
		{
			Relay_In_346();
		}
	}

	private void Relay_Hide_520()
	{
		logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_520.Hide(logic_uScript_SetBlockLimitUIState_showReason_520);
		if (logic_uScript_SetBlockLimitUIState_uScript_SetBlockLimitUIState_520.Out)
		{
			Relay_In_346();
		}
	}

	private void Relay_In_521()
	{
		logic_uScript_IsBlockLimitEnabled_uScript_IsBlockLimitEnabled_521.In();
		bool num = logic_uScript_IsBlockLimitEnabled_uScript_IsBlockLimitEnabled_521.True;
		bool flag = logic_uScript_IsBlockLimitEnabled_uScript_IsBlockLimitEnabled_521.False;
		if (num)
		{
			Relay_Hide_520();
		}
		if (flag)
		{
			Relay_In_346();
		}
	}

	private void Relay_In_522()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_522.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_522.Out)
		{
			Relay_In_346();
		}
	}

	private void Relay_In_523()
	{
		logic_uScript_DisableQuickMenu_uScript_DisableQuickMenu_523.In(logic_uScript_DisableQuickMenu_disableQuickMenu_523);
		if (logic_uScript_DisableQuickMenu_uScript_DisableQuickMenu_523.Out)
		{
			Relay_In_17();
		}
	}
}
