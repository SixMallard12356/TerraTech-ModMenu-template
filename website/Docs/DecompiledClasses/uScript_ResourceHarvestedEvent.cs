using System;

[FriendlyName("uScript_ResourceHarvestedEvent", "Listen for resource harvested events and pass them through if they match the type we're insterested in")]
[NodePath("TerraTech/Progression/Stats")]
public class uScript_ResourceHarvestedEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, ResourceHarvestedEventArgs args);

	public class ResourceHarvestedEventArgs : EventArgs
	{
		private ChunkTypes m_ResourceType;

		private int m_ResourceTypeTotal;

		private int m_HarvestedTotal;

		[FriendlyName("Resource Type", "Type of the resource that was harvested.")]
		[SocketState(false, false)]
		public ChunkTypes ResourceType => m_ResourceType;

		[FriendlyName("Resource Type total", "The total number of resources harvested of this type.")]
		[SocketState(false, false)]
		public int ResourceTypeTotal => m_ResourceTypeTotal;

		[FriendlyName("Resource Total", "The total number of resources harvested of all types combined.")]
		[SocketState(false, false)]
		public int HarvestedTotal => m_HarvestedTotal;

		public ResourceHarvestedEventArgs(ChunkTypes resourceType, int resourceTypeTotal, int harvestedTotal)
		{
			m_ResourceType = resourceType;
			m_ResourceTypeTotal = resourceTypeTotal;
			m_HarvestedTotal = harvestedTotal;
		}
	}

	[FriendlyName("Resource Harvested", "Called every time a resource is harvested")]
	public event uScriptEventHandler ResourceHarvestedEvent;

	private void OnResourceHarvested(int resourceTypeIdx, int resourceTypeTotal, int harvestedTotal)
	{
		if (base.gameObject.activeInHierarchy && this.ResourceHarvestedEvent != null)
		{
			this.ResourceHarvestedEvent(this, new ResourceHarvestedEventArgs((ChunkTypes)resourceTypeIdx, resourceTypeTotal, harvestedTotal));
		}
	}

	private void OnEnable()
	{
		Singleton.Manager<ManStats>.inst.ResourceHarvestedEvent += OnResourceHarvested;
	}

	private void OnDisable()
	{
		Singleton.Manager<ManStats>.inst.ResourceHarvestedEvent -= OnResourceHarvested;
	}
}
