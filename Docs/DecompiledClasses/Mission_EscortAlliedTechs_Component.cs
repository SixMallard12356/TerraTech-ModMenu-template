using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_EscortAlliedTechs")]
public class Mission_EscortAlliedTechs_Component : uScriptCode
{
	public Mission_EscortAlliedTechs ExposedVariables = new Mission_EscortAlliedTechs();

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

	public LocalisedString[] msgWin
	{
		get
		{
			return ExposedVariables.msgWin;
		}
		set
		{
			ExposedVariables.msgWin = value;
		}
	}

	public SpawnTechData[] enemyGroup1
	{
		get
		{
			return ExposedVariables.enemyGroup1;
		}
		set
		{
			ExposedVariables.enemyGroup1 = value;
		}
	}

	public SpawnTechData[] enemyGroup2
	{
		get
		{
			return ExposedVariables.enemyGroup2;
		}
		set
		{
			ExposedVariables.enemyGroup2 = value;
		}
	}

	public SpawnTechData[] enemyGroup3
	{
		get
		{
			return ExposedVariables.enemyGroup3;
		}
		set
		{
			ExposedVariables.enemyGroup3 = value;
		}
	}

	public float[] enemySpawnThresholds
	{
		get
		{
			return ExposedVariables.enemySpawnThresholds;
		}
		set
		{
			ExposedVariables.enemySpawnThresholds = value;
		}
	}

	public float enemySpawnDistFromPlayer
	{
		get
		{
			return ExposedVariables.enemySpawnDistFromPlayer;
		}
		set
		{
			ExposedVariables.enemySpawnDistFromPlayer = value;
		}
	}

	public SpawnTechData destinationObject
	{
		get
		{
			return ExposedVariables.destinationObject;
		}
		set
		{
			ExposedVariables.destinationObject = value;
		}
	}

	public SpawnTechData[] techsToDefend
	{
		get
		{
			return ExposedVariables.techsToDefend;
		}
		set
		{
			ExposedVariables.techsToDefend = value;
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
