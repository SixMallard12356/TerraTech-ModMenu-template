using UnityEngine;

[AddComponentMenu("uScript/Graphs/BlockInteractionTest")]
public class BlockInteractionTest_Component : uScriptCode
{
	public BlockInteractionTest ExposedVariables = new BlockInteractionTest();

	public SpawnTechData[] techData
	{
		get
		{
			return ExposedVariables.techData;
		}
		set
		{
			ExposedVariables.techData = value;
		}
	}

	public uScript_AddMessage.MessageData msgBlockClicked
	{
		get
		{
			return ExposedVariables.msgBlockClicked;
		}
		set
		{
			ExposedVariables.msgBlockClicked = value;
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

	public BlockTypes interactableBlockType
	{
		get
		{
			return ExposedVariables.interactableBlockType;
		}
		set
		{
			ExposedVariables.interactableBlockType = value;
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
