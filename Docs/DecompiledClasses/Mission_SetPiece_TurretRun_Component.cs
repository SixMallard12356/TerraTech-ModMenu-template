using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_TurretRun")]
public class Mission_SetPiece_TurretRun_Component : uScriptCode
{
	public Mission_SetPiece_TurretRun ExposedVariables = new Mission_SetPiece_TurretRun();

	public SpawnTechData[] NPCTechData
	{
		get
		{
			return ExposedVariables.NPCTechData;
		}
		set
		{
			ExposedVariables.NPCTechData = value;
		}
	}

	public string O1TriggerArea
	{
		get
		{
			return ExposedVariables.O1TriggerArea;
		}
		set
		{
			ExposedVariables.O1TriggerArea = value;
		}
	}

	public string O4TriggerArea
	{
		get
		{
			return ExposedVariables.O4TriggerArea;
		}
		set
		{
			ExposedVariables.O4TriggerArea = value;
		}
	}

	public string O5TriggerArea
	{
		get
		{
			return ExposedVariables.O5TriggerArea;
		}
		set
		{
			ExposedVariables.O5TriggerArea = value;
		}
	}

	public string O6TriggerArea
	{
		get
		{
			return ExposedVariables.O6TriggerArea;
		}
		set
		{
			ExposedVariables.O6TriggerArea = value;
		}
	}

	public string O7TriggerArea
	{
		get
		{
			return ExposedVariables.O7TriggerArea;
		}
		set
		{
			ExposedVariables.O7TriggerArea = value;
		}
	}

	public string ExitArea
	{
		get
		{
			return ExposedVariables.ExitArea;
		}
		set
		{
			ExposedVariables.ExitArea = value;
		}
	}

	public string StartArea
	{
		get
		{
			return ExposedVariables.StartArea;
		}
		set
		{
			ExposedVariables.StartArea = value;
		}
	}

	public string EndArea
	{
		get
		{
			return ExposedVariables.EndArea;
		}
		set
		{
			ExposedVariables.EndArea = value;
		}
	}

	public uScript_PlayDialogue.Dialogue IntroDialogue
	{
		get
		{
			return ExposedVariables.IntroDialogue;
		}
		set
		{
			ExposedVariables.IntroDialogue = value;
		}
	}

	public uScript_PlayDialogue.Dialogue StartRaceDialogue
	{
		get
		{
			return ExposedVariables.StartRaceDialogue;
		}
		set
		{
			ExposedVariables.StartRaceDialogue = value;
		}
	}

	public uScript_PlayDialogue.Dialogue GoStartRaceDialogue
	{
		get
		{
			return ExposedVariables.GoStartRaceDialogue;
		}
		set
		{
			ExposedVariables.GoStartRaceDialogue = value;
		}
	}

	public uScript_PlayDialogue.Dialogue TimeUpDialogue
	{
		get
		{
			return ExposedVariables.TimeUpDialogue;
		}
		set
		{
			ExposedVariables.TimeUpDialogue = value;
		}
	}

	public float Timer
	{
		get
		{
			return ExposedVariables.Timer;
		}
		set
		{
			ExposedVariables.Timer = value;
		}
	}

	public SpawnTechData[] NPCEndTechData
	{
		get
		{
			return ExposedVariables.NPCEndTechData;
		}
		set
		{
			ExposedVariables.NPCEndTechData = value;
		}
	}

	public string ExitAreaChecks
	{
		get
		{
			return ExposedVariables.ExitAreaChecks;
		}
		set
		{
			ExposedVariables.ExitAreaChecks = value;
		}
	}

	public uScript_PlayDialogue.Dialogue RaceFinishedDialogue01
	{
		get
		{
			return ExposedVariables.RaceFinishedDialogue01;
		}
		set
		{
			ExposedVariables.RaceFinishedDialogue01 = value;
		}
	}

	public uScript_PlayDialogue.Dialogue RaceFinishedDialogue02
	{
		get
		{
			return ExposedVariables.RaceFinishedDialogue02;
		}
		set
		{
			ExposedVariables.RaceFinishedDialogue02 = value;
		}
	}

	public string RespawnArea
	{
		get
		{
			return ExposedVariables.RespawnArea;
		}
		set
		{
			ExposedVariables.RespawnArea = value;
		}
	}

	public Transform NPCVanish
	{
		get
		{
			return ExposedVariables.NPCVanish;
		}
		set
		{
			ExposedVariables.NPCVanish = value;
		}
	}

