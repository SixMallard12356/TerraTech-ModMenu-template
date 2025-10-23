using System.Collections.Generic;
using UnityEngine;

public class InfoOverlayDataValues
{
	public struct ItemAttribute
	{
		public Sprite m_Icon;

		public string m_Title;

		public Color m_Color;

		public ItemAttribute(string m_Title, Sprite m_Icon, Color m_Color = default(Color))
		{
			this.m_Title = m_Title;
			this.m_Icon = m_Icon;
			this.m_Color = m_Color;
		}
	}

	public BlockTypes m_BlockType;

	public string m_MainTitle;

	public string m_Subtitle;

	public string m_SubtitleExpanded;

	public string m_Description;

	public string m_Category;

	public ObjectTypes m_SubjectType;

	public InfoOverlayData m_Config;

	public ManDamage.DamageableType m_DamageableType;

	public ManDamage.DamageType m_DamageType;

	public bool m_HasDamageType;

	public string m_Rarity;

	public Sprite m_RarityIcon;

	public string m_Price;

	public string m_BlockGrade;

	public Sprite m_BlockGradeIcon;

	public string m_BlockLimiterCost;

	public List<ItemAttribute> m_BlockAttributes = new List<ItemAttribute>();

	public List<ItemAttribute> m_ControlAttributes = new List<ItemAttribute>();

	private Sprite m_icon;

	private bool m_OverrideSubtitle;

	public Sprite IconSprite
	{
		get
		{
			if (m_icon != null)
			{
				return m_icon;
			}
			if (m_Config != null)
			{
				return m_Config.m_IconSprite;
			}
			return null;
		}
		set
		{
			m_icon = value;
		}
	}

	public bool OverrideSubtitle
	{
		get
		{
			if (m_OverrideSubtitle)
			{
				return m_ControlAttributes.Count > 0;
			}
			return false;
		}
		set
		{
			m_OverrideSubtitle = value;
		}
	}

	public InfoOverlayDataValues(InfoOverlayData config)
	{
		m_Config = config;
	}

	public void Reset()
	{
		m_MainTitle = string.Empty;
		m_Subtitle = string.Empty;
		m_SubtitleExpanded = string.Empty;
		m_Description = string.Empty;
		m_Category = string.Empty;
		m_icon = null;
		m_SubjectType = ObjectTypes.Null;
		m_Price = string.Empty;
		m_Rarity = string.Empty;
		m_RarityIcon = null;
		m_BlockGrade = string.Empty;
		m_BlockGradeIcon = null;
		m_BlockAttributes.Clear();
		m_ControlAttributes.Clear();
		m_OverrideSubtitle = false;
	}

	public void AddBlockItemAttributes(Sprite icon, string title, Color color = default(Color))
	{
		m_BlockAttributes.Add(new ItemAttribute
		{
			m_Title = title,
			m_Icon = icon,
			m_Color = color
		});
	}

	public void AddControlItemAttribute(string title)
	{
		m_ControlAttributes.Add(new ItemAttribute
		{
			m_Title = title
		});
	}
}
