using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_Test")]
public class Mission_SetPiece_Test_Component : uScriptCode
{
	public Mission_SetPiece_Test ExposedVariables = new Mission_SetPiece_Test();

	public SpawnTechData[] TechData
	{
		get
		{
			return ExposedVariables.TechData;
		}
		set
		{
			ExposedVariables.TechData = value;
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
