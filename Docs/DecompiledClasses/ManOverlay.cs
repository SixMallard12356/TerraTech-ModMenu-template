#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class ManOverlay : Singleton.Manager<ManOverlay>
{
	[SerializeField]
	private TankDescriptionData m_TankOverlayData;

	[SerializeField]
	private WaypointOverlayData m_WaypointOverlayData;

	[SerializeField]
	private InfoOverlayData m_TooltipOverlayData;

	[SerializeField]
	private InfoOverlayData m_SpawnedBlockOverlayData;

	[SerializeField]
	private InfoOverlayData m_WarningOverlayData;

	[SerializeField]
	private FloatingTextOverlayData m_ConsumptionAddMoneyOverlayData;

	[SerializeField]
	private FloatingTextOverlayData m_AddXPOverlayData;

	[SerializeField]
	private CraftingOverlayData m_CraftingOverlayData;

	[SerializeField]
	private RefiningOverlayData m_RefiningOverlayData;

	[SerializeField]
	private FilterInfoOverlayData m_FilterOverlayData;

	[SerializeField]
	private RadarOverlayData m_RadarOverlayData;

	[SerializeField]
	private GameObject m_TooltipPrefab;

	[SerializeField]
	private GameObject m_TooltipPrefabSwitch;

	[EnumArray(typeof(WarningHolder.WarningType))]
	[SerializeField]
	private AnimationCurve[] m_WarningPossibleFrequency;

	[EnumArray(typeof(WarningHolder.WarningType))]
	[SerializeField]
	private bool[] m_WarningShowWhileBuilding;

	[SerializeField]
	private float m_TooltipAppearDelay = 2f;

	[SerializeField]
	private float m_TooltipDisappearDelay = 2f;

	[SerializeField]
	private float m_TooltipAppearDelayGamepad = 1f;

	[SerializeField]
	public float m_GunFireCheckTime = 1f;

	private List<Overlay> m_ActiveOverlays = new List<Overlay>();

	private List<InfoOverlay> m_QueuedOverlays = new List<InfoOverlay>();

	private List<WaypointOverlay> m_Waypoints = new List<WaypointOverlay>();

	private InfoOverlay m_CurrentInfoOverlay;

	private UITooltipNew m_Tooltip;

	private Visible m_TooltipVisible;

	private float m_TooltipTimer;

	private InfoOverlay m_TooltipOverlay;

	private bool m_TooltipOverlayIsQueued;

	public bool ForceHideWaypointOverlay { get; set; }

	public TankDescriptionOverlay AddTankOverlay(Tank tank, TankDescriptionData overridePrefab = null)
	{
		TankDescriptionData tankDescriptionData = ((overridePrefab != null) ? overridePrefab : m_TankOverlayData);
		TankDescriptionOverlay tankDescriptionOverlay = new TankDescriptionOverlay(tank, tankDescriptionData);
		if (tankDescriptionData.VisibleInCurrentMode)
		{
			AddActiveOverlay(tankDescriptionOverlay);
		}
		return tankDescriptionOverlay;
	}

	public WorldPosition WorldPositionForFloatingText(Visible subject)
	{
		float num = ((subject.type == ObjectTypes.Vehicle) ? (subject.tank.blockBounds.extents.y + 0.5f) : subject.Radius);
		return WorldPosition.FromScenePosition(subject.centrePosition + Vector3.up * num);
	}

	public FloatingTextOverlay AddFloatingTextOverlay(string text, WorldPosition position)
	{
		FloatingTextOverlay floatingTextOverlay = new FloatingTextOverlay(m_ConsumptionAddMoneyOverlayData);
		floatingTextOverlay.Set(text, position);
		if (m_ConsumptionAddMoneyOverlayData.VisibleInCurrentMode)
		{
			AddActiveOverlay(floatingTextOverlay);
		}
		return floatingTextOverlay;
	}

	public FloatingTextOverlay AddFloatingTextOverlayXP(int xp, WorldPosition position)
	{
		FloatingTextOverlay floatingTextOverlay = new FloatingTextOverlay(m_AddXPOverlayData);
		string moneyString = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HUD, 162), xp);
		floatingTextOverlay.Set(moneyString, position);
		if (m_AddXPOverlayData.VisibleInCurrentMode)
		{
			AddActiveOverlay(floatingTextOverlay);
		}
		return floatingTextOverlay;
	}

	public InfoOverlay AddBlockSpawnedOverlay(Visible discoveredBlock)
	{
		InfoOverlay infoOverlay = new InfoOverlay(m_SpawnedBlockOverlayData);
		infoOverlay.Setup(discoveredBlock);
		if (m_SpawnedBlockOverlayData.VisibleInCurrentMode)
		{
			AddQueuedOverlay(infoOverlay);
		}
		return infoOverlay;
	}

	public InfoOverlay AddWarningOverlay(Visible attachedVisible, bool showWhileBuilding)
	{
		InfoOverlay infoOverlay = new InfoOverlay(m_WarningOverlayData);
		infoOverlay.Setup(attachedVisible);
		infoOverlay.ShowWhileBuilding = showWhileBuilding;
		if (m_WarningOverlayData.VisibleInCurrentMode)
		{
			AddQueuedOverlay(infoOverlay);
		}
		return infoOverlay;
	}

	public WorldObjectOverlay AddCraftingOverlay(ModuleItemConsume consume)
	{
		WorldObjectOverlay worldObjectOverlay = new WorldObjectOverlay(consume, m_CraftingOverlayData);
		if (m_CraftingOverlayData.VisibleInCurrentMode)
		{
			AddActiveOverlay(worldObjectOverlay);
		}
		return worldObjectOverlay;
	}

	public WorldObjectOverlay AddRefiningOverlay(ModuleItemConsume consume)
	{
		WorldObjectOverlay worldObjectOverlay = new WorldObjectOverlay(consume, m_RefiningOverlayData);
		if (m_RefiningOverlayData.VisibleInCurrentMode)
		{
			AddActiveOverlay(worldObjectOverlay);
		}
		return worldObjectOverlay;
	}

	public WorldObjectOverlay AddFilterOverlay(ModuleItemFilter filter)
	{
		WorldObjectOverlay worldObjectOverlay = new WorldObjectOverlay(filter, m_FilterOverlayData);
		if (m_FilterOverlayData.VisibleInCurrentMode)
		{
			AddActiveOverlay(worldObjectOverlay);
		}
		return worldObjectOverlay;
	}

	public WorldObjectOverlay AddRadarOverlay(ModuleRadar radar)
	{
		WorldObjectOverlay worldObjectOverlay = new WorldObjectOverlay(radar, m_RadarOverlayData);
		if (m_FilterOverlayData.VisibleInCurrentMode)
		{
			AddActiveOverlay(worldObjectOverlay);
		}
		return worldObjectOverlay;
	}

	public void RemoveTankOverlay(TankDescriptionOverlay tankOverlay)
	{
		RemoveActiveOverlay(tankOverlay);
	}

	public void RemoveFloatingTextOverlay(FloatingTextOverlay textOverlay)
	{
		RemoveActiveOverlay(textOverlay);
	}

	public void RemoveWarningOverlay(InfoOverlay infoOverlay)
	{
		RemoveQueuedOverlay(infoOverlay);
	}

	public void RemoveObjectOverlay(WorldObjectOverlay worldObjectOverlay)
	{
		RemoveActiveOverlay(worldObjectOverlay);
	}

	public float LookupWarningAppearDelay(int curWarningCount, WarningHolder.WarningType warningType)
	{
		float result = 0f;
		if (warningType >= WarningHolder.WarningType.GunLineOfSight && (int)warningType < m_WarningPossibleFrequency.Length)
		{
			result = m_WarningPossibleFrequency[(int)warningType].Evaluate(curWarningCount);
		}
		return result;
	}

	public bool ShowWarningWhileBuilding(WarningHolder.WarningType warningType)
	{
		bool result = false;
		if (warningType >= WarningHolder.WarningType.GunLineOfSight && (int)warningType < m_WarningShowWhileBuilding.Length)
		{
			result = m_WarningShowWhileBuilding[(int)warningType];
		}
		return result;
	}

	public void AddToolTip(TooltipComponent tooltipProvider)
	{
		if ((bool)m_Tooltip)
		{
			m_Tooltip.ShowToolTip(tooltipProvider);
		}
	}

	public void RemoveToolTip(TooltipComponent tooltipProvider)
	{
		if ((bool)m_Tooltip)
		{
			m_Tooltip.HideToolTip(tooltipProvider);
		}
	}

	public void AddToolTip(UITooltipNew.TooltipInfo tooltipInfo)
	{
		if ((bool)m_Tooltip)
		{
			m_Tooltip.ShowToolTip(tooltipInfo);
		}
	}

	public void RemoveToolTip()
	{
		if ((bool)m_Tooltip)
		{
			m_Tooltip.HideToolTip();
		}
	}

	public void AddWaypointOverlay(TrackedVisible trackedVis, WaypointOverlayData data = null)
	{
		WaypointOverlay waypointOverlay = m_Waypoints.Find((WaypointOverlay searchOverlay) => searchOverlay.TrackedVis == trackedVis);
		d.Assert(waypointOverlay == null, "ManOverlayNew::AddWaypointOverlay() - adding the same tracked visible twice (" + trackedVis.ID + ")");
		if (waypointOverlay == null)
		{
			WaypointOverlay waypointOverlay2 = new WaypointOverlay(data ? data : m_WaypointOverlayData);
			waypointOverlay2.TrackedVis = trackedVis;
			m_Waypoints.Add(waypointOverlay2);
			AddActiveOverlay(waypointOverlay2);
		}
	}

	public void RemoveWaypointOverlay(TrackedVisible trackedVis, bool allowMissing = false)
	{
		int num = m_Waypoints.FindIndex((WaypointOverlay searchOverlay) => searchOverlay.TrackedVis == trackedVis);
		if (num >= 0)
		{
			RemoveActiveOverlay(m_Waypoints[num]);
			m_Waypoints.RemoveAt(num);
		}
		else
		{
			d.Assert(allowMissing, "ManOverlayNew::RemoveWaypointOverlay() - cannot find waypoint pointing at visible with ID (" + trackedVis.ID + ")");
		}
	}

	public void RemoveAllWaypointOverlays()
	{
		for (int i = 0; i < m_Waypoints.Count; i++)
		{
			RemoveActiveOverlay(m_Waypoints[i]);
		}
		m_Waypoints.Clear();
	}

	public void ClearAll()
	{
		while (m_QueuedOverlays.Count > 0)
		{
			RemoveQueuedOverlay(m_QueuedOverlays[0]);
		}
		while (m_ActiveOverlays.Count > 0)
		{
			RemoveActiveOverlay(m_ActiveOverlays[0]);
		}
		m_Waypoints.Clear();
		m_CurrentInfoOverlay = null;
		m_TooltipVisible = null;
		m_TooltipTimer = 0f;
		m_TooltipOverlay = null;
		m_TooltipOverlayIsQueued = false;
	}

	public void DismissAllInfos()
	{
		if (m_CurrentInfoOverlay != null)
		{
			RemoveQueuedOverlay(m_CurrentInfoOverlay);
		}
		while (m_QueuedOverlays.Count > 0)
		{
			RemoveQueuedOverlay(m_QueuedOverlays[0]);
		}
	}

	private void Update()
	{
		DisplayTooltipIfPointedAt();
		SwitchToHighestPriorityInfoOverlay();
		int num = 0;
		while (num < m_ActiveOverlays.Count)
		{
			Overlay overlay = m_ActiveOverlays[num];
			overlay.Update();
			if (overlay.HasExpired())
			{
				RemoveActiveOverlay(overlay);
			}
			else
			{
				num++;
			}
		}
	}

	private void SwitchToHighestPriorityInfoOverlay()
	{
		InfoOverlay infoOverlay = m_CurrentInfoOverlay;
		for (int i = 0; i < m_QueuedOverlays.Count; i++)
		{
			InfoOverlay infoOverlay2 = m_QueuedOverlays[i];
			if ((infoOverlay == null || infoOverlay2.HasPriorityOver(infoOverlay)) && infoOverlay2.CanShowNow())
			{
				infoOverlay = infoOverlay2;
			}
		}
		if (infoOverlay != m_CurrentInfoOverlay)
		{
			if (m_CurrentInfoOverlay != null)
			{
				m_CurrentInfoOverlay.OnHide();
				m_QueuedOverlays.Add(m_CurrentInfoOverlay);
				RemoveActiveOverlay(m_CurrentInfoOverlay);
			}
			if (infoOverlay != null)
			{
				m_QueuedOverlays.Remove(infoOverlay);
				m_ActiveOverlays.Add(infoOverlay);
				infoOverlay.OnShow();
			}
			m_CurrentInfoOverlay = infoOverlay;
		}
	}

	private void DisplayTooltipIfPointedAt()
	{
		Visible visible = Singleton.Manager<ManPointer>.inst.targetVisible;
		if (Singleton.Manager<ManPointer>.inst.DraggingItem != null)
		{
			visible = null;
		}
		else if (visible != null && visible.block != null && visible.block.tank != null && !visible.block.tank.IsFriendly())
		{
			visible = null;
		}
		bool num = Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad();
		if (num && visible.IsNotNull() && !Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.InteractionMode))
		{
			visible = null;
		}
		bool flag = false;
		float num2 = ((m_TooltipVisible != null) ? m_TooltipAppearDelay : m_TooltipDisappearDelay);
		bool flag2 = false;
		if (num)
		{
			flag2 = Singleton.Manager<ManInput>.inst.GetButtonDown(27);
			num2 = ((visible != null && !flag2) ? m_TooltipAppearDelayGamepad : 0f);
			flag = visible == null || visible != m_TooltipVisible;
		}
		if (visible != m_TooltipVisible)
		{
			m_TooltipTimer = 0f;
			m_TooltipVisible = visible;
		}
		m_TooltipTimer += Time.deltaTime;
		if (m_TooltipTimer >= num2)
		{
			if (m_TooltipVisible != null)
			{
				if (m_TooltipOverlay == null)
				{
					m_TooltipOverlay = new InfoOverlay(m_TooltipOverlayData);
				}
				if (!m_TooltipOverlayIsQueued)
				{
					AddQueuedOverlay(m_TooltipOverlay);
					m_TooltipOverlayIsQueued = true;
				}
				m_TooltipOverlay.Setup(m_TooltipVisible);
				m_TooltipOverlay.ExpandedByDefault = flag2;
				m_TooltipOverlay.ResetDismissTimer();
			}
			else
			{
				flag = true;
			}
		}
		if (m_TooltipOverlay != null && flag)
		{
			RemoveQueuedOverlay(m_TooltipOverlay);
			m_TooltipOverlayIsQueued = false;
		}
	}

	private void AddActiveOverlay(Overlay overlay)
	{
		if (overlay != null)
		{
			m_ActiveOverlays.Add(overlay);
		}
	}

	private void RemoveActiveOverlay(Overlay overlay)
	{
		if (overlay != null)
		{
			m_ActiveOverlays.Remove(overlay);
			overlay.PerformCleanup();
			if (m_TooltipOverlay == overlay)
			{
				m_TooltipOverlay = null;
			}
			if (m_CurrentInfoOverlay == overlay)
			{
				m_CurrentInfoOverlay = null;
			}
		}
	}

	private void AddQueuedOverlay(InfoOverlay info)
	{
		if (info != null)
		{
			m_QueuedOverlays.Add(info);
		}
	}

	private void RemoveQueuedOverlay(InfoOverlay info)
	{
		if (info != null)
		{
			if (info == m_CurrentInfoOverlay)
			{
				RemoveActiveOverlay(info);
			}
			else
			{
				m_QueuedOverlays.Remove(info);
			}
			info.PerformCleanup();
		}
	}

	private void Start()
	{
		GameObject gameObject = (SKU.SwitchUI ? m_TooltipPrefabSwitch : m_TooltipPrefab);
		d.Assert(gameObject, "Missing tooltip prefab on " + base.name, this);
		d.Assert(gameObject.GetComponent<UITooltipNew>(), "Missing UITooltipNew component on " + gameObject?.name, this);
		m_Tooltip = Object.Instantiate(gameObject).GetComponent<UITooltipNew>();
		m_Tooltip.Init();
		Singleton.Manager<ManHUD>.inst.AddTooltip(m_Tooltip.transform as RectTransform);
	}
}
