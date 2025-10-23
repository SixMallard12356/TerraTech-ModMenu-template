#define UNITY_EDITOR
public class uScript_ClearItemPickup : uScriptLogic
{
	public bool Out => true;

	public void In(Tank tech)
	{
		if (tech != null)
		{
			BlockManager.BlockIterator<ModuleItemPickup>.Enumerator enumerator = tech.blockman.IterateBlockComponents<ModuleItemPickup>().GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.RestrictAcceptItemTypes = null;
			}
		}
		else
		{
			d.LogError("uScript_RestrictItemPickup.In - tech is null");
		}
	}
}
