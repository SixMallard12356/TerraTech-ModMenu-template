using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_SamSiteRidge_02")]
public class Mission_SetPiece_SamSiteRidge_02_Component : uScriptCode
{
	public Mission_SetPiece_SamSiteRidge_02 ExposedVariables = new Mission_SetPiece_SamSiteRidge_02();

	public LocalisedString[] msgComplete
	{
		get
		{
			return ExposedVariables.msgComplete;
		}
		set
		{
			ExposedVariables.msgComplete = value;
		}
	}

	public SpawnTechData[] BossTechData
	{
		get
		{
			return ExposedVariables.BossTechData;
		}
		set
		{
			ExposedVariables.BossTechData = value;
		}
	}

	public LocalisedString[] msgTechDeadCharger1
	{
		get
		{
			return ExposedVariables.msgTechDeadCharger1;
		}
		set
		{
			ExposedVariables.msgTechDeadCharger1 = value;
		}
	}

	public string Trigger1
	{
		get
		{
			return ExposedVariables.Trigger1;
		}
		set
		{
			ExposedVariables.Trigger1 = value;
		}
	}

	public LocalisedString[] msgTechNearCharger1
	{
		get
		{
			return ExposedVariables.msgTechNearCharger1;
		}
		set
		{
			ExposedVariables.msgTechNearCharger1 = value;
		}
	}

	public string Trigger2
	{
		get
		{
			return ExposedVariables.Trigger2;
		}
		set
		{
			ExposedVariables.Trigger2 = value;
		}
	}

	public LocalisedString[] msgTechDeadCharger2
	{
		get
		{
			return ExposedVariables.msgTechDeadCharger2;
		}
		set
		{
			ExposedVariables.msgTechDeadCharger2 = value;
		}
	}

	public LocalisedString[] msgTechNearCharger2
	{
		get
		{
			return ExposedVariables.msgTechNearCharger2;
		}
		set
		{
			ExposedVariables.msgTechNearCharger2 = value;
		}
	}

	public string Trigger3
	{
		get
		{
			return ExposedVariables.Trigger3;
		}
		set
		{
			ExposedVariables.Trigger3 = value;
		}
	}

	public LocalisedString[] msgTechNearCharger3
	{
		get
		{
			return ExposedVariables.msgTechNearCharger3;
		}
		set
		{
			ExposedVariables.msgTechNearCharger3 = value;
		}
	}

	public SpawnTechData[] ChargerTech3
	{
		get
		{
			return ExposedVariables.ChargerTech3;
		}
		set
		{
			ExposedVariables.ChargerTech3 = value;
		}
	}

	public LocalisedString[] msgTechDeadCharger3
	{
		get
		{
			return ExposedVariables.msgTechDeadCharger3;
		}
		set
		{
			ExposedVariables.msgTechDeadCharger3 = value;
		}
	}

	public SpawnTechData[] ChargerTech2
	{
		get
		{
			return ExposedVariables.ChargerTech2;
		}
		set
		{
			ExposedVariables.ChargerTech2 = value;
		}
	}

	public SpawnTechData[] ChargerTech1
	{
		get
		{
			return ExposedVariables.ChargerTech1;
		}
		set
		{
			ExposedVariables.ChargerTech1 = value;
		}
	}

	public LocalisedString[] msgTechDeadChargersAll
	{
		get
		{
			return ExposedVariables.msgTechDeadChargersAll;
		}
		set
		{
			ExposedVariables.msgTechDeadChargersAll = value;
		}
	}

	public LocalisedString[] msgAproachedSAMEarly
	{
		get
		{
			return ExposedVariables.msgAproachedSAMEarly;
		}
		set
		{
			ExposedVariables.msgAproachedSAMEarly = value;
		}
	}

	public LocalisedString[] msgFoundMissionArea
	{
		get
		{
			return ExposedVariables.msgFoundMissionArea;
		}
		set
		{
			ExposedVariables.msgFoundMissionArea = value;
		}
	}

	public ManOnScreenMessages.Speaker messageSpeaker
	{
		get
		{
			return ExposedVariables.messageSpeaker;
		}
		set
		{
			ExposedVariables.messageSpeaker = value;
		}
	}

