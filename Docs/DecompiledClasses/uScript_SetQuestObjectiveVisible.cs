#define UNITY_EDITOR
using UnityEngine;

[NodePath("TerraTech/Actions/Encounters")]
[FriendlyName("Set Quest Objective Visible", "Sets visibility of a numbered quest sub-objective in the log")]
public class uScript_SetQuestObjectiveVisible : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void SetVisible([FriendlyName("Owner Node", "Owner Node of Encounter")] GameObject owner, [FriendlyName("Objective ID", "Index of the targeted objective. 1 based")] int objectiveId, [FriendlyName("Visible", "Flag indicating whether to show or hide the objective")][SocketState(false, false)][DefaultValue(true)] bool visible)
	{
		if (m_Encounter == null && owner != null)
		{
			m_Encounter = owner.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			if (m_Encounter.QuestLog != null)
			{
				m_Encounter.QuestLog.SetObjectiveVisible(objectiveId - 1, visible);
			}
			else
			{
				d.LogError("ERROR - uScript_SetQuestObjectiveVisible - Encounter " + owner.name + " Does not have a valid QuestLog");
			}
		}
		else
		{
			d.LogError("ERROR - uScript_SetQuestObjectiveVisible - Failed to get encounter!");
		}
	}
}
