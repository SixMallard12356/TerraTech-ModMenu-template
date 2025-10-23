using System;

public class uScript_CancelEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EncounterCancelledEventArgs args);

	public class EncounterCancelledEventArgs : EventArgs
	{
		private Encounter m_CancelledEncounter;

		[FriendlyName("Cancelled Encounter")]
		public Encounter CancelledEncounter => m_CancelledEncounter;

		public EncounterCancelledEventArgs(Encounter encounter)
		{
			m_CancelledEncounter = encounter;
		}
	}

	[FriendlyName("Encounter Cancel Event", "Called when any encounter is cancelled! The listening script is responsible for verifying if its attached encounter is the one being cancelled.")]
	public event uScriptEventHandler CancelEvent;

	private void OnEnable()
	{
		Singleton.Manager<ManEncounter>.inst.EncounterCancelledEvent.Subscribe(OnEncounterCancelled);
	}

	private void OnDisable()
	{
		Singleton.Manager<ManEncounter>.inst.EncounterCancelledEvent.Unsubscribe(OnEncounterCancelled);
	}

	private void OnEncounterCancelled(Encounter cancelledEncounter)
	{
		if (base.gameObject.activeInHierarchy && this.CancelEvent != null)
		{
			this.CancelEvent(this, new EncounterCancelledEventArgs(cancelledEncounter));
		}
	}
}
