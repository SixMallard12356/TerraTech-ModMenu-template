#define UNITY_EDITOR
using Rewired;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUndoButton : UIHUDElement
{
	public class Context
	{
		public UndoTypes m_UndoTypes;

		public string m_TechName;
	}

	[SerializeField]
	private Image m_Icon;

	[SerializeField]
	private TextMeshProUGUI m_GlyphTextField;

	[SerializeField]
	[EnumArray(typeof(UndoTypes))]
	private LocalisedString[] m_TooltipStrings;

	[SerializeField]
	private GameObject m_ControllerUI;

	private TooltipComponent m_TooltipComponent;

	public override void Show(object context)
	{
		UpdateContext(context);
		if (m_ControllerUI != null)
		{
			m_ControllerUI.SetActive(Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled());
		}
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			Singleton.Manager<ManInput>.inst.RegisterInputEventDelegate(62, InputActionEventType.ButtonJustPressed, OnGamePadUndoButtonPressed);
		}
		bool flag = Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad();
		m_GlyphTextField.gameObject.SetActive(flag);
		m_Icon.gameObject.SetActive(!flag);
		if (flag)
		{
			string textWithPlaceholder = "{0}";
			m_GlyphTextField.text = Singleton.Manager<Localisation>.inst.ReplaceGlyphPlaceHolders(textWithPlaceholder, new Localisation.GlyphInfo(62));
		}
		base.Show(context);
	}

	public override void Hide(object context)
	{
		Singleton.Manager<ManInput>.inst.UnregisterInputEventDelegate(62, InputActionEventType.ButtonJustPressed, OnGamePadUndoButtonPressed);
		base.Hide(context);
	}

	public void UpdateContext(object context)
	{
		if (context is Context { m_UndoTypes: var undoTypes } context2)
		{
			string text = m_TooltipStrings[(int)undoTypes].Value;
			if (undoTypes == UndoTypes.SendBlocksToInventory)
			{
				text = string.Format(text, context2.m_TechName);
			}
			m_TooltipComponent.SetText(text);
		}
		else
		{
			d.LogError("UIUndoButton.UpdateContext - Updating undo context without valid object!");
		}
	}

	private void OnGamePadUndoButtonPressed(InputActionEventData eventData)
	{
		OnButtonClicked();
	}

	public void OnButtonClicked()
	{
		Singleton.Manager<ManUndo>.inst.OnButtonPressed();
	}

	private void OnPool()
	{
		RegisterObscuredBy(ManHUD.HUDElementType.SkinsPalette);
		m_TooltipComponent = GetComponent<TooltipComponent>();
	}
}
