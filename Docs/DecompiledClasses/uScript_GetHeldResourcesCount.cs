#define UNITY_EDITOR
[FriendlyName("uScript_GetHeldResourcesCount", "Returns the amount of a specified resource chunk held by a specified block")]
[NodePath("TerraTech/Actions/Crafting")]
public class uScript_GetHeldResourcesCount : uScriptLogic
{
	private ModuleItemHolder m_Holder;

	public bool Out => true;

	public int In([FriendlyName("Block", "Block to check")] TankBlock block, [FriendlyName("Resource Type", "Resource type to count")] ChunkTypes resourceType)
	{
		int num = 0;
		if (block != null)
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
					if (current.pickup != null && resourceType == (ChunkTypes)current.ItemType)
					{
						num++;
					}
				}
			}
			else
			{
				d.LogError("uScript_GetHeldResourcesCount: block does not have an item holder");
			}
		}
		else
		{
			d.LogError("uScript_GetHeldResourcesCount: no block passed in");
		}
		return num;
	}

	public void OnDisable()
	{
		m_Holder = null;
	}
}
