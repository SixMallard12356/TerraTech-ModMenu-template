using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_2_Crafting_01")]
public class Mission_GSO_2_Crafting_01_Component : uScriptCode
{
	public Mission_GSO_2_Crafting_01 ExposedVariables = new Mission_GSO_2_Crafting_01();

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

	public ChunkTypes resourceTypeRefined
	{
		get
		{
			return ExposedVariables.resourceTypeRefined;
		}
		set
		{
			ExposedVariables.resourceTypeRefined = value;
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

	public GhostBlockSpawnData[] ghostBlockRefinery
	{
		get
		{
			return ExposedVariables.ghostBlockRefinery;
		}
		set
		{
			ExposedVariables.ghostBlockRefinery = value;
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

	public GhostBlockSpawnData[] ghostBlockConveyor01
	{
		get
		{
			return ExposedVariables.ghostBlockConveyor01;
		}
		set
		{
			ExposedVariables.ghostBlockConveyor01 = value;
		}
	}

	public GhostBlockSpawnData[] ghostBlockConveyor02
	{
		get
		{
			return ExposedVariables.ghostBlockConveyor02;
		}
		set
		{
			ExposedVariables.ghostBlockConveyor02 = value;
		}
	}

	public GhostBlockSpawnData[] ghostBlockConveyor04
	{
		get
		{
			return ExposedVariables.ghostBlockConveyor04;
		}
		set
		{
			ExposedVariables.ghostBlockConveyor04 = value;
		}
	}

	public GhostBlockSpawnData[] ghostBlockConveyor03
	{
		get
		{
			return ExposedVariables.ghostBlockConveyor03;
		}
		set
		{
			ExposedVariables.ghostBlockConveyor03 = value;
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

	public BlockTypes blockTypeConveyor
	{
		get
		{
			return ExposedVariables.blockTypeConveyor;
		}
		set
		{
			ExposedVariables.blockTypeConveyor = value;
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

	public float timeWaitBeforeResourceSold
	{
		get
		{
			return ExposedVariables.timeWaitBeforeResourceSold;
		}
		set
		{
			ExposedVariables.timeWaitBeforeResourceSold = value;
		}
	}

	public SpawnBlockData[] blockSpawnDataRefinery
	{
		get
		{
			return ExposedVariables.blockSpawnDataRefinery;
		}
		set
		{
			ExposedVariables.blockSpawnDataRefinery = value;
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

	public uScript_AddMessage.MessageData msg03AttachConveyors
	{
		get
		{
			return ExposedVariables.msg03AttachConveyors;
		}
		set
		{
			ExposedVariables.msg03AttachConveyors = value;
		}
	}

	public uScript_AddMessage.MessageData msg04ConveyorsAttached
	{
		get
		{
			return ExposedVariables.msg04ConveyorsAttached;
		}
		set
		{
			ExposedVariables.msg04ConveyorsAttached = value;
		}
	}

	public uScript_AddMessage.MessageData msg05AttachReceiver
	{
		get
		{
			return ExposedVariables.msg05AttachReceiver;
		}
		set
		{
			ExposedVariables.msg05AttachReceiver = value;
		}
	}

	public uScript_AddMessage.MessageData msg06ReceiverAttached
	{
		get
		{
			return ExposedVariables.msg06ReceiverAttached;
		}
		set
		{
			ExposedVariables.msg06ReceiverAttached = value;
		}
	}

	public uScript_AddMessage.MessageData msg07UnrefinedChunksSold
	{
		get
		{
			return ExposedVariables.msg07UnrefinedChunksSold;
		}
		set
		{
			ExposedVariables.msg07UnrefinedChunksSold = value;
		}
	}

	public uScript_AddMessage.MessageData msg08RefinerySpawned
	{
		get
		{
			return ExposedVariables.msg08RefinerySpawned;
		}
		set
		{
			ExposedVariables.msg08RefinerySpawned = value;
		}
	}

	public uScript_AddMessage.MessageData msg09AttachRefinery
	{
		get
		{
			return ExposedVariables.msg09AttachRefinery;
		}
		set
		{
			ExposedVariables.msg09AttachRefinery = value;
		}
	}

	public uScript_AddMessage.MessageData msg10RefineryAttached
	{
		get
		{
			return ExposedVariables.msg10RefineryAttached;
		}
		set
		{
			ExposedVariables.msg10RefineryAttached = value;
		}
	}

	public uScript_AddMessage.MessageData msg11Complete
	{
		get
		{
			return ExposedVariables.msg11Complete;
		}
		set
		{
			ExposedVariables.msg11Complete = value;
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

	public ChunkTypes[] resourceList
	{
		get
		{
			return ExposedVariables.resourceList;
		}
		set
		{
			ExposedVariables.resourceList = value;
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
