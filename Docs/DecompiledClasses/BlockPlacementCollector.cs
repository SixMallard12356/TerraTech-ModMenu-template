#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Unity.Profiling;
using UnityEngine;

public class BlockPlacementCollector
{
	public class Placement
	{
		public Vector3 localPos;

		public OrthoRotation orthoRot;

		public long shape;

		public int attachedness;

		public bool preferential;

		public bool isAllocated => attachedness > 0;

		public void Reset()
		{
			attachedness = 0;
		}
	}

	public class Collection
	{
		public class PlacementList
		{
			private List<Placement> _list = new List<Placement>();

			public float totalAttachedness;

			public float AverageAttachedness => totalAttachedness / (float)_list.Count;

			public int Count => _list.Count();

			public Placement this[int index] => _list[index];

			public void Clear()
			{
				_list.Clear();
				totalAttachedness = 0f;
			}

			public void Add(Placement placement)
			{
				_list.Add(placement);
			}

			public void Insert(int index, Placement placement)
			{
				_list.Insert(index, placement);
			}

			public Placement First()
			{
				return _list.First();
			}

			public Placement Last()
			{
				return _list.Last();
			}
		}

		public struct Index
		{
			private Collection collection;

			private PlacementList currentList;

			private int indexInList;

			public Placement Value
			{
				get
				{
					if (collection == null)
					{
						return null;
					}
					return currentList[indexInList];
				}
			}

			public bool IsValid => collection != null;

			public Index(Collection collection, int listIndex, int itemIndex)
			{
				this.collection = collection;
				currentList = collection?.placementListsByShape[listIndex];
				indexInList = itemIndex;
			}

			public bool IsFirst()
			{
				if (collection != null)
				{
					if (currentList == collection.placementListsByShape[0])
					{
						return indexInList == 0;
					}
					return false;
				}
				d.Assert(condition: false, "null collection");
				return true;
			}

			public static Index operator ++(Index index)
			{
				d.Assert(index.collection != null && index.collection.Count != 0);
				lock (index.collection)
				{
					int num = index.collection.placementListsByShape.IndexOf(index.currentList);
					int num2 = num;
					do
					{
						if (++num == index.collection.placementListsByShape.Count)
						{
							num = 0;
							index.indexInList++;
						}
						index.currentList = index.collection.placementListsByShape[num];
						if (num == num2 && index.indexInList >= index.currentList.Count)
						{
							index.currentList = index.collection.placementListsByShape[0];
							index.indexInList = 0;
							break;
						}
					}
					while (index.indexInList >= index.currentList.Count);
				}
				return index;
			}

			public static Index operator --(Index index)
			{
				d.Assert(index.collection != null && index.collection.Count != 0);
				lock (index.collection)
				{
					int num = index.collection.placementListsByShape.IndexOf(index.currentList);
					do
					{
						if (--num == -1)
						{
							num = index.collection.placementListsByShape.Count - 1;
							index.indexInList--;
						}
						index.currentList = index.collection.placementListsByShape[num];
						if (index.indexInList == -1)
						{
							Index index2 = index.collection.Last();
							index.currentList = index2.currentList;
							index.indexInList = index2.indexInList;
							break;
						}
					}
					while (index.indexInList >= index.currentList.Count);
				}
				return index;
			}

			public static bool operator ==(Index a, Index b)
			{
				if (a.currentList == b.currentList)
				{
					return a.indexInList == b.indexInList;
				}
				return false;
			}

			public static bool operator !=(Index a, Index b)
			{
				if (a.currentList == b.currentList)
				{
					return a.indexInList != b.indexInList;
				}
				return true;
			}

			public override bool Equals(object obj)
			{
				if (!(obj is Index))
				{
					return false;
				}
				return this == (Index)obj;
			}

			public override int GetHashCode()
			{
				d.Assert(condition: false, "BlockPlacementCollector.Collection.Index.GetHashCode() is unimplemeted");
				return -1;
			}
		}

