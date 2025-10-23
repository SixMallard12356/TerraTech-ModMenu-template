using UnityEngine;

public class TriggerCatcherListenerStay : TriggerEventListener
{
	private void OnTriggerStay(Collider other)
	{
		Event.Send(TriggerCatcher.Interaction.Stay, other);
	}
}
