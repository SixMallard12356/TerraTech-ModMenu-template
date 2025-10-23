using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_UnlockHawkeyeLicense")]
public class Mission_UnlockHawkeyeLicense_Component : uScriptCode
{
	public Mission_UnlockHawkeyeLicense ExposedVariables = new Mission_UnlockHawkeyeLicense();

	public Transform craterPrefab
	{
		get
		{
			return ExposedVariables.craterPrefab;
		}
		set
		{
			ExposedVariables.craterPrefab = value;
		}
	}

	public GameObject animPrefabToInstantiate
	{
		get
		{
			return ExposedVariables.animPrefabToInstantiate;
		}
		set
		{
			ExposedVariables.animPrefabToInstantiate = value;
		}
	}

	public string animStateToCheck
	{
		get
		{
			return ExposedVariables.animStateToCheck;
		}
		set
		{
			ExposedVariables.animStateToCheck = value;
		}
	}

	public Transform explosionPrefab
	{
		get
		{
			return ExposedVariables.explosionPrefab;
		}
		set
		{
			ExposedVariables.explosionPrefab = value;
		}
	}

	public Transform smokestackPrefab
	{
		get
		{
			return ExposedVariables.smokestackPrefab;
		}
		set
		{
			ExposedVariables.smokestackPrefab = value;
		}
	}

	public float distCrashAnimTriggered
	{
		get
		{
			return ExposedVariables.distCrashAnimTriggered;
		}
		set
		{
			ExposedVariables.distCrashAnimTriggered = value;
		}
	}

	public float sceneryRemovalRadius
	{
		get
		{
			return ExposedVariables.sceneryRemovalRadius;
		}
		set
		{
			ExposedVariables.sceneryRemovalRadius = value;
		}
	}

	public SpawnTechData[] crashedTechData
	{
		get
		{
			return ExposedVariables.crashedTechData;
		}
		set
		{
			ExposedVariables.crashedTechData = value;
		}
	}

	public float looseBlockDamagePercent
	{
		get
		{
			return ExposedVariables.looseBlockDamagePercent;
		}
		set
		{
			ExposedVariables.looseBlockDamagePercent = value;
		}
	}

	public SpawnBlockData[] crashedBlocksData
	{
		get
		{
			return ExposedVariables.crashedBlocksData;
		}
		set
		{
			ExposedVariables.crashedBlocksData = value;
		}
	}

	public float distCrashedTechBlowUp
	{
		get
		{
			return ExposedVariables.distCrashedTechBlowUp;
		}
		set
		{
			ExposedVariables.distCrashedTechBlowUp = value;
		}
	}

	public string animSpawnPos
	{
		get
		{
			return ExposedVariables.animSpawnPos;
		}
		set
		{
			ExposedVariables.animSpawnPos = value;
		}
	}

	public float distCrashedTechSpotted
	{
		get
		{
			return ExposedVariables.distCrashedTechSpotted;
		}
		set
		{
			ExposedVariables.distCrashedTechSpotted = value;
		}
	}

	public float enemySpawnDelay
	{
		get
		{
			return ExposedVariables.enemySpawnDelay;
		}
		set
		{
			ExposedVariables.enemySpawnDelay = value;
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

	public uScript_AddMessage.MessageData msg01MissionStart
	{
		get
		{
			return ExposedVariables.msg01MissionStart;
		}
		set
		{
			ExposedVariables.msg01MissionStart = value;
		}
	}

	public uScript_AddMessage.MessageData msg02TechCrashing
	{
		get
		{
			return ExposedVariables.msg02TechCrashing;
		}
		set
		{
			ExposedVariables.msg02TechCrashing = value;
		}
	}

	public uScript_AddMessage.MessageData msg03CrashedTechFound
	{
		get
		{
			return ExposedVariables.msg03CrashedTechFound;
		}
		set
		{
			ExposedVariables.msg03CrashedTechFound = value;
		}
	}

	public uScript_AddMessage.MessageData msg04CrashedTechBlownUp
	{
		get
		{
			return ExposedVariables.msg04CrashedTechBlownUp;
		}
		set
		{
			ExposedVariables.msg04CrashedTechBlownUp = value;
		}
	}

	public uScript_AddMessage.MessageData msg05EnemiesIncoming
	{
		get
		{
			return ExposedVariables.msg05EnemiesIncoming;
		}
		set
		{
			ExposedVariables.msg05EnemiesIncoming = value;
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
