using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_LearnToFly")]
public class Mission_LearnToFly_Component : uScriptCode
{
	public Mission_LearnToFly ExposedVariables = new Mission_LearnToFly();

	public TerrainObject StuntRampPrefab
	{
		get
		{
			return ExposedVariables.StuntRampPrefab;
		}
		set
		{
			ExposedVariables.StuntRampPrefab = value;
		}
	}

	public string EncounterCentrePosName
	{
		get
		{
			return ExposedVariables.EncounterCentrePosName;
		}
		set
		{
			ExposedVariables.EncounterCentrePosName = value;
		}
	}

	public string Spawned_Ramp_Name
	{
		get
		{
			return ExposedVariables.Spawned_Ramp_Name;
		}
		set
		{
			ExposedVariables.Spawned_Ramp_Name = value;
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

	public float distAtWhichNPCInRange
	{
		get
		{
			return ExposedVariables.distAtWhichNPCInRange;
		}
		set
		{
			ExposedVariables.distAtWhichNPCInRange = value;
		}
	}

	public uScript_AddMessage.MessageData msg01FindTech
	{
		get
		{
			return ExposedVariables.msg01FindTech;
		}
		set
		{
			ExposedVariables.msg01FindTech = value;
		}
	}

	public SpawnTechData[] techData
	{
		get
		{
			return ExposedVariables.techData;
		}
		set
		{
			ExposedVariables.techData = value;
		}
	}

	public uScript_AddMessage.MessageData msg02TechFound
	{
		get
		{
			return ExposedVariables.msg02TechFound;
		}
		set
		{
			ExposedVariables.msg02TechFound = value;
		}
	}

	public uScript_AddMessage.MessageData msg04ControlTechComplete_Pad
	{
		get
		{
			return ExposedVariables.msg04ControlTechComplete_Pad;
		}
		set
		{
			ExposedVariables.msg04ControlTechComplete_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg03AccessMenu
	{
		get
		{
			return ExposedVariables.msg03AccessMenu;
		}
		set
		{
			ExposedVariables.msg03AccessMenu = value;
		}
	}

	public uScript_AddMessage.MessageData msg03AccessMenu_Pad
	{
		get
		{
			return ExposedVariables.msg03AccessMenu_Pad;
		}
		set
		{
			ExposedVariables.msg03AccessMenu_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg04ControlTechComplete
	{
		get
		{
			return ExposedVariables.msg04ControlTechComplete;
		}
		set
		{
			ExposedVariables.msg04ControlTechComplete = value;
		}
	}

	public float distAtWhichTechCloseRange
	{
		get
		{
			return ExposedVariables.distAtWhichTechCloseRange;
		}
		set
		{
			ExposedVariables.distAtWhichTechCloseRange = value;
		}
	}

	public float distAtWhichTechFound
	{
		get
		{
			return ExposedVariables.distAtWhichTechFound;
		}
		set
		{
			ExposedVariables.distAtWhichTechFound = value;
		}
	}

	public uScript_AddMessage.MessageData msgStuntStarted
	{
		get
		{
			return ExposedVariables.msgStuntStarted;
		}
		set
		{
			ExposedVariables.msgStuntStarted = value;
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

	public uScript_AddMessage.MessageData msgTouchedGround
	{
		get
		{
			return ExposedVariables.msgTouchedGround;
		}
		set
		{
			ExposedVariables.msgTouchedGround = value;
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

	public uScript_AddMessage.MessageData msgStuntComplete
	{
		get
		{
			return ExposedVariables.msgStuntComplete;
		}
		set
		{
			ExposedVariables.msgStuntComplete = value;
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

	public string targetChallengeID
	{
		get
		{
			return ExposedVariables.targetChallengeID;
		}
		set
		{
			ExposedVariables.targetChallengeID = value;
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
