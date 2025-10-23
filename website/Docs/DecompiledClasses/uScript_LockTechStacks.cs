[NodeToolTip("Locks out ability to grab chunks from resource holders!)")]
public class uScript_LockTechStacks : uScriptLogic
{
	public bool Out => true;

	public void In(Tank tech)
	{
		if (!(tech != null))
		{
			return;
		}
		BlockManager.BlockIterator<ModuleItemHolder>.Enumerator enumerator = tech.blockman.IterateBlockComponents<ModuleItemHolder>().GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleItemHolder.StackIterator.Enumerator enumerator2 = enumerator.Current.Stacks.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				enumerator2.Current.LockStackUserPickup();
			}
		}
	}
}
