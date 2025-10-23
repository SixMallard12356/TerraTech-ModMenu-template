using UnityEngine;

public class FireDataShotgun : FireData
{
	public float m_ShotMinRange;

	public float m_ShotMaxRange = 20f;

	public float m_ShotFullDamageRange = 5f;

	[Range(0f, 180f)]
	[Tooltip("Angle of the shot spread in Degrees")]
	public float m_ShotArc = 60f;

	[Range(0f, 1f)]
	public float m_MinDamagePercent = 0.2f;

	[Range(0f, 1f)]
	public float m_MaxDamagePercent = 1f;

	public float m_TargetKickbackStrength = 1f;

	public float m_TargetKickbackDuration;
}
