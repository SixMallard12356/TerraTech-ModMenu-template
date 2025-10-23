using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_1_3_FightFirstEnemy")]
public class Mission_GSO_1_3_FightFirstEnemy_Component : uScriptCode
{
	public Mission_GSO_1_3_FightFirstEnemy ExposedVariables = new Mission_GSO_1_3_FightFirstEnemy();

	public TankPreset enemyPreset
	{
		get
		{
			return ExposedVariables.enemyPreset;
		}
		set
		{
			ExposedVariables.enemyPreset = value;
		}
	}

	public float enemyForwardSpawnDist
	{
		get
		{
			return ExposedVariables.enemyForwardSpawnDist;
		}
		set
		{
			ExposedVariables.enemyForwardSpawnDist = value;
		}
	}

	public float distDriveTutorialCheck
	{
		get
		{
			return ExposedVariables.distDriveTutorialCheck;
		}
		set
		{
			ExposedVariables.distDriveTutorialCheck = value;
		}
	}

	public float distDriveWithoutFiringCheck
	{
		get
		{
			return ExposedVariables.distDriveWithoutFiringCheck;
		}
		set
		{
			ExposedVariables.distDriveWithoutFiringCheck = value;
		}
	}

	public float enemyFightTimeoutSecs
	{
		get
		{
			return ExposedVariables.enemyFightTimeoutSecs;
		}
		set
		{
			ExposedVariables.enemyFightTimeoutSecs = value;
		}
	}

	public float distRunFromEnemyCheck
	{
		get
		{
			return ExposedVariables.distRunFromEnemyCheck;
		}
		set
		{
			ExposedVariables.distRunFromEnemyCheck = value;
		}
	}

	public float distDriveWithoutScavengingCheck
	{
		get
		{
			return ExposedVariables.distDriveWithoutScavengingCheck;
		}
		set
		{
			ExposedVariables.distDriveWithoutScavengingCheck = value;
		}
	}

	public uScript_AddMessage.MessageData msg03EnemyIncoming
	{
		get
		{
			return ExposedVariables.msg03EnemyIncoming;
		}
		set
		{
			ExposedVariables.msg03EnemyIncoming = value;
		}
	}

	public uScript_AddMessage.MessageData msg04FightEnemy
	{
		get
		{
			return ExposedVariables.msg04FightEnemy;
		}
		set
		{
			ExposedVariables.msg04FightEnemy = value;
		}
	}

	public uScript_AddMessage.MessageData msg05ShootReminder
	{
		get
		{
			return ExposedVariables.msg05ShootReminder;
		}
		set
		{
			ExposedVariables.msg05ShootReminder = value;
		}
	}

	public float enemyThrottleDriveModifer
	{
		get
		{
			return ExposedVariables.enemyThrottleDriveModifer;
		}
		set
		{
			ExposedVariables.enemyThrottleDriveModifer = value;
		}
	}

	public float enemyThrottleTurnModifer
	{
		get
		{
			return ExposedVariables.enemyThrottleTurnModifer;
		}
		set
		{
			ExposedVariables.enemyThrottleTurnModifer = value;
		}
	}

	public uScript_AddMessage.MessageData msg01TryDriving
	{
		get
		{
			return ExposedVariables.msg01TryDriving;
		}
		set
		{
			ExposedVariables.msg01TryDriving = value;
		}
	}

	public uScript_AddMessage.MessageData msg01TryDriving_Pad
	{
		get
		{
			return ExposedVariables.msg01TryDriving_Pad;
		}
		set
		{
			ExposedVariables.msg01TryDriving_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg02TryShooting
	{
		get
		{
			return ExposedVariables.msg02TryShooting;
		}
		set
		{
			ExposedVariables.msg02TryShooting = value;
		}
	}

	public uScript_AddMessage.MessageData msg02TryShooting_Pad
	{
		get
		{
			return ExposedVariables.msg02TryShooting_Pad;
		}
		set
		{
			ExposedVariables.msg02TryShooting_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg06ScavengeBlocks
	{
		get
		{
			return ExposedVariables.msg06ScavengeBlocks;
		}
		set
		{
			ExposedVariables.msg06ScavengeBlocks = value;
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

	public uScript_AddMessage.MessageData msg06ScavengeBlocks_Pad
	{
		get
		{
			return ExposedVariables.msg06ScavengeBlocks_Pad;
		}
		set
		{
			ExposedVariables.msg06ScavengeBlocks_Pad = value;
		}
	}

	public uScript_AddMessage.MessageData msg05ShootReminder_Pad
	{
		get
		{
			return ExposedVariables.msg05ShootReminder_Pad;
		}
		set
		{
			ExposedVariables.msg05ShootReminder_Pad = value;
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
