#define UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class BlockPlacementExtensions
{
	private static void TraceShapes(IEnumerable<IGrouping<int, BlockPlacementCollector.Placement>> groupedByShape, TankBlock block, int tableSize)
	{
		Vector3 vector = new Vector3(tableSize, tableSize, tableSize) / 2f;
		d.Log($"{groupedByShape.Count()} distinct shape hashes:");
		foreach (IGrouping<int, BlockPlacementCollector.Placement> item in groupedByShape)
		{
			d.Log($"shape hash {item.Key}");
			List<string> list = new List<string>();
			foreach (BlockPlacementCollector.Placement item2 in item)
			{
				List<IntVector3> list2 = new List<IntVector3>();
				IntVector3[] filledCells = block.filledCells;
				foreach (IntVector3 intVector in filledCells)
				{
					list2.Add(new IntVector3(item2.localPos + item2.orthoRot * intVector + vector));
				}
				List<IntVector3> list3 = list2.ToList();
				list3.Sort((IntVector3 e1, IntVector3 e2) => e1.GetHashCode().CompareTo(e2.GetHashCode()));
				list.Add(string.Join(" ", list3.Select((IntVector3 c) => c.ToString()).ToArray()));
			}
			foreach (IGrouping<string, string> item3 in from s in list
				group s by s)
			{
				d.Log($"{item3.Count()} x {item3.Key}");
			}
		}
	}
}
