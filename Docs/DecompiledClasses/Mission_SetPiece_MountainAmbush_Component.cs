using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_MountainAmbush")]
public class Mission_SetPiece_MountainAmbush_Component : uScriptCode
{
	public Mission_SetPiece_MountainAmbush ExposedVariables = new Mission_SetPiece_MountainAmbush();

	public LocalisedString[] msgComplete
	{
		get
		{
			return ExposedVariables.msgComplete;
		}
		set
		{
			ExposedVariables.msgComplete = value;
		}
	}

	public LocalisedString[] msgTrapSprung
	{
		get
		{
			return ExposedVariables.msgTrapSprung;
		}
		set
		{
			ExposedVariables.msgTrapSprung = value;
		}
	}

	public SpawnTechData[] ambushEnemyTechData
	{
		get
		{
			return ExposedVariables.ambushEnemyTechData;
		}
		set
		{
			ExposedVariables.ambushEnemyTechData = value;
		}
	}

	public LocalisedString[] msgEnemySpotted
	{
		get
		{
			return ExposedVariables.msgEnemySpotted;
		}
		set
		{
			ExposedVariables.msgEnemySpotted = value;
		}
	}

	public string crateSpawnPos
	{
		get
		{
			return ExposedVariables.crateSpawnPos;
		}
		set
		{
			ExposedVariables.crateSpawnPos = value;
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

	public string clearSceneryPos
	{
		get
		{
			return ExposedVariables.clearSceneryPos;
		}
		set
		{
			ExposedVariables.clearSceneryPos = value;
		}
	}

	public LocalisedString[] msgApproachCrate
	{
		get
		{
			return ExposedVariables.msgApproachCrate;
		}
		set
		{
			ExposedVariables.msgApproachCrate = value;
		}
	}

	public LocalisedString[] msgTurretWarning
	{
		get
		{
			return ExposedVariables.msgTurretWarning;
		}
		set
		{
			ExposedVariables.msgTurretWarning = value;
		}
	}

	public LocalisedString[] msgRunAway
	{
		get
		{
			return ExposedVariables.msgRunAway;
		}
		set
		{
			ExposedVariables.msgRunAway = value;
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

	public SpawnTechData[] NPCTechSpawn
	{
		get
		{
			return ExposedVariables.NPCTechSpawn;
		}
		set
		{
			ExposedVariables.NPCTechSpawn = value;
		}
	}

	public LocalisedString[] msgNPCGreeting01
	{
		get
		{
			return ExposedVariables.msgNPCGreeting01;
		}
		set
		{
			ExposedVariables.msgNPCGreeting01 = value;
		}
	}

	public SpawnTechData[] ChaseEnemiesSpawnData
	{
		get
		{
			return ExposedVariables.ChaseEnemiesSpawnData;
		}
		set
		{
			ExposedVariables.ChaseEnemiesSpawnData = value;
		}
	}

	public LocalisedString[] msgObjective
	{
		get
		{
			return ExposedVariables.msgObjective;
		}
		set
		{
			ExposedVariables.msgObjective = value;
		}
	}

	public LocalisedString[] msgMissionInRange
	{
		get
		{
			return ExposedVariables.msgMissionInRange;
		}
		set
		{
			ExposedVariables.msgMissionInRange = value;
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

	public SpawnTechData[] firstEnemiesSpawnData
	{
		get
		{
			return ExposedVariables.firstEnemiesSpawnData;
		}
		set
		{
			ExposedVariables.firstEnemiesSpawnData = value;
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
