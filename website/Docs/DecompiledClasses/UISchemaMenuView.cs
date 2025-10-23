using System;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class UISchemaMenuView : MonoBehaviour
{
	[SerializeField]
	private UISchemaMenuLibrary m_Library;

	[SerializeField]
	private UISchemaMenuBindingsPanel m_BindingsPanel;

	[SerializeField]
	private UIOptionsBehaviourButton m_ApplyButton;

	[SerializeField]
	private RawImage m_ModelImage;

	[SerializeField]
	[EnumArray(typeof(MovementAxis))]
	private UISchemaImageAxis[] m_AxisImages;

	[SerializeField]
	private Button[] m_DisabledControllerButtons;

	public UISchemaMenuLibrary Library => m_Library;

	public UISchemaMenuBindingsPanel Bindings => m_BindingsPanel;

	public void Init(Texture modelTex, ControlSchemeLibrary schemeLibrary, List<ControlScheme> techSchemes, InputAxisMapping[] disabledMappings, Action<ControlScheme> clickedCallback, Action<ControlScheme, bool> toggledCallback)
	{
		SetModelImage(modelTex);
		m_Library.Setup(schemeLibrary, techSchemes, clickedCallback, toggledCallback);
		m_BindingsPanel.Setup(disabledMappings);
		SetApplyButtonInteractable(interactable: false);
		SetAxisImage(MovementAxis.MoveX_MoveRight, enabled: false, default(AxisMapping));
		bool flag = Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad();
		Button[] disabledControllerButtons = m_DisabledControllerButtons;
		for (int i = 0; i < disabledControllerButtons.Length; i++)
		{
			disabledControllerButtons[i].gameObject.SetActive(!flag);
		}
		base.gameObject.SetActive(value: false);
		base.gameObject.SetActive(value: true);
		Canvas.ForceUpdateCanvases();
	}

	public void AddNewScheme(ControlScheme newScheme, Action<ControlScheme> clickedCallback, Action<ControlScheme, bool> toggledCallback)
	{
		m_Library.AddSchemeToList(newScheme, isAvailableOnTech: false, clickedCallback, toggledCallback);
	}

	public void DisplayScheme(ControlScheme activeScheme, Action<bool, MovementAxis, int> pollInputCallback, Action<MovementAxis, int, InputAxisMapping, bool> mappingChangedCallback, Action<bool> invertReverseChangedCallback, Action<MovementAxis, bool> hoverCallback)
	{
		m_Library.DisplayScheme(activeScheme);
		m_BindingsPanel.DisplayScheme(activeScheme, pollInputCallback, mappingChangedCallback, invertReverseChangedCallback, hoverCallback);
	}

	public void RemoveScheme(ControlScheme controlScheme)
	{
		m_Library.RemoveSchemeFromList(controlScheme);
	}

	public void Close()
	{
		m_Library.CleanList();
	}

	public void UpdateTextForScheme(ControlScheme controlScheme)
	{
		m_Library.UpdateTextForScheme(controlScheme);
		m_BindingsPanel.SetText(controlScheme);
	}

	public void UpdateTechSchemeToggles(List<ControlScheme> techSchemes)
	{
		m_Library.UpdateTechSchemeToggles(techSchemes);
	}

	public void RefreshAvailableAxis(ControlScheme controlScheme)
	{
		m_BindingsPanel.UpdateAxisAfterMappingChange(controlScheme);
	}

	public void SetApplyButtonInteractable(bool interactable)
	{
		m_ApplyButton.GetComponent<TooltipComponent>().enabled = interactable;
		m_ApplyButton.GetComponent<Button>().interactable = interactable;
		m_ApplyButton.GetButton().interactable = interactable;
	}

	public void SetAxisImage(MovementAxis axis, bool enabled, AxisMapping mapping)
	{
		for (int i = 0; i < m_AxisImages.Length; i++)
		{
			if (m_AxisImages[i] != null)
			{
				m_AxisImages[i].gameObject.SetActive(enabled && axis == (MovementAxis)i);
				if (enabled)
				{
					mapping.GetRewiredActionElements(out var pos, out var neg, out var pos2, out var neg2);
					string stringForKeys = GetStringForKeys(pos, pos2);
					string stringForKeys2 = GetStringForKeys(neg, neg2);
					m_AxisImages[i].SetText(stringForKeys, stringForKeys2);
				}
			}
		}
	}

	private void SetModelImage(Texture tex)
	{
		m_ModelImage.texture = tex;
	}

	private string GetStringForKeys(ActionElementMap keyMap, ActionElementMap keyMap2)
	{
		string text = string.Empty;
		if (keyMap != null || keyMap2 != null)
		{
			if (keyMap != null)
			{
				text = text + Singleton.Manager<Localisation>.inst.ActionElementMapToString(keyMap) + "\n";
			}
			if (keyMap2 != null)
			{
				text += Singleton.Manager<Localisation>.inst.ActionElementMapToString(keyMap2);
			}
		}
		else
		{
			text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 72);
		}
		return text;
	}
}
