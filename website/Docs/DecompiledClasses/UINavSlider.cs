using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UINavSlider : Slider
{
	public override void OnMove(AxisEventData eventData)
	{
		if (eventData.moveDir != MoveDirection.Left && eventData.moveDir != MoveDirection.Right)
		{
			base.OnMove(eventData);
		}
	}
}
