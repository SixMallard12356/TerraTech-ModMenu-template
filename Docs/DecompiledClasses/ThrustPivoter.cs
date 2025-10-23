#define UNITY_EDITOR
using System.Diagnostics;
using UnityEngine;

public class ThrustPivoter : MonoBehaviour
{
	private struct TargetRotationCalculation
	{
		public Quaternion ClampedTargetRotationOnPlane;

		public Quaternion TargetRotation;

		public Vector3 TargetDirection;

		public Quaternion ArcCenterWorldRotation;

		public Quaternion MinArcWorldRotation;

		public Quaternion MaxArcWorldRotation;

		public Quaternion DefaultWorldRotation;

		public Vector3 PivotWorldNormal;

		public static TargetRotationCalculation Calculate(Transform transform, Vector3 targetWorldDirection, Quaternion defaultLocalRotation, float pivotCenterOffset01, float pivotMaxAngle)
		{
			TargetRotationCalculation result = default(TargetRotationCalculation);
			result.TargetDirection = targetWorldDirection;
			result.TargetRotation = Quaternion.LookRotation(result.TargetDirection);
			result.DefaultWorldRotation = transform.TransformRotation(defaultLocalRotation);
			result.PivotWorldNormal = result.DefaultWorldRotation * Vector3.right;
			Vector3 normalized = Vector3.ProjectOnPlane(result.TargetDirection, result.PivotWorldNormal.normalized).normalized;
			result.ClampedTargetRotationOnPlane = (normalized.sqrMagnitude.Approximately(0f) ? result.DefaultWorldRotation : Quaternion.LookRotation(normalized));
			result.ArcCenterWorldRotation = CalculateArcCenter(result.DefaultWorldRotation, pivotCenterOffset01, pivotMaxAngle);
			result.MinArcWorldRotation = result.ArcCenterWorldRotation * Quaternion.AngleAxis(0f - pivotMaxAngle, Vector3.right);
			result.MaxArcWorldRotation = result.ArcCenterWorldRotation * Quaternion.AngleAxis(pivotMaxAngle, Vector3.right);
			Vector3 vector = result.ClampedTargetRotationOnPlane * Vector3.forward;
			if (Vector3.Angle(vector, result.ArcCenterWorldRotation * Vector3.forward) > pivotMaxAngle)
			{
				float num = Vector3.Angle(vector, result.MinArcWorldRotation * Vector3.forward);
				float num2 = Vector3.Angle(vector, result.MaxArcWorldRotation * Vector3.forward);
				result.ClampedTargetRotationOnPlane = ((num < num2) ? result.MinArcWorldRotation : result.MaxArcWorldRotation);
			}
			if (Vector3.Angle(result.ClampedTargetRotationOnPlane * Vector3.forward, result.TargetRotation * Vector3.forward) >= 65f)
			{
				result.ClampedTargetRotationOnPlane = result.DefaultWorldRotation;
			}
			return result;
		}

		public static Quaternion CalculateArcCenter(Quaternion defaultWorldRotation, float pivotCenterOffset01, float pivotMaxAngle)
		{
			return defaultWorldRotation * Quaternion.AngleAxis(pivotMaxAngle * (pivotCenterOffset01 * 2f - 1f), Vector3.right);
		}
	}

