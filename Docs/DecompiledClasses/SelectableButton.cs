using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableButton : Button, ISelectHandler, IEventSystemHandler
{
	public Event<bool> OnSelected;

	public override void OnSelect(BaseEventData eventData)
	{
		base.OnSelect(eventData);
		OnSelected.Send(paramA: true);
	}

	public override void OnDeselect(BaseEventData eventData)
	{
		base.OnDeselect(eventData);
		OnSelected.Send(paramA: false);
	}
}
