using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_CanyonSpiral")]
public class Mission_SetPiece_CanyonSpiral_Component : uScriptCode
{
	public Mission_SetPiece_CanyonSpiral ExposedVariables = new Mission_SetPiece_CanyonSpiral();

	public LocalisedString[] msgEnemySpottedBoss
	{
		get
		{
			return ExposedVariables.msgEnemySpottedBoss;
		}
		set
		{
			ExposedVariables.msgEnemySpottedBoss = value;
		}
	}

	public float distEnemiesSpottedBoss
	{
		get
		{
			return ExposedVariables.distEnemiesSpottedBoss;
		}
		set
		{
			ExposedVariables.distEnemiesSpottedBoss = value;
		}
	}

	public ManOnScreenMessages.Speaker messageSpeakerBoss
	{
		get
		{
			return ExposedVariables.messageSpeakerBoss;
		}
		set
		{
			ExposedVariables.messageSpeakerBoss = value;
		}
	}

	public string Trigger5
	{
		get
		{
			return ExposedVariables.Trigger5;
		}
		set
		{
			ExposedVariables.Trigger5 = value;
		}
	}

	public string Trigger6
	{
		get
		{
			return ExposedVariables.Trigger6;
		}
		set
		{
			ExposedVariables.Trigger6 = value;
		}
	}

	public SpawnTechData[] enemyTechDataBoss
	{
		get
		{
			return ExposedVariables.enemyTechDataBoss;
		}
		set
		{
			ExposedVariables.enemyTechDataBoss = value;
		}
	}

	public LocalisedString[] msgMissionCompleteNPC
	{
		get
		{
			return ExposedVariables.msgMissionCompleteNPC;
		}
		set
		{
			ExposedVariables.msgMissionCompleteNPC = value;
		}
	}

	public string Trigger4
	{
		get
		{
			return ExposedVariables.Trigger4;
		}
		set
		{
			ExposedVariables.Trigger4 = value;
		}
	}

	public LocalisedString[] msgTechsDeadF
	{
		get
		{
			return ExposedVariables.msgTechsDeadF;
		}
		set
		{
			ExposedVariables.msgTechsDeadF = value;
		}
	}

	public LocalisedString[] msgEnemySpottedF
	{
		get
		{
			return ExposedVariables.msgEnemySpottedF;
		}
		set
		{
			ExposedVariables.msgEnemySpottedF = value;
		}
	}

	public LocalisedString[] msgTechsDeadE
	{
		get
		{
			return ExposedVariables.msgTechsDeadE;
		}
		set
		{
			ExposedVariables.msgTechsDeadE = value;
		}
	}

	public LocalisedString[] msgEnemySpottedE
	{
		get
		{
			return ExposedVariables.msgEnemySpottedE;
		}
		set
		{
			ExposedVariables.msgEnemySpottedE = value;
		}
	}

	public string Trigger3
	{
		get
		{
			return ExposedVariables.Trigger3;
		}
		set
		{
			ExposedVariables.Trigger3 = value;
		}
	}

	public LocalisedString[] msgTechsDeadCandD
	{
		get
		{
			return ExposedVariables.msgTechsDeadCandD;
		}
		set
		{
			ExposedVariables.msgTechsDeadCandD = value;
		}
	}

	public LocalisedString[] msgEnemySpottedC
	{
		get
		{
			return ExposedVariables.msgEnemySpottedC;
		}
		set
		{
			ExposedVariables.msgEnemySpottedC = value;
		}
	}

	public string Trigger2
	{
		get
		{
			return ExposedVariables.Trigger2;
		}
		set
		{
			ExposedVariables.Trigger2 = value;
		}
	}

	public LocalisedString[] msgEnemySpottedD
	{
		get
		{
			return ExposedVariables.msgEnemySpottedD;
		}
		set
		{
			ExposedVariables.msgEnemySpottedD = value;
		}
	}

	public LocalisedString[] msgTechsDeadBandA
	{
		get
		{
			return ExposedVariables.msgTechsDeadBandA;
		}
		set
		{
			ExposedVariables.msgTechsDeadBandA = value;
		}
	}

	public float distEnemiesSpottedMinions
	{
		get
		{
			return ExposedVariables.distEnemiesSpottedMinions;
		}
		set
		{
			ExposedVariables.distEnemiesSpottedMinions = value;
		}
	}

	public string Trigger1
	{
		get
		{
			return ExposedVariables.Trigger1;
		}
		set
		{
			ExposedVariables.Trigger1 = value;
		}
	}

