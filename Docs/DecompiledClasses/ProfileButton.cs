using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProfileButton : Button
{
	public Event<ProfileButton> Selected;

	public override void OnSelect(BaseEventData eventData)
	{
		base.OnSelect(eventData);
		Selected.Send(this);
		eventData.Use();
	}
}
