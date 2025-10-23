using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SJ_Grade1_Scrapping_01")]
public class Mission_SJ_Grade1_Scrapping_01_Component : uScriptCode
{
	public Mission_SJ_Grade1_Scrapping_01 ExposedVariables = new Mission_SJ_Grade1_Scrapping_01();

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

	public uScript_AddMessage.MessageData msg07ScrappingBlock1_InProgress
	{
		get
		{
			return ExposedVariables.msg07ScrappingBlock1_InProgress;
		}
		set
		{
			ExposedVariables.msg07ScrappingBlock1_InProgress = value;
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

	public uScript_AddMessage.MessageData msg08ScrappedBlock1
	{
		get
		{
			return ExposedVariables.msg08ScrappedBlock1;
		}
		set
		{
			ExposedVariables.msg08ScrappedBlock1 = value;
		}
	}

	public uScript_AddMessage.MessageData msg06PutBlock1InScrapper
	{
		get
		{
			return ExposedVariables.msg06PutBlock1InScrapper;
		}
		set
		{
			ExposedVariables.msg06PutBlock1InScrapper = value;
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

	public uScript_AddMessage.MessageData msg13Complete
	{
		get
		{
			return ExposedVariables.msg13Complete;
		}
		set
		{
			ExposedVariables.msg13Complete = value;
		}
	}

	public uScript_AddMessage.MessageData msg09Block1Scrapped
	{
		get
		{
			return ExposedVariables.msg09Block1Scrapped;
		}
		set
		{
			ExposedVariables.msg09Block1Scrapped = value;
		}
	}

	public uScript_AddMessage.MessageData msg10PutBlock2InScrapper
	{
		get
		{
			return ExposedVariables.msg10PutBlock2InScrapper;
		}
		set
		{
			ExposedVariables.msg10PutBlock2InScrapper = value;
		}
	}

	public uScript_AddMessage.MessageData msg11ScrappingBlock2_InProgress
	{
		get
		{
			return ExposedVariables.msg11ScrappingBlock2_InProgress;
		}
		set
		{
			ExposedVariables.msg11ScrappingBlock2_InProgress = value;
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

	public uScript_AddMessage.MessageData msg14AltComplete
	{
		get
		{
			return ExposedVariables.msg14AltComplete;
		}
		set
		{
			ExposedVariables.msg14AltComplete = value;
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

	public BlockTypes BlockTypeSJScrapper
	{
		get
		{
			return ExposedVariables.BlockTypeSJScrapper;
		}
		set
		{
			ExposedVariables.BlockTypeSJScrapper = value;
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
