#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class TechHolders : TechComponent, INetworkedTechComponent
{
	public enum Heartbeat
	{
		PrePass,
		PostPass
	}

	public struct HolderIterator
	{
		private List<ModuleItemHolder> holders;

		private int index;

		public ModuleItemHolder Current => holders[index];

		public HolderIterator(List<ModuleItemHolder> holders)
		{
			this.holders = holders;
			index = -1;
		}

		public bool MoveNext()
		{
			index++;
			return index < holders.Count;
		}
	}

	private struct Operation
	{
		public int holderID;

		public Func<OperationResult> opFunc;

		public Operation(int id, Func<OperationResult> func)
		{
			holderID = id;
			opFunc = func;
		}
	}

	public enum OperationResult
	{
		None,
		Effect,
		Retry,
		EffectRetry
	}

	private struct StackArrow
	{
		public int fromToHash;

		public ModuleItemHolder.Stack from;

		public int connectionIndex;

		public bool isPullArrow;

		public Transform geometry;

		public Transform prefab;

		public int priority;

		public StackArrow(ModuleItemHolder.Stack from, int connectionIndex, bool isPullArrow, Transform geometry, Transform prefab, int priority)
		{
			this.from = from;
			this.connectionIndex = connectionIndex;
			this.isPullArrow = isPullArrow;
			this.geometry = geometry;
			this.prefab = prefab;
			this.priority = priority;
			fromToHash = FromToHash(from, connectionIndex);
		}
	}

	private class ItemPickupDatabase
	{
		private Dictionary<int, float> m_ItemPickupTimes = new Dictionary<int, float>();

		private List<int> m_IDsToRemove = new List<int>();

		private float m_ForgetAfterTime;

		private float m_ForgetAfterTimeStamp;

		public ItemPickupDatabase(float forgetAfterTime)
		{
			m_ForgetAfterTime = forgetAfterTime;
		}

		public void ClearOldTimestamps()
		{
			if (Time.time - m_ForgetAfterTimeStamp < m_ForgetAfterTime)
			{
				return;
			}
			m_ForgetAfterTimeStamp = Time.time;
			m_IDsToRemove.Clear();
			Dictionary<int, float>.Enumerator enumerator = m_ItemPickupTimes.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Value > m_ForgetAfterTimeStamp)
				{
					m_IDsToRemove.Add(enumerator.Current.Key);
				}
			}
			foreach (int item in m_IDsToRemove)
			{
				m_ItemPickupTimes.Remove(item);
			}
		}

		public void ClearAll()
		{
			m_ItemPickupTimes.Clear();
			m_IDsToRemove.Clear();
		}

		public float GetItemPickupTime(int itemID)
		{
			if (!m_ItemPickupTimes.TryGetValue(itemID, out var value))
			{
				return -1f;
			}
			return value;
		}

		public void SetItemPickupTime(int itemID, float time)
		{
			m_ItemPickupTimes[itemID] = time;
		}

		public void ClearItemPickupTime(int itemID)
		{
			m_ItemPickupTimes.Remove(itemID);
		}
	}

	public enum HeartbeatSpeed
	{
		Paused,
		Slow,
		Normal,
		Fast,
		Vendor
	}

	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public float m_HeartbeatInterval;
	}

	[SerializeField]
	private float m_FlowArrowHeightOffset = 0.2f;

	[SerializeField]
	private float m_ItemPickupForgetTime = 10f;

	[SerializeField]
	[EnumArray(typeof(HeartbeatSpeed))]
	private float[] m_HeartbeatMultiplier;

	public Event<int, Heartbeat> HBEvent;

	public EventNoParams CraftingSetupChanged;

	public Event<Tank, Visible> ItemPickupEvent;

	public Event<Tank, Visible> ItemReleaseEvent;

	private List<ModuleItemHolder> m_Holders = new List<ModuleItemHolder>(10);

	private List<ModulePacemaker> m_Pacemakers = new List<ModulePacemaker>();

	private List<KeyValuePair<int, PooledLinkedList<Operation>>> m_PassingFuncsByPriority = new List<KeyValuePair<int, PooledLinkedList<Operation>>>();

	private bool m_PassingFuncsByPriorityNeedSort;

	private PooledLinkedList<Operation> m_ActionList = new PooledLinkedList<Operation>();

	private HashSet<int> m_HolderIDsUnregistered = new HashSet<int>();

	private float m_HeartbeatTime;

	private float m_CurrentHeartbeatInterval;

	private Dictionary<int, ModuleItemHolder.Stack> m_RequestedItems = new Dictionary<int, ModuleItemHolder.Stack>();

	private Dictionary<int, LinkedListNode<StackArrow>> m_AllStackArrows = new Dictionary<int, LinkedListNode<StackArrow>>();

	private PooledLinkedList<StackArrow> m_StackArrowsToUpdate = new PooledLinkedList<StackArrow>();

	private PooledLinkedList<StackArrow> m_StackArrowsUpdated = new PooledLinkedList<StackArrow>();

	private Transform m_StackArrowParent;

	private ItemPickupDatabase m_ItemPickupDB;

	private static readonly OperationResult[,] s_ResultCombineTable = new OperationResult[4, 4]
	{
		{
			OperationResult.None,
			OperationResult.Effect,
			OperationResult.Retry,
			OperationResult.EffectRetry
		},
		{
			OperationResult.Effect,
			OperationResult.Effect,
			OperationResult.EffectRetry,
			OperationResult.EffectRetry
		},
		{
			OperationResult.Retry,
			OperationResult.EffectRetry,
			OperationResult.Retry,
			OperationResult.EffectRetry
		},
		{
			OperationResult.EffectRetry,
			OperationResult.EffectRetry,
			OperationResult.EffectRetry,
			OperationResult.EffectRetry
		}
	};

	public int HeartbeatCount { get; private set; }

	public float NextHeartBeatTime => m_HeartbeatTime;

	public float CurrentHeartbeatInterval => m_CurrentHeartbeatInterval;

	public static OperationResult CombineOperationResults(OperationResult a, OperationResult b)
	{
		return s_ResultCombineTable[(int)a, (int)b];
	}

	public void Register(ModuleItemHolder holder)
	{
		m_Holders.Add(holder);
	}

	public void Unregister(ModuleItemHolder holder)
	{
		m_Holders.Remove(holder);
	}

	public void AddPacemaker(ModulePacemaker pacemaker)
	{
		m_Pacemakers.Add(pacemaker);
	}

	public void RemovePacemaker(ModulePacemaker pacemaker)
	{
		m_Pacemakers.Remove(pacemaker);
		if (ManNetwork.IsHost && m_Pacemakers.Count == 0)
		{
			SetHeartbeatSpeed(HeartbeatSpeed.Normal);
		}
	}

	public HolderIterator GetEnumerator()
	{
		return new HolderIterator(m_Holders);
	}

	public void RegisterOperation(ModuleItemHolder holder, Func<OperationResult> func, int priority)
	{
		PooledLinkedList<Operation> pooledLinkedList = null;
		foreach (KeyValuePair<int, PooledLinkedList<Operation>> item in m_PassingFuncsByPriority)
		{
			if (item.Key == priority)
			{
				pooledLinkedList = item.Value;
				break;
			}
		}
		if (pooledLinkedList == null)
		{
			pooledLinkedList = new PooledLinkedList<Operation>();
			m_PassingFuncsByPriority.Add(new KeyValuePair<int, PooledLinkedList<Operation>>(priority, pooledLinkedList));
			m_PassingFuncsByPriorityNeedSort = true;
		}
		if (m_HolderIDsUnregistered.Contains(holder.block.visible.ID))
		{
			foreach (KeyValuePair<int, PooledLinkedList<Operation>> item2 in m_PassingFuncsByPriority)
			{
				LinkedListNode<Operation> linkedListNode = item2.Value.First;
				while (linkedListNode != null)
				{
					if (linkedListNode.Value.holderID == holder.block.visible.ID)
					{
						LinkedListNode<Operation> next = linkedListNode.Next;
						item2.Value.Remove(linkedListNode);
						linkedListNode = next;
					}
					else
					{
						linkedListNode = linkedListNode.Next;
					}
				}
			}
			m_HolderIDsUnregistered.Remove(holder.block.visible.ID);
		}
		pooledLinkedList.Add(new Operation(holder.block.visible.ID, func));
	}

	public void UnregisterOperations(ModuleItemHolder holder)
	{
		m_HolderIDsUnregistered.Add(holder.block.visible.ID);
	}

	public void RequestSetHeartbeatSpeed(HeartbeatSpeed speed)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.SetHeartbeatSpeed, new SetHeartbeatSpeedMessage
			{
				m_Speed = speed
			}, base.Tech.netTech.netId);
		}
		else
		{
			SetHeartbeatSpeed(speed);
		}
	}

	public void SetHeartbeatSpeed(HeartbeatSpeed speed)
	{
		float heartbeatInterval = 0f;
		if (m_HeartbeatMultiplier[(int)speed] != 0f)
		{
			heartbeatInterval = Globals.inst.m_TankHolderHeartbeatInterval / m_HeartbeatMultiplier[(int)speed];
		}
		SetHeartbeatInterval(heartbeatInterval);
	}

	public void SetHeartbeatInterval(float interval)
	{
		if (m_CurrentHeartbeatInterval != 0f)
		{
			float num = (m_HeartbeatTime - Time.time) / m_CurrentHeartbeatInterval;
			m_HeartbeatTime -= num * m_CurrentHeartbeatInterval;
			m_HeartbeatTime += num * interval;
		}
		else
		{
			m_HeartbeatTime = Time.time + interval;
		}
		m_CurrentHeartbeatInterval = interval;
	}

	public void DoHeartbeat()
	{
		if (!ManNetwork.IsHost)
		{
			m_HeartbeatTime = float.PositiveInfinity;
		}
		else if (Time.time > m_HeartbeatTime && m_CurrentHeartbeatInterval != 0f)
		{
			m_HeartbeatTime += m_CurrentHeartbeatInterval;
			if ((bool)Singleton.Manager<ManPointer>.inst.DraggingItem && Singleton.Manager<ManPointer>.inst.DraggingItem.holderStack != null && Singleton.Manager<ManPointer>.inst.DraggingItem.holderStack.myHolder.block.tank == base.Tech)
			{
				Singleton.Manager<ManPointer>.inst.DraggingItem.SetHolder(null);
			}
			HeartbeatCount++;
			HBEvent.Send(HeartbeatCount, Heartbeat.PrePass);
			DoPassingLogic();
			HBEvent.Send(HeartbeatCount, Heartbeat.PostPass);
			m_RequestedItems.Clear();
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				base.Tech.netTech.SetTechComponentDirty(this);
			}
		}
	}

	public bool IsItemRequested(Visible item)
	{
		return m_RequestedItems.ContainsKey(item.ID);
	}

	public ModuleItemHolder.Stack GetRequestedItemNextHop(Visible item)
	{
		if (item != null)
		{
			m_RequestedItems.TryGetValue(item.ID, out var value);
			return value;
		}
		return null;
	}

	public void SetItemRequested(Visible item, ModuleItemHolder.Stack stackToPassTo)
	{
		if ((bool)item)
		{
			if (!m_RequestedItems.ContainsKey(item.ID))
			{
				m_RequestedItems.Add(item.ID, stackToPassTo);
				return;
			}
			d.LogErrorFormat("TechHolder.SetItemRequested called more than once for ID {0}", item.ID);
		}
	}

	public float GetItemPickupTime(int itemID)
	{
		return m_ItemPickupDB.GetItemPickupTime(itemID);
	}

	public void SetItemPickupTime(int itemID, float time = -1f)
	{
		m_ItemPickupDB.SetItemPickupTime(itemID, (time != -1f) ? time : Time.time);
	}

	public void ClearItemPickupTime(int itemID)
	{
		m_ItemPickupDB.ClearItemPickupTime(itemID);
	}

	private static int FromToHash(ModuleItemHolder.Stack stack, int connectionIndex)
	{
		return stack.GetHashCode() ^ connectionIndex;
	}

	public void UpdateStackArrow(ModuleItemHolder.Stack stack, int connectionIndex, bool isPullArrow, Transform arrowPrefab, int priority)
	{
		int key = FromToHash(stack, connectionIndex);
		if (!m_AllStackArrows.TryGetValue(key, out var value))
		{
			Transform transform = SpawnStackArrow(stack, connectionIndex, isPullArrow, arrowPrefab);
			if ((bool)transform)
			{
				value = m_StackArrowsUpdated.AllocateNode(new StackArrow(stack, connectionIndex, isPullArrow, transform, arrowPrefab, priority));
				m_StackArrowsUpdated.AddNode(value);
				m_AllStackArrows.Add(key, value);
			}
		}
		else if (m_StackArrowsUpdated.ContainsNode(value))
		{
			if (priority > value.Value.priority)
			{
				Transform transform2 = SpawnStackArrow(stack, connectionIndex, isPullArrow, arrowPrefab);
				if ((bool)transform2)
				{
					value.Value.geometry.Recycle();
					value.Value = new StackArrow(stack, connectionIndex, isPullArrow, transform2, arrowPrefab, priority);
				}
			}
		}
		else
		{
			if (priority < value.Value.priority)
			{
				return;
			}
			if (value.Value.prefab != arrowPrefab)
			{
				Transform transform3 = SpawnStackArrow(stack, connectionIndex, isPullArrow, arrowPrefab);
				if ((bool)transform3)
				{
					value.Value.geometry.Recycle();
					value.Value = new StackArrow(stack, connectionIndex, isPullArrow, transform3, arrowPrefab, priority);
				}
			}
			m_StackArrowsToUpdate.RemoveNode(value);
			m_StackArrowsUpdated.AddNode(value);
		}
	}

	private void DoPassingLogic()
	{
		m_ActionList.Clear();
		if (m_PassingFuncsByPriorityNeedSort)
		{
			m_PassingFuncsByPriority.Sort((KeyValuePair<int, PooledLinkedList<Operation>> a, KeyValuePair<int, PooledLinkedList<Operation>> b) => b.Key - a.Key);
			m_PassingFuncsByPriorityNeedSort = false;
		}
		foreach (KeyValuePair<int, PooledLinkedList<Operation>> item in m_PassingFuncsByPriority)
		{
			LinkedListNode<Operation> linkedListNode = item.Value.First;
			while (linkedListNode != null)
			{
				if (m_HolderIDsUnregistered.Contains(linkedListNode.Value.holderID))
				{
					LinkedListNode<Operation> next = linkedListNode.Next;
					item.Value.Remove(linkedListNode);
					linkedListNode = next;
				}
				else
				{
					m_ActionList.Add(linkedListNode.Value);
					linkedListNode = linkedListNode.Next;
				}
			}
		}
		m_HolderIDsUnregistered.Clear();
		bool flag = true;
		int num = 0;
		while (flag)
		{
			flag = false;
			OperationResult operationResult = OperationResult.None;
			LinkedListNode<Operation> linkedListNode2 = m_ActionList.First;
			while (linkedListNode2 != null)
			{
				OperationResult operationResult2 = linkedListNode2.Value.opFunc();
				LinkedListNode<Operation> node = linkedListNode2;
				linkedListNode2 = linkedListNode2.Next;
				if (operationResult2 != OperationResult.Retry && operationResult2 != OperationResult.EffectRetry)
				{
					m_ActionList.Remove(node);
				}
				if (!flag)
				{
					if (operationResult2 == OperationResult.EffectRetry)
					{
						flag = true;
					}
					else if (operationResult == OperationResult.None && operationResult2 == OperationResult.Retry)
					{
						operationResult = OperationResult.Retry;
					}
					else if (operationResult == OperationResult.Retry && operationResult2 == OperationResult.Effect)
					{
						flag = true;
					}
				}
			}
			int num2 = 1000;
			if (num >= num2)
			{
				string text = ((m_Holders.Count > 0) ? m_Holders[0].block.tank.name : "Unknown");
				d.LogError("ERROR: TechHolders.DoPassingLogic() isn't completing on tech " + text);
				flag = false;
			}
			num++;
		}
	}

	private void ClearRegisteredOperations()
	{
		foreach (KeyValuePair<int, PooledLinkedList<Operation>> item in m_PassingFuncsByPriority)
		{
			item.Value.Clear();
		}
		m_PassingFuncsByPriority.Clear();
		m_HolderIDsUnregistered.Clear();
	}

	private Transform SpawnStackArrow(ModuleItemHolder.Stack stack, int connectionIndex, bool isPullArrow, Transform arrowPrefab)
	{
		Transform transform = null;
		if ((bool)arrowPrefab)
		{
			transform = arrowPrefab.Spawn(m_StackArrowParent, Vector3.zero, Quaternion.identity);
			MoveStackArrow(stack, connectionIndex, isPullArrow, transform);
		}
		return transform;
	}

	private void MoveStackArrow(ModuleItemHolder.Stack stack, int connectionIndex, bool isPullArrow, Transform arrowToModify)
	{
		if (stack == null || connectionIndex < 0 || connectionIndex >= stack.apConnectionIndices.Length)
		{
			d.LogError("MoveStackArrow - StackArrow has null from or to stack!");
			return;
		}
		ModuleItemHolder myHolder = stack.myHolder;
		int num = stack.apConnectionIndices[connectionIndex];
		Vector3 vector = myHolder.block.attachPoints[num];
		Vector3 rhs = vector - stack.basePos;
		rhs -= myHolder.UpDir * Vector3.Dot(myHolder.UpDir, rhs);
		Vector3 vector2 = ((Mathf.Abs(rhs.y) > Mathf.Abs(rhs.z) && Mathf.Abs(rhs.y) > Mathf.Abs(rhs.x)) ? new Vector3(0f, Mathf.Sign(rhs.y), 0f) : ((!(Mathf.Abs(rhs.x) > Mathf.Abs(rhs.z))) ? new Vector3(0f, 0f, Mathf.Sign(rhs.z)) : new Vector3(Mathf.Sign(rhs.x), 0f, 0f)));
		float num2 = Vector3.Dot(stack.basePos - vector, stack.UpDir);
		Vector3 vector3 = vector + stack.UpDir * (num2 + m_FlowArrowHeightOffset);
		Quaternion quaternion = Quaternion.LookRotation(isPullArrow ? (-vector2) : vector2, myHolder.UpDir);
		d.Assert(arrowToModify, "Missing arrowToModify");
		if ((bool)arrowToModify)
		{
			Transform trans = myHolder.block.trans;
			arrowToModify.localPosition = trans.localPosition + trans.localRotation * vector3;
			arrowToModify.localRotation = trans.localRotation * quaternion;
		}
	}

	private void RecycleExpiredStackArrows()
	{
		for (int num = m_StackArrowsToUpdate.Count - 1; num >= 0; num--)
		{
			LinkedListNode<StackArrow> last = m_StackArrowsToUpdate.Last;
			StackArrow value = last.Value;
			value.geometry.Recycle();
			m_StackArrowsToUpdate.Remove(last);
			m_AllStackArrows.Remove(value.fromToHash);
		}
		PooledLinkedList<StackArrow> stackArrowsToUpdate = m_StackArrowsToUpdate;
		m_StackArrowsToUpdate = m_StackArrowsUpdated;
		m_StackArrowsUpdated = stackArrowsToUpdate;
	}

	private void ClearAllStackArrows()
	{
		PooledLinkedList<StackArrow>.Iterator enumerator = m_StackArrowsToUpdate.GetEnumerator();
		while (enumerator.MoveNext())
		{
			enumerator.Current.geometry.Recycle();
		}
		enumerator = m_StackArrowsUpdated.GetEnumerator();
		while (enumerator.MoveNext())
		{
			enumerator.Current.geometry.Recycle();
		}
		m_StackArrowsToUpdate.Clear();
		m_StackArrowsUpdated.Clear();
		m_AllStackArrows.Clear();
	}

	private void OnBlockAddedOrRemoved(TankBlock tBlock, Tank t)
	{
		foreach (LinkedListNode<StackArrow> value2 in m_AllStackArrows.Values)
		{
			StackArrow value = value2.Value;
			MoveStackArrow(value.from, value.connectionIndex, value.isPullArrow, value.geometry);
		}
		CraftingSetupChanged.Send();
	}

	private void OnSerialize(bool saving, Dictionary<int, TechComponent.SerialData> saveState)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.m_HeartbeatInterval = m_CurrentHeartbeatInterval;
			serialData.Store(saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(saveState);
		if (serialData2 != null)
		{
			SetHeartbeatInterval(serialData2.m_HeartbeatInterval);
		}
	}

	public NetworkedTechComponentID GetTechComponentID()
	{
		return NetworkedTechComponentID.TechHolders;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		writer.Write(HeartbeatCount);
		writer.Write(m_CurrentHeartbeatInterval);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		HeartbeatCount = reader.ReadInt32();
		m_CurrentHeartbeatInterval = reader.ReadSingle();
	}

	private void OnPool()
	{
		m_StackArrowParent = new GameObject("Stack Arrows").transform;
		bool worldPositionStays = false;
		m_StackArrowParent.SetParent(base.Tech.transform, worldPositionStays);
		m_ItemPickupDB = new ItemPickupDatabase(m_ItemPickupForgetTime);
		base.Tech.UpdateEvent.Subscribe(OnUpdate);
	}

	private void OnSpawn()
	{
		m_Holders.Clear();
		m_HeartbeatTime = Time.time;
		m_CurrentHeartbeatInterval = Globals.inst.m_TankHolderHeartbeatInterval / m_HeartbeatMultiplier[2];
		HeartbeatCount = 0;
		m_ItemPickupDB.ClearAll();
		base.Tech.AttachEvent.Subscribe(OnBlockAddedOrRemoved);
		base.Tech.DetachEvent.Subscribe(OnBlockAddedOrRemoved);
		base.Tech.SerializeEvent.Subscribe(OnSerialize);
	}

	private void OnRecycle()
	{
		base.Tech.AttachEvent.Unsubscribe(OnBlockAddedOrRemoved);
		base.Tech.DetachEvent.Unsubscribe(OnBlockAddedOrRemoved);
		base.Tech.SerializeEvent.Unsubscribe(OnSerialize);
		ClearRegisteredOperations();
		ClearAllStackArrows();
		m_ItemPickupDB.ClearAll();
	}

	private void OnUpdate()
	{
		RecycleExpiredStackArrows();
		m_ItemPickupDB.ClearOldTimestamps();
	}
}
