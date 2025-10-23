using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_Crafting_Tutorial_ManageBlock", "")]
[NodePath("Graphs")]
public class SubGraph_Crafting_Tutorial_ManageBlock : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public TankBlock block;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private SpawnBlockData[] external_5 = new SpawnBlockData[0];

	private int external_23;

	private TankBlock external_8;

	private bool external_34;

	private uScript_AddMessage.MessageData external_24;

	private uScript_AddMessage.MessageSpeaker external_29;

	private string local_19_System_String = "";

	private SpawnBlockData local_22_SpawnBlockData;

	private TankBlock[] local_3_TankBlockArray = new TankBlock[0];

	private TankBlock local_Block_TankBlock;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_18;

	private uScript_KeepBlockInvulnerable logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_0 = new uScript_KeepBlockInvulnerable();

	private TankBlock logic_uScript_KeepBlockInvulnerable_block_0;

	private bool logic_uScript_KeepBlockInvulnerable_Out_0 = true;

	private uScript_GetAndCheckBlocks logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_1 = new uScript_GetAndCheckBlocks();

	private SpawnBlockData[] logic_uScript_GetAndCheckBlocks_blockData_1 = new SpawnBlockData[0];

	private GameObject logic_uScript_GetAndCheckBlocks_ownerNode_1;

	private TankBlock[] logic_uScript_GetAndCheckBlocks_blocks_1 = new TankBlock[0];

	private bool logic_uScript_GetAndCheckBlocks_AllAlive_1 = true;

	private bool logic_uScript_GetAndCheckBlocks_SomeAlive_1 = true;

	private bool logic_uScript_GetAndCheckBlocks_AllDead_1 = true;

	private bool logic_uScript_GetAndCheckBlocks_WaitingToSpawn_1 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_4 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_4 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_4;

	private TankBlock logic_uScript_AccessListBlock_value_4;

	private bool logic_uScript_AccessListBlock_Out_4 = true;

	private uScript_LockTutorialBlockAttach logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_9 = new uScript_LockTutorialBlockAttach();

	private TankBlock logic_uScript_LockTutorialBlockAttach_block_9;

	private bool logic_uScript_LockTutorialBlockAttach_Out_9 = true;

	private uScript_KeepVisibleInEncounterArea logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_10 = new uScript_KeepVisibleInEncounterArea();

	private GameObject logic_uScript_KeepVisibleInEncounterArea_ownerNode_10;

	private object logic_uScript_KeepVisibleInEncounterArea_objectToEnclose_10 = "";

	private string logic_uScript_KeepVisibleInEncounterArea_resetPosName_10 = "";

	private Vector3 logic_uScript_KeepVisibleInEncounterArea_positionBeforeReset_10;

	private bool logic_uScript_KeepVisibleInEncounterArea_Out_10 = true;

	private bool logic_uScript_KeepVisibleInEncounterArea_InsideArea_10 = true;

	private bool logic_uScript_KeepVisibleInEncounterArea_ResetFromOutsideArea_10 = true;

	private uScript_LockVisibleStackAccept logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_11 = new uScript_LockVisibleStackAccept();

	private object logic_uScript_LockVisibleStackAccept_targetObject_11 = "";

	private bool logic_uScript_LockVisibleStackAccept_Out_11 = true;

	private uScript_GetBlockSpawnDataPositionName logic_uScript_GetBlockSpawnDataPositionName_uScript_GetBlockSpawnDataPositionName_12 = new uScript_GetBlockSpawnDataPositionName();

	private SpawnBlockData logic_uScript_GetBlockSpawnDataPositionName_blockData_12;

	private string logic_uScript_GetBlockSpawnDataPositionName_positionName_12;

	private bool logic_uScript_GetBlockSpawnDataPositionName_Out_12 = true;

	private uScript_CastBlock logic_uScript_CastBlock_uScript_CastBlock_14 = new uScript_CastBlock();

	private TankBlock logic_uScript_CastBlock_block_14;

	private TankBlock logic_uScript_CastBlock_outBlock_14;

	private bool logic_uScript_CastBlock_Out_14 = true;

	private uScript_AccessListBlockSpawnData logic_uScript_AccessListBlockSpawnData_uScript_AccessListBlockSpawnData_21 = new uScript_AccessListBlockSpawnData();

	private SpawnBlockData[] logic_uScript_AccessListBlockSpawnData_dataList_21 = new SpawnBlockData[0];

	private int logic_uScript_AccessListBlockSpawnData_index_21;

	private SpawnBlockData logic_uScript_AccessListBlockSpawnData_value_21;

	private bool logic_uScript_AccessListBlockSpawnData_Out_21 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_26 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_26 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_27 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_27 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_28 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_28;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_28;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_28;

	private bool logic_uScript_AddMessage_Out_28 = true;

	private bool logic_uScript_AddMessage_Shown_28 = true;

	private uScript_LockBlockAttach logic_uScript_LockBlockAttach_uScript_LockBlockAttach_30 = new uScript_LockBlockAttach();

	private TankBlock logic_uScript_LockBlockAttach_block_30;

	private bool logic_uScript_LockBlockAttach_Out_30 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_31;

	private bool logic_uScriptCon_CompareBool_True_31 = true;

	private bool logic_uScriptCon_CompareBool_False_31 = true;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
		}
		if (null == owner_Connection_18 || !m_RegisteredForEvents)
		{
			owner_Connection_18 = parentGameObject;
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
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_0.SetParent(g);
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_1.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_4.SetParent(g);
		logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_9.SetParent(g);
		logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_10.SetParent(g);
		logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_11.SetParent(g);
		logic_uScript_GetBlockSpawnDataPositionName_uScript_GetBlockSpawnDataPositionName_12.SetParent(g);
		logic_uScript_CastBlock_uScript_CastBlock_14.SetParent(g);
		logic_uScript_AccessListBlockSpawnData_uScript_AccessListBlockSpawnData_21.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_26.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_27.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_28.SetParent(g);
		logic_uScript_LockBlockAttach_uScript_LockBlockAttach_30.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.SetParent(g);
		owner_Connection_2 = parentGameObject;
		owner_Connection_18 = parentGameObject;
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
		logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_9.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_AddMessage_uScript_AddMessage_28.OnDisable();
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
	public void In([FriendlyName("blockSpawnData", "")] ref SpawnBlockData[] blockSpawnData, [FriendlyName("blockIndex", "")] int blockIndex, [FriendlyName("block", "")] ref TankBlock block, [FriendlyName("allowAnchoring", "")] bool allowAnchoring, [FriendlyName("msgBlockOutsideArea", "")] uScript_AddMessage.MessageData msgBlockOutsideArea, [FriendlyName("messageSpeaker", "")] uScript_AddMessage.MessageSpeaker messageSpeaker)
	{
		external_5 = blockSpawnData;
		external_23 = blockIndex;
		external_8 = block;
		external_34 = allowAnchoring;
		external_24 = msgBlockOutsideArea;
		external_29 = messageSpeaker;
		Relay_In_1();
	}

	private void Relay_In_0()
	{
		logic_uScript_KeepBlockInvulnerable_block_0 = local_Block_TankBlock;
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_0.In(logic_uScript_KeepBlockInvulnerable_block_0);
		if (logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_0.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_1()
	{
		int num = 0;
		Array array = external_5;
		if (logic_uScript_GetAndCheckBlocks_blockData_1.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blockData_1, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckBlocks_blockData_1, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckBlocks_ownerNode_1 = owner_Connection_2;
		int num2 = 0;
		Array array2 = local_3_TankBlockArray;
		if (logic_uScript_GetAndCheckBlocks_blocks_1.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blocks_1, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckBlocks_blocks_1, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_1.In(logic_uScript_GetAndCheckBlocks_blockData_1, logic_uScript_GetAndCheckBlocks_ownerNode_1, ref logic_uScript_GetAndCheckBlocks_blocks_1);
		local_3_TankBlockArray = logic_uScript_GetAndCheckBlocks_blocks_1;
		bool allAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_1.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_1.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_1.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_1.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_4();
		}
		if (someAlive)
		{
			Relay_AtIndex_4();
		}
		if (allDead)
		{
			Relay_In_26();
		}
		if (waitingToSpawn)
		{
			Relay_In_26();
		}
	}

	private void Relay_AtIndex_4()
	{
		int num = 0;
		Array array = local_3_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_4.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_4, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_4, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_index_4 = external_23;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_4.AtIndex(ref logic_uScript_AccessListBlock_blockList_4, logic_uScript_AccessListBlock_index_4, out logic_uScript_AccessListBlock_value_4);
		local_3_TankBlockArray = logic_uScript_AccessListBlock_blockList_4;
		external_8 = logic_uScript_AccessListBlock_value_4;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_4.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_Connection_5()
	{
	}

	private void Relay_Connection_6()
	{
		if (this.Out != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.block = external_8;
			this.Out(this, e);
		}
	}

	private void Relay_Connection_7()
	{
	}

	private void Relay_Connection_8()
	{
	}

	private void Relay_In_9()
	{
		logic_uScript_LockTutorialBlockAttach_block_9 = local_Block_TankBlock;
		logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_9.In(logic_uScript_LockTutorialBlockAttach_block_9);
		if (logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_9.Out)
		{
			Relay_In_11();
		}
	}

	private void Relay_In_10()
	{
		logic_uScript_KeepVisibleInEncounterArea_ownerNode_10 = owner_Connection_18;
		logic_uScript_KeepVisibleInEncounterArea_objectToEnclose_10 = local_Block_TankBlock;
		logic_uScript_KeepVisibleInEncounterArea_resetPosName_10 = local_19_System_String;
		logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_10.In(logic_uScript_KeepVisibleInEncounterArea_ownerNode_10, logic_uScript_KeepVisibleInEncounterArea_objectToEnclose_10, logic_uScript_KeepVisibleInEncounterArea_resetPosName_10, out logic_uScript_KeepVisibleInEncounterArea_positionBeforeReset_10);
		bool insideArea = logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_10.InsideArea;
		bool resetFromOutsideArea = logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_10.ResetFromOutsideArea;
		if (insideArea)
		{
			Relay_Connection_6();
		}
		if (resetFromOutsideArea)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_11()
	{
		logic_uScript_LockVisibleStackAccept_targetObject_11 = local_Block_TankBlock;
		logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_11.In(logic_uScript_LockVisibleStackAccept_targetObject_11);
		if (logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_11.Out)
		{
			Relay_AtIndex_21();
		}
	}

	private void Relay_In_12()
	{
		logic_uScript_GetBlockSpawnDataPositionName_blockData_12 = local_22_SpawnBlockData;
		logic_uScript_GetBlockSpawnDataPositionName_uScript_GetBlockSpawnDataPositionName_12.In(logic_uScript_GetBlockSpawnDataPositionName_blockData_12, out logic_uScript_GetBlockSpawnDataPositionName_positionName_12);
		local_19_System_String = logic_uScript_GetBlockSpawnDataPositionName_positionName_12;
		if (logic_uScript_GetBlockSpawnDataPositionName_uScript_GetBlockSpawnDataPositionName_12.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_14()
	{
		logic_uScript_CastBlock_block_14 = external_8;
		logic_uScript_CastBlock_uScript_CastBlock_14.In(logic_uScript_CastBlock_block_14, out logic_uScript_CastBlock_outBlock_14);
		local_Block_TankBlock = logic_uScript_CastBlock_outBlock_14;
		if (logic_uScript_CastBlock_uScript_CastBlock_14.Out)
		{
			Relay_In_0();
		}
	}

	private void Relay_AtIndex_21()
	{
		int num = 0;
		Array array = external_5;
		if (logic_uScript_AccessListBlockSpawnData_dataList_21.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlockSpawnData_dataList_21, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlockSpawnData_dataList_21, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlockSpawnData_index_21 = external_23;
		logic_uScript_AccessListBlockSpawnData_uScript_AccessListBlockSpawnData_21.AtIndex(logic_uScript_AccessListBlockSpawnData_dataList_21, logic_uScript_AccessListBlockSpawnData_index_21, out logic_uScript_AccessListBlockSpawnData_value_21);
		local_22_SpawnBlockData = logic_uScript_AccessListBlockSpawnData_value_21;
		if (logic_uScript_AccessListBlockSpawnData_uScript_AccessListBlockSpawnData_21.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_Connection_23()
	{
	}

	private void Relay_Connection_24()
	{
	}

	private void Relay_In_26()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_26.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_26.Out)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_27()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_27.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_27.Out)
		{
			Relay_Connection_6();
		}
	}

	private void Relay_In_28()
	{
		logic_uScript_AddMessage_messageData_28 = external_24;
		logic_uScript_AddMessage_speaker_28 = external_29;
		logic_uScript_AddMessage_Return_28 = logic_uScript_AddMessage_uScript_AddMessage_28.In(logic_uScript_AddMessage_messageData_28, logic_uScript_AddMessage_speaker_28);
	}

	private void Relay_Connection_29()
	{
	}

	private void Relay_In_30()
	{
		logic_uScript_LockBlockAttach_block_30 = local_Block_TankBlock;
		logic_uScript_LockBlockAttach_uScript_LockBlockAttach_30.In(logic_uScript_LockBlockAttach_block_30);
		if (logic_uScript_LockBlockAttach_uScript_LockBlockAttach_30.Out)
		{
			Relay_In_11();
		}
	}

	private void Relay_In_31()
	{
		logic_uScriptCon_CompareBool_Bool_31 = external_34;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.In(logic_uScriptCon_CompareBool_Bool_31);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.False;
		if (num)
		{
			Relay_In_30();
		}
		if (flag)
		{
			Relay_In_9();
		}
	}

	private void Relay_Connection_34()
	{
	}
}
