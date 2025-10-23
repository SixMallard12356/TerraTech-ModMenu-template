using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_2_1_UseTradingStation")]
public class Mission_GSO_2_1_UseTradingStation_Component : uScriptCode
{
	public Mission_GSO_2_1_UseTradingStation ExposedVariables = new Mission_GSO_2_1_UseTradingStation();

	public uScript_AddMessage.MessageData msg01EarnMoneyIntro
	{
		get
		{
			return ExposedVariables.msg01EarnMoneyIntro;
		}
		set
		{
			ExposedVariables.msg01EarnMoneyIntro = value;
		}
	}

	public uScript_AddMessage.MessageData msg05ResourcesSold
	{
		get
		{
			return ExposedVariables.msg05ResourcesSold;
		}
		set
		{
			ExposedVariables.msg05ResourcesSold = value;
		}
	}

	public uScript_AddMessage.MessageData msg03AttachBlock
	{
		get
		{
			return ExposedVariables.msg03AttachBlock;
		}
		set
		{
			ExposedVariables.msg03AttachBlock = value;
		}
	}

	public ItemTypeInfo blockToHighlight
	{
		get
		{
			return ExposedVariables.blockToHighlight;
		}
		set
		{
			ExposedVariables.blockToHighlight = value;
		}
	}

	public BlockCategories blockCategoryToHighlight
	{
		get
		{
			return ExposedVariables.blockCategoryToHighlight;
		}
		set
		{
			ExposedVariables.blockCategoryToHighlight = value;
		}
	}

	public BlockTypes blockToPurchase
	{
		get
		{
			return ExposedVariables.blockToPurchase;
		}
		set
		{
			ExposedVariables.blockToPurchase = value;
		}
	}

	public uScript_AddMessage.MessageData msg06OpenMissionBoard
	{
		get
		{
			return ExposedVariables.msg06OpenMissionBoard;
		}
		set
		{
			ExposedVariables.msg06OpenMissionBoard = value;
		}
	}

	public uScript_AddMessage.MessageData msg07Complete
	{
		get
		{
			return ExposedVariables.msg07Complete;
		}
		set
		{
			ExposedVariables.msg07Complete = value;
		}
	}

	public uScript_AddMessage.MessageData msg02bPurchaseBlock
	{
		get
		{
			return ExposedVariables.msg02bPurchaseBlock;
		}
		set
		{
			ExposedVariables.msg02bPurchaseBlock = value;
		}
	}

	public uScript_AddMessage.MessageData msg02bPurchaseBlock_Pad
	{
		get
		{
			return ExposedVariables.msg02bPurchaseBlock_Pad;
		}
		set
		{
			ExposedVariables.msg02bPurchaseBlock_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg02aOpenShop_Pad
	{
		get
		{
			return ExposedVariables.msg02aOpenShop_Pad;
		}
		set
		{
			ExposedVariables.msg02aOpenShop_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg02aOpenShop
	{
		get
		{
			return ExposedVariables.msg02aOpenShop;
		}
		set
		{
			ExposedVariables.msg02aOpenShop = value;
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

	public uScript_AddMessage.MessageData msg04SellResources
	{
		get
		{
			return ExposedVariables.msg04SellResources;
		}
		set
		{
			ExposedVariables.msg04SellResources = value;
		}
	}

	public Vector3 shopArrowOffset
	{
		get
		{
			return ExposedVariables.shopArrowOffset;
		}
		set
		{
			ExposedVariables.shopArrowOffset = value;
		}
	}

	public Vector3 missionBoardArrowOffset
	{
		get
		{
			return ExposedVariables.missionBoardArrowOffset;
		}
		set
		{
			ExposedVariables.missionBoardArrowOffset = value;
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
