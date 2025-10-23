#define UNITY_EDITOR
[NodePath("TerraTech/Actions/Blocks")]
[FriendlyName("uScript_AccessListBlockSpawnData", "Access the contents of a list. May return the first or last item, a random item, or the item at a specific index.")]
public class uScript_AccessListBlockSpawnData : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("At Index")]
	public void AtIndex([FriendlyName("List", "The list to operate on.")] SpawnBlockData[] dataList, [FriendlyName("Index", "The index or position of the item to return. If the list contains 5 items, the valid range is 0-4, where 0 is the first item. (this parameter is only used with the At Index input).")] int index, [FriendlyName("Selected", "The selected variable.")] out SpawnBlockData value)
	{
		bool flag = false;
		if (index < 0 || index >= dataList.Length)
		{
			flag = true;
		}
		if (flag)
		{
			string text = "[Access List (SpawnBlockData)] You are trying to use an index number that is out of range for this list variable!";
			if (dataList == null || dataList.Length == 0)
			{
				value = default(SpawnBlockData);
				text += "empty default object was returned instead.";
			}
			else
			{
				value = dataList[0];
				text += "Index 0 was returned instead.";
			}
			d.LogError(text);
		}
		else
		{
			value = dataList[index];
		}
	}
}
