using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_PurchaseBlock", "")]
[NodePath("Graphs")]
public class SubGraph_PurchaseBlock : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private BlockTypes external_3;

	private Tank local_8_Tank;

	private GameObject owner_Connection_9;

	private uScript_ClearBlockPurchaseRestrictions logic_uScript_ClearBlockPurchaseRestrictions_uScript_ClearBlockPurchaseRestrictions_0 = new uScript_ClearBlockPurchaseRestrictions();

	private bool logic_uScript_ClearBlockPurchaseRestrictions_Out_0 = true;

	private uScript_HasPlayerPurchased logic_uScript_HasPlayerPurchased_uScript_HasPlayerPurchased_1 = new uScript_HasPlayerPurchased();

	private BlockTypes logic_uScript_HasPlayerPurchased_blockType_1;

	private int logic_uScript_HasPlayerPurchased_quantity_1 = 1;

	private bool logic_uScript_HasPlayerPurchased_Out_1 = true;

	private bool logic_uScript_HasPlayerPurchased_True_1 = true;

	private bool logic_uScript_HasPlayerPurchased_False_1 = true;

	private uScript_RestrictBlockPurchase logic_uScript_RestrictBlockPurchase_uScript_RestrictBlockPurchase_2 = new uScript_RestrictBlockPurchase();

	private BlockTypes[] logic_uScript_RestrictBlockPurchase_allowedBlockTypes_2 = new BlockTypes[0];

	private bool logic_uScript_RestrictBlockPurchase_Out_2 = true;

	private uScript_FindNearestVendor logic_uScript_FindNearestVendor_uScript_FindNearestVendor_6 = new uScript_FindNearestVendor();

	private Tank logic_uScript_FindNearestVendor_Return_6;

	private bool logic_uScript_FindNearestVendor_Out_6 = true;

	private bool logic_uScript_FindNearestVendor_Returned_6 = true;

	private bool logic_uScript_FindNearestVendor_NotReturned_6 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_7 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_7;

	private object logic_uScript_SetEncounterTarget_visibleObject_7 = "";

	private bool logic_uScript_SetEncounterTarget_Out_7 = true;

	[FriendlyName("Complete")]
	public event uScriptEventHandler Complete;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
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
		logic_uScript_ClearBlockPurchaseRestrictions_uScript_ClearBlockPurchaseRestrictions_0.SetParent(g);
		logic_uScript_HasPlayerPurchased_uScript_HasPlayerPurchased_1.SetParent(g);
		logic_uScript_RestrictBlockPurchase_uScript_RestrictBlockPurchase_2.SetParent(g);
		logic_uScript_FindNearestVendor_uScript_FindNearestVendor_6.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_7.SetParent(g);
		owner_Connection_9 = parentGameObject;
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
	public void In([FriendlyName("blockToPurchase", "")] BlockTypes blockToPurchase)
	{
		external_3 = blockToPurchase;
		Relay_In_6();
	}

	private void Relay_In_0()
	{
		logic_uScript_ClearBlockPurchaseRestrictions_uScript_ClearBlockPurchaseRestrictions_0.In();
		if (logic_uScript_ClearBlockPurchaseRestrictions_uScript_ClearBlockPurchaseRestrictions_0.Out)
		{
			Relay_Connection_5();
		}
	}

	private void Relay_In_1()
	{
		logic_uScript_HasPlayerPurchased_blockType_1 = external_3;
		logic_uScript_HasPlayerPurchased_uScript_HasPlayerPurchased_1.In(logic_uScript_HasPlayerPurchased_blockType_1, logic_uScript_HasPlayerPurchased_quantity_1);
		bool num = logic_uScript_HasPlayerPurchased_uScript_HasPlayerPurchased_1.True;
		bool flag = logic_uScript_HasPlayerPurchased_uScript_HasPlayerPurchased_1.False;
		if (num)
		{
			Relay_In_0();
		}
		if (flag)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_2()
	{
		int num = 0;
		if (logic_uScript_RestrictBlockPurchase_allowedBlockTypes_2.Length <= num)
		{
			Array.Resize(ref logic_uScript_RestrictBlockPurchase_allowedBlockTypes_2, num + 1);
		}
		logic_uScript_RestrictBlockPurchase_allowedBlockTypes_2[num++] = external_3;
		logic_uScript_RestrictBlockPurchase_uScript_RestrictBlockPurchase_2.In(logic_uScript_RestrictBlockPurchase_allowedBlockTypes_2);
	}

	private void Relay_Connection_3()
	{
	}

	private void Relay_Connection_4()
	{
	}

	private void Relay_Connection_5()
	{
		if (this.Complete != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Complete(this, args);
		}
	}

	private void Relay_In_6()
	{
		logic_uScript_FindNearestVendor_Return_6 = logic_uScript_FindNearestVendor_uScript_FindNearestVendor_6.In();
		local_8_Tank = logic_uScript_FindNearestVendor_Return_6;
		if (logic_uScript_FindNearestVendor_uScript_FindNearestVendor_6.Returned)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_7()
	{
		logic_uScript_SetEncounterTarget_owner_7 = owner_Connection_9;
		logic_uScript_SetEncounterTarget_visibleObject_7 = local_8_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_7.In(logic_uScript_SetEncounterTarget_owner_7, logic_uScript_SetEncounterTarget_visibleObject_7);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_7.Out)
		{
			Relay_In_1();
		}
	}
}
