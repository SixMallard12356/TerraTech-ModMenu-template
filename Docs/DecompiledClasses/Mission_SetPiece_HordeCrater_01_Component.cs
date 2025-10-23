using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_HordeCrater_01")]
public class Mission_SetPiece_HordeCrater_01_Component : uScriptCode
{
	public Mission_SetPiece_HordeCrater_01 ExposedVariables = new Mission_SetPiece_HordeCrater_01();

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

	public LocalisedString[] msgNPCGreeting
	{
		get
		{
			return ExposedVariables.msgNPCGreeting;
		}
		set
		{
			ExposedVariables.msgNPCGreeting = value;
		}
	}

	public ManSFX.MiscSfxType SFXChallengeComplete
	{
		get
		{
			return ExposedVariables.SFXChallengeComplete;
		}
		set
		{
			ExposedVariables.SFXChallengeComplete = value;
		}
	}

	public LocalisedString[] msgNPCAttack
	{
		get
		{
			return ExposedVariables.msgNPCAttack;
		}
		set
		{
			ExposedVariables.msgNPCAttack = value;
		}
	}

	public LocalisedString[] msgNPCRetrying
	{
		get
		{
			return ExposedVariables.msgNPCRetrying;
		}
		set
		{
			ExposedVariables.msgNPCRetrying = value;
		}
	}

	public LocalisedString[] msgNPCWellDone
	{
		get
		{
			return ExposedVariables.msgNPCWellDone;
		}
		set
		{
			ExposedVariables.msgNPCWellDone = value;
		}
	}

	public LocalisedString[] msgNPCTestComplete
	{
		get
		{
			return ExposedVariables.msgNPCTestComplete;
		}
		set
		{
			ExposedVariables.msgNPCTestComplete = value;
		}
	}

	public LocalisedString[] msgNPCRebriefing
	{
		get
		{
			return ExposedVariables.msgNPCRebriefing;
		}
		set
		{
			ExposedVariables.msgNPCRebriefing = value;
		}
	}

