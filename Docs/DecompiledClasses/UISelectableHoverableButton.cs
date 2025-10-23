using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class UISelectableHoverableButton : Button
{
	public ButtonClickedEvent onPointerEnter = new ButtonClickedEvent();

	public ButtonClickedEvent onPointerExit = new ButtonClickedEvent();

	public UnityEvent onSelected = new UnityEvent();

	public UnityEvent onDeselected = new UnityEvent();

	[SerializeField]
	public Sprite m_OnStateSprite;

	private SpriteState m_OnSpriteState;

	private SpriteState m_NormalSpriteState;

	private bool m_IsOnSet;

	private bool m_Initialised;

	public virtual void Initialise()
	{
		if (!m_Initialised)
		{
			m_NormalSpriteState = new SpriteState
			{
				disabledSprite = base.spriteState.disabledSprite,
				highlightedSprite = base.spriteState.highlightedSprite,
				pressedSprite = base.spriteState.pressedSprite
			};
			m_OnSpriteState = new SpriteState
			{
				disabledSprite = m_OnStateSprite,
				highlightedSprite = m_OnStateSprite,
				pressedSprite = m_OnStateSprite
			};
			m_Initialised = true;
		}
		base.spriteState = m_NormalSpriteState;
		m_IsOnSet = false;
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);
		onPointerEnter.Invoke();
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
		onPointerExit.Invoke();
	}

	public override void OnSelect(BaseEventData eventData)
	{
		base.OnSelect(eventData);
		onSelected.Invoke();
	}

	public override void OnDeselect(BaseEventData eventData)
	{
		base.OnDeselect(eventData);
		onDeselected.Invoke();
	}

	public void SetOnState(bool isOn)
	{
		base.spriteState = (isOn ? m_OnSpriteState : m_NormalSpriteState);
		m_IsOnSet = isOn;
		if (!isOn)
		{
			DoStateTransition(SelectionState.Normal, instant: true);
		}
	}

	protected override void DoStateTransition(SelectionState state, bool instant)
	{
		if (state == SelectionState.Normal && m_IsOnSet)
		{
			base.DoStateTransition(SelectionState.Highlighted, instant);
		}
		else
		{
			base.DoStateTransition(state, instant);
		}
	}

	private void OnRecycle()
	{
		SetOnState(isOn: false);
	}
}