		private List<PlacementList> placementListsByShape;

		private Dictionary<long, PlacementList> placementListLookup;

		public bool HasPreferentialPlacements { get; private set; }

		public int Count { get; private set; }

		public Collection()
		{
			placementListsByShape = new List<PlacementList>();
			placementListLookup = new Dictionary<long, PlacementList>();
			HasPreferentialPlacements = false;
		}

		public Index First()
		{
			if (Count == 0)
			{
				return new Index(null, 0, 0);
			}
			return new Index(this, 0, 0);
		}

		public Index FirstPreferred(OrthoRotation preferredRotation)
		{
			if (Count == 0)
			{
				return new Index(null, 0, 0);
			}
			Index index = new Index(this, 0, 0);
			if (index.Value.orthoRot == preferredRotation)
			{
				return index;
			}
			Index index2 = index;
			do
			{
				++index2;
			}
			while (index2.Value.orthoRot != preferredRotation && index2 != index);
			return index2;
		}

		public Index Last()
		{
			if (Count != 0)
			{
				int num = 0;
				int listIndex = 0;
				for (int i = 0; i < placementListsByShape.Count; i++)
				{
					if (placementListsByShape[i].Count >= num)
					{
						listIndex = i;
						num = placementListsByShape[i].Count;
					}
				}
				if (num != 0)
				{
					return new Index(this, listIndex, num - 1);
				}
			}
			return new Index(null, 0, 0);
		}

		public IEnumerable<Index> EnumeratePlacements(OrthoRotation rot)
		{
			yield break;
		}

		public void Add(Placement placement, Stack<PlacementList> placementListPool)
		{
			PlacementList value = null;
			if (!placementListLookup.TryGetValue(placement.shape, out value))
			{
				if (placementListPool.Count == 0)
				{
					for (int num = 1000; num != 0; num--)
					{
						placementListPool.Push(new PlacementList());
					}
				}
				value = placementListPool.Pop();
				placementListLookup[placement.shape] = value;
				placementListsByShape.Add(value);
			}
			if (value.Count != 0 && placement.attachedness > value[value.Count - 1].attachedness)
			{
				int num2 = value.Count - 1;
				while (--num2 >= 0 && value[num2].attachedness < placement.attachedness)
				{
				}
				value.Insert(num2 + 1, placement);
			}
			else
			{
				value.Add(placement);
			}
			value.totalAttachedness += placement.attachedness;
			int num3 = placementListsByShape.IndexOf(value);
			float averageAttachedness = value.AverageAttachedness;
			bool flag = false;
			while (num3 < placementListsByShape.Count - 1 && placementListsByShape[num3 + 1].AverageAttachedness > averageAttachedness)
			{
				placementListsByShape[num3] = placementListsByShape[++num3];
				placementListsByShape[num3] = value;
				flag = true;
			}
			while (!flag && num3 > 0 && placementListsByShape[num3 - 1].AverageAttachedness < averageAttachedness)
			{
				placementListsByShape[num3] = placementListsByShape[--num3];
				placementListsByShape[num3] = value;
			}
			if (placement.preferential)
			{
				HasPreferentialPlacements = true;
			}
			Count++;
		}

		public void Clear(Stack<Placement> placementPool, Stack<PlacementList> placementListPool)
		{
			for (int i = 0; i < placementListsByShape.Count; i++)
			{
				PlacementList placementList = placementListsByShape[i];
				for (int j = 0; j < placementList.Count; j++)
				{
					Placement placement = placementList[j];
					if (placement.isAllocated)
					{
						placement.Reset();
						placementPool.Push(placement);
					}
				}
				placementList.Clear();
				placementListPool.Push(placementList);
			}
			placementListLookup.Clear();
			placementListsByShape.Clear();
			HasPreferentialPlacements = false;
			Count = 0;
		}
	}

