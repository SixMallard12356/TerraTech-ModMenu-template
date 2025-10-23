#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
internal class BlockFilterCorpBlackList : BlockFilterLookup.IFilterBuilder
{
	[SerializeField]
	private List<FactionSubTypes> m_IllegalFactions;

	public bool CheckBlockAllowed(BlockTypes blockType, TankBlock prefab, bool logReason)
	{
		FactionSubTypes factionSubTypes = ManSpawn.DetermineFactionFromBlockTypeSlow(blockType);
		bool flag = m_IllegalFactions.Contains(factionSubTypes);
		if (flag && logReason)
		{
			d.LogFormat("Filter BlockFilterCorpBlackList has blocked type {0} because it contains illegal corp {1}", blockType, factionSubTypes);
		}
		return !flag;
	}
}
