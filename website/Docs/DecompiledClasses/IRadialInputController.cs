using UnityEngine;

public interface IRadialInputController
{
	Vector2 GetAnchorPosition();

	void Activate();

	void Deactivate();

	void Update();

	void SetCustomPosition(Vector2 position);

	Vector2 GetRelativePosition();

	bool IsSelecting();

	bool DidSelect();

	bool DidCancel();

	void SetModal(bool modal);

	bool IsModal();

	bool IsCursorInsideRect(RectTransform rect);

	bool IsGamePad();
}
