#define UNITY_EDITOR
using UnityEngine;

[FriendlyName("uScript_SetEncounterWaypointVisibility", "Show or hide the waypoint marker for the parent encounter")]
[NodePath("TerraTech/UI")]
public class uScript_SetEncounterWaypointVisibility : uScriptLogic
{
	private Encounter m_EncounterObject;

	public bool Out => true;

	public void Show(GameObject ownerNode)
	{
		SetVisiblity(ownerNode, show: true);
	}

	public void Hide(GameObject ownerNode)
	{
		SetVisiblity(ownerNode, show: false);
	}

	private void SetVisiblity(GameObject ownerNode, bool show)
	{
		if ((bool)ownerNode)
		{
			if (!m_EncounterObject)
			{
				m_EncounterObject = ownerNode.GetComponent<Encounter>();
			}
			bool waypointOverlayEnabled = true;
			if (!show && Singleton.Manager<ManQuestLog>.inst.TrackedEncounterId == m_EncounterObject.EncounterDef)
			{
				waypointOverlayEnabled = false;
			}
			m_EncounterObject.DefaultWaypoint.WaypointOverlayEnabled = waypointOverlayEnabled;
		}
		else
		{
			d.LogError("uScript_SetEncounterWaypointVisibility - owner is null");
		}
	}

	private void OnDisable()
	{
		Singleton.Manager<ManOverlay>.inst.ForceHideWaypointOverlay = false;
	}
}
