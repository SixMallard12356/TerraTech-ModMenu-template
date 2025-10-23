using System;
using TMPro;
using UnityEngine;

public class UIBlockPaletteFilterText : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI m_CategoryFilterText;

	[SerializeField]
	private TextMeshProUGUI m_CorpFilterText;

	[SerializeField]
	private LocalisedString m_AllCorpString;

	[SerializeField]
	private LocalisedString m_AllCategoryString;

	public void UpdateText(UICorpToggles corpToggles, bool showCorpText, UICategoryToggles categoryToggles, bool showCategoryText)
	{
		if (m_CorpFilterText != null)
		{
			m_CorpFilterText.gameObject.SetActive(showCorpText);
			if (showCorpText)
			{
				FactionSubTypes corporation = FactionSubTypes.NULL;
				int num = 0;
				for (int i = 0; i < Singleton.Manager<ManPurchases>.inst.AvailableCorporations.Count; i++)
				{
					FactionSubTypes factionSubTypes = Singleton.Manager<ManPurchases>.inst.AvailableCorporations[i];
					if (corpToggles.Selection.Contains((int)factionSubTypes))
					{
						num++;
						corporation = factionSubTypes;
					}
				}
				string text = Singleton.Manager<Localisation>.inst.ReplaceGlyphPlaceHolders("{0} ", new Localisation.GlyphInfo(41));
				if (num == 1)
				{
					m_CorpFilterText.text = text + StringLookup.GetCorporationName(corporation);
				}
				else
				{
					m_CorpFilterText.text = text + m_AllCorpString.Value;
				}
			}
		}
		UpdateCategoryText(showCategoryText, categoryToggles, GetBlockCategoryName, new Localisation.GlyphInfo(42));
	}

	private string GetBlockCategoryName(int index)
	{
		return StringLookup.GetBlockCategoryName((BlockCategories)index);
	}

	public void UpdateCategoryText(bool showCategoryText, UICategoryToggles categoryToggles, Func<int, string> stringLookupFunc, Localisation.GlyphInfo rewiredButtonAction1 = null, Localisation.GlyphInfo rewiredButtonAction2 = null)
	{
		if (!(m_CategoryFilterText != null))
		{
			return;
		}
		m_CategoryFilterText.gameObject.SetActive(showCategoryText);
		if (!showCategoryText)
		{
			return;
		}
		int arg = -1;
		int num = 0;
		foreach (int item in categoryToggles.Selection)
		{
			if (categoryToggles.Selection.Contains(item))
			{
				num++;
				arg = item;
			}
		}
		string text = string.Empty;
		if (rewiredButtonAction1.m_RewiredAction >= 0)
		{
			text += Singleton.Manager<Localisation>.inst.ReplaceGlyphPlaceHolders("{0} ", rewiredButtonAction1);
		}
		text = ((num != 1) ? (text + m_AllCategoryString.Value) : (text + stringLookupFunc(arg)));
		if (rewiredButtonAction2.m_RewiredAction >= 0)
		{
			text += Singleton.Manager<Localisation>.inst.ReplaceGlyphPlaceHolders(" {0}", rewiredButtonAction2);
		}
		m_CategoryFilterText.text = text;
	}
}
