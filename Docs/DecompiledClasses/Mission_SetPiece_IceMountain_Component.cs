using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_IceMountain")]
public class Mission_SetPiece_IceMountain_Component : uScriptCode
{
	public Mission_SetPiece_IceMountain ExposedVariables = new Mission_SetPiece_IceMountain();

	public uScript_PlayDialogue.Dialogue FailDialogue
	{
		get
		{
			return ExposedVariables.FailDialogue;
		}
		set
		{
			ExposedVariables.FailDialogue = value;
		}
	}

	public uScript_PlayDialogue.Dialogue MissionCompleteDialogue
	{
		get
		{
			return ExposedVariables.MissionCompleteDialogue;
		}
		set
		{
			ExposedVariables.MissionCompleteDialogue = value;
		}
	}

	public string Level01Area
	{
		get
		{
			return ExposedVariables.Level01Area;
		}
		set
		{
			ExposedVariables.Level01Area = value;
		}
	}

	public string FinishTriggerArea
	{
		get
		{
			return ExposedVariables.FinishTriggerArea;
		}
		set
		{
			ExposedVariables.FinishTriggerArea = value;
		}
	}

	public string EnemySpawnTrigger01
	{
		get
		{
			return ExposedVariables.EnemySpawnTrigger01;
		}
		set
		{
			ExposedVariables.EnemySpawnTrigger01 = value;
		}
	}

	public string EnemySpawnTrigger02
	{
		get
		{
			return ExposedVariables.EnemySpawnTrigger02;
		}
		set
		{
			ExposedVariables.EnemySpawnTrigger02 = value;
		}
	}

	public uScript_PlayDialogue.Dialogue RaceStartDialogue
	{
		get
		{
			return ExposedVariables.RaceStartDialogue;
		}
		set
		{
			ExposedVariables.RaceStartDialogue = value;
		}
	}

