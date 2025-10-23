using System;
using MonsterLove.StateMachine;
using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIOptionsBehaviourKeyBinding : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	private enum States
	{
		Idle,
		Highlighted,
		Focused
	}

	[SerializeField]
	private int m_RewiredActionID;

	[SerializeField]
	private InputAxisMapping m_Axis;

	[SerializeField]
	private bool m_ActionUsesNegative;

	[SerializeField]
	private bool m_FlipNegativePositive;

	[SerializeField]
	private LocalisedString m_Description;

	[SerializeField]
	private LocalisedString m_DescriptionNegative;

	[SerializeField]
	private bool m_KeyBindingRequired;

	[Header("Internal links")]
	[SerializeField]
	private UILocalisedText m_Text;

	[SerializeField]
	private UILocalisedText m_SubText;

	[SerializeField]
	private UILocalisedText m_SubTextNegative;

	[SerializeField]
	private RectTransform m_PositiveContainer;

	[SerializeField]
	private RectTransform m_NegativeContainer;

	[SerializeField]
	private UISchemaOptionsAxisString m_AxisString;

	[SerializeField]
	private RectTransform m_WarningContainer;

	[SerializeField]
	private Button m_TargetA;

	[SerializeField]
	private Button m_TargetB;

	private Action OnSelectHook;

	private Action OnDeselectHook;

	private StateMachine<States> m_FSM;

	private UIKeyBindButton m_ButtonA;

	private UIKeyBindButton m_ButtonB;

	private bool m_ShowingWarning;

	private Transform m_WarningObject;

	public int CountAssignedKeys()
	{
		int num = 0;
		if (m_ButtonA != null && m_ButtonA.IsAssignedToAKey)
		{
			num++;
		}
		if (m_ButtonB != null && m_ButtonB.IsAssignedToAKey)
		{
			num++;
		}
		return num;
	}

	public void ShowWarning(GameObject warningPrefab)
	{
		if (!m_ShowingWarning)
		{
			m_WarningObject = warningPrefab.transform.Spawn();
			m_WarningObject.SetParent((m_WarningContainer != null) ? m_WarningContainer : base.transform, worldPositionStays: false);
			m_WarningObject.SetAsFirstSibling();
			RectTransform component = m_WarningObject.GetComponent<RectTransform>();
			component.offsetMin = default(Vector2);
			component.offsetMax = default(Vector2);
			m_ShowingWarning = true;
		}
	}

	public void RemoveWarning()
	{
		if (m_ShowingWarning)
		{
			m_WarningObject.Recycle();
			m_WarningObject = null;
			m_ShowingWarning = false;
		}
	}

	public bool ContainsButton(UIKeyBindDisplay button)
	{
		if (!(m_ButtonA == button))
		{
			return m_ButtonB == button;
		}
		return true;
	}

	public bool IsKeyBindingRequired()
	{
		return m_KeyBindingRequired;
	}

	public void Init()
	{
		States newState = States.Idle;
		if (EventSystem.current.currentSelectedGameObject == base.gameObject)
		{
			newState = States.Highlighted;
		}
		m_FSM.ChangeState(newState);
		if (m_TargetA != null)
		{
			m_ButtonA = m_TargetA.GetComponent<UIKeyBindButton>();
		}
		if (m_TargetB != null)
		{
			m_ButtonB = m_TargetB.GetComponent<UIKeyBindButton>();
		}
	}

	private void Idle_Enter()
	{
		OnSelectHook = Idle_OnSelect;
	}

	private void Idle_OnSelect()
	{
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.CheckBox);
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
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(21))
		{
			m_FSM.ChangeState(States.Focused);
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
		EventSystem.current.SetSelectedGameObject(m_TargetA.gameObject);
	}

	private void Focused_Update()
	{
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(22))
		{
			EventSystem.current.SetSelectedGameObject(base.gameObject);
			m_FSM.ChangeState(States.Highlighted);
		}
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

	private void OnPool()
	{
		m_FSM = StateMachine<States>.Initialize(this);
		if (!m_ActionUsesNegative && (bool)m_NegativeContainer)
		{
			m_NegativeContainer.gameObject.SetActive(value: false);
		}
		m_AxisString?.SetAxis(m_Axis);
		if ((bool)m_Text && (bool)m_SubText && (bool)m_SubTextNegative)
		{
			if (m_Axis == InputAxisMapping.Unmapped)
			{
				m_Text.gameObject.SetActive(value: true);
				m_SubText.gameObject.SetActive(value: false);
				m_SubTextNegative.gameObject.SetActive(value: false);
				m_Text.m_String = m_Description;
			}
			else
			{
				m_Text.gameObject.SetActive(value: false);
				m_SubText.gameObject.SetActive(value: true);
				m_SubText.m_String = m_Description;
				m_SubTextNegative.gameObject.SetActive(value: true);
				m_SubTextNegative.m_String = m_DescriptionNegative;
			}
		}
		if ((bool)m_PositiveContainer && (bool)m_NegativeContainer)
		{
			UIKeyBindButton[] componentsInChildren = m_PositiveContainer.GetComponentsInChildren<UIKeyBindButton>();
			foreach (UIKeyBindButton obj in componentsInChildren)
			{
				obj.m_ActionID = m_RewiredActionID;
				obj.m_AxisContribution = (m_FlipNegativePositive ? Pole.Negative : Pole.Positive);
			}
			componentsInChildren = m_NegativeContainer.GetComponentsInChildren<UIKeyBindButton>();
			foreach (UIKeyBindButton obj2 in componentsInChildren)
			{
				obj2.m_ActionID = m_RewiredActionID;
				obj2.m_AxisContribution = ((!m_FlipNegativePositive) ? Pole.Negative : Pole.Positive);
			}
		}
	}
}
