#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class ModeWaves : ModePVP<ModeWaves>
{
	private enum WaveState
	{
		Init,
		InWave,
		WaitingForNextWave
	}

	private NetController.WaveData m_WaveData;

	private NetController.Wave m_CurrentWave;

	private WaveState m_CurrentWaveState;

	private int m_CurrentWaveNum;

	private float m_WaveTimer;

	private List<Tank> m_WaveEnemies = new List<Tank>();

	private List<Tank> m_CurrentWaveEnemies = new List<Tank>();

	private static bool m_kTemporarilyDisableWaveSpawn = true;

	public bool WavesFinished()
	{
		bool result = false;
		if (m_WaveData != null && !m_WaveData.m_Endless)
		{
			result = m_CurrentWaveNum >= m_WaveData.m_Waves.Count;
		}
		return result;
	}

	public override MultiplayerModeType GetMultiplayerGameType()
	{
		return MultiplayerModeType.CoOpCreative;
	}

	public override string GetGameMode()
	{
		return "ModeWaves";
	}

	public override ManGameMode.GameType GetGameType()
	{
		return ManGameMode.GameType.CoOpCreative;
	}

	protected override void GameModeInit()
	{
		m_CurrentWaveNum = 0;
		m_WaveTimer = 0f;
		m_WaveData = null;
		m_CurrentWave = null;
		m_CurrentWaveState = WaveState.Init;
		m_WaveEnemies.Clear();
		m_CurrentWaveEnemies.Clear();
	}

	protected override void OnServerPhaseEnter(NetController.Phase phase)
	{
		if (phase == NetController.Phase.Intro)
		{
			if (Singleton.Manager<ManNetwork>.inst.NetController.UseWaves)
			{
				m_WaveData = Singleton.Manager<ManNetwork>.inst.NetController.CurrentWaveData;
			}
			else
			{
				d.LogError("EnemyWavesMultiplayerGameMode.OnServerPhaseEnter - NetOptions not set to use Waves");
			}
			Singleton.Manager<ManNetwork>.inst.NetTechDestroyed.Subscribe(OnServerOnNetTechDestroyed);
		}
	}

	protected override void OnClientPhaseEnter(NetController.Phase phase)
	{
	}

	protected override void ServerUpdateGameMode()
	{
		if (m_CurrentWaveState == WaveState.Init)
		{
			if (m_WaveData.m_Waves != null && m_WaveData.m_Waves.Count > 0)
			{
				m_CurrentWave = m_WaveData.m_Waves[0];
				EnterWaveState(WaveState.InWave);
			}
			else
			{
				d.LogError("ModeMultiplayer.UpdateGameMode - Using Waves, but no Waves in WaveData");
			}
		}
		else
		{
			UpdateWaveState();
			m_WaveTimer += Time.deltaTime;
		}
	}

	protected override void ClientUpdateGameMode()
	{
	}

	protected override void OnServerPlayerOutOfBounds()
	{
	}

	protected override void GameModeExit()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Singleton.Manager<ManNetwork>.inst.NetTechDestroyed.Unsubscribe(OnServerOnNetTechDestroyed);
			Singleton.Manager<ManNetwork>.inst.NetController.ClearGameTimer();
		}
	}

	private void UpdateWaveState()
	{
		switch (m_CurrentWaveState)
		{
		case WaveState.InWave:
			if (m_kTemporarilyDisableWaveSpawn || !CheckForWaveCompletion())
			{
				break;
			}
			if (Singleton.Manager<ManNetwork>.inst.NetController.CurrentScorePolicy == NetController.ScorePolicy.NumWaves)
			{
				for (int i = 0; i < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); i++)
				{
					NetPlayer player = Singleton.Manager<ManNetwork>.inst.GetPlayer(i);
					if (player.IsPlayerActive)
					{
						player.OnServerAddPoints(1f);
					}
				}
			}
			if (m_WaveData.m_Endless || m_CurrentWaveNum + 1 < m_WaveData.m_Waves.Count)
			{
				EnterWaveState(WaveState.WaitingForNextWave);
			}
			else
			{
				m_CurrentWaveNum++;
			}
			break;
		case WaveState.WaitingForNextWave:
		{
			float gameTimer = Mathf.Max(m_CurrentWave.m_NextWaveTime - m_WaveTimer, 0f);
			Singleton.Manager<ManNetwork>.inst.NetController.SetGameTimer(gameTimer);
			if (m_WaveTimer >= m_CurrentWave.m_NextWaveTime)
			{
				m_CurrentWaveNum++;
				if (m_CurrentWaveNum < m_WaveData.m_Waves.Count)
				{
					m_CurrentWave = m_WaveData.m_Waves[m_CurrentWaveNum];
				}
				EnterWaveState(WaveState.InWave);
			}
			break;
		}
		}
	}

	private void EnterWaveState(WaveState state)
	{
		switch (state)
		{
		case WaveState.InWave:
			Singleton.Manager<ManNetwork>.inst.NetController.ClearGameTimer();
			if (!m_kTemporarilyDisableWaveSpawn)
			{
				SpawnWave();
			}
			break;
		case WaveState.WaitingForNextWave:
			if (m_CurrentWave.m_RewardsPerPlayer || m_CurrentWave.m_RewardsIntoInventory)
			{
				int numPlayers = Singleton.Manager<ManNetwork>.inst.GetNumPlayers();
				for (int i = 0; i < numPlayers; i++)
				{
					NetPlayer player = Singleton.Manager<ManNetwork>.inst.GetPlayer(i);
					if (!m_CurrentWave.m_RewardsIntoInventory)
					{
						NetTech curTech = player.CurTech;
						if (curTech != null)
						{
							Singleton.Manager<ManSpawn>.inst.RewardSpawner.AddBlocksAroundTech(m_CurrentWave.m_BlockRewards, curTech.tech);
						}
					}
				}
			}
			else
			{
				Singleton.Manager<ManSpawn>.inst.RewardSpawner.AddBlocksAtPosition(m_CurrentWave.m_BlockRewards, Vector3.zero);
			}
			break;
		}
		m_WaveTimer = 0f;
		m_CurrentWaveState = state;
	}

	private void SpawnWave()
	{
		m_CurrentWaveEnemies.Clear();
		int count = m_CurrentWave.m_WaveEnemies.Count;
		float y = 360f / (float)count;
		Quaternion quaternion = Quaternion.Euler(new Vector3(0f, y, 0f));
		float num = 75f;
		Vector3 zero = Vector3.zero;
		Vector3 vector = Vector3.forward * num;
		for (int i = 0; i < count; i++)
		{
			Quaternion rot = Quaternion.LookRotation(-vector, Vector3.up);
			Vector3 pos = zero + vector;
			TechData techDataFormatted = m_CurrentWave.m_WaveEnemies[i].GetTechDataFormatted();
			uint[] array = new uint[techDataFormatted.m_BlockSpecs.Count];
			for (int j = 0; j < techDataFormatted.m_BlockSpecs.Count; j++)
			{
				array[j] = Singleton.Manager<ManNetwork>.inst.GetNextHostBlockPoolID();
			}
			TrackedVisible trackedVisible = Singleton.Manager<ManNetwork>.inst.SpawnNetworkedNonPlayerTech(techDataFormatted, array, pos, rot, grounded: true);
			if (trackedVisible != null && trackedVisible.visible != null)
			{
				m_CurrentWaveEnemies.Add(trackedVisible.visible.tank);
				m_WaveEnemies.Add(trackedVisible.visible.tank);
			}
			else
			{
				d.LogError("ModeMultiplayer.SpawnWave - No Tracked Visible or Visible returned");
			}
			vector = quaternion * vector;
		}
	}

	private bool CheckForWaveCompletion()
	{
		bool result = false;
		switch (m_WaveData.m_WaveEndCondition)
		{
		case NetController.WaveCompletion.TimeExpired:
			result = m_WaveTimer >= m_CurrentWave.m_WaveTime;
			break;
		case NetController.WaveCompletion.AllKilled:
			result = m_CurrentWaveEnemies.Count == 0;
			break;
		}
		return result;
	}

	private void OnServerOnNetTechDestroyed(Tank tech)
	{
		m_CurrentWaveEnemies.Remove(tech);
		m_WaveEnemies.Remove(tech);
	}
}
