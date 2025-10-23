using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_PlateauRun_01")]
public class Mission_SetPiece_PlateauRun_01_Component : uScriptCode
{
	public Mission_SetPiece_PlateauRun_01 ExposedVariables = new Mission_SetPiece_PlateauRun_01();

	public LocalisedString[] msgMissionComplete
	{
		get
		{
			return ExposedVariables.msgMissionComplete;
		}
		set
		{
			ExposedVariables.msgMissionComplete = value;
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

	public uScript_AddMessage.MessageData msgSwitchTech
	{
		get
		{
			return ExposedVariables.msgSwitchTech;
		}
		set
		{
			ExposedVariables.msgSwitchTech = value;
		}
	}

	public uScript_AddMessage.MessageData msgSwitchTech_Pad
	{
		get
		{
			return ExposedVariables.msgSwitchTech_Pad;
		}
		set
		{
			ExposedVariables.msgSwitchTech_Pad = value;
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

	public LocalisedString[] msgNPCIntro
	{
		get
		{
			return ExposedVariables.msgNPCIntro;
		}
		set
		{
			ExposedVariables.msgNPCIntro = value;
		}
	}

	public string msgTagPurchase
	{
		get
		{
			return ExposedVariables.msgTagPurchase;
		}
		set
		{
			ExposedVariables.msgTagPurchase = value;
		}
	}

	public LocalisedString[] msgNPCVehicleSwitched
	{
		get
		{
			return ExposedVariables.msgNPCVehicleSwitched;
		}
		set
		{
			ExposedVariables.msgNPCVehicleSwitched = value;
		}
	}

	public uScript_AddMessage.MessageData msgNPCNotEnoughMoney
	{
		get
		{
			return ExposedVariables.msgNPCNotEnoughMoney;
		}
		set
		{
			ExposedVariables.msgNPCNotEnoughMoney = value;
		}
	}

	public uScript_AddMessage.MessageData msgNPCPurchaseDeclined
	{
		get
		{
			return ExposedVariables.msgNPCPurchaseDeclined;
		}
		set
		{
			ExposedVariables.msgNPCPurchaseDeclined = value;
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

	public ExternalBehaviorTree NPCFlyAwayBehavior
	{
		get
		{
			return ExposedVariables.NPCFlyAwayBehavior;
		}
		set
		{
			ExposedVariables.NPCFlyAwayBehavior = value;
		}
	}

	public LocalisedString[] msgLeavingEarlyPrePurchase
	{
		get
		{
			return ExposedVariables.msgLeavingEarlyPrePurchase;
		}
		set
		{
			ExposedVariables.msgLeavingEarlyPrePurchase = value;
		}
	}

	public LocalisedString[] msgLeavingEarlyPostPurchase
	{
		get
		{
			return ExposedVariables.msgLeavingEarlyPostPurchase;
		}
		set
		{
			ExposedVariables.msgLeavingEarlyPostPurchase = value;
		}
	}

	public LocalisedString[] msgLeavingEarlyDuringIntro
	{
		get
		{
			return ExposedVariables.msgLeavingEarlyDuringIntro;
		}
		set
		{
			ExposedVariables.msgLeavingEarlyDuringIntro = value;
		}
	}

	public LocalisedString[] msgPlateauRunIntro
	{
		get
		{
			return ExposedVariables.msgPlateauRunIntro;
		}
		set
		{
			ExposedVariables.msgPlateauRunIntro = value;
		}
	}

	public string FallTrigger
	{
		get
		{
			return ExposedVariables.FallTrigger;
		}
		set
		{
			ExposedVariables.FallTrigger = value;
		}
	}

	public LocalisedString[] msgFellOutOfBounds
	{
		get
		{
			return ExposedVariables.msgFellOutOfBounds;
		}
		set
		{
			ExposedVariables.msgFellOutOfBounds = value;
		}
	}

	public LocalisedString[] msgThrownOutOfBounds
	{
		get
		{
			return ExposedVariables.msgThrownOutOfBounds;
		}
		set
		{
			ExposedVariables.msgThrownOutOfBounds = value;
		}
	}

	public string EndTrigger
	{
		get
		{
			return ExposedVariables.EndTrigger;
		}
		set
		{
			ExposedVariables.EndTrigger = value;
		}
	}

	public string TopTrigger
	{
		get
		{
			return ExposedVariables.TopTrigger;
		}
		set
		{
			ExposedVariables.TopTrigger = value;
		}
	}

	public LocalisedString[] msgFlewOutOfBounds
	{
		get
		{
			return ExposedVariables.msgFlewOutOfBounds;
		}
		set
		{
			ExposedVariables.msgFlewOutOfBounds = value;
		}
	}

	public LocalisedString[] msgStartTooEarly
	{
		get
		{
			return ExposedVariables.msgStartTooEarly;
		}
		set
		{
			ExposedVariables.msgStartTooEarly = value;
		}
	}

	public string InsideTrigger
	{
		get
		{
			return ExposedVariables.InsideTrigger;
		}
		set
		{
			ExposedVariables.InsideTrigger = value;
		}
	}

	public string NearNPCTrigger
	{
		get
		{
			return ExposedVariables.NearNPCTrigger;
		}
		set
		{
			ExposedVariables.NearNPCTrigger = value;
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

	public LocalisedString[] msgYouCanReBuy
	{
		get
		{
			return ExposedVariables.msgYouCanReBuy;
		}
		set
		{
			ExposedVariables.msgYouCanReBuy = value;
		}
	}

	public string Ramp1Trigger
	{
		get
		{
			return ExposedVariables.Ramp1Trigger;
		}
		set
		{
			ExposedVariables.Ramp1Trigger = value;
		}
	}

	public LocalisedString[] msgRamp1
	{
		get
		{
			return ExposedVariables.msgRamp1;
		}
		set
		{
			ExposedVariables.msgRamp1 = value;
		}
	}

	public LocalisedString[] msgRamp2
	{
		get
		{
			return ExposedVariables.msgRamp2;
		}
		set
		{
			ExposedVariables.msgRamp2 = value;
		}
	}

	public string Ramp2Trigger
	{
		get
		{
			return ExposedVariables.Ramp2Trigger;
		}
		set
		{
			ExposedVariables.Ramp2Trigger = value;
		}
	}

	public LocalisedString[] msgRamp3
	{
		get
		{
			return ExposedVariables.msgRamp3;
		}
		set
		{
			ExposedVariables.msgRamp3 = value;
		}
	}

	public string Ramp3Trigger
	{
		get
		{
			return ExposedVariables.Ramp3Trigger;
		}
		set
		{
			ExposedVariables.Ramp3Trigger = value;
		}
	}

	public string Bridge1Trigger
	{
		get
		{
			return ExposedVariables.Bridge1Trigger;
		}
		set
		{
			ExposedVariables.Bridge1Trigger = value;
		}
	}

	public string Bridge2Trigger
	{
		get
		{
			return ExposedVariables.Bridge2Trigger;
		}
		set
		{
			ExposedVariables.Bridge2Trigger = value;
		}
	}

	public LocalisedString[] msgBridge2
	{
		get
		{
			return ExposedVariables.msgBridge2;
		}
		set
		{
			ExposedVariables.msgBridge2 = value;
		}
	}

	public LocalisedString[] msgBridge3
	{
		get
		{
			return ExposedVariables.msgBridge3;
		}
		set
		{
			ExposedVariables.msgBridge3 = value;
		}
	}

	public string Bridge3Trigger
	{
		get
		{
			return ExposedVariables.Bridge3Trigger;
		}
		set
		{
			ExposedVariables.Bridge3Trigger = value;
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

	public LocalisedString[] msgNPCVehiclePurchased
	{
		get
		{
			return ExposedVariables.msgNPCVehiclePurchased;
		}
		set
		{
			ExposedVariables.msgNPCVehiclePurchased = value;
		}
	}

	public LocalisedString[] msgOutOfTime
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

	public LocalisedString[] msgOutOfTimeCanStillBuy
	{
		get
		{
			return ExposedVariables.msgOutOfTimeCanStillBuy;
		}
		set
		{
			ExposedVariables.msgOutOfTimeCanStillBuy = value;
		}
	}

	public string StartPosition
	{
		get
		{
			return ExposedVariables.StartPosition;
		}
		set
		{
			ExposedVariables.StartPosition = value;
		}
	}

	public string Ramp4Trigger
	{
		get
		{
			return ExposedVariables.Ramp4Trigger;
		}
		set
		{
			ExposedVariables.Ramp4Trigger = value;
		}
	}

	public LocalisedString[] msgRamp4
	{
		get
		{
			return ExposedVariables.msgRamp4;
		}
		set
		{
			ExposedVariables.msgRamp4 = value;
		}
	}

	public float RunTimeLimit
	{
		get
		{
			return ExposedVariables.RunTimeLimit;
		}
		set
		{
			ExposedVariables.RunTimeLimit = value;
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

	public LocalisedString[] MsgTooManyVehiclesSpawnedAlready
	{
		get
		{
			return ExposedVariables.MsgTooManyVehiclesSpawnedAlready;
		}
		set
		{
			ExposedVariables.MsgTooManyVehiclesSpawnedAlready = value;
		}
	}

	public string msgTagSwitchTech
	{
		get
		{
			return ExposedVariables.msgTagSwitchTech;
		}
		set
		{
			ExposedVariables.msgTagSwitchTech = value;
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

	public LocalisedString msgPromptAccessDenied
	{
		get
		{
			return ExposedVariables.msgPromptAccessDenied;
		}
		set
		{
			ExposedVariables.msgPromptAccessDenied = value;
		}
	}

	public SpawnTechData[] vehicleSpawnData3
	{
		get
		{
			return ExposedVariables.vehicleSpawnData3;
		}
		set
		{
			ExposedVariables.vehicleSpawnData3 = value;
		}
	}

	public SpawnTechData[] vehicleSpawnData2
	{
		get
		{
			return ExposedVariables.vehicleSpawnData2;
		}
		set
		{
			ExposedVariables.vehicleSpawnData2 = value;
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

	public LocalisedString[] msgBoosterControlsZoomer
	{
		get
		{
			return ExposedVariables.msgBoosterControlsZoomer;
		}
		set
		{
			ExposedVariables.msgBoosterControlsZoomer = value;
		}
	}

	public string msgTagControls
	{
		get
		{
			return ExposedVariables.msgTagControls;
		}
		set
		{
			ExposedVariables.msgTagControls = value;
		}
	}

	public LocalisedString[] msgBoosterControlsGeneric
	{
		get
		{
			return ExposedVariables.msgBoosterControlsGeneric;
		}
		set
		{
			ExposedVariables.msgBoosterControlsGeneric = value;
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

	public SpawnTechData[] NPCPaymentPoint
	{
		get
		{
			return ExposedVariables.NPCPaymentPoint;
		}
		set
		{
			ExposedVariables.NPCPaymentPoint = value;
		}
	}

	public ManSFX.MiscSfxType SFXRaceStart
	{
		get
		{
			return ExposedVariables.SFXRaceStart;
		}
		set
		{
			ExposedVariables.SFXRaceStart = value;
		}
	}

	public ManSFX.MiscSfxType SFXRaceFailed
	{
		get
		{
			return ExposedVariables.SFXRaceFailed;
		}
		set
		{
			ExposedVariables.SFXRaceFailed = value;
		}
	}

	public ManSFX.MiscSfxType SFXRaceComplete
	{
		get
		{
			return ExposedVariables.SFXRaceComplete;
		}
		set
		{
			ExposedVariables.SFXRaceComplete = value;
		}
	}

	public string StartTrigger
	{
		get
		{
			return ExposedVariables.StartTrigger;
		}
		set
		{
			ExposedVariables.StartTrigger = value;
		}
	}

	public LocalisedString[] msgMissionCompleteNoZoomer
	{
		get
		{
			return ExposedVariables.msgMissionCompleteNoZoomer;
		}
		set
		{
			ExposedVariables.msgMissionCompleteNoZoomer = value;
		}
	}

	public string FlightBlocker1Trigger
	{
		get
		{
			return ExposedVariables.FlightBlocker1Trigger;
		}
		set
		{
			ExposedVariables.FlightBlocker1Trigger = value;
		}
	}

	public LocalisedString[] msgFlightBlockerHit
	{
		get
		{
			return ExposedVariables.msgFlightBlockerHit;
		}
		set
		{
			ExposedVariables.msgFlightBlockerHit = value;
		}
	}

	public ManOnScreenMessages.Speaker NPCSpeaker
	{
		get
		{
			return ExposedVariables.NPCSpeaker;
		}
		set
		{
			ExposedVariables.NPCSpeaker = value;
		}
	}

	public string FlightBlocker2Trigger
	{
		get
		{
			return ExposedVariables.FlightBlocker2Trigger;
		}
		set
		{
			ExposedVariables.FlightBlocker2Trigger = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker SpeakerNPC
	{
		get
		{
			return ExposedVariables.SpeakerNPC;
		}
		set
		{
			ExposedVariables.SpeakerNPC = value;
		}
	}

	public uScript_AddMessage.MessageData msgDespawnTechs
	{
		get
		{
			return ExposedVariables.msgDespawnTechs;
		}
		set
		{
			ExposedVariables.msgDespawnTechs = value;
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
