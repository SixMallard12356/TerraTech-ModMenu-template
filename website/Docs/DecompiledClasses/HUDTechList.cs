using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HUDTechList : Singleton.Manager<HUDTechList>
{
	private float m_RefreshInterval = 2f;

	private float nextUpdateTime;

	public void UpdateTechs()
	{
		StringBuilder stringBuilder = new StringBuilder();
		HashSet<int> hashSet = new HashSet<int>();
		foreach (Encounter activeEncounter in Singleton.Manager<ManEncounter>.inst.ActiveEncounters)
		{
			int limiterCostEstimateForEncounter = Singleton.Manager<ManEncounter>.inst.GetLimiterCostEstimateForEncounter(activeEncounter);
			string text = (activeEncounter.HasSpecificBlockLimit ? $"{limiterCostEstimateForEncounter}" : $"{limiterCostEstimateForEncounter}(estimate)");
			stringBuilder.AppendFormat("Encounter:" + activeEncounter.name + " Estimated block limit:" + text + "\n");
			int num = 0;
			int num2 = 0;
			foreach (EncounterVisibleData spawnedVisible in activeEncounter.GetSpawnedVisibles())
			{
				if (spawnedVisible.m_VisibleId != -2 && spawnedVisible.ObjectType == ObjectTypes.Vehicle)
				{
					hashSet.Add(spawnedVisible.m_VisibleId);
					int trackedTechCost = Singleton.Manager<ManBlockLimiter>.inst.GetTrackedTechCost(spawnedVisible.m_VisibleId, includeHeldItems: true);
					Tank tank = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(spawnedVisible.m_VisibleId)?.visible?.tank;
					stringBuilder.AppendFormat("  {0} = {1}\n", (tank == null) ? "[unloaded tech]" : tank.name, trackedTechCost);
					num += trackedTechCost;
					num2++;
				}
			}
			if (num2 == 0)
			{
				stringBuilder.AppendFormat("  --no techs\n");
			}
			else
			{
				stringBuilder.AppendFormat($"  --{num2} tech(s), current total={num}\n");
			}
		}
		stringBuilder.AppendFormat("\nOther loaded non-player techs:\n");
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		foreach (Tank item in Singleton.Manager<ManTechs>.inst.IterateTechs())
		{
			if (!ManSpawn.IsPlayerTeam(item.Team))
			{
				int num7 = Singleton.Manager<ManBlockLimiter>.inst.PerTechCost;
				BlockManager.BlockIterator<TankBlock>.Enumerator enumerator3 = item.blockman.IterateBlocks().GetEnumerator();
				while (enumerator3.MoveNext())
				{
					TankBlock current4 = enumerator3.Current;
					num7 += Singleton.Manager<ManBlockLimiter>.inst.GetBlockCost(current4);
				}
				num5 += num7;
				num6++;
				if (!hashSet.Contains(item.visible.ID))
				{
					num3 += num7;
					num4++;
					stringBuilder.AppendFormat("  {0}{2} = {1}\n", item.name, num7, item.IsPopulation ? "(Population)" : "");
				}
			}
		}
		stringBuilder.AppendFormat($"  --{num4} tech(s), total={num3}\n");
		Text componentInChildren = GetComponentInChildren<Text>();
		if ((bool)componentInChildren)
		{
			componentInChildren.text = $"<size=30>Tech list</size> (total loaded non-player techs = {num5} in {num6} techs)\n" + stringBuilder.ToString().TrimEnd();
		}
		nextUpdateTime = Time.realtimeSinceStartup + m_RefreshInterval;
	}

	private int GetCost(TrackedVisible trackedVisible)
	{
		if (trackedVisible.ObjectType == ObjectTypes.Vehicle)
		{
			if ((bool)trackedVisible.visible)
			{
				int num = Singleton.Manager<ManBlockLimiter>.inst.PerTechCost;
				BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = trackedVisible.visible.tank.blockman.IterateBlocks().GetEnumerator();
				while (enumerator.MoveNext())
				{
					TankBlock current = enumerator.Current;
					num += Singleton.Manager<ManBlockLimiter>.inst.GetBlockCost(current);
				}
				return num;
			}
			return Singleton.Manager<ManBlockLimiter>.inst.GetTrackedTechCost(trackedVisible.ID, includeHeldItems: false);
		}
		return 0;
	}

	private void Start()
	{
		base.gameObject.SetActive(value: false);
	}

	private void Update()
	{
		if (Time.realtimeSinceStartup > nextUpdateTime)
		{
			UpdateTechs();
		}
	}
}
