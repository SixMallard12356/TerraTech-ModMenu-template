using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_1_S_Shield")]
public class Mission_GSO_1_S_Shield_Component : uScriptCode
{
	public Mission_GSO_1_S_Shield ExposedVariables = new Mission_GSO_1_S_Shield();

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

	public LocalisedString QLTitle
	{
		get
		{
			return ExposedVariables.QLTitle;
		}
		set
		{
			ExposedVariables.QLTitle = value;
		}
	}

	public LocalisedString QL1FindRetriveShield
	{
		get
		{
			return ExposedVariables.QL1FindRetriveShield;
		}
		set
		{
			ExposedVariables.QL1FindRetriveShield = value;
		}
	}

	public LocalisedString QL4AttachShield
	{
		get
		{
			return ExposedVariables.QL4AttachShield;
		}
		set
		{
			ExposedVariables.QL4AttachShield = value;
		}
	}

	public LocalisedString QL2AttackTheRobber
	{
		get
		{
			return ExposedVariables.QL2AttackTheRobber;
		}
		set
		{
			ExposedVariables.QL2AttackTheRobber = value;
		}
	}

	public LocalisedString[] Msg1EnemyEncounter
	{
		get
		{
			return ExposedVariables.Msg1EnemyEncounter;
		}
		set
		{
			ExposedVariables.Msg1EnemyEncounter = value;
		}
	}

	public LocalisedString[] Msg5AttachShield
	{
		get
		{
			return ExposedVariables.Msg5AttachShield;
		}
		set
		{
			ExposedVariables.Msg5AttachShield = value;
		}
	}

	public LocalisedString[] Msg7ShieldAttached
	{
		get
		{
			return ExposedVariables.Msg7ShieldAttached;
		}
		set
		{
			ExposedVariables.Msg7ShieldAttached = value;
		}
	}

	public SpawnTechData SpawnDataRobber
	{
		get
		{
			return ExposedVariables.SpawnDataRobber;
		}
		set
		{
			ExposedVariables.SpawnDataRobber = value;
		}
	}

	public LocalisedString[] Msg6OopsNotRight
	{
		get
		{
			return ExposedVariables.Msg6OopsNotRight;
		}
		set
		{
			ExposedVariables.Msg6OopsNotRight = value;
		}
	}

	public LocalisedString[] Msg4PickUpShield
	{
		get
		{
			return ExposedVariables.Msg4PickUpShield;
		}
		set
		{
			ExposedVariables.Msg4PickUpShield = value;
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
