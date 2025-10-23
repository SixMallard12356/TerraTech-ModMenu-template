using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_1_6_FindTradingStation")]
public class Mission_GSO_1_6_FindTradingStation_Component : uScriptCode
{
	public Mission_GSO_1_6_FindTradingStation ExposedVariables = new Mission_GSO_1_6_FindTradingStation();

	public uScript_AddMessage.MessageData msg01FindTradingStation
	{
		get
		{
			return ExposedVariables.msg01FindTradingStation;
		}
		set
		{
			ExposedVariables.msg01FindTradingStation = value;
		}
	}

	public uScript_AddMessage.MessageData msg02TradingStationFound
	{
		get
		{
			return ExposedVariables.msg02TradingStationFound;
		}
		set
		{
			ExposedVariables.msg02TradingStationFound = value;
		}
	}

	public uScript_AddMessage.MessageData msg04DefeatTroll
	{
		get
		{
			return ExposedVariables.msg04DefeatTroll;
		}
		set
		{
			ExposedVariables.msg04DefeatTroll = value;
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

	public float TrollAchievementTimeSeconds
	{
		get
		{
			return ExposedVariables.TrollAchievementTimeSeconds;
		}
		set
		{
			ExposedVariables.TrollAchievementTimeSeconds = value;
		}
	}

	public float bossBatteryCharge
	{
		get
		{
			return ExposedVariables.bossBatteryCharge;
		}
		set
		{
			ExposedVariables.bossBatteryCharge = value;
		}
	}

	public SpawnTechData SpawnDataBoss
	{
		get
		{
			return ExposedVariables.SpawnDataBoss;
		}
		set
		{
			ExposedVariables.SpawnDataBoss = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker messageSpeaker
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

	public uScript_AddMessage.MessageData msg03TrollIncoming
	{
		get
		{
			return ExposedVariables.msg03TrollIncoming;
		}
		set
		{
			ExposedVariables.msg03TrollIncoming = value;
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
