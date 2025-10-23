using UnityEngine;

public struct AutoStabiliser
{
	private Vector3 m_StationaryRefPos;

	private byte m_StationaryNumFrames;

	private const byte kStationaryMaxFrames = 15;

	private const float kStationaryVelThreshold = 1f;

	private const float kStationaryBreakDist = 0.5f;

	private const float kStationarySpringStrength = 10f;

	public void Reset()
	{
		m_StationaryNumFrames = 0;
	}

	public Vector3 GetAutoStabilisationVelocity(Rigidbody rbody, Vector3 effectorPos)
	{
		Vector3 pointVelocity = rbody.GetPointVelocity(effectorPos);
		if (m_StationaryNumFrames < 15)
		{
			if (Vector3.Scale(pointVelocity, new Vector3(1f, 0f, 1f)).magnitude < 1f)
			{
				m_StationaryNumFrames++;
				if (m_StationaryNumFrames == 15)
				{
					m_StationaryRefPos = effectorPos;
				}
			}
			else
			{
				m_StationaryNumFrames = 0;
			}
		}
		if (m_StationaryNumFrames == 15)
		{
			Vector3 vector = Vector3.Scale(effectorPos - m_StationaryRefPos, new Vector3(1f, 0f, 1f));
			if (vector.magnitude > 0.5f)
			{
				m_StationaryNumFrames = 0;
			}
			else
			{
				pointVelocity += vector * 10f;
			}
		}
		return pointVelocity;
	}
}
