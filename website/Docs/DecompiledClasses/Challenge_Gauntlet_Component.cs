using UnityEngine;

[AddComponentMenu("uScript/Graphs/Challenge_Gauntlet")]
public class Challenge_Gauntlet_Component : uScriptCode
{
	public Challenge_Gauntlet ExposedVariables = new Challenge_Gauntlet();

	public LocalisedString[] msgBuildTech
	{
		get
		{
			return ExposedVariables.msgBuildTech;
		}
		set
		{
			ExposedVariables.msgBuildTech = value;
		}
	}

	public LocalisedString[] msgReadyToStart
	{
		get
		{
			return ExposedVariables.msgReadyToStart;
		}
		set
		{
			ExposedVariables.msgReadyToStart = value;
		}
	}

	public LocalisedString MsgIntroScreen
	{
		get
		{
			return ExposedVariables.MsgIntroScreen;
		}
		set
		{
			ExposedVariables.MsgIntroScreen = value;
		}
	}

	public LocalisedString[] msgGauntletComplete
	{
		get
		{
			return ExposedVariables.msgGauntletComplete;
		}
		set
		{
			ExposedVariables.msgGauntletComplete = value;
		}
	}

	public LocalisedString[] msgOutOfBoundsWarning
	{
		get
		{
			return ExposedVariables.msgOutOfBoundsWarning;
		}
		set
		{
			ExposedVariables.msgOutOfBoundsWarning = value;
		}
	}

	public LocalisedString[] msgOutOfBounds
	{
		get
		{
			return ExposedVariables.msgOutOfBounds;
		}
		set
		{
			ExposedVariables.msgOutOfBounds = value;
		}
	}

	public float startAreaMsgDuration
	{
		get
		{
			return ExposedVariables.startAreaMsgDuration;
		}
		set
		{
			ExposedVariables.startAreaMsgDuration = value;
		}
	}

	public float startAreaMsgRefreshTime
	{
		get
		{
			return ExposedVariables.startAreaMsgRefreshTime;
		}
		set
		{
			ExposedVariables.startAreaMsgRefreshTime = value;
		}
	}

	public LocalisedString Accept
	{
		get
		{
			return ExposedVariables.Accept;
		}
		set
		{
			ExposedVariables.Accept = value;
		}
	}

	public LocalisedString[] msgBuildTech_Pad
	{
		get
		{
			return ExposedVariables.msgBuildTech_Pad;
		}
		set
		{
			ExposedVariables.msgBuildTech_Pad = value;
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
