#define UNITY_EDITOR
using MonsterLove.StateMachine;
using UnityEngine;
using UnityEngine.UI;

public class UIRadialMenuOptionDelayTimer : UIRadialMenuOptionWithWarning
{
	public enum States
	{
		Disabled,
		Locked,
		Waiting,
		Armed
	}

	public enum TooltipReason
	{
		Nil,
		Default,
		NoInventory,
		EnemiesNearby
	}

	[SerializeField]
	private Sprite m_WaitingHighlightSprite;

	[SerializeField]
	private float m_DelayTime;

	[SerializeField]
	private Image m_TimeoutBar;

	[SerializeField]
	private LocalisedString m_TooltipEnemiesNearby;

	private StateMachine<States> m_StateMachine;

	private float m_WaitingStartTime;

	private TooltipReason m_PrevTooltipContext;

	private bool m_CanLock = true;

	public override bool IsSelected => m_StateMachine.State == States.Armed;

	public void SetCanLock(bool canLock)
	{
		m_CanLock = canLock;
		DoStateTransition(SelectionState.Normal, instant: true);
	}

	public override void Deselect()
	{
		if (m_StateMachine.State != States.Disabled)
		{
			m_StateMachine.ChangeState(States.Locked);
		}
	}

	public override void SetIsInside(bool isInside)
	{
		d.Assert(!isInside || m_StateMachine != null, "Setting Radial Option Delay Timer to inside before state machine setup");
		if (m_StateMachine != null)
		{
			switch (m_StateMachine.State)
			{
			case States.Disabled:
				base.SetIsInside(isInside);
				break;
			case States.Locked:
				Locked_SetIsInside(isInside);
				break;
			case States.Waiting:
				Waiting_SetIsInside(isInside);
				break;
			case States.Armed:
				Armed_SetIsInside(isInside);
				break;
			}
		}
	}

	public override void SetIsAllowed(bool isAllowed)
	{
		if (isAllowed)
		{
			if (m_StateMachine.State == States.Disabled)
			{
				m_StateMachine.ChangeState(States.Locked);
			}
		}
		else if (m_StateMachine.State != States.Disabled)
		{
			m_StateMachine.ChangeState(States.Disabled);
		}
	}

	public void SetTooltip(TooltipReason context)
	{
		if (m_PrevTooltipContext != context)
		{
			string text = string.Empty;
			UITooltipOptions mode = UITooltipOptions.Default;
			switch (context)
			{
			case TooltipReason.Default:
				text = m_TooltipString.Value;
				mode = UITooltipOptions.Default;
				break;
			case TooltipReason.NoInventory:
				text = m_TooltipWarningString.Value;
				mode = UITooltipOptions.Warning;
				break;
			case TooltipReason.EnemiesNearby:
				text = m_TooltipEnemiesNearby.Value;
				mode = UITooltipOptions.Warning;
				break;
			default:
				d.LogWarning("UIRadialMenuOptionDelayTimer.SetTooltip: No case provided for " + context);
				break;
			}
			base.TooltipComponent.SetText(text);
			base.TooltipComponent.SetMode(mode);
			m_PrevTooltipContext = context;
		}
	}

	private void Disabled_Enter()
	{
		SetSpriteHighlight(isDefault: false);
		m_IsAllowed = false;
	}

	private void Disabled_Exit()
	{
		SetSpriteHighlight(isDefault: true);
		m_IsAllowed = true;
	}

	private void Locked_Enter()
	{
		DoStateTransition(SelectionState.Normal, instant: false);
		UpdateTooltip(showTooltip: false);
	}

	private void Locked_SetIsInside(bool isInside)
	{
		if (isInside)
		{
			m_StateMachine.ChangeState(States.Waiting);
		}
	}

	private void Waiting_Enter()
	{
		UpdateTooltip(showTooltip: true);
		if (m_CanLock)
		{
			m_WaitingStartTime = Time.time;
			m_TimeoutBar.fillAmount = 1f;
			m_TimeoutBar.gameObject.SetActive(value: true);
			SpriteState spriteState = base.spriteState;
			spriteState.highlightedSprite = m_WaitingHighlightSprite;
			base.spriteState = spriteState;
		}
		DoStateTransition(SelectionState.Highlighted, instant: true);
		if (!m_CanLock)
		{
			m_StateMachine.ChangeState(States.Armed);
		}
	}

	private void Waiting_Update()
	{
		float num = Time.time - m_WaitingStartTime;
		float num2 = num / m_DelayTime;
		m_TimeoutBar.fillAmount = Mathf.Clamp01(1f - num2);
		if (num >= m_DelayTime)
		{
			m_StateMachine.ChangeState(States.Armed);
		}
	}

	private void Waiting_SetIsInside(bool isInside)
	{
		if (!isInside)
		{
			m_StateMachine.ChangeState(States.Locked);
		}
	}

	private void Waiting_Exit()
	{
		m_TimeoutBar.gameObject.SetActive(value: false);
		SpriteState spriteState = base.spriteState;
		spriteState.highlightedSprite = m_DefautHighlight;
		base.spriteState = spriteState;
		DoStateTransition(SelectionState.Highlighted, instant: true);
	}

	private void Armed_Enter()
	{
		UpdateTooltip(showTooltip: true);
	}

	private void Armed_SetIsInside(bool isInside)
	{
		if (!isInside && m_CanLock)
		{
			m_StateMachine.ChangeState(States.Locked);
		}
	}

	private void OnPool()
	{
		m_StateMachine = StateMachine<States>.Initialize(this);
	}

	private void OnSpawn()
	{
		m_TimeoutBar.gameObject.SetActive(value: false);
		m_StateMachine.ChangeState(States.Disabled);
	}
}
