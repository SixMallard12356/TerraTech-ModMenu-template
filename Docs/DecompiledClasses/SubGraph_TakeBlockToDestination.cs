using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_TakeBlockToDestination", "")]
[NodePath("Graphs")]
public class SubGraph_TakeBlockToDestination : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private TankBlock external_12;

	private object external_23 = "";

	private LocalisedString[] external_3 = new LocalisedString[0];

	private float external_24;

	private TankBlock local_27_TankBlock;

	private bool local_ObjectiveComplete_System_Boolean;

	private Tank local_PlayerTech_Tank;

	private string local_TargetBlock_System_String = "TargetBlock";

	private TankBlock local_TargetBlock_TankBlock;

	private GameObject owner_Connection_22;

	private GameObject owner_Connection_29;

	private GameObject owner_Connection_30;

	private GameObject owner_Connection_34;

	private GameObject owner_Connection_37;

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

	private uScript_IsPlayerInRangeOfVisible logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_13 = new uScript_IsPlayerInRangeOfVisible();

	private object logic_uScript_IsPlayerInRangeOfVisible_visibleObject_13 = "";

	private float logic_uScript_IsPlayerInRangeOfVisible_range_13;

	private bool logic_uScript_IsPlayerInRangeOfVisible_Out_13 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_InRange_13 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_OutOfRange_13 = true;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_14 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_14;

	private TankBlock logic_uScript_BlockAttachedToTech_block_14;

	private bool logic_uScript_BlockAttachedToTech_True_14 = true;

	private bool logic_uScript_BlockAttachedToTech_False_14 = true;

	private uScript_GetPlayerTank logic_uScript_GetPlayerTank_uScript_GetPlayerTank_15 = new uScript_GetPlayerTank();

	private Tank logic_uScript_GetPlayerTank_Return_15;

	private bool logic_uScript_GetPlayerTank_Returned_15 = true;

	private bool logic_uScript_GetPlayerTank_NotReturned_15 = true;

	private uScript_KeepBlockInvulnerable logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_18 = new uScript_KeepBlockInvulnerable();

	private TankBlock logic_uScript_KeepBlockInvulnerable_block_18;

	private bool logic_uScript_KeepBlockInvulnerable_Out_18 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_21 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_21;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_21 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_21 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_25 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_25;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_25 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_25 = true;

	private uScript_GetNamedBlock logic_uScript_GetNamedBlock_uScript_GetNamedBlock_28 = new uScript_GetNamedBlock();

	private string logic_uScript_GetNamedBlock_name_28 = "";

	private GameObject logic_uScript_GetNamedBlock_owner_28;

	private TankBlock logic_uScript_GetNamedBlock_Return_28;

	private bool logic_uScript_GetNamedBlock_Out_28 = true;

	private bool logic_uScript_GetNamedBlock_Destroyed_28 = true;

	private bool logic_uScript_GetNamedBlock_BlockExists_28 = true;

	private bool logic_uScript_GetNamedBlock_WaitingForBlock_28 = true;

	private bool logic_uScript_GetNamedBlock_NoBlock_28 = true;

	private uScript_SaveNamedBlock logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_33 = new uScript_SaveNamedBlock();

	private TankBlock logic_uScript_SaveNamedBlock_block_33;

	private string logic_uScript_SaveNamedBlock_uniqueName_33 = "";

	private GameObject logic_uScript_SaveNamedBlock_owner_33;

	private bool logic_uScript_SaveNamedBlock_Out_33 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_36 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_36;

	private object logic_uScript_SetEncounterTarget_visibleObject_36 = "";

	private bool logic_uScript_SetEncounterTarget_Out_36 = true;

	[FriendlyName("Complete")]
	public event uScriptEventHandler Complete;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_22 || !m_RegisteredForEvents)
		{
			owner_Connection_22 = parentGameObject;
		}
		if (null == owner_Connection_29 || !m_RegisteredForEvents)
		{
			owner_Connection_29 = parentGameObject;
		}
		if (null == owner_Connection_30 || !m_RegisteredForEvents)
		{
			owner_Connection_30 = parentGameObject;
		}
		if (null == owner_Connection_34 || !m_RegisteredForEvents)
		{
			owner_Connection_34 = parentGameObject;
		}
		if (null == owner_Connection_37 || !m_RegisteredForEvents)
		{
			owner_Connection_37 = parentGameObject;
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
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_13.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_14.SetParent(g);
		logic_uScript_GetPlayerTank_uScript_GetPlayerTank_15.SetParent(g);
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_18.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_21.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_25.SetParent(g);
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_28.SetParent(g);
		logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_33.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_36.SetParent(g);
		owner_Connection_22 = parentGameObject;
		owner_Connection_29 = parentGameObject;
		owner_Connection_30 = parentGameObject;
		owner_Connection_34 = parentGameObject;
		owner_Connection_37 = parentGameObject;
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
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_13.OnDisable();
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_28.OnDisable();
		logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_33.OnDisable();
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
	public void In([FriendlyName("TargetBlock", "")] TankBlock TargetBlock, [FriendlyName("destinationObject", "")] object destinationObject, [FriendlyName("msgArrivedAtDestination", "")] LocalisedString[] msgArrivedAtDestination, [FriendlyName("nearDestinationDistance", "")] float nearDestinationDistance)
	{
		external_12 = TargetBlock;
		external_23 = destinationObject;
		external_3 = msgArrivedAtDestination;
		external_24 = nearDestinationDistance;
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
			Relay_In_33();
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
			Relay_In_21();
		}
		if (notDragging)
		{
			Relay_In_28();
		}
	}

	private void Relay_Connection_12()
	{
	}

	private void Relay_In_13()
	{
		logic_uScript_IsPlayerInRangeOfVisible_visibleObject_13 = external_23;
		logic_uScript_IsPlayerInRangeOfVisible_range_13 = external_24;
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_13.In(logic_uScript_IsPlayerInRangeOfVisible_visibleObject_13, logic_uScript_IsPlayerInRangeOfVisible_range_13);
		if (logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_13.InRange)
		{
			Relay_True_7();
		}
	}

	private void Relay_In_14()
	{
		logic_uScript_BlockAttachedToTech_tech_14 = local_PlayerTech_Tank;
		logic_uScript_BlockAttachedToTech_block_14 = external_12;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_14.In(logic_uScript_BlockAttachedToTech_tech_14, logic_uScript_BlockAttachedToTech_block_14);
		bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_14.True;
		bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_14.False;
		if (num)
		{
			Relay_In_21();
		}
		if (flag)
		{
			Relay_In_11();
		}
	}

	private void Relay_In_15()
	{
		logic_uScript_GetPlayerTank_Return_15 = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_15.In();
		local_PlayerTech_Tank = logic_uScript_GetPlayerTank_Return_15;
		if (logic_uScript_GetPlayerTank_uScript_GetPlayerTank_15.Returned)
		{
			Relay_In_14();
		}
	}

	private void Relay_In_18()
	{
		logic_uScript_KeepBlockInvulnerable_block_18 = local_TargetBlock_TankBlock;
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_18.In(logic_uScript_KeepBlockInvulnerable_block_18);
		if (logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_18.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_21()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_21 = owner_Connection_22;
		logic_uScript_MoveEncounterWithVisible_visibleObject_21 = external_23;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_21.In(logic_uScript_MoveEncounterWithVisible_ownerNode_21, logic_uScript_MoveEncounterWithVisible_visibleObject_21);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_21.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_Connection_23()
	{
	}

	private void Relay_Connection_24()
	{
	}

	private void Relay_In_25()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_25 = owner_Connection_30;
		logic_uScript_MoveEncounterWithVisible_visibleObject_25 = local_27_TankBlock;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_25.In(logic_uScript_MoveEncounterWithVisible_ownerNode_25, logic_uScript_MoveEncounterWithVisible_visibleObject_25);
	}

	private void Relay_In_28()
	{
		logic_uScript_GetNamedBlock_name_28 = local_TargetBlock_System_String;
		logic_uScript_GetNamedBlock_owner_28 = owner_Connection_29;
		logic_uScript_GetNamedBlock_Return_28 = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_28.In(logic_uScript_GetNamedBlock_name_28, logic_uScript_GetNamedBlock_owner_28);
		local_27_TankBlock = logic_uScript_GetNamedBlock_Return_28;
		if (logic_uScript_GetNamedBlock_uScript_GetNamedBlock_28.BlockExists)
		{
			Relay_In_25();
		}
	}

	private void Relay_In_33()
	{
		logic_uScript_SaveNamedBlock_block_33 = external_12;
		logic_uScript_SaveNamedBlock_uniqueName_33 = local_TargetBlock_System_String;
		logic_uScript_SaveNamedBlock_owner_33 = owner_Connection_34;
		logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_33.In(logic_uScript_SaveNamedBlock_block_33, logic_uScript_SaveNamedBlock_uniqueName_33, logic_uScript_SaveNamedBlock_owner_33);
		if (logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_33.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_In_36()
	{
		logic_uScript_SetEncounterTarget_owner_36 = owner_Connection_37;
		logic_uScript_SetEncounterTarget_visibleObject_36 = external_23;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_36.In(logic_uScript_SetEncounterTarget_owner_36, logic_uScript_SetEncounterTarget_visibleObject_36);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_36.Out)
		{
			Relay_In_13();
		}
	}
}
