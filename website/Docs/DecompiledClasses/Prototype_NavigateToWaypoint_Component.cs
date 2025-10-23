using UnityEngine;

[AddComponentMenu("uScript/Graphs/Prototype_NavigateToWaypoint")]
public class Prototype_NavigateToWaypoint_Component : uScriptCode
{
	public Prototype_NavigateToWaypoint ExposedVariables = new Prototype_NavigateToWaypoint();

	public float destinationRadius
	{
		get
		{
			return ExposedVariables.destinationRadius;
		}
		set
		{
			ExposedVariables.destinationRadius = value;
		}
	}

	public LocalisedString[] msgMissionPrototype003
	{
		get
		{
			return ExposedVariables.msgMissionPrototype003;
		}
		set
		{
			ExposedVariables.msgMissionPrototype003 = value;
		}
	}

	public bool itemDelivery
	{
		get
		{
			return ExposedVariables.itemDelivery;
		}
		set
		{
			ExposedVariables.itemDelivery = value;
		}
	}

	public ItemTypeInfo itemDeliveryData
	{
		get
		{
			return ExposedVariables.itemDeliveryData;
		}
		set
		{
			ExposedVariables.itemDeliveryData = value;
		}
	}

	public int itemDeliveryNum
	{
		get
		{
			return ExposedVariables.itemDeliveryNum;
		}
		set
		{
			ExposedVariables.itemDeliveryNum = value;
		}
	}

	public bool timeLimit
	{
		get
		{
			return ExposedVariables.timeLimit;
		}
		set
		{
			ExposedVariables.timeLimit = value;
		}
	}

	public float timeLimitInSecs
	{
		get
		{
			return ExposedVariables.timeLimitInSecs;
		}
		set
		{
			ExposedVariables.timeLimitInSecs = value;
		}
	}

	public LocalisedString[] msgLose
	{
		get
		{
			return ExposedVariables.msgLose;
		}
		set
		{
			ExposedVariables.msgLose = value;
		}
	}

	public bool destinationIsTech
	{
		get
		{
			return ExposedVariables.destinationIsTech;
		}
		set
		{
			ExposedVariables.destinationIsTech = value;
		}
	}

	public bool destinationIsLocation
	{
		get
		{
			return ExposedVariables.destinationIsLocation;
		}
		set
		{
			ExposedVariables.destinationIsLocation = value;
		}
	}

	public float originRadius
	{
		get
		{
			return ExposedVariables.originRadius;
		}
		set
		{
			ExposedVariables.originRadius = value;
		}
	}

	public LocalisedString[] msgMissionPrototype002
	{
		get
		{
			return ExposedVariables.msgMissionPrototype002;
		}
		set
		{
			ExposedVariables.msgMissionPrototype002 = value;
		}
	}

	public LocalisedString[] msgMissionPrototype001
	{
		get
		{
			return ExposedVariables.msgMissionPrototype001;
		}
		set
		{
			ExposedVariables.msgMissionPrototype001 = value;
		}
	}

	public LocalisedString[] msgMissionPrototype004
	{
		get
		{
			return ExposedVariables.msgMissionPrototype004;
		}
		set
		{
			ExposedVariables.msgMissionPrototype004 = value;
		}
	}

	public string destinationLocationPrefab
	{
		get
		{
			return ExposedVariables.destinationLocationPrefab;
		}
		set
		{
			ExposedVariables.destinationLocationPrefab = value;
		}
	}

	public SpawnTechData enemyTechData
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

	public SpawnTechData destinationTech
	{
		get
		{
			return ExposedVariables.destinationTech;
		}
		set
		{
			ExposedVariables.destinationTech = value;
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
