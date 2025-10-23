using UnityEngine.EventSystems;

public interface IInteractionCursorEnterHandler : IEventSystemHandler
{
	void OnInteractionCursorEnter(PointerEventData eventData);
}
