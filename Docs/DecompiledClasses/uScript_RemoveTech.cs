#define UNITY_EDITOR
using UnityEngine.Networking;

public class uScript_RemoveTech : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("In", "Remove a Tech from the game")]
	public void In(Tank tech)
	{
		if (tech != null)
		{
			d.Log($"uScript_RemoveTech - removing Tech ID={tech.visible.ID} name={tech.name}");
			if (tech.netTech != null)
			{
				NetworkServer.UnSpawn(tech.netTech.gameObject);
			}
			tech.visible.RemoveFromGame();
		}
		else
		{
			d.LogError("uScript_RemoveTech - no Tech passed in");
		}
	}
}
