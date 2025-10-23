using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_1_4_SolarGenerator")]
public class Mission_GSO_1_4_SolarGenerator_Component : uScriptCode
{
	public Mission_GSO_1_4_SolarGenerator ExposedVariables = new Mission_GSO_1_4_SolarGenerator();

	public Transform particles
	{
		get
		{
			return ExposedVariables.particles;
		}
		set
		{
			ExposedVariables.particles = value;
		}
	}

	public GhostBlockSpawnData[] ghostBlockRegen
	{
		get
		{
			return ExposedVariables.ghostBlockRegen;
		}
		set
		{
			ExposedVariables.ghostBlockRegen = value;
		}
	}

	public SpawnBlockData[] blockDataSolarGen
	{
		get
		{
			return ExposedVariables.blockDataSolarGen;
		}
		set
		{
			ExposedVariables.blockDataSolarGen = value;
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

	public string PositionId
	{
		get
		{
			return ExposedVariables.PositionId;
		}
		set
		{
			ExposedVariables.PositionId = value;
		}
	}

	public uScript_AddMessage.MessageData msg01FindSolarGenerator
	{
		get
		{
			return ExposedVariables.msg01FindSolarGenerator;
		}
		set
		{
			ExposedVariables.msg01FindSolarGenerator = value;
		}
	}

	public uScript_AddMessage.MessageData msg02SolarGeneratorFound
	{
		get
		{
			return ExposedVariables.msg02SolarGeneratorFound;
		}
		set
		{
			ExposedVariables.msg02SolarGeneratorFound = value;
		}
	}

	public uScript_AddMessage.MessageData msg05RepairBubbleSpawned
	{
		get
		{
			return ExposedVariables.msg05RepairBubbleSpawned;
		}
		set
		{
			ExposedVariables.msg05RepairBubbleSpawned = value;
		}
	}

	public uScript_AddMessage.MessageData msg06PickUpRepairBubble
	{
		get
		{
			return ExposedVariables.msg06PickUpRepairBubble;
		}
		set
		{
			ExposedVariables.msg06PickUpRepairBubble = value;
		}
	}

	public uScript_AddMessage.MessageData msg08ReturnRepairBubble
	{
		get
		{
			return ExposedVariables.msg08ReturnRepairBubble;
		}
		set
		{
			ExposedVariables.msg08ReturnRepairBubble = value;
		}
	}

	public uScript_AddMessage.MessageData msg09RepairBubbleAttached
	{
		get
		{
			return ExposedVariables.msg09RepairBubbleAttached;
		}
		set
		{
			ExposedVariables.msg09RepairBubbleAttached = value;
		}
	}

	public uScript_AddMessage.MessageData msg04SolarGeneratorAnchored
	{
		get
		{
			return ExposedVariables.msg04SolarGeneratorAnchored;
		}
		set
		{
			ExposedVariables.msg04SolarGeneratorAnchored = value;
		}
	}

	public uScript_AddMessage.MessageData msg10MoveInsideRepairBubble
	{
		get
		{
			return ExposedVariables.msg10MoveInsideRepairBubble;
		}
		set
		{
			ExposedVariables.msg10MoveInsideRepairBubble = value;
		}
	}

	public uScript_AddMessage.MessageData msg14FindOtherParts
	{
		get
		{
			return ExposedVariables.msg14FindOtherParts;
		}
		set
		{
			ExposedVariables.msg14FindOtherParts = value;
		}
	}

	public TankPreset completedSolGenRegenPreset
	{
		get
		{
			return ExposedVariables.completedSolGenRegenPreset;
		}
		set
		{
			ExposedVariables.completedSolGenRegenPreset = value;
		}
	}

	public uScript_AddMessage.MessageData msg07AttachRepairBubble
	{
		get
		{
			return ExposedVariables.msg07AttachRepairBubble;
		}
		set
		{
			ExposedVariables.msg07AttachRepairBubble = value;
		}
	}

	public uScript_AddMessage.MessageData msg07AttachRepairBubble_Pad
	{
		get
		{
			return ExposedVariables.msg07AttachRepairBubble_Pad;
		}
		set
		{
			ExposedVariables.msg07AttachRepairBubble_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg03AnchorSolarGenerator_Pad1
	{
		get
		{
			return ExposedVariables.msg03AnchorSolarGenerator_Pad1;
		}
		set
		{
			ExposedVariables.msg03AnchorSolarGenerator_Pad1 = value;
		}
	}

	public uScript_AddMessage.MessageData msg03AnchorSolarGenerator_Pad2
	{
		get
		{
			return ExposedVariables.msg03AnchorSolarGenerator_Pad2;
		}
		set
		{
			ExposedVariables.msg03AnchorSolarGenerator_Pad2 = value;
		}
	}

	public uScript_AddMessage.MessageData msg03AnchorSolarGenerator
	{
		get
		{
			return ExposedVariables.msg03AnchorSolarGenerator;
		}
		set
		{
			ExposedVariables.msg03AnchorSolarGenerator = value;
		}
	}

	public uScript_AddMessage.MessageData msg13UnanchorSolarGenerator_Pad1
	{
		get
		{
			return ExposedVariables.msg13UnanchorSolarGenerator_Pad1;
		}
		set
		{
			ExposedVariables.msg13UnanchorSolarGenerator_Pad1 = value;
		}
	}

	public uScript_AddMessage.MessageData msg13UnanchorSolarGenerator_Pad2
	{
		get
		{
			return ExposedVariables.msg13UnanchorSolarGenerator_Pad2;
		}
		set
		{
			ExposedVariables.msg13UnanchorSolarGenerator_Pad2 = value;
		}
	}

	public uScript_AddMessage.MessageData msg11PlayerHealed
	{
		get
		{
			return ExposedVariables.msg11PlayerHealed;
		}
		set
		{
			ExposedVariables.msg11PlayerHealed = value;
		}
	}

	public uScript_AddMessage.MessageData msg13UnanchorSolarGenerator
	{
		get
		{
			return ExposedVariables.msg13UnanchorSolarGenerator;
		}
		set
		{
			ExposedVariables.msg13UnanchorSolarGenerator = value;
		}
	}

	public uScript_AddMessage.MessageData msg12DismantleBase
	{
		get
		{
			return ExposedVariables.msg12DismantleBase;
		}
		set
		{
			ExposedVariables.msg12DismantleBase = value;
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

	public uScript_AddMessage.MessageData msg13UnanchorSolarGenerator_Pad3
	{
		get
		{
			return ExposedVariables.msg13UnanchorSolarGenerator_Pad3;
		}
		set
		{
			ExposedVariables.msg13UnanchorSolarGenerator_Pad3 = value;
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
