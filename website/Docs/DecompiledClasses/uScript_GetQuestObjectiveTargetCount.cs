#define UNITY_EDITOR
using UnityEngine;

[FriendlyName("Get Quest Objective Target Count", "Gets the goal amount of a numbered objective to work towards")]
[NodePath("TerraTech/Actions/Encounters")]
public class uScript_GetQuestObjectiveTargetCount : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public int GetTargetCount([FriendlyName("Owner Node", "Owner Node of Encounter")] GameObject owner, [FriendlyName("Objective ID", "Index of the targeted objective. 1 based")] int objectiveId)
	{
		int result = 0;
		if (m_Encounter == null && owner != null)
		{
			m_Encounter = owner.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			result = m_Encounter.QuestLog.GetObjectiveTargetCount(objectiveId - 1);
		}
		else
		{
			d.LogError("ERROR - uScript_GetQuestObjectiveTargetCount - Failed to get encounter!");
		}
		return result;
	}
}
