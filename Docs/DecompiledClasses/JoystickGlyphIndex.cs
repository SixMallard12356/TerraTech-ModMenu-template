#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Rewired.Data.Mapping;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "JoystickGlyphIndex", menuName = "Asset/Joystick Glyphs for Platform")]
public class JoystickGlyphIndex : ScriptableObject
{
	[Serializable]
	public class ControllerGlyphBinding
	{
		public int m_ElementID;

		public int m_SpriteID;

		public int m_GroupSpriteID;

		public bool m_HasGroupSprite;

		public int m_AxisGroupSpriteID;

		public bool m_HasAxisGroupSprite;
	}

	public enum GlyphType
	{
		Default,
		Group,
		AxisGroup
	}

	[SerializeField]
	private HardwareJoystickTemplateMap m_TemplateMap;

	[SerializeField]
	private HardwareJoystickMap m_JoystickMap;

	[SerializeField]
	private TMP_SpriteAsset m_SpriteAsset;

	[SerializeField]
	private List<ControllerGlyphBinding> m_Bindings;

	public Guid HardwareGUID
	{
		get
		{
			if (m_JoystickMap != null)
			{
				return m_JoystickMap.Guid;
			}
			if (m_TemplateMap != null)
			{
				return m_TemplateMap.Guid;
			}
			return default(Guid);
		}
	}

	public int GetSpriteID(int buttonElementID, GlyphType glyphType = GlyphType.Default)
	{
		for (int i = 0; i < m_Bindings.Count; i++)
		{
			if (m_Bindings[i].m_ElementID != buttonElementID)
			{
				continue;
			}
			switch (glyphType)
			{
			case GlyphType.Group:
				if (!m_Bindings[i].m_HasGroupSprite)
				{
					d.LogErrorFormat(this, "JoystickGlyphIndex.GetSpriteID - No group sprite asset configured for index {0} on {1}", i, base.name);
					break;
				}
				return m_Bindings[i].m_GroupSpriteID;
			case GlyphType.AxisGroup:
				if (!m_Bindings[i].m_HasAxisGroupSprite)
				{
					d.LogErrorFormat(this, "JoystickGlyphIndex.GetSpriteID - No axis group sprite asset configured for index {0} on {1}", i, base.name);
					break;
				}
				return m_Bindings[i].m_AxisGroupSpriteID;
			}
			return m_Bindings[i].m_SpriteID;
		}
		d.LogErrorFormat(this, "JoystickGlyphIndex.GetSpriteID - no sprite found for the buttonID {0}. Is this asset ({1}) configured correctly? ", buttonElementID, base.name);
		return -1;
	}

	public string GetSpriteAssetName()
	{
		if (m_SpriteAsset != null)
		{
			return m_SpriteAsset.name;
		}
		d.LogError("JoystickGlyphIndex.GetSpriteAssetName - No sprite asset configured for the index " + base.name);
		return null;
	}
}
