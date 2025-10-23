using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_1_5_DeliveryCrate")]
public class Mission_GSO_1_5_DeliveryCrate_Component : uScriptCode
{
	public Mission_GSO_1_5_DeliveryCrate ExposedVariables = new Mission_GSO_1_5_DeliveryCrate();

	public uScript_AddMessage.MessageData msg01GoToLocation
	{
		get
		{
			return ExposedVariables.msg01GoToLocation;
		}
		set
		{
			ExposedVariables.msg01GoToLocation = value;
		}
	}

	public uScript_AddMessage.MessageData msg05CrateUnlocked
	{
		get
		{
			return ExposedVariables.msg05CrateUnlocked;
		}
		set
		{
			ExposedVariables.msg05CrateUnlocked = value;
		}
	}

	public string cratePositionName
	{
		get
		{
			return ExposedVariables.cratePositionName;
		}
		set
		{
			ExposedVariables.cratePositionName = value;
		}
	}

	public uScript_AddMessage.MessageData msg04CrateLanded
	{
		get
		{
			return ExposedVariables.msg04CrateLanded;
		}
		set
		{
			ExposedVariables.msg04CrateLanded = value;
		}
	}

	public uScript_AddMessage.MessageData msg06LeaveArea
	{
		get
		{
			return ExposedVariables.msg06LeaveArea;
		}
		set
		{
			ExposedVariables.msg06LeaveArea = value;
		}
	}

	public float crateOpeningTime
	{
		get
		{
			return ExposedVariables.crateOpeningTime;
		}
		set
		{
			ExposedVariables.crateOpeningTime = value;
		}
	}

	public uScript_AddMessage.MessageData msg02ArrivedAtLocation
	{
		get
		{
			return ExposedVariables.msg02ArrivedAtLocation;
		}
		set
		{
			ExposedVariables.msg02ArrivedAtLocation = value;
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

	public uScript_AddMessage.MessageData msg03CrateLanding
	{
		get
		{
			return ExposedVariables.msg03CrateLanding;
		}
		set
		{
			ExposedVariables.msg03CrateLanding = value;
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

	public Transform craterPrefab
	{
		get
		{
			return ExposedVariables.craterPrefab;
		}
		set
		{
			ExposedVariables.craterPrefab = value;
		}
	}

	public string craterPositionName
	{
		get
		{
			return ExposedVariables.craterPositionName;
		}
		set
		{
			ExposedVariables.craterPositionName = value;
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
