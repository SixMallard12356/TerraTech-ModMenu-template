using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(ModuleItemHolder))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleItemDropper : Module, ModuleItemHolder.IStackDirection
{
	[SerializeField]
	[Tooltip("Time before this item can be collected by ModuleItemPickup (eg Recievers and TractorPads)")]
	private float m_CollectionTimeout = 1f;

	[SerializeField]
	private Transform m_PullArrowPrefab;

	private ModuleItemHolder m_Holder;

	bool ModuleItemHolder.IStackDirection.CanReceiveOn(Vector3 apLocal, ModuleItemHolder.Stack ownStack)
	{
		return true;
	}

	bool ModuleItemHolder.IStackDirection.CanOutputTo(Vector3 apLocal, ModuleItemHolder.Stack ownStack)
	{
		return false;
	}

	private void OnAttached()
	{
		base.block.tank.Holders.RegisterOperation(m_Holder, OnCycle, -9);
	}

	private void OnDetaching()
	{
		base.block.tank.Holders.UnregisterOperations(m_Holder);
	}

	private TechHolders.OperationResult OnCycle()
	{
		TechHolders.OperationResult operationResult = TechHolders.OperationResult.None;
		if (!m_Holder.IsEmpty)
		{
			Visible firstItem = m_Holder.SingleStack.FirstItem;
			if (!firstItem.TakenThisHeartbeat)
			{
				bool notifyRelease = true;
				firstItem.SetHolder(null, notifyRelease);
				firstItem.SetLockTimout(Visible.LockTimerTypes.ItemCollection, m_CollectionTimeout);
				operationResult = TechHolders.OperationResult.Effect;
			}
		}
		if (m_Holder.IsEmpty)
		{
			ModuleItemHolder.Stack[] connectedNeighbourStacks = m_Holder.SingleStack.connectedNeighbourStacks;
			foreach (ModuleItemHolder.Stack stack in connectedNeighbourStacks)
			{
				if (stack == null)
				{
					continue;
				}
				TechHolders.OperationResult operationResult2 = TechHolders.OperationResult.None;
				ModuleItemHolder.Stack.ItemIterator enumerator = stack.IterateItemsIncludingLinkedStacks().GetEnumerator();
				while (enumerator.MoveNext())
				{
					Visible current = enumerator.Current;
					operationResult2 = m_Holder.SingleStack.TryTakeOnHeartbeat(current);
					if (operationResult2 != TechHolders.OperationResult.None)
					{
						operationResult = TechHolders.CombineOperationResults(operationResult, operationResult2);
						break;
					}
				}
				if (operationResult2 != TechHolders.OperationResult.None)
				{
					break;
				}
			}
		}
		return operationResult;
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		m_Holder = GetComponent<ModuleItemHolder>();
	}

	private void OnUpdate()
	{
		if (!(m_PullArrowPrefab != null) || !base.block.IsAttached)
		{
			return;
		}
		ModuleItemHolder.Stack singleStack = m_Holder.SingleStack;
		for (int i = 0; i < singleStack.connectedNeighbourStacks.Length; i++)
		{
			if (singleStack.connectedNeighbourStacks[i] != null)
			{
				base.block.tank.Holders.UpdateStackArrow(singleStack, i, isPullArrow: true, m_PullArrowPrefab, 5);
			}
		}
	}
}
