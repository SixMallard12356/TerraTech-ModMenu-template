#define UNITY_EDITOR
using System;
using MonsterLove.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIOptionsBehaviourInputField : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, ISelectHandler, IDeselectHandler
{
	private enum States
	{
		Idle,
		Highlighted,
		Focused
	}

	[SerializeField]
	private InputField m_Target;

	[SerializeField]
	private LocalisedString m_VirtualKeyboardTitleText;

	[SerializeField]
	private LocalisedString m_VirtualKeyboardDescText;

	private Action OnSelectHook;

	private Action OnDeselectHook;

	private Action OnPointerClickHook;

	private Action OnTextFieldLoseFocusHook;

	private StateMachine<States> m_FSM;

	private string m_PreInputName;

	private void Idle_Enter()
	{
		OnSelectHook = Idle_OnSelect;
		OnPointerClickHook = Idle_OnPointerClick;
	}

	private void Idle_OnSelect()
	{
		m_FSM.ChangeState(States.Highlighted);
	}

	private void Idle_OnPointerClick()
	{
		m_FSM.ChangeState(States.Focused);
	}

	private void Idle_Exit()
	{
		OnSelectHook = null;
		OnPointerClickHook = null;
	}

	private void Highlighted_Enter()
	{
		if (!EventSystem.current.alreadySelecting)
		{
			EventSystem.current.SetSelectedGameObject(base.gameObject);
		}
		OnDeselectHook = Highlighted_OnDeselect;
		OnPointerClickHook = Highlighted_OnPointerClick;
	}

	private void Highlighted_Update()
	{
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(21))
		{
			m_FSM.ChangeState(States.Focused);
		}
	}

	private void Highlighted_OnDeselect()
	{
		m_FSM.ChangeState(States.Idle);
	}

	private void Highlighted_OnPointerClick()
	{
		m_FSM.ChangeState(States.Focused);
	}

	private void Highlighted_Exit()
	{
		OnDeselectHook = null;
		OnPointerClickHook = null;
	}

	private void Focused_Enter()
	{
		if (VirtualKeyboard.IsRequired())
		{
			OpenVirtualKeyboard();
		}
		else
		{
			EventSystem.current.SetSelectedGameObject(m_Target.gameObject);
			m_Target.Select();
			m_Target.ActivateInputField();
		}
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(21, OnSubmitPressed);
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(22, OnCancelPressed);
		OnTextFieldLoseFocusHook = Focused_OnTextFieldLoseFocusHook;
	}

	private void Focused_Update()
	{
	}

	private void Focused_OnTextFieldLoseFocusHook()
	{
		m_FSM.ChangeState(States.Idle);
	}

	private void Focused_Exit()
	{
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(21, OnSubmitPressed);
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(22, OnCancelPressed);
		OnTextFieldLoseFocusHook = null;
	}

	public void OnSelect(BaseEventData eventData)
	{
		if (OnSelectHook != null)
		{
			OnSelectHook();
		}
	}

	public void OnDeselect(BaseEventData eventData)
	{
		if (OnDeselectHook != null)
		{
			OnDeselectHook();
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (OnPointerClickHook != null)
		{
			OnPointerClickHook();
		}
	}

	public void OnTargetEndEdit(string newTextUnused)
	{
		if (OnTextFieldLoseFocusHook != null)
		{
			OnTextFieldLoseFocusHook();
		}
	}

	private void OnSubmitPressed(PayloadUIEventData evt)
	{
		evt.Use();
		m_FSM.ChangeState(States.Highlighted);
	}

	private void OnCancelPressed(PayloadUIEventData evt)
	{
		evt.Use();
		m_Target.DeactivateInputField();
		EventSystem.current.SetSelectedGameObject(base.gameObject);
		m_FSM.ChangeState(States.Highlighted);
	}

	private void OnPool()
	{
		m_Target.onEndEdit.AddListener(OnTargetEndEdit);
	}

	private void OnEnable()
	{
		if (m_FSM == null)
		{
			m_FSM = StateMachine<States>.Initialize(this);
		}
		States newState = States.Idle;
		if (EventSystem.current.currentSelectedGameObject == base.gameObject)
		{
			newState = States.Highlighted;
		}
		m_FSM.ChangeState(newState);
	}

	private void OpenVirtualKeyboard()
	{
		string value = m_VirtualKeyboardTitleText.Value;
		string value2 = m_VirtualKeyboardDescText.Value;
		m_PreInputName = m_Target.text;
		VirtualKeyboard.PromptInput(onCompleteHandler: delegate(bool accepted, string result)
		{
			d.Log("OpenVirtualKeyboard - Accepted: " + accepted + " Input = " + ((result == null) ? "NULL" : result));
			if (accepted)
			{
				m_Target.text = result;
			}
			else
			{
				m_Target.text = m_PreInputName;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject);
			m_FSM.ChangeState(States.Highlighted);
		}, keyboardTitle: value, keyboardDesc: value2, defaultText: m_Target.text);
	}

	private void OnDisable()
	{
		if (m_FSM != null)
		{
			m_FSM.ChangeState(States.Idle);
		}
	}
}
