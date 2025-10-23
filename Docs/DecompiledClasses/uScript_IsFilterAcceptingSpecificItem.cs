#define UNITY_EDITOR
public class uScript_IsFilterAcceptingSpecificItem : uScriptLogic
{
	private bool m_AcceptingItem;

	private ModuleItemFilter m_ItemFilter;

	public bool True => m_AcceptingItem;

	public bool False => !m_AcceptingItem;

	public void In(TankBlock filterBlock, ItemTypeInfo itemType)
	{
		m_AcceptingItem = false;
		if (filterBlock != null)
		{
			if (!m_ItemFilter)
			{
				m_ItemFilter = filterBlock.GetComponent<ModuleItemFilter>();
			}
			if (m_ItemFilter != null)
			{
				if (itemType != null)
				{
					if (m_ItemFilter.AcceptsSpecificItemType)
					{
						m_AcceptingItem = m_ItemFilter.AcceptsType(itemType);
					}
				}
				else
				{
					d.LogError("uScript_IsFilterAcceptingItem: itemType is null");
				}
			}
			else
			{
				d.LogError("uScript_IsFilterAcceptingItem: block doesn't have a filter module");
			}
		}
		else
		{
			d.LogError("uScript_IsFilterAcceptingItem: filter block is null");
		}
	}

	public void OnEnable()
	{
		m_AcceptingItem = false;
	}
}
