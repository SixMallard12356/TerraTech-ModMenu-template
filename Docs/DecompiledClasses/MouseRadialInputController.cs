using UnityEngine;

public class MouseRadialInputController : IRadialInputController
{
	private Vector2 m_AnchoredPosition;

	private Vector2 m_CustomPosition;

	private bool m_Modal;

	private bool m_UsingCustomPosition;

	public void Activate()
	{
		m_AnchoredPosition = Singleton.Manager<ManHUD>.inst.GetMousePositionOnScreen();
	}

	public void Deactivate()
	{
		SetModal(modal: false);
		m_UsingCustomPosition = false;
	}

	public bool IsSelecting()
	{
		if (m_Modal)
		{
			return Input.GetMouseButton(0) | Input.GetMouseButton(1);
		}
		return Input.GetMouseButton(1);
	}

	public bool DidSelect()
	{
		if (m_Modal)
		{
			return Input.GetMouseButtonUp(0) | Input.GetMouseButtonUp(1);
		}
		return Input.GetMouseButtonUp(1);
	}

	public bool DidCancel()
	{
		return false;
	}

	public Vector2 GetRelativePosition()
	{
		return Singleton.Manager<ManHUD>.inst.GetMousePositionOnScreen() - GetAnchorPosition();
	}

	public void Update()
	{
	}

	public void SetCustomPosition(Vector2 position)
	{
		m_CustomPosition = position;
		m_UsingCustomPosition = true;
	}

	public Vector2 GetAnchorPosition()
	{
		if (m_UsingCustomPosition)
		{
			return m_CustomPosition;
		}
		return m_AnchoredPosition;
	}

	public void SetModal(bool modal)
	{
		m_Modal = modal;
	}

	public bool IsModal()
	{
		return m_Modal;
	}

	public bool IsCursorInsideRect(RectTransform rect)
	{
		Vector2 mousePositionInRect = Singleton.Manager<ManHUD>.inst.GetMousePositionInRect(rect);
		return rect.rect.Contains(mousePositionInRect);
	}

	public bool IsGamePad()
	{
		return false;
	}
}
