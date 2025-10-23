#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(ModuleItemHolder))]
public class ModuleItemFilter : Module, ItemSearchHandler, ItemSearchFilter, ManPointer.OpenMenuEventConsumer, INetworkedModule, ModuleItemHolder.IStackDirection
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public IntVector3 takeFromHolderPos = IntVector3.zero;

		public int takeFromStackIndex = -1;

		public ItemTypeInfo type;

		public AcceptMode mode;

		public int nextStackToPass;
	}

	public enum AcceptMode
	{
		None,
		Any,
		RawResource,
		RefinedResource,
		Component,
		Block,
		SpecificItem,
		Resource,
		Fuel
	}

	private enum StackUpdateState
	{
		Valid,
		AdvanceWithoutUpdate,
		UpdateAndAdvanceIfNeeded
	}

	private enum OutputStackState
	{
		Disabled,
		Enabled,
		Active
	}

	[SerializeField]
	private Vector3 m_TakeFromDirection = -Vector3.forward;

	[SerializeField]
	private AcceptMode m_FixedAcceptMode;

	[SerializeField]
	private Transform m_PushArrowPrefab;

	[SerializeField]
	private Transform m_PushAltPrefab;

	[SerializeField]
	private Transform m_PullArrowPrefab;

	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	private ModuleItemHolder m_Holder;

	private ItemTypeInfo m_AcceptItemType = new ItemTypeInfo(ObjectTypes.Null, 0);

	private AcceptMode m_AcceptMode;

	private WorldObjectOverlay m_Overlay;

	private int m_NextPushStackIndex;

	private ModuleItemHolder.Stack[] m_PushStacks;

	private StackUpdateState m_StackUpdateState;

	private int m_CircuitEnabledFlags;

	[SerializeField]
	[HideInInspector]
	private int m_PullStackIndex;

	[SerializeField]
	[HideInInspector]
	private int[] m_PushStackIndices;

	private NetworkedProperty<ItemFilterSelectionMessage> m_FilterSelection;

	public bool AcceptsSpecificItemType => m_AcceptMode == AcceptMode.SpecificItem;

	public bool FilterIsFixed => m_FixedAcceptMode != AcceptMode.None;

	public AcceptMode FilterMode
	{
		get
		{
			if (!IsCircuitInterrupted)
			{
				return m_AcceptMode;
			}
			return AcceptMode.None;
		}
	}

	public ItemTypeInfo SpecificAcceptedItem => m_AcceptItemType;

	private bool CircuitControlled
	{
		get
		{
			if (m_IsUsedOnCircuit)
			{
				return base.block.CircuitReceiver.IsConnectedToOtherNodes;
			}
			return false;
		}
	}

	private bool IsCircuitInterrupted
	{
		get
		{
			if (CircuitControlled)
			{
				return base.block.CircuitReceiver.CurrentChargeData.ChargeStrength == 0;
			}
			return false;
		}
	}

	private ModuleItemHolder.Stack PullFromStack => m_Holder.SingleStack.connectedNeighbourStacks[m_PullStackIndex];

	private ModuleItemHolder.Stack NextPushStack => m_PushStacks[m_NextPushStackIndex];

	public void RequestSetAcceptMode(AcceptMode filterAcceptMode, ItemTypeInfo itemTypeInfo = null)
	{
		SetAcceptMode(filterAcceptMode, itemTypeInfo);
		m_FilterSelection.Sync();
	}

	public void SetAcceptMode(AcceptMode filterAcceptMode, ItemTypeInfo itemTypeInfo = null)
	{
		if (!FilterIsFixed)
		{
			Internal_SetAcceptMode(filterAcceptMode, itemTypeInfo);
		}
	}

	public void SetAcceptMode(AcceptMode filterAcceptMode)
	{
		SetAcceptMode(filterAcceptMode, null);
	}

	public void SetAcceptSpecificItem(ItemTypeInfo itemTypeInfo)
	{
		SetAcceptMode(AcceptMode.SpecificItem, itemTypeInfo);
	}

	public bool CanOpenMenu(bool isRadial)
	{
		if (isRadial && !FilterIsFixed)
		{
			return !base.block.tank.IsEnemy();
		}
		return false;
	}

	public bool OnOpenMenuEvent(OpenMenuEventData openMenu)
	{
		if (openMenu.m_AllowRadialMenu && !FilterIsFixed)
		{
			Singleton.Manager<ManHUD>.inst.AddRadialOpenRequest(ManHUD.HUDElementType.FilterMenu, Singleton.Manager<ManHUD>.inst.GetMousePositionOnScreen(), openMenu);
			return true;
		}
		return false;
	}

	private void Internal_SetAcceptMode(AcceptMode filterAcceptMode, ItemTypeInfo itemTypeInfo)
	{
		if (m_AcceptMode == AcceptMode.Block || (m_AcceptMode == AcceptMode.SpecificItem && m_AcceptItemType.ObjectType == ObjectTypes.Block))
		{
			filterAcceptMode = AcceptMode.Any;
			itemTypeInfo = null;
		}
		if (itemTypeInfo != null && itemTypeInfo.ObjectType != ObjectTypes.Null)
		{
			d.AssertFormat(filterAcceptMode == AcceptMode.SpecificItem, this, "Setting filter to accept specific item '{0}' but mode provided was '{1}' instead of expected AcceptMode.SpecificItem", itemTypeInfo, filterAcceptMode);
			filterAcceptMode = AcceptMode.SpecificItem;
		}
		else
		{
			d.Assert(filterAcceptMode != AcceptMode.SpecificItem, "Setting filter accept mode to AcceptMode.SpecificItem but no ItemTypeInfo was provided", this);
			itemTypeInfo = new ItemTypeInfo(ObjectTypes.Null, 0);
		}
		AcceptMode acceptMode = m_AcceptMode;
		ItemTypeInfo acceptItemType = m_AcceptItemType;
		m_AcceptMode = filterAcceptMode;
		m_AcceptItemType = itemTypeInfo;
		m_FilterSelection.Data.m_FilterAcceptMode = m_AcceptMode;
		m_FilterSelection.Data.m_ChunkItemType = ((m_AcceptMode == AcceptMode.SpecificItem) ? m_AcceptItemType.ItemType : (-1));
		if (base.block.IsAttached && (acceptMode != m_AcceptMode || acceptItemType != m_AcceptItemType))
		{
			base.block.tank.Holders.CraftingSetupChanged.Send();
		}
	}

	private void ClearFilter()
	{
		Internal_SetAcceptMode((!FilterIsFixed) ? AcceptMode.Any : m_FixedAcceptMode, new ItemTypeInfo(ObjectTypes.Null, 0));
		m_NextPushStackIndex = 0;
		m_StackUpdateState = StackUpdateState.Valid;
		for (int i = 0; i < m_PushStacks.Length; i++)
		{
			m_PushStacks[i] = null;
		}
	}

	private bool CurrentModeAcceptsItem(ItemTypeInfo itemType)
	{
		switch (m_AcceptMode)
		{
		case AcceptMode.None:
			return false;
		case AcceptMode.Resource:
			return itemType.ObjectType == ObjectTypes.Chunk;
		case AcceptMode.RawResource:
			return Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.IsDescriptorFlag(itemType.GetHashCode(), typeof(ChunkCategory), 1);
		case AcceptMode.RefinedResource:
			return Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.IsDescriptorFlag(itemType.GetHashCode(), typeof(ChunkCategory), 2);
		case AcceptMode.Component:
			return Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.IsDescriptorFlag(itemType.GetHashCode(), typeof(ChunkCategory), 4);
		case AcceptMode.Fuel:
			return Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.IsDescriptorFlag(itemType.GetHashCode(), typeof(ChunkCategory), 8);
		case AcceptMode.SpecificItem:
			return itemType.Equals(m_AcceptItemType);
		case AcceptMode.Any:
			return itemType.ObjectType != ObjectTypes.Block;
		default:
			d.Assert(condition: false, "Invalid accept mode: " + m_AcceptMode);
			return false;
		}
	}

	private void UpdatePushStackConnections()
	{
		if (m_StackUpdateState == StackUpdateState.Valid)
		{
			return;
		}
		if (m_StackUpdateState != StackUpdateState.AdvanceWithoutUpdate)
		{
			for (int i = 0; i < m_PushStackIndices.Length; i++)
			{
				int num = m_PushStackIndices[i];
				if (num >= m_Holder.SingleStack.connectedNeighbourStacks.Length)
				{
					DebugUtil.AssertRelease(condition: false, "UpdatePushStackConnections: trying to get at neighbourInd " + num + " when there are only  " + m_Holder.SingleStack.connectedNeighbourStacks.ToString() + " neighbours");
				}
				else
				{
					ModuleItemHolder.Stack stack = m_Holder.SingleStack.connectedNeighbourStacks[num];
					if (stack != null && !stack.CanAccept(null, m_Holder.SingleStack, ModuleItemHolder.PassType.Pass | ModuleItemHolder.PassType.Test))
					{
						stack = null;
					}
					m_PushStacks[i] = stack;
				}
			}
		}
		if (m_StackUpdateState != StackUpdateState.UpdateAndAdvanceIfNeeded || NextPushStack == null)
		{
			for (int j = 0; j < m_PushStackIndices.Length; j++)
			{
				m_NextPushStackIndex = (m_NextPushStackIndex + 1) % m_PushStackIndices.Length;
				if (GetOutputStackStateAtIndex(m_NextPushStackIndex) != OutputStackState.Disabled)
				{
					break;
				}
			}
		}
		m_StackUpdateState = StackUpdateState.Valid;
	}

	private OutputStackState GetOutputStackStateAtIndex(int index)
	{
		if (m_PushStacks[index] == null)
		{
			return OutputStackState.Disabled;
		}
		if (CircuitControlled && m_PushStacks.Length > 1 && (m_CircuitEnabledFlags & (1 << index)) == 0)
		{
			return OutputStackState.Disabled;
		}
		if (m_NextPushStackIndex == index)
		{
			return OutputStackState.Active;
		}
		return OutputStackState.Enabled;
	}

	bool ModuleItemHolder.IStackDirection.CanReceiveOn(Vector3 apLocal, ModuleItemHolder.Stack ownStack)
	{
		int num = m_Holder.SingleStack.apConnectionIndices[m_PullStackIndex];
		Vector3 b = base.block.attachPoints[num];
		return apLocal.EqualsEpsilon(b);
	}

	bool ModuleItemHolder.IStackDirection.CanOutputTo(Vector3 apLocal, ModuleItemHolder.Stack ownStack)
	{
		int[] pushStackIndices = m_PushStackIndices;
		foreach (int num in pushStackIndices)
		{
			int num2 = m_Holder.SingleStack.apConnectionIndices[num];
			Vector3 b = base.block.attachPoints[num2];
			if (apLocal.EqualsEpsilon(b))
			{
				return true;
			}
		}
		return false;
	}

	private void OnAttached()
	{
		base.block.tank.Holders.RegisterOperation(m_Holder, OnPull, 12);
		base.block.tank.Holders.RegisterOperation(m_Holder, OnPush, 13);
		if (!FilterIsFixed)
		{
			d.Assert(m_Overlay == null, "ModuleItemFilter: Already have an overlay before attach!");
			m_Overlay = Singleton.Manager<ManOverlay>.inst.AddFilterOverlay(this);
		}
	}

	private TechHolders.OperationResult OnPull()
	{
		ModuleItemHolder.Stack pullFromStack = PullFromStack;
		if (pullFromStack != null)
		{
			ModuleItemHolder.Stack.ItemIterator enumerator = pullFromStack.IterateItemsIncludingLinkedStacks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				Visible current = enumerator.Current;
				TechHolders.OperationResult operationResult = m_Holder.SingleStack.TryTakeOnHeartbeat(current);
				if (operationResult != TechHolders.OperationResult.None)
				{
					return operationResult;
				}
			}
		}
		return TechHolders.OperationResult.None;
	}

	private TechHolders.OperationResult OnPush()
	{
		UpdatePushStackConnections();
		ModuleItemHolder.Stack nextPushStack = NextPushStack;
		ModuleItemHolder.Stack singleStack = m_Holder.SingleStack;
		if (nextPushStack == null || singleStack.IsEmpty)
		{
			return TechHolders.OperationResult.None;
		}
		return nextPushStack.TryTakeOnHeartbeat(singleStack.FirstItem);
	}

	private void OnStackConnect(ModuleItemHolder.Stack thisStack, ModuleItemHolder.Stack otherStack, Vector3 localAPPos, Vector3 otherLocalAP)
	{
		d.Assert(thisStack == m_Holder.SingleStack);
		if (otherStack != PullFromStack)
		{
			m_StackUpdateState = StackUpdateState.UpdateAndAdvanceIfNeeded;
		}
	}

	private void OnDetaching()
	{
		base.block.tank.Holders.UnregisterOperations(m_Holder);
		if (!FilterIsFixed)
		{
			d.Assert(m_Overlay != null, "ModuleItemFilter: Don't have an overlay before detach!");
			Singleton.Manager<ManOverlay>.inst.RemoveObjectOverlay(m_Overlay);
			m_Overlay = null;
		}
	}

	private void OnStackDisconnect(ModuleItemHolder.Stack thisStack, ModuleItemHolder.Stack otherStack, bool detachingSelf)
	{
		d.Assert(thisStack == m_Holder.SingleStack);
		m_StackUpdateState = StackUpdateState.UpdateAndAdvanceIfNeeded;
	}

	private bool CanAcceptItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack, ModuleItemHolder.PassType passType)
	{
		if ((passType & ModuleItemHolder.PassType.Pass) != 0 && (PullFromStack == null || fromStack.myHolder != PullFromStack.myHolder))
		{
			return false;
		}
		if (passType == (ModuleItemHolder.PassType.Pass | ModuleItemHolder.PassType.Test))
		{
			return true;
		}
		if ((passType & ModuleItemHolder.PassType.Drop) == 0 && IsCircuitInterrupted)
		{
			return false;
		}
		if (m_Holder.SingleStack.IsEmpty)
		{
			if ((passType & ModuleItemHolder.PassType.Drop) == 0)
			{
				return CurrentModeAcceptsItem(item.m_ItemType);
			}
			return true;
		}
		return false;
	}

	private bool CanReleaseItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack, ModuleItemHolder.PassType passType)
	{
		if (passType == ModuleItemHolder.PassType.Pass)
		{
			return toStack == NextPushStack;
		}
		return true;
	}

	private void OnReleaseItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack)
	{
		if (toStack == NextPushStack)
		{
			m_StackUpdateState = StackUpdateState.AdvanceWithoutUpdate;
		}
	}

	private void OnTakeItem(Visible item, ModuleItemHolder.Stack stack)
	{
		if (Singleton.Manager<ManPointer>.inst.DraggingItem == item && base.block.IsInteractible)
		{
			SetAcceptSpecificItem(item.m_ItemType);
		}
	}

	private void OnVisualChargeChanged(Circuits.BlockChargeData charge)
	{
		if (m_PushStacks.Length > 1 && charge.ChargeStrength != m_CircuitEnabledFlags)
		{
			m_CircuitEnabledFlags = charge.ChargeStrength;
			if (GetOutputStackStateAtIndex(m_NextPushStackIndex) == OutputStackState.Disabled)
			{
				m_StackUpdateState = StackUpdateState.AdvanceWithoutUpdate;
			}
		}
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.type = m_AcceptItemType;
			serialData.mode = m_AcceptMode;
			serialData.nextStackToPass = m_NextPushStackIndex;
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			if (!FilterIsFixed)
			{
				Internal_SetAcceptMode(serialData2.mode, serialData2.type);
			}
			m_NextPushStackIndex = serialData2.nextStackToPass;
		}
	}

	private void OnSerializeText(bool saving, TankPreset.BlockSpec context, bool onTech)
	{
		if (saving)
		{
			context.Store(GetType(), "type", m_AcceptItemType.ToString());
			context.Store(GetType(), "mode", m_AcceptMode.ToString());
		}
		else if (!FilterIsFixed)
		{
			AcceptMode result = m_AcceptMode;
			ItemTypeInfo itemTypeInfo = ItemTypeInfo.Parse(context.Retrieve(GetType(), "type"));
			string text = context.Retrieve(GetType(), "mode");
			if (!text.NullOrEmpty() && !Enum.TryParse<AcceptMode>(text, out result))
			{
				result = AcceptMode.None;
				d.LogWarning("ModuleItemFilter - Deserialising accept mode from filter: Mode '" + text + "' was not recognised as a valid AcceptMode.");
			}
			Internal_SetAcceptMode(result, itemTypeInfo);
		}
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleItemFilter;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		m_FilterSelection.Serialise(writer);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		m_FilterSelection.Deserialise(reader);
	}

	private void OnMPFilterSelectionUpdated(ItemFilterSelectionMessage msg)
	{
		ItemTypeInfo itemTypeInfo = ((msg.m_ChunkItemType >= 0) ? new ItemTypeInfo(ObjectTypes.Chunk, msg.m_ChunkItemType) : null);
		SetAcceptMode(msg.m_FilterAcceptMode, itemTypeInfo);
	}

	private void PrePool()
	{
		if (m_IsUsedOnCircuit)
		{
			this.SetupForCircuitInput();
		}
		m_PullStackIndex = -1;
		List<int> list = new List<int>();
		ModuleItemHolder.Stack singleStack = base.block.GetComponent<ModuleItemHolder>().SingleStack;
		for (int i = 0; i < singleStack.apConnectionIndices.Length; i++)
		{
			Vector3 vector = base.block.attachPoints[singleStack.apConnectionIndices[i]];
			if (vector.normalized.Dot(m_TakeFromDirection) > 0.9f)
			{
				m_PullStackIndex = i;
			}
			else
			{
				list.Add(i);
			}
		}
		d.Assert(m_PullStackIndex != -1 && list.Count != 0, "Failed to find TakeFrom and PassTo stacks from AP data");
		m_PushStackIndices = list.ToArray();
	}

	private void OnPool()
	{
		m_PushStacks = new ModuleItemHolder.Stack[m_PushStackIndices.Length];
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.serializeTextEvent.Subscribe(OnSerializeText);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		m_Holder = GetComponent<ModuleItemHolder>();
		m_Holder.ReleaseItemEvent.Subscribe(OnReleaseItem);
		m_Holder.TakeItemEvent.Subscribe(OnTakeItem);
		m_Holder.StackConnectEvent.Subscribe(OnStackConnect);
		m_Holder.StackDisconnectEvent.Subscribe(OnStackDisconnect);
		m_Holder.SetAcceptFilterCallback(CanAcceptItem);
		m_Holder.SetReleaseFilterCallback(CanReleaseItem);
		m_Holder.ItemRequestHandler = this;
		if (m_IsUsedOnCircuit && m_PushStackIndices.Length > 1)
		{
			base.block.CircuitReceiver.SubscribeToChargeData(null, null, null, OnVisualChargeChanged, requireExtensiveChargeData: false);
		}
		m_FilterSelection = new NetworkedProperty<ItemFilterSelectionMessage>(this, TTMsgType.SetItemFilterSelection, OnMPFilterSelectionUpdated);
	}

	private void OnSpawn()
	{
		ClearFilter();
	}

	private void OnRecycle()
	{
		ClearFilter();
	}

	private void OnUpdate()
	{
		if (!base.block.IsAttached)
		{
			return;
		}
		UpdatePushStackConnections();
		for (int i = 0; i < m_PushStacks.Length; i++)
		{
			OutputStackState outputStackStateAtIndex = GetOutputStackStateAtIndex(i);
			switch (outputStackStateAtIndex)
			{
			case OutputStackState.Enabled:
				base.block.tank.Holders.UpdateStackArrow(m_Holder.SingleStack, m_PushStackIndices[i], isPullArrow: false, m_PushAltPrefab, 20);
				break;
			case OutputStackState.Active:
				base.block.tank.Holders.UpdateStackArrow(m_Holder.SingleStack, m_PushStackIndices[i], isPullArrow: false, m_PushArrowPrefab, 20);
				break;
			default:
				d.LogErrorFormat(this, "Unaccounted for state {0} in ModuleItemFilter", outputStackStateAtIndex);
				break;
			case OutputStackState.Disabled:
				break;
			}
		}
		if (PullFromStack != null)
		{
			base.block.tank.Holders.UpdateStackArrow(m_Holder.SingleStack, m_PullStackIndex, isPullArrow: true, m_PullArrowPrefab, 10);
		}
	}

	public void HandleExpandSearch(ItemSearcher builder, ModuleItemHolder.Stack entryStack, ModuleItemHolder.Stack prevStack, out ItemSearchAvailableItems availItems)
	{
		ModuleItemHolder.Stack pullFromStack = PullFromStack;
		if (pullFromStack == null || pullFromStack != prevStack)
		{
			builder.PushFilter(this);
			if (pullFromStack != null)
			{
				builder.PushNode(pullFromStack);
			}
			availItems = ItemSearchAvailableItems.Processed;
		}
		else
		{
			availItems = ItemSearchAvailableItems.None;
		}
	}

	public void HandleSearchRequest()
	{
	}

	public bool WantsToKnowAboutSearchRequest()
	{
		return false;
	}

	public void HandleCollectItems(ItemSearchCollector collector, bool processed)
	{
		if (!m_Holder.SingleStack.IsEmpty)
		{
			collector.OfferItem(m_Holder.SingleStack.FirstItem);
		}
	}

	public bool AcceptsType(ItemTypeInfo type)
	{
		if (!IsCircuitInterrupted)
		{
			return CurrentModeAcceptsItem(type);
		}
		return false;
	}
}
