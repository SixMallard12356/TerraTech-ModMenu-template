using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_Grade1_Scrapping_Mission_Template")]
public class Mission_Grade1_Scrapping_Mission_Template_Component : uScriptCode
{
	public Mission_Grade1_Scrapping_Mission_Template ExposedVariables = new Mission_Grade1_Scrapping_Mission_Template();

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

	public BlockTypes BlockTypeSJSscrapper
	{
		get
		{
			return ExposedVariables.BlockTypeSJSscrapper;
		}
		set
		{
			ExposedVariables.BlockTypeSJSscrapper = value;
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

	public uScript_AddMessage.MessageData msg05Complete
	{
		get
		{
			return ExposedVariables.msg05Complete;
		}
		set
		{
			ExposedVariables.msg05Complete = value;
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

	public int TargetAmount
	{
		get
		{
			return ExposedVariables.TargetAmount;
		}
		set
		{
			ExposedVariables.TargetAmount = value;
		}
	}

	public uScript_AddMessage.MessageData msg03PutBlocksInScrapper
	{
		get
		{
			return ExposedVariables.msg03PutBlocksInScrapper;
		}
		set
		{
			ExposedVariables.msg03PutBlocksInScrapper = value;
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

	public uScript_AddMessage.MessageData msg04AllBlocksScrapped
	{
		get
		{
			return ExposedVariables.msg04AllBlocksScrapped;
		}
		set
		{
			ExposedVariables.msg04AllBlocksScrapped = value;
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
