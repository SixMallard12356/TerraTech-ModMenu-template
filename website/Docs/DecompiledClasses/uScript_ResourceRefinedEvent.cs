using System;

[NodePath("TerraTech/Progression/Stats")]
[FriendlyName("uScript_ResourceRefinedEvent", "Listen for resource refine events and pass them through if they match the type we're insterested in")]
public class uScript_ResourceRefinedEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, ResourceRefinedEventArgs args);

	public class ResourceRefinedEventArgs : EventArgs
	{
		private ChunkTypes m_ResourceType;

		private int m_ResourceTypeTotal;

		private int m_RefinedTotal;

		[FriendlyName("Resource Type", "Type of the resource that was refined.")]
		[SocketState(false, false)]
		public ChunkTypes ResourceType => m_ResourceType;

		[SocketState(false, false)]
		[FriendlyName("Resource Type total", "The total number of resources refined of this type.")]
		public int ResourceTypeTotal => m_ResourceTypeTotal;

		[FriendlyName("Resource Total", "The total number of resources refined of all types combined.")]
		[SocketState(false, false)]
		public int RefinedTotal => m_RefinedTotal;

		public ResourceRefinedEventArgs(ChunkTypes resourceType, int resourceTypeTotal, int refinedTotal)
		{
			m_ResourceType = resourceType;
			m_ResourceTypeTotal = resourceTypeTotal;
			m_RefinedTotal = refinedTotal;
		}
	}

	[FriendlyName("Resource Refined", "Called every time a resource is refined")]
	public event uScriptEventHandler ResourceRefinedEvent;

	private void OnResourceRefined(int resourceTypeIdx, int resourceTypeTotal, int refinedTotal)
	{
		if (base.gameObject.activeInHierarchy && this.ResourceRefinedEvent != null)
		{
			this.ResourceRefinedEvent(this, new ResourceRefinedEventArgs((ChunkTypes)resourceTypeIdx, resourceTypeTotal, refinedTotal));
		}
	}

	private void OnEnable()
	{
		Singleton.Manager<ManStats>.inst.ResourceRefinedEvent += OnResourceRefined;
	}

	private void OnDisable()
	{
		Singleton.Manager<ManStats>.inst.ResourceRefinedEvent -= OnResourceRefined;
	}
}
