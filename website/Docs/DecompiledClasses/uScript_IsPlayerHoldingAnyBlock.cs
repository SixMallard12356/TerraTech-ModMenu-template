[NodePath("TerraTech/Actions/Blocks")]
[NodeDescription("Checks if the player is currently holding a block with the cursor")]
public class uScript_IsPlayerHoldingAnyBlock : uScriptLogic
{
	private bool m_IsHolding;

	public bool Holding => m_IsHolding;

	public bool NotHolding => !m_IsHolding;

	public void In()
	{
		m_IsHolding = (bool)Singleton.Manager<ManPointer>.inst.DraggingItem && Singleton.Manager<ManPointer>.inst.DraggingItem.type == ObjectTypes.Block;
	}

	public void OnEnable()
	{
		m_IsHolding = false;
	}
}