	public ExternalBehaviorTree NPCFlyAI
	{
		get
		{
			return ExposedVariables.NPCFlyAI;
		}
		set
		{
			ExposedVariables.NPCFlyAI = value;
		}
	}

	public string O7FlamerTriggerArea
	{
		get
		{
			return ExposedVariables.O7FlamerTriggerArea;
		}
		set
		{
			ExposedVariables.O7FlamerTriggerArea = value;
		}
	}

	public uScript_PlayDialogue.Dialogue ReadyGoDialogue
	{
		get
		{
			return ExposedVariables.ReadyGoDialogue;
		}
		set
		{
			ExposedVariables.ReadyGoDialogue = value;
		}
	}

	public uScript_PlayDialogue.Dialogue IntroDialogueMP
	{
		get
		{
			return ExposedVariables.IntroDialogueMP;
		}
		set
		{
			ExposedVariables.IntroDialogueMP = value;
		}
	}

	public uScript_PlayDialogue.Dialogue GoStartRaceDialogueMP
	{
		get
		{
			return ExposedVariables.GoStartRaceDialogueMP;
		}
		set
		{
			ExposedVariables.GoStartRaceDialogueMP = value;
		}
	}

	public uScript_PlayDialogue.Dialogue TimeUpDialogueMP
	{
		get
		{
			return ExposedVariables.TimeUpDialogueMP;
		}
		set
		{
			ExposedVariables.TimeUpDialogueMP = value;
		}
	}

	public uScript_PlayDialogue.Dialogue RaceFinishedDialogue01MP
	{
		get
		{
			return ExposedVariables.RaceFinishedDialogue01MP;
		}
		set
		{
			ExposedVariables.RaceFinishedDialogue01MP = value;
		}
	}

	public uScript_PlayDialogue.Dialogue RaceFinishedDialogue02MP
	{
		get
		{
			return ExposedVariables.RaceFinishedDialogue02MP;
		}
		set
		{
			ExposedVariables.RaceFinishedDialogue02MP = value;
		}
	}

	public string StartGatesPos
	{
		get
		{
			return ExposedVariables.StartGatesPos;
		}
		set
		{
			ExposedVariables.StartGatesPos = value;
		}
	}

	public string NPCOBJPos
	{
		get
		{
			return ExposedVariables.NPCOBJPos;
		}
		set
		{
			ExposedVariables.NPCOBJPos = value;
		}
	}

	public string O3TriggerAreaOBJ
	{
		get
		{
			return ExposedVariables.O3TriggerAreaOBJ;
		}
		set
		{
			ExposedVariables.O3TriggerAreaOBJ = value;
		}
	}

	public string O2TriggerArea
	{
		get
		{
			return ExposedVariables.O2TriggerArea;
		}
		set
		{
			ExposedVariables.O2TriggerArea = value;
		}
	}

	public uScript_PlayDialogue.Dialogue OutsideAreaDialogueMP
	{
		get
		{
			return ExposedVariables.OutsideAreaDialogueMP;
		}
		set
		{
			ExposedVariables.OutsideAreaDialogueMP = value;
		}
	}

	public uScript_PlayDialogue.Dialogue StopBackwardsDialogueMP
	{
		get
		{
			return ExposedVariables.StopBackwardsDialogueMP;
		}
		set
		{
			ExposedVariables.StopBackwardsDialogueMP = value;
		}
	}

	public uScript_PlayDialogue.Dialogue OutsideAreaDialogue
	{
		get
		{
			return ExposedVariables.OutsideAreaDialogue;
		}
		set
		{
			ExposedVariables.OutsideAreaDialogue = value;
		}
	}

	public uScript_PlayDialogue.Dialogue StopBackwardsDialogue
	{
		get
		{
			return ExposedVariables.StopBackwardsDialogue;
		}
		set
		{
			ExposedVariables.StopBackwardsDialogue = value;
		}
	}

	public uScript_PlayDialogue.Dialogue RespawnAreaDialogueMP
	{
		get
		{
			return ExposedVariables.RespawnAreaDialogueMP;
		}
		set
		{
			ExposedVariables.RespawnAreaDialogueMP = value;
		}
	}

	public uScript_PlayDialogue.Dialogue RespawnAreaDialogue
	{
		get
		{
			return ExposedVariables.RespawnAreaDialogue;
		}
		set
		{
			ExposedVariables.RespawnAreaDialogue = value;
		}
	}

