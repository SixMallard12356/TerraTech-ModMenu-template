using UnityEngine;

public class TriggerCatcherListenerExit : TriggerEventListener
{
	private void OnTriggerExit(Collider other)
	{
		Event.Send(TriggerCatcher.Interaction.Exit, other);
	}
}
