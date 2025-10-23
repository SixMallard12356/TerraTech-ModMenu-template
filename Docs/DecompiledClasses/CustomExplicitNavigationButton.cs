using UnityEngine;
using UnityEngine.UI;

public class CustomExplicitNavigationButton : ButtonWithTooltip
{
	public enum Direction
	{
		UP,
		RIGHT,
		DOWN,
		LEFT
	}

	[HideInInspector]
	[SerializeField]
	private Selectable m_UpSelectable;

	[SerializeField]
	[HideInInspector]
	private bool m_UseUpSelectable;

	[HideInInspector]
	[SerializeField]
	private Selectable m_DownSelectable;

	[SerializeField]
	[HideInInspector]
	private bool m_UseDownSelectable;

	[HideInInspector]
	[SerializeField]
	private Selectable m_LeftSelectable;

	[SerializeField]
	[HideInInspector]
	private bool m_UseLeftSelectable;

	[SerializeField]
	[HideInInspector]
	private Selectable m_RightSelectable;

	[SerializeField]
	[HideInInspector]
	private bool m_UseRightSelectable;

	private bool[] m_DisabledDirections = new bool[4];

	public override Selectable FindSelectableOnUp()
	{
		if (IsDirectionDisabled(Direction.UP))
		{
			return null;
		}
		if (!m_UseUpSelectable)
		{
			return base.FindSelectableOnUp();
		}
		return m_UpSelectable;
	}

	public override Selectable FindSelectableOnDown()
	{
		if (IsDirectionDisabled(Direction.DOWN))
		{
			return null;
		}
		if (!m_UseDownSelectable)
		{
			return base.FindSelectableOnDown();
		}
		return m_DownSelectable;
	}

	public override Selectable FindSelectableOnLeft()
	{
		if (IsDirectionDisabled(Direction.LEFT))
		{
			return null;
		}
		if (!m_UseLeftSelectable)
		{
			return base.FindSelectableOnLeft();
		}
		return m_LeftSelectable;
	}

	public override Selectable FindSelectableOnRight()
	{
		if (IsDirectionDisabled(Direction.RIGHT))
		{
			return null;
		}
		if (!m_UseRightSelectable)
		{
			return base.FindSelectableOnRight();
		}
		return m_RightSelectable;
	}

	public void DisableDirection(Direction direction, bool disabled)
	{
		m_DisabledDirections[(int)direction] = disabled;
	}

	private bool IsDirectionDisabled(Direction direction)
	{
		return m_DisabledDirections[(int)direction];
	}

	private void OnRecycle()
	{
		for (int i = 0; i < m_DisabledDirections.Length; i++)
		{
			m_DisabledDirections[i] = false;
		}
	}
}
