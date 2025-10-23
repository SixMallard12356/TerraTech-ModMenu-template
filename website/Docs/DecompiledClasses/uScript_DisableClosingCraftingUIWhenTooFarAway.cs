#define UNITY_EDITOR
[NodeToolTip("Prevents the crafting UI from closing itself if the player moves too far away.")]
public class uScript_DisableClosingCraftingUIWhenTooFarAway : uScriptLogic
{
	private ModuleItemConsume m_ItemConsumer;

	public bool Out => true;

	public void DisableAutoCloseUI(TankBlock craftingBlock)
	{
		SetAutoCloseEnabled(craftingBlock, autoCloseEnabled: false);
	}

	public void EnableAutoCloseUI(TankBlock craftingBlock)
	{
		SetAutoCloseEnabled(craftingBlock, autoCloseEnabled: true);
	}

	private void SetAutoCloseEnabled(TankBlock craftingBlock, bool autoCloseEnabled)
	{
		if (craftingBlock != null)
		{
			if (!m_ItemConsumer)
			{
				m_ItemConsumer = craftingBlock.GetComponent<ModuleItemConsume>();
			}
			if (m_ItemConsumer != null)
			{
				m_ItemConsumer.DisableClosingCraftingUIWhenTooFar = !autoCloseEnabled;
			}
			else
			{
				d.LogError("uScript_DisableClosingCraftingUIWhenTooFarAway: Block doesn't have an item consumer module!");
			}
		}
		else
		{
			d.LogError("uScript_DisableClosingCraftingUIWhenTooFarAway: crafting block is null");
		}
	}

	private void OnEnable()
	{
		m_ItemConsumer = null;
	}
}
