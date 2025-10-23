using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_3_AITutorial")]
public class Mission_GSO_3_AITutorial_Component : uScriptCode
{
	public Mission_GSO_3_AITutorial ExposedVariables = new Mission_GSO_3_AITutorial();

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

	public string messageTag
	{
		get
		{
			return ExposedVariables.messageTag;
		}
		set
		{
			ExposedVariables.messageTag = value;
		}
	}

	public FTUEEnumType FTUEAIMenuOpened
	{
		get
		{
			return ExposedVariables.FTUEAIMenuOpened;
		}
		set
		{
			ExposedVariables.FTUEAIMenuOpened = value;
		}
	}

	public float distAtWhichTechFound
	{
		get
		{
			return ExposedVariables.distAtWhichTechFound;
		}
		set
		{
			ExposedVariables.distAtWhichTechFound = value;
		}
	}

	public uScript_AddMessage.MessageData msg01FindTech
	{
		get
		{
			return ExposedVariables.msg01FindTech;
		}
		set
		{
			ExposedVariables.msg01FindTech = value;
		}
	}

	public uScript_AddMessage.MessageData msg02TechFound
	{
		get
		{
			return ExposedVariables.msg02TechFound;
		}
		set
		{
			ExposedVariables.msg02TechFound = value;
		}
	}

	public uScript_AddMessage.MessageData msg03AccessMenu
	{
		get
		{
			return ExposedVariables.msg03AccessMenu;
		}
		set
		{
			ExposedVariables.msg03AccessMenu = value;
		}
	}

	public uScript_AddMessage.MessageData msg04TutorialComplete
	{
		get
		{
			return ExposedVariables.msg04TutorialComplete;
		}
		set
		{
			ExposedVariables.msg04TutorialComplete = value;
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
