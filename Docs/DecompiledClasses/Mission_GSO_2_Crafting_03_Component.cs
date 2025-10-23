using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_2_Crafting_03")]
public class Mission_GSO_2_Crafting_03_Component : uScriptCode
{
	public Mission_GSO_2_Crafting_03 ExposedVariables = new Mission_GSO_2_Crafting_03();

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

	public ItemTypeInfo ResourceTypeToFilterWood
	{
		get
		{
			return ExposedVariables.ResourceTypeToFilterWood;
		}
		set
		{
			ExposedVariables.ResourceTypeToFilterWood = value;
		}
	}

	public GhostBlockSpawnData[] ghostBlockFilter
	{
		get
		{
			return ExposedVariables.ghostBlockFilter;
		}
		set
		{
			ExposedVariables.ghostBlockFilter = value;
		}
	}

	public GhostBlockSpawnData[] ghostBlockReceiver
	{
		get
		{
			return ExposedVariables.ghostBlockReceiver;
		}
		set
		{
			ExposedVariables.ghostBlockReceiver = value;
		}
	}

	public GhostBlockSpawnData[] ghostBlockGenerator
	{
		get
		{
			return ExposedVariables.ghostBlockGenerator;
		}
		set
		{
			ExposedVariables.ghostBlockGenerator = value;
		}
	}

	public ChunkTypes filterResourceType
	{
		get
		{
			return ExposedVariables.filterResourceType;
		}
		set
		{
			ExposedVariables.filterResourceType = value;
		}
	}

	public int filterResourceAmount
	{
		get
		{
			return ExposedVariables.filterResourceAmount;
		}
		set
		{
			ExposedVariables.filterResourceAmount = value;
		}
	}

	public string resourceSpawnPos
	{
		get
		{
			return ExposedVariables.resourceSpawnPos;
		}
		set
		{
			ExposedVariables.resourceSpawnPos = value;
		}
	}

	public BlockTypes shieldBlockType
	{
		get
		{
			return ExposedVariables.shieldBlockType;
		}
		set
		{
			ExposedVariables.shieldBlockType = value;
		}
	}

	public float timeWaitForResourcesInSilo
	{
		get
		{
			return ExposedVariables.timeWaitForResourcesInSilo;
		}
		set
		{
			ExposedVariables.timeWaitForResourcesInSilo = value;
		}
	}

