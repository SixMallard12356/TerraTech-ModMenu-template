using UnityEngine;

public class RotationAligner : MonoBehaviour
{
	[Tooltip("The world direction that this transform should align its 'up' towards")]
	[SerializeField]
	private Vector3 m_PoleAxis = Vector3.up;

	[SerializeField]
	[Tooltip("The angle to the pole axis at which this transform should no longer align itself with the pole axis and instead, align with the tech or, if not connected to a tech, the camera forward")]
	private float m_PoleExclusionAngle = 25f;

	[Tooltip("The associated tank block that this object is attached to. Can be Null.")]
	[SerializeField]
	private TankBlock m_TankBlock;

	[SerializeField]
	[Tooltip("The degrees at which rotations towards the pole should be clamped")]
	private float m_ClampAngle = 90f;

	private float m_AngleToPole;

	private Vector3 m_PointToward;

	private Vector3 m_Eulers;

	private Quaternion m_NextRot;

	private MinMaxFloat m_NonPoleAngleRange;

	private void Awake()
	{
		m_NonPoleAngleRange = new MinMaxFloat(m_PoleExclusionAngle, 180f - m_PoleExclusionAngle);
	}

	private void LateUpdate()
	{
		if (base.transform.parent == null)
		{
			return;
		}
		m_AngleToPole = Vector3.Angle(m_PoleAxis, base.transform.forward);
		if (m_NonPoleAngleRange.Contains(m_AngleToPole))
		{
			m_PointToward = m_PoleAxis;
		}
		else if (m_TankBlock != null && m_TankBlock.tank != null)
		{
			m_PointToward = m_TankBlock.tank.rootBlockTrans.forward;
		}
		else
		{
			if (!(Camera.main != null))
			{
				return;
			}
			m_PointToward = Camera.main.transform.forward;
		}
		m_NextRot = Quaternion.LookRotation(base.transform.forward, m_PointToward);
		m_NextRot = Quaternion.Inverse(base.transform.parent.rotation) * m_NextRot;
		m_Eulers = m_NextRot.eulerAngles / m_ClampAngle;
		m_Eulers = new Vector3(Mathf.RoundToInt(m_Eulers.x), Mathf.RoundToInt(m_Eulers.y), Mathf.RoundToInt(m_Eulers.z)) * m_ClampAngle;
		base.transform.localRotation = Quaternion.Euler(m_Eulers);
	}
}
