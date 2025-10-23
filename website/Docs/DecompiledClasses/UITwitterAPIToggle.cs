using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITwitterAPIToggle : MonoBehaviour
{
	[SerializeField]
	private Toggle m_TwitterToggle;

	[SerializeField]
	private UnityEvent m_OnTwitterAPIBeginAuthenticate;

	[SerializeField]
	private UnityEvent m_OnTwitterAPIAuthenticateCompleted;

	[SerializeField]
	private UnityEvent m_OnTwitterAPIDisable;

	private bool m_IgnoreToggleCallback;

	private void Awake()
	{
		m_TwitterToggle.isOn = Singleton.Manager<TwitterAPI>.inst.UserEnabled;
		m_TwitterToggle.onValueChanged.AddListener(OnToggleValueChanged);
	}

	public void OnToggleValueChanged(bool isOn)
	{
		if (!m_IgnoreToggleCallback)
		{
			if (isOn)
			{
				Singleton.Manager<TwitterAuthenticationUIHandler>.inst.TryAuthenticateAsync(OnTwitterCheckComplete, OnTwitterCheckCancel);
				if (m_OnTwitterAPIBeginAuthenticate != null)
				{
					m_OnTwitterAPIBeginAuthenticate.Invoke();
				}
			}
			else
			{
				OnTwitterCheckCancel();
			}
		}
		m_IgnoreToggleCallback = false;
	}

	private void OnTwitterCheckComplete(bool loggedIn)
	{
		SetToggle(loggedIn);
		if (m_OnTwitterAPIAuthenticateCompleted != null)
		{
			m_OnTwitterAPIAuthenticateCompleted.Invoke();
		}
	}

	private void OnTwitterCheckCancel()
	{
		SetToggle(toggled: false);
		Singleton.Manager<TwitterAuthenticationUIHandler>.inst.CancelAsync();
		if (m_OnTwitterAPIDisable != null)
		{
			m_OnTwitterAPIDisable.Invoke();
		}
	}

	private void SetToggle(bool toggled)
	{
		m_IgnoreToggleCallback = true;
		m_TwitterToggle.isOn = toggled;
	}
}
