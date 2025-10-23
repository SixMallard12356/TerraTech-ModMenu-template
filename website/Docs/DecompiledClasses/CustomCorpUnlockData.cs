using System.Collections.Generic;

public class CustomCorpUnlockData
{
	public FactionSubTypes m_VanillaCorp = FactionSubTypes.GSO;

	public List<int> m_CustomCorpIDs = new List<int>();

	public static CustomCorpUnlockData CreateDataOrNull(FactionSubTypes corp)
	{
		CustomCorpUnlockData customCorpUnlockData = new CustomCorpUnlockData
		{
			m_VanillaCorp = corp
		};
		foreach (int customCorpID in Singleton.Manager<ManMods>.inst.GetCustomCorpIDs())
		{
			ModdedCorpDefinition corpDefinition = Singleton.Manager<ManMods>.inst.GetCorpDefinition((FactionSubTypes)customCorpID);
			if (corpDefinition != null && Singleton.Manager<ManMods>.inst.GetCorpIndex(corpDefinition.m_RewardCorp) == corp)
			{
				customCorpUnlockData.m_CustomCorpIDs.Add(customCorpID);
			}
		}
		if (customCorpUnlockData.m_CustomCorpIDs.Count > 0)
		{
			return customCorpUnlockData;
		}
		return null;
	}
}
