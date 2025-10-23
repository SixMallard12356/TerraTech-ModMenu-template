using UnityEngine;

[RequireComponent(typeof(ModuleItemHolder))]
public class ModuleItemStore : Module, ItemSearchHandler, ICircuitDispensor, ModuleItemHolder.IStackDirection
{
	[SerializeField]
	private Transform m_PullArrowPrefab;

	[SerializeField]
	private int m_Capacity;

	[SerializeField]
	private bool m_SingleType;

	private ModuleItemHolder m_Holder;

	private ItemTypeInfo m_CurrentItemType = new ItemTypeInfo(ObjectTypes.Null, 0);

	private int m_LastRequestHeartbeat;

	int ICircuitDispensor.GetDispensableCharge(Vector3 ap)
	{
		if (!base.block.IsAttached)
		{
			return 0;
		}
		return m_Holder.NumContents;
	}

	bool ModuleItemHolder.IStackDirection.CanReceiveOn(Vector3 apLocal, ModuleItemHolder.Stack ownStack)
	{
		return true;
	}

	bool ModuleItemHolder.IStackDirection.CanOutputTo(Vector3 apLocal, ModuleItemHolder.Stack ownStack)
	{
		return true;
	}

	private void OnAttached()
	{
		base.block.tank.Holders.RegisterOperation(m_Holder, OnPull, 5);
		base.block.tank.Holders.RegisterOperation(m_Holder, OnPush, 15);
	}

