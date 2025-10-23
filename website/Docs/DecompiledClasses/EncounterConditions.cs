using System;
using UnityEngine;

[Serializable]
public class EncounterConditions
{
	[SerializeField]
	private LicenseCondition[] m_LicenseConditions;

	[SerializeField]
	private XPPercentageCondition[] m_XpPercentageCondition;

	[SerializeField]
	private MissionCompletedCondition[] m_MissionCompletedCondition;

	[SerializeField]
	private CoreEncounterCompletedCondition[] m_CoreEncounterCompletedCondition;

	[SerializeField]
	private bool m_UnfinishedMission;

	public bool IsUnfinishedMission => m_UnfinishedMission;

	public bool Passes()
	{
		bool result = true;
		for (int i = 0; i < m_LicenseConditions.Length; i++)
		{
			if (!m_LicenseConditions[i].Passes())
			{
				result = false;
				break;
			}
		}
		for (int j = 0; j < m_XpPercentageCondition.Length; j++)
		{
			if (!m_XpPercentageCondition[j].Passes())
			{
				result = false;
				break;
			}
		}
		for (int k = 0; k < m_MissionCompletedCondition.Length; k++)
		{
			if (!m_MissionCompletedCondition[k].Passes())
			{
				result = false;
				break;
			}
		}
		for (int l = 0; l < m_CoreEncounterCompletedCondition.Length; l++)
		{
			if (!m_CoreEncounterCompletedCondition[l].Passes())
			{
				result = false;
				break;
			}
		}
		if (m_UnfinishedMission)
		{
			result = false;
		}
		return result;
	}
}
