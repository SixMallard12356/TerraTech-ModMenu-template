using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public readonly struct WorldPosition : IEquatable<WorldPosition>
{
	[JsonProperty]
	[SerializeField]
	private readonly IntVector2 m_TileCoord;

	[SerializeField]
	[JsonProperty]
	private readonly V3Serial m_TileRelativePos;

	[JsonIgnore]
	public IntVector2 TileCoord => m_TileCoord;

	[JsonIgnore]
	public Vector3 TileRelativePos => m_TileRelativePos;

	[JsonIgnore]
	public Vector3 GameWorldPosition => Singleton.Manager<ManWorld>.inst.TileManager.CalcTileOrigin(in m_TileCoord) + m_TileRelativePos;

	[JsonIgnore]
	public Vector3 ScenePosition => Singleton.Manager<ManWorld>.inst.TileManager.CalcTileOriginScene(in m_TileCoord) + m_TileRelativePos;

	public WorldPosition(in IntVector2 tileCoord, in Vector3 posInTile)
	{
		m_TileCoord = tileCoord;
		m_TileRelativePos = posInTile;
	}

	public static WorldPosition OffsetPosition(in WorldPosition pos, in Vector3 delta)
	{
		Vector3 vector = pos.m_TileRelativePos + delta;
		IntVector2 tileCoord = pos.m_TileCoord;
		float tileSize = Singleton.Manager<ManWorld>.inst.TileSize;
		IntVector2 intVector = new IntVector2(Mathf.FloorToInt(vector.x / tileSize), Mathf.FloorToInt(vector.z / tileSize));
		Vector3 vector2 = new Vector3(-intVector.x, 0f, -intVector.y) * tileSize;
		return new WorldPosition(tileCoord + intVector, vector + vector2);
	}

	public static WorldPosition FromGameWorldPosition(in Vector3 gameWorldPos)
	{
		IntVector2 tileCoord = Singleton.Manager<ManWorld>.inst.TileManager.WorldToTileCoord(in gameWorldPos);
		Vector3 vector = Singleton.Manager<ManWorld>.inst.TileManager.CalcTileOrigin(in tileCoord);
		return new WorldPosition(in tileCoord, gameWorldPos - vector);
	}

	public static WorldPosition FromScenePosition(in Vector3 scenePos)
	{
		IntVector2 tileCoordWorld = Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(in scenePos);
		Vector3 vector = Singleton.Manager<ManWorld>.inst.TileManager.CalcTileOriginScene(in tileCoordWorld);
		return new WorldPosition(in tileCoordWorld, scenePos - vector);
	}

	public static bool operator ==(WorldPosition a, WorldPosition b)
	{
		if (a.m_TileCoord == b.m_TileCoord && a.m_TileRelativePos.x == b.m_TileRelativePos.x && a.m_TileRelativePos.y == b.m_TileRelativePos.y)
		{
			return a.m_TileRelativePos.z == b.m_TileRelativePos.z;
		}
		return false;
	}

	public static bool operator !=(WorldPosition a, WorldPosition b)
	{
		return !(a == b);
	}

	public override bool Equals(object obj)
	{
		if (!(obj is WorldPosition))
		{
			return false;
		}
		return this == (WorldPosition)obj;
	}

	public bool Equals(WorldPosition other)
	{
		return this == other;
	}

	public override int GetHashCode()
	{
		return m_TileCoord.GetHashCode() ^ m_TileRelativePos.GetHashCode();
	}

	public override string ToString()
	{
		return $"Tile {m_TileCoord} at {m_TileRelativePos}";
	}
}
