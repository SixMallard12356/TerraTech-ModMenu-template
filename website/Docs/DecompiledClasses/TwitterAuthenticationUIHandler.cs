#define UNITY_EDITOR
using System;

public class TwitterAuthenticationUIHandler : Singleton.Manager<TwitterAuthenticationUIHandler>
{
	public EventNoParams OnShowAuthenticatePopup;

	public EventNoParams OnHideAuthenticatePopup;

	private Event<bool> OnComplete;

	private EventNoParams OnCancel;

	private bool m_AuthenticateInProgress;

	private bool m_LoginSuccess;

	private bool m_TriggerLoginCallbackAction;

	private UIScreenTwitterAuth m_TwitterAuthScreen;

	public void TryAuthenticateAsync(Action<bool> onComplete, Action onCancel = null)
	{
		d.Assert(!m_AuthenticateInProgress, "Trying to start a new Twitter authentication request (TryAuthenticateAsync) while already have one running!");
		if (!m_AuthenticateInProgress)
		{
			OnComplete.Clear();
			OnCancel.Clear();
			OnComplete.Subscribe(onComplete);
			if (onCancel != null)
			{
				OnCancel.Subscribe(onCancel);
			}
			m_AuthenticateInProgress = true;
			m_TriggerLoginCallbackAction = false;
			Singleton.Manager<TwitterAPI>.inst.TryAuthenticateIgnoreUserEnabled(OnTwitterCheckLoggedInCallback);
		}
	}

	public void CancelAsync()
	{
		if (m_AuthenticateInProgress)
		{
			Singleton.Manager<TwitterAPI>.inst.UserEnabled = false;
			Singleton.Manager<TwitterAPI>.inst.Cancel();
			m_AuthenticateInProgress = false;
			m_TriggerLoginCallbackAction = false;
			OnCancel.Send();
			ClearEventHandlers();
		}
	}

	private void OnTwitterCheckLoggedInCallback(bool loggedIn)
	{
		m_LoginSuccess = loggedIn;
		m_TriggerLoginCallbackAction = true;
	}

	private void HandleTwitterAuthResult(bool twitterAPIenabled)
	{
		Singleton.Manager<TwitterAPI>.inst.UserEnabled = twitterAPIenabled;
		OnComplete.Send(twitterAPIenabled);
		ClearEventHandlers();
	}

	private void ClearEventHandlers()
	{
		OnComplete.Clear();
		OnCancel.Clear();
		OnShowAuthenticatePopup.Clear();
		OnHideAuthenticatePopup.Clear();
	}

	private void Update()
	{
		if (!m_AuthenticateInProgress || !m_TriggerLoginCallbackAction)
		{
			return;
		}
		m_AuthenticateInProgress = false;
		m_TriggerLoginCallbackAction = false;
		if (m_LoginSuccess)
		{
			Singleton.Manager<TwitterAPI>.inst.UserEnabled = true;
			OnComplete.Send(paramA: true);
			ClearEventHandlers();
			return;
		}
		m_TwitterAuthScreen = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.TwitterAuth) as UIScreenTwitterAuth;
		m_TwitterAuthScreen.SetFailAction(delegate
		{
			Singleton.Manager<TwitterAPI>.inst.UserEnabled = false;
			OnComplete.Send(paramA: false);
			Singleton.Manager<ManUI>.inst.PopScreen();
			OnHideAuthenticatePopup.Send();
			ClearEventHandlers();
		});
		m_TwitterAuthScreen.SetSuccessAction(delegate
		{
			Singleton.Manager<TwitterAPI>.inst.UserEnabled = true;
			OnComplete.Send(paramA: true);
			Singleton.Manager<ManUI>.inst.PopScreen();
			OnHideAuthenticatePopup.Send();
			ClearEventHandlers();
			Singleton.Manager<ManProfile>.inst.Save();
		});
		m_TwitterAuthScreen.SetCancelAction(delegate
		{
			Singleton.Manager<TwitterAPI>.inst.UserEnabled = false;
			OnComplete.Send(paramA: false);
			OnHideAuthenticatePopup.Send();
			ClearEventHandlers();
		});
		OnShowAuthenticatePopup.Send();
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(m_TwitterAuthScreen);
	}
}
