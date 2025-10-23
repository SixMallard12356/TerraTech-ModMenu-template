using System;

[NodePath("TerraTech/Progression/Stats")]
[FriendlyName("uScript_EnemyTechDestroyedEvent", "Listen for enemy tech destroyed events")]
public class uScript_EnemyTechDestroyedEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EnemyTechDestroyedEventArgs args);

	public class EnemyTechDestroyedEventArgs : EventArgs
	{
		private FactionSubTypes m_Faction;

		private int m_FactionTotal;

		private int m_NumEnemyTechsDestroyed;

		[FriendlyName("Tech Faction", "The faction the destroyed tech belongs to")]
		[SocketState(false, false)]
		public FactionSubTypes Faction => m_Faction;

		[FriendlyName("Faction total", "The total number of tech destroyed from this faction.")]
		[SocketState(false, false)]
		public int FactionTotal => m_FactionTotal;

		[FriendlyName("Enemy Techs Destroyed total", "The total number of enemy techs destroyed")]
		[SocketState(true, false)]
		public int NumEnemyTechsDestroyed => m_NumEnemyTechsDestroyed;

		public EnemyTechDestroyedEventArgs(FactionSubTypes faction, int factionTotal, int totalDestroyed)
		{
			m_Faction = faction;
			m_FactionTotal = factionTotal;
			m_NumEnemyTechsDestroyed = totalDestroyed;
		}
	}

	[FriendlyName("Enemy Tech Destroyed", "Called every time an enemy tech is destroyed")]
	public event uScriptEventHandler EnemyTechDestroyedEvent;

	private void OnEnemyTechDestroyed(int faction, int factionTotal, int newTotal)
	{
		if (base.gameObject.activeInHierarchy && this.EnemyTechDestroyedEvent != null)
		{
			this.EnemyTechDestroyedEvent(this, new EnemyTechDestroyedEventArgs((FactionSubTypes)faction, factionTotal, newTotal));
		}
	}

	private void OnEnable()
	{
		Singleton.Manager<ManStats>.inst.EnemyTechDestroyedEvent += OnEnemyTechDestroyed;
	}

	private void OnDisable()
	{
		Singleton.Manager<ManStats>.inst.EnemyTechDestroyedEvent -= OnEnemyTechDestroyed;
	}
}
