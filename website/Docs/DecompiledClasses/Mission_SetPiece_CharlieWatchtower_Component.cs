using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_CharlieWatchtower")]
public class Mission_SetPiece_CharlieWatchtower_Component : uScriptCode
{
	public Mission_SetPiece_CharlieWatchtower ExposedVariables = new Mission_SetPiece_CharlieWatchtower();

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

	public LocalisedString[] msgTechNearBoss
	{
		get
		{
			return ExposedVariables.msgTechNearBoss;
		}
		set
		{
			ExposedVariables.msgTechNearBoss = value;
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

	public LocalisedString[] msgNPCGreeting
	{
		get
		{
			return ExposedVariables.msgNPCGreeting;
		}
		set
		{
			ExposedVariables.msgNPCGreeting = value;
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

	public string IntroMsg
	{
		get
		{
			return ExposedVariables.IntroMsg;
		}
		set
		{
			ExposedVariables.IntroMsg = value;
		}
	}

	public BlockTypes Special_Shield_Data
	{
		get
		{
			return ExposedVariables.Special_Shield_Data;
		}
		set
		{
			ExposedVariables.Special_Shield_Data = value;
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
