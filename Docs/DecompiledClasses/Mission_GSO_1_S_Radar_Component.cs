using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_1_S_Radar")]
public class Mission_GSO_1_S_Radar_Component : uScriptCode
{
	public Mission_GSO_1_S_Radar ExposedVariables = new Mission_GSO_1_S_Radar();

	public Transform Particles
	{
		get
		{
			return ExposedVariables.Particles;
		}
		set
		{
			ExposedVariables.Particles = value;
		}
	}

	public string PositionId
	{
		get
		{
			return ExposedVariables.PositionId;
		}
		set
		{
			ExposedVariables.PositionId = value;
		}
	}

	public SpawnTechData SpawnDataThief
	{
		get
		{
			return ExposedVariables.SpawnDataThief;
		}
		set
		{
			ExposedVariables.SpawnDataThief = value;
		}
	}

	public uScript_AddMessage.MessageData msg01EnemyFound
	{
		get
		{
			return ExposedVariables.msg01EnemyFound;
		}
		set
		{
			ExposedVariables.msg01EnemyFound = value;
		}
	}

	public uScript_AddMessage.MessageData msg02PickUpAttachRadar
	{
		get
		{
			return ExposedVariables.msg02PickUpAttachRadar;
		}
		set
		{
			ExposedVariables.msg02PickUpAttachRadar = value;
		}
	}

	public uScript_AddMessage.MessageData msg05RadarDestroyed
	{
		get
		{
			return ExposedVariables.msg05RadarDestroyed;
		}
		set
		{
			ExposedVariables.msg05RadarDestroyed = value;
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

	public uScript_AddMessage.MessageData msg04RadarAttached
	{
		get
		{
			return ExposedVariables.msg04RadarAttached;
		}
		set
		{
			ExposedVariables.msg04RadarAttached = value;
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
