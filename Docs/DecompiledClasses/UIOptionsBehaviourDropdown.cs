#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using MonsterLove.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIOptionsBehaviourDropdown : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler, IPointerClickHandler, ISubmitHandler
{
	private enum States
	{
		Idle,
		Highlighted,
		Focused
	}

	[SerializeField]
	private Dropdown m_Target;

	private Action OnSelectHook;

	private Action OnDeselectHook;

	public EventNoParams DropdownOpenedEvent;

	public EventNoParams DropdownClosedEvent;

	private Button m_Button;

	private StateMachine<States> m_FSM;

	private int m_DropdownStartIndex;

	private Transform m_TargetTrans;

	public bool innerDropdownInteractable
	{
		set
		{
			m_Target.interactable = value;
		}
	}

	public bool interactable
	{
		set
		{
			m_Button.interactable = value;
			innerDropdownInteractable = value;
		}
	}

	public int value
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

	public List<Dropdown.OptionData> options
	{
		get
		{
			return m_Target.options;
		}
		set
		{
			m_Target.options = value;
		}
	}

	public Dropdown.DropdownEvent onValueChanged => m_Target.onValueChanged;

	public Dropdown Target => m_Target;

	public void Hide()
	{
		m_Target.Hide();
	}

	public void RefreshShownValue()
	{
		m_Target.RefreshShownValue();
	}

	public void ClearOptions()
	{
		m_Target.ClearOptions();
	}

	public void AddOptions(List<Dropdown.OptionData> options)
	{
		m_Target.AddOptions(options);
	}

	public void AddOptions(List<string> options)
	{
		m_Target.AddOptions(options);
	}

	public bool IsOpen()
	{
		return m_FSM.State == States.Focused;
	}

	public void SetValue(int value)
	{
		m_Target.SetValue(value);
	}

	private void Idle_Enter()
	{
		OnSelectHook = Idle_OnSelect;
	}

	private void Idle_Update()
	{
		if (IsShowing())
		{
			m_FSM.ChangeState(States.Focused);
		}
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
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(21, Highlighted_OnSubmit);
	}

	private void Highlighted_Update()
	{
		if (IsShowing())
		{
			m_FSM.ChangeState(States.Focused);
		}
	}

	private void Highlighted_OnSubmit(PayloadUIEventData evt)
	{
		evt.Use();
		m_FSM.ChangeState(States.Focused);
	}

	private void Highlighted_OnDeselect()
	{
		m_FSM.ChangeState(States.Idle);
	}

	private void Highlighted_Exit()
	{
		OnDeselectHook = null;
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(21, Highlighted_OnSubmit);
	}

	private void Focused_Enter()
	{
		DropdownOpenedEvent.Send();
		m_DropdownStartIndex = m_Target.value;
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(21, Focused_OnGlobalSubmit);
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(22, Focused_OnGlobalCancel);
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(41, Focused_OnGlobalTabHandler);
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(42, Focused_OnGlobalTabHandler);
		BaseEventData eventData = new BaseEventData(EventSystem.current);
		m_Target.OnSubmit(eventData);
	}

	private void Focused_Update()
	{
		if (FindDropDownList() == null)
		{
			m_FSM.ChangeState(States.Highlighted);
		}
	}

	private void Focused_OnGlobalSubmit(PayloadUIEventData evt)
	{
		evt.Use();
		m_FSM.ChangeState(States.Highlighted);
	}

	private void Focused_OnGlobalCancel(PayloadUIEventData evt)
	{
		evt.Use();
		m_Target.value = m_DropdownStartIndex;
		m_FSM.ChangeState(States.Highlighted);
	}

	private void Focused_OnGlobalTabHandler(PayloadUIEventData evt)
	{
		evt.Use();
	}

	private void Focused_Exit()
	{
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(21, Focused_OnGlobalSubmit);
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(22, Focused_OnGlobalCancel);
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(41, Focused_OnGlobalTabHandler);
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(42, Focused_OnGlobalTabHandler);
		m_Target.Hide();
		DropdownClosedEvent.Send();
	}

	private bool IsShowing()
	{
		return FindDropDownList() != null;
	}

	private Transform FindDropDownList()
	{
		Transform result = null;
		if (m_TargetTrans != null)
		{
			for (int i = 0; i < m_TargetTrans.childCount; i++)
			{
				Transform child = m_TargetTrans.GetChild(i);
				if (child.name == "Dropdown List")
				{
					result = child;
					break;
				}
			}
		}
		return result;
	}

	public void OnSelect(BaseEventData eventData)
	{
		if (OnSelectHook != null)
		{
			OnSelectHook();
			if (m_Target.interactable)
			{
				ManBtnPrompt.PromptData promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextSelect);
				Singleton.Manager<ManUI>.inst.UpdateScreenPrompt(promptDataByType);
			}
		}
	}

	public void OnDeselect(BaseEventData eventData)
	{
		if (OnDeselectHook != null)
		{
			OnDeselectHook();
			ManBtnPrompt.PromptData promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextSelect);
			Singleton.Manager<ManUI>.inst.ToggleBtnPrompt(active: false, promptDataByType.prompts[0].m_InlineGlyphs);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		m_Target.OnPointerClick(eventData);
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.DropDown);
	}

	public void OnSubmit(BaseEventData eventData)
	{
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.DropDown);
	}

	private void OnPool()
	{
		m_TargetTrans = m_Target.transform;
		m_FSM = StateMachine<States>.Initialize(this);
		d.Assert(m_Target != null, "Missing Target reference on UIOptionsBehaviourDropdown", this);
		m_Button = GetComponent<Button>();
		d.Assert(m_Button != null, "Missing Button component on UIOptionsBehaviourDropdown", this);
	}

	private void OnEnable()
	{
		if (m_FSM != null)
		{
			States newState = States.Idle;
			if (EventSystem.current.currentSelectedGameObject == base.gameObject)
			{
				newState = States.Highlighted;
			}
			m_FSM.ChangeState(newState);
			Transform transform = FindDropDownList();
			if (transform != null)
			{
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
	}

	private void OnDisable()
	{
		if (m_FSM != null)
		{
			m_FSM.ChangeState(States.Idle);
		}
	}
}
