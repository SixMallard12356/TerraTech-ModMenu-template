using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_RemoveBlockAfterDelay", "")]
public class SubGraph_RemoveBlockAfterDelay : uScriptLogic
{
	private delegate void ContinueExecution();

	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public TankBlock BlockResult;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private ContinueExecution m_ContinueExecution;

	private bool m_Breakpoint;

	private const int MaxRelayCallCount = 1000;

	private int relayCallCount;

	private TankBlock external_17;

	private float external_21;

	private Transform external_20;

	private TankBlock external_22;

	private TankBlock local_blockToRemove_TankBlock;

	private GameObject owner_Connection_0;

	private uScript_LockVisibleStackAccept logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_3 = new uScript_LockVisibleStackAccept();

	private object logic_uScript_LockVisibleStackAccept_targetObject_3 = "";

	private bool logic_uScript_LockVisibleStackAccept_Out_3 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_5 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_5;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_5 = Visible.LockTimerTypes.SendToSCU;

	private bool logic_uScript_LockBlock_Out_5 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_9 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_9;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_9 = Visible.LockTimerTypes.Grabbable;

	private bool logic_uScript_LockBlock_Out_9 = true;

	private uScript_CompareBlock logic_uScript_CompareBlock_uScript_CompareBlock_11 = new uScript_CompareBlock();

	private TankBlock logic_uScript_CompareBlock_A_11;

	private TankBlock logic_uScript_CompareBlock_B_11;

	private bool logic_uScript_CompareBlock_EqualTo_11 = true;

	private bool logic_uScript_CompareBlock_NotEqualTo_11 = true;

	private uScript_RecycleLooseBlock logic_uScript_RecycleLooseBlock_uScript_RecycleLooseBlock_12 = new uScript_RecycleLooseBlock();

	private GameObject logic_uScript_RecycleLooseBlock_owner_12;

	private TankBlock logic_uScript_RecycleLooseBlock_block_12;

	private Transform logic_uScript_RecycleLooseBlock_despawnParticleEffect_12;

	private Vector3 logic_uScript_RecycleLooseBlock_particleSpawnOffset_12 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_RecycleLooseBlock_Out_12 = true;

	private uScript_LockBlockAttach logic_uScript_LockBlockAttach_uScript_LockBlockAttach_13 = new uScript_LockBlockAttach();

	private TankBlock logic_uScript_LockBlockAttach_block_13;

	private bool logic_uScript_LockBlockAttach_Out_13 = true;

	private uScript_SetBlock logic_uScript_SetBlock_uScript_SetBlock_14 = new uScript_SetBlock();

	private TankBlock logic_uScript_SetBlock_Value_14;

	private TankBlock logic_uScript_SetBlock_TargetGameObject_14;

	private bool logic_uScript_SetBlock_Out_14 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_15 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_15;

	private bool logic_uScript_Wait_repeat_15 = true;

	private bool logic_uScript_Wait_Waited_15 = true;

	private uScript_CastBlock logic_uScript_CastBlock_uScript_CastBlock_18 = new uScript_CastBlock();

	private TankBlock logic_uScript_CastBlock_block_18;

	private TankBlock logic_uScript_CastBlock_outBlock_18;

	private bool logic_uScript_CastBlock_Out_18 = true;

	[FriendlyName("OutNoBlock")]
	public event uScriptEventHandler OutNoBlock;

	[FriendlyName("Out")]
	public event uScriptEventHandler Out;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
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
		logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_3.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_5.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_9.SetParent(g);
		logic_uScript_CompareBlock_uScript_CompareBlock_11.SetParent(g);
		logic_uScript_RecycleLooseBlock_uScript_RecycleLooseBlock_12.SetParent(g);
		logic_uScript_LockBlockAttach_uScript_LockBlockAttach_13.SetParent(g);
		logic_uScript_SetBlock_uScript_SetBlock_14.SetParent(g);
		logic_uScript_Wait_uScript_Wait_15.SetParent(g);
		logic_uScript_CastBlock_uScript_CastBlock_18.SetParent(g);
		owner_Connection_0 = parentGameObject;
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
		logic_uScript_RecycleLooseBlock_uScript_RecycleLooseBlock_12.OnDisable();
		logic_uScript_Wait_uScript_Wait_15.OnDisable();
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
	public void In([FriendlyName("BlockToRemove", "")] TankBlock BlockToRemove, [FriendlyName("TimeToWait", "")] float TimeToWait, [FriendlyName("DespawnParticlesTransform", "")] Transform DespawnParticlesTransform)
	{
		external_17 = BlockToRemove;
		external_21 = TimeToWait;
		external_20 = DespawnParticlesTransform;
		Relay_In_18();
	}

