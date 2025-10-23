using System;
using UnityEngine;

[Serializable]
public class CoreEncounterCompletedCondition : Condition
{
	[Tooltip("Corporation")]
	[SerializeField]
	private FactionSubTypes m_Corporation;

	[Tooltip("Grade")]
	[SerializeField]
	private int m_Grade;

	[Tooltip("Encounter Name")]
	[SerializeField]
	private string m_EncounterName;

	public override bool Passes()
	{
		return Singleton.Manager<ManProgression>.inst.IsCoreEncounterCompleted(m_Corporation, m_Grade, m_EncounterName);
	}
}
