using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHUDOpenButton : Button, IPointerEnterHandler, IEventSystemHandler
{
	public Event<ManHUD.HUDElementType> OnHUDButtonHighlighted;

	private ManHUD.HUDElementType m_HUDElementType;

	public void SetHUDElementType(ManHUD.HUDElementType inpType)
	{
		m_HUDElementType = inpType;
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		OnHUDButtonHighlighted.Send(m_HUDElementType);
	}

	public override void OnSelect(BaseEventData eventData)
	{
		OnHUDButtonHighlighted.Send(m_HUDElementType);
		base.OnSelect(eventData);
	}
}