	public BlockTypes SpecialShieldBlockData
	{
		get
		{
			return ExposedVariables.SpecialShieldBlockData;
		}
		set
		{
			ExposedVariables.SpecialShieldBlockData = value;
		}
	}

	public string Trigger7
	{
		get
		{
			return ExposedVariables.Trigger7;
		}
		set
		{
			ExposedVariables.Trigger7 = value;
		}
	}

	public SpawnTechData[] MobsData1
	{
		get
		{
			return ExposedVariables.MobsData1;
		}
		set
		{
			ExposedVariables.MobsData1 = value;
		}
	}

	public string Trigger12
	{
		get
		{
			return ExposedVariables.Trigger12;
		}
		set
		{
			ExposedVariables.Trigger12 = value;
		}
	}

	public SpawnTechData[] MobsData2
	{
		get
		{
			return ExposedVariables.MobsData2;
		}
		set
		{
			ExposedVariables.MobsData2 = value;
		}
	}

	public string Trigger11
	{
		get
		{
			return ExposedVariables.Trigger11;
		}
		set
		{
			ExposedVariables.Trigger11 = value;
		}
	}

	public SpawnTechData[] MobsData3
	{
		get
		{
			return ExposedVariables.MobsData3;
		}
		set
		{
			ExposedVariables.MobsData3 = value;
		}
	}

	public string Trigger10
	{
		get
		{
			return ExposedVariables.Trigger10;
		}
		set
		{
			ExposedVariables.Trigger10 = value;
		}
	}

	public SpawnTechData[] MobsData4
	{
		get
		{
			return ExposedVariables.MobsData4;
		}
		set
		{
			ExposedVariables.MobsData4 = value;
		}
	}

	public SpawnTechData[] MobsDataTrigger8
	{
		get
		{
			return ExposedVariables.MobsDataTrigger8;
		}
		set
		{
			ExposedVariables.MobsDataTrigger8 = value;
		}
	}

	public SpawnTechData[] MobsDataTrigger9
	{
		get
		{
			return ExposedVariables.MobsDataTrigger9;
		}
		set
		{
			ExposedVariables.MobsDataTrigger9 = value;
		}
	}

	public string Trigger6
	{
		get
		{
			return ExposedVariables.Trigger6;
		}
		set
		{
			ExposedVariables.Trigger6 = value;
		}
	}

	public string Trigger4
	{
		get
		{
			return ExposedVariables.Trigger4;
		}
		set
		{
			ExposedVariables.Trigger4 = value;
		}
	}

	public string Trigger5
	{
		get
		{
			return ExposedVariables.Trigger5;
		}
		set
		{
			ExposedVariables.Trigger5 = value;
		}
	}

	public SpawnTechData[] MobsDataTrigger5
	{
		get
		{
			return ExposedVariables.MobsDataTrigger5;
		}
		set
		{
			ExposedVariables.MobsDataTrigger5 = value;
		}
	}

	public SpawnTechData[] MobsDataTrigger4
	{
		get
		{
			return ExposedVariables.MobsDataTrigger4;
		}
		set
		{
			ExposedVariables.MobsDataTrigger4 = value;
		}
	}

	public SpawnTechData[] MobsDataTrigger6
	{
		get
		{
			return ExposedVariables.MobsDataTrigger6;
		}
		set
		{
			ExposedVariables.MobsDataTrigger6 = value;
		}
	}

	private void Awake()
	{
		base.useGUILayout = false;
		ExposedVariables.Awake();
		ExposedVariables.SetParent(base.gameObject);
		if ("1.CMR" != uScript_MasterComponent.Version)
		{
			uScriptDebug.Log("The generated code is not compatible with your current uScript Runtime " + uScript_MasterComponent.Version, uScriptDebug.Type.Error);
			ExposedVariables = null;
			Debug.Break();
		}
	}

	private void Start()
	{
		ExposedVariables.Start();
	}

	private void OnEnable()
	{
		ExposedVariables.OnEnable();
	}

	private void OnDisable()
	{
		ExposedVariables.OnDisable();
	}

	private void Update()
	{
		ExposedVariables.Update();
	}

	private void OnDestroy()
	{
		ExposedVariables.OnDestroy();
	}
}
