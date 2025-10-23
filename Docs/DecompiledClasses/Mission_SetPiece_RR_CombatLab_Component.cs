using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_RR_CombatLab")]
public class Mission_SetPiece_RR_CombatLab_Component : uScriptCode
{
	public Mission_SetPiece_RR_CombatLab ExposedVariables = new Mission_SetPiece_RR_CombatLab();

	public BlockTypes laserTurretShieldBlockType
	{
		get
		{
			return ExposedVariables.laserTurretShieldBlockType;
		}
		set
		{
			ExposedVariables.laserTurretShieldBlockType = value;
		}
	}

	public BlockTypes laserTurretWeaponBlockType
	{
		get
		{
			return ExposedVariables.laserTurretWeaponBlockType;
		}
		set
		{
			ExposedVariables.laserTurretWeaponBlockType = value;
		}
	}

	public float[] laserTurretGroupAnimDurations
	{
		get
		{
			return ExposedVariables.laserTurretGroupAnimDurations;
		}
		set
		{
			ExposedVariables.laserTurretGroupAnimDurations = value;
		}
	}

	public string[] laserTurretGroupAnimTriggers
	{
		get
		{
			return ExposedVariables.laserTurretGroupAnimTriggers;
		}
		set
		{
			ExposedVariables.laserTurretGroupAnimTriggers = value;
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

	public string NPCTriggerVolume01
	{
		get
		{
			return ExposedVariables.NPCTriggerVolume01;
		}
		set
		{
			ExposedVariables.NPCTriggerVolume01 = value;
		}
	}

	public BlockTypes teslaTurretShieldBlockType
	{
		get
		{
			return ExposedVariables.teslaTurretShieldBlockType;
		}
		set
		{
			ExposedVariables.teslaTurretShieldBlockType = value;
		}
	}

	public SpawnTechData[] NPCSpawnData01
	{
		get
		{
			return ExposedVariables.NPCSpawnData01;
		}
		set
		{
			ExposedVariables.NPCSpawnData01 = value;
		}
	}

	public Transform NPCDespawnEffect
	{
		get
		{
			return ExposedVariables.NPCDespawnEffect;
		}
		set
		{
			ExposedVariables.NPCDespawnEffect = value;
		}
	}

	public string NPCTriggerVolume02
	{
		get
		{
			return ExposedVariables.NPCTriggerVolume02;
		}
		set
		{
			ExposedVariables.NPCTriggerVolume02 = value;
		}
	}

	public SpawnTechData[] NPCSpawnData02
	{
		get
		{
			return ExposedVariables.NPCSpawnData02;
		}
		set
		{
			ExposedVariables.NPCSpawnData02 = value;
		}
	}

	public uScript_AddMessage.MessageData msg07Outro
	{
		get
		{
			return ExposedVariables.msg07Outro;
		}
		set
		{
			ExposedVariables.msg07Outro = value;
		}
	}

	public uScript_AddMessage.MessageData msg06BossDefeated
	{
		get
		{
			return ExposedVariables.msg06BossDefeated;
		}
		set
		{
			ExposedVariables.msg06BossDefeated = value;
		}
	}

	public uScript_AddMessage.MessageData msg05ChargersDestroyedAll
	{
		get
		{
			return ExposedVariables.msg05ChargersDestroyedAll;
		}
		set
		{
			ExposedVariables.msg05ChargersDestroyedAll = value;
		}
	}

	public BlockTypes bossTurretShieldBlockType
	{
		get
		{
			return ExposedVariables.bossTurretShieldBlockType;
		}
		set
		{
			ExposedVariables.bossTurretShieldBlockType = value;
		}
	}

	public bool _DEBUG_SKIPS_ACTIVE
	{
		get
		{
			return ExposedVariables._DEBUG_SKIPS_ACTIVE;
		}
		set
		{
			ExposedVariables._DEBUG_SKIPS_ACTIVE = value;
		}
	}

	public SpawnTechData[] ForcefieldInnerGroupSpawnData
	{
		get
		{
			return ExposedVariables.ForcefieldInnerGroupSpawnData;
		}
		set
		{
			ExposedVariables.ForcefieldInnerGroupSpawnData = value;
		}
	}

	public SpawnTechData[] laserTurretGroupSpawnData
	{
		get
		{
			return ExposedVariables.laserTurretGroupSpawnData;
		}
		set
		{
			ExposedVariables.laserTurretGroupSpawnData = value;
		}
	}

	public SpawnTechData[] bossTurretSpawnData
	{
		get
		{
			return ExposedVariables.bossTurretSpawnData;
		}
		set
		{
			ExposedVariables.bossTurretSpawnData = value;
		}
	}

	public SpawnTechData[] shieldChargerGroupSpawnData
	{
		get
		{
			return ExposedVariables.shieldChargerGroupSpawnData;
		}
		set
		{
			ExposedVariables.shieldChargerGroupSpawnData = value;
		}
	}

	public SpawnTechData[] ForcefieldOuterGroupSpawnData
	{
		get
		{
			return ExposedVariables.ForcefieldOuterGroupSpawnData;
		}
		set
		{
			ExposedVariables.ForcefieldOuterGroupSpawnData = value;
		}
	}

	public string initialTechSpawnCheck_Position
	{
		get
		{
			return ExposedVariables.initialTechSpawnCheck_Position;
		}
		set
		{
			ExposedVariables.initialTechSpawnCheck_Position = value;
		}
	}

	public float telsaTurretGroupSpawnInterval
	{
		get
		{
			return ExposedVariables.telsaTurretGroupSpawnInterval;
		}
		set
		{
			ExposedVariables.telsaTurretGroupSpawnInterval = value;
		}
	}

	public float initialTechSpawnCheck_Distance
	{
		get
		{
			return ExposedVariables.initialTechSpawnCheck_Distance;
		}
		set
		{
			ExposedVariables.initialTechSpawnCheck_Distance = value;
		}
	}

	public SpawnTechData[] teslaTurretGroupSpawnData
	{
		get
		{
			return ExposedVariables.teslaTurretGroupSpawnData;
		}
		set
		{
			ExposedVariables.teslaTurretGroupSpawnData = value;
		}
	}

	public uScript_AddMessage.MessageData msg03ChargersDestroyedHalfway
	{
		get
		{
			return ExposedVariables.msg03ChargersDestroyedHalfway;
		}
		set
		{
			ExposedVariables.msg03ChargersDestroyedHalfway = value;
		}
	}

	public uScript_AddMessage.MessageData msg04ChargersDestroyedOneLeft
	{
		get
		{
			return ExposedVariables.msg04ChargersDestroyedOneLeft;
		}
		set
		{
			ExposedVariables.msg04ChargersDestroyedOneLeft = value;
		}
	}

	public string room01TriggerVolume
	{
		get
		{
			return ExposedVariables.room01TriggerVolume;
		}
		set
		{
			ExposedVariables.room01TriggerVolume = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker NPCSpeaker
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

	public uScript_AddMessage.MessageData msg02TurretExplanation
	{
		get
		{
			return ExposedVariables.msg02TurretExplanation;
		}
		set
		{
			ExposedVariables.msg02TurretExplanation = value;
		}
	}

	public float BossTurretHostileBatteryCharge
	{
		get
		{
			return ExposedVariables.BossTurretHostileBatteryCharge;
		}
		set
		{
			ExposedVariables.BossTurretHostileBatteryCharge = value;
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
