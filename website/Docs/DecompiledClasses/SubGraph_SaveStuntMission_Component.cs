using UnityEngine;

[AddComponentMenu("uScript/Graphs/SubGraph_SaveStuntMission")]
public class SubGraph_SaveStuntMission_Component : uScriptCode
{
	public SubGraph_SaveStuntMission ExposedVariables = new SubGraph_SaveStuntMission();

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

	public bool clearSceneryAlongTrack
	{
		get
		{
			return ExposedVariables.clearSceneryAlongTrack;
		}
		set
		{
			ExposedVariables.clearSceneryAlongTrack = value;
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
