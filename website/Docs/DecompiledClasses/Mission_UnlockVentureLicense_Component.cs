using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_UnlockVentureLicense")]
public class Mission_UnlockVentureLicense_Component : uScriptCode
{
	public Mission_UnlockVentureLicense ExposedVariables = new Mission_UnlockVentureLicense();

	public TerrainObject terrainObjectPrefab
	{
		get
		{
			return ExposedVariables.terrainObjectPrefab;
		}
		set
		{
			ExposedVariables.terrainObjectPrefab = value;
		}
	}

	public string terrainObjectName
	{
		get
		{
			return ExposedVariables.terrainObjectName;
		}
		set
		{
			ExposedVariables.terrainObjectName = value;
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

	public uScript_AddMessage.MessageData msgHalfwayRound
	{
		get
		{
			return ExposedVariables.msgHalfwayRound;
		}
		set
		{
			ExposedVariables.msgHalfwayRound = value;
		}
	}

	public int halfwayCheckpointIndex
	{
		get
		{
			return ExposedVariables.halfwayCheckpointIndex;
		}
		set
		{
			ExposedVariables.halfwayCheckpointIndex = value;
		}
	}

	public uScript_AddMessage.MessageData msgRaceStarted
	{
		get
		{
			return ExposedVariables.msgRaceStarted;
		}
		set
		{
			ExposedVariables.msgRaceStarted = value;
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

	public uScript_AddMessage.MessageData msgStartRace
	{
		get
		{
			return ExposedVariables.msgStartRace;
		}
		set
		{
			ExposedVariables.msgStartRace = value;
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

	public uScript_AddMessage.MessageData msgRaceStartedEarly
	{
		get
		{
			return ExposedVariables.msgRaceStartedEarly;
		}
		set
		{
			ExposedVariables.msgRaceStartedEarly = value;
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

	public uScript_AddMessage.MessageData msgOutOfBounds
	{
		get
		{
			return ExposedVariables.msgOutOfBounds;
		}
		set
		{
			ExposedVariables.msgOutOfBounds = value;
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

	public uScript_AddMessage.MessageData msgQuitFromMenu
	{
		get
		{
			return ExposedVariables.msgQuitFromMenu;
		}
		set
		{
			ExposedVariables.msgQuitFromMenu = value;
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
