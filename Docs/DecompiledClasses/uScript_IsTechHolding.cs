public class uScript_IsTechHolding : uScriptLogic
{
	private bool m_Held;

	public bool True => m_Held;

	public bool False => !m_Held;

	public void In(Tank tech, ItemTypeInfo heldItem, int Quantity)
	{
		m_Held = false;
		int num = 0;
		TechHolders.HolderIterator enumerator = tech.Holders.GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleItemHolder.Stack.ItemIterator enumerator2 = enumerator.Current.Contents.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				if (enumerator2.Current.m_ItemType == heldItem)
				{
					num++;
				}
				if (num >= Quantity)
				{
					m_Held = true;
					return;
				}
			}
		}
	}
}
