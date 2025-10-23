#define UNITY_EDITOR
using System.Collections.Generic;

public class ItemListRequest : ItemSearcher
{
	private class Node
	{
		public ModuleItemHolder.Stack stack;

		public ModuleItemHolder.Stack nextHop;

		public Node prev;

		public ItemPipe itemPipe;
	}

	private static int s_RequestId = 0;

	private static LinearAllocPool<Node> s_NodePool = new LinearAllocPool<Node>();

	private Node m_StartingNode;

	private PooledLinkedList<Node> m_ExploreList = new PooledLinkedList<Node>();

	private Node m_CurNode;

	private bool m_AcceptingNewNodes;

	private bool m_StructureDirty;

	private int m_FilterCount;

	private int m_ConverterCount;

	private ItemPipeEnd m_RootPipe = new ItemPipeEnd();

	private PooledLinkedList<ItemSearchHandler> m_AllHandlers = new PooledLinkedList<ItemSearchHandler>();

	public ItemListRequest(ModuleItemHolder.Stack startingStack)
	{
		m_StartingNode = new Node
		{
			stack = startingStack
		};
		m_StructureDirty = true;
	}

	public void Clear()
	{
		m_StructureDirty = true;
		DestroySearchStructure();
	}

	public void LookForItems(List<ModuleItemHolder.Stack> initialNodesList, List<ItemTypeInfo> neededItems, HashSet<ItemTypeInfo> missingItems)
	{
		if (m_StructureDirty)
		{
			BuildSearchStructure(initialNodesList);
			m_StructureDirty = false;
		}
		PooledLinkedList<ItemSearchHandler>.Iterator enumerator = m_AllHandlers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			enumerator.Current.HandleSearchRequest();
		}
		TechHolders holders = m_StartingNode.stack.myHolder.block.tank.Holders;
		m_RootPipe.TryBuildNeededItems(holders, neededItems, missingItems);
		m_RootPipe.CleanUpItemsInHierarchy();
	}

	public void FlagStructureAsDirty()
	{
		m_StructureDirty = true;
	}

	public void OnDrawGizmos()
	{
	}

	private void BuildSearchStructure(List<ModuleItemHolder.Stack> initialNodesList)
	{
		DestroySearchStructure();
		s_RequestId++;
		for (int i = 0; i < initialNodesList.Count; i++)
		{
			ModuleItemHolder.Stack stack = initialNodesList[i];
			if (stack.myHolder.ItemRequestHandler != null)
			{
				Node startingNode = m_StartingNode;
				ItemPipe rootPipe = m_RootPipe;
				AddExploreNode(stack, startingNode.stack, startingNode, rootPipe, 1);
			}
		}
		m_StartingNode.stack.myHolder.RequestVisitedTimestamp = s_RequestId;
		int num = 1000000;
		int num2 = 0;
		while (m_ExploreList.Count > 0)
		{
			LinkedListNode<Node> first = m_ExploreList.First;
			m_CurNode = first.Value;
			m_ExploreList.Remove(first);
			ItemSearchHandler itemRequestHandler = m_CurNode.stack.myHolder.ItemRequestHandler;
			d.Assert(itemRequestHandler != null, "We have a holder to explore that doesn't have a request handler");
			if (m_CurNode.stack.myHolder.RequestVisitedTimestamp != s_RequestId)
			{
				if (itemRequestHandler.WantsToKnowAboutSearchRequest())
				{
					m_AllHandlers.Add(itemRequestHandler);
				}
				m_CurNode.stack.myHolder.RequestVisitedTimestamp = s_RequestId;
			}
			m_AcceptingNewNodes = true;
			if (itemRequestHandler != null)
			{
				ItemPipe itemPipe = m_CurNode.itemPipe;
				ModuleItemHolder.Stack prevStack = ((m_CurNode.prev != null) ? m_CurNode.prev.stack : null);
				itemRequestHandler.HandleExpandSearch(this, m_CurNode.stack, prevStack, out var availItems);
				if (availItems == ItemSearchAvailableItems.Processed || availItems == ItemSearchAvailableItems.ProcessedAndNonProcessed)
				{
					bool itemsAreProcessed = true;
					itemPipe.AddItemSource(itemRequestHandler, itemsAreProcessed, m_CurNode.stack.myHolder, m_CurNode.nextHop);
				}
				if (availItems == ItemSearchAvailableItems.ProcessedAndNonProcessed)
				{
					bool itemsAreProcessed2 = false;
					m_CurNode.itemPipe.AddItemSource(itemRequestHandler, itemsAreProcessed2, m_CurNode.stack.myHolder, m_CurNode.nextHop);
				}
			}
			num2++;
			bool num3 = num2 > num;
			d.Assert(!num3, "Recursion error in item list request search");
			if (num3)
			{
				break;
			}
		}
		m_CurNode = null;
		m_ExploreList.Clear();
		s_NodePool.Reset();
		m_FilterCount = 0;
		m_ConverterCount = 0;
		m_RootPipe.DetermineBuildableTypes();
	}

	private void DestroySearchStructure()
	{
		m_RootPipe.DestroyHierarchy();
		m_AllHandlers.Clear();
	}

	private void AddExploreNode(ModuleItemHolder.Stack stack, ModuleItemHolder.Stack nextHopStack, Node prevNode, ItemPipe itemPipe, int debugIndex)
	{
		Node node = s_NodePool.Alloc();
		node.stack = stack;
		node.nextHop = nextHopStack;
		node.prev = prevNode;
		node.itemPipe = itemPipe;
		m_ExploreList.Add(node);
	}

	public void PushNode(ModuleItemHolder.Stack newStack)
	{
		PushNode(newStack, m_CurNode.stack);
	}

	public void PushNode(ModuleItemHolder.Stack newStack, ModuleItemHolder.Stack nextHopStack)
	{
		if (!m_AcceptingNewNodes || newStack.myHolder.ItemRequestHandler == null || nextHopStack == null)
		{
			return;
		}
		bool flag = true;
		if (newStack.myHolder.RequestVisitedTimestamp == s_RequestId)
		{
			Node node = m_CurNode;
			bool flag2 = false;
			while (node != null)
			{
				if (node.stack == newStack)
				{
					flag2 = true;
					break;
				}
				node = node.prev;
			}
			if (flag2 && !m_CurNode.itemPipe.ContainsConverterBeforeLink(node.itemPipe))
			{
				flag = false;
			}
		}
		if (flag)
		{
			int debugIndex = 0;
			AddExploreNode(newStack, nextHopStack, m_CurNode, m_CurNode.itemPipe, debugIndex);
		}
	}

	public void PushConverter(ItemSearchConverter conv)
	{
		d.Assert(conv != null, "Null converter pushed");
		if (conv != null)
		{
			if (m_CurNode.itemPipe.CanAddConverter(conv))
			{
				ItemPipeConverter itemPipeConverter = new ItemPipeConverter();
				itemPipeConverter.Setup(conv, m_CurNode.itemPipe);
				m_CurNode.itemPipe = itemPipeConverter;
				m_ConverterCount++;
			}
			else
			{
				m_AcceptingNewNodes = false;
			}
		}
	}

	public void PushFilter(ItemSearchFilter filter)
	{
		d.Assert(filter != null, "Null filter pushed");
		if (filter != null)
		{
			ItemPipeFilter itemPipeFilter = new ItemPipeFilter();
			itemPipeFilter.Setup(filter, m_CurNode.itemPipe);
			m_CurNode.itemPipe = itemPipeFilter;
			m_FilterCount++;
		}
	}
}
