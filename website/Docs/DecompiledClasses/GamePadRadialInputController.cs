using UnityEngine;

public class GamePadRadialInputController : IRadialInputController
{
	private Vector2 m_AnchoredPosition;

	private float m_Scale = 150f;

	private bool m_Selected;

	private bool m_Cancelled;

	public void Activate()
	{
		UIHUDElement hudElement = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.InteractionMode);
		if ((bool)hudElement)
		{
			m_AnchoredPosition = hudElement.GetComponent<RectTransform>().anchoredPosition;
		}
		else
		{
			m_AnchoredPosition = Singleton.Manager<ManHUD>.inst.GetMousePositionOnScreen();
		}
	}

	public void Deactivate()
	{
	}

	public bool IsSelecting()
	{
		return !m_Selected;
	}

	public bool DidSelect()
	{
		bool selected = m_Selected;
		m_Selected = false;
		return selected;
	}

	public bool DidCancel()
	{
		bool cancelled = m_Cancelled;
		m_Cancelled = false;
		return cancelled;
	}

	public Vector2 GetRelativePosition()
	{
		Vector2 vector = Singleton.Manager<ManInput>.inst.GetAxis2D(19, 20);
		if (vector.sqrMagnitude > 1f)
		{
			vector = vector.normalized;
		}
		return vector * m_Scale;
	}

	public void Update()
	{
		m_Selected = Singleton.Manager<ManInput>.inst.GetButtonDown(21);
		m_Cancelled = Singleton.Manager<ManInput>.inst.GetButtonDown(22);
	}

	public void SetCustomPosition(Vector2 position)
	{
		m_AnchoredPosition = position;
	}

	public Vector2 GetAnchorPosition()
	{
		return m_AnchoredPosition;
	}

	public void SetModal(bool modal)
	{
	}

	public bool IsModal()
	{
		return false;
	}

	public bool IsCursorInsideRect(RectTransform rect)
	{
		return false;
	}

	public bool IsGamePad()
	{
		return true;
	}
}
