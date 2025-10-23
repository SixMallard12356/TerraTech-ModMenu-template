using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_DefeatOrFleeEnemyAmbush")]
public class Mission_DefeatOrFleeEnemyAmbush_Component : uScriptCode
{
	public Mission_DefeatOrFleeEnemyAmbush ExposedVariables = new Mission_DefeatOrFleeEnemyAmbush();

	public LocalisedString[] msgFledComplete
	{
		get
		{
			return ExposedVariables.msgFledComplete;
		}
		set
		{
			ExposedVariables.msgFledComplete = value;
		}
	}

	public float distToTriggerAmbush
	{
		get
		{
			return ExposedVariables.distToTriggerAmbush;
		}
		set
		{
			ExposedVariables.distToTriggerAmbush = value;
		}
	}

	public LocalisedString QLFlee
	{
		get
		{
			return ExposedVariables.QLFlee;
		}
		set
		{
			ExposedVariables.QLFlee = value;
		}
	}

	public string EncounterCentralPos
	{
		get
		{
			return ExposedVariables.EncounterCentralPos;
		}
		set
		{
			ExposedVariables.EncounterCentralPos = value;
		}
	}

	public LocalisedString[] msgTrapSprung
	{
		get
		{
			return ExposedVariables.msgTrapSprung;
		}
		set
		{
			ExposedVariables.msgTrapSprung = value;
		}
	}

	public SpawnTechData[] enemyTechData
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

	public float distEnemyRange
	{
		get
		{
			return ExposedVariables.distEnemyRange;
		}
		set
		{
			ExposedVariables.distEnemyRange = value;
		}
	}

	public LocalisedString[] msgAllDeadComplete
	{
		get
		{
			return ExposedVariables.msgAllDeadComplete;
		}
		set
		{
			ExposedVariables.msgAllDeadComplete = value;
		}
	}

	public LocalisedString[] msgEnemySpotted
	{
		get
		{
			return ExposedVariables.msgEnemySpotted;
		}
		set
		{
			ExposedVariables.msgEnemySpotted = value;
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
