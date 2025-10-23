using UnityEngine.UI;

public class UIRadialMenuSlider : Slider
{
	public void SetIsHighlighted(bool isHighlighted)
	{
		if (isHighlighted)
		{
			DoStateTransition(SelectionState.Highlighted, instant: false);
		}
		else
		{
			DoStateTransition(SelectionState.Normal, instant: false);
		}
	}
}
