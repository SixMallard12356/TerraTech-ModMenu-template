using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_2_TradingStationSCU")]
public class Mission_GSO_2_TradingStationSCU_Component : uScriptCode
{
	public Mission_GSO_2_TradingStationSCU ExposedVariables = new Mission_GSO_2_TradingStationSCU();

	public uScript_AddMessage.MessageData msg01aDefeatEnemies
	{
		get
		{
			return ExposedVariables.msg01aDefeatEnemies;
		}
		set
		{
			ExposedVariables.msg01aDefeatEnemies = value;
		}
	}

	public uScript_AddMessage.MessageData msg02EnemiesDefeated
	{
		get
		{
			return ExposedVariables.msg02EnemiesDefeated;
		}
		set
		{
			ExposedVariables.msg02EnemiesDefeated = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker messageSpeakerTurret
	{
		get
		{
			return ExposedVariables.messageSpeakerTurret;
		}
		set
		{
			ExposedVariables.messageSpeakerTurret = value;
		}
	}

	public uScript_AddMessage.MessageData msg05AllyOrdered
	{
		get
		{
			return ExposedVariables.msg05AllyOrdered;
		}
		set
		{
			ExposedVariables.msg05AllyOrdered = value;
		}
	}

	public uScript_AddMessage.MessageData msg03TurretIdle
	{
		get
		{
			return ExposedVariables.msg03TurretIdle;
		}
		set
		{
			ExposedVariables.msg03TurretIdle = value;
		}
	}

	public uScript_AddMessage.MessageData msg01bDefeatEnemies
	{
		get
		{
			return ExposedVariables.msg01bDefeatEnemies;
		}
		set
		{
			ExposedVariables.msg01bDefeatEnemies = value;
		}
	}

	public string messageTag
	{
		get
		{
			return ExposedVariables.messageTag;
		}
		set
		{
			ExposedVariables.messageTag = value;
		}
	}

	public uScript_AddMessage.MessageData msg06SCUEnabled
	{
		get
		{
			return ExposedVariables.msg06SCUEnabled;
		}
		set
		{
			ExposedVariables.msg06SCUEnabled = value;
		}
	}

	public uScript_AddMessage.MessageData msg08MissionComplete
	{
		get
		{
			return ExposedVariables.msg08MissionComplete;
		}
		set
		{
			ExposedVariables.msg08MissionComplete = value;
		}
	}

	public SpawnTechData[] enemyTechData
	{
		get
		{
			return ExposedVariables.enemyTechData;
		}
		set
		{
			ExposedVariables.enemyTechData = value;
		}
	}

	public SpawnTechData[] alliedTechData
	{
		get
		{
			return ExposedVariables.alliedTechData;
		}
		set
		{
			ExposedVariables.alliedTechData = value;
		}
	}

	public float clearSceneryRadius
	{
		get
		{
			return ExposedVariables.clearSceneryRadius;
		}
		set
		{
			ExposedVariables.clearSceneryRadius = value;
		}
	}

	public float tradingStationFoundDist
	{
		get
		{
			return ExposedVariables.tradingStationFoundDist;
		}
		set
		{
			ExposedVariables.tradingStationFoundDist = value;
		}
	}

	public uScript_AddMessage.MessageData msg04aOpenAIMenu
	{
		get
		{
			return ExposedVariables.msg04aOpenAIMenu;
		}
		set
		{
			ExposedVariables.msg04aOpenAIMenu = value;
		}
	}

	public uScript_AddMessage.MessageData msg04aOpenAIMenu_Pad2
	{
		get
		{
			return ExposedVariables.msg04aOpenAIMenu_Pad2;
		}
		set
		{
			ExposedVariables.msg04aOpenAIMenu_Pad2 = value;
		}
	}

	public uScript_AddMessage.MessageData msg04bSelectGuard_Pad
	{
		get
		{
			return ExposedVariables.msg04bSelectGuard_Pad;
		}
		set
		{
			ExposedVariables.msg04bSelectGuard_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg04bSelectGuard
	{
		get
		{
			return ExposedVariables.msg04bSelectGuard;
		}
		set
		{
			ExposedVariables.msg04bSelectGuard = value;
		}
	}

	public uScript_AddMessage.MessageData msg04aOpenAIMenu_Pad1
	{
		get
		{
			return ExposedVariables.msg04aOpenAIMenu_Pad1;
		}
		set
		{
			ExposedVariables.msg04aOpenAIMenu_Pad1 = value;
		}
	}

	public uScript_AddMessage.MessageData msg07OpenInventory_Pad1
	{
		get
		{
			return ExposedVariables.msg07OpenInventory_Pad1;
		}
		set
		{
			ExposedVariables.msg07OpenInventory_Pad1 = value;
		}
	}

	public uScript_AddMessage.MessageData msg07OpenInventory
	{
		get
		{
			return ExposedVariables.msg07OpenInventory;
		}
		set
		{
			ExposedVariables.msg07OpenInventory = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker messageSpeakerDefault
	{
		get
		{
			return ExposedVariables.messageSpeakerDefault;
		}
		set
		{
			ExposedVariables.messageSpeakerDefault = value;
		}
	}

	public uScript_AddMessage.MessageData msg07OpenInventory_Pad2
	{
		get
		{
			return ExposedVariables.msg07OpenInventory_Pad2;
		}
		set
		{
			ExposedVariables.msg07OpenInventory_Pad2 = value;
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
