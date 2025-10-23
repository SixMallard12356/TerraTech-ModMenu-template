using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_UnlockGeoCorpLicense")]
public class Mission_UnlockGeoCorpLicense_Component : uScriptCode
{
	public Mission_UnlockGeoCorpLicense ExposedVariables = new Mission_UnlockGeoCorpLicense();

	public uScript_AddMessage.MessageData msg04Complete
	{
		get
		{
			return ExposedVariables.msg04Complete;
		}
		set
		{
			ExposedVariables.msg04Complete = value;
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

	public uScript_AddMessage.MessageData msg02NPCFound
	{
		get
		{
			return ExposedVariables.msg02NPCFound;
		}
		set
		{
			ExposedVariables.msg02NPCFound = value;
		}
	}

	public string NPCPosition
	{
		get
		{
			return ExposedVariables.NPCPosition;
		}
		set
		{
			ExposedVariables.NPCPosition = value;
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

	public BlockTypes blockTypeConsumer
	{
		get
		{
			return ExposedVariables.blockTypeConsumer;
		}
		set
		{
			ExposedVariables.blockTypeConsumer = value;
		}
	}

	public uScript_AddMessage.MessageData msgLeavingMissionArea
	{
		get
		{
			return ExposedVariables.msgLeavingMissionArea;
		}
		set
		{
			ExposedVariables.msgLeavingMissionArea = value;
		}
	}

	public string messageTag
	{
		get
		{
			return ExposedVariables.messageTag;
		}
		set
		{
			ExposedVariables.messageTag = value;
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

	public uScript_AddMessage.MessageData msg03aConsumingStarted
	{
		get
		{
			return ExposedVariables.msg03aConsumingStarted;
		}
		set
		{
			ExposedVariables.msg03aConsumingStarted = value;
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

	public uScript_AddMessage.MessageData msg03bConsumingHalfway
	{
		get
		{
			return ExposedVariables.msg03bConsumingHalfway;
		}
		set
		{
			ExposedVariables.msg03bConsumingHalfway = value;
		}
	}

	public float consumeTime
	{
		get
		{
			return ExposedVariables.consumeTime;
		}
		set
		{
			ExposedVariables.consumeTime = value;
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

	public bool _DEBUGEmulateMultiplayer
	{
		get
		{
			return ExposedVariables._DEBUGEmulateMultiplayer;
		}
		set
		{
			ExposedVariables._DEBUGEmulateMultiplayer = value;
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
