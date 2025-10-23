using System;
using UnityEngine;

public class ResourceTable : ScriptableObject
{
	[Serializable]
	public class Definition
	{
		public string name;

		public ChunkTypes m_ChunkType;

		public int saleValue = 100;

		public float mass = 1f;

		public float frictionStatic = 0.2f;

		public float frictionDynamic = 0.2f;

		public float restitution = 0.2f;

		public Transform basePrefab;
	}

	public Definition[] resources;
}
