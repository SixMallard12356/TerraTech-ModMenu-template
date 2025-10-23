using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_Cube")]
public class Mission_SetPiece_Cube_Component : uScriptCode
{
	public Mission_SetPiece_Cube ExposedVariables = new Mission_SetPiece_Cube();

	public uScript_AddMessage.MessageData MsgNPCIntro01
	{
		get
		{
			return ExposedVariables.MsgNPCIntro01;
		}
		set
		{
			ExposedVariables.MsgNPCIntro01 = value;
		}
	}

	public uScript_AddMessage.MessageData MsgCubeDestroyed02
	{
		get
		{
			return ExposedVariables.MsgCubeDestroyed02;
		}
		set
		{
			ExposedVariables.MsgCubeDestroyed02 = value;
		}
	}

	public uScript_AddMessage.MessageData MsgCubeLeaveAreaFail
	{
		get
		{
			return ExposedVariables.MsgCubeLeaveAreaFail;
		}
		set
		{
			ExposedVariables.MsgCubeLeaveAreaFail = value;
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

	public string CubeFailTrigger
	{
		get
		{
			return ExposedVariables.CubeFailTrigger;
		}
		set
		{
			ExposedVariables.CubeFailTrigger = value;
		}
	}

	public uScript_AddMessage.MessageData MsgCrazedAmbush
	{
		get
		{
			return ExposedVariables.MsgCrazedAmbush;
		}
		set
		{
			ExposedVariables.MsgCrazedAmbush = value;
		}
	}

	public string CrazedNPCTrigger
	{
		get
		{
			return ExposedVariables.CrazedNPCTrigger;
		}
		set
		{
			ExposedVariables.CrazedNPCTrigger = value;
		}
	}

	public string NPCMsgTag
	{
		get
		{
			return ExposedVariables.NPCMsgTag;
		}
		set
		{
			ExposedVariables.NPCMsgTag = value;
		}
	}

	public uScript_AddMessage.MessageData MsgNPCInterrupt
	{
		get
		{
			return ExposedVariables.MsgNPCInterrupt;
		}
		set
		{
			ExposedVariables.MsgNPCInterrupt = value;
		}
	}

	public uScript_AddMessage.MessageData MsgCrazedIntro01
	{
		get
		{
			return ExposedVariables.MsgCrazedIntro01;
		}
		set
		{
			ExposedVariables.MsgCrazedIntro01 = value;
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

	public uScript_AddMessage.MessageData MsgCrazedMinionInterrupt01
	{
		get
		{
			return ExposedVariables.MsgCrazedMinionInterrupt01;
		}
		set
		{
			ExposedVariables.MsgCrazedMinionInterrupt01 = value;
		}
	}

	public uScript_AddMessage.MessageData MsgCrazedIntro04
	{
		get
		{
			return ExposedVariables.MsgCrazedIntro04;
		}
		set
		{
			ExposedVariables.MsgCrazedIntro04 = value;
		}
	}

	public uScript_AddMessage.MessageData MsgCrazedIntro03
	{
		get
		{
			return ExposedVariables.MsgCrazedIntro03;
		}
		set
		{
			ExposedVariables.MsgCrazedIntro03 = value;
		}
	}

	public uScript_AddMessage.MessageData MsgCrazedMinionInterrupt02
	{
		get
		{
			return ExposedVariables.MsgCrazedMinionInterrupt02;
		}
		set
		{
			ExposedVariables.MsgCrazedMinionInterrupt02 = value;
		}
	}

	public uScript_AddMessage.MessageData MsgCrazedLeaderB4Fight01
	{
		get
		{
			return ExposedVariables.MsgCrazedLeaderB4Fight01;
		}
		set
		{
			ExposedVariables.MsgCrazedLeaderB4Fight01 = value;
		}
	}

	public uScript_AddMessage.MessageData MsgCrazedMinionInterruptB4Fight01
	{
		get
		{
			return ExposedVariables.MsgCrazedMinionInterruptB4Fight01;
		}
		set
		{
			ExposedVariables.MsgCrazedMinionInterruptB4Fight01 = value;
		}
	}

	public uScript_AddMessage.MessageData MsgCrazedLeaderB4Fight02
	{
		get
		{
			return ExposedVariables.MsgCrazedLeaderB4Fight02;
		}
		set
		{
			ExposedVariables.MsgCrazedLeaderB4Fight02 = value;
		}
	}

	public uScript_AddMessage.MessageData MsgCrazedLeaderB4Fight03
	{
		get
		{
			return ExposedVariables.MsgCrazedLeaderB4Fight03;
		}
		set
		{
			ExposedVariables.MsgCrazedLeaderB4Fight03 = value;
		}
	}

	public uScript_AddMessage.MessageData MsgCubeDestroyed01
	{
		get
		{
			return ExposedVariables.MsgCubeDestroyed01;
		}
		set
		{
			ExposedVariables.MsgCubeDestroyed01 = value;
		}
	}

	public uScript_AddMessage.MessageData MsgCrazedIntro05
	{
		get
		{
			return ExposedVariables.MsgCrazedIntro05;
		}
		set
		{
			ExposedVariables.MsgCrazedIntro05 = value;
		}
	}

	public uScript_AddMessage.MessageData MsgCrazedMinionInterrupt03
	{
		get
		{
			return ExposedVariables.MsgCrazedMinionInterrupt03;
		}
		set
		{
			ExposedVariables.MsgCrazedMinionInterrupt03 = value;
		}
	}

	public uScript_AddMessage.MessageData MsgCrazedIntro02
	{
		get
		{
			return ExposedVariables.MsgCrazedIntro02;
		}
		set
		{
			ExposedVariables.MsgCrazedIntro02 = value;
		}
	}

	public string NPCIntroStartTrigger
	{
		get
		{
			return ExposedVariables.NPCIntroStartTrigger;
		}
		set
		{
			ExposedVariables.NPCIntroStartTrigger = value;
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

	public string NPCIntroTechInRange
	{
		get
		{
			return ExposedVariables.NPCIntroTechInRange;
		}
		set
		{
			ExposedVariables.NPCIntroTechInRange = value;
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

	public uScript_AddMessage.MessageData MsgMissionComplete
	{
		get
		{
			return ExposedVariables.MsgMissionComplete;
		}
		set
		{
			ExposedVariables.MsgMissionComplete = value;
		}
	}

	public uScript_AddMessage.MessageData msgTooEarly
	{
		get
		{
			return ExposedVariables.msgTooEarly;
		}
		set
		{
			ExposedVariables.msgTooEarly = value;
		}
	}

	public uScript_AddMessage.MessageData MsgNPCIntro02
	{
		get
		{
			return ExposedVariables.MsgNPCIntro02;
		}
		set
		{
			ExposedVariables.MsgNPCIntro02 = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker NPCTechSpeaker
	{
		get
		{
			return ExposedVariables.NPCTechSpeaker;
		}
		set
		{
			ExposedVariables.NPCTechSpeaker = value;
		}
	}

	public uScript_AddMessage.MessageData MsgOutOfTime2
	{
		get
		{
			return ExposedVariables.MsgOutOfTime2;
		}
		set
		{
			ExposedVariables.MsgOutOfTime2 = value;
		}
	}

	public uScript_AddMessage.MessageData msgTooEarly2
	{
		get
		{
			return ExposedVariables.msgTooEarly2;
		}
		set
		{
			ExposedVariables.msgTooEarly2 = value;
		}
	}

	public uScript_AddMessage.MessageData MsgLeaderTryAgain01
	{
		get
		{
			return ExposedVariables.MsgLeaderTryAgain01;
		}
		set
		{
			ExposedVariables.MsgLeaderTryAgain01 = value;
		}
	}

	public uScript_AddMessage.MessageData MsgLeftAreaCompletely
	{
		get
		{
			return ExposedVariables.MsgLeftAreaCompletely;
		}
		set
		{
			ExposedVariables.MsgLeftAreaCompletely = value;
		}
	}

	public string MissionArea
	{
		get
		{
			return ExposedVariables.MissionArea;
		}
		set
		{
			ExposedVariables.MissionArea = value;
		}
	}

	public uScript_AddMessage.MessageData MsgMinionsDead
	{
		get
		{
			return ExposedVariables.MsgMinionsDead;
		}
		set
		{
			ExposedVariables.MsgMinionsDead = value;
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

	public uScript_AddMessage.MessageSpeaker GroupMinionTechSpeaker
	{
		get
		{
			return ExposedVariables.GroupMinionTechSpeaker;
		}
		set
		{
			ExposedVariables.GroupMinionTechSpeaker = value;
		}
	}

	public SpawnTechData[] NPCTechData
	{
		get
		{
			return ExposedVariables.NPCTechData;
		}
		set
		{
			ExposedVariables.NPCTechData = value;
		}
	}

	public SpawnTechData[] CrazedLeaderTechData
	{
		get
		{
			return ExposedVariables.CrazedLeaderTechData;
		}
		set
		{
			ExposedVariables.CrazedLeaderTechData = value;
		}
	}

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

	public uScript_AddMessage.MessageData MsgOutOfTimeMultiplayer
	{
		get
		{
			return ExposedVariables.MsgOutOfTimeMultiplayer;
		}
		set
		{
			ExposedVariables.MsgOutOfTimeMultiplayer = value;
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

	public uScript_AddMessage.MessageData MsgOutOfTimeMultiplayerLeave
	{
		get
		{
			return ExposedVariables.MsgOutOfTimeMultiplayerLeave;
		}
		set
		{
			ExposedVariables.MsgOutOfTimeMultiplayerLeave = value;
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
