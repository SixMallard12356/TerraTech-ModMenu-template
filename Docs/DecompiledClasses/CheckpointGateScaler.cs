using System.Collections.Generic;
using UnityEngine;

public class CheckpointGateScaler : MonoBehaviour, ICheckpointVisualer
{
	[SerializeField]
	private Vector3[] m_UpcomingScales;

	[SerializeField]
	private Vector3[] m_PassedScales;

	[SerializeField]
	private float m_AnimDuration = 0.5f;

	[SerializeField]
	private bool m_ScaleUpOnStart = true;

	[SerializeField]
	private bool m_ScaleDownOnCleanup;

	private Transform m_Transform;

	private Vector3 m_InitialGateScale;

	private float m_MaxScaleStep;

	private Vector3 m_ScaleTarget;

	private List<Collider> m_Colliders;

	public void Initialise(Checkpoint checkpoint, int relativeCheckpointIndex, float time, int numFutureGatesToShow)
	{
		m_InitialGateScale = m_Transform.localScale;
		Vector3 targetScaleForRelativeCheckpointIndex = GetTargetScaleForRelativeCheckpointIndex(relativeCheckpointIndex, numFutureGatesToShow);
		bool flag = true;
		if (m_ScaleUpOnStart)
		{
			m_Transform.localScale = Vector3.zero;
			flag = false;
		}
		float animTime = (flag ? 0f : m_AnimDuration);
		SetTargetScale(targetScaleForRelativeCheckpointIndex, animTime);
	}

	public void RelativeIndexUpdated(int relativeCheckpointIndex, int numFutureGatesToShow)
	{
		Vector3 targetScaleForRelativeCheckpointIndex = GetTargetScaleForRelativeCheckpointIndex(relativeCheckpointIndex, numFutureGatesToShow);
		SetTargetScale(targetScaleForRelativeCheckpointIndex, m_AnimDuration);
	}

	public void StartCleanup()
	{
		if (m_ScaleDownOnCleanup)
		{
			SetTargetScale(Vector3.zero, m_AnimDuration);
		}
	}

	public bool IsReadyWithCleanup()
	{
		bool flag = m_MaxScaleStep == 0f;
		return !m_ScaleDownOnCleanup || flag;
	}

	private void SetTargetScale(Vector3 targetScale, float animTime = 0f)
	{
		if (animTime == 0f || targetScale == m_Transform.localScale)
		{
			m_Transform.localScale = targetScale;
			if (m_MaxScaleStep > 0f)
			{
				SetCollidersEnabled(enabled: true);
			}
		}
		else
		{
			m_MaxScaleStep = (m_Transform.localScale - targetScale).magnitude / animTime;
			SetCollidersEnabled(enabled: false);
		}
		m_ScaleTarget = targetScale;
	}

	private Vector3 GetTargetScaleForRelativeCheckpointIndex(int relativeCheckpointIndex, int numFutureGatesToShow)
	{
		Vector3 result = Vector3.one;
		int num = numFutureGatesToShow - 1;
		if (relativeCheckpointIndex > num && m_UpcomingScales.Length != 0)
		{
			int value = relativeCheckpointIndex - num - 1;
			value = Mathf.Clamp(value, 0, m_UpcomingScales.Length - 1);
			result = m_UpcomingScales[value];
		}
		else if (relativeCheckpointIndex < 0 && m_PassedScales.Length != 0)
		{
			int a = relativeCheckpointIndex * -1 - 1;
			a = Mathf.Min(a, m_PassedScales.Length - 1);
			result = m_PassedScales[a];
		}
		result.Scale(m_InitialGateScale);
		return result;
	}

	private void SetCollidersEnabled(bool enabled)
	{
		for (int i = 0; i < m_Colliders.Count; i++)
		{
			m_Colliders[i].enabled = enabled;
		}
	}

	private void OnPool()
	{
		m_Transform = base.transform;
		m_Colliders = new List<Collider>();
		GetComponentsInChildren(m_Colliders);
		for (int num = m_Colliders.Count - 1; num >= 0; num--)
		{
			if (m_Colliders[num].isTrigger || !m_Colliders[num].enabled)
			{
				m_Colliders.RemoveAt(num);
			}
		}
	}

	private void OnSpawn()
	{
		m_Transform.localScale = m_InitialGateScale;
		m_MaxScaleStep = 0f;
		SetCollidersEnabled(enabled: true);
	}

	private void Update()
	{
		if (m_MaxScaleStep > 0f && m_AnimDuration > 0f && m_Transform.localScale != m_ScaleTarget)
		{
			float maxDistanceDelta = m_MaxScaleStep * Time.deltaTime;
			m_Transform.localScale = Vector3.MoveTowards(m_Transform.localScale, m_ScaleTarget, maxDistanceDelta);
			if (m_Transform.localScale == m_ScaleTarget)
			{
				m_MaxScaleStep = 0f;
				SetCollidersEnabled(enabled: true);
			}
		}
	}
}