	public LocalisedString[] msgNPCIntroJoke
	{
		get
		{
			return ExposedVariables.msgNPCIntroJoke;
		}
		set
		{
			ExposedVariables.msgNPCIntroJoke = value;
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

	public ManSFX.MiscSfxType SFXChallengeStarted
	{
		get
		{
			return ExposedVariables.SFXChallengeStarted;
		}
		set
		{
			ExposedVariables.SFXChallengeStarted = value;
		}
	}

	public LocalisedString[] msgNPCTestFailedPlayerFled
	{
		get
		{
			return ExposedVariables.msgNPCTestFailedPlayerFled;
		}
		set
		{
			ExposedVariables.msgNPCTestFailedPlayerFled = value;
		}
	}

	public LocalisedString[] msgNPCTestFailedPlayerSwitchedTech
	{
		get
		{
			return ExposedVariables.msgNPCTestFailedPlayerSwitchedTech;
		}
		set
		{
			ExposedVariables.msgNPCTestFailedPlayerSwitchedTech = value;
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

	public ExternalBehaviorTree FlyAwayBehaviour
	{
		get
		{
			return ExposedVariables.FlyAwayBehaviour;
		}
		set
		{
			ExposedVariables.FlyAwayBehaviour = value;
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

	public string TriggerNPC
	{
		get
		{
			return ExposedVariables.TriggerNPC;
		}
		set
		{
			ExposedVariables.TriggerNPC = value;
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

	public uScript_AddMessage.MessageData msgNPCSoldOut
	{
		get
		{
			return ExposedVariables.msgNPCSoldOut;
		}
		set
		{
			ExposedVariables.msgNPCSoldOut = value;
		}
	}

	public SpawnTechData enemyGroup5Data
	{
		get
		{
			return ExposedVariables.enemyGroup5Data;
		}
		set
		{
			ExposedVariables.enemyGroup5Data = value;
		}
	}

	public SpawnTechData enemyGroup1Data
	{
		get
		{
			return ExposedVariables.enemyGroup1Data;
		}
		set
		{
			ExposedVariables.enemyGroup1Data = value;
		}
	}

	public SpawnTechData enemyGroup3Data
	{
		get
		{
			return ExposedVariables.enemyGroup3Data;
		}
		set
		{
			ExposedVariables.enemyGroup3Data = value;
		}
	}

	public SpawnTechData enemyGroup4Data
	{
		get
		{
			return ExposedVariables.enemyGroup4Data;
		}
		set
		{
			ExposedVariables.enemyGroup4Data = value;
		}
	}

	public SpawnTechData enemyGroup2Data
	{
		get
		{
			return ExposedVariables.enemyGroup2Data;
		}
		set
		{
			ExposedVariables.enemyGroup2Data = value;
		}
	}

	public LocalisedString[] msgNPCTestFailedPlayerDead
	{
		get
		{
			return ExposedVariables.msgNPCTestFailedPlayerDead;
		}
		set
		{
			ExposedVariables.msgNPCTestFailedPlayerDead = value;
		}
	}

	public BlockTypes PrototypeWeaponBlock
	{
		get
		{
			return ExposedVariables.PrototypeWeaponBlock;
		}
		set
		{
			ExposedVariables.PrototypeWeaponBlock = value;
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

	public SpawnTechData[] vehicleSpawnData4
	{
		get
		{
			return ExposedVariables.vehicleSpawnData4;
		}
		set
		{
			ExposedVariables.vehicleSpawnData4 = value;
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

	public SpawnTechData[] vehicleSpawnDataRebuy4
	{
		get
		{
			return ExposedVariables.vehicleSpawnDataRebuy4;
		}
		set
		{
			ExposedVariables.vehicleSpawnDataRebuy4 = value;
		}
	}

	public SpawnTechData[] vehicleSpawnDataRebuy3
	{
		get
		{
			return ExposedVariables.vehicleSpawnDataRebuy3;
		}
		set
		{
			ExposedVariables.vehicleSpawnDataRebuy3 = value;
		}
	}

	public SpawnTechData[] vehicleSpawnDataRebuy2
	{
		get
		{
			return ExposedVariables.vehicleSpawnDataRebuy2;
		}
		set
		{
			ExposedVariables.vehicleSpawnDataRebuy2 = value;
		}
	}

	public SpawnTechData[] vehicleSpawnDataRebuy1
	{
		get
		{
			return ExposedVariables.vehicleSpawnDataRebuy1;
		}
		set
		{
			ExposedVariables.vehicleSpawnDataRebuy1 = value;
		}
	}

	public uScript_AddMessage.MessageData msgNPCAllPlayersMustBeInAreaForTest
	{
		get
		{
			return ExposedVariables.msgNPCAllPlayersMustBeInAreaForTest;
		}
		set
		{
			ExposedVariables.msgNPCAllPlayersMustBeInAreaForTest = value;
		}
	}

	public uScript_AddMessage.MessageData msgNPCYouCanRebuy
	{
		get
		{
			return ExposedVariables.msgNPCYouCanRebuy;
		}
		set
		{
			ExposedVariables.msgNPCYouCanRebuy = value;
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

	public LocalisedString[] msgNPCTestFailedPlayerDetachedWeapon
	{
		get
		{
			return ExposedVariables.msgNPCTestFailedPlayerDetachedWeapon;
		}
		set
		{
			ExposedVariables.msgNPCTestFailedPlayerDetachedWeapon = value;
		}
	}

	public LocalisedString[] msgNPCTestFailedSaveLoaded
	{
		get
		{
			return ExposedVariables.msgNPCTestFailedSaveLoaded;
		}
		set
		{
			ExposedVariables.msgNPCTestFailedSaveLoaded = value;
		}
	}

	public ManOnScreenMessages.Speaker messageSpeaker
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

	public string TriggerCrater
	{
		get
		{
			return ExposedVariables.TriggerCrater;
		}
		set
		{
			ExposedVariables.TriggerCrater = value;
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
