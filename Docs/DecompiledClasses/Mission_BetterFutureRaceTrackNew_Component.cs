using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_BetterFutureRaceTrackNew")]
public class Mission_BetterFutureRaceTrackNew_Component : uScriptCode
{
	public Mission_BetterFutureRaceTrackNew ExposedVariables = new Mission_BetterFutureRaceTrackNew();

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

	public uScript_AddMessage.MessageData msgReadyToBegin
	{
		get
		{
			return ExposedVariables.msgReadyToBegin;
		}
		set
		{
			ExposedVariables.msgReadyToBegin = value;
		}
	}

	public uScript_AddMessage.MessageData msgReadyToBegin_Pad
	{
		get
		{
			return ExposedVariables.msgReadyToBegin_Pad;
		}
		set
		{
			ExposedVariables.msgReadyToBegin_Pad = value;
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

	public string messageTagIntro
	{
		get
		{
			return ExposedVariables.messageTagIntro;
		}
		set
		{
			ExposedVariables.messageTagIntro = value;
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

	public uScript_AddMessage.MessageData msgRaceStarted
	{
		get
		{
			return ExposedVariables.msgRaceStarted;
		}
		set
		{
			ExposedVariables.msgRaceStarted = value;
		}
	}

	public uScript_AddMessage.MessageData msgIntro
	{
		get
		{
			return ExposedVariables.msgIntro;
		}
		set
		{
			ExposedVariables.msgIntro = value;
		}
	}

	public uScript_AddMessage.MessageData msgOutOfTime
	{
		get
		{
			return ExposedVariables.msgOutOfTime;
		}
		set
		{
			ExposedVariables.msgOutOfTime = value;
		}
	}

	public SpawnTechData[] HoverNPCSpawnData
	{
		get
		{
			return ExposedVariables.HoverNPCSpawnData;
		}
		set
		{
			ExposedVariables.HoverNPCSpawnData = value;
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
