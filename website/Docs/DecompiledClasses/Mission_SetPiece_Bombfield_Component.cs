using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_Bombfield")]
public class Mission_SetPiece_Bombfield_Component : uScriptCode
{
	public Mission_SetPiece_Bombfield ExposedVariables = new Mission_SetPiece_Bombfield();

	public LocalisedString[] MsgMissionComplete
	{
		get
		{
			return ExposedVariables.MsgMissionComplete;
		}
		set
		{
			ExposedVariables.MsgMissionComplete = value;
		}
	}

	public LocalisedString[] MsgArrivedAtMission
	{
		get
		{
			return ExposedVariables.MsgArrivedAtMission;
		}
		set
		{
			ExposedVariables.MsgArrivedAtMission = value;
		}
	}

	public LocalisedString[] MsgFirstTurretDead
	{
		get
		{
			return ExposedVariables.MsgFirstTurretDead;
		}
		set
		{
			ExposedVariables.MsgFirstTurretDead = value;
		}
	}

	public LocalisedString[] MsgSecondTurretDead
	{
		get
		{
			return ExposedVariables.MsgSecondTurretDead;
		}
		set
		{
			ExposedVariables.MsgSecondTurretDead = value;
		}
	}

	public LocalisedString[] MsgThirdTurretDead
	{
		get
		{
			return ExposedVariables.MsgThirdTurretDead;
		}
		set
		{
			ExposedVariables.MsgThirdTurretDead = value;
		}
	}

	public string TriggerA
	{
		get
		{
			return ExposedVariables.TriggerA;
		}
		set
		{
			ExposedVariables.TriggerA = value;
		}
	}

	public LocalisedString[] MsgNearTurret1
	{
		get
		{
			return ExposedVariables.MsgNearTurret1;
		}
		set
		{
			ExposedVariables.MsgNearTurret1 = value;
		}
	}

	public LocalisedString[] MsgNearTurret2
	{
		get
		{
			return ExposedVariables.MsgNearTurret2;
		}
		set
		{
			ExposedVariables.MsgNearTurret2 = value;
		}
	}

	public string TriggerB
	{
		get
		{
			return ExposedVariables.TriggerB;
		}
		set
		{
			ExposedVariables.TriggerB = value;
		}
	}

	public string TriggerC
	{
		get
		{
			return ExposedVariables.TriggerC;
		}
		set
		{
			ExposedVariables.TriggerC = value;
		}
	}

	public LocalisedString[] MsgNearTurret3
	{
		get
		{
			return ExposedVariables.MsgNearTurret3;
		}
		set
		{
			ExposedVariables.MsgNearTurret3 = value;
		}
	}

	public LocalisedString[] MsgAtRightHeight
	{
		get
		{
			return ExposedVariables.MsgAtRightHeight;
		}
		set
		{
			ExposedVariables.MsgAtRightHeight = value;
		}
	}

	public string TriggerD
	{
		get
		{
			return ExposedVariables.TriggerD;
		}
		set
		{
			ExposedVariables.TriggerD = value;
		}
	}

	public LocalisedString[] MsgMinefieldWarning
	{
		get
		{
			return ExposedVariables.MsgMinefieldWarning;
		}
		set
		{
			ExposedVariables.MsgMinefieldWarning = value;
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

	public string TriggerX
	{
		get
		{
			return ExposedVariables.TriggerX;
		}
		set
		{
			ExposedVariables.TriggerX = value;
		}
	}

	public LocalisedString[] MsgBuyNewPlaneOffer
	{
		get
		{
			return ExposedVariables.MsgBuyNewPlaneOffer;
		}
		set
		{
			ExposedVariables.MsgBuyNewPlaneOffer = value;
		}
	}

	public BlockTypes PlayerTechPropBlock
	{
		get
		{
			return ExposedVariables.PlayerTechPropBlock;
		}
		set
		{
			ExposedVariables.PlayerTechPropBlock = value;
		}
	}

	public BlockTypes PlayerTechBombBlock
	{
		get
		{
			return ExposedVariables.PlayerTechBombBlock;
		}
		set
		{
			ExposedVariables.PlayerTechBombBlock = value;
		}
	}

	public LocalisedString[] MsgPlayerHitAMine
	{
		get
		{
			return ExposedVariables.MsgPlayerHitAMine;
		}
		set
		{
			ExposedVariables.MsgPlayerHitAMine = value;
		}
	}

	public string TriggerG
	{
		get
		{
			return ExposedVariables.TriggerG;
		}
		set
		{
			ExposedVariables.TriggerG = value;
		}
	}

	public LocalisedString[] MsgEnemyHitAMine
	{
		get
		{
			return ExposedVariables.MsgEnemyHitAMine;
		}
		set
		{
			ExposedVariables.MsgEnemyHitAMine = value;
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

	public SpawnTechData[] vehicleSpawnData5
	{
		get
		{
			return ExposedVariables.vehicleSpawnData5;
		}
		set
		{
			ExposedVariables.vehicleSpawnData5 = value;
		}
	}

	public LocalisedString[] MsgTooManyPlanesSpawnedAlready
	{
		get
		{
			return ExposedVariables.MsgTooManyPlanesSpawnedAlready;
		}
		set
		{
			ExposedVariables.MsgTooManyPlanesSpawnedAlready = value;
		}
	}

	public SpawnTechData[] TurretData1
	{
		get
		{
			return ExposedVariables.TurretData1;
		}
		set
		{
			ExposedVariables.TurretData1 = value;
		}
	}

	public SpawnTechData[] TurretData2
	{
		get
		{
			return ExposedVariables.TurretData2;
		}
		set
		{
			ExposedVariables.TurretData2 = value;
		}
	}

	public SpawnTechData[] TurretData3
	{
		get
		{
			return ExposedVariables.TurretData3;
		}
		set
		{
			ExposedVariables.TurretData3 = value;
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

	public LocalisedString msgPromptTextBlackbird
	{
		get
		{
			return ExposedVariables.msgPromptTextBlackbird;
		}
		set
		{
			ExposedVariables.msgPromptTextBlackbird = value;
		}
	}

	public LocalisedString[] msgDespawnTechs
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
