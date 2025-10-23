using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_DestroyFleeingTech")]
public class Mission_DestroyFleeingTech_Component : uScriptCode
{
	public Mission_DestroyFleeingTech ExposedVariables = new Mission_DestroyFleeingTech();

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

	public LocalisedString[] msgMissionComplete
	{
		get
		{
			return ExposedVariables.msgMissionComplete;
		}
		set
		{
			ExposedVariables.msgMissionComplete = value;
		}
	}

	public LocalisedString[] msgFailedOutOfTime
	{
		get
		{
			return ExposedVariables.msgFailedOutOfTime;
		}
		set
		{
			ExposedVariables.msgFailedOutOfTime = value;
		}
	}

	public LocalisedString[] msgFailedLostThem
	{
		get
		{
			return ExposedVariables.msgFailedLostThem;
		}
		set
		{
			ExposedVariables.msgFailedLostThem = value;
		}
	}

	public LocalisedString[] msgEnemiesSpotted
	{
		get
		{
			return ExposedVariables.msgEnemiesSpotted;
		}
		set
		{
			ExposedVariables.msgEnemiesSpotted = value;
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
