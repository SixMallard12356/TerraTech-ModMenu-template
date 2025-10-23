#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Collections;
using Unity.IL2CPP.CompilerServices;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class BiomeMap : MonoBehaviour
{
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	public class MapData
	{
		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public class HeightBuffer
		{
			public float[,] heights;

			public float[,] adjacentHeights;

			public float[] heightError;

			public int Dimension => heights.GetLength(0);

			public HeightBuffer()
			{
				heights = new float[s_HeightBufferPoolDimension, s_HeightBufferPoolDimension];
				adjacentHeights = new float[4, s_HeightBufferPoolDimension];
				int num = 0;
				for (int num2 = s_HeightBufferPoolDimension; num2 >= 16; num2 /= 2)
				{
					num += num2 / 16 * (num2 / 16);
				}
				heightError = new float[num];
			}
		}

		public MapCell[,] cells;

		public SpawnDetail[,] sceneryPlacement;

		public CounterArray biomesUsed = new CounterArray();

		public int dimension;

		public int mtDimension;

		public HeightBuffer heightData;

		public float[,,] splatData;

		public TerrainLayer[] terrainLayers;

		public TerrainData terrainData;

		public TerrainSetPiece setPiece;

		public IntVector2 setPieceOffset;

		public float setPieceOffsetHeight;

		private static int s_SceneryPlacementDimension = 0;

		private static Stack<SpawnDetail[,]> s_SceneryPlacementPool = new Stack<SpawnDetail[,]>();

		private static int s_HeightBufferPoolDimension = 0;

		private static Stack<HeightBuffer> s_HeightBufferPool = new Stack<HeightBuffer>();

		private static int s_SplatBufferPoolDimension = 0;

		private static Dictionary<int, Stack<float[,,]>> s_SplatBufferPool = new Dictionary<int, Stack<float[,,]>>();

		private static Dictionary<int, Stack<TerrainLayer[]>> s_TerrainLayerArrayPool = new Dictionary<int, Stack<TerrainLayer[]>>();

		public void Init(int dimension)
		{
			int num = dimension + 3;
			if (cells == null || this.dimension != dimension)
			{
				cells = new MapCell[num, num];
			}
			setPiece = null;
			splatData = null;
			terrainLayers = null;
			sceneryPlacement = null;
			this.dimension = dimension;
			if (terrainData == null)
			{
				terrainData = new TerrainData();
			}
			terrainData.baseMapResolution = Globals.inst.m_TerrainBaseMapResolution;
			terrainData.thickness = Globals.inst.m_TerrainCollisionThickness;
		}

		public void AllocateHeightBuffer(int dimension)
		{
			lock (s_HeightBufferPool)
			{
				d.Assert(Mathf.IsPowerOfTwo(dimension), "heightmap buffer must be power-two (plus one)");
				dimension++;
				if (s_HeightBufferPoolDimension != dimension)
				{
					s_HeightBufferPoolDimension = dimension;
					s_HeightBufferPool.Clear();
				}
				if (s_HeightBufferPool.Count == 0)
				{
					s_HeightBufferPool.Push(new HeightBuffer());
				}
				heightData = s_HeightBufferPool.Pop();
			}
		}

		public void AllocateSplatBuffer(int dimension, int numSplats)
		{
			lock (s_SplatBufferPool)
			{
				if (s_SplatBufferPoolDimension != dimension)
				{
					s_SplatBufferPoolDimension = dimension;
					foreach (Stack<float[,,]> value2 in s_SplatBufferPool.Values)
					{
						value2.Clear();
					}
					s_SplatBufferPool.Clear();
				}
				if (!s_SplatBufferPool.TryGetValue(numSplats, out var value))
				{
					value = new Stack<float[,,]>();
					s_SplatBufferPool.Add(numSplats, value);
				}
				if (value.Count == 0)
				{
					value.Push(new float[s_SplatBufferPoolDimension, s_SplatBufferPoolDimension, numSplats]);
				}
				splatData = value.Pop();
			}
		}

		public void AllocateTerrainLayerArray(int size)
		{
			lock (s_TerrainLayerArrayPool)
			{
				if (!s_TerrainLayerArrayPool.TryGetValue(size, out var value))
				{
					value = new Stack<TerrainLayer[]>();
					s_TerrainLayerArrayPool.Add(size, value);
				}
				if (value.Count == 0)
				{
					TerrainLayer[] array = new TerrainLayer[size];
					for (int i = 0; i < size; i++)
					{
						array[i] = null;
					}
					value.Push(array);
				}
				terrainLayers = value.Pop();
			}
		}

		public void AllocateSceneryBuffer(int dimension)
		{
			lock (s_SceneryPlacementPool)
			{
				if (dimension != s_SceneryPlacementDimension)
				{
					s_SceneryPlacementDimension = dimension;
					s_SceneryPlacementPool.Clear();
				}
				if (s_SceneryPlacementPool.Count == 0)
				{
					s_SceneryPlacementPool.Push(new SpawnDetail[dimension, dimension]);
				}
				sceneryPlacement = s_SceneryPlacementPool.Pop();
			}
		}

		public void RecycleHeightBuffer()
		{
			lock (this)
			{
				lock (s_HeightBufferPool)
				{
					if (heightData != null && heightData.Dimension == s_HeightBufferPoolDimension)
					{
						s_HeightBufferPool.Push(heightData);
						heightData = null;
					}
				}
			}
		}

		public void RecycleSplatBuffer()
		{
			lock (this)
			{
				lock (s_SplatBufferPool)
				{
					if (splatData != null && splatData.GetLength(0) == s_SplatBufferPoolDimension)
					{
						int length = splatData.GetLength(2);
						if (!s_SplatBufferPool.TryGetValue(length, out var value))
						{
							d.Assert(condition: false, "couldn't find sub-pool when recycling splat buffer");
							return;
						}
						value.Push(splatData);
						splatData = null;
					}
				}
			}
		}

		public void ReturnTerrainLayerArray()
		{
			lock (this)
			{
				lock (s_TerrainLayerArrayPool)
				{
					if (terrainLayers != null)
					{
						if (!s_TerrainLayerArrayPool.TryGetValue(terrainLayers.Length, out var value))
						{
							d.Assert(condition: false, "couldn't find sub-pool when recycling splat proto buffer");
							return;
						}
						value.Push(terrainLayers);
						terrainLayers = null;
					}
				}
			}
		}

		public void RecycleSceneryBuffer()
		{
			lock (this)
			{
				lock (s_SceneryPlacementPool)
				{
					if (sceneryPlacement != null && sceneryPlacement.GetLength(0) == s_SceneryPlacementDimension)
					{
						s_SceneryPlacementPool.Push(sceneryPlacement);
						sceneryPlacement = null;
					}
				}
			}
		}

		public static void LogBufferPoolSizes()
		{
			int num = 0;
			int num2 = 0;
			num = s_HeightBufferPoolDimension * s_HeightBufferPoolDimension * 4 * s_HeightBufferPool.Count;
			num2 += num;
			d.Log($"heightmap buffer pool usage: {s_HeightBufferPool.Count} = {(float)num / 1024f:0.0}k");
			num = 0;
			string text = "";
			foreach (KeyValuePair<int, Stack<float[,,]>> item in s_SplatBufferPool)
			{
				num += s_SplatBufferPoolDimension * s_SplatBufferPoolDimension * item.Key * 4 * item.Value.Count;
				text += $"{item.Value.Count} of size {item.Key}, ";
			}
			num2 += num;
			d.Log($"splatmap buffer pool usage: {text} = {(float)num / 1024f:0.0}k");
			num = 0;
			text = "";
			foreach (KeyValuePair<int, Stack<TerrainLayer[]>> item2 in s_TerrainLayerArrayPool)
			{
				num += item2.Key * Marshal.SizeOf(typeof(TerrainLayer)) * item2.Value.Count;
				text += $"{item2.Value.Count} of size {item2.Key}, ";
			}
			num2 += num;
			d.Log($"splatmap prototype buffer pool usage: {text} = {(float)num / 1024f:0.0}k");
			num = s_SceneryPlacementDimension * s_SceneryPlacementDimension * 12 * s_SceneryPlacementPool.Count;
			num2 += num;
			d.Log($"scenery buffer pool usage: {s_SceneryPlacementPool.Count} = {(float)num / 1024f:0.0}k");
			d.Log($"total BiomeMap buffer pool memory: {(float)num2 / 1048576f:0.0}m");
		}
	}

	public struct MapCell
	{
		private Byte4 m_Indices;

		private Byte4 m_Weights;

		private Byte4 m_AltWeights;

		private const float k_WeightScaleMul = 0.003921569f;

		public const byte k_BiomeNull = byte.MaxValue;

		public const int NumWeights = 4;

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public void Init(byte[] indices, float[] weights, float[] weightsAlt = null)
		{
			d.Assert(condition: true);
			d.Assert(6 >= indices.Length && indices.Length == weights.Length);
			d.Assert(weightsAlt == null || weightsAlt.Length == weights.Length);
			m_Indices = uint.MaxValue;
			m_Weights = 0u;
			m_AltWeights = 0u;
			for (int i = 0; i < 4; i++)
			{
				m_Indices[i] = indices[i];
				m_Weights[i] = (byte)(255f * weights[i] + 0.25f);
				m_AltWeights[i] = ((weightsAlt == null) ? m_Weights[i] : ((byte)(255f * weightsAlt[i] + 0.25f)));
			}
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public void InitLegacyTruncate(byte[] indices, float[] weights, float[] weightsAlt = null)
		{
			d.Assert(6 >= indices.Length && indices.Length == weights.Length);
			d.Assert(weightsAlt == null || weightsAlt.Length == weights.Length);
			for (int i = 0; i < 4; i++)
			{
				if (i >= indices.Length)
				{
					m_Indices[i] = byte.MaxValue;
					m_Weights[i] = 0;
					m_AltWeights[i] = 0;
				}
				else
				{
					m_Indices[i] = indices[i];
					m_Weights[i] = (byte)(255f * weights[i] + 0.25f);
					m_AltWeights[i] = ((weightsAlt == null) ? m_Weights[i] : ((byte)(255f * weightsAlt[i] + 0.25f)));
				}
			}
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public byte Index(int index)
		{
			return m_Indices[index];
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public float Weight(int index)
		{
			return 0.003921569f * (float)(int)m_Weights[index];
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public float WeightAlt(int index)
		{
			return 0.003921569f * (float)(int)m_AltWeights[index];
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public float WeightAltAvg(int index)
		{
			return 0.0019607844f * (float)(m_Weights[index] + m_AltWeights[index]);
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public Color GetNearestColour(Func<byte, Biome> lookupBiome, Func<Biome, Color> getColourForBiomeFunc)
		{
			Biome arg = lookupBiome(m_Indices[0]);
			return getColourForBiomeFunc(arg);
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public Color GetNearestColour(Func<byte, Biome> lookupBiome)
		{
			return GetNearestColour(lookupBiome, GetBiomeEditorColour);
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public Color GetWeightedColour(Func<byte, Biome> lookupBiome, Func<Biome, Color> getColourForBiomeFunc)
		{
			Color color = Color.black;
			for (int i = 0; i < 4 && m_Weights[i] != 0; i++)
			{
				Biome arg = lookupBiome(m_Indices[i]);
				Color color2 = getColourForBiomeFunc(arg);
				color = color.Add(color2.ScaleRGB(0.003921569f * (float)(int)m_Weights[i]));
			}
			return color;
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public Color GetWeightedColour(Func<byte, Biome> lookupBiome)
		{
			return GetWeightedColour(lookupBiome, GetBiomeEditorColour);
		}

		private static Color GetBiomeEditorColour(Biome biome)
		{
			return biome.EditorRenderColour;
		}
	}

	public struct SpawnDetail
	{
		public TerrainObject t0;

		public TerrainObject t1;

		public TerrainObject t2;

		public float scale;

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public void Add(TerrainObject terrainObj)
		{
			if (t0 == null)
			{
				t0 = terrainObj;
			}
			else if (t1 == null)
			{
				t1 = terrainObj;
			}
			else if (t2 == null)
			{
				t2 = terrainObj;
			}
			else
			{
				d.Assert(condition: false, "too many prefabs for SpawnDetail cell");
			}
		}

		public void Clear()
		{
			t0 = null;
			t1 = null;
			t2 = null;
		}
	}

	public enum RenderMode
	{
		Nearest,
		Weighted
	}

	[Serializable]
	public class DistributionWeight
	{
		public string tag = "TAG";

		public float weight = 1f;

		public bool allowGreaterThanOne;

		public float calculatedMultiplier = 1f;
	}

	private class WeightingTagCounter
	{
		public DistributionWeight distWeight;

		public int count;
	}

	private struct PrefabGroupStackItem
	{
		public Biome.SceneryDistributor.PrefabGroup group;

		public int overrideIndex;

		public static readonly PrefabGroupStackItem Null = new PrefabGroupStackItem(null);

		public bool IsNull => group == null;

		public PrefabGroupStackItem(Biome.SceneryDistributor.PrefabGroup group, int overrideIndex = -1)
		{
			this.group = group;
			this.overrideIndex = overrideIndex;
		}
	}

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	public class CounterArray
	{
		private int[] counters;

		public void Clear(int maxBiomes)
		{
			if (counters == null || counters.Length < maxBiomes)
			{
				counters = new int[maxBiomes];
			}
			else
			{
				Array.Clear(counters, 0, counters.Length);
			}
		}

		public void Increment(byte index)
		{
			counters[index]++;
		}

		public void InitFromNative(NativeArray<int> srcData)
		{
			Clear(srcData.Length);
			for (int i = 0; i < srcData.Length; i++)
			{
				counters[i] = srcData[i];
			}
		}

		public int CountBiomes()
		{
			int num = 0;
			for (int i = 0; i < counters.Length; i++)
			{
				if (counters[i] != 0)
				{
					num++;
				}
			}
			return num;
		}

		public int GetBest(byte[] best)
		{
			int num = 0;
			for (int i = 0; i < best.Length; i++)
			{
				best[i] = byte.MaxValue;
			}
			for (byte b = 0; b < counters.Length; b++)
			{
				if (counters[b] != 0)
				{
					num++;
					int num2 = 0;
					bool flag = false;
					for (int j = 0; j < best.Length; j++)
					{
						if (best[j] == byte.MaxValue)
						{
							best[j] = b;
							flag = true;
							break;
						}
						if (counters[best[j]] < counters[best[num2]])
						{
							num2 = j;
						}
					}
					if (!flag && counters[b] > counters[best[num2]])
					{
						best[num2] = b;
					}
				}
			}
			return num;
		}

		public void TraceReport()
		{
		}
	}

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	private class GenerationBuffers
	{
		private struct BiomeIndexReference
		{
			public byte[] indices;

			public byte indexToUse;
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public class MapBuf
		{
			public float[] cellScale;

			public float[] invCellScale;

			public Vector2[][,] VCPs;

			public byte[][,] biomeIndices;

			public IntVector2[,] gridRange;

			public IntVector2 origin;

			public IntVector2 extents;

			public int tileSize;

			public int cellSize;

			public int detail;

			public int seed;

			public DRNG drng = new DRNG();

			public byte[] indices9 = new byte[9];

			public float[] distances9 = new float[9];

			public byte[] indices4 = new byte[4];

			public float[] weights4 = new float[4];

			public float[] weights4alt = new float[4];

			public CounterArray biomesUsed;

			public void SetParams(IntVector2 origin, IntVector2 extents, int tileSize, int cellSize, int useDetailLevel, int seed)
			{
				this.origin = origin;
				this.extents = extents;
				this.tileSize = tileSize;
				this.cellSize = cellSize;
				detail = useDetailLevel;
				this.seed = seed;
			}

			public void Initialise(float distScaleMacro, float distScaleMajor, float distScaleMinor, float distScaleMicro)
			{
				d.Assert(distScaleMajor == 2f, "distScaleMajor must be exactly 2 to guarantee maximum 4 weights per cell");
				Vector2 vector = (Vector2)origin * (float)cellSize;
				Vector2 vector2 = (Vector2)(origin + extents - IntVector2.one) * (float)cellSize;
				if (cellScale == null)
				{
					cellScale = new float[4];
				}
				cellScale[0] = (float)tileSize / distScaleMacro;
				cellScale[1] = (float)tileSize / distScaleMajor;
				cellScale[2] = (float)tileSize / distScaleMinor;
				cellScale[3] = (float)tileSize / distScaleMicro;
				if (invCellScale == null)
				{
					invCellScale = new float[4];
				}
				invCellScale[0] = distScaleMacro / (float)tileSize;
				invCellScale[1] = distScaleMajor / (float)tileSize;
				invCellScale[2] = distScaleMinor / (float)tileSize;
				invCellScale[3] = distScaleMicro / (float)tileSize;
				if (VCPs == null)
				{
					VCPs = new Vector2[4][,];
				}
				if (biomeIndices == null)
				{
					biomeIndices = new byte[4][,];
				}
				if (gridRange == null)
				{
					gridRange = new IntVector2[4, 2];
				}
				for (int i = 0; i < 4; i++)
				{
					IntVector2 intVector = IntVector2.one * (4 - i);
					Vector2 vector3 = vector * invCellScale[i];
					Vector2 vector4 = vector2 * invCellScale[i];
					gridRange[i, 0] = new IntVector2(Mathf.FloorToInt(vector3.x), Mathf.FloorToInt(vector3.y)) - intVector;
					gridRange[i, 1] = new IntVector2(Mathf.FloorToInt(vector4.x), Mathf.FloorToInt(vector4.y)) + intVector;
					IntVector2 intVector2 = gridRange[i, 1] - gridRange[i, 0] + IntVector2.one;
					if (VCPs[i] == null || VCPs[i].GetLength(0) < intVector2.x || VCPs[i].GetLength(1) < intVector2.y)
					{
						VCPs[i] = new Vector2[intVector2.x, intVector2.y];
					}
					if (biomeIndices[i] == null || biomeIndices[i].GetLength(0) < intVector2.x || biomeIndices[i].GetLength(1) < intVector2.y)
					{
						biomeIndices[i] = new byte[intVector2.x, intVector2.y];
					}
				}
			}

			public void ResetPointBuffers()
			{
				byte[] array = indices4;
				byte[] array2 = indices4;
				byte[] array3 = indices4;
				byte b;
				indices4[3] = (b = byte.MaxValue);
				array[0] = (array2[1] = (array3[2] = b));
				weights4[0] = (weights4[1] = (weights4[2] = (weights4[3] = 0f)));
				weights4alt[0] = (weights4alt[1] = (weights4alt[2] = (weights4alt[3] = 0f)));
			}

			private static void SwapWeights(float[] weights, byte[] indices, int a, int b)
			{
				if (weights[b] > weights[a])
				{
					float num = weights[a];
					weights[a] = weights[b];
					weights[b] = num;
					byte b2 = indices[a];
					indices[a] = indices[b];
					indices[b] = b2;
				}
			}

			public void SortPointBuffers()
			{
				SwapWeights(weights4, indices4, 0, 1);
				SwapWeights(weights4, indices4, 1, 2);
				SwapWeights(weights4, indices4, 2, 3);
				SwapWeights(weights4, indices4, 0, 1);
				SwapWeights(weights4, indices4, 1, 2);
				SwapWeights(weights4, indices4, 0, 1);
			}
		}

		public Dictionary<int, byte> voronoiPointLookup = new Dictionary<int, byte>();

		public BiomeMap activeMap;

		public Biome.SceneryDistributor currentDistrib;

		public MapBuf mapBuffer;

		public Vector2[] voronoiPoints = new Vector2[6];

		public Vector2[] voronoiPointsRegion = new Vector2[4];

		public MapCell mapCellWorking;

		public float[,] heightIntermediateData;

		public float[,] heightIntermediateAdjacentData;

		public float[,,] texBlendIntermediateData;

		private Dictionary<int, Vector4[,]> interpTables = new Dictionary<int, Vector4[,]>();

		private Dictionary<int, MapGenerator.GenerationContext> blendContextLookup = new Dictionary<int, MapGenerator.GenerationContext>();

		private static Stack<MapGenerator.GenerationContext> s_BlendContextPool = new Stack<MapGenerator.GenerationContext>();

		private BiomeIndexReference biomeIndexRef;

		public void SetIndexRef(byte[] indices, byte indexToUse)
		{
			biomeIndexRef.indices = indices;
			biomeIndexRef.indexToUse = indexToUse;
		}

		public void SetIndexFromRef(byte value)
		{
			biomeIndexRef.indices[biomeIndexRef.indexToUse] = value;
		}

		public GenerationBuffers()
		{
			mapBuffer = new MapBuf();
		}

		public void InitIndexLookup(BiomeMap active)
		{
			voronoiPointLookup.Clear();
			activeMap = active;
		}

		public void InitHeightIntermediate(int dimension)
		{
			if (heightIntermediateData == null || heightIntermediateData.GetLength(0) < dimension + 1)
			{
				heightIntermediateData = new float[dimension + 1, dimension + 1];
				heightIntermediateAdjacentData = new float[4, dimension + 1];
			}
		}

		public void InitTexBlendIntermediate(int dimension, int numSplats)
		{
			if (texBlendIntermediateData == null || texBlendIntermediateData.GetLength(0) < dimension + 1 || texBlendIntermediateData.GetLength(2) < numSplats)
			{
				texBlendIntermediateData = new float[dimension + 1, dimension + 1, numSplats];
			}
		}

		public void ClearBlendContextLookup()
		{
			lock (s_BlendContextPool)
			{
				foreach (MapGenerator.GenerationContext value in blendContextLookup.Values)
				{
					s_BlendContextPool.Push(value);
				}
			}
			blendContextLookup.Clear();
		}

		public MapGenerator.GenerationContext GetBlendContextLookup(MapGenerator generator, int seed)
		{
			int hashCode = generator.GetHashCode();
			if (!blendContextLookup.TryGetValue(hashCode, out var value))
			{
				lock (s_BlendContextPool)
				{
					if (s_BlendContextPool.Count == 0)
					{
						s_BlendContextPool.Push(new MapGenerator.GenerationContext());
					}
					value = s_BlendContextPool.Pop();
				}
				value.Setup(generator, seed);
				blendContextLookup.Add(hashCode, value);
			}
			return value;
		}

		public Vector4[,] GetBilinearTable(int upScale)
		{
			if (!interpTables.TryGetValue(upScale, out var value))
			{
				value = GenerateBilinearTable(upScale);
				interpTables[upScale] = value;
			}
			return value;
		}
	}

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	private class RenderBuffers
	{
		public MapCell[,] cells;

		public GenerationBuffers.MapBuf mapBuf;

		private Dictionary<uint, HashSet<byte>> tileLookup = new Dictionary<uint, HashSet<byte>>();

		private Stack<HashSet<byte>> indexSetPool = new Stack<HashSet<byte>>();

		public void Init(IntVector2 bufferExtents, IntVector2 origin, IntVector2 extents, int tileSize, int cellSize, int detailLevel, int seed)
		{
			if (cells == null || cells.GetLength(0) != bufferExtents.x || cells.GetLength(1) != bufferExtents.y)
			{
				cells = new MapCell[bufferExtents.x, bufferExtents.y];
			}
			if (mapBuf == null)
			{
				mapBuf = new GenerationBuffers.MapBuf();
			}
			mapBuf.SetParams(origin, extents, tileSize, cellSize, detailLevel, seed);
		}

		public void RenderControlPoints(MapCell[,] cells, int maxControlPoints)
		{
			int num = mapBuf.gridRange[mapBuf.detail, 1].x - mapBuf.gridRange[mapBuf.detail, 0].x;
			int num2 = mapBuf.gridRange[mapBuf.detail, 1].y - mapBuf.gridRange[mapBuf.detail, 0].y;
			if (num * num2 > maxControlPoints)
			{
				return;
			}
			mapBuf.ResetPointBuffers();
			mapBuf.weights4[0] = 0f;
			int num3 = 0;
			for (int i = mapBuf.gridRange[mapBuf.detail, 0].x; i <= mapBuf.gridRange[mapBuf.detail, 1].x; i++)
			{
				int num4 = 0;
				for (int j = mapBuf.gridRange[mapBuf.detail, 0].y; j <= mapBuf.gridRange[mapBuf.detail, 1].y; j++)
				{
					Vector2 vector = mapBuf.VCPs[mapBuf.detail][num3, num4];
					Vector2 vector2 = mapBuf.origin * mapBuf.cellSize;
					Vector2 vector3 = (mapBuf.origin + mapBuf.extents) * mapBuf.cellSize;
					if (vector.x > vector2.x && vector.y > vector2.y && vector.x < vector3.x && vector.y < vector3.y)
					{
						Vector2 vector4 = vector / mapBuf.cellSize;
						IntVector2 intVector = new IntVector2(Mathf.FloorToInt(vector4.x), Mathf.FloorToInt(vector4.y)) - mapBuf.origin;
						cells[intVector.x, intVector.y].Init(mapBuf.indices4, mapBuf.weights4, mapBuf.weights4alt);
					}
					num4++;
				}
				num3++;
			}
		}

		public void CountBiomesPerTile(IntVector2 bufferExtents)
		{
			tileLookup.Clear();
			float num = mapBuf.invCellScale[1] / 2f;
			Vector2 vector = Vector2.one * 0.5f;
			for (int i = 0; i < bufferExtents.x; i++)
			{
				for (int j = 0; j < bufferExtents.y; j++)
				{
					Vector2 vector2 = ((Vector2)(mapBuf.origin + new IntVector2(i, j)) * (float)mapBuf.cellSize + vector) * num + vector;
					IntVector2 intVector = new IntVector2(Mathf.FloorToInt(vector2.x), Mathf.FloorToInt(vector2.y));
					uint key = (uint)((intVector.x + 32767 << 16) | (intVector.y + 32767));
					if (!tileLookup.TryGetValue(key, out var value))
					{
						if (indexSetPool.Count == 0)
						{
							indexSetPool.Push(new HashSet<byte>());
						}
						value = indexSetPool.Pop();
						tileLookup.Add(key, value);
					}
					ref MapCell reference = ref cells[i, j];
					for (int k = 0; k < 4 && reference.Weight(k) != 0f; k++)
					{
						value.Add(reference.Index(k));
					}
				}
			}
			foreach (KeyValuePair<uint, HashSet<byte>> item in tileLookup)
			{
				if (item.Value.Count > 4)
				{
					int num2 = (int)((long)(item.Key >> 16) - 32767L);
					int num3 = (int)((long)(item.Key & 0xFFFF) - 32767L);
					d.Log($"{item.Value.Count} biomes acting on tile [{num2},{num3}]");
				}
			}
			foreach (HashSet<byte> value2 in tileLookup.Values)
			{
				value2.Clear();
				indexSetPool.Push(value2);
			}
			tileLookup.Clear();
		}

		public byte GetMainBiomeIndexAtCellCoord(IntVector2 worldCoord)
		{
			IntVector2 intVector = worldCoord / mapBuf.cellSize - mapBuf.origin;
			if (intVector.x < 0 || intVector.y < 0 || intVector.x >= cells.GetLength(0) || intVector.y >= cells.GetLength(1))
			{
				return byte.MaxValue;
			}
			return cells[intVector.x, intVector.y].Index(0);
		}
	}

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	private class BiomeGroupDatabase
	{
		private struct ThresholdTable
		{
			public float[] threshold;

			public byte[] biomeIndex;

			public ThresholdTable(int size)
			{
				threshold = new float[size];
				biomeIndex = new byte[size];
			}
		}

		private struct WeightingFrame
		{
			public float distance;

			public float[] thresholds;

			public uint weightsHash;
		}

		private Biome[] m_Biomes;

		private BiomeGroup[] m_BiomeGroups;

		private ThresholdTable[] m_GroupThresholdTables;

		private WeightingFrame[] m_WeightingFrames;

		public void Init(BiomeMap map)
		{
			if (m_BiomeGroups != null)
			{
				return;
			}
			m_BiomeGroups = map.m_BiomeGroups;
			CalculateCombinedBiomeGroupWeights();
			List<Biome> list = new List<Biome>();
			List<ThresholdTable> list2 = new List<ThresholdTable>();
			BiomeGroup[] biomeGroups = map.m_BiomeGroups;
			foreach (BiomeGroup biomeGroup in biomeGroups)
			{
				d.Assert(biomeGroup.Biomes.Length == biomeGroup.BiomeWeights.Length);
				ThresholdTable item = new ThresholdTable(biomeGroup.Biomes.Length);
				list2.Add(item);
				float num = 0f;
				float[] biomeWeights = biomeGroup.BiomeWeights;
				foreach (float num2 in biomeWeights)
				{
					num += num2;
				}
				float num3 = 0f;
				for (int k = 0; k < biomeGroup.Biomes.Length; k++)
				{
					Biome biome = biomeGroup.Biomes[k];
					int num4 = list.FindIndex((Biome b) => (object)b == biome);
					if (num4 == -1)
					{
						num4 = list.Count;
						list.Add(biome);
					}
					num3 += biomeGroup.BiomeWeights[k] / num;
					item.threshold[k] = num3;
					item.biomeIndex[k] = (byte)num4;
				}
			}
			m_Biomes = list.ToArray();
			m_GroupThresholdTables = list2.ToArray();
			if (m_Biomes.Length > 255)
			{
				d.Assert(condition: false, $"{m_Biomes.Length} biomes in use! BiomeMap only supports up to 255: biome index must be converted from byte to short");
			}
		}

		private static uint GetBiomeGroupEditHash(BiomeGroup[] groups)
		{
			return 2673365688u;
		}

		private static int CompareKeys(WeightingFrame a, WeightingFrame b)
		{
			return (int)((a.distance - b.distance) * 1000f);
		}

		private void CalculateCombinedBiomeGroupWeights()
		{
			int num = 0;
			BiomeGroup[] biomeGroups = m_BiomeGroups;
			foreach (BiomeGroup biomeGroup in biomeGroups)
			{
				num += biomeGroup.WeightingByDistance.length;
				Biome[] biomes = biomeGroup.Biomes;
				for (int j = 0; j < biomes.Length; j++)
				{
					if (biomes[j] == null)
					{
						d.LogError(string.Format("Null biome in group {0}", Singleton.IsCalledOnMainThread ? biomeGroup.name : "unknown"));
					}
				}
			}
			m_WeightingFrames = new WeightingFrame[num];
			int num2 = 0;
			biomeGroups = m_BiomeGroups;
			for (int i = 0; i < biomeGroups.Length; i++)
			{
				Keyframe[] keys = biomeGroups[i].WeightingByDistance.keys;
				for (int j = 0; j < keys.Length; j++)
				{
					Keyframe keyframe = keys[j];
					ref WeightingFrame reference = ref m_WeightingFrames[num2];
					reference.distance = keyframe.time;
					reference.thresholds = new float[m_BiomeGroups.Length];
					float num3 = 0f;
					if (m_BiomeGroups.Length > 1)
					{
						for (int k = 0; k < m_BiomeGroups.Length; k++)
						{
							float num4 = m_BiomeGroups[k].WeightingByDistance.Evaluate(keyframe.time);
							reference.thresholds[k] = num4;
							num3 += num4;
						}
						if (num3 == 0f)
						{
							d.LogError(string.Format("No valid BiomeGroup weighting at distance {0}: defaulting to group {1}", keyframe.time, Singleton.IsCalledOnMainThread ? m_BiomeGroups[0].name : "(at index 0)"));
						}
					}
					if (num3 == 0f)
					{
						reference.thresholds[0] = 1f;
						num3 = 1f;
					}
					float num5 = 0f;
					float num6 = 1f / num3;
					uint num7 = 0u;
					for (int l = 0; l < m_BiomeGroups.Length; l++)
					{
						num5 += reference.thresholds[l] * num6;
						reference.thresholds[l] = num5;
						num7 = ((l == 0) ? HashCodeUtility.FNVHash(num5) : HashCodeUtility.FNVHashCombine(num7, num5));
					}
					reference.weightsHash = num7;
					num2++;
				}
			}
			Array.Sort(m_WeightingFrames, CompareKeys);
		}

		public float[] GetInterpolatedWeightingFrame(float distance, ref float[] weightingThresholdsContainer)
		{
			int num = -1;
			int num2 = -1;
			for (int i = 0; i < m_WeightingFrames.Length; i++)
			{
				if (m_WeightingFrames[i].distance <= distance)
				{
					num = i;
					continue;
				}
				num2 = i;
				break;
			}
			if (num == -1 && num2 == -1)
			{
				d.Assert(condition: false, "No weighting frames?");
				return null;
			}
			if (num == -1)
			{
				return m_WeightingFrames[num2].thresholds;
			}
			if (num2 == -1 || m_WeightingFrames[num].weightsHash == m_WeightingFrames[num2].weightsHash)
			{
				return m_WeightingFrames[num].thresholds;
			}
			if (weightingThresholdsContainer == null || weightingThresholdsContainer.Length != m_WeightingFrames[num].thresholds.Length)
			{
				weightingThresholdsContainer = new float[m_WeightingFrames[num].thresholds.Length];
			}
			float num3 = (distance - m_WeightingFrames[num].distance) / (m_WeightingFrames[num2].distance - m_WeightingFrames[num].distance);
			for (int j = 0; j < m_BiomeGroups.Length; j++)
			{
				weightingThresholdsContainer[j] = m_WeightingFrames[num].thresholds[j] + num3 * (m_WeightingFrames[num2].thresholds[j] - m_WeightingFrames[num].thresholds[j]);
			}
			return weightingThresholdsContainer;
		}

		public byte GetBiomeByGroupWeighting(byte groupIndex, float rng)
		{
			int num = -1;
			ref ThresholdTable reference = ref m_GroupThresholdTables[groupIndex];
			while (++num < reference.threshold.Length)
			{
				if (rng <= reference.threshold[num])
				{
					return reference.biomeIndex[num];
				}
			}
			d.Assert(condition: false, "failed to match any weighting threshold");
			return byte.MaxValue;
		}

		public Biome LookupBiome(byte index)
		{
			if (index == byte.MaxValue)
			{
				return null;
			}
			return m_Biomes[index];
		}

		public int NumBiomes()
		{
			return m_Biomes.Length;
		}

		public BiomeIterator IterateBiomes()
		{
			return new BiomeIterator(m_Biomes);
		}
	}

	public struct BiomeIterator
	{
		private Biome[] biomes;

		private int index;

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public Biome Current => biomes[index];

		public BiomeIterator(Biome[] biomes)
		{
			this.biomes = biomes;
			index = -1;
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public bool MoveNext()
		{
			return ++index != biomes.Length;
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public BiomeIterator GetEnumerator()
		{
			index = -1;
			return this;
		}
	}

	[Serializable]
	public class AdvancedParameters
	{
		[Range(0f, 1f)]
		public float vCellVarianceMacro = 1f;

		[Range(0f, 1f)]
		public float vCellVarianceMajor = 1f;

		[Range(0f, 1f)]
		public float vCellVarianceMinor = 1f;

		[Range(0f, 1f)]
		public float vCellVarianceMicro = 1f;

		public bool weightsDSquared = true;

		public bool weightsISqrt = true;

		public bool altWeightsDSquared = true;

		public bool altWeightsISqrt = true;

		public int heightmapResolutionPerCell = 2;

		public int multiTextureResolutionPerCell = 4;
	}

	[Serializable]
	public enum WorldGenVersioningType
	{
		ChangleListVersionInt,
		BiomeMapIteration
	}

	public struct GenOutput : IDisposable
	{
		public JobHandle jobHandle;

		public int rowLen;

		public NativeArray<Byte4> allIndices4;

		public NativeArray<float4> allWeights4;

		public NativeArray<float4> allWeightsAlt4;

		public void Dispose()
		{
			allIndices4.Dispose();
			allWeights4.Dispose();
			allWeightsAlt4.Dispose();
		}
	}

	[BurstCompile]
	public struct Byte4
	{
		public static readonly Byte4 zero;

		private uint val;

		public byte this[int index]
		{
			get
			{
				return (byte)(val >> index * 8);
			}
			set
			{
				val &= (uint)(~(255 << index * 8));
				val |= (uint)(value << index * 8);
			}
		}

		public static implicit operator Byte4(uint u)
		{
			return new Byte4
			{
				val = u
			};
		}

		public static explicit operator uint(Byte4 b4)
		{
			return b4.val;
		}
	}

	[BurstCompile]
	public struct Byte12
	{
		private uint3 val;

		public byte this[int index]
		{
			get
			{
				return (byte)(val[index / 3] >> index % 3 * 8);
			}
			set
			{
				val[index / 3] &= (uint)(~(255 << index % 3 * 8));
				val[index / 3] |= (uint)(value << index % 3 * 8);
			}
		}
	}

	[BurstCompile]
	public struct Float9
	{
		private float3x3 val;

		public float this[int index]
		{
			get
			{
				return val[index / 3][index % 3];
			}
			set
			{
				val[index / 3][index % 3] = value;
			}
		}
	}

	[BurstCompile]
	private struct CalcBiomeCellValuesJob : IJobParallelFor
	{
		public Vector2Int origin;

		public int cellSize;

		public int rowExtents;

		public float invCellScale;

		public Vector2Int gridrange;

		[Unity.Collections.ReadOnly]
		public NativeArray<Vector2> controlPoints;

		[Unity.Collections.ReadOnly]
		public NativeArray<byte> biomeIndices;

		public int vcpRowLen;

		public bool weightsDSquared;

		public bool weightsISqrt;

		public bool altWeightsUsed;

		[NativeDisableParallelForRestriction]
		public NativeArray<Byte4> allIndices4;

		[NativeDisableParallelForRestriction]
		public NativeArray<float4> allWeights4;

		[NativeDisableParallelForRestriction]
		public NativeArray<float4> allWeightsAlt4;

		public void Execute(int index)
		{
			CalculateCellBiomeValues(index);
		}

		private void CalculateCellBiomeValues(int index)
		{
			Byte12 @byte = default(Byte12);
			Float9 @float = default(Float9);
			int num = index % rowExtents;
			int num2 = index / rowExtents;
			Vector2 vector = new Vector2(origin.x + num, origin.y + num2) * cellSize * invCellScale;
			Vector2Int vector2Int = new Vector2Int(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y)) - gridrange;
			int num3 = 0;
			float num4 = float.MaxValue;
			for (int i = -1; i <= 1; i++)
			{
				int num5 = vector2Int.y + i;
				for (int j = -1; j <= 1; j++)
				{
					int num6 = vector2Int.x + j;
					Vector2 vector2 = vector - controlPoints[num6 + num5 * vcpRowLen] * invCellScale;
					float num7 = (weightsDSquared ? (vector2.x * vector2.x + vector2.y * vector2.y) : Mathf.Sqrt(vector2.x * vector2.x + vector2.y * vector2.y));
					num4 = Mathf.Min(num4, num7);
					@float[num3] = num7;
					@byte[num3] = biomeIndices[num6 + num5 * vcpRowLen];
					num3++;
				}
			}
			float num8 = 0f;
			float num9 = 1f / (weightsISqrt ? Mathf.Sqrt(num4) : num4);
			Byte4 value = (altWeightsUsed ? allIndices4[index] : ((Byte4)uint.MaxValue));
			float4 value2 = allWeights4[index];
			float4 value3 = (altWeightsUsed ? allWeightsAlt4[index] : default(float4));
			for (int k = 0; k < 9; k++)
			{
				float num10 = @float[k] - num4;
				float num11 = 1f - num10 * num9;
				if (num11 < 0f)
				{
					continue;
				}
				num8 += num11;
				byte b = @byte[k];
				for (int l = 0; l < 4; l++)
				{
					if (value[l] == byte.MaxValue)
					{
						value[l] = b;
						value2[l] = num11;
						if (altWeightsUsed)
						{
							value3[l] = 0f;
						}
						break;
					}
					if (value[l] == b)
					{
						value2[l] += num11;
						break;
					}
				}
			}
			float num12 = 1f / num8;
			value2 *= num12;
			allIndices4[index] = value;
			allWeights4[index] = value2;
			if (altWeightsUsed)
			{
				allWeightsAlt4[index] = value3;
			}
		}
	}

	[BurstCompile]
	private struct SwapWeightsJob : IJobParallelFor
	{
		[NativeDisableParallelForRestriction]
		public NativeArray<Byte4> allIndices4;

		[NativeDisableParallelForRestriction]
		public NativeArray<float4> allWeights4;

		public void Execute(int index)
		{
			Byte4 indices = allIndices4[index];
			float4 weights = allWeights4[index];
			SwapWeights(ref indices, ref weights, 0, 1);
			SwapWeights(ref indices, ref weights, 1, 2);
			SwapWeights(ref indices, ref weights, 2, 3);
			SwapWeights(ref indices, ref weights, 0, 1);
			SwapWeights(ref indices, ref weights, 1, 2);
			SwapWeights(ref indices, ref weights, 0, 1);
			allIndices4[index] = indices;
			allWeights4[index] = weights;
		}

		private static void SwapWeights(ref Byte4 indices, ref float4 weights, int a, int b)
		{
			if (weights[b] > weights[a])
			{
				float value = weights[a];
				weights[a] = weights[b];
				weights[b] = value;
				byte value2 = indices[a];
				indices[a] = indices[b];
				indices[b] = value2;
			}
		}
	}

	[BurstCompile]
	private struct AccumulateBiomeCountJob : IJob
	{
		public NativeArray<Byte4> allIndices4;

		public NativeArray<int> outBiomeCounts;

		public void Execute()
		{
			for (int i = 0; i < allIndices4.Length; i++)
			{
				Byte4 @byte = allIndices4[i];
				for (int j = 0; j < 4; j++)
				{
					byte b = @byte[j];
					if (b != byte.MaxValue)
					{
						outBiomeCounts[b]++;
					}
				}
			}
		}
	}

	[SerializeField]
	[HideInInspector]
	private Vector2 layer0Translation;

	[SerializeField]
	[HideInInspector]
	private float layer0Rotation;

	[SerializeField]
	[HideInInspector]
	private float layer0Scale = 1f;

	[HideInInspector]
	[SerializeField]
	private MapFuncVoronoi.DistanceMethod layer0DistMethod = MapFuncVoronoi.DistanceMethod.Length2;

	[SerializeField]
	[HideInInspector]
	private Vector2 layer1Translation;

	[HideInInspector]
	[SerializeField]
	private float layer1Rotation;

	[SerializeField]
	[HideInInspector]
	private float layer1Scale = 1f;

	[HideInInspector]
	[SerializeField]
	private MapFuncVoronoi.DistanceMethod layer1DistMethod = MapFuncVoronoi.DistanceMethod.Length4;

	[HideInInspector]
	[SerializeField]
	private float bandTolerance = 1f;

	[HideInInspector]
	[SerializeField]
	private bool enableRegions = true;

	[SerializeField]
	private Biome[] biomes;

	[SerializeField]
	private int m_BiomeDefaultSwapNumClosestRegions;

	[SerializeField]
	private Biome defaultBiome;

	[Tooltip("ChangleListVersionInt - Legacy system of comparing the Unix time (previously change list version) of saves and the biome maps\nBiomeMapIteration - New system where we manually assign iterating numbers independant from time")]
	[Space(10f)]
	[SerializeField]
	[FormerlySerializedAs("m_WorldGenVersioningType")]
	private WorldGenVersioningType m_RequiredWorldGenVersionType;

	[SerializeField]
	private int m_RequiredWorldGenVersion;

	[SerializeField]
	[Range(0.1f, 2f)]
	private float m_BiomeDistributionScaleMacro = 0.7f;

	[SerializeField]
	private AdvancedParameters m_AdvancedParameters = new AdvancedParameters();

	[SerializeField]
	private BiomeGroup[] m_BiomeGroups = new BiomeGroup[0];

	[SerializeField]
	public Landmarks m_LandmarkSpawner;

	[SerializeField]
	public VendorSpawner m_VendorSpawner;

	[SerializeField]
	public float m_VendorLandmarkMinSeparation;

	[SerializeField]
	private StuntRampSpawner m_StuntRampSpawner;

	[HideInInspector]
	[SerializeField]
	private DistributionWeight[] m_SceneryDistributionWeights;

	private MapGenerator.LayerXForm layer0XForm;

	private MapGenerator.LayerXForm layer1XForm;

	private int biomeSwapIndex;

	private int biomeSwapIndexDefault;

	private WorldGenVersionData m_WorldGenVersion;

	private static GenerationBuffers s_GenBuffers = new GenerationBuffers();

	private static GenerationBuffers s_AltBuffers = new GenerationBuffers();

	private static Stack<PrefabGroupStackItem> s_PrefabGroupStackWorking = new Stack<PrefabGroupStackItem>();

	private static Dictionary<int, WeightingTagCounter> s_TagCounters;

	private static RenderBuffers s_RenderBuffers;

	private const uint k_RNGSalt = 2673365688u;

	private const uint k_RNGSaltL0 = 2439608133u;

	private const uint k_RNGSaltL1 = 655679793u;

	private const uint k_RNGSaltL2 = 1043482650u;

	private const float k_BiomeDistributionScaleMajor = 2f;

	private const float k_BiomeDistributionScaleMinor = 5f;

	private const float k_BiomeDistributionScaleMicro = 9f;

	private const int numVoronoiMapBlends = 6;

	private const int numVoronoiMapBlendsRegion = 4;

	private Dictionary<int, DistributionWeight> m_DistributionWeights;

	private Dictionary<int, byte> m_BiomeSwapVPointLookup = new Dictionary<int, byte>();

	private BiomeGroupDatabase m_BiomeGroupDatabase;

	private int m_HeightmapResolutionPerCell;

	private int m_MultiTextureResolutionPerCell;

	private static Dictionary<int, Vector2> vpointverify = new Dictionary<int, Vector2>();

	private static Dictionary<int, int> vpointcollisions = new Dictionary<int, int>();

	private const int kLegacyNumWeights = 6;

	private Stopwatch m_Stopwatch = new Stopwatch();

	private static readonly ProfilerMarker s_PerfMarkerGenAll = new ProfilerMarker("Gen.All");

	private static readonly ProfilerMarker s_PerfMarkerGenInit = new ProfilerMarker("Gen.Init");

	private static readonly ProfilerMarker s_PerfMarkerGenMacro = new ProfilerMarker("Gen.Macro");

	private static readonly ProfilerMarker s_PerfMarkerGenMajor = new ProfilerMarker("Gen.Major");

	private static readonly ProfilerMarker s_PerfMarkerGenMinor = new ProfilerMarker("Gen.Minor");

	private static readonly ProfilerMarker s_PerfMarkerGenMicro = new ProfilerMarker("Gen.Micro");

	private static readonly ProfilerMarker s_PerfMarkerGenPost = new ProfilerMarker("Gen.Post");

	private static readonly ProfilerMarker s_PerfMarkerGenSchedule = new ProfilerMarker("Gen.Schedule");

	private static readonly ProfilerMarker s_PerfMarkerGenComplete = new ProfilerMarker("Gen.Complete");

	private static readonly ProfilerMarker s_PerfMarkerGenDoPoint = new ProfilerMarker("Gen.DoPoint");

	private IntVector2[] m_SplatMapMapping;

	private List<TerrainLayer> m_SplatMaps = new List<TerrainLayer>(5);

	public Landmarks LandmarkSpawner => m_LandmarkSpawner;

	public VendorSpawner VendorSpawner => m_VendorSpawner;

	public float VendorLandmarkMinSeparation => m_VendorLandmarkMinSeparation;

	public StuntRampSpawner StuntRampSpawner => m_StuntRampSpawner;

	public int RequiredWorldGenVersion => m_RequiredWorldGenVersion;

	public WorldGenVersioningType RequiredWorldGenVersionType => m_RequiredWorldGenVersionType;

	public WorldGenVersionData WorldGenVersionData
	{
		get
		{
			m_WorldGenVersion = new WorldGenVersionData(m_RequiredWorldGenVersion, m_RequiredWorldGenVersionType);
			return m_WorldGenVersion;
		}
	}

	private int Legacy_FixedHash(int orig, Vector2 v)
	{
		if (orig == 1674795557)
		{
			return Mathf.Abs(new Vector2(v.y * 10f, v.x * 0.1f).GetHashCode());
		}
		return orig;
	}

	private Dictionary<int, DistributionWeight> GetDistributionWeightsDict()
	{
		if (m_DistributionWeights == null)
		{
			m_DistributionWeights = new Dictionary<int, DistributionWeight>();
			DistributionWeight[] sceneryDistributionWeights = m_SceneryDistributionWeights;
			foreach (DistributionWeight distributionWeight in sceneryDistributionWeights)
			{
				m_DistributionWeights.Add(distributionWeight.tag.GetHashCode(), distributionWeight);
			}
		}
		return m_DistributionWeights;
	}

	private int HashVPoint(Vector2 vpoint)
	{
		return (int)(HashCodeUtility.FNVHash(vpoint.x, vpoint.y) & 0x7FFFFFFF);
	}

	private byte VoronoiBiomeMapRegions(Vector2 p, GenerationBuffers buf)
	{
		MapFuncVoronoi.GetPoints(p, layer0DistMethod, buf.voronoiPointsRegion);
		int num = ((!(WorldGenVersionData < WorldGenVersionData.kLegacy_8808_FixedHash)) ? HashVPoint(buf.voronoiPointsRegion[0]) : Legacy_FixedHash(Mathf.Abs(buf.voronoiPointsRegion[0].GetHashCode() / 2), buf.voronoiPointsRegion[0]));
		if (m_BiomeDefaultSwapNumClosestRegions != 0 && WorldGenVersionData >= WorldGenVersionData.kLegacy_13975_BiomeSwapIdx && m_BiomeSwapVPointLookup.TryGetValue(num, out var value))
		{
			return value;
		}
		return (byte)(num % buf.activeMap.biomes.Length);
	}

	private static void StoreBiomeIndex(IntVector2 ii, Vector2 cc, GenerationBuffers buf)
	{
		buf.SetIndexFromRef(buf.activeMap.VoronoiBiomeMapRegions(cc, buf));
	}

	private static GenerationBuffers GetGenBuffersByThread()
	{
		if (!Singleton.IsCalledOnMainThread)
		{
			return s_GenBuffers;
		}
		return s_AltBuffers;
	}

	public static void ResetGenerationBuffers()
	{
		s_GenBuffers.ClearBlendContextLookup();
		s_AltBuffers.ClearBlendContextLookup();
	}

	private void VoronoiBiomeMapDetail(Vector2 p, MapData map, ref MapCell mapCell)
	{
		GenerationBuffers buffers = GetGenBuffersByThread();
		float[] array = new float[6];
		byte[] array2 = new byte[6];
		MapFuncVoronoi.GetWeightsWithPointsBanded(p, layer1DistMethod, bandTolerance, array, buffers.voronoiPoints);
		for (byte b = 0; b < 6; b++)
		{
			if (array[b] == 0f)
			{
				array2[b] = byte.MaxValue;
			}
			else
			{
				int num = ((!(WorldGenVersionData < WorldGenVersionData.kLegacy_8808_FixedHash)) ? HashVPoint(buffers.voronoiPoints[b]) : Legacy_FixedHash(buffers.voronoiPoints[b].GetHashCode() >>> 1, buffers.voronoiPoints[b]));
				Vector2 value;
				if (!buffers.voronoiPointLookup.TryGetValue(num, out array2[b]))
				{
					buffers.SetIndexRef(array2, b);
					vpointverify[num] = buffers.voronoiPoints[b];
					if (enableRegions)
					{
						MapGenerator.GenerateOnePoint(layer0XForm, buffers.voronoiPoints[b], delegate(IntVector2 ii, Vector2 cc)
						{
							StoreBiomeIndex(ii, cc, buffers);
						});
					}
					else
					{
						array2[b] = (byte)(num % buffers.activeMap.biomes.Length);
					}
					if (WorldGenVersionData < WorldGenVersionData.kLegacy_13975_BiomeSwapIdx)
					{
						if (array2[b] == biomeSwapIndex)
						{
							array2[b] = (byte)biomeSwapIndexDefault;
						}
						else if (array2[b] == biomeSwapIndexDefault)
						{
							array2[b] = (byte)biomeSwapIndex;
						}
					}
					for (byte b2 = 0; b2 < array2[b]; b2++)
					{
						if (buffers.activeMap.biomes[b2] == buffers.activeMap.biomes[array2[b]])
						{
							array2[b] = b2;
							break;
						}
					}
					buffers.voronoiPointLookup[num] = array2[b];
					map?.biomesUsed.Increment(array2[b]);
				}
				else if (vpointverify.TryGetValue(num, out value))
				{
					if (value != buffers.voronoiPoints[b])
					{
						string text = "vpoint hash collision! " + buffers.voronoiPoints[b].x + "," + buffers.voronoiPoints[b].y + " vs " + vpointverify[num].x + "," + buffers.voronoiPoints[b].y + " : " + num + " (TELL RUSS)";
						int persistentHashCode = HashCodeUtility.GetPersistentHashCode(text);
						if (!vpointcollisions.ContainsKey(persistentHashCode))
						{
							d.LogError(text);
							vpointcollisions[persistentHashCode] = 1;
						}
					}
				}
				else
				{
					DebugUtil.AssertRelease(condition: false, $"vpoint potentially collides but can't check as it's not in the verify dictionary Vector2:({buffers.voronoiPoints[b].x},{buffers.voronoiPoints[b].y}) Hash:{num} (TELL RUSS)");
				}
				for (int num2 = b - 1; num2 >= 0; num2--)
				{
					if (array2[num2] != byte.MaxValue && buffers.activeMap.biomes[array2[num2]] == buffers.activeMap.biomes[array2[b]])
					{
						array[num2] += array[b];
						array[b] = 0f;
						array2[b] = byte.MaxValue;
						break;
					}
				}
			}
		}
		mapCell.InitLegacyTruncate(array2, array);
	}

	private void VoronoiBiomeMapInitBiomeSwapIndex(Vector2 p, byte swapIndex)
	{
		GenerationBuffers buffers = GetGenBuffersByThread();
		float[] da = new float[6];
		byte[] array = new byte[6];
		MapFuncVoronoi.GetWeightsWithPointsBanded(p, layer1DistMethod, bandTolerance, da, buffers.voronoiPoints);
		int num = ((!(WorldGenVersionData < WorldGenVersionData.kLegacy_8808_FixedHash)) ? HashVPoint(buffers.voronoiPoints[0]) : Legacy_FixedHash(buffers.voronoiPoints[0].GetHashCode() >>> 1, buffers.voronoiPoints[0]));
		buffers.SetIndexRef(array, 0);
		vpointverify[num] = buffers.voronoiPoints[0];
		if (enableRegions)
		{
			MapGenerator.GenerateOnePoint(layer0XForm, buffers.voronoiPoints[0], delegate(IntVector2 ii, Vector2 cc)
			{
				StoreBiomeIndex(ii, cc, buffers);
			});
		}
		else
		{
			array[0] = (byte)(num % buffers.activeMap.biomes.Length);
		}
		if (WorldGenVersionData < WorldGenVersionData.kLegacy_13975_BiomeSwapIdx)
		{
			if (biomeSwapIndexDefault != -1)
			{
				biomeSwapIndex = array[0];
			}
		}
		else if (m_BiomeDefaultSwapNumClosestRegions != 0)
		{
			m_BiomeSwapVPointLookup.Clear();
			for (int num2 = 0; num2 < m_BiomeDefaultSwapNumClosestRegions; num2++)
			{
				num = ((!(WorldGenVersionData < WorldGenVersionData.kLegacy_8808_FixedHash)) ? HashVPoint(buffers.voronoiPointsRegion[num2]) : Legacy_FixedHash(Mathf.Abs(buffers.voronoiPointsRegion[num2].GetHashCode() / 2), buffers.voronoiPointsRegion[num2]));
				m_BiomeSwapVPointLookup.Add(num, swapIndex);
			}
		}
	}

	private BiomeMap()
	{
		EditorHooks.PlayModeChanged -= InvalidateBiomeDB;
		EditorHooks.PlayModeChanged += InvalidateBiomeDB;
	}

	public bool CompatibleWithSavedWorldGen()
	{
		return ManSaveGame.CurrentSavedWorldGenVersion >= WorldGenVersionData;
	}

	private BiomeGroupDatabase GetBiomeDB()
	{
		if (m_BiomeGroupDatabase == null)
		{
			m_BiomeGroupDatabase = new BiomeGroupDatabase();
		}
		m_BiomeGroupDatabase.Init(this);
		return m_BiomeGroupDatabase;
	}

	public void InvalidateBiomeDB()
	{
		m_BiomeGroupDatabase = null;
	}

	public Biome LookupBiome(byte index)
	{
		if (index == byte.MaxValue)
		{
			return null;
		}
		if (WorldGenVersionData < WorldGenVersionData.kLegacy_14775_BiomeDB)
		{
			return biomes[index];
		}
		return GetBiomeDB().LookupBiome(index);
	}

	public int GetNumBiomes()
	{
		if (WorldGenVersionData < WorldGenVersionData.kLegacy_14775_BiomeDB)
		{
			return biomes.Length;
		}
		return GetBiomeDB().NumBiomes();
	}

	public BiomeIterator IterateBiomes()
	{
		if (WorldGenVersionData < WorldGenVersionData.kLegacy_14775_BiomeDB)
		{
			return new BiomeIterator(biomes);
		}
		return GetBiomeDB().IterateBiomes();
	}

	public void Render(Vector2 origin, Vector2 size, int step, int seed, int gridScale, int cellScale, Action<Color> doPixel, int renderDetail, RenderMode renderMode)
	{
		m_Stopwatch.Restart();
		if (WorldGenVersionData < WorldGenVersionData.kLegacy_14775_BiomeDB)
		{
			InitBiomeSwapIndex(seed);
			layer0XForm = new MapGenerator.LayerXForm(layer0Translation, layer0Rotation, layer0Scale, seed, WorldGenVersionData);
			layer1XForm = new MapGenerator.LayerXForm(layer1Translation, layer1Rotation, layer1Scale, seed, WorldGenVersionData);
			MapData dummyMap = new MapData();
			dummyMap.biomesUsed.Clear(biomes.Length);
			MapGenerator.GenerateMapAbstract(layer1XForm, origin, size, step, delegate(IntVector2 v, Vector2 c)
			{
				MapCell mapCell = default(MapCell);
				VoronoiBiomeMapDetail(c, dummyMap, ref mapCell);
				switch (renderMode)
				{
				case RenderMode.Nearest:
					doPixel(biomes[mapCell.Index(0)].EditorRenderColour);
					break;
				case RenderMode.Weighted:
				{
					Color color = Color.black;
					for (int i = 0; i < 4; i++)
					{
						if (mapCell.Weight(i) != 0f)
						{
							color = color.Add(biomes[mapCell.Index(0)].EditorRenderColour.ScaleRGB(mapCell.Weight(i)));
						}
					}
					doPixel(color);
					break;
				}
				}
			});
			return;
		}
		if (s_RenderBuffers == null)
		{
			s_RenderBuffers = new RenderBuffers();
		}
		IntVector2 bufferExtents = new IntVector2(size / step);
		s_RenderBuffers.Init(bufferExtents, origin / step, size / step, gridScale * cellScale, cellScale * step, renderDetail, seed);
		Func<byte, Biome> lookupBiome = GetBiomeDB().LookupBiome;
		GenBiomeMap(s_RenderBuffers.mapBuf, s_RenderBuffers.cells);
		m_Stopwatch.Stop();
		_ = (double)m_Stopwatch.ElapsedTicks / (double)Stopwatch.Frequency;
		if (renderMode == RenderMode.Nearest)
		{
			s_RenderBuffers.RenderControlPoints(s_RenderBuffers.cells, bufferExtents.x * bufferExtents.y / 16);
			for (int num = 0; num < s_RenderBuffers.cells.GetLength(1); num++)
			{
				for (int num2 = 0; num2 < s_RenderBuffers.cells.GetLength(0); num2++)
				{
					doPixel(s_RenderBuffers.cells[num2, num].GetNearestColour(lookupBiome));
				}
			}
		}
		else
		{
			if (renderMode != RenderMode.Weighted)
			{
				return;
			}
			for (int num3 = 0; num3 < s_RenderBuffers.cells.GetLength(1); num3++)
			{
				for (int num4 = 0; num4 < s_RenderBuffers.cells.GetLength(0); num4++)
				{
					doPixel(s_RenderBuffers.cells[num4, num3].GetWeightedColour(lookupBiome));
				}
			}
		}
	}

	public void Render_Jobbed(Vector2 origin, Vector2 size, int step, int seed, int gridScale, int cellScale, Action<Color32> doPixel, int renderDetail, RenderMode renderMode)
	{
		m_Stopwatch.Restart();
		if (WorldGenVersionData < WorldGenVersionData.kLegacy_14775_BiomeDB)
		{
			Render(origin, size, step, seed, gridScale, cellScale, delegate(Color c)
			{
				doPixel(c);
			}, renderDetail, renderMode);
			return;
		}
		if (s_RenderBuffers == null)
		{
			s_RenderBuffers = new RenderBuffers();
		}
		IntVector2 bufferExtents = new IntVector2(size / step);
		s_RenderBuffers.Init(bufferExtents, origin / step, size / step, gridScale * cellScale, cellScale * step, renderDetail, seed);
		GetBiomeDB();
		GenOutput genOutput = GenBiomeMap_Jobbed(s_RenderBuffers.mapBuf);
		switch (renderMode)
		{
		case RenderMode.Nearest:
		{
			for (int num2 = 0; num2 < genOutput.allIndices4.Length; num2++)
			{
				byte r2 = genOutput.allIndices4[num2][0];
				byte b2 = byte.MaxValue;
				doPixel(new Color32(r2, 0, b2, 0));
			}
			break;
		}
		case RenderMode.Weighted:
		{
			for (int num = 0; num < genOutput.allIndices4.Length; num++)
			{
				byte r = genOutput.allIndices4[num][0];
				byte g = genOutput.allIndices4[num][1];
				byte b = (byte)(255f * genOutput.allWeights4[num][0] + 0.25f);
				doPixel(new Color32(r, g, b, 0));
			}
			break;
		}
		}
		genOutput.Dispose();
		m_Stopwatch.Stop();
		_ = (double)m_Stopwatch.ElapsedTicks / (double)Stopwatch.Frequency;
	}

	public Biome LookupBiomeInRenderBuffer(Vector2 worldCoord)
	{
		return LookupBiome(s_RenderBuffers.GetMainBiomeIndexAtCellCoord(worldCoord));
	}

	public void GenerateBiomeBlendMap(Vector2 generationOriginWorld, WorldTile tile, int seed, int gridScale, int cellScale)
	{
		d.Assert(ManSaveGame.CurrentSavedWorldGenVersion >= WorldGenVersionData, $"Minimum worldgen version check failed: {WorldGenVersionData} vs {ManSaveGame.CurrentSavedWorldGenVersion}");
		lock (tile.BiomeMapData)
		{
			GenerationBuffers genBuffersByThread = GetGenBuffersByThread();
			if (WorldGenVersionData < WorldGenVersionData.kLegacy_14775_BiomeDB)
			{
				genBuffersByThread.InitIndexLookup(this);
				tile.BiomeMapData.biomesUsed.Clear(biomes.Length);
				layer0XForm = new MapGenerator.LayerXForm(layer0Translation, layer0Rotation, layer0Scale, seed, WorldGenVersionData);
				layer1XForm = new MapGenerator.LayerXForm(layer1Translation, layer1Rotation, layer1Scale, seed, WorldGenVersionData);
				MapGenerator.GenerateMapAbstract(layer1XForm, generationOriginWorld - Vector2.one * cellScale, Vector2.one * cellScale * (gridScale + 3), cellScale, delegate(IntVector2 v, Vector2 c)
				{
					VoronoiBiomeMapDetail(c, tile.BiomeMapData, ref tile.BiomeMapData.cells[v.x, v.y]);
				});
				return;
			}
			IntVector2 origin = new IntVector2(generationOriginWorld / cellScale) - IntVector2.one;
			IntVector2 extents = IntVector2.one * (gridScale + 3);
			tile.BiomeMapData.biomesUsed.Clear(GetNumBiomes());
			genBuffersByThread.mapBuffer.biomesUsed = tile.BiomeMapData.biomesUsed;
			genBuffersByThread.mapBuffer.SetParams(origin, extents, gridScale * cellScale, cellScale, 2, seed);
			GenBiomeMap(genBuffersByThread.mapBuffer, tile.BiomeMapData.cells);
			if ((bool)tile.BiomeMapData.setPiece)
			{
				d.Log($"SetPiece tile @({tile.Coord.x},{tile.Coord.y}) height offset = {tile.BiomeMapData.setPieceOffsetHeight} XZ cell offset = {tile.BiomeMapData.setPieceOffset}");
			}
		}
	}

	public void GenerateBiomeBlendsAtPoint(ref MapCell mapCell, Vector2 coord, int seed, int gridScale, int cellScale)
	{
		GenerationBuffers genBuffersByThread = GetGenBuffersByThread();
		if (WorldGenVersionData < WorldGenVersionData.kLegacy_14775_BiomeDB)
		{
			genBuffersByThread.InitIndexLookup(this);
			layer0XForm = new MapGenerator.LayerXForm(layer0Translation, layer0Rotation, layer0Scale, seed, WorldGenVersionData);
			layer1XForm = new MapGenerator.LayerXForm(layer1Translation, layer1Rotation, layer1Scale, seed, WorldGenVersionData);
			MapCell mapCellDummy = default(MapCell);
			MapGenerator.GenerateOnePoint(layer1XForm, coord, delegate(IntVector2 v, Vector2 c)
			{
				VoronoiBiomeMapDetail(c, null, ref mapCellDummy);
			});
			mapCell = mapCellDummy;
		}
		else
		{
			IntVector2 origin = new IntVector2(coord / cellScale);
			IntVector2 one = IntVector2.one;
			genBuffersByThread.mapBuffer.SetParams(origin, one, gridScale * cellScale, cellScale, 2, seed);
			MapCell[,] array = new MapCell[1, 1] { { mapCell } };
			GenBiomeMap(genBuffersByThread.mapBuffer, array);
			mapCell = array[0, 0];
		}
	}

	public float GenerateHeightAtPoint(Vector2 coord, int seed, int gridScale, int cellScale)
	{
		GenerationBuffers genBuffersByThread = GetGenBuffersByThread();
		GenerateBiomeBlendsAtPoint(ref genBuffersByThread.mapCellWorking, coord, seed, gridScale, cellScale);
		float num = 0f;
		float generatedValue = 0f;
		MapGenerator.PopulateCellDelegate populateCellDelegate = delegate(Vector2 p, float v, Color c)
		{
			generatedValue = v * 0.5f + 0.5f;
		};
		for (int num2 = 0; num2 < 4; num2++)
		{
			float num3 = genBuffersByThread.mapCellWorking.WeightAltAvg(num2);
			if (num3 != 0f)
			{
				Biome biome = LookupBiome(genBuffersByThread.mapCellWorking.Index(num2));
				MapGenerator.GenerationContext blendContextLookup = genBuffersByThread.GetBlendContextLookup(biome.HeightMapGenerator, seed);
				populateCellDelegate(coord, biome.HeightMapGenerator.GeneratePoint(blendContextLookup, coord), Color.black);
				num += generatedValue * num3;
			}
		}
		return num;
	}

	public void InitBiomeSwapIndex(int seed)
	{
		GetGenBuffersByThread().InitIndexLookup(this);
		layer0XForm = new MapGenerator.LayerXForm(layer0Translation, layer0Rotation, layer0Scale, seed, WorldGenVersionData);
		layer1XForm = new MapGenerator.LayerXForm(layer1Translation, layer1Rotation, layer1Scale, seed, WorldGenVersionData);
		byte swapIndex = (byte)biomes.ToList().FindIndex((Biome b) => b == defaultBiome);
		if (WorldGenVersionData < WorldGenVersionData.kLegacy_13975_BiomeSwapIdx)
		{
			biomeSwapIndexDefault = swapIndex;
			biomeSwapIndex = biomeSwapIndexDefault;
		}
		MapGenerator.GenerateOnePoint(layer1XForm, Vector3.zero, delegate(IntVector2 v, Vector2 c)
		{
			VoronoiBiomeMapInitBiomeSwapIndex(c, swapIndex);
		});
	}

	private static Vector2 ClampStartingGridCoords(int x, int y, Vector2 gridCoord)
	{
		if (x == 0 && y == 0)
		{
			return Vector2.zero;
		}
		if (x >= -1 && x <= 1 && y >= -1 && y <= 1)
		{
			if (Mathf.Abs(gridCoord.x) > Mathf.Abs(gridCoord.y))
			{
				gridCoord.x = Mathf.Max(Mathf.Abs(gridCoord.x), 1f) * Mathf.Sign(gridCoord.x);
			}
			else
			{
				gridCoord.y = Mathf.Max(Mathf.Abs(gridCoord.y), 1f) * Mathf.Sign(gridCoord.y);
			}
		}
		return gridCoord;
	}

	private static Vector2 GenCoordWithVariance(float x, float y, float variance, DRNG drng)
	{
		float num = (1f - variance) * 0.5f;
		return new Vector2(x + num + drng.One() * variance, y + num + drng.One() * variance);
	}

	private void PreCacheVCellGridMacro(GenerationBuffers.MapBuf mapBuf, uint rngSeed)
	{
		Vector2 vector = Vector2.one * -0.5f;
		float[] weightingThresholdsContainer = null;
		int num = 0;
		for (int i = mapBuf.gridRange[0, 0].x; i <= mapBuf.gridRange[0, 1].x; i++)
		{
			int num2 = 0;
			for (int j = mapBuf.gridRange[0, 0].y; j <= mapBuf.gridRange[0, 1].y; j++)
			{
				mapBuf.drng.SetSeedFromTable((uint)(i * 1234 + j) ^ rngSeed, (uint)(j * 5432 + i) ^ rngSeed);
				Vector2 gridCoord = vector + GenCoordWithVariance(i, j, m_AdvancedParameters.vCellVarianceMacro, mapBuf.drng);
				gridCoord = ClampStartingGridCoords(i, j, gridCoord);
				Vector2 vector2 = gridCoord * mapBuf.cellScale[0];
				mapBuf.VCPs[0][num, num2] = vector2;
				float[] interpolatedWeightingFrame = GetBiomeDB().GetInterpolatedWeightingFrame(vector2.magnitude / (float)mapBuf.tileSize, ref weightingThresholdsContainer);
				float num3 = mapBuf.drng.One();
				int num4 = -1;
				while (++num4 < m_BiomeGroups.Length && !(num3 < interpolatedWeightingFrame[num4]))
				{
				}
				d.Assert(num4 != interpolatedWeightingFrame.Length);
				mapBuf.biomeIndices[0][num, num2] = (byte)num4;
				num2++;
			}
			num++;
		}
	}

	private void PreCacheVCellGridMajor(GenerationBuffers.MapBuf mapBuf, uint rngSeed)
	{
		Vector2 vector = Vector2.one * -0.5f;
		int num = 0;
		for (int i = mapBuf.gridRange[1, 0].x; i <= mapBuf.gridRange[1, 1].x; i++)
		{
			int num2 = 0;
			for (int j = mapBuf.gridRange[1, 0].y; j <= mapBuf.gridRange[1, 1].y; j++)
			{
				mapBuf.drng.SetSeedFromTable((uint)(i * 1234 + j) ^ rngSeed, (uint)(j * 5432 + i) ^ rngSeed);
				mapBuf.VCPs[1][num, num2] = GenCoordWithVariance(i, j, m_AdvancedParameters.vCellVarianceMajor, mapBuf.drng) * mapBuf.cellScale[1];
				int num3 = i >> 1;
				int num4 = j >> 1;
				Vector2 vector2 = (new Vector2(num3, num4) - vector) * (mapBuf.cellScale[1] * 2f);
				IntVector2 intVector = new IntVector2(Mathf.FloorToInt(vector2.x * mapBuf.invCellScale[0]), Mathf.FloorToInt(vector2.y * mapBuf.invCellScale[0]));
				byte b = byte.MaxValue;
				float num5 = float.MaxValue;
				IntVector2 intVector2 = intVector - IntVector2.one - mapBuf.gridRange[0, 0];
				IntVector2 intVector3 = intVector + IntVector2.one - mapBuf.gridRange[0, 0];
				for (int k = intVector2.y; k <= intVector3.y; k++)
				{
					for (int l = intVector2.x; l <= intVector3.x; l++)
					{
						Vector2 vector3 = vector2 - mapBuf.VCPs[0][l, k];
						float num6 = vector3.x * vector3.x + vector3.y * vector3.y;
						if (num6 < num5)
						{
							num5 = num6;
							b = mapBuf.biomeIndices[0][l, k];
						}
					}
				}
				mapBuf.drng.SetSeedFromTable(HashCodeUtility.FNVHash((uint)num3, (uint)num4, b) ^ rngSeed);
				byte biomeByGroupWeighting = GetBiomeDB().GetBiomeByGroupWeighting(b, mapBuf.drng.One());
				mapBuf.biomeIndices[1][num, num2] = biomeByGroupWeighting;
				num2++;
			}
			num++;
		}
	}

	private void PreCacheVCellGridMinor(GenerationBuffers.MapBuf mapBuf, uint rngSeed)
	{
		int num = 0;
		for (int i = mapBuf.gridRange[2, 0].x; i <= mapBuf.gridRange[2, 1].x; i++)
		{
			int num2 = 0;
			for (int j = mapBuf.gridRange[2, 0].y; j <= mapBuf.gridRange[2, 1].y; j++)
			{
				mapBuf.drng.SetSeedFromTable((uint)(i * 1234 + j) ^ rngSeed, (uint)(j * 5432 + i) ^ rngSeed);
				Vector2 vector = GenCoordWithVariance(i, j, m_AdvancedParameters.vCellVarianceMinor, mapBuf.drng) * mapBuf.cellScale[2];
				mapBuf.VCPs[2][num, num2] = vector;
				IntVector2 intVector = new IntVector2(Mathf.FloorToInt(vector.x * mapBuf.invCellScale[1]), Mathf.FloorToInt(vector.y * mapBuf.invCellScale[1]));
				byte b = byte.MaxValue;
				float num3 = float.MaxValue;
				IntVector2 intVector2 = intVector - IntVector2.one - mapBuf.gridRange[1, 0];
				IntVector2 intVector3 = intVector + IntVector2.one - mapBuf.gridRange[1, 0];
				for (int k = intVector2.y; k <= intVector3.y; k++)
				{
					for (int l = intVector2.x; l <= intVector3.x; l++)
					{
						Vector2 vector2 = vector - mapBuf.VCPs[1][l, k];
						float num4 = vector2.x * vector2.x + vector2.y * vector2.y;
						if (num4 < num3)
						{
							num3 = num4;
							b = mapBuf.biomeIndices[1][l, k];
						}
					}
				}
				mapBuf.biomeIndices[2][num, num2] = b;
				num2++;
			}
			num++;
		}
	}

	private void PreCacheVCellGridMicro(GenerationBuffers.MapBuf mapBuf, uint rngSeed)
	{
		float[] array = new float[9];
		byte[] array2 = new byte[9];
		int num = 0;
		for (int i = mapBuf.gridRange[3, 0].x; i <= mapBuf.gridRange[3, 1].x; i++)
		{
			int num2 = 0;
			for (int j = mapBuf.gridRange[3, 0].y; j <= mapBuf.gridRange[3, 1].y; j++)
			{
				mapBuf.drng.SetSeedFromTable((uint)(i * 1234 + j) ^ rngSeed, (uint)(j * 5432 + i) ^ rngSeed);
				Vector2 vector = GenCoordWithVariance(i, j, m_AdvancedParameters.vCellVarianceMicro, mapBuf.drng) * mapBuf.cellScale[3];
				mapBuf.VCPs[3][num, num2] = vector;
				IntVector2 intVector = new IntVector2(Mathf.FloorToInt(vector.x * mapBuf.invCellScale[2]), Mathf.FloorToInt(vector.y * mapBuf.invCellScale[2]));
				int num3 = 0;
				float num4 = float.MaxValue;
				IntVector2 intVector2 = intVector - IntVector2.one - mapBuf.gridRange[2, 0];
				IntVector2 intVector3 = intVector + IntVector2.one - mapBuf.gridRange[2, 0];
				for (int k = intVector2.y; k <= intVector3.y; k++)
				{
					for (int l = intVector2.x; l <= intVector3.x; l++)
					{
						Vector2 vector2 = vector - mapBuf.VCPs[2][l, k];
						float num5 = vector2.x * vector2.x + vector2.y * vector2.y;
						if (num5 < num4)
						{
							num4 = num5;
						}
						array[num3] = num5;
						array2[num3] = mapBuf.biomeIndices[2][l, k];
						num3++;
					}
				}
				float num6 = 0f;
				float num7 = 1f / Mathf.Sqrt(num4);
				for (int m = 0; m < 9; m++)
				{
					float num8 = array[m] - num4;
					float num9 = Mathf.Max(1f - num8 * num7, 0f);
					num6 = (array[m] = num6 + num9);
				}
				mapBuf.drng.SetSeed((uint)i, (uint)j, rngSeed);
				float num10 = mapBuf.drng.One() * num6;
				byte b = byte.MaxValue;
				for (int n = 0; n < 9; n++)
				{
					if (num10 <= array[n])
					{
						b = array2[n];
						break;
					}
				}
				d.Assert(b != byte.MaxValue);
				mapBuf.biomeIndices[3][num, num2] = b;
				num2++;
			}
			num++;
		}
	}

	private void GenBiomeMap(GenerationBuffers.MapBuf mapBuf, MapCell[,] cells)
	{
		if (m_BiomeGroups.Length == 0 || m_BiomeGroups[0].Biomes.Length == 0)
		{
			return;
		}
		uint num = (uint)(-1621601608 ^ mapBuf.seed);
		mapBuf.Initialise(m_BiomeDistributionScaleMacro, 2f, 5f, 9f);
		try
		{
			PreCacheVCellGridMacro(mapBuf, num);
			PreCacheVCellGridMajor(mapBuf, num ^ 0x91697745u);
			PreCacheVCellGridMinor(mapBuf, num ^ 0x2714E131);
			PreCacheVCellGridMicro(mapBuf, num ^ 0x3E32481A);
			for (int i = 0; i < mapBuf.extents.y; i++)
			{
				for (int j = 0; j < mapBuf.extents.x; j++)
				{
					Vector2 worldCoord = (Vector2)(mapBuf.origin + new IntVector2(j, i)) * (float)mapBuf.cellSize;
					mapBuf.ResetPointBuffers();
					CalculateCellBiomeValues(mapBuf, in worldCoord, mapBuf.detail, mapBuf.weights4, m_AdvancedParameters.weightsDSquared, m_AdvancedParameters.weightsISqrt);
					mapBuf.SortPointBuffers();
					if (mapBuf.detail >= 1)
					{
						CalculateCellBiomeValues(mapBuf, in worldCoord, 1, mapBuf.weights4alt, m_AdvancedParameters.altWeightsDSquared, m_AdvancedParameters.altWeightsISqrt, altWeights: true);
					}
					cells[j, i].Init(mapBuf.indices4, mapBuf.weights4, mapBuf.weights4alt);
				}
			}
		}
		catch (IndexOutOfRangeException)
		{
			d.Log("buffer access out of range: try adjusting distribution scales");
		}
	}

	private GenOutput GenBiomeMap_Jobbed(GenerationBuffers.MapBuf mapBuf)
	{
		if (m_BiomeGroups.Length == 0 || m_BiomeGroups[0].Biomes.Length == 0)
		{
			return default(GenOutput);
		}
		if (!Singleton.IsCalledOnMainThread)
		{
			d.LogError("GenBiomeMap_Jobbed can only be called from the main thread!");
			return default(GenOutput);
		}
		uint num = (uint)(-1621601608 ^ mapBuf.seed);
		mapBuf.Initialise(m_BiomeDistributionScaleMacro, 2f, 5f, 9f);
		try
		{
			PreCacheVCellGridMacro(mapBuf, num);
			PreCacheVCellGridMajor(mapBuf, num ^ 0x91697745u);
			PreCacheVCellGridMinor(mapBuf, num ^ 0x2714E131);
			PreCacheVCellGridMicro(mapBuf, num ^ 0x3E32481A);
			int detail = mapBuf.detail;
			bool num2 = detail >= 1;
			NativeArray<Vector2> controlPoints = CreateAndPopulateNativeArray<Vector2>(mapBuf.VCPs, detail);
			NativeArray<Vector2> controlPoints2 = (num2 ? CreateAndPopulateNativeArray<Vector2>(mapBuf.VCPs, 1) : default(NativeArray<Vector2>));
			NativeArray<byte> biomeIndices = CreateAndPopulateNativeArray<byte>(mapBuf.biomeIndices, detail);
			NativeArray<byte> biomeIndices2 = (num2 ? CreateAndPopulateNativeArray<byte>(mapBuf.biomeIndices, 1) : default(NativeArray<byte>));
			int num3 = mapBuf.extents.x * mapBuf.extents.y;
			NativeArray<Byte4> allIndices = new NativeArray<Byte4>(num3, Allocator.TempJob);
			NativeArray<float4> nativeArray = new NativeArray<float4>(num3, Allocator.TempJob);
			NativeArray<float4> nativeArray2 = new NativeArray<float4>(num3, Allocator.TempJob);
			NativeArray<int> nativeArray3 = new NativeArray<int>(GetNumBiomes(), Allocator.TempJob);
			CalcBiomeCellValuesJob jobData = new CalcBiomeCellValuesJob
			{
				origin = mapBuf.origin,
				cellSize = mapBuf.cellSize,
				rowExtents = mapBuf.extents.x,
				invCellScale = mapBuf.invCellScale[detail],
				gridrange = mapBuf.gridRange[detail, 0],
				vcpRowLen = mapBuf.VCPs[detail].GetLength(0),
				controlPoints = controlPoints,
				biomeIndices = biomeIndices,
				allIndices4 = allIndices,
				allWeights4 = nativeArray,
				allWeightsAlt4 = nativeArray2,
				weightsDSquared = m_AdvancedParameters.weightsDSquared,
				weightsISqrt = m_AdvancedParameters.weightsISqrt
			};
			JobHandle jobHandle = IJobParallelForExtensions.Schedule(dependsOn: jobData.Schedule(num3, 16), jobData: new SwapWeightsJob
			{
				allIndices4 = allIndices,
				allWeights4 = nativeArray
			}, arrayLength: num3, innerloopBatchCount: 16);
			if (num2)
			{
				jobData.invCellScale = mapBuf.invCellScale[1];
				jobData.gridrange = mapBuf.gridRange[1, 0];
				jobData.vcpRowLen = mapBuf.VCPs[1].GetLength(0);
				jobData.controlPoints = controlPoints2;
				jobData.biomeIndices = biomeIndices2;
				jobData.allIndices4 = allIndices;
				jobData.altWeightsUsed = true;
				jobData.allWeights4 = nativeArray2;
				jobData.allWeightsAlt4 = nativeArray;
				jobData.weightsDSquared = m_AdvancedParameters.altWeightsDSquared;
				jobData.weightsISqrt = m_AdvancedParameters.altWeightsISqrt;
				jobHandle = jobData.Schedule(num3, 16, jobHandle);
			}
			if (mapBuf.biomesUsed != null)
			{
				jobHandle = new AccumulateBiomeCountJob
				{
					allIndices4 = allIndices,
					outBiomeCounts = nativeArray3
				}.Schedule(jobHandle);
			}
			jobHandle.Complete();
			if (mapBuf.biomesUsed != null)
			{
				mapBuf.biomesUsed.InitFromNative(nativeArray3);
			}
			controlPoints.Dispose();
			controlPoints2.Dispose();
			biomeIndices.Dispose();
			biomeIndices2.Dispose();
			nativeArray3.Dispose();
			return new GenOutput
			{
				jobHandle = jobHandle,
				rowLen = mapBuf.extents.x,
				allIndices4 = allIndices,
				allWeights4 = nativeArray,
				allWeightsAlt4 = nativeArray2
			};
		}
		catch (IndexOutOfRangeException)
		{
			d.Log("buffer access out of range: try adjusting distribution scales");
			return default(GenOutput);
		}
		static NativeArray<T> CreateAndPopulateNativeArray<T>(T[][,] src, int detailLevel) where T : struct
		{
			int length = src[detailLevel].GetLength(0);
			int num4 = length * src[detailLevel].GetLength(1);
			NativeArray<T> result = new NativeArray<T>(num4, Allocator.TempJob);
			for (int i = 0; i < num4; i++)
			{
				int num5 = i % length;
				int num6 = i / length;
				result[i] = src[detailLevel][num5, num6];
			}
			return result;
		}
	}

	private static void CalculateCellBiomeValues(GenerationBuffers.MapBuf mapBuf, in Vector2 worldCoord, int VCPDetail, float[] weights4, bool weightsDSquared, bool weightsISqrt, bool altWeights = false)
	{
		float num = mapBuf.invCellScale[VCPDetail];
		Vector2 vector = worldCoord * num;
		IntVector2 intVector = new IntVector2(Mathf.FloorToInt(vector.x), Mathf.FloorToInt(vector.y)) - mapBuf.gridRange[VCPDetail, 0];
		Vector2[,] array = mapBuf.VCPs[VCPDetail];
		byte[,] array2 = mapBuf.biomeIndices[VCPDetail];
		int num2 = 0;
		float num3 = float.MaxValue;
		for (int i = -1; i <= 1; i++)
		{
			int num4 = intVector.y + i;
			for (int j = -1; j <= 1; j++)
			{
				int num5 = intVector.x + j;
				Vector2 vector2 = vector - array[num5, num4] * num;
				float num6 = (weightsDSquared ? (vector2.x * vector2.x + vector2.y * vector2.y) : Mathf.Sqrt(vector2.x * vector2.x + vector2.y * vector2.y));
				num3 = Mathf.Min(num3, num6);
				mapBuf.distances9[num2] = num6;
				mapBuf.indices9[num2] = array2[num5, num4];
				num2++;
			}
		}
		float num7 = 0f;
		float num8 = 1f / (weightsISqrt ? Mathf.Sqrt(num3) : num3);
		for (int k = 0; k < 9; k++)
		{
			float num9 = mapBuf.distances9[k] - num3;
			float num10 = 1f - num9 * num8;
			if (num10 < 0f)
			{
				continue;
			}
			num7 += num10;
			byte b = mapBuf.indices9[k];
			for (int l = 0; l < 4; l++)
			{
				if (mapBuf.indices4[l] == byte.MaxValue)
				{
					weights4[l] = num10;
					mapBuf.indices4[l] = b;
					if (altWeights)
					{
						mapBuf.weights4[l] = 0f;
					}
					else if (mapBuf.biomesUsed != null)
					{
						mapBuf.biomesUsed.Increment(b);
					}
					break;
				}
				if (mapBuf.indices4[l] == b)
				{
					weights4[l] += num10;
					break;
				}
			}
		}
		float num11 = 1f / num7;
		for (int m = 0; m < 4; m++)
		{
			weights4[m] *= num11;
		}
	}

	private void GenerateHeightMapToBuffers(WorldTile tile, Vector2 tileMin, Vector2 tileSize, int dimension, int seed, float[,] adjacentHeights, float[,] heights)
	{
		lock (tile.BiomeMapData)
		{
			float generatedValue = 0f;
			MapGenerator.PopulateCellDelegate populateCellDelegate = delegate(Vector2 p, float v, Color c)
			{
				generatedValue = v * 0.5f + 0.5f;
			};
			GenerationBuffers genBuffersByThread = GetGenBuffersByThread();
			float num = tileSize.y / (float)dimension;
			float num2 = tileMin.y - num;
			int num3 = -1;
			int num4 = dimension + 1;
			int num5 = -1;
			while (num5 < dimension + 2)
			{
				bool flag = num5 == num3;
				bool flag2 = num5 == num4;
				float num6 = tileMin.x - num;
				int num7 = -1;
				while (num7 < dimension + 2)
				{
					bool flag3 = num7 == num3;
					bool flag4 = num7 == num4;
					if (!(flag || flag2) || !(flag3 || flag4))
					{
						MapCell mapCell = tile.BiomeMapData.cells[num7 + 1, num5 + 1];
						float num8 = 0f;
						for (int num9 = 0; num9 < 4; num9++)
						{
							float num10 = mapCell.WeightAltAvg(num9);
							if (num10 != 0f)
							{
								Biome biome = LookupBiome(mapCell.Index(num9));
								MapGenerator.GenerationContext blendContextLookup = genBuffersByThread.GetBlendContextLookup(biome.HeightMapGenerator, seed);
								Vector2 vector = new Vector2(num6, num2);
								populateCellDelegate(vector, biome.HeightMapGenerator.GeneratePoint(blendContextLookup, vector), Color.black);
								num8 += generatedValue * num10;
							}
						}
						if (flag)
						{
							adjacentHeights[0, num7] = num8;
						}
						else if (flag3)
						{
							adjacentHeights[1, num5] = num8;
						}
						else if (flag2)
						{
							adjacentHeights[2, num7] = num8;
						}
						else if (flag4)
						{
							adjacentHeights[3, num5] = num8;
						}
						else
						{
							heights[num5, num7] = num8;
						}
					}
					num7++;
					num6 += num;
				}
				num5++;
				num2 += num;
			}
			if (tile.BiomeMapData.setPiece != null)
			{
				tile.BiomeMapData.setPiece.ApplyHeightMap(tile.BiomeMapData, adjacentHeights, heights, dimension);
			}
		}
	}

	public void GenerateHeightMap(WorldTile tile, Vector2 tileMin, Vector2 tileSize, int dimension, int seed)
	{
		lock (tile.BiomeMapData)
		{
			if (m_HeightmapResolutionPerCell <= 1)
			{
				tile.BiomeMapData.AllocateHeightBuffer(dimension);
				GenerateHeightMapToBuffers(tile, tileMin, tileSize, dimension, seed, tile.BiomeMapData.heightData.adjacentHeights, tile.BiomeMapData.heightData.heights);
				for (int i = 0; i < tile.BiomeMapData.heightData.heightError.Length; i++)
				{
					tile.BiomeMapData.heightData.heightError[i] = 0f;
				}
				return;
			}
			d.Assert(m_HeightmapResolutionPerCell > 1, "not optimised for resolution 1 (no interp)");
			int num = m_HeightmapResolutionPerCell * dimension;
			tile.BiomeMapData.AllocateHeightBuffer(num);
			GenerationBuffers genBuffersByThread = GetGenBuffersByThread();
			genBuffersByThread.InitHeightIntermediate(dimension);
			GenerateHeightMapToBuffers(tile, tileMin, tileSize, dimension, seed, genBuffersByThread.heightIntermediateAdjacentData, genBuffersByThread.heightIntermediateData);
			Vector4[,] bilinearTable = genBuffersByThread.GetBilinearTable(m_HeightmapResolutionPerCell);
			MapInterpGeneral(num, m_HeightmapResolutionPerCell, genBuffersByThread.heightIntermediateData, tile.BiomeMapData.heightData.heights, bilinearTable);
			MapInterpEdges(num, m_HeightmapResolutionPerCell, genBuffersByThread.heightIntermediateAdjacentData, tile.BiomeMapData.heightData.adjacentHeights, bilinearTable);
			for (int j = 0; j < tile.BiomeMapData.heightData.heightError.Length; j++)
			{
				tile.BiomeMapData.heightData.heightError[j] = 0f;
			}
			int num2 = num / 16 * (num / 16);
			int num3 = 32;
			int num4 = 2;
			for (int num5 = m_HeightmapResolutionPerCell; num5 > 1; num5 /= 2)
			{
				GenerateTesselationErrorTerm(tile.BiomeMapData.heightData.heightError, num2, tile.BiomeMapData.heightData.heights, num, num3, num4);
				num2 += num / num3 * num / num3;
				num3 *= 2;
				num4 *= 2;
			}
			PropagateTesselationError(tile.BiomeMapData.heightData.heightError, 0, num / 16);
		}
	}

	public static void GenerateTesselationErrorTerm(float[] errorOut, int errorIndex, float[,] buffer, int dimension, int boxSize, int step)
	{
		for (int i = 0; i < dimension; i += boxSize)
		{
			for (int j = 0; j < dimension; j += boxSize)
			{
				float num = 0f;
				for (int k = i; k < i + boxSize; k += step)
				{
					for (int l = j; l < j + boxSize; l += step)
					{
						float num2 = buffer[k, l];
						float num3 = buffer[k, l + step];
						float num4 = buffer[k + step, l];
						float num5 = buffer[k + step, l + step];
						float b = Mathf.Abs(num2 + num5 - (num4 + num3)) / 4f;
						num = Mathf.Max(num, b);
					}
				}
				errorOut[errorIndex] = num;
				errorIndex++;
			}
		}
	}

	public static void PropagateTesselationError(float[] errors, int startIndex, int dimension)
	{
		int num = startIndex + dimension * dimension;
		for (int i = 0; i < dimension; i += 2)
		{
			for (int j = 0; j < dimension; j += 2)
			{
				float a = 0f;
				a = Mathf.Max(a, errors[startIndex + j + i * dimension]);
				a = Mathf.Max(a, errors[startIndex + (j + 1) + i * dimension]);
				a = Mathf.Max(a, errors[startIndex + j + (i + 1) * dimension]);
				a = Mathf.Max(a, errors[startIndex + (j + 1) + (i + 1) * dimension]);
				errors[num] = Mathf.Max(errors[num], a);
				num++;
			}
		}
		if (dimension > 2)
		{
			PropagateTesselationError(errors, startIndex + dimension * dimension, dimension / 2);
		}
	}

	private float EstimateSteepness(MapData.HeightBuffer heightData, int i, int j)
	{
		d.Assert(heightData != null, "missing height data");
		int num = heightData.Dimension - 1;
		i *= m_HeightmapResolutionPerCell;
		j *= m_HeightmapResolutionPerCell;
		float a = ((j == 0) ? heightData.adjacentHeights[0, i] : heightData.heights[j - 1, i]);
		float b = ((i == 0) ? heightData.adjacentHeights[1, j] : heightData.heights[j, i - 1]);
		float a2 = ((j == num) ? heightData.adjacentHeights[2, i] : heightData.heights[j + 1, i]);
		float b2 = ((i == num) ? heightData.adjacentHeights[3, j] : heightData.heights[j, i + 1]);
		float num2 = Mathf.Min(Mathf.Min(a, b), Mathf.Min(a2, b2));
		return Mathf.Max(Mathf.Max(a, b), Mathf.Max(a2, b2)) - num2;
	}

	private int AllocateSplatMap(TerrainLayer layer)
	{
		int num = m_SplatMaps.IndexOf(layer);
		if (num == -1)
		{
			num = m_SplatMaps.Count;
			m_SplatMaps.Add(layer);
		}
		return num;
	}

	public void GenerateSplatMaps(WorldTile tile, Vector2 tileMin, Vector2 tileSize, int dimension, int seed)
	{
		lock (tile.BiomeMapData)
		{
			DebugUtil.AssertRelease(tile.BiomeMapData.heightData != null, "tile has no height buffer");
			tile.BiomeMapData.mtDimension = m_MultiTextureResolutionPerCell * dimension;
			TerrainSetPiece setPiece = tile.BiomeMapData.setPiece;
			int num = 4;
			byte[] array = new byte[num];
			int num2 = tile.BiomeMapData.biomesUsed.GetBest(array);
			if (num2 > num)
			{
				d.LogWarning($"Tile {tile.Coord} has weights for {num2} biomes: truncating to {num}");
				num2 = num;
			}
			m_SplatMaps.Clear();
			if (m_SplatMapMapping == null || m_SplatMapMapping.Length != GetNumBiomes())
			{
				m_SplatMapMapping = new IntVector2[GetNumBiomes()];
			}
			for (int i = 0; i < num2; i++)
			{
				Biome biome = LookupBiome(array[i]);
				m_SplatMapMapping[array[i]] = new IntVector2(AllocateSplatMap(biome.MainMaterialLayer), AllocateSplatMap(biome.AltMaterialLayer));
			}
			int splatIndex = -1;
			int splatIndex2 = -1;
			if (setPiece.IsNotNull())
			{
				if ((object)setPiece.terrainLayer1 != null)
				{
					splatIndex = AllocateSplatMap(setPiece.terrainLayer1);
				}
				if ((object)setPiece.terrainLayer2 != null)
				{
					splatIndex2 = AllocateSplatMap(setPiece.terrainLayer2);
				}
			}
			tile.BiomeMapData.AllocateTerrainLayerArray(m_SplatMaps.Count);
			for (int j = 0; j < m_SplatMaps.Count; j++)
			{
				tile.BiomeMapData.terrainLayers[j] = m_SplatMaps[j];
			}
			int count = m_SplatMaps.Count;
			tile.BiomeMapData.AllocateSplatBuffer(tile.BiomeMapData.mtDimension, count);
			GenerationBuffers genBuffersByThread = GetGenBuffersByThread();
			genBuffersByThread.InitTexBlendIntermediate(dimension, count);
			bool flag = WorldGenVersionData < WorldGenVersionData.kLegacy_14775_BiomeDB;
			float num3 = tileSize.y / (float)dimension;
			float num4 = tileMin.y;
			int num5 = 0;
			while (num5 <= dimension)
			{
				float num6 = tileMin.x;
				int num7 = 0;
				while (num7 <= dimension)
				{
					MapCell mapCell = tile.BiomeMapData.cells[num7 + 1, num5 + 1];
					for (int k = 0; k < count; k++)
					{
						genBuffersByThread.texBlendIntermediateData[num5, num7, k] = 0f;
					}
					for (int l = 0; l < 4; l++)
					{
						float num8 = mapCell.Weight(l);
						if (num8 == 0f)
						{
							if (!flag)
							{
								break;
							}
							continue;
						}
						byte b = mapCell.Index(l);
						int x = m_SplatMapMapping[b].x;
						int y = m_SplatMapMapping[b].y;
						Biome biome2 = LookupBiome(b);
						MapGenerator.GenerationContext blendContextLookup = genBuffersByThread.GetBlendContextLookup(biome2.MultiTextureGenerator, seed);
						Vector2 v = new Vector2(num6, num4);
						float num9 = biome2.MultiTextureGenerator.GeneratePoint(blendContextLookup, v) * 0.5f + 0.5f;
						float num10 = biome2.TextureBlendSteepnessRange[1] - biome2.TextureBlendSteepnessRange[0];
						if (num10 > 0f && biome2.TextureBlendSteepnessWeighting > 0f)
						{
							float num11 = EstimateSteepness(tile.BiomeMapData.heightData, num7, num5);
							num11 = 1f - Mathf.Clamp01((num11 - biome2.TextureBlendSteepnessRange[0]) / num10);
							num9 = Mathf.Lerp(num9, num11, biome2.TextureBlendSteepnessWeighting);
						}
						genBuffersByThread.texBlendIntermediateData[num5, num7, x] += num9 * num8;
						genBuffersByThread.texBlendIntermediateData[num5, num7, y] += (1f - num9) * num8;
					}
					num7++;
					num6 += num3;
				}
				num5++;
				num4 += num3;
			}
			if (setPiece != null)
			{
				setPiece.MergeSplatMap(tile.BiomeMapData, splatIndex, splatIndex2, genBuffersByThread.texBlendIntermediateData, dimension);
			}
			int multiTextureResolutionPerCell = m_MultiTextureResolutionPerCell;
			Vector4[,] bilinearTable = genBuffersByThread.GetBilinearTable(multiTextureResolutionPerCell);
			int m;
			switch (count)
			{
			case 0:
			case 1:
				m = 0;
				break;
			case 2:
			case 3:
				SplatInterp2(tile.BiomeMapData.mtDimension, multiTextureResolutionPerCell, genBuffersByThread.texBlendIntermediateData, tile.BiomeMapData.splatData, bilinearTable);
				m = 2;
				break;
			case 4:
			case 5:
				SplatInterp4(tile.BiomeMapData.mtDimension, multiTextureResolutionPerCell, genBuffersByThread.texBlendIntermediateData, tile.BiomeMapData.splatData, bilinearTable);
				m = 4;
				break;
			case 6:
			case 7:
				SplatInterp6(tile.BiomeMapData.mtDimension, multiTextureResolutionPerCell, genBuffersByThread.texBlendIntermediateData, tile.BiomeMapData.splatData, bilinearTable);
				m = 6;
				break;
			case 8:
			case 9:
				SplatInterp8(tile.BiomeMapData.mtDimension, multiTextureResolutionPerCell, genBuffersByThread.texBlendIntermediateData, tile.BiomeMapData.splatData, bilinearTable);
				m = 8;
				break;
			default:
				SplatInterp10(tile.BiomeMapData.mtDimension, multiTextureResolutionPerCell, genBuffersByThread.texBlendIntermediateData, tile.BiomeMapData.splatData, bilinearTable);
				m = 10;
				break;
			}
			for (; m < count; m++)
			{
				SplatInterpSingle(tile.BiomeMapData.mtDimension, multiTextureResolutionPerCell, genBuffersByThread.texBlendIntermediateData, tile.BiomeMapData.splatData, bilinearTable, m);
			}
			for (int n = 0; n < num2; n++)
			{
				m_SplatMapMapping[array[n]] = IntVector2.zero;
			}
		}
	}

	public void GenerateSceneryMap(WorldTile tile, Vector2 tileMin, Vector2 tileSize, int dimension, int seed)
	{
		lock (tile.BiomeMapData)
		{
			GenerationBuffers genBuffersByThread = GetGenBuffersByThread();
			tile.BiomeMapData.AllocateSceneryBuffer(dimension);
			float num = tileSize.y / (float)dimension;
			GenerateSceneryCells(genBuffersByThread, tile.BiomeMapData, tileMin / num, (uint)dimension, num, seed, null);
		}
	}

	private void GenerateSceneryCells(GenerationBuffers buffers, MapData mapData, IntVector2 cellOrigin, uint dimension, float cellScale, int seed, Func<int, int, MapCell?> GetMapCell)
	{
		float generatedValue = 0f;
		MapGenerator.PopulateCellDelegate populateCellDelegate = delegate(Vector2 p, float v, Color c)
		{
			generatedValue = v;
		};
		uint num = (uint)(-1621601608 ^ seed);
		MapCell? mapCell = null;
		for (int num2 = 0; num2 < dimension; num2++)
		{
			for (int num3 = 0; num3 < dimension; num3++)
			{
				IntVector2 intVector = new IntVector2(num3, num2) + cellOrigin;
				Vector2 vector = (Vector2)intVector * cellScale;
				int num4 = seed;
				if (mapData != null)
				{
					mapCell = mapData.cells[num3 + 1, num2 + 1];
					mapData.sceneryPlacement[num2, num3].Clear();
				}
				else if (GetMapCell != null)
				{
					mapCell = GetMapCell(num3, num2);
				}
				buffers.mapBuffer.drng.SetSeed(num ^ (uint)((intVector.y << 16) + intVector.x));
				Biome biome = null;
				if (mapData != null && mapData.setPiece.IsNotNull() && mapData.setPiece.OverrideScenerySpawn(mapData, num3, num2, ref mapData.sceneryPlacement[num2, num3]))
				{
					biome = mapData.setPiece.SuppressedSceneryBiome;
					if (mapData.setPiece.OverwriteSceneryGenParams)
					{
						intVector = new IntVector2(num3, num2) + mapData.setPieceOffset + (IntVector2)mapData.setPiece.SceneryGenCellOffset;
						vector = (Vector2)intVector * cellScale;
						num4 = mapData.setPiece.SceneryGenSeed;
						uint num5 = (uint)(-1621601608 ^ num4);
						buffers.mapBuffer.drng.SetSeed(num5 ^ (uint)((intVector.y << 16) + intVector.x));
					}
				}
				else
				{
					float num6 = buffers.mapBuffer.drng.One();
					if (WorldGenVersionData < WorldGenVersionData.kLegacy_14775_BiomeDB)
					{
						float num7 = 0f;
						for (int num8 = 0; num8 < 4; num8++)
						{
							num7 += mapCell.Value.Weight(num8);
							if (num6 <= num7)
							{
								biome = LookupBiome(mapCell.Value.Index(num8));
								break;
							}
						}
					}
					else if (num6 <= mapCell.Value.Weight(0))
					{
						biome = LookupBiome(mapCell.Value.Index(0));
					}
				}
				Biome.DetailLayer detailLayer = null;
				if (biome != null)
				{
					for (int num9 = 0; num9 < biome.DetailLayers.Length; num9++)
					{
						MapGenerator.GenerationContext blendContextLookup = buffers.GetBlendContextLookup(biome.DetailLayers[num9].generator, num4);
						populateCellDelegate(vector, biome.DetailLayers[num9].generator.GeneratePoint(blendContextLookup, vector), Color.black);
						if (generatedValue > biome.DetailLayers[num9].generator.CutoffThreshold)
						{
							detailLayer = biome.DetailLayers[num9];
						}
					}
				}
				PrefabGroupStackItem prefabGroupStackItem = PrefabGroupStackItem.Null;
				if (detailLayer != null && detailLayer.distributor != null)
				{
					float num10 = buffers.mapBuffer.drng.One();
					s_PrefabGroupStackWorking.Clear();
					s_PrefabGroupStackWorking.Push(new PrefabGroupStackItem(detailLayer.distributor.basic));
					int num11 = -1;
					if (num10 > detailLayer.distributor.nonBasicThreshold && detailLayer.distributor.variants != null && detailLayer.distributor.variants.Length != 0)
					{
						num11 = SelectVariant(num4, detailLayer.distributor, vector);
						if (num11 != -1)
						{
							s_PrefabGroupStackWorking.Push(new PrefabGroupStackItem(detailLayer.distributor.variants[num11]));
						}
					}
					float num12 = buffers.mapBuffer.drng.One();
					float num13 = 0f;
					Biome.SceneryDistributor.UpgradeRule[] upgradeRules = detailLayer.distributor.upgradeRules;
					for (int num14 = 0; num14 < upgradeRules.Length; num14++)
					{
						Biome.SceneryDistributor.UpgradeRule upgradeRule = upgradeRules[num14];
						num13 += upgradeRule.upgradeChance;
						if (num12 < num13)
						{
							int num15 = (upgradeRule.randomPrefab ? (-1) : (num11 + 1));
							d.Assert(num15 < upgradeRule.upgrade.terrainObject.Length);
							s_PrefabGroupStackWorking.Push(new PrefabGroupStackItem(upgradeRule.upgrade, num15));
							break;
						}
					}
					prefabGroupStackItem = ((mapData == null) ? s_PrefabGroupStackWorking.Pop() : GetBestPrefabGroup(s_PrefabGroupStackWorking, buffers.mapBuffer.drng));
				}
				if (prefabGroupStackItem.IsNull || mapData == null)
				{
					continue;
				}
				float scale = 1f;
				bool flag = true;
				if (detailLayer.distributor.spawnModifiers != null)
				{
					Biome.SceneryDistributor.SpawnModifierParams[] spawnModifiers = detailLayer.distributor.spawnModifiers;
					for (int num14 = 0; num14 < spawnModifiers.Length; num14++)
					{
						Biome.SceneryDistributor.SpawnModifierParams spawnModifierParams = spawnModifiers[num14];
						switch (spawnModifierParams.type)
						{
						case Biome.SceneryDistributor.SpawnModifier.ScaleByGenerator:
						{
							MapGenerator.GenerationContext blendContextLookup2 = buffers.GetBlendContextLookup(spawnModifierParams.generator, num4);
							float num17 = (spawnModifierParams.generator.GeneratePoint(blendContextLookup2, vector) + 1f) * 0.5f;
							RangeFloat range = spawnModifierParams.range;
							float min = range.min;
							range = spawnModifierParams.range;
							scale = min + num17 * range.extent;
							break;
						}
						case Biome.SceneryDistributor.SpawnModifier.CullBySteepness:
						{
							float num16 = EstimateSteepness(mapData.heightData, num3, num2) * 100f;
							RangeFloat range = spawnModifierParams.range;
							if (num16 > range.max)
							{
								flag = false;
							}
							break;
						}
						}
					}
				}
				if (flag)
				{
					ref SpawnDetail reference = ref mapData.sceneryPlacement[num2, num3];
					reference.scale = scale;
					if (detailLayer.distributor.decoration != null && detailLayer.distributor.decoration.terrainObject.Length != 0)
					{
						TerrainObject[] terrainObject = detailLayer.distributor.decoration.terrainObject;
						float num18 = buffers.mapBuffer.drng.One();
						reference.Add(terrainObject[(int)(num18 * (float)terrainObject.Length * 0.999999f)]);
					}
					float num19 = buffers.mapBuffer.drng.One();
					int num20 = ((prefabGroupStackItem.overrideIndex != -1) ? prefabGroupStackItem.overrideIndex : ((int)(num19 * (float)prefabGroupStackItem.group.terrainObject.Length * 0.999999f)));
					reference.Add(prefabGroupStackItem.group.terrainObject[num20]);
				}
			}
		}
	}

	private static int SelectVariant(int seed, Biome.SceneryDistributor distrib, Vector2 coord)
	{
		GenerationBuffers genBuffersByThread = GetGenBuffersByThread();
		genBuffersByThread.InitIndexLookup(null);
		distrib.layer0XForm = new MapGenerator.LayerXForm(distrib.layer0Translation, distrib.layer0Rotation, distrib.layer0Scale, seed, WorldGenVersionData.kMax);
		distrib.layer1XForm = new MapGenerator.LayerXForm(distrib.layer1Translation, distrib.layer1Rotation, distrib.layer1Scale, seed, WorldGenVersionData.kMax);
		genBuffersByThread.currentDistrib = distrib;
		int winner = -1;
		MapGenerator.GenerateOnePoint(distrib.layer1XForm, coord, delegate(IntVector2 v, Vector2 c)
		{
			VoronoiSceneryDistribution(c, distrib, ref winner);
		});
		return winner;
	}

	private PrefabGroupStackItem GetBestPrefabGroup(Stack<PrefabGroupStackItem> groupStack, DRNG drng)
	{
		while (groupStack.Count != 0)
		{
			PrefabGroupStackItem result = groupStack.Pop();
			if (!GetDistributionWeightsDict().TryGetValue(result.group.weightingTagHash, out var value) || drng.One() <= value.calculatedMultiplier)
			{
				return result;
			}
		}
		return PrefabGroupStackItem.Null;
	}

	public void RecalculateDistributionWeightMultipliers(int areaSize, float cellScale, Action<float> updateProgressFn = null)
	{
		GenerationBuffers buffers = GetGenBuffersByThread();
		buffers.ClearBlendContextLookup();
		InitBiomeSwapIndex(0);
		layer0XForm = new MapGenerator.LayerXForm(layer0Translation, layer0Rotation, layer0Scale);
		layer1XForm = new MapGenerator.LayerXForm(layer1Translation, layer1Rotation, layer1Scale);
		CompoundExpression.ElementTransform baseMat = layer1XForm.ToMatrix();
		s_TagCounters = new Dictionary<int, WeightingTagCounter>();
		DistributionWeight[] sceneryDistributionWeights = m_SceneryDistributionWeights;
		foreach (DistributionWeight distributionWeight in sceneryDistributionWeights)
		{
			s_TagCounters.Add(distributionWeight.tag.GetHashCode(), new WeightingTagCounter
			{
				distWeight = distributionWeight,
				count = 0
			});
		}
		int cellCounter = 0;
		int totalCells = areaSize * areaSize;
		int progressUpdateInterval = totalCells / 100;
		MapData dummyMap = new MapData();
		Func<int, int, MapCell?> getMapCell = delegate(int num3, int j)
		{
			buffers.InitIndexLookup(this);
			MapCell mapCell = default(MapCell);
			VoronoiBiomeMapDetail(baseMat * new Vector2(num3, j) * cellScale, dummyMap, ref mapCell);
			if (updateProgressFn != null && ++cellCounter % progressUpdateInterval == 0)
			{
				updateProgressFn((float)cellCounter / (float)totalCells);
			}
			return mapCell;
		};
		GenerateSceneryCells(buffers, null, IntVector2.zero, (uint)areaSize, cellScale, 0, getMapCell);
		string text = "counted:\n";
		float num = float.MaxValue;
		foreach (WeightingTagCounter value in s_TagCounters.Values)
		{
			float num2 = (float)value.count / value.distWeight.weight;
			if (!value.distWeight.allowGreaterThanOne && num2 < num)
			{
				num = num2;
			}
			text += $"{value.distWeight.tag} {value.count}   ";
		}
		d.Log(text);
		foreach (WeightingTagCounter value2 in s_TagCounters.Values)
		{
			value2.distWeight.calculatedMultiplier = num / ((float)value2.count / value2.distWeight.weight);
		}
	}

	public static void RenderVariants(int seed, Vector2 tileMin, Vector2 tileSize, float step, Biome.SceneryDistributor distrib, Action<Color> doPixel)
	{
		GenerationBuffers genBuffersByThread = GetGenBuffersByThread();
		genBuffersByThread.InitIndexLookup(null);
		distrib.layer0XForm = new MapGenerator.LayerXForm(distrib.layer0Translation, distrib.layer0Rotation, distrib.layer0Scale, seed, WorldGenVersionData.kMax);
		distrib.layer1XForm = new MapGenerator.LayerXForm(distrib.layer1Translation, distrib.layer1Rotation, distrib.layer1Scale, seed, WorldGenVersionData.kMax);
		genBuffersByThread.currentDistrib = distrib;
		MapGenerator.GenerateMapAbstract(distrib.layer1XForm, tileMin, tileSize, step, delegate(IntVector2 v, Vector2 c)
		{
			int variantChosen = -1;
			VoronoiSceneryDistribution(c, distrib, ref variantChosen);
			float num = (float)variantChosen / (float)distrib.variants.Length;
			doPixel(new Color(num, num, num));
		});
	}

	private static byte VoronoiVariantRegions(Vector2 p)
	{
		GenerationBuffers genBuffersByThread = GetGenBuffersByThread();
		MapFuncVoronoi.GetPoints(p, genBuffersByThread.currentDistrib.layer0DistMethod, genBuffersByThread.voronoiPointsRegion);
		return (byte)(Mathf.Abs(genBuffersByThread.voronoiPointsRegion[0].GetHashCode() / 2) % genBuffersByThread.currentDistrib.variants.Length);
	}

	private static void StoreVariantIndex(IntVector2 ii, Vector2 cc)
	{
		GetGenBuffersByThread().SetIndexFromRef(VoronoiVariantRegions(cc));
	}

	private static void VoronoiSceneryDistribution(Vector2 p, Biome.SceneryDistributor distrib, ref int variantChosen)
	{
		GenerationBuffers genBuffersByThread = GetGenBuffersByThread();
		int num = distrib.variants.Length;
		float[] array = new float[6];
		byte[] array2 = new byte[6];
		MapFuncVoronoi.GetWeightsWithPointsBanded(p, distrib.layer1DistMethod, distrib.bandTolerance, array, genBuffersByThread.voronoiPoints);
		for (byte b = 0; b < num; b++)
		{
			if (array[b] == 0f)
			{
				array2[b] = byte.MaxValue;
			}
			else
			{
				int num2 = genBuffersByThread.voronoiPoints[b].GetHashCode() >>> 1;
				if (!genBuffersByThread.voronoiPointLookup.TryGetValue(num2, out array2[b]))
				{
					genBuffersByThread.SetIndexRef(array2, b);
					if (distrib.enableRegions)
					{
						MapGenerator.GenerateOnePoint(distrib.layer0XForm, genBuffersByThread.voronoiPoints[b], StoreVariantIndex);
					}
					else
					{
						array2[b] = (byte)(num2 % num);
					}
					genBuffersByThread.voronoiPointLookup[num2] = array2[b];
				}
				for (int num3 = b - 1; num3 >= 0; num3--)
				{
					if (array2[num3] != byte.MaxValue && array2[num3] == array2[b])
					{
						array[num3] += array[b];
						array[b] = 0f;
						array2[b] = byte.MaxValue;
						break;
					}
				}
			}
		}
		float num4 = 0f;
		for (int i = 0; i < num; i++)
		{
			if (array[i] > num4)
			{
				num4 = array[i];
				variantChosen = array2[i];
			}
		}
	}

	public static Vector4[,] GenerateBilinearTable(int upScale)
	{
		Vector4[,] array = new Vector4[upScale, upScale];
		for (int i = 0; i < upScale; i++)
		{
			for (int j = 0; j < upScale; j++)
			{
				array[i, j] = new Vector4
				{
					w = (float)(upScale - i) / (float)upScale * (float)(upScale - j) / (float)upScale,
					x = (float)i / (float)upScale * (float)(upScale - j) / (float)upScale,
					y = (float)(upScale - i) / (float)upScale * (float)j / (float)upScale,
					z = (float)i / (float)upScale * (float)j / (float)upScale
				};
			}
		}
		return array;
	}

	public static void MapInterpGeneral(int dimension, int upScale, float[,] inData, float[,] outData, Vector4[,] table)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		int num5 = dimension / upScale;
		d.Assert(num5 * upScale == dimension);
		for (int i = 0; i < num5; i++)
		{
			for (int j = 0; j < num5; j++)
			{
				num = inData[i, j];
				num3 = inData[i + 1, j];
				num2 = inData[i, j + 1];
				num4 = inData[i + 1, j + 1];
				int num6 = i * upScale;
				int num7 = 0;
				while (num7 < upScale)
				{
					int num8 = j * upScale;
					int num9 = 0;
					while (num9 < upScale)
					{
						Vector4 vector = table[num7, num9];
						outData[num6, num8] = num * vector.w + num3 * vector.x + num2 * vector.y + num4 * vector.z;
						num9++;
						num8++;
					}
					num7++;
					num6++;
				}
			}
		}
		int num10 = num5 * upScale;
		for (int k = 0; k < num5; k++)
		{
			num = inData[k, num5];
			num3 = inData[k + 1, num5];
			int num11 = k * upScale;
			int num12 = 0;
			while (num12 < upScale)
			{
				outData[num11, num10] = num * table[num12, 0].w + num3 * table[num12, 0].x;
				num12++;
				num11++;
			}
		}
		for (int l = 0; l < num5; l++)
		{
			num = inData[num5, l];
			num2 = inData[num5, l + 1];
			int num13 = l * upScale;
			int num14 = 0;
			while (num14 < upScale)
			{
				outData[num10, num13] = num * table[0, num14].w + num2 * table[0, num14].y;
				num14++;
				num13++;
			}
		}
		outData[num10, num10] = inData[num5, num5];
	}

	private static void MapInterpEdges(int dimension, int upScale, float[,] inData, float[,] outData, Vector4[,] table)
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = dimension / upScale;
		d.Assert(num3 * upScale == dimension);
		d.Assert(inData.GetLength(0) == outData.GetLength(0));
		int length = inData.GetLength(0);
		for (int i = 0; i < length; i++)
		{
			for (int j = 0; j < num3; j++)
			{
				num = inData[i, j];
				num2 = inData[i, j + 1];
				int num4 = j * upScale;
				int num5 = 0;
				while (num5 < upScale)
				{
					Vector4 vector = table[0, num5];
					outData[i, num4] = num * vector.w + num2 * vector.y;
					num5++;
					num4++;
				}
			}
			outData[i, num3 * upScale] = inData[i, num3];
		}
	}

	public static void SplatInterpSingle(int dimension, int upScale, float[,,] inData, float[,,] outData, Vector4[,] table, int splatIndex)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		int num5 = dimension / upScale;
		d.Assert(num5 * upScale == dimension);
		for (int i = 0; i < num5; i++)
		{
			for (int j = 0; j < num5; j++)
			{
				num = inData[i, j, splatIndex];
				num3 = inData[i + 1, j, splatIndex];
				num2 = inData[i, j + 1, splatIndex];
				num4 = inData[i + 1, j + 1, splatIndex];
				int num6 = i * upScale;
				int num7 = 0;
				while (num7 < upScale)
				{
					int num8 = j * upScale;
					int num9 = 0;
					while (num9 < upScale)
					{
						Vector4 vector = table[num7, num9];
						outData[num6, num8, splatIndex] = num * vector.w + num3 * vector.x + num2 * vector.y + num4 * vector.z;
						num9++;
						num8++;
					}
					num7++;
					num6++;
				}
			}
		}
	}

	private static void SplatInterp2(int dimension, int upScale, float[,,] inData, float[,,] outData, Vector4[,] table)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		int num9 = dimension / upScale;
		d.Assert(num9 * upScale == dimension);
		for (int i = 0; i < num9; i++)
		{
			for (int j = 0; j < num9; j++)
			{
				num = inData[i, j, 0];
				num5 = inData[i, j, 1];
				num3 = inData[i + 1, j, 0];
				num7 = inData[i + 1, j, 1];
				num2 = inData[i, j + 1, 0];
				num6 = inData[i, j + 1, 1];
				num4 = inData[i + 1, j + 1, 0];
				num8 = inData[i + 1, j + 1, 1];
				int num10 = i * upScale;
				int num11 = 0;
				while (num11 < upScale)
				{
					int num12 = j * upScale;
					int num13 = 0;
					while (num13 < upScale)
					{
						Vector4 vector = table[num11, num13];
						outData[num10, num12, 0] = num * vector.w + num3 * vector.x + num2 * vector.y + num4 * vector.z;
						outData[num10, num12, 1] = num5 * vector.w + num7 * vector.x + num6 * vector.y + num8 * vector.z;
						num13++;
						num12++;
					}
					num11++;
					num10++;
				}
			}
		}
	}

	private static void SplatInterp4(int dimension, int upScale, float[,,] inData, float[,,] outData, Vector4[,] table)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		float num10 = 0f;
		float num11 = 0f;
		float num12 = 0f;
		float num13 = 0f;
		float num14 = 0f;
		float num15 = 0f;
		float num16 = 0f;
		int num17 = dimension / upScale;
		d.Assert(num17 * upScale == dimension);
		for (int i = 0; i < num17; i++)
		{
			for (int j = 0; j < num17; j++)
			{
				num = inData[i, j, 0];
				num5 = inData[i, j, 1];
				num9 = inData[i, j, 2];
				num13 = inData[i, j, 3];
				num3 = inData[i + 1, j, 0];
				num7 = inData[i + 1, j, 1];
				num11 = inData[i + 1, j, 2];
				num15 = inData[i + 1, j, 3];
				num2 = inData[i, j + 1, 0];
				num6 = inData[i, j + 1, 1];
				num10 = inData[i, j + 1, 2];
				num14 = inData[i, j + 1, 3];
				num4 = inData[i + 1, j + 1, 0];
				num8 = inData[i + 1, j + 1, 1];
				num12 = inData[i + 1, j + 1, 2];
				num16 = inData[i + 1, j + 1, 3];
				int num18 = i * upScale;
				int num19 = 0;
				while (num19 < upScale)
				{
					int num20 = j * upScale;
					int num21 = 0;
					while (num21 < upScale)
					{
						Vector4 vector = table[num19, num21];
						outData[num18, num20, 0] = num * vector.w + num3 * vector.x + num2 * vector.y + num4 * vector.z;
						outData[num18, num20, 1] = num5 * vector.w + num7 * vector.x + num6 * vector.y + num8 * vector.z;
						outData[num18, num20, 2] = num9 * vector.w + num11 * vector.x + num10 * vector.y + num12 * vector.z;
						outData[num18, num20, 3] = num13 * vector.w + num15 * vector.x + num14 * vector.y + num16 * vector.z;
						num21++;
						num20++;
					}
					num19++;
					num18++;
				}
			}
		}
	}

	private static void SplatInterp6(int dimension, int upScale, float[,,] inData, float[,,] outData, Vector4[,] table)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		float num10 = 0f;
		float num11 = 0f;
		float num12 = 0f;
		float num13 = 0f;
		float num14 = 0f;
		float num15 = 0f;
		float num16 = 0f;
		float num17 = 0f;
		float num18 = 0f;
		float num19 = 0f;
		float num20 = 0f;
		float num21 = 0f;
		float num22 = 0f;
		float num23 = 0f;
		float num24 = 0f;
		int num25 = dimension / upScale;
		d.Assert(num25 * upScale == dimension);
		for (int i = 0; i < num25; i++)
		{
			for (int j = 0; j < num25; j++)
			{
				num = inData[i, j, 0];
				num5 = inData[i, j, 1];
				num9 = inData[i, j, 2];
				num13 = inData[i, j, 3];
				num17 = inData[i, j, 4];
				num21 = inData[i, j, 5];
				num3 = inData[i + 1, j, 0];
				num7 = inData[i + 1, j, 1];
				num11 = inData[i + 1, j, 2];
				num15 = inData[i + 1, j, 3];
				num19 = inData[i + 1, j, 4];
				num23 = inData[i + 1, j, 5];
				num2 = inData[i, j + 1, 0];
				num6 = inData[i, j + 1, 1];
				num10 = inData[i, j + 1, 2];
				num14 = inData[i, j + 1, 3];
				num18 = inData[i, j + 1, 4];
				num22 = inData[i, j + 1, 5];
				num4 = inData[i + 1, j + 1, 0];
				num8 = inData[i + 1, j + 1, 1];
				num12 = inData[i + 1, j + 1, 2];
				num16 = inData[i + 1, j + 1, 3];
				num20 = inData[i + 1, j + 1, 4];
				num24 = inData[i + 1, j + 1, 5];
				int num26 = i * upScale;
				int num27 = 0;
				while (num27 < upScale)
				{
					int num28 = j * upScale;
					int num29 = 0;
					while (num29 < upScale)
					{
						Vector4 vector = table[num27, num29];
						outData[num26, num28, 0] = num * vector.w + num3 * vector.x + num2 * vector.y + num4 * vector.z;
						outData[num26, num28, 1] = num5 * vector.w + num7 * vector.x + num6 * vector.y + num8 * vector.z;
						outData[num26, num28, 2] = num9 * vector.w + num11 * vector.x + num10 * vector.y + num12 * vector.z;
						outData[num26, num28, 3] = num13 * vector.w + num15 * vector.x + num14 * vector.y + num16 * vector.z;
						outData[num26, num28, 4] = num17 * vector.w + num19 * vector.x + num18 * vector.y + num20 * vector.z;
						outData[num26, num28, 5] = num21 * vector.w + num23 * vector.x + num22 * vector.y + num24 * vector.z;
						num29++;
						num28++;
					}
					num27++;
					num26++;
				}
			}
		}
	}

	private static void SplatInterp8(int dimension, int upScale, float[,,] inData, float[,,] outData, Vector4[,] table)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		float num10 = 0f;
		float num11 = 0f;
		float num12 = 0f;
		float num13 = 0f;
		float num14 = 0f;
		float num15 = 0f;
		float num16 = 0f;
		float num17 = 0f;
		float num18 = 0f;
		float num19 = 0f;
		float num20 = 0f;
		float num21 = 0f;
		float num22 = 0f;
		float num23 = 0f;
		float num24 = 0f;
		float num25 = 0f;
		float num26 = 0f;
		float num27 = 0f;
		float num28 = 0f;
		float num29 = 0f;
		float num30 = 0f;
		float num31 = 0f;
		float num32 = 0f;
		int num33 = dimension / upScale;
		d.Assert(num33 * upScale == dimension);
		for (int i = 0; i < num33; i++)
		{
			for (int j = 0; j < num33; j++)
			{
				num = inData[i, j, 0];
				num5 = inData[i, j, 1];
				num9 = inData[i, j, 2];
				num13 = inData[i, j, 3];
				num17 = inData[i, j, 4];
				num21 = inData[i, j, 5];
				num25 = inData[i, j, 6];
				num29 = inData[i, j, 7];
				num3 = inData[i + 1, j, 0];
				num7 = inData[i + 1, j, 1];
				num11 = inData[i + 1, j, 2];
				num15 = inData[i + 1, j, 3];
				num19 = inData[i + 1, j, 4];
				num23 = inData[i + 1, j, 5];
				num27 = inData[i + 1, j, 6];
				num31 = inData[i + 1, j, 7];
				num2 = inData[i, j + 1, 0];
				num6 = inData[i, j + 1, 1];
				num10 = inData[i, j + 1, 2];
				num14 = inData[i, j + 1, 3];
				num18 = inData[i, j + 1, 4];
				num22 = inData[i, j + 1, 5];
				num26 = inData[i, j + 1, 6];
				num30 = inData[i, j + 1, 7];
				num4 = inData[i + 1, j + 1, 0];
				num8 = inData[i + 1, j + 1, 1];
				num12 = inData[i + 1, j + 1, 2];
				num16 = inData[i + 1, j + 1, 3];
				num20 = inData[i + 1, j + 1, 4];
				num24 = inData[i + 1, j + 1, 5];
				num28 = inData[i + 1, j + 1, 6];
				num32 = inData[i + 1, j + 1, 7];
				int num34 = i * upScale;
				int num35 = 0;
				while (num35 < upScale)
				{
					int num36 = j * upScale;
					int num37 = 0;
					while (num37 < upScale)
					{
						Vector4 vector = table[num35, num37];
						outData[num34, num36, 0] = num * vector.w + num3 * vector.x + num2 * vector.y + num4 * vector.z;
						outData[num34, num36, 1] = num5 * vector.w + num7 * vector.x + num6 * vector.y + num8 * vector.z;
						outData[num34, num36, 2] = num9 * vector.w + num11 * vector.x + num10 * vector.y + num12 * vector.z;
						outData[num34, num36, 3] = num13 * vector.w + num15 * vector.x + num14 * vector.y + num16 * vector.z;
						outData[num34, num36, 4] = num17 * vector.w + num19 * vector.x + num18 * vector.y + num20 * vector.z;
						outData[num34, num36, 5] = num21 * vector.w + num23 * vector.x + num22 * vector.y + num24 * vector.z;
						outData[num34, num36, 6] = num25 * vector.w + num27 * vector.x + num26 * vector.y + num28 * vector.z;
						outData[num34, num36, 7] = num29 * vector.w + num31 * vector.x + num30 * vector.y + num32 * vector.z;
						num37++;
						num36++;
					}
					num35++;
					num34++;
				}
			}
		}
	}

	private static void SplatInterp10(int dimension, int upScale, float[,,] inData, float[,,] outData, Vector4[,] table)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		float num7 = 0f;
		float num8 = 0f;
		float num9 = 0f;
		float num10 = 0f;
		float num11 = 0f;
		float num12 = 0f;
		float num13 = 0f;
		float num14 = 0f;
		float num15 = 0f;
		float num16 = 0f;
		float num17 = 0f;
		float num18 = 0f;
		float num19 = 0f;
		float num20 = 0f;
		float num21 = 0f;
		float num22 = 0f;
		float num23 = 0f;
		float num24 = 0f;
		float num25 = 0f;
		float num26 = 0f;
		float num27 = 0f;
		float num28 = 0f;
		float num29 = 0f;
		float num30 = 0f;
		float num31 = 0f;
		float num32 = 0f;
		float num33 = 0f;
		float num34 = 0f;
		float num35 = 0f;
		float num36 = 0f;
		float num37 = 0f;
		float num38 = 0f;
		float num39 = 0f;
		float num40 = 0f;
		int num41 = dimension / upScale;
		d.Assert(num41 * upScale == dimension);
		for (int i = 0; i < num41; i++)
		{
			for (int j = 0; j < num41; j++)
			{
				num = inData[i, j, 0];
				num5 = inData[i, j, 1];
				num9 = inData[i, j, 2];
				num13 = inData[i, j, 3];
				num17 = inData[i, j, 4];
				num21 = inData[i, j, 5];
				num25 = inData[i, j, 6];
				num29 = inData[i, j, 7];
				num33 = inData[i, j, 8];
				num37 = inData[i, j, 9];
				num3 = inData[i + 1, j, 0];
				num7 = inData[i + 1, j, 1];
				num11 = inData[i + 1, j, 2];
				num15 = inData[i + 1, j, 3];
				num19 = inData[i + 1, j, 4];
				num23 = inData[i + 1, j, 5];
				num27 = inData[i + 1, j, 6];
				num31 = inData[i + 1, j, 7];
				num35 = inData[i + 1, j, 8];
				num39 = inData[i + 1, j, 9];
				num2 = inData[i, j + 1, 0];
				num6 = inData[i, j + 1, 1];
				num10 = inData[i, j + 1, 2];
				num14 = inData[i, j + 1, 3];
				num18 = inData[i, j + 1, 4];
				num22 = inData[i, j + 1, 5];
				num26 = inData[i, j + 1, 6];
				num30 = inData[i, j + 1, 7];
				num34 = inData[i, j + 1, 8];
				num38 = inData[i, j + 1, 9];
				num4 = inData[i + 1, j + 1, 0];
				num8 = inData[i + 1, j + 1, 1];
				num12 = inData[i + 1, j + 1, 2];
				num16 = inData[i + 1, j + 1, 3];
				num20 = inData[i + 1, j + 1, 4];
				num24 = inData[i + 1, j + 1, 5];
				num28 = inData[i + 1, j + 1, 6];
				num32 = inData[i + 1, j + 1, 7];
				num36 = inData[i + 1, j + 1, 8];
				num40 = inData[i + 1, j + 1, 9];
				int num42 = i * upScale;
				int num43 = 0;
				while (num43 < upScale)
				{
					int num44 = j * upScale;
					int num45 = 0;
					while (num45 < upScale)
					{
						Vector4 vector = table[num43, num45];
						outData[num42, num44, 0] = num * vector.w + num3 * vector.x + num2 * vector.y + num4 * vector.z;
						outData[num42, num44, 1] = num5 * vector.w + num7 * vector.x + num6 * vector.y + num8 * vector.z;
						outData[num42, num44, 2] = num9 * vector.w + num11 * vector.x + num10 * vector.y + num12 * vector.z;
						outData[num42, num44, 3] = num13 * vector.w + num15 * vector.x + num14 * vector.y + num16 * vector.z;
						outData[num42, num44, 4] = num17 * vector.w + num19 * vector.x + num18 * vector.y + num20 * vector.z;
						outData[num42, num44, 5] = num21 * vector.w + num23 * vector.x + num22 * vector.y + num24 * vector.z;
						outData[num42, num44, 6] = num25 * vector.w + num27 * vector.x + num26 * vector.y + num28 * vector.z;
						outData[num42, num44, 7] = num29 * vector.w + num31 * vector.x + num30 * vector.y + num32 * vector.z;
						outData[num42, num44, 8] = num33 * vector.w + num35 * vector.x + num34 * vector.y + num36 * vector.z;
						outData[num42, num44, 9] = num37 * vector.w + num39 * vector.x + num38 * vector.y + num40 * vector.z;
						num45++;
						num44++;
					}
					num43++;
					num42++;
				}
			}
		}
	}

	private TerrainObject GetTerrainObjectFromPrefabGroup(Biome.SceneryDistributor.PrefabGroup prefabGroup, SceneryTypes sceneryType)
	{
		for (int i = 0; i < prefabGroup.terrainObject.Length; i++)
		{
			TerrainObject terrainObject = prefabGroup.terrainObject[i];
			if (terrainObject != null)
			{
				Visible component = terrainObject.GetComponent<Visible>();
				if (component != null && component.type == ObjectTypes.Scenery && component.ItemType == (int)sceneryType)
				{
					return terrainObject;
				}
			}
		}
		return null;
	}

	public void ApplyQualitySettings()
	{
		int reducedHeightmapDetail = QualitySettingsExtended.ReducedHeightmapDetail;
		m_HeightmapResolutionPerCell = Mathf.Max(1, m_AdvancedParameters.heightmapResolutionPerCell >> reducedHeightmapDetail);
		m_MultiTextureResolutionPerCell = Mathf.Max(1, m_AdvancedParameters.multiTextureResolutionPerCell >> reducedHeightmapDetail);
		d.Log($"[BiomeMap] Setting m_HeightmapResolutionPerCell {m_AdvancedParameters.heightmapResolutionPerCell}=>{m_HeightmapResolutionPerCell} m_MultiTextureResolutionPerCell {m_AdvancedParameters.multiTextureResolutionPerCell}=>{m_MultiTextureResolutionPerCell}");
	}

	private void OnValidate()
	{
		Biome[] array = biomes;
		foreach (Biome biome in array)
		{
			if (!biome.HeightMapGenerator || !biome.MultiTextureGenerator)
			{
				d.LogError("Biome " + biome.name + " must have generators for heightmap and multitexture");
			}
		}
		BiomeGroup[] biomeGroups = m_BiomeGroups;
		for (int i = 0; i < biomeGroups.Length; i++)
		{
			array = biomeGroups[i].Biomes;
			foreach (Biome biome2 in array)
			{
				if (!biome2.HeightMapGenerator || !biome2.MultiTextureGenerator)
				{
					d.LogError("Biome " + biome2.name + " must have generators for heightmap and multitexture");
				}
			}
		}
	}
}
