#define UNITY_EDITOR
public class uScript_IsCraftingBlockProducingItem : uScriptLogic
{
	private bool m_True;

	private ModuleItemConsume m_ItemConsumer;

	public bool True => m_True;

	public bool False => !m_True;

	public void In(TankBlock craftingBlock)
	{
		m_True = false;
		if (craftingBlock != null)
		{
			if (!m_ItemConsumer)
			{
				m_ItemConsumer = craftingBlock.GetComponent<ModuleItemConsume>();
			}
			if (m_ItemConsumer != null)
			{
				m_True = m_ItemConsumer.IsProducingItem;
			}
			else
			{
				d.LogError("uScript_IsCraftingBlockProducingItem: block doesn't have an item consumer module");
			}
		}
		else
		{
			d.LogError("uScript_IsCraftingBlockProducingItem: crafting block is null");
		}
	}
}