	public struct Enumerator : IDisposable
	{
		private BlockPlacementCollector collector;

		private Dictionary<IntVector3, Collection>.Enumerator dictEnumerator;

		public KeyValuePair<IntVector3, Collection> Current => dictEnumerator.Current;

		public Enumerator(BlockPlacementCollector collector)
		{
			this.collector = collector;
			dictEnumerator = default(Dictionary<IntVector3, Collection>.Enumerator);
		}

		public Enumerator Get()
		{
			Monitor.Enter(collector.m_Placements);
			dictEnumerator = collector.m_Placements.GetEnumerator();
			return this;
		}

		public bool MoveNext()
		{
			return dictEnumerator.MoveNext();
		}

		public void Dispose()
		{
			dictEnumerator.Dispose();
			Monitor.Exit(collector.m_Placements);
		}
	}

	private struct QueryPosition
	{
		public struct Comparer : IComparer<QueryPosition>
		{
			private IntVector3 referencePos;

			private ushort refPosHash;

			public Comparer(IntVector3 referencePos)
			{
				this.referencePos = referencePos;
				refPosHash = (ushort)((referencePos.x << 10) | (referencePos.y << 5) | referencePos.x);
			}

			public int Compare(QueryPosition a, QueryPosition b)
			{
				return b.DSqr(in referencePos, refPosHash) - a.DSqr(in referencePos, refPosHash);
			}
		}

		public IntVector3 pos;

		private ushort dSqrCached;

		private ushort cachedSortID;

		public QueryPosition(in IntVector3 position)
		{
			pos = position;
			dSqrCached = 0;
			cachedSortID = 0;
		}

		private int DSqr(in IntVector3 refrencePos, ushort sortID)
		{
			if (cachedSortID != sortID)
			{
				dSqrCached = (ushort)(pos - refrencePos).sqrMagnitude;
				cachedSortID = sortID;
			}
			return dSqrCached;
		}
	}

	private struct RotatedBlockData
	{
		public IntBounds bounds;

		public IntVector3[] filledCells;

		public IntVector3[] apPosLocal;

		public IntVector3[] cellLocal;
	}

	private TankBlock m_BlockToAttach;

	private Tank m_TechToAttachTo;

	private IntVector3 m_PlacementBiasPosLocal;

	private Vector3[] m_PlacementFilter;

	private OrthoRotation m_PreferredOrientation;

	private Dictionary<IntVector3, Collection> m_Placements;

	private List<IntVector3> m_BlockAttachmentLocalApCache;

	private List<QueryPosition> m_QueryPositionsSorted = new List<QueryPosition>(10000);

	private OrthoRotation.r[] m_OrientationBuffer = new OrthoRotation.r[24];

	private RotatedBlockData[] m_RotatedBlockData = new RotatedBlockData[24];

	private BlockManager.TableCache m_BlockTableCache;

	private int m_BlockToAttachNumAPs;

	private int m_BlockToAttachNumFilledCells;

	private bool m_BlockToAttachHasCircuitComponent;

	private Enumerator m_Enumerator;

	private ThreadedJobProcessor m_CollectionThread = new ThreadedJobProcessor();

	private Stack<Placement> m_PlacementPool;

	private Stack<Collection.PlacementList> m_PlacementListPool;

	private Stack<Collection> m_CollectionPool;

	private const int k_InitPlacementPoolSize = 10000;

	private const int k_InitPlacementListPoolSize = 10000;

	private const int k_InitPlacementListLength = 10;

	private const int k_InitCollectionPoolSize = 2000;

	private const int k_PlacementBiasResortThresholdSqr = 100;

	private const int k_CollectFastPathMaxBlockSize = 6;

	private static readonly ProfilerMarker marker_CollectGatherAttachments = new ProfilerMarker("BlockPlacement.GatherAPs");

