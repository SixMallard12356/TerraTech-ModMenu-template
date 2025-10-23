using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Challenge_GauntletTutorial : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private int local_currentCheckpoint_System_Int32;

	private string local_msg_System_String = "msg";

	private bool local_Msg03ReminderOnScreen_System_Boolean;

	private int local_TutorialBuildStage_System_Int32 = 1;

	private int local_TutorialDriveStage_System_Int32 = 1;

	private bool local_TutorialInBuildPhase_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_TutorialMsg03aRotateToFindWheels_ManOnScreenMessages_OnScreenMessage;

	public float TimeBeforeTutorialStart;

	public BlockTypes tutorialBodyBlock;

	public int tutorialBodyBlockAmount;

	public Vector3[] tutorialBodyPlacementFilter = new Vector3[0];

	public int tutorialBoosterAmount;

	public BlockTypes tutorialBoosterBlock;

	public Vector3[] tutorialBoosterPlacementFilter = new Vector3[0];

	public float tutorialCamTurnDegrees;

	public float tutorialDistToDrive;

	public float tutorialDistToDriveAway;

	public int tutorialGunAmount;

	public BlockTypes tutorialGunBlock;

	public Vector3[] tutorialGunPlacementFilter = new Vector3[0];

	public LocalisedString[] tutorialMsg01RotateCamera = new LocalisedString[0];

	public LocalisedString[] tutorialMsg02AttachBodyBlock = new LocalisedString[0];

	public LocalisedString[] tutorialMsg03aAttachWheels = new LocalisedString[0];

	public LocalisedString[] tutorialMsg03aRotateToFindWheels = new LocalisedString[0];

	public LocalisedString[] tutorialMsg03bAttachBoosters = new LocalisedString[0];

	public LocalisedString[] tutorialMsg04AttachGun = new LocalisedString[0];

	public LocalisedString[] tutorialMsg05ExitBeam = new LocalisedString[0];

	public LocalisedString[] tutorialMsg06TryDriving = new LocalisedString[0];

	public LocalisedString[] tutorialMsg07aTryShooting = new LocalisedString[0];

	public LocalisedString[] tutorialMsg07bTryBoosting = new LocalisedString[0];

	public LocalisedString[] tutorialMsg08BlockPalette = new LocalisedString[0];

	public int tutorialWheelAmount;

	public BlockTypes tutorialWheelBlock;

	public Vector3[] tutorialWheelPlacementFilter = new Vector3[0];

	private GameObject owner_Connection_20;

	private GameObject owner_Connection_31;

	private GameObject owner_Connection_41;

	private GameObject owner_Connection_44;

	private GameObject owner_Connection_48;

	private GameObject owner_Connection_55;

	private GameObject owner_Connection_77;

	private GameObject owner_Connection_84;

	private GameObject owner_Connection_134;

	private GameObject owner_Connection_138;

	private GameObject owner_Connection_140;

	private GameObject owner_Connection_162;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_0 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_0;

	private bool logic_uScriptCon_CompareBool_True_0 = true;

	private bool logic_uScriptCon_CompareBool_False_0 = true;

	private uScript_SetPlacementFilter logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_1 = new uScript_SetPlacementFilter();

	private Vector3[] logic_uScript_SetPlacementFilter_placementFilter_1 = new Vector3[0];

	private bool logic_uScript_SetPlacementFilter_Out_1 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_2 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_2;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_2 = "WheelBlock3";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_2;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_2;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_2 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_3 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_3;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_3 = "Gun";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_3;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_3;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_3 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_4 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_4 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_4 = true;

	private string logic_uScript_AddOnScreenMessage_tag_4 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_4;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_4;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_4;

	private bool logic_uScript_AddOnScreenMessage_Out_4 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_4 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_5 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_5 = uScript_ChangeBuildingOptions.BuildingOptions.ToggleBeam;

	private bool logic_uScript_ChangeBuildingOptions_allow_5;

	private bool logic_uScript_ChangeBuildingOptions_Out_5 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_9 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_9 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_9 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_9 = true;

	private string logic_uScript_AddOnScreenMessage_tag_9 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_9;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_9;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_9;

	private bool logic_uScript_AddOnScreenMessage_Out_9 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_9 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_11;

	private uScript_IsPlayerInBeam logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_13 = new uScript_IsPlayerInBeam();

	private bool logic_uScript_IsPlayerInBeam_True_13 = true;

	private bool logic_uScript_IsPlayerInBeam_False_13 = true;

	private uScript_HasCameraRotated logic_uScript_HasCameraRotated_uScript_HasCameraRotated_14 = new uScript_HasCameraRotated();

	private float logic_uScript_HasCameraRotated_turnCameraMinDegrees_14;

	private bool logic_uScript_HasCameraRotated_True_14 = true;

	private bool logic_uScript_HasCameraRotated_False_14 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_15 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_15 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_15 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_15 = true;

	private string logic_uScript_AddOnScreenMessage_tag_15 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_15;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_15;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_15;

	private bool logic_uScript_AddOnScreenMessage_Out_15 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_15 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_16 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_16 = uScript_ChangeBuildingOptions.BuildingOptions.Detach;

	private bool logic_uScript_ChangeBuildingOptions_allow_16;

	private bool logic_uScript_ChangeBuildingOptions_Out_16 = true;

	private uScript_HasCameraRotated logic_uScript_HasCameraRotated_uScript_HasCameraRotated_17 = new uScript_HasCameraRotated();

	private float logic_uScript_HasCameraRotated_turnCameraMinDegrees_17 = 3f;

	private bool logic_uScript_HasCameraRotated_True_17 = true;

	private bool logic_uScript_HasCameraRotated_False_17 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_18 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_18 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_18 = true;

	private string logic_uScript_AddOnScreenMessage_tag_18 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_18;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_18;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_18;

	private bool logic_uScript_AddOnScreenMessage_Out_18 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_18 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_19 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_19 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_19 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_19 = true;

	private string logic_uScript_AddOnScreenMessage_tag_19 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_19;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_19;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_19;

	private bool logic_uScript_AddOnScreenMessage_Out_19 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_19 = true;

	private uScript_DoesPlayerTankHaveBlock logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_22 = new uScript_DoesPlayerTankHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerTankHaveBlock_blockType_22;

	private int logic_uScript_DoesPlayerTankHaveBlock_amount_22;

	private bool logic_uScript_DoesPlayerTankHaveBlock_True_22 = true;

	private bool logic_uScript_DoesPlayerTankHaveBlock_False_22 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_23 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_23 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_23 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_23 = true;

	private string logic_uScript_AddOnScreenMessage_tag_23 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_23;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_23;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_23;

	private bool logic_uScript_AddOnScreenMessage_Out_23 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_23 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_24 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_24;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_24 = "WheelBlock2";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_24;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_24;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_24 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_25 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_25 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_25 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_25 = true;

	private string logic_uScript_AddOnScreenMessage_tag_25 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_25;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_25;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_25;

	private bool logic_uScript_AddOnScreenMessage_Out_25 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_25 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_27 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_27 = uScript_ChangeBuildingOptions.BuildingOptions.ReduceDragSpeed;

	private bool logic_uScript_ChangeBuildingOptions_allow_27 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_27 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_28 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_28;

	private bool logic_uScript_ChangeBuildingOptions_allow_28;

	private bool logic_uScript_ChangeBuildingOptions_Out_28 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_29 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_29;

	private bool logic_uScriptAct_SetBool_Out_29 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_29 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_29 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_32 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_32 = uScript_ChangeBuildingOptions.BuildingOptions.ToggleBeam;

	private bool logic_uScript_ChangeBuildingOptions_allow_32 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_32 = true;

	private uScript_DoesPlayerTankHaveBlock logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_35 = new uScript_DoesPlayerTankHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerTankHaveBlock_blockType_35;

	private int logic_uScript_DoesPlayerTankHaveBlock_amount_35 = 2;

	private bool logic_uScript_DoesPlayerTankHaveBlock_True_35 = true;

	private bool logic_uScript_DoesPlayerTankHaveBlock_False_35 = true;

	private uScript_DoesPlayerTankHaveBlock logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_36 = new uScript_DoesPlayerTankHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerTankHaveBlock_blockType_36;

	private int logic_uScript_DoesPlayerTankHaveBlock_amount_36;

	private bool logic_uScript_DoesPlayerTankHaveBlock_True_36 = true;

	private bool logic_uScript_DoesPlayerTankHaveBlock_False_36 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_38 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_38;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_38 = "BodyBlock1";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_38;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_38;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_38 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_39 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_39;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_39 = "WheelBlock4";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_39;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_39;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_39 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_40 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_40;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_40 = "WheelBlock1";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_40;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_40;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_40 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_46 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_46 = 5f;

	private bool logic_uScript_Wait_repeat_46;

	private bool logic_uScript_Wait_Waited_46 = true;

	private uScript_SetPlacementFilter logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_49 = new uScript_SetPlacementFilter();

	private Vector3[] logic_uScript_SetPlacementFilter_placementFilter_49 = new Vector3[0];

	private bool logic_uScript_SetPlacementFilter_Out_49 = true;

	private uScript_DoesPlayerTankHaveBlock logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_52 = new uScript_DoesPlayerTankHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerTankHaveBlock_blockType_52;

	private int logic_uScript_DoesPlayerTankHaveBlock_amount_52;

	private bool logic_uScript_DoesPlayerTankHaveBlock_True_52 = true;

	private bool logic_uScript_DoesPlayerTankHaveBlock_False_52 = true;

	private uScript_SetGrabDistance logic_uScript_SetGrabDistance_uScript_SetGrabDistance_53 = new uScript_SetGrabDistance();

	private float logic_uScript_SetGrabDistance_dist_53;

	private bool logic_uScript_SetGrabDistance_Out_53 = true;

	private uScript_SetPlacementFilter logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_54 = new uScript_SetPlacementFilter();

	private Vector3[] logic_uScript_SetPlacementFilter_placementFilter_54 = new Vector3[0];

	private bool logic_uScript_SetPlacementFilter_Out_54 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_65 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_65 = 1;

	private int logic_uScriptAct_AddInt_v2_B_65;

	private int logic_uScriptAct_AddInt_v2_IntResult_65;

	private float logic_uScriptAct_AddInt_v2_FloatResult_65;

	private bool logic_uScriptAct_AddInt_v2_Out_65 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_67 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_67 = 1;

	private int logic_uScriptAct_AddInt_v2_B_67;

	private int logic_uScriptAct_AddInt_v2_IntResult_67;

	private float logic_uScriptAct_AddInt_v2_FloatResult_67;

	private bool logic_uScriptAct_AddInt_v2_Out_67 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_68 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_68 = 1;

	private int logic_uScriptAct_AddInt_v2_B_68;

	private int logic_uScriptAct_AddInt_v2_IntResult_68;

	private float logic_uScriptAct_AddInt_v2_FloatResult_68;

	private bool logic_uScriptAct_AddInt_v2_Out_68 = true;

	private uScript_GraphEvents logic_uScript_GraphEvents_uScript_GraphEvents_73 = new uScript_GraphEvents();

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_75 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_75 = 1;

	private int logic_uScriptAct_SetInt_Target_75;

	private bool logic_uScriptAct_SetInt_Out_75 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_83 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_83;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_83 = "BodyBlock2";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_83;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_83;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_83 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_86 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_86 = 1;

	private int logic_uScriptAct_AddInt_v2_B_86;

	private int logic_uScriptAct_AddInt_v2_IntResult_86;

	private float logic_uScriptAct_AddInt_v2_FloatResult_86;

	private bool logic_uScriptAct_AddInt_v2_Out_86 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_87;

	private bool logic_uScriptCon_CompareBool_True_87 = true;

	private bool logic_uScriptCon_CompareBool_False_87 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_89;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_92 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_92;

	private bool logic_uScriptAct_SetBool_Out_92 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_92 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_92 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_93 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_93 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_93 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_93 = true;

	private string logic_uScript_AddOnScreenMessage_tag_93 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_93;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_93;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_93;

	private bool logic_uScript_AddOnScreenMessage_Out_93 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_93 = true;

	private uScript_HasPlayerFiredGun logic_uScript_HasPlayerFiredGun_uScript_HasPlayerFiredGun_96 = new uScript_HasPlayerFiredGun();

	private bool logic_uScript_HasPlayerFiredGun_True_96 = true;

	private bool logic_uScript_HasPlayerFiredGun_False_96 = true;

	private uScript_HasPlayerTraveledDistance logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_98 = new uScript_HasPlayerTraveledDistance();

	private float logic_uScript_HasPlayerTraveledDistance_distance_98;

	private bool logic_uScript_HasPlayerTraveledDistance_True_98 = true;

	private bool logic_uScript_HasPlayerTraveledDistance_False_98 = true;

	private uScript_BlockDecay logic_uScript_BlockDecay_uScript_BlockDecay_99 = new uScript_BlockDecay();

	private bool logic_uScript_BlockDecay_allow_99 = true;

	private bool logic_uScript_BlockDecay_Out_99 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_100 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_100 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_100 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_100 = true;

	private string logic_uScript_AddOnScreenMessage_tag_100 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_100;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_100;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_100;

	private bool logic_uScript_AddOnScreenMessage_Out_100 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_100 = true;

	private uScript_HasPlayerTraveledDistance logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_101 = new uScript_HasPlayerTraveledDistance();

	private float logic_uScript_HasPlayerTraveledDistance_distance_101;

	private bool logic_uScript_HasPlayerTraveledDistance_True_101 = true;

	private bool logic_uScript_HasPlayerTraveledDistance_False_101 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_104 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_104 = 1;

	private int logic_uScriptAct_AddInt_v2_B_104;

	private int logic_uScriptAct_AddInt_v2_IntResult_104;

	private float logic_uScriptAct_AddInt_v2_FloatResult_104;

	private bool logic_uScriptAct_AddInt_v2_Out_104 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_106 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_106 = 1;

	private int logic_uScriptAct_SetInt_Target_106;

	private bool logic_uScriptAct_SetInt_Out_106 = true;

	private uScript_DisablePlayerInput logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_110 = new uScript_DisablePlayerInput();

	private bool logic_uScript_DisablePlayerInput_disableInput_110 = true;

	private bool logic_uScript_DisablePlayerInput_Out_110 = true;

	private uScript_DisablePlayerInput logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_111 = new uScript_DisablePlayerInput();

	private bool logic_uScript_DisablePlayerInput_disableInput_111;

	private bool logic_uScript_DisablePlayerInput_Out_111 = true;

	private uScript_Gauntlet_EndTutorial logic_uScript_Gauntlet_EndTutorial_uScript_Gauntlet_EndTutorial_112 = new uScript_Gauntlet_EndTutorial();

	private bool logic_uScript_Gauntlet_EndTutorial_Out_112 = true;

	private uScript_Gauntlet_IsTutorialActive logic_uScript_Gauntlet_IsTutorialActive_uScript_Gauntlet_IsTutorialActive_113 = new uScript_Gauntlet_IsTutorialActive();

	private bool logic_uScript_Gauntlet_IsTutorialActive_True_113 = true;

	private bool logic_uScript_Gauntlet_IsTutorialActive_False_113 = true;

	private uScript_HideHUD logic_uScript_HideHUD_uScript_HideHUD_114 = new uScript_HideHUD();

	private bool logic_uScript_HideHUD_hide_114;

	private bool logic_uScript_HideHUD_Out_114 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_115 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_115 = uScript_ChangeBuildingOptions.BuildingOptions.ReduceDragSpeed;

	private bool logic_uScript_ChangeBuildingOptions_allow_115;

	private bool logic_uScript_ChangeBuildingOptions_Out_115 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_116 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_116;

	private bool logic_uScript_ChangeBuildingOptions_allow_116 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_116 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_117 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_117 = uScript_ChangeBuildingOptions.BuildingOptions.Detach;

	private bool logic_uScript_ChangeBuildingOptions_allow_117 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_117 = true;

	private uScript_SetGrabDistance logic_uScript_SetGrabDistance_uScript_SetGrabDistance_118 = new uScript_SetGrabDistance();

	private float logic_uScript_SetGrabDistance_dist_118;

	private bool logic_uScript_SetGrabDistance_Out_118 = true;

	private uScript_SetPlacementFilter logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_119 = new uScript_SetPlacementFilter();

	private Vector3[] logic_uScript_SetPlacementFilter_placementFilter_119 = new Vector3[0];

	private bool logic_uScript_SetPlacementFilter_Out_119 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_120 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_120;

	private bool logic_uScript_Wait_repeat_120;

	private bool logic_uScript_Wait_Waited_120 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_121 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_121;

	private bool logic_uScriptAct_SetBool_Out_121 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_121 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_121 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_124 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_124;

	private bool logic_uScriptAct_SetBool_Out_124 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_124 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_124 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_127 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_127 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_127 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_127;

	private string logic_uScript_AddOnScreenMessage_tag_127 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_127;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_127;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_127;

	private bool logic_uScript_AddOnScreenMessage_Out_127 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_127 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_129 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_129 = 1;

	private int logic_uScriptAct_AddInt_v2_B_129;

	private int logic_uScriptAct_AddInt_v2_IntResult_129;

	private float logic_uScriptAct_AddInt_v2_FloatResult_129;

	private bool logic_uScriptAct_AddInt_v2_Out_129 = true;

	private uScript_BlockPaletteOptions logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_131 = new uScript_BlockPaletteOptions();

	private bool logic_uScript_BlockPaletteOptions_show_131;

	private bool logic_uScript_BlockPaletteOptions_open_131;

	private bool logic_uScript_BlockPaletteOptions_Out_131 = true;

	private uScript_BlockPaletteOptions logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_132 = new uScript_BlockPaletteOptions();

	private bool logic_uScript_BlockPaletteOptions_show_132 = true;

	private bool logic_uScript_BlockPaletteOptions_open_132 = true;

	private bool logic_uScript_BlockPaletteOptions_Out_132 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_135 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_135;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_135 = "BodyBlock3";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_135;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_135;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_135 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_137 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_137;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_137 = "Booster1";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_137;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_137;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_137 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_139 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_139;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_139 = "Booster2";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_139;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_139;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_139 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_144 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_144 = 1;

	private int logic_uScriptAct_AddInt_v2_B_144;

	private int logic_uScriptAct_AddInt_v2_IntResult_144;

	private float logic_uScriptAct_AddInt_v2_FloatResult_144;

	private bool logic_uScriptAct_AddInt_v2_Out_144 = true;

	private uScript_DoesPlayerTankHaveBlock logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_145 = new uScript_DoesPlayerTankHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerTankHaveBlock_blockType_145;

	private int logic_uScript_DoesPlayerTankHaveBlock_amount_145;

	private bool logic_uScript_DoesPlayerTankHaveBlock_True_145 = true;

	private bool logic_uScript_DoesPlayerTankHaveBlock_False_145 = true;

	private uScript_SetPlacementFilter logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_146 = new uScript_SetPlacementFilter();

	private Vector3[] logic_uScript_SetPlacementFilter_placementFilter_146 = new Vector3[0];

	private bool logic_uScript_SetPlacementFilter_Out_146 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_149 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_149 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_149 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_149 = true;

	private string logic_uScript_AddOnScreenMessage_tag_149 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_149;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_149;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_149;

	private bool logic_uScript_AddOnScreenMessage_Out_149 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_149 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_155 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_155 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_155 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_155 = true;

	private string logic_uScript_AddOnScreenMessage_tag_155 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_155;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_155;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_155;

	private bool logic_uScript_AddOnScreenMessage_Out_155 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_155 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_156 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_156 = 5f;

	private bool logic_uScript_Wait_repeat_156;

	private bool logic_uScript_Wait_Waited_156 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_158 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_158 = 1;

	private int logic_uScriptAct_AddInt_v2_B_158;

	private int logic_uScriptAct_AddInt_v2_IntResult_158;

	private float logic_uScriptAct_AddInt_v2_FloatResult_158;

	private bool logic_uScriptAct_AddInt_v2_Out_158 = true;

	private uScript_GetCheckPointIndex logic_uScript_GetCheckPointIndex_uScript_GetCheckPointIndex_164 = new uScript_GetCheckPointIndex();

	private int logic_uScript_GetCheckPointIndex_Return_164;

	private bool logic_uScript_GetCheckPointIndex_Out_164 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_165 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_165;

	private int logic_uScriptCon_CompareInt_B_165;

	private bool logic_uScriptCon_CompareInt_GreaterThan_165 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_165 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_165 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_165 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_165 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_165 = true;

	private uScript_Gauntlet_EndTutorial logic_uScript_Gauntlet_EndTutorial_uScript_Gauntlet_EndTutorial_166 = new uScript_Gauntlet_EndTutorial();

	private bool logic_uScript_Gauntlet_EndTutorial_Out_166 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_179 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_179 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_179 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_179 = true;

	private uScript_HideHUD logic_uScript_HideHUD_uScript_HideHUD_181 = new uScript_HideHUD();

	private bool logic_uScript_HideHUD_hide_181;

	private bool logic_uScript_HideHUD_Out_181 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_182 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_182 = uScript_ChangeBuildingOptions.BuildingOptions.ReduceDragSpeed;

	private bool logic_uScript_ChangeBuildingOptions_allow_182;

	private bool logic_uScript_ChangeBuildingOptions_Out_182 = true;

	private uScript_BlockDecay logic_uScript_BlockDecay_uScript_BlockDecay_183 = new uScript_BlockDecay();

	private bool logic_uScript_BlockDecay_allow_183 = true;

	private bool logic_uScript_BlockDecay_Out_183 = true;

	private uScript_SetGrabDistance logic_uScript_SetGrabDistance_uScript_SetGrabDistance_184 = new uScript_SetGrabDistance();

	private float logic_uScript_SetGrabDistance_dist_184;

	private bool logic_uScript_SetGrabDistance_Out_184 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_185 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_185 = uScript_ChangeBuildingOptions.BuildingOptions.Detach;

	private bool logic_uScript_ChangeBuildingOptions_allow_185 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_185 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_186 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_186;

	private bool logic_uScript_ChangeBuildingOptions_allow_186 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_186 = true;

	private uScript_SetPlacementFilter logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_187 = new uScript_SetPlacementFilter();

	private Vector3[] logic_uScript_SetPlacementFilter_placementFilter_187 = new Vector3[0];

	private bool logic_uScript_SetPlacementFilter_Out_187 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_188 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_188 = uScript_ChangeBuildingOptions.BuildingOptions.ToggleBeam;

	private bool logic_uScript_ChangeBuildingOptions_allow_188 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_188 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_191 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_191 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_191 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_191 = true;

	private uScript_DisableTechRadialMenu logic_uScript_DisableTechRadialMenu_uScript_DisableTechRadialMenu_194 = new uScript_DisableTechRadialMenu();

	private bool logic_uScript_DisableTechRadialMenu_disableRadialMenu_194 = true;

	private bool logic_uScript_DisableTechRadialMenu_Out_194 = true;

	private uScript_DisableTechRadialMenu logic_uScript_DisableTechRadialMenu_uScript_DisableTechRadialMenu_381 = new uScript_DisableTechRadialMenu();

	private bool logic_uScript_DisableTechRadialMenu_disableRadialMenu_381;

	private bool logic_uScript_DisableTechRadialMenu_Out_381 = true;

	private int event_UnityEngine_GameObject_CheckpointIndex_193;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_20 || !m_RegisteredForEvents)
		{
			owner_Connection_20 = parentGameObject;
		}
		if (null == owner_Connection_31 || !m_RegisteredForEvents)
		{
			owner_Connection_31 = parentGameObject;
		}
		if (null == owner_Connection_41 || !m_RegisteredForEvents)
		{
			owner_Connection_41 = parentGameObject;
		}
		if (null == owner_Connection_44 || !m_RegisteredForEvents)
		{
			owner_Connection_44 = parentGameObject;
		}
		if (null == owner_Connection_48 || !m_RegisteredForEvents)
		{
			owner_Connection_48 = parentGameObject;
		}
		if (null == owner_Connection_55 || !m_RegisteredForEvents)
		{
			owner_Connection_55 = parentGameObject;
		}
		if (null == owner_Connection_77 || !m_RegisteredForEvents)
		{
			owner_Connection_77 = parentGameObject;
			if (null != owner_Connection_77)
			{
				uScript_Update uScript_Update2 = owner_Connection_77.GetComponent<uScript_Update>();
				if (null == uScript_Update2)
				{
					uScript_Update2 = owner_Connection_77.AddComponent<uScript_Update>();
				}
				if (null != uScript_Update2)
				{
					uScript_Update2.OnUpdate += Instance_OnUpdate_76;
					uScript_Update2.OnLateUpdate += Instance_OnLateUpdate_76;
					uScript_Update2.OnFixedUpdate += Instance_OnFixedUpdate_76;
				}
			}
		}
		if (null == owner_Connection_84 || !m_RegisteredForEvents)
		{
			owner_Connection_84 = parentGameObject;
		}
		if (null == owner_Connection_134 || !m_RegisteredForEvents)
		{
			owner_Connection_134 = parentGameObject;
		}
		if (null == owner_Connection_138 || !m_RegisteredForEvents)
		{
			owner_Connection_138 = parentGameObject;
		}
		if (null == owner_Connection_140 || !m_RegisteredForEvents)
		{
			owner_Connection_140 = parentGameObject;
		}
		if (!(null == owner_Connection_162) && m_RegisteredForEvents)
		{
			return;
		}
		owner_Connection_162 = parentGameObject;
		if (null != owner_Connection_162)
		{
			uScript_CheckPointPassedEvent uScript_CheckPointPassedEvent2 = owner_Connection_162.GetComponent<uScript_CheckPointPassedEvent>();
			if (null == uScript_CheckPointPassedEvent2)
			{
				uScript_CheckPointPassedEvent2 = owner_Connection_162.AddComponent<uScript_CheckPointPassedEvent>();
			}
			if (null != uScript_CheckPointPassedEvent2)
			{
				uScript_CheckPointPassedEvent2.OnCheckPointPassed += Instance_OnCheckPointPassed_193;
			}
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_77)
		{
			uScript_Update uScript_Update2 = owner_Connection_77.GetComponent<uScript_Update>();
			if (null == uScript_Update2)
			{
				uScript_Update2 = owner_Connection_77.AddComponent<uScript_Update>();
			}
			if (null != uScript_Update2)
			{
				uScript_Update2.OnUpdate += Instance_OnUpdate_76;
				uScript_Update2.OnLateUpdate += Instance_OnLateUpdate_76;
				uScript_Update2.OnFixedUpdate += Instance_OnFixedUpdate_76;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_162)
		{
			uScript_CheckPointPassedEvent uScript_CheckPointPassedEvent2 = owner_Connection_162.GetComponent<uScript_CheckPointPassedEvent>();
			if (null == uScript_CheckPointPassedEvent2)
			{
				uScript_CheckPointPassedEvent2 = owner_Connection_162.AddComponent<uScript_CheckPointPassedEvent>();
			}
			if (null != uScript_CheckPointPassedEvent2)
			{
				uScript_CheckPointPassedEvent2.OnCheckPointPassed += Instance_OnCheckPointPassed_193;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_77)
		{
			uScript_Update component = owner_Connection_77.GetComponent<uScript_Update>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_76;
				component.OnLateUpdate -= Instance_OnLateUpdate_76;
				component.OnFixedUpdate -= Instance_OnFixedUpdate_76;
			}
		}
		if (null != owner_Connection_162)
		{
			uScript_CheckPointPassedEvent component2 = owner_Connection_162.GetComponent<uScript_CheckPointPassedEvent>();
			if (null != component2)
			{
				component2.OnCheckPointPassed -= Instance_OnCheckPointPassed_193;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_0.SetParent(g);
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_1.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_2.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_3.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_5.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_9.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.SetParent(g);
		logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_13.SetParent(g);
		logic_uScript_HasCameraRotated_uScript_HasCameraRotated_14.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_15.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_16.SetParent(g);
		logic_uScript_HasCameraRotated_uScript_HasCameraRotated_17.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_19.SetParent(g);
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_22.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_23.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_24.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_25.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_27.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_28.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_32.SetParent(g);
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_35.SetParent(g);
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_36.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_38.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_39.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_40.SetParent(g);
		logic_uScript_Wait_uScript_Wait_46.SetParent(g);
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_49.SetParent(g);
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_52.SetParent(g);
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_53.SetParent(g);
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_54.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_65.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_67.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_68.SetParent(g);
		logic_uScript_GraphEvents_uScript_GraphEvents_73.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_75.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_83.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_86.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_93.SetParent(g);
		logic_uScript_HasPlayerFiredGun_uScript_HasPlayerFiredGun_96.SetParent(g);
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_98.SetParent(g);
		logic_uScript_BlockDecay_uScript_BlockDecay_99.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_100.SetParent(g);
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_101.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_104.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_106.SetParent(g);
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_110.SetParent(g);
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_111.SetParent(g);
		logic_uScript_Gauntlet_EndTutorial_uScript_Gauntlet_EndTutorial_112.SetParent(g);
		logic_uScript_Gauntlet_IsTutorialActive_uScript_Gauntlet_IsTutorialActive_113.SetParent(g);
		logic_uScript_HideHUD_uScript_HideHUD_114.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_115.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_116.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_117.SetParent(g);
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_118.SetParent(g);
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_119.SetParent(g);
		logic_uScript_Wait_uScript_Wait_120.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_121.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_124.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_127.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_129.SetParent(g);
		logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_131.SetParent(g);
		logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_132.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_135.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_137.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_139.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_144.SetParent(g);
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_145.SetParent(g);
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_146.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_149.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_155.SetParent(g);
		logic_uScript_Wait_uScript_Wait_156.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_158.SetParent(g);
		logic_uScript_GetCheckPointIndex_uScript_GetCheckPointIndex_164.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_165.SetParent(g);
		logic_uScript_Gauntlet_EndTutorial_uScript_Gauntlet_EndTutorial_166.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_179.SetParent(g);
		logic_uScript_HideHUD_uScript_HideHUD_181.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_182.SetParent(g);
		logic_uScript_BlockDecay_uScript_BlockDecay_183.SetParent(g);
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_184.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_185.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_186.SetParent(g);
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_187.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_188.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_191.SetParent(g);
		logic_uScript_DisableTechRadialMenu_uScript_DisableTechRadialMenu_194.SetParent(g);
		logic_uScript_DisableTechRadialMenu_uScript_DisableTechRadialMenu_381.SetParent(g);
		owner_Connection_20 = parentGameObject;
		owner_Connection_31 = parentGameObject;
		owner_Connection_41 = parentGameObject;
		owner_Connection_44 = parentGameObject;
		owner_Connection_48 = parentGameObject;
		owner_Connection_55 = parentGameObject;
		owner_Connection_77 = parentGameObject;
		owner_Connection_84 = parentGameObject;
		owner_Connection_134 = parentGameObject;
		owner_Connection_138 = parentGameObject;
		owner_Connection_140 = parentGameObject;
		owner_Connection_162 = parentGameObject;
	}

	public void Awake()
	{
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.Output1 += uScriptCon_ManualSwitch_Output1_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.Output2 += uScriptCon_ManualSwitch_Output2_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.Output3 += uScriptCon_ManualSwitch_Output3_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.Output4 += uScriptCon_ManualSwitch_Output4_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.Output5 += uScriptCon_ManualSwitch_Output5_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.Output6 += uScriptCon_ManualSwitch_Output6_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.Output7 += uScriptCon_ManualSwitch_Output7_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.Output8 += uScriptCon_ManualSwitch_Output8_11;
		logic_uScript_GraphEvents_uScript_GraphEvents_73.uScriptEnable += uScript_GraphEvents_uScriptEnable_73;
		logic_uScript_GraphEvents_uScript_GraphEvents_73.uScriptDisable += uScript_GraphEvents_uScriptDisable_73;
		logic_uScript_GraphEvents_uScript_GraphEvents_73.uScriptDestroy += uScript_GraphEvents_uScriptDestroy_73;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output1 += uScriptCon_ManualSwitch_Output1_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output2 += uScriptCon_ManualSwitch_Output2_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output3 += uScriptCon_ManualSwitch_Output3_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output4 += uScriptCon_ManualSwitch_Output4_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output5 += uScriptCon_ManualSwitch_Output5_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output6 += uScriptCon_ManualSwitch_Output6_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output7 += uScriptCon_ManualSwitch_Output7_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output8 += uScriptCon_ManualSwitch_Output8_89;
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
		logic_uScript_GraphEvents_uScript_GraphEvents_73.OnEnable();
		logic_uScript_GetCheckPointIndex_uScript_GetCheckPointIndex_164.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_2.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_3.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_9.OnDisable();
		logic_uScript_HasCameraRotated_uScript_HasCameraRotated_14.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_15.OnDisable();
		logic_uScript_HasCameraRotated_uScript_HasCameraRotated_17.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_19.OnDisable();
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_22.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_23.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_24.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_25.OnDisable();
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_35.OnDisable();
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_36.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_38.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_39.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_40.OnDisable();
		logic_uScript_Wait_uScript_Wait_46.OnDisable();
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_52.OnDisable();
		logic_uScript_GraphEvents_uScript_GraphEvents_73.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_83.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_93.OnDisable();
		logic_uScript_HasPlayerFiredGun_uScript_HasPlayerFiredGun_96.OnDisable();
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_98.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_100.OnDisable();
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_101.OnDisable();
		logic_uScript_Wait_uScript_Wait_120.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_127.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_135.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_137.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_139.OnDisable();
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_145.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_149.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_155.OnDisable();
		logic_uScript_Wait_uScript_Wait_156.OnDisable();
		logic_uScript_GetCheckPointIndex_uScript_GetCheckPointIndex_164.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
	}

	public void OnDestroy()
	{
		logic_uScript_GraphEvents_uScript_GraphEvents_73.OnDestroy();
		logic_uScript_Gauntlet_IsTutorialActive_uScript_Gauntlet_IsTutorialActive_113.OnDestroy();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.Output1 -= uScriptCon_ManualSwitch_Output1_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.Output2 -= uScriptCon_ManualSwitch_Output2_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.Output3 -= uScriptCon_ManualSwitch_Output3_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.Output4 -= uScriptCon_ManualSwitch_Output4_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.Output5 -= uScriptCon_ManualSwitch_Output5_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.Output6 -= uScriptCon_ManualSwitch_Output6_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.Output7 -= uScriptCon_ManualSwitch_Output7_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.Output8 -= uScriptCon_ManualSwitch_Output8_11;
		logic_uScript_GraphEvents_uScript_GraphEvents_73.uScriptEnable -= uScript_GraphEvents_uScriptEnable_73;
		logic_uScript_GraphEvents_uScript_GraphEvents_73.uScriptDisable -= uScript_GraphEvents_uScriptDisable_73;
		logic_uScript_GraphEvents_uScript_GraphEvents_73.uScriptDestroy -= uScript_GraphEvents_uScriptDestroy_73;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output1 -= uScriptCon_ManualSwitch_Output1_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output2 -= uScriptCon_ManualSwitch_Output2_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output3 -= uScriptCon_ManualSwitch_Output3_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output4 -= uScriptCon_ManualSwitch_Output4_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output5 -= uScriptCon_ManualSwitch_Output5_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output6 -= uScriptCon_ManualSwitch_Output6_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output7 -= uScriptCon_ManualSwitch_Output7_89;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.Output8 -= uScriptCon_ManualSwitch_Output8_89;
	}

	private void Instance_OnUpdate_76(object o, EventArgs e)
	{
		Relay_OnUpdate_76();
	}

	private void Instance_OnLateUpdate_76(object o, EventArgs e)
	{
		Relay_OnLateUpdate_76();
	}

	private void Instance_OnFixedUpdate_76(object o, EventArgs e)
	{
		Relay_OnFixedUpdate_76();
	}

	private void Instance_OnCheckPointPassed_193(object o, uScript_CheckPointPassedEvent.CheckpointPassedEventArgs e)
	{
		event_UnityEngine_GameObject_CheckpointIndex_193 = e.CheckpointIndex;
		Relay_OnCheckPointPassed_193();
	}

	private void uScriptCon_ManualSwitch_Output1_11(object o, EventArgs e)
	{
		Relay_Output1_11();
	}

	private void uScriptCon_ManualSwitch_Output2_11(object o, EventArgs e)
	{
		Relay_Output2_11();
	}

	private void uScriptCon_ManualSwitch_Output3_11(object o, EventArgs e)
	{
		Relay_Output3_11();
	}

	private void uScriptCon_ManualSwitch_Output4_11(object o, EventArgs e)
	{
		Relay_Output4_11();
	}

	private void uScriptCon_ManualSwitch_Output5_11(object o, EventArgs e)
	{
		Relay_Output5_11();
	}

	private void uScriptCon_ManualSwitch_Output6_11(object o, EventArgs e)
	{
		Relay_Output6_11();
	}

	private void uScriptCon_ManualSwitch_Output7_11(object o, EventArgs e)
	{
		Relay_Output7_11();
	}

	private void uScriptCon_ManualSwitch_Output8_11(object o, EventArgs e)
	{
		Relay_Output8_11();
	}

	private void uScript_GraphEvents_uScriptEnable_73(object o, EventArgs e)
	{
		Relay_uScriptEnable_73();
	}

	private void uScript_GraphEvents_uScriptDisable_73(object o, EventArgs e)
	{
		Relay_uScriptDisable_73();
	}

	private void uScript_GraphEvents_uScriptDestroy_73(object o, EventArgs e)
	{
		Relay_uScriptDestroy_73();
	}

	private void uScriptCon_ManualSwitch_Output1_89(object o, EventArgs e)
	{
		Relay_Output1_89();
	}

	private void uScriptCon_ManualSwitch_Output2_89(object o, EventArgs e)
	{
		Relay_Output2_89();
	}

	private void uScriptCon_ManualSwitch_Output3_89(object o, EventArgs e)
	{
		Relay_Output3_89();
	}

	private void uScriptCon_ManualSwitch_Output4_89(object o, EventArgs e)
	{
		Relay_Output4_89();
	}

	private void uScriptCon_ManualSwitch_Output5_89(object o, EventArgs e)
	{
		Relay_Output5_89();
	}

	private void uScriptCon_ManualSwitch_Output6_89(object o, EventArgs e)
	{
		Relay_Output6_89();
	}

	private void uScriptCon_ManualSwitch_Output7_89(object o, EventArgs e)
	{
		Relay_Output7_89();
	}

	private void uScriptCon_ManualSwitch_Output8_89(object o, EventArgs e)
	{
		Relay_Output8_89();
	}

	private void Relay_In_0()
	{
		logic_uScriptCon_CompareBool_Bool_0 = local_Msg03ReminderOnScreen_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_0.In(logic_uScriptCon_CompareBool_Bool_0);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_0.False)
		{
			Relay_In_9();
		}
	}

	private void Relay_In_1()
	{
		int num = 0;
		Array array = tutorialGunPlacementFilter;
		if (logic_uScript_SetPlacementFilter_placementFilter_1.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetPlacementFilter_placementFilter_1, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetPlacementFilter_placementFilter_1, num, array.Length);
		num += array.Length;
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_1.In(logic_uScript_SetPlacementFilter_placementFilter_1);
		if (logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_1.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_In_2()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_2 = tutorialWheelBlock;
		logic_uScript_SpawnBlockAbovePlayer_owner_2 = owner_Connection_41;
		logic_uScript_SpawnBlockAbovePlayer_Return_2 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_2.In(logic_uScript_SpawnBlockAbovePlayer_blockType_2, logic_uScript_SpawnBlockAbovePlayer_uniqueName_2, logic_uScript_SpawnBlockAbovePlayer_owner_2);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_2.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_In_3()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_3 = tutorialGunBlock;
		logic_uScript_SpawnBlockAbovePlayer_owner_3 = owner_Connection_48;
		logic_uScript_SpawnBlockAbovePlayer_Return_3 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_3.In(logic_uScript_SpawnBlockAbovePlayer_blockType_3, logic_uScript_SpawnBlockAbovePlayer_uniqueName_3, logic_uScript_SpawnBlockAbovePlayer_owner_3);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_3.Out)
		{
			Relay_In_1();
		}
	}

	private void Relay_In_4()
	{
		int num = 0;
		Array array = tutorialMsg02AttachBodyBlock;
		if (logic_uScript_AddOnScreenMessage_locString_4.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_4, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_4, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_4 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_4 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.In(logic_uScript_AddOnScreenMessage_locString_4, logic_uScript_AddOnScreenMessage_msgPriority_4, logic_uScript_AddOnScreenMessage_holdMsg_4, logic_uScript_AddOnScreenMessage_tag_4, logic_uScript_AddOnScreenMessage_speaker_4, logic_uScript_AddOnScreenMessage_side_4);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_4.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_In_5()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_5.In(logic_uScript_ChangeBuildingOptions_change_5, logic_uScript_ChangeBuildingOptions_allow_5);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_5.Out)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_9()
	{
		int num = 0;
		Array array = tutorialMsg03aAttachWheels;
		if (logic_uScript_AddOnScreenMessage_locString_9.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_9, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_9, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_9 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_9 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_9.In(logic_uScript_AddOnScreenMessage_locString_9, logic_uScript_AddOnScreenMessage_msgPriority_9, logic_uScript_AddOnScreenMessage_holdMsg_9, logic_uScript_AddOnScreenMessage_tag_9, logic_uScript_AddOnScreenMessage_speaker_9, logic_uScript_AddOnScreenMessage_side_9);
	}

	private void Relay_Output1_11()
	{
		Relay_In_110();
	}

	private void Relay_Output2_11()
	{
		Relay_In_38();
	}

	private void Relay_Output3_11()
	{
		Relay_In_40();
	}

	private void Relay_Output4_11()
	{
		Relay_In_137();
	}

	private void Relay_Output5_11()
	{
		Relay_In_3();
	}

	private void Relay_Output6_11()
	{
		Relay_In_87();
	}

	private void Relay_Output7_11()
	{
	}

	private void Relay_Output8_11()
	{
	}

	private void Relay_In_11()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_11 = local_TutorialBuildStage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_11.In(logic_uScriptCon_ManualSwitch_CurrentOutput_11);
	}

	private void Relay_In_13()
	{
		logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_13.In();
		bool num = logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_13.True;
		bool flag = logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_13.False;
		if (num)
		{
			Relay_In_25();
		}
		if (flag)
		{
			Relay_False_92();
		}
	}

	private void Relay_In_14()
	{
		logic_uScript_HasCameraRotated_turnCameraMinDegrees_14 = tutorialCamTurnDegrees;
		logic_uScript_HasCameraRotated_uScript_HasCameraRotated_14.In(logic_uScript_HasCameraRotated_turnCameraMinDegrees_14);
		bool num = logic_uScript_HasCameraRotated_uScript_HasCameraRotated_14.True;
		bool flag = logic_uScript_HasCameraRotated_uScript_HasCameraRotated_14.False;
		if (num)
		{
			Relay_In_65();
		}
		if (flag)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_15()
	{
		int num = 0;
		Array array = tutorialMsg03aRotateToFindWheels;
		if (logic_uScript_AddOnScreenMessage_locString_15.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_15, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_15, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_15 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_15 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_15.In(logic_uScript_AddOnScreenMessage_locString_15, logic_uScript_AddOnScreenMessage_msgPriority_15, logic_uScript_AddOnScreenMessage_holdMsg_15, logic_uScript_AddOnScreenMessage_tag_15, logic_uScript_AddOnScreenMessage_speaker_15, logic_uScript_AddOnScreenMessage_side_15);
		local_TutorialMsg03aRotateToFindWheels_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddOnScreenMessage_Return_15;
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_15.Out)
		{
			Relay_False_29();
		}
	}

	private void Relay_In_16()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_16.In(logic_uScript_ChangeBuildingOptions_change_16, logic_uScript_ChangeBuildingOptions_allow_16);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_16.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_In_17()
	{
		logic_uScript_HasCameraRotated_uScript_HasCameraRotated_17.In(logic_uScript_HasCameraRotated_turnCameraMinDegrees_17);
		bool num = logic_uScript_HasCameraRotated_uScript_HasCameraRotated_17.True;
		bool flag = logic_uScript_HasCameraRotated_uScript_HasCameraRotated_17.False;
		if (num)
		{
			Relay_In_0();
		}
		if (flag)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_18()
	{
		int num = 0;
		Array array = tutorialMsg04AttachGun;
		if (logic_uScript_AddOnScreenMessage_locString_18.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_18, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_18, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_18 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_18 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.In(logic_uScript_AddOnScreenMessage_locString_18, logic_uScript_AddOnScreenMessage_msgPriority_18, logic_uScript_AddOnScreenMessage_holdMsg_18, logic_uScript_AddOnScreenMessage_tag_18, logic_uScript_AddOnScreenMessage_speaker_18, logic_uScript_AddOnScreenMessage_side_18);
	}

	private void Relay_In_19()
	{
		int num = 0;
		Array array = tutorialMsg03aAttachWheels;
		if (logic_uScript_AddOnScreenMessage_locString_19.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_19, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_19, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_19 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_19 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_19.In(logic_uScript_AddOnScreenMessage_locString_19, logic_uScript_AddOnScreenMessage_msgPriority_19, logic_uScript_AddOnScreenMessage_holdMsg_19, logic_uScript_AddOnScreenMessage_tag_19, logic_uScript_AddOnScreenMessage_speaker_19, logic_uScript_AddOnScreenMessage_side_19);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_19.Out)
		{
			Relay_True_124();
		}
	}

	private void Relay_In_22()
	{
		logic_uScript_DoesPlayerTankHaveBlock_blockType_22 = tutorialGunBlock;
		logic_uScript_DoesPlayerTankHaveBlock_amount_22 = tutorialGunAmount;
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_22.In(logic_uScript_DoesPlayerTankHaveBlock_blockType_22, logic_uScript_DoesPlayerTankHaveBlock_amount_22);
		bool num = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_22.True;
		bool flag = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_22.False;
		if (num)
		{
			Relay_In_86();
		}
		if (flag)
		{
			Relay_In_18();
		}
	}

	private void Relay_In_23()
	{
		int num = 0;
		Array array = tutorialMsg01RotateCamera;
		if (logic_uScript_AddOnScreenMessage_locString_23.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_23, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_23, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_23 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_23 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_23.In(logic_uScript_AddOnScreenMessage_locString_23, logic_uScript_AddOnScreenMessage_msgPriority_23, logic_uScript_AddOnScreenMessage_holdMsg_23, logic_uScript_AddOnScreenMessage_tag_23, logic_uScript_AddOnScreenMessage_speaker_23, logic_uScript_AddOnScreenMessage_side_23);
	}

	private void Relay_In_24()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_24 = tutorialWheelBlock;
		logic_uScript_SpawnBlockAbovePlayer_owner_24 = owner_Connection_31;
		logic_uScript_SpawnBlockAbovePlayer_Return_24 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_24.In(logic_uScript_SpawnBlockAbovePlayer_blockType_24, logic_uScript_SpawnBlockAbovePlayer_uniqueName_24, logic_uScript_SpawnBlockAbovePlayer_owner_24);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_24.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_25()
	{
		int num = 0;
		Array array = tutorialMsg05ExitBeam;
		if (logic_uScript_AddOnScreenMessage_locString_25.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_25, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_25, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_25 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_25 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_25.In(logic_uScript_AddOnScreenMessage_locString_25, logic_uScript_AddOnScreenMessage_msgPriority_25, logic_uScript_AddOnScreenMessage_holdMsg_25, logic_uScript_AddOnScreenMessage_tag_25, logic_uScript_AddOnScreenMessage_speaker_25, logic_uScript_AddOnScreenMessage_side_25);
	}

	private void Relay_In_27()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_27.In(logic_uScript_ChangeBuildingOptions_change_27, logic_uScript_ChangeBuildingOptions_allow_27);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_27.Out)
		{
			Relay_SetDefault_53();
		}
	}

	private void Relay_In_28()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_28.In(logic_uScript_ChangeBuildingOptions_change_28, logic_uScript_ChangeBuildingOptions_allow_28);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_28.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_True_29()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.True(out logic_uScriptAct_SetBool_Target_29);
		local_Msg03ReminderOnScreen_System_Boolean = logic_uScriptAct_SetBool_Target_29;
	}

	private void Relay_False_29()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.False(out logic_uScriptAct_SetBool_Target_29);
		local_Msg03ReminderOnScreen_System_Boolean = logic_uScriptAct_SetBool_Target_29;
	}

	private void Relay_In_32()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_32.In(logic_uScript_ChangeBuildingOptions_change_32, logic_uScript_ChangeBuildingOptions_allow_32);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_32.Out)
		{
			Relay_In_13();
		}
	}

	private void Relay_In_35()
	{
		logic_uScript_DoesPlayerTankHaveBlock_blockType_35 = tutorialWheelBlock;
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_35.In(logic_uScript_DoesPlayerTankHaveBlock_blockType_35, logic_uScript_DoesPlayerTankHaveBlock_amount_35);
		bool num = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_35.True;
		bool flag = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_35.False;
		if (num)
		{
			Relay_In_52();
		}
		if (flag)
		{
			Relay_In_19();
		}
	}

	private void Relay_In_36()
	{
		logic_uScript_DoesPlayerTankHaveBlock_blockType_36 = tutorialBodyBlock;
		logic_uScript_DoesPlayerTankHaveBlock_amount_36 = tutorialBodyBlockAmount;
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_36.In(logic_uScript_DoesPlayerTankHaveBlock_blockType_36, logic_uScript_DoesPlayerTankHaveBlock_amount_36);
		if (logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_36.True)
		{
			Relay_In_67();
		}
	}

	private void Relay_In_38()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_38 = tutorialBodyBlock;
		logic_uScript_SpawnBlockAbovePlayer_owner_38 = owner_Connection_55;
		logic_uScript_SpawnBlockAbovePlayer_Return_38 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_38.In(logic_uScript_SpawnBlockAbovePlayer_blockType_38, logic_uScript_SpawnBlockAbovePlayer_uniqueName_38, logic_uScript_SpawnBlockAbovePlayer_owner_38);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_38.Out)
		{
			Relay_In_83();
		}
	}

	private void Relay_In_39()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_39 = tutorialWheelBlock;
		logic_uScript_SpawnBlockAbovePlayer_owner_39 = owner_Connection_20;
		logic_uScript_SpawnBlockAbovePlayer_Return_39 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_39.In(logic_uScript_SpawnBlockAbovePlayer_blockType_39, logic_uScript_SpawnBlockAbovePlayer_uniqueName_39, logic_uScript_SpawnBlockAbovePlayer_owner_39);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_39.Out)
		{
			Relay_In_49();
		}
	}

	private void Relay_In_40()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_40 = tutorialWheelBlock;
		logic_uScript_SpawnBlockAbovePlayer_owner_40 = owner_Connection_44;
		logic_uScript_SpawnBlockAbovePlayer_Return_40 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_40.In(logic_uScript_SpawnBlockAbovePlayer_blockType_40, logic_uScript_SpawnBlockAbovePlayer_uniqueName_40, logic_uScript_SpawnBlockAbovePlayer_owner_40);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_40.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_46()
	{
		logic_uScript_Wait_uScript_Wait_46.In(logic_uScript_Wait_seconds_46, logic_uScript_Wait_repeat_46);
		if (logic_uScript_Wait_uScript_Wait_46.Waited)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_49()
	{
		int num = 0;
		Array array = tutorialWheelPlacementFilter;
		if (logic_uScript_SetPlacementFilter_placementFilter_49.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetPlacementFilter_placementFilter_49, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetPlacementFilter_placementFilter_49, num, array.Length);
		num += array.Length;
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_49.In(logic_uScript_SetPlacementFilter_placementFilter_49);
		if (logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_49.Out)
		{
			Relay_In_35();
		}
	}

	private void Relay_In_52()
	{
		logic_uScript_DoesPlayerTankHaveBlock_blockType_52 = tutorialWheelBlock;
		logic_uScript_DoesPlayerTankHaveBlock_amount_52 = tutorialWheelAmount;
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_52.In(logic_uScript_DoesPlayerTankHaveBlock_blockType_52, logic_uScript_DoesPlayerTankHaveBlock_amount_52);
		bool num = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_52.True;
		bool flag = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_52.False;
		if (num)
		{
			Relay_In_68();
		}
		if (flag)
		{
			Relay_In_17();
		}
	}

	private void Relay_SetMainDefault_53()
	{
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_53.SetMainDefault(logic_uScript_SetGrabDistance_dist_53);
		if (logic_uScript_SetGrabDistance_uScript_SetGrabDistance_53.Out)
		{
			Relay_In_120();
		}
	}

	private void Relay_SetDefault_53()
	{
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_53.SetDefault(logic_uScript_SetGrabDistance_dist_53);
		if (logic_uScript_SetGrabDistance_uScript_SetGrabDistance_53.Out)
		{
			Relay_In_120();
		}
	}

	private void Relay_Set_53()
	{
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_53.Set(logic_uScript_SetGrabDistance_dist_53);
		if (logic_uScript_SetGrabDistance_uScript_SetGrabDistance_53.Out)
		{
			Relay_In_120();
		}
	}

	private void Relay_In_54()
	{
		int num = 0;
		Array array = tutorialBodyPlacementFilter;
		if (logic_uScript_SetPlacementFilter_placementFilter_54.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetPlacementFilter_placementFilter_54, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetPlacementFilter_placementFilter_54, num, array.Length);
		num += array.Length;
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_54.In(logic_uScript_SetPlacementFilter_placementFilter_54);
		if (logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_54.Out)
		{
			Relay_In_4();
		}
	}

	private void Relay_In_65()
	{
		logic_uScriptAct_AddInt_v2_B_65 = local_TutorialBuildStage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_65.In(logic_uScriptAct_AddInt_v2_A_65, logic_uScriptAct_AddInt_v2_B_65, out logic_uScriptAct_AddInt_v2_IntResult_65, out logic_uScriptAct_AddInt_v2_FloatResult_65);
		local_TutorialBuildStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_65;
	}

	private void Relay_In_67()
	{
		logic_uScriptAct_AddInt_v2_B_67 = local_TutorialBuildStage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_67.In(logic_uScriptAct_AddInt_v2_A_67, logic_uScriptAct_AddInt_v2_B_67, out logic_uScriptAct_AddInt_v2_IntResult_67, out logic_uScriptAct_AddInt_v2_FloatResult_67);
		local_TutorialBuildStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_67;
	}

	private void Relay_In_68()
	{
		logic_uScriptAct_AddInt_v2_B_68 = local_TutorialBuildStage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_68.In(logic_uScriptAct_AddInt_v2_A_68, logic_uScriptAct_AddInt_v2_B_68, out logic_uScriptAct_AddInt_v2_IntResult_68, out logic_uScriptAct_AddInt_v2_FloatResult_68);
		local_TutorialBuildStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_68;
	}

	private void Relay_uScriptEnable_73()
	{
		Relay_In_75();
	}

	private void Relay_uScriptDisable_73()
	{
		Relay_In_183();
	}

	private void Relay_uScriptDestroy_73()
	{
		Relay_In_183();
	}

	private void Relay_In_75()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_75.In(logic_uScriptAct_SetInt_Value_75, out logic_uScriptAct_SetInt_Target_75);
		local_TutorialBuildStage_System_Int32 = logic_uScriptAct_SetInt_Target_75;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_75.Out)
		{
			Relay_In_106();
		}
	}

	private void Relay_OnUpdate_76()
	{
		Relay_In_113();
	}

	private void Relay_OnLateUpdate_76()
	{
	}

	private void Relay_OnFixedUpdate_76()
	{
	}

	private void Relay_In_83()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_83 = tutorialBodyBlock;
		logic_uScript_SpawnBlockAbovePlayer_owner_83 = owner_Connection_84;
		logic_uScript_SpawnBlockAbovePlayer_Return_83 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_83.In(logic_uScript_SpawnBlockAbovePlayer_blockType_83, logic_uScript_SpawnBlockAbovePlayer_uniqueName_83, logic_uScript_SpawnBlockAbovePlayer_owner_83);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_83.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_In_86()
	{
		logic_uScriptAct_AddInt_v2_B_86 = local_TutorialBuildStage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_86.In(logic_uScriptAct_AddInt_v2_A_86, logic_uScriptAct_AddInt_v2_B_86, out logic_uScriptAct_AddInt_v2_IntResult_86, out logic_uScriptAct_AddInt_v2_FloatResult_86);
		local_TutorialBuildStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_86;
	}

	private void Relay_In_87()
	{
		logic_uScriptCon_CompareBool_Bool_87 = local_TutorialInBuildPhase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.In(logic_uScriptCon_CompareBool_Bool_87);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.False;
		if (num)
		{
			Relay_In_32();
		}
		if (flag)
		{
			Relay_In_89();
		}
	}

	private void Relay_Output1_89()
	{
		Relay_In_93();
	}

	private void Relay_Output2_89()
	{
		Relay_In_100();
	}

	private void Relay_Output3_89()
	{
		Relay_In_155();
	}

	private void Relay_Output4_89()
	{
		Relay_In_99();
	}

	private void Relay_Output5_89()
	{
	}

	private void Relay_Output6_89()
	{
	}

	private void Relay_Output7_89()
	{
	}

	private void Relay_Output8_89()
	{
	}

	private void Relay_In_89()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_89 = local_TutorialDriveStage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_89.In(logic_uScriptCon_ManualSwitch_CurrentOutput_89);
	}

	private void Relay_True_92()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.True(out logic_uScriptAct_SetBool_Target_92);
		local_TutorialInBuildPhase_System_Boolean = logic_uScriptAct_SetBool_Target_92;
	}

	private void Relay_False_92()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.False(out logic_uScriptAct_SetBool_Target_92);
		local_TutorialInBuildPhase_System_Boolean = logic_uScriptAct_SetBool_Target_92;
	}

	private void Relay_In_93()
	{
		int num = 0;
		Array array = tutorialMsg06TryDriving;
		if (logic_uScript_AddOnScreenMessage_locString_93.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_93, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_93, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_93 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_93 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_93.In(logic_uScript_AddOnScreenMessage_locString_93, logic_uScript_AddOnScreenMessage_msgPriority_93, logic_uScript_AddOnScreenMessage_holdMsg_93, logic_uScript_AddOnScreenMessage_tag_93, logic_uScript_AddOnScreenMessage_speaker_93, logic_uScript_AddOnScreenMessage_side_93);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_93.Out)
		{
			Relay_In_98();
		}
	}

	private void Relay_In_96()
	{
		logic_uScript_HasPlayerFiredGun_uScript_HasPlayerFiredGun_96.In();
		bool num = logic_uScript_HasPlayerFiredGun_uScript_HasPlayerFiredGun_96.True;
		bool flag = logic_uScript_HasPlayerFiredGun_uScript_HasPlayerFiredGun_96.False;
		if (num)
		{
			Relay_In_129();
		}
		if (flag)
		{
			Relay_In_101();
		}
	}

	private void Relay_In_98()
	{
		logic_uScript_HasPlayerTraveledDistance_distance_98 = tutorialDistToDrive;
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_98.In(logic_uScript_HasPlayerTraveledDistance_distance_98);
		if (logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_98.True)
		{
			Relay_In_104();
		}
	}

	private void Relay_In_99()
	{
		logic_uScript_BlockDecay_uScript_BlockDecay_99.In(logic_uScript_BlockDecay_allow_99);
		if (logic_uScript_BlockDecay_uScript_BlockDecay_99.Out)
		{
			Relay_In_114();
		}
	}

	private void Relay_In_100()
	{
		int num = 0;
		Array array = tutorialMsg07aTryShooting;
		if (logic_uScript_AddOnScreenMessage_locString_100.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_100, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_100, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_100 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_100 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_100.In(logic_uScript_AddOnScreenMessage_locString_100, logic_uScript_AddOnScreenMessage_msgPriority_100, logic_uScript_AddOnScreenMessage_holdMsg_100, logic_uScript_AddOnScreenMessage_tag_100, logic_uScript_AddOnScreenMessage_speaker_100, logic_uScript_AddOnScreenMessage_side_100);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_100.Out)
		{
			Relay_In_96();
		}
	}

	private void Relay_In_101()
	{
		logic_uScript_HasPlayerTraveledDistance_distance_101 = tutorialDistToDriveAway;
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_101.In(logic_uScript_HasPlayerTraveledDistance_distance_101);
		if (logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_101.True)
		{
			Relay_In_129();
		}
	}

	private void Relay_In_104()
	{
		logic_uScriptAct_AddInt_v2_B_104 = local_TutorialDriveStage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_104.In(logic_uScriptAct_AddInt_v2_A_104, logic_uScriptAct_AddInt_v2_B_104, out logic_uScriptAct_AddInt_v2_IntResult_104, out logic_uScriptAct_AddInt_v2_FloatResult_104);
		local_TutorialDriveStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_104;
	}

	private void Relay_In_106()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_106.In(logic_uScriptAct_SetInt_Value_106, out logic_uScriptAct_SetInt_Target_106);
		local_TutorialDriveStage_System_Int32 = logic_uScriptAct_SetInt_Target_106;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_106.Out)
		{
			Relay_True_121();
		}
	}

	private void Relay_In_110()
	{
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_110.In(logic_uScript_DisablePlayerInput_disableInput_110);
		if (logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_110.Out)
		{
			Relay_In_131();
		}
	}

	private void Relay_In_111()
	{
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_111.In(logic_uScript_DisablePlayerInput_disableInput_111);
		if (logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_111.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_In_112()
	{
		logic_uScript_Gauntlet_EndTutorial_uScript_Gauntlet_EndTutorial_112.In();
	}

	private void Relay_In_113()
	{
		logic_uScript_Gauntlet_IsTutorialActive_uScript_Gauntlet_IsTutorialActive_113.In();
		if (logic_uScript_Gauntlet_IsTutorialActive_uScript_Gauntlet_IsTutorialActive_113.True)
		{
			Relay_In_11();
		}
	}

	private void Relay_In_114()
	{
		logic_uScript_HideHUD_uScript_HideHUD_114.In(logic_uScript_HideHUD_hide_114);
		if (logic_uScript_HideHUD_uScript_HideHUD_114.Out)
		{
			Relay_In_119();
		}
	}

	private void Relay_In_115()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_115.In(logic_uScript_ChangeBuildingOptions_change_115, logic_uScript_ChangeBuildingOptions_allow_115);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_115.Out)
		{
			Relay_SetMainDefault_118();
		}
	}

	private void Relay_In_116()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_116.In(logic_uScript_ChangeBuildingOptions_change_116, logic_uScript_ChangeBuildingOptions_allow_116);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_116.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_In_117()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_117.In(logic_uScript_ChangeBuildingOptions_change_117, logic_uScript_ChangeBuildingOptions_allow_117);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_117.Out)
		{
			Relay_In_115();
		}
	}

	private void Relay_SetMainDefault_118()
	{
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_118.SetMainDefault(logic_uScript_SetGrabDistance_dist_118);
		if (logic_uScript_SetGrabDistance_uScript_SetGrabDistance_118.Out)
		{
			Relay_In_132();
		}
	}

	private void Relay_SetDefault_118()
	{
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_118.SetDefault(logic_uScript_SetGrabDistance_dist_118);
		if (logic_uScript_SetGrabDistance_uScript_SetGrabDistance_118.Out)
		{
			Relay_In_132();
		}
	}

	private void Relay_Set_118()
	{
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_118.Set(logic_uScript_SetGrabDistance_dist_118);
		if (logic_uScript_SetGrabDistance_uScript_SetGrabDistance_118.Out)
		{
			Relay_In_132();
		}
	}

	private void Relay_In_119()
	{
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_119.In(logic_uScript_SetPlacementFilter_placementFilter_119);
		if (logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_119.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_In_120()
	{
		logic_uScript_Wait_seconds_120 = TimeBeforeTutorialStart;
		logic_uScript_Wait_uScript_Wait_120.In(logic_uScript_Wait_seconds_120, logic_uScript_Wait_repeat_120);
		if (logic_uScript_Wait_uScript_Wait_120.Waited)
		{
			Relay_In_111();
		}
	}

	private void Relay_True_121()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_121.True(out logic_uScriptAct_SetBool_Target_121);
		local_TutorialInBuildPhase_System_Boolean = logic_uScriptAct_SetBool_Target_121;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_121.Out)
		{
			Relay_In_191();
		}
	}

	private void Relay_False_121()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_121.False(out logic_uScriptAct_SetBool_Target_121);
		local_TutorialInBuildPhase_System_Boolean = logic_uScriptAct_SetBool_Target_121;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_121.Out)
		{
			Relay_In_191();
		}
	}

	private void Relay_True_124()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_124.True(out logic_uScriptAct_SetBool_Target_124);
		local_Msg03ReminderOnScreen_System_Boolean = logic_uScriptAct_SetBool_Target_124;
	}

	private void Relay_False_124()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_124.False(out logic_uScriptAct_SetBool_Target_124);
		local_Msg03ReminderOnScreen_System_Boolean = logic_uScriptAct_SetBool_Target_124;
	}

	private void Relay_In_127()
	{
		int num = 0;
		Array array = tutorialMsg08BlockPalette;
		if (logic_uScript_AddOnScreenMessage_locString_127.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_127, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_127, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_127 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_127 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_127.In(logic_uScript_AddOnScreenMessage_locString_127, logic_uScript_AddOnScreenMessage_msgPriority_127, logic_uScript_AddOnScreenMessage_holdMsg_127, logic_uScript_AddOnScreenMessage_tag_127, logic_uScript_AddOnScreenMessage_speaker_127, logic_uScript_AddOnScreenMessage_side_127);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_127.Shown)
		{
			Relay_In_112();
		}
	}

	private void Relay_In_129()
	{
		logic_uScriptAct_AddInt_v2_B_129 = local_TutorialDriveStage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_129.In(logic_uScriptAct_AddInt_v2_A_129, logic_uScriptAct_AddInt_v2_B_129, out logic_uScriptAct_AddInt_v2_IntResult_129, out logic_uScriptAct_AddInt_v2_FloatResult_129);
		local_TutorialDriveStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_129;
	}

	private void Relay_In_131()
	{
		logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_131.In(logic_uScript_BlockPaletteOptions_show_131, logic_uScript_BlockPaletteOptions_open_131);
		if (logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_131.Out)
		{
			Relay_In_194();
		}
	}

	private void Relay_In_132()
	{
		logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_132.In(logic_uScript_BlockPaletteOptions_show_132, logic_uScript_BlockPaletteOptions_open_132);
		if (logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_132.Out)
		{
			Relay_In_381();
		}
	}

	private void Relay_In_135()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_135 = tutorialBodyBlock;
		logic_uScript_SpawnBlockAbovePlayer_owner_135 = owner_Connection_134;
		logic_uScript_SpawnBlockAbovePlayer_Return_135 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_135.In(logic_uScript_SpawnBlockAbovePlayer_blockType_135, logic_uScript_SpawnBlockAbovePlayer_uniqueName_135, logic_uScript_SpawnBlockAbovePlayer_owner_135);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_135.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_137()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_137 = tutorialBoosterBlock;
		logic_uScript_SpawnBlockAbovePlayer_owner_137 = owner_Connection_138;
		logic_uScript_SpawnBlockAbovePlayer_Return_137 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_137.In(logic_uScript_SpawnBlockAbovePlayer_blockType_137, logic_uScript_SpawnBlockAbovePlayer_uniqueName_137, logic_uScript_SpawnBlockAbovePlayer_owner_137);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_137.Out)
		{
			Relay_In_139();
		}
	}

	private void Relay_In_139()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_139 = tutorialBoosterBlock;
		logic_uScript_SpawnBlockAbovePlayer_owner_139 = owner_Connection_140;
		logic_uScript_SpawnBlockAbovePlayer_Return_139 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_139.In(logic_uScript_SpawnBlockAbovePlayer_blockType_139, logic_uScript_SpawnBlockAbovePlayer_uniqueName_139, logic_uScript_SpawnBlockAbovePlayer_owner_139);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_139.Out)
		{
			Relay_In_146();
		}
	}

	private void Relay_In_144()
	{
		logic_uScriptAct_AddInt_v2_B_144 = local_TutorialBuildStage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_144.In(logic_uScriptAct_AddInt_v2_A_144, logic_uScriptAct_AddInt_v2_B_144, out logic_uScriptAct_AddInt_v2_IntResult_144, out logic_uScriptAct_AddInt_v2_FloatResult_144);
		local_TutorialBuildStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_144;
	}

	private void Relay_In_145()
	{
		logic_uScript_DoesPlayerTankHaveBlock_blockType_145 = tutorialBoosterBlock;
		logic_uScript_DoesPlayerTankHaveBlock_amount_145 = tutorialBoosterAmount;
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_145.In(logic_uScript_DoesPlayerTankHaveBlock_blockType_145, logic_uScript_DoesPlayerTankHaveBlock_amount_145);
		bool num = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_145.True;
		bool flag = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_145.False;
		if (num)
		{
			Relay_In_144();
		}
		if (flag)
		{
			Relay_In_149();
		}
	}

	private void Relay_In_146()
	{
		int num = 0;
		Array array = tutorialBoosterPlacementFilter;
		if (logic_uScript_SetPlacementFilter_placementFilter_146.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetPlacementFilter_placementFilter_146, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetPlacementFilter_placementFilter_146, num, array.Length);
		num += array.Length;
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_146.In(logic_uScript_SetPlacementFilter_placementFilter_146);
		if (logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_146.Out)
		{
			Relay_In_145();
		}
	}

	private void Relay_In_149()
	{
		int num = 0;
		Array array = tutorialMsg03bAttachBoosters;
		if (logic_uScript_AddOnScreenMessage_locString_149.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_149, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_149, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_149 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_149 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_149.In(logic_uScript_AddOnScreenMessage_locString_149, logic_uScript_AddOnScreenMessage_msgPriority_149, logic_uScript_AddOnScreenMessage_holdMsg_149, logic_uScript_AddOnScreenMessage_tag_149, logic_uScript_AddOnScreenMessage_speaker_149, logic_uScript_AddOnScreenMessage_side_149);
	}

	private void Relay_In_155()
	{
		int num = 0;
		Array array = tutorialMsg07bTryBoosting;
		if (logic_uScript_AddOnScreenMessage_locString_155.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_155, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_155, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_155 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_155 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_155.In(logic_uScript_AddOnScreenMessage_locString_155, logic_uScript_AddOnScreenMessage_msgPriority_155, logic_uScript_AddOnScreenMessage_holdMsg_155, logic_uScript_AddOnScreenMessage_tag_155, logic_uScript_AddOnScreenMessage_speaker_155, logic_uScript_AddOnScreenMessage_side_155);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_155.Out)
		{
			Relay_In_156();
		}
	}

	private void Relay_In_156()
	{
		logic_uScript_Wait_uScript_Wait_156.In(logic_uScript_Wait_seconds_156, logic_uScript_Wait_repeat_156);
		if (logic_uScript_Wait_uScript_Wait_156.Waited)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_158()
	{
		logic_uScriptAct_AddInt_v2_B_158 = local_TutorialDriveStage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_158.In(logic_uScriptAct_AddInt_v2_A_158, logic_uScriptAct_AddInt_v2_B_158, out logic_uScriptAct_AddInt_v2_IntResult_158, out logic_uScriptAct_AddInt_v2_FloatResult_158);
		local_TutorialDriveStage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_158;
	}

	private void Relay_In_164()
	{
		logic_uScript_GetCheckPointIndex_Return_164 = logic_uScript_GetCheckPointIndex_uScript_GetCheckPointIndex_164.In();
		local_currentCheckpoint_System_Int32 = logic_uScript_GetCheckPointIndex_Return_164;
		if (logic_uScript_GetCheckPointIndex_uScript_GetCheckPointIndex_164.Out)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_165()
	{
		logic_uScriptCon_CompareInt_A_165 = local_currentCheckpoint_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_165.In(logic_uScriptCon_CompareInt_A_165, logic_uScriptCon_CompareInt_B_165);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_165.GreaterThanOrEqualTo)
		{
			Relay_In_179();
		}
	}

	private void Relay_In_166()
	{
		logic_uScript_Gauntlet_EndTutorial_uScript_Gauntlet_EndTutorial_166.In();
	}

	private void Relay_In_179()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_179 = local_msg_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_179.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_179, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_179);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_179.Out)
		{
			Relay_In_166();
		}
	}

	private void Relay_In_181()
	{
		logic_uScript_HideHUD_uScript_HideHUD_181.In(logic_uScript_HideHUD_hide_181);
		if (logic_uScript_HideHUD_uScript_HideHUD_181.Out)
		{
			Relay_In_187();
		}
	}

	private void Relay_In_182()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_182.In(logic_uScript_ChangeBuildingOptions_change_182, logic_uScript_ChangeBuildingOptions_allow_182);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_182.Out)
		{
			Relay_SetMainDefault_184();
		}
	}

	private void Relay_In_183()
	{
		logic_uScript_BlockDecay_uScript_BlockDecay_183.In(logic_uScript_BlockDecay_allow_183);
		if (logic_uScript_BlockDecay_uScript_BlockDecay_183.Out)
		{
			Relay_In_181();
		}
	}

	private void Relay_SetMainDefault_184()
	{
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_184.SetMainDefault(logic_uScript_SetGrabDistance_dist_184);
	}

	private void Relay_SetDefault_184()
	{
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_184.SetDefault(logic_uScript_SetGrabDistance_dist_184);
	}

	private void Relay_Set_184()
	{
		logic_uScript_SetGrabDistance_uScript_SetGrabDistance_184.Set(logic_uScript_SetGrabDistance_dist_184);
	}

	private void Relay_In_185()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_185.In(logic_uScript_ChangeBuildingOptions_change_185, logic_uScript_ChangeBuildingOptions_allow_185);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_185.Out)
		{
			Relay_In_188();
		}
	}

	private void Relay_In_186()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_186.In(logic_uScript_ChangeBuildingOptions_change_186, logic_uScript_ChangeBuildingOptions_allow_186);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_186.Out)
		{
			Relay_In_185();
		}
	}

	private void Relay_In_187()
	{
		logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_187.In(logic_uScript_SetPlacementFilter_placementFilter_187);
		if (logic_uScript_SetPlacementFilter_uScript_SetPlacementFilter_187.Out)
		{
			Relay_In_186();
		}
	}

	private void Relay_In_188()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_188.In(logic_uScript_ChangeBuildingOptions_change_188, logic_uScript_ChangeBuildingOptions_allow_188);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_188.Out)
		{
			Relay_In_182();
		}
	}

	private void Relay_In_191()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_191 = local_msg_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_191.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_191, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_191);
	}

	private void Relay_OnCheckPointPassed_193()
	{
		Relay_In_164();
	}

	private void Relay_In_194()
	{
		logic_uScript_DisableTechRadialMenu_uScript_DisableTechRadialMenu_194.In(logic_uScript_DisableTechRadialMenu_disableRadialMenu_194);
		if (logic_uScript_DisableTechRadialMenu_uScript_DisableTechRadialMenu_194.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_381()
	{
		logic_uScript_DisableTechRadialMenu_uScript_DisableTechRadialMenu_381.In(logic_uScript_DisableTechRadialMenu_disableRadialMenu_381);
		if (logic_uScript_DisableTechRadialMenu_uScript_DisableTechRadialMenu_381.Out)
		{
			Relay_In_127();
		}
	}
}
