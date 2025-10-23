using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RenderQualityTweaks : MonoBehaviour
{
	[SerializeField]
	private bool m_IsUnimportantShadowCaster = true;

	[SerializeField]
	private float m_LimitLODCullScreenSize;

	private List<MeshRenderer> m_MeshRenderers = new List<MeshRenderer>();

	private LODGroup m_LODGroup;

	private float m_DefaultLODCullSize;

	private void UpdateQualitySettings()
	{
		if (m_IsUnimportantShadowCaster)
		{
			foreach (MeshRenderer meshRenderer in m_MeshRenderers)
			{
				if ((bool)meshRenderer)
				{
					meshRenderer.shadowCastingMode = (QualitySettingsExtended.UnimportantShadowCasters ? ShadowCastingMode.On : ShadowCastingMode.Off);
				}
			}
		}
		if (m_LimitLODCullScreenSize > 0f && (bool)m_LODGroup)
		{
			LOD[] lODs = m_LODGroup.GetLODs();
			lODs[lODs.Length - 1].screenRelativeTransitionHeight = Mathf.Min(m_DefaultLODCullSize, m_LimitLODCullScreenSize * QualitySettings.lodBias);
			m_LODGroup.SetLODs(lODs);
		}
	}

	private void OnPool()
	{
		base.transform.GetComponentsInChildren(m_MeshRenderers);
		if (m_LimitLODCullScreenSize > 0f)
		{
			m_LODGroup = GetComponent<LODGroup>();
			if ((bool)m_LODGroup)
			{
				LOD[] lODs = m_LODGroup.GetLODs();
				m_DefaultLODCullSize = lODs[lODs.Length - 1].screenRelativeTransitionHeight;
			}
		}
		m_MeshRenderers.RemoveAll((MeshRenderer i) => i.shadowCastingMode != ShadowCastingMode.On);
		UpdateQualitySettings();
		QualitySettingsExtended.QualitySettingChangedEvent.Subscribe(UpdateQualitySettings);
	}

	private void OnDepool()
	{
		QualitySettingsExtended.QualitySettingChangedEvent.Unsubscribe(UpdateQualitySettings);
	}
}
