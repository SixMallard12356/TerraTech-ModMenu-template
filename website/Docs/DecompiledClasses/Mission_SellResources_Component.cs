using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SellResources")]
public class Mission_SellResources_Component : uScriptCode
{
	public Mission_SellResources ExposedVariables = new Mission_SellResources();

	public ChunkTypes targetResourceType
	{
		get
		{
			return ExposedVariables.targetResourceType;
		}
		set
		{
			ExposedVariables.targetResourceType = value;
		}
	}

	public bool useResourceType
	{
		get
		{
			return ExposedVariables.useResourceType;
		}
		set
		{
			ExposedVariables.useResourceType = value;
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

	public uScript_AddMessage.MessageData msgComplete
	{
		get
		{
			return ExposedVariables.msgComplete;
		}
		set
		{
			ExposedVariables.msgComplete = value;
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
