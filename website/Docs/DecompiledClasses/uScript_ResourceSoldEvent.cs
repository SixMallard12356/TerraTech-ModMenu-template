using System;

[FriendlyName("uScript_ResourceSoldEvent", "Listen for resource sell events and pass them through if they match the type we're insterested in")]
[NodePath("TerraTech/Progression/Stats")]
public class uScript_ResourceSoldEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, ResourceSoldEventArgs args);

	public class ResourceSoldEventArgs : EventArgs
	{
		private ChunkTypes m_ResourceType;

		private int m_ResourceTypeTotal;

		private int m_SoldTotal;

		[FriendlyName("Resource Type", "Type of the resource that was sold.")]
		[SocketState(false, false)]
		public ChunkTypes ResourceType => m_ResourceType;

		[SocketState(false, false)]
		[FriendlyName("Resource Type total", "The total number of resources sold of this type.")]
		public int ResourceTypeTotal => m_ResourceTypeTotal;

		[SocketState(false, false)]
		[FriendlyName("Resource Total", "The total number of resources sold of all types combined.")]
		public int SoldTotal => m_SoldTotal;

		public ResourceSoldEventArgs(ChunkTypes resourceType, int resourceTypeTotal, int soldTotal)
		{
			m_ResourceType = resourceType;
			m_ResourceTypeTotal = resourceTypeTotal;
			m_SoldTotal = soldTotal;
		}
	}

	[FriendlyName("Resource Sold", "Called every time a resource is harvested")]
	public event uScriptEventHandler ResourceSoldEvent;

	private void OnResourceSold(int resourceTypeIdx, int resourceTypeTotal, int soldTotal)
	{
		if (base.gameObject.activeInHierarchy && this.ResourceSoldEvent != null)
		{
			this.ResourceSoldEvent(this, new ResourceSoldEventArgs((ChunkTypes)resourceTypeIdx, resourceTypeTotal, soldTotal));
		}
	}

	private void OnEnable()
	{
		Singleton.Manager<ManStats>.inst.ResourceSoldEvent += OnResourceSold;
	}

	private void OnDisable()
	{
		Singleton.Manager<ManStats>.inst.ResourceSoldEvent -= OnResourceSold;
	}
}
