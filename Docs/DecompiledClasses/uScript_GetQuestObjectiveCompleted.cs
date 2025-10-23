#define UNITY_EDITOR
using UnityEngine;

[NodePath("TerraTech/Actions/Encounters")]
[FriendlyName("uScript_GetQuestObjectiveCompleted", "Check if a numbered quest sub-objective is completed in the log")]
public class uScript_GetQuestObjectiveCompleted : uScriptLogic
{
	private Encounter m_Encounter;

	private bool m_Completed;

	public bool Complete => m_Completed;

	public bool Incomplete => !m_Completed;

	public void In([FriendlyName("Owner Node", "Owner Node of Encounter")] GameObject owner, [FriendlyName("Objective ID", "Index of the targeted objective. 1 based")] int objectiveId)
	{
		m_Completed = false;
		if (m_Encounter == null && owner != null)
		{
			m_Encounter = owner.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			if (m_Encounter.QuestLog != null)
			{
				m_Completed = m_Encounter.QuestLog.GetObjectiveCompleted(objectiveId - 1);
			}
			else
			{
				d.LogError("ERROR - uScript_GetQuestObjectiveCompleted - Encounter " + owner.name + " Does not have a valid QuestLog");
			}
		}
		else
		{
			d.LogError("ERROR - uScript_GetQuestObjectiveCompleted - Failed to get encounter!");
		}
	}

	public void OnDisable()
	{
		m_Encounter = null;
		m_Completed = false;
	}
}
