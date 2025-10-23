using UnityEngine;

public class TriggerCatcherListenerEnter : TriggerEventListener
{
	private void OnTriggerEnter(Collider other)
	{
		Event.Send(TriggerCatcher.Interaction.Enter, other);
	}
}