	public string MissionAreaTrigger
	{
		get
		{
			return ExposedVariables.MissionAreaTrigger;
		}
		set
		{
			ExposedVariables.MissionAreaTrigger = value;
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

	public string Checkpoint01
	{
		get
		{
			return ExposedVariables.Checkpoint01;
		}
		set
		{
			ExposedVariables.Checkpoint01 = value;
		}
	}

	public string Level02Area
	{
		get
		{
			return ExposedVariables.Level02Area;
		}
		set
		{
			ExposedVariables.Level02Area = value;
		}
	}

	public string Level03Area
	{
		get
		{
			return ExposedVariables.Level03Area;
		}
		set
		{
			ExposedVariables.Level03Area = value;
		}
	}

	public string Checkpoint02
	{
		get
		{
			return ExposedVariables.Checkpoint02;
		}
		set
		{
			ExposedVariables.Checkpoint02 = value;
		}
	}

	public string Checkpoint03
	{
		get
		{
			return ExposedVariables.Checkpoint03;
		}
		set
		{
			ExposedVariables.Checkpoint03 = value;
		}
	}

	public string OutsideArea01
	{
		get
		{
			return ExposedVariables.OutsideArea01;
		}
		set
		{
			ExposedVariables.OutsideArea01 = value;
		}
	}

	public string OutsideArea02
	{
		get
		{
			return ExposedVariables.OutsideArea02;
		}
		set
		{
			ExposedVariables.OutsideArea02 = value;
		}
	}

	public string OutsideArea03
	{
		get
		{
			return ExposedVariables.OutsideArea03;
		}
		set
		{
			ExposedVariables.OutsideArea03 = value;
		}
	}

	public string OutsideArea04
	{
		get
		{
			return ExposedVariables.OutsideArea04;
		}
		set
		{
			ExposedVariables.OutsideArea04 = value;
		}
	}

	public string OutsideArea05
	{
		get
		{
			return ExposedVariables.OutsideArea05;
		}
		set
		{
			ExposedVariables.OutsideArea05 = value;
		}
	}

	public string OutsideArea06
	{
		get
		{
			return ExposedVariables.OutsideArea06;
		}
		set
		{
			ExposedVariables.OutsideArea06 = value;
		}
	}

	public string OutsideArea07
	{
		get
		{
			return ExposedVariables.OutsideArea07;
		}
		set
		{
			ExposedVariables.OutsideArea07 = value;
		}
	}

	public uScript_PlayDialogue.Dialogue OutOfBounds
	{
		get
		{
			return ExposedVariables.OutOfBounds;
		}
		set
		{
			ExposedVariables.OutOfBounds = value;
		}
	}

	public string OutsideArea02Half01
	{
		get
		{
			return ExposedVariables.OutsideArea02Half01;
		}
		set
		{
			ExposedVariables.OutsideArea02Half01 = value;
		}
	}

	public string OutsideArea02Half02
	{
		get
		{
			return ExposedVariables.OutsideArea02Half02;
		}
		set
		{
			ExposedVariables.OutsideArea02Half02 = value;
		}
	}

	public string OutsideArea02Half03
	{
		get
		{
			return ExposedVariables.OutsideArea02Half03;
		}
		set
		{
			ExposedVariables.OutsideArea02Half03 = value;
		}
	}

	public string EnemySpawnTrigger05
	{
		get
		{
			return ExposedVariables.EnemySpawnTrigger05;
		}
		set
		{
			ExposedVariables.EnemySpawnTrigger05 = value;
		}
	}

	public string StartLineTrigger
	{
		get
		{
			return ExposedVariables.StartLineTrigger;
		}
		set
		{
			ExposedVariables.StartLineTrigger = value;
		}
	}

	public uScript_PlayDialogue.Dialogue ForbiddenBlockDialogue
	{
		get
		{
			return ExposedVariables.ForbiddenBlockDialogue;
		}
		set
		{
			ExposedVariables.ForbiddenBlockDialogue = value;
		}
	}

	public LocalisedString[] msgForbiddenBlock
	{
		get
		{
			return ExposedVariables.msgForbiddenBlock;
		}
		set
		{
			ExposedVariables.msgForbiddenBlock = value;
		}
	}

	public string ForbiddenBlockTag
	{
		get
		{
			return ExposedVariables.ForbiddenBlockTag;
		}
		set
		{
			ExposedVariables.ForbiddenBlockTag = value;
		}
	}

	public LocalisedString[] msgForbiddenBlockRunning
	{
		get
		{
			return ExposedVariables.msgForbiddenBlockRunning;
		}
		set
		{
			ExposedVariables.msgForbiddenBlockRunning = value;
		}
	}

	public string IntroTrigger
	{
		get
		{
			return ExposedVariables.IntroTrigger;
		}
		set
		{
			ExposedVariables.IntroTrigger = value;
		}
	}

	public string ObjPos03
	{
		get
		{
			return ExposedVariables.ObjPos03;
		}
		set
		{
			ExposedVariables.ObjPos03 = value;
		}
	}

	public string ObjPos02
	{
		get
		{
			return ExposedVariables.ObjPos02;
		}
		set
		{
			ExposedVariables.ObjPos02 = value;
		}
	}

	public string ObjPos04
	{
		get
		{
			return ExposedVariables.ObjPos04;
		}
		set
		{
			ExposedVariables.ObjPos04 = value;
		}
	}

	public string ObjPos01
	{
		get
		{
			return ExposedVariables.ObjPos01;
		}
		set
		{
			ExposedVariables.ObjPos01 = value;
		}
	}

	public uScript_PlayDialogue.Dialogue CheckpointMissed01
	{
		get
		{
			return ExposedVariables.CheckpointMissed01;
		}
		set
		{
			ExposedVariables.CheckpointMissed01 = value;
		}
	}

	public string OutsideArea02Half04
	{
		get
		{
			return ExposedVariables.OutsideArea02Half04;
		}
		set
		{
			ExposedVariables.OutsideArea02Half04 = value;
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

	public uScript_PlayDialogue.Dialogue FailDeadDialogue
	{
		get
		{
			return ExposedVariables.FailDeadDialogue;
		}
		set
		{
			ExposedVariables.FailDeadDialogue = value;
		}
	}

	public ExternalBehaviorTree AIFlyTree
	{
		get
		{
			return ExposedVariables.AIFlyTree;
		}
		set
		{
			ExposedVariables.AIFlyTree = value;
		}
	}

	public Transform RemovalParticles
	{
		get
		{
			return ExposedVariables.RemovalParticles;
		}
		set
		{
			ExposedVariables.RemovalParticles = value;
		}
	}

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

	public SpawnTechData[] NPCTechEnd01Data
	{
		get
		{
			return ExposedVariables.NPCTechEnd01Data;
		}
		set
		{
			ExposedVariables.NPCTechEnd01Data = value;
		}
	}

	public string EnemySpawnTrigger03
	{
		get
		{
			return ExposedVariables.EnemySpawnTrigger03;
		}
		set
		{
			ExposedVariables.EnemySpawnTrigger03 = value;
		}
	}

	public SpawnTechData[] EnemyWaveData04
	{
		get
		{
			return ExposedVariables.EnemyWaveData04;
		}
		set
		{
			ExposedVariables.EnemyWaveData04 = value;
		}
	}

	public SpawnTechData[] EnemyWaveData05
	{
		get
		{
			return ExposedVariables.EnemyWaveData05;
		}
		set
		{
			ExposedVariables.EnemyWaveData05 = value;
		}
	}

	public SpawnTechData[] EnemyWaveData01
	{
		get
		{
			return ExposedVariables.EnemyWaveData01;
		}
		set
		{
			ExposedVariables.EnemyWaveData01 = value;
		}
	}

	public SpawnTechData[] EnemyWaveData02
	{
		get
		{
			return ExposedVariables.EnemyWaveData02;
		}
		set
		{
			ExposedVariables.EnemyWaveData02 = value;
		}
	}

	public string IntroTag
	{
		get
		{
			return ExposedVariables.IntroTag;
		}
		set
		{
			ExposedVariables.IntroTag = value;
		}
	}

	public LocalisedString[] msgIntroInterrupted
	{
		get
		{
			return ExposedVariables.msgIntroInterrupted;
		}
		set
		{
			ExposedVariables.msgIntroInterrupted = value;
		}
	}

	public ManOnScreenMessages.Speaker NPCSpeaker
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

	public string OutsideIntroTrigger
	{
		get
		{
			return ExposedVariables.OutsideIntroTrigger;
		}
		set
		{
			ExposedVariables.OutsideIntroTrigger = value;
		}
	}

	public SpawnTechData[] EnemyWaveData03
	{
		get
		{
			return ExposedVariables.EnemyWaveData03;
		}
		set
		{
			ExposedVariables.EnemyWaveData03 = value;
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
