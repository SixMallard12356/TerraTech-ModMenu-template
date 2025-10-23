using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_Airfield_01")]
public class Mission_SetPiece_Airfield_01_Component : uScriptCode
{
	public Mission_SetPiece_Airfield_01 ExposedVariables = new Mission_SetPiece_Airfield_01();

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
