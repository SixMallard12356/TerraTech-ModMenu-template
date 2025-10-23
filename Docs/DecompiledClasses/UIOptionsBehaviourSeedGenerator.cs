#define UNITY_EDITOR
using System;
using MonsterLove.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIOptionsBehaviourSeedGenerator : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	private enum States
	{
		Idle,
		Highlighted,
		Focused
	}

	[SerializeField]
	private InputField m_Target;

	[FormerlySerializedAs("M_SeedGenerator")]
	[SerializeField]
	private UISeedGenerator m_SeedGenerator;

	private Action OnSelectHook;

	private Action OnDeselectHook;

	private StateMachine<States> m_FSM;

	private string m_PreInputName;

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
	}

	private void Highlighted_Update()
	{
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(57))
		{
			m_FSM.ChangeState(States.Focused);
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(21))
		{
			m_SeedGenerator.Generate();
		}
	}

	private void Highlighted_OnDeselect()
	{
		m_FSM.ChangeState(States.Idle);
	}

	private void Highlighted_Exit()
	{
		OnDeselectHook = null;
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
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(22, OnCancelPressed);
	}

	private void Focused_Update()
	{
	}

	private void Focused_Exit()
	{
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(22, OnCancelPressed);
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

	private void OnCancelPressed(PayloadUIEventData evt)
	{
		evt.Use();
		m_Target.DeactivateInputField();
		EventSystem.current.SetSelectedGameObject(base.gameObject);
		m_FSM.ChangeState(States.Highlighted);
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
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.VirtualKeyboard, 5);
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.VirtualKeyboard, 6);
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
		}, keyboardTitle: localisedString, keyboardDesc: localisedString2, defaultText: m_Target.text);
	}

	private void OnDisable()
	{
		if (m_FSM != null)
		{
			m_FSM.ChangeState(States.Idle);
		}
	}
}
