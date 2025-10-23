using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_CaptureEnemyBase")]
public class Mission_CaptureEnemyBase_Component : uScriptCode
{
	public Mission_CaptureEnemyBase ExposedVariables = new Mission_CaptureEnemyBase();

	public SpawnTechData baseData
	{
		get
		{
			return ExposedVariables.baseData;
		}
		set
		{
			ExposedVariables.baseData = value;
		}
	}

	public SpawnTechData[] guardianData
	{
		get
		{
			return ExposedVariables.guardianData;
		}
		set
		{
			ExposedVariables.guardianData = value;
		}
	}

	public SpawnTechData[] harvesterData
	{
		get
		{
			return ExposedVariables.harvesterData;
		}
		set
		{
			ExposedVariables.harvesterData = value;
		}
	}

	public LocalisedString[] msgBaseSpotted
	{
		get
		{
			return ExposedVariables.msgBaseSpotted;
		}
		set
		{
			ExposedVariables.msgBaseSpotted = value;
		}
	}

	public LocalisedString[] msgBaseDestroyed
	{
		get
		{
			return ExposedVariables.msgBaseDestroyed;
		}
		set
		{
			ExposedVariables.msgBaseDestroyed = value;
		}
	}

	public LocalisedString[] msgBaseCaptured
	{
		get
		{
			return ExposedVariables.msgBaseCaptured;
		}
		set
		{
			ExposedVariables.msgBaseCaptured = value;
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
