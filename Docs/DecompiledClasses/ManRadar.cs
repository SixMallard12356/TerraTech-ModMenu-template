#define UNITY_EDITOR
using System;
using UnityEngine;

public class ManRadar : Singleton.Manager<ManRadar>
{
	public enum IconType
	{
		FriendlyVehicle,
		EnemyVehicle,
		NeutralVehicle,
		FriendlyBase,
		EnemyBase,
		NeutralBase,
		Dispenser,
		Checkpoint,
		Invader,
		Vendor,
		HeartBase,
		UndiscoveredQuest,
		DiscoveredQuest,
		AreaQuest,
		Crate,
		ManuallyTargetedEnemyVehicle,
		ManuallyTargetedEnemyBase,
		MultiplayerCrown,
		rm_Flag,
		rm_Square,
		rm_Diamond,
		rm_Star,
		rm_Cross,
		rm_Arrow,
		GenericResource,
		MapNavigation,
		PlayerGrave
	}

	public enum RadarMarkerColorType
	{
		White,
		Magenta,
		Blue,
		Green,
		Yellow,
		Red
	}

	public enum MiniMapType
	{
		Compass,
		ProximityRadar,
		MiniMap
	}

	[Serializable]
	public struct IconEntry
	{
		public UIMiniMapElement mapIconPrefab;

		public int numDisplayingAtRange;

		public bool offMapRotates;

		public bool canBeRadarMarkerIcon;

		public float priority;

		public Color colour;

		public Mesh mesh;
	}

	[SerializeField]
	private IconEntry[] m_Icons;

	[SerializeField]
	private Color[] m_RadarMarkerColors;

	[SerializeField]
	private Color[] m_RadarMarkerHDRColors;

	[SerializeField]
	private UIQuestMarker m_QuestMarkerIcon;

	public static int IconTypeCount => EnumValuesIterator<IconType>.Count;

	public UIMiniMapElement GetIconElementPrefab(IconType iconType)
	{
		UIMiniMapElement result = null;
		if ((int)iconType < m_Icons.Length)
		{
			result = m_Icons[(int)iconType].mapIconPrefab;
		}
		return result;
	}

	public Color GetIconColor(IconType iconType)
	{
		Color result = Color.white;
		if ((int)iconType < m_Icons.Length)
		{
			result = m_Icons[(int)iconType].colour;
		}
		return result;
	}

	public Color GetRadarMarkerColor(RadarMarkerColorType colorType, bool adjustForHDR = false)
	{
		Color result = Color.white;
		int num = (int)colorType;
		if (num > -1 && num < m_RadarMarkerColors.Length)
		{
			result = ((adjustForHDR && Singleton.camera.allowHDR) ? m_RadarMarkerHDRColors[num] : m_RadarMarkerColors[num]);
		}
		else
		{
			d.LogError("[Beltain's words echoed] Available radar marker colours in ManRadar do not posses the requested index: " + num);
		}
		return result;
	}

	public Mesh GetRadarMarkerMesh(IconType iconType)
	{
		Mesh result = null;
		int num = (int)iconType;
		if (num > -1 && num < m_Icons.Length && m_Icons[num].canBeRadarMarkerIcon && m_Icons[num].mesh != null)
		{
			result = m_Icons[num].mesh;
		}
		else
		{
			d.LogError("[Beltain's words echoed] Available icon meshes in ManRadar do not posses the requested index: " + num);
		}
		return result;
	}

	public UIQuestMarker GetQuestMarkerPrefab()
	{
		return m_QuestMarkerIcon;
	}

	public int GetCountDisplayingPastRange(IconType iconType)
	{
		int result = 0;
		if ((int)iconType < m_Icons.Length)
		{
			result = m_Icons[(int)iconType].numDisplayingAtRange;
		}
		return result;
	}

	public bool CheckDisplaysPastRange(IconType iconType)
	{
		return GetCountDisplayingPastRange(iconType) > 0;
	}

	public bool CheckIconRotates(IconType iconType, bool onMap)
	{
		if (onMap)
		{
			return false;
		}
		if ((int)iconType < m_Icons.Length)
		{
			return m_Icons[(int)iconType].offMapRotates;
		}
		return false;
	}

	public float GetPriority(IconType iconType)
	{
		float result = 0f;
		if ((int)iconType < m_Icons.Length)
		{
			result = m_Icons[(int)iconType].priority;
		}
		return result;
	}
}