	private static readonly ProfilerMarker marker_CollectGatherInner = new ProfilerMarker("BlockPlacement.GatherAPs.Position");

	private static readonly ProfilerMarker marker_CollectQueryPositions_Default = new ProfilerMarker("BlockPlacement.QueryPositions.Default");

	private static readonly ProfilerMarker marker_CollectQueryPositions_FastPath = new ProfilerMarker("BlockPlacement.QueryPositions.Fast");

	private HashSet<IntVector3> m_CellsWithOpenAPs = new HashSet<IntVector3>();

	public bool PlacementsValid
	{
		get
		{
			if (m_BlockToAttach != null)
			{
				return m_TechToAttachTo != null;
			}
			return false;
		}
	}

	public bool PlacementsFinal => m_CollectionThread.JobCompleted;

	public int Count => m_Placements.Count;

	public Enumerator GetEnumerator()
	{
		return m_Enumerator.Get();
	}

	public void Initialise()
	{
		if (m_Placements == null)
		{
			m_Placements = new Dictionary<IntVector3, Collection>(2000);
		}
		if (m_PlacementPool == null)
		{
			m_PlacementPool = new Stack<Placement>();
			for (int i = 0; i < 10000; i++)
			{
				m_PlacementPool.Push(new Placement());
			}
		}
		if (m_PlacementListPool == null)
		{
			m_PlacementListPool = new Stack<Collection.PlacementList>();
			for (int j = 0; j < 10000; j++)
			{
				m_PlacementListPool.Push(new Collection.PlacementList());
			}
		}
		if (m_CollectionPool == null)
		{
			m_CollectionPool = new Stack<Collection>();
			for (int k = 0; k < 2000; k++)
			{
				m_CollectionPool.Push(new Collection());
			}
		}
		m_Enumerator = new Enumerator(this);
		m_BlockAttachmentLocalApCache = new List<IntVector3>(32);
		m_BlockToAttach = null;
		m_TechToAttachTo = null;
		m_PlacementFilter = null;
		ClearPlacements();
		m_CollectionThread.Initialise("BlockPlacementCollector", CollectPlacements);
	}

	public void Terminate(bool waitForExit)
	{
		m_CollectionThread.Terminate(waitForExit);
	}

	public void Start(TankBlock block, Tank tech, Vector3[] filter, OrthoRotation preferredRotation)
	{
		m_CollectionThread.Stop();
		ResetState(block, tech, filter, preferredRotation);
		m_CollectionThread.Start();
	}

	public void Stop()
	{
		m_CollectionThread.Stop();
		ResetState(null, null, null, OrthoRotation.identity);
	}

	public void UpdateBlockPosTechLocal(Vector3 pos)
	{
		m_PlacementBiasPosLocal = pos;
	}

	private void ResetState(TankBlock block, Tank tech, Vector3[] filter, OrthoRotation preferredRotation)
	{
		m_BlockToAttach = block;
		if (block != null)
		{
			m_BlockToAttachNumAPs = block.attachPoints.Length;
			m_BlockToAttachNumFilledCells = block.filledCells.Length;
			m_BlockToAttachHasCircuitComponent = block.CircuitNode.IsNotNull();
		}
		else
		{
			m_BlockToAttachNumAPs = -1;
			m_BlockToAttachNumFilledCells = -1;
			m_BlockToAttachHasCircuitComponent = false;
		}
		m_TechToAttachTo = tech;
		m_PlacementFilter = filter;
		m_PreferredOrientation = preferredRotation;
		if ((bool)m_TechToAttachTo)
		{
			m_BlockTableCache = m_TechToAttachTo.blockman.GetTableCacheForPlacementCollection();
		}
		ClearPlacements();
	}

	[Conditional("PROFILE_COLLECTION")]
	private void LogProfileInfo(object message)
	{
	}

