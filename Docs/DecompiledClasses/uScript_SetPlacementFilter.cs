using UnityEngine;

[FriendlyName("Set block build filter")]
public class uScript_SetPlacementFilter : uScriptLogic
{
	public bool Out => true;

	public void In(Vector3[] placementFilter)
	{
		Singleton.Manager<ManTechBuildingTutorial>.inst.FilterPlacementPositions(placementFilter);
	}
}
