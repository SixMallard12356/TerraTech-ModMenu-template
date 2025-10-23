using UnityEngine;

[AddComponentMenu("uScript/Graphs/SubGraph_EscortAlliedTechs_SpawnEnemyGroup")]
public class SubGraph_EscortAlliedTechs_SpawnEnemyGroup_Component : uScriptCode
{
	public SubGraph_EscortAlliedTechs_SpawnEnemyGroup ExposedVariables = new SubGraph_EscortAlliedTechs_SpawnEnemyGroup();

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
