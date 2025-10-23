#define UNITY_EDITOR
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class FanJet : Thruster, IWorldTreadmill
{
	[Tooltip("how much oomph have I got (in the other direction)")]
	[SerializeField]
	protected float backForce = 250f;

	[SerializeField]
	[Tooltip("how quickly fire rate can change")]
	protected float spinDelta = 0.1f;

	[SerializeField]
	protected float spinSpeed = 1f;

	[SerializeField]
	[Tooltip("For legacy purposes will default to first Spinner child in parent block")]
	protected Spinner m_FanSpinner;

	[SerializeField]
	public bool m_DisableTrail;

	[HideInInspector]
	[SerializeField]
	private TrailRenderer m_Trail;

	protected override float GetForceToApply(float actuationT)
	{
		float num = ((actuationT > 0f) ? m_Force : backForce) * actuationT;
		if (!base.IsAttachedToTank)
		{
			num *= m_DetachedForceFactor;
		}
		return num;
	}

	protected override float ValidateActuationRate(float unvalidatedSpin)
	{
		return Mathf.Clamp(unvalidatedSpin, -1f, 1f);
	}

	private void PrePool()
	{
		m_Trail = base.transform.root.GetComponentInChildren<TrailRenderer>();
	}

	private void OnPool()
	{
		if (m_FanSpinner == null)
		{
			m_FanSpinner = m_ParentBlock.GetComponentsInChildren<Spinner>(includeInactive: true).FirstOrDefault();
			if (m_FanSpinner == null)
			{
				d.LogWarning("FanJet on" + m_ParentBlock.name + " found no fan geometry (Spinner), did you mean to add some?");
			}
		}
	}

	private void OnSpawn()
	{
		if ((bool)m_Trail)
		{
			Singleton.Manager<ManWorldTreadmill>.inst.AddListener(this);
		}
	}

	private void OnRecycle()
	{
		if ((bool)m_Trail)
		{
			Singleton.Manager<ManWorldTreadmill>.inst.RemoveListener(this);
			m_Trail.enabled = false;
			m_Trail.Clear();
		}
	}

	protected override void OnUpdate()
	{
		if (m_FanSpinner != null)
		{
			m_FanSpinner.UpdateSpin(m_CurrentThrustRate * spinSpeed * Time.deltaTime);
		}
		base.OnUpdate();
	}

	protected override void OnFixedUpdate()
	{
		if (!base.IsAttachedToTank)
		{
			m_TargetThrustRate = 0f;
		}
		m_CurrentThrustRate = Mathf.MoveTowards(m_CurrentThrustRate, m_TargetThrustRate, spinDelta * Time.deltaTime);
		m_LastForce = QueryBoostThrustVector(m_CurrentThrustRate, out var centerOfBoosterThrustWorld);
		bool flag = m_LastForce != Vector3.zero;
		if (flag)
		{
			m_TargetRigidbody.AddForceAtPosition(m_LastForce, centerOfBoosterThrustWorld);
		}
		if ((bool)m_Trail)
		{
			m_Trail.enabled = flag;
			m_Trail.emitting = flag && !m_DisableTrail;
		}
		base.OnFixedUpdate();
	}

	public void OnMoveWorldOrigin(IntVector3 amountToMove)
	{
		if ((bool)m_Trail && m_Trail.enabled && m_Trail.positionCount > 0)
		{
			Vector3 vector = new Vector3(amountToMove.x, amountToMove.y, amountToMove.z);
			Vector3[] array = new Vector3[m_Trail.positionCount];
			m_Trail.GetPositions(array);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] += vector;
			}
			m_Trail.SetPositions(array);
		}
	}
}
