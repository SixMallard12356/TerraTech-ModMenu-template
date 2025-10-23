using UnityEngine;

[AddComponentMenu("uScript/Graphs/GC_Harvesting_Mission")]
public class GC_Harvesting_Mission_Component : uScriptCode
{
	public GC_Harvesting_Mission ExposedVariables = new GC_Harvesting_Mission();

	public float distNPCFound
	{
		get
		{
			return ExposedVariables.distNPCFound;
		}
		set
		{
			ExposedVariables.distNPCFound = value;
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

	public BlockTypes blockTypeSilo
	{
		get
		{
			return ExposedVariables.blockTypeSilo;
		}
		set
		{
			ExposedVariables.blockTypeSilo = value;
		}
	}

	public uScript_AddMessage.MessageData msg03Complete
	{
		get
		{
			return ExposedVariables.msg03Complete;
		}
		set
		{
			ExposedVariables.msg03Complete = value;
		}
	}

	public uScript_AddMessage.MessageData msg02NPCFound
	{
		get
		{
			return ExposedVariables.msg02NPCFound;
		}
		set
		{
			ExposedVariables.msg02NPCFound = value;
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

	public string NPCPosition
	{
		get
		{
			return ExposedVariables.NPCPosition;
		}
		set
		{
			ExposedVariables.NPCPosition = value;
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

	public ChunkTypes[] resourceTypesAccepted
	{
		get
		{
			return ExposedVariables.resourceTypesAccepted;
		}
		set
		{
			ExposedVariables.resourceTypesAccepted = value;
		}
	}

	public bool timedMission
	{
		get
		{
			return ExposedVariables.timedMission;
		}
		set
		{
			ExposedVariables.timedMission = value;
		}
	}

	public bool useRandomResourceType
	{
		get
		{
			return ExposedVariables.useRandomResourceType;
		}
		set
		{
			ExposedVariables.useRandomResourceType = value;
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
