using System.Collections.Generic;
using UnityEngine;

public class ForceGizmo : MonoBehaviour
{
	[Header("Geometry Defaults")]
	[SerializeField]
	protected Transform m_BallTrans;

	[SerializeField]
	protected float m_BallDefaultRadius;

	[SerializeField]
	protected Transform m_ArrowLineTrans;

	[SerializeField]
	protected float m_ArrowLineDefaultLength;

	[SerializeField]
	protected Transform m_ArrowTipTrans;

	[SerializeField]
	protected float m_ArrowTipDefaultLength;

	private float m_LowestForceVisualisationThreshold;

	private float m_ArrowLineUnitLength;

	private bool m_IsActive = true;

	private bool m_DisableIfZero;

	private static HashSet<ForceGizmo> s_SpawnedForceGizmos = new HashSet<ForceGizmo>();

	private const float k_MaxLineLength = 10f;

	private static float s_LineNormalisedScaleFactor = 1f;

	private float m_DesiredLineLength;

	private const float k_GenericForceScaler = 0.035f;

	private float m_ForceScaler = 1f;

	private Renderer[] m_Renderers;

	private MaterialPropertyBlock m_MatPropBlock;

	private int m_MPBColorPropID;

	public static ForceGizmo SpawnForceGizmo(Transform parent, Color color, bool disableIfZero, float forceScaler)
	{
		ForceGizmo forceGizmo = Globals.inst.m_ForceGizmoPrefab.Spawn(parent);
		forceGizmo.Init(color, disableIfZero, forceScaler);
		return forceGizmo;
	}

	public static void RescaleLinesToFitMaxSize()
	{
		float num = 0f;
		foreach (ForceGizmo s_SpawnedForceGizmo in s_SpawnedForceGizmos)
		{
			if (s_SpawnedForceGizmo.m_DesiredLineLength > num)
			{
				num = s_SpawnedForceGizmo.m_DesiredLineLength;
			}
		}
		float num2 = ((num > 10f) ? (10f / num) : 1f);
		if (num2 == s_LineNormalisedScaleFactor)
		{
			return;
		}
		s_LineNormalisedScaleFactor = num2;
		foreach (ForceGizmo s_SpawnedForceGizmo2 in s_SpawnedForceGizmos)
		{
			s_SpawnedForceGizmo2.SetLineLength(s_SpawnedForceGizmo2.m_DesiredLineLength);
		}
	}

	public void Init(Color color, bool disableIfZero, float forceScaler)
	{
		m_ForceScaler = forceScaler;
		m_DisableIfZero = disableIfZero;
		m_MatPropBlock.SetColor(m_MPBColorPropID, color);
		Renderer[] renderers = m_Renderers;
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].SetPropertyBlock(m_MatPropBlock);
		}
		SetForceVector(Vector3.zero, Vector3.zero);
	}

	public void SetForceVector(Vector3 localOrigin, Vector3 forceVector)
	{
		base.transform.localPosition = localOrigin;
		SetActive(!m_DisableIfZero || forceVector.sqrMagnitude > 0.0001f);
		if (!m_IsActive)
		{
			SetLineLength(0f);
			return;
		}
		base.transform.rotation = Quaternion.LookRotation(forceVector);
		SetLineLength(forceVector.magnitude * 0.035f * m_ForceScaler);
	}

	private void SetLineLength(float length)
	{
		m_DesiredLineLength = length;
		length *= s_LineNormalisedScaleFactor;
		bool flag = length >= m_LowestForceVisualisationThreshold;
		if (!flag)
		{
			m_ArrowLineTrans.gameObject.SetActive(value: false);
		}
		else if (!m_ArrowLineTrans.gameObject.activeSelf)
		{
			m_ArrowLineTrans.gameObject.SetActive(value: true);
		}
		float num = length - m_LowestForceVisualisationThreshold;
		if (flag)
		{
			m_ArrowLineTrans.localScale = m_ArrowLineTrans.localScale.SetZ(num / m_ArrowLineUnitLength);
		}
		m_ArrowTipTrans.localPosition = m_ArrowTipTrans.localPosition.SetZ(flag ? (m_ArrowLineTrans.localPosition.z + num * m_ArrowLineUnitLength) : m_BallDefaultRadius);
		RescaleLinesToFitMaxSize();
	}

	private void SetActive(bool state)
	{
		m_IsActive = state;
		m_BallTrans.gameObject.SetActive(state);
		m_ArrowLineTrans.gameObject.SetActive(state);
		m_ArrowTipTrans.gameObject.SetActive(state);
	}

	private void OnPool()
	{
		m_Renderers = GetComponentsInChildren<Renderer>();
		m_MatPropBlock = new MaterialPropertyBlock();
		m_MPBColorPropID = Shader.PropertyToID("_Color");
		m_LowestForceVisualisationThreshold = m_BallDefaultRadius + m_ArrowTipDefaultLength;
		m_ArrowLineUnitLength = m_ArrowLineDefaultLength * (1f / m_ArrowLineTrans.localScale.z);
		Init(Color.white, disableIfZero: true, 1f);
	}

	private void OnSpawn()
	{
		m_DesiredLineLength = 0f;
		s_SpawnedForceGizmos.Add(this);
		SetActive(state: true);
		SetForceVector(Vector3.zero, Vector3.zero);
	}

	private void OnRecycle()
	{
		s_SpawnedForceGizmos.Remove(this);
	}
}
