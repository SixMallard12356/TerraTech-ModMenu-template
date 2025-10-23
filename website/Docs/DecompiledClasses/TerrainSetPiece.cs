#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zlib;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainSetPiece", menuName = "Asset/TerrainSetPiece")]
public class TerrainSetPiece : ScriptableObject
{
	[Serializable]
	public struct TerrainObjectData
	{
		public IntVector2 m_CellPos;

		public TerrainObject m_TerrainObject;

		public Quaternion m_Rotation;

		public Vector3 m_Offset;

		public float m_Scale;

		public static bool Similar(TerrainObjectData a, TerrainObjectData b)
		{
			if (a.m_CellPos != b.m_CellPos)
			{
				return false;
			}
			if (a.m_TerrainObject != b.m_TerrainObject)
			{
				return false;
			}
			if ((a.m_Rotation.eulerAngles - b.m_Rotation.eulerAngles).magnitude > 0.1f)
			{
				return false;
			}
			if ((a.m_Offset - b.m_Offset).magnitude > 0.1f)
			{
				return false;
			}
			if (Mathf.Abs(a.m_Scale - b.m_Scale) > 0.01f)
			{
				return false;
			}
			return true;
		}

		public TerrainObjectData GetRotated(int rotation, IntVector2 cellCentre, IntVector2 newCellCentre)
		{
			TerrainObjectData result = this;
			Quaternion quaternion = Quaternion.AngleAxis(rotation, Vector3.up);
			result.m_Rotation = quaternion * m_Rotation;
			Vector3 vector = (m_CellPos - cellCentre).ToVector3XZ() + m_Offset / 6f;
			Vector3 vector2 = quaternion * vector;
			result.m_CellPos = new IntVector2(Mathf.RoundToInt(vector2.x), Mathf.RoundToInt(vector2.z));
			result.m_Offset = (vector2 - result.m_CellPos.ToVector3XZ()) * 6f;
			result.m_CellPos += newCellCentre;
			return result;
		}
	}

	public enum ScenerySuppressType
	{
		HeightmapAlpha,
		SplatmapColour,
		SplatmapAlpha
	}

	public enum VisualisationBlackMode
	{
		None,
		TerrainAlpha,
		ShowObjectSuppression
	}

	[Range(0f, 1f)]
	[Tooltip("The brightness value at which the default height is calculated: 0 = black, 1 = white")]
	[SerializeField]
	[Header("Terrain data")]
	private float m_DefaultHeight = 0.5f;

	[SerializeField]
	private float m_HeightScaling = 1f;

	[SerializeField]
	private bool m_AllowHeightOverflow;

	[SerializeField]
	private ScenerySuppressType m_ScenerySuppressType;

	[SerializeField]
	[Tooltip("Alpha level at which to prevent normal clutter/trees from spawning")]
	private float m_SuppressSceneryThreshold = 0.25f;

	[SerializeField]
	private TerrainLayer m_TerrainLayer1;

	[SerializeField]
	private TerrainLayer m_TerrainLayer2;

	[SerializeField]
	private bool m_OverwriteSceneryGenParams;

	[InspectorVisibilityControl("m_OverwriteSceneryGenParams", InspectorVisibilityControlAttribute.ComparisonType.Equals)]
	[SerializeField]
	private int m_SceneryGenSeed;

	[Tooltip("Number of cells to offset by - based on cellsize set in ManWorld")]
	[SerializeField]
	[InspectorVisibilityControl("m_OverwriteSceneryGenParams", InspectorVisibilityControlAttribute.ComparisonType.Equals)]
	private Vector2Int m_SceneryGenCellOffset;

	[Header("World")]
	[SerializeField]
	[Tooltip("Maximum number of instances of this set piece. Set to -1 for unlimited.")]
	private int m_MaxInstancesInWorld = -1;

	[SerializeField]
	[Tooltip("Friction level of replaced materials (blended with underlying friction by splatmap intensity. 0.9 is the normal value")]
	private float m_Friction = 0.9f;

	[Header("Objects")]
	[SerializeField]
	private Biome m_SuppressedSceneryBiome;

