using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_EnemyAmbush")]
public class Mission_SetPiece_EnemyAmbush_Component : uScriptCode
{
	public Mission_SetPiece_EnemyAmbush ExposedVariables = new Mission_SetPiece_EnemyAmbush();

	public LocalisedString[] msgComplete
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

	public bool ItsATrap
	{
		get
		{
			return ExposedVariables.ItsATrap;
		}
		set
		{
			ExposedVariables.ItsATrap = value;
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

	public ManOnScreenMessages.Speaker messageSpeaker
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

	public LocalisedString[] msgApproachCrate
	{
		get
		{
			return ExposedVariables.msgApproachCrate;
		}
		set
		{
			ExposedVariables.msgApproachCrate = value;
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
