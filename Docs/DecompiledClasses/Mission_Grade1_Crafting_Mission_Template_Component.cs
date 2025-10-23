using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_Grade1_Crafting_Mission_Template")]
public class Mission_Grade1_Crafting_Mission_Template_Component : uScriptCode
{
	public Mission_Grade1_Crafting_Mission_Template ExposedVariables = new Mission_Grade1_Crafting_Mission_Template();

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

	public uScript_AddMessage.MessageData msg03OpenMenu
	{
		get
		{
			return ExposedVariables.msg03OpenMenu;
		}
		set
		{
			ExposedVariables.msg03OpenMenu = value;
		}
	}

	public uScript_AddMessage.MessageData msg03OpenMenu_Pad
	{
		get
		{
			return ExposedVariables.msg03OpenMenu_Pad;
		}
		set
		{
			ExposedVariables.msg03OpenMenu_Pad = value;
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

	public uScript_AddMessage.MessageData msg04CraftBlock_Pad
	{
		get
		{
			return ExposedVariables.msg04CraftBlock_Pad;
		}
		set
		{
			ExposedVariables.msg04CraftBlock_Pad = value;
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

	public uScript_AddMessage.MessageData msg04CraftBlock
	{
		get
		{
			return ExposedVariables.msg04CraftBlock;
		}
		set
		{
			ExposedVariables.msg04CraftBlock = value;
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

	public uScript_AddMessage.MessageData msg06AllBlocksCrafted
	{
		get
		{
			return ExposedVariables.msg06AllBlocksCrafted;
		}
		set
		{
			ExposedVariables.msg06AllBlocksCrafted = value;
		}
	}

	public BlockTypes BlockTypeFusionMachine
	{
		get
		{
			return ExposedVariables.BlockTypeFusionMachine;
		}
		set
		{
			ExposedVariables.BlockTypeFusionMachine = value;
		}
	}

	public BlockTypes targetBlockType
	{
		get
		{
			return ExposedVariables.targetBlockType;
		}
		set
		{
			ExposedVariables.targetBlockType = value;
		}
	}

	public uScript_AddMessage.MessageData msgSpawnMinion
	{
		get
		{
			return ExposedVariables.msgSpawnMinion;
		}
		set
		{
			ExposedVariables.msgSpawnMinion = value;
		}
	}

	public SpawnTechData[] EnemyTechData
	{
		get
		{
			return ExposedVariables.EnemyTechData;
		}
		set
		{
			ExposedVariables.EnemyTechData = value;
		}
	}

	public string BasePosition
	{
		get
		{
			return ExposedVariables.BasePosition;
		}
		set
		{
			ExposedVariables.BasePosition = value;
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

	public string BaseVFXSpawn
	{
		get
		{
			return ExposedVariables.BaseVFXSpawn;
		}
		set
		{
			ExposedVariables.BaseVFXSpawn = value;
		}
	}

	public SpawnTechData[] CraftingBaseSpawnData
	{
		get
		{
			return ExposedVariables.CraftingBaseSpawnData;
		}
		set
		{
			ExposedVariables.CraftingBaseSpawnData = value;
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

	public uScript_AddMessage.MessageData msg05CraftAmountOfBlocksNeeded
	{
		get
		{
			return ExposedVariables.msg05CraftAmountOfBlocksNeeded;
		}
		set
		{
			ExposedVariables.msg05CraftAmountOfBlocksNeeded = value;
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

	public int targetAmount
	{
		get
		{
			return ExposedVariables.targetAmount;
		}
		set
		{
			ExposedVariables.targetAmount = value;
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

	public float ParticleTimer
	{
		get
		{
			return ExposedVariables.ParticleTimer;
		}
		set
		{
			ExposedVariables.ParticleTimer = value;
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

	private void OnGUI()
	{
		ExposedVariables.OnGUI();
	}
}
