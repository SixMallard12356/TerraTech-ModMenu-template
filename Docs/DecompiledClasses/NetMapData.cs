using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NetMapData
{
	[SerializeField]
	public List<IntVector2> m_FixedTilesLoaded;

	[SerializeField]
	public int m_TileGenerationOffsetX;

	[SerializeField]
	public int m_TileGenerationOffsetY;

	[SerializeField]
	public int m_OriginTileX;

	[SerializeField]
	public int m_OriginTileY;

	[SerializeField]
	public int m_SpawnPointsDistance = 160;

	[SerializeField]
	public int m_DangerDistance = 200;

	[SerializeField]
	public int m_KillDistance = 300;
}
