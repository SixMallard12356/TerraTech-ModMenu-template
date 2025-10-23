#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class ButtonPrompts
{
	private RectTransform m_Panel;

	private List<UIBtnPromptElement> m_ActivePrompts = new List<UIBtnPromptElement>();

	private Dictionary<int, UIBtnPromptElement> m_BtnPromptDict = new Dictionary<int, UIBtnPromptElement>();

	private int[] m_ButtonDisplayOrder = new int[23]
	{
		0, 1, 20, 2, 3, 21, 16, 17, 18, 19,
		6, 7, 8, 9, 4, 10, 5, 11, 14, 15,
		12, 13, 22
	};

	public ButtonPrompts(RectTransform panel)
	{
		m_Panel = panel;
	}

	public void UpdateCurrentPrompt(LocalisedString replacementString, Localisation.GlyphInfo[] rewiredActionIds)
	{
		int key = CreateUniqueHashFromRewiredActionIDs(rewiredActionIds);
		if (m_BtnPromptDict.TryGetValue(key, out var value))
		{
			value.SetEnabled(enabled: true);
			value.SetLocalisedText(replacementString);
		}
		else
		{
			AddBtnPrompt(replacementString, rewiredActionIds);
			SortElements();
		}
	}

	public void ToggleBtnPrompt(bool active, Localisation.GlyphInfo[] rewiredActionIds)
	{
		int key = CreateUniqueHashFromRewiredActionIDs(rewiredActionIds);
		if (m_BtnPromptDict.TryGetValue(key, out var value))
		{
			value.SetEnabled(active);
		}
	}

	public void SpawnPrompts(ManBtnPrompt.PromptData data)
	{
		Cleanup();
		for (int i = 0; i < data.prompts.Length; i++)
		{
			LocalisedString localisedString = data.prompts[i];
			AddBtnPrompt(localisedString, localisedString.m_InlineGlyphs);
		}
		SortElements();
	}

	private void AddBtnPrompt(LocalisedString display, Localisation.GlyphInfo[] rewiredActionIds)
	{
		UIBtnPromptElement uIBtnPromptElement = Singleton.Manager<ManBtnPrompt>.inst.m_BtnPromptPrefab.Spawn();
		int rewiredAction = rewiredActionIds[0].m_RewiredAction;
		int key = CreateUniqueHashFromRewiredActionIDs(rewiredActionIds);
		uIBtnPromptElement.RectTransform.SetParent(m_Panel, worldPositionStays: false);
		uIBtnPromptElement.Setup(rewiredAction, display);
		m_ActivePrompts.Add(uIBtnPromptElement);
		m_BtnPromptDict.Add(key, uIBtnPromptElement);
	}

	private void RecyclePrompts(UIBtnPromptElement btnPrompt)
	{
		btnPrompt.RectTransform.SetParent(null, worldPositionStays: false);
		btnPrompt.Recycle();
	}

	public void Cleanup()
	{
		foreach (UIBtnPromptElement activePrompt in m_ActivePrompts)
		{
			RecyclePrompts(activePrompt);
		}
		m_ActivePrompts.Clear();
		m_BtnPromptDict.Clear();
	}

	private int CreateUniqueHashFromRewiredActionIDs(Localisation.GlyphInfo[] rewiredActionIds)
	{
		d.Assert(rewiredActionIds.Length <= 4, "CreateHashFromRewiredActionIDs - Doesn't support concatenating more than 4 action Ids");
		int num = 0;
		for (int num2 = rewiredActionIds.Length - 1; num2 >= 0; num2--)
		{
			num |= rewiredActionIds[num2].m_RewiredAction << num2 * 8;
		}
		return num;
	}

	private int CompareUIBtnPromptElements(UIBtnPromptElement a, UIBtnPromptElement b)
	{
		return GetButtonDisplayIndex(a.RewiredActionID).CompareTo(GetButtonDisplayIndex(b.RewiredActionID));
	}

	private void SortElements()
	{
		m_ActivePrompts.Sort(CompareUIBtnPromptElements);
		for (int i = 0; i < m_ActivePrompts.Count; i++)
		{
			if (m_ActivePrompts[i].RectTransform.GetSiblingIndex() != i)
			{
				m_ActivePrompts[i].RectTransform.SetSiblingIndex(i);
			}
		}
	}

	private int GetButtonDisplayIndex(int rewiredActionID)
	{
		ManInput.GamepadElementID gamepadElementID = Singleton.Manager<ManInput>.inst.FindButtonIDForAction(rewiredActionID);
		int num = (int)gamepadElementID;
		int num2 = -1;
		for (int i = 0; i < m_ButtonDisplayOrder.Length; i++)
		{
			if (m_ButtonDisplayOrder[i] == num)
			{
				num2 = i;
				break;
			}
		}
		d.AssertFormat(num2 != -1, "ButtonPrompt.GetButtonDisplayIndex - Failed to find display priority for rewired action {0}, mapped to gamepad button {1}", rewiredActionID, gamepadElementID.ToString());
		return num2;
	}
}
