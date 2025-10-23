using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_3_1_HeartBlock")]
public class Mission_GSO_3_1_HeartBlock_Component : uScriptCode
{
	public Mission_GSO_3_1_HeartBlock ExposedVariables = new Mission_GSO_3_1_HeartBlock();

	public string heartBlockPosition
	{
		get
		{
			return ExposedVariables.heartBlockPosition;
		}
		set
		{
			ExposedVariables.heartBlockPosition = value;
		}
	}

	public float heartBlockSpawnDist
	{
		get
		{
			return ExposedVariables.heartBlockSpawnDist;
		}
		set
		{
			ExposedVariables.heartBlockSpawnDist = value;
		}
	}

	public SpawnBlockData[] heartBlockData
	{
		get
		{
			return ExposedVariables.heartBlockData;
		}
		set
		{
			ExposedVariables.heartBlockData = value;
		}
	}

	public float heartBlockFoundDist
	{
		get
		{
			return ExposedVariables.heartBlockFoundDist;
		}
		set
		{
			ExposedVariables.heartBlockFoundDist = value;
		}
	}

	public string MessageTag
	{
		get
		{
			return ExposedVariables.MessageTag;
		}
		set
		{
			ExposedVariables.MessageTag = value;
		}
	}

	public SpawnTechData[] enemyTechData
	{
		get
		{
			return ExposedVariables.enemyTechData;
		}
		set
		{
			ExposedVariables.enemyTechData = value;
		}
	}

	public uScript_AddMessage.MessageData msg01FindHeartBlock
	{
		get
		{
			return ExposedVariables.msg01FindHeartBlock;
		}
		set
		{
			ExposedVariables.msg01FindHeartBlock = value;
		}
	}

	public uScript_AddMessage.MessageData msg02AnchorHeartBlock
	{
		get
		{
			return ExposedVariables.msg02AnchorHeartBlock;
		}
		set
		{
			ExposedVariables.msg02AnchorHeartBlock = value;
		}
	}

	public uScript_AddMessage.MessageData msg03EnemiesIncoming
	{
		get
		{
			return ExposedVariables.msg03EnemiesIncoming;
		}
		set
		{
			ExposedVariables.msg03EnemiesIncoming = value;
		}
	}

	public uScript_AddMessage.MessageData msg04DefendHeartBlock
	{
		get
		{
			return ExposedVariables.msg04DefendHeartBlock;
		}
		set
		{
			ExposedVariables.msg04DefendHeartBlock = value;
		}
	}

	public uScript_AddMessage.MessageData msg05PoweredButEnemiesAlive
	{
		get
		{
			return ExposedVariables.msg05PoweredButEnemiesAlive;
		}
		set
		{
			ExposedVariables.msg05PoweredButEnemiesAlive = value;
		}
	}

	public uScript_AddMessage.MessageData msg06EnemiesDeadButNotPowered
	{
		get
		{
			return ExposedVariables.msg06EnemiesDeadButNotPowered;
		}
		set
		{
			ExposedVariables.msg06EnemiesDeadButNotPowered = value;
		}
	}

	public uScript_AddMessage.MessageData msg07MissionComplete
	{
		get
		{
			return ExposedVariables.msg07MissionComplete;
		}
		set
		{
			ExposedVariables.msg07MissionComplete = value;
		}
	}

	public uScript_AddMessage.MessageData msg08MissionCompleteNotAnchored
	{
		get
		{
			return ExposedVariables.msg08MissionCompleteNotAnchored;
		}
		set
		{
			ExposedVariables.msg08MissionCompleteNotAnchored = value;
		}
	}

	public uScript_AddMessage.MessageData msg09HeartBlockExplanation
	{
		get
		{
			return ExposedVariables.msg09HeartBlockExplanation;
		}
		set
		{
			ExposedVariables.msg09HeartBlockExplanation = value;
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

	public uScript_AddMessage.MessageData msg10HeartBlockDestroyed
	{
		get
		{
			return ExposedVariables.msg10HeartBlockDestroyed;
		}
		set
		{
			ExposedVariables.msg10HeartBlockDestroyed = value;
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
