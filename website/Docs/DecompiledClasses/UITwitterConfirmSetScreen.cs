using System;
using UnityEngine;

public class UITwitterConfirmSetScreen : MonoBehaviour
{
	public enum OnClickBehaviour
	{
		GoBack,
		StartMode,
		GoToScreen
	}

	[SerializeField]
	private OnClickBehaviour m_NoButtonBehaviour;

	[SerializeField]
	private OnClickBehaviour m_YesButtonBehaviour;

	[SerializeField]
	[Tooltip("This is only used for the GoToScreen Behaviour")]
	[SortedEnum]
	private ManUI.ScreenType m_ScreenType;

	private Action m_GoBackAction;

	private Action m_StartModeAction;

	private Action m_GoToScreenAction;

	private Action GetAction(OnClickBehaviour behaviour)
	{
		Action result = null;
		switch (behaviour)
		{
		case OnClickBehaviour.GoBack:
			result = m_GoBackAction;
			break;
		case OnClickBehaviour.StartMode:
			result = m_StartModeAction;
			break;
		case OnClickBehaviour.GoToScreen:
			result = m_GoToScreenAction;
			break;
		}
		return result;
	}

	public void OnButtonClicked()
	{
		Action action = GetAction(m_YesButtonBehaviour);
		Action action2 = GetAction(m_NoButtonBehaviour);
		UIScreenTwitterAuth uIScreenTwitterAuth = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.TwitterAuth) as UIScreenTwitterAuth;
		if ((bool)uIScreenTwitterAuth)
		{
			uIScreenTwitterAuth.SetSuccessAction(action);
			uIScreenTwitterAuth.SetFailAction(action2);
		}
		uIScreenTwitterAuth.SetUseLegacyMode();
	}

	private void Start()
	{
		m_GoBackAction = delegate
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
		};
		m_StartModeAction = delegate
		{
			Singleton.Manager<ManUI>.inst.ExitAllScreens();
			Singleton.Manager<ManGameMode>.inst.NextModeSetting.SwitchToMode();
		};
		m_GoToScreenAction = delegate
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
			Singleton.Manager<ManUI>.inst.GoToScreen(m_ScreenType);
		};
	}
}
