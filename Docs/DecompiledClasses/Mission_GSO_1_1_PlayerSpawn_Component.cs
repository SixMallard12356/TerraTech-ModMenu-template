using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_1_1_PlayerSpawn")]
public class Mission_GSO_1_1_PlayerSpawn_Component : uScriptCode
{
	public Mission_GSO_1_1_PlayerSpawn ExposedVariables = new Mission_GSO_1_1_PlayerSpawn();

	public Transform FallEffect
	{
		get
		{
			return ExposedVariables.FallEffect;
		}
		set
		{
			ExposedVariables.FallEffect = value;
		}
	}

	public TankPreset CabPreset
	{
		get
		{
			return ExposedVariables.CabPreset;
		}
		set
		{
			ExposedVariables.CabPreset = value;
		}
	}

	public TankPreset BuiltTechPreset
	{
		get
		{
			return ExposedVariables.BuiltTechPreset;
		}
		set
		{
			ExposedVariables.BuiltTechPreset = value;
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
