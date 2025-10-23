using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_AttachBlockToTech", "")]
public class SubGraph_AttachBlockToTech : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private TankBlock external_12;

	private BlockTypes external_25;

	private LocalisedString[] external_3 = new LocalisedString[0];

	private TankBlock local_37_TankBlock;

	private bool local_ObjectiveComplete_System_Boolean;

	private Tank local_PlayerTech_Tank;

	private Tank local_ReturnedTargetTech_Tank;

	private string local_TargetBlock_System_String = "TargetBlock";

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_22;

	private GameObject owner_Connection_32;

	private GameObject owner_Connection_36;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_0 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_0 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_0 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_0;

	private string logic_uScript_AddOnScreenMessage_tag_0 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_0;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_0;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_0;

	private bool logic_uScript_AddOnScreenMessage_Out_0 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_0 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_5;

	private bool logic_uScriptCon_CompareBool_True_5 = true;

	private bool logic_uScriptCon_CompareBool_False_5 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_7 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_7;

	private bool logic_uScriptAct_SetBool_Out_7 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_7 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_7 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_11 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_11;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_11 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_11 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_11 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_11 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_11 = true;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_13 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_13;

	private TankBlock logic_uScript_BlockAttachedToTech_block_13;

	private bool logic_uScript_BlockAttachedToTech_True_13 = true;

	private bool logic_uScript_BlockAttachedToTech_False_13 = true;

	private uScript_GetPlayerTank logic_uScript_GetPlayerTank_uScript_GetPlayerTank_14 = new uScript_GetPlayerTank();

	private Tank logic_uScript_GetPlayerTank_Return_14;

	private bool logic_uScript_GetPlayerTank_Returned_14 = true;

	private bool logic_uScript_GetPlayerTank_NotReturned_14 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_18 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_18;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_18 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_18 = true;

	private uScript_KeepBlockInvulnerable logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_19 = new uScript_KeepBlockInvulnerable();

	private TankBlock logic_uScript_KeepBlockInvulnerable_block_19;

	private bool logic_uScript_KeepBlockInvulnerable_Out_19 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_21 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_21;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_21 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_21 = true;

	private uScript_GetPlayerTankWithBlock logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_23 = new uScript_GetPlayerTankWithBlock();

	private BlockTypes logic_uScript_GetPlayerTankWithBlock_block_23;

	private TankBlock logic_uScript_GetPlayerTankWithBlock_tankBlock_23;

	private bool logic_uScript_GetPlayerTankWithBlock_useBlockType_23 = true;

	private Tank logic_uScript_GetPlayerTankWithBlock_Return_23;

	private bool logic_uScript_GetPlayerTankWithBlock_Returned_23 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_NotReturned_23 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_Out_23 = true;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_27 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_27;

	private TankBlock logic_uScript_BlockAttachedToTech_block_27;

	private bool logic_uScript_BlockAttachedToTech_True_27 = true;

	private bool logic_uScript_BlockAttachedToTech_False_27 = true;

	private uScript_SaveNamedBlock logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_30 = new uScript_SaveNamedBlock();

	private TankBlock logic_uScript_SaveNamedBlock_block_30;

	private string logic_uScript_SaveNamedBlock_uniqueName_30 = "";

	private GameObject logic_uScript_SaveNamedBlock_owner_30;

	private bool logic_uScript_SaveNamedBlock_Out_30 = true;

	private uScript_GetNamedBlock logic_uScript_GetNamedBlock_uScript_GetNamedBlock_33 = new uScript_GetNamedBlock();

	private string logic_uScript_GetNamedBlock_name_33 = "";

	private GameObject logic_uScript_GetNamedBlock_owner_33;

	private TankBlock logic_uScript_GetNamedBlock_Return_33;

	private bool logic_uScript_GetNamedBlock_Out_33 = true;

	private bool logic_uScript_GetNamedBlock_Destroyed_33 = true;

	private bool logic_uScript_GetNamedBlock_BlockExists_33 = true;

	private bool logic_uScript_GetNamedBlock_WaitingForBlock_33 = true;

	private bool logic_uScript_GetNamedBlock_NoBlock_33 = true;

	[FriendlyName("Complete")]
	public event uScriptEventHandler Complete;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_17 || !m_RegisteredForEvents)
		{
			owner_Connection_17 = parentGameObject;
		}
		if (null == owner_Connection_22 || !m_RegisteredForEvents)
		{
			owner_Connection_22 = parentGameObject;
		}
		if (null == owner_Connection_32 || !m_RegisteredForEvents)
		{
			owner_Connection_32 = parentGameObject;
		}
		if (null == owner_Connection_36 || !m_RegisteredForEvents)
		{
			owner_Connection_36 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_0.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_11.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_13.SetParent(g);
		logic_uScript_GetPlayerTank_uScript_GetPlayerTank_14.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_18.SetParent(g);
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_19.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_21.SetParent(g);
		logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_23.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_27.SetParent(g);
		logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_30.SetParent(g);
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_33.SetParent(g);
		owner_Connection_17 = parentGameObject;
		owner_Connection_22 = parentGameObject;
		owner_Connection_32 = parentGameObject;
		owner_Connection_36 = parentGameObject;
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
	}

	public void OnDisable()
	{
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_0.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_11.OnDisable();
		logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_30.OnDisable();
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_33.OnDisable();
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

	[FriendlyName("In", "")]
	public void In([FriendlyName("TargetBlock", "")] TankBlock TargetBlock, [FriendlyName("techToAttachBlockTo", "")] BlockTypes techToAttachBlockTo, [FriendlyName("msgBlockAttachedToObject", "")] LocalisedString[] msgBlockAttachedToObject)
	{
		external_12 = TargetBlock;
		external_25 = techToAttachBlockTo;
		external_3 = msgBlockAttachedToObject;
		Relay_In_5();
	}

	private void Relay_In_0()
	{
		int num = 0;
		Array array = external_3;
		if (logic_uScript_AddOnScreenMessage_locString_0.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_0, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_0, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_0 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_0.In(logic_uScript_AddOnScreenMessage_locString_0, logic_uScript_AddOnScreenMessage_msgPriority_0, logic_uScript_AddOnScreenMessage_holdMsg_0, logic_uScript_AddOnScreenMessage_tag_0, logic_uScript_AddOnScreenMessage_speaker_0, logic_uScript_AddOnScreenMessage_side_0);
	}

	private void Relay_Connection_2()
	{
	}

	private void Relay_Connection_3()
	{
	}

	private void Relay_Connection_4()
	{
		if (this.Complete != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Complete(this, args);
		}
	}

	private void Relay_In_5()
	{
		logic_uScriptCon_CompareBool_Bool_5 = local_ObjectiveComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.In(logic_uScriptCon_CompareBool_Bool_5);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.False;
		if (num)
		{
			Relay_Connection_4();
		}
		if (flag)
		{
			Relay_In_30();
		}
	}

	private void Relay_True_7()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.True(out logic_uScriptAct_SetBool_Target_7);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_7;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_7.Out)
		{
			Relay_In_0();
		}
	}

	private void Relay_False_7()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.False(out logic_uScriptAct_SetBool_Target_7);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_7;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_7.Out)
		{
			Relay_In_0();
		}
	}

	private void Relay_In_11()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_11 = external_12;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_11.In(logic_uScript_IsPlayerInteractingWithBlock_block_11);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_11.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_11.NotDragging;
		if (dragging)
		{
			Relay_In_23();
		}
		if (notDragging)
		{
			Relay_In_33();
		}
	}

	private void Relay_Connection_12()
	{
	}

	private void Relay_In_13()
	{
		logic_uScript_BlockAttachedToTech_tech_13 = local_PlayerTech_Tank;
		logic_uScript_BlockAttachedToTech_block_13 = external_12;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_13.In(logic_uScript_BlockAttachedToTech_tech_13, logic_uScript_BlockAttachedToTech_block_13);
		bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_13.True;
		bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_13.False;
		if (num)
		{
			Relay_In_23();
		}
		if (flag)
		{
			Relay_In_11();
		}
	}

	private void Relay_In_14()
	{
		logic_uScript_GetPlayerTank_Return_14 = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_14.In();
		local_PlayerTech_Tank = logic_uScript_GetPlayerTank_Return_14;
		if (logic_uScript_GetPlayerTank_uScript_GetPlayerTank_14.Returned)
		{
			Relay_In_13();
		}
	}

	private void Relay_In_18()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_18 = owner_Connection_17;
		logic_uScript_MoveEncounterWithVisible_visibleObject_18 = local_37_TankBlock;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_18.In(logic_uScript_MoveEncounterWithVisible_ownerNode_18, logic_uScript_MoveEncounterWithVisible_visibleObject_18);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_18.Out)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_19()
	{
		logic_uScript_KeepBlockInvulnerable_block_19 = external_12;
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_19.In(logic_uScript_KeepBlockInvulnerable_block_19);
		if (logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_19.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_In_21()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_21 = owner_Connection_22;
		logic_uScript_MoveEncounterWithVisible_visibleObject_21 = local_ReturnedTargetTech_Tank;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_21.In(logic_uScript_MoveEncounterWithVisible_ownerNode_21, logic_uScript_MoveEncounterWithVisible_visibleObject_21);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_21.Out)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_23()
	{
		logic_uScript_GetPlayerTankWithBlock_block_23 = external_25;
		logic_uScript_GetPlayerTankWithBlock_Return_23 = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_23.In(logic_uScript_GetPlayerTankWithBlock_block_23, logic_uScript_GetPlayerTankWithBlock_tankBlock_23, logic_uScript_GetPlayerTankWithBlock_useBlockType_23);
		local_ReturnedTargetTech_Tank = logic_uScript_GetPlayerTankWithBlock_Return_23;
		if (logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_23.Returned)
		{
			Relay_In_21();
		}
	}

	private void Relay_Connection_25()
	{
	}

	private void Relay_In_27()
	{
		logic_uScript_BlockAttachedToTech_tech_27 = local_ReturnedTargetTech_Tank;
		logic_uScript_BlockAttachedToTech_block_27 = external_12;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_27.In(logic_uScript_BlockAttachedToTech_tech_27, logic_uScript_BlockAttachedToTech_block_27);
		if (logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_27.True)
		{
			Relay_True_7();
		}
	}

	private void Relay_In_30()
	{
		logic_uScript_SaveNamedBlock_block_30 = external_12;
		logic_uScript_SaveNamedBlock_uniqueName_30 = local_TargetBlock_System_String;
		logic_uScript_SaveNamedBlock_owner_30 = owner_Connection_32;
		logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_30.In(logic_uScript_SaveNamedBlock_block_30, logic_uScript_SaveNamedBlock_uniqueName_30, logic_uScript_SaveNamedBlock_owner_30);
		if (logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_30.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_In_33()
	{
		logic_uScript_GetNamedBlock_name_33 = local_TargetBlock_System_String;
		logic_uScript_GetNamedBlock_owner_33 = owner_Connection_36;
		logic_uScript_GetNamedBlock_Return_33 = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_33.In(logic_uScript_GetNamedBlock_name_33, logic_uScript_GetNamedBlock_owner_33);
		local_37_TankBlock = logic_uScript_GetNamedBlock_Return_33;
		if (logic_uScript_GetNamedBlock_uScript_GetNamedBlock_33.BlockExists)
		{
			Relay_In_18();
		}
	}
}
