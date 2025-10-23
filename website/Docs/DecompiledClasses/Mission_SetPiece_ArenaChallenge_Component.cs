using BehaviorDesigner.Runtime;
using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_ArenaChallenge")]
public class Mission_SetPiece_ArenaChallenge_Component : uScriptCode
{
	public Mission_SetPiece_ArenaChallenge ExposedVariables = new Mission_SetPiece_ArenaChallenge();

	public string EntranceTriggerArea
	{
		get
		{
			return ExposedVariables.EntranceTriggerArea;
		}
		set
		{
			ExposedVariables.EntranceTriggerArea = value;
		}
	}

	public string NPCTriggerArea
	{
		get
		{
			return ExposedVariables.NPCTriggerArea;
		}
		set
		{
			ExposedVariables.NPCTriggerArea = value;
		}
	}

	public string RespawnTriggerArea
	{
		get
		{
			return ExposedVariables.RespawnTriggerArea;
		}
		set
		{
			ExposedVariables.RespawnTriggerArea = value;
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

	public uScript_PlayDialogue.Dialogue R1IntroDialogue
	{
		get
		{
			return ExposedVariables.R1IntroDialogue;
		}
		set
		{
			ExposedVariables.R1IntroDialogue = value;
		}
	}

	public BlockTypes S1MainTechShieldBlockData
	{
		get
		{
			return ExposedVariables.S1MainTechShieldBlockData;
		}
		set
		{
			ExposedVariables.S1MainTechShieldBlockData = value;
		}
	}

	public string NPCTriggerAreaOuter
	{
		get
		{
			return ExposedVariables.NPCTriggerAreaOuter;
		}
		set
		{
			ExposedVariables.NPCTriggerAreaOuter = value;
		}
	}

	public string EarlyTag
	{
		get
		{
			return ExposedVariables.EarlyTag;
		}
		set
		{
			ExposedVariables.EarlyTag = value;
		}
	}

	public SpawnTechData[] GuardianTechData
	{
		get
		{
			return ExposedVariables.GuardianTechData;
		}
		set
		{
			ExposedVariables.GuardianTechData = value;
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

	public SpawnTechData[] R1MainTechData
	{
		get
		{
			return ExposedVariables.R1MainTechData;
		}
		set
		{
			ExposedVariables.R1MainTechData = value;
		}
	}

	public SpawnTechData[] TurretTechData
	{
		get
		{
			return ExposedVariables.TurretTechData;
		}
		set
		{
			ExposedVariables.TurretTechData = value;
		}
	}

	public SpawnTechData R2Child03TechData
	{
		get
		{
			return ExposedVariables.R2Child03TechData;
		}
		set
		{
			ExposedVariables.R2Child03TechData = value;
		}
	}

	public SpawnTechData R2Child01TechData
	{
		get
		{
			return ExposedVariables.R2Child01TechData;
		}
		set
		{
			ExposedVariables.R2Child01TechData = value;
		}
	}

	public SpawnTechData R2Child02TechData
	{
		get
		{
			return ExposedVariables.R2Child02TechData;
		}
		set
		{
			ExposedVariables.R2Child02TechData = value;
		}
	}

	public string ArenaTriggerArea
	{
		get
		{
			return ExposedVariables.ArenaTriggerArea;
		}
		set
		{
			ExposedVariables.ArenaTriggerArea = value;
		}
	}

	public uScript_PlayDialogue.Dialogue R1DeadDialogue
	{
		get
		{
			return ExposedVariables.R1DeadDialogue;
		}
		set
		{
			ExposedVariables.R1DeadDialogue = value;
		}
	}

	public uScript_PlayDialogue.Dialogue ShieldsDownDialogue
	{
		get
		{
			return ExposedVariables.ShieldsDownDialogue;
		}
		set
		{
			ExposedVariables.ShieldsDownDialogue = value;
		}
	}

	public uScript_PlayDialogue.Dialogue VictoryDialogue
	{
		get
		{
			return ExposedVariables.VictoryDialogue;
		}
		set
		{
			ExposedVariables.VictoryDialogue = value;
		}
	}

	public ExternalBehaviorTree FlyAI
	{
		get
		{
			return ExposedVariables.FlyAI;
		}
		set
		{
			ExposedVariables.FlyAI = value;
		}
	}

	public Transform FlyParticle
	{
		get
		{
			return ExposedVariables.FlyParticle;
		}
		set
		{
			ExposedVariables.FlyParticle = value;
		}
	}

	public SpawnTechData[] NPC2TechData
	{
		get
		{
			return ExposedVariables.NPC2TechData;
		}
		set
		{
			ExposedVariables.NPC2TechData = value;
		}
	}

	public LocalisedString[] MsgTooEarly
	{
		get
		{
			return ExposedVariables.MsgTooEarly;
		}
		set
		{
			ExposedVariables.MsgTooEarly = value;
		}
	}

	public string MsgTooEarlyTag
	{
		get
		{
			return ExposedVariables.MsgTooEarlyTag;
		}
		set
		{
			ExposedVariables.MsgTooEarlyTag = value;
		}
	}

	public string MsgOutOfArenaTag
	{
		get
		{
			return ExposedVariables.MsgOutOfArenaTag;
		}
		set
		{
			ExposedVariables.MsgOutOfArenaTag = value;
		}
	}

	public LocalisedString[] MsgOutOfArena
	{
		get
		{
			return ExposedVariables.MsgOutOfArena;
		}
		set
		{
			ExposedVariables.MsgOutOfArena = value;
		}
	}

	public string MsgPlayerDeadTag
	{
		get
		{
			return ExposedVariables.MsgPlayerDeadTag;
		}
		set
		{
			ExposedVariables.MsgPlayerDeadTag = value;
		}
	}

	public LocalisedString[] MsgPlayerDead
	{
		get
		{
			return ExposedVariables.MsgPlayerDead;
		}
		set
		{
			ExposedVariables.MsgPlayerDead = value;
		}
	}

	public BlockTypes R1Turret03TechShieldBlockData
	{
		get
		{
			return ExposedVariables.R1Turret03TechShieldBlockData;
		}
		set
		{
			ExposedVariables.R1Turret03TechShieldBlockData = value;
		}
	}

	public BlockTypes R1Turret02TechShieldBlockData
	{
		get
		{
			return ExposedVariables.R1Turret02TechShieldBlockData;
		}
		set
		{
			ExposedVariables.R1Turret02TechShieldBlockData = value;
		}
	}

	public BlockTypes R1Turret04TechShieldBlockData
	{
		get
		{
			return ExposedVariables.R1Turret04TechShieldBlockData;
		}
		set
		{
			ExposedVariables.R1Turret04TechShieldBlockData = value;
		}
	}

	public BlockTypes R1Turret01TechShieldBlockData
	{
		get
		{
			return ExposedVariables.R1Turret01TechShieldBlockData;
		}
		set
		{
			ExposedVariables.R1Turret01TechShieldBlockData = value;
		}
	}

	public string CornerArena02TriggerArea
	{
		get
		{
			return ExposedVariables.CornerArena02TriggerArea;
		}
		set
		{
			ExposedVariables.CornerArena02TriggerArea = value;
		}
	}

	public string CornerArena03TriggerArea
	{
		get
		{
			return ExposedVariables.CornerArena03TriggerArea;
		}
		set
		{
			ExposedVariables.CornerArena03TriggerArea = value;
		}
	}

	public string CornerArena04TriggerArea
	{
		get
		{
			return ExposedVariables.CornerArena04TriggerArea;
		}
		set
		{
			ExposedVariables.CornerArena04TriggerArea = value;
		}
	}

	public string CornerArena01TriggerArea
	{
		get
		{
			return ExposedVariables.CornerArena01TriggerArea;
		}
		set
		{
			ExposedVariables.CornerArena01TriggerArea = value;
		}
	}

	public LocalisedString[] MsgR1IntroInterrupted
	{
		get
		{
			return ExposedVariables.MsgR1IntroInterrupted;
		}
		set
		{
			ExposedVariables.MsgR1IntroInterrupted = value;
		}
	}

	public ManOnScreenMessages.Speaker TechSpeaker
	{
		get
		{
			return ExposedVariables.TechSpeaker;
		}
		set
		{
			ExposedVariables.TechSpeaker = value;
		}
	}

	public uScript_PlayDialogue.Dialogue R1IntroDialogueMP
	{
		get
		{
			return ExposedVariables.R1IntroDialogueMP;
		}
		set
		{
			ExposedVariables.R1IntroDialogueMP = value;
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
