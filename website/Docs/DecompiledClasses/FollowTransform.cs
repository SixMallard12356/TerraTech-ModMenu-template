using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class FollowTransform : MonoBehaviour
{
	[SerializeField]
	private Transform m_TransformToFollow;

	[SerializeField]
	private bool m_FollowOrientation;

	private Transform m_Trans;

	private Transform Transform
	{
		get
		{
			if (m_Trans == null)
			{
				m_Trans = GetComponent<Transform>();
			}
			return m_Trans;
		}
	}

	public void SetFollowTransform(Transform toFollow)
	{
		m_TransformToFollow = toFollow;
	}

	private void LateUpdate()
	{
		if ((bool)m_TransformToFollow)
		{
			Transform.position = m_TransformToFollow.position;
			if (m_FollowOrientation)
			{
				Transform.rotation = m_TransformToFollow.rotation;
			}
		}
	}
}
