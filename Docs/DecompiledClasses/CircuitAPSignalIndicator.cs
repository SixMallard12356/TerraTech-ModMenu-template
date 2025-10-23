#define UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CircuitAPSignalIndicator : MonoBehaviour
{
	[SerializeField]
	protected Vector3 m_AttachPoint;

	[EnumFlag]
	[SerializeField]
	protected ModuleCircuitNode.ConnexionTypes m_ConnexionDisplayFlags = (ModuleCircuitNode.ConnexionTypes)(-1);

	[SerializeField]
	[ColorUsage(false, true)]
	protected Color m_ActiveEmissionColor = Color.white;

	protected Renderer[] m_IndicatorRenderers;

	protected Animation m_IndicatorAnimation;

	protected TankBlock m_Block;

	private bool m_Initted;

	private bool m_IsActive;

	private bool m_VisualsActive;

	private AnimationState m_AnimState;

	public ModuleCircuitNode.ConnexionTypes ConnexionDisplayFlags => m_ConnexionDisplayFlags;

	public void InitOnBlock(TankBlock block)
	{
		m_Block = block;
	}

	public static bool TryRefreshIndicators(IEnumerable<Vector3> signalValuesOnAPs, CircuitAPSignalIndicator[] indicators)
	{
		bool flag = false;
		if (signalValuesOnAPs.Count() == 0)
		{
			CircuitAPSignalIndicator[] array = indicators;
			foreach (CircuitAPSignalIndicator circuitAPSignalIndicator in array)
			{
				circuitAPSignalIndicator.SetActive(state: false);
				flag |= circuitAPSignalIndicator.TryRefreshVisuals();
			}
		}
		else
		{
			CircuitAPSignalIndicator[] array = indicators;
			foreach (CircuitAPSignalIndicator circuitAPSignalIndicator2 in array)
			{
				circuitAPSignalIndicator2.SetActive(circuitAPSignalIndicator2.m_AttachPoint == Vector3.zero || signalValuesOnAPs.Contains(circuitAPSignalIndicator2.m_AttachPoint));
				flag |= circuitAPSignalIndicator2.TryRefreshVisuals();
			}
		}
		return flag;
	}

	public void SetActive(bool state)
	{
		m_IsActive = state;
	}

	public bool TryRefreshVisuals(bool forceRefresh = false)
	{
		TryInitIndicators();
		bool flag = m_IsActive != m_VisualsActive || forceRefresh;
		if (!flag)
		{
			return flag;
		}
		m_VisualsActive = m_IsActive;
		RefreshAnimState();
		RefreshRendererState();
		return flag;
	}

	private void RefreshAnimState()
	{
		if (!(m_IndicatorAnimation == null))
		{
			m_AnimState.normalizedTime = (m_IsActive ? 1 : 0);
			if (!m_IndicatorAnimation.isPlaying)
			{
				m_IndicatorAnimation.Play();
			}
		}
	}

	private void RefreshRendererState()
	{
		d.Assert(m_Block != null, "Trying to use indicator without initialising it first! AAAA!");
		if (m_IsActive)
		{
			m_Block.RegisterVariableColours(m_IndicatorRenderers, Color.white, m_ActiveEmissionColor);
		}
		else
		{
			m_Block.ClearVariableColours(m_IndicatorRenderers);
		}
	}

	private void TryInitIndicators()
	{
		if (m_Initted)
		{
			return;
		}
		m_Initted = true;
		m_IndicatorRenderers = GetComponents<Renderer>();
		m_IndicatorAnimation = GetComponent<Animation>();
		if (!(m_IndicatorAnimation != null) || !(m_AnimState == null))
		{
			return;
		}
		foreach (AnimationState item in m_IndicatorAnimation)
		{
			m_AnimState = item;
		}
		m_AnimState.normalizedSpeed = 0f;
		m_AnimState.wrapMode = WrapMode.ClampForever;
	}
}
