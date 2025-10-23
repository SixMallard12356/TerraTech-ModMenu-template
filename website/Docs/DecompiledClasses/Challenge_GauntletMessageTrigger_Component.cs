using UnityEngine;

[AddComponentMenu("uScript/Graphs/Challenge_GauntletMessageTrigger")]
public class Challenge_GauntletMessageTrigger_Component : uScriptCode
{
	public Challenge_GauntletMessageTrigger ExposedVariables = new Challenge_GauntletMessageTrigger();

	public LocalisedString[] msgString
	{
		get
		{
			return ExposedVariables.msgString;
		}
		set
		{
			ExposedVariables.msgString = value;
		}
	}

	public bool holdMsg
	{
		get
		{
			return ExposedVariables.holdMsg;
		}
		set
		{
			ExposedVariables.holdMsg = value;
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