	[SerializeField]
	[Range(0f, 45f)]
	protected float m_MaxAngle = 45f;

	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("How justified between the two extremes of the arc the default orientation should be")]
	protected float m_DefaultAngleOffset = 0.5f;

	[SerializeField]
	[Tooltip("Can we align this thrust pivoter towards the opposite of the target thrust direction if it's closer? This is useful for propellors which can have negative thrust")]
	protected bool m_AllowForInverseAlignment = true;

	[Tooltip("The angle per second that we can pivot")]
	[SerializeField]
	protected float m_RotationPerSecond = 90f;

	[SerializeField]
	[HideInInspector]
	protected Quaternion m_DefaultLocalRotation;

	[SerializeField]
	[HideInInspector]
	protected ModuleBooster m_OwnerBooster;

	private const float k_MinTargetContributionAngle = 65f;

	private const float k_MinAngleDeltaForRotationRecalc = 0.05f;

	protected bool m_Enabled;

	protected Thruster[] m_ManagedThrusters;

	private Vector3 m_TargetDirectionTechRelative;

	private TargetRotationCalculation m_TargetRotationCalcCache;

	protected Quaternion DefaultWorldRotation => base.transform.TransformRotation(m_DefaultLocalRotation);

	public void ResetAlignmentDirectionInstantly()
	{
		SetAlignmentDirectionInstantly(Vector3.zero);
	}

	public void SetAlignmentDirectionInstantly(Vector3 targetAlignmentDirection)
	{
		SetTechAlignmentDirection(targetAlignmentDirection);
		RecalculateAlignmentRotation();
		MoveTowardsTargetRotation(instantly: true);
	}

	public void ResetTechAlignmentDirection()
	{
		SetTechAlignmentDirection(Vector3.zero);
	}

	public void SetTechAlignmentDirection(Vector3 targetAlignmentTankRelative)
	{
		m_TargetDirectionTechRelative = targetAlignmentTankRelative.normalized;
	}

	private Vector3 GetWorldspaceTargetAlignmentDirection()
	{
		if (!m_TargetDirectionTechRelative.sqrMagnitude.Approximately(0f))
		{
			return m_OwnerBooster.block.tank.rootBlockTrans.rotation * m_TargetDirectionTechRelative;
		}
		return DefaultWorldRotation * Vector3.forward;
	}

	private void TryRecalculateAlignentRotation()
	{
		if (m_TargetRotationCalcCache.TargetDirection != default(Vector3))
		{
			float num = Vector3.Angle(GetWorldspaceTargetAlignmentDirection(), m_TargetRotationCalcCache.TargetDirection);
			float num2 = Vector3.Angle(m_TargetRotationCalcCache.PivotWorldNormal, base.transform.right);
			if (num < 0.05f && num2 < 0.05f)
			{
				return;
			}
		}
		RecalculateAlignmentRotation();
	}

	private void RecalculateAlignmentRotation()
	{
		Vector3 vector = GetWorldspaceTargetAlignmentDirection();
		if (m_AllowForInverseAlignment)
		{
			Vector3 to = TargetRotationCalculation.CalculateArcCenter(DefaultWorldRotation, m_DefaultAngleOffset, m_MaxAngle) * Vector3.forward;
			if (Vector3.Angle(vector, to) > Vector3.Angle(-vector, to))
			{
				vector = -vector;
			}
		}
		m_TargetRotationCalcCache = TargetRotationCalculation.Calculate(base.transform, vector, m_DefaultLocalRotation, m_DefaultAngleOffset, m_MaxAngle);
	}

	private void MoveTowardsTargetRotation(bool instantly = false)
	{
		Vector3 forward = base.transform.forward;
		base.transform.rotation = Util.QuaternionUtils.RotateTowardsAroundAxis(base.transform.rotation, m_TargetRotationCalcCache.ClampedTargetRotationOnPlane, m_TargetRotationCalcCache.PivotWorldNormal, instantly ? 180f : (m_RotationPerSecond * Time.deltaTime));
		if (forward != base.transform.forward)
		{
			Thruster[] managedThrusters = m_ManagedThrusters;
			for (int i = 0; i < managedThrusters.Length; i++)
			{
				managedThrusters[i].RecalculateThrustDirection();
			}
		}
	}

	private void OnAttached()
	{
		m_Enabled = true;
	}

	private void OnDetaching()
	{
		m_Enabled = false;
		ResetAlignmentDirectionInstantly();
	}

	private void PrePool()
	{
		m_DefaultLocalRotation = base.transform.localRotation;
		m_OwnerBooster = base.transform.GetComponentInParents<ModuleBooster>();
		d.Assert(m_OwnerBooster != null, "ERROR: Tried to use a thrust pivoter with no modulebooster in its parents. We don't support this!");
	}

	private void OnPool()
	{
		m_ManagedThrusters = GetComponentsInChildren<Thruster>();
		m_OwnerBooster.block.AttachedEvent.Subscribe(OnAttached);
		m_OwnerBooster.block.DetachingEvent.Subscribe(OnDetaching);
	}

	private void OnSpawn()
	{
		ResetAlignmentDirectionInstantly();
	}

	private void Update()
	{
		if (m_Enabled && !(m_OwnerBooster.block.tank == null) && !m_OwnerBooster.block.tank.beam.IsActive)
		{
			TryRecalculateAlignentRotation();
			MoveTowardsTargetRotation();
		}
	}

	[Conditional("UNITY_EDITOR")]
	private void OnDrawGizmosSelected()
	{
		TargetRotationCalculation targetRotationCalculation = ((!Application.isPlaying) ? TargetRotationCalculation.Calculate(base.transform, Vector3.forward, base.transform.localRotation, m_DefaultAngleOffset, m_MaxAngle) : m_TargetRotationCalcCache);
		Gizmos.color = Color.blue;
		Gizmos.color = Color.yellow;
		DebugUtil.GizmosDrawArrow(base.transform.position, base.transform.position + targetRotationCalculation.TargetRotation * Vector3.forward);
		Gizmos.color = Color.red;
		DebugUtil.GizmosDrawArrow(base.transform.position, base.transform.position + targetRotationCalculation.ClampedTargetRotationOnPlane * Vector3.forward);
	}
}