	private void Relay_In_3()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("41e57f42-cc63-46ae-b257-7962aca68fa0", "uScript_LockVisibleStackAccept", Relay_In_3))
			{
				logic_uScript_LockVisibleStackAccept_targetObject_3 = local_blockToRemove_TankBlock;
				logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_3.In(logic_uScript_LockVisibleStackAccept_targetObject_3);
				if (logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_3.Out)
				{
					Relay_In_9();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_RemoveBlockAfterDelay.uscript at uScript_LockVisibleStackAccept.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_5()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("63c8a0bd-d149-488a-8d55-703944de7c69", "Lock_Block_Functionality", Relay_In_5))
			{
				logic_uScript_LockBlock_block_5 = local_blockToRemove_TankBlock;
				logic_uScript_LockBlock_uScript_LockBlock_5.In(logic_uScript_LockBlock_block_5, logic_uScript_LockBlock_functionalityToLock_5);
				if (logic_uScript_LockBlock_uScript_LockBlock_5.Out)
				{
					Relay_In_15();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_RemoveBlockAfterDelay.uscript at Lock Block Functionality.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_9()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2a4876c7-5995-438b-93bf-0ff1a1b805c2", "Lock_Block_Functionality", Relay_In_9))
			{
				logic_uScript_LockBlock_block_9 = local_blockToRemove_TankBlock;
				logic_uScript_LockBlock_uScript_LockBlock_9.In(logic_uScript_LockBlock_block_9, logic_uScript_LockBlock_functionalityToLock_9);
				if (logic_uScript_LockBlock_uScript_LockBlock_9.Out)
				{
					Relay_In_5();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_RemoveBlockAfterDelay.uscript at Lock Block Functionality.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_11()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f80f98b6-7a09-498a-81a8-66b7088541b1", "Compare_Block", Relay_In_11))
			{
				logic_uScript_CompareBlock_A_11 = local_blockToRemove_TankBlock;
				logic_uScript_CompareBlock_uScript_CompareBlock_11.In(logic_uScript_CompareBlock_A_11, logic_uScript_CompareBlock_B_11);
				bool equalTo = logic_uScript_CompareBlock_uScript_CompareBlock_11.EqualTo;
				bool notEqualTo = logic_uScript_CompareBlock_uScript_CompareBlock_11.NotEqualTo;
				if (equalTo)
				{
					Relay_Connection_24();
				}
				if (notEqualTo)
				{
					Relay_In_13();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_RemoveBlockAfterDelay.uscript at Compare Block.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_12()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e6225b92-7f37-4127-8ec1-c3550f89f06d", "Recycle_Block", Relay_In_12))
			{
				logic_uScript_RecycleLooseBlock_owner_12 = owner_Connection_0;
				logic_uScript_RecycleLooseBlock_block_12 = local_blockToRemove_TankBlock;
				logic_uScript_RecycleLooseBlock_despawnParticleEffect_12 = external_20;
				logic_uScript_RecycleLooseBlock_uScript_RecycleLooseBlock_12.In(logic_uScript_RecycleLooseBlock_owner_12, logic_uScript_RecycleLooseBlock_block_12, logic_uScript_RecycleLooseBlock_despawnParticleEffect_12, logic_uScript_RecycleLooseBlock_particleSpawnOffset_12);
				if (logic_uScript_RecycleLooseBlock_uScript_RecycleLooseBlock_12.Out)
				{
					Relay_In_14();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_RemoveBlockAfterDelay.uscript at Recycle Block.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_13()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d2ab4290-0836-4088-a667-6a0679fff849", "uScript_LockBlockAttach", Relay_In_13))
			{
				logic_uScript_LockBlockAttach_block_13 = local_blockToRemove_TankBlock;
				logic_uScript_LockBlockAttach_uScript_LockBlockAttach_13.In(logic_uScript_LockBlockAttach_block_13);
				if (logic_uScript_LockBlockAttach_uScript_LockBlockAttach_13.Out)
				{
					Relay_In_3();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_RemoveBlockAfterDelay.uscript at uScript_LockBlockAttach.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_14()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b803d320-0938-4110-8861-d2e7aaa93457", "Set_Block", Relay_In_14))
			{
				logic_uScript_SetBlock_uScript_SetBlock_14.In(logic_uScript_SetBlock_Value_14, out logic_uScript_SetBlock_TargetGameObject_14);
				external_22 = logic_uScript_SetBlock_TargetGameObject_14;
				if (logic_uScript_SetBlock_uScript_SetBlock_14.Out)
				{
					Relay_Connection_23();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_RemoveBlockAfterDelay.uscript at Set Block.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_15()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7aac368a-85b2-412b-bc75-325ccad5a99f", "uScript_Wait", Relay_In_15))
			{
				logic_uScript_Wait_seconds_15 = external_21;
				logic_uScript_Wait_uScript_Wait_15.In(logic_uScript_Wait_seconds_15, logic_uScript_Wait_repeat_15);
				if (logic_uScript_Wait_uScript_Wait_15.Waited)
				{
					Relay_In_12();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_RemoveBlockAfterDelay.uscript at uScript_Wait.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Connection_16()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("a25168dd-b8d5-4538-a140-f442c21be349", "", Relay_Connection_16);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_RemoveBlockAfterDelay.uscript at In.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Connection_17()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("19525752-1ca0-4241-9184-3879cdef4de7", "", Relay_Connection_17);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_RemoveBlockAfterDelay.uscript at BlockToRemove.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_18()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6e57c54c-4b4f-45d3-b2c8-0ead9a30da93", "Cast_External_param_to_TankBlock", Relay_In_18))
			{
				logic_uScript_CastBlock_block_18 = external_17;
				logic_uScript_CastBlock_uScript_CastBlock_18.In(logic_uScript_CastBlock_block_18, out logic_uScript_CastBlock_outBlock_18);
				local_blockToRemove_TankBlock = logic_uScript_CastBlock_outBlock_18;
				if (logic_uScript_CastBlock_uScript_CastBlock_18.Out)
				{
					Relay_In_11();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_RemoveBlockAfterDelay.uscript at Cast External param to TankBlock.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Connection_20()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("7a8a379a-9f13-4401-a155-4e68480f6a6f", "", Relay_Connection_20);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_RemoveBlockAfterDelay.uscript at DespawnParticlesTransform.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Connection_21()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("170f8e74-fa14-44de-81c2-78e3e12cb8ef", "", Relay_Connection_21);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_RemoveBlockAfterDelay.uscript at TimeToWait.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Connection_22()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("38c1b392-49af-4b70-b86b-c0a8a511cb6c", "", Relay_Connection_22);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_RemoveBlockAfterDelay.uscript at BlockResult.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Connection_23()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("19cda25e-e1fa-463f-ad37-306928baeede", "", Relay_Connection_23) && this.Out != null)
			{
				LogicEventArgs e = new LogicEventArgs();
				e.BlockResult = external_22;
				this.Out(this, e);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_RemoveBlockAfterDelay.uscript at Out.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Connection_24()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("48731033-9c9f-4b25-bdcd-ce585433b5df", "", Relay_Connection_24) && this.OutNoBlock != null)
			{
				LogicEventArgs e = new LogicEventArgs();
				e.BlockResult = external_22;
				this.OutNoBlock(this, e);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript SubGraph_RemoveBlockAfterDelay.uscript at OutNoBlock.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void UpdateEditorValues()
	{
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("SubGraph_RemoveBlockAfterDelay.uscript:blockToRemove", local_blockToRemove_TankBlock);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("3e836fc6-bf03-4693-84e6-9152b6ce77fe", local_blockToRemove_TankBlock);
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
