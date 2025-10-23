using System;

public readonly struct WorldGenVersionData : IComparable<WorldGenVersionData>, IEquatable<WorldGenVersionData>
{
	public readonly int m_VersionID;

	public readonly BiomeMap.WorldGenVersioningType m_VersioningType;

	public static readonly WorldGenVersionData kUninitialised = new WorldGenVersionData(0, BiomeMap.WorldGenVersioningType.ChangleListVersionInt);

	public static readonly WorldGenVersionData kMax = new WorldGenVersionData(int.MaxValue, (BiomeMap.WorldGenVersioningType)2147483647);

	public static readonly WorldGenVersionData kLegacy_8808_FixedHash = new WorldGenVersionData(8808, BiomeMap.WorldGenVersioningType.ChangleListVersionInt);

	public static readonly WorldGenVersionData kLegacy_13975_BiomeSwapIdx = new WorldGenVersionData(13975, BiomeMap.WorldGenVersioningType.ChangleListVersionInt);

	public static readonly WorldGenVersionData kLegacy_14775_BiomeDB = new WorldGenVersionData(14775, BiomeMap.WorldGenVersioningType.ChangleListVersionInt);

	private static readonly WorldGenVersionData kInternalBuildBiome7Branch = new WorldGenVersionData(1652699439, BiomeMap.WorldGenVersioningType.ChangleListVersionInt);

	private static readonly WorldGenVersionData kInternalBuildDevBranch = new WorldGenVersionData(1653433200, BiomeMap.WorldGenVersioningType.ChangleListVersionInt);

	private static readonly WorldGenVersionData kInternalTimeOfFixingVersioningIssue = new WorldGenVersionData(1, BiomeMap.WorldGenVersioningType.BiomeMapIteration);

	public WorldGenVersionData(int versionID, BiomeMap.WorldGenVersioningType versioningType)
	{
		m_VersionID = versionID;
		m_VersioningType = versioningType;
	}

	public int CompareTo(WorldGenVersionData other)
	{
		if (m_VersionID == -1 || other.m_VersionID == -1)
		{
			return other.m_VersionID.CompareTo(m_VersionID);
		}
		if (m_VersioningType != other.m_VersioningType)
		{
			return ((int)m_VersioningType).CompareTo((int)other.m_VersioningType);
		}
		if (m_VersionID != other.m_VersionID)
		{
			return m_VersionID.CompareTo(other.m_VersionID);
		}
		return 0;
	}

	public bool Equals(WorldGenVersionData other)
	{
		return this == other;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is WorldGenVersionData worldGenVersionData))
		{
			return false;
		}
		return this == worldGenVersionData;
	}

	public override int GetHashCode()
	{
		return 23 * (int)(31 + m_VersioningType) * (31 + m_VersionID);
	}

	public override string ToString()
	{
		if (m_VersionID == -1)
		{
			return "Unversioned(-1)";
		}
		return $"{(int)m_VersioningType}_{m_VersionID}";
	}

	public static bool operator <(WorldGenVersionData a, WorldGenVersionData b)
	{
		return a.CompareTo(b) < 0;
	}

	public static bool operator >(WorldGenVersionData a, WorldGenVersionData b)
	{
		return a.CompareTo(b) > 0;
	}

	public static bool operator <=(WorldGenVersionData a, WorldGenVersionData b)
	{
		return a.CompareTo(b) <= 0;
	}

	public static bool operator >=(WorldGenVersionData a, WorldGenVersionData b)
	{
		return a.CompareTo(b) >= 0;
	}

	public static bool operator ==(WorldGenVersionData a, WorldGenVersionData b)
	{
		return a.CompareTo(b) == 0;
	}

	public static bool operator !=(WorldGenVersionData a, WorldGenVersionData b)
	{
		return !(a == b);
	}
}
