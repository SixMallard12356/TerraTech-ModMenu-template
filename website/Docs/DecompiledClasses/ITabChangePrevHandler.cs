using UnityEngine.EventSystems;

public interface ITabChangePrevHandler : IEventSystemHandler
{
	void OnTabChangePrev(BaseEventData eventData);
}