	public ManOnScreenMessages.Speaker messageSpeakerMinion
	{
		get
		{
			return ExposedVariables.messageSpeakerMinion;
		}
		set
		{
			ExposedVariables.messageSpeakerMinion = value;
		}
	}

	public LocalisedString[] msgEnemySpottedB
	{
		get
		{
			return ExposedVariables.msgEnemySpottedB;
		}
		set
		{
			ExposedVariables.msgEnemySpottedB = value;
		}
	}

	public LocalisedString[] msgEnemySpottedA
	{
		get
		{
			return ExposedVariables.msgEnemySpottedA;
		}
		set
		{
			ExposedVariables.msgEnemySpottedA = value;
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

	public SpawnTechData[] NPCTechData
	{
		get
		{
			return ExposedVariables.NPCTechData;
		}
		set
		{
			ExposedVariables.NPCTechData = value;
		}
	}

	public ManOnScreenMessages.Speaker messageSpeakerNPC
	{
		get
		{
			return ExposedVariables.messageSpeakerNPC;
		}
		set
		{
			ExposedVariables.messageSpeakerNPC = value;
		}
	}

	public LocalisedString[] msgMissionCompleteGeneric
	{
		get
		{
			return ExposedVariables.msgMissionCompleteGeneric;
		}
		set
		{
			ExposedVariables.msgMissionCompleteGeneric = value;
		}
	}

	public ManOnScreenMessages.Speaker messageSpeakerGeneric
	{
		get
		{
			return ExposedVariables.messageSpeakerGeneric;
		}
		set
		{
			ExposedVariables.messageSpeakerGeneric = value;
		}
	}

	public string TriggerA
	{
		get
		{
			return ExposedVariables.TriggerA;
		}
		set
		{
			ExposedVariables.TriggerA = value;
		}
	}

	public string NPCPos
	{
		get
		{
			return ExposedVariables.NPCPos;
		}
		set
		{
			ExposedVariables.NPCPos = value;
		}
	}

	public string EnemyPosH
	{
		get
		{
			return ExposedVariables.EnemyPosH;
		}
		set
		{
			ExposedVariables.EnemyPosH = value;
		}
	}

	public SpawnTechData[] enemyTechDataG
	{
		get
		{
			return ExposedVariables.enemyTechDataG;
		}
		set
		{
			ExposedVariables.enemyTechDataG = value;
		}
	}

	public SpawnTechData[] enemyTechDataChargingPoint
	{
		get
		{
			return ExposedVariables.enemyTechDataChargingPoint;
		}
		set
		{
			ExposedVariables.enemyTechDataChargingPoint = value;
		}
	}

	public SpawnTechData[] enemyTechDataB
	{
		get
		{
			return ExposedVariables.enemyTechDataB;
		}
		set
		{
			ExposedVariables.enemyTechDataB = value;
		}
	}

	public SpawnTechData[] enemyTechDataA
	{
		get
		{
			return ExposedVariables.enemyTechDataA;
		}
		set
		{
			ExposedVariables.enemyTechDataA = value;
		}
	}

	public SpawnTechData[] enemyTechDataC
	{
		get
		{
			return ExposedVariables.enemyTechDataC;
		}
		set
		{
			ExposedVariables.enemyTechDataC = value;
		}
	}

	public SpawnTechData[] enemyTechDataD
	{
		get
		{
			return ExposedVariables.enemyTechDataD;
		}
		set
		{
			ExposedVariables.enemyTechDataD = value;
		}
	}

	public SpawnTechData[] enemyTechDataE
	{
		get
		{
			return ExposedVariables.enemyTechDataE;
		}
		set
		{
			ExposedVariables.enemyTechDataE = value;
		}
	}

	public SpawnTechData[] enemyTechDataF
	{
		get
		{
			return ExposedVariables.enemyTechDataF;
		}
		set
		{
			ExposedVariables.enemyTechDataF = value;
		}
	}

	public string TriggerC
	{
		get
		{
			return ExposedVariables.TriggerC;
		}
		set
		{
			ExposedVariables.TriggerC = value;
		}
	}

	public string TriggerD
	{
		get
		{
			return ExposedVariables.TriggerD;
		}
		set
		{
			ExposedVariables.TriggerD = value;
		}
	}

	public string TriggerB
	{
		get
		{
			return ExposedVariables.TriggerB;
		}
		set
		{
			ExposedVariables.TriggerB = value;
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
