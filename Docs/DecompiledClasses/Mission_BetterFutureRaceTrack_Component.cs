using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_BetterFutureRaceTrack")]
public class Mission_BetterFutureRaceTrack_Component : uScriptCode
{
	public Mission_BetterFutureRaceTrack ExposedVariables = new Mission_BetterFutureRaceTrack();

	public TerrainObject terrainObjectPrefab
	{
		get
		{
			return ExposedVariables.terrainObjectPrefab;
		}
		set
		{
			ExposedVariables.terrainObjectPrefab = value;
		}
	}

	public string terrainObjectName
	{
		get
		{
			return ExposedVariables.terrainObjectName;
		}
		set
		{
			ExposedVariables.terrainObjectName = value;
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

	public string clearSceneryPos
	{
		get
		{
			return ExposedVariables.clearSceneryPos;
		}
		set
		{
			ExposedVariables.clearSceneryPos = value;
		}
	}

	public uScript_AddMessage.MessageData msgQuitFromMenu
	{
		get
		{
			return ExposedVariables.msgQuitFromMenu;
		}
		set
		{
			ExposedVariables.msgQuitFromMenu = value;
		}
	}

	public uScript_AddMessage.MessageData msgTouchedGround
	{
		get
		{
			return ExposedVariables.msgTouchedGround;
		}
		set
		{
			ExposedVariables.msgTouchedGround = value;
		}
	}

	public uScript_AddMessage.MessageData msgOutOfBounds
	{
		get
		{
			return ExposedVariables.msgOutOfBounds;
		}
		set
		{
			ExposedVariables.msgOutOfBounds = value;
		}
	}

	public uScript_AddMessage.MessageData msgRaceComplete
	{
		get
		{
			return ExposedVariables.msgRaceComplete;
		}
		set
		{
			ExposedVariables.msgRaceComplete = value;
		}
	}

	public BlockTypes[] discoverableBlockTypesOnVehicle
	{
		get
		{
			return ExposedVariables.discoverableBlockTypesOnVehicle;
		}
		set
		{
			ExposedVariables.discoverableBlockTypesOnVehicle = value;
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

	public float distNearNPC
	{
		get
		{
			return ExposedVariables.distNearNPC;
		}
		set
		{
			ExposedVariables.distNearNPC = value;
		}
	}

	public uScript_AddMessage.MessageData msgVehicleControls
	{
		get
		{
			return ExposedVariables.msgVehicleControls;
		}
		set
		{
			ExposedVariables.msgVehicleControls = value;
		}
	}

	public uScript_AddMessage.MessageData msgVehicleControls_Pad
	{
		get
		{
			return ExposedVariables.msgVehicleControls_Pad;
		}
		set
		{
			ExposedVariables.msgVehicleControls_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msgPurchaseVehicle
	{
		get
		{
			return ExposedVariables.msgPurchaseVehicle;
		}
		set
		{
			ExposedVariables.msgPurchaseVehicle = value;
		}
	}

	public uScript_AddMessage.MessageData msgPurchaseVehicle_Pad
	{
		get
		{
			return ExposedVariables.msgPurchaseVehicle_Pad;
		}
		set
		{
			ExposedVariables.msgPurchaseVehicle_Pad = value;
		}
	}

	public string messageTagControls
	{
		get
		{
			return ExposedVariables.messageTagControls;
		}
		set
		{
			ExposedVariables.messageTagControls = value;
		}
	}

	public string messageTagPurchase
	{
		get
		{
			return ExposedVariables.messageTagPurchase;
		}
		set
		{
			ExposedVariables.messageTagPurchase = value;
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

	public bool _DEBUGIgnoreMoneyCheck
	{
		get
		{
			return ExposedVariables._DEBUGIgnoreMoneyCheck;
		}
		set
		{
			ExposedVariables._DEBUGIgnoreMoneyCheck = value;
		}
	}

	public LocalisedString msgPromptDecline
	{
		get
		{
			return ExposedVariables.msgPromptDecline;
		}
		set
		{
			ExposedVariables.msgPromptDecline = value;
		}
	}

	public LocalisedString msgPromptAccept
	{
		get
		{
			return ExposedVariables.msgPromptAccept;
		}
		set
		{
			ExposedVariables.msgPromptAccept = value;
		}
	}

	public LocalisedString msgPromptNoMoney
	{
		get
		{
			return ExposedVariables.msgPromptNoMoney;
		}
		set
		{
			ExposedVariables.msgPromptNoMoney = value;
		}
	}

	public int vehicleCost
	{
		get
		{
			return ExposedVariables.vehicleCost;
		}
		set
		{
			ExposedVariables.vehicleCost = value;
		}
	}

	public string raceStartPosition
	{
		get
		{
			return ExposedVariables.raceStartPosition;
		}
		set
		{
			ExposedVariables.raceStartPosition = value;
		}
	}

	public SpawnTechData[] vehicleSpawnData
	{
		get
		{
			return ExposedVariables.vehicleSpawnData;
		}
		set
		{
			ExposedVariables.vehicleSpawnData = value;
		}
	}

	public LocalisedString msgWorldCapacityFull
	{
		get
		{
			return ExposedVariables.msgWorldCapacityFull;
		}
		set
		{
			ExposedVariables.msgWorldCapacityFull = value;
		}
	}

	public LocalisedString msgPromptText
	{
		get
		{
			return ExposedVariables.msgPromptText;
		}
		set
		{
			ExposedVariables.msgPromptText = value;
		}
	}

	public bool BlockLimitCritical
	{
		get
		{
			return ExposedVariables.BlockLimitCritical;
		}
		set
		{
			ExposedVariables.BlockLimitCritical = value;
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

	public uScript_AddMessage.MessageData msgWorldCapacityFullReply
	{
		get
		{
			return ExposedVariables.msgWorldCapacityFullReply;
		}
		set
		{
			ExposedVariables.msgWorldCapacityFullReply = value;
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
