using UnityEngine;

public class RotateRandomOnSpawn : MonoBehaviour
{
	[SerializeField]
	private float[] m_AvailableRotations;

	[SerializeField]
	private Axis m_RotationAxis = new Axis(Axis.AxisType.X);

	private Transform m_Trans;

	private Quaternion m_InitialRotation;

	public void UpdateRotation()
	{
		if (m_AvailableRotations != null && m_AvailableRotations.Length != 0)
		{
			int num = Random.Range(0, m_AvailableRotations.Length - 1);
			m_Trans.localRotation = m_InitialRotation * Quaternion.AngleAxis(m_AvailableRotations[num], m_RotationAxis);
		}
	}

	private void OnPool()
	{
		m_Trans = base.transform;
		m_InitialRotation = m_Trans.localRotation;
	}

	private void OnSpawn()
	{
		UpdateRotation();
	}
}
