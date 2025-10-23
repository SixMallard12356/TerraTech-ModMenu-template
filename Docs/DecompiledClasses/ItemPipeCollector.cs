public class ItemPipeCollector : ItemSearchCollector
{
	private LinearAllocPool<ConcreteOfferedItem> m_ConcretePool;

	private LinearAllocPool<AnonymousOfferedItem> m_AnonPool;

	private ItemPipe m_Pipe;

	private ModuleItemHolder m_SrcHolder;

	private ModuleItemHolder.Stack m_NextHop;

	private int m_AnonItemInd;

	public TechHolders TankHolders { get; set; }

	public ItemPipeCollector(LinearAllocPool<ConcreteOfferedItem> concretePool, LinearAllocPool<AnonymousOfferedItem> anonPool)
	{
		m_ConcretePool = concretePool;
		m_AnonPool = anonPool;
	}

	public void DeliverItemsToPipe(ItemSearchHandler itemSource, bool processedItems, ItemPipe pipe, ModuleItemHolder holder, ModuleItemHolder.Stack nextHop)
	{
		m_Pipe = pipe;
		m_SrcHolder = holder;
		m_NextHop = nextHop;
		m_AnonItemInd = 0;
		itemSource.HandleCollectItems(this, processedItems);
	}

	public void OfferItem(Visible offeredItem)
	{
		if (offeredItem.IsNotNull() && !TankHolders.IsItemRequested(offeredItem))
		{
			ConcreteOfferedItem concreteOfferedItem = m_ConcretePool.Alloc();
			concreteOfferedItem.Setup(offeredItem, m_SrcHolder, m_NextHop);
			m_Pipe.OfferItem(concreteOfferedItem);
		}
	}

	public void OfferAnonItem(ItemTypeInfo offeredItem)
	{
		if (!m_SrcHolder.CheckAnonItemTaken(m_AnonItemInd))
		{
			AnonymousOfferedItem anonymousOfferedItem = m_AnonPool.Alloc();
			anonymousOfferedItem.Setup(offeredItem, m_SrcHolder, m_AnonItemInd);
			m_Pipe.OfferItem(anonymousOfferedItem);
		}
		m_AnonItemInd++;
	}
}
