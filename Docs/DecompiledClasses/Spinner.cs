using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class Spinner : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Revolutions per second")]
	[FormerlySerializedAs("speed")]
	public float m_Speed = 1f;

	[SerializeField]
	[FormerlySerializedAs("rotationAxis")]
	public Axis m_RotationAxis = new Axis(Axis.AxisType.X);

	[SerializeField]
	[FormerlySerializedAs("steerAxis")]
	public Axis m_SteerAxis = new Axis(Axis.AxisType.Y);

	[SerializeField]
	[FormerlySerializedAs("autoSpin")]
	public bool m_AutoSpin;

	[FormerlySerializedAs("spinUpTime")]
	[SerializeField]
	public float m_SpinUpTime = 1f;

	private float m_CurrentAngle;

	private float m_PrevAngle;

	private float m_CurrentAutoSpeed;

	private float m_CurrentSpeedFraction;

	private Quaternion m_InitialRotation;

	private Quaternion m_AxisSteerRotation;

	private AudioSource m_SpinnerSound;

	private float m_SpinnerSoundOriginalPitch;

	public Vector3 worldAxis => trans.TransformDirection(m_RotationAxis);

	public Transform trans { get; private set; }

	public float Speed => m_Speed;

	public float SpeedFraction => m_CurrentAutoSpeed / m_Speed;

	public bool AtFullSpeed => m_CurrentAutoSpeed.Approximately(m_Speed);

	public void SetAutoSpin(bool enableAutoSpin)
	{
		if (enableAutoSpin != m_AutoSpin)
		{
			m_AutoSpin = enableAutoSpin;
			if (m_SpinUpTime <= Mathf.Epsilon)
			{
				m_CurrentAutoSpeed = (m_AutoSpin ? m_Speed : 0f);
				m_CurrentSpeedFraction = (m_AutoSpin ? 1f : 0f);
			}
		}
	}

	public void UpdateSpin(float angle)
	{
		m_CurrentAngle += angle;
	}

	public void SetAngle(float angle)
	{
		m_CurrentAngle = angle;
	}

	public void SteerAxis(float angle)
	{
		m_AxisSteerRotation = Quaternion.Euler((Vector3)m_SteerAxis * angle);
	}

	public void Reset()
	{
		m_CurrentAngle = 0f;
		m_PrevAngle = float.MinValue;
		m_AxisSteerRotation = Quaternion.identity;
		m_CurrentAutoSpeed = (m_AutoSpin ? m_Speed : 0f);
		m_CurrentSpeedFraction = (m_AutoSpin ? 1f : 0f);
		trans.localRotation = m_InitialRotation;
	}

	public void ReInit()
	{
		m_InitialRotation = base.transform.localRotation;
	}

	private void OnPool()
	{
		trans = base.transform;
		m_InitialRotation = base.transform.localRotation;
		m_SpinnerSound = GetComponent<AudioSource>();
		if ((bool)m_SpinnerSound)
		{
			m_SpinnerSoundOriginalPitch = m_SpinnerSound.pitch;
		}
		TankBlock componentInParents = this.GetComponentInParents<TankBlock>();
		((componentInParents != null) ? componentInParents.BlockUpdate : new MonoBehaviourEvent<MB_Update>(base.gameObject)).Subscribe(OnUpdate);
	}

	private void OnSpawn()
	{
		Reset();
	}

	private void OnUpdate()
	{
		float currentSpeedFraction = m_CurrentSpeedFraction;
		if (m_AutoSpin)
		{
			if (m_CurrentSpeedFraction < 1f)
			{
				m_CurrentSpeedFraction = Mathf.Clamp(m_CurrentSpeedFraction + Time.deltaTime / m_SpinUpTime, 0f, 1f);
			}
		}
		else if (m_CurrentSpeedFraction > 0f)
		{
			m_CurrentSpeedFraction = Mathf.Clamp(m_CurrentSpeedFraction - Time.deltaTime / m_SpinUpTime, 0f, 1f);
		}
		if (currentSpeedFraction != m_CurrentSpeedFraction)
		{
			m_CurrentAutoSpeed = Mathf.SmoothStep(0f, m_Speed, m_CurrentSpeedFraction);
		}
		if (m_CurrentAutoSpeed != 0f)
		{
			m_CurrentAngle += 360f * m_CurrentAutoSpeed * Time.deltaTime;
		}
		if (m_SpinnerSound != null)
		{
			if (m_CurrentAutoSpeed != 0f)
			{
				if (!m_SpinnerSound.isPlaying)
				{
					m_SpinnerSound.Play();
				}
				m_SpinnerSound.pitch = m_SpinnerSoundOriginalPitch * m_CurrentSpeedFraction;
			}
			else if (m_SpinnerSound.isPlaying)
			{
				m_SpinnerSound.Stop();
			}
		}
		if (m_CurrentAngle != m_PrevAngle)
		{
			trans.SetLocalRotationIfChanged(m_InitialRotation * m_AxisSteerRotation * Quaternion.AngleAxis(m_CurrentAngle, m_RotationAxis));
			m_PrevAngle = m_CurrentAngle;
		}
	}

	private void OnDrawGizmos()
	{
		if (base.gameObject.EditorSelectedSingle())
		{
			Gizmos.color = Color.blue;
			Vector3 vector = base.transform.TransformDirection(m_RotationAxis);
			DebugUtil.GizmosDrawArrow(base.transform.position - vector, base.transform.position + vector);
		}
	}
}
