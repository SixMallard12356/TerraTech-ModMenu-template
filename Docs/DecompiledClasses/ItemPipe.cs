#define UNITY_EDITOR
using System;
using System.Collections.Generic;

public abstract class ItemPipe
{
	private struct ItemSource
	{
		public ItemSearchHandler handler;

		public bool itemsAreProcessed;

		public ModuleItemHolder holder;

		public ModuleItemHolder.Stack nextHop;
	}

	public enum Pass
	{
		BuildAllSimultaneously,
		BuildWhatPossible
	}

	protected static LinearAllocPool<AnonymousOfferedItem> s_AnonymousPool = new LinearAllocPool<AnonymousOfferedItem>();

	protected static LinearAllocPool<ConcreteOfferedItem> s_ConcretePool = new LinearAllocPool<ConcreteOfferedItem>();

	protected static ItemPipeCollector s_ItemCollector = new ItemPipeCollector(s_ConcretePool, s_AnonymousPool);

	protected ItemPipe m_Parent;

	protected List<ItemPipe> m_Children = new List<ItemPipe>();

	private int m_ChildExpandPriority = -1;

	private HashSet<ItemTypeInfo> m_BuildableItemTypes = new HashSet<ItemTypeInfo>();

	private List<ItemSource> m_ItemSources = new List<ItemSource>(64);

	private bool m_ItemSourcesExpanded;

	private List<OfferedItem> m_AvailableItems = new List<OfferedItem>();

	private static Comparison<ItemPipe> s_PriorityComparer = PriorityComparer;

	public HashSet<ItemTypeInfo>.Enumerator BuildableTypes => m_BuildableItemTypes.GetEnumerator();

	public void DestroyHierarchy()
	{
		foreach (ItemPipe child in m_Children)
		{
			child.DestroyHierarchy();
		}
		m_Children.Clear();
		m_Parent = null;
		Destroy();
	}

	public void CleanUpItemsInHierarchy()
	{
		CleanUpItemsInHierarchyRecursive();
		s_AnonymousPool.Reset();
		s_ConcretePool.Reset();
	}

	private void CleanUpItemsInHierarchyRecursive()
	{
		foreach (ItemPipe child in m_Children)
		{
			child.CleanUpItemsInHierarchyRecursive();
		}
		m_ItemSourcesExpanded = false;
		m_AvailableItems.Clear();
	}

	public void AddItemSource(ItemSearchHandler searchHandler, bool itemsAreProcessed, ModuleItemHolder holder, ModuleItemHolder.Stack nextHop)
	{
		m_ItemSources.Add(new ItemSource
		{
			handler = searchHandler,
			itemsAreProcessed = itemsAreProcessed,
			holder = holder,
			nextHop = nextHop
		});
	}

	protected bool TrySourceItemLocally(ItemTypeInfo item, ItemBuildContext context)
	{
		bool result = false;
		if (!m_ItemSourcesExpanded)
		{
			for (int i = 0; i < m_ItemSources.Count; i++)
			{
				ItemSource itemSource = m_ItemSources[i];
				s_ItemCollector.DeliverItemsToPipe(itemSource.handler, itemSource.itemsAreProcessed, this, itemSource.holder, itemSource.nextHop);
			}
			m_ItemSourcesExpanded = true;
		}
		for (int j = 0; j < m_AvailableItems.Count; j++)
		{
			if (m_AvailableItems[j].GetTypeInfo() == item && !context.IsItemUsed(m_AvailableItems[j]))
			{
				result = true;
				context.AddUsedItem(m_AvailableItems[j]);
				break;
			}
		}
		return result;
	}

	protected void SetParent(ItemPipe parent)
	{
		d.Assert(m_Parent == null, "Cannot set parent more than once on an ItemPipe");
		m_Parent = parent;
		m_Parent.m_Children.Add(this);
		m_ChildExpandPriority = 0;
	}

	protected void PrioritySortChildrenRecursive()
	{
		int count = m_Children.Count;
		bool flag = false;
		for (int i = 0; i < count; i++)
		{
			ItemPipe itemPipe = m_Children[i];
			flag = itemPipe.UpdatePriority() || flag;
			itemPipe.PrioritySortChildrenRecursive();
		}
		if (count > 1 && flag)
		{
			m_Children.Sort(s_PriorityComparer);
		}
	}

	private bool UpdatePriority()
	{
		return m_ChildExpandPriority != (m_ChildExpandPriority = EvaluatePriority());
	}

	protected virtual int EvaluatePriority()
	{
		return 0;
	}

	private static int PriorityComparer(ItemPipe a, ItemPipe b)
	{
		return b.m_ChildExpandPriority.CompareTo(a.m_ChildExpandPriority);
	}

	protected void AddBuildableType(ItemTypeInfo type)
	{
		m_BuildableItemTypes.Add(type);
	}

	public bool CanBuildItemFromScratch(ItemTypeInfo type)
	{
		return m_BuildableItemTypes.Contains(type);
	}

	protected virtual void Destroy()
	{
		m_BuildableItemTypes.Clear();
		m_ItemSources.Clear();
	}

	public virtual void OfferItem(OfferedItem item)
	{
		m_AvailableItems.Add(item);
	}

	public abstract bool TryBuildItem(ItemTypeInfo item, ItemBuildContext context, Pass pass, out bool hasMissingInputs);

	public bool ContainsConverterBeforeLink(ItemPipe endLink)
	{
		bool result = false;
		ItemPipe itemPipe = this;
		while (itemPipe != null && itemPipe != endLink)
		{
			if (itemPipe.GetConverter() != null)
			{
				result = true;
				break;
			}
			itemPipe = itemPipe.m_Parent;
		}
		return result;
	}

	public bool CanAddConverter(ItemSearchConverter conv)
	{
		bool result = true;
		int num = 0;
		for (ItemPipe itemPipe = this; itemPipe != null; itemPipe = itemPipe.m_Parent)
		{
			ItemSearchConverter converter = itemPipe.GetConverter();
			if (converter != null)
			{
				num++;
				if (converter == conv || num >= 5)
				{
					result = false;
					break;
				}
			}
		}
		return result;
	}

	protected virtual ItemSearchConverter GetConverter()
	{
		return null;
	}

	public abstract void DetermineBuildableTypes();
}
