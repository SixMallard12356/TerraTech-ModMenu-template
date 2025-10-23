using System;

[NodePath("TerraTech/Progression/Stats")]
[FriendlyName("uScript_SceneryObjectDestroyedEvent", "Listen for scenery object destroy events and pass them through if they match the type we're insterested in")]
public class uScript_SceneryObjectDestroyedEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, SceneryObjectDestroyedEventArgs args);

	public class SceneryObjectDestroyedEventArgs : EventArgs
	{
		private SceneryTypes m_SceneryObjectType;

		private int m_SceneryObjectTypeTotal;

		private int m_SoldTotal;

		[SocketState(false, false)]
		[FriendlyName("Scenery Type", "Type of the scenery object that was destroyed.")]
		public SceneryTypes SceneryObjectType => m_SceneryObjectType;

		[SocketState(false, false)]
		[FriendlyName("Scenery Type total", "The total number of scenery objects destroyed of this type.")]
		public int SceneryObjectTypeTotal => m_SceneryObjectTypeTotal;

		[SocketState(false, false)]
		[FriendlyName("Scenery Total", "The total number of scenery objects destroyed of all types combined.")]
		public int SoldTotal => m_SoldTotal;

		public SceneryObjectDestroyedEventArgs(SceneryTypes sceneryObjectType, int sceneryObjectTypeTotal, int destroyedTotal)
		{
			m_SceneryObjectType = sceneryObjectType;
			m_SceneryObjectTypeTotal = sceneryObjectTypeTotal;
			m_SoldTotal = destroyedTotal;
		}
	}

	[FriendlyName("SceneryObject Sold", "Called every time a scenery object is harvested")]
	public event uScriptEventHandler SceneryObjectDestroyedEvent;

	private void OnSceneryObjectDestroyed(int sceneryObjectTypeIdx, int sceneryObjectTypeTotal, int destroyedTotal)
	{
		if (base.gameObject.activeInHierarchy && this.SceneryObjectDestroyedEvent != null)
		{
			this.SceneryObjectDestroyedEvent(this, new SceneryObjectDestroyedEventArgs((SceneryTypes)sceneryObjectTypeIdx, sceneryObjectTypeTotal, destroyedTotal));
		}
	}

	private void OnEnable()
	{
		Singleton.Manager<ManStats>.inst.SceneryObjectDestroyedEvent += OnSceneryObjectDestroyed;
	}

	private void OnDisable()
	{
		Singleton.Manager<ManStats>.inst.SceneryObjectDestroyedEvent -= OnSceneryObjectDestroyed;
	}
}
