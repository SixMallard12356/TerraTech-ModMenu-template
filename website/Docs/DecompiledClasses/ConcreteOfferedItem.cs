public class ConcreteOfferedItem : OfferedItem
{
	private Visible m_Visible;

	private ModuleItemHolder m_Holder;

	private ModuleItemHolder.Stack m_NextHop;

	public void Setup(Visible visible, ModuleItemHolder holder, ModuleItemHolder.Stack nextHop)
	{
		m_Visible = visible;
		m_Holder = holder;
		m_NextHop = nextHop;
	}

	public ItemTypeInfo GetTypeInfo()
	{
		return m_Visible.m_ItemType;
	}

	public void SetRequested()
	{
		m_Holder.block.tank.Holders.SetItemRequested(m_Visible, m_NextHop);
	}

	public bool IsSameObjectAs(OfferedItem other)
	{
		if (other is ConcreteOfferedItem concreteOfferedItem)
		{
			return m_Visible == concreteOfferedItem.m_Visible;
		}
		return false;
	}
}
