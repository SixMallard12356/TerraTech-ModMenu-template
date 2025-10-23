#define UNITY_EDITOR
[FriendlyName("uScript_AccessListChunk", "Access the contents of a list. May return the first or last item, a random item, or the item at a specific index.")]
[NodePath("TerraTech/Actions/Crafting")]
public class uScript_AccessListChunk : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("At Index")]
	public void AtIndex([FriendlyName("List", "The list to operate on.")] ref ChunkTypes[] chunkList, [FriendlyName("Index", "The index or position of the item to return. If the list contains 5 items, the valid range is 0-4, where 0 is the first item. (this parameter is only used with the At Index input).")] int index, [FriendlyName("Selected", "The selected variable.")] out ChunkTypes value)
	{
		bool flag = false;
		if (index < 0 || index >= chunkList.Length)
		{
			flag = true;
		}
		if (flag)
		{
			string text = "uScript_AccessListChunk: You are trying to use an index number that is out of range for this list variable.";
			if (chunkList == null || chunkList.Length == 0)
			{
				value = ChunkTypes.Wood;
				text += "null was returned instead.";
			}
			else
			{
				value = chunkList[0];
				text += "Index 0 was returned instead.";
			}
			d.LogError(text);
		}
		else
		{
			value = chunkList[index];
		}
	}
}
