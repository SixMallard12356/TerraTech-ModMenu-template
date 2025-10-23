using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_BridgeTroll")]
public class Mission_SetPiece_BridgeTroll_Component : uScriptCode
{
	public Mission_SetPiece_BridgeTroll ExposedVariables = new Mission_SetPiece_BridgeTroll();

	public uScript_AddMessage.MessageData msg05LicensePurchased
	{
		get
		{
			return ExposedVariables.msg05LicensePurchased;
		}
		set
		{
			ExposedVariables.msg05LicensePurchased = value;
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

	public float distNPCFound
	{
		get
		{
			return ExposedVariables.distNPCFound;
		}
		set
		{
			ExposedVariables.distNPCFound = value;
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

	public uScript_AddMessage.MessageData msg03bNotEnoughMoney
	{
		get
		{
			return ExposedVariables.msg03bNotEnoughMoney;
		}
		set
		{
			ExposedVariables.msg03bNotEnoughMoney = value;
		}
	}

	public uScript_AddMessage.MessageData msg03aPurchaseDeclined
	{
		get
		{
			return ExposedVariables.msg03aPurchaseDeclined;
		}
		set
		{
			ExposedVariables.msg03aPurchaseDeclined = value;
		}
	}

	public uScript_AddMessage.MessageData msg02ClickScreen
	{
		get
		{
			return ExposedVariables.msg02ClickScreen;
		}
		set
		{
			ExposedVariables.msg02ClickScreen = value;
		}
	}

	public uScript_AddMessage.MessageData msg02ClickScreen_Pad
	{
		get
		{
			return ExposedVariables.msg02ClickScreen_Pad;
		}
		set
		{
			ExposedVariables.msg02ClickScreen_Pad = value;
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

	public uScript_AddMessage.MessageData msgAngered
	{
		get
		{
			return ExposedVariables.msgAngered;
		}
		set
		{
			ExposedVariables.msgAngered = value;
		}
	}

	public string clearMsgTagWhenAngry
	{
		get
		{
			return ExposedVariables.clearMsgTagWhenAngry;
		}
		set
		{
			ExposedVariables.clearMsgTagWhenAngry = value;
		}
	}

	public SpawnTechData[] VendorSpawnData
	{
		get
		{
			return ExposedVariables.VendorSpawnData;
		}
		set
		{
			ExposedVariables.VendorSpawnData = value;
		}
	}

	public uScript_AddMessage.MessageData msgMissionComplete
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

	public string KillBoxTrigger
	{
		get
		{
			return ExposedVariables.KillBoxTrigger;
		}
		set
		{
			ExposedVariables.KillBoxTrigger = value;
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

	public uScript_AddMessage.MessageData msgMissionCompleteBridgeFall
	{
		get
		{
			return ExposedVariables.msgMissionCompleteBridgeFall;
		}
		set
		{
			ExposedVariables.msgMissionCompleteBridgeFall = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker genericSpeaker
	{
		get
		{
			return ExposedVariables.genericSpeaker;
		}
		set
		{
			ExposedVariables.genericSpeaker = value;
		}
	}

	public string clearMsgTagWhenNormal
	{
		get
		{
			return ExposedVariables.clearMsgTagWhenNormal;
		}
		set
		{
			ExposedVariables.clearMsgTagWhenNormal = value;
		}
	}

	public int LicenseCost
	{
		get
		{
			return ExposedVariables.LicenseCost;
		}
		set
		{
			ExposedVariables.LicenseCost = value;
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

	public string AngryZone
	{
		get
		{
			return ExposedVariables.AngryZone;
		}
		set
		{
			ExposedVariables.AngryZone = value;
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
