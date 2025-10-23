using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Asset/Achievements/CraftBlocksAchievement")]
public class CraftBlocksAchievement : AchievementObject
{
	[Serializable]
	public struct BlockTypeToCraft
	{
		public BlockTypes blockType;

		public int requiredCount;
	}

	[SerializeField]
	private List<BlockTypeToCraft> m_BlockTypesToCraft;

	public override void Initialise()
	{
		base.Initialise();
		Singleton.Manager<ManStats>.inst.BlockCraftedEvent += OnBlockCrafted;
	}

	private void OnBlockCrafted(TankBlock crafter, TankBlock block, int blockTypeIdx, int blockTypeTotal, int craftedTotal)
	{
		if (!IsActive() || m_BlockTypesToCraft.FindIndex((BlockTypeToCraft x) => x.blockType == (BlockTypes)blockTypeIdx) < 0)
		{
			return;
		}
		bool flag = true;
		for (int num = 0; num < m_BlockTypesToCraft.Count; num++)
		{
			BlockTypeToCraft blockTypeToCraft = m_BlockTypesToCraft[num];
			if (Singleton.Manager<ManStats>.inst.GetNumBlocksCrafted(blockTypeToCraft.blockType) < blockTypeToCraft.requiredCount)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			CompleteAchievement();
			Singleton.Manager<ManStats>.inst.BlockCraftedEvent -= OnBlockCrafted;
		}
	}
}
