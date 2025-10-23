using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_RR_Circuits_TutorialCarrier_Mission")]
public class Mission_SetPiece_RR_Circuits_TutorialCarrier_Mission_Component : uScriptCode
{
	public Mission_SetPiece_RR_Circuits_TutorialCarrier_Mission ExposedVariables = new Mission_SetPiece_RR_Circuits_TutorialCarrier_Mission();

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

	public uScript_AddMessage.MessageData msgLeavingMissionArea
	{
		get
		{
			return ExposedVariables.msgLeavingMissionArea;
		}
		set
		{
			ExposedVariables.msgLeavingMissionArea = value;
		}
	}

	public float distBaseFound
	{
		get
		{
			return ExposedVariables.distBaseFound;
		}
		set
		{
			ExposedVariables.distBaseFound = value;
		}
	}

	public uScript_AddMessage.MessageData msg04AtomStuckInsideBase
	{
		get
		{
			return ExposedVariables.msg04AtomStuckInsideBase;
		}
		set
		{
			ExposedVariables.msg04AtomStuckInsideBase = value;
		}
	}

	public BlockTypes blockTypeToggle
	{
		get
		{
			return ExposedVariables.blockTypeToggle;
		}
		set
		{
			ExposedVariables.blockTypeToggle = value;
		}
	}

	public TankPreset completedTransmitterBasePreset
	{
		get
		{
			return ExposedVariables.completedTransmitterBasePreset;
		}
		set
		{
			ExposedVariables.completedTransmitterBasePreset = value;
		}
	}

	public GhostBlockSpawnData[] GhostBlockTransmitter
	{
		get
		{
			return ExposedVariables.GhostBlockTransmitter;
		}
		set
		{
			ExposedVariables.GhostBlockTransmitter = value;
		}
	}

	public float clearSceneryRadius
	{
		get
		{
			return ExposedVariables.clearSceneryRadius;
		}
		set
		{
			ExposedVariables.clearSceneryRadius = value;
		}
	}

	public SpawnTechData[] lockedbaseSpawnData
	{
		get
		{
			return ExposedVariables.lockedbaseSpawnData;
		}
		set
		{
			ExposedVariables.lockedbaseSpawnData = value;
		}
	}

	public SpawnTechData[] ReceiverbaseSpawnData
	{
		get
		{
			return ExposedVariables.ReceiverbaseSpawnData;
		}
		set
		{
			ExposedVariables.ReceiverbaseSpawnData = value;
		}
	}

	public uScript_AddMessage.MessageData msgBlockOutsideArea
	{
		get
		{
			return ExposedVariables.msgBlockOutsideArea;
		}
		set
		{
			ExposedVariables.msgBlockOutsideArea = value;
		}
	}

	public string lockedBasePosition
	{
		get
		{
			return ExposedVariables.lockedBasePosition;
		}
		set
		{
			ExposedVariables.lockedBasePosition = value;
		}
	}

	public SpawnBlockData[] TransmitterBlockSpawnData
	{
		get
		{
			return ExposedVariables.TransmitterBlockSpawnData;
		}
		set
		{
			ExposedVariables.TransmitterBlockSpawnData = value;
		}
	}

	public SpawnTechData[] TransmitterbaseSpawnData
	{
		get
		{
			return ExposedVariables.TransmitterbaseSpawnData;
		}
		set
		{
			ExposedVariables.TransmitterbaseSpawnData = value;
		}
	}

	public BlockTypes blockTypeTransmitter
	{
		get
		{
			return ExposedVariables.blockTypeTransmitter;
		}
		set
		{
			ExposedVariables.blockTypeTransmitter = value;
		}
	}

	public uScript_AddMessage.MessageData msg07AttachTransmitter
	{
		get
		{
			return ExposedVariables.msg07AttachTransmitter;
		}
		set
		{
			ExposedVariables.msg07AttachTransmitter = value;
		}
	}

	public uScript_AddMessage.MessageData msg06AtomFreeFromBaseShown
	{
		get
		{
			return ExposedVariables.msg06AtomFreeFromBaseShown;
		}
		set
		{
			ExposedVariables.msg06AtomFreeFromBaseShown = value;
		}
	}

	public BlockTypes blockTypeReceiver
	{
		get
		{
			return ExposedVariables.blockTypeReceiver;
		}
		set
		{
			ExposedVariables.blockTypeReceiver = value;
		}
	}

	public uScript_AddMessage.MessageData msg08TransmitterAttached
	{
		get
		{
			return ExposedVariables.msg08TransmitterAttached;
		}
		set
		{
			ExposedVariables.msg08TransmitterAttached = value;
		}
	}

	public uScript_AddMessage.MessageData msg09TransmitterExplained
	{
		get
		{
			return ExposedVariables.msg09TransmitterExplained;
		}
		set
		{
			ExposedVariables.msg09TransmitterExplained = value;
		}
	}

