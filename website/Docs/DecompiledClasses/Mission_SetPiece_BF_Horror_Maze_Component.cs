using UnityEngine;

[AddComponentMenu("uScript/Graphs/Mission_SetPiece_BF_Horror_Maze")]
public class Mission_SetPiece_BF_Horror_Maze_Component : uScriptCode
{
	public Mission_SetPiece_BF_Horror_Maze ExposedVariables = new Mission_SetPiece_BF_Horror_Maze();

	public uScript_AddMessage.MessageData msgOutro
	{
		get
		{
			return ExposedVariables.msgOutro;
		}
		set
		{
			ExposedVariables.msgOutro = value;
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

	public string Stage1_Enemy01_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage1_Enemy01_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage1_Enemy01_Spawn_Trig = value;
		}
	}

	public string Stage1_Enemy01_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage1_Enemy01_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage1_Enemy01_PreSpawn_Trig = value;
		}
	}

	public string Stage1_Enemy01_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage1_Enemy01_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage1_Enemy01_Kill_Trig = value;
		}
	}

	public string Stage1_Enemy02_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage1_Enemy02_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage1_Enemy02_Spawn_Trig = value;
		}
	}

	public string Stage1_Enemy02_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage1_Enemy02_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage1_Enemy02_PreSpawn_Trig = value;
		}
	}

	public string Stage1_Enemy02_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage1_Enemy02_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage1_Enemy02_Kill_Trig = value;
		}
	}

	public string Stage1_Enemy03_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage1_Enemy03_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage1_Enemy03_PreSpawn_Trig = value;
		}
	}

	public string Stage1_Enemy03_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage1_Enemy03_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage1_Enemy03_Kill_Trig = value;
		}
	}

	public string Stage1_Enemy03_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage1_Enemy03_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage1_Enemy03_Spawn_Trig = value;
		}
	}

	public string Stage1_Enemy04_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage1_Enemy04_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage1_Enemy04_Spawn_Trig = value;
		}
	}

	public string Stage1_Enemy04_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage1_Enemy04_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage1_Enemy04_PreSpawn_Trig = value;
		}
	}

	public string Stage1_Enemy04_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage1_Enemy04_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage1_Enemy04_Kill_Trig = value;
		}
	}

	public SpawnTechData[] ForcefieldStage3GroupSpawnData
	{
		get
		{
			return ExposedVariables.ForcefieldStage3GroupSpawnData;
		}
		set
		{
			ExposedVariables.ForcefieldStage3GroupSpawnData = value;
		}
	}

	public SpawnTechData[] ForcefieldStage2GroupSpawnData
	{
		get
		{
			return ExposedVariables.ForcefieldStage2GroupSpawnData;
		}
		set
		{
			ExposedVariables.ForcefieldStage2GroupSpawnData = value;
		}
	}

	public string Stage2_Enemy01_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy01_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy01_PreSpawn_Trig = value;
		}
	}

	public string Stage2_Enemy01_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy01_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy01_Kill_Trig = value;
		}
	}

	public string Stage2_Enemy01_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy01_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy01_Spawn_Trig = value;
		}
	}

	public string Stage2_Enemy02_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy02_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy02_PreSpawn_Trig = value;
		}
	}

	public string Stage2_Enemy02_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy02_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy02_Kill_Trig = value;
		}
	}

	public string Stage2_Enemy02_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy02_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy02_Spawn_Trig = value;
		}
	}

	public string Stage2_Enemy03_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy03_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy03_Spawn_Trig = value;
		}
	}

	public string Stage2_Enemy03_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy03_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy03_Kill_Trig = value;
		}
	}

	public string Stage2_Enemy03_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy03_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy03_PreSpawn_Trig = value;
		}
	}

	public string Stage2_Enemy04_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy04_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy04_Kill_Trig = value;
		}
	}

	public string Stage2_Enemy04_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy04_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy04_Spawn_Trig = value;
		}
	}

	public string Stage2_Enemy04_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy04_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy04_PreSpawn_Trig = value;
		}
	}

	public string Stage2_Enemy05_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy05_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy05_Spawn_Trig = value;
		}
	}

	public string Stage2_Enemy05_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy05_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy05_PreSpawn_Trig = value;
		}
	}

	public string Stage2_Enemy05_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy05_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy05_Kill_Trig = value;
		}
	}

	public string Stage2_Enemy06_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy06_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy06_Kill_Trig = value;
		}
	}

	public string Stage2_Enemy06_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy06_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy06_PreSpawn_Trig = value;
		}
	}

	public string Stage2_Enemy06_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy06_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy06_Spawn_Trig = value;
		}
	}

	public string Stage2_Enemy07_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy07_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy07_Spawn_Trig = value;
		}
	}

	public string Stage2_Enemy07_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy07_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy07_Kill_Trig = value;
		}
	}

	public string Stage2_Enemy07_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy07_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy07_PreSpawn_Trig = value;
		}
	}

	public string Stage2_Enemy08_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy08_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy08_Kill_Trig = value;
		}
	}

	public string Stage2_Enemy08_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy08_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy08_PreSpawn_Trig = value;
		}
	}

	public string Stage2_Enemy08_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy08_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy08_Spawn_Trig = value;
		}
	}

	public string Stage2_Enemy09_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy09_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy09_Kill_Trig = value;
		}
	}

	public string Stage2_Enemy09_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy09_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy09_Spawn_Trig = value;
		}
	}

	public string Stage2_Enemy09_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy09_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy09_PreSpawn_Trig = value;
		}
	}

	public string Stage2_Enemy10_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy10_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy10_PreSpawn_Trig = value;
		}
	}

	public string Stage2_Enemy10_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy10_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy10_Spawn_Trig = value;
		}
	}

	public string Stage2_Enemy10_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage2_Enemy10_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage2_Enemy10_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy01_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy01_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy01_PreSpawn_Trig = value;
		}
	}

	public string Stage3_Enemy01_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy01_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy01_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy01_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy01_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy01_Spawn_Trig = value;
		}
	}

	public string Stage3_Enemy02_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy02_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy02_Spawn_Trig = value;
		}
	}

	public string Stage3_Enemy02_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy02_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy02_PreSpawn_Trig = value;
		}
	}

	public string Stage3_Enemy02_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy02_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy02_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy03_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy03_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy03_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy03_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy03_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy03_Spawn_Trig = value;
		}
	}

	public string Stage3_Enemy03_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy03_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy03_PreSpawn_Trig = value;
		}
	}

	public string Stage3_Enemy04_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy04_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy04_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy04_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy04_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy04_Spawn_Trig = value;
		}
	}

	public string Stage3_Enemy04_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy04_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy04_PreSpawn_Trig = value;
		}
	}

	public string Stage3_Enemy05_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy05_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy05_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy05_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy05_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy05_PreSpawn_Trig = value;
		}
	}

	public string Stage3_Enemy05_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy05_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy05_Spawn_Trig = value;
		}
	}

	public string Stage3_Enemy06_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy06_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy06_PreSpawn_Trig = value;
		}
	}

	public string Stage3_Enemy06_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy06_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy06_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy06_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy06_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy06_Spawn_Trig = value;
		}
	}

	public string Stage3_Enemy07_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy07_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy07_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy07_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy07_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy07_PreSpawn_Trig = value;
		}
	}

	public string Stage3_Enemy07_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy07_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy07_Spawn_Trig = value;
		}
	}

	public string Stage3_Enemy08_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy08_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy08_PreSpawn_Trig = value;
		}
	}

	public string Stage3_Enemy08_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy08_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy08_Spawn_Trig = value;
		}
	}

	public string Stage3_Enemy08_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy08_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy08_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy09_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy09_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy09_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy09_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy09_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy09_Spawn_Trig = value;
		}
	}

	public string Stage3_Enemy09_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy09_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy09_PreSpawn_Trig = value;
		}
	}

	public string Stage3_Enemy10_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy10_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy10_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy10_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy10_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy10_Spawn_Trig = value;
		}
	}

	public string Stage3_Enemy10_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy10_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy10_PreSpawn_Trig = value;
		}
	}

	public string Stage3_Enemy11_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy11_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy11_PreSpawn_Trig = value;
		}
	}

	public string Stage3_Enemy11_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy11_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy11_Spawn_Trig = value;
		}
	}

	public string Stage3_Enemy11_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy11_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy11_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy12_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy12_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy12_Spawn_Trig = value;
		}
	}

	public string Stage3_Enemy12_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy12_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy12_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy12_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy12_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy12_PreSpawn_Trig = value;
		}
	}

	public string Stage3_Enemy13_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy13_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy13_Spawn_Trig = value;
		}
	}

	public string Stage3_Enemy13_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy13_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy13_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy13_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy13_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy13_PreSpawn_Trig = value;
		}
	}

	public string Stage3_Enemy14_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy14_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy14_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy14_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy14_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy14_Spawn_Trig = value;
		}
	}

	public string Stage3_Enemy14_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy14_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy14_PreSpawn_Trig = value;
		}
	}

	public string Stage3_Enemy15_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy15_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy15_PreSpawn_Trig = value;
		}
	}

	public string Stage3_Enemy15_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy15_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy15_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy15_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy15_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy15_Spawn_Trig = value;
		}
	}

	public string Stage3_Enemy16_Spawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy16_Spawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy16_Spawn_Trig = value;
		}
	}

	public string Stage3_Enemy16_Kill_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy16_Kill_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy16_Kill_Trig = value;
		}
	}

	public string Stage3_Enemy16_PreSpawn_Trig
	{
		get
		{
			return ExposedVariables.Stage3_Enemy16_PreSpawn_Trig;
		}
		set
		{
			ExposedVariables.Stage3_Enemy16_PreSpawn_Trig = value;
		}
	}

	public string OverheadKillVolume1
	{
		get
		{
			return ExposedVariables.OverheadKillVolume1;
		}
		set
		{
			ExposedVariables.OverheadKillVolume1 = value;
		}
	}

	public BlockTypes Terminal_Block
	{
		get
		{
			return ExposedVariables.Terminal_Block;
		}
		set
		{
			ExposedVariables.Terminal_Block = value;
		}
	}

	public SpawnTechData[] Terminal3SpawnData
	{
		get
		{
			return ExposedVariables.Terminal3SpawnData;
		}
		set
		{
			ExposedVariables.Terminal3SpawnData = value;
		}
	}

	public SpawnTechData[] Terminal2SpawnData
	{
		get
		{
			return ExposedVariables.Terminal2SpawnData;
		}
		set
		{
			ExposedVariables.Terminal2SpawnData = value;
		}
	}

	public SpawnTechData[] Terminal1SpawnData
	{
		get
		{
			return ExposedVariables.Terminal1SpawnData;
		}
		set
		{
			ExposedVariables.Terminal1SpawnData = value;
		}
	}

	public SpawnTechData[] ForcefieldStage1GroupSpawnData
	{
		get
		{
			return ExposedVariables.ForcefieldStage1GroupSpawnData;
		}
		set
		{
			ExposedVariables.ForcefieldStage1GroupSpawnData = value;
		}
	}

	public SpawnTechData[] ForcefieldStage4GroupSpawnData
	{
		get
		{
			return ExposedVariables.ForcefieldStage4GroupSpawnData;
		}
		set
		{
			ExposedVariables.ForcefieldStage4GroupSpawnData = value;
		}
	}

	public string Stage2GoalTriggerVolume
	{
		get
		{
			return ExposedVariables.Stage2GoalTriggerVolume;
		}
		set
		{
			ExposedVariables.Stage2GoalTriggerVolume = value;
		}
	}

	public string Stage3GoalTriggerVolume
	{
		get
		{
			return ExposedVariables.Stage3GoalTriggerVolume;
		}
		set
		{
			ExposedVariables.Stage3GoalTriggerVolume = value;
		}
	}

	public uScript_PlayDialogue.Dialogue msgStage1Complete
	{
		get
		{
			return ExposedVariables.msgStage1Complete;
		}
		set
		{
			ExposedVariables.msgStage1Complete = value;
		}
	}

	public uScript_PlayDialogue.Dialogue msgStage2Complete
	{
		get
		{
			return ExposedVariables.msgStage2Complete;
		}
		set
		{
			ExposedVariables.msgStage2Complete = value;
		}
	}

	public uScript_PlayDialogue.Dialogue msgEpilogue
	{
		get
		{
			return ExposedVariables.msgEpilogue;
		}
		set
		{
			ExposedVariables.msgEpilogue = value;
		}
	}

	public uScript_PlayDialogue.Dialogue msgStage3Complete
	{
		get
		{
			return ExposedVariables.msgStage3Complete;
		}
		set
		{
			ExposedVariables.msgStage3Complete = value;
		}
	}

	public string FlightHeightTrig
	{
		get
		{
			return ExposedVariables.FlightHeightTrig;
		}
		set
		{
			ExposedVariables.FlightHeightTrig = value;
		}
	}

	public uScript_AddMessage.MessageData msgFlyingWarning
	{
		get
		{
			return ExposedVariables.msgFlyingWarning;
		}
		set
		{
			ExposedVariables.msgFlyingWarning = value;
		}
	}

	public uScript_AddMessage.MessageData msgOverheadKillHitBubl
	{
		get
		{
			return ExposedVariables.msgOverheadKillHitBubl;
		}
		set
		{
			ExposedVariables.msgOverheadKillHitBubl = value;
		}
	}

	public uScript_AddMessage.MessageSpeaker SpeakerBubl
	{
		get
		{
			return ExposedVariables.SpeakerBubl;
		}
		set
		{
			ExposedVariables.SpeakerBubl = value;
		}
	}

	public uScript_AddMessage.MessageData msgOverheadKillHitHubl
	{
		get
		{
			return ExposedVariables.msgOverheadKillHitHubl;
		}
		set
		{
			ExposedVariables.msgOverheadKillHitHubl = value;
		}
	}

	public string MissionRangeTrig
	{
		get
		{
			return ExposedVariables.MissionRangeTrig;
		}
		set
		{
			ExposedVariables.MissionRangeTrig = value;
		}
	}

	public string NotMissionRangeTrig1
	{
		get
		{
			return ExposedVariables.NotMissionRangeTrig1;
		}
		set
		{
			ExposedVariables.NotMissionRangeTrig1 = value;
		}
	}

	public string NotMissionRangeTrig3
	{
		get
		{
			return ExposedVariables.NotMissionRangeTrig3;
		}
		set
		{
			ExposedVariables.NotMissionRangeTrig3 = value;
		}
	}

	public string NotMissionRangeTrig2
	{
		get
		{
			return ExposedVariables.NotMissionRangeTrig2;
		}
		set
		{
			ExposedVariables.NotMissionRangeTrig2 = value;
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

	public uScript_AddMessage.MessageData MsgIntroWaitingForPlayers
	{
		get
		{
			return ExposedVariables.MsgIntroWaitingForPlayers;
		}
		set
		{
			ExposedVariables.MsgIntroWaitingForPlayers = value;
		}
	}

	public SpawnTechData[] ForcefieldEntranceGroupSpawnData
	{
		get
		{
			return ExposedVariables.ForcefieldEntranceGroupSpawnData;
		}
		set
		{
			ExposedVariables.ForcefieldEntranceGroupSpawnData = value;
		}
	}

	public string Stage1GoalTriggerVolume
	{
		get
		{
			return ExposedVariables.Stage1GoalTriggerVolume;
		}
		set
		{
			ExposedVariables.Stage1GoalTriggerVolume = value;
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

	public uScript_AddMessage.MessageSpeaker SpeakerHubl
	{
		get
		{
			return ExposedVariables.SpeakerHubl;
		}
		set
		{
			ExposedVariables.SpeakerHubl = value;
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

	public SpawnTechData[] Stage1_Enemy04_Data
	{
		get
		{
			return ExposedVariables.Stage1_Enemy04_Data;
		}
		set
		{
			ExposedVariables.Stage1_Enemy04_Data = value;
		}
	}

	public SpawnTechData[] Stage1_Enemy03_Data
	{
		get
		{
			return ExposedVariables.Stage1_Enemy03_Data;
		}
		set
		{
			ExposedVariables.Stage1_Enemy03_Data = value;
		}
	}

	public SpawnTechData[] Stage1_Enemy02_Data
	{
		get
		{
			return ExposedVariables.Stage1_Enemy02_Data;
		}
		set
		{
			ExposedVariables.Stage1_Enemy02_Data = value;
		}
	}

	public SpawnTechData[] Stage1_Enemy01_Data
	{
		get
		{
			return ExposedVariables.Stage1_Enemy01_Data;
		}
		set
		{
			ExposedVariables.Stage1_Enemy01_Data = value;
		}
	}

	public SpawnTechData[] Stage2_Enemy07_Data
	{
		get
		{
			return ExposedVariables.Stage2_Enemy07_Data;
		}
		set
		{
			ExposedVariables.Stage2_Enemy07_Data = value;
		}
	}

	public SpawnTechData[] Stage2_Enemy10_Data
	{
		get
		{
			return ExposedVariables.Stage2_Enemy10_Data;
		}
		set
		{
			ExposedVariables.Stage2_Enemy10_Data = value;
		}
	}

	public SpawnTechData[] Stage2_Enemy01_Data
	{
		get
		{
			return ExposedVariables.Stage2_Enemy01_Data;
		}
		set
		{
			ExposedVariables.Stage2_Enemy01_Data = value;
		}
	}

	public SpawnTechData[] Stage2_Enemy11_Data
	{
		get
		{
			return ExposedVariables.Stage2_Enemy11_Data;
		}
		set
		{
			ExposedVariables.Stage2_Enemy11_Data = value;
		}
	}

	public SpawnTechData[] Stage2_Enemy06_Data
	{
		get
		{
			return ExposedVariables.Stage2_Enemy06_Data;
		}
		set
		{
			ExposedVariables.Stage2_Enemy06_Data = value;
		}
	}

	public SpawnTechData[] Stage2_Enemy04_Data
	{
		get
		{
			return ExposedVariables.Stage2_Enemy04_Data;
		}
		set
		{
			ExposedVariables.Stage2_Enemy04_Data = value;
		}
	}

	public SpawnTechData[] Stage2_Enemy05_Data
	{
		get
		{
			return ExposedVariables.Stage2_Enemy05_Data;
		}
		set
		{
			ExposedVariables.Stage2_Enemy05_Data = value;
		}
	}

	public SpawnTechData[] Stage2_Enemy02_Data
	{
		get
		{
			return ExposedVariables.Stage2_Enemy02_Data;
		}
		set
		{
			ExposedVariables.Stage2_Enemy02_Data = value;
		}
	}

	public SpawnTechData[] Stage2_Enemy08_Data
	{
		get
		{
			return ExposedVariables.Stage2_Enemy08_Data;
		}
		set
		{
			ExposedVariables.Stage2_Enemy08_Data = value;
		}
	}

	public SpawnTechData[] Stage2_Enemy03_Data
	{
		get
		{
			return ExposedVariables.Stage2_Enemy03_Data;
		}
		set
		{
			ExposedVariables.Stage2_Enemy03_Data = value;
		}
	}

	public SpawnTechData[] Stage2_Enemy09_Data
	{
		get
		{
			return ExposedVariables.Stage2_Enemy09_Data;
		}
		set
		{
			ExposedVariables.Stage2_Enemy09_Data = value;
		}
	}

	public SpawnTechData[] Stage3_Enemy10_Data
	{
		get
		{
			return ExposedVariables.Stage3_Enemy10_Data;
		}
		set
		{
			ExposedVariables.Stage3_Enemy10_Data = value;
		}
	}

	public SpawnTechData[] Stage3_Enemy08_Data
	{
		get
		{
			return ExposedVariables.Stage3_Enemy08_Data;
		}
		set
		{
			ExposedVariables.Stage3_Enemy08_Data = value;
		}
	}

	public SpawnTechData[] Stage3_Enemy15_Data
	{
		get
		{
			return ExposedVariables.Stage3_Enemy15_Data;
		}
		set
		{
			ExposedVariables.Stage3_Enemy15_Data = value;
		}
	}

	public SpawnTechData[] Stage3_Enemy14_Data
	{
		get
		{
			return ExposedVariables.Stage3_Enemy14_Data;
		}
		set
		{
			ExposedVariables.Stage3_Enemy14_Data = value;
		}
	}

	public SpawnTechData[] Stage3_Enemy03_Data
	{
		get
		{
			return ExposedVariables.Stage3_Enemy03_Data;
		}
		set
		{
			ExposedVariables.Stage3_Enemy03_Data = value;
		}
	}

	public SpawnTechData[] Stage3_Enemy12_Data
	{
		get
		{
			return ExposedVariables.Stage3_Enemy12_Data;
		}
		set
		{
			ExposedVariables.Stage3_Enemy12_Data = value;
		}
	}

	public SpawnTechData[] Stage3_Enemy01_Data
	{
		get
		{
			return ExposedVariables.Stage3_Enemy01_Data;
		}
		set
		{
			ExposedVariables.Stage3_Enemy01_Data = value;
		}
	}

	public SpawnTechData[] Stage3_Enemy13_Data
	{
		get
		{
			return ExposedVariables.Stage3_Enemy13_Data;
		}
		set
		{
			ExposedVariables.Stage3_Enemy13_Data = value;
		}
	}

	public SpawnTechData[] Stage3_Enemy05_Data
	{
		get
		{
			return ExposedVariables.Stage3_Enemy05_Data;
		}
		set
		{
			ExposedVariables.Stage3_Enemy05_Data = value;
		}
	}

	public SpawnTechData[] Stage3_Enemy04_Data
	{
		get
		{
			return ExposedVariables.Stage3_Enemy04_Data;
		}
		set
		{
			ExposedVariables.Stage3_Enemy04_Data = value;
		}
	}

	public SpawnTechData[] Stage3_Enemy07_Data
	{
		get
		{
			return ExposedVariables.Stage3_Enemy07_Data;
		}
		set
		{
			ExposedVariables.Stage3_Enemy07_Data = value;
		}
	}

	public SpawnTechData[] Stage3_Enemy09_Data
	{
		get
		{
			return ExposedVariables.Stage3_Enemy09_Data;
		}
		set
		{
			ExposedVariables.Stage3_Enemy09_Data = value;
		}
	}

	public SpawnTechData[] Stage3_Enemy02_Data
	{
		get
		{
			return ExposedVariables.Stage3_Enemy02_Data;
		}
		set
		{
			ExposedVariables.Stage3_Enemy02_Data = value;
		}
	}

	public SpawnTechData[] Stage3_Enemy16_Data
	{
		get
		{
			return ExposedVariables.Stage3_Enemy16_Data;
		}
		set
		{
			ExposedVariables.Stage3_Enemy16_Data = value;
		}
	}

	public SpawnTechData[] Stage3_Enemy11_Data
	{
		get
		{
			return ExposedVariables.Stage3_Enemy11_Data;
		}
		set
		{
			ExposedVariables.Stage3_Enemy11_Data = value;
		}
	}

	public SpawnTechData[] Stage3_Enemy06_Data
	{
		get
		{
			return ExposedVariables.Stage3_Enemy06_Data;
		}
		set
		{
			ExposedVariables.Stage3_Enemy06_Data = value;
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
