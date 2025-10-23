using UnityEngine;

public class CollisionCatcherEventListener : MonoBehaviour
{
	public bool IsEnabled;

	public Event<CollisionCatcher.Type, Collision> CollisionEvent;
}
