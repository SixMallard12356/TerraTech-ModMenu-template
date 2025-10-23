#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KitBashPanelPool
{
	protected class PoolRandomiser
	{
		public List<KitBashPanel> SourceList;

		public int TotalWeight;

		public PoolRandomiser()
		{
			SourceList = new List<KitBashPanel>();
			TotalWeight = 0;
		}

		public void AddPanel(KitBashPanel panel)
		{
			SourceList.Add(panel);
			TotalWeight += panel.SpawnWeight;
		}

		public KitBashPanelTypes GetRandomPanel(DRNG randomizer)
		{
			int num = randomizer.Range(0, TotalWeight);
			for (int i = 0; i < SourceList.Count; i++)
			{
				num -= SourceList[i].SpawnWeight;
				if (num <= 0)
				{
					return SourceList[i].PanelType;
				}
			}
			d.LogError("How the hell did this happen? Bad random lookup??? [KitBashPanelPool]");
			return KitBashPanelTypes.Unassigned;
		}
	}

	[SerializeField]
	protected KitBashPanelTypes[] m_PanelTypes = new KitBashPanelTypes[0];

	protected PoolRandomiser[,] m_PanelDimentionLookup;

	public bool NotSet => m_PanelTypes.Length == 0;

	public void TryInitialize()
	{
		if (m_PanelDimentionLookup != null)
		{
			return;
		}
		HashSet<KitBashPanel> hashSet = new HashSet<KitBashPanel>();
		Vector2Int vector2Int = default(Vector2Int);
		for (int i = 0; i < m_PanelTypes.Length; i++)
		{
			KitBashPanel kitBashPanelPrefab = Singleton.Manager<ManSpawn>.inst.GetKitBashPanelPrefab(m_PanelTypes[i]);
			if (!hashSet.Add(kitBashPanelPrefab))
			{
				d.LogError("Detected kitbash panel pool with duplicate entries! Call code!");
			}
			if (vector2Int.x < kitBashPanelPrefab.Dimentions.x + 1)
			{
				vector2Int.x = kitBashPanelPrefab.Dimentions.x + 1;
			}
			if (vector2Int.y < kitBashPanelPrefab.Dimentions.y + 1)
			{
				vector2Int.y = kitBashPanelPrefab.Dimentions.y + 1;
			}
		}
		m_PanelDimentionLookup = new PoolRandomiser[vector2Int.x, vector2Int.y];
		foreach (KitBashPanel item in hashSet)
		{
			if (m_PanelDimentionLookup[item.Dimentions.x, item.Dimentions.y] == null)
			{
				m_PanelDimentionLookup[item.Dimentions.x, item.Dimentions.y] = new PoolRandomiser();
			}
			m_PanelDimentionLookup[item.Dimentions.x, item.Dimentions.y].AddPanel(item);
		}
	}

	public KitBashPanel SpawnPanelFromPool(Vector2Int desiredDimentions, Vector3 position, Quaternion rotation, DRNG randomizer, Transform parent = null)
	{
		TryInitialize();
		bool flag = false;
		PoolRandomiser poolRandomiser = m_PanelDimentionLookup[desiredDimentions.x, desiredDimentions.y];
		if (poolRandomiser == null)
		{
			poolRandomiser = m_PanelDimentionLookup[desiredDimentions.y, desiredDimentions.x];
			flag = true;
		}
		d.Assert(poolRandomiser != null, $"Tried to get a panel from pool but was missing panel of dimentions {desiredDimentions}! Make sure Kit Bash Panel pools are populated!");
		KitBashPanelTypes randomPanel = poolRandomiser.GetRandomPanel(randomizer);
		KitBashPanel kitBashPanel = Singleton.Manager<ManSpawn>.inst.SpawnKitBashPanel(randomPanel, position, rotation, parent);
		if (flag)
		{
			kitBashPanel.SetPerpendicularVariant();
		}
		return kitBashPanel;
	}
}
