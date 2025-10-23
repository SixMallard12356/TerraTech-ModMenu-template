#define UNITY_EDITOR
public class uScript_SetCustomRadarTeamID : uScriptLogic
{
	public bool Out => true;

	public void In(Tank tech, int radarTeamID)
	{
		if (tech != null)
		{
			Visible visibleFromObject = Singleton.Manager<ManVisible>.inst.GetVisibleFromObject(tech);
			TrackedVisible trackedVisible = (visibleFromObject ? Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visibleFromObject.ID) : null);
			if (trackedVisible != null)
			{
				trackedVisible.RadarTeamID = radarTeamID;
			}
			else if (visibleFromObject == null)
			{
				d.LogError("uScript_SetCustomRadarTeamID - Could not get visible from passed in object " + tech.ToString());
			}
			else
			{
				d.LogError("uScript_SetCustomRadarTeamID - No tracked visible associated with visible " + visibleFromObject.name);
			}
		}
		else
		{
			d.LogError("uScript_SetCustomRadarTeamID - Null object passed in!");
		}
	}
}