	public SpawnBlockData[] blockSpawnDataGenerator
	{
		get
		{
			return ExposedVariables.blockSpawnDataGenerator;
		}
		set
		{
			ExposedVariables.blockSpawnDataGenerator = value;
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

	public uScript_AddMessage.MessageData msg03AttachFilter
	{
		get
		{
			return ExposedVariables.msg03AttachFilter;
		}
		set
		{
			ExposedVariables.msg03AttachFilter = value;
		}
	}

	public uScript_AddMessage.MessageData msg04FilterAttached
	{
		get
		{
			return ExposedVariables.msg04FilterAttached;
		}
		set
		{
			ExposedVariables.msg04FilterAttached = value;
		}
	}

	public uScript_AddMessage.MessageData msg05SetFilter
	{
		get
		{
			return ExposedVariables.msg05SetFilter;
		}
		set
		{
			ExposedVariables.msg05SetFilter = value;
		}
	}

	public uScript_AddMessage.MessageData msg06FilterSet
	{
		get
		{
			return ExposedVariables.msg06FilterSet;
		}
		set
		{
			ExposedVariables.msg06FilterSet = value;
		}
	}

	public uScript_AddMessage.MessageData msg07GeneratorSpawned
	{
		get
		{
			return ExposedVariables.msg07GeneratorSpawned;
		}
		set
		{
			ExposedVariables.msg07GeneratorSpawned = value;
		}
	}

	public uScript_AddMessage.MessageData msg08AttachGenerator
	{
		get
		{
			return ExposedVariables.msg08AttachGenerator;
		}
		set
		{
			ExposedVariables.msg08AttachGenerator = value;
		}
	}

	public uScript_AddMessage.MessageData msg09GeneratorAttached
	{
		get
		{
			return ExposedVariables.msg09GeneratorAttached;
		}
		set
		{
			ExposedVariables.msg09GeneratorAttached = value;
		}
	}

	public uScript_AddMessage.MessageData msg10AttachReceiver
	{
		get
		{
			return ExposedVariables.msg10AttachReceiver;
		}
		set
		{
			ExposedVariables.msg10AttachReceiver = value;
		}
	}

	public uScript_AddMessage.MessageData msg11ReceiverAttached
	{
		get
		{
			return ExposedVariables.msg11ReceiverAttached;
		}
		set
		{
			ExposedVariables.msg11ReceiverAttached = value;
		}
	}

	public uScript_AddMessage.MessageData msg12FilterExplanation
	{
		get
		{
			return ExposedVariables.msg12FilterExplanation;
		}
		set
		{
			ExposedVariables.msg12FilterExplanation = value;
		}
	}

	public uScript_AddMessage.MessageData msg13FuelExplanation
	{
		get
		{
			return ExposedVariables.msg13FuelExplanation;
		}
		set
		{
			ExposedVariables.msg13FuelExplanation = value;
		}
	}

	public uScript_AddMessage.MessageData msg15SelectFuelChunks
	{
		get
		{
			return ExposedVariables.msg15SelectFuelChunks;
		}
		set
		{
			ExposedVariables.msg15SelectFuelChunks = value;
		}
	}

	public uScript_AddMessage.MessageData msg16FilterSetToFuel
	{
		get
		{
			return ExposedVariables.msg16FilterSetToFuel;
		}
		set
		{
			ExposedVariables.msg16FilterSetToFuel = value;
		}
	}

	public uScript_AddMessage.MessageData msg17Complete
	{
		get
		{
			return ExposedVariables.msg17Complete;
		}
		set
		{
			ExposedVariables.msg17Complete = value;
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

	public ChunkTypes[] resourceListWoods
	{
		get
		{
			return ExposedVariables.resourceListWoods;
		}
		set
		{
			ExposedVariables.resourceListWoods = value;
		}
	}

	public ChunkTypes[] resourceListFuels
	{
		get
		{
			return ExposedVariables.resourceListFuels;
		}
		set
		{
			ExposedVariables.resourceListFuels = value;
		}
	}

	public uScript_AddMessage.MessageData msg07aFeedGenerator
	{
		get
		{
			return ExposedVariables.msg07aFeedGenerator;
		}
		set
		{
			ExposedVariables.msg07aFeedGenerator = value;
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

	public uScript_AddMessage.MessageData msg05SetFilter_Pad
	{
		get
		{
			return ExposedVariables.msg05SetFilter_Pad;
		}
		set
		{
			ExposedVariables.msg05SetFilter_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg15SelectFuelChunks_Pad
	{
		get
		{
			return ExposedVariables.msg15SelectFuelChunks_Pad;
		}
		set
		{
			ExposedVariables.msg15SelectFuelChunks_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg14OpenFilterMenu
	{
		get
		{
			return ExposedVariables.msg14OpenFilterMenu;
		}
		set
		{
			ExposedVariables.msg14OpenFilterMenu = value;
		}
	}

	public uScript_AddMessage.MessageData msg14OpenFilterMenu_Pad1
	{
		get
		{
			return ExposedVariables.msg14OpenFilterMenu_Pad1;
		}
		set
		{
			ExposedVariables.msg14OpenFilterMenu_Pad1 = value;
		}
	}

	public uScript_AddMessage.MessageData msg14OpenFilterMenu_Pad2
	{
		get
		{
			return ExposedVariables.msg14OpenFilterMenu_Pad2;
		}
		set
		{
			ExposedVariables.msg14OpenFilterMenu_Pad2 = value;
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
