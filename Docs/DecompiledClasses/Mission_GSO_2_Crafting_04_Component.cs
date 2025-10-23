using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_2_Crafting_04")]
public class Mission_GSO_2_Crafting_04_Component : uScriptCode
{
	public Mission_GSO_2_Crafting_04 ExposedVariables = new Mission_GSO_2_Crafting_04();

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

	public GhostBlockSpawnData[] ghostBlockScrapper
	{
		get
		{
			return ExposedVariables.ghostBlockScrapper;
		}
		set
		{
			ExposedVariables.ghostBlockScrapper = value;
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

	public SpawnBlockData[] blockSpawnDataScrapper
	{
		get
		{
			return ExposedVariables.blockSpawnDataScrapper;
		}
		set
		{
			ExposedVariables.blockSpawnDataScrapper = value;
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

	public uScript_AddMessage.MessageData msg03ScrapperSpawned
	{
		get
		{
			return ExposedVariables.msg03ScrapperSpawned;
		}
		set
		{
			ExposedVariables.msg03ScrapperSpawned = value;
		}
	}

	public uScript_AddMessage.MessageData msg04AttachScrapper
	{
		get
		{
			return ExposedVariables.msg04AttachScrapper;
		}
		set
		{
			ExposedVariables.msg04AttachScrapper = value;
		}
	}

	public uScript_AddMessage.MessageData msg05ScrapperAttached
	{
		get
		{
			return ExposedVariables.msg05ScrapperAttached;
		}
		set
		{
			ExposedVariables.msg05ScrapperAttached = value;
		}
	}

	public uScript_AddMessage.MessageData msg06PutBlockInScrapper
	{
		get
		{
			return ExposedVariables.msg06PutBlockInScrapper;
		}
		set
		{
			ExposedVariables.msg06PutBlockInScrapper = value;
		}
	}

	public uScript_AddMessage.MessageData msg07ScrappingInProgress
	{
		get
		{
			return ExposedVariables.msg07ScrappingInProgress;
		}
		set
		{
			ExposedVariables.msg07ScrappingInProgress = value;
		}
	}

	public uScript_AddMessage.MessageData msg08Complete
	{
		get
		{
			return ExposedVariables.msg08Complete;
		}
		set
		{
			ExposedVariables.msg08Complete = value;
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

	public TankPreset completedBasePreset
	{
		get
		{
			return ExposedVariables.completedBasePreset;
		}
		set
		{
			ExposedVariables.completedBasePreset = value;
		}
	}

	public SpawnTechData[] baseSpawnData
	{
		get
		{
			return ExposedVariables.baseSpawnData;
		}
		set
		{
			ExposedVariables.baseSpawnData = value;
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
