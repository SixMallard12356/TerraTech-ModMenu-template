using System;
using MonsterLove.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIOptionsBehaviourScreenSize : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	private enum States
	{
		Idle,
		Highlighted,
		Focused
	}

	[SerializeField]
	private Slider m_Target;

	private Action OnSelectHook;

	private Action OnDeselectHook;

	private StateMachine<States> m_FSM;

	private float m_SliderStartValue;

	public float value
	{
		get
		{
			return m_Target.value;
		}
		set
		{
			m_Target.value = value;
		}
	}

	public Slider.SliderEvent onValueChanged => m_Target.onValueChanged;

	private void Idle_Enter()
	{
		OnSelectHook = Idle_OnSelect;
	}

	private void Idle_OnSelect()
	{
		m_FSM.ChangeState(States.Highlighted);
	}

	private void Idle_Exit()
	{
		OnSelectHook = null;
	}

	private void Highlighted_Enter()
	{
		if (!EventSystem.current.alreadySelecting)
		{
			EventSystem.current.SetSelectedGameObject(base.gameObject);
		}
		OnDeselectHook = Highlighted_OnDeselect;
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(21, OnSubmitPressedHighlighted);
	}

	private void Highlighted_Update()
	{
	}

	private void Highlighted_OnDeselect()
	{
		m_FSM.ChangeState(States.Idle);
	}

	private void Highlighted_Exit()
	{
		OnDeselectHook = null;
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(21, OnSubmitPressedHighlighted);
	}

	private void Focused_Enter()
	{
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(21, OnSubmitPressedFocused);
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(22, OnCancelPressedFocused);
		EventSystem.current.SetSelectedGameObject(m_Target.gameObject);
		m_SliderStartValue = m_Target.value;
		Singleton.Manager<CameraManager>.inst.StartModifyScreenSize();
	}

	private void Focused_Update()
	{
	}

	private void Focused_Exit()
	{
		Singleton.Manager<CameraManager>.inst.EndModifyScreenSize();
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(21, OnSubmitPressedFocused);
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(22, OnCancelPressedFocused);
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

	private void OnSubmitPressedHighlighted(PayloadUIEventData evt)
	{
		evt.Use();
		m_SliderStartValue = m_Target.value;
		m_FSM.ChangeState(States.Focused);
	}

	private void OnSubmitPressedFocused(PayloadUIEventData evt)
	{
		evt.Use();
		m_FSM.ChangeState(States.Highlighted);
	}

	private void OnCancelPressedFocused(PayloadUIEventData evt)
	{
		evt.Use();
		m_FSM.ChangeState(States.Highlighted);
		m_Target.value = m_SliderStartValue;
	}

	private void OnPool()
	{
		m_FSM = StateMachine<States>.Initialize(this);
	}

	private void OnEnable()
	{
		States newState = States.Idle;
		if (EventSystem.current.currentSelectedGameObject == base.gameObject)
		{
			newState = States.Highlighted;
		}
		m_FSM.ChangeState(newState);
	}

	private void OnDisable()
	{
		if (m_FSM != null)
		{
			m_FSM.ChangeState(States.Idle);
		}
	}
}
