using UnityEngine.EventSystems;

public interface ITabChangeNextHandler : IEventSystemHandler
{
	void OnTabChangeNext(BaseEventData eventData);
}
