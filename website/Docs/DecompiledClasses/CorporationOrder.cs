using UnityEngine;

public class CorporationOrder : ScriptableObject
{
	[SerializeField]
	private EnumOrder m_Order = new EnumOrder(typeof(FactionSubTypes));

	public bool Lookup(FactionSubTypes corp, out int order)
	{
		return m_Order.LookupOrder((int)corp, out order);
	}
}
