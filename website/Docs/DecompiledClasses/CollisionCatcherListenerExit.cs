using UnityEngine;

public class CollisionCatcherListenerExit : CollisionCatcherEventListener
{
	private void OnCollisionExit(Collision collision)
	{
		if (IsEnabled)
		{
			CollisionEvent.Send(CollisionCatcher.Type.Exit, collision);
		}
	}
}
