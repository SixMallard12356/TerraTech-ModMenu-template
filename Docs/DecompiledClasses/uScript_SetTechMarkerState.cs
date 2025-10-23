#define UNITY_EDITOR
[NodePath("TerraTech/Techs")]
[FriendlyName("uScript_SetTechMarkerState", "Show or hide the marker above a tech and its radar icon")]
public class uScript_SetTechMarkerState : uScriptLogic
{
	public bool Out => true;

	public void Show(Tank tech)
	{
		ShowTechMarkers(tech, show: true);
	}

	public void Hide(Tank tech)
	{
		ShowTechMarkers(tech, show: false);
	}

	private void ShowTechMarkers(Tank tech, bool show)
	{
		if (tech == null)
		{
			d.LogError("uScript_SetTechMarkerState - Tech is null");
			return;
		}
		Visible visibleFromObject = Singleton.Manager<ManVisible>.inst.GetVisibleFromObject(tech);
		TrackedVisible trackedVisible = (visibleFromObject ? Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visibleFromObject.ID) : null);
		if (trackedVisible == null)
		{
			d.LogError("uScript_SetTechMarkerState - TrackedVisible is null");
			return;
		}
		RadarTypes radarTypes = ((!tech.IsBase) ? RadarTypes.Vehicle : RadarTypes.Base);
		trackedVisible.RadarType = (show ? radarTypes : RadarTypes.Hidden);
		if (tech.ShouldShowOverlay != show)
		{
			tech.ShouldShowOverlay = show;
			if (tech.netTech != null && ManNetwork.IsNetworked)
			{
				tech.netTech.OnServerSetShowOverlayDirty();
			}
		}
	}

	public void OnDisable()
	{
	}
}
