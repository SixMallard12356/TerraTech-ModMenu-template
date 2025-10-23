using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_Crafting_Tutorial_AttachBlockToBase", "")]
[NodePath("Graphs")]
public class SubGraph_Crafting_Tutorial_AttachBlockToBase : uScriptLogic
{
	private delegate void ContinueExecution();

	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private ContinueExecution m_ContinueExecution;

	private bool m_Breakpoint;

	private const int MaxRelayCallCount = 1000;

	private int relayCallCount;

	private TankBlock external_28;

	private Tank external_9;

	private GhostBlockSpawnData[] external_5 = new GhostBlockSpawnData[0];

	private BlockTypes external_35;

	private Vector3 external_36;

	private TankBlock[] local_14_TankBlockArray = new TankBlock[0];

	private bool local_43_System_Boolean;

	private TankBlock local_Block_TankBlock;

	private TankBlock local_GhostBlock_TankBlock;

	private bool local_PlayerHoldingBlock_System_Boolean;

	private GameObject owner_Connection_2;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_0 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_0;

	private TankBlock logic_uScript_BlockAttachedToTech_block_0;

	private bool logic_uScript_BlockAttachedToTech_True_0 = true;

	private bool logic_uScript_BlockAttachedToTech_False_0 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_1 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_1 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_1;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_1;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_1;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_1 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_1 = true;

	private uScript_RemoveAllGhostBlocksOnTech logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_3 = new uScript_RemoveAllGhostBlocksOnTech();

	private Tank logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_3;

	private bool logic_uScript_RemoveAllGhostBlocksOnTech_Out_3 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_4 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_4 = true;

	private uScript_PointArrowAtBlock logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_8 = new uScript_PointArrowAtBlock();

	private TankBlock logic_uScript_PointArrowAtBlock_block_8;

	private float logic_uScript_PointArrowAtBlock_timeToShowFor_8 = -1f;

	private Vector3 logic_uScript_PointArrowAtBlock_offset_8 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtBlock_Out_8 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_12 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_12;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_12 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_12 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_12 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_12 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_12 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_13 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_13 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_13;

	private TankBlock logic_uScript_AccessListBlock_value_13;

	private bool logic_uScript_AccessListBlock_Out_13 = true;

	private uScript_PointArrowAtBlock logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_16 = new uScript_PointArrowAtBlock();

	private TankBlock logic_uScript_PointArrowAtBlock_block_16;

	private float logic_uScript_PointArrowAtBlock_timeToShowFor_16 = -1f;

	private Vector3 logic_uScript_PointArrowAtBlock_offset_16 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtBlock_Out_16 = true;

	private uScript_LockTutorialBlockAttach logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_17 = new uScript_LockTutorialBlockAttach();

	private TankBlock logic_uScript_LockTutorialBlockAttach_block_17;

	private bool logic_uScript_LockTutorialBlockAttach_Out_17 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_18 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_18 = "";

	private bool logic_uScript_EnableGlow_enable_18;

	private bool logic_uScript_EnableGlow_Out_18 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_20 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_20 = "";

	private bool logic_uScript_EnableGlow_enable_20;

	private bool logic_uScript_EnableGlow_Out_20 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_23 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_23 = "";

	private bool logic_uScript_EnableGlow_enable_23;

	private bool logic_uScript_EnableGlow_Out_23 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_25 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_25 = "";

	private bool logic_uScript_EnableGlow_enable_25;

	private bool logic_uScript_EnableGlow_Out_25 = true;

	private uScript_CastBlock logic_uScript_CastBlock_uScript_CastBlock_27 = new uScript_CastBlock();

	private TankBlock logic_uScript_CastBlock_block_27;

	private TankBlock logic_uScript_CastBlock_outBlock_27;

	private bool logic_uScript_CastBlock_Out_27 = true;

	private uScript_DoesTechHaveBlockAtPosition logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_33 = new uScript_DoesTechHaveBlockAtPosition();

	private Tank logic_uScript_DoesTechHaveBlockAtPosition_tech_33;

	private BlockTypes logic_uScript_DoesTechHaveBlockAtPosition_blockType_33;

