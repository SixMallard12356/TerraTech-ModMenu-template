using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_Grade1_Quiz_Mission_Template")]
public class Mission_Grade1_Quiz_Mission_Template_Component : uScriptCode
{
	public Mission_Grade1_Quiz_Mission_Template ExposedVariables = new Mission_Grade1_Quiz_Mission_Template();

	public SpawnTechData[] buttonbase3SpawnData
	{
		get
		{
			return ExposedVariables.buttonbase3SpawnData;
		}
		set
		{
			ExposedVariables.buttonbase3SpawnData = value;
		}
	}

	public string ButtonBasePosition
	{
		get
		{
			return ExposedVariables.ButtonBasePosition;
		}
		set
		{
			ExposedVariables.ButtonBasePosition = value;
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

	public BlockTypes blockTypeButton2
	{
		get
		{
			return ExposedVariables.blockTypeButton2;
		}
		set
		{
			ExposedVariables.blockTypeButton2 = value;
		}
	}

	public BlockTypes blockTypeButton1
	{
		get
		{
			return ExposedVariables.blockTypeButton1;
		}
		set
		{
			ExposedVariables.blockTypeButton1 = value;
		}
	}

	public SpawnTechData[] buttonbase1SpawnData
	{
		get
		{
			return ExposedVariables.buttonbase1SpawnData;
		}
		set
		{
			ExposedVariables.buttonbase1SpawnData = value;
		}
	}

	public BlockTypes blockTypeButton4
	{
		get
		{
			return ExposedVariables.blockTypeButton4;
		}
		set
		{
			ExposedVariables.blockTypeButton4 = value;
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

	public BlockTypes blockTypeButton3
	{
		get
		{
			return ExposedVariables.blockTypeButton3;
		}
		set
		{
			ExposedVariables.blockTypeButton3 = value;
		}
	}

	public SpawnTechData[] buttonbase4SpawnData
	{
		get
		{
			return ExposedVariables.buttonbase4SpawnData;
		}
		set
		{
			ExposedVariables.buttonbase4SpawnData = value;
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

	public SpawnTechData[] buttonbase2SpawnData
	{
		get
		{
			return ExposedVariables.buttonbase2SpawnData;
		}
		set
		{
			ExposedVariables.buttonbase2SpawnData = value;
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

	public uScript_AddMessage.MessageData msg02BaseFound
	{
		get
		{
			return ExposedVariables.msg02BaseFound;
		}
		set
		{
			ExposedVariables.msg02BaseFound = value;
		}
	}

	public bool Q2Button1
	{
		get
		{
			return ExposedVariables.Q2Button1;
		}
		set
		{
			ExposedVariables.Q2Button1 = value;
		}
	}

	public bool Q1Button3
	{
		get
		{
			return ExposedVariables.Q1Button3;
		}
		set
		{
			ExposedVariables.Q1Button3 = value;
		}
	}

	public bool Q1Button2
	{
		get
		{
			return ExposedVariables.Q1Button2;
		}
		set
		{
			ExposedVariables.Q1Button2 = value;
		}
	}

	public bool Q1Button4
	{
		get
		{
			return ExposedVariables.Q1Button4;
		}
		set
		{
			ExposedVariables.Q1Button4 = value;
		}
	}

	public uScript_AddMessage.MessageData msg04QuestionOneWrong
	{
		get
		{
			return ExposedVariables.msg04QuestionOneWrong;
		}
		set
		{
			ExposedVariables.msg04QuestionOneWrong = value;
		}
	}

	public uScript_AddMessage.MessageData msg03QuestionOneCorrect
	{
		get
		{
			return ExposedVariables.msg03QuestionOneCorrect;
		}
		set
		{
			ExposedVariables.msg03QuestionOneCorrect = value;
		}
	}

	public uScript_AddMessage.MessageData msg07QuestionTwoWrong
	{
		get
		{
			return ExposedVariables.msg07QuestionTwoWrong;
		}
		set
		{
			ExposedVariables.msg07QuestionTwoWrong = value;
		}
	}

	public uScript_AddMessage.MessageData msg05QuestionTwo
	{
		get
		{
			return ExposedVariables.msg05QuestionTwo;
		}
		set
		{
			ExposedVariables.msg05QuestionTwo = value;
		}
	}

	public bool Q2Button2
	{
		get
		{
			return ExposedVariables.Q2Button2;
		}
		set
		{
			ExposedVariables.Q2Button2 = value;
		}
	}

	public bool Q2Button4
	{
		get
		{
			return ExposedVariables.Q2Button4;
		}
		set
		{
			ExposedVariables.Q2Button4 = value;
		}
	}

	public uScript_AddMessage.MessageData msg03QuestionOne
	{
		get
		{
			return ExposedVariables.msg03QuestionOne;
		}
		set
		{
			ExposedVariables.msg03QuestionOne = value;
		}
	}

	public bool Q2Button3
	{
		get
		{
			return ExposedVariables.Q2Button3;
		}
		set
		{
			ExposedVariables.Q2Button3 = value;
		}
	}

	public bool Q1Button1
	{
		get
		{
			return ExposedVariables.Q1Button1;
		}
		set
		{
			ExposedVariables.Q1Button1 = value;
		}
	}

	public uScript_AddMessage.MessageData msg06QuestionTwoCorrect
	{
		get
		{
			return ExposedVariables.msg06QuestionTwoCorrect;
		}
		set
		{
			ExposedVariables.msg06QuestionTwoCorrect = value;
		}
	}

	public string ButtonBase1VFXSpawn
	{
		get
		{
			return ExposedVariables.ButtonBase1VFXSpawn;
		}
		set
		{
			ExposedVariables.ButtonBase1VFXSpawn = value;
		}
	}

	public bool Q3Button3
	{
		get
		{
			return ExposedVariables.Q3Button3;
		}
		set
		{
			ExposedVariables.Q3Button3 = value;
		}
	}

	public uScript_AddMessage.MessageData msgSpawnMinion
	{
		get
		{
			return ExposedVariables.msgSpawnMinion;
		}
		set
		{
			ExposedVariables.msgSpawnMinion = value;
		}
	}

	public uScript_AddMessage.MessageData msgMinionDefeated
	{
		get
		{
			return ExposedVariables.msgMinionDefeated;
		}
		set
		{
			ExposedVariables.msgMinionDefeated = value;
		}
	}

	public string ButtonBase3VFXSpawn
	{
		get
		{
			return ExposedVariables.ButtonBase3VFXSpawn;
		}
		set
		{
			ExposedVariables.ButtonBase3VFXSpawn = value;
		}
	}

	public bool Q3Button1
	{
		get
		{
			return ExposedVariables.Q3Button1;
		}
		set
		{
			ExposedVariables.Q3Button1 = value;
		}
	}

	public uScript_AddMessage.MessageData msgEncounterComplete
	{
		get
		{
			return ExposedVariables.msgEncounterComplete;
		}
		set
		{
			ExposedVariables.msgEncounterComplete = value;
		}
	}

	public string ButtonBase4VFXSpawn
	{
		get
		{
			return ExposedVariables.ButtonBase4VFXSpawn;
		}
		set
		{
			ExposedVariables.ButtonBase4VFXSpawn = value;
		}
	}

	public SpawnTechData[] EnemyTechData
	{
		get
		{
			return ExposedVariables.EnemyTechData;
		}
		set
		{
			ExposedVariables.EnemyTechData = value;
		}
	}

	public bool Q3Button4
	{
		get
		{
			return ExposedVariables.Q3Button4;
		}
		set
		{
			ExposedVariables.Q3Button4 = value;
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

	public string ButtonBase2VFXSpawn
	{
		get
		{
			return ExposedVariables.ButtonBase2VFXSpawn;
		}
		set
		{
			ExposedVariables.ButtonBase2VFXSpawn = value;
		}
	}

	public float WaitingTime
	{
		get
		{
			return ExposedVariables.WaitingTime;
		}
		set
		{
			ExposedVariables.WaitingTime = value;
		}
	}

	public bool Q3Button2
	{
		get
		{
			return ExposedVariables.Q3Button2;
		}
		set
		{
			ExposedVariables.Q3Button2 = value;
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

	public uScript_AddMessage.MessageData msg09QuestionThreeCorrect
	{
		get
		{
			return ExposedVariables.msg09QuestionThreeCorrect;
		}
		set
		{
			ExposedVariables.msg09QuestionThreeCorrect = value;
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

	public uScript_AddMessage.MessageData msg08QuestionThree
	{
		get
		{
			return ExposedVariables.msg08QuestionThree;
		}
		set
		{
			ExposedVariables.msg08QuestionThree = value;
		}
	}

	public uScript_AddMessage.MessageData msg10QuestionThreeWrong
	{
		get
		{
			return ExposedVariables.msg10QuestionThreeWrong;
		}
		set
		{
			ExposedVariables.msg10QuestionThreeWrong = value;
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

	private void OnGUI()
	{
		ExposedVariables.OnGUI();
	}
}
