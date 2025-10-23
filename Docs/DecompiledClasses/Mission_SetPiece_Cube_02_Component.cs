using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_Cube_02")]
public class Mission_SetPiece_Cube_02_Component : uScriptCode
{
	public Mission_SetPiece_Cube_02 ExposedVariables = new Mission_SetPiece_Cube_02();

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

	public uScript_AddMessage.MessageData MsgCubeIntro01
	{
		get
		{
			return ExposedVariables.MsgCubeIntro01;
		}
		set
		{
			ExposedVariables.MsgCubeIntro01 = value;
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

	public uScript_AddMessage.MessageData MsgCrazedNPCFly
	{
		get
		{
			return ExposedVariables.MsgCrazedNPCFly;
		}
		set
		{
			ExposedVariables.MsgCrazedNPCFly = value;
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

	public string MissionCompleteArea
	{
		get
		{
			return ExposedVariables.MissionCompleteArea;
		}
		set
		{
			ExposedVariables.MissionCompleteArea = value;
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

	public uScript_AddMessage.MessageData MsgFillerNPCFly
	{
		get
		{
			return ExposedVariables.MsgFillerNPCFly;
		}
		set
		{
			ExposedVariables.MsgFillerNPCFly = value;
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

	public uScript_AddMessage.MessageData MsgCrazedMinionInterrupt04
	{
		get
		{
			return ExposedVariables.MsgCrazedMinionInterrupt04;
		}
		set
		{
			ExposedVariables.MsgCrazedMinionInterrupt04 = value;
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

	public SpawnTechData[] EnemyMinionWaveData
	{
		get
		{
			return ExposedVariables.EnemyMinionWaveData;
		}
		set
		{
			ExposedVariables.EnemyMinionWaveData = value;
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
