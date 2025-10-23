using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_2_Crafting_05")]
public class Mission_GSO_2_Crafting_05_Component : uScriptCode
{
	public Mission_GSO_2_Crafting_05 ExposedVariables = new Mission_GSO_2_Crafting_05();

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

	public BlockTypes blockTypeToCraft02
	{
		get
		{
			return ExposedVariables.blockTypeToCraft02;
		}
		set
		{
			ExposedVariables.blockTypeToCraft02 = value;
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

	public BlockTypes blockTypeSilo
	{
		get
		{
			return ExposedVariables.blockTypeSilo;
		}
		set
		{
			ExposedVariables.blockTypeSilo = value;
		}
	}

	public BlockTypes blockTypeToCraft01
	{
		get
		{
			return ExposedVariables.blockTypeToCraft01;
		}
		set
		{
			ExposedVariables.blockTypeToCraft01 = value;
		}
	}

	public uScript_IsBlockHoldingResources.ResourceQuantity[] resourcesToHoldInSilo
	{
		get
		{
			return ExposedVariables.resourcesToHoldInSilo;
		}
		set
		{
			ExposedVariables.resourcesToHoldInSilo = value;
		}
	}

	public ChunkTypes[] baseAllowedResourceTypes
	{
		get
		{
			return ExposedVariables.baseAllowedResourceTypes;
		}
		set
		{
			ExposedVariables.baseAllowedResourceTypes = value;
		}
	}

	public BlockTypes blockTypeFabricator
	{
		get
		{
			return ExposedVariables.blockTypeFabricator;
		}
		set
		{
			ExposedVariables.blockTypeFabricator = value;
		}
	}

	public BlockCategories blockTypeToCraftCategory
	{
		get
		{
			return ExposedVariables.blockTypeToCraftCategory;
		}
		set
		{
			ExposedVariables.blockTypeToCraftCategory = value;
		}
	}

	public float timeRepeatResourcesReminder
	{
		get
		{
			return ExposedVariables.timeRepeatResourcesReminder;
		}
		set
		{
			ExposedVariables.timeRepeatResourcesReminder = value;
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

	public uScript_AddMessage.MessageData msg03ResourcesReminder
	{
		get
		{
			return ExposedVariables.msg03ResourcesReminder;
		}
		set
		{
			ExposedVariables.msg03ResourcesReminder = value;
		}
	}

	public uScript_AddMessage.MessageData msg04ResourcesInSilo
	{
		get
		{
			return ExposedVariables.msg04ResourcesInSilo;
		}
		set
		{
			ExposedVariables.msg04ResourcesInSilo = value;
		}
	}

	public uScript_AddMessage.MessageData msg05AttachRefinery
	{
		get
		{
			return ExposedVariables.msg05AttachRefinery;
		}
		set
		{
			ExposedVariables.msg05AttachRefinery = value;
		}
	}

	public uScript_AddMessage.MessageData msg06CraftBlock01
	{
		get
		{
			return ExposedVariables.msg06CraftBlock01;
		}
		set
		{
			ExposedVariables.msg06CraftBlock01 = value;
		}
	}

	public uScript_AddMessage.MessageData msg07RefineryExplanation01
	{
		get
		{
			return ExposedVariables.msg07RefineryExplanation01;
		}
		set
		{
			ExposedVariables.msg07RefineryExplanation01 = value;
		}
	}

	public uScript_AddMessage.MessageData msg08CraftBlock02
	{
		get
		{
			return ExposedVariables.msg08CraftBlock02;
		}
		set
		{
			ExposedVariables.msg08CraftBlock02 = value;
		}
	}

	public uScript_AddMessage.MessageData msg09RefineryExplanation02
	{
		get
		{
			return ExposedVariables.msg09RefineryExplanation02;
		}
		set
		{
			ExposedVariables.msg09RefineryExplanation02 = value;
		}
	}

	public uScript_AddMessage.MessageData msg10Complete
	{
		get
		{
			return ExposedVariables.msg10Complete;
		}
		set
		{
			ExposedVariables.msg10Complete = value;
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

	public ItemTypeInfo blockTypeToHighlight01
	{
		get
		{
			return ExposedVariables.blockTypeToHighlight01;
		}
		set
		{
			ExposedVariables.blockTypeToHighlight01 = value;
		}
	}

	public ItemTypeInfo blockTypeToHighlight02
	{
		get
		{
			return ExposedVariables.blockTypeToHighlight02;
		}
		set
		{
			ExposedVariables.blockTypeToHighlight02 = value;
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
