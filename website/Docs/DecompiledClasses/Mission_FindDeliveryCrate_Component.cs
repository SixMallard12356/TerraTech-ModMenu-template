using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_FindDeliveryCrate")]
public class Mission_FindDeliveryCrate_Component : uScriptCode
{
	public Mission_FindDeliveryCrate ExposedVariables = new Mission_FindDeliveryCrate();

	public uScript_AddMessage.MessageData msgEnteringArea
	{
		get
		{
			return ExposedVariables.msgEnteringArea;
		}
		set
		{
			ExposedVariables.msgEnteringArea = value;
		}
	}

	public uScript_AddMessage.MessageData msgEnemyLanding
	{
		get
		{
			return ExposedVariables.msgEnemyLanding;
		}
		set
		{
			ExposedVariables.msgEnemyLanding = value;
		}
	}

	public uScript_AddMessage.MessageData msgEnemyDestroyed
	{
		get
		{
			return ExposedVariables.msgEnemyDestroyed;
		}
		set
		{
			ExposedVariables.msgEnemyDestroyed = value;
		}
	}

	public string cratePosition
	{
		get
		{
			return ExposedVariables.cratePosition;
		}
		set
		{
			ExposedVariables.cratePosition = value;
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

	public uScript_AddMessage.MessageData msgCrateUnlocked
	{
		get
		{
			return ExposedVariables.msgCrateUnlocked;
		}
		set
		{
			ExposedVariables.msgCrateUnlocked = value;
		}
	}

	public string enemyPosition
	{
		get
		{
			return ExposedVariables.enemyPosition;
		}
		set
		{
			ExposedVariables.enemyPosition = value;
		}
	}

	public SpawnTechData[] enemySpawnData
	{
		get
		{
			return ExposedVariables.enemySpawnData;
		}
		set
		{
			ExposedVariables.enemySpawnData = value;
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
