using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_1_S_Battery")]
public class Mission_GSO_1_S_Battery_Component : uScriptCode
{
	public Mission_GSO_1_S_Battery ExposedVariables = new Mission_GSO_1_S_Battery();

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

	public Transform batteryTreePrefab
	{
		get
		{
			return ExposedVariables.batteryTreePrefab;
		}
		set
		{
			ExposedVariables.batteryTreePrefab = value;
		}
	}

	public string PositionID
	{
		get
		{
			return ExposedVariables.PositionID;
		}
		set
		{
			ExposedVariables.PositionID = value;
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

	public float NearTreeDistance
	{
		get
		{
			return ExposedVariables.NearTreeDistance;
		}
		set
		{
			ExposedVariables.NearTreeDistance = value;
		}
	}

	public uScript_AddMessage.MessageData msg01FindBattery
	{
		get
		{
			return ExposedVariables.msg01FindBattery;
		}
		set
		{
			ExposedVariables.msg01FindBattery = value;
		}
	}

	public uScript_AddMessage.MessageData msg02BatteryFound
	{
		get
		{
			return ExposedVariables.msg02BatteryFound;
		}
		set
		{
			ExposedVariables.msg02BatteryFound = value;
		}
	}

	public uScript_AddMessage.MessageData msg03DestroyTree
	{
		get
		{
			return ExposedVariables.msg03DestroyTree;
		}
		set
		{
			ExposedVariables.msg03DestroyTree = value;
		}
	}

	public uScript_AddMessage.MessageData msg07BatteryDestroyed
	{
		get
		{
			return ExposedVariables.msg07BatteryDestroyed;
		}
		set
		{
			ExposedVariables.msg07BatteryDestroyed = value;
		}
	}

	public uScript_AddMessage.MessageData msg04PickUpBattery
	{
		get
		{
			return ExposedVariables.msg04PickUpBattery;
		}
		set
		{
			ExposedVariables.msg04PickUpBattery = value;
		}
	}

	public uScript_AddMessage.MessageData msg05AttachBattery
	{
		get
		{
			return ExposedVariables.msg05AttachBattery;
		}
		set
		{
			ExposedVariables.msg05AttachBattery = value;
		}
	}

	public uScript_AddMessage.MessageData msg06BatteryDropped
	{
		get
		{
			return ExposedVariables.msg06BatteryDropped;
		}
		set
		{
			ExposedVariables.msg06BatteryDropped = value;
		}
	}

	public uScript_AddMessage.MessageData msg08BatteryAttached
	{
		get
		{
			return ExposedVariables.msg08BatteryAttached;
		}
		set
		{
			ExposedVariables.msg08BatteryAttached = value;
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