	public string MissionGates
	{
		get
		{
			return ExposedVariables.MissionGates;
		}
		set
		{
			ExposedVariables.MissionGates = value;
		}
	}

	public string NPCTriggerPos
	{
		get
		{
			return ExposedVariables.NPCTriggerPos;
		}
		set
		{
			ExposedVariables.NPCTriggerPos = value;
		}
	}

	public string NPCLargeTriggerPos
	{
		get
		{
			return ExposedVariables.NPCLargeTriggerPos;
		}
		set
		{
			ExposedVariables.NPCLargeTriggerPos = value;
		}
	}

	public string O2TriggerAreaSpawn
	{
		get
		{
			return ExposedVariables.O2TriggerAreaSpawn;
		}
		set
		{
			ExposedVariables.O2TriggerAreaSpawn = value;
		}
	}

	public string O4TriggerAreaSpawn
	{
		get
		{
			return ExposedVariables.O4TriggerAreaSpawn;
		}
		set
		{
			ExposedVariables.O4TriggerAreaSpawn = value;
		}
	}

	public string O5TriggerAreaSpawn
	{
		get
		{
			return ExposedVariables.O5TriggerAreaSpawn;
		}
		set
		{
			ExposedVariables.O5TriggerAreaSpawn = value;
		}
	}

	public string O6TriggerAreaSpawn
	{
		get
		{
			return ExposedVariables.O6TriggerAreaSpawn;
		}
		set
		{
			ExposedVariables.O6TriggerAreaSpawn = value;
		}
	}

	public uScript_PlayDialogue.Dialogue TooEarlyDialogue
	{
		get
		{
			return ExposedVariables.TooEarlyDialogue;
		}
		set
		{
			ExposedVariables.TooEarlyDialogue = value;
		}
	}

	public string RaceStartTag
	{
		get
		{
			return ExposedVariables.RaceStartTag;
		}
		set
		{
			ExposedVariables.RaceStartTag = value;
		}
	}

	public string ExitAreaChecksOutside
	{
		get
		{
			return ExposedVariables.ExitAreaChecksOutside;
		}
		set
		{
			ExposedVariables.ExitAreaChecksOutside = value;
		}
	}

	public SpawnTechData[] O1_TurretAGTechDataSPLIT03
	{
		get
		{
			return ExposedVariables.O1_TurretAGTechDataSPLIT03;
		}
		set
		{
			ExposedVariables.O1_TurretAGTechDataSPLIT03 = value;
		}
	}

	public SpawnTechData[] O1_TurretAGTechData
	{
		get
		{
			return ExposedVariables.O1_TurretAGTechData;
		}
		set
		{
			ExposedVariables.O1_TurretAGTechData = value;
		}
	}

	public SpawnTechData[] O1_TurretAGTechDataSPLIT02
	{
		get
		{
			return ExposedVariables.O1_TurretAGTechDataSPLIT02;
		}
		set
		{
			ExposedVariables.O1_TurretAGTechDataSPLIT02 = value;
		}
	}

	public SpawnTechData[] O7_FireTechData
	{
		get
		{
			return ExposedVariables.O7_FireTechData;
		}
		set
		{
			ExposedVariables.O7_FireTechData = value;
		}
	}

	public SpawnTechData[] O7_TurretBossTechData
	{
		get
		{
			return ExposedVariables.O7_TurretBossTechData;
		}
		set
		{
			ExposedVariables.O7_TurretBossTechData = value;
		}
	}

	public string O3TriggerArea
	{
		get
		{
			return ExposedVariables.O3TriggerArea;
		}
		set
		{
			ExposedVariables.O3TriggerArea = value;
		}
	}

	public string O4TriggerAreaOBJ
	{
		get
		{
			return ExposedVariables.O4TriggerAreaOBJ;
		}
		set
		{
			ExposedVariables.O4TriggerAreaOBJ = value;
		}
	}

	public string O3TriggerAreaSpawn
	{
		get
		{
			return ExposedVariables.O3TriggerAreaSpawn;
		}
		set
		{
			ExposedVariables.O3TriggerAreaSpawn = value;
		}
	}

	public string O5TriggerAreaOBJ
	{
		get
		{
			return ExposedVariables.O5TriggerAreaOBJ;
		}
		set
		{
			ExposedVariables.O5TriggerAreaOBJ = value;
		}
	}

