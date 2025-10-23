#define UNITY_EDITOR
using System;

public class uScript_ShowPrompt : uScriptLogic
{
	public class Context
	{
		public State m_State;
	}

	public enum State
	{
		Showing,
		Accepted,
		Declined
	}

	private Context m_LastContext;

	public bool Out => true;

	public Context In(LocalisedString bodyText, LocalisedString acceptButtonText, LocalisedString rejectButtonText)
	{
		d.Assert(bodyText != null, "uScript_ShowPrompt has been passed a null bodyText");
		((UIScreenNotifications)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen)).Set((bodyText != null) ? bodyText.Value : "", AcceptHandler, DeclineHandler, acceptButtonText?.Value, rejectButtonText?.Value);
		Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
		m_LastContext = new Context();
		m_LastContext.m_State = State.Showing;
		return m_LastContext;
	}

	public void OnEnable()
	{
		ManUI inst = Singleton.Manager<ManUI>.inst;
		inst.OnScreenChangeEvent = (Action<bool, ManUI.ScreenType>)Delegate.Combine(inst.OnScreenChangeEvent, new Action<bool, ManUI.ScreenType>(OnScreenChangedEvent));
	}

	public void OnDisable()
	{
		ManUI inst = Singleton.Manager<ManUI>.inst;
		inst.OnScreenChangeEvent = (Action<bool, ManUI.ScreenType>)Delegate.Remove(inst.OnScreenChangeEvent, new Action<bool, ManUI.ScreenType>(OnScreenChangedEvent));
		m_LastContext = null;
	}

	private void AcceptHandler()
	{
		if (m_LastContext != null && m_LastContext.m_State == State.Showing)
		{
			m_LastContext.m_State = State.Accepted;
		}
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	private void DeclineHandler()
	{
		if (m_LastContext != null && m_LastContext.m_State == State.Showing)
		{
			m_LastContext.m_State = State.Declined;
		}
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	private void OnScreenChangedEvent(bool pushed, ManUI.ScreenType screenType)
	{
		if (screenType == ManUI.ScreenType.NotificationScreen && !pushed && m_LastContext != null && m_LastContext.m_State == State.Showing)
		{
			m_LastContext.m_State = State.Declined;
		}
	}
}
