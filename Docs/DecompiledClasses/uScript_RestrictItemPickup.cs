#define UNITY_EDITOR
public class uScript_RestrictItemPickup : uScriptLogic
{
	public bool Out => true;

	public void In(Tank tech, ChunkTypes[] typesToAccept)
	{
		if (tech != null)
		{
			if (typesToAccept != null)
			{
				int[] array = new int[typesToAccept.Length];
				for (int i = 0; i < typesToAccept.Length; i++)
				{
					array[i] = (int)typesToAccept[i];
				}
				BlockManager.BlockIterator<ModuleItemPickup>.Enumerator enumerator = tech.blockman.IterateBlockComponents<ModuleItemPickup>().GetEnumerator();
				while (enumerator.MoveNext())
				{
					enumerator.Current.RestrictAcceptItemTypes = array;
				}
			}
			else
			{
				d.LogError("uScript_RestrictItemPickup.In - typesToAccept is null, to Clear use uScript_ClearItemPickup instead");
			}
		}
		else
		{
			d.LogError("uScript_RestrictItemPickup.In - tech is null");
		}
	}
}
