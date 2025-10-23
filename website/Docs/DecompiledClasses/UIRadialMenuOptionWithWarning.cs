using UnityEngine;
using UnityEngine.UI;

public class UIRadialMenuOptionWithWarning : UIRadialMenuOption
{
	[SerializeField]
	private Sprite m_WarningHighlight;

	[SerializeField]
	protected LocalisedString m_TooltipString;

	[SerializeField]
	protected LocalisedString m_TooltipWarningString;

	protected Sprite m_DefaultSprite;

	protected Sprite m_DefautHighlight;

	protected bool m_IsAllowed;

	public LocalisedString TooltipString
	{
		get
		{
			return m_TooltipString;
		}
		set
		{
			m_TooltipString = value;
		}
	}

	public LocalisedString TooltipWarningString
	{
		get
		{
			return m_TooltipWarningString;
		}
		set
		{
			m_TooltipWarningString = value;
		}
	}

	public bool IsAllowed => m_IsAllowed;

	public virtual bool IsSelected => m_IsInside;

	public override void Deselect()
	{
		base.Deselect();
		SetSpriteHighlight(m_IsAllowed);
	}

	public virtual void SetIsAllowed(bool isAllowed)
	{
		m_IsAllowed = isAllowed;
		SetSpriteHighlight(m_IsAllowed);
		if (base.TooltipComponent != null)
		{
			base.TooltipComponent.SetText(GetTooltipString());
			base.TooltipComponent.SetMode(GetTooltipMode());
		}
	}

	private string GetTooltipString()
	{
		if (!IsAllowed)
		{
			return m_TooltipWarningString.Value;
		}
		return m_TooltipString.Value;
	}

	private UITooltipOptions GetTooltipMode()
	{
		if (!IsAllowed)
		{
			return UITooltipOptions.Warning;
		}
		return UITooltipOptions.Default;
	}

	protected void SetSpriteHighlight(bool isDefault)
	{
		SpriteState spriteState = base.spriteState;
		base.image.sprite = (isDefault ? m_DefaultSprite : base.spriteState.disabledSprite);
		spriteState.highlightedSprite = (isDefault ? m_DefautHighlight : m_WarningHighlight);
		base.spriteState = spriteState;
	}

	private void OnPool()
	{
		m_DefaultSprite = base.image.sprite;
		m_DefautHighlight = base.spriteState.highlightedSprite;
	}
}
