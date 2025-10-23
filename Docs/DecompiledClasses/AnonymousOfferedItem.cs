public class AnonymousOfferedItem : OfferedItem
{
	private ItemTypeInfo m_Type;

	private ModuleItemHolder m_Holder;

	private int m_HolderIndex;

	public void Setup(ItemTypeInfo typeInfo, ModuleItemHolder holder, int holderIndex)
	{
		m_Type = typeInfo;
		m_Holder = holder;
		m_HolderIndex = holderIndex;
	}

	public ItemTypeInfo GetTypeInfo()
	{
		return m_Type;
	}

	public void SetRequested()
	{
		m_Holder.SetAnonItemTaken(m_HolderIndex);
	}

	public bool IsSameObjectAs(OfferedItem other)
	{
		if (other is AnonymousOfferedItem anonymousOfferedItem && m_Holder == anonymousOfferedItem.m_Holder)
		{
			return m_HolderIndex == anonymousOfferedItem.m_HolderIndex;
		}
		return false;
	}
}
