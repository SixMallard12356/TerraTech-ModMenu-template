using UnityEngine;

public class CollisionCatcherListenerEnter : CollisionCatcherEventListener
{
	private void OnCollisionEnter(Collision collision)
	{
		if (IsEnabled)
		{
			CollisionEvent.Send(CollisionCatcher.Type.Enter, collision);
		}
	}
}
