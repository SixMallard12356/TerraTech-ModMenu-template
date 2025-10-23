#define UNITY_EDITOR
public class uScript_GetScrapperBlockBeingScrapped : uScriptLogic
{
	private bool m_IsOperating;

	private ModuleItemConsume m_ItemConsumer;

	public bool Out => true;

	public bool IsOperating => m_IsOperating;

	public BlockTypes In(TankBlock craftingBlock, ref FactionSubTypes blockFaction)
	{
		m_IsOperating = false;
		BlockTypes blockTypes = BlockTypes._deprecated_01;
		if (craftingBlock != null)
		{
			if (m_ItemConsumer == null)
			{
				m_ItemConsumer = craftingBlock.GetComponent<ModuleItemConsume>();
			}
			if (m_ItemConsumer != null)
			{
				m_IsOperating = m_ItemConsumer.IsOperating;
				if (m_ItemConsumer.Recipe != null)
				{
					if (m_ItemConsumer.Recipe.m_InputItems.Length == 1 && m_ItemConsumer.Recipe.m_InputItems[0].m_Quantity == 1 && m_ItemConsumer.Recipe.m_InputItems[0].m_Item.ObjectType == ObjectTypes.Block)
					{
						blockTypes = (BlockTypes)m_ItemConsumer.Recipe.m_InputItems[0].m_Item.ItemType;
						blockFaction = Singleton.Manager<ManSpawn>.inst.GetCorporation(blockTypes);
					}
					else
					{
						d.LogErrorFormat("uScript_GetScrapperBlockBeingScrapped expect recipe input to be a SINGLE block, but instead got a different recipe: {0}", m_ItemConsumer.Recipe);
					}
				}
			}
			else
			{
				d.LogError("uScript_GetScrapperBlockBeingScrapped: block doesn't have an item consumer module");
			}
		}
		else
		{
			d.LogError("uScript_GetScrapperBlockBeingScrapped: crafting block is null");
		}
		return blockTypes;
	}

	private void OnDisable()
	{
		m_ItemConsumer = null;
	}
}