	private Vector3 logic_uScript_DoesTechHaveBlockAtPosition_localPosition_33;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_True_33 = true;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_False_33 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_37 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_37;

	private bool logic_uScriptAct_SetBool_Out_37 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_37 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_37 = true;

	private uScriptAct_InvertBool logic_uScriptAct_InvertBool_uScriptAct_InvertBool_42 = new uScriptAct_InvertBool();

	private bool logic_uScriptAct_InvertBool_Target_42;

	private bool logic_uScriptAct_InvertBool_Value_42;

	private bool logic_uScriptAct_InvertBool_Out_42 = true;

	[FriendlyName("Block Attached")]
	public event uScriptEventHandler Block_Attached;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
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
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_0.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_1.SetParent(g);
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_3.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_4.SetParent(g);
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_8.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_12.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_13.SetParent(g);
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_16.SetParent(g);
		logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_17.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_18.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_20.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_23.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_25.SetParent(g);
		logic_uScript_CastBlock_uScript_CastBlock_27.SetParent(g);
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_33.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_37.SetParent(g);
		logic_uScriptAct_InvertBool_uScriptAct_InvertBool_42.SetParent(g);
		owner_Connection_2 = parentGameObject;
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
		logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_17.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_12.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		if (m_ContinueExecution != null)
		{
			ContinueExecution continueExecution = m_ContinueExecution;
			m_ContinueExecution = null;
			m_Breakpoint = false;
			continueExecution();
		}
		else
		{
			UpdateEditorValues();
			SyncEventListeners();
		}
	}

	public void OnDestroy()
	{
	}

	[FriendlyName("In", "")]
	public void In([FriendlyName("Block", "")] TankBlock Block, [FriendlyName("CraftingBaseTech", "")] Tank CraftingBaseTech, [FriendlyName("ghostBlockData", "")] GhostBlockSpawnData[] ghostBlockData, [FriendlyName("BlockType", "")] BlockTypes BlockType, [FriendlyName("BlockPosition", "")] Vector3 BlockPosition)
	{
		external_28 = Block;
		external_9 = CraftingBaseTech;
		external_5 = ghostBlockData;
		external_35 = BlockType;
		external_36 = BlockPosition;
		Relay_In_27();
	}

	private void Relay_In_0()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f2a1d564-2279-44ad-abee-a9e13b4355c0", "uScript_BlockAttachedToTech", Relay_In_0))
			{
				logic_uScript_BlockAttachedToTech_tech_0 = external_9;
				logic_uScript_BlockAttachedToTech_block_0 = local_Block_TankBlock;
				logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_0.In(logic_uScript_BlockAttachedToTech_tech_0, logic_uScript_BlockAttachedToTech_block_0);
				bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_0.True;
				bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_0.False;
				if (num)
				{
					Relay_In_3();
				}
				if (flag)
				{
					Relay_In_33();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at uScript_BlockAttachedToTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_TrySpawnOnTech_1()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ca19e99e-32c4-4a49-9cf4-5614297bdd6c", "uScript_SpawnGhostBlocks", Relay_TrySpawnOnTech_1))
			{
				int num = 0;
				Array array = external_5;
				if (logic_uScript_SpawnGhostBlocks_ghostBlockData_1.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_1, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_1, num, array.Length);
				num += array.Length;
				logic_uScript_SpawnGhostBlocks_ownerNode_1 = owner_Connection_2;
				logic_uScript_SpawnGhostBlocks_targetTech_1 = external_9;
				logic_uScript_SpawnGhostBlocks_Return_1 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_1.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_1, logic_uScript_SpawnGhostBlocks_ownerNode_1, logic_uScript_SpawnGhostBlocks_targetTech_1);
				local_14_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_1;
				bool onAlreadySpawned = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_1.OnAlreadySpawned;
				bool onSpawned = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_1.OnSpawned;
				if (onAlreadySpawned)
				{
					Relay_In_12();
				}
				if (onSpawned)
				{
					Relay_AtIndex_13();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at uScript_SpawnGhostBlocks.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_3()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7de0d04e-9988-4d2b-8fb5-8c03b7e7ff6c", "uScript_RemoveAllGhostBlocksOnTech", Relay_In_3))
			{
				logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_3 = external_9;
				logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_3.In(logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_3);
				if (logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_3.Out)
				{
					Relay_In_4();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at uScript_RemoveAllGhostBlocksOnTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_4()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("57cfa817-f6ed-4a8e-ae2f-a6f81b5de309", "uScript_HideArrow", Relay_In_4))
			{
				logic_uScript_HideArrow_uScript_HideArrow_4.In();
				if (logic_uScript_HideArrow_uScript_HideArrow_4.Out)
				{
					Relay_In_25();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at uScript_HideArrow.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Connection_5()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("590e0d7e-68a3-4ef2-8ea9-6fef498ce6fa", "", Relay_Connection_5);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at ghostBlockData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Connection_6()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("eaf731e4-cef3-42ae-b06e-dda84eb0bdb6", "", Relay_Connection_6);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at In.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Connection_7()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2af142d6-90ec-4844-b2f0-9794fc008ccd", "", Relay_Connection_7) && this.Block_Attached != null)
			{
				LogicEventArgs args = new LogicEventArgs();
				this.Block_Attached(this, args);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at Block Attached.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_8()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("25f73306-ae98-4fdd-95f3-066835c116ac", "uScript_PointArrowAtBlock", Relay_In_8))
			{
				logic_uScript_PointArrowAtBlock_block_8 = local_GhostBlock_TankBlock;
				logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_8.In(logic_uScript_PointArrowAtBlock_block_8, logic_uScript_PointArrowAtBlock_timeToShowFor_8, logic_uScript_PointArrowAtBlock_offset_8);
				if (logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_8.Out)
				{
					Relay_In_18();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at uScript_PointArrowAtBlock.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Connection_9()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("bf3dad43-56bd-431e-8328-865a4cd4676d", "", Relay_Connection_9);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at CraftingBaseTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_12()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("351620d4-2c1b-496b-8f83-9686166bd0cb", "Player_Is_player_interacting_with_block", Relay_In_12))
			{
				logic_uScript_IsPlayerInteractingWithBlock_block_12 = local_Block_TankBlock;
				logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_12.In(logic_uScript_IsPlayerInteractingWithBlock_block_12);
				bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_12.Dragging;
				bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_12.NotDragging;
				if (dragging)
				{
					Relay_True_37();
				}
				if (notDragging)
				{
					Relay_False_37();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at Player/Is player interacting with block.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_AtIndex_13()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5175eebf-b5c1-4ead-acc8-8108cf22778c", "uScript_AccessListBlock", Relay_AtIndex_13))
			{
				int num = 0;
				Array array = local_14_TankBlockArray;
				if (logic_uScript_AccessListBlock_blockList_13.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AccessListBlock_blockList_13, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_13, num, array.Length);
				num += array.Length;
				logic_uScript_AccessListBlock_uScript_AccessListBlock_13.AtIndex(ref logic_uScript_AccessListBlock_blockList_13, logic_uScript_AccessListBlock_index_13, out logic_uScript_AccessListBlock_value_13);
				local_14_TankBlockArray = logic_uScript_AccessListBlock_blockList_13;
				local_GhostBlock_TankBlock = logic_uScript_AccessListBlock_value_13;
				if (logic_uScript_AccessListBlock_uScript_AccessListBlock_13.Out)
				{
					Relay_In_12();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at uScript_AccessListBlock.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_16()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0ba5a185-b6d2-44cd-aaf9-249055c1ac82", "uScript_PointArrowAtBlock", Relay_In_16))
			{
				logic_uScript_PointArrowAtBlock_block_16 = local_Block_TankBlock;
				logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_16.In(logic_uScript_PointArrowAtBlock_block_16, logic_uScript_PointArrowAtBlock_timeToShowFor_16, logic_uScript_PointArrowAtBlock_offset_16);
				if (logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_16.Out)
				{
					Relay_In_18();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at uScript_PointArrowAtBlock.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_17()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5b7b5c94-36d4-4882-8610-2ccdeeb23802", "uScript_LockTutorialBlockAttach", Relay_In_17))
			{
				logic_uScript_LockTutorialBlockAttach_block_17 = local_Block_TankBlock;
				logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_17.In(logic_uScript_LockTutorialBlockAttach_block_17);
				if (logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_17.Out)
				{
					Relay_TrySpawnOnTech_1();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at uScript_LockTutorialBlockAttach.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_18()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0beb1062-79e1-4c0b-9011-53e1faec0ad7", "uScript_EnableGlow", Relay_In_18))
			{
				logic_uScript_EnableGlow_targetObject_18 = local_GhostBlock_TankBlock;
				logic_uScript_EnableGlow_enable_18 = local_PlayerHoldingBlock_System_Boolean;
				logic_uScript_EnableGlow_uScript_EnableGlow_18.In(logic_uScript_EnableGlow_targetObject_18, logic_uScript_EnableGlow_enable_18);
				if (logic_uScript_EnableGlow_uScript_EnableGlow_18.Out)
				{
					Relay_In_42();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at uScript_EnableGlow.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_20()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e6ea1a08-a1b5-44d5-896c-e2f486058b24", "uScript_EnableGlow", Relay_In_20))
			{
				logic_uScript_EnableGlow_targetObject_20 = local_Block_TankBlock;
				logic_uScript_EnableGlow_enable_20 = local_43_System_Boolean;
				logic_uScript_EnableGlow_uScript_EnableGlow_20.In(logic_uScript_EnableGlow_targetObject_20, logic_uScript_EnableGlow_enable_20);
				if (logic_uScript_EnableGlow_uScript_EnableGlow_20.Out)
				{
					Relay_In_0();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at uScript_EnableGlow.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_23()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cd1ecae8-a7e1-441f-95e2-4fe85bc4aa78", "uScript_EnableGlow", Relay_In_23))
			{
				logic_uScript_EnableGlow_targetObject_23 = local_GhostBlock_TankBlock;
				logic_uScript_EnableGlow_uScript_EnableGlow_23.In(logic_uScript_EnableGlow_targetObject_23, logic_uScript_EnableGlow_enable_23);
				if (logic_uScript_EnableGlow_uScript_EnableGlow_23.Out)
				{
					Relay_Connection_7();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at uScript_EnableGlow.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_25()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6015f685-a2d1-4977-a0e6-2d0f75f95c4b", "uScript_EnableGlow", Relay_In_25))
			{
				logic_uScript_EnableGlow_targetObject_25 = local_Block_TankBlock;
				logic_uScript_EnableGlow_uScript_EnableGlow_25.In(logic_uScript_EnableGlow_targetObject_25, logic_uScript_EnableGlow_enable_25);
				if (logic_uScript_EnableGlow_uScript_EnableGlow_25.Out)
				{
					Relay_In_23();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at uScript_EnableGlow.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_27()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bcfd9171-68a6-4347-ad5c-3708d1987572", "Cast_External_param_to_TankBlock", Relay_In_27))
			{
				logic_uScript_CastBlock_block_27 = external_28;
				logic_uScript_CastBlock_uScript_CastBlock_27.In(logic_uScript_CastBlock_block_27, out logic_uScript_CastBlock_outBlock_27);
				local_Block_TankBlock = logic_uScript_CastBlock_outBlock_27;
				if (logic_uScript_CastBlock_uScript_CastBlock_27.Out)
				{
					Relay_In_17();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at Cast External param to TankBlock.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Connection_28()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("26d026c2-fe37-4203-bb0c-3120a9dbec32", "", Relay_Connection_28);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at Block.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_33()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d15f30c8-903b-4549-b81c-2cef0cbf0440", "uScript_DoesTechHaveBlockAtPosition", Relay_In_33))
			{
				logic_uScript_DoesTechHaveBlockAtPosition_tech_33 = external_9;
				logic_uScript_DoesTechHaveBlockAtPosition_blockType_33 = external_35;
				logic_uScript_DoesTechHaveBlockAtPosition_localPosition_33 = external_36;
				logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_33.In(logic_uScript_DoesTechHaveBlockAtPosition_tech_33, logic_uScript_DoesTechHaveBlockAtPosition_blockType_33, logic_uScript_DoesTechHaveBlockAtPosition_localPosition_33);
				if (logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_33.True)
				{
					Relay_In_3();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at uScript_DoesTechHaveBlockAtPosition.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Connection_35()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("47f2fbc2-e68f-4e78-b3bd-4d47218aad01", "", Relay_Connection_35);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at BlockType.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Connection_36()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("5a4bbd21-5d03-4689-a7ac-c69180b8da05", "", Relay_Connection_36);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at BlockPosition.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_37()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("fcd74f4e-7016-4e2d-80ff-d23e3b915beb", "Set_Bool", Relay_True_37))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_37.True(out logic_uScriptAct_SetBool_Target_37);
				local_PlayerHoldingBlock_System_Boolean = logic_uScriptAct_SetBool_Target_37;
				bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_37.SetTrue;
				bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_37.SetFalse;
				if (setTrue)
				{
					Relay_In_8();
				}
				if (setFalse)
				{
					Relay_In_16();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_37()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("fcd74f4e-7016-4e2d-80ff-d23e3b915beb", "Set_Bool", Relay_False_37))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_37.False(out logic_uScriptAct_SetBool_Target_37);
				local_PlayerHoldingBlock_System_Boolean = logic_uScriptAct_SetBool_Target_37;
				bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_37.SetTrue;
				bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_37.SetFalse;
				if (setTrue)
				{
					Relay_In_8();
				}
				if (setFalse)
				{
					Relay_In_16();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_42()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4b940688-c74c-492b-a29c-5009e75a8d8c", "Invert_Bool", Relay_In_42))
			{
				logic_uScriptAct_InvertBool_Target_42 = local_PlayerHoldingBlock_System_Boolean;
				logic_uScriptAct_InvertBool_uScriptAct_InvertBool_42.In(logic_uScriptAct_InvertBool_Target_42, out logic_uScriptAct_InvertBool_Value_42);
				local_43_System_Boolean = logic_uScriptAct_InvertBool_Value_42;
				if (logic_uScriptAct_InvertBool_uScriptAct_InvertBool_42.Out)
				{
					Relay_In_20();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript at Invert Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void UpdateEditorValues()
	{
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript:14", local_14_TankBlockArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("3aa49403-1d41-4ed5-84d0-1fd5f943a019", local_14_TankBlockArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript:Block", local_Block_TankBlock);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("33225bb7-c5fd-4eec-a03d-3095af022bf7", local_Block_TankBlock);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript:GhostBlock", local_GhostBlock_TankBlock);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("0b5fe92d-35fe-45a3-a3f9-8db9ff3cf624", local_GhostBlock_TankBlock);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript:PlayerHoldingBlock", local_PlayerHoldingBlock_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("4da63ae0-1fad-4af2-b471-700e0356a924", local_PlayerHoldingBlock_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("SubGraph_Crafting_Tutorial_AttachBlockToBase.uscript:43", local_43_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("77bf69f7-50cb-43a8-955b-e37f654a9c3e", local_43_System_Boolean);
	}

	private bool CheckDebugBreak(string guid, string name, ContinueExecution method)
	{
		if (m_Breakpoint)
		{
			return true;
		}
		if (uScript_MasterComponent.FindBreakpoint(guid))
		{
			if (!(uScript_MasterComponent.LatestMasterComponent.CurrentBreakpoint == guid))
			{
				uScript_MasterComponent.LatestMasterComponent.CurrentBreakpoint = guid;
				UpdateEditorValues();
				Debug.Log(("uScript BREAK Node:" + name + " ((Time: " + Time.time) ?? "");
				Debug.Break();
				m_ContinueExecution = method.Invoke;
				m_Breakpoint = true;
				return true;
			}
			uScript_MasterComponent.LatestMasterComponent.CurrentBreakpoint = "";
		}
		return false;
	}
}
