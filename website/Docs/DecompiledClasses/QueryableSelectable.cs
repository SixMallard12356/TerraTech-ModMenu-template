using UnityEngine.UI;

public class QueryableSelectable : Selectable
{
	public new bool IsHighlighted => base.currentSelectionState == SelectionState.Highlighted;

	public bool IsSelected => base.currentSelectionState == SelectionState.Pressed;
}
