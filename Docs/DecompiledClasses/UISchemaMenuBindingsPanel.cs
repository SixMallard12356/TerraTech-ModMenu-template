using System;
using UnityEngine;
using UnityEngine.UI;

public class UISchemaMenuBindingsPanel : MonoBehaviour
{
	[SerializeField]
	private UISchemaMenuAxisPanel m_AxisPanel;

	[SerializeField]
	private Text m_SchemaNameText;

	[SerializeField]
	private Text m_RestoreDefaultsText;

	[SerializeField]
	private TooltipComponent m_RestoreDefaultsTooltip;

	[SerializeField]
	private Toggle m_InvertReversingToggle;

	[SerializeField]
	private UISchemaMenuBindingPair[] m_MappingPanels;

	[SerializeField]
	private UIOptionsBehaviourButton m_RenameButton;

	[SerializeField]
	private UISchemaIcon m_Icon;

	[SerializeField]
	private bool m_CanRenameDefaultSchemes;

	private Event<MovementAxis, int, InputAxisMapping, bool> OnMappingChanged;

	private Event<bool, MovementAxis, int> OnPollInputChanged;

	private MovementAxis m_CurrentAxis;

	private int m_BindingIndex;

	private bool m_RequestingNegativeDirection;

	private Action<bool> m_InvertReverseChangedCallback;

	public MovementAxis CurrentAxis => m_CurrentAxis;

	public bool RequestingNegativeDirection => m_RequestingNegativeDirection;

	public void Setup(InputAxisMapping[] disabledMappings)
	{
		m_AxisPanel.Setup(disabledMappings);
	}

	public void DisplayScheme(ControlScheme activeScheme, Action<bool, MovementAxis, int> pollInputCallback, Action<MovementAxis, int, InputAxisMapping, bool> mappingChangedCallback, Action<bool> invertReverseChangedCallback, Action<MovementAxis, bool> hoveredCallback)
	{
		OnMappingChanged.Clear();
		OnMappingChanged.Subscribe(mappingChangedCallback);
		OnPollInputChanged.Clear();
		OnPollInputChanged.Subscribe(pollInputCallback);
		EnableRenameButton(m_CanRenameDefaultSchemes || activeScheme.IsCustom);
		m_InvertReverseChangedCallback = invertReverseChangedCallback;
		SetText(activeScheme);
		UpdateIcon(activeScheme.Category);
		UISchemaMenuBindingPair[] mappingPanels = m_MappingPanels;
		for (int i = 0; i < mappingPanels.Length; i++)
		{
			mappingPanels[i].Init(activeScheme, OnPanelClicked, hoveredCallback);
		}
		SetButtonToDefaultsOrDelete(!activeScheme.IsCustom);
		m_InvertReversingToggle.SetValue(activeScheme.ReverseSteering);
		UpdateAxisAfterMappingChange(activeScheme);
		HideAxisPanel();
	}

	public void SetText(ControlScheme scheme)
	{
		m_SchemaNameText.text = scheme.GetName();
	}

	public void UpdateIcon(ControlSchemeCategory category)
	{
		m_Icon.SetIcon(category);
	}

	public void UpdateAxisAfterMappingChange(ControlScheme controlScheme)
	{
		m_AxisPanel.UpdateText(controlScheme);
		UISchemaMenuBindingPair[] mappingPanels = m_MappingPanels;
		for (int i = 0; i < mappingPanels.Length; i++)
		{
			mappingPanels[i].UpdateText();
		}
		HideAxisPanel();
	}

	private void OnPanelClicked(MovementAxis axis, bool negativeDirection, int bindingIndex)
	{
		string axisText = string.Empty;
		m_CurrentAxis = axis;
		m_BindingIndex = bindingIndex;
		m_RequestingNegativeDirection = negativeDirection;
		UISchemaMenuBindingPair[] mappingPanels = m_MappingPanels;
		foreach (UISchemaMenuBindingPair uISchemaMenuBindingPair in mappingPanels)
		{
			if (uISchemaMenuBindingPair.m_MovementAxis == axis)
			{
				axisText = uISchemaMenuBindingPair.AssigningKeyString.Value;
			}
		}
		m_AxisPanel.Show(OnAxisSelected, axisText);
		OnPollInputChanged.Send(paramA: true, axis, bindingIndex);
	}

	private void OnAxisSelected(InputAxisMapping inputAxis, bool reverse)
	{
		if (m_RequestingNegativeDirection)
		{
			reverse = !reverse;
		}
		OnMappingChanged.Send(m_CurrentAxis, m_BindingIndex, inputAxis, reverse);
	}

	private void OnInvertReversingToggle(bool on)
	{
		m_InvertReverseChangedCallback.Send(on);
	}

	private void SetButtonToDefaultsOrDelete(bool defaults)
	{
		if (defaults)
		{
			m_RestoreDefaultsTooltip.SetText(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 58));
			m_RestoreDefaultsText.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 57);
		}
		else
		{
			m_RestoreDefaultsText.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 59);
			m_RestoreDefaultsTooltip.SetText(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlSchemas, 60));
		}
	}

	private void EnableRenameButton(bool enable)
	{
		m_RenameButton.GetComponent<Button>().interactable = enable;
		m_RenameButton.GetButton().interactable = enable;
		m_RenameButton.GetComponent<TooltipComponent>().enabled = enable;
	}

	private void HideAxisPanel()
	{
		m_AxisPanel.Hide();
		OnPollInputChanged.Send(paramA: false, MovementAxis.MoveX_MoveRight, m_BindingIndex);
	}

	private void OnPool()
	{
		m_InvertReversingToggle.onValueChanged.AddListener(OnInvertReversingToggle);
	}
}
