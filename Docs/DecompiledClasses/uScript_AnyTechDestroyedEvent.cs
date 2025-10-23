using System;

[FriendlyName("uScript_AnyTechDestroyedEvent", "Listen for any tech destroyed events")]
[NodePath("TerraTech/Progression")]
public class uScript_AnyTechDestroyedEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, TechDestroyedEventArgs args);

	public class TechDestroyedEventArgs : EventArgs
	{
		private Tank m_Tech;

		[SocketState(true, false)]
		[FriendlyName("Tech", "The tech that was destroyed")]
		public Tank Tech => m_Tech;

		public TechDestroyedEventArgs(Tank tech)
		{
			m_Tech = tech;
		}
	}

	[FriendlyName("Any Tech Destroyed", "Called every time any tech is destroyed")]
	public event uScriptEventHandler TechDestroyedEvent;

	private void OnTechDestroyed(Tank tech, ManDamage.DamageInfo info)
	{
		if (base.gameObject.activeInHierarchy && this.TechDestroyedEvent != null)
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
