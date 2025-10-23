using System;

[NodePath("TerraTech/Progression/Stats")]
[FriendlyName("uScript_InvaderDestroyedEvent", "Listen for invader destroyed events")]
public class uScript_InvaderDestroyedEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, InvaderDestroyedEventArgs args);

	public class InvaderDestroyedEventArgs : EventArgs
	{
		private int m_NumInvadersDestroyed;

		[FriendlyName("Invaders Destroyed total", "The total number of Invaders destroyed")]
		[SocketState(true, false)]
		public int NumInvadersDestroyed => m_NumInvadersDestroyed;

		public InvaderDestroyedEventArgs(int totalDestroyed)
		{
			m_NumInvadersDestroyed = totalDestroyed;
		}
	}

	[FriendlyName("Invader Destroyed", "Called every time an Invader is destroyed")]
	public event uScriptEventHandler InvaderDestroyedEvent;

	private void OnInvaderDestroyed(int newTotal)
	{
		if (base.gameObject.activeInHierarchy && this.InvaderDestroyedEvent != null)
		{
			this.InvaderDestroyedEvent(this, new InvaderDestroyedEventArgs(newTotal));
		}
	}

	private void OnEnable()
	{
		Singleton.Manager<ManStats>.inst.InvaderDestroyedEvent += OnInvaderDestroyed;
	}

	private void OnDisable()
	{
		Singleton.Manager<ManStats>.inst.InvaderDestroyedEvent -= OnInvaderDestroyed;
	}
}