	[SerializeField]
	private List<TerrainObjectData> m_TerrainObjectsList;

	public const float kCellScale = 6f;

	public const float kTerrainHeight = 100f;

	private readonly HideFlags kVisualisationHideFlags = HideFlags.DontSave;

	[SerializeField]
	[HideInInspector]
	private int m_Depth;

	[SerializeField]
	[HideInInspector]
	private int m_Width;

	[HideInInspector]
	[SerializeField]
	private IntVector2 m_BoundMin;

	[SerializeField]
	[HideInInspector]
	private IntVector2 m_BoundMax;

	[SerializeField]
	[HideInInspector]
	private IntVector2 m_CellMidPoint;

	[SerializeField]
	[HideInInspector]
	private float m_MinHeight;

	[SerializeField]
	[HideInInspector]
	private float m_MaxHeight;

	[SerializeField]
	[HideInInspector]
	private byte[] m_CompressedData;

	[SerializeField]
	[HideInInspector]
	private int m_CompressedDataVersion;

	private byte[] m_Heights;

	private byte[] m_HeightBlend;

	private byte[] m_SplatMap1;

	private byte[] m_SplatMap2;

	private byte[] m_ScenerySuppressBits;

	private TerrainSetPiece m_CreatedFrom;

	private int m_CopyRotation;

	public TerrainLayer terrainLayer1 => m_TerrainLayer1;

	public TerrainLayer terrainLayer2 => m_TerrainLayer2;

	public int MaxInstancesInWorld => m_MaxInstancesInWorld;

	public IntVector2 CellMidPoint => m_CellMidPoint;

	public TerrainSetPiece OriginalSetPiece
	{
		get
		{
			if (!m_CreatedFrom)
			{
				return this;
			}
			return m_CreatedFrom;
		}
	}

	public int ModifiedRotation
	{
		get
		{
			if (!m_CreatedFrom)
			{
				return 0;
			}
			return m_CopyRotation;
		}
	}

	public Biome SuppressedSceneryBiome => m_SuppressedSceneryBiome;

	public bool OverwriteSceneryGenParams => m_OverwriteSceneryGenParams;

	public int SceneryGenSeed => m_SceneryGenSeed;

	public Vector2Int SceneryGenCellOffset => m_SceneryGenCellOffset;

	private int RoundUpToBytes(int size)
	{
		return (size + 7) & -8;
	}

