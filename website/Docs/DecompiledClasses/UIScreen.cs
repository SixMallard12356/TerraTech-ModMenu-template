using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScreen : MonoBehaviour
{
	public enum State
	{
		Hide,
		Show
	}

	[SerializeField]
	protected GameObject m_DefaultUIElement;

	[SerializeField]
	public Button m_ExitButton;

	[SerializeField]
	private bool m_ExitBlocked;

	protected GameObject m_LastFocusBeforePopup;

	public ManUI.ScreenType Type { get; private set; }

	public State state { get; protected set; }

	public bool CanExit => !m_ExitBlocked;

	public virtual void ScreenInitialize(ManUI.ScreenType type)
	{
		Type = type;
	}

	public virtual void Show(bool fromStackPop)
	{
		if (state != State.Show)
		{
			SelectButton(m_DefaultUIElement);
			base.gameObject.SetActive(value: true);
			state = State.Show;
		}
	}

	public virtual void Hide()
	{
		if (state != State.Hide)
		{
			base.gameObject.SetActive(value: false);
			state = State.Hide;
		}
		m_LastFocusBeforePopup = null;
	}

	public virtual void ReturnFromPopup()
	{
		if ((bool)EventSystem.current)
		{
			EventSystem.current.SetSelectedGameObject(m_LastFocusBeforePopup);
		}
	}

	public virtual void HideBehindPopup()
	{
		if ((bool)EventSystem.current)
		{
			m_LastFocusBeforePopup = EventSystem.current.currentSelectedGameObject;
		}
	}

	public void SetupExitButton()
	{
		if ((bool)m_ExitButton)
		{
			m_ExitButton.onClick.AddListener(delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen(showPrev: false);
			});
		}
	}

	public void SelectButton(GameObject button)
	{
		if (button != null)
		{
			EventSystem.current.SetSelectedGameObject(null);
			Button component = button.GetComponent<Button>();
			if (component != null)
			{
				component.Select();
				component.OnSelect(null);
			}
			EventSystem.current.SetSelectedGameObject(button);
		}
	}

	public void BlockScreenExit(bool exitBlocked)
	{
		m_ExitBlocked = exitBlocked;
	}

	public virtual bool GoBack()
	{
		return true;
	}
}
