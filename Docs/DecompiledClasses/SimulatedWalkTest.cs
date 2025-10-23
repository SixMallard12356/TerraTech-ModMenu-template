using System;
using UnityEngine;

[Serializable]
public class SimulatedWalkTest : CustomModeBehaviour
{
	public float m_CameraSpeedPerSecond;

	public float m_HeightAboveTerrain;

	public float m_MaxYMovementPerSecond;

	public bool m_ForceStopMovement;

	public float m_BiasTowardsExistingDirection;

	public float m_RotateSpeedPerSecond;

	private bool m_LastDirectionPositive;

	public override void ExitMode()
	{
	}

	public override void EnterPreMode()
	{
		Singleton.Manager<CameraManager>.inst.Switch<DebugCamera>();
		Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(null);
	}

	public override void UpdateMode()
	{
		if (!m_ForceStopMovement)
		{
			m_LastDirectionPositive = ((UnityEngine.Random.value < m_BiasTowardsExistingDirection) ? m_LastDirectionPositive : (!m_LastDirectionPositive));
			Singleton.cameraTrans.Rotate(Vector3.up * Time.deltaTime * m_RotateSpeedPerSecond * (m_LastDirectionPositive ? 1 : (-1)), Space.World);
			Singleton.cameraTrans.position += Singleton.cameraTrans.forward * m_CameraSpeedPerSecond * Time.deltaTime;
			Vector3 position = Singleton.cameraTrans.position;
			float outHeight = 0f;
			Singleton.Manager<ManWorld>.inst.GetTerrainHeight(position, out outHeight);
			float num = outHeight + m_HeightAboveTerrain;
			float f = num - position.y;
			if (Mathf.Abs(f) < m_MaxYMovementPerSecond * Time.deltaTime)
			{
				position.y = num;
			}
			else
			{
				position.y += Mathf.Sign(f) * m_MaxYMovementPerSecond * Time.deltaTime;
			}
			Singleton.cameraTrans.position = position;
		}
	}
}
