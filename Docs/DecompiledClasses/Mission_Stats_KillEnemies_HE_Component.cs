using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_Stats_KillEnemies_HE")]
public class Mission_Stats_KillEnemies_HE_Component : uScriptCode
{
	public Mission_Stats_KillEnemies_HE ExposedVariables = new Mission_Stats_KillEnemies_HE();

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

	public int ObjectiveToCount
	{
		get
		{
			return ExposedVariables.ObjectiveToCount;
		}
		set
		{
			ExposedVariables.ObjectiveToCount = value;
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
