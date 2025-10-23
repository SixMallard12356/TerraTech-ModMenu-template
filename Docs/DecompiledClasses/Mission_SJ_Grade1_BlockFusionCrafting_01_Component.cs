using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SJ_Grade1_BlockFusionCrafting_01")]
public class Mission_SJ_Grade1_BlockFusionCrafting_01_Component : uScriptCode
{
	public Mission_SJ_Grade1_BlockFusionCrafting_01 ExposedVariables = new Mission_SJ_Grade1_BlockFusionCrafting_01();

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

	public GhostBlockSpawnData[] ghostBlockFusionMachine
	{
		get
		{
			return ExposedVariables.ghostBlockFusionMachine;
		}
		set
		{
			ExposedVariables.ghostBlockFusionMachine = value;
		}
	}

	public SpawnBlockData[] blockSpawnDataFusionMachine
	{
		get
		{
			return ExposedVariables.blockSpawnDataFusionMachine;
		}
		set
		{
			ExposedVariables.blockSpawnDataFusionMachine = value;
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

	public uScript_AddMessage.MessageData msg09AttachFusionMachine
	{
		get
		{
			return ExposedVariables.msg09AttachFusionMachine;
		}
		set
		{
			ExposedVariables.msg09AttachFusionMachine = value;
		}
	}

	public uScript_AddMessage.MessageData msg08FusionMachineSpawned
	{
		get
		{
			return ExposedVariables.msg08FusionMachineSpawned;
		}
		set
		{
			ExposedVariables.msg08FusionMachineSpawned = value;
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

	public uScript_AddMessage.MessageData msg13CraftingInProgress
	{
		get
		{
			return ExposedVariables.msg13CraftingInProgress;
		}
		set
		{
			ExposedVariables.msg13CraftingInProgress = value;
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

	public uScript_AddMessage.MessageData msg14Complete
	{
		get
		{
			return ExposedVariables.msg14Complete;
		}
		set
		{
			ExposedVariables.msg14Complete = value;
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
