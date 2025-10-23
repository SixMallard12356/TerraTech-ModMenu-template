using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_RR_LaserLab")]
public class Mission_SetPiece_RR_LaserLab_Component : uScriptCode
{
	public Mission_SetPiece_RR_LaserLab ExposedVariables = new Mission_SetPiece_RR_LaserLab();

	public SpawnTechData[] npcSpawnData
	{
		get
		{
			return ExposedVariables.npcSpawnData;
		}
		set
		{
			ExposedVariables.npcSpawnData = value;
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

	public uScript_AddMessage.MessageData msg04MissionComplete
	{
		get
		{
			return ExposedVariables.msg04MissionComplete;
		}
		set
		{
			ExposedVariables.msg04MissionComplete = value;
		}
	}

	public uScript_AddMessage.MessageData msg03ReturnToNPC
	{
		get
		{
			return ExposedVariables.msg03ReturnToNPC;
		}
		set
		{
			ExposedVariables.msg03ReturnToNPC = value;
		}
	}

	public BlockTypes turretShieldBlockType
	{
		get
		{
			return ExposedVariables.turretShieldBlockType;
		}
		set
		{
			ExposedVariables.turretShieldBlockType = value;
		}
	}

	public BlockTypes turretWeaponBlockType
	{
		get
		{
			return ExposedVariables.turretWeaponBlockType;
		}
		set
		{
			ExposedVariables.turretWeaponBlockType = value;
		}
	}

	public SpawnTechData[] turretGroup02SpawnData
	{
		get
		{
			return ExposedVariables.turretGroup02SpawnData;
		}
		set
		{
			ExposedVariables.turretGroup02SpawnData = value;
		}
	}

	public SpawnTechData[] turretGroup03SpawnData
	{
		get
		{
			return ExposedVariables.turretGroup03SpawnData;
		}
		set
		{
			ExposedVariables.turretGroup03SpawnData = value;
		}
	}

	public SpawnTechData[] turretGroup01SpawnData
	{
		get
		{
			return ExposedVariables.turretGroup01SpawnData;
		}
		set
		{
			ExposedVariables.turretGroup01SpawnData = value;
		}
	}

	public uScript_AddMessage.MessageData msg02aShieldsDown
	{
		get
		{
			return ExposedVariables.msg02aShieldsDown;
		}
		set
		{
			ExposedVariables.msg02aShieldsDown = value;
		}
	}

	public uScript_AddMessage.MessageData msg02bShieldsDown
	{
		get
		{
			return ExposedVariables.msg02bShieldsDown;
		}
		set
		{
			ExposedVariables.msg02bShieldsDown = value;
		}
	}

	public uScript_AddMessage.MessageData msg02cShieldsDown
	{
		get
		{
			return ExposedVariables.msg02cShieldsDown;
		}
		set
		{
			ExposedVariables.msg02cShieldsDown = value;
		}
	}

	public SpawnTechData[] charger01SpawnData
	{
		get
		{
			return ExposedVariables.charger01SpawnData;
		}
		set
		{
			ExposedVariables.charger01SpawnData = value;
		}
	}

	public SpawnTechData[] charger02SpawnData
	{
		get
		{
			return ExposedVariables.charger02SpawnData;
		}
		set
		{
			ExposedVariables.charger02SpawnData = value;
		}
	}

	public SpawnTechData[] charger03SpawnData
	{
		get
		{
			return ExposedVariables.charger03SpawnData;
		}
		set
		{
			ExposedVariables.charger03SpawnData = value;
		}
	}

	public string[] turretGroup01AnimTriggers
	{
		get
		{
			return ExposedVariables.turretGroup01AnimTriggers;
		}
		set
		{
			ExposedVariables.turretGroup01AnimTriggers = value;
		}
	}

	public string[] turretGroup02AnimTriggers
	{
		get
		{
			return ExposedVariables.turretGroup02AnimTriggers;
		}
		set
		{
			ExposedVariables.turretGroup02AnimTriggers = value;
		}
	}

	public string[] turretGroup03AnimTriggers
	{
		get
		{
			return ExposedVariables.turretGroup03AnimTriggers;
		}
		set
		{
			ExposedVariables.turretGroup03AnimTriggers = value;
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

	public uScript_AddMessage.MessageData msg01IntroMP
	{
		get
		{
			return ExposedVariables.msg01IntroMP;
		}
		set
		{
			ExposedVariables.msg01IntroMP = value;
		}
	}

	public uScript_AddMessage.MessageData msg02FirstRoomClear
	{
		get
		{
			return ExposedVariables.msg02FirstRoomClear;
		}
		set
		{
			ExposedVariables.msg02FirstRoomClear = value;
		}
	}

	public string room02TriggerVolume
	{
		get
		{
			return ExposedVariables.room02TriggerVolume;
		}
		set
		{
			ExposedVariables.room02TriggerVolume = value;
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

	public SpawnTechData[] npcSpawnData02
	{
		get
		{
			return ExposedVariables.npcSpawnData02;
		}
		set
		{
			ExposedVariables.npcSpawnData02 = value;
		}
	}

	public string objectiveMarkerPosition
	{
		get
		{
			return ExposedVariables.objectiveMarkerPosition;
		}
		set
		{
			ExposedVariables.objectiveMarkerPosition = value;
		}
	}

	public string chargerMsgTriggerVolume
	{
		get
		{
			return ExposedVariables.chargerMsgTriggerVolume;
		}
		set
		{
			ExposedVariables.chargerMsgTriggerVolume = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker npcSpeaker
	{
		get
		{
			return ExposedVariables.npcSpeaker;
		}
		set
		{
			ExposedVariables.npcSpeaker = value;
		}
	}

	public uScript_AddMessage.MessageData msg02ShieldChargerInfo
	{
		get
		{
			return ExposedVariables.msg02ShieldChargerInfo;
		}
		set
		{
			ExposedVariables.msg02ShieldChargerInfo = value;
		}
	}

	public float[] turretGroup01AnimDurations
	{
		get
		{
			return ExposedVariables.turretGroup01AnimDurations;
		}
		set
		{
			ExposedVariables.turretGroup01AnimDurations = value;
		}
	}

	public float[] turretGroup02AnimDurations
	{
		get
		{
			return ExposedVariables.turretGroup02AnimDurations;
		}
		set
		{
			ExposedVariables.turretGroup02AnimDurations = value;
		}
	}

	public float[] turretGroup03AnimDurations
	{
		get
		{
			return ExposedVariables.turretGroup03AnimDurations;
		}
		set
		{
			ExposedVariables.turretGroup03AnimDurations = value;
		}
	}

	public string[] switchObjectNames
	{
		get
		{
			return ExposedVariables.switchObjectNames;
		}
		set
		{
			ExposedVariables.switchObjectNames = value;
		}
	}

	public string switch01TriggerVolume
	{
		get
		{
			return ExposedVariables.switch01TriggerVolume;
		}
		set
		{
			ExposedVariables.switch01TriggerVolume = value;
		}
	}

	public string switch02TriggerVolume
	{
		get
		{
			return ExposedVariables.switch02TriggerVolume;
		}
		set
		{
			ExposedVariables.switch02TriggerVolume = value;
		}
	}

	public string switch03TriggerVolume
	{
		get
		{
			return ExposedVariables.switch03TriggerVolume;
		}
		set
		{
			ExposedVariables.switch03TriggerVolume = value;
		}
	}

	public SpawnTechData[] forcefieldSpawnData
	{
		get
		{
			return ExposedVariables.forcefieldSpawnData;
		}
		set
		{
			ExposedVariables.forcefieldSpawnData = value;
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
