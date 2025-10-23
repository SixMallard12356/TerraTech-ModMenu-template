using UnityEngine;

[AddComponentMenu("uScript/Graphs/SubGraph_RR_CombatLab_ForcefieldAndChargerController")]
public class SubGraph_RR_CombatLab_ForcefieldAndChargerController_Component : uScriptCode
{
	public SubGraph_RR_CombatLab_ForcefieldAndChargerController ExposedVariables = new SubGraph_RR_CombatLab_ForcefieldAndChargerController();

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
