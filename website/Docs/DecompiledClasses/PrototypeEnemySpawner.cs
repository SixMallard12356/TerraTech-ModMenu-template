#define UNITY_EDITOR
using UnityEngine;

public class PrototypeEnemySpawner : MonoBehaviour
{
	[SerializeField]
	private TankPreset m_PresetToSpawn;

	[SerializeField]
	private Transform m_SpawnPos;

	private bool m_Triggered;

	private bool m_SpawnEnemy;

	private Tank.WeakReference m_SpawnedTank = new Tank.WeakReference();

	public void Reset()
	{
		m_Triggered = false;
		m_SpawnEnemy = false;
		m_SpawnedTank.Set(null);
	}

	public void RespawnEnemyIfActive()
	{
		Tank tank = m_SpawnedTank.Get();
		if ((bool)tank && Singleton.Manager<ManWorld>.inst.CheckIsTileAtPositionLoaded(m_SpawnPos.position))
		{
			tank.visible.RemoveFromGame();
			DoSpawnEnemy();
		}
	}

	private void DoSpawnEnemy()
	{
		ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
		{
			techData = m_PresetToSpawn.GetTechDataFormatted(),
			blockIDs = null,
			teamID = 1,
			position = m_SpawnPos.position,
			rotation = m_SpawnPos.rotation,
			grounded = true
		};
		Tank tank = Singleton.Manager<ManSpawn>.inst.SpawnTank(param, addToObjectManager: true);
		m_SpawnedTank.Set(tank);
	}

	private void OnSpawn()
	{
		Reset();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (m_Triggered)
		{
			return;
		}
		Visible componentInChildren = other.attachedRigidbody.GetComponentInChildren<Visible>();
		if (componentInChildren != null && (bool)componentInChildren.tank && componentInChildren.tank == Singleton.playerTank)
		{
			if ((bool)m_PresetToSpawn)
			{
				m_SpawnEnemy = true;
				m_Triggered = true;
			}
			else
			{
				d.LogWarning("PrototypeEnemySpawner.OnTriggerEnter: Need to set enemy preset to spawn");
			}
		}
	}

	private void Update()
	{
		if (m_SpawnEnemy)
		{
			DoSpawnEnemy();
			m_SpawnEnemy = false;
		}
	}
}
