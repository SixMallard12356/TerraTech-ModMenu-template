using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_Grade2_Quiz_Mission_Template")]
public class Mission_Grade2_Quiz_Mission_Template_Component : uScriptCode
{
	public Mission_Grade2_Quiz_Mission_Template ExposedVariables = new Mission_Grade2_Quiz_Mission_Template();

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

	public uScript_AddMessage.MessageData msg08QuestionTwoWrong
	{
		get
		{
			return ExposedVariables.msg08QuestionTwoWrong;
		}
		set
		{
			ExposedVariables.msg08QuestionTwoWrong = value;
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

	public uScript_AddMessage.MessageData msg07QuestionTwoCorrect
	{
		get
		{
			return ExposedVariables.msg07QuestionTwoCorrect;
		}
		set
		{
			ExposedVariables.msg07QuestionTwoCorrect = value;
		}
	}

	public uScript_AddMessage.MessageData msg20QuestionSixWrong
	{
		get
		{
			return ExposedVariables.msg20QuestionSixWrong;
		}
		set
		{
			ExposedVariables.msg20QuestionSixWrong = value;
		}
	}

	public uScript_AddMessage.MessageData msg06QuestionTwo
	{
		get
		{
			return ExposedVariables.msg06QuestionTwo;
		}
		set
		{
			ExposedVariables.msg06QuestionTwo = value;
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

	public uScript_AddMessage.MessageData msg04QuestionOneCorrect
	{
		get
		{
			return ExposedVariables.msg04QuestionOneCorrect;
		}
		set
		{
			ExposedVariables.msg04QuestionOneCorrect = value;
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

	public uScript_AddMessage.MessageData msg19QuestionSixCorrect
	{
		get
		{
			return ExposedVariables.msg19QuestionSixCorrect;
		}
		set
		{
			ExposedVariables.msg19QuestionSixCorrect = value;
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

	public uScript_AddMessage.MessageData msg18QuestionSix
	{
		get
		{
			return ExposedVariables.msg18QuestionSix;
		}
		set
		{
			ExposedVariables.msg18QuestionSix = value;
		}
	}

	public uScript_AddMessage.MessageData msg05QuestionOneWrong
	{
		get
		{
			return ExposedVariables.msg05QuestionOneWrong;
		}
		set
		{
			ExposedVariables.msg05QuestionOneWrong = value;
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

	public uScript_AddMessage.MessageData msg09QuestionThree
	{
		get
		{
			return ExposedVariables.msg09QuestionThree;
		}
		set
		{
			ExposedVariables.msg09QuestionThree = value;
		}
	}

	public uScript_AddMessage.MessageData msg11QuestionThreeWrong
	{
		get
		{
			return ExposedVariables.msg11QuestionThreeWrong;
		}
		set
		{
			ExposedVariables.msg11QuestionThreeWrong = value;
		}
	}

	public uScript_AddMessage.MessageData msg10QuestionThreeCorrect
	{
		get
		{
			return ExposedVariables.msg10QuestionThreeCorrect;
		}
		set
		{
			ExposedVariables.msg10QuestionThreeCorrect = value;
		}
	}

	public uScript_AddMessage.MessageData msg12QuestionFour
	{
		get
		{
			return ExposedVariables.msg12QuestionFour;
		}
		set
		{
			ExposedVariables.msg12QuestionFour = value;
		}
	}

	public uScript_AddMessage.MessageData msg13QuestionFourCorrect
	{
		get
		{
			return ExposedVariables.msg13QuestionFourCorrect;
		}
		set
		{
			ExposedVariables.msg13QuestionFourCorrect = value;
		}
	}

	public uScript_AddMessage.MessageData msg14QuestionFourWrong
	{
		get
		{
			return ExposedVariables.msg14QuestionFourWrong;
		}
		set
		{
			ExposedVariables.msg14QuestionFourWrong = value;
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

	public uScript_AddMessage.MessageData msg15QuestionFive
	{
		get
		{
			return ExposedVariables.msg15QuestionFive;
		}
		set
		{
			ExposedVariables.msg15QuestionFive = value;
		}
	}

	public uScript_AddMessage.MessageData msg16QuestionFiveCorrect
	{
		get
		{
			return ExposedVariables.msg16QuestionFiveCorrect;
		}
		set
		{
			ExposedVariables.msg16QuestionFiveCorrect = value;
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

	public uScript_AddMessage.MessageData msg17QuestionFiveWrong
	{
		get
		{
			return ExposedVariables.msg17QuestionFiveWrong;
		}
		set
		{
			ExposedVariables.msg17QuestionFiveWrong = value;
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

	public bool Q4Button1
	{
		get
		{
			return ExposedVariables.Q4Button1;
		}
		set
		{
			ExposedVariables.Q4Button1 = value;
		}
	}

	public bool Q4Button2
	{
		get
		{
			return ExposedVariables.Q4Button2;
		}
		set
		{
			ExposedVariables.Q4Button2 = value;
		}
	}

	public bool Q4Button3
	{
		get
		{
			return ExposedVariables.Q4Button3;
		}
		set
		{
			ExposedVariables.Q4Button3 = value;
		}
	}

	public bool Q4Button4
	{
		get
		{
			return ExposedVariables.Q4Button4;
		}
		set
		{
			ExposedVariables.Q4Button4 = value;
		}
	}

	public bool Q5Button1
	{
		get
		{
			return ExposedVariables.Q5Button1;
		}
		set
		{
			ExposedVariables.Q5Button1 = value;
		}
	}

	public bool Q5Button2
	{
		get
		{
			return ExposedVariables.Q5Button2;
		}
		set
		{
			ExposedVariables.Q5Button2 = value;
		}
	}

	public bool Q5Button3
	{
		get
		{
			return ExposedVariables.Q5Button3;
		}
		set
		{
			ExposedVariables.Q5Button3 = value;
		}
	}

	public bool Q5Button4
	{
		get
		{
			return ExposedVariables.Q5Button4;
		}
		set
		{
			ExposedVariables.Q5Button4 = value;
		}
	}

	public bool Q6Button1
	{
		get
		{
			return ExposedVariables.Q6Button1;
		}
		set
		{
			ExposedVariables.Q6Button1 = value;
		}
	}

	public bool Q6Button2
	{
		get
		{
			return ExposedVariables.Q6Button2;
		}
		set
		{
			ExposedVariables.Q6Button2 = value;
		}
	}

	public bool Q6Button3
	{
		get
		{
			return ExposedVariables.Q6Button3;
		}
		set
		{
			ExposedVariables.Q6Button3 = value;
		}
	}

	public bool Q6Button4
	{
		get
		{
			return ExposedVariables.Q6Button4;
		}
		set
		{
			ExposedVariables.Q6Button4 = value;
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
