using UnityEngine;

public class ModulePlacementZoneEffect : Module
{
	[SerializeField]
	private float m_Radius = 2f;

	[SerializeField]
	private GameObject m_VFXBubble;

	[SerializeField]
	private float m_VFXBubbleRadiusFudgeFactor = 1f;

	private void OnPool()
	{
		if ((bool)m_VFXBubble)
		{
			m_VFXBubble.transform.SetLocalScaleIfChanged(m_Radius * m_VFXBubbleRadiusFudgeFactor * 2f * Vector3.one);
			base.block.BlockUpdate.Subscribe(OnUpdate);
		}
	}

	private void OnSpawn()
	{
		m_VFXBubble.SetActive(base.block.IsBeingDragged);
	}

	private void OnUpdate()
	{
		if ((bool)m_VFXBubble)
		{
			if (base.block.IsBeingDragged != m_VFXBubble.activeSelf)
			{
				m_VFXBubble.SetActive(base.block.IsBeingDragged);
			}
			if (!Mathf.Approximately(m_VFXBubble.transform.localScale.x, m_Radius))
			{
				m_VFXBubble.transform.SetLocalScaleIfChanged(m_Radius * m_VFXBubbleRadiusFudgeFactor * 2f * Vector3.one);
			}
		}
	}
}
