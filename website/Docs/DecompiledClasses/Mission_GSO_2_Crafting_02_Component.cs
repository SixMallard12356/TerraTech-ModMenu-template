using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_2_Crafting_02")]
public class Mission_GSO_2_Crafting_02_Component : uScriptCode
{
	public Mission_GSO_2_Crafting_02 ExposedVariables = new Mission_GSO_2_Crafting_02();

	public BlockTypes blockTypeToCraft
	{
		get
		{
			return ExposedVariables.blockTypeToCraft;
		}
		set
		{
			ExposedVariables.blockTypeToCraft = value;
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

	public float timeWaitForChunksInSilos
	{
		get
		{
			return ExposedVariables.timeWaitForChunksInSilos;
		}
		set
		{
			ExposedVariables.timeWaitForChunksInSilos = value;
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

	public SpawnBlockData[] blockSpawnDataFabricator
	{
		get
		{
			return ExposedVariables.blockSpawnDataFabricator;
		}
		set
		{
			ExposedVariables.blockSpawnDataFabricator = value;
		}
	}

	public GhostBlockSpawnData[] ghostBlockFabricator
	{
		get
		{
			return ExposedVariables.ghostBlockFabricator;
		}
		set
		{
			ExposedVariables.ghostBlockFabricator = value;
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

	public uScript_AddMessage.MessageData msg03AttachReceiver
	{
		get
		{
			return ExposedVariables.msg03AttachReceiver;
		}
		set
		{
			ExposedVariables.msg03AttachReceiver = value;
		}
	}

	public uScript_AddMessage.MessageData msg04ReceiverAttached
	{
		get
		{
			return ExposedVariables.msg04ReceiverAttached;
		}
		set
		{
			ExposedVariables.msg04ReceiverAttached = value;
		}
	}

	public uScript_AddMessage.MessageData msg05SiloExplanation
	{
		get
		{
			return ExposedVariables.msg05SiloExplanation;
		}
		set
		{
			ExposedVariables.msg05SiloExplanation = value;
		}
	}

	public uScript_AddMessage.MessageData msg06AttachConveyors
	{
		get
		{
			return ExposedVariables.msg06AttachConveyors;
		}
		set
		{
			ExposedVariables.msg06AttachConveyors = value;
		}
	}

	public uScript_AddMessage.MessageData msg07ConveyorsAttached
	{
		get
		{
			return ExposedVariables.msg07ConveyorsAttached;
		}
		set
		{
			ExposedVariables.msg07ConveyorsAttached = value;
		}
	}

	public uScript_AddMessage.MessageData msg08FabricatorSpawned
	{
		get
		{
			return ExposedVariables.msg08FabricatorSpawned;
		}
		set
		{
			ExposedVariables.msg08FabricatorSpawned = value;
		}
	}

	public uScript_AddMessage.MessageData msg09AttachFabricator
	{
		get
		{
			return ExposedVariables.msg09AttachFabricator;
		}
		set
		{
			ExposedVariables.msg09AttachFabricator = value;
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

	public uScript_AddMessage.MessageData msg12Complete
	{
		get
		{
			return ExposedVariables.msg12Complete;
		}
		set
		{
			ExposedVariables.msg12Complete = value;
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

	public ItemTypeInfo blockTypeToHighlight
	{
		get
		{
			return ExposedVariables.blockTypeToHighlight;
		}
		set
		{
			ExposedVariables.blockTypeToHighlight = value;
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

	public uScript_AddMessage.MessageData msg10OpenMenu
	{
		get
		{
			return ExposedVariables.msg10OpenMenu;
		}
		set
		{
			ExposedVariables.msg10OpenMenu = value;
		}
	}

	public uScript_AddMessage.MessageData msg10OpenMenu_Pad
	{
		get
		{
			return ExposedVariables.msg10OpenMenu_Pad;
		}
		set
		{
			ExposedVariables.msg10OpenMenu_Pad = value;
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

	public uScript_AddMessage.MessageData msg11CraftBlock_Pad
	{
		get
		{
			return ExposedVariables.msg11CraftBlock_Pad;
		}
		set
		{
			ExposedVariables.msg11CraftBlock_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg11CraftBlock
	{
		get
		{
			return ExposedVariables.msg11CraftBlock;
		}
		set
		{
			ExposedVariables.msg11CraftBlock = value;
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
