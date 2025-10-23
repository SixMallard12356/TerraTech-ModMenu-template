using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_DefendFriendlyTech")]
public class Mission_DefendFriendlyTech_Component : uScriptCode
{
	public Mission_DefendFriendlyTech ExposedVariables = new Mission_DefendFriendlyTech();

	public LocalisedString[] msgFriendlyTechFound
	{
		get
		{
			return ExposedVariables.msgFriendlyTechFound;
		}
		set
		{
			ExposedVariables.msgFriendlyTechFound = value;
		}
	}

	public LocalisedString[] msgMissionComplete
	{
		get
		{
			return ExposedVariables.msgMissionComplete;
		}
		set
		{
			ExposedVariables.msgMissionComplete = value;
		}
	}

	public SpawnTechData[] friendlyTechData
	{
		get
		{
			return ExposedVariables.friendlyTechData;
		}
		set
		{
			ExposedVariables.friendlyTechData = value;
		}
	}

	public LocalisedString[] msgMissionFailedDistance
	{
		get
		{
			return ExposedVariables.msgMissionFailedDistance;
		}
		set
		{
			ExposedVariables.msgMissionFailedDistance = value;
		}
	}

	public LocalisedString[] msgLeavingMissionArea
	{
		get
		{
			return ExposedVariables.msgLeavingMissionArea;
		}
		set
		{
			ExposedVariables.msgLeavingMissionArea = value;
		}
	}

	public LocalisedString[] msgMissionFailedDeath
	{
		get
		{
			return ExposedVariables.msgMissionFailedDeath;
		}
		set
		{
			ExposedVariables.msgMissionFailedDeath = value;
		}
	}

	public SpawnTechData enemyGroupData
	{
		get
		{
			return ExposedVariables.enemyGroupData;
		}
		set
		{
			ExposedVariables.enemyGroupData = value;
		}
	}

	public float DelayBetweenEnemyGroupSpaws
	{
		get
		{
			return ExposedVariables.DelayBetweenEnemyGroupSpaws;
		}
		set
		{
			ExposedVariables.DelayBetweenEnemyGroupSpaws = value;
		}
	}

	public int ReinforcementSubGroupSize
	{
		get
		{
			return ExposedVariables.ReinforcementSubGroupSize;
		}
		set
		{
			ExposedVariables.ReinforcementSubGroupSize = value;
		}
	}

	public float DelayBetweenRespawnArivals
	{
		get
		{
			return ExposedVariables.DelayBetweenRespawnArivals;
		}
		set
		{
			ExposedVariables.DelayBetweenRespawnArivals = value;
		}
	}

	public LocalisedString[] msgEnemiesIncoming
	{
		get
		{
			return ExposedVariables.msgEnemiesIncoming;
		}
		set
		{
			ExposedVariables.msgEnemiesIncoming = value;
		}
	}

	public LocalisedString[] msgEnemiesGivingUp
	{
		get
		{
			return ExposedVariables.msgEnemiesGivingUp;
		}
		set
		{
			ExposedVariables.msgEnemiesGivingUp = value;
		}
	}

	public bool AllowEnemyGroupToRespawn
	{
		get
		{
			return ExposedVariables.AllowEnemyGroupToRespawn;
		}
		set
		{
			ExposedVariables.AllowEnemyGroupToRespawn = value;
		}
	}

	public LocalisedString[] msgDistressSignal
	{
		get
		{
			return ExposedVariables.msgDistressSignal;
		}
		set
		{
			ExposedVariables.msgDistressSignal = value;
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

	public string clearSceneryPos
	{
		get
		{
			return ExposedVariables.clearSceneryPos;
		}
		set
		{
			ExposedVariables.clearSceneryPos = value;
		}
	}

	public WaveSizeSpecification EnemyGroupSize
	{
		get
		{
			return ExposedVariables.EnemyGroupSize;
		}
		set
		{
			ExposedVariables.EnemyGroupSize = value;
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
