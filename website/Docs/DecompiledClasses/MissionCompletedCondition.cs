using System;
using UnityEngine;

[Serializable]
public class MissionCompletedCondition : Condition
{
	[Tooltip("Mission Type (GSO etc)")]
	[SerializeField]
	private string m_Type;

	[SerializeField]
	[Tooltip("Mission Group (Tier1 etc)")]
	private string m_Group;

	[SerializeField]
	[Tooltip("Mission Name (Kill The Boss etc)")]
	private string m_Name;

	public override bool Passes()
	{
		return false;
	}
}
