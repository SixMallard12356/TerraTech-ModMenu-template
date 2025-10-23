#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using TerraTech.Network;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class UIMiniMapLayerTech : UIMiniMapLayer
{
	public enum AreaMarkers
	{
		Invasion,
		MultiplayerBounds
	}

	private class IconCache
	{
		public int numUsed;

		public List<UIMiniMapElement> icons;
	}

	private class ClosestIcons
	{
		private struct Rec
		{
			public TrackedVisible tracked;

			public float distSq;

			public Color colour;
		}

		private class TypeList
		{
			public Rec[] recs;

			public int count;
		}

		private TypeList[] m_TypeLists;

		public ClosestIcons()
		{
			m_TypeLists = new TypeList[ManRadar.IconTypeCount];
			for (int i = 0; i < m_TypeLists.Length; i++)
			{
				m_TypeLists[i] = new TypeList
				{
					recs = new Rec[Singleton.Manager<ManRadar>.inst.GetCountDisplayingPastRange((ManRadar.IconType)i)],
					count = 0
				};
			}
		}

		public void Reset()
		{
			for (int i = 0; i < m_TypeLists.Length; i++)
			{
				TypeList typeList = m_TypeLists[i];
				for (int j = 0; j < typeList.count; j++)
				{
					typeList.recs[j] = new Rec
					{
						tracked = null,
						distSq = 0f,
						colour = Color.white
					};
				}
				typeList.count = 0;
			}
		}

		public void TryAdd(ManRadar.IconType iconType, TrackedVisible tv, float distSq, Color colour)
		{
			TypeList typeList = m_TypeLists[(int)iconType];
			int num = typeList.recs.Length;
			if (num <= 0)
			{
				return;
			}
			int num2 = typeList.count;
			bool flag = num2 >= num;
			if (!flag || distSq < typeList.recs[num2 - 1].distSq)
			{
				int i;
				for (i = 0; i < num2 && distSq >= typeList.recs[i].distSq; i++)
				{
				}
				if (!flag)
				{
					num2 = (typeList.count = num2 + 1);
				}
				for (int num3 = num2 - 1; num3 > i; num3--)
				{
					typeList.recs[num3] = typeList.recs[num3 - 1];
				}
				typeList.recs[i] = new Rec
				{
					tracked = tv,
					distSq = distSq,
					colour = colour
				};
			}
		}

		public int GetCount(ManRadar.IconType type)
		{
			return m_TypeLists[(int)type].count;
		}

		public TrackedVisible GetTrackedVis(ManRadar.IconType type, int index)
		{
			return m_TypeLists[(int)type].recs[index].tracked;
		}

		public Color GetColour(ManRadar.IconType type, int index)
		{
			return m_TypeLists[(int)type].recs[index].colour;
		}
	}

	private struct TooltipConfig
	{
		public string text;

		public UITooltipOptions mode;

		public bool enabled => text != null;

		public static TooltipConfig Default => new TooltipConfig
		{
			text = null,
			mode = UITooltipOptions.Default
		};
	}

	[Serializable]
	public class MapAreaData
	{
		public RectTransform m_Prefab;

		[Range(0f, 1f)]
		[Tooltip("Size of the area (border) inside the root prefab. Expressed as AreaSize/RootSize (eg 140px border inside a 256px texture: (140/256)")]
		public float NormalisedGfxSize = 1f;
	}

	private abstract class MapArea
	{
		protected MapAreaData m_PrefabData;

		public abstract bool IsActive { get; }

		public abstract float Radius { get; }

		public abstract Vector3 WorldPosition { get; }

		public float GfxSize => Radius / NormalisedGfxSize;

		public virtual RectTransform Prefab => m_PrefabData.m_Prefab;

		protected virtual float NormalisedGfxSize => m_PrefabData.NormalisedGfxSize;

		public RectTransform SpawnedTrans { get; set; }

		public MapArea(MapAreaData prefabData)
		{
			m_PrefabData = prefabData;
		}
	}

	private class MapAreaInvasion : MapArea
	{
		public override bool IsActive
		{
			get
			{
				if (Singleton.Manager<ManInvasion>.inst.InvasionOccurring)
				{
					return Singleton.playerTank != null;
				}
				return false;
			}
		}

		public override float Radius => Singleton.Manager<ManInvasion>.inst.GetInvasionRange(includeChaseRange: false);

		public override Vector3 WorldPosition => Singleton.Manager<ManInvasion>.inst.GetInvasionPosWorld();

		public MapAreaInvasion(MapAreaData md)
			: base(md)
		{
		}
	}

	private class MapAreaMultiplayerBoundary : MapArea
	{
		public override bool IsActive => Singleton.Manager<ManNetwork>.inst.IsMultiplayer();

		public override float Radius => Singleton.Manager<ManNetwork>.inst.DangerDistance;

		public override Vector3 WorldPosition => Singleton.Manager<ManNetwork>.inst.MapCenter.GameWorldPosition;

		public MapAreaMultiplayerBoundary(MapAreaData md)
			: base(md)
		{
		}
	}

	[SerializeField]
	private bool m_DisplayDirectionOnly;

	[SerializeField]
	private bool m_DisplayRelativeToPlayer;

	[SerializeField]
	private bool m_LimitToIconsInRange = true;

	[SerializeField]
	private bool m_RestrictDistantIconCount = true;

	[SerializeField]
	private bool m_EnableIconTooltips;

	[SerializeField]
	private RectTransform m_InnerAreaMask;

	[EnumArray(typeof(AreaMarkers))]
	[SerializeField]
	private MapAreaData[] m_AreaPrefabs;

	[SerializeField]
	private int m_MaxPlayerDeathMarkers = -1;

	private UIQuestMarker m_QuestMarker;

	private List<MapArea> m_AreaMarkers = new List<MapArea>();

	private float m_RectRadius;

	private IconCache[] m_IconCache;

	private ClosestIcons m_ClosestIcons;

	public override void UpdateLayer()
	{
		if ((bool)Singleton.playerTank)
		{
			UpdateMapMarkers();
			UpdateAreaMarkers();
		}
	}

	private void UpdateMapMarkers()
	{
		bool circleVisible = false;
		bool blipVisible = false;
		ZeroIconCount();
		m_ClosestIcons.Reset();
		Vector3 scenePosition = m_MapDisplay.FocalPoint.ScenePosition;
		float num = (Singleton.playerTank ? Singleton.playerTank.Radar.GetRange(ModuleRadar.RadarScanType.Techs) : 0f);
		float num2 = num * num;
		float num3 = (m_MapDisplay.LimitDisplayRange ? m_MapDisplay.MaxDisplayRange : num);
		Visible visible = (Singleton.playerTank ? Singleton.playerTank.visible : null);
		TrackedVisible invaderTrackedVisible = Singleton.Manager<ManInvasion>.inst.GetInvaderTrackedVisible();
		TrackedVisible trackedVisible = null;
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetwork>.inst.NetController != null)
		{
			NetTech notableTech = Singleton.Manager<ManNetwork>.inst.NetController.GetNotableTech();
			if (notableTech != null)
			{
				trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(notableTech.tech.visible.ID);
			}
		}
		Visible visible2 = ((Singleton.playerTank != null) ? Singleton.playerTank.Weapons.GetManualTarget() : null);
		TrackedVisible trackedVisible2 = (visible2 ? Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visible2.ID) : null);
		TrackedVisible trackedVisible3 = ((!Singleton.Manager<ManQuestLog>.inst.HasTrackedEncounter) ? null : Singleton.Manager<ManQuestLog>.inst.GetTrackedEncounterDisplayData()?.ActiveQuestLog?.GetEncounterWaypoint());
		Dictionary<int, TrackedVisible>.Enumerator allTrackedVisiblesEnumerator = Singleton.Manager<ManVisible>.inst.AllTrackedVisiblesEnumerator;
		while (allTrackedVisiblesEnumerator.MoveNext())
		{
			TrackedVisible value = allTrackedVisiblesEnumerator.Current.Value;
			if ((bool)value.visible && value.visible == visible)
			{
				continue;
			}
			bool flag = value == trackedVisible3;
			bool isManualTarget = value == trackedVisible2;
			ManRadar.IconType outIconType;
			Color iconColour;
			bool flag2 = TryGetIconForTrackedVisible(value, flag, isManualTarget, out outIconType, out iconColour);
			if (value == trackedVisible)
			{
				outIconType = ManRadar.IconType.MultiplayerCrown;
				iconColour = Color.white;
				flag2 = true;
			}
			else if (ManSpawn.IsPlayerTeam(value.TeamID) && Singleton.Manager<ManPlayer>.inst.DoesTechHavePlayerHeartBlock(value))
			{
				if (!value.RadarMarkerConfig.IsUsed)
				{
					outIconType = ManRadar.IconType.HeartBase;
					iconColour = Color.white;
					flag2 = true;
				}
			}
			else if (invaderTrackedVisible != null && value.ID == invaderTrackedVisible.ID)
			{
				outIconType = ManRadar.IconType.Invader;
				iconColour = Color.white;
				flag2 = true;
			}
			if (!flag2)
			{
				continue;
			}
			Vector3 vector = (value.Position - scenePosition).SetY(0f);
			float distSq = vector.sqrMagnitude;
			bool flag3 = outIconType == ManRadar.IconType.AreaQuest;
			float num4 = 0f;
			if (flag3)
			{
				num4 = Singleton.Manager<ManEncounter>.inst.GetEncounterRadius(value.HostID);
				float num5 = Mathf.Max(0f, vector.magnitude - num4);
				distSq = num5 * num5;
			}
			bool flag4 = m_LimitToIconsInRange && !flag && outIconType != ManRadar.IconType.MapNavigation;
			float maxRangeSqr = (flag4 ? num2 : num3);
			Vector2 iconPosition2D = default(Vector2);
			bool flag5 = !m_DisplayDirectionOnly && CalculateIconPositionFromScene(value.Position, m_LimitToIconsInRange, maxRangeSqr, num4, out iconPosition2D);
			bool num6 = flag5 || CalculateIconDirectionFromScene(value.Position, flag4, num2, m_RectRadius, out iconPosition2D);
			bool flag6 = flag && !flag5 && !Singleton.Manager<ManRadar>.inst.CheckDisplaysPastRange(outIconType);
			if (num6)
			{
				Vector3 iconPos3D = GetIconPos3D(iconPosition2D, outIconType);
				if (!flag6)
				{
					TooltipConfig iconTooltip = GetTooltip(value);
					UIMiniMapElement iconFromCache = GetIconFromCache(outIconType);
					iconFromCache.TrackedVis = value;
					bool iconRotates = Singleton.Manager<ManRadar>.inst.CheckIconRotates(outIconType, flag5);
					PlaceElement(iconFromCache, iconColour, iconPos3D, iconRotates, in iconTooltip);
					if (flag3)
					{
						SetAreaSize(iconFromCache.RectTrans, num4);
					}
				}
				if (flag)
				{
					circleVisible = flag6 || !flag3;
					blipVisible = flag6;
					m_QuestMarker.LocalPosition = iconPos3D;
					m_QuestMarker.LocalRotation = Quaternion.Inverse(m_RectTrans.rotation);
				}
			}
			else if (m_RestrictDistantIconCount)
			{
				m_ClosestIcons.TryAdd(outIconType, flag ? null : value, distSq, iconColour);
			}
		}
		PlaceDistantDirectionalIcons(num3);
		PlaceVendorIcons();
		PlacePlayerDeathMarkers();
		RecycleUnusedIcons();
		if ((bool)m_QuestMarker)
		{
			m_QuestMarker.CircleVisible = circleVisible;
			m_QuestMarker.BlipVisible = blipVisible;
		}
	}

	private void PlaceDistantDirectionalIcons(float minDistance)
	{
		if (!m_RestrictDistantIconCount)
		{
			return;
		}
		Vector3 scenePosition = m_MapDisplay.FocalPoint.ScenePosition;
		float num = minDistance * minDistance;
		for (int i = 0; i < ManRadar.IconTypeCount; i++)
		{
			ManRadar.IconType iconType = (ManRadar.IconType)i;
			int count = m_ClosestIcons.GetCount(iconType);
			for (int j = 0; j < count; j++)
			{
				TrackedVisible trackedVis = m_ClosestIcons.GetTrackedVis(iconType, j);
				if (trackedVis != null && (trackedVis.Position - scenePosition).ToVector2XZ().sqrMagnitude > num)
				{
					CalculateIconDirectionFromScene(trackedVis.Position, limitRange: false, 0f, m_RectRadius, out var iconPosition2D);
					Vector3 iconPos3D = GetIconPos3D(iconPosition2D, iconType);
					Color colour = m_ClosestIcons.GetColour(iconType, j);
					TooltipConfig iconTooltip = GetTooltip(trackedVis);
					UIMiniMapElement iconFromCache = GetIconFromCache(iconType);
					iconFromCache.TrackedVis = trackedVis;
					bool iconRotates = Singleton.Manager<ManRadar>.inst.CheckIconRotates(iconType, onMap: false);
					PlaceElement(iconFromCache, colour, iconPos3D, iconRotates, in iconTooltip);
				}
			}
		}
	}

	private void PlaceVendorIcons()
	{
		if (!Singleton.Manager<ManWorld>.inst.Vendors.VisibleOnRadar || !Singleton.Manager<ManWorld>.inst.VendorSpawner.IsNotNull())
		{
			return;
		}
		Vector3 gameWorldPosition = m_MapDisplay.FocalPoint.GameWorldPosition;
		Singleton.Manager<ManWorld>.inst.VendorSpawner.TryFindNearestVendorPos(gameWorldPosition, out var nearestVendorPosWorld);
		bool flag = false;
		TrackedVisible trackedVisible = ((!Singleton.Manager<ManQuestLog>.inst.HasTrackedEncounter) ? null : Singleton.Manager<ManQuestLog>.inst.GetTrackedEncounterDisplayData()?.ActiveQuestLog?.GetEncounterWaypoint());
		if (trackedVisible != null && trackedVisible.RadarType == RadarTypes.Vendor)
		{
			Vector3 gameWorldPosition2 = trackedVisible.GetWorldPosition().GameWorldPosition;
			flag = (nearestVendorPosWorld - gameWorldPosition2).ToVector2XZ().sqrMagnitude < 25f;
		}
		if (!flag)
		{
			DrawVendorIcon(nearestVendorPosWorld);
		}
		if (ManWorld.s_DebugTradingStations)
		{
			List<Vector3> list = new List<Vector3>();
			Rect mapBounds = m_MapDisplay.GetMapBounds();
			float num = 1f / (m_MapDisplay.WorldToUIUnitRatio * m_MapDisplay.CurrentZoomLevel);
			mapBounds.xMin *= num;
			mapBounds.xMax *= num;
			mapBounds.yMin *= num;
			mapBounds.yMax *= num;
			float radius = Mathf.Max(Mathf.Abs(mapBounds.xMin), mapBounds.xMax, Mathf.Abs(mapBounds.yMin), mapBounds.yMax);
			Singleton.Manager<ManWorld>.inst.VendorSpawner.ListVendorsInRange(mapBounds.center, radius, list);
			{
				foreach (Vector3 item in list)
				{
					if (mapBounds.Contains(item))
					{
						DrawVendorIcon(item);
					}
				}
				return;
			}
		}
		if (m_RestrictDistantIconCount)
		{
			return;
		}
		Dictionary<int, TrackedVisible>.Enumerator allTrackedVisiblesEnumerator = Singleton.Manager<ManVisible>.inst.AllTrackedVisiblesEnumerator;
		while (allTrackedVisiblesEnumerator.MoveNext())
		{
			TrackedVisible value = allTrackedVisiblesEnumerator.Current.Value;
			if (value.RadarType == RadarTypes.Vendor && value != trackedVisible)
			{
				Vector3 gameWorldPosition3 = value.GetWorldPosition().GameWorldPosition;
				if (!((nearestVendorPosWorld - gameWorldPosition3).ToVector2XZ().sqrMagnitude < 25f))
				{
					DrawVendorIcon(gameWorldPosition3);
				}
			}
		}
		void DrawVendorIcon(Vector3 worldPos)
		{
			Vector2 iconPosition2D = default(Vector2);
			bool flag2 = !m_DisplayDirectionOnly && CalculateIconPositionFromWorld(worldPos, m_LimitToIconsInRange, m_MapDisplay.MaxDisplayRangeSqr, 0f, out iconPosition2D);
			bool flag3 = flag2 || CalculateIconDirectionFromWorld(worldPos, m_RectRadius, out iconPosition2D);
			if (flag2 || flag3)
			{
				Vector3 iconPos3D = GetIconPos3D(iconPosition2D, ManRadar.IconType.Vendor);
				Color iconColor = Singleton.Manager<ManRadar>.inst.GetIconColor(ManRadar.IconType.Vendor);
				TooltipConfig iconTooltip = TooltipConfig.Default;
				if (m_EnableIconTooltips)
				{
					iconTooltip.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.HUD.TradingStationName);
				}
				ManRadar.IconType iconType = ManRadar.IconType.Vendor;
				UIMiniMapElement iconFromCache = GetIconFromCache(iconType);
				bool iconRotates = Singleton.Manager<ManRadar>.inst.CheckIconRotates(iconType, flag2);
				PlaceElement(iconFromCache, iconColor, iconPos3D, iconRotates, in iconTooltip);
			}
		}
	}

	private void PlacePlayerDeathMarkers()
	{
		int num = Singleton.Manager<ManPlayer>.inst.PlayerDeathLocations.Count();
		num = ((m_MaxPlayerDeathMarkers == -1) ? num : Mathf.Min(num, m_MaxPlayerDeathMarkers));
		IEnumerator<WorldPosition> enumerator = Singleton.Manager<ManPlayer>.inst.PlayerDeathLocations.Reverse().Take(num).GetEnumerator();
		int num2 = 0;
		while (enumerator.MoveNext())
		{
			Vector3 gameWorldPosition = enumerator.Current.GameWorldPosition;
			Vector2 iconPosition2D = default(Vector2);
			bool flag = !m_DisplayDirectionOnly && CalculateIconPositionFromWorld(gameWorldPosition, m_LimitToIconsInRange, m_MapDisplay.MaxDisplayRangeSqr, 0f, out iconPosition2D);
			bool flag2 = flag || CalculateIconDirectionFromWorld(gameWorldPosition, limitRange: true, m_MapDisplay.MaxDisplayRange, m_RectRadius, out iconPosition2D);
			bool flag3 = flag || flag2;
			if (!flag3 && m_RestrictDistantIconCount && num2 < Singleton.Manager<ManRadar>.inst.GetCountDisplayingPastRange(ManRadar.IconType.PlayerGrave))
			{
				flag3 = CalculateIconDirectionFromWorld(gameWorldPosition, m_RectRadius, out iconPosition2D);
				if (flag3)
				{
					num2++;
				}
			}
			if (flag3)
			{
				Vector3 iconPos3D = GetIconPos3D(iconPosition2D, ManRadar.IconType.PlayerGrave);
				Color iconColor = Singleton.Manager<ManRadar>.inst.GetIconColor(ManRadar.IconType.PlayerGrave);
				TooltipConfig iconTooltip = TooltipConfig.Default;
				if (m_EnableIconTooltips)
				{
					iconTooltip.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.HUD.PlayerGraveTooltip);
				}
				ManRadar.IconType iconType = ManRadar.IconType.PlayerGrave;
				UIMiniMapElement iconFromCache = GetIconFromCache(iconType);
				bool iconRotates = Singleton.Manager<ManRadar>.inst.CheckIconRotates(iconType, flag);
				PlaceElement(iconFromCache, iconColor, iconPos3D, iconRotates, in iconTooltip);
			}
		}
	}

	private void UpdateAreaMarkers()
	{
		foreach (MapArea areaMarker in m_AreaMarkers)
		{
			UpdateAreaMarker(areaMarker);
		}
	}

	private void UpdateAreaMarker(MapArea mapArea)
	{
		if (mapArea.IsActive)
		{
			if (mapArea.SpawnedTrans == null)
			{
				mapArea.SpawnedTrans = SpawnArea(mapArea.Prefab, mapArea.GfxSize);
			}
			if (mapArea.SpawnedTrans.sizeDelta.x != mapArea.GfxSize)
			{
				SetAreaSize(mapArea.SpawnedTrans, mapArea.GfxSize);
			}
			SetAreaPosition(mapArea.SpawnedTrans, mapArea.WorldPosition);
		}
		else if ((bool)mapArea.SpawnedTrans)
		{
			RecycleArea(mapArea.SpawnedTrans);
			mapArea.SpawnedTrans = null;
		}
	}

	private Vector3 CalcIconPos(ManRadar.IconType iconType, bool onMap, Vector3 vecToTarget, float detectionRange)
	{
		if (onMap)
		{
			float magnitude = vecToTarget.magnitude;
			magnitude /= detectionRange / m_RectRadius;
			vecToTarget = vecToTarget.normalized * magnitude;
		}
		else
		{
			vecToTarget = vecToTarget.normalized * m_RectRadius;
		}
		vecToTarget.y = vecToTarget.z;
		vecToTarget.z = Singleton.Manager<ManRadar>.inst.GetPriority(iconType);
		return vecToTarget;
	}

	private void PlaceElement(UIMiniMapElement element, Color iconColour, Vector3 iconPos, bool iconRotates, in TooltipConfig iconTooltip = default(TooltipConfig))
	{
		element.Icon.color = iconColour;
		element.RectTrans.localPosition = iconPos;
		if (iconRotates)
		{
			float f = iconPos.normalized.Dot(Vector3.up);
			float num = iconPos.normalized.Dot(Vector3.right);
			f = Mathf.Acos(f) * 57.29578f;
			if (num >= 0f)
			{
				f = 0f - f;
			}
			element.RectTrans.localRotation = Quaternion.Euler(0f, 0f, f);
		}
		else
		{
			element.RectTrans.localRotation = Quaternion.Inverse(m_RectTrans.rotation);
		}
		if (iconTooltip.enabled)
		{
			element.EnableTooltip(iconTooltip.text, iconTooltip.mode);
		}
		else
		{
			element.DisableTooltip();
		}
	}

	private UIMiniMapElement GetIconFromCache(ManRadar.IconType iconType)
	{
		UIMiniMapElement result = null;
		UIMiniMapElement iconElementPrefab = Singleton.Manager<ManRadar>.inst.GetIconElementPrefab(iconType);
		if (iconElementPrefab != null)
		{
			IconCache iconCache = m_IconCache[(int)iconType];
			if (iconCache.numUsed >= iconCache.icons.Count)
			{
				UIMiniMapElement uIMiniMapElement = iconElementPrefab.Spawn();
				RectTransform parent = ((iconType == ManRadar.IconType.AreaQuest) ? m_InnerAreaMask : m_RectTrans);
				uIMiniMapElement.RectTrans.SetParent(parent, worldPositionStays: false);
				iconCache.icons.Add(uIMiniMapElement);
			}
			result = iconCache.icons[iconCache.numUsed];
			iconCache.numUsed++;
		}
		return result;
	}

	private void ZeroIconCount()
	{
		for (int i = 0; i < m_IconCache.Length; i++)
		{
			m_IconCache[i].numUsed = 0;
		}
	}

	private void RecycleUnusedIcons()
	{
		for (int i = 0; i < m_IconCache.Length; i++)
		{
			IconCache iconCache = m_IconCache[i];
			while (iconCache.icons.Count > iconCache.numUsed)
			{
				iconCache.icons[iconCache.icons.Count - 1].RectTrans.SetParent(null, worldPositionStays: false);
				iconCache.icons[iconCache.icons.Count - 1].Recycle(worldPosStays: false);
				iconCache.icons.RemoveAt(iconCache.icons.Count - 1);
			}
		}
	}

	private void ClearMiniMap()
	{
		ZeroIconCount();
		RecycleUnusedIcons();
	}

	public static bool TryGetIconForTrackedVisible(TrackedVisible trackedVisible, bool isQuestMarker, bool isManualTarget, out ManRadar.IconType outIconType, out Color iconColour)
	{
		bool flag = false;
		bool flag2;
		switch (trackedVisible.RadarType)
		{
		case RadarTypes.Hidden:
			flag2 = false;
			outIconType = ManRadar.IconType.FriendlyBase;
			break;
		case RadarTypes.Vehicle:
		{
			int radarTeamID = trackedVisible.RadarTeamID;
			if (ManSpawn.IsPlayerTeam(radarTeamID))
			{
				outIconType = ManRadar.IconType.FriendlyVehicle;
			}
			else if (radarTeamID == -2)
			{
				outIconType = ManRadar.IconType.NeutralVehicle;
			}
			else
			{
				outIconType = ((!isManualTarget) ? ManRadar.IconType.EnemyVehicle : ManRadar.IconType.ManuallyTargetedEnemyVehicle);
			}
			flag2 = !Singleton.Manager<ManNetwork>.inst.IsMultiplayer() || !trackedVisible.visible.IsNotNull() || !trackedVisible.visible.tank.IsNotNull() || !trackedVisible.visible.tank.netTech.IsNotNull() || trackedVisible.visible.tank.netTech.InitialSpawnShieldID == 0;
			break;
		}
		case RadarTypes.Base:
		{
			int radarTeamID2 = trackedVisible.RadarTeamID;
			if (ManSpawn.IsPlayerTeam(radarTeamID2))
			{
				if (trackedVisible.RadarMarkerConfig.IsUsed)
				{
					outIconType = trackedVisible.RadarMarkerConfig.Icon;
					flag = true;
				}
				else
				{
					outIconType = ManRadar.IconType.FriendlyBase;
				}
			}
			else if (radarTeamID2 == -2)
			{
				outIconType = ManRadar.IconType.NeutralBase;
			}
			else
			{
				outIconType = (isManualTarget ? ManRadar.IconType.ManuallyTargetedEnemyBase : ManRadar.IconType.EnemyBase);
			}
			flag2 = true;
			break;
		}
		case RadarTypes.Vendor:
			outIconType = ManRadar.IconType.Vendor;
			flag2 = Singleton.Manager<ManWorld>.inst.Vendors.VisibleOnRadar && isQuestMarker;
			break;
		case RadarTypes.Dispenser:
			outIconType = ManRadar.IconType.Dispenser;
			flag2 = true;
			break;
		case RadarTypes.Checkpoint:
			outIconType = ManRadar.IconType.Checkpoint;
			flag2 = true;
			break;
		case RadarTypes.UndiscoveredQuest:
			outIconType = ManRadar.IconType.UndiscoveredQuest;
			flag2 = true;
			break;
		case RadarTypes.DiscoveredQuest:
			outIconType = ManRadar.IconType.DiscoveredQuest;
			flag2 = true;
			break;
		case RadarTypes.AreaQuest:
			outIconType = ManRadar.IconType.AreaQuest;
			flag2 = true;
			break;
		case RadarTypes.Crate:
			outIconType = ManRadar.IconType.Crate;
			flag2 = true;
			break;
		case RadarTypes.Block:
			outIconType = ManRadar.IconType.DiscoveredQuest;
			flag2 = isQuestMarker;
			break;
		case RadarTypes.MultiplayerObject:
			outIconType = ManRadar.IconType.MultiplayerCrown;
			flag2 = true;
			break;
		case RadarTypes.MapNavTarget:
			outIconType = ManRadar.IconType.MapNavigation;
			flag2 = true;
			break;
		default:
			d.LogError($"Invalid RadarType: {trackedVisible.RadarType}");
			outIconType = ManRadar.IconType.FriendlyBase;
			flag2 = false;
			break;
		}
		if (flag2)
		{
			Color col;
			if (flag)
			{
				iconColour = Singleton.Manager<ManRadar>.inst.GetRadarMarkerColor(trackedVisible.RadarMarkerConfig.Color).SetAlpha(1f);
			}
			else if (TryGetMultiplayerTechColour(trackedVisible, out col))
			{
				iconColour = col;
			}
			else
			{
				iconColour = Singleton.Manager<ManRadar>.inst.GetIconColor(outIconType);
			}
		}
		else
		{
			iconColour = Color.white;
		}
		return flag2;
	}

	private static bool TryGetMultiplayerTechColour(TrackedVisible tv, out Color col)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && tv.ObjectType == ObjectTypes.Vehicle && ManSpawn.IsPlayerTeam(tv.RadarTeamID))
		{
			Tank tank = (tv.visible.IsNotNull() ? tv.visible.tank : null);
			NetTech netTech = (tank.IsNotNull() ? tank.netTech : null);
			col = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetMultiplayerTechColour(netTech, tv.RadarTeamID, Color.white);
			return true;
		}
		col = Color.white;
		return false;
	}

	private TooltipConfig GetTooltip(TrackedVisible tv)
	{
		TooltipConfig result = TooltipConfig.Default;
		if (m_EnableIconTooltips)
		{
			if (Singleton.Manager<ManQuestLog>.inst.TryGetEncounterIdentifier(tv.HostID, out var encounterID))
			{
				EncounterDisplayData encounterDisplayData = Singleton.Manager<ManQuestLog>.inst.GetEncounterDisplayData(encounterID);
				if (encounterDisplayData != null)
				{
					result.text = encounterDisplayData.Title;
				}
			}
			else if (tv.ObjectType == ObjectTypes.Vehicle)
			{
				if (tv.RadarType == RadarTypes.Vendor)
				{
					result.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.HUD.TradingStationName);
				}
				else
				{
					string text = ((!tv.visible.IsNotNull()) ? Singleton.Manager<ManVisible>.inst.GetStoredTechData(tv)?.Name : ((Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && tv.visible.tank != null && tv.visible.tank.netTech != null && tv.visible.tank.netTech.NetPlayer != null) ? tv.visible.tank.netTech.NetPlayer.name : tv.visible.name));
					result.text = text;
					result.mode = (ManSpawn.IsEnemyTeam(tv.RadarTeamID) ? UITooltipOptions.Warning : UITooltipOptions.Default);
				}
			}
			else if (tv.ObjectType == ObjectTypes.Crate)
			{
				result.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.HUD.CrateDropName);
			}
			else if (tv.ObjectType == ObjectTypes.Waypoint && tv.RadarType == RadarTypes.MapNavTarget)
			{
				result.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.HUD.PlayerMarkerName);
			}
		}
		return result;
	}

	private RectTransform SpawnArea(RectTransform prefab, float worldUnitRadius)
	{
		RectTransform rectTransform = prefab.Spawn(m_InnerAreaMask);
		rectTransform.transform.SetAsFirstSibling();
		rectTransform.localScale = Vector3.one;
		rectTransform.anchoredPosition3D = Vector3.zero;
		rectTransform.gameObject.SetActive(value: true);
		SetAreaSize(rectTransform, worldUnitRadius);
		return rectTransform;
	}

	private void RecycleArea(RectTransform rectTrans)
	{
		rectTrans.transform.SetParent(null, worldPositionStays: false);
		rectTrans.Recycle(worldPosStays: false);
	}

	private void SetAreaSize(RectTransform rectTrans, float worldUnitRadius)
	{
		float num = worldUnitRadius * 2f * m_MapDisplay.WorldToUIUnitRatio * m_MapDisplay.CurrentZoomLevel;
		rectTrans.sizeDelta = new Vector2(num, num);
	}

	private void SetAreaPosition(RectTransform rectTrans, Vector3 worldPos, bool stayUpright = true)
	{
		CalculateIconPositionFromWorld(worldPos, out var iconPosition2D);
		rectTrans.localPosition = iconPosition2D.ToVector3XY();
		if (stayUpright)
		{
			rectTrans.localRotation = Quaternion.Inverse(m_RectTrans.rotation);
		}
	}

	private void OnHide()
	{
		ClearMiniMap();
	}

	private void OnZoomChanged()
	{
		foreach (MapArea areaMarker in m_AreaMarkers)
		{
			if (areaMarker.SpawnedTrans != null)
			{
				SetAreaSize(areaMarker.SpawnedTrans, areaMarker.GfxSize);
			}
		}
	}

	private void OnPool()
	{
		m_ClosestIcons = new ClosestIcons();
		m_RectRadius = m_RectTrans.rect.width / 2f;
	}

	private void OnSpawn()
	{
		m_MapDisplay.HideEvent.Subscribe(OnHide);
		m_MapDisplay.ZoomChangedEvent.Subscribe(OnZoomChanged);
		m_IconCache = new IconCache[ManRadar.IconTypeCount];
		for (int i = 0; i < m_IconCache.Length; i++)
		{
			m_IconCache[i] = new IconCache
			{
				numUsed = 0,
				icons = new List<UIMiniMapElement>()
			};
		}
		if (m_AreaPrefabs[0].m_Prefab != null)
		{
			m_AreaMarkers.Add(new MapAreaInvasion(m_AreaPrefabs[0]));
		}
		if (m_AreaPrefabs[1].m_Prefab != null)
		{
			m_AreaMarkers.Add(new MapAreaMultiplayerBoundary(m_AreaPrefabs[1]));
		}
		UIQuestMarker questMarkerPrefab = Singleton.Manager<ManRadar>.inst.GetQuestMarkerPrefab();
		if ((bool)questMarkerPrefab)
		{
			m_QuestMarker = questMarkerPrefab.Spawn();
			m_QuestMarker.SetParent(m_RectTrans, worldPositionStays: false);
		}
	}

	private void OnRecycle()
	{
		ClearMiniMap();
		foreach (MapArea areaMarker in m_AreaMarkers)
		{
			if (areaMarker.SpawnedTrans != null)
			{
				RecycleArea(areaMarker.SpawnedTrans);
			}
		}
		m_AreaMarkers.Clear();
		if ((bool)m_QuestMarker)
		{
			m_QuestMarker.transform.SetParent(null, worldPositionStays: false);
			m_QuestMarker.Recycle();
			m_QuestMarker = null;
		}
		m_MapDisplay.HideEvent.Unsubscribe(OnHide);
		m_MapDisplay.ZoomChangedEvent.Unsubscribe(OnZoomChanged);
	}
}
