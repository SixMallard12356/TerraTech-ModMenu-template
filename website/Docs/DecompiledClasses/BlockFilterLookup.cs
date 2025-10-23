#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BlockFilterLookup
{
	public interface IFilterBuilder
	{
		bool CheckBlockAllowed(BlockTypes type, TankBlock prefab, bool logReason);
	}

	[SerializeField]
	private List<string> m_AllowedBlockNames = new List<string>();

	private HashSet<BlockTypes> m_Lookup;

	public bool CheckBlockAllowed(BlockTypes blockType)
	{
		if (m_Lookup == null)
		{
			RebuildLookup();
		}
		return m_Lookup.Contains(blockType);
	}

	public void Rebuild(IFilterBuilder[] filters, Dictionary<BlockTypes, TankBlock> blockLookup, BlockUnlockTable unlockTable)
	{
		DoRebuild(filters, blockLookup, unlockTable, logReason: true);
	}

	public bool CheckNeedsRebuildSlow(IFilterBuilder[] filters, Dictionary<BlockTypes, TankBlock> blockLookup, BlockUnlockTable unlockTable)
	{
		BlockFilterLookup blockFilterLookup = new BlockFilterLookup();
		blockFilterLookup.DoRebuild(filters, blockLookup, unlockTable, logReason: false);
		bool flag;
		if (m_AllowedBlockNames.Count == blockFilterLookup.m_AllowedBlockNames.Count)
		{
			flag = true;
			for (int i = 0; i < m_AllowedBlockNames.Count; i++)
			{
				if (m_AllowedBlockNames[i] != blockFilterLookup.m_AllowedBlockNames[i])
				{
					flag = false;
					break;
				}
			}
		}
		else
		{
			flag = false;
		}
		return !flag;
	}

	private void RebuildLookup()
	{
		m_Lookup = new HashSet<BlockTypes>(new BlockTypesComparer());
		foreach (string allowedBlockName in m_AllowedBlockNames)
		{
			try
			{
				BlockTypes item = (BlockTypes)Enum.Parse(typeof(BlockTypes), allowedBlockName, ignoreCase: true);
				m_Lookup.Add(item);
			}
			catch (Exception)
			{
				d.LogErrorFormat("BlockTableLookup unable to parse block type name {0}.  Try rebuilding BlockFilterTables.", allowedBlockName);
			}
		}
	}

	private void DoRebuild(IFilterBuilder[] filters, Dictionary<BlockTypes, TankBlock> blockLookup, BlockUnlockTable unlockTable, bool logReason)
	{
		d.LogError("Cannot rebuild BlockFilterLookup outside of editor");
	}
}
