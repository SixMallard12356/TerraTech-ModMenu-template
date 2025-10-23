using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_MineIslandRescue_01")]
public class Mission_SetPiece_MineIslandRescue_01_Component : uScriptCode
{
	public Mission_SetPiece_MineIslandRescue_01 ExposedVariables = new Mission_SetPiece_MineIslandRescue_01();

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

	public uScript_AddMessage.MessageData msgMeetNPC
	{
		get
		{
			return ExposedVariables.msgMeetNPC;
		}
		set
		{
			ExposedVariables.msgMeetNPC = value;
		}
	}

	public uScript_AddMessage.MessageData msgAttachBlock
	{
		get
		{
			return ExposedVariables.msgAttachBlock;
		}
		set
		{
			ExposedVariables.msgAttachBlock = value;
		}
	}

	public BlockTypes blockTypeToAttach
	{
		get
		{
			return ExposedVariables.blockTypeToAttach;
		}
		set
		{
			ExposedVariables.blockTypeToAttach = value;
		}
	}

	public GhostBlockSpawnData[] ghostBlock
	{
		get
		{
			return ExposedVariables.ghostBlock;
		}
		set
		{
			ExposedVariables.ghostBlock = value;
		}
	}

	public TankPreset CompletedNPCPreset
	{
		get
		{
			return ExposedVariables.CompletedNPCPreset;
		}
		set
		{
			ExposedVariables.CompletedNPCPreset = value;
		}
	}

	public string MinefieldTrigger
	{
		get
		{
			return ExposedVariables.MinefieldTrigger;
		}
		set
		{
			ExposedVariables.MinefieldTrigger = value;
		}
	}

	public uScript_AddMessage.MessageData msgMeetBoss
	{
		get
		{
			return ExposedVariables.msgMeetBoss;
		}
		set
		{
			ExposedVariables.msgMeetBoss = value;
		}
	}

	public uScript_AddMessage.MessageData msgNPCCallsForHelp
	{
		get
		{
			return ExposedVariables.msgNPCCallsForHelp;
		}
		set
		{
			ExposedVariables.msgNPCCallsForHelp = value;
		}
	}

	public uScript_AddMessage.MessageData msgBossResponds
	{
		get
		{
			return ExposedVariables.msgBossResponds;
		}
		set
		{
			ExposedVariables.msgBossResponds = value;
		}
	}

	public uScript_AddMessage.MessageData msgBossDead
	{
		get
		{
			return ExposedVariables.msgBossDead;
		}
		set
		{
			ExposedVariables.msgBossDead = value;
		}
	}

	public uScript_AddMessage.MessageData msgNothingToSeeHere
	{
		get
		{
			return ExposedVariables.msgNothingToSeeHere;
		}
		set
		{
			ExposedVariables.msgNothingToSeeHere = value;
		}
	}

	public uScript_AddMessage.MessageData msgReachedMineIsland
	{
		get
		{
			return ExposedVariables.msgReachedMineIsland;
		}
		set
		{
			ExposedVariables.msgReachedMineIsland = value;
		}
	}

	public uScript_AddMessage.MessageData msgBackAgain
	{
		get
		{
			return ExposedVariables.msgBackAgain;
		}
		set
		{
			ExposedVariables.msgBackAgain = value;
		}
	}

	public uScript_AddMessage.MessageData msgArrivedAtMineIsland
	{
		get
		{
			return ExposedVariables.msgArrivedAtMineIsland;
		}
		set
		{
			ExposedVariables.msgArrivedAtMineIsland = value;
		}
	}

	public uScript_AddMessage.MessageData msgHowDareYou
	{
		get
		{
			return ExposedVariables.msgHowDareYou;
		}
		set
		{
			ExposedVariables.msgHowDareYou = value;
		}
	}

	public string NearLongJumpTrigger
	{
		get
		{
			return ExposedVariables.NearLongJumpTrigger;
		}
		set
		{
			ExposedVariables.NearLongJumpTrigger = value;
		}
	}

	public uScript_AddMessage.MessageData msgNearLongJump
	{
		get
		{
			return ExposedVariables.msgNearLongJump;
		}
		set
		{
			ExposedVariables.msgNearLongJump = value;
		}
	}

