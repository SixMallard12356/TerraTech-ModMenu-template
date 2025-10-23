using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_CommunityEvent_ARG_Encounter_2023")]
public class Mission_CommunityEvent_ARG_Encounter_2023_Component : uScriptCode
{
	public Mission_CommunityEvent_ARG_Encounter_2023 ExposedVariables = new Mission_CommunityEvent_ARG_Encounter_2023();

	public uScript_AddMessage.MessageData msg01HUBL
	{
		get
		{
			return ExposedVariables.msg01HUBL;
		}
		set
		{
			ExposedVariables.msg01HUBL = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker messageSpeaker01
	{
		get
		{
			return ExposedVariables.messageSpeaker01;
		}
		set
		{
			ExposedVariables.messageSpeaker01 = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker messageSpeaker02
	{
		get
		{
			return ExposedVariables.messageSpeaker02;
		}
		set
		{
			ExposedVariables.messageSpeaker02 = value;
		}
	}

	public uScript_AddMessage.MessageData msg02RUSTY
	{
		get
		{
			return ExposedVariables.msg02RUSTY;
		}
		set
		{
			ExposedVariables.msg02RUSTY = value;
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
