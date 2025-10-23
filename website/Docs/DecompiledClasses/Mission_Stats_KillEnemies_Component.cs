using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_Stats_KillEnemies")]
public class Mission_Stats_KillEnemies_Component : uScriptCode
{
	public Mission_Stats_KillEnemies ExposedVariables = new Mission_Stats_KillEnemies();

	public bool useFactionType
	{
		get
		{
			return ExposedVariables.useFactionType;
		}
		set
		{
			ExposedVariables.useFactionType = value;
		}
	}

	public FactionSubTypes factionType
	{
		get
		{
			return ExposedVariables.factionType;
		}
		set
		{
			ExposedVariables.factionType = value;
		}
	}

	public bool markNearestEnemyOnRadar
	{
		get
		{
			return ExposedVariables.markNearestEnemyOnRadar;
		}
		set
		{
			ExposedVariables.markNearestEnemyOnRadar = value;
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

	public uScript_AddMessage.MessageData msgComplete
	{
		get
		{
			return ExposedVariables.msgComplete;
		}
		set
		{
			ExposedVariables.msgComplete = value;
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
