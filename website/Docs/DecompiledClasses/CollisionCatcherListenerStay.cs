using UnityEngine;

public class CollisionCatcherListenerStay : CollisionCatcherEventListener
{
	private void OnCollisionStay(Collision collision)
	{
		if (IsEnabled)
		{
			CollisionEvent.Send(CollisionCatcher.Type.Stay, collision);
		}
	}
}
