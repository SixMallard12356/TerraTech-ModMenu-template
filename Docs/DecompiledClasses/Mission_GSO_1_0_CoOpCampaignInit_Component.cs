using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_GSO_1_0_CoOpCampaignInit")]
public class Mission_GSO_1_0_CoOpCampaignInit_Component : uScriptCode
{
	public Mission_GSO_1_0_CoOpCampaignInit ExposedVariables = new Mission_GSO_1_0_CoOpCampaignInit();

	public Transform Bomb
	{
		get
		{
			return ExposedVariables.Bomb;
		}
		set
		{
			ExposedVariables.Bomb = value;
		}
	}

	public Transform Explosion
	{
		get
		{
			return ExposedVariables.Explosion;
		}
		set
		{
			ExposedVariables.Explosion = value;
		}
	}

	public string DontDisableBombMeshChild
	{
		get
		{
			return ExposedVariables.DontDisableBombMeshChild;
		}
		set
		{
			ExposedVariables.DontDisableBombMeshChild = value;
		}
	}

	public uScript_SetCraterMissionsSpawnPos.CraterSpawnData[] CraterSpawnData
	{
		get
		{
			return ExposedVariables.CraterSpawnData;
		}
		set
		{
			ExposedVariables.CraterSpawnData = value;
		}
	}

	public float messageStartDelay
	{
		get
		{
			return ExposedVariables.messageStartDelay;
		}
		set
		{
			ExposedVariables.messageStartDelay = value;
		}
	}

	public uScript_AddMessage.MessageData msgIntro
	{
		get
		{
			return ExposedVariables.msgIntro;
		}
		set
		{
			ExposedVariables.msgIntro = value;
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

	public int startingMoney
	{
		get
		{
			return ExposedVariables.startingMoney;
		}
		set
		{
			ExposedVariables.startingMoney = value;
		}
	}

	public TankPreset BuiltTechPreset
	{
		get
		{
			return ExposedVariables.BuiltTechPreset;
		}
		set
		{
			ExposedVariables.BuiltTechPreset = value;
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
