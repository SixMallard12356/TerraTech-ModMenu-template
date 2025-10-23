using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_DefendAlliedTechs")]
public class Mission_DefendAlliedTechs_Component : uScriptCode
{
	public Mission_DefendAlliedTechs ExposedVariables = new Mission_DefendAlliedTechs();

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

	public SpawnTechData[] wave01Techs
	{
		get
		{
			return ExposedVariables.wave01Techs;
		}
		set
		{
			ExposedVariables.wave01Techs = value;
		}
	}

	public SpawnTechData[] wave02Techs
	{
		get
		{
			return ExposedVariables.wave02Techs;
		}
		set
		{
			ExposedVariables.wave02Techs = value;
		}
	}

	public SpawnTechData[] wave03Techs
	{
		get
		{
			return ExposedVariables.wave03Techs;
		}
		set
		{
			ExposedVariables.wave03Techs = value;
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

	public SpawnTechData[] wave04Techs
	{
		get
		{
			return ExposedVariables.wave04Techs;
		}
		set
		{
			ExposedVariables.wave04Techs = value;
		}
	}

	public SpawnTechData[] wave05Techs
	{
		get
		{
			return ExposedVariables.wave05Techs;
		}
		set
		{
			ExposedVariables.wave05Techs = value;
		}
	}

	public float autoTriggerNextWaveTime
	{
		get
		{
			return ExposedVariables.autoTriggerNextWaveTime;
		}
		set
		{
			ExposedVariables.autoTriggerNextWaveTime = value;
		}
	}

	public float timeBeforeNextWave
	{
		get
		{
			return ExposedVariables.timeBeforeNextWave;
		}
		set
		{
			ExposedVariables.timeBeforeNextWave = value;
		}
	}

	public bool autoTriggerNextWave
	{
		get
		{
			return ExposedVariables.autoTriggerNextWave;
		}
		set
		{
			ExposedVariables.autoTriggerNextWave = value;
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
