using UnityEngine;

public class BalloonVolume : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Approximate radius of the envelope at full inflation. This affects the lift positively, with a higher radius equalling higher volume equalling more lift. (If the gas density is lighter than air)")]
	protected float m_Radius = 1f;

	[SerializeField]
	[HideInInspector]
	public float m_Volume;

	protected const float k_FallbackAirDensity = 1.22f;

	public float Volume => m_Volume;

	public static Vector3 s_DirectionOfLift => -Physics.gravity;

	public Vector3 GetBuoyantForceFromGasDensity(float gasDensity)
	{
		float num = Singleton.Manager<ManGameMode>.inst.GetCurrentModeAtmosphereCurve()?.Evaluate(Singleton.Manager<ManGameMode>.inst.GetCurrentModeAltitude(base.transform.position.y)) ?? 1.22f;
		return Mathf.Max(0f, Volume * (num - gasDensity)) * s_DirectionOfLift;
	}

	private void PrePool()
	{
		m_Volume = 4.1887903f * Mathf.Pow(m_Radius, 3f);
	}

	private void OnDrawGizmos()
	{
		if (!Application.isPlaying)
		{
			Gizmos.color = new Color(0.1f, 0.8f, 1f, 0.8f);
			Gizmos.DrawWireSphere(base.transform.position, m_Radius);
		}
	}
}
