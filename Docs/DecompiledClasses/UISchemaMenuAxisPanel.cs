using System;
using UnityEngine;
using UnityEngine.UI;

public class UISchemaMenuAxisPanel : MonoBehaviour
{
	[SerializeField]
	private UISchemaMenuAxisButtonPair[] m_Buttons;

	[SerializeField]
	private Text m_AxisText;

	private Event<InputAxisMapping, bool> OnMappingSelected;

	public void Setup(InputAxisMapping[] disabledAxis)
	{
		if (disabledAxis == null)
		{
			return;
		}
		UISchemaMenuAxisButtonPair[] buttons = m_Buttons;
		foreach (UISchemaMenuAxisButtonPair uISchemaMenuAxisButtonPair in buttons)
		{
			bool active = true;
			for (int j = 0; j < disabledAxis.Length; j++)
			{
				if (disabledAxis[j] == uISchemaMenuAxisButtonPair.GetAssignedAxisMapping())
				{
					active = false;
					break;
				}
			}
			uISchemaMenuAxisButtonPair.gameObject.SetActive(active);
		}
	}

	public void UpdateText(ControlScheme controlScheme)
	{
		UISchemaMenuAxisButtonPair[] buttons = m_Buttons;
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].Setup(controlScheme);
		}
	}

	public void Show(Action<InputAxisMapping, bool> selectedCallback, string axisText)
	{
		OnMappingSelected.Clear();
		OnMappingSelected.Subscribe(selectedCallback);
		if ((bool)m_AxisText)
		{
			m_AxisText.text = axisText;
		}
		UISchemaMenuAxisButtonPair[] buttons = m_Buttons;
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].UpdateText();
		}
		base.gameObject.SetActive(value: true);
		ManBtnPrompt.PromptData promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextSchemeClear);
		Singleton.Manager<ManUI>.inst.UpdateScreenPrompt(promptDataByType);
	}

	public void Hide()
	{
		if (base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(value: false);
			ManBtnPrompt.PromptData promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextSchemeClear);
			Singleton.Manager<ManUI>.inst.ToggleBtnPrompt(active: false, promptDataByType.prompts[0].m_InlineGlyphs);
		}
	}

	private void OnMappingClicked(InputAxisMapping mapping, bool reverse)
	{
		OnMappingSelected.Send(mapping, reverse);
		Hide();
	}

	private void Awake()
	{
		UISchemaMenuAxisButtonPair[] buttons = m_Buttons;
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].Init(OnMappingClicked);
		}
	}

	private void Update()
	{
		if (Singleton.Manager<ManInput>.inst.GetButtonUp(57))
		{
			OnMappingClicked(InputAxisMapping.Unmapped, reverse: false);
		}
	}
}
