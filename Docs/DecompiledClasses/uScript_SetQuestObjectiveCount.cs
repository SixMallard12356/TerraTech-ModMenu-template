#define UNITY_EDITOR
using UnityEngine;

[NodePath("TerraTech/Actions/Encounters")]
[FriendlyName("Set Quest log Objective count", "Sets the progress count of a numbered objective towards the goal amount")]
public class uScript_SetQuestObjectiveCount : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void SetCount([FriendlyName("Owner Node", "Owner Node of Encounter")] GameObject owner, [FriendlyName("Objective ID", "Index of the targeted objective. 1 based")] int objectiveId, [FriendlyName("Current Count", "The new count of the objective progress")] int currentCount)
	{
		if (m_Encounter == null && owner != null)
		{
			m_Encounter = owner.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			m_Encounter.QuestLog.SetObjectiveCount(objectiveId - 1, currentCount);
		}
		else
		{
			d.LogError("ERROR - uScript_SetQuestObjectiveCount - Failed to get encounter!");
		}
	}
}
