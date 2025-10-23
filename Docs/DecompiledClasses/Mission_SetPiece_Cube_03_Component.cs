using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_Cube_03")]
public class Mission_SetPiece_Cube_03_Component : uScriptCode
{
	public Mission_SetPiece_Cube_03 ExposedVariables = new Mission_SetPiece_Cube_03();

	public SpawnTechData[] CrazedTechData
	{
		get
		{
			return ExposedVariables.CrazedTechData;
		}
		set
		{
			ExposedVariables.CrazedTechData = value;
		}
	}

	public SpawnTechData[] FillerTechData
	{
		get
		{
			return ExposedVariables.FillerTechData;
		}
		set
		{
			ExposedVariables.FillerTechData = value;
		}
	}

	public string FillerNPCRange01
	{
		get
		{
			return ExposedVariables.FillerNPCRange01;
		}
		set
		{
			ExposedVariables.FillerNPCRange01 = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker CrazedMinionTechSpeaker
	{
		get
		{
			return ExposedVariables.CrazedMinionTechSpeaker;
		}
		set
		{
			ExposedVariables.CrazedMinionTechSpeaker = value;
		}
	}

	public uScript_AddMessage.MessageData MsgFillerNPC02
	{
		get
		{
			return ExposedVariables.MsgFillerNPC02;
		}
		set
		{
			ExposedVariables.MsgFillerNPC02 = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker SingleMinionTechSpeaker
	{
		get
		{
			return ExposedVariables.SingleMinionTechSpeaker;
		}
		set
		{
			ExposedVariables.SingleMinionTechSpeaker = value;
		}
	}

	public string FillerNPCRange02
	{
		get
		{
			return ExposedVariables.FillerNPCRange02;
		}
		set
		{
			ExposedVariables.FillerNPCRange02 = value;
		}
	}

	public uScript_AddMessage.MessageData MsgFillerNPC01
	{
		get
		{
			return ExposedVariables.MsgFillerNPC01;
		}
		set
		{
			ExposedVariables.MsgFillerNPC01 = value;
		}
	}

	public uScript_AddMessage.MessageData msgCubeTooEarly
	{
		get
		{
			return ExposedVariables.msgCubeTooEarly;
		}
		set
		{
			ExposedVariables.msgCubeTooEarly = value;
		}
	}

	public uScript_AddMessage.MessageData MsgCrazedInterrupt
	{
		get
		{
			return ExposedVariables.MsgCrazedInterrupt;
		}
		set
		{
			ExposedVariables.MsgCrazedInterrupt = value;
		}
	}

	public uScript_PlayDialogue.Dialogue LeaderIntroDialogue
	{
		get
		{
			return ExposedVariables.LeaderIntroDialogue;
		}
		set
		{
			ExposedVariables.LeaderIntroDialogue = value;
		}
	}

	public string CrazedMsgTag
	{
		get
		{
			return ExposedVariables.CrazedMsgTag;
		}
		set
		{
			ExposedVariables.CrazedMsgTag = value;
		}
	}

	public float TimeLimit
	{
		get
		{
			return ExposedVariables.TimeLimit;
		}
		set
		{
			ExposedVariables.TimeLimit = value;
		}
	}

	public uScript_PlayDialogue.Dialogue InvulnerableCubeIntro
	{
		get
		{
			return ExposedVariables.InvulnerableCubeIntro;
		}
		set
		{
			ExposedVariables.InvulnerableCubeIntro = value;
		}
	}

	public uScript_AddMessage.MessageData MsgStartBossFight
	{
		get
		{
			return ExposedVariables.MsgStartBossFight;
		}
		set
		{
			ExposedVariables.MsgStartBossFight = value;
		}
	}

	public SpawnTechData[] CrazedMinionTechData
	{
		get
		{
			return ExposedVariables.CrazedMinionTechData;
		}
		set
		{
			ExposedVariables.CrazedMinionTechData = value;
		}
	}

	public uScript_PlayDialogue.Dialogue SpawnAmbushDialogue
	{
		get
		{
			return ExposedVariables.SpawnAmbushDialogue;
		}
		set
		{
			ExposedVariables.SpawnAmbushDialogue = value;
		}
	}

	public uScript_PlayDialogue.Dialogue NoMoreAmbushDialogue
	{
		get
		{
			return ExposedVariables.NoMoreAmbushDialogue;
		}
		set
		{
			ExposedVariables.NoMoreAmbushDialogue = value;
		}
	}

	public uScript_AddMessage.MessageData MsgOutOfTime
	{
		get
		{
			return ExposedVariables.MsgOutOfTime;
		}
		set
		{
			ExposedVariables.MsgOutOfTime = value;
		}
	}

	public string LeaderIntroStartTrigger
	{
		get
		{
			return ExposedVariables.LeaderIntroStartTrigger;
		}
		set
		{
			ExposedVariables.LeaderIntroStartTrigger = value;
		}
	}

	public SpawnTechData[] CubeTechData
	{
		get
		{
			return ExposedVariables.CubeTechData;
		}
		set
		{
			ExposedVariables.CubeTechData = value;
		}
	}

	public uScript_PlayDialogue.Dialogue FailedToKillInvincibleCubeDialogue
	{
		get
		{
			return ExposedVariables.FailedToKillInvincibleCubeDialogue;
		}
		set
		{
			ExposedVariables.FailedToKillInvincibleCubeDialogue = value;
		}
	}

	public SpawnTechData[] CubeAmbushTechData
	{
		get
		{
			return ExposedVariables.CubeAmbushTechData;
		}
		set
		{
			ExposedVariables.CubeAmbushTechData = value;
		}
	}

	public uScript_PlayDialogue.Dialogue CubeDefeatedDialogue
	{
		get
		{
			return ExposedVariables.CubeDefeatedDialogue;
		}
		set
		{
			ExposedVariables.CubeDefeatedDialogue = value;
		}
	}

	public Transform TechFlyParticles
	{
		get
		{
			return ExposedVariables.TechFlyParticles;
		}
		set
		{
			ExposedVariables.TechFlyParticles = value;
		}
	}

	public ExternalBehaviorTree TechFlyAI
	{
		get
		{
			return ExposedVariables.TechFlyAI;
		}
		set
		{
			ExposedVariables.TechFlyAI = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker GCSpeaker
	{
		get
		{
			return ExposedVariables.GCSpeaker;
		}
		set
		{
			ExposedVariables.GCSpeaker = value;
		}
	}

	public uScript_AddMessage.MessageData MsgMissionCompleteNoTrigger
	{
		get
		{
			return ExposedVariables.MsgMissionCompleteNoTrigger;
		}
		set
		{
			ExposedVariables.MsgMissionCompleteNoTrigger = value;
		}
	}

	public BlockTypes CubeShieldBlockData
	{
		get
		{
			return ExposedVariables.CubeShieldBlockData;
		}
		set
		{
			ExposedVariables.CubeShieldBlockData = value;
		}
	}

	public uScript_AddMessage.MessageData MsgInvincibleCubeSwitchOff
	{
		get
		{
			return ExposedVariables.MsgInvincibleCubeSwitchOff;
		}
		set
		{
			ExposedVariables.MsgInvincibleCubeSwitchOff = value;
		}
	}

	public string LeaderOutOfRangeTrigger
	{
		get
		{
			return ExposedVariables.LeaderOutOfRangeTrigger;
		}
		set
		{
			ExposedVariables.LeaderOutOfRangeTrigger = value;
		}
	}

	public uScript_PlayDialogue.Dialogue CompletelyNoMoreAmbushDialogue
	{
		get
		{
			return ExposedVariables.CompletelyNoMoreAmbushDialogue;
		}
		set
		{
			ExposedVariables.CompletelyNoMoreAmbushDialogue = value;
		}
	}

	public string CubeAreaTrigger
	{
		get
		{
			return ExposedVariables.CubeAreaTrigger;
		}
		set
		{
			ExposedVariables.CubeAreaTrigger = value;
		}
	}

	public uScript_AddMessage.MessageData MsgMultiplayerLeaveArea
	{
		get
		{
			return ExposedVariables.MsgMultiplayerLeaveArea;
		}
		set
		{
			ExposedVariables.MsgMultiplayerLeaveArea = value;
		}
	}

	public uScript_AddMessage.MessageData MsgLeaderTryAgainFollow
	{
		get
		{
			return ExposedVariables.MsgLeaderTryAgainFollow;
		}
		set
		{
			ExposedVariables.MsgLeaderTryAgainFollow = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker CrazedLeaderSpeaker
	{
		get
		{
			return ExposedVariables.CrazedLeaderSpeaker;
		}
		set
		{
			ExposedVariables.CrazedLeaderSpeaker = value;
		}
	}

	public uScript_PlayDialogue.Dialogue LeaderTryAgainDialogue
	{
		get
		{
			return ExposedVariables.LeaderTryAgainDialogue;
		}
		set
		{
			ExposedVariables.LeaderTryAgainDialogue = value;
		}
	}

	public uScript_PlayDialogue.Dialogue MsgFinalCubeDeathDialogue
	{
		get
		{
			return ExposedVariables.MsgFinalCubeDeathDialogue;
		}
		set
		{
			ExposedVariables.MsgFinalCubeDeathDialogue = value;
		}
	}

	public uScript_PlayDialogue.Dialogue StartBossFightDialogue
	{
		get
		{
			return ExposedVariables.StartBossFightDialogue;
		}
		set
		{
			ExposedVariables.StartBossFightDialogue = value;
		}
	}

	public uScript_PlayDialogue.Dialogue SpawnAmbushDeadDialogue
	{
		get
		{
			return ExposedVariables.SpawnAmbushDeadDialogue;
		}
		set
		{
			ExposedVariables.SpawnAmbushDeadDialogue = value;
		}
	}

	public uScript_PlayDialogue.Dialogue OutofTimeDialogue
	{
		get
		{
			return ExposedVariables.OutofTimeDialogue;
		}
		set
		{
			ExposedVariables.OutofTimeDialogue = value;
		}
	}

	public uScript_PlayDialogue.Dialogue CubeLeaveAreaFailDialogue
	{
		get
		{
			return ExposedVariables.CubeLeaveAreaFailDialogue;
		}
		set
		{
			ExposedVariables.CubeLeaveAreaFailDialogue = value;
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

	private void OnGUI()
	{
		ExposedVariables.OnGUI();
	}
}
