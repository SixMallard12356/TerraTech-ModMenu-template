using System;

[FriendlyName("uScript_PlayerTechDestroyedEvent", "Listen for player tech destroyed events")]
[NodePath("TerraTech/Progression")]
public class uScript_PlayerTechDestroyedEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, TechDestroyedEventArgs args);

	public class TechDestroyedEventArgs : EventArgs
	{
		private Tank m_Tech;

		[FriendlyName("Tech", "The tech that was destroyed")]
		[SocketState(true, false)]
		public Tank Tech => m_Tech;

		public TechDestroyedEventArgs(Tank tech)
		{
			m_Tech = tech;
		}
	}

	[FriendlyName("Player Tech Destroyed", "Called every time a player tech is destroyed")]
	public event uScriptEventHandler TechDestroyedEvent;

	private void OnTechDestroyed(Tank tech, ManDamage.DamageInfo info)
	{
		if (base.gameObject.activeInHierarchy && this.TechDestroyedEvent != null && tech.WasPlayerControlledAtFatalDamageTime)
		{
			this.TechDestroyedEvent(this, new TechDestroyedEventArgs(tech));
		}
	}

	private void OnEnable()
	{
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Subscribe(OnTechDestroyed);
	}

	private void OnDisable()
	{
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Unsubscribe(OnTechDestroyed);
	}
}
