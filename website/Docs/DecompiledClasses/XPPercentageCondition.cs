using System;
using UnityEngine;

[Serializable]
public class XPPercentageCondition : Condition
{
	[SerializeField]
	[Tooltip("Corporation")]
	private FactionSubTypes m_Corp;

	[SerializeField]
	[Tooltip("Grade")]
	private int m_Grade;

	[Tooltip("Grade Percentage")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_PercentageInGrade;

	public override bool Passes()
	{
		int grade = m_Grade - 1;
		return Singleton.Manager<ManLicenses>.inst.HasPercentageXpInGrade(m_Corp, grade, m_PercentageInGrade);
	}
}
