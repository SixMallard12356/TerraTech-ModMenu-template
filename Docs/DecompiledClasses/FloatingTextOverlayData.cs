using UnityEngine;

public class FloatingTextOverlayData : OverlayBaseData
{
	public FloatingTextPanel m_PanelPrefab;

	public float m_FadeOutTime;

	public float m_StayTime;

	public float m_AboveDist;

	public float m_MinCameraResizeDist;

	public float m_MaxCameraResizeDist = 30f;

	public AnimationCurve m_CamResizeCurve;
}
