using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PoolInitTable : ScriptableObject
{
	[Serializable]
	public class PoolSpec
	{
		[FormerlySerializedAs("prefab")]
		public Component assetPrefab;

		public string name;

		public int size;

		public ComponentPool.IPoolMetrics poolMetrics { get; set; }

		public PoolSpec(Component p, string n, int s)
		{
			assetPrefab = p;
			name = n;
			size = s;
		}
	}

	public List<PoolSpec> poolSpecs = new List<PoolSpec>();

	public List<PoolSpec> netPoolSpecs = new List<PoolSpec>();
}