	private bool CollectPlacements(ThreadedJobProcessor.TestShouldExitDelegate testShouldExit)
	{
		if (m_BlockToAttach == null)
		{
			throw new Exception("CollectPlacements m_BlockToAttach is null");
		}
		if (m_BlockToAttach.visible == null)
		{
			throw new Exception("CollectPlacements m_BlockToAttach.visible is null");
		}
		if (m_TechToAttachTo == null)
		{
			throw new Exception("CollectPlacements m_TechToAttachTo is null");
		}
		OrthoRotation.r[] array = Singleton.Manager<ManTechBuilder>.inst.GetBlockRotationOrder(m_BlockToAttach);
		int num = array.Length;
		if (m_PreferredOrientation != OrthoRotation.invalid)
		{
			m_OrientationBuffer[0] = m_PreferredOrientation.rot;
			int i = 1;
			int num2 = 1;
			for (; i < num; i++)
			{
				if (num2 == 1 && array[i - 1] == m_PreferredOrientation.rot)
				{
					num2 = 0;
				}
				m_OrientationBuffer[i] = array[i - num2];
			}
			array = m_OrientationBuffer;
		}
		m_QueryPositionsSorted.Clear();
		if (m_PlacementFilter != null && m_PlacementFilter.Length != 0)
		{
			for (int j = 0; j < m_PlacementFilter.Length; j++)
			{
				m_QueryPositionsSorted.Add(new QueryPosition((IntVector3)m_PlacementFilter[j]));
			}
		}
		else
		{
			IntBounds bounds = m_BlockTableCache.bounds;
			Vector3 size = m_BlockToAttach.BlockCellBounds.size;
			int num3 = Mathf.RoundToInt(Mathf.Max(size.x, Mathf.Max(size.y, size.z))) + 1;
			IntBounds intBounds = new IntBounds(bounds.min - IntVector3.one * num3, bounds.max + IntVector3.one * num3);
			IntVector3 size2 = intBounds.size;
			int num4 = size2.x * size2.y * size2.z;
			if (m_QueryPositionsSorted.Capacity < num4)
			{
				m_QueryPositionsSorted.Capacity = num4;
			}
			if (num3 < 6 && num3 < m_BlockTableCache.size)
			{
				_ = m_BlockTableCache;
				int size3 = m_BlockTableCache.size;
				Vector3 vector = Vector3.one * (num3 - 1);
				BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = m_TechToAttachTo.blockman.IterateBlocks().GetEnumerator();
				while (enumerator.MoveNext())
				{
					TankBlock current = enumerator.Current;
					if (current.NumConnectedAPs == current.attachPoints.Length)
					{
						continue;
					}
					Vector3 cachedLocalPosition = current.cachedLocalPosition;
					OrthoRotation cachedLocalRotation = current.cachedLocalRotation;
					for (int k = 0; k < current.attachPoints.Length; k++)
					{
						if (current.ConnectedBlocksByAP[k].IsNotNull())
						{
							continue;
						}
						Vector3 vector2 = current.attachPoints[k];
						IntVector3 intVector = new IntVector3(OutwardToInt(vector2.x), OutwardToInt(vector2.y), OutwardToInt(vector2.z));
						if (num3 == 1)
						{
							m_CellsWithOpenAPs.Add(cachedLocalRotation * intVector + cachedLocalPosition);
							continue;
						}
						IntVector3 intVector2 = cachedLocalRotation * intVector + cachedLocalPosition;
						IntVector3 b = intVector2 - vector;
						IntVector3 b2 = intVector2 + vector;
						IntVector3 intVector3 = IntVector3.Max(bounds.max, b2) - IntVector3.Min(bounds.min, b);
						if (intVector3.x - size3 > num3 || intVector3.y - size3 > num3 || intVector3.z - size3 > num3)
						{
							continue;
						}
						for (int l = b.x; l <= b2.x; l++)
						{
							for (int m = b.y; m <= b2.y; m++)
							{
								for (int n = b.z; n <= b2.z; n++)
								{
									m_CellsWithOpenAPs.Add(new IntVector3(l, m, n));
								}
							}
						}
					}
				}
				foreach (IntVector3 cellsWithOpenAP in m_CellsWithOpenAPs)
				{
					IntVector3 position = cellsWithOpenAP;
					m_QueryPositionsSorted.Add(new QueryPosition(in position));
				}
				m_CellsWithOpenAPs.Clear();
			}
			else
			{
				IntVector3 min = intBounds.min;
				IntVector3 max = intBounds.max;
				for (int num5 = min.x; num5 <= max.x; num5++)
				{
					for (int num6 = min.y; num6 <= max.y; num6++)
					{
						for (int num7 = min.z; num7 <= max.z; num7++)
						{
							m_QueryPositionsSorted.Add(new QueryPosition(new IntVector3(num5, num6, num7)));
						}
					}
				}
			}
		}
		m_BlockTableCache.bounds = m_BlockTableCache.bounds.Translate(m_BlockTableCache.blockTableCentre);
		for (int num8 = 0; num8 < num; num8++)
		{
			OrthoRotation.r r = array[num8];
			OrthoRotation orthoRotation = new OrthoRotation(r);
			int num9 = (int)r;
			ref RotatedBlockData reference = ref m_RotatedBlockData[num9];
			Bounds bounds2 = orthoRotation * m_BlockToAttach.BlockCellBounds;
			reference.bounds = new IntBounds(bounds2.min, bounds2.max + IntVector3.one);
			if (reference.filledCells == null || reference.filledCells.Length < m_BlockToAttachNumFilledCells)
			{
				reference.filledCells = new IntVector3[m_BlockToAttachNumFilledCells];
			}
			for (int num10 = 0; num10 < m_BlockToAttachNumFilledCells; num10++)
			{
				reference.filledCells[num10] = orthoRotation * m_BlockToAttach.filledCells[num10];
			}
			if (reference.apPosLocal == null || reference.apPosLocal.Length < m_BlockToAttachNumAPs)
			{
				reference.apPosLocal = new IntVector3[m_BlockToAttachNumAPs];
				reference.cellLocal = new IntVector3[m_BlockToAttachNumAPs];
			}
			for (int num11 = 0; num11 < m_BlockToAttachNumAPs; num11++)
			{
				IntVector3 filledCellForAPIndex = m_BlockToAttach.GetFilledCellForAPIndex(num11);
				IntVector3 intVector4 = m_BlockToAttach.attachPoints[num11] * 2f;
				reference.apPosLocal[num11] = orthoRotation * intVector4;
				reference.cellLocal[num11] = orthoRotation * filledCellForAPIndex;
			}
		}
		IntVector3 placementBiasPosLocal = m_PlacementBiasPosLocal;
		int num12 = m_QueryPositionsSorted.Count / 10;
		bool flag = true;
		for (int num13 = 9; num13 >= 0; num13--)
		{
			int num14 = num13 * num12;
			int num15 = ((num13 == 9) ? m_QueryPositionsSorted.Count : (num14 + num12));
			if (flag)
			{
				placementBiasPosLocal = m_PlacementBiasPosLocal;
				m_QueryPositionsSorted.Sort(0, num15, new QueryPosition.Comparer(m_PlacementBiasPosLocal));
				flag = false;
			}
			for (int num16 = 0; num16 < num; num16++)
			{
				OrthoRotation orthoRot = new OrthoRotation(array[num16]);
				for (int num17 = num15 - 1; num17 >= num14; num17--)
				{
					if (testShouldExit())
					{
						return false;
					}
					IntVector3 pos = m_QueryPositionsSorted[num17].pos;
					int attachedness;
					bool isPreferential;
					long shape = GatherBlockAttachments(pos, m_RotatedBlockData[(int)orthoRot.rot], m_BlockAttachmentLocalApCache, out attachedness, out isPreferential);
					TryAddPlacement(pos, orthoRot, shape, attachedness, isPreferential);
				}
			}
			flag = (m_PlacementBiasPosLocal - placementBiasPosLocal).sqrMagnitude > 100;
		}
		return true;
	}

