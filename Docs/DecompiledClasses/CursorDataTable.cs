using System;
using System.Collections.Generic;
using UnityEngine;

public class CursorDataTable : ScriptableObject
{
	public enum PlatformSetTypes
	{
		PC,
		Console
	}

	[Serializable]
	public struct CursorData
	{
		public Texture2D m_Texture;

		public Sprite m_Sprite;

		[Tooltip("From Top Left")]
		public Vector2 m_Hotspot;
	}

	[Serializable]
	public class CursorDataSet
	{
		public string m_Name;

		public LocalisedString m_LocalisedName;

		public bool m_UseSoftwareCursor;

		[EnumArray(typeof(GameCursor.CursorState))]
		public CursorData[] m_CursorData;
	}

	[Serializable]
	public class PlatformCursorData
	{
		[SerializeField]
		[HideInInspector]
		public string m_Name;

		public PlatformSetTypes m_PlatformType;

		public CursorDataSet[] m_DataSets;

		public CursorData m_FallbackCursor;
	}

	[EnumArray(typeof(PlatformSetTypes))]
	[SerializeField]
	protected PlatformCursorData[] m_CursorPlatformSets = new PlatformCursorData[0];

	private Dictionary<PlatformSetTypes, PlatformCursorData> _PlatformSets;

	public Dictionary<PlatformSetTypes, PlatformCursorData> PlatformSets
	{
		get
		{
			if (_PlatformSets == null)
			{
				_PlatformSets = new Dictionary<PlatformSetTypes, PlatformCursorData>();
				for (int i = 0; i < m_CursorPlatformSets.Length; i++)
				{
					_PlatformSets[m_CursorPlatformSets[i].m_PlatformType] = m_CursorPlatformSets[i];
				}
			}
			return _PlatformSets;
		}
	}

	public CursorData GetCurrentCursorData(PlatformSetTypes platformSetType, out CursorMode mode, int overrideState = -1)
	{
		PlatformCursorData platformCursorData = PlatformSets[platformSetType];
		int num = ((overrideState != -1) ? overrideState : ((int)GameCursor.GetCursorState()));
		int num2 = ((platformSetType == PlatformSetTypes.PC) ? GameCursor.CursorSize : 0);
		CursorDataSet cursorDataSet = platformCursorData.m_DataSets[(num2 >= 0 && num2 < platformCursorData.m_DataSets.Length) ? num2 : 0];
		mode = (cursorDataSet.m_UseSoftwareCursor ? CursorMode.ForceSoftware : CursorMode.Auto);
		CursorData result = ((num >= 0 && num < cursorDataSet.m_CursorData.Length) ? cursorDataSet.m_CursorData[num] : default(CursorData));
		if (result.m_Texture == null && result.m_Sprite == null)
		{
			mode = CursorMode.Auto;
			result = platformCursorData.m_FallbackCursor;
		}
		return result;
	}

	public void OnValidate()
	{
		PlatformCursorData[] cursorPlatformSets = m_CursorPlatformSets;
		foreach (PlatformCursorData obj in cursorPlatformSets)
		{
			obj.m_Name = obj.m_PlatformType.ToString();
		}
	}
}
