#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BlockFilterTable", menuName = "Asset/Table/BlockFilterTable")]
public class BlockFilterTable : ScriptableObject
{
	[SerializeField]
	private BlockTable m_BlockTable;

	[SerializeField]
	private BlockUnlockTable m_UnlockTable;

	[SerializeField]
	private BlockFilterComponentWhiteList m_ComponentsWhiteList;

	[SerializeField]
	private BlockFilterCorpBlackList m_CorpsBlackList;

	[SerializeField]
	private BlockFilterLookup m_Lookup;

	public bool CheckBlockAllowed(BlockTypes blockType)
	{
		bool flag = false;
		if (Singleton.Manager<ManMods>.inst.IsModdedBlock(blockType))
		{
			TankBlock blockPrefab = Singleton.Manager<ManSpawn>.inst.GetBlockPrefab(blockType);
			if (blockPrefab == null)
			{
				d.LogWarning($"Modded block {blockType} was not correctly injected into ManSpawn");
				return false;
			}
			return m_CorpsBlackList.CheckBlockAllowed(blockType, blockPrefab, logReason: false) && m_ComponentsWhiteList.CheckBlockAllowed(blockType, blockPrefab, logReason: false);
		}
		return m_Lookup.CheckBlockAllowed(blockType);
	}

	public void Rebuild()
	{
		d.Assert(condition: false, "Unable to rebuild BlockFilterTable outside of editor");
	}

	public bool CheckNeedsRebuildSlow()
	{
		return m_Lookup.CheckNeedsRebuildSlow(new BlockFilterLookup.IFilterBuilder[2] { m_ComponentsWhiteList, m_CorpsBlackList }, BuildBlockDict(), m_UnlockTable);
	}

	public IEnumerable<BlockTypes> ListAvailableBlocksSlow()
	{
		return from b in m_UnlockTable.GetAllBlocksUnfiltered()
			where m_Lookup.CheckBlockAllowed(b)
			select b;
	}

	public IEnumerable<BlockTypes> ListRejectedBlocksSlow()
	{
		return from b in m_UnlockTable.GetAllBlocksUnfiltered()
			where !m_Lookup.CheckBlockAllowed(b)
			select b;
	}

	public List<Type> ListAllowedModuleTypes()
	{
		return m_ComponentsWhiteList.ListAllowedTypes();
	}

	private Dictionary<BlockTypes, TankBlock> BuildBlockDict()
	{
		Dictionary<BlockTypes, TankBlock> dictionary = new Dictionary<BlockTypes, TankBlock>(new BlockTypesComparer());
		foreach (GameObject block in m_BlockTable.m_Blocks)
		{
			TankBlock component = block.GetComponent<TankBlock>();
			Visible component2 = block.GetComponent<Visible>();
			BlockTypes itemType = (BlockTypes)component2.ItemType;
			if (component != null && component2 != null)
			{
				if (!dictionary.ContainsKey(itemType))
				{
					dictionary.Add(itemType, component);
				}
			}
			else
			{
				d.LogErrorFormat("Block table entry {0} has either no TankBlock or Visible component!", block.name);
			}
		}
		return dictionary;
	}
}
