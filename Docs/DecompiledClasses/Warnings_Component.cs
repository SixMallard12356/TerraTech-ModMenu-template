using UnityEngine;

[AddComponentMenu("uScript/Graphs/Warnings")]
public class Warnings_Component : uScriptCode
{
	public Warnings ExposedVariables = new Warnings();

	public LocalisedString[] msgBaseUnderAttack
	{
		get
		{
			return ExposedVariables.msgBaseUnderAttack;
		}
		set
		{
			ExposedVariables.msgBaseUnderAttack = value;
		}
	}

	public LocalisedString[] msgHeartBlockAttacked
	{
		get
		{
			return ExposedVariables.msgHeartBlockAttacked;
		}
		set
		{
			ExposedVariables.msgHeartBlockAttacked = value;
		}
	}

	public uScript_AddMessage.MessageData msgTippedOverFallen
	{
		get
		{
			return ExposedVariables.msgTippedOverFallen;
		}
		set
		{
			ExposedVariables.msgTippedOverFallen = value;
		}
	}

	public uScript_AddMessage.MessageData msgTippedOverFallen_Pad
	{
		get
		{
			return ExposedVariables.msgTippedOverFallen_Pad;
		}
		set
		{
			ExposedVariables.msgTippedOverFallen_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msgTippedOverRecovered_Pad
	{
		get
		{
			return ExposedVariables.msgTippedOverRecovered_Pad;
		}
		set
		{
			ExposedVariables.msgTippedOverRecovered_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msgTippedOverRecovered
	{
		get
		{
			return ExposedVariables.msgTippedOverRecovered;
		}
		set
		{
			ExposedVariables.msgTippedOverRecovered = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker messageSpeaker
	{
		get
		{
			return ExposedVariables.messageSpeaker;
		}
		set
		{
			ExposedVariables.messageSpeaker = value;
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
