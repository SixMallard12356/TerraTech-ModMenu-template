using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_AttackProtectedTarget")]
public class Mission_AttackProtectedTarget_Component : uScriptCode
{
	public Mission_AttackProtectedTarget ExposedVariables = new Mission_AttackProtectedTarget();

	public float distAtWhichFriendlyTechFound
	{
		get
		{
			return ExposedVariables.distAtWhichFriendlyTechFound;
		}
		set
		{
			ExposedVariables.distAtWhichFriendlyTechFound = value;
		}
	}

	public LocalisedString[] msgTargetTechFound
	{
		get
		{
			return ExposedVariables.msgTargetTechFound;
		}
		set
		{
			ExposedVariables.msgTargetTechFound = value;
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

	public float DistInRangeOfNPC
	{
		get
		{
			return ExposedVariables.DistInRangeOfNPC;
		}
		set
		{
			ExposedVariables.DistInRangeOfNPC = value;
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

	public string clearSceneryPosTarget
	{
		get
		{
			return ExposedVariables.clearSceneryPosTarget;
		}
		set
		{
			ExposedVariables.clearSceneryPosTarget = value;
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

	public string clearSceneryPosNPC
	{
		get
		{
			return ExposedVariables.clearSceneryPosNPC;
		}
		set
		{
			ExposedVariables.clearSceneryPosNPC = value;
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
