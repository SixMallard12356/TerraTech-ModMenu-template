#define UNITY_EDITOR
[FriendlyName("uScript_AccessListTech", "Access the contents of a list. May return the first or last item, a random item, or the item at a specific index.")]
[NodePath("TerraTech/Actions/Techs")]
public class uScript_AccessListTech : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("At Index")]
	public void AtIndex([FriendlyName("List", "The list to operate on.")] ref Tank[] techList, [FriendlyName("Index", "The index or position of the item to return. If the list contains 5 items, the valid range is 0-4, where 0 is the first item. (this parameter is only used with the At Index input).")] int index, [FriendlyName("Selected", "The selected variable.")] out Tank value)
	{
		bool flag = false;
		if (index < 0 || index >= techList.Length)
		{
			flag = true;
		}
		if (flag)
		{
			string text = "[Access List (Tech)] You are trying to use an index number that is out of range for this list variable.";
			if (techList == null || techList.Length == 0)
			{
				value = null;
				text += "null was returned instead.";
			}
			else
			{
				value = techList[0];
				text += "Index 0 was returned instead.";
			}
			d.LogError(text);
		}
		else
		{
			value = techList[index];
			if (techList[index] != null && techList[index].IsBeingRecycled())
			{
				d.LogWarningFormat("uScript_AccessListTech on a tech that's being recycled '{0}' ", (techList[index] == null) ? "" : techList[index].name);
			}
		}
	}
}
