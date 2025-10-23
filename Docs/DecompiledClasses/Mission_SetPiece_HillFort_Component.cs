using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_HillFort")]
public class Mission_SetPiece_HillFort_Component : uScriptCode
{
	public Mission_SetPiece_HillFort ExposedVariables = new Mission_SetPiece_HillFort();

	public float distAtWhichTargetTechFound
	{
		get
		{
			return ExposedVariables.distAtWhichTargetTechFound;
		}
		set
		{
			ExposedVariables.distAtWhichTargetTechFound = value;
		}
	}

	public LocalisedString[] msgMissionComplete
	{
		get
		{
			return ExposedVariables.msgMissionComplete;
		}
		set
		{
			ExposedVariables.msgMissionComplete = value;
		}
	}

	public float distAtWhichNPCTechFound
	{
		get
		{
			return ExposedVariables.distAtWhichNPCTechFound;
		}
		set
		{
			ExposedVariables.distAtWhichNPCTechFound = value;
		}
	}

	public LocalisedString[] msgNPCGreetingEnemyDead
	{
		get
		{
			return ExposedVariables.msgNPCGreetingEnemyDead;
		}
		set
		{
			ExposedVariables.msgNPCGreetingEnemyDead = value;
		}
	}

	public LocalisedString[] msgNPCGreeting
	{
		get
		{
			return ExposedVariables.msgNPCGreeting;
		}
		set
		{
			ExposedVariables.msgNPCGreeting = value;
		}
	}

	public SpawnTechData[] NPCTech
	{
		get
		{
			return ExposedVariables.NPCTech;
		}
		set
		{
			ExposedVariables.NPCTech = value;
		}
	}

	public ManOnScreenMessages.Speaker messageSpeaker
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

	public LocalisedString[] msgNPCGreetingInturrupt
	{
		get
		{
			return ExposedVariables.msgNPCGreetingInturrupt;
		}
		set
		{
			ExposedVariables.msgNPCGreetingInturrupt = value;
		}
	}

	public float DelayBetweenEnemyGroupSpaws
	{
		get
		{
			return ExposedVariables.DelayBetweenEnemyGroupSpaws;
		}
		set
		{
			ExposedVariables.DelayBetweenEnemyGroupSpaws = value;
		}
	}

	public int ReinforcementSubGroupSize
	{
		get
		{
			return ExposedVariables.ReinforcementSubGroupSize;
		}
		set
		{
			ExposedVariables.ReinforcementSubGroupSize = value;
		}
	}

	public float DelayBetweenRespawnArivals
	{
		get
		{
			return ExposedVariables.DelayBetweenRespawnArivals;
		}
		set
		{
			ExposedVariables.DelayBetweenRespawnArivals = value;
		}
	}

	public SpawnTechData[] targetTechData
	{
		get
		{
			return ExposedVariables.targetTechData;
		}
		set
		{
			ExposedVariables.targetTechData = value;
		}
	}

	public LocalisedString[] msgTargetTechFound5
	{
		get
		{
			return ExposedVariables.msgTargetTechFound5;
		}
		set
		{
			ExposedVariables.msgTargetTechFound5 = value;
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

	public SpawnTechData[] FirstFly01TechData
	{
		get
		{
			return ExposedVariables.FirstFly01TechData;
		}
		set
		{
			ExposedVariables.FirstFly01TechData = value;
		}
	}

	public SpawnTechData[] FirstFly02TechData
	{
		get
		{
			return ExposedVariables.FirstFly02TechData;
		}
		set
		{
			ExposedVariables.FirstFly02TechData = value;
		}
	}

	public SpawnTechData[] FirstFly03TechData
	{
		get
		{
			return ExposedVariables.FirstFly03TechData;
		}
		set
		{
			ExposedVariables.FirstFly03TechData = value;
		}
	}

	public bool AllowEnemyGroupToRespawn
	{
		get
		{
			return ExposedVariables.AllowEnemyGroupToRespawn;
		}
		set
		{
			ExposedVariables.AllowEnemyGroupToRespawn = value;
		}
	}

	public SpawnTechData enemyGroupData
	{
		get
		{
			return ExposedVariables.enemyGroupData;
		}
		set
		{
			ExposedVariables.enemyGroupData = value;
		}
	}

	public float distAtWhichTargetTechLeft
	{
		get
		{
			return ExposedVariables.distAtWhichTargetTechLeft;
		}
		set
		{
			ExposedVariables.distAtWhichTargetTechLeft = value;
		}
	}

	public LocalisedString[] msgTargetTechFound1to2
	{
		get
		{
			return ExposedVariables.msgTargetTechFound1to2;
		}
		set
		{
			ExposedVariables.msgTargetTechFound1to2 = value;
		}
	}

	public ManOnScreenMessages.Speaker messageSpeakerSpider
	{
		get
		{
			return ExposedVariables.messageSpeakerSpider;
		}
		set
		{
			ExposedVariables.messageSpeakerSpider = value;
		}
	}

	public LocalisedString[] msgTargetTechFound3to4
	{
		get
		{
			return ExposedVariables.msgTargetTechFound3to4;
		}
		set
		{
			ExposedVariables.msgTargetTechFound3to4 = value;
		}
	}

	public SpawnTechData[] guardFliesTechData
	{
		get
		{
			return ExposedVariables.guardFliesTechData;
		}
		set
		{
			ExposedVariables.guardFliesTechData = value;
		}
	}

	public SpawnTechData[] chargerTechData
	{
		get
		{
			return ExposedVariables.chargerTechData;
		}
		set
		{
			ExposedVariables.chargerTechData = value;
		}
	}

	public WaveSizeSpecification EnemyGroupSize
	{
		get
		{
			return ExposedVariables.EnemyGroupSize;
		}
		set
		{
			ExposedVariables.EnemyGroupSize = value;
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
