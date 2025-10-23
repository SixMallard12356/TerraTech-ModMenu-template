#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
[RequireComponent(typeof(ModuleItemHolder))]
public class ModuleItemPickup : Module, ModuleItemHolder.IStackDirection
{
	private struct DistanceSortComparer
	{
		public Vector3 worldCentre;

		public int CompareVisibleRange(Visible a, Visible b)
		{
			return (int)((a.centrePosition - worldCentre).sqrMagnitude - (b.centrePosition - worldCentre).sqrMagnitude);
		}
	}

	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public List<int> m_PickedUpItems;
	}

	[SerializeField]
	[Tooltip("If specified, gives the only stack which is allowed to pick up")]
	private ModuleItemHolder.StackHandle m_SpecificPickupStack;

	[SerializeField]
	[Tooltip("If specified, gives another stack within this block to hand off items when they are to be hoisted into the air")]
	private ModuleItemHolder.StackHandle m_HandOffStack;

	[SerializeField]
	[Tooltip("Distance from centre of block that it can pickup items from")]
	private float m_PickupRange = 3f;

	[SerializeField]
	[Tooltip("Number of buckets for range sort. Don't touch unless you know what you're doing")]
	private int m_PickupSortBuckets = 4;

	[SerializeField]
	[Tooltip("Pickup one item every N seconds")]
	private float m_VisionRefreshInterval = 1f;

	[Tooltip("If true, Receivers can take from neutral techs (ie any tech that is not an enemy)")]
	[SerializeField]
	private bool m_AcceptNeutral;

	[Tooltip("Period before an item is physically lifted, during which other pickups can contend for it")]
	[SerializeField]
	private float m_PrePickupPeriod;

	[Tooltip("Wait this long before trying to pickup something we recently tried to pick up (regardless of whether we succeeded before)")]
	[SerializeField]
	private float m_PickupAfterMinInterval;

	[Tooltip("Wait this long before a newly-collected item is handed over to another holder")]
	[SerializeField]
	private float m_HandoverAfterMinInterval = 2f;

	[SerializeField]
	[Tooltip("Used to resolve contention when several pickups try to take the same item")]
	private int m_Priority;

	[SerializeField]
	[Tooltip("Range marker to spawn")]
	private Transform m_RangeIndicatorPrefab;

	[SerializeField]
	[Tooltip("If this is set, use the Z axis of this Transform to specify the direction the angle to pick up items is based around")]
	private Transform m_PickUpRadiusFwd;

	[Tooltip("This is the full angle around the above direction. E.g. 180 = 90 degrees either side of the direction")]
	[SerializeField]
	private float m_PickUpRadiusAngle = 360f;

	[SearchableEnum(SortedEnum.EnumSortType.AlphabeticalAscending, false)]
	[SerializeField]
	private TechAudio.SFXType m_SFXPickupItem = TechAudio.SFXType.ItemPickup;

	[SearchableEnum(SortedEnum.EnumSortType.AlphabeticalAscending, false)]
	[SerializeField]
	private TechAudio.SFXType m_SFXContentionLoop;

	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	private ModuleItemHolder m_Holder;

	private ModuleItemHolderMagnet m_Magnet;

	private float m_VisionRefreshTimer;

	private ManVisible.SearchIterator m_VisibleSearch = new ManVisible.SearchIterator();

	private List<Visible>[] m_VisibleBuckets;

	private bool m_SortFirstBucket;

	private int m_PushStackIndex;

	private Transform m_RangeIndicator;

	private float m_RangeIndicatorSpinAngle;

	private ModuleAudioProvider m_AudioProvider;

	private NetworkedProperty<EmptyBlockMessage> m_ItemPickupTrigger;

	private Func<Visible, bool> m_PickupFilterCallback;

	private static HashSet<int> s_VisiblesSeenThisRefresh = new HashSet<int>();

	private static readonly Bitfield<ObjectTypes> k_MaskBlocksChunks = new Bitfield<ObjectTypes>(new ObjectTypes[2]
	{
		ObjectTypes.Block,
		ObjectTypes.Chunk
	});

	private HashSet<int> m_PickedUpItems = new HashSet<int>();

	public float PickupRange => m_PickupRange;

	public bool IsEnabled { get; set; }

	public int[] RestrictAcceptItemTypes { get; set; }

	private bool CircuitControlled
	{
		get
		{
			if (m_IsUsedOnCircuit)
			{
				return base.block.CircuitNode.Receiver.IsConnectedToOtherNodes;
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

	public void SetPickupFilterCallback(Func<Visible, bool> callback)
	{
		d.Assert(m_PickupFilterCallback == null, "pickup filter callback set twice");
		m_PickupFilterCallback = callback;
	}

	private bool IsPickupStack(ModuleItemHolder.Stack stack)
	{
		if (m_SpecificPickupStack.stack == null || m_SpecificPickupStack.stack == stack)
		{
			if (m_HandOffStack.stack != null)
			{
				return m_HandOffStack.stack != stack;
			}
			return true;
		}
		return false;
	}

	private bool IsPrePickup(Visible item)
	{
		if (base.block.IsAttached)
		{
			return m_Holder.PickupPriority != int.MaxValue && Time.time - base.block.tank.Holders.GetItemPickupTime(item.ID) < m_Holder.PickupContentionPeriod;
		}
		d.LogError("ModuleItemPickup.IsPrePickup - Invalid call to IsPrePickup - Holder must be attached to a tech at time of calling!");
		return false;
	}

	private void TryPickupItems()
	{
		float time = Time.time;
		if (!Singleton.Manager<ManBlockLimiter>.inst.AllowPickupBy(base.block, m_Holder.Acceptance))
		{
			while (m_VisionRefreshTimer < time)
			{
				m_VisionRefreshTimer += m_VisionRefreshInterval;
			}
			return;
		}
		ModuleItemHolder.Stack stack = null;
		ModuleItemHolder.StackIterator.Enumerator enumerator = m_Holder.Stacks.GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleItemHolder.Stack current = enumerator.Current;
			if (!current.IsFull && (stack == null || current.NumItems < stack.NumItems) && IsPickupStack(current))
			{
				stack = current;
			}
		}
		if (stack == null)
		{
			return;
		}
		Vector3 centreOfMassWorld = base.block.centreOfMassWorld;
		if (time >= m_VisionRefreshTimer)
		{
			do
			{
				m_VisionRefreshTimer += m_VisionRefreshInterval;
			}
			while (m_VisionRefreshTimer < time);
			m_SortFirstBucket = true;
			if (m_PickupSortBuckets > m_VisibleBuckets.Length)
			{
				int num = m_VisibleBuckets.Length;
				Array.Resize(ref m_VisibleBuckets, m_PickupSortBuckets);
				for (int i = num; i < m_VisibleBuckets.Length; i++)
				{
					m_VisibleBuckets[i] = new List<Visible>();
				}
			}
			for (int j = 0; j < m_PickupSortBuckets; j++)
			{
				m_VisibleBuckets[j].Clear();
			}
			s_VisiblesSeenThisRefresh.Clear();
			if (m_Holder.IsFlag(ModuleItemHolder.Flags.Receiver))
			{
				SearchReceivableItems(centreOfMassWorld);
			}
			if (m_Holder.IsFlag(ModuleItemHolder.Flags.Collector))
			{
				SearchCollectibleItems(centreOfMassWorld);
			}
		}
		if ((bool)TakeOneItem(centreOfMassWorld, stack) && !m_Holder.HasPickupContentionPeriod)
		{
			ServerPlayItemPickupSfx();
		}
	}

	private void ServerPlayItemPickupSfx()
	{
		d.Assert(ManNetwork.IsHost, "Server only function not called from host!?");
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			m_ItemPickupTrigger.Sync();
		}
		else
		{
			PlayItemPickupSfx();
		}
	}

	private void PlayItemPickupSfx()
	{
		TechAudio.AudioTickData data = TechAudio.AudioTickData.ConfigureOneshot(base.block, m_SFXPickupItem);
		base.block.tank.TechAudio.PlayOneshot(data);
	}

	private void SearchReceivableItems(Vector3 blockCentre)
	{
		float num = (float)m_PickupSortBuckets / (m_PickupRange - m_Holder.HorizontalBoundsRadius);
		TileManager.VisibleIterator enumerator = Singleton.Manager<ManWorld>.inst.TileManager.IterateVisibles(ObjectTypes.Vehicle, blockCentre, m_PickupRange).GetEnumerator();
		while (enumerator.MoveNext())
		{
			Visible current = enumerator.Current;
			Tank tank = current.tank;
			if ((object)tank == base.block.tank)
			{
				continue;
			}
			float sqrMagnitude = (tank.boundsCentreWorld - blockCentre).sqrMagnitude;
			float num2 = m_PickupRange + current.Radius;
			if (sqrMagnitude > num2 * num2)
			{
				continue;
			}
			TechHolders.HolderIterator enumerator2 = tank.Holders.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				ModuleItemHolder current2 = enumerator2.Current;
				if (!current2.IsFlag(ModuleItemHolder.Flags.Deliverer) || current2.IsEmpty || !(m_AcceptNeutral ? (!current2.block.tank.IsEnemy(base.block.tank.Team)) : current2.block.tank.IsFriendly(base.block.tank.Team)))
				{
					continue;
				}
				int num3 = (int)(Mathf.Max((current2.block.centreOfMassWorld - blockCentre).magnitude - (current2.HorizontalBoundsRadius + m_Holder.HorizontalBoundsRadius), 0f) * num);
				if (num3 < m_PickupSortBuckets)
				{
					ModuleItemHolder.Stack.ItemIterator enumerator3 = current2.Contents.GetEnumerator();
					while (enumerator3.MoveNext())
					{
						Visible current3 = enumerator3.Current;
						s_VisiblesSeenThisRefresh.Add(current3.ID);
						m_VisibleBuckets[num3].Add(current3);
					}
				}
			}
		}
	}

	private void SearchCollectibleItems(Vector3 blockCentre)
	{
		float num = (float)m_PickupSortBuckets / (m_PickupRange - m_Holder.HorizontalBoundsRadius);
		int pickerMask = (((m_Holder.Acceptance & ModuleItemHolder.AcceptFlags.Blocks) != 0) ? Globals.inst.layerTank.mask : 0) | (((m_Holder.Acceptance & ModuleItemHolder.AcceptFlags.Chunks) != 0) ? Globals.inst.layerPickup.mask : 0);
		Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadius(m_VisibleSearch, base.block.centreOfMassWorld, m_PickupRange, k_MaskBlocksChunks, includeTriggers: false, pickerMask);
		foreach (Visible item in m_VisibleSearch)
		{
			if ((object)item == base.block.visible)
			{
				continue;
			}
			s_VisiblesSeenThisRefresh.Add(item.ID);
			if (item.type != ObjectTypes.Block || !item.block.IsAttached)
			{
				int num2 = (int)(Mathf.Max((item.centrePosition - blockCentre).magnitude - m_Holder.HorizontalBoundsRadius, 0f) * num);
				if (num2 < m_PickupSortBuckets)
				{
					m_VisibleBuckets[num2].Add(item);
				}
			}
		}
	}

	private bool CanTakeHeldItem(Visible item, bool isPrePickup)
	{
		ModuleItemHolder myHolder = item.holderStack.myHolder;
		if (isPrePickup)
		{
			if (m_Holder.PickupPriority > myHolder.PickupPriority)
			{
				return true;
			}
			if (m_Holder.PickupPriority == myHolder.PickupPriority)
			{
				Vector3 centrePosition = item.centrePosition;
				if ((centrePosition - m_Holder.block.centreOfMassWorld).sqrMagnitude < (centrePosition - myHolder.block.centreOfMassWorld).sqrMagnitude)
				{
					return true;
				}
			}
		}
		if (m_Holder.IsFlag(ModuleItemHolder.Flags.Receiver) && myHolder.IsFlag(ModuleItemHolder.Flags.Deliverer))
		{
			if (!m_AcceptNeutral)
			{
				return myHolder.block.tank.IsFriendly(base.block.tank.Team);
			}
			return !myHolder.block.tank.IsEnemy(base.block.tank.Team);
		}
		return false;
	}

	private Visible TakeOneItem(Vector3 blockCentre, ModuleItemHolder.Stack stackToTake)
	{
		Visible visible = null;
		if (m_Magnet.IsNotNull() && !m_Magnet.IsOperating)
		{
			return visible;
		}
		int num = 0;
		for (int i = 0; num < m_PickupSortBuckets; i++)
		{
			if (i >= m_VisibleBuckets[num].Count)
			{
				num++;
				i = -1;
				continue;
			}
			if (m_SortFirstBucket)
			{
				m_SortFirstBucket = false;
				d.Assert(i == 0);
				DistanceSortComparer distanceSortComparer = new DistanceSortComparer
				{
					worldCentre = blockCentre
				};
				float num2 = 1.5f * m_VisionRefreshInterval / Time.deltaTime;
				if ((float)m_VisibleBuckets[num].Count >= num2)
				{
					m_VisibleBuckets[num].Sort(((DistanceSortComparer)distanceSortComparer).CompareVisibleRange);
				}
			}
			Visible visible2 = m_VisibleBuckets[num][i];
			if (visible2 == null || !visible2.isActive || visible2 == Singleton.Manager<ManPointer>.inst.DraggingItem || (visible2.type == ObjectTypes.Block && visible2.block.IsAttached) || !visible2.CanBeCollected)
			{
				continue;
			}
			ColliderSwapper colliderSwapper = visible2.ColliderSwapper;
			if ((colliderSwapper != null && !colliderSwapper.CollisionEnabled) || !Singleton.Manager<ManBlockLimiter>.inst.AllowPickup(base.block, visible2) || (visible2.centrePosition - base.block.centreOfMassWorld).sqrMagnitude > m_PickupRange * m_PickupRange || (m_PickUpRadiusFwd != null && Mathf.Acos(Mathf.Clamp(Vector3.Dot((visible2.trans.position - stackToTake.BasePosWorld()).SetY(0f).normalized, m_PickUpRadiusFwd.forward.SetY(0f).normalized), -1f, 1f)) * 57.29578f > m_PickUpRadiusAngle * 0.5f))
			{
				continue;
			}
			bool flag = false;
			bool flag2 = false;
			if (visible2.holderStack != null)
			{
				flag = visible2.IsPrePickup;
				flag2 = !flag;
				if (visible2.holderStack.myHolder == m_Holder || !CanTakeHeldItem(visible2, flag) || !stackToTake.CanAcceptObjectType(visible2.type) || !stackToTake.TestAcceptFilters(visible2, visible2.holderStack, ModuleItemHolder.PassType.Pass | ModuleItemHolder.PassType.External))
				{
					continue;
				}
			}
			else
			{
				float itemPickupTime = base.block.tank.Holders.GetItemPickupTime(visible2.ID);
				if (itemPickupTime != -1f && Time.time - itemPickupTime < m_PickupAfterMinInterval)
				{
					continue;
				}
			}
			bool flag3 = m_PickupFilterCallback != null && !m_PickupFilterCallback(visible2);
			if (visible == null && !flag3 && (bool)stackToTake.Take(visible2, flag, insertAtBase: false))
			{
				visible = visible2;
				if (flag2)
				{
					base.block.tank.Holders.SetItemPickupTime(visible2.ID, Time.time - m_PrePickupPeriod);
				}
				m_PickedUpItems.Add(visible2.ID);
				if (m_PickupFilterCallback == null)
				{
					break;
				}
			}
		}
		return visible;
	}

	private int GetTallestStackIndex()
	{
		int result = 0;
		int num = 0;
		for (int num2 = m_Holder.NumStacks - 1; num2 >= 0; num2--)
		{
			int numItems = m_Holder.GetStack(num2).NumItems;
			if (numItems > num)
			{
				result = num2;
				num = numItems;
			}
		}
		return result;
	}

	private void UpdateFromCircuitState()
	{
		if (base.block.CircuitReceiver.CurrentChargeData.ChargeStrength != 0 || !m_Holder.HasPickupContentionPeriod)
		{
			return;
		}
		ModuleItemHolder.StackIterator.Enumerator enumerator = m_Holder.Stacks.GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleItemHolder.Stack current = enumerator.Current;
			if (IsPickupStack(current))
			{
				current.ReleaseAll();
			}
		}
	}

	bool ModuleItemHolder.IStackDirection.CanReceiveOn(Vector3 apLocal, ModuleItemHolder.Stack ownStack)
	{
		return false;
	}

	bool ModuleItemHolder.IStackDirection.CanOutputTo(Vector3 apLocal, ModuleItemHolder.Stack ownStack)
	{
		return true;
	}

	private void OnAttached()
	{
		IsEnabled = true;
		if (m_Holder.IsFlag(ModuleItemHolder.Flags.Receiver))
		{
			d.Assert(m_Holder.NumStacks != 0);
			base.block.tank.Holders.RegisterOperation(m_Holder, OnPush, 11);
		}
		if (m_HandOffStack.stack != null)
		{
			base.block.tank.Holders.RegisterOperation(m_Holder, OnHandOffItem, 30);
		}
		m_VisionRefreshTimer = Time.time + UnityEngine.Random.Range(0f, m_VisionRefreshInterval);
		for (int i = 0; i < m_PickupSortBuckets; i++)
		{
			m_VisibleBuckets[i].Clear();
		}
	}

	private void OnDetaching()
	{
		IsEnabled = false;
		base.block.tank.Holders.UnregisterOperations(m_Holder);
	}

	private TechHolders.OperationResult OnPush()
	{
		TechHolders.OperationResult operationResult = TechHolders.OperationResult.None;
		if (m_Holder.IsFlag(ModuleItemHolder.Flags.Receiver))
		{
			m_PushStackIndex = GetTallestStackIndex();
			ModuleItemHolder.StackIterator.Enumerator enumerator = m_Holder.Stacks.GetEnumerator();
			while (enumerator.MoveNext())
			{
				ModuleItemHolder.Stack current = enumerator.Current;
				ModuleItemHolder.Stack.ConnectedStackIterator.Enumerator enumerator2 = current.ConnectedStacks.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					ModuleItemHolder.Stack current2 = enumerator2.Current;
					operationResult = TechHolders.CombineOperationResults(operationResult, PushToStack(current, current2));
				}
			}
		}
		return operationResult;
	}

	private TechHolders.OperationResult OnHandOffItem()
	{
		if (m_HandOffStack.stack == null)
		{
			d.Assert(condition: false, "OnHandOffItem being called when we have no hand off stack");
			return TechHolders.OperationResult.None;
		}
		TechHolders.OperationResult result = TechHolders.OperationResult.None;
		ModuleItemHolder.StackIterator.Enumerator enumerator = m_Holder.Stacks.GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleItemHolder.Stack current = enumerator.Current;
			if (IsPickupStack(current) && !current.IsEmpty && !current.FirstItem.IsPrePickup)
			{
				bool force = true;
				bool insertAtBase = false;
				if (m_HandOffStack.stack.IsFull || !m_HandOffStack.stack.Take(current.FirstItem, force, insertAtBase))
				{
					break;
				}
				ServerPlayItemPickupSfx();
				result = TechHolders.OperationResult.Effect;
			}
		}
		return result;
	}

	private TechHolders.OperationResult PushToStack(ModuleItemHolder.Stack myStack, ModuleItemHolder.Stack toStack)
	{
		d.Assert(toStack != null);
		ModuleItemHolder.Stack.ItemIterator enumerator = myStack.IterateItemsIncludingLinkedStacks(m_PushStackIndex).GetEnumerator();
		while (enumerator.MoveNext())
		{
			Visible current = enumerator.Current;
			TechHolders.OperationResult operationResult = toStack.TryTakeOnHeartbeat(current);
			if (operationResult == TechHolders.OperationResult.Effect)
			{
				m_PushStackIndex = GetTallestStackIndex();
			}
			if (operationResult != TechHolders.OperationResult.None)
			{
				return operationResult;
			}
		}
		return TechHolders.OperationResult.None;
	}

	private bool CanAcceptItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack, ModuleItemHolder.PassType passType)
	{
		if ((bool)item && RestrictAcceptItemTypes != null)
		{
			int i;
			for (i = 0; i < RestrictAcceptItemTypes.Length && item.ItemType != RestrictAcceptItemTypes[i]; i++)
			{
			}
			if (i == RestrictAcceptItemTypes.Length)
			{
				return false;
			}
		}
		if (m_Holder.IsFlag(ModuleItemHolder.Flags.Collector) && (passType & ModuleItemHolder.PassType.Pick) != 0)
		{
			return true;
		}
		if (m_Holder.IsFlag(ModuleItemHolder.Flags.Receiver) && passType == (ModuleItemHolder.PassType.Pass | ModuleItemHolder.PassType.External))
		{
			return true;
		}
		if (m_Holder.IsFlag(ModuleItemHolder.Flags.ReceiverInternal) && passType == ModuleItemHolder.PassType.Pass)
		{
			return true;
		}
		if ((passType & ModuleItemHolder.PassType.Drop) != 0)
		{
			return true;
		}
		return false;
	}

	private bool CanReleaseItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack, ModuleItemHolder.PassType passType)
	{
		ModuleItemHolder.PassType passType2 = ModuleItemHolder.PassType.Pass | ModuleItemHolder.PassType.External;
		if ((passType & passType2) == passType2)
		{
			float itemPickupTime = base.block.tank.Holders.GetItemPickupTime(item.ID);
			d.Assert(itemPickupTime != -1f);
			if (Time.time - itemPickupTime < m_HandoverAfterMinInterval)
			{
				return false;
			}
		}
		return true;
	}

	private void OnReleaseItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack)
	{
		m_PickedUpItems.Remove(item.ID);
	}

	private void OnItemPickedUp(EmptyBlockMessage msg)
	{
		PlayItemPickupSfx();
	}

	private void OnChargeChanged(Circuits.BlockChargeData charge)
	{
		UpdateFromCircuitState();
	}

	private void OnConnectedToCircuitNetwork(bool state)
	{
		UpdateFromCircuitState();
	}

	private void PrePool()
	{
		if (m_IsUsedOnCircuit)
		{
			this.SetupForCircuitInput();
		}
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.DragEvent.Subscribe(OnDragBlock);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		m_Holder = GetComponent<ModuleItemHolder>();
		m_Magnet = GetComponent<ModuleItemHolderMagnet>();
		m_Holder.SetAcceptFilterCallback(CanAcceptItem);
		m_Holder.SetReleaseFilterCallback(CanReleaseItem);
		m_Holder.ReleaseItemEvent.Subscribe(OnReleaseItem);
		m_Holder.PickupPriority = m_Priority;
		m_Holder.PickupContentionPeriod = Mathf.Max(m_Holder.PickupContentionPeriod, m_PrePickupPeriod);
		m_SpecificPickupStack.InitReference(m_Holder);
		m_HandOffStack.InitReference(m_Holder);
		m_Holder.SetPrePickupFn(IsPrePickup, m_SpecificPickupStack.stack);
		m_AudioProvider = GetComponent<ModuleAudioProvider>();
		m_VisibleBuckets = new List<Visible>[m_PickupSortBuckets];
		for (int i = 0; i < m_VisibleBuckets.Length; i++)
		{
			m_VisibleBuckets[i] = new List<Visible>();
		}
		m_ItemPickupTrigger = new NetworkedProperty<EmptyBlockMessage>(this, TTMsgType.ItemPickedUpTrigger, OnItemPickedUp, forceUseMessages: true);
		if (m_IsUsedOnCircuit)
		{
			base.block.CircuitNode.ConnectedToOtherNodesEvent.Subscribe(OnConnectedToCircuitNetwork);
			base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
		}
	}

	private void OnSpawn()
	{
		RestrictAcceptItemTypes = null;
	}

	private void OnRecycle()
	{
		RestrictAcceptItemTypes = null;
		if ((bool)m_RangeIndicator)
		{
			m_RangeIndicator.Recycle();
			m_RangeIndicator = null;
		}
		m_PickedUpItems.Clear();
	}

	private void OnDragBlock(ManPointer.DragAction dragAction, Vector3 screenPos)
	{
		bool flag = dragAction == ManPointer.DragAction.Update && Singleton.Manager<ManTechBuilder>.inst.IsBlockHeldInPosition(base.block) && (bool)m_RangeIndicatorPrefab;
		if (flag && !m_RangeIndicator)
		{
			m_RangeIndicator = m_RangeIndicatorPrefab.Spawn(base.block.trans, base.block.centreOfMassWorld);
			m_RangeIndicator.GetComponent<Projector>().orthographicSize = m_PickupRange;
		}
		else if (!flag && (bool)m_RangeIndicator)
		{
			m_RangeIndicator.Recycle();
			m_RangeIndicator = null;
		}
		if (flag)
		{
			m_RangeIndicator.transform.rotation = Quaternion.Euler(90f, m_RangeIndicatorSpinAngle, 0f);
			m_RangeIndicatorSpinAngle = (m_RangeIndicatorSpinAngle + Globals.inst.m_PickupRangeIndicatorSpinSpeed * Time.deltaTime) % 360f;
		}
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.m_PickedUpItems = new List<int>(m_PickedUpItems);
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			m_PickedUpItems = new HashSet<int>(serialData2.m_PickedUpItems);
		}
		else
		{
			m_PickedUpItems.Clear();
		}
	}

	private void OnUpdate()
	{
		if (!IsEnabled || !base.block.IsAttached || IsCircuitInterrupted || Singleton.Manager<ManGameMode>.inst.GetModePhase() != ManGameMode.GameState.InGame)
		{
			return;
		}
		if (ManNetwork.IsHost)
		{
			TryPickupItems();
		}
		int num = 0;
		if (!m_Holder.HasPickupContentionPeriod || !(m_AudioProvider != null))
		{
			return;
		}
		ModuleItemHolder.StackIterator.Enumerator enumerator = m_Holder.Stacks.GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleItemHolder.Stack current = enumerator.Current;
			if (IsPickupStack(current))
			{
				num += current.NumItems;
			}
		}
		bool isNoteOn = num > 0;
		FMODEvent.FMODParams additionalParams = new FMODEvent.FMODParams("count", num);
		m_AudioProvider.SetNoteOn(m_SFXContentionLoop, isNoteOn, additionalParams);
	}
}
