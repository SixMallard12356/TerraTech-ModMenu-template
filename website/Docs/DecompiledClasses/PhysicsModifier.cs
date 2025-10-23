using UnityEngine;

public class PhysicsModifier : MonoBehaviour
{
	[SerializeField]
	private float m_HoverForceScale = 1f;

	[SerializeField]
	private float m_HoverMaxClimbDistance = 100f;

	public float HoverForceScale => m_HoverForceScale;

	public float HoverMaxClimbDistance => m_HoverMaxClimbDistance;
}