	private void Compress()
	{
		byte b = (byte)(m_DefaultHeight * 255.99f);
		for (int i = 0; i < m_HeightBlend.Length; i++)
		{
			if (m_HeightBlend[i] < byte.MaxValue)
			{
				m_HeightBlend[i] &= 254;
			}
			m_SplatMap1[i] = (byte)(m_SplatMap1[i] / 5 * 5);
			m_SplatMap2[i] = (byte)(m_SplatMap2[i] / 5 * 5);
			if (m_HeightBlend[i] == 0)
			{
				m_Heights[i] = b;
			}
		}
		using MemoryStream memoryStream = new MemoryStream((m_Heights.Length + m_HeightBlend.Length + m_SplatMap1.Length + m_SplatMap2.Length) / 2);
		using (GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Compress))
		{
			gZipStream.Write(m_Heights, 0, m_Heights.Length);
			gZipStream.Write(m_HeightBlend, 0, m_HeightBlend.Length);
			gZipStream.Write(m_SplatMap1, 0, m_SplatMap1.Length);
			gZipStream.Write(m_SplatMap2, 0, m_SplatMap2.Length);
			gZipStream.Write(m_ScenerySuppressBits, 0, m_ScenerySuppressBits.Length);
		}
		m_CompressedData = memoryStream.ToArray();
		m_CompressedDataVersion = 1;
	}

	private void Decompress()
	{
		if (m_CompressedData == null || m_CompressedData.Length == 0)
		{
			return;
		}
		using MemoryStream stream = new MemoryStream(m_CompressedData);
		using GZipStream gZipStream = new GZipStream(stream, CompressionMode.Decompress);
		if (m_Heights == null || m_ScenerySuppressBits == null || m_Heights.Length != m_Depth * m_Width)
		{
			m_Heights = new byte[m_Depth * m_Width];
			m_HeightBlend = new byte[m_Depth * m_Width];
			m_SplatMap1 = new byte[m_Depth * m_Width];
			m_SplatMap2 = new byte[m_Depth * m_Width];
			m_ScenerySuppressBits = new byte[RoundUpToBytes(m_Depth * m_Width)];
		}
		gZipStream.Read(m_Heights, 0, m_Depth * m_Width);
		gZipStream.Read(m_HeightBlend, 0, m_Depth * m_Width);
		gZipStream.Read(m_SplatMap1, 0, m_Depth * m_Width);
		gZipStream.Read(m_SplatMap2, 0, m_Depth * m_Width);
		if (m_CompressedDataVersion >= 1)
		{
			gZipStream.Read(m_ScenerySuppressBits, 0, m_ScenerySuppressBits.Length);
		}
	}

	private void GeneratePointData(int i, int j, out float heightBlend, out float height, out float splat1, out float splat2)
	{
		if (i >= m_BoundMin.x && j >= m_BoundMin.y && i <= m_BoundMax.x && j <= m_BoundMax.y)
		{
			int num = j * m_Width + i;
			height = ((float)(int)m_Heights[num] * 0.003921569f - m_DefaultHeight) * m_HeightScaling;
			heightBlend = (float)(int)m_HeightBlend[num] * 0.003921569f;
			splat1 = (float)(int)m_SplatMap1[num] * 0.003921569f;
			splat2 = (float)(int)m_SplatMap2[num] * 0.003921569f;
		}
		else
		{
			heightBlend = 0f;
			height = 0f;
			splat1 = 0f;
			splat2 = 0f;
		}
	}

	private void SetLayersFromTerrainData(TerrainLayer[] layers, out int splatIndex1, out int splatIndex2)
	{
		splatIndex1 = Array.IndexOf(layers, m_TerrainLayer1);
		splatIndex2 = Array.IndexOf(layers, m_TerrainLayer2);
		if (m_TerrainLayer1 != null && splatIndex1 == -1)
		{
			d.LogWarning("No usage of TerrainLayer " + m_TerrainLayer1.name);
		}
		if (m_TerrainLayer2 != null && splatIndex2 == -1)
		{
			d.LogWarning("No usage of TerrainLayer " + m_TerrainLayer1.name);
		}
	}

	private void CalcBounds()
	{
		m_CellMidPoint = new IntVector2(m_Width, m_Depth) / 2;
		m_MinHeight = float.MaxValue;
		m_MaxHeight = float.MinValue;
		m_BoundMin = new IntVector2(int.MaxValue, int.MaxValue);
		m_BoundMax = new IntVector2(int.MinValue, int.MinValue);
		int i = 0;
		int num = 0;
		for (; i < m_Depth; i++)
		{
			int num2 = 0;
			while (num2 < m_Width)
			{
				if (m_HeightBlend[num] > 127)
				{
					float b = (float)(int)m_Heights[i * m_Width + num2] * 0.003921569f;
					m_MinHeight = Mathf.Min(m_MinHeight, b);
					m_MaxHeight = Mathf.Max(m_MaxHeight, b);
				}
				if (m_HeightBlend[num] > 0 || m_SplatMap1[num] > 0 || m_SplatMap2[num] > 0)
				{
					m_BoundMin.x = Mathf.Min(m_BoundMin.x, num2);
					m_BoundMax.x = Mathf.Max(m_BoundMax.x, num2);
					m_BoundMin.y = Mathf.Min(m_BoundMin.y, i);
					m_BoundMax.y = Mathf.Max(m_BoundMax.y, i);
				}
				num2++;
				num++;
			}
		}
		float num3 = (m_MinHeight - m_DefaultHeight) * m_HeightScaling;
		float num4 = (m_MaxHeight - m_DefaultHeight) * m_HeightScaling;
		if (num4 - num3 > 1f && !m_AllowHeightOverflow)
		{
			d.LogWarning($"Compressing height range to fit (was {num3} - {num4})");
			m_HeightScaling /= num4 - num3;
		}
		if (num3 - num4 > 1f && !m_AllowHeightOverflow)
		{
			d.LogWarning($"Compressing height range to fit (was {num3} - {num4})");
			m_HeightScaling /= num3 - num4;
		}
	}

	public void GetCellBoundsRotationInvariant(out IntVector2 min, out IntVector2 max)
	{
		int a = Mathf.Max(m_CellMidPoint.x - m_BoundMin.x, m_BoundMax.x - m_CellMidPoint.x);
		int b = Mathf.Max(m_CellMidPoint.y - m_BoundMin.y, m_BoundMax.y - m_CellMidPoint.y);
		int num = Mathf.Max(a, b);
		min = IntVector2.one * (-num - 1);
		max = IntVector2.one * (num + 1);
	}

	public float GetApproxCellRadius()
	{
		GetCellBoundsRotationInvariant(out var min, out var _);
		return Mathf.Sqrt(min.x * min.x + min.y * min.y);
	}

	public void GetCellBounds(out IntVector2 min, out IntVector2 max, int rotation)
	{
		LoadTerrainData();
		RotateBounds(rotation, out var _, out var _, out var outMidpoint, out var outBoundMin, out var outBoundMax);
		min = outBoundMin - IntVector2.one - outMidpoint;
		max = outBoundMax + IntVector2.one - outMidpoint;
	}

	public float ChooseHeightOffset(float targetHeight)
	{
		float num = targetHeight;
		float num2 = num + (m_MinHeight - m_DefaultHeight) * m_HeightScaling;
		float num3 = num + (m_MaxHeight - m_DefaultHeight) * m_HeightScaling;
		if (num2 > num3)
		{
			float num4 = num2;
			num2 = num3;
			num3 = num4;
		}
		if (!m_AllowHeightOverflow)
		{
			float num5 = num;
			if (num2 < 0f)
			{
				num -= num2;
			}
			if (num3 > 1f)
			{
				num -= num3 - 1f;
			}
			if (num != num5)
			{
				d.Log($"Clamping set piece height offset from {num5} to {num}");
			}
		}
		return num;
	}

	public float ChooseHeightOffset(IntVector2 cellPosition, BiomeMap biomeMap)
	{
		cellPosition -= m_CellMidPoint;
		float num = 0f;
		int num2 = 0;
		int num3 = 30;
		int num4 = Mathf.Max(4, (int)Mathf.Sqrt(m_Width * m_Depth / num3));
		for (int i = m_BoundMin.y; i <= m_BoundMax.y; i += num4)
		{
			for (int j = m_BoundMin.x; j <= m_BoundMax.x; j += num4)
			{
				int num5 = i * m_Width + j;
				if (m_HeightBlend[num5] > 0)
				{
					num2++;
					num += biomeMap.GenerateHeightAtPoint(cellPosition + new IntVector2(j, i), Singleton.Manager<ManWorld>.inst.SeedValue, Singleton.Manager<ManWorld>.inst.CellsPerTileEdge, (int)Singleton.Manager<ManWorld>.inst.CellScale);
				}
			}
		}
		if (num2 == 0)
		{
			IntVector2 intVector = cellPosition + (m_BoundMin + m_BoundMax) / 2;
			num = biomeMap.GenerateHeightAtPoint(intVector, Singleton.Manager<ManWorld>.inst.SeedValue, Singleton.Manager<ManWorld>.inst.CellsPerTileEdge, (int)Singleton.Manager<ManWorld>.inst.CellScale);
		}
		else
		{
			num /= (float)num2;
		}
		float num6 = ChooseHeightOffset(num);
		d.Log($"SetPiece: sampled {num2} when choosing spawn height for {base.name}. Sampled height = {num} Adjusted height = {num6}");
		return num6;
	}

	private static void BlendHeight(ref float height, float weight, float newHeight)
	{
		height += (newHeight - height) * weight;
	}

	public float GenerateHeightForScenePos(IntVector2 tileCoord, Vector3 scenePos, float baseHeight, float setPieceOffsetHeight, IntVector2 setPieceOffset)
	{
		Vector2 vector = (scenePos - Singleton.Manager<ManWorld>.inst.TileManager.CalcTileOriginScene(in tileCoord)).ToVector2XZ() / Singleton.Manager<ManWorld>.inst.CellScale;
		IntVector2 intVector = setPieceOffset + m_CellMidPoint + (IntVector2)vector;
		GeneratePointData(intVector.x, intVector.y, out var heightBlend, out var height, out var _, out var _);
		height += setPieceOffsetHeight;
		BlendHeight(ref baseHeight, heightBlend, height);
		return baseHeight;
	}

	public float IsScenePosInside(WorldTile tile, Vector3 scenePos, BiomeMap.MapData biomeMapData)
	{
		Vector2 vector = (scenePos - Singleton.Manager<ManWorld>.inst.TileManager.CalcTileOriginScene(tile.Coord)).ToVector2XZ() / Singleton.Manager<ManWorld>.inst.CellScale;
		IntVector2 intVector = biomeMapData.setPieceOffset + m_CellMidPoint + (IntVector2)vector;
		GeneratePointData(intVector.x, intVector.y, out var _, out var _, out var splat, out var splat2);
		return splat + splat2;
	}

	public void ApplyHeightMap(BiomeMap.MapData biomeMapData, float[,] adjacentHeights, float[,] heights, int dimension)
	{
		int num = -1;
		int num2 = dimension + 1;
		IntVector2 intVector = biomeMapData.setPieceOffset + m_CellMidPoint;
		IntVector2 intVector2 = IntVector2.Max(new IntVector2(-1, -1), m_BoundMin - intVector);
		IntVector2 intVector3 = IntVector2.Min(new IntVector2(dimension + 1, dimension + 1), m_BoundMax - intVector);
		for (int i = intVector2.y; i <= intVector3.y; i++)
		{
			bool flag = i == num;
			bool flag2 = i == num2;
			int num3 = (i + intVector.y) * m_Width + (intVector2.x + intVector.x);
			int num4 = intVector2.x;
			while (num4 <= intVector3.x)
			{
				bool flag3 = num4 == num;
				bool flag4 = num4 == num2;
				float num5 = ((float)(int)m_Heights[num3] * 0.003921569f - m_DefaultHeight) * m_HeightScaling;
				float num6 = (float)(int)m_HeightBlend[num3] * 0.003921569f;
				if (num6 > 0f)
				{
					num5 += biomeMapData.setPieceOffsetHeight;
					if (!(flag || flag2) || !(flag3 || flag4))
					{
						if (flag)
						{
							BlendHeight(ref adjacentHeights[0, num4], num6, num5);
						}
						else if (flag3)
						{
							BlendHeight(ref adjacentHeights[1, i], num6, num5);
						}
						else if (flag2)
						{
							BlendHeight(ref adjacentHeights[2, num4], num6, num5);
						}
						else if (flag4)
						{
							BlendHeight(ref adjacentHeights[3, i], num6, num5);
						}
						else
						{
							BlendHeight(ref heights[i, num4], num6, num5);
						}
					}
				}
				num4++;
				num3++;
			}
		}
	}

	public bool OverrideScenerySpawn(BiomeMap.MapData biomeMapData, int i, int j, ref BiomeMap.SpawnDetail spawnDetail)
	{
		IntVector2 intVector = biomeMapData.setPieceOffset + m_CellMidPoint;
		bool result = false;
		i += intVector.x;
		j += intVector.y;
		if (i >= m_BoundMin.x && j >= m_BoundMin.y && i <= m_BoundMax.x && j <= m_BoundMax.y)
		{
			int num = j * m_Width + i;
			result = (m_ScenerySuppressBits[num / 8] & (1 << (num & 7))) != 0;
		}
		return result;
	}

	public float GetFrictionBlended(BiomeMap.MapData biomeMapData, Vector2 tileRelativeCellPos, float oldFriction)
	{
		d.Assert(6f == Singleton.Manager<ManWorld>.inst.CellScale);
		IntVector2 intVector = biomeMapData.setPieceOffset + m_CellMidPoint + (IntVector2)tileRelativeCellPos;
		GeneratePointData(intVector.x, intVector.y, out var _, out var _, out var splat, out var splat2);
		return Mathf.Lerp(oldFriction, m_Friction, splat + splat2);
	}

	public void MergeSplatMap(BiomeMap.MapData biomeMapData, int splatIndex1, int splatIndex2, float[,,] splatDataOut, int dimension)
	{
		int length = splatDataOut.GetLength(2);
		d.Assert(splatIndex1 < length);
		d.Assert(splatIndex2 < length);
		d.Assert(splatDataOut.GetLength(0) == dimension + 1 && splatDataOut.GetLength(1) == dimension + 1);
		IntVector2 intVector = biomeMapData.setPieceOffset + m_CellMidPoint;
		IntVector2 intVector2 = IntVector2.Max(new IntVector2(0, 0), m_BoundMin - intVector);
		IntVector2 intVector3 = IntVector2.Min(new IntVector2(dimension, dimension), m_BoundMax - intVector);
		for (int i = intVector2.y; i <= intVector3.y; i++)
		{
			int num = (i + intVector.y) * m_Width + (intVector2.x + intVector.x);
			int num2 = intVector2.x;
			while (num2 <= intVector3.x)
			{
				float num3 = (float)(int)m_SplatMap1[num] * 0.003921569f;
				float num4 = (float)(int)m_SplatMap2[num] * 0.003921569f;
				if (num3 > 0f || num4 > 0f)
				{
					for (int j = 0; j < length; j++)
					{
						splatDataOut[i, num2, j] *= 1f - (num3 + num4);
					}
					splatDataOut[i, num2, splatIndex1] += num3;
					splatDataOut[i, num2, splatIndex2] += num4;
				}
				num2++;
				num++;
			}
		}
	}

	public void AddTerrainObjects(IntVector2 tileOffset, IntVector2 clipOffset, int clipDimension, List<TileManager.SpawnData> spawnData, float jitterAmount, DRNG drng)
	{
		IntVector2 intVector = tileOffset + m_CellMidPoint;
		int num = 0;
		foreach (TerrainObjectData terrainObjects in m_TerrainObjectsList)
		{
			IntVector2 cellCoord = terrainObjects.m_CellPos - intVector;
			if (cellCoord.y < clipOffset.y || cellCoord.y >= clipOffset.y + clipDimension || cellCoord.x < clipOffset.x || cellCoord.x >= clipOffset.x + clipDimension)
			{
				continue;
			}
			bool ignoreBlockers = true;
			if (terrainObjects.m_Rotation.x != 0f || terrainObjects.m_Rotation.y != 0f || terrainObjects.m_Rotation.z != 0f || terrainObjects.m_Rotation.w != 0f)
			{
				Vector3 position = new Vector3(cellCoord.x, 0f, cellCoord.y) * 6f + terrainObjects.m_Offset;
				spawnData.Add(new TileManager.SpawnData(terrainObjects.m_TerrainObject, cellCoord, position, terrainObjects.m_Rotation, terrainObjects.m_Scale, ignoreBlockers));
			}
			else
			{
				drng.SetSeed((uint)cellCoord.x, (uint)cellCoord.y);
				Vector2 rotHV = new Vector2(drng.One(), drng.OneInclusive());
				Vector3 position2 = new Vector3(cellCoord.x, 0f, cellCoord.y) * 6f;
				if (jitterAmount != 0f)
				{
					Vector2 vector = new Vector2(drng.OnePosNeg(), drng.OnePosNeg());
					position2 += (vector * jitterAmount).ToVector3XZ();
				}
				spawnData.Add(new TileManager.SpawnData(terrainObjects.m_TerrainObject, cellCoord, position2, rotHV, terrainObjects.m_Scale, ignoreBlockers));
			}
			num++;
		}
	}

	public void LoadTerrainData(bool forceReload = false)
	{
		if ((forceReload || m_Heights == null || m_Heights.Length == 0) && !(m_CreatedFrom != null))
		{
			if (!forceReload && m_CompressedData != null && m_CompressedData.Length != 0)
			{
				Decompress();
			}
			else
			{
				d.LogError("Cannot reload terrain data for set piece mission when not in editor");
			}
		}
	}

	private byte[] RotateCopy(byte[] data, int w0, int h0, int rotation)
	{
		byte[] array = new byte[w0 * h0];
		int num = (((rotation & 1) == 1) ? w0 : h0);
		int num2 = (((rotation & 1) == 1) ? h0 : w0);
		switch ((rotation / 90) & 3)
		{
		case 0:
			Array.Copy(data, array, w0 * h0);
			break;
		case 1:
		{
			int num9 = 0;
			int num10 = 0;
			while (num9 < h0)
			{
				int num11 = 0;
				int num12 = num - 1;
				while (num11 < w0)
				{
					int num13 = num11 + num9 * w0;
					int num14 = num10 + num12 * num2;
					array[num14] = data[num13];
					num11++;
					num12--;
				}
				num9++;
				num10++;
			}
			break;
		}
		case 2:
		{
			int num15 = 0;
			int num16 = w0 * h0 - 1;
			while (num15 < w0 * h0)
			{
				array[num15] = data[num16];
				num15++;
				num16--;
			}
			break;
		}
		case 3:
		{
			int num3 = 0;
			int num4 = num2 - 1;
			while (num3 < h0)
			{
				int num5 = 0;
				int num6 = 0;
				while (num5 < w0)
				{
					int num7 = num5 + num3 * w0;
					int num8 = num4 + num6 * num2;
					array[num8] = data[num7];
					num5++;
					num6++;
				}
				num3++;
				num4--;
			}
			break;
		}
		}
		return array;
	}

	private byte[] RotateCopyBits(byte[] data, int w0, int h0, int rotation)
	{
		byte[] array = new byte[data.Length];
		int num = (((rotation & 1) == 1) ? w0 : h0);
		int num2 = (((rotation & 1) == 1) ? h0 : w0);
		switch ((rotation / 90) & 3)
		{
		case 0:
			Array.Copy(data, array, data.Length);
			break;
		case 1:
		{
			int num9 = 0;
			int num10 = 0;
			while (num9 < h0)
			{
				int num11 = 0;
				int num12 = num - 1;
				while (num11 < w0)
				{
					int num13 = num11 + num9 * w0;
					int num14 = num10 + num12 * num2;
					if ((data[num13 / 8] & (1 << (num13 & 7))) != 0)
					{
						array[num14 / 8] |= (byte)(1 << (num14 & 7));
					}
					num11++;
					num12--;
				}
				num9++;
				num10++;
			}
			break;
		}
		case 2:
		{
			int num15 = 0;
			int num16 = num - 1;
			while (num15 < h0)
			{
				int num17 = 0;
				int num18 = num2 - 1;
				while (num17 < w0)
				{
					int num19 = num17 + num15 * w0;
					int num20 = num18 + num16 * num2;
					if ((data[num19 / 8] & (1 << (num19 & 7))) != 0)
					{
						array[num20 / 8] |= (byte)(1 << (num20 & 7));
					}
					num17++;
					num18--;
				}
				num15++;
				num16--;
			}
			break;
		}
		case 3:
		{
			int num3 = 0;
			int num4 = num2 - 1;
			while (num3 < h0)
			{
				int num5 = 0;
				int num6 = 0;
				while (num5 < w0)
				{
					int num7 = num5 + num3 * w0;
					int num8 = num4 + num6 * num2;
					if ((data[num7 / 8] & (1 << (num7 & 7))) != 0)
					{
						array[num8 / 8] |= (byte)(1 << (num8 & 7));
					}
					num5++;
					num6++;
				}
				num3++;
				num4--;
			}
			break;
		}
		}
		return array;
	}

	public TerrainSetPiece CreateRotatedCopy(int rotation)
	{
		if (rotation % 360 == 0)
		{
			return this;
		}
		TerrainSetPiece terrainSetPiece = ScriptableObject.CreateInstance<TerrainSetPiece>();
		CopyDataRotatedTo(terrainSetPiece, rotation);
		return terrainSetPiece;
	}

	private void RotateBounds(int rotation, out int outWidth, out int outDepth, out IntVector2 outMidpoint, out IntVector2 outBoundMin, out IntVector2 outBoundMax)
	{
		if (rotation % 90 != 0)
		{
			d.LogError("Non-90 degree rotations are not supported for rotation terrain set pieces.");
		}
		switch ((rotation / 90) & 3)
		{
		case 0:
			outWidth = m_Width;
			outDepth = m_Depth;
			outBoundMin = m_BoundMin;
			outBoundMax = m_BoundMax;
			outMidpoint = m_CellMidPoint;
			break;
		case 1:
			outWidth = m_Depth;
			outDepth = m_Width;
			outMidpoint = new IntVector2(m_CellMidPoint.y, m_Width - 1 - m_CellMidPoint.x);
			outBoundMin = new IntVector2(m_BoundMin.y, m_Width - 1 - m_BoundMax.x);
			outBoundMax = new IntVector2(m_BoundMax.y, m_Width - 1 - m_BoundMin.x);
			break;
		case 2:
			outWidth = m_Width;
			outDepth = m_Depth;
			outMidpoint = new IntVector2(m_Width - 1 - m_CellMidPoint.x, m_Depth - 1 - m_CellMidPoint.y);
			outBoundMin = new IntVector2(m_Width - 1 - m_BoundMax.x, m_Depth - 1 - m_BoundMax.y);
			outBoundMax = new IntVector2(m_Width - 1 - m_BoundMin.x, m_Depth - 1 - m_BoundMin.y);
			break;
		default:
			outWidth = m_Depth;
			outDepth = m_Width;
			outMidpoint = new IntVector2(m_Depth - 1 - m_CellMidPoint.y, m_CellMidPoint.x);
			outBoundMin = new IntVector2(m_Depth - 1 - m_BoundMax.y, m_BoundMin.x);
			outBoundMax = new IntVector2(m_Depth - 1 - m_BoundMin.y, m_BoundMax.x);
			break;
		}
	}

	public void CopyDataRotatedTo(TerrainSetPiece rotated, int rotation)
	{
		if (rotation % 90 != 0)
		{
			d.LogError("Non-90 degree rotations are not supported for rotation terrain set pieces.");
		}
		LoadTerrainData();
		rotated.m_CreatedFrom = this;
		rotated.m_CopyRotation = rotation;
		rotated.m_DefaultHeight = m_DefaultHeight;
		rotated.m_HeightScaling = m_HeightScaling;
		rotated.m_AllowHeightOverflow = m_AllowHeightOverflow;
		rotated.m_ScenerySuppressType = m_ScenerySuppressType;
		rotated.m_SuppressSceneryThreshold = m_SuppressSceneryThreshold;
		rotated.m_TerrainLayer1 = m_TerrainLayer1;
		rotated.m_TerrainLayer2 = m_TerrainLayer2;
		rotated.m_MaxInstancesInWorld = m_MaxInstancesInWorld;
		rotated.m_Friction = m_Friction;
		rotated.m_MinHeight = m_MinHeight;
		rotated.m_MaxHeight = m_MaxHeight;
		RotateBounds(rotation, out rotated.m_Width, out rotated.m_Depth, out rotated.m_CellMidPoint, out rotated.m_BoundMin, out rotated.m_BoundMax);
		rotated.m_Heights = RotateCopy(m_Heights, m_Width, m_Depth, rotation);
		rotated.m_HeightBlend = RotateCopy(m_HeightBlend, m_Width, m_Depth, rotation);
		rotated.m_SplatMap1 = RotateCopy(m_SplatMap1, m_Width, m_Depth, rotation);
		rotated.m_SplatMap2 = RotateCopy(m_SplatMap2, m_Width, m_Depth, rotation);
		rotated.m_ScenerySuppressBits = RotateCopyBits(m_ScenerySuppressBits, m_Width, m_Depth, rotation);
		rotated.m_TerrainObjectsList = new List<TerrainObjectData>(m_TerrainObjectsList.Count);
		foreach (TerrainObjectData terrainObjects in m_TerrainObjectsList)
		{
			rotated.m_TerrainObjectsList.Add(terrainObjects.GetRotated(rotation, m_CellMidPoint, rotated.m_CellMidPoint));
		}
	}
}
