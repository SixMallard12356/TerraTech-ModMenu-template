using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockRotationTable : ScriptableObject
{
	[Serializable]
	public struct GroupIndexLookup
	{
		public int blockType;

		public string groupName;
	}

	[Serializable]
	public class RotationGroup
	{
		public string name;

		public OrthoRotation.r[] rotations;
	}

	public List<GroupIndexLookup> m_BlockRotationGroupIndex = new List<GroupIndexLookup>();

	public RotationGroup[] m_RotationGroups;

	private Dictionary<int, RotationGroup> m_BlockRotationGroupLookup;

	public OrthoRotation.r[] GetBlockRotationOrder(int blockType)
	{
		if (m_BlockRotationGroupLookup.TryGetValue(blockType, out var value))
		{
			return value.rotations;
		}
		return OrthoRotation.AllRotations;
	}

	public void InitRuntime()
	{
		if (m_BlockRotationGroupLookup == null)
		{
			m_BlockRotationGroupLookup = new Dictionary<int, RotationGroup>();
		}
		else
		{
			m_BlockRotationGroupLookup.Clear();
		}
		foreach (GroupIndexLookup pair in m_BlockRotationGroupIndex)
		{
			m_BlockRotationGroupLookup[pair.blockType] = Array.Find(m_RotationGroups, (RotationGroup g) => g.name == pair.groupName);
		}
	}
}
