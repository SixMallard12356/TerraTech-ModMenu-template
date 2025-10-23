using UnityEngine;

public abstract class WorldObjectOverlayData : OverlayBaseData
{
	public OverlayPanel m_PanelPrefab;

	public float m_ZPos = 1f;

	public Material m_LineMat;

	public Vector2 m_PanelOffset;

	public float m_PanelMaxDisplayDistance = 200f;

	public bool m_ScaleWithDistance = true;

	public abstract Vector3 GetTargetPosition(object context);

	public abstract bool ShouldDisplay(object context);
}
