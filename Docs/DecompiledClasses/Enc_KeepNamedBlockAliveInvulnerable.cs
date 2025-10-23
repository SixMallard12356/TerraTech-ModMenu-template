using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Enc_Keep Named Block Alive & Invulnerable", "")]
public class Enc_KeepNamedBlockAliveInvulnerable : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private string external_5 = "";

	private TankBlock local_Block_TankBlock;

	private GameObject owner_Connection_3;

	private uScript_KeepBlockInvulnerable logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_1 = new uScript_KeepBlockInvulnerable();

	private TankBlock logic_uScript_KeepBlockInvulnerable_block_1;

	private bool logic_uScript_KeepBlockInvulnerable_Out_1 = true;

	private uScript_GetNamedBlock logic_uScript_GetNamedBlock_uScript_GetNamedBlock_2 = new uScript_GetNamedBlock();

	private string logic_uScript_GetNamedBlock_name_2 = "";

	private GameObject logic_uScript_GetNamedBlock_owner_2;

	private TankBlock logic_uScript_GetNamedBlock_Return_2;

	private bool logic_uScript_GetNamedBlock_Out_2 = true;

	private bool logic_uScript_GetNamedBlock_Destroyed_2 = true;

	private bool logic_uScript_GetNamedBlock_BlockExists_2 = true;

	private bool logic_uScript_GetNamedBlock_WaitingForBlock_2 = true;

	private bool logic_uScript_GetNamedBlock_NoBlock_2 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
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
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_1.SetParent(g);
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_2.SetParent(g);
		owner_Connection_3 = parentGameObject;
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
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_2.OnDisable();
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
	public void In([FriendlyName("BlockName", "")] string BlockName)
	{
		external_5 = BlockName;
		Relay_In_2();
	}

	private void Relay_In_1()
	{
		logic_uScript_KeepBlockInvulnerable_block_1 = local_Block_TankBlock;
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_1.In(logic_uScript_KeepBlockInvulnerable_block_1);
		if (logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_1.Out)
		{
			Relay_Connection_4();
		}
	}

	private void Relay_In_2()
	{
		logic_uScript_GetNamedBlock_name_2 = external_5;
		logic_uScript_GetNamedBlock_owner_2 = owner_Connection_3;
		logic_uScript_GetNamedBlock_Return_2 = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_2.In(logic_uScript_GetNamedBlock_name_2, logic_uScript_GetNamedBlock_owner_2);
		local_Block_TankBlock = logic_uScript_GetNamedBlock_Return_2;
		bool destroyed = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_2.Destroyed;
		bool blockExists = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_2.BlockExists;
		bool waitingForBlock = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_2.WaitingForBlock;
		bool noBlock = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_2.NoBlock;
		if (destroyed)
		{
			Relay_Connection_4();
		}
		if (blockExists)
		{
			Relay_In_1();
		}
		if (waitingForBlock)
		{
			Relay_Connection_4();
		}
		if (noBlock)
		{
			Relay_Connection_4();
		}
	}

	private void Relay_Connection_4()
	{
		if (this.Out != null)
		{
			LogicEventArgs args = new LogicEventArgs();
			this.Out(this, args);
		}
	}

	private void Relay_Connection_5()
	{
	}

	private void Relay_Connection_6()
	{
	}
}
