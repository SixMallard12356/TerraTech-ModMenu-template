using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_UnlockCorporationLicense")]
public class Mission_UnlockCorporationLicense_Component : uScriptCode
{
	public Mission_UnlockCorporationLicense ExposedVariables = new Mission_UnlockCorporationLicense();

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

	public float distEnemiesSpotted
	{
		get
		{
			return ExposedVariables.distEnemiesSpotted;
		}
		set
		{
			ExposedVariables.distEnemiesSpotted = value;
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
