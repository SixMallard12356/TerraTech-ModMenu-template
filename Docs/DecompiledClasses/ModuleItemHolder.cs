#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleItemHolder : Module
{
	public delegate bool AllowAcceptItemFunc(Visible item, Stack fromStack, Stack toStack, PassType passOpType);

	public delegate bool AllowReleaseItemFunc(Visible item, Stack fromStack, Stack optionalToStack, PassType passOpType);

	[Flags]
	public enum Flags
	{
		Collector = 1,
		Receiver = 2,
		Deliverer = 4,
		TakeFromSilo = 8,
		LinkedStacks = 0x10,
		NoSimpleCollider = 0x20,
		SortLinkedStacks = 0x40,
		NoAcceptDrops = 0x80,
		ReceiverInternal = 0x100
	}

	[Flags]
	public enum AcceptFlags
	{
		Blocks = 1,
		Chunks = 2
	}

	[Flags]
	public enum PassType
	{
		Pick = 1,
		Pass = 2,
		Drop = 4,
		Test = 8,
		External = 0x10
	}

	public enum ResultCode
	{
		Uninitialised,
		InvalidBlockState,
		ItemHoldBlocked,
		HolderIsFull,
		IncompatibleItemType,
		ItemHeldByPlayer,
		ReleaseFilterFail,
		AcceptFilterFail,
		NoItemAuthority,
		Success
	}

	public struct AcceptResult
	{
		private ResultCode code;

		public bool IsSuccess => code == ResultCode.Success;

		public ResultCode FailReason => code;

		private AcceptResult(ResultCode _res)
		{
			code = _res;
		}

		public static implicit operator AcceptResult(ResultCode resCode)
		{
			return new AcceptResult(resCode);
		}

		public static implicit operator bool(AcceptResult res)
		{
			return res.IsSuccess;
		}
	}

	[Serializable]
	public class Stack
	{
		public struct ConnectedStackIterator
		{
			public struct Enumerator
			{
				private Stack[] stacks;

				private int index;

				public Stack Current => stacks[index];

				public Enumerator(Stack[] stacks)
				{
					this.stacks = stacks;
					index = -1;
				}

				public bool MoveNext()
				{
					do
					{
						index++;
						if (index == stacks.Length)
						{
							return false;
						}
					}
					while (stacks[index] == null);
					return true;
				}
			}

			private Stack[] stacks;

			public ConnectedStackIterator(Stack[] stacks)
			{
				this.stacks = stacks;
			}

			public Enumerator GetEnumerator()
			{
				return new Enumerator(stacks);
			}
		}

		public struct ItemIterator
		{
			private Stack singleStack;

			private Stack[] allStacks;

			private int stackIndex;

			private int itemIndex;

			private int modIndex;

			private int stackCount;

			public Visible Current => ((singleStack == null) ? allStacks[stackIndex] : singleStack).items[itemIndex];

			public ItemIterator(Stack stack)
			{
				d.Assert(stack != null && (bool)stack.myHolder);
				singleStack = stack;
				allStacks = null;
				stackIndex = -1;
				stackCount = 0;
				itemIndex = -1;
				modIndex = stack.myHolder.m_ContentsModificationIndex;
			}

			public ItemIterator(Stack stack, int startingStack, bool forceLinked)
			{
				d.Assert(stack != null && (bool)stack.myHolder);
				d.Assert(startingStack < stack.myHolder.m_Stacks.Length);
				singleStack = null;
				allStacks = null;
				stackIndex = startingStack;
				stackCount = 0;
				itemIndex = -1;
				modIndex = stack.myHolder.m_ContentsModificationIndex;
				if (forceLinked || stack.myHolder.IsFlag(Flags.LinkedStacks))
				{
					allStacks = stack.myHolder.m_Stacks;
				}
				else
				{
					singleStack = stack;
				}
			}

			public ItemIterator GetEnumerator()
			{
				return this;
			}

			public bool MoveNext()
			{
				itemIndex++;
				Stack stack = ((singleStack == null) ? allStacks[stackIndex] : singleStack);
				d.Assert(modIndex == stack.myHolder.m_ContentsModificationIndex, "Contents modified while iterating");
				while (itemIndex == stack.items.Count)
				{
					if (singleStack != null)
					{
						return false;
					}
					stackIndex = (stackIndex + 1) % allStacks.Length;
					itemIndex = 0;
					if (++stackCount == stack.myHolder.m_Stacks.Length)
					{
						return false;
					}
					stack = allStacks[stackIndex];
				}
				return true;
			}
		}

		public Vector3 basePos;

		public int[] apConnectionIndices;

		[NonSerialized]
		public ModuleItemHolder myHolder;

		[NonSerialized]
		public List<Visible> items;

		[NonSerialized]
		public Stack[] connectedNeighbourStacks;

		[NonSerialized]
		public Func<Visible, bool> isPrePickupFn;

		[SerializeField]
		private float m_StackLockTimer;

		private int m_ReceivedOnHeartbeat;

		public bool IsPickupLocked => m_StackLockTimer > Time.time;

		public bool IsEmpty => items.Count == 0;

		public bool IsFull => items.Count >= myHolder.m_CapacityPerStack;

		public int NumItems => items.Count;

		public Visible FirstItem => items[0];

		public Visible LastItem => items[items.Count - 1];

		public Vector3 UpDir => myHolder.m_StackUpDir;

		public bool NetClientsUpdateItemPos { get; set; } = true;

		public bool ReceivedThisHeartbeat
		{
			get
			{
				if (myHolder.block.IsAttached)
				{
					return m_ReceivedOnHeartbeat == myHolder.block.tank.Holders.HeartbeatCount;
				}
				return false;
			}
		}

		public ConnectedStackIterator ConnectedStacks => new ConnectedStackIterator(connectedNeighbourStacks);

		public ItemIterator IterateItems()
		{
			return new ItemIterator(this);
		}

		public ItemIterator IterateItemsIncludingLinkedStacks(int startingStack = 0)
		{
			return new ItemIterator(this, startingStack, forceLinked: false);
		}

		public void OfferAllItemsToCollector(ItemSearchCollector collector)
		{
			int count = items.Count;
			for (int i = 0; i < count; i++)
			{
				collector.OfferItem(items[i]);
			}
		}

		public Vector3 BasePosWorld()
		{
			Vector3 result = myHolder.block.trans.TransformPoint(basePos);
			Rigidbody rigidbody = (myHolder.block.IsAttached ? myHolder.block.tank.rbody : myHolder.block.rbody);
			if (rigidbody.IsNotNull())
			{
				result += rigidbody.position - rigidbody.transform.position;
			}
			return result;
		}

		public Vector3 BasePosWorldOffsetLocal(Vector3 offset)
		{
			Vector3 result = myHolder.block.trans.TransformPoint(basePos + offset);
			Rigidbody rigidbody = (myHolder.block.IsAttached ? myHolder.block.tank.rbody : myHolder.block.rbody);
			if (rigidbody.IsNotNull())
			{
				result += rigidbody.position - rigidbody.transform.position;
			}
			return result;
		}

		public int GetStackIndex()
		{
			return Array.FindIndex(myHolder.m_Stacks, (Stack s) => s == this);
		}

		public void ClearConnectedStack(Stack stack)
		{
			int num = Array.FindIndex(connectedNeighbourStacks, (Stack s) => s == stack);
			if (num != -1)
			{
				connectedNeighbourStacks[num] = null;
			}
		}

		public bool TestAcceptFilters(Visible item, Stack fromStack, PassType passType)
		{
			return myHolder.TestAcceptFilters(item, fromStack, this, passType);
		}

		public AcceptResult CanAccept(Visible item, Stack fromStack, PassType passType)
		{
			if (passType != (PassType.Pass | PassType.Test))
			{
				Stack holderStack = item.holderStack;
				if (!item.CanHoldInStack)
				{
					return ResultCode.ItemHoldBlocked;
				}
				if (myHolder.IsFlag(Flags.LinkedStacks) ? myHolder.IsFull : IsFull)
				{
					return ResultCode.HolderIsFull;
				}
				if (!CanAcceptObjectType(item.type))
				{
					return ResultCode.IncompatibleItemType;
				}
				if (item == Singleton.Manager<ManPointer>.inst.DraggingItem && ((passType & PassType.Drop) == 0 || myHolder.IsFlag(Flags.NoAcceptDrops)))
				{
					return ResultCode.ItemHeldByPlayer;
				}
				if (holderStack != null && !holderStack.myHolder.TestReleaseFilters(item, fromStack, this, passType))
				{
					return ResultCode.ReleaseFilterFail;
				}
			}
			else
			{
				d.Assert(item == null);
			}
			if (!myHolder.TestAcceptFilters(item, fromStack, this, passType))
			{
				return ResultCode.AcceptFilterFail;
			}
			return ResultCode.Success;
		}

		public bool CanAcceptObjectType(ObjectTypes type)
		{
			return (type != ObjectTypes.Block || (myHolder.m_AcceptFlags & AcceptFlags.Blocks) != 0) && (type != ObjectTypes.Chunk || (myHolder.m_AcceptFlags & AcceptFlags.Chunks) != 0);
		}

		public AcceptResult Take(Visible item, bool force = false, bool insertAtBase = true)
		{
			return Take(item, force, (!insertAtBase) ? int.MaxValue : 0, fromNetworkData: false);
		}

		public AcceptResult Take(Visible item, bool force, int insertPosition, bool fromNetworkData)
		{
			d.Assert(item != null, "null item");
			if (!myHolder.block.IsAttached)
			{
				d.Assert(condition: false, "holder not attached");
				return ResultCode.InvalidBlockState;
			}
			NetBlockChunk netBlockChunk = item.GetNetBlockChunk();
			if (netBlockChunk.IsNotNull() && !fromNetworkData && !netBlockChunk.NetIdentity.HasEffectiveAuthority())
			{
				return ResultCode.NoItemAuthority;
			}
			Stack holderStack = item.holderStack;
			Stack stack = this;
			if (!force)
			{
				PassType passType;
				if (item == Singleton.Manager<ManPointer>.inst.DraggingItem)
				{
					passType = PassType.Drop;
				}
				else if (holderStack == null)
				{
					passType = PassType.Pick;
				}
				else
				{
					passType = PassType.Pass;
					if (holderStack.myHolder.block.tank != myHolder.block.tank)
					{
						passType |= PassType.External;
					}
				}
				AcceptResult result = CanAccept(item, holderStack, passType);
				if (!result.IsSuccess)
				{
					return result;
				}
				if (myHolder.IsFlag(Flags.LinkedStacks) && IsFull)
				{
					StackIterator.Enumerator enumerator = myHolder.Stacks.GetEnumerator();
					while (enumerator.MoveNext())
					{
						Stack current = enumerator.Current;
						if (!current.IsFull)
						{
							stack = current;
							break;
						}
					}
				}
			}
			if (insertPosition < 0 || insertPosition >= stack.items.Count)
			{
				insertPosition = stack.items.Count;
			}
			stack.items.Insert(insertPosition, item);
			myHolder.NumContents++;
			myHolder.m_ContentsModificationIndex++;
			m_ReceivedOnHeartbeat = myHolder.block.tank.Holders.HeartbeatCount;
			item.SetHolder(stack, notifyRelease: true, isBeingRecycled: false, !fromNetworkData);
			myHolder.TakeItemEvent.Send(item, stack);
			return ResultCode.Success;
		}

		public void ReleaseAll()
		{
			if (items.Count <= 0)
			{
				return;
			}
			foreach (Visible item in items)
			{
				item.SetHolder(null, notifyRelease: false);
				myHolder.NumContents--;
				myHolder.ReleaseItemEvent.Send(item, this, null);
			}
			items.Clear();
			myHolder.m_ContentsModificationIndex++;
		}

		public void Release(Visible item, Stack toStack)
		{
			items.Remove(item);
			myHolder.NumContents--;
			myHolder.m_ContentsModificationIndex++;
			myHolder.ReleaseItemEvent.Send(item, this, toStack);
		}

		public TechHolders.OperationResult TryTakeOnHeartbeat(Visible item)
		{
			if (item.TakenThisHeartbeat)
			{
				return TechHolders.OperationResult.None;
			}
			AcceptResult acceptResult = Take(item);
			if (acceptResult.IsSuccess)
			{
				return TechHolders.OperationResult.Effect;
			}
			if (acceptResult.FailReason == ResultCode.HolderIsFull && (myHolder.m_CapacityPerStack != 1 || !FirstItem.TakenThisHeartbeat))
			{
				return TechHolders.OperationResult.Retry;
			}
			return TechHolders.OperationResult.None;
		}

		public void LockStackUserPickup()
		{
			m_StackLockTimer = Time.time + 1f;
		}

		public void Reset()
		{
			m_StackLockTimer = 0f;
		}
	}

	public interface IStackDirection
	{
		bool CanReceiveOn(Vector3 apLocal, Stack ownStack);

		bool CanOutputTo(Vector3 apLocal, Stack ownStack);
	}

	[Serializable]
	public class StackHandle
	{
		public Vector3 localPos = k_InvalidPos;

		[NonSerialized]
		public Stack stack;

		public static readonly Vector3 k_InvalidPos = Vector3.one * -1000f;

		public bool IsValidPos => localPos != k_InvalidPos;

		public void InitReference(ModuleItemHolder holder)
		{
			if (localPos == k_InvalidPos)
			{
				stack = null;
			}
			else
			{
				stack = holder.GetNearestStack(localPos, local: true);
			}
		}
	}

	[Serializable]
	public struct APOverrideCollection
	{
		public int[] indices;
	}

	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public int[][] contentsIDs;
	}

	public struct StackIterator
	{
		public struct Enumerator
		{
			private Stack[] stacks;

			private int index;

			public Stack Current => stacks[index];

			public Enumerator(Stack[] stacks)
			{
				this.stacks = stacks;
				index = -1;
			}

			public bool MoveNext()
			{
				index++;
				return index < stacks.Length;
			}
		}

		private Stack[] stacks;

		public StackIterator(Stack[] stacks)
		{
			this.stacks = stacks;
		}

		public Enumerator GetEnumerator()
		{
			return new Enumerator(stacks);
		}
	}

	[SerializeField]
	private Vector3[] m_OverrideBasePositons;

	[SerializeField]
	private APOverrideCollection[] m_OverrideAPConnections;

	[SerializeField]
	private Vector3 m_StackUpDir = Vector3.up;

	[SerializeField]
	private int m_CapacityPerStack = 10;

	[EnumFlag]
	[SerializeField]
	private Flags m_Flags;

	[EnumFlag]
	[SerializeField]
	private AcceptFlags m_AcceptFlags = AcceptFlags.Chunks;

	[SerializeField]
	private StackHandle m_DefaultDropStack;

	public ItemSearchHandler ItemRequestHandler;

	public const float k_DefaultPrePickupPeriod = 0.001f;

	public Event<Visible, Stack> TakeItemEvent;

	public Event<Visible, Stack, Stack> ReleaseItemEvent;

	public Event<int> PreDetachEvent;

	public Event<Stack, Stack, Vector3, Vector3> StackConnectEvent;

	public Event<Stack, Stack, bool> StackDisconnectEvent;

	[SerializeField]
	[HideInInspector]
	private Stack[] m_Stacks;

	[HideInInspector]
	[SerializeField]
	private float m_HorizontalBoundsRadius;

	private int m_ContentsModificationIndex;

	private List<AllowAcceptItemFunc> m_AcceptFilterCallback;

	private List<AllowReleaseItemFunc> m_ReleaseFilterCallback;

	private static List<int> s_SerialHeldItemIDs;

	private static List<int[]> s_SerialHeldItemArrays;

	private static readonly Stack[] s_TempSwapStackArray;

	private static List<Visible> s_ItemSortWorking;

	private Bitfield<int> m_AnonItemTakenFlags = new Bitfield<int>();

	private int m_AnonItemTimeStamp;

	private int m_ModificationIndexLastStackBalance;

	private static Comparison<Visible> s_ItemSortComparer;

	public int NumContents { get; private set; }

	public int NumStacks => m_Stacks.Length;

	public bool IsEmpty => NumContents == 0;

	public bool IsFull => NumContents >= m_CapacityPerStack * m_Stacks.Length;

	public AcceptFlags Acceptance => m_AcceptFlags;

	public Stack SingleStack
	{
		get
		{
			d.Assert(m_Stacks.Length == 1, "expected one stack only");
			return m_Stacks[0];
		}
	}

	public StackIterator Stacks => new StackIterator(m_Stacks);

	public Stack.ItemIterator Contents => GetContentsIterator();

	public ModuleItemConveyor Conveyor { get; private set; }

	public ModuleAntenna Antenna { get; private set; }

	public ModuleItemStore ItemStore { get; private set; }

	public float HorizontalBoundsRadius => m_HorizontalBoundsRadius;

	public int PickupPriority { get; set; }

	public bool HasPickupContentionPeriod => PickupContentionPeriod > 0.001f;

	public float PickupContentionPeriod { get; set; }

	public Vector3 UpDir => m_StackUpDir;

	public int RequestVisitedTimestamp { get; set; }

	public bool IsFlag(Flags flag)
	{
		return (m_Flags & flag) != 0;
	}

	public void DropAll()
	{
		for (int i = 0; i < m_Stacks.Length; i++)
		{
			m_Stacks[i].ReleaseAll();
		}
		d.AssertFormat(NumContents == 0, this, "DropAll failed to release all items!? {0}", base.block);
		NumContents = 0;
	}

	public Stack GetStack(int i)
	{
		if (i >= m_Stacks.Length)
		{
			return null;
		}
		return m_Stacks[i];
	}

	public Stack GetNearestStack(Vector3 position, bool local = false)
	{
		float num = Globals.inst.m_TankHolderDropStackTolerance;
		Stack result = m_DefaultDropStack.stack;
		for (int i = 0; i < m_Stacks.Length; i++)
		{
			Stack stack = m_Stacks[i];
			float sqrMagnitude = ((local ? stack.basePos : stack.BasePosWorld()) - position).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				num = sqrMagnitude;
				result = stack;
			}
		}
		return result;
	}

	private Stack.ItemIterator GetContentsIterator()
	{
		if (m_Stacks == null || m_Stacks.Length == 0)
		{
			return default(Stack.ItemIterator);
		}
		return new Stack.ItemIterator(m_Stacks[0], 0, forceLinked: true);
	}

	public void OfferAllItemsToCollector(ItemSearchCollector collector)
	{
		int num = m_Stacks.Length;
		for (int i = 0; i < num; i++)
		{
			m_Stacks[i].OfferAllItemsToCollector(collector);
		}
	}

	public IEnumerable<Vector3> EditorEnumerateBasePositions()
	{
		if (m_OverrideBasePositons != null && m_OverrideBasePositons.Length != 0)
		{
			for (int i = 0; i < m_OverrideBasePositons.Length; i++)
			{
				yield return m_OverrideBasePositons[i];
			}
			yield break;
		}
		IntVector3[] filledCells = GetComponent<TankBlock>().filledCells;
		for (int i = 0; i < filledCells.Length; i++)
		{
			yield return filledCells[i];
		}
	}

	public void SetAcceptFilterCallback(AllowAcceptItemFunc canAcceptFunc, bool addFirst = false)
	{
		if (m_AcceptFilterCallback == null)
		{
			m_AcceptFilterCallback = new List<AllowAcceptItemFunc>();
		}
		if (addFirst)
		{
			m_AcceptFilterCallback.Insert(0, canAcceptFunc);
		}
		else
		{
			m_AcceptFilterCallback.Add(canAcceptFunc);
		}
	}

	public void SetReleaseFilterCallback(AllowReleaseItemFunc checkCanReleaseFunc, bool addFirst = false)
	{
		if (m_ReleaseFilterCallback == null)
		{
			m_ReleaseFilterCallback = new List<AllowReleaseItemFunc>();
		}
		if (addFirst)
		{
			m_ReleaseFilterCallback.Insert(0, checkCanReleaseFunc);
		}
		else
		{
			m_ReleaseFilterCallback.Add(checkCanReleaseFunc);
		}
	}

	public void SetPrePickupFn(Func<Visible, bool> fn, Stack specificStack)
	{
		if (specificStack != null)
		{
			specificStack.isPrePickupFn = fn;
			return;
		}
		Stack[] stacks = m_Stacks;
		for (int i = 0; i < stacks.Length; i++)
		{
			stacks[i].isPrePickupFn = fn;
		}
	}

	public bool CheckAnonItemTaken(int index)
	{
		ClearAnonItemsOnHeartbeat();
		if (index < 32)
		{
			return m_AnonItemTakenFlags.Contains(index);
		}
		return false;
	}

	public void SetAnonItemTaken(int index)
	{
		ClearAnonItemsOnHeartbeat();
		if (index < 32)
		{
			m_AnonItemTakenFlags.Set(index, enabled: true);
		}
	}

	public void OverrideStackCapacity(int capacity)
	{
		m_CapacityPerStack = capacity;
	}

	static ModuleItemHolder()
	{
		s_SerialHeldItemIDs = new List<int>();
		s_SerialHeldItemArrays = new List<int[]>();
		s_TempSwapStackArray = new Stack[4];
		s_ItemSortWorking = new List<Visible>();
		s_ItemSortComparer = ItemSortComparer;
		d.Assert(EnumValuesIterator<BlockTypes>.Count < 2048 && EnumValuesIterator<ChunkTypes>.Count < 2048, "ItemSortComparer() doesn't support item types greater than 2^11");
	}

	private int CompareAPsByAngle(int apIndexA, int apIndexB, Vector3 basePos)
	{
		int num = (Mathf.RoundToInt((base.block.attachPoints[apIndexA] - basePos).HorizontalAngle()) + 360) % 360;
		int num2 = (Mathf.RoundToInt((base.block.attachPoints[apIndexB] - basePos).HorizontalAngle()) + 360) % 360;
		return num - num2;
	}

	private static int ItemSortComparer(Visible a, Visible b)
	{
		return ((a.ItemType << 20) | (a.ID & 0xFFFFF)) - ((b.ItemType << 20) | (b.ID & 0xFFFFF));
	}

	private void BalanceLinkedStacks()
	{
		if (m_ModificationIndexLastStackBalance == m_ContentsModificationIndex)
		{
			return;
		}
		m_ModificationIndexLastStackBalance = m_ContentsModificationIndex;
		int i;
		for (i = 0; i < m_Stacks.Length && m_Stacks[i].NumItems <= 1; i++)
		{
		}
		if (i == m_Stacks.Length)
		{
			return;
		}
		s_ItemSortWorking.Clear();
		Stack[] stacks = m_Stacks;
		foreach (Stack stack in stacks)
		{
			s_ItemSortWorking.AddRange(stack.items);
			stack.items.Clear();
		}
		if (IsFlag(Flags.SortLinkedStacks))
		{
			s_ItemSortWorking.Sort(s_ItemSortComparer);
		}
		i = -1;
		int num = 0;
		for (int k = 0; k < s_ItemSortWorking.Count; k++)
		{
			int num2 = s_ItemSortWorking.Count - k;
			if (num == 0)
			{
				i++;
				int num3 = m_Stacks.Length - i;
				num = Mathf.CeilToInt((float)num2 / (float)num3);
			}
			num--;
			Visible visible = s_ItemSortWorking[k];
			Stack stack2 = m_Stacks[i];
			stack2.items.Add(visible);
			bool inBeam = visible.InBeam;
			visible.SetHolder(stack2, notifyRelease: false);
			visible.InBeam = inBeam;
		}
		m_ContentsModificationIndex++;
	}

	private bool TestAcceptFilters(Visible item, Stack fromStack, Stack toStack, PassType passType)
	{
		if (m_AcceptFilterCallback != null)
		{
			foreach (AllowAcceptItemFunc item2 in m_AcceptFilterCallback)
			{
				if (!item2(item, fromStack, toStack, passType))
				{
					return false;
				}
			}
		}
		return true;
	}

	private bool TestReleaseFilters(Visible item, Stack fromStack, Stack toStack, PassType passType)
	{
		if (m_ReleaseFilterCallback != null)
		{
			foreach (AllowReleaseItemFunc item2 in m_ReleaseFilterCallback)
			{
				if (!item2(item, fromStack, toStack, passType))
				{
					return false;
				}
			}
		}
		return true;
	}

	public int GetTotalCapacityForLimiter()
	{
		int num = 0;
		num = ((m_Stacks != null) ? m_Stacks.Length : ((m_OverrideBasePositons.Length == 0) ? (base.block.IsNotNull() ? base.block : GetComponent<TankBlock>()).filledCells.Length : m_OverrideBasePositons.Length));
		return num * m_CapacityPerStack;
	}

	private void OnAttached()
	{
		base.block.tank.Holders.Register(this);
		if (IsFlag(Flags.LinkedStacks))
		{
			base.block.tank.Holders.HBEvent.Subscribe(OnHeartbeat);
		}
		for (int i = 0; i < m_Stacks.Length; i++)
		{
			Stack stack = m_Stacks[i];
			for (int j = 0; j < stack.apConnectionIndices.Length; j++)
			{
				int num = stack.apConnectionIndices[j];
				TankBlock tankBlock = base.block.ConnectedBlocksByAP[num];
				ModuleItemHolder moduleItemHolder = (tankBlock.IsNotNull() ? tankBlock.GetComponent<ModuleItemHolder>() : null);
				if (moduleItemHolder.IsNull())
				{
					continue;
				}
				Vector3 vector = base.block.attachPoints[num];
				Vector3 a = base.block.trans.TransformPoint(vector);
				for (int k = 0; k < moduleItemHolder.m_Stacks.Length; k++)
				{
					Stack stack2 = moduleItemHolder.m_Stacks[k];
					for (int l = 0; l < stack2.apConnectionIndices.Length; l++)
					{
						int num2 = stack2.apConnectionIndices[l];
						Vector3 vector2 = tankBlock.attachPoints[num2];
						Vector3 b = tankBlock.trans.TransformPoint(vector2);
						if (a.EqualsEpsilon(b))
						{
							stack.connectedNeighbourStacks[j] = stack2;
							stack2.connectedNeighbourStacks[l] = stack;
							stack2.myHolder.StackConnectEvent.Send(stack2, stack, vector2, vector);
							StackConnectEvent.Send(stack, stack2, vector, vector2);
						}
					}
				}
			}
		}
	}

	private void OnDetaching()
	{
		base.block.tank.Holders.Unregister(this);
		if (IsFlag(Flags.LinkedStacks))
		{
			base.block.tank.Holders.HBEvent.Unsubscribe(OnHeartbeat);
		}
		PreDetachEvent.Send(0);
		if (!ManSaveGame.Storing)
		{
			DropAll();
		}
		bool paramC = true;
		for (int i = 0; i < m_Stacks.Length; i++)
		{
			Stack stack = m_Stacks[i];
			Stack[] connectedNeighbourStacks = stack.connectedNeighbourStacks;
			stack.connectedNeighbourStacks = s_TempSwapStackArray;
			foreach (Stack stack2 in connectedNeighbourStacks)
			{
				if (stack2 != null)
				{
					stack2.ClearConnectedStack(stack);
					StackDisconnectEvent.Send(stack, stack2, paramC);
					stack2.myHolder.StackDisconnectEvent.Send(stack2, stack, paramC: false);
					paramC = false;
				}
			}
			stack.connectedNeighbourStacks = connectedNeighbourStacks;
			Array.Clear(stack.connectedNeighbourStacks, 0, stack.connectedNeighbourStacks.Length);
		}
	}

	private void OnHeartbeat(int hbCount, TechHolders.Heartbeat hbStep)
	{
		if (hbStep == TechHolders.Heartbeat.PostPass && IsFlag(Flags.LinkedStacks))
		{
			BalanceLinkedStacks();
		}
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			s_SerialHeldItemArrays.Clear();
			for (int i = 0; i < m_Stacks.Length; i++)
			{
				Stack stack = m_Stacks[i];
				s_SerialHeldItemIDs.Clear();
				for (int j = 0; j < stack.items.Count; j++)
				{
					s_SerialHeldItemIDs.Add(stack.items[j].ID);
				}
				s_SerialHeldItemArrays.Add(s_SerialHeldItemIDs.ToArray());
			}
			SerialData serialData = new SerialData();
			serialData.contentsIDs = s_SerialHeldItemArrays.ToArray();
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 == null || !ManNetwork.IsHost)
		{
			return;
		}
		int num = 0;
		for (int k = 0; k < serialData2.contentsIDs.Length; k++)
		{
			int[] array = serialData2.contentsIDs[k];
			Stack stack2 = m_Stacks[num++];
			foreach (int num2 in array)
			{
				Visible visible = Singleton.Manager<ManSaveGame>.inst.LookupSerializedVisible(num2);
				if (!visible)
				{
					d.LogError(base.name + " serializing held item, lookup failed: " + num2);
					continue;
				}
				if (visible.holderStack == null)
				{
					stack2.Take(visible, force: true);
					continue;
				}
				string text = $"{base.block.name} ({base.block.cachedLocalPosition})";
				if (base.block.IsAttached)
				{
					text = $"{base.block.tank.name}:({text})";
				}
				TankBlock tankBlock = visible.holderStack.myHolder.block;
				string text2 = $"{tankBlock.name} ({tankBlock.cachedLocalPosition})";
				if (tankBlock.IsAttached)
				{
					text2 = $"{tankBlock.tank.name}:({text2})";
				}
				d.LogErrorFormat("ModuleItemHolder Deserialize - {0} is trying to Take() previously held item {1} but item is already held by {2}", text, visible.name, text2);
			}
		}
	}

	private void ClearAnonItemsOnHeartbeat()
	{
		bool flag;
		if (base.block.IsAttached)
		{
			flag = base.block.tank.Holders.HeartbeatCount != m_AnonItemTimeStamp;
			if (flag)
			{
				m_AnonItemTimeStamp = base.block.tank.Holders.HeartbeatCount;
			}
		}
		else
		{
			flag = true;
		}
		if (flag)
		{
			m_AnonItemTakenFlags.Clear();
		}
	}

	private void PrePool()
	{
		List<Stack> list = new List<Stack>();
		List<Vector3> list2 = new List<Vector3>();
		if (m_OverrideBasePositons.Length != 0)
		{
			list2.AddRange(m_OverrideBasePositons);
		}
		else
		{
			IntVector3[] filledCells = base.block.filledCells;
			foreach (IntVector3 intVector in filledCells)
			{
				list2.Add(intVector);
			}
		}
		List<int> list3 = new List<int>();
		for (int j = 0; j < list2.Count; j++)
		{
			list3.Clear();
			Vector3 basePos = list2[j];
			d.AssertFormat(!list.Any((Stack s) => (s.basePos.ToVector2XZ() - basePos.ToVector2XZ()).magnitude < 0.5f), "overlapping stacks in {0}", base.block.name);
			bool flag = false;
			if (m_OverrideAPConnections.Length == 0)
			{
				for (int num = 0; num < base.block.attachPoints.Length; num++)
				{
					Vector3 input = base.block.attachPoints[num] - basePos;
					if (input.sqrMagnitude < 1f && input.SetY(0f).sqrMagnitude > 0.1f)
					{
						list3.Add(num);
					}
				}
			}
			else if (m_OverrideAPConnections.Length > j)
			{
				for (int num2 = 0; num2 < m_OverrideAPConnections[j].indices.Length; num2++)
				{
					list3.Add(m_OverrideAPConnections[j].indices[num2]);
				}
				flag = true;
			}
			if (!flag)
			{
				list3.Sort((int a, int b) => CompareAPsByAngle(a, b, basePos));
			}
			list.Add(new Stack
			{
				basePos = basePos,
				apConnectionIndices = list3.ToArray()
			});
		}
		m_Stacks = list.ToArray();
		Bounds bounds = new Bounds(Vector3.zero, -Vector3.one);
		for (int num3 = 0; num3 < base.block.filledCells.Length; num3++)
		{
			Vector3 vector = base.block.filledCells[num3];
			Vector3 vector2 = base.transform.localPosition + base.transform.localRotation * vector;
			if (bounds.size == -Vector3.one)
			{
				bounds = new Bounds(vector2, Vector3.zero);
			}
			else
			{
				bounds.Encapsulate(vector2);
			}
		}
		m_HorizontalBoundsRadius = ((bounds.size + Vector3.one) * 0.5f).SetY(0f).magnitude;
		m_StackUpDir.Normalize();
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialize);
		for (int i = 0; i < m_Stacks.Length; i++)
		{
			m_Stacks[i].myHolder = this;
			m_Stacks[i].items = new List<Visible>();
			m_Stacks[i].connectedNeighbourStacks = new Stack[m_Stacks[i].apConnectionIndices.Length];
		}
		m_DefaultDropStack.InitReference(this);
		Conveyor = GetComponent<ModuleItemConveyor>();
		Antenna = GetComponent<ModuleAntenna>();
		ItemStore = GetComponent<ModuleItemStore>();
	}

	private void OnSpawn()
	{
		DropAll();
		for (int i = 0; i < m_Stacks.Length; i++)
		{
			Stack stack = m_Stacks[i];
			stack.Reset();
			Array.Clear(stack.connectedNeighbourStacks, 0, stack.connectedNeighbourStacks.Length);
		}
	}

	private void OnRecycle()
	{
		DropAll();
	}

	private void OnDrawGizmosSelected()
	{
		TankBlock tankBlock = ((base.block != null) ? base.block : GetComponent<TankBlock>());
		int num = 0;
		Vector3 vector = base.transform.TransformVector(m_StackUpDir);
		bool flag = m_OverrideBasePositons != null && m_OverrideBasePositons.Length != 0;
		int num2 = (flag ? m_OverrideBasePositons.Length : tankBlock.filledCells.Length);
		for (int i = 0; i < num2; i++)
		{
			Vector3 vector2 = (flag ? m_OverrideBasePositons[i] : ((Vector3)tankBlock.filledCells[i]));
			Gizmos.color = Color.cyan;
			Vector3 vector3 = base.transform.TransformPoint(vector2);
			Gizmos.DrawWireCube(vector3, Vector3.one * 0.1f);
			DebugUtil.GizmosDrawArrow(vector3, vector3 + vector, 0.1f, 75f);
			if (m_OverrideAPConnections.Length == 0)
			{
				for (int j = 0; j < tankBlock.attachPoints.Length; j++)
				{
					Vector3 input = tankBlock.attachPoints[j] - vector2;
					if (input.sqrMagnitude < 1f && input.SetY(0f).sqrMagnitude > 0.1f)
					{
						DebugUtil.GizmosDrawArrow(vector3, base.transform.TransformPoint(tankBlock.attachPoints[j]), 0.1f);
					}
				}
			}
			else
			{
				for (int k = 0; k < m_OverrideAPConnections[num].indices.Length; k++)
				{
					int num3 = m_OverrideAPConnections[num].indices[k];
					DebugUtil.GizmosDrawArrow(vector3, base.transform.TransformPoint(tankBlock.attachPoints[num3]), 0.1f);
				}
			}
			if (Application.isPlaying)
			{
				Gizmos.color = Color.magenta;
				Stack stack = m_Stacks[num];
				for (int l = 0; l < stack.connectedNeighbourStacks.Length; l++)
				{
					Stack stack2 = stack.connectedNeighbourStacks[l];
					if (stack2 != null)
					{
						DebugUtil.GizmosDrawArrow(vector3, stack2.myHolder.transform.TransformPoint(stack2.basePos));
					}
				}
			}
			num++;
		}
	}

	public ModuleItemHolder()
	{
		PickupPriority = int.MaxValue;
		PickupContentionPeriod = 0.001f;
	}
}
