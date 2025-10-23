using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[NodePath("TerraTech/Actions/Tutorial")]
public class uScript_SpawnGhostBlocks : uScriptLogic
{
	private Encounter m_Encounter;

	protected Tank m_Active_TargetTech;

	protected HashSet<TankBlock> m_Active_GhostBlocks = new HashSet<TankBlock>();

	protected TankBlock[] m_Active_GhostBlockArray;

	public bool OnAlreadySpawned => !OnSpawned;

	public bool OnSpawned { get; private set; }

	protected bool IsSpawned => m_Active_GhostBlocks.Count > 0;

	public TankBlock[] TrySpawnOnTech(GhostBlockSpawnData[] ghostBlockData, [FriendlyName("Owner Node", "Owner Node of Encounter")] GameObject ownerNode, Tank targetTech)
	{
		if (IsSpawned)
		{
			OnSpawned = false;
			return m_Active_GhostBlockArray;
		}
		m_Active_TargetTech = targetTech;
		m_Active_GhostBlocks.Clear();
		if (m_Encounter == null && ownerNode != null)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		if (m_Encounter != null && targetTech != null)
		{
			foreach (TankBlock item in SpawnGhostBlocks(ghostBlockData, targetTech))
			{
				m_Active_GhostBlocks.Add(item);
			}
		}
		m_Active_GhostBlockArray = m_Active_GhostBlocks.ToArray();
		OnSpawned = true;
		return m_Active_GhostBlockArray;
	}

	private void ResetInternal()
	{
		foreach (TankBlock active_GhostBlock in m_Active_GhostBlocks)
		{
			active_GhostBlock.visible.RecycledEvent.Unsubscribe(OnGhostBlockRecycled);
			Singleton.Manager<ManTechBuildingTutorial>.inst.TryRemoveGhostBlock(active_GhostBlock);
		}
		m_Active_GhostBlocks.Clear();
		m_Active_GhostBlockArray = new TankBlock[0];
		m_Active_TargetTech = null;
	}

	private IEnumerable<TankBlock> SpawnGhostBlocks(GhostBlockSpawnData[] ghostBlockData, Tank targetTech)
	{
		TankBlock[] array = new TankBlock[ghostBlockData.Length];
		if ((bool)m_Encounter)
		{
			for (int i = 0; i < ghostBlockData.Length; i++)
			{
				GhostBlockSpawnData ghostBlockSpawnData = ghostBlockData[i];
				array[i] = Singleton.Manager<ManTechBuildingTutorial>.inst.AddGhostBlock(ghostBlockSpawnData.m_BlockType, ghostBlockSpawnData.m_WorldPos, ghostBlockSpawnData.m_BlockRotation, targetTech);
				array[i].visible.RecycledEvent.Subscribe(OnGhostBlockRecycled);
			}
		}
		return array;
	}

	private void OnGhostBlockRecycled(Visible blockVis)
	{
		TankBlock block = blockVis.block;
		blockVis.RecycledEvent.Unsubscribe(OnGhostBlockRecycled);
		m_Active_GhostBlocks.Remove(block);
		m_Active_GhostBlockArray = m_Active_GhostBlocks.ToArray();
	}

	private void OnDisable()
	{
		ResetInternal();
	}
}
