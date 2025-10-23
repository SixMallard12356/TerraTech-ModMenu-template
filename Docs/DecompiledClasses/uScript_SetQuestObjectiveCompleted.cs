#define UNITY_EDITOR
using UnityEngine;

[FriendlyName("Set Quest Objective Completed", "Marks a numbered quest sub-objective as completed in the log")]
[NodePath("TerraTech/Actions/Encounters")]
public class uScript_SetQuestObjectiveCompleted : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void MarkCompleted([FriendlyName("Owner Node", "Owner Node of Encounter")] GameObject owner, [FriendlyName("Objective ID", "Index of the targeted objective. 1 based")] int objectiveId, [DefaultValue(true)][FriendlyName("Completed", "Set objective to be either complete or incomplete")] bool completed)
	{
		if (m_Encounter == null && owner != null)
		{
			m_Encounter = owner.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			if (m_Encounter.QuestLog != null)
			{
				m_Encounter.QuestLog.SetObjectiveCompleted(objectiveId - 1, completed);
			}
			else
			{
				d.LogError("ERROR - uScript_SetQuestObjectiveCompleted - Encounter " + owner.name + " Does not have a valid QuestLog");
			}
		}
		else
		{
			d.LogError("ERROR - uScript_SetQuestObjectiveCompleted - Failed to get encounter!");
		}
	}
}
