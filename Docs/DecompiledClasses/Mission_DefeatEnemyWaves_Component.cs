using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_DefeatEnemyWaves")]
public class Mission_DefeatEnemyWaves_Component : uScriptCode
{
	public Mission_DefeatEnemyWaves ExposedVariables = new Mission_DefeatEnemyWaves();

	public LocalisedString[] msgComplete1
	{
		get
		{
			return ExposedVariables.msgComplete1;
		}
		set
		{
			ExposedVariables.msgComplete1 = value;
		}
	}

	public LocalisedString[] msgEnemiesSpotted1
	{
		get
		{
			return ExposedVariables.msgEnemiesSpotted1;
		}
		set
		{
			ExposedVariables.msgEnemiesSpotted1 = value;
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

	public LocalisedString[] msgEnemiesSpotted2
	{
		get
		{
			return ExposedVariables.msgEnemiesSpotted2;
		}
		set
		{
			ExposedVariables.msgEnemiesSpotted2 = value;
		}
	}

	public LocalisedString[] msgComplete2
	{
		get
		{
			return ExposedVariables.msgComplete2;
		}
		set
		{
			ExposedVariables.msgComplete2 = value;
		}
	}

	public SpawnTechData[] enemyTechData1
	{
		get
		{
			return ExposedVariables.enemyTechData1;
		}
		set
		{
			ExposedVariables.enemyTechData1 = value;
		}
	}

	public SpawnTechData[] enemyTechData2
	{
		get
		{
			return ExposedVariables.enemyTechData2;
		}
		set
		{
			ExposedVariables.enemyTechData2 = value;
		}
	}

	public SpawnTechData[] enemyTechData3
	{
		get
		{
			return ExposedVariables.enemyTechData3;
		}
		set
		{
			ExposedVariables.enemyTechData3 = value;
		}
	}

	public LocalisedString[] msgComplete3
	{
		get
		{
			return ExposedVariables.msgComplete3;
		}
		set
		{
			ExposedVariables.msgComplete3 = value;
		}
	}

	public LocalisedString[] msgEnemiesSpotted3
	{
		get
		{
			return ExposedVariables.msgEnemiesSpotted3;
		}
		set
		{
			ExposedVariables.msgEnemiesSpotted3 = value;
		}
	}

	public LocalisedString[] msgComplete4
	{
		get
		{
			return ExposedVariables.msgComplete4;
		}
		set
		{
			ExposedVariables.msgComplete4 = value;
		}
	}

	public LocalisedString[] msgEnemiesSpotted4
	{
		get
		{
			return ExposedVariables.msgEnemiesSpotted4;
		}
		set
		{
			ExposedVariables.msgEnemiesSpotted4 = value;
		}
	}

	public int numWaves
	{
		get
		{
			return ExposedVariables.numWaves;
		}
		set
		{
			ExposedVariables.numWaves = value;
		}
	}

	public SpawnTechData[] enemyTechData4
	{
		get
		{
			return ExposedVariables.enemyTechData4;
		}
		set
		{
			ExposedVariables.enemyTechData4 = value;
		}
	}

	public SpawnTechData[] enemyTechData5
	{
		get
		{
			return ExposedVariables.enemyTechData5;
		}
		set
		{
			ExposedVariables.enemyTechData5 = value;
		}
	}

	public LocalisedString[] msgEnemiesSpotted5
	{
		get
		{
			return ExposedVariables.msgEnemiesSpotted5;
		}
		set
		{
			ExposedVariables.msgEnemiesSpotted5 = value;
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

	public LocalisedString[] msgComplete5
	{
		get
		{
			return ExposedVariables.msgComplete5;
		}
		set
		{
			ExposedVariables.msgComplete5 = value;
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
