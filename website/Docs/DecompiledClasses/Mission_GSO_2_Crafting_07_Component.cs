using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_2_Crafting_07")]
public class Mission_GSO_2_Crafting_07_Component : uScriptCode
{
	public Mission_GSO_2_Crafting_07 ExposedVariables = new Mission_GSO_2_Crafting_07();

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

	public uScript_AddMessage.MessageData msgLeavingMissionArea01
	{
		get
		{
			return ExposedVariables.msgLeavingMissionArea01;
		}
		set
		{
			ExposedVariables.msgLeavingMissionArea01 = value;
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

	public uScript_AddMessage.MessageData msg05OpenComponentMenu
	{
		get
		{
			return ExposedVariables.msg05OpenComponentMenu;
		}
		set
		{
			ExposedVariables.msg05OpenComponentMenu = value;
		}
	}

	public uScript_AddMessage.MessageData msg06CraftComponent
	{
		get
		{
			return ExposedVariables.msg06CraftComponent;
		}
		set
		{
			ExposedVariables.msg06CraftComponent = value;
		}
	}

	public uScript_AddMessage.MessageData msg07AttachBlock
	{
		get
		{
			return ExposedVariables.msg07AttachBlock;
		}
		set
		{
			ExposedVariables.msg07AttachBlock = value;
		}
	}

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

	public uScript_AddMessage.MessageData msg03OpenFabricatorMenu
	{
		get
		{
			return ExposedVariables.msg03OpenFabricatorMenu;
		}
		set
		{
			ExposedVariables.msg03OpenFabricatorMenu = value;
		}
	}

	public uScript_AddMessage.MessageData msg04aSelectBlockToCraft
	{
		get
		{
			return ExposedVariables.msg04aSelectBlockToCraft;
		}
		set
		{
			ExposedVariables.msg04aSelectBlockToCraft = value;
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

	public uScript_AddMessage.MessageData msg04bBlockRecipe
	{
		get
		{
			return ExposedVariables.msg04bBlockRecipe;
		}
		set
		{
			ExposedVariables.msg04bBlockRecipe = value;
		}
	}

	public uScript_AddMessage.MessageData msg04cCraftBlock
	{
		get
		{
			return ExposedVariables.msg04cCraftBlock;
		}
		set
		{
			ExposedVariables.msg04cCraftBlock = value;
		}
	}

	public GhostBlockSpawnData[] ghostBlock
	{
		get
		{
			return ExposedVariables.ghostBlock;
		}
		set
		{
			ExposedVariables.ghostBlock = value;
		}
	}

	public uScript_AddMessage.MessageData msg06bWorkOutHowToCraft
	{
		get
		{
			return ExposedVariables.msg06bWorkOutHowToCraft;
		}
		set
		{
			ExposedVariables.msg06bWorkOutHowToCraft = value;
		}
	}

	public uScript_AddMessage.MessageData msg09Reward
	{
		get
		{
			return ExposedVariables.msg09Reward;
		}
		set
		{
			ExposedVariables.msg09Reward = value;
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

	public ItemTypeInfo ComponentTypeToHighlight
	{
		get
		{
			return ExposedVariables.ComponentTypeToHighlight;
		}
		set
		{
			ExposedVariables.ComponentTypeToHighlight = value;
		}
	}

	public uScript_AddMessage.MessageData msg04dComponentExplanation
	{
		get
		{
			return ExposedVariables.msg04dComponentExplanation;
		}
		set
		{
			ExposedVariables.msg04dComponentExplanation = value;
		}
	}

	public uScript_AddMessage.MessageData msg06aComponentBeingCrafted
	{
		get
		{
			return ExposedVariables.msg06aComponentBeingCrafted;
		}
		set
		{
			ExposedVariables.msg06aComponentBeingCrafted = value;
		}
	}

	public float TEMP_TimeWaitForComponentCrafted
	{
		get
		{
			return ExposedVariables.TEMP_TimeWaitForComponentCrafted;
		}
		set
		{
			ExposedVariables.TEMP_TimeWaitForComponentCrafted = value;
		}
	}

	public float timeRepeatCraftingHint
	{
		get
		{
			return ExposedVariables.timeRepeatCraftingHint;
		}
		set
		{
			ExposedVariables.timeRepeatCraftingHint = value;
		}
	}

	public uScript_AddMessage.MessageData msgCraftingHint01
	{
		get
		{
			return ExposedVariables.msgCraftingHint01;
		}
		set
		{
			ExposedVariables.msgCraftingHint01 = value;
		}
	}

	public uScript_AddMessage.MessageData msgCraftingHint02
	{
		get
		{
			return ExposedVariables.msgCraftingHint02;
		}
		set
		{
			ExposedVariables.msgCraftingHint02 = value;
		}
	}

	public uScript_AddMessage.MessageData msgCraftingHint03
	{
		get
		{
			return ExposedVariables.msgCraftingHint03;
		}
		set
		{
			ExposedVariables.msgCraftingHint03 = value;
		}
	}

	public uScript_AddMessage.MessageData msgCraftingHint04
	{
		get
		{
			return ExposedVariables.msgCraftingHint04;
		}
		set
		{
			ExposedVariables.msgCraftingHint04 = value;
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

	public uScript_AddMessage.MessageData msgLeavingMissionArea02
	{
		get
		{
			return ExposedVariables.msgLeavingMissionArea02;
		}
		set
		{
			ExposedVariables.msgLeavingMissionArea02 = value;
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

	public uScript_AddMessage.MessageData msg04aSelectBlockToCraft_Pad
	{
		get
		{
			return ExposedVariables.msg04aSelectBlockToCraft_Pad;
		}
		set
		{
			ExposedVariables.msg04aSelectBlockToCraft_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg05OpenComponentMenu_Pad
	{
		get
		{
			return ExposedVariables.msg05OpenComponentMenu_Pad;
		}
		set
		{
			ExposedVariables.msg05OpenComponentMenu_Pad = value;
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
