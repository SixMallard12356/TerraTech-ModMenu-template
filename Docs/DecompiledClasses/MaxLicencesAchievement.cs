#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Asset/Achievements/MaxLicencesAchievement")]
public class MaxLicencesAchievement : AchievementObject
{
	[SerializeField]
	private List<FactionSubTypes> m_TargetFactions;

	public override void Initialise()
	{
		base.Initialise();
		Singleton.Manager<ManLicenses>.inst.LicenceMaxedEvent.Subscribe(OnLicenseLevelUp);
	}

	private void OnLicenseLevelUp(FactionSubTypes factionType)
	{
		d.Log("[MaxLicencesAchievement] OnLicenseLevelUp: " + factionType);
		if (!IsActive() || !m_TargetFactions.Contains(factionType))
		{
			return;
		}
		bool flag = true;
		for (int i = 0; i < m_TargetFactions.Count; i++)
		{
			FactionLicense license = Singleton.Manager<ManLicenses>.inst.GetLicense(m_TargetFactions[i]);
			if (license == null || !license.HasReachedMaxLevel)
			{
				d.Log("[MaxLicencesAchievement] OnLicenseLevelUp - exiting.. user does not have licence " + i);
				flag = false;
				break;
			}
		}
		if (flag)
		{
			d.Log("[MaxLicencesAchievement] all licences - completing achievement!");
			CompleteAchievement();
			Singleton.Manager<ManLicenses>.inst.LicenceMaxedEvent.Unsubscribe(OnLicenseLevelUp);
		}
	}
}
