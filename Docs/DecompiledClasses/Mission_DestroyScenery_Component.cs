using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_DestroyScenery")]
public class Mission_DestroyScenery_Component : uScriptCode
{
	public Mission_DestroyScenery ExposedVariables = new Mission_DestroyScenery();

	public SceneryTypes targetSceneryType
	{
		get
		{
			return ExposedVariables.targetSceneryType;
		}
		set
		{
			ExposedVariables.targetSceneryType = value;
		}
	}

	public int targetAmount
	{
		get
		{
			return ExposedVariables.targetAmount;
		}
		set
		{
			ExposedVariables.targetAmount = value;
		}
	}

	public bool useSceneryType
	{
		get
		{
			return ExposedVariables.useSceneryType;
		}
		set
		{
			ExposedVariables.useSceneryType = value;
		}
	}

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

	public LocalisedString[] msgStart
	{
		get
		{
			return ExposedVariables.msgStart;
		}
		set
		{
			ExposedVariables.msgStart = value;
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
