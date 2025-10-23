using System;
using UnityEngine;

[Serializable]
public struct GhostBlockSpawnData
{
	public BlockTypes m_BlockType;

	public string m_UniqueName;

	public Vector3 m_WorldPos;

	public Vector3 m_BlockRotation;
}
