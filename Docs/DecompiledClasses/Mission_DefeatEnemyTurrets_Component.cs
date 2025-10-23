using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_DefeatEnemyTurrets")]
public class Mission_DefeatEnemyTurrets_Component : uScriptCode
{
	public Mission_DefeatEnemyTurrets ExposedVariables = new Mission_DefeatEnemyTurrets();

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

	public float distEnemiesSpotted
	{
		get
		{
			return ExposedVariables.distEnemiesSpotted;
		}
		set
		{
			ExposedVariables.distEnemiesSpotted = value;
		}
	}

	public LocalisedString[] msgEnemiesSpotted
	{
		get
		{
			return ExposedVariables.msgEnemiesSpotted;
		}
		set
		{
			ExposedVariables.msgEnemiesSpotted = value;
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

	public SpawnTechData[] enemyTechData
	{
		get
		{
			return ExposedVariables.enemyTechData;
		}
		set
		{
			ExposedVariables.enemyTechData = value;
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
