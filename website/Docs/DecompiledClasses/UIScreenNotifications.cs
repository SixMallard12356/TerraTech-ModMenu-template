#define UNITY_EDITOR
using System;
using Rewired;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScreenNotifications : UIScreen
{
	[SerializeField]
	private TextMeshProUGUI m_Message;

	[SerializeField]
	private GameObject m_ImageContainer;

	[SerializeField]
	private RawImage m_Image;

	[SerializeField]
	private GameObject m_JoypadContainer;

	[SerializeField]
	private GameObject m_ButtonConatiner;

	[SerializeField]
	private Button m_OkButton;

	[SerializeField]
	private Button m_DeclineButton;

	[SerializeField]
	private TextMeshProUGUI m_Accept;

	[SerializeField]
	private TextMeshProUGUI m_Decline;

	[SerializeField]
	private TextMeshProUGUI m_AcceptJoypad;

	[SerializeField]
	private TextMeshProUGUI m_DeclineJoypad;

	[SerializeField]
	private float m_NoButtonsMinDisplayTime = 1f;

	[SerializeField]
	private float m_NoButtonsMaxDisplayTime = 8f;

	private bool m_NoButtonsMode;

	private Action m_NoButtonsAcceptAction;

	private float m_NoButtonsStartTime;

	private bool m_UseJoypad;

	private bool m_HasAccept;

	private bool m_HasDecline;

	private UIButtonData m_AcceptData;

	private UIButtonData m_DeclineData;

	private bool m_UseNewInputHandler;

	private int m_FrameCountWhenShown;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		m_FrameCountWhenShown = Time.frameCount;
		if (!fromStackPop)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.PopUpOpen);
		}
		BlockScreenExit(exitBlocked: true);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.FullscreenUI);
		EventSystem.current.SetSelectedGameObject(base.gameObject);
		if (m_UseNewInputHandler && m_UseJoypad)
		{
			Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(m_AcceptData.m_RewiredAction, OnUIAccept);
			Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(m_DeclineData.m_RewiredAction, OnUICancel);
		}
	}

	public override void Hide()
	{
		base.Hide();
		BlockScreenExit(exitBlocked: false);
		EventSystem.current.SetSelectedGameObject(null);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.FullscreenUI);
		if (m_UseNewInputHandler)
		{
			Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(m_AcceptData.m_RewiredAction, OnUIAccept);
			Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(m_DeclineData.m_RewiredAction, OnUICancel);
		}
		m_UseNewInputHandler = false;
	}

	public void Set(string notification)
	{
		SetMe(notification, null, null, null, null, -1, -1, null);
	}

	public void Set(string notification, Action Accept, string accept)
	{
		SetMe(notification, Accept, null, accept, null, 21, -1, null);
	}

	public void Set(string notification, Action Accept, Action Decline, string accept, string decline)
	{
		SetMe(notification, Accept, Decline, accept, decline, 21, 22, null);
	}

	public void Set(string notification, UIButtonData accept, UIButtonData decline)
	{
		SetMe(notification, accept.m_Callback, decline.m_Callback, accept.m_Label, decline.m_Label, accept.m_RewiredAction, decline.m_RewiredAction, null);
	}

	public void Set(string notification, UIButtonData accept, UIButtonData decline, Texture2D image)
	{
		SetMe(notification, accept.m_Callback, decline.m_Callback, accept.m_Label, decline.m_Label, accept.m_RewiredAction, decline.m_RewiredAction, image);
	}

	public void SetUseNewInputHandler(bool useNewInputHandler)
	{
		m_UseNewInputHandler = useNewInputHandler;
	}

	private void SetMe(string notification, Action acceptAction, Action declineAction, string acceptText, string declineText, int acceptActionID, int declineActionID, Texture2D image)
	{
		m_Message.text = notification;
		if (m_Image != null && m_ImageContainer != null)
		{
			m_ImageContainer.SetActive(image != null);
			m_Image.texture = image;
		}
		m_UseJoypad = Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled();
		m_HasAccept = !acceptText.NullOrEmpty();
		m_HasDecline = !declineText.NullOrEmpty();
		m_ButtonConatiner.SetActive(!m_UseJoypad);
		m_JoypadContainer.SetActive(m_UseJoypad);
		if (m_UseJoypad)
		{
			m_AcceptData = new UIButtonData
			{
				m_Label = acceptText,
				m_Callback = acceptAction,
				m_RewiredAction = acceptActionID
			};
			m_DeclineData = new UIButtonData
			{
				m_Label = declineText,
				m_Callback = declineAction,
				m_RewiredAction = declineActionID
			};
		}
		if (m_HasAccept)
		{
			if (m_UseJoypad)
			{
				d.Assert(m_AcceptData.m_RewiredAction >= 0, "UIScreenNotifications.SetMe has incorrectly configured rewired action for button " + acceptText);
				acceptText = "{0} " + acceptText;
				m_AcceptJoypad.text = Singleton.Manager<Localisation>.inst.ReplaceGlyphPlaceHolders(acceptText, new Localisation.GlyphInfo(acceptActionID));
				m_AcceptJoypad.gameObject.SetActive(value: true);
			}
			else
			{
				m_Accept.text = acceptText;
				m_OkButton.gameObject.SetActive(value: true);
				m_OkButton.onClick.RemoveAllListeners();
				if (acceptAction != null)
				{
					m_OkButton.onClick.AddListener(delegate
					{
						acceptAction();
					});
				}
			}
		}
		else if (m_UseJoypad)
		{
			m_AcceptJoypad.gameObject.SetActive(value: false);
		}
		else
		{
			m_OkButton.gameObject.SetActive(value: false);
		}
		if (m_HasDecline)
		{
			if (m_UseJoypad)
			{
				d.Assert(m_DeclineData.m_RewiredAction >= 0, "UIScreenNotifications.SetMe has incorrectly configured rewired action for button " + acceptText);
				declineText = "{0} " + declineText;
				m_DeclineJoypad.text = Singleton.Manager<Localisation>.inst.ReplaceGlyphPlaceHolders(declineText, new Localisation.GlyphInfo(declineActionID));
				m_DeclineJoypad.gameObject.SetActive(value: true);
			}
			else
			{
				m_Decline.text = declineText;
				m_DeclineButton.gameObject.SetActive(value: true);
				m_DeclineButton.onClick.RemoveAllListeners();
				if (declineAction != null)
				{
					m_DeclineButton.onClick.AddListener(delegate
					{
						declineAction();
					});
				}
			}
		}
		else if (m_UseJoypad)
		{
			m_DeclineJoypad.gameObject.SetActive(value: false);
		}
		else
		{
			m_DeclineButton.gameObject.SetActive(value: false);
		}
		m_NoButtonsMode = !m_OkButton.gameObject.activeSelf && !m_DeclineButton.gameObject.activeSelf && acceptAction != null;
		m_NoButtonsAcceptAction = acceptAction;
		m_NoButtonsStartTime = Time.realtimeSinceStartup;
	}

	private void OnUIAccept(PayloadUIEventData eventData)
	{
		if (Time.frameCount > m_FrameCountWhenShown && m_HasAccept && m_AcceptData.m_Callback != null)
		{
			m_AcceptData.m_Callback();
			eventData?.Use();
		}
	}

	private void OnUICancel(PayloadUIEventData eventData)
	{
		if (Time.frameCount > m_FrameCountWhenShown && m_HasDecline && m_DeclineData.m_Callback != null)
		{
			m_DeclineData.m_Callback();
			eventData.Use();
		}
	}

	private void Update()
	{
		if (m_NoButtonsMode)
		{
			float num = Time.realtimeSinceStartup - m_NoButtonsStartTime;
			bool flag = Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
			if (num > m_NoButtonsMaxDisplayTime || (num > m_NoButtonsMinDisplayTime && flag))
			{
				m_NoButtonsAcceptAction();
				m_NoButtonsMode = false;
			}
		}
		if (m_UseJoypad && !m_UseNewInputHandler)
		{
			if (m_HasAccept && Singleton.Manager<ManInput>.inst.GetButtonDown(m_AcceptData.m_RewiredAction, ControllerType.Joystick) && m_AcceptData.m_Callback != null)
			{
				m_AcceptData.m_Callback();
			}
			if (m_HasDecline && Singleton.Manager<ManInput>.inst.GetButtonDown(m_DeclineData.m_RewiredAction, ControllerType.Joystick) && m_DeclineData.m_Callback != null)
			{
				m_DeclineData.m_Callback();
			}
		}
	}
}
