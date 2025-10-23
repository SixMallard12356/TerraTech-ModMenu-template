using UnityEngine;

[CreateAssetMenu(fileName = "BlockSortOrder", menuName = "Asset/BlockSortOrder")]
public class BlockSortOrder : ScriptableObject
{
	[SerializeField]
	private int[] m_BlockTypeToOrderLookup;

	public const int kDefaultUnsorted = -1;

	public bool TryGetBlockOrder(BlockTypes blockType, out int order)
	{
		if (blockType >= BlockTypes.GSOAIController_111 && (int)blockType < m_BlockTypeToOrderLookup.Length)
		{
			order = m_BlockTypeToOrderLookup[(int)blockType];
			return order != -1;
		}
		order = -1;
		return false;
	}

	public int CompareOrder(BlockTypes blockTypeA, BlockTypes blockTypeB)
	{
		int order;
		bool flag = TryGetBlockOrder(blockTypeA, out order);
		int order2;
		bool flag2 = TryGetBlockOrder(blockTypeB, out order2);
		if (flag && flag2)
		{
			return order.CompareTo(order2);
		}
		if (flag)
		{
			return -1;
		}
		if (flag2)
		{
			return 1;
		}
		return 0;
	}
}
