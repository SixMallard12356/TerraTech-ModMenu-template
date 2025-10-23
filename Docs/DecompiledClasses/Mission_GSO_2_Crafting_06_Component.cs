using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_2_Crafting_06")]
public class Mission_GSO_2_Crafting_06_Component : uScriptCode
{
	public Mission_GSO_2_Crafting_06 ExposedVariables = new Mission_GSO_2_Crafting_06();

	public float distBaseFound
	{
		get
		{
			return ExposedVariables.distBaseFound;
		}
		set
		{
			ExposedVariables.distBaseFound = value;
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

	public SpawnBlockData[] blockSpawnDataAutoMiner
	{
		get
		{
			return ExposedVariables.blockSpawnDataAutoMiner;
		}
		set
		{
			ExposedVariables.blockSpawnDataAutoMiner = value;
		}
	}

	public uScript_AddMessage.MessageData msg01Intro
	{
		get
		{
			return ExposedVariables.msg01Intro;
		}
		set
		{
			ExposedVariables.msg01Intro = value;
		}
	}

	public uScript_AddMessage.MessageData msg04aSeamUncoveredEarly
	{
		get
		{
			return ExposedVariables.msg04aSeamUncoveredEarly;
		}
		set
		{
			ExposedVariables.msg04aSeamUncoveredEarly = value;
		}
	}

	public uScript_AddMessage.MessageData msg05AutoMinerSpawned
	{
		get
		{
			return ExposedVariables.msg05AutoMinerSpawned;
		}
		set
		{
			ExposedVariables.msg05AutoMinerSpawned = value;
		}
	}

	public uScript_AddMessage.MessageData msg06AnchorAutoMiner
	{
		get
		{
			return ExposedVariables.msg06AnchorAutoMiner;
		}
		set
		{
			ExposedVariables.msg06AnchorAutoMiner = value;
		}
	}

	public uScript_AddMessage.MessageData msgBlockOutsideArea
	{
		get
		{
			return ExposedVariables.msgBlockOutsideArea;
		}
		set
		{
			ExposedVariables.msgBlockOutsideArea = value;
		}
	}

	public uScript_AddMessage.MessageData msgLeavingMissionArea
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

	public string basePosition
	{
		get
		{
			return ExposedVariables.basePosition;
		}
		set
		{
			ExposedVariables.basePosition = value;
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

	public SpawnBlockData[] blockSpawnData
	{
		get
		{
			return ExposedVariables.blockSpawnData;
		}
		set
		{
			ExposedVariables.blockSpawnData = value;
		}
	}

	public SpawnTechData[] NPCSpawnData
	{
		get
		{
			return ExposedVariables.NPCSpawnData;
		}
		set
		{
			ExposedVariables.NPCSpawnData = value;
		}
	}

	public uScript_AddMessage.MessageData msg07Complete
	{
		get
		{
			return ExposedVariables.msg07Complete;
		}
		set
		{
			ExposedVariables.msg07Complete = value;
		}
	}

	public uScript_AddMessage.MessageData msg02BaseFound
	{
		get
		{
			return ExposedVariables.msg02BaseFound;
		}
		set
		{
			ExposedVariables.msg02BaseFound = value;
		}
	}

	public uScript_AddMessage.MessageData msg04SeamUncovered
	{
		get
		{
			return ExposedVariables.msg04SeamUncovered;
		}
		set
		{
			ExposedVariables.msg04SeamUncovered = value;
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

	public uScript_AddMessage.MessageData msg03UncoverSeam
	{
		get
		{
			return ExposedVariables.msg03UncoverSeam;
		}
		set
		{
			ExposedVariables.msg03UncoverSeam = value;
		}
	}

	public TerrainObject resourceSeamPrefab
	{
		get
		{
			return ExposedVariables.resourceSeamPrefab;
		}
		set
		{
			ExposedVariables.resourceSeamPrefab = value;
		}
	}

	public float scenerySpawnDistanceMax
	{
		get
		{
			return ExposedVariables.scenerySpawnDistanceMax;
		}
		set
		{
			ExposedVariables.scenerySpawnDistanceMax = value;
		}
	}

	public int numSceneryObjectsToSpawn
	{
		get
		{
			return ExposedVariables.numSceneryObjectsToSpawn;
		}
		set
		{
			ExposedVariables.numSceneryObjectsToSpawn = value;
		}
	}

	public TerrainObject sceneryPrefab
	{
		get
		{
			return ExposedVariables.sceneryPrefab;
		}
		set
		{
			ExposedVariables.sceneryPrefab = value;
		}
	}

	public float scenerySpawnDistanceMin
	{
		get
		{
			return ExposedVariables.scenerySpawnDistanceMin;
		}
		set
		{
			ExposedVariables.scenerySpawnDistanceMin = value;
		}
	}

	public ExternalBehaviorTree NPCFlyAwayAI
	{
		get
		{
			return ExposedVariables.NPCFlyAwayAI;
		}
		set
		{
			ExposedVariables.NPCFlyAwayAI = value;
		}
	}

	public Transform NPCDespawnParticleEffect
	{
		get
		{
			return ExposedVariables.NPCDespawnParticleEffect;
		}
		set
		{
			ExposedVariables.NPCDespawnParticleEffect = value;
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
