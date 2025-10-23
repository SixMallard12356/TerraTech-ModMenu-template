using UnityEngine;

[AddComponentMenu("uScript/Graphs/Prototype_RD_Racing_Challenge")]
public class Prototype_RD_Racing_Challenge_Component : uScriptCode
{
	public Prototype_RD_Racing_Challenge ExposedVariables = new Prototype_RD_Racing_Challenge();

	public LocalisedString[] msgWin
	{
		get
		{
			return ExposedVariables.msgWin;
		}
		set
		{
			ExposedVariables.msgWin = value;
		}
	}

	public ChallengeData challengeData
	{
		get
		{
			return ExposedVariables.challengeData;
		}
		set
		{
			ExposedVariables.challengeData = value;
		}
	}

	public LocalisedString startPromptText
	{
		get
		{
			return ExposedVariables.startPromptText;
		}
		set
		{
			ExposedVariables.startPromptText = value;
		}
	}

	public LocalisedString msgContinue
	{
		get
		{
			return ExposedVariables.msgContinue;
		}
		set
		{
			ExposedVariables.msgContinue = value;
		}
	}

	public LocalisedString msgCancel
	{
		get
		{
			return ExposedVariables.msgCancel;
		}
		set
		{
			ExposedVariables.msgCancel = value;
		}
	}

	public GameObject startingTape
	{
		get
		{
			return ExposedVariables.startingTape;
		}
		set
		{
			ExposedVariables.startingTape = value;
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
