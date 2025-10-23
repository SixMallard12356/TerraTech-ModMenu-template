using UnityEngine;

[ExecuteInEditMode]
public class FollowSuspension : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Target transform to look at (down Z axis). Typically the Wheel")]
	private Transform m_TargetWheelTrans;

	[SerializeField]
	[Tooltip("Transform to use for Up Vector. Cannot be the wheel as it rotates. Typically the Block root object")]
	private Transform m_TargetUpTrans;

	[SerializeField]
	private bool m_KeepOriginalRotationOffset = true;

	[SerializeField]
	private bool m_ScaleToMatchDistance = true;

	private Transform m_Transform;

	private float m_OriginalDistance;

	private Quaternion m_OriginalRotationOffset;

	private Vector3 TargetUp
	{
		get
		{
			if (!(m_TargetUpTrans != null))
			{
				return m_TargetWheelTrans.up;
			}
			return m_TargetUpTrans.up;
		}
	}

	private void OnPool()
	{
		m_Transform = base.transform;
		Vector3 forward = m_TargetWheelTrans.position - m_Transform.position;
		m_OriginalDistance = forward.magnitude / m_Transform.localScale.z;
		m_OriginalRotationOffset = Quaternion.Inverse(Quaternion.LookRotation(forward, TargetUp)) * m_Transform.rotation;
	}

	private void LateUpdate()
	{
		m_Transform.LookAt(m_TargetWheelTrans, TargetUp);
		if (m_KeepOriginalRotationOffset)
		{
			m_Transform.rotation *= m_OriginalRotationOffset;
		}
		if (m_ScaleToMatchDistance)
		{
			Vector3 localScale = m_Transform.localScale;
			float magnitude = (m_TargetWheelTrans.position - m_Transform.position).magnitude;
			localScale = localScale.SetZ(magnitude / m_OriginalDistance);
			m_Transform.localScale = localScale;
		}
	}
}
