using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_GSO_1_1_CoOpFindTradingStation : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private Tank local_GSOVendor_Tank;

	private Vector3 local_VendorPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_10;

	private GameObject owner_Connection_14;

	private uScript_FindNearestVendor logic_uScript_FindNearestVendor_uScript_FindNearestVendor_1 = new uScript_FindNearestVendor();

	private Tank logic_uScript_FindNearestVendor_Return_1;

	private bool logic_uScript_FindNearestVendor_Out_1 = true;

	private bool logic_uScript_FindNearestVendor_Returned_1 = true;

	private bool logic_uScript_FindNearestVendor_NotReturned_1 = true;

	private uScript_SetEncounterPosition logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_2 = new uScript_SetEncounterPosition();

	private GameObject logic_uScript_SetEncounterPosition_ownerNode_2;

	private Vector3 logic_uScript_SetEncounterPosition_position_2;

	private bool logic_uScript_SetEncounterPosition_Out_2 = true;

	private uScript_GetNearestVendorPos logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_4 = new uScript_GetNearestVendorPos();

	private Vector3 logic_uScript_GetNearestVendorPos_Return_4;

	private bool logic_uScript_GetNearestVendorPos_Out_4 = true;

	private bool logic_uScript_GetNearestVendorPos_Found_4 = true;

	private bool logic_uScript_GetNearestVendorPos_Missing_4 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_9 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_9;

	private object logic_uScript_SetEncounterTarget_visibleObject_9 = "";

	private bool logic_uScript_SetEncounterTarget_Out_9 = true;

	private uScript_HasAcceptedAnyMissionFromBoard logic_uScript_HasAcceptedAnyMissionFromBoard_uScript_HasAcceptedAnyMissionFromBoard_12 = new uScript_HasAcceptedAnyMissionFromBoard();

	private bool logic_uScript_HasAcceptedAnyMissionFromBoard_Out_12 = true;

	private bool logic_uScript_HasAcceptedAnyMissionFromBoard_WaitingForAccept_12 = true;

	private bool logic_uScript_HasAcceptedAnyMissionFromBoard_AnyMissionAccepted_12 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_13 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_13;

	private bool logic_uScript_FinishEncounter_Out_13 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_6 || !m_RegisteredForEvents)
		{
			owner_Connection_6 = parentGameObject;
		}
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
			if (null != owner_Connection_8)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_8.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_8.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
				}
			}
		}
		if (null == owner_Connection_10 || !m_RegisteredForEvents)
		{
			owner_Connection_10 = parentGameObject;
		}
		if (null == owner_Connection_14 || !m_RegisteredForEvents)
		{
			owner_Connection_14 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_8)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_8.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_8.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_8)
		{
			uScript_EncounterUpdate component = owner_Connection_8.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_0;
				component.OnSuspend -= Instance_OnSuspend_0;
				component.OnResume -= Instance_OnResume_0;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_FindNearestVendor_uScript_FindNearestVendor_1.SetParent(g);
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_2.SetParent(g);
		logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_4.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_9.SetParent(g);
		logic_uScript_HasAcceptedAnyMissionFromBoard_uScript_HasAcceptedAnyMissionFromBoard_12.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_13.SetParent(g);
		owner_Connection_6 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_10 = parentGameObject;
		owner_Connection_14 = parentGameObject;
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
		logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_4.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_HasAcceptedAnyMissionFromBoard_uScript_HasAcceptedAnyMissionFromBoard_12.OnDisable();
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

	private void Instance_OnUpdate_0(object o, EventArgs e)
	{
		Relay_OnUpdate_0();
	}

	private void Instance_OnSuspend_0(object o, EventArgs e)
	{
		Relay_OnSuspend_0();
	}

	private void Instance_OnResume_0(object o, EventArgs e)
	{
		Relay_OnResume_0();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_4();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
	}

	private void Relay_In_1()
	{
		logic_uScript_FindNearestVendor_Return_1 = logic_uScript_FindNearestVendor_uScript_FindNearestVendor_1.In();
		local_GSOVendor_Tank = logic_uScript_FindNearestVendor_Return_1;
		if (logic_uScript_FindNearestVendor_uScript_FindNearestVendor_1.Returned)
		{
			Relay_In_9();
		}
	}

	private void Relay_In_2()
	{
		logic_uScript_SetEncounterPosition_ownerNode_2 = owner_Connection_6;
		logic_uScript_SetEncounterPosition_position_2 = local_VendorPos_UnityEngine_Vector3;
		logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_2.In(logic_uScript_SetEncounterPosition_ownerNode_2, logic_uScript_SetEncounterPosition_position_2);
		if (logic_uScript_SetEncounterPosition_uScript_SetEncounterPosition_2.Out)
		{
			Relay_In_1();
		}
	}

	private void Relay_In_4()
	{
		logic_uScript_GetNearestVendorPos_Return_4 = logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_4.In();
		local_VendorPos_UnityEngine_Vector3 = logic_uScript_GetNearestVendorPos_Return_4;
		if (logic_uScript_GetNearestVendorPos_uScript_GetNearestVendorPos_4.Found)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_9()
	{
		logic_uScript_SetEncounterTarget_owner_9 = owner_Connection_10;
		logic_uScript_SetEncounterTarget_visibleObject_9 = local_GSOVendor_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_9.In(logic_uScript_SetEncounterTarget_owner_9, logic_uScript_SetEncounterTarget_visibleObject_9);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_9.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_12()
	{
		logic_uScript_HasAcceptedAnyMissionFromBoard_uScript_HasAcceptedAnyMissionFromBoard_12.In();
		if (logic_uScript_HasAcceptedAnyMissionFromBoard_uScript_HasAcceptedAnyMissionFromBoard_12.AnyMissionAccepted)
		{
			Relay_Succeed_13();
		}
	}

	private void Relay_Succeed_13()
	{
		logic_uScript_FinishEncounter_owner_13 = owner_Connection_14;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_13.Succeed(logic_uScript_FinishEncounter_owner_13);
	}

	private void Relay_Fail_13()
	{
		logic_uScript_FinishEncounter_owner_13 = owner_Connection_14;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_13.Fail(logic_uScript_FinishEncounter_owner_13);
	}
}
