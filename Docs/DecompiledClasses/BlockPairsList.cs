using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockPairsList", menuName = "Asset/Table/BlockPairsList")]
public class BlockPairsList : ScriptableObject
{
	[Serializable]
	public struct BlockPairs
	{
		public BlockTypes m_Block;

		public BlockTypes m_PairedBlock;

		public bool IsDuplicatePair => m_Block == m_PairedBlock;
	}

	public BlockPairs[] m_BlockPairs;

	private List<BlockTypes> m_PairedBlocksToIgnore = new List<BlockTypes>();

	private static List<BlockTypes> _s_FillPairs_AdditionalBlocks = new List<BlockTypes>();

	public List<BlockTypes> PairedBlocksToIgnore => m_PairedBlocksToIgnore;

	public void InitIgnoreList()
	{
		m_PairedBlocksToIgnore.Clear();
		for (int i = 0; i < m_BlockPairs.Length; i++)
		{
			if (m_BlockPairs[i].m_Block != m_BlockPairs[i].m_PairedBlock)
			{
				m_PairedBlocksToIgnore.Add(m_BlockPairs[i].m_PairedBlock);
			}
		}
	}

	public IEnumerable<BlockTypes> FillPairs(IEnumerable<BlockTypes> originCollection, bool onlyEnsurePairedOnce = false)
	{
		foreach (BlockTypes originBlockType in originCollection)
		{
			BlockPairs blockPairs = m_BlockPairs.SingleOrDefault((BlockPairs r) => r.m_Block == originBlockType || r.m_PairedBlock == originBlockType);
			if (blockPairs.Equals(default(BlockPairs)))
			{
				continue;
			}
			BlockTypes blockTypes = ((originBlockType == blockPairs.m_Block) ? blockPairs.m_PairedBlock : blockPairs.m_Block);
			if (onlyEnsurePairedOnce)
			{
				bool flag = false;
				if ((!blockPairs.IsDuplicatePair) ? (originCollection.Contains(blockTypes) || _s_FillPairs_AdditionalBlocks.Contains(blockTypes)) : (originCollection.Count((BlockTypes r) => r == originBlockType) + _s_FillPairs_AdditionalBlocks.Count((BlockTypes r) => r == originBlockType) >= 2))
				{
					continue;
				}
			}
			_s_FillPairs_AdditionalBlocks.Add(blockTypes);
		}
		BlockTypes[] result = originCollection.Concat(_s_FillPairs_AdditionalBlocks).ToArray();
		_s_FillPairs_AdditionalBlocks.Clear();
		return result;
	}
}