	private static int OutwardToInt(float f)
	{
		return (int)Mathf.Sign(f) * Mathf.CeilToInt(Mathf.Abs(f));
	}

	private Placement GetNewPlacement(Vector3 pos, OrthoRotation rot, long shape, int attachedness, bool isPreferential)
	{
		if (m_PlacementPool.Count == 0)
		{
			for (int num = 1000; num != 0; num--)
			{
				m_PlacementPool.Push(new Placement());
			}
		}
		d.Assert(attachedness > 0, "A placement with 0 attachments ?!");
		Placement placement = m_PlacementPool.Pop();
		placement.localPos = pos;
		placement.orthoRot = rot;
		placement.shape = shape;
		placement.attachedness = attachedness;
		placement.preferential = isPreferential;
		return placement;
	}

	private Collection GetNewCollection()
	{
		if (m_CollectionPool.Count == 0)
		{
			for (int num = 200; num != 0; num--)
			{
				m_CollectionPool.Push(new Collection());
			}
		}
		return m_CollectionPool.Pop();
	}

	private void ClearPlacements()
	{
		lock (m_Placements)
		{
			foreach (KeyValuePair<IntVector3, Collection> placement in m_Placements)
			{
				placement.Value.Clear(m_PlacementPool, m_PlacementListPool);
				m_CollectionPool.Push(placement.Value);
			}
			m_Placements.Clear();
		}
	}

