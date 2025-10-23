using System;
using System.Collections.Generic;
using UnityEngine;

public class DebugEnemySpawnTest : ScriptableObject
{
	[SerializeField]
	private TankPreset[] m_EnemyPresetsToSpawn;

	[SerializeField]
	private bool m_SpawnCaptureAsPlayer;

	[SerializeField]
	private float m_SpawnDistanceFromCamera = 40f;

	[SerializeField]
	private bool m_GoToNextSnapshot;

	[SerializeField]
	private float m_DistanceFromEnemies = 10f;

	[SerializeField]
	[Range(5f, 360f)]
	private float m_EnemySpawnArc = 100f;

	public int m_SelectedCaptureIndex;

	public static List<string> s_CapturePaths;

	public static string[] s_CaptureNames;

	public void SpawnTarget()
	{
		TechData techData = ManSnapshots.Debug_LoadTechData(s_CapturePaths[m_SelectedCaptureIndex]);
		if (techData != null)
		{
			GetSpawnCentreAndRotation(out var position, out var rotation);
			ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
			{
				techData = techData,
				blockIDs = null,
				teamID = 0,
				position = position,
				rotation = rotation,
				grounded = true
			};
			Tank tank = Singleton.Manager<ManSpawn>.inst.SpawnTank(param, addToObjectManager: true);
			if (m_SpawnCaptureAsPlayer)
			{
				if ((bool)Singleton.playerTank)
				{
					Singleton.playerTank.visible.RemoveFromGame();
				}
				Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(tank);
			}
		}
		if (m_GoToNextSnapshot)
		{
			m_SelectedCaptureIndex = (m_SelectedCaptureIndex + 1) % s_CapturePaths.Count;
		}
	}

	public void SpawnEnemies()
	{
		float a = m_DistanceFromEnemies;
		float num = ManSnapshots.Debug_LoadTechData(s_CapturePaths[m_SelectedCaptureIndex])?.Radius ?? 0f;
		float num2 = 0f;
		for (int i = 0; i < m_EnemyPresetsToSpawn.Length; i++)
		{
			a = Mathf.Max(a, (m_EnemyPresetsToSpawn[i].Radius + num) * 0.5f + m_DistanceFromEnemies);
			if (i < m_EnemyPresetsToSpawn.Length - 1)
			{
				num2 = Mathf.Max(num2, m_EnemyPresetsToSpawn[i].Radius + m_EnemyPresetsToSpawn[i + 1].Radius);
			}
		}
		float num3 = ((m_EnemySpawnArc == 360f) ? (m_EnemySpawnArc / (float)m_EnemyPresetsToSpawn.Length) : (m_EnemySpawnArc / (float)(m_EnemyPresetsToSpawn.Length - 1)));
		float num4 = m_EnemySpawnArc * -0.5f;
		float f = (180f - num3) * 0.5f * ((float)Math.PI / 180f);
		float b = (num2 * 0.5f + 1f) / Mathf.Sin(num3 * ((float)Math.PI / 180f)) * Mathf.Sin(f);
		a = Mathf.Max(a, b);
		GetSpawnCentreAndRotation(out var position, out var rotation);
		Vector3 point = position + rotation * Vector3.forward * a;
		for (int j = 0; j < m_EnemyPresetsToSpawn.Length; j++)
		{
			Vector3 vector = point.RotatePointAroundPivot(position, new Vector3(0f, num4 + num3 * (float)j, 0f));
			Quaternion rotation2 = Quaternion.LookRotation((position - vector).SetY(0f), Vector3.up);
			ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
			{
				techData = m_EnemyPresetsToSpawn[j].GetTechDataFormatted(),
				blockIDs = null,
				teamID = 1,
				position = vector,
				rotation = rotation2,
				grounded = true
			};
			Singleton.Manager<ManSpawn>.inst.SpawnTank(param, addToObjectManager: false);
		}
	}

	private void GetSpawnCentreAndRotation(out Vector3 position, out Quaternion rotation)
	{
		if (m_SpawnCaptureAsPlayer && (bool)Singleton.playerTank)
		{
			position = Singleton.playerTank.boundsCentreWorld;
			rotation = Singleton.playerTank.trans.rotation * Quaternion.Inverse(Singleton.playerTank.rootBlockTrans.localRotation);
		}
		else
		{
			position = Singleton.cameraTrans.position + Singleton.cameraTrans.forward.SetY(0f) * m_SpawnDistanceFromCamera;
			position = Singleton.Manager<ManWorld>.inst.ProjectToGround(position, hitScenery: true) + Vector3.up;
			rotation = Quaternion.LookRotation(Singleton.cameraTrans.forward.SetY(0f), Vector3.up);
		}
	}
}
