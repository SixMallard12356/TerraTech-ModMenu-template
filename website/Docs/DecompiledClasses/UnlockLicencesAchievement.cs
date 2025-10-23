using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Asset/Achievements/UnlockLicencesAchievement")]
public class UnlockLicencesAchievement : AchievementObject
{
	[SerializeField]
	private List<FactionSubTypes> m_TargetFactions;

	public override void Initialise()
	{
		base.Initialise();
		Singleton.Manager<ManLicenses>.inst.LevelUpEvent.Subscribe(OnLicenseLevelUp);
	}

	private void OnLicenseLevelUp(FactionSubTypes factionType, int licenseLevel)
	{
		if (!IsActive() || licenseLevel != 0 || !m_TargetFactions.Contains(factionType))
		{
			return;
		}
		bool flag = true;
		for (int i = 0; i < m_TargetFactions.Count; i++)
		{
			FactionLicense license = Singleton.Manager<ManLicenses>.inst.GetLicense(m_TargetFactions[i]);
			if (license == null || !license.IsDiscovered)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			CompleteAchievement();
			Singleton.Manager<ManLicenses>.inst.LevelUpEvent.Unsubscribe(OnLicenseLevelUp);
		}
	}
}
