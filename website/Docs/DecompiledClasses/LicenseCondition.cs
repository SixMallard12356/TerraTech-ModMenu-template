using System;
using UnityEngine;

[Serializable]
public class LicenseCondition : Condition
{
	[Tooltip("Corporation Tiers Apply too")]
	[SerializeField]
	private FactionSubTypes m_Corp;

	[Tooltip("Min Tier allowed. -1 means no min tier")]
	[SerializeField]
	private int m_MinTier;

	[SerializeField]
	[Tooltip("Max Tier allowed. -1 means no max tier")]
	private int m_MaxTier;

	public override bool Passes()
	{
		bool result = false;
		int num = m_MinTier - 1;
		int num2 = m_MaxTier - 1;
		int currentLevel = Singleton.Manager<ManLicenses>.inst.GetCurrentLevel(m_Corp);
		if (currentLevel >= num && (currentLevel <= num2 || num2 == -1))
		{
			result = true;
		}
		return result;
	}
}
