using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UIScreenTwitterAuth : UIScreen
{
	[FormerlySerializedAs("m_TwitterRequiredPanel")]
	[SerializeField]
	private RectTransform m_ConfirmPanel;

	[SerializeField]
	private TwitterPinUI m_PinUI;

	[SerializeField]
	private RectTransform m_AuthSuccessPanel;

	[SerializeField]
	private RectTransform m_AuthFailPanel;

	private Action m_SuccessAction;

	private Action m_FailAction;

	private Action m_CancelAction;

	private bool m_TriggerSuccessAction;

	private bool m_TriggerFailAction;

	private bool m_TriggerCancelAction;

	private bool m_LegacyMode;

	public void SetUseLegacyMode()
	{
		m_LegacyMode = true;
	}

	public void SetSuccessAction(Action action)
	{
		m_SuccessAction = action;
	}

	public void SetFailAction(Action action)
	{
		m_FailAction = action;
	}

	public void SetCancelAction(Action action)
	{
		m_CancelAction = action;
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		if (!fromStackPop)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.PopUpOpen);
		}
		SetActivePanel(null);
		m_TriggerSuccessAction = false;
		m_TriggerFailAction = false;
		m_TriggerCancelAction = true;
		if (m_LegacyMode)
		{
			Singleton.Manager<TwitterAPI>.inst.TryAuthenticateIgnoreUserEnabled(OnCheckLoggedInCallback);
			m_LegacyMode = false;
		}
		else
		{
			SetActivePanel(m_ConfirmPanel);
		}
	}

	public override void Hide()
	{
		base.Hide();
		Singleton.Manager<TwitterAPI>.inst.Cancel();
		SetActivePanel(null);
		if (m_TriggerCancelAction && m_CancelAction != null)
		{
			m_CancelAction();
		}
	}

	private void Update()
	{
		if (m_TriggerSuccessAction)
		{
			m_TriggerCancelAction = false;
			if (m_SuccessAction != null)
			{
				m_SuccessAction();
				m_SuccessAction = null;
			}
			m_TriggerSuccessAction = false;
		}
		else if (m_TriggerFailAction)
		{
			m_TriggerCancelAction = false;
			if (m_FailAction != null)
			{
				m_FailAction();
				m_FailAction = null;
			}
			m_TriggerFailAction = false;
		}
	}

	private void SetActivePanel(UnityEngine.Object panel)
	{
		if (m_ConfirmPanel != null)
		{
			m_ConfirmPanel.gameObject.SetActive(m_ConfirmPanel == panel);
		}
		if (m_PinUI != null)
		{
			m_PinUI.gameObject.SetActive(m_PinUI == panel);
		}
		if (m_AuthSuccessPanel != null)
		{
			m_AuthSuccessPanel.gameObject.SetActive(m_AuthSuccessPanel == panel);
		}
		if (m_AuthFailPanel != null)
		{
			m_AuthFailPanel.gameObject.SetActive(m_AuthFailPanel == panel);
		}
	}

	public void OnCheckLoggedInCallback(bool loggedIn)
	{
		if (loggedIn)
		{
			m_TriggerSuccessAction = true;
		}
		else
		{
			SetActivePanel(m_ConfirmPanel);
		}
	}

	public void OnBeginAuthentication()
	{
		SetActivePanel(null);
		Singleton.Manager<TwitterAPI>.inst.Authenticate(OnTwitterLoginSuccess, OnTwitterLoginFail, m_PinUI);
	}

	public void OnTriggerSuccess()
	{
		m_TriggerSuccessAction = true;
	}

	public void OnTriggerFailure()
	{
		m_TriggerFailAction = true;
	}

	public void OnTwitterLoginSuccess()
	{
		SetActivePanel(m_AuthSuccessPanel);
	}

	public void OnTwitterLoginFail()
	{
		SetActivePanel(m_AuthFailPanel);
	}

	public void OnContinueFromFailedScreen()
	{
		OnBeginAuthentication();
	}
}