	private TechHolders.OperationResult OnPush()
	{
		TechHolders.OperationResult operationResult = TechHolders.OperationResult.None;
		ModuleItemHolder.StackIterator.Enumerator enumerator = m_Holder.Stacks.GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleItemHolder.Stack current = enumerator.Current;
			for (int num = current.NumItems - 1; num >= 0; num--)
			{
				Visible item = current.items[num];
				ModuleItemHolder.Stack requestedItemNextHop = base.block.tank.Holders.GetRequestedItemNextHop(item);
				if (requestedItemNextHop != null)
				{
					operationResult = TechHolders.CombineOperationResults(operationResult, requestedItemNextHop.TryTakeOnHeartbeat(item));
				}
			}
		}
		return operationResult;
	}

	private TechHolders.OperationResult OnPull()
	{
		if (m_Holder.IsFull)
		{
			return TechHolders.OperationResult.Retry;
		}
		TechHolders.OperationResult operationResult = TechHolders.OperationResult.None;
		ModuleItemHolder.StackIterator.Enumerator enumerator = m_Holder.Stacks.GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleItemHolder.Stack current = enumerator.Current;
			if (current.ReceivedThisHeartbeat)
			{
				continue;
			}
			TechHolders.OperationResult operationResult2 = TechHolders.OperationResult.None;
			ModuleItemHolder.Stack.ConnectedStackIterator.Enumerator enumerator2 = current.ConnectedStacks.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				ModuleItemHolder.Stack current2 = enumerator2.Current;
				if (current2.myHolder.ItemStore != null)
				{
					continue;
				}
				ModuleItemHolder.Stack.ItemIterator enumerator3 = current2.IterateItemsIncludingLinkedStacks().GetEnumerator();
				while (enumerator3.MoveNext())
				{
					Visible current3 = enumerator3.Current;
					operationResult2 = current.TryTakeOnHeartbeat(current3);
					if (operationResult2 != TechHolders.OperationResult.None)
					{
						break;
					}
				}
				if (operationResult2 != TechHolders.OperationResult.None)
				{
					break;
				}
			}
			operationResult = TechHolders.CombineOperationResults(operationResult, operationResult2);
		}
		return operationResult;
	}

	private void OnDetaching()
	{
		base.block.tank.Holders.UnregisterOperations(m_Holder);
		m_CurrentItemType.Set(ObjectTypes.Null, 0);
	}

	private void OnReleaseItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack)
	{
		if (m_Holder.IsEmpty)
		{
			m_CurrentItemType.Set(ObjectTypes.Null, 0);
		}
	}

	private void OnTakeItem(Visible item, ModuleItemHolder.Stack stack)
	{
		if (m_SingleType)
		{
			m_CurrentItemType.Set(item.m_ItemType);
		}
	}

	private bool CanAcceptItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack, ModuleItemHolder.PassType passType)
	{
		if (passType == (ModuleItemHolder.PassType.Pass | ModuleItemHolder.PassType.Test))
		{
			return true;
		}
		if (m_Holder.NumContents >= m_Capacity)
		{
			return false;
		}
		if (m_SingleType && m_CurrentItemType.ObjectType != ObjectTypes.Null && !m_CurrentItemType.Equals(item.m_ItemType))
		{
			return false;
		}
		ModuleItemHolder.Stack requestedItemNextHop = base.block.tank.Holders.GetRequestedItemNextHop(item);
		if (requestedItemNextHop != null)
		{
			return requestedItemNextHop.myHolder == m_Holder;
		}
		return true;
	}

	private bool CanReleaseItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack, ModuleItemHolder.PassType passType)
	{
		if ((passType & ModuleItemHolder.PassType.Pass) != 0 && base.block.tank.Holders.GetRequestedItemNextHop(item) != toStack)
		{
			return toStack?.myHolder.IsFlag(ModuleItemHolder.Flags.TakeFromSilo) ?? false;
		}
		return true;
	}

	private void PrePool()
	{
		ModuleItemHolder component = GetComponent<ModuleItemHolder>();
		component.OverrideStackCapacity(Mathf.CeilToInt(m_Capacity / component.NumStacks) + component.NumStacks);
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		m_Holder = GetComponent<ModuleItemHolder>();
		m_Holder.SetAcceptFilterCallback(CanAcceptItem);
		m_Holder.SetReleaseFilterCallback(CanReleaseItem);
		m_Holder.ReleaseItemEvent.Subscribe(OnReleaseItem);
		m_Holder.TakeItemEvent.Subscribe(OnTakeItem);
		m_Holder.ItemRequestHandler = this;
	}

	private void OnSpawn()
	{
		m_CurrentItemType.Set(ObjectTypes.Null, 0);
		m_LastRequestHeartbeat = -1;
	}

	private void OnUpdate()
	{
		if (m_PullArrowPrefab != null && base.block.IsAttached)
		{
			ModuleItemHolder.StackIterator.Enumerator enumerator = m_Holder.Stacks.GetEnumerator();
			while (enumerator.MoveNext())
			{
				ModuleItemHolder.Stack current = enumerator.Current;
				for (int i = 0; i < current.connectedNeighbourStacks.Length; i++)
				{
					ModuleItemHolder.Stack stack = current.connectedNeighbourStacks[i];
					if (stack != null && stack.myHolder != m_Holder)
					{
						base.block.tank.Holders.UpdateStackArrow(current, i, isPullArrow: true, m_PullArrowPrefab, 15);
					}
				}
			}
		}
		if (ManNetwork.IsHost && m_Holder.Antenna != null)
		{
			bool active = base.block.IsAttached && m_LastRequestHeartbeat == base.block.tank.Holders.HeartbeatCount;
			m_Holder.Antenna.SetActive(active);
		}
	}

	public void HandleExpandSearch(ItemSearcher builder, ModuleItemHolder.Stack entryStack, ModuleItemHolder.Stack prevStack, out ItemSearchAvailableItems availItems)
	{
		availItems = ItemSearchAvailableItems.Processed;
	}

	public void HandleSearchRequest()
	{
		m_LastRequestHeartbeat = base.block.tank.Holders.HeartbeatCount;
	}

	public bool WantsToKnowAboutSearchRequest()
	{
		return true;
	}

	public void HandleCollectItems(ItemSearchCollector collector, bool processed)
	{
		if (!m_Holder.IsEmpty)
		{
			m_Holder.OfferAllItemsToCollector(collector);
		}
	}
}
