using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_RR_Circuits_TutorialInput_Mission")]
public class Mission_SetPiece_RR_Circuits_TutorialInput_Mission_Component : uScriptCode
{
	public Mission_SetPiece_RR_Circuits_TutorialInput_Mission ExposedVariables = new Mission_SetPiece_RR_Circuits_TutorialInput_Mission();

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

	public GhostBlockSpawnData[] GhostBlockBeamSensor
	{
		get
		{
			return ExposedVariables.GhostBlockBeamSensor;
		}
		set
		{
			ExposedVariables.GhostBlockBeamSensor = value;
		}
	}

	public GhostBlockSpawnData[] GhostBlockLight
	{
		get
		{
			return ExposedVariables.GhostBlockLight;
		}
		set
		{
			ExposedVariables.GhostBlockLight = value;
		}
	}

	public BlockTypes blockTypeBeamSensor
	{
		get
		{
			return ExposedVariables.blockTypeBeamSensor;
		}
		set
		{
			ExposedVariables.blockTypeBeamSensor = value;
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

	public uScript_AddMessage.MessageData msg06PickupLight
	{
		get
		{
			return ExposedVariables.msg06PickupLight;
		}
		set
		{
			ExposedVariables.msg06PickupLight = value;
		}
	}

	public uScript_AddMessage.MessageData msg05AttachedWires
	{
		get
		{
			return ExposedVariables.msg05AttachedWires;
		}
		set
		{
			ExposedVariables.msg05AttachedWires = value;
		}
	}

	public uScript_AddMessage.MessageData msg07AttachedLight
	{
		get
		{
			return ExposedVariables.msg07AttachedLight;
		}
		set
		{
			ExposedVariables.msg07AttachedLight = value;
		}
	}

	public uScript_AddMessage.MessageData msg09ToggleIsOn
	{
		get
		{
			return ExposedVariables.msg09ToggleIsOn;
		}
		set
		{
			ExposedVariables.msg09ToggleIsOn = value;
		}
	}

	public uScript_AddMessage.MessageData msg12BeamSensorSpawned
	{
		get
		{
			return ExposedVariables.msg12BeamSensorSpawned;
		}
		set
		{
			ExposedVariables.msg12BeamSensorSpawned = value;
		}
	}

	public GhostBlockSpawnData[] ghostBlockWire03
	{
		get
		{
			return ExposedVariables.ghostBlockWire03;
		}
		set
		{
			ExposedVariables.ghostBlockWire03 = value;
		}
	}

	public BlockTypes blockTypeWire
	{
		get
		{
			return ExposedVariables.blockTypeWire;
		}
		set
		{
			ExposedVariables.blockTypeWire = value;
		}
	}

	public GhostBlockSpawnData[] ghostBlockWire01
	{
		get
		{
			return ExposedVariables.ghostBlockWire01;
		}
		set
		{
			ExposedVariables.ghostBlockWire01 = value;
		}
	}

	public GhostBlockSpawnData[] ghostBlockWire02
	{
		get
		{
			return ExposedVariables.ghostBlockWire02;
		}
		set
		{
			ExposedVariables.ghostBlockWire02 = value;
		}
	}

	public uScript_AddMessage.MessageData msg04PickupWire
	{
		get
		{
			return ExposedVariables.msg04PickupWire;
		}
		set
		{
			ExposedVariables.msg04PickupWire = value;
		}
	}

	public uScript_AddMessage.MessageData msg11ToggleIsOff
	{
		get
		{
			return ExposedVariables.msg11ToggleIsOff;
		}
		set
		{
			ExposedVariables.msg11ToggleIsOff = value;
		}
	}

	public string MissionRangeTrig
	{
		get
		{
			return ExposedVariables.MissionRangeTrig;
		}
		set
		{
			ExposedVariables.MissionRangeTrig = value;
		}
	}

	public TankPreset completedBasePreset
	{
		get
		{
			return ExposedVariables.completedBasePreset;
		}
		set
		{
			ExposedVariables.completedBasePreset = value;
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

	public string CircuitsBasePosition
	{
		get
		{
			return ExposedVariables.CircuitsBasePosition;
		}
		set
		{
			ExposedVariables.CircuitsBasePosition = value;
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

	public SpawnBlockData[] BeamSensorBlockSpawnData
	{
		get
		{
			return ExposedVariables.BeamSensorBlockSpawnData;
		}
		set
		{
			ExposedVariables.BeamSensorBlockSpawnData = value;
		}
	}

	public SpawnBlockData[] BlockSpawnData
	{
		get
		{
			return ExposedVariables.BlockSpawnData;
		}
		set
		{
			ExposedVariables.BlockSpawnData = value;
		}
	}

	public SpawnTechData[] CircuirtsbaseSpawnData
	{
		get
		{
			return ExposedVariables.CircuirtsbaseSpawnData;
		}
		set
		{
			ExposedVariables.CircuirtsbaseSpawnData = value;
		}
	}

	public int MissionHour
	{
		get
		{
			return ExposedVariables.MissionHour;
		}
		set
		{
			ExposedVariables.MissionHour = value;
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

	public uScript_AddMessage.MessageData msg03ToggleIntro
	{
		get
		{
			return ExposedVariables.msg03ToggleIntro;
		}
		set
		{
			ExposedVariables.msg03ToggleIntro = value;
		}
	}

	public BlockTypes blockTypeLight
	{
		get
		{
			return ExposedVariables.blockTypeLight;
		}
		set
		{
			ExposedVariables.blockTypeLight = value;
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

	public uScript_AddMessage.MessageData msg15SetBeamSensorActive
	{
		get
		{
			return ExposedVariables.msg15SetBeamSensorActive;
		}
		set
		{
			ExposedVariables.msg15SetBeamSensorActive = value;
		}
	}

	public uScript_AddMessage.MessageData msg14AttachedBeamSensor
	{
		get
		{
			return ExposedVariables.msg14AttachedBeamSensor;
		}
		set
		{
			ExposedVariables.msg14AttachedBeamSensor = value;
		}
	}

	public ExternalBehaviorTree NPCFlyAwayAI
	{
		get
		{
			return ExposedVariables.NPCFlyAwayAI;
		}
		set
		{
			ExposedVariables.NPCFlyAwayAI = value;
		}
	}

	public uScript_AddMessage.MessageData msg17BeamSensorOn
	{
		get
		{
			return ExposedVariables.msg17BeamSensorOn;
		}
		set
		{
			ExposedVariables.msg17BeamSensorOn = value;
		}
	}

	public uScript_AddMessage.MessageData msg18Complete
	{
		get
		{
			return ExposedVariables.msg18Complete;
		}
		set
		{
			ExposedVariables.msg18Complete = value;
		}
	}

	public uScript_AddMessage.MessageData msg08SetToggleActive_Pad
	{
		get
		{
			return ExposedVariables.msg08SetToggleActive_Pad;
		}
		set
		{
			ExposedVariables.msg08SetToggleActive_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg08SetToggleActive
	{
		get
		{
			return ExposedVariables.msg08SetToggleActive;
		}
		set
		{
			ExposedVariables.msg08SetToggleActive = value;
		}
	}

	public uScript_AddMessage.MessageData msg10SetToggleInactive
	{
		get
		{
			return ExposedVariables.msg10SetToggleInactive;
		}
		set
		{
			ExposedVariables.msg10SetToggleInactive = value;
		}
	}

	public uScript_AddMessage.MessageData msg10SetToggleInactive_Pad
	{
		get
		{
			return ExposedVariables.msg10SetToggleInactive_Pad;
		}
		set
		{
			ExposedVariables.msg10SetToggleInactive_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg13PickupBeamSensor
	{
		get
		{
			return ExposedVariables.msg13PickupBeamSensor;
		}
		set
		{
			ExposedVariables.msg13PickupBeamSensor = value;
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

	public uScript_AddMessage.MessageData msg13PickupBeamSensor_Pad
	{
		get
		{
			return ExposedVariables.msg13PickupBeamSensor_Pad;
		}
		set
		{
			ExposedVariables.msg13PickupBeamSensor_Pad = value;
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
