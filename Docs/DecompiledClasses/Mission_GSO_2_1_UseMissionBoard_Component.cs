using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_2_1_UseMissionBoard")]
public class Mission_GSO_2_1_UseMissionBoard_Component : uScriptCode
{
	public Mission_GSO_2_1_UseMissionBoard ExposedVariables = new Mission_GSO_2_1_UseMissionBoard();

	public uScript_AddMessage.MessageData msg01TradingStationIntro
	{
		get
		{
			return ExposedVariables.msg01TradingStationIntro;
		}
		set
		{
			ExposedVariables.msg01TradingStationIntro = value;
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

	public uScript_AddMessage.MessageData msg02OpenMissionBoard
	{
		get
		{
			return ExposedVariables.msg02OpenMissionBoard;
		}
		set
		{
			ExposedVariables.msg02OpenMissionBoard = value;
		}
	}

	public uScript_AddMessage.MessageData msg02OpenMissionBoard_Pad
	{
		get
		{
			return ExposedVariables.msg02OpenMissionBoard_Pad;
		}
		set
		{
			ExposedVariables.msg02OpenMissionBoard_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg03AcceptMission_Pad
	{
		get
		{
			return ExposedVariables.msg03AcceptMission_Pad;
		}
		set
		{
			ExposedVariables.msg03AcceptMission_Pad = value;
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

	public uScript_AddMessage.MessageData msg03AcceptMission
	{
		get
		{
			return ExposedVariables.msg03AcceptMission;
		}
		set
		{
			ExposedVariables.msg03AcceptMission = value;
		}
	}

	public BlockTypes missionBoardBlockType
	{
		get
		{
			return ExposedVariables.missionBoardBlockType;
		}
		set
		{
			ExposedVariables.missionBoardBlockType = value;
		}
	}

	public Vector3 missionBoardArrowOffset
	{
		get
		{
			return ExposedVariables.missionBoardArrowOffset;
		}
		set
		{
			ExposedVariables.missionBoardArrowOffset = value;
		}
	}

	public uScript_PlayDialogue.Dialogue TradingStationIntroDialogue
	{
		get
		{
			return ExposedVariables.TradingStationIntroDialogue;
		}
		set
		{
			ExposedVariables.TradingStationIntroDialogue = value;
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
