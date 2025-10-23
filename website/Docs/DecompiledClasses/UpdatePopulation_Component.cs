using UnityEngine;

[AddComponentMenu("uScript/Graphs/UpdatePopulation")]
public class UpdatePopulation_Component : uScriptCode
{
	public UpdatePopulation ExposedVariables = new UpdatePopulation();

	public bool PopSize4LargeGuard
	{
		get
		{
			return ExposedVariables.PopSize4LargeGuard;
		}
		set
		{
			ExposedVariables.PopSize4LargeGuard = value;
		}
	}

	public bool PopSize5HugeGuard
	{
		get
		{
			return ExposedVariables.PopSize5HugeGuard;
		}
		set
		{
			ExposedVariables.PopSize5HugeGuard = value;
		}
	}

	public bool PopSize1TinyGuard
	{
		get
		{
			return ExposedVariables.PopSize1TinyGuard;
		}
		set
		{
			ExposedVariables.PopSize1TinyGuard = value;
		}
	}

	public bool PopSize3MediumGuard
	{
		get
		{
			return ExposedVariables.PopSize3MediumGuard;
		}
		set
		{
			ExposedVariables.PopSize3MediumGuard = value;
		}
	}

	public bool PopSize2LittleGuard
	{
		get
		{
			return ExposedVariables.PopSize2LittleGuard;
		}
		set
		{
			ExposedVariables.PopSize2LittleGuard = value;
		}
	}

	public bool PopSize2LittleFlee
	{
		get
		{
			return ExposedVariables.PopSize2LittleFlee;
		}
		set
		{
			ExposedVariables.PopSize2LittleFlee = value;
		}
	}

	public bool PopSize3MediumFlee
	{
		get
		{
			return ExposedVariables.PopSize3MediumFlee;
		}
		set
		{
			ExposedVariables.PopSize3MediumFlee = value;
		}
	}

	public bool PopSize1TinyFlee
	{
		get
		{
			return ExposedVariables.PopSize1TinyFlee;
		}
		set
		{
			ExposedVariables.PopSize1TinyFlee = value;
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