	public uScript_AddMessage.MessageData msg10ReceiverExplained
	{
		get
		{
			return ExposedVariables.msg10ReceiverExplained;
		}
		set
		{
			ExposedVariables.msg10ReceiverExplained = value;
		}
	}

	public uScript_AddMessage.MessageData msg15ReceiverChannelHasBeenSet
	{
		get
		{
			return ExposedVariables.msg15ReceiverChannelHasBeenSet;
		}
		set
		{
			ExposedVariables.msg15ReceiverChannelHasBeenSet = value;
		}
	}

	public BlockTypes blockTypeFirewiorksLauncher
	{
		get
		{
			return ExposedVariables.blockTypeFirewiorksLauncher;
		}
		set
		{
			ExposedVariables.blockTypeFirewiorksLauncher = value;
		}
	}

	public BlockTypes blockTypeButton
	{
		get
		{
			return ExposedVariables.blockTypeButton;
		}
		set
		{
			ExposedVariables.blockTypeButton = value;
		}
	}

	public uScript_AddMessage.MessageData msg17Outro
	{
		get
		{
			return ExposedVariables.msg17Outro;
		}
		set
		{
			ExposedVariables.msg17Outro = value;
		}
	}

	public uScript_AddMessage.MessageData msg11Configure
	{
		get
		{
			return ExposedVariables.msg11Configure;
		}
		set
		{
			ExposedVariables.msg11Configure = value;
		}
	}

	public uScript_AddMessage.MessageData msg13TransmitterChannelHasBeenSet
	{
		get
		{
			return ExposedVariables.msg13TransmitterChannelHasBeenSet;
		}
		set
		{
			ExposedVariables.msg13TransmitterChannelHasBeenSet = value;
		}
	}

	public uScript_AddMessage.MessageData msg14SetReceiverChannel
	{
		get
		{
			return ExposedVariables.msg14SetReceiverChannel;
		}
		set
		{
			ExposedVariables.msg14SetReceiverChannel = value;
		}
	}

	public uScript_AddMessage.MessageData msg03CircuitsLocked
	{
		get
		{
			return ExposedVariables.msg03CircuitsLocked;
		}
		set
		{
			ExposedVariables.msg03CircuitsLocked = value;
		}
	}

	public uScript_AddMessage.MessageData msg02BaseFound
	{
		get
		{
			return ExposedVariables.msg02BaseFound;
		}
		set
		{
			ExposedVariables.msg02BaseFound = value;
		}
	}

	public SpawnTechData[] NPC01SpawnData
	{
		get
		{
			return ExposedVariables.NPC01SpawnData;
		}
		set
		{
			ExposedVariables.NPC01SpawnData = value;
		}
	}

	public Transform NPCDespawnParticleEffect
	{
		get
		{
			return ExposedVariables.NPCDespawnParticleEffect;
		}
		set
		{
			ExposedVariables.NPCDespawnParticleEffect = value;
		}
	}

	public uScript_AddMessage.MessageData msg01Intro
	{
		get
		{
			return ExposedVariables.msg01Intro;
		}
		set
		{
			ExposedVariables.msg01Intro = value;
		}
	}

	public uScript_AddMessage.MessageData msg05SetLockedBaseToggleOff
	{
		get
		{
			return ExposedVariables.msg05SetLockedBaseToggleOff;
		}
		set
		{
			ExposedVariables.msg05SetLockedBaseToggleOff = value;
		}
	}

	public uScript_AddMessage.MessageData msg05SetLockedBaseToggleOff_Pad
	{
		get
		{
			return ExposedVariables.msg05SetLockedBaseToggleOff_Pad;
		}
		set
		{
			ExposedVariables.msg05SetLockedBaseToggleOff_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg12SetTransmitterChannel
	{
		get
		{
			return ExposedVariables.msg12SetTransmitterChannel;
		}
		set
		{
			ExposedVariables.msg12SetTransmitterChannel = value;
		}
	}

	public uScript_AddMessage.MessageData msg12SetTransmitterChannel_Pad
	{
		get
		{
			return ExposedVariables.msg12SetTransmitterChannel_Pad;
		}
		set
		{
			ExposedVariables.msg12SetTransmitterChannel_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg14SetReceiverChannel_Pad
	{
		get
		{
			return ExposedVariables.msg14SetReceiverChannel_Pad;
		}
		set
		{
			ExposedVariables.msg14SetReceiverChannel_Pad = value;
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

	public uScript_AddMessage.MessageData msg16SetTransmitterBaseButtonOn
	{
		get
		{
			return ExposedVariables.msg16SetTransmitterBaseButtonOn;
		}
		set
		{
			ExposedVariables.msg16SetTransmitterBaseButtonOn = value;
		}
	}

	public uScript_AddMessage.MessageData msg16SetTransmitterBaseButtonOn_Pad
	{
		get
		{
			return ExposedVariables.msg16SetTransmitterBaseButtonOn_Pad;
		}
		set
		{
			ExposedVariables.msg16SetTransmitterBaseButtonOn_Pad = value;
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
