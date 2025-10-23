using UnityEngine;

public class CategoryOrder : ScriptableObject
{
	[SerializeField]
	private EnumOrder m_Order = new EnumOrder(typeof(BlockCategories));

	public bool Lookup(BlockCategories cat, out int order)
	{
		return m_Order.LookupOrder((int)cat, out order);
	}
}
