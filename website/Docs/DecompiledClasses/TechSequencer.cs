#define UNITY_EDITOR
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class TechSequencer : TechComponent
{
	public enum ChainType
	{
		EnergyModule,
		ShieldBubble,
		RepairBubble
	}

	public class SequenceNode
	{
		public Event<SequenceNode> OnCompleteChanged;

		private TankBlock m_Block;

		public int OwnerInstanceId { get; private set; }

		public bool Complete { get; private set; }

		public SequenceNode(TankBlock block)
		{
			m_Block = block;
			OwnerInstanceId = block.GetInstanceID();
			Complete = false;
		}

		public bool CheckIsReady()
		{
			bool result = false;
			if ((bool)m_Block.tank)
			{
				result = m_Block.tank.Sequencer.IsNodeReady(this);
			}
			return result;
		}

		public void SetComplete(bool complete)
		{
			if (complete != Complete)
			{
				Complete = complete;
				OnCompleteChanged.Send(this);
			}
		}
	}

	private class Chain
	{
		public List<ChainItem> items;

		public bool dirty;

		public bool disabled;

		public Chain prevChain;

		public Chain nextChain;
	}

	private struct ChainItem
	{
		public Chain chain;

		public SequenceNode node;

		public bool ready;
	}

	private Chain[] m_Chains;

	private Dictionary<int, ChainItem> m_Lookup;

	public TechSequencer()
	{
		int count = EnumValuesIterator<ChainType>.Count;
		m_Chains = new Chain[count];
		for (int i = 0; i < count; i++)
		{
			m_Chains[i] = new Chain
			{
				items = new List<ChainItem>()
			};
		}
		m_Chains[2].prevChain = m_Chains[1];
		m_Chains[1].nextChain = m_Chains[2];
		m_Lookup = new Dictionary<int, ChainItem>();
	}

	public void RegisterNode(SequenceNode node, ChainType chainType)
	{
		int ownerInstanceId = node.OwnerInstanceId;
		bool flag = m_Lookup.ContainsKey(ownerInstanceId);
		d.AssertFormat(!flag, "TechStartupSequencer.RegisterItem passed already registered node {0}", ownerInstanceId);
		if (!flag)
		{
			d.AssertFormat((int)chainType < m_Chains.Length, "TechStartupSequencer.RegisterItem references invalid chain {0}", chainType);
			if ((int)chainType < m_Chains.Length)
			{
				Chain chain = m_Chains[(int)chainType];
				ChainItem chainItem = new ChainItem
				{
					chain = chain,
					node = node,
					ready = false
				};
				chain.items.Add(chainItem);
				m_Lookup.Add(ownerInstanceId, chainItem);
				node.OnCompleteChanged.Subscribe(OnCompleteChanged);
				SetChainDirty(chain);
			}
		}
	}

	public void UnregisterNode(SequenceNode item)
	{
		int ownerInstanceId = item.OwnerInstanceId;
		if (m_Lookup.TryGetValue(ownerInstanceId, out var value))
		{
			value.chain.items.Remove(value);
			m_Lookup.Remove(ownerInstanceId);
			item.OnCompleteChanged.Unsubscribe(OnCompleteChanged);
			SetChainDirty(value.chain);
		}
		else
		{
			d.AssertFormat(false, "TechStartupSequencer.UnregisterItem cannot find node with id {0}", ownerInstanceId);
		}
	}

	public bool IsNodeReady(SequenceNode node)
	{
		int ownerInstanceId = node.OwnerInstanceId;
		bool result = false;
		if (m_Lookup.TryGetValue(ownerInstanceId, out var value))
		{
			if (value.chain.dirty)
			{
				RecalculateReady(value.chain);
				result = m_Lookup[ownerInstanceId].ready;
			}
			else
			{
				result = value.ready;
			}
		}
		else
		{
			d.AssertFormat(false, "TechStartupSequencer.IsItemReady cannot find node with id {0}", ownerInstanceId);
		}
		return result;
	}

	public void SetChainDisabled(ChainType chainType, bool disabled)
	{
		m_Chains[(int)chainType].disabled = disabled;
	}

	private void RecalculateReady(Chain chain)
	{
		chain.dirty = false;
		Chain prevChain = chain.prevChain;
		if (prevChain != null && prevChain.dirty)
		{
			RecalculateReady(prevChain);
		}
		bool flag = true;
		if (prevChain != null && !prevChain.disabled && prevChain.items.Count > 0 && !prevChain.items[prevChain.items.Count - 1].node.Complete)
		{
			flag = false;
		}
		List<ChainItem> items = chain.items;
		int count = items.Count;
		for (int i = 0; i < count; i++)
		{
			ChainItem value = items[i];
			if (value.ready != flag)
			{
				value.ready = flag;
				items[i] = value;
				m_Lookup[value.node.OwnerInstanceId] = value;
			}
			if (!value.node.Complete)
			{
				flag = false;
			}
		}
	}

	private void SetChainDirty(Chain chain)
	{
		chain.dirty = true;
		if (chain.nextChain != null)
		{
			SetChainDirty(chain.nextChain);
		}
	}

	private void OnCompleteChanged(SequenceNode item)
	{
		int ownerInstanceId = item.OwnerInstanceId;
		if (m_Lookup.TryGetValue(ownerInstanceId, out var value))
		{
			SetChainDirty(value.chain);
			return;
		}
		d.AssertFormat(false, "TechStartupSequencer.OnCompleteChanged cannot find item with id {0}", ownerInstanceId);
	}

	public void ForceAllChainItemsReady(ChainType chainType)
	{
		Chain chain = m_Chains[(int)chainType];
		for (int i = 0; i < chain.items.Count; i++)
		{
			ChainItem value = chain.items[i];
			value.ready = true;
			chain.items[i] = value;
			m_Lookup[value.node.OwnerInstanceId] = value;
		}
		chain.dirty = false;
	}
}
