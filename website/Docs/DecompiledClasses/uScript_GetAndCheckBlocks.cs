#define UNITY_EDITOR
using System;
using UnityEngine;

[NodePath("TerraTech/Actions/Blocks")]
public class uScript_GetAndCheckBlocks : uScriptLogic
{
	private Encounter m_Encounter;

	private bool m_AllDead = true;

	private bool m_AllAlive;

	private bool m_AllValid;

	[FriendlyName("All Alive", "Are all the blocks Ready and Alive")]
	public bool AllAlive
	{
		get
		{
			if (m_AllValid)
			{
				return m_AllAlive;
			}
			return false;
		}
	}

	[FriendlyName("Some Alive", "Are all the blocks Ready and Some Alive")]
	public bool SomeAlive
	{
		get
		{
			if (m_AllValid && !m_AllAlive)
			{
				return !m_AllDead;
			}
			return false;
		}
	}

	[FriendlyName("All Dead", "Are all the blocks Ready and Dead")]
	public bool AllDead
	{
		get
		{
			if (m_AllValid)
			{
				return m_AllDead;
			}
			return false;
		}
	}

	[FriendlyName("Waiting To Spawn", "Some of the blocks aren't Ready yet")]
	public bool WaitingToSpawn => !m_AllValid;

	public void In(SpawnBlockData[] blockData, GameObject ownerNode, ref TankBlock[] blocks)
	{
		if ((bool)ownerNode && !m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			if (blockData != null && blockData.Length != 0)
			{
				if (blocks == null)
				{
					blocks = new TankBlock[blockData.Length];
				}
				else if (blocks.Length != blockData.Length)
				{
					Array.Resize(ref blocks, blockData.Length);
				}
				m_AllDead = true;
				m_AllAlive = true;
				m_AllValid = true;
				for (int i = 0; i < blockData.Length; i++)
				{
					EncounterVisibleData visible = m_Encounter.GetVisible(blockData[i].m_UniqueName);
					blocks[i] = null;
					if (visible != null)
					{
						if (visible.m_VisibleId == -2)
						{
							m_AllAlive = false;
							continue;
						}
						TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visible.m_VisibleId);
						if (trackedVisible == null)
						{
							d.LogError("Cannot find tracked visible for encounter visible id " + visible.m_VisibleId);
						}
						if (trackedVisible == null || trackedVisible.visible == null)
						{
							m_AllValid = false;
						}
						else
						{
							blocks[i] = trackedVisible.visible.block;
						}
						m_AllDead = false;
					}
					else
					{
						m_AllValid = false;
					}
				}
			}
			else
			{
				d.LogError("uScript_GetAndCheckBlocks - BlockData is either null or length zero for " + ownerNode.name);
			}
		}
		else
		{
			string text = (ownerNode ? ("No Encounter Component on " + ownerNode.name) : "OwnerNode is null");
			d.LogError("uScript_GetAndCheckBlocks - " + text);
		}
	}
}
