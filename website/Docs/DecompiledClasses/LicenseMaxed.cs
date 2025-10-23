using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class LicenseMaxed : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public FactionSubTypes corporation;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_3;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_2 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_2;

	private bool logic_uScript_FinishEncounter_Out_2 = true;

	private uScript_IsCorporationLicenseMaxed logic_uScript_IsCorporationLicenseMaxed_uScript_IsCorporationLicenseMaxed_6 = new uScript_IsCorporationLicenseMaxed();

	private FactionSubTypes logic_uScript_IsCorporationLicenseMaxed_corporation_6;

	private bool logic_uScript_IsCorporationLicenseMaxed_True_6 = true;

	private bool logic_uScript_IsCorporationLicenseMaxed_False_6 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
			if (null != owner_Connection_1)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_1.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_0;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_0;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_0;
				}
			}
		}
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_1)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_1.AddComponent<uScript_EncounterUpdate>();
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
		if (null != owner_Connection_1)
		{
			uScript_EncounterUpdate component = owner_Connection_1.GetComponent<uScript_EncounterUpdate>();
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
		logic_uScript_FinishEncounter_uScript_FinishEncounter_2.SetParent(g);
		logic_uScript_IsCorporationLicenseMaxed_uScript_IsCorporationLicenseMaxed_6.SetParent(g);
		owner_Connection_1 = parentGameObject;
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
		Relay_In_6();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
	}

	private void Relay_Succeed_2()
	{
		logic_uScript_FinishEncounter_owner_2 = owner_Connection_3;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_2.Succeed(logic_uScript_FinishEncounter_owner_2);
	}

	private void Relay_Fail_2()
	{
		logic_uScript_FinishEncounter_owner_2 = owner_Connection_3;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_2.Fail(logic_uScript_FinishEncounter_owner_2);
	}

	private void Relay_In_6()
	{
		logic_uScript_IsCorporationLicenseMaxed_corporation_6 = corporation;
		logic_uScript_IsCorporationLicenseMaxed_uScript_IsCorporationLicenseMaxed_6.In(logic_uScript_IsCorporationLicenseMaxed_corporation_6);
		if (logic_uScript_IsCorporationLicenseMaxed_uScript_IsCorporationLicenseMaxed_6.True)
		{
			Relay_Succeed_2();
		}
	}
}
