#define UNITY_EDITOR
using System;

public class uScript_IsBlockHoldingResources : uScriptLogic
{
	[Serializable]
	public struct ResourceQuantity
	{
		public ChunkTypes m_Resource;

		public int m_Quantity;
	}

	private bool m_Holding;

	private ModuleItemHolder m_Holder;

	public bool True => m_Holding;

	public bool False => !m_Holding;

	public void In(TankBlock block, ResourceQuantity[] resources)
	{
		if (block != null || resources != null)
		{
			if (m_Holder == null || m_Holder.block != block)
			{
				m_Holder = block.GetComponent<ModuleItemHolder>();
			}
			if (m_Holder != null)
			{
				ModuleItemHolder.Stack.ItemIterator enumerator = m_Holder.Contents.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Visible current = enumerator.Current;
					if (!(current.pickup != null))
					{
						continue;
					}
					for (int i = 0; i < resources.Length; i++)
					{
						if (resources[i].m_Resource == (ChunkTypes)current.ItemType)
						{
							resources[i].m_Quantity--;
							break;
						}
					}
				}
				m_Holding = true;
				for (int j = 0; j < resources.Length; j++)
				{
					if (resources[j].m_Quantity > 0)
					{
						m_Holding = false;
						break;
					}
				}
			}
			else
			{
				d.LogError("uScript_IsBlockHoldingResources: Block " + block.name + " does not have a ModuleItemHolder");
				m_Holding = false;
			}
		}
		else
		{
			string text = ((block == null) ? "no block passed in" : "no list of resources passed in");
			d.LogError("uScript_IsBlockHoldingResources: " + text);
			m_Holding = false;
		}
	}

	public void OnDisable()
	{
		m_Holder = null;
		m_Holding = false;
	}
}