	private static long hashTableCoord(IntVector3 v)
	{
		return ((v.x << 20) | (v.y << 10) | v.z) * v.x * v.y * v.z;
	}

	private long GatherBlockAttachments(IntVector3 localPos, RotatedBlockData rotatedBlockData, List<IntVector3> attachments, out int attachedness, out bool isPreferential)
	{
		attachedness = 0;
		isPreferential = false;
		attachments.Clear();
		IntVector3 intVector = localPos + m_BlockTableCache.blockTableCentre;
		IntBounds other = rotatedBlockData.bounds.Translate(intVector);
		IntVector3 size = m_BlockTableCache.bounds.Union(other).size;
		int size2 = m_BlockTableCache.size;
		if (size.x > size2 || size.y > size2 || size.z > size2)
		{
			return 0L;
		}
		for (int i = 0; i < m_BlockToAttachNumFilledCells; i++)
		{
			IntVector3 intVector2 = intVector + rotatedBlockData.filledCells[i];
			if (intVector2.x >= 0 && intVector2.x < size2 && intVector2.y >= 0 && intVector2.y < size2 && intVector2.z >= 0 && intVector2.z < size2 && m_BlockTableCache.blockTable[intVector2.x, intVector2.y, intVector2.z].IsNotNull())
			{
				return 0L;
			}
		}
		IntVector3 intVector3 = intVector + rotatedBlockData.bounds.min;
		IntVector3 intVector4 = intVector + rotatedBlockData.bounds.max + IntVector3.one;
		bool flag = intVector3.x <= 0 || intVector3.y <= 0 || intVector3.z <= 0 || intVector4.x >= size2 || intVector4.y >= size2 || intVector4.z >= size2;
		for (int j = 0; j < m_BlockToAttachNumAPs; j++)
		{
			IntVector3 intVector5 = rotatedBlockData.apPosLocal[j];
			IntVector3 intVector6 = rotatedBlockData.cellLocal[j];
			IntVector3 intVector7 = intVector5 - intVector6;
			IntVector3 intVector8 = intVector + intVector7;
			if (flag && (intVector8.x < 0 || intVector8.x >= size2 || intVector8.y < 0 || intVector8.y >= size2 || intVector8.z < 0 || intVector8.z >= size2))
			{
				continue;
			}
			byte b = m_BlockTableCache.apTable[intVector8.x, intVector8.y, intVector8.z];
			if (b == 0)
			{
				continue;
			}
			byte apBit = (intVector7 - intVector6).APHalfBits();
			if ((b & BlockManager.OppositeAPBit(apBit)) == 0)
			{
				continue;
			}
			TankBlock tankBlock = m_BlockTableCache.blockTable[intVector8.x, intVector8.y, intVector8.z];
			IntVector3 intVector9 = localPos + localPos + intVector5;
			if (tankBlock.IsNotNull() && tankBlock.APIgnoreFilter != null)
			{
				int num = -1;
				for (int k = 0; k < tankBlock.attachPoints.Length; k++)
				{
					IntVector3 intVector10 = (tankBlock.cachedLocalRotation * tankBlock.attachPoints[k] + tankBlock.cachedLocalPosition).LocalToAP();
					if (intVector9 == intVector10)
					{
						num = k;
						break;
					}
				}
				if (num >= 0)
				{
					if (tankBlock.APIgnoreFilter(num))
					{
						continue;
					}
				}
				else
				{
					d.LogError("Failed to match apPos to attach position within other block");
				}
			}
			attachments.Add(intVector9);
			attachedness++;
			if (!m_BlockToAttachHasCircuitComponent || !tankBlock.CircuitNode.IsNotNull())
			{
				continue;
			}
			ModuleCircuitNode circuitNode = m_BlockToAttach.CircuitNode;
			ModuleCircuitNode circuitNode2 = tankBlock.CircuitNode;
			if (circuitNode.IsCompatibleWithBlock(circuitNode2))
			{
				Vector3 vector = m_BlockToAttach.attachPoints[j];
				Vector3 vector2 = BlockPlacementPreviewHandler.TechPlacementToBlockAP(intVector9, tankBlock.cachedLocalPosition, tankBlock.cachedLocalRotation);
				if (circuitNode.ConnexionLookupByAP.TryGetValue(vector, out var value) && circuitNode2.ConnexionLookupByAP.TryGetValue(vector2, out var value2) && ModuleCircuitNode.AreConnexionsCompatible(value, value2) && ((ModuleCircuitNode.Theoretics.CanOutputChargeThroughAP(circuitNode, vector) && ModuleCircuitNode.Theoretics.CanReceiveChargeThroughAP(circuitNode2, vector2)) || (ModuleCircuitNode.Theoretics.CanReceiveChargeThroughAP(circuitNode, vector) && ModuleCircuitNode.Theoretics.CanOutputChargeThroughAP(circuitNode2, vector2))))
				{
					attachedness += 2;
					isPreferential = true;
				}
			}
		}
		long num2 = 0L;
		if (attachedness > 0)
		{
			for (int l = 0; l < m_BlockToAttachNumFilledCells; l++)
			{
				num2 ^= hashTableCoord(intVector + rotatedBlockData.filledCells[l]);
			}
		}
		return num2;
	}

	private void TryAddPlacement(IntVector3 localPos, OrthoRotation orthoRot, long shape, int attachedness, bool isPreferentialPlacement)
	{
		if (attachedness <= 0)
		{
			return;
		}
		Placement newPlacement = GetNewPlacement(localPos, orthoRot, shape, attachedness, isPreferentialPlacement);
		foreach (IntVector3 item in m_BlockAttachmentLocalApCache)
		{
			bool flag = false;
			Collection value;
			lock (m_Placements)
			{
				flag = m_Placements.TryGetValue(item, out value);
			}
			if (!flag)
			{
				value = GetNewCollection();
				lock (m_Placements)
				{
					m_Placements[item] = value;
				}
			}
			lock (value)
			{
				value.Add(newPlacement, m_PlacementListPool);
			}
		}
	}
}
