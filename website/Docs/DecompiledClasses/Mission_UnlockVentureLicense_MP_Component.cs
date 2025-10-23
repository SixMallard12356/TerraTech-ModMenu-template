using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_UnlockVentureLicense_MP")]
public class Mission_UnlockVentureLicense_MP_Component : uScriptCode
{
	public Mission_UnlockVentureLicense_MP ExposedVariables = new Mission_UnlockVentureLicense_MP();

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

	public string raceStartPosition
	{
		get
		{
			return ExposedVariables.raceStartPosition;
		}
		set
		{
			ExposedVariables.raceStartPosition = value;
		}
	}

	public uScript_AddMessage.MessageData msgComplete
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

	public float distNearNPC
	{
		get
		{
			return ExposedVariables.distNearNPC;
		}
		set
		{
			ExposedVariables.distNearNPC = value;
		}
	}

	public uScript_AddMessage.MessageData msgIntro
	{
		get
		{
			return ExposedVariables.msgIntro;
		}
		set
		{
			ExposedVariables.msgIntro = value;
		}
	}

	public float distLeavingMission
	{
		get
		{
			return ExposedVariables.distLeavingMission;
		}
		set
		{
			ExposedVariables.distLeavingMission = value;
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

	public uScript_AddMessage.MessageData msgOutOfTime
	{
		get
		{
			return ExposedVariables.msgOutOfTime;
		}
		set
		{
			ExposedVariables.msgOutOfTime = value;
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

	public uScript_AddMessage.MessageData msgNPCFound
	{
		get
		{
			return ExposedVariables.msgNPCFound;
		}
		set
		{
			ExposedVariables.msgNPCFound = value;
		}
	}

	public float distNearTradingStation
	{
		get
		{
			return ExposedVariables.distNearTradingStation;
		}
		set
		{
			ExposedVariables.distNearTradingStation = value;
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
