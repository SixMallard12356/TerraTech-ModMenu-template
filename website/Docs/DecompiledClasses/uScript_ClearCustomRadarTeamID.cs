public class uScript_ClearCustomRadarTeamID : uScriptLogic
{
	public bool Out => true;

	public void In(Tank tech)
	{
		if (tech != null)
		{
			Visible visibleFromObject = Singleton.Manager<ManVisible>.inst.GetVisibleFromObject(tech);
			TrackedVisible trackedVisible = (visibleFromObject ? Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visibleFromObject.ID) : null);
			if (trackedVisible != null)
			{
				trackedVisible.RadarTeamID = int.MaxValue;
			}
		}
	}
}
