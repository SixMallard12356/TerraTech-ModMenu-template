using UnityEngine.EventSystems;

public interface IInteractionCursorExitHandler : IEventSystemHandler
{
	void OnInteractionCursorExit(PointerEventData eventData);
}
