using UnityEngine;

public struct GravityManipulationZone
{
	public Vector3 m_Position;

	public bool m_PositionIsLocal;

	public float m_Radius;

	public float m_ManipulationAmount;

	private int m_CachedHash;

	public override int GetHashCode()
	{
		if (m_CachedHash == 0)
		{
			if (m_PositionIsLocal)
			{
				m_CachedHash |= int.MinValue;
			}
			m_CachedHash |= m_Position.GetHashCode() << 16;
			m_CachedHash |= ((int)m_Radius & 0xFF) << 8;
			m_CachedHash |= (int)(m_ManipulationAmount * 16f) & 0xFF;
		}
		return m_CachedHash;
	}
}
