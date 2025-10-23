using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class CameraRotateForTrailer : MonoBehaviour
{
	[SerializeField]
	private float m_RotateSpeed = 1f;

	[SerializeField]
	private float m_NoRotations = 3f;

	[SerializeField]
	private bool m_Grounded = true;

	[SerializeField]
	private TankPreset[] m_PresetsToSpawn;

	private int m_TechIndex;

	private float m_Timer;

	public void Enable(bool enable)
	{
		base.enabled = enable;
		m_Timer = 0f;
		m_TechIndex = 0;
		TankCamera.inst.FreezeCamera(enable);
	}

	public void Toggle()
	{
		Enable(!base.enabled);
	}

	private void Update()
	{
		if (!Singleton.playerTank)
		{
			return;
		}
		float num = 360f / m_RotateSpeed;
		Singleton.cameraTrans.RotateAround(Singleton.playerTank.visible.centrePosition, Vector3.up, num * Time.deltaTime);
		m_Timer += Time.deltaTime;
		if (m_Timer >= m_RotateSpeed * m_NoRotations)
		{
			m_Timer = 0f;
			m_TechIndex++;
			if (m_TechIndex >= m_PresetsToSpawn.Length)
			{
				m_TechIndex = 0;
			}
			ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
			{
				techData = m_PresetsToSpawn[m_TechIndex].GetTechDataFormatted(),
				blockIDs = null,
				teamID = 0,
				position = Singleton.playerTank.boundsCentreWorld,
				rotation = Singleton.playerTank.trans.rotation * Quaternion.Inverse(Singleton.playerTank.rootBlockTrans.localRotation),
				grounded = m_Grounded
			};
			TrackedVisible trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnTankRef(param, addToObjectManager: true);
			trackedVisible.visible.trans.rotation *= Quaternion.Inverse(trackedVisible.visible.tank.rootBlockTrans.localRotation);
			Singleton.playerTank.visible.RemoveFromGame();
			Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(trackedVisible.visible.tank);
		}
	}
}