	public SpawnTechData[] O3_TurretTechData
	{
		get
		{
			return ExposedVariables.O3_TurretTechData;
		}
		set
		{
			ExposedVariables.O3_TurretTechData = value;
		}
	}

	public SpawnTechData[] O4_TurretTechData
	{
		get
		{
			return ExposedVariables.O4_TurretTechData;
		}
		set
		{
			ExposedVariables.O4_TurretTechData = value;
		}
	}

	public string O6TriggerAreaOBJ
	{
		get
		{
			return ExposedVariables.O6TriggerAreaOBJ;
		}
		set
		{
			ExposedVariables.O6TriggerAreaOBJ = value;
		}
	}

	public SpawnTechData[] O5_SpinnerTechData
	{
		get
		{
			return ExposedVariables.O5_SpinnerTechData;
		}
		set
		{
			ExposedVariables.O5_SpinnerTechData = value;
		}
	}

	public string O7TriggerAreaOBJ
	{
		get
		{
			return ExposedVariables.O7TriggerAreaOBJ;
		}
		set
		{
			ExposedVariables.O7TriggerAreaOBJ = value;
		}
	}

	public SpawnTechData[] O5_SpinnerTechDataSPLIT02
	{
		get
		{
			return ExposedVariables.O5_SpinnerTechDataSPLIT02;
		}
		set
		{
			ExposedVariables.O5_SpinnerTechDataSPLIT02 = value;
		}
	}

	public SpawnTechData[] O5_SpinnerTechDataSPLIT03
	{
		get
		{
			return ExposedVariables.O5_SpinnerTechDataSPLIT03;
		}
		set
		{
			ExposedVariables.O5_SpinnerTechDataSPLIT03 = value;
		}
	}

	public string EndOBJPos
	{
		get
		{
			return ExposedVariables.EndOBJPos;
		}
		set
		{
			ExposedVariables.EndOBJPos = value;
		}
	}

	public SpawnTechData[] O6_TurretTechData01
	{
		get
		{
			return ExposedVariables.O6_TurretTechData01;
		}
		set
		{
			ExposedVariables.O6_TurretTechData01 = value;
		}
	}

	public BlockTypes RewardBlock01
	{
		get
		{
			return ExposedVariables.RewardBlock01;
		}
		set
		{
			ExposedVariables.RewardBlock01 = value;
		}
	}

	public string RewardBlockName01
	{
		get
		{
			return ExposedVariables.RewardBlockName01;
		}
		set
		{
			ExposedVariables.RewardBlockName01 = value;
		}
	}

	public string RewardBlockName02
	{
		get
		{
			return ExposedVariables.RewardBlockName02;
		}
		set
		{
			ExposedVariables.RewardBlockName02 = value;
		}
	}

	public BlockTypes RewardBlock02
	{
		get
		{
			return ExposedVariables.RewardBlock02;
		}
		set
		{
			ExposedVariables.RewardBlock02 = value;
		}
	}

	public string O2_2TriggerAreaSpawn
	{
		get
		{
			return ExposedVariables.O2_2TriggerAreaSpawn;
		}
		set
		{
			ExposedVariables.O2_2TriggerAreaSpawn = value;
		}
	}

	public SpawnTechData[] O3_WallBlockTechDataSPLIT02
	{
		get
		{
			return ExposedVariables.O3_WallBlockTechDataSPLIT02;
		}
		set
		{
			ExposedVariables.O3_WallBlockTechDataSPLIT02 = value;
		}
	}

	public SpawnTechData[] O3_WallBlockTechData
	{
		get
		{
			return ExposedVariables.O3_WallBlockTechData;
		}
		set
		{
			ExposedVariables.O3_WallBlockTechData = value;
		}
	}

	public string O3_2TriggerAreaSpawn
	{
		get
		{
			return ExposedVariables.O3_2TriggerAreaSpawn;
		}
		set
		{
			ExposedVariables.O3_2TriggerAreaSpawn = value;
		}
	}

	public SpawnTechData[] O4_TurretTechDataSPLIT02
	{
		get
		{
			return ExposedVariables.O4_TurretTechDataSPLIT02;
		}
		set
		{
			ExposedVariables.O4_TurretTechDataSPLIT02 = value;
		}
	}

	public string O4_2TriggerAreaSpawn
	{
		get
		{
			return ExposedVariables.O4_2TriggerAreaSpawn;
		}
		set
		{
			ExposedVariables.O4_2TriggerAreaSpawn = value;
		}
	}

