using System;

[NodePath("TerraTech/Progression/Stats")]
[FriendlyName("uScript_MoneyFromResourceSaleEvent", "Listen for resource sell events and pass them through if they match the type we're insterested in")]
public class uScript_MoneyFromResourceSaleEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, MoneyFromResourceSaleEventArgs args);

	public class MoneyFromResourceSaleEventArgs : EventArgs
	{
		private ChunkTypes m_ResourceType;

		private int m_ResourceTypeTotal;

		private int m_MoneyTotal;

		[FriendlyName("Resource Type", "Type of the resource that was sold.")]
		[SocketState(false, false)]
		public ChunkTypes ResourceType => m_ResourceType;

		[SocketState(false, false)]
		[FriendlyName("Resource Type total", "The total number of resources sold of this type.")]
		public int ResourceTypeTotal => m_ResourceTypeTotal;

		[FriendlyName("Resource Total", "The total number of resources sold of all types combined.")]
		[SocketState(false, false)]
		public int MoneyTotal => m_MoneyTotal;

		public MoneyFromResourceSaleEventArgs(ChunkTypes resourceType, int resourceTypeTotal, int moneyTotal)
		{
			m_ResourceType = resourceType;
			m_ResourceTypeTotal = resourceTypeTotal;
			m_MoneyTotal = moneyTotal;
		}
	}

	[FriendlyName("Resource Sold", "Called every time a resource is sold")]
	public event uScriptEventHandler MoneyFromResourceSaleEvent;

	private void OnMoneyFromResourceSale(int resourceTypeIdx, int resourceTypeTotal, int moneyTotal)
	{
		if (base.gameObject.activeInHierarchy && this.MoneyFromResourceSaleEvent != null)
		{
			this.MoneyFromResourceSaleEvent(this, new MoneyFromResourceSaleEventArgs((ChunkTypes)resourceTypeIdx, resourceTypeTotal, moneyTotal));
		}
	}

	private void OnEnable()
	{
		Singleton.Manager<ManStats>.inst.MoneyFromResourceSaleEvent += OnMoneyFromResourceSale;
	}

	private void OnDisable()
	{
		Singleton.Manager<ManStats>.inst.MoneyFromResourceSaleEvent -= OnMoneyFromResourceSale;
	}
}
