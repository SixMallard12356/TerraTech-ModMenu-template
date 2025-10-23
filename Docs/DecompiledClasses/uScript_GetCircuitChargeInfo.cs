[NodePath("TerraTech/Actions/Blocks")]
[NodeToolTip("View the current circuit signal")]
[NodeDescription("Get the circuit signal value from a block that has a valid configuration")]
[FriendlyName("Get Block Circuit Signal")]
public class uScript_GetCircuitChargeInfo : uScriptLogic
{
	protected bool m_HasBlock;

	protected uint m_CurTankBlockID;

	protected bool m_CurTankBlockValid;

	protected Tank m_CurTech;

	protected int m_CurTechBlockCount;

	protected ModuleCircuitNode m_CircuitNode;

	public bool Out => true;

	public bool HasValidBlock => m_CurTankBlockValid;

	protected int SignalValue
	{
		get
		{
			if (!m_CurTankBlockValid)
			{
				return 0;
			}
			return m_CircuitNode.CurrentHighestCharge;
		}
	}

	public int In(TankBlock block, [SocketState(false, false)] Tank tech = null, [SocketState(false, false)] BlockTypes blockType = BlockTypes.GSOAIController_111)
	{
		if (m_CurTech != tech)
		{
			m_CurTech = tech;
			m_CurTechBlockCount = 0;
		}
		if (m_CurTech != null && block == null)
		{
			if (m_CurTech.blockman.blockCount != m_CurTechBlockCount)
			{
				m_CurTechBlockCount = m_CurTech.blockman.blockCount;
				BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = m_CurTech.blockman.IterateBlocks().GetEnumerator();
				while (enumerator.MoveNext())
				{
					TankBlock current = enumerator.Current;
					if (current.BlockType == blockType)
					{
						block = current;
						break;
					}
				}
			}
			else if (m_HasBlock)
			{
				block = m_CurTech.blockman.GetBlockWithID(m_CurTankBlockID);
			}
		}
		if (block == null && m_HasBlock)
		{
			Reset();
			return SignalValue;
		}
		if (m_HasBlock && block.blockPoolID == m_CurTankBlockID)
		{
			return SignalValue;
		}
		m_CurTankBlockID = block.blockPoolID;
		m_HasBlock = true;
		m_CircuitNode = block.CircuitNode;
		m_CurTankBlockValid = m_CircuitNode != null;
		return SignalValue;
	}

	private void Reset()
	{
		m_CurTech = null;
		m_CurTechBlockCount = 0;
		m_HasBlock = false;
		m_CurTankBlockID = 0u;
		m_CurTankBlockValid = false;
		m_CircuitNode = null;
	}

	public void OnDisable()
	{
		Reset();
	}
}
