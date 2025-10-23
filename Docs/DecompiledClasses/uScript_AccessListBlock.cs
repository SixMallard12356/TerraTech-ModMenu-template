#define UNITY_EDITOR
[FriendlyName("uScript_AccessListBlock", "Access the contents of a list. May return the first or last item, a random item, or the item at a specific index.")]
[NodePath("TerraTech/Actions/Blocks")]
public class uScript_AccessListBlock : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("At Index")]
	public void AtIndex([FriendlyName("List", "The list to operate on.")] ref TankBlock[] blockList, [FriendlyName("Index", "The index or position of the item to return. If the list contains 5 items, the valid range is 0-4, where 0 is the first item. (this parameter is only used with the At Index input).")] int index, [FriendlyName("Selected", "The selected variable.")] out TankBlock value)
	{
		bool flag = false;
		if (index < 0 || index >= blockList.Length)
		{
			flag = true;
		}
		if (flag)
		{
			string text = "[Access List (Block)] You are trying to use an index number that is out of range for this list variable.";
			if (blockList == null || blockList.Length == 0)
			{
				value = null;
				text += "null was returned instead.";
			}
			else
			{
				value = blockList[0];
				text += "Index 0 was returned instead.";
			}
			d.LogError(text);
		}
		else
		{
			value = blockList[index];
		}
	}
}