	public SpawnTechData[] O5_SpinnerTechDataSPLIT04
	{
		get
		{
			return ExposedVariables.O5_SpinnerTechDataSPLIT04;
		}
		set
		{
			ExposedVariables.O5_SpinnerTechDataSPLIT04 = value;
		}
	}

	public SpawnTechData[] O6_WallBlockTechData
	{
		get
		{
			return ExposedVariables.O6_WallBlockTechData;
		}
		set
		{
			ExposedVariables.O6_WallBlockTechData = value;
		}
	}

	public SpawnTechData[] O6_TurretTechData02
	{
		get
		{
			return ExposedVariables.O6_TurretTechData02;
		}
		set
		{
			ExposedVariables.O6_TurretTechData02 = value;
		}
	}

	public string O5_2TriggerAreaSpawn
	{
		get
		{
			return ExposedVariables.O5_2TriggerAreaSpawn;
		}
		set
		{
			ExposedVariables.O5_2TriggerAreaSpawn = value;
		}
	}

	public string O6_2TriggerAreaSpawn
	{
		get
		{
			return ExposedVariables.O6_2TriggerAreaSpawn;
		}
		set
		{
			ExposedVariables.O6_2TriggerAreaSpawn = value;
		}
	}

	public SpawnTechData[] O7_TurretAGTechData
	{
		get
		{
			return ExposedVariables.O7_TurretAGTechData;
		}
		set
		{
			ExposedVariables.O7_TurretAGTechData = value;
		}
	}

	public SpawnTechData[] O7_TurretLaserTechData02
	{
		get
		{
			return ExposedVariables.O7_TurretLaserTechData02;
		}
		set
		{
			ExposedVariables.O7_TurretLaserTechData02 = value;
		}
	}

	public string O7_2FlamerTriggerArea
	{
		get
		{
			return ExposedVariables.O7_2FlamerTriggerArea;
		}
		set
		{
			ExposedVariables.O7_2FlamerTriggerArea = value;
		}
	}

	public SpawnTechData[] O7_TurretLaserTechData01
	{
		get
		{
			return ExposedVariables.O7_TurretLaserTechData01;
		}
		set
		{
			ExposedVariables.O7_TurretLaserTechData01 = value;
		}
	}

	public SpawnTechData[] O1_WallBlockTechData
	{
		get
		{
			return ExposedVariables.O1_WallBlockTechData;
		}
		set
		{
			ExposedVariables.O1_WallBlockTechData = value;
		}
	}

	public SpawnTechData[] O2_TurretTechDataSPLIT02
	{
		get
		{
			return ExposedVariables.O2_TurretTechDataSPLIT02;
		}
		set
		{
			ExposedVariables.O2_TurretTechDataSPLIT02 = value;
		}
	}

	public SpawnTechData[] O2_TurretTechData
	{
		get
		{
			return ExposedVariables.O2_TurretTechData;
		}
		set
		{
			ExposedVariables.O2_TurretTechData = value;
		}
	}

	public SpawnTechData[] O2_WallBlockTechDataSPLIT02
	{
		get
		{
			return ExposedVariables.O2_WallBlockTechDataSPLIT02;
		}
		set
		{
			ExposedVariables.O2_WallBlockTechDataSPLIT02 = value;
		}
	}

	public SpawnTechData[] O2_WallBlockTechData
	{
		get
		{
			return ExposedVariables.O2_WallBlockTechData;
		}
		set
		{
			ExposedVariables.O2_WallBlockTechData = value;
		}
	}

	public string O4_3TriggerAreaSpawn
	{
		get
		{
			return ExposedVariables.O4_3TriggerAreaSpawn;
		}
		set
		{
			ExposedVariables.O4_3TriggerAreaSpawn = value;
		}
	}

	public SpawnTechData[] O5_SpinnerTechDataSPLIT06
	{
		get
		{
			return ExposedVariables.O5_SpinnerTechDataSPLIT06;
		}
		set
		{
			ExposedVariables.O5_SpinnerTechDataSPLIT06 = value;
		}
	}

	public SpawnTechData[] O5_SpinnerTechDataSPLIT05
	{
		get
		{
			return ExposedVariables.O5_SpinnerTechDataSPLIT05;
		}
		set
		{
			ExposedVariables.O5_SpinnerTechDataSPLIT05 = value;
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
