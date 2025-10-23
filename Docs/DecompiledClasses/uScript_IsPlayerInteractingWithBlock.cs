[FriendlyName("Player/Is player interacting with block")]
[NodeDescription("Dragging takes priorty over interacted, interacted will be false when dragging")]
public class uScript_IsPlayerInteractingWithBlock : uScriptLogic
{
	private bool m_HasInteracted;

	private bool m_IsInteracting;

	public bool Out => false;

	public bool Interacted
	{
		get
		{
			if (m_HasInteracted)
			{
				return !m_IsInteracting;
			}
			return false;
		}
	}

	public bool NotInteracted => !m_HasInteracted;

	public bool Dragging => m_IsInteracting;

	public bool NotDragging => !m_IsInteracting;

	public void In(TankBlock block)
	{
		if ((bool)block)
		{
			Visible draggingItem = Singleton.Manager<ManPointer>.inst.DraggingItem;
			m_IsInteracting = (bool)draggingItem && draggingItem.block.IsNotNull() && draggingItem.block == block;
			if (!m_IsInteracting && Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				for (int i = 0; i < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); i++)
				{
					NetPlayer player = Singleton.Manager<ManNetwork>.inst.GetPlayer(i);
					if (player.IsNotNull() && player.IsHoldingBlock(block))
					{
						m_IsInteracting = true;
						break;
					}
				}
			}
			m_HasInteracted |= m_IsInteracting;
		}
		else
		{
			m_IsInteracting = false;
		}
	}

	public void OnDisable()
	{
		m_HasInteracted = false;
		m_IsInteracting = false;
	}
}
