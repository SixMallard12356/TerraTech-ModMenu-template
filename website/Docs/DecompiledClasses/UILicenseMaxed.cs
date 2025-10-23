using System;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class UILicenseMaxed : UIHUDElement
{
	[Serializable]
	private struct MessageOverride
	{
		public FactionSubTypes corp;

		public LocalisedString message;
	}

	[SerializeField]
	private Image m_FactionIcon;

	[SerializeField]
	private Text m_Title;

	[SerializeField]
	private Text m_BodyText;

	[SerializeField]
	private Button m_DismissButton;

	[SerializeField]
	private Transform m_GamepadDismissPrompt;

	[SerializeField]
	private MessageOverride[] m_MessageOverrides;

	public override void Show(object context)
	{
		if (!base.IsVisible)
		{
			base.Show(context);
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.FullscreenUI);
		}
		Setup((FactionSubTypes)context);
		bool flag = Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad();
		m_DismissButton.gameObject.SetActive(!flag);
		m_GamepadDismissPrompt.gameObject.SetActive(flag);
		Singleton.Manager<ManInput>.inst.RegisterInputEventDelegate(22, InputActionEventType.ButtonJustPressed, OnCancelPressed);
	}

	public override void Hide(object context)
	{
		Singleton.Manager<ManInput>.inst.UnregisterInputEventDelegate(22, InputActionEventType.ButtonJustPressed, OnCancelPressed);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.FullscreenUI);
		base.Hide(context);
	}

	public void Close()
	{
		HideSelf();
	}

	private void Setup(FactionSubTypes corporation)
	{
		m_FactionIcon.sprite = Singleton.Manager<ManUI>.inst.GetCorpIcon(corporation);
		string corporationName = StringLookup.GetCorporationName(corporation);
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Licensing, 14);
		m_Title.text = string.Format(localisedString, corporationName);
		string text = null;
		if (m_MessageOverrides != null)
		{
			MessageOverride[] messageOverrides = m_MessageOverrides;
			for (int i = 0; i < messageOverrides.Length; i++)
			{
				MessageOverride messageOverride = messageOverrides[i];
				if (messageOverride.corp == corporation)
				{
					text = messageOverride.message.Value;
				}
			}
		}
		if (text == null)
		{
			text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Licensing, 15);
		}
		m_BodyText.text = string.Format(text, corporationName);
	}

	private void OnCancelPressed(InputActionEventData eventData)
	{
		if (eventData.IsCurrentInputSource(ControllerType.Joystick) && Singleton.Manager<ManUI>.inst.IsStackEmpty())
		{
			m_DismissButton.onClick.Invoke();
		}
	}

	private void OnSpawn()
	{
		AddElementToGroup(ManHUD.HUDGroup.Main);
		AddElementToGroup(ManHUD.HUDGroup.GamepadCursorHidingElements);
	}
}
