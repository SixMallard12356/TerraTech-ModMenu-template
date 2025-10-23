using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_TakeBlockFromEnemyTech : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public BlockTypes blockToGet;

	public SpawnTechData enemyTechData;

	public LocalisedString[] msgAttachBlock = new LocalisedString[0];

	public LocalisedString[] msgAttackTheEnemy = new LocalisedString[0];

	public LocalisedString[] msgBlockDestroyed = new LocalisedString[0];

	public LocalisedString[] msgEnemyDroppedBlock = new LocalisedString[0];

	public LocalisedString[] msgEnemySpotted = new LocalisedString[0];

	public LocalisedString[] msgPickUpBlock = new LocalisedString[0];

	public LocalisedString[] msgPlayerHasBlock = new LocalisedString[0];

	public LocalisedString QLAttachBlock;

	public LocalisedString QLAttackEnemy;

	private GameObject owner_Connection_12;

	private GameObject owner_Connection_27;

	private SubGraph_TakeBlockFromEnemyTech logic_SubGraph_TakeBlockFromEnemyTech_SubGraph_TakeBlockFromEnemyTech_0 = new SubGraph_TakeBlockFromEnemyTech();

	private BlockTypes logic_SubGraph_TakeBlockFromEnemyTech_blockToGet_0;

	private LocalisedString[] logic_SubGraph_TakeBlockFromEnemyTech_msgEnemySpotted_0 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_TakeBlockFromEnemyTech_msgAttackTheEnemy_0 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_TakeBlockFromEnemyTech_msgEnemyDroppedBlock_0 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_TakeBlockFromEnemyTech_msgBlockDestroyed_0 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_TakeBlockFromEnemyTech_msgPickUpBlock_0 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_TakeBlockFromEnemyTech_msgAttachBlock_0 = new LocalisedString[0];

	private LocalisedString[] logic_SubGraph_TakeBlockFromEnemyTech_msgPlayerHasBlock_0 = new LocalisedString[0];

	private SpawnTechData[] logic_SubGraph_TakeBlockFromEnemyTech_enemyTechData_0 = new SpawnTechData[0];

	private TankBlock logic_SubGraph_TakeBlockFromEnemyTech_ReturnedTargetBlock_0;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_28 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_28;

	private bool logic_uScript_FinishEncounter_Out_28 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_12 || !m_RegisteredForEvents)
		{
			owner_Connection_12 = parentGameObject;
			if (null != owner_Connection_12)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_12.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_12.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_13;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_13;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_13;
				}
			}
		}
		if (null == owner_Connection_27 || !m_RegisteredForEvents)
		{
			owner_Connection_27 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_12)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_12.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_12.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_13;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_13;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_13;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_12)
		{
			uScript_EncounterUpdate component = owner_Connection_12.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_13;
				component.OnSuspend -= Instance_OnSuspend_13;
				component.OnResume -= Instance_OnResume_13;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_SubGraph_TakeBlockFromEnemyTech_SubGraph_TakeBlockFromEnemyTech_0.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_28.SetParent(g);
		owner_Connection_12 = parentGameObject;
		owner_Connection_27 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_TakeBlockFromEnemyTech_SubGraph_TakeBlockFromEnemyTech_0.Awake();
		logic_SubGraph_TakeBlockFromEnemyTech_SubGraph_TakeBlockFromEnemyTech_0.Complete += SubGraph_TakeBlockFromEnemyTech_Complete_0;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_TakeBlockFromEnemyTech_SubGraph_TakeBlockFromEnemyTech_0.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_TakeBlockFromEnemyTech_SubGraph_TakeBlockFromEnemyTech_0.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_TakeBlockFromEnemyTech_SubGraph_TakeBlockFromEnemyTech_0.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_TakeBlockFromEnemyTech_SubGraph_TakeBlockFromEnemyTech_0.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_TakeBlockFromEnemyTech_SubGraph_TakeBlockFromEnemyTech_0.OnDestroy();
		logic_SubGraph_TakeBlockFromEnemyTech_SubGraph_TakeBlockFromEnemyTech_0.Complete -= SubGraph_TakeBlockFromEnemyTech_Complete_0;
	}

	private void Instance_OnUpdate_13(object o, EventArgs e)
	{
		Relay_OnUpdate_13();
	}

	private void Instance_OnSuspend_13(object o, EventArgs e)
	{
		Relay_OnSuspend_13();
	}

	private void Instance_OnResume_13(object o, EventArgs e)
	{
		Relay_OnResume_13();
	}

	private void SubGraph_TakeBlockFromEnemyTech_Complete_0(object o, SubGraph_TakeBlockFromEnemyTech.LogicEventArgs e)
	{
		logic_SubGraph_TakeBlockFromEnemyTech_ReturnedTargetBlock_0 = e.ReturnedTargetBlock;
		Relay_Complete_0();
	}

	private void Relay_Complete_0()
	{
		Relay_Succeed_28();
	}

	private void Relay_In_0()
	{
		logic_SubGraph_TakeBlockFromEnemyTech_blockToGet_0 = blockToGet;
		int num = 0;
		Array array = msgEnemySpotted;
		if (logic_SubGraph_TakeBlockFromEnemyTech_msgEnemySpotted_0.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_TakeBlockFromEnemyTech_msgEnemySpotted_0, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_TakeBlockFromEnemyTech_msgEnemySpotted_0, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array array2 = msgAttackTheEnemy;
		if (logic_SubGraph_TakeBlockFromEnemyTech_msgAttackTheEnemy_0.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_TakeBlockFromEnemyTech_msgAttackTheEnemy_0, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_TakeBlockFromEnemyTech_msgAttackTheEnemy_0, num2, array2.Length);
		num2 += array2.Length;
		int num3 = 0;
		Array array3 = msgEnemyDroppedBlock;
		if (logic_SubGraph_TakeBlockFromEnemyTech_msgEnemyDroppedBlock_0.Length != num3 + array3.Length)
		{
			Array.Resize(ref logic_SubGraph_TakeBlockFromEnemyTech_msgEnemyDroppedBlock_0, num3 + array3.Length);
		}
		Array.Copy(array3, 0, logic_SubGraph_TakeBlockFromEnemyTech_msgEnemyDroppedBlock_0, num3, array3.Length);
		num3 += array3.Length;
		int num4 = 0;
		Array array4 = msgBlockDestroyed;
		if (logic_SubGraph_TakeBlockFromEnemyTech_msgBlockDestroyed_0.Length != num4 + array4.Length)
		{
			Array.Resize(ref logic_SubGraph_TakeBlockFromEnemyTech_msgBlockDestroyed_0, num4 + array4.Length);
		}
		Array.Copy(array4, 0, logic_SubGraph_TakeBlockFromEnemyTech_msgBlockDestroyed_0, num4, array4.Length);
		num4 += array4.Length;
		int num5 = 0;
		Array array5 = msgPickUpBlock;
		if (logic_SubGraph_TakeBlockFromEnemyTech_msgPickUpBlock_0.Length != num5 + array5.Length)
		{
			Array.Resize(ref logic_SubGraph_TakeBlockFromEnemyTech_msgPickUpBlock_0, num5 + array5.Length);
		}
		Array.Copy(array5, 0, logic_SubGraph_TakeBlockFromEnemyTech_msgPickUpBlock_0, num5, array5.Length);
		num5 += array5.Length;
		int num6 = 0;
		Array array6 = msgAttachBlock;
		if (logic_SubGraph_TakeBlockFromEnemyTech_msgAttachBlock_0.Length != num6 + array6.Length)
		{
			Array.Resize(ref logic_SubGraph_TakeBlockFromEnemyTech_msgAttachBlock_0, num6 + array6.Length);
		}
		Array.Copy(array6, 0, logic_SubGraph_TakeBlockFromEnemyTech_msgAttachBlock_0, num6, array6.Length);
		num6 += array6.Length;
		int num7 = 0;
		Array array7 = msgPlayerHasBlock;
		if (logic_SubGraph_TakeBlockFromEnemyTech_msgPlayerHasBlock_0.Length != num7 + array7.Length)
		{
			Array.Resize(ref logic_SubGraph_TakeBlockFromEnemyTech_msgPlayerHasBlock_0, num7 + array7.Length);
		}
		Array.Copy(array7, 0, logic_SubGraph_TakeBlockFromEnemyTech_msgPlayerHasBlock_0, num7, array7.Length);
		num7 += array7.Length;
		int num8 = 0;
		if (logic_SubGraph_TakeBlockFromEnemyTech_enemyTechData_0.Length <= num8)
		{
			Array.Resize(ref logic_SubGraph_TakeBlockFromEnemyTech_enemyTechData_0, num8 + 1);
		}
		logic_SubGraph_TakeBlockFromEnemyTech_enemyTechData_0[num8++] = enemyTechData;
		logic_SubGraph_TakeBlockFromEnemyTech_SubGraph_TakeBlockFromEnemyTech_0.In(logic_SubGraph_TakeBlockFromEnemyTech_blockToGet_0, logic_SubGraph_TakeBlockFromEnemyTech_msgEnemySpotted_0, logic_SubGraph_TakeBlockFromEnemyTech_msgAttackTheEnemy_0, logic_SubGraph_TakeBlockFromEnemyTech_msgEnemyDroppedBlock_0, logic_SubGraph_TakeBlockFromEnemyTech_msgBlockDestroyed_0, logic_SubGraph_TakeBlockFromEnemyTech_msgPickUpBlock_0, logic_SubGraph_TakeBlockFromEnemyTech_msgAttachBlock_0, logic_SubGraph_TakeBlockFromEnemyTech_msgPlayerHasBlock_0, logic_SubGraph_TakeBlockFromEnemyTech_enemyTechData_0);
	}

	private void Relay_OnUpdate_13()
	{
		Relay_In_0();
	}

	private void Relay_OnSuspend_13()
	{
	}

	private void Relay_OnResume_13()
	{
	}

	private void Relay_Succeed_28()
	{
		logic_uScript_FinishEncounter_owner_28 = owner_Connection_27;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_28.Succeed(logic_uScript_FinishEncounter_owner_28);
	}

	private void Relay_Fail_28()
	{
		logic_uScript_FinishEncounter_owner_28 = owner_Connection_27;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_28.Fail(logic_uScript_FinishEncounter_owner_28);
	}
}
