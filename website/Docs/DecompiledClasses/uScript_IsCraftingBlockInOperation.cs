#define UNITY_EDITOR
public class uScript_IsCraftingBlockInOperation : uScriptLogic
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
				m_True = m_ItemConsumer.IsOperating;
			}
			else
			{
				d.LogError("uScript_IsCraftingBlockInOperation: block doesn't have an item consumer module");
			}
		}
		else
		{
			d.LogError("uScript_IsCraftingBlockInOperation: crafting block is null");
		}
	}

	public void OnEnable()
	{
		m_ItemConsumer = null;
	}
}