	public uScript_AddMessage.MessageData msgNearBrokenBridge
	{
		get
		{
			return ExposedVariables.msgNearBrokenBridge;
		}
		set
		{
			ExposedVariables.msgNearBrokenBridge = value;
		}
	}

	public string NearBrokenBridgeTrigger
	{
		get
		{
			return ExposedVariables.NearBrokenBridgeTrigger;
		}
		set
		{
			ExposedVariables.NearBrokenBridgeTrigger = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker messageBossSpeaker
	{
		get
		{
			return ExposedVariables.messageBossSpeaker;
		}
		set
		{
			ExposedVariables.messageBossSpeaker = value;
		}
	}

	public uScript_AddMessage.MessageData msgShutUpWimpy
	{
		get
		{
			return ExposedVariables.msgShutUpWimpy;
		}
		set
		{
			ExposedVariables.msgShutUpWimpy = value;
		}
	}

	public uScript_AddMessage.MessageData msgEnemyHitAMine
	{
		get
		{
			return ExposedVariables.msgEnemyHitAMine;
		}
		set
		{
			ExposedVariables.msgEnemyHitAMine = value;
		}
	}

	public uScript_AddMessage.MessageData msgPlayerHitAMine
	{
		get
		{
			return ExposedVariables.msgPlayerHitAMine;
		}
		set
		{
			ExposedVariables.msgPlayerHitAMine = value;
		}
	}

	public SpawnBlockData[] KeyBlock
	{
		get
		{
			return ExposedVariables.KeyBlock;
		}
		set
		{
			ExposedVariables.KeyBlock = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker messageNPCSpeaker
	{
		get
		{
			return ExposedVariables.messageNPCSpeaker;
		}
		set
		{
			ExposedVariables.messageNPCSpeaker = value;
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

	public ExternalBehaviorTree NPCFlyAwayBehaviour
	{
		get
		{
			return ExposedVariables.NPCFlyAwayBehaviour;
		}
		set
		{
			ExposedVariables.NPCFlyAwayBehaviour = value;
		}
	}

	public SpawnTechData[] RepulsorSpawnData
	{
		get
		{
			return ExposedVariables.RepulsorSpawnData;
		}
		set
		{
			ExposedVariables.RepulsorSpawnData = value;
		}
	}

	public SpawnTechData[] BossSpawnData
	{
		get
		{
			return ExposedVariables.BossSpawnData;
		}
		set
		{
			ExposedVariables.BossSpawnData = value;
		}
	}

	public string MineIslandTrigger
	{
		get
		{
			return ExposedVariables.MineIslandTrigger;
		}
		set
		{
			ExposedVariables.MineIslandTrigger = value;
		}
	}

	public string NearRepulsorTrigger
	{
		get
		{
			return ExposedVariables.NearRepulsorTrigger;
		}
		set
		{
			ExposedVariables.NearRepulsorTrigger = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker messageRepulsorSpeaker
	{
		get
		{
			return ExposedVariables.messageRepulsorSpeaker;
		}
		set
		{
			ExposedVariables.messageRepulsorSpeaker = value;
		}
	}

	public uScript_AddMessage.MessageData msgIAmRepulsor
	{
		get
		{
			return ExposedVariables.msgIAmRepulsor;
		}
		set
		{
			ExposedVariables.msgIAmRepulsor = value;
		}
	}

	public uScript_AddMessage.MessageData msgRepulsorBaseline
	{
		get
		{
			return ExposedVariables.msgRepulsorBaseline;
		}
		set
		{
			ExposedVariables.msgRepulsorBaseline = value;
		}
	}

	public string TagRepulsor
	{
		get
		{
			return ExposedVariables.TagRepulsor;
		}
		set
		{
			ExposedVariables.TagRepulsor = value;
		}
	}

	public SpawnTechData[] NPCSpawnData
	{
		get
		{
			return ExposedVariables.NPCSpawnData;
		}
		set
		{
			ExposedVariables.NPCSpawnData = value;
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
