using UnityEngine;

public class WaypointOverlay : Overlay
{
	private WaypointOverlayData m_Data;

	private LocatorPanel m_PanelInst;

	public TrackedVisible TrackedVis { get; set; }

	public WaypointOverlay(WaypointOverlayData data)
	{
		TrackedVis = null;
		m_Data = data;
	}

	public override void Update()
	{
		if ((TrackedVis.visible == null || TrackedVis.visible.tank == null) && TrackedVis != null && m_Data.VisibleInCurrentMode && TrackedVis.WaypointOverlayEnabled && (Singleton.cameraTrans.position - TrackedVis.Position).sqrMagnitude >= m_Data.m_PanelMinDisplayDistance * m_Data.m_PanelMinDisplayDistance)
		{
			SpawnAndShowPanel();
			Vector3 position = TrackedVis.Position;
			m_PanelInst.PointToWorldPosition(position);
		}
		else
		{
			PerformCleanup();
		}
	}

	public override void PerformCleanup()
	{
		if (m_PanelInst != null)
		{
			m_PanelInst.Recycle();
			m_PanelInst = null;
		}
	}

	public override bool HasExpired()
	{
		return false;
	}

	private void SpawnAndShowPanel()
	{
		if (m_PanelInst == null)
		{
			m_PanelInst = m_Data.m_PanelPrefab.Spawn();
			m_PanelInst.Format(m_Data.m_IconSetup.m_Sprite, m_Data.m_IconSetup.m_Color, m_Data.m_PinSprite, m_Data.m_IconSetup.m_Color, TechWeapon.ManualTargetingReticuleState.NotTargeted);
		}
	}
}
