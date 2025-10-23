using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_GSO_2_1_UseTradingStation : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public BlockCategories blockCategoryToHighlight;

	public ItemTypeInfo blockToHighlight;

	public BlockTypes blockToPurchase = BlockTypes.GSOTractorMini_111;

	private ManHUD.HUDElementType local_109_ManHUD_HUDElementType = ManHUD.HUDElementType.MissionBoard;

	private ManOnScreenMessages.OnScreenMessage local_122_ManOnScreenMessages_OnScreenMessage;

	private int local_14_System_Int32;

	private ManOnScreenMessages.OnScreenMessage local_168_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_180_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_181_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_184_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_187_ManOnScreenMessages_OnScreenMessage;

	private TankBlock local_195_TankBlock;

	private TankBlock local_203_TankBlock;

	private TankBlock local_205_TankBlock;

	private GameHints.HintID local_232_GameHints_HintID = GameHints.HintID.TradingStationRestock;

	private TankBlock local_85_TankBlock;

	private int local_CurrentCount_System_Int32;

	private int local_CurrentSalesTotal_System_Int32;

	private Tank local_GSOVendor_Tank;

	private bool local_Init_System_Boolean;

	private int local_InitialSalesAmount_System_Int32;

	private bool local_InitSellingStage_System_Boolean;

	private TankBlock local_MissionBoardBlock_TankBlock;

	private bool local_Msg04SellResourcesShown_System_Boolean;

	private bool local_MsgEarnMoneyIntroShown_System_Boolean;

	private bool local_MsgResourcesSoldShown_System_Boolean;

	private TankBlock local_PurchasedBlock_TankBlock;

	private int local_SellingStage_System_Int32 = 3;

	private float local_ShopHideDistance_System_Single;

	private ManHUD.HUDElementType local_ShopHUD_ManHUD_HUDElementType = ManHUD.HUDElementType.BlockShop;

	private int local_Stage_System_Int32 = 1;

	private int local_TargetCount_System_Int32;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	public Vector3 missionBoardArrowOffset = new Vector3(0f, 0f, 0f);

	public uScript_AddMessage.MessageData msg01EarnMoneyIntro;

	public uScript_AddMessage.MessageData msg02aOpenShop;

	public uScript_AddMessage.MessageData msg02aOpenShop_Pad;

	public uScript_AddMessage.MessageData msg02bPurchaseBlock;

	public uScript_AddMessage.MessageData msg02bPurchaseBlock_Pad;

	public uScript_AddMessage.MessageData msg03AttachBlock;

	public uScript_AddMessage.MessageData msg04SellResources;

	public uScript_AddMessage.MessageData msg05ResourcesSold;

	public uScript_AddMessage.MessageData msg06OpenMissionBoard;

	public uScript_AddMessage.MessageData msg07Complete;

	public Vector3 shopArrowOffset = new Vector3(0f, 0f, 0f);

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_38;

	private GameObject owner_Connection_47;

	private GameObject owner_Connection_48;

	private GameObject owner_Connection_59;

	private GameObject owner_Connection_67;

	private GameObject owner_Connection_74;

	private GameObject owner_Connection_148;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_2 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_2;

	private object logic_uScript_SetEncounterTarget_visibleObject_2 = "";

	private bool logic_uScript_SetEncounterTarget_Out_2 = true;

	private uScript_FindNearestVendor logic_uScript_FindNearestVendor_uScript_FindNearestVendor_7 = new uScript_FindNearestVendor();

	private Tank logic_uScript_FindNearestVendor_Return_7;

	private bool logic_uScript_FindNearestVendor_Out_7 = true;

	private bool logic_uScript_FindNearestVendor_Returned_7 = true;

	private bool logic_uScript_FindNearestVendor_NotReturned_7 = true;

	private uScript_AddMoney logic_uScript_AddMoney_uScript_AddMoney_10 = new uScript_AddMoney();

	private int logic_uScript_AddMoney_amount_10;

	private bool logic_uScript_AddMoney_Out_10 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_11 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_11;

	private bool logic_uScriptAct_SetBool_Out_11 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_11 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_11 = true;

	private uScript_GetBlockPrice logic_uScript_GetBlockPrice_uScript_GetBlockPrice_12 = new uScript_GetBlockPrice();

	private BlockTypes logic_uScript_GetBlockPrice_blockType_12;

	private int logic_uScript_GetBlockPrice_Return_12;

	private bool logic_uScript_GetBlockPrice_Out_12 = true;

	private uScript_DiscoverBlock logic_uScript_DiscoverBlock_uScript_DiscoverBlock_16 = new uScript_DiscoverBlock();

	private BlockTypes logic_uScript_DiscoverBlock_blockType_16;

	private bool logic_uScript_DiscoverBlock_Out_16 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_18;

	private bool logic_uScriptCon_CompareBool_True_18 = true;

	private bool logic_uScriptCon_CompareBool_False_18 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_19;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_19 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_19 = "Init";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_21 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_21;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_21 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_21 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_23 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_23;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_25;

	private uScript_DoesPlayerHaveBlockRef logic_uScript_DoesPlayerHaveBlockRef_uScript_DoesPlayerHaveBlockRef_28 = new uScript_DoesPlayerHaveBlockRef();

	private TankBlock logic_uScript_DoesPlayerHaveBlockRef_block_28;

	private BlockTypes logic_uScript_DoesPlayerHaveBlockRef_type_28;

	private bool logic_uScript_DoesPlayerHaveBlockRef_useBlockType_28 = true;

	private bool logic_uScript_DoesPlayerHaveBlockRef_Out_28 = true;

	private bool logic_uScript_DoesPlayerHaveBlockRef_True_28 = true;

	private bool logic_uScript_DoesPlayerHaveBlockRef_False_28 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_30 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_30;

	private float logic_uScript_IsPlayerInRangeOfTech_range_30;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_30 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_30 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_30 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_30 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_36 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_36 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_37;

	private bool logic_uScriptCon_CompareBool_True_37 = true;

	private bool logic_uScriptCon_CompareBool_False_37 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_40 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_40;

	private bool logic_uScriptAct_SetBool_Out_40 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_40 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_40 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_41 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_41;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_41;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_41;

	private bool logic_uScript_SetQuestObjectiveCount_Out_41 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_44 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_44;

	private bool logic_uScript_FinishEncounter_Out_44 = true;

	private uScript_GetMoneyFromResourceSales logic_uScript_GetMoneyFromResourceSales_uScript_GetMoneyFromResourceSales_49 = new uScript_GetMoneyFromResourceSales();

	private ChunkTypes logic_uScript_GetMoneyFromResourceSales_resourceType_49;

	private int logic_uScript_GetMoneyFromResourceSales_Return_49;

	private bool logic_uScript_GetMoneyFromResourceSales_Out_49 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_50;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_50;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_53 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_53;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_53;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_53;

	private bool logic_uScript_AddMessage_Out_53 = true;

	private bool logic_uScript_AddMessage_Shown_53 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_55;

	private int logic_SubGraph_SaveLoadInt_integer_55;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_55 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_55 = "InitialSalesAmount";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_58;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_58 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_58 = "InitSellingStage";

	private uScriptAct_SubtractInt logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_61 = new uScriptAct_SubtractInt();

	private int logic_uScriptAct_SubtractInt_A_61;

	private int logic_uScriptAct_SubtractInt_B_61;

	private int logic_uScriptAct_SubtractInt_IntResult_61;

	private float logic_uScriptAct_SubtractInt_FloatResult_61;

	private bool logic_uScriptAct_SubtractInt_Out_61 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_62 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_62;

	private int logic_uScriptCon_CompareInt_B_62;

	private bool logic_uScriptCon_CompareInt_GreaterThan_62 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_62 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_62 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_62 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_62 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_62 = true;

	private uScript_GetQuestObjectiveTargetCount logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_64 = new uScript_GetQuestObjectiveTargetCount();

	private GameObject logic_uScript_GetQuestObjectiveTargetCount_owner_64;

	private int logic_uScript_GetQuestObjectiveTargetCount_objectiveId_64;

	private int logic_uScript_GetQuestObjectiveTargetCount_Return_64;

	private bool logic_uScript_GetQuestObjectiveTargetCount_Out_64 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_65 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_65;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_65;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_65;

	private bool logic_uScript_SetQuestObjectiveCount_Out_65 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_72 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_72;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_72;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_72;

	private bool logic_uScript_SetQuestObjectiveCount_Out_72 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_76;

	private int logic_SubGraph_SaveLoadInt_integer_76;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_76 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_76 = "CurrentCount";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_78 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_78;

	private bool logic_uScriptAct_SetBool_Out_78 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_78 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_78 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_80;

	private bool logic_uScriptCon_CompareBool_True_80 = true;

	private bool logic_uScriptCon_CompareBool_False_80 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_81 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_81;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_81;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_81;

	private bool logic_uScript_AddMessage_Out_81 = true;

	private bool logic_uScript_AddMessage_Shown_81 = true;

	private uScript_IsHUDElementVisible logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_87 = new uScript_IsHUDElementVisible();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementVisible_hudElement_87 = ManHUD.HUDElementType.BlockShop;

	private bool logic_uScript_IsHUDElementVisible_True_87 = true;

	private bool logic_uScript_IsHUDElementVisible_False_87 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_88;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_88;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_89 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_89;

	private BlockTypes logic_uScript_GetTankBlock_blockType_89 = BlockTypes.GSOVendor_Terminal_TEMP;

	private TankBlock logic_uScript_GetTankBlock_Return_89;

	private bool logic_uScript_GetTankBlock_Out_89 = true;

	private bool logic_uScript_GetTankBlock_Returned_89 = true;

	private bool logic_uScript_GetTankBlock_NotFound_89 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_91 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_91;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_91;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_91;

	private bool logic_uScript_AddMessage_Out_91 = true;

	private bool logic_uScript_AddMessage_Shown_91 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_94 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_94;

	private bool logic_uScriptAct_SetBool_Out_94 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_94 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_94 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_96 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_96;

	private bool logic_uScriptCon_CompareBool_True_96 = true;

	private bool logic_uScriptCon_CompareBool_False_96 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_98;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_98;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_99;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_99 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_99 = "MsgEarnMoneyIntroShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_100;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_100 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_100 = "MsgResourcesSoldShown";

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_103 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_103;

	private int logic_uScriptCon_CompareInt_B_103;

	private bool logic_uScriptCon_CompareInt_GreaterThan_103 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_103 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_103 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_103 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_103 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_103 = true;

	private uScript_IsHUDElementVisible logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_110 = new uScript_IsHUDElementVisible();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementVisible_hudElement_110;

	private bool logic_uScript_IsHUDElementVisible_True_110 = true;

	private bool logic_uScript_IsHUDElementVisible_False_110 = true;

	private uScript_GetShopHideDistance logic_uScript_GetShopHideDistance_uScript_GetShopHideDistance_112 = new uScript_GetShopHideDistance();

	private float logic_uScript_GetShopHideDistance_Return_112;

	private bool logic_uScript_GetShopHideDistance_Out_112 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_114 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_114;

	private bool logic_uScript_RemoveOnScreenMessage_instant_114;

	private bool logic_uScript_RemoveOnScreenMessage_Out_114 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_115 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_115;

	private bool logic_uScript_RemoveOnScreenMessage_instant_115;

	private bool logic_uScript_RemoveOnScreenMessage_Out_115 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_117 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_117;

	private float logic_uScript_IsPlayerInRangeOfTech_range_117;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_117 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_117 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_117 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_117 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_120 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_120;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_120;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_120;

	private bool logic_uScript_AddMessage_Out_120 = true;

	private bool logic_uScript_AddMessage_Shown_120 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_121 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_121;

	private bool logic_uScript_RemoveOnScreenMessage_instant_121;

	private bool logic_uScript_RemoveOnScreenMessage_Out_121 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_125 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_125;

	private float logic_uScript_IsPlayerInRangeOfTech_range_125;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_125 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_125 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_125 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_125 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_128 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_128;

	private float logic_uScript_IsPlayerInRangeOfTech_range_128;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_128 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_128 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_128 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_128 = true;

	private uScript_GridUIHighlightItem logic_uScript_GridUIHighlightItem_uScript_GridUIHighlightItem_131 = new uScript_GridUIHighlightItem();

	private ManHUD.HUDElementType logic_uScript_GridUIHighlightItem_hudElement_131;

	private ItemTypeInfo logic_uScript_GridUIHighlightItem_itemToHighlight_131;

	private bool logic_uScript_GridUIHighlightItem_Out_131 = true;

	private bool logic_uScript_GridUIHighlightItem_Waiting_131 = true;

	private bool logic_uScript_GridUIHighlightItem_Selected_131 = true;

	private uScript_GridUIHighlightBlockCategory logic_uScript_GridUIHighlightBlockCategory_uScript_GridUIHighlightBlockCategory_132 = new uScript_GridUIHighlightBlockCategory();

	private ManHUD.HUDElementType logic_uScript_GridUIHighlightBlockCategory_hudElement_132;

	private BlockCategories logic_uScript_GridUIHighlightBlockCategory_blockCategory_132;

	private bool logic_uScript_GridUIHighlightBlockCategory_Out_132 = true;

	private bool logic_uScript_GridUIHighlightBlockCategory_Waiting_132 = true;

	private bool logic_uScript_GridUIHighlightBlockCategory_Selected_132 = true;

	private uScript_GridUIHighlightConfirmButton logic_uScript_GridUIHighlightConfirmButton_uScript_GridUIHighlightConfirmButton_133 = new uScript_GridUIHighlightConfirmButton();

	private ManHUD.HUDElementType logic_uScript_GridUIHighlightConfirmButton_hudElement_133;

	private bool logic_uScript_GridUIHighlightConfirmButton_Out_133 = true;

	private bool logic_uScript_GridUIHighlightConfirmButton_Waiting_133 = true;

	private bool logic_uScript_GridUIHighlightConfirmButton_Selected_133 = true;

	private uScript_HideHUDElement logic_uScript_HideHUDElement_uScript_HideHUDElement_139 = new uScript_HideHUDElement();

	private ManHUD.HUDElementType logic_uScript_HideHUDElement_hudElement_139;

	private bool logic_uScript_HideHUDElement_Out_139 = true;

	private uScript_WaitForPurchasedBlock logic_uScript_WaitForPurchasedBlock_uScript_WaitForPurchasedBlock_141 = new uScript_WaitForPurchasedBlock();

	private BlockTypes logic_uScript_WaitForPurchasedBlock_blockType_141;

	private TankBlock logic_uScript_WaitForPurchasedBlock_Return_141;

	private bool logic_uScript_WaitForPurchasedBlock_Out_141 = true;

	private bool logic_uScript_WaitForPurchasedBlock_PurchaseFound_141 = true;

	private bool logic_uScript_WaitForPurchasedBlock_Waiting_141 = true;

	private uScript_PointArrowAtBlock logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_144 = new uScript_PointArrowAtBlock();

	private TankBlock logic_uScript_PointArrowAtBlock_block_144;

	private float logic_uScript_PointArrowAtBlock_timeToShowFor_144 = -1f;

	private Vector3 logic_uScript_PointArrowAtBlock_offset_144 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtBlock_Out_144 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_145 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_145 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_147 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_147;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_147 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_149 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_149 = true;

	private bool logic_uScript_LockPlayerInput_includeCamera_149 = true;

	private bool logic_uScript_LockPlayerInput_Out_149 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_150 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_150 = true;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_150 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_150 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_151 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_151;

	private bool logic_uScript_LockPlayerInput_includeCamera_151 = true;

	private bool logic_uScript_LockPlayerInput_Out_151 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_152 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_152;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_152 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_152 = true;

	private uScript_AddBlockToShopInventory logic_uScript_AddBlockToShopInventory_uScript_AddBlockToShopInventory_154 = new uScript_AddBlockToShopInventory();

	private BlockTypes logic_uScript_AddBlockToShopInventory_blockType_154;

	private bool logic_uScript_AddBlockToShopInventory_Out_154 = true;

	private uScript_ForceMissionBoardUpdate logic_uScript_ForceMissionBoardUpdate_uScript_ForceMissionBoardUpdate_156 = new uScript_ForceMissionBoardUpdate();

	private bool logic_uScript_ForceMissionBoardUpdate_Out_156 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_158 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_158;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_158;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_158;

	private bool logic_uScript_AddMessage_Out_158 = true;

	private bool logic_uScript_AddMessage_Shown_158 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_162 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_162;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_162;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_162;

	private bool logic_uScript_AddMessage_Out_162 = true;

	private bool logic_uScript_AddMessage_Shown_162 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_164 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_164;

	private float logic_uScript_IsPlayerInRangeOfTech_range_164;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_164 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_164 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_164 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_164 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_167 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_167;

	private bool logic_uScript_RemoveOnScreenMessage_instant_167;

	private bool logic_uScript_RemoveOnScreenMessage_Out_167 = true;

	private uScript_LockVisibleStackAccept logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_169 = new uScript_LockVisibleStackAccept();

	private object logic_uScript_LockVisibleStackAccept_targetObject_169 = "";

	private bool logic_uScript_LockVisibleStackAccept_Out_169 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_171 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_171;

	private int logic_uScriptCon_CompareInt_B_171 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_171 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_171 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_171 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_171 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_171 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_171 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_173 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_173 = true;

	private uScript_SetVendorsEnabled logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_174 = new uScript_SetVendorsEnabled();

	private bool logic_uScript_SetVendorsEnabled_enableShop_174 = true;

	private bool logic_uScript_SetVendorsEnabled_enableMissionBoard_174 = true;

	private bool logic_uScript_SetVendorsEnabled_enableSelling_174;

	private bool logic_uScript_SetVendorsEnabled_enableSCU_174;

	private bool logic_uScript_SetVendorsEnabled_enableCharging_174;

	private bool logic_uScript_SetVendorsEnabled_Out_174 = true;

	private uScript_SetVendorsEnabled logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_175 = new uScript_SetVendorsEnabled();

	private bool logic_uScript_SetVendorsEnabled_enableShop_175 = true;

	private bool logic_uScript_SetVendorsEnabled_enableMissionBoard_175 = true;

	private bool logic_uScript_SetVendorsEnabled_enableSelling_175 = true;

	private bool logic_uScript_SetVendorsEnabled_enableSCU_175;

	private bool logic_uScript_SetVendorsEnabled_enableCharging_175 = true;

	private bool logic_uScript_SetVendorsEnabled_Out_175 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_176 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_176;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_176;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_176;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_176;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_176;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_182 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_182;

	private bool logic_uScript_RemoveOnScreenMessage_instant_182;

	private bool logic_uScript_RemoveOnScreenMessage_Out_182 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_186 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_186;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_186;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_186;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_186;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_186;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_189 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_189;

	private bool logic_uScript_RemoveOnScreenMessage_instant_189;

	private bool logic_uScript_RemoveOnScreenMessage_Out_189 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_192 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_192 = "";

	private bool logic_uScript_EnableGlow_enable_192 = true;

	private bool logic_uScript_EnableGlow_Out_192 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_194 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_194 = "";

	private bool logic_uScript_EnableGlow_enable_194;

	private bool logic_uScript_EnableGlow_Out_194 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_196 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_196;

	private BlockTypes logic_uScript_GetTankBlock_blockType_196 = BlockTypes.GSOVendor_Terminal_TEMP;

	private TankBlock logic_uScript_GetTankBlock_Return_196;

	private bool logic_uScript_GetTankBlock_Out_196 = true;

	private bool logic_uScript_GetTankBlock_Returned_196 = true;

	private bool logic_uScript_GetTankBlock_NotFound_196 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_197 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_197 = "";

	private bool logic_uScript_EnableGlow_enable_197 = true;

	private bool logic_uScript_EnableGlow_Out_197 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_199 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_199 = "";

	private bool logic_uScript_EnableGlow_enable_199;

	private bool logic_uScript_EnableGlow_Out_199 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_201 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_201;

	private BlockTypes logic_uScript_GetTankBlock_blockType_201 = BlockTypes.GSOVendor_Terminal_TEMP;

	private TankBlock logic_uScript_GetTankBlock_Return_201;

	private bool logic_uScript_GetTankBlock_Out_201 = true;

	private bool logic_uScript_GetTankBlock_Returned_201 = true;

	private bool logic_uScript_GetTankBlock_NotFound_201 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_204 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_204 = "";

	private bool logic_uScript_EnableGlow_enable_204;

	private bool logic_uScript_EnableGlow_Out_204 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_206 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_206 = "";

	private bool logic_uScript_EnableGlow_enable_206;

	private bool logic_uScript_EnableGlow_Out_206 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_208 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_208;

	private BlockTypes logic_uScript_GetTankBlock_blockType_208 = BlockTypes.GSOVendor_DeliCannon_TEMP;

	private TankBlock logic_uScript_GetTankBlock_Return_208;

	private bool logic_uScript_GetTankBlock_Out_208 = true;

	private bool logic_uScript_GetTankBlock_Returned_208 = true;

	private bool logic_uScript_GetTankBlock_NotFound_208 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_211 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_211 = "";

	private bool logic_uScript_EnableGlow_enable_211;

	private bool logic_uScript_EnableGlow_Out_211 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_212 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_212;

	private BlockTypes logic_uScript_GetTankBlock_blockType_212 = BlockTypes.GSOVendor_MissionBoard_TEMP;

	private TankBlock logic_uScript_GetTankBlock_Return_212;

	private bool logic_uScript_GetTankBlock_Out_212 = true;

	private bool logic_uScript_GetTankBlock_Returned_212 = true;

	private bool logic_uScript_GetTankBlock_NotFound_212 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_214 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_214 = "";

	private bool logic_uScript_EnableGlow_enable_214 = true;

	private bool logic_uScript_EnableGlow_Out_214 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_215 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_215;

	private BlockTypes logic_uScript_GetTankBlock_blockType_215 = BlockTypes.GSOVendor_MissionBoard_TEMP;

	private TankBlock logic_uScript_GetTankBlock_Return_215;

	private bool logic_uScript_GetTankBlock_Out_215 = true;

	private bool logic_uScript_GetTankBlock_Returned_215 = true;

	private bool logic_uScript_GetTankBlock_NotFound_215 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_218 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_218 = "";

	private bool logic_uScript_EnableGlow_enable_218;

	private bool logic_uScript_EnableGlow_Out_218 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_219 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_219;

	private BlockTypes logic_uScript_GetTankBlock_blockType_219 = BlockTypes.GSOVendor_MissionBoard_TEMP;

	private TankBlock logic_uScript_GetTankBlock_Return_219;

	private bool logic_uScript_GetTankBlock_Out_219 = true;

	private bool logic_uScript_GetTankBlock_Returned_219 = true;

	private bool logic_uScript_GetTankBlock_NotFound_219 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_221 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_221 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_222 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_222 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_223 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_223 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_224 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_224 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_225;

	private bool logic_uScriptCon_CompareBool_True_225 = true;

	private bool logic_uScriptCon_CompareBool_False_225 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_227 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_227;

	private bool logic_uScriptAct_SetBool_Out_227 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_227 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_227 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_230 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_230;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_230;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_230;

	private bool logic_uScript_AddMessage_Out_230 = true;

	private bool logic_uScript_AddMessage_Shown_230 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_231 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_231;

	private bool logic_uScript_ShowHint_Out_231 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_233 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_233;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_233 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_233 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_235;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_235 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_235 = "Msg04SellResourcesShown";

	private uScript_PointArrowAtBlock logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_237 = new uScript_PointArrowAtBlock();

	private TankBlock logic_uScript_PointArrowAtBlock_block_237;

	private float logic_uScript_PointArrowAtBlock_timeToShowFor_237 = 0.1f;

	private Vector3 logic_uScript_PointArrowAtBlock_offset_237;

	private bool logic_uScript_PointArrowAtBlock_Out_237 = true;

	private uScript_PointArrowAtBlock logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_239 = new uScript_PointArrowAtBlock();

	private TankBlock logic_uScript_PointArrowAtBlock_block_239;

	private float logic_uScript_PointArrowAtBlock_timeToShowFor_239 = 0.1f;

	private Vector3 logic_uScript_PointArrowAtBlock_offset_239;

	private bool logic_uScript_PointArrowAtBlock_Out_239 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_242 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_242 = "tutorial_stage";

	private string logic_uScript_SendAnaliticsEvent_parameterName_242 = "stage_complete";

	private object logic_uScript_SendAnaliticsEvent_parameter_242 = "use_trading_station";

	private bool logic_uScript_SendAnaliticsEvent_Out_242 = true;

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_243 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_243;

	private bool logic_uScript_LockHudGroup_locked_243 = true;

	private bool logic_uScript_LockHudGroup_Out_243 = true;

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_244 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_244;

	private bool logic_uScript_LockHudGroup_locked_244;

	private bool logic_uScript_LockHudGroup_Out_244 = true;

	private uScript_TutorialFinished logic_uScript_TutorialFinished_uScript_TutorialFinished_526 = new uScript_TutorialFinished();

	private bool logic_uScript_TutorialFinished_Out_526 = true;

	private ChunkTypes event_UnityEngine_GameObject_ResourceType_45;

	private int event_UnityEngine_GameObject_ResourceTypeTotal_45;

	private int event_UnityEngine_GameObject_MoneyTotal_45;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
			if (null != owner_Connection_1)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_1.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_1.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_0;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_0;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_0;
				}
			}
		}
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
		}
		if (null == owner_Connection_5 || !m_RegisteredForEvents)
		{
			owner_Connection_5 = parentGameObject;
			if (null != owner_Connection_5)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_5.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_5.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_6;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_6;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_6;
				}
			}
		}
		if (null == owner_Connection_38 || !m_RegisteredForEvents)
		{
			owner_Connection_38 = parentGameObject;
		}
		if (null == owner_Connection_47 || !m_RegisteredForEvents)
		{
			owner_Connection_47 = parentGameObject;
			if (null != owner_Connection_47)
			{
				uScript_MoneyFromResourceSaleEvent uScript_MoneyFromResourceSaleEvent2 = owner_Connection_47.GetComponent<uScript_MoneyFromResourceSaleEvent>();
				if (null == uScript_MoneyFromResourceSaleEvent2)
				{
					uScript_MoneyFromResourceSaleEvent2 = owner_Connection_47.AddComponent<uScript_MoneyFromResourceSaleEvent>();
				}
				if (null != uScript_MoneyFromResourceSaleEvent2)
				{
					uScript_MoneyFromResourceSaleEvent2.MoneyFromResourceSaleEvent += Instance_MoneyFromResourceSaleEvent_45;
				}
			}
		}
		if (null == owner_Connection_48 || !m_RegisteredForEvents)
		{
			owner_Connection_48 = parentGameObject;
		}
		if (null == owner_Connection_59 || !m_RegisteredForEvents)
		{
			owner_Connection_59 = parentGameObject;
		}
		if (null == owner_Connection_67 || !m_RegisteredForEvents)
		{
			owner_Connection_67 = parentGameObject;
		}
		if (null == owner_Connection_74 || !m_RegisteredForEvents)
		{
			owner_Connection_74 = parentGameObject;
		}
		if (null == owner_Connection_148 || !m_RegisteredForEvents)
		{
			owner_Connection_148 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_1)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_1.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_1.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_0;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_0;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_0;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_5)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_5.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_5.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_6;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_6;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_6;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_47)
		{
			uScript_MoneyFromResourceSaleEvent uScript_MoneyFromResourceSaleEvent2 = owner_Connection_47.GetComponent<uScript_MoneyFromResourceSaleEvent>();
			if (null == uScript_MoneyFromResourceSaleEvent2)
			{
				uScript_MoneyFromResourceSaleEvent2 = owner_Connection_47.AddComponent<uScript_MoneyFromResourceSaleEvent>();
			}
			if (null != uScript_MoneyFromResourceSaleEvent2)
			{
				uScript_MoneyFromResourceSaleEvent2.MoneyFromResourceSaleEvent += Instance_MoneyFromResourceSaleEvent_45;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_1)
		{
			uScript_SaveLoad component = owner_Connection_1.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_0;
				component.LoadEvent -= Instance_LoadEvent_0;
				component.RestartEvent -= Instance_RestartEvent_0;
			}
		}
		if (null != owner_Connection_5)
		{
			uScript_EncounterUpdate component2 = owner_Connection_5.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_6;
				component2.OnSuspend -= Instance_OnSuspend_6;
				component2.OnResume -= Instance_OnResume_6;
			}
		}
		if (null != owner_Connection_47)
		{
			uScript_MoneyFromResourceSaleEvent component3 = owner_Connection_47.GetComponent<uScript_MoneyFromResourceSaleEvent>();
			if (null != component3)
			{
				component3.MoneyFromResourceSaleEvent -= Instance_MoneyFromResourceSaleEvent_45;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_2.SetParent(g);
		logic_uScript_FindNearestVendor_uScript_FindNearestVendor_7.SetParent(g);
		logic_uScript_AddMoney_uScript_AddMoney_10.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_11.SetParent(g);
		logic_uScript_GetBlockPrice_uScript_GetBlockPrice_12.SetParent(g);
		logic_uScript_DiscoverBlock_uScript_DiscoverBlock_16.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_23.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.SetParent(g);
		logic_uScript_DoesPlayerHaveBlockRef_uScript_DoesPlayerHaveBlockRef_28.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_30.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_36.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_41.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_44.SetParent(g);
		logic_uScript_GetMoneyFromResourceSales_uScript_GetMoneyFromResourceSales_49.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_53.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.SetParent(g);
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_61.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_62.SetParent(g);
		logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_64.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_65.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_72.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_78.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_81.SetParent(g);
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_87.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_89.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_91.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_96.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_103.SetParent(g);
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_110.SetParent(g);
		logic_uScript_GetShopHideDistance_uScript_GetShopHideDistance_112.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_114.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_115.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_117.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_120.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_121.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_125.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_128.SetParent(g);
		logic_uScript_GridUIHighlightItem_uScript_GridUIHighlightItem_131.SetParent(g);
		logic_uScript_GridUIHighlightBlockCategory_uScript_GridUIHighlightBlockCategory_132.SetParent(g);
		logic_uScript_GridUIHighlightConfirmButton_uScript_GridUIHighlightConfirmButton_133.SetParent(g);
		logic_uScript_HideHUDElement_uScript_HideHUDElement_139.SetParent(g);
		logic_uScript_WaitForPurchasedBlock_uScript_WaitForPurchasedBlock_141.SetParent(g);
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_144.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_145.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_147.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_149.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_150.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_151.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_152.SetParent(g);
		logic_uScript_AddBlockToShopInventory_uScript_AddBlockToShopInventory_154.SetParent(g);
		logic_uScript_ForceMissionBoardUpdate_uScript_ForceMissionBoardUpdate_156.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_158.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_162.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_164.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_167.SetParent(g);
		logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_169.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_171.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_173.SetParent(g);
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_174.SetParent(g);
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_175.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_176.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_182.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_186.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_189.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_192.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_194.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_196.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_197.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_199.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_201.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_204.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_206.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_208.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_211.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_212.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_214.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_215.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_218.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_219.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_221.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_222.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_223.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_224.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_227.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_230.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_231.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_233.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.SetParent(g);
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_237.SetParent(g);
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_239.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_242.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_243.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_244.SetParent(g);
		logic_uScript_TutorialFinished_uScript_TutorialFinished_526.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_5 = parentGameObject;
		owner_Connection_38 = parentGameObject;
		owner_Connection_47 = parentGameObject;
		owner_Connection_48 = parentGameObject;
		owner_Connection_59 = parentGameObject;
		owner_Connection_67 = parentGameObject;
		owner_Connection_74 = parentGameObject;
		owner_Connection_148 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_23.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_176.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_186.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.Save_Out += SubGraph_SaveLoadBool_Save_Out_19;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.Load_Out += SubGraph_SaveLoadBool_Load_Out_19;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_19;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21.Save_Out += SubGraph_SaveLoadInt_Save_Out_21;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21.Load_Out += SubGraph_SaveLoadInt_Load_Out_21;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_21;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_23.Out += SubGraph_LoadObjectiveStates_Out_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output1 += uScriptCon_ManualSwitch_Output1_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output2 += uScriptCon_ManualSwitch_Output2_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output3 += uScriptCon_ManualSwitch_Output3_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output4 += uScriptCon_ManualSwitch_Output4_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output5 += uScriptCon_ManualSwitch_Output5_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output6 += uScriptCon_ManualSwitch_Output6_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output7 += uScriptCon_ManualSwitch_Output7_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output8 += uScriptCon_ManualSwitch_Output8_25;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.Out += SubGraph_CompleteObjectiveStage_Out_50;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Save_Out += SubGraph_SaveLoadInt_Save_Out_55;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Load_Out += SubGraph_SaveLoadInt_Load_Out_55;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_55;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save_Out += SubGraph_SaveLoadBool_Save_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load_Out += SubGraph_SaveLoadBool_Load_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_58;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76.Save_Out += SubGraph_SaveLoadInt_Save_Out_76;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76.Load_Out += SubGraph_SaveLoadInt_Load_Out_76;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_76;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.Out += SubGraph_CompleteObjectiveStage_Out_88;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.Out += SubGraph_CompleteObjectiveStage_Out_98;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Save_Out += SubGraph_SaveLoadBool_Save_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Load_Out += SubGraph_SaveLoadBool_Load_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Save_Out += SubGraph_SaveLoadBool_Save_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Load_Out += SubGraph_SaveLoadBool_Load_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_100;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_176.Out += SubGraph_AddMessageWithPadSupport_Out_176;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_176.Shown += SubGraph_AddMessageWithPadSupport_Shown_176;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_186.Out += SubGraph_AddMessageWithPadSupport_Out_186;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_186.Shown += SubGraph_AddMessageWithPadSupport_Shown_186;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.Save_Out += SubGraph_SaveLoadBool_Save_Out_235;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.Load_Out += SubGraph_SaveLoadBool_Load_Out_235;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_235;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_23.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_176.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_186.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_23.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.OnEnable();
		logic_uScript_GridUIHighlightItem_uScript_GridUIHighlightItem_131.OnEnable();
		logic_uScript_GridUIHighlightBlockCategory_uScript_GridUIHighlightBlockCategory_132.OnEnable();
		logic_uScript_GridUIHighlightConfirmButton_uScript_GridUIHighlightConfirmButton_133.OnEnable();
		logic_uScript_WaitForPurchasedBlock_uScript_WaitForPurchasedBlock_141.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_147.OnEnable();
		logic_uScript_AddBlockToShopInventory_uScript_AddBlockToShopInventory_154.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_176.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_186.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_233.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_23.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_30.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_53.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_81.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_89.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_91.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_117.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_120.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_125.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_128.OnDisable();
		logic_uScript_GridUIHighlightBlockCategory_uScript_GridUIHighlightBlockCategory_132.OnDisable();
		logic_uScript_GridUIHighlightConfirmButton_uScript_GridUIHighlightConfirmButton_133.OnDisable();
		logic_uScript_WaitForPurchasedBlock_uScript_WaitForPurchasedBlock_141.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_158.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_162.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_164.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_176.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_186.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_196.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_201.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_208.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_212.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_215.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_219.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_230.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_23.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_176.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_186.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_23.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_176.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_186.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.Save_Out -= SubGraph_SaveLoadBool_Save_Out_19;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.Load_Out -= SubGraph_SaveLoadBool_Load_Out_19;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_19;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21.Save_Out -= SubGraph_SaveLoadInt_Save_Out_21;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21.Load_Out -= SubGraph_SaveLoadInt_Load_Out_21;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_21;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_23.Out -= SubGraph_LoadObjectiveStates_Out_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output1 -= uScriptCon_ManualSwitch_Output1_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output2 -= uScriptCon_ManualSwitch_Output2_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output3 -= uScriptCon_ManualSwitch_Output3_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output4 -= uScriptCon_ManualSwitch_Output4_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output5 -= uScriptCon_ManualSwitch_Output5_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output6 -= uScriptCon_ManualSwitch_Output6_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output7 -= uScriptCon_ManualSwitch_Output7_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output8 -= uScriptCon_ManualSwitch_Output8_25;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.Out -= SubGraph_CompleteObjectiveStage_Out_50;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Save_Out -= SubGraph_SaveLoadInt_Save_Out_55;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Load_Out -= SubGraph_SaveLoadInt_Load_Out_55;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_55;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save_Out -= SubGraph_SaveLoadBool_Save_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load_Out -= SubGraph_SaveLoadBool_Load_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_58;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76.Save_Out -= SubGraph_SaveLoadInt_Save_Out_76;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76.Load_Out -= SubGraph_SaveLoadInt_Load_Out_76;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_76;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.Out -= SubGraph_CompleteObjectiveStage_Out_88;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.Out -= SubGraph_CompleteObjectiveStage_Out_98;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Save_Out -= SubGraph_SaveLoadBool_Save_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Load_Out -= SubGraph_SaveLoadBool_Load_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Save_Out -= SubGraph_SaveLoadBool_Save_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Load_Out -= SubGraph_SaveLoadBool_Load_Out_100;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_100;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_176.Out -= SubGraph_AddMessageWithPadSupport_Out_176;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_176.Shown -= SubGraph_AddMessageWithPadSupport_Shown_176;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_186.Out -= SubGraph_AddMessageWithPadSupport_Out_186;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_186.Shown -= SubGraph_AddMessageWithPadSupport_Shown_186;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.Save_Out -= SubGraph_SaveLoadBool_Save_Out_235;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.Load_Out -= SubGraph_SaveLoadBool_Load_Out_235;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_235;
	}

	private void Instance_SaveEvent_0(object o, EventArgs e)
	{
		Relay_SaveEvent_0();
	}

	private void Instance_LoadEvent_0(object o, EventArgs e)
	{
		Relay_LoadEvent_0();
	}

	private void Instance_RestartEvent_0(object o, EventArgs e)
	{
		Relay_RestartEvent_0();
	}

	private void Instance_OnUpdate_6(object o, EventArgs e)
	{
		Relay_OnUpdate_6();
	}

	private void Instance_OnSuspend_6(object o, EventArgs e)
	{
		Relay_OnSuspend_6();
	}

	private void Instance_OnResume_6(object o, EventArgs e)
	{
		Relay_OnResume_6();
	}

	private void Instance_MoneyFromResourceSaleEvent_45(object o, uScript_MoneyFromResourceSaleEvent.MoneyFromResourceSaleEventArgs e)
	{
		event_UnityEngine_GameObject_ResourceType_45 = e.ResourceType;
		event_UnityEngine_GameObject_ResourceTypeTotal_45 = e.ResourceTypeTotal;
		event_UnityEngine_GameObject_MoneyTotal_45 = e.MoneyTotal;
		Relay_MoneyFromResourceSaleEvent_45();
	}

	private void SubGraph_SaveLoadBool_Save_Out_19(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_19 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_19;
		Relay_Save_Out_19();
	}

	private void SubGraph_SaveLoadBool_Load_Out_19(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_19 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_19;
		Relay_Load_Out_19();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_19(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_19 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_19;
		Relay_Restart_Out_19();
	}

	private void SubGraph_SaveLoadInt_Save_Out_21(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_21 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_21;
		Relay_Save_Out_21();
	}

	private void SubGraph_SaveLoadInt_Load_Out_21(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_21 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_21;
		Relay_Load_Out_21();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_21(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_21 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_21;
		Relay_Restart_Out_21();
	}

	private void SubGraph_LoadObjectiveStates_Out_23(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_23();
	}

	private void uScriptCon_ManualSwitch_Output1_25(object o, EventArgs e)
	{
		Relay_Output1_25();
	}

	private void uScriptCon_ManualSwitch_Output2_25(object o, EventArgs e)
	{
		Relay_Output2_25();
	}

	private void uScriptCon_ManualSwitch_Output3_25(object o, EventArgs e)
	{
		Relay_Output3_25();
	}

	private void uScriptCon_ManualSwitch_Output4_25(object o, EventArgs e)
	{
		Relay_Output4_25();
	}

	private void uScriptCon_ManualSwitch_Output5_25(object o, EventArgs e)
	{
		Relay_Output5_25();
	}

	private void uScriptCon_ManualSwitch_Output6_25(object o, EventArgs e)
	{
		Relay_Output6_25();
	}

	private void uScriptCon_ManualSwitch_Output7_25(object o, EventArgs e)
	{
		Relay_Output7_25();
	}

	private void uScriptCon_ManualSwitch_Output8_25(object o, EventArgs e)
	{
		Relay_Output8_25();
	}

	private void SubGraph_CompleteObjectiveStage_Out_50(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_50 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_50;
		Relay_Out_50();
	}

	private void SubGraph_SaveLoadInt_Save_Out_55(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_55 = e.integer;
		local_InitialSalesAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_55;
		Relay_Save_Out_55();
	}

	private void SubGraph_SaveLoadInt_Load_Out_55(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_55 = e.integer;
		local_InitialSalesAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_55;
		Relay_Load_Out_55();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_55(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_55 = e.integer;
		local_InitialSalesAmount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_55;
		Relay_Restart_Out_55();
	}

	private void SubGraph_SaveLoadBool_Save_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_InitSellingStage_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Save_Out_58();
	}

	private void SubGraph_SaveLoadBool_Load_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_InitSellingStage_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Load_Out_58();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_InitSellingStage_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Restart_Out_58();
	}

	private void SubGraph_SaveLoadInt_Save_Out_76(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_76 = e.integer;
		local_CurrentCount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_76;
		Relay_Save_Out_76();
	}

	private void SubGraph_SaveLoadInt_Load_Out_76(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_76 = e.integer;
		local_CurrentCount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_76;
		Relay_Load_Out_76();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_76(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_76 = e.integer;
		local_CurrentCount_System_Int32 = logic_SubGraph_SaveLoadInt_integer_76;
		Relay_Restart_Out_76();
	}

	private void SubGraph_CompleteObjectiveStage_Out_88(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_88 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_88;
		Relay_Out_88();
	}

	private void SubGraph_CompleteObjectiveStage_Out_98(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_98 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_98;
		Relay_Out_98();
	}

	private void SubGraph_SaveLoadBool_Save_Out_99(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = e.boolean;
		local_MsgEarnMoneyIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_99;
		Relay_Save_Out_99();
	}

	private void SubGraph_SaveLoadBool_Load_Out_99(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = e.boolean;
		local_MsgEarnMoneyIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_99;
		Relay_Load_Out_99();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_99(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = e.boolean;
		local_MsgEarnMoneyIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_99;
		Relay_Restart_Out_99();
	}

	private void SubGraph_SaveLoadBool_Save_Out_100(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = e.boolean;
		local_MsgResourcesSoldShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_100;
		Relay_Save_Out_100();
	}

	private void SubGraph_SaveLoadBool_Load_Out_100(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = e.boolean;
		local_MsgResourcesSoldShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_100;
		Relay_Load_Out_100();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_100(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = e.boolean;
		local_MsgResourcesSoldShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_100;
		Relay_Restart_Out_100();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_176(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_176 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_176 = e.messageControlPadReturn;
		local_180_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_176;
		local_181_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_176;
		Relay_Out_176();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_176(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_176 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_176 = e.messageControlPadReturn;
		local_180_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_176;
		local_181_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_176;
		Relay_Shown_176();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_186(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_186 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_186 = e.messageControlPadReturn;
		local_187_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_186;
		local_184_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_186;
		Relay_Out_186();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_186(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_186 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_186 = e.messageControlPadReturn;
		local_187_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_186;
		local_184_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_186;
		Relay_Shown_186();
	}

	private void SubGraph_SaveLoadBool_Save_Out_235(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_235 = e.boolean;
		local_Msg04SellResourcesShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_235;
		Relay_Save_Out_235();
	}

	private void SubGraph_SaveLoadBool_Load_Out_235(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_235 = e.boolean;
		local_Msg04SellResourcesShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_235;
		Relay_Load_Out_235();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_235(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_235 = e.boolean;
		local_Msg04SellResourcesShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_235;
		Relay_Restart_Out_235();
	}

	private void Relay_SaveEvent_0()
	{
		Relay_Save_19();
	}

	private void Relay_LoadEvent_0()
	{
		Relay_Load_19();
	}

	private void Relay_RestartEvent_0()
	{
		Relay_Set_False_19();
	}

	private void Relay_In_2()
	{
		logic_uScript_SetEncounterTarget_owner_2 = owner_Connection_3;
		logic_uScript_SetEncounterTarget_visibleObject_2 = local_GSOVendor_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_2.In(logic_uScript_SetEncounterTarget_owner_2, logic_uScript_SetEncounterTarget_visibleObject_2);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_2.Out)
		{
			Relay_In_147();
		}
	}

	private void Relay_OnUpdate_6()
	{
		Relay_In_18();
	}

	private void Relay_OnSuspend_6()
	{
	}

	private void Relay_OnResume_6()
	{
	}

	private void Relay_In_7()
	{
		logic_uScript_FindNearestVendor_Return_7 = logic_uScript_FindNearestVendor_uScript_FindNearestVendor_7.In();
		local_GSOVendor_Tank = logic_uScript_FindNearestVendor_Return_7;
		if (logic_uScript_FindNearestVendor_uScript_FindNearestVendor_7.Returned)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_10()
	{
		logic_uScript_AddMoney_amount_10 = local_14_System_Int32;
		logic_uScript_AddMoney_uScript_AddMoney_10.In(logic_uScript_AddMoney_amount_10);
		if (logic_uScript_AddMoney_uScript_AddMoney_10.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_True_11()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_11.True(out logic_uScriptAct_SetBool_Target_11);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_11;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_11.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_False_11()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_11.False(out logic_uScriptAct_SetBool_Target_11);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_11;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_11.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_12()
	{
		logic_uScript_GetBlockPrice_blockType_12 = blockToPurchase;
		logic_uScript_GetBlockPrice_Return_12 = logic_uScript_GetBlockPrice_uScript_GetBlockPrice_12.In(logic_uScript_GetBlockPrice_blockType_12);
		local_14_System_Int32 = logic_uScript_GetBlockPrice_Return_12;
		if (logic_uScript_GetBlockPrice_uScript_GetBlockPrice_12.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_16()
	{
		logic_uScript_DiscoverBlock_blockType_16 = blockToPurchase;
		logic_uScript_DiscoverBlock_uScript_DiscoverBlock_16.In(logic_uScript_DiscoverBlock_blockType_16);
		if (logic_uScript_DiscoverBlock_uScript_DiscoverBlock_16.Out)
		{
			Relay_In_171();
		}
	}

	private void Relay_In_18()
	{
		logic_uScriptCon_CompareBool_Bool_18 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.In(logic_uScriptCon_CompareBool_Bool_18);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_18.False;
		if (num)
		{
			Relay_In_171();
		}
		if (flag)
		{
			Relay_True_11();
		}
	}

	private void Relay_Save_Out_19()
	{
		Relay_Save_58();
	}

	private void Relay_Load_Out_19()
	{
		Relay_Load_58();
	}

	private void Relay_Restart_Out_19()
	{
		Relay_Set_False_58();
	}

	private void Relay_Save_19()
	{
		logic_SubGraph_SaveLoadBool_boolean_19 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_19 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.Save(ref logic_SubGraph_SaveLoadBool_boolean_19, logic_SubGraph_SaveLoadBool_boolAsVariable_19, logic_SubGraph_SaveLoadBool_uniqueID_19);
	}

	private void Relay_Load_19()
	{
		logic_SubGraph_SaveLoadBool_boolean_19 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_19 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.Load(ref logic_SubGraph_SaveLoadBool_boolean_19, logic_SubGraph_SaveLoadBool_boolAsVariable_19, logic_SubGraph_SaveLoadBool_uniqueID_19);
	}

	private void Relay_Set_True_19()
	{
		logic_SubGraph_SaveLoadBool_boolean_19 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_19 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_19, logic_SubGraph_SaveLoadBool_boolAsVariable_19, logic_SubGraph_SaveLoadBool_uniqueID_19);
	}

	private void Relay_Set_False_19()
	{
		logic_SubGraph_SaveLoadBool_boolean_19 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_19 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_19.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_19, logic_SubGraph_SaveLoadBool_boolAsVariable_19, logic_SubGraph_SaveLoadBool_uniqueID_19);
	}

	private void Relay_Save_Out_21()
	{
	}

	private void Relay_Load_Out_21()
	{
		Relay_In_23();
	}

	private void Relay_Restart_Out_21()
	{
	}

	private void Relay_Save_21()
	{
		logic_SubGraph_SaveLoadInt_integer_21 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_21 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21.Save(logic_SubGraph_SaveLoadInt_restartValue_21, ref logic_SubGraph_SaveLoadInt_integer_21, logic_SubGraph_SaveLoadInt_intAsVariable_21, logic_SubGraph_SaveLoadInt_uniqueID_21);
	}

	private void Relay_Load_21()
	{
		logic_SubGraph_SaveLoadInt_integer_21 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_21 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21.Load(logic_SubGraph_SaveLoadInt_restartValue_21, ref logic_SubGraph_SaveLoadInt_integer_21, logic_SubGraph_SaveLoadInt_intAsVariable_21, logic_SubGraph_SaveLoadInt_uniqueID_21);
	}

	private void Relay_Restart_21()
	{
		logic_SubGraph_SaveLoadInt_integer_21 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_21 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_21.Restart(logic_SubGraph_SaveLoadInt_restartValue_21, ref logic_SubGraph_SaveLoadInt_integer_21, logic_SubGraph_SaveLoadInt_intAsVariable_21, logic_SubGraph_SaveLoadInt_uniqueID_21);
	}

	private void Relay_Out_23()
	{
		Relay_SetCount_72();
	}

	private void Relay_In_23()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_23 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_23.In(logic_SubGraph_LoadObjectiveStates_currentObjective_23);
	}

	private void Relay_Output1_25()
	{
		Relay_In_80();
	}

	private void Relay_Output2_25()
	{
		Relay_In_28();
	}

	private void Relay_Output3_25()
	{
		Relay_In_225();
	}

	private void Relay_Output4_25()
	{
		Relay_In_96();
	}

	private void Relay_Output5_25()
	{
	}

	private void Relay_Output6_25()
	{
	}

	private void Relay_Output7_25()
	{
	}

	private void Relay_Output8_25()
	{
	}

	private void Relay_In_25()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_25 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.In(logic_uScriptCon_ManualSwitch_CurrentOutput_25);
	}

	private void Relay_In_28()
	{
		logic_uScript_DoesPlayerHaveBlockRef_type_28 = blockToPurchase;
		logic_uScript_DoesPlayerHaveBlockRef_uScript_DoesPlayerHaveBlockRef_28.In(logic_uScript_DoesPlayerHaveBlockRef_block_28, logic_uScript_DoesPlayerHaveBlockRef_type_28, logic_uScript_DoesPlayerHaveBlockRef_useBlockType_28);
		bool num = logic_uScript_DoesPlayerHaveBlockRef_uScript_DoesPlayerHaveBlockRef_28.True;
		bool flag = logic_uScript_DoesPlayerHaveBlockRef_uScript_DoesPlayerHaveBlockRef_28.False;
		if (num)
		{
			Relay_In_145();
		}
		if (flag)
		{
			Relay_In_117();
		}
	}

	private void Relay_In_30()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_30 = local_GSOVendor_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_30 = local_ShopHideDistance_System_Single;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_30.In(logic_uScript_IsPlayerInRangeOfTech_tech_30, logic_uScript_IsPlayerInRangeOfTech_range_30, logic_uScript_IsPlayerInRangeOfTech_techs_30);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_30.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_30.OutOfRange;
		if (inRange)
		{
			Relay_In_25();
		}
		if (outOfRange)
		{
			Relay_In_36();
		}
	}

	private void Relay_In_36()
	{
		logic_uScript_HideArrow_uScript_HideArrow_36.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_36.Out)
		{
			Relay_In_25();
		}
	}

	private void Relay_In_37()
	{
		logic_uScriptCon_CompareBool_Bool_37 = local_InitSellingStage_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.In(logic_uScriptCon_CompareBool_Bool_37);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.False)
		{
			Relay_True_40();
		}
	}

	private void Relay_True_40()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.True(out logic_uScriptAct_SetBool_Target_40);
		local_InitSellingStage_System_Boolean = logic_uScriptAct_SetBool_Target_40;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_40.Out)
		{
			Relay_In_175();
		}
	}

	private void Relay_False_40()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.False(out logic_uScriptAct_SetBool_Target_40);
		local_InitSellingStage_System_Boolean = logic_uScriptAct_SetBool_Target_40;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_40.Out)
		{
			Relay_In_175();
		}
	}

	private void Relay_SetCount_41()
	{
		logic_uScript_SetQuestObjectiveCount_owner_41 = owner_Connection_48;
		logic_uScript_SetQuestObjectiveCount_objectiveId_41 = local_SellingStage_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_41.SetCount(logic_uScript_SetQuestObjectiveCount_owner_41, logic_uScript_SetQuestObjectiveCount_objectiveId_41, logic_uScript_SetQuestObjectiveCount_currentCount_41);
	}

	private void Relay_Succeed_44()
	{
		logic_uScript_FinishEncounter_owner_44 = owner_Connection_38;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_44.Succeed(logic_uScript_FinishEncounter_owner_44);
		if (logic_uScript_FinishEncounter_uScript_FinishEncounter_44.Out)
		{
			Relay_In_156();
		}
	}

	private void Relay_Fail_44()
	{
		logic_uScript_FinishEncounter_owner_44 = owner_Connection_38;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_44.Fail(logic_uScript_FinishEncounter_owner_44);
		if (logic_uScript_FinishEncounter_uScript_FinishEncounter_44.Out)
		{
			Relay_In_156();
		}
	}

	private void Relay_MoneyFromResourceSaleEvent_45()
	{
		local_CurrentSalesTotal_System_Int32 = event_UnityEngine_GameObject_MoneyTotal_45;
		Relay_In_103();
	}

	private void Relay_AllResources_49()
	{
		logic_uScript_GetMoneyFromResourceSales_Return_49 = logic_uScript_GetMoneyFromResourceSales_uScript_GetMoneyFromResourceSales_49.AllResources(logic_uScript_GetMoneyFromResourceSales_resourceType_49);
		local_InitialSalesAmount_System_Int32 = logic_uScript_GetMoneyFromResourceSales_Return_49;
		if (logic_uScript_GetMoneyFromResourceSales_uScript_GetMoneyFromResourceSales_49.Out)
		{
			Relay_In_53();
		}
	}

	private void Relay_ResourcesOfType_49()
	{
		logic_uScript_GetMoneyFromResourceSales_Return_49 = logic_uScript_GetMoneyFromResourceSales_uScript_GetMoneyFromResourceSales_49.ResourcesOfType(logic_uScript_GetMoneyFromResourceSales_resourceType_49);
		local_InitialSalesAmount_System_Int32 = logic_uScript_GetMoneyFromResourceSales_Return_49;
		if (logic_uScript_GetMoneyFromResourceSales_uScript_GetMoneyFromResourceSales_49.Out)
		{
			Relay_In_53();
		}
	}

	private void Relay_Out_50()
	{
	}

	private void Relay_In_50()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_50 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_50.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_50, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_50);
	}

	private void Relay_In_53()
	{
		logic_uScript_AddMessage_messageData_53 = msg04SellResources;
		logic_uScript_AddMessage_speaker_53 = messageSpeaker;
		logic_uScript_AddMessage_Return_53 = logic_uScript_AddMessage_uScript_AddMessage_53.In(logic_uScript_AddMessage_messageData_53, logic_uScript_AddMessage_speaker_53);
		if (logic_uScript_AddMessage_uScript_AddMessage_53.Out)
		{
			Relay_SetCount_41();
		}
	}

	private void Relay_Save_Out_55()
	{
		Relay_Save_76();
	}

	private void Relay_Load_Out_55()
	{
		Relay_Load_76();
	}

	private void Relay_Restart_Out_55()
	{
		Relay_Restart_76();
	}

	private void Relay_Save_55()
	{
		logic_SubGraph_SaveLoadInt_integer_55 = local_InitialSalesAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_55 = local_InitialSalesAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Save(logic_SubGraph_SaveLoadInt_restartValue_55, ref logic_SubGraph_SaveLoadInt_integer_55, logic_SubGraph_SaveLoadInt_intAsVariable_55, logic_SubGraph_SaveLoadInt_uniqueID_55);
	}

	private void Relay_Load_55()
	{
		logic_SubGraph_SaveLoadInt_integer_55 = local_InitialSalesAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_55 = local_InitialSalesAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Load(logic_SubGraph_SaveLoadInt_restartValue_55, ref logic_SubGraph_SaveLoadInt_integer_55, logic_SubGraph_SaveLoadInt_intAsVariable_55, logic_SubGraph_SaveLoadInt_uniqueID_55);
	}

	private void Relay_Restart_55()
	{
		logic_SubGraph_SaveLoadInt_integer_55 = local_InitialSalesAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_55 = local_InitialSalesAmount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Restart(logic_SubGraph_SaveLoadInt_restartValue_55, ref logic_SubGraph_SaveLoadInt_integer_55, logic_SubGraph_SaveLoadInt_intAsVariable_55, logic_SubGraph_SaveLoadInt_uniqueID_55);
	}

	private void Relay_Save_Out_58()
	{
		Relay_Save_235();
	}

	private void Relay_Load_Out_58()
	{
		Relay_Load_235();
	}

	private void Relay_Restart_Out_58()
	{
		Relay_Set_False_235();
	}

	private void Relay_Save_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_InitSellingStage_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_InitSellingStage_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Load_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_InitSellingStage_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_InitSellingStage_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Set_True_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_InitSellingStage_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_InitSellingStage_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Set_False_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_InitSellingStage_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_InitSellingStage_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_In_61()
	{
		logic_uScriptAct_SubtractInt_A_61 = local_CurrentSalesTotal_System_Int32;
		logic_uScriptAct_SubtractInt_B_61 = local_InitialSalesAmount_System_Int32;
		logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_61.In(logic_uScriptAct_SubtractInt_A_61, logic_uScriptAct_SubtractInt_B_61, out logic_uScriptAct_SubtractInt_IntResult_61, out logic_uScriptAct_SubtractInt_FloatResult_61);
		local_CurrentCount_System_Int32 = logic_uScriptAct_SubtractInt_IntResult_61;
		if (logic_uScriptAct_SubtractInt_uScriptAct_SubtractInt_61.Out)
		{
			Relay_SetCount_65();
		}
	}

	private void Relay_In_62()
	{
		logic_uScriptCon_CompareInt_A_62 = local_CurrentCount_System_Int32;
		logic_uScriptCon_CompareInt_B_62 = local_TargetCount_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_62.In(logic_uScriptCon_CompareInt_A_62, logic_uScriptCon_CompareInt_B_62);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_62.GreaterThanOrEqualTo)
		{
			Relay_In_98();
		}
	}

	private void Relay_GetTargetCount_64()
	{
		logic_uScript_GetQuestObjectiveTargetCount_owner_64 = owner_Connection_67;
		logic_uScript_GetQuestObjectiveTargetCount_objectiveId_64 = local_SellingStage_System_Int32;
		logic_uScript_GetQuestObjectiveTargetCount_Return_64 = logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_64.GetTargetCount(logic_uScript_GetQuestObjectiveTargetCount_owner_64, logic_uScript_GetQuestObjectiveTargetCount_objectiveId_64);
		local_TargetCount_System_Int32 = logic_uScript_GetQuestObjectiveTargetCount_Return_64;
		if (logic_uScript_GetQuestObjectiveTargetCount_uScript_GetQuestObjectiveTargetCount_64.Out)
		{
			Relay_In_62();
		}
	}

	private void Relay_SetCount_65()
	{
		logic_uScript_SetQuestObjectiveCount_owner_65 = owner_Connection_59;
		logic_uScript_SetQuestObjectiveCount_objectiveId_65 = local_SellingStage_System_Int32;
		logic_uScript_SetQuestObjectiveCount_currentCount_65 = local_CurrentCount_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_65.SetCount(logic_uScript_SetQuestObjectiveCount_owner_65, logic_uScript_SetQuestObjectiveCount_objectiveId_65, logic_uScript_SetQuestObjectiveCount_currentCount_65);
		if (logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_65.Out)
		{
			Relay_GetTargetCount_64();
		}
	}

	private void Relay_SetCount_72()
	{
		logic_uScript_SetQuestObjectiveCount_owner_72 = owner_Connection_74;
		logic_uScript_SetQuestObjectiveCount_objectiveId_72 = local_SellingStage_System_Int32;
		logic_uScript_SetQuestObjectiveCount_currentCount_72 = local_CurrentCount_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_72.SetCount(logic_uScript_SetQuestObjectiveCount_owner_72, logic_uScript_SetQuestObjectiveCount_objectiveId_72, logic_uScript_SetQuestObjectiveCount_currentCount_72);
	}

	private void Relay_Save_Out_76()
	{
		Relay_Save_21();
	}

	private void Relay_Load_Out_76()
	{
		Relay_Load_21();
	}

	private void Relay_Restart_Out_76()
	{
		Relay_Restart_21();
	}

	private void Relay_Save_76()
	{
		logic_SubGraph_SaveLoadInt_integer_76 = local_CurrentCount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_76 = local_CurrentCount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76.Save(logic_SubGraph_SaveLoadInt_restartValue_76, ref logic_SubGraph_SaveLoadInt_integer_76, logic_SubGraph_SaveLoadInt_intAsVariable_76, logic_SubGraph_SaveLoadInt_uniqueID_76);
	}

	private void Relay_Load_76()
	{
		logic_SubGraph_SaveLoadInt_integer_76 = local_CurrentCount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_76 = local_CurrentCount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76.Load(logic_SubGraph_SaveLoadInt_restartValue_76, ref logic_SubGraph_SaveLoadInt_integer_76, logic_SubGraph_SaveLoadInt_intAsVariable_76, logic_SubGraph_SaveLoadInt_uniqueID_76);
	}

	private void Relay_Restart_76()
	{
		logic_SubGraph_SaveLoadInt_integer_76 = local_CurrentCount_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_76 = local_CurrentCount_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_76.Restart(logic_SubGraph_SaveLoadInt_restartValue_76, ref logic_SubGraph_SaveLoadInt_integer_76, logic_SubGraph_SaveLoadInt_intAsVariable_76, logic_SubGraph_SaveLoadInt_uniqueID_76);
	}

	private void Relay_True_78()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_78.True(out logic_uScriptAct_SetBool_Target_78);
		local_MsgEarnMoneyIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_78;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_78.Out)
		{
			Relay_In_141();
		}
	}

	private void Relay_False_78()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_78.False(out logic_uScriptAct_SetBool_Target_78);
		local_MsgEarnMoneyIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_78;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_78.Out)
		{
			Relay_In_141();
		}
	}

	private void Relay_In_80()
	{
		logic_uScriptCon_CompareBool_Bool_80 = local_MsgEarnMoneyIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.In(logic_uScriptCon_CompareBool_Bool_80);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.False;
		if (num)
		{
			Relay_In_141();
		}
		if (flag)
		{
			Relay_In_81();
		}
	}

	private void Relay_In_81()
	{
		logic_uScript_AddMessage_messageData_81 = msg01EarnMoneyIntro;
		logic_uScript_AddMessage_speaker_81 = messageSpeaker;
		logic_uScript_AddMessage_Return_81 = logic_uScript_AddMessage_uScript_AddMessage_81.In(logic_uScript_AddMessage_messageData_81, logic_uScript_AddMessage_speaker_81);
		if (logic_uScript_AddMessage_uScript_AddMessage_81.Shown)
		{
			Relay_In_174();
		}
	}

	private void Relay_In_87()
	{
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_87.In(logic_uScript_IsHUDElementVisible_hudElement_87);
		bool num = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_87.True;
		bool flag = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_87.False;
		if (num)
		{
			Relay_In_128();
		}
		if (flag)
		{
			Relay_In_125();
		}
	}

	private void Relay_Out_88()
	{
	}

	private void Relay_In_88()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_88 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_88, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_88);
	}

	private void Relay_In_89()
	{
		logic_uScript_GetTankBlock_tank_89 = local_GSOVendor_Tank;
		logic_uScript_GetTankBlock_Return_89 = logic_uScript_GetTankBlock_uScript_GetTankBlock_89.In(logic_uScript_GetTankBlock_tank_89, logic_uScript_GetTankBlock_blockType_89);
		local_85_TankBlock = logic_uScript_GetTankBlock_Return_89;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_89.Returned)
		{
			Relay_In_192();
		}
	}

	private void Relay_In_91()
	{
		logic_uScript_AddMessage_messageData_91 = msg05ResourcesSold;
		logic_uScript_AddMessage_speaker_91 = messageSpeaker;
		logic_uScript_AddMessage_Return_91 = logic_uScript_AddMessage_uScript_AddMessage_91.In(logic_uScript_AddMessage_messageData_91, logic_uScript_AddMessage_speaker_91);
		if (logic_uScript_AddMessage_uScript_AddMessage_91.Shown)
		{
			Relay_True_94();
		}
	}

	private void Relay_True_94()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.True(out logic_uScriptAct_SetBool_Target_94);
		local_MsgResourcesSoldShown_System_Boolean = logic_uScriptAct_SetBool_Target_94;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_94.Out)
		{
			Relay_In_221();
		}
	}

	private void Relay_False_94()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.False(out logic_uScriptAct_SetBool_Target_94);
		local_MsgResourcesSoldShown_System_Boolean = logic_uScriptAct_SetBool_Target_94;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_94.Out)
		{
			Relay_In_221();
		}
	}

	private void Relay_In_96()
	{
		logic_uScriptCon_CompareBool_Bool_96 = local_MsgResourcesSoldShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_96.In(logic_uScriptCon_CompareBool_Bool_96);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_96.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_96.False;
		if (num)
		{
			Relay_In_164();
		}
		if (flag)
		{
			Relay_In_91();
		}
	}

	private void Relay_Out_98()
	{
		Relay_In_208();
	}

	private void Relay_In_98()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_98 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_98, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_98);
	}

	private void Relay_Save_Out_99()
	{
		Relay_Save_100();
	}

	private void Relay_Load_Out_99()
	{
		Relay_Load_100();
	}

	private void Relay_Restart_Out_99()
	{
		Relay_Set_False_100();
	}

	private void Relay_Save_99()
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = local_MsgEarnMoneyIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_99 = local_MsgEarnMoneyIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Save(ref logic_SubGraph_SaveLoadBool_boolean_99, logic_SubGraph_SaveLoadBool_boolAsVariable_99, logic_SubGraph_SaveLoadBool_uniqueID_99);
	}

	private void Relay_Load_99()
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = local_MsgEarnMoneyIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_99 = local_MsgEarnMoneyIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Load(ref logic_SubGraph_SaveLoadBool_boolean_99, logic_SubGraph_SaveLoadBool_boolAsVariable_99, logic_SubGraph_SaveLoadBool_uniqueID_99);
	}

	private void Relay_Set_True_99()
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = local_MsgEarnMoneyIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_99 = local_MsgEarnMoneyIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_99, logic_SubGraph_SaveLoadBool_boolAsVariable_99, logic_SubGraph_SaveLoadBool_uniqueID_99);
	}

	private void Relay_Set_False_99()
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = local_MsgEarnMoneyIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_99 = local_MsgEarnMoneyIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_99, logic_SubGraph_SaveLoadBool_boolAsVariable_99, logic_SubGraph_SaveLoadBool_uniqueID_99);
	}

	private void Relay_Save_Out_100()
	{
		Relay_Save_55();
	}

	private void Relay_Load_Out_100()
	{
		Relay_Load_55();
	}

	private void Relay_Restart_Out_100()
	{
		Relay_Restart_55();
	}

	private void Relay_Save_100()
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = local_MsgResourcesSoldShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_100 = local_MsgResourcesSoldShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Save(ref logic_SubGraph_SaveLoadBool_boolean_100, logic_SubGraph_SaveLoadBool_boolAsVariable_100, logic_SubGraph_SaveLoadBool_uniqueID_100);
	}

	private void Relay_Load_100()
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = local_MsgResourcesSoldShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_100 = local_MsgResourcesSoldShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Load(ref logic_SubGraph_SaveLoadBool_boolean_100, logic_SubGraph_SaveLoadBool_boolAsVariable_100, logic_SubGraph_SaveLoadBool_uniqueID_100);
	}

	private void Relay_Set_True_100()
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = local_MsgResourcesSoldShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_100 = local_MsgResourcesSoldShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_100, logic_SubGraph_SaveLoadBool_boolAsVariable_100, logic_SubGraph_SaveLoadBool_uniqueID_100);
	}

	private void Relay_Set_False_100()
	{
		logic_SubGraph_SaveLoadBool_boolean_100 = local_MsgResourcesSoldShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_100 = local_MsgResourcesSoldShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_100.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_100, logic_SubGraph_SaveLoadBool_boolAsVariable_100, logic_SubGraph_SaveLoadBool_uniqueID_100);
	}

	private void Relay_In_103()
	{
		logic_uScriptCon_CompareInt_A_103 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_B_103 = local_SellingStage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_103.In(logic_uScriptCon_CompareInt_A_103, logic_uScriptCon_CompareInt_B_103);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_103.EqualTo)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_110()
	{
		logic_uScript_IsHUDElementVisible_hudElement_110 = local_109_ManHUD_HUDElementType;
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_110.In(logic_uScript_IsHUDElementVisible_hudElement_110);
		if (logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_110.True)
		{
			Relay_In_242();
		}
	}

	private void Relay_In_112()
	{
		logic_uScript_GetShopHideDistance_Return_112 = logic_uScript_GetShopHideDistance_uScript_GetShopHideDistance_112.In();
		local_ShopHideDistance_System_Single = logic_uScript_GetShopHideDistance_Return_112;
		if (logic_uScript_GetShopHideDistance_uScript_GetShopHideDistance_112.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_114()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_114 = local_180_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_114.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_114, logic_uScript_RemoveOnScreenMessage_instant_114);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_114.Out)
		{
			Relay_In_182();
		}
	}

	private void Relay_In_115()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_115 = local_187_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_115.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_115, logic_uScript_RemoveOnScreenMessage_instant_115);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_115.Out)
		{
			Relay_In_189();
		}
	}

	private void Relay_In_117()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_117 = local_GSOVendor_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_117 = local_ShopHideDistance_System_Single;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_117.In(logic_uScript_IsPlayerInRangeOfTech_tech_117, logic_uScript_IsPlayerInRangeOfTech_range_117, logic_uScript_IsPlayerInRangeOfTech_techs_117);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_117.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_117.OutOfRange;
		if (inRange)
		{
			Relay_In_120();
		}
		if (outOfRange)
		{
			Relay_In_121();
		}
	}

	private void Relay_In_120()
	{
		logic_uScript_AddMessage_messageData_120 = msg03AttachBlock;
		logic_uScript_AddMessage_speaker_120 = messageSpeaker;
		logic_uScript_AddMessage_Return_120 = logic_uScript_AddMessage_uScript_AddMessage_120.In(logic_uScript_AddMessage_messageData_120, logic_uScript_AddMessage_speaker_120);
		local_122_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_120;
	}

	private void Relay_In_121()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_121 = local_122_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_121.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_121, logic_uScript_RemoveOnScreenMessage_instant_121);
	}

	private void Relay_In_125()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_125 = local_GSOVendor_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_125 = local_ShopHideDistance_System_Single;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_125.In(logic_uScript_IsPlayerInRangeOfTech_tech_125, logic_uScript_IsPlayerInRangeOfTech_range_125, logic_uScript_IsPlayerInRangeOfTech_techs_125);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_125.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_125.OutOfRange;
		if (inRange)
		{
			Relay_In_186();
		}
		if (outOfRange)
		{
			Relay_In_115();
		}
	}

	private void Relay_In_128()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_128 = local_GSOVendor_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_128 = local_ShopHideDistance_System_Single;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_128.In(logic_uScript_IsPlayerInRangeOfTech_tech_128, logic_uScript_IsPlayerInRangeOfTech_range_128, logic_uScript_IsPlayerInRangeOfTech_techs_128);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_128.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_128.OutOfRange;
		if (inRange)
		{
			Relay_In_176();
		}
		if (outOfRange)
		{
			Relay_In_114();
		}
	}

	private void Relay_In_131()
	{
		logic_uScript_GridUIHighlightItem_hudElement_131 = local_ShopHUD_ManHUD_HUDElementType;
		logic_uScript_GridUIHighlightItem_itemToHighlight_131 = blockToHighlight;
		logic_uScript_GridUIHighlightItem_uScript_GridUIHighlightItem_131.In(logic_uScript_GridUIHighlightItem_hudElement_131, logic_uScript_GridUIHighlightItem_itemToHighlight_131);
		if (logic_uScript_GridUIHighlightItem_uScript_GridUIHighlightItem_131.Selected)
		{
			Relay_In_133();
		}
	}

	private void Relay_AllCategory_132()
	{
		logic_uScript_GridUIHighlightBlockCategory_hudElement_132 = local_ShopHUD_ManHUD_HUDElementType;
		logic_uScript_GridUIHighlightBlockCategory_blockCategory_132 = blockCategoryToHighlight;
		logic_uScript_GridUIHighlightBlockCategory_uScript_GridUIHighlightBlockCategory_132.AllCategory(logic_uScript_GridUIHighlightBlockCategory_hudElement_132, logic_uScript_GridUIHighlightBlockCategory_blockCategory_132);
		if (logic_uScript_GridUIHighlightBlockCategory_uScript_GridUIHighlightBlockCategory_132.Selected)
		{
			Relay_In_131();
		}
	}

	private void Relay_Category_132()
	{
		logic_uScript_GridUIHighlightBlockCategory_hudElement_132 = local_ShopHUD_ManHUD_HUDElementType;
		logic_uScript_GridUIHighlightBlockCategory_blockCategory_132 = blockCategoryToHighlight;
		logic_uScript_GridUIHighlightBlockCategory_uScript_GridUIHighlightBlockCategory_132.Category(logic_uScript_GridUIHighlightBlockCategory_hudElement_132, logic_uScript_GridUIHighlightBlockCategory_blockCategory_132);
		if (logic_uScript_GridUIHighlightBlockCategory_uScript_GridUIHighlightBlockCategory_132.Selected)
		{
			Relay_In_131();
		}
	}

	private void Relay_In_133()
	{
		logic_uScript_GridUIHighlightConfirmButton_hudElement_133 = local_ShopHUD_ManHUD_HUDElementType;
		logic_uScript_GridUIHighlightConfirmButton_uScript_GridUIHighlightConfirmButton_133.In(logic_uScript_GridUIHighlightConfirmButton_hudElement_133);
	}

	private void Relay_In_139()
	{
		logic_uScript_HideHUDElement_hudElement_139 = local_ShopHUD_ManHUD_HUDElementType;
		logic_uScript_HideHUDElement_uScript_HideHUDElement_139.In(logic_uScript_HideHUDElement_hudElement_139);
		if (logic_uScript_HideHUDElement_uScript_HideHUDElement_139.Out)
		{
			Relay_In_152();
		}
	}

	private void Relay_In_141()
	{
		logic_uScript_WaitForPurchasedBlock_blockType_141 = blockToPurchase;
		logic_uScript_WaitForPurchasedBlock_Return_141 = logic_uScript_WaitForPurchasedBlock_uScript_WaitForPurchasedBlock_141.In(logic_uScript_WaitForPurchasedBlock_blockType_141);
		local_PurchasedBlock_TankBlock = logic_uScript_WaitForPurchasedBlock_Return_141;
		bool purchaseFound = logic_uScript_WaitForPurchasedBlock_uScript_WaitForPurchasedBlock_141.PurchaseFound;
		bool waiting = logic_uScript_WaitForPurchasedBlock_uScript_WaitForPurchasedBlock_141.Waiting;
		if (purchaseFound)
		{
			Relay_In_144();
		}
		if (waiting)
		{
			Relay_In_87();
		}
	}

	private void Relay_In_144()
	{
		logic_uScript_PointArrowAtBlock_block_144 = local_PurchasedBlock_TankBlock;
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_144.In(logic_uScript_PointArrowAtBlock_block_144, logic_uScript_PointArrowAtBlock_timeToShowFor_144, logic_uScript_PointArrowAtBlock_offset_144);
		if (logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_144.Out)
		{
			Relay_In_197();
		}
	}

	private void Relay_In_145()
	{
		logic_uScript_HideArrow_uScript_HideArrow_145.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_145.Out)
		{
			Relay_In_199();
		}
	}

	private void Relay_In_147()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_147 = owner_Connection_148;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_147.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_147);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_147.Out)
		{
			Relay_In_112();
		}
	}

	private void Relay_In_149()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_149.In(logic_uScript_LockPlayerInput_lockInput_149, logic_uScript_LockPlayerInput_includeCamera_149);
		if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_149.Out)
		{
			Relay_In_196();
		}
	}

	private void Relay_In_150()
	{
		logic_uScript_LockPause_uScript_LockPause_150.In(logic_uScript_LockPause_lockPause_150, logic_uScript_LockPause_disabledReason_150);
		if (logic_uScript_LockPause_uScript_LockPause_150.Out)
		{
			Relay_In_243();
		}
	}

	private void Relay_In_151()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_151.In(logic_uScript_LockPlayerInput_lockInput_151, logic_uScript_LockPlayerInput_includeCamera_151);
		if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_151.Out)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_152()
	{
		logic_uScript_LockPause_uScript_LockPause_152.In(logic_uScript_LockPause_lockPause_152, logic_uScript_LockPause_disabledReason_152);
		if (logic_uScript_LockPause_uScript_LockPause_152.Out)
		{
			Relay_In_244();
		}
	}

	private void Relay_In_154()
	{
		logic_uScript_AddBlockToShopInventory_blockType_154 = blockToPurchase;
		logic_uScript_AddBlockToShopInventory_uScript_AddBlockToShopInventory_154.In(logic_uScript_AddBlockToShopInventory_blockType_154);
		if (logic_uScript_AddBlockToShopInventory_uScript_AddBlockToShopInventory_154.Out)
		{
			Relay_Category_132();
		}
	}

	private void Relay_In_156()
	{
		logic_uScript_ForceMissionBoardUpdate_uScript_ForceMissionBoardUpdate_156.In();
		if (logic_uScript_ForceMissionBoardUpdate_uScript_ForceMissionBoardUpdate_156.Out)
		{
			Relay_In_162();
		}
	}

	private void Relay_In_158()
	{
		logic_uScript_AddMessage_messageData_158 = msg06OpenMissionBoard;
		logic_uScript_AddMessage_speaker_158 = messageSpeaker;
		logic_uScript_AddMessage_Return_158 = logic_uScript_AddMessage_uScript_AddMessage_158.In(logic_uScript_AddMessage_messageData_158, logic_uScript_AddMessage_speaker_158);
		local_168_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_158;
		if (logic_uScript_AddMessage_uScript_AddMessage_158.Out)
		{
			Relay_In_215();
		}
	}

	private void Relay_In_162()
	{
		logic_uScript_AddMessage_messageData_162 = msg07Complete;
		logic_uScript_AddMessage_speaker_162 = messageSpeaker;
		logic_uScript_AddMessage_Return_162 = logic_uScript_AddMessage_uScript_AddMessage_162.In(logic_uScript_AddMessage_messageData_162, logic_uScript_AddMessage_speaker_162);
		if (logic_uScript_AddMessage_uScript_AddMessage_162.Out)
		{
			Relay_In_219();
		}
	}

	private void Relay_In_164()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_164 = local_GSOVendor_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_164 = local_ShopHideDistance_System_Single;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_164.In(logic_uScript_IsPlayerInRangeOfTech_tech_164, logic_uScript_IsPlayerInRangeOfTech_range_164, logic_uScript_IsPlayerInRangeOfTech_techs_164);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_164.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_164.OutOfRange;
		if (inRange)
		{
			Relay_In_158();
		}
		if (outOfRange)
		{
			Relay_In_167();
		}
	}

	private void Relay_In_167()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_167 = local_168_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_167.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_167, logic_uScript_RemoveOnScreenMessage_instant_167);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_167.Out)
		{
			Relay_In_212();
		}
	}

	private void Relay_In_169()
	{
		logic_uScript_LockVisibleStackAccept_targetObject_169 = local_PurchasedBlock_TankBlock;
		logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_169.In(logic_uScript_LockVisibleStackAccept_targetObject_169);
		if (logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_169.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_171()
	{
		logic_uScriptCon_CompareInt_A_171 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_171.In(logic_uScriptCon_CompareInt_A_171, logic_uScriptCon_CompareInt_B_171);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_171.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_171.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_169();
		}
		if (lessThan)
		{
			Relay_In_173();
		}
	}

	private void Relay_In_173()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_173.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_173.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_174()
	{
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_174.In(logic_uScript_SetVendorsEnabled_enableShop_174, logic_uScript_SetVendorsEnabled_enableMissionBoard_174, logic_uScript_SetVendorsEnabled_enableSelling_174, logic_uScript_SetVendorsEnabled_enableSCU_174, logic_uScript_SetVendorsEnabled_enableCharging_174);
		if (logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_174.Out)
		{
			Relay_True_78();
		}
	}

	private void Relay_In_175()
	{
		logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_175.In(logic_uScript_SetVendorsEnabled_enableShop_175, logic_uScript_SetVendorsEnabled_enableMissionBoard_175, logic_uScript_SetVendorsEnabled_enableSelling_175, logic_uScript_SetVendorsEnabled_enableSCU_175, logic_uScript_SetVendorsEnabled_enableCharging_175);
		if (logic_uScript_SetVendorsEnabled_uScript_SetVendorsEnabled_175.Out)
		{
			Relay_AllResources_49();
		}
	}

	private void Relay_Out_176()
	{
		Relay_In_150();
	}

	private void Relay_Shown_176()
	{
	}

	private void Relay_In_176()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_176 = msg02bPurchaseBlock;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_176 = msg02bPurchaseBlock_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_176 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_176.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_176, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_176, logic_SubGraph_AddMessageWithPadSupport_speaker_176);
	}

	private void Relay_In_182()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_182 = local_181_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_182.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_182, logic_uScript_RemoveOnScreenMessage_instant_182);
	}

	private void Relay_Out_186()
	{
		Relay_In_89();
	}

	private void Relay_Shown_186()
	{
	}

	private void Relay_In_186()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_186 = msg02aOpenShop;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_186 = msg02aOpenShop_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_186 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_186.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_186, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_186, logic_SubGraph_AddMessageWithPadSupport_speaker_186);
	}

	private void Relay_In_189()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_189 = local_184_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_189.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_189, logic_uScript_RemoveOnScreenMessage_instant_189);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_189.Out)
		{
			Relay_In_201();
		}
	}

	private void Relay_In_192()
	{
		logic_uScript_EnableGlow_targetObject_192 = local_85_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_192.In(logic_uScript_EnableGlow_targetObject_192, logic_uScript_EnableGlow_enable_192);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_192.Out)
		{
			Relay_In_237();
		}
	}

	private void Relay_In_194()
	{
		logic_uScript_EnableGlow_targetObject_194 = local_195_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_194.In(logic_uScript_EnableGlow_targetObject_194, logic_uScript_EnableGlow_enable_194);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_194.Out)
		{
			Relay_In_154();
		}
	}

	private void Relay_In_196()
	{
		logic_uScript_GetTankBlock_tank_196 = local_GSOVendor_Tank;
		logic_uScript_GetTankBlock_Return_196 = logic_uScript_GetTankBlock_uScript_GetTankBlock_196.In(logic_uScript_GetTankBlock_tank_196, logic_uScript_GetTankBlock_blockType_196);
		local_195_TankBlock = logic_uScript_GetTankBlock_Return_196;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_196.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_196.NotFound;
		if (returned)
		{
			Relay_In_194();
		}
		if (notFound)
		{
			Relay_In_222();
		}
	}

	private void Relay_In_197()
	{
		logic_uScript_EnableGlow_targetObject_197 = local_PurchasedBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_197.In(logic_uScript_EnableGlow_targetObject_197, logic_uScript_EnableGlow_enable_197);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_197.Out)
		{
			Relay_In_139();
		}
	}

	private void Relay_In_199()
	{
		logic_uScript_EnableGlow_targetObject_199 = local_PurchasedBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_199.In(logic_uScript_EnableGlow_targetObject_199, logic_uScript_EnableGlow_enable_199);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_199.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_201()
	{
		logic_uScript_GetTankBlock_tank_201 = local_GSOVendor_Tank;
		logic_uScript_GetTankBlock_Return_201 = logic_uScript_GetTankBlock_uScript_GetTankBlock_201.In(logic_uScript_GetTankBlock_tank_201, logic_uScript_GetTankBlock_blockType_201);
		local_203_TankBlock = logic_uScript_GetTankBlock_Return_201;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_201.Returned)
		{
			Relay_In_204();
		}
	}

	private void Relay_In_204()
	{
		logic_uScript_EnableGlow_targetObject_204 = local_203_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_204.In(logic_uScript_EnableGlow_targetObject_204, logic_uScript_EnableGlow_enable_204);
	}

	private void Relay_In_206()
	{
		logic_uScript_EnableGlow_targetObject_206 = local_205_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_206.In(logic_uScript_EnableGlow_targetObject_206, logic_uScript_EnableGlow_enable_206);
	}

	private void Relay_In_208()
	{
		logic_uScript_GetTankBlock_tank_208 = local_GSOVendor_Tank;
		logic_uScript_GetTankBlock_Return_208 = logic_uScript_GetTankBlock_uScript_GetTankBlock_208.In(logic_uScript_GetTankBlock_tank_208, logic_uScript_GetTankBlock_blockType_208);
		local_205_TankBlock = logic_uScript_GetTankBlock_Return_208;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_208.Returned)
		{
			Relay_In_206();
		}
	}

	private void Relay_In_211()
	{
		logic_uScript_EnableGlow_targetObject_211 = local_MissionBoardBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_211.In(logic_uScript_EnableGlow_targetObject_211, logic_uScript_EnableGlow_enable_211);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_211.Out)
		{
			Relay_In_110();
		}
	}

	private void Relay_In_212()
	{
		logic_uScript_GetTankBlock_tank_212 = local_GSOVendor_Tank;
		logic_uScript_GetTankBlock_Return_212 = logic_uScript_GetTankBlock_uScript_GetTankBlock_212.In(logic_uScript_GetTankBlock_tank_212, logic_uScript_GetTankBlock_blockType_212);
		local_MissionBoardBlock_TankBlock = logic_uScript_GetTankBlock_Return_212;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_212.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_212.NotFound;
		if (returned)
		{
			Relay_In_211();
		}
		if (notFound)
		{
			Relay_In_224();
		}
	}

	private void Relay_In_214()
	{
		logic_uScript_EnableGlow_targetObject_214 = local_MissionBoardBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_214.In(logic_uScript_EnableGlow_targetObject_214, logic_uScript_EnableGlow_enable_214);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_214.Out)
		{
			Relay_In_239();
		}
	}

	private void Relay_In_215()
	{
		logic_uScript_GetTankBlock_tank_215 = local_GSOVendor_Tank;
		logic_uScript_GetTankBlock_Return_215 = logic_uScript_GetTankBlock_uScript_GetTankBlock_215.In(logic_uScript_GetTankBlock_tank_215, logic_uScript_GetTankBlock_blockType_215);
		local_MissionBoardBlock_TankBlock = logic_uScript_GetTankBlock_Return_215;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_215.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_215.NotFound;
		if (returned)
		{
			Relay_In_214();
		}
		if (notFound)
		{
			Relay_In_223();
		}
	}

	private void Relay_In_218()
	{
		logic_uScript_EnableGlow_targetObject_218 = local_MissionBoardBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_218.In(logic_uScript_EnableGlow_targetObject_218, logic_uScript_EnableGlow_enable_218);
	}

	private void Relay_In_219()
	{
		logic_uScript_GetTankBlock_tank_219 = local_GSOVendor_Tank;
		logic_uScript_GetTankBlock_Return_219 = logic_uScript_GetTankBlock_uScript_GetTankBlock_219.In(logic_uScript_GetTankBlock_tank_219, logic_uScript_GetTankBlock_blockType_219);
		local_MissionBoardBlock_TankBlock = logic_uScript_GetTankBlock_Return_219;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_219.Returned)
		{
			Relay_In_218();
		}
	}

	private void Relay_In_221()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_221.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_221.Out)
		{
			Relay_In_110();
		}
	}

	private void Relay_In_222()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_222.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_222.Out)
		{
			Relay_In_154();
		}
	}

	private void Relay_In_223()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_223.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_223.Out)
		{
			Relay_In_110();
		}
	}

	private void Relay_In_224()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_224.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_224.Out)
		{
			Relay_In_110();
		}
	}

	private void Relay_In_225()
	{
		logic_uScriptCon_CompareBool_Bool_225 = local_Msg04SellResourcesShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225.In(logic_uScriptCon_CompareBool_Bool_225);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225.False)
		{
			Relay_In_230();
		}
	}

	private void Relay_True_227()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_227.True(out logic_uScriptAct_SetBool_Target_227);
		local_Msg04SellResourcesShown_System_Boolean = logic_uScriptAct_SetBool_Target_227;
	}

	private void Relay_False_227()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_227.False(out logic_uScriptAct_SetBool_Target_227);
		local_Msg04SellResourcesShown_System_Boolean = logic_uScriptAct_SetBool_Target_227;
	}

	private void Relay_In_230()
	{
		logic_uScript_AddMessage_messageData_230 = msg04SellResources;
		logic_uScript_AddMessage_speaker_230 = messageSpeaker;
		logic_uScript_AddMessage_Return_230 = logic_uScript_AddMessage_uScript_AddMessage_230.In(logic_uScript_AddMessage_messageData_230, logic_uScript_AddMessage_speaker_230);
		bool num = logic_uScript_AddMessage_uScript_AddMessage_230.Out;
		bool shown = logic_uScript_AddMessage_uScript_AddMessage_230.Shown;
		if (num)
		{
			Relay_In_37();
		}
		if (shown)
		{
			Relay_In_233();
		}
	}

	private void Relay_In_231()
	{
		logic_uScript_ShowHint_hintId_231 = local_232_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_231.In(logic_uScript_ShowHint_hintId_231);
		if (logic_uScript_ShowHint_uScript_ShowHint_231.Out)
		{
			Relay_True_227();
		}
	}

	private void Relay_In_233()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_233 = local_232_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_233.In(logic_uScript_HasHintBeenShownBefore_hintID_233);
		bool shown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_233.Shown;
		bool notShown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_233.NotShown;
		if (shown)
		{
			Relay_True_227();
		}
		if (notShown)
		{
			Relay_In_231();
		}
	}

	private void Relay_Save_Out_235()
	{
		Relay_Save_99();
	}

	private void Relay_Load_Out_235()
	{
		Relay_Load_99();
	}

	private void Relay_Restart_Out_235()
	{
		Relay_Set_False_99();
	}

	private void Relay_Save_235()
	{
		logic_SubGraph_SaveLoadBool_boolean_235 = local_Msg04SellResourcesShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_235 = local_Msg04SellResourcesShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.Save(ref logic_SubGraph_SaveLoadBool_boolean_235, logic_SubGraph_SaveLoadBool_boolAsVariable_235, logic_SubGraph_SaveLoadBool_uniqueID_235);
	}

	private void Relay_Load_235()
	{
		logic_SubGraph_SaveLoadBool_boolean_235 = local_Msg04SellResourcesShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_235 = local_Msg04SellResourcesShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.Load(ref logic_SubGraph_SaveLoadBool_boolean_235, logic_SubGraph_SaveLoadBool_boolAsVariable_235, logic_SubGraph_SaveLoadBool_uniqueID_235);
	}

	private void Relay_Set_True_235()
	{
		logic_SubGraph_SaveLoadBool_boolean_235 = local_Msg04SellResourcesShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_235 = local_Msg04SellResourcesShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_235, logic_SubGraph_SaveLoadBool_boolAsVariable_235, logic_SubGraph_SaveLoadBool_uniqueID_235);
	}

	private void Relay_Set_False_235()
	{
		logic_SubGraph_SaveLoadBool_boolean_235 = local_Msg04SellResourcesShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_235 = local_Msg04SellResourcesShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_235.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_235, logic_SubGraph_SaveLoadBool_boolAsVariable_235, logic_SubGraph_SaveLoadBool_uniqueID_235);
	}

	private void Relay_In_237()
	{
		logic_uScript_PointArrowAtBlock_block_237 = local_85_TankBlock;
		logic_uScript_PointArrowAtBlock_offset_237 = shopArrowOffset;
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_237.In(logic_uScript_PointArrowAtBlock_block_237, logic_uScript_PointArrowAtBlock_timeToShowFor_237, logic_uScript_PointArrowAtBlock_offset_237);
	}

	private void Relay_In_239()
	{
		logic_uScript_PointArrowAtBlock_block_239 = local_MissionBoardBlock_TankBlock;
		logic_uScript_PointArrowAtBlock_offset_239 = missionBoardArrowOffset;
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_239.In(logic_uScript_PointArrowAtBlock_block_239, logic_uScript_PointArrowAtBlock_timeToShowFor_239, logic_uScript_PointArrowAtBlock_offset_239);
		if (logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_239.Out)
		{
			Relay_In_110();
		}
	}

	private void Relay_In_242()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_242.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_242, logic_uScript_SendAnaliticsEvent_parameterName_242, logic_uScript_SendAnaliticsEvent_parameter_242);
		if (logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_242.Out)
		{
			Relay_In_526();
		}
	}

	private void Relay_In_243()
	{
		logic_uScript_LockHudGroup_uScript_LockHudGroup_243.In(logic_uScript_LockHudGroup_group_243, logic_uScript_LockHudGroup_locked_243);
		if (logic_uScript_LockHudGroup_uScript_LockHudGroup_243.Out)
		{
			Relay_In_149();
		}
	}

	private void Relay_In_244()
	{
		logic_uScript_LockHudGroup_uScript_LockHudGroup_244.In(logic_uScript_LockHudGroup_group_244, logic_uScript_LockHudGroup_locked_244);
		if (logic_uScript_LockHudGroup_uScript_LockHudGroup_244.Out)
		{
			Relay_In_151();
		}
	}

	private void Relay_In_526()
	{
		logic_uScript_TutorialFinished_uScript_TutorialFinished_526.In();
		if (logic_uScript_TutorialFinished_uScript_TutorialFinished_526.Out)
		{
			Relay_Succeed_44();
		}
	}
}
