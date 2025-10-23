#define UNITY_EDITOR
using UnityEngine;

public class RadarInfoPanel : OverlayPanel
{
	[SerializeField]
	private UIItemDisplay m_SpecificItemDisplay;

	private ModuleRadar m_Radar;

	private ChunkTypes m_DisplayedChunkType;

	public override void SetContext(object context)
	{
		m_Radar = context as ModuleRadar;
		d.Assert(context == null || m_Radar != null, "RadarInfoPanel - Failed to cast context to ModuleRadar");
		SetupPanel();
	}

	private void SetupPanel()
	{
		if (m_Radar != null)
		{
			m_DisplayedChunkType = m_Radar.ResourceType;
			m_SpecificItemDisplay.Setup(new ItemTypeInfo(ObjectTypes.Chunk, (int)m_DisplayedChunkType));
		}
	}

	public override void RefreshPanel(object context)
	{
		if ((bool)m_Radar && m_DisplayedChunkType != m_Radar.ResourceType)
		{
			SetupPanel();
		}
	}
}
