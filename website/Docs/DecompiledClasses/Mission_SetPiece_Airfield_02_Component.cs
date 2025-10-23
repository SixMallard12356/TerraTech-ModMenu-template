using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_Airfield_02")]
public class Mission_SetPiece_Airfield_02_Component : uScriptCode
{
	public Mission_SetPiece_Airfield_02 ExposedVariables = new Mission_SetPiece_Airfield_02();

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
