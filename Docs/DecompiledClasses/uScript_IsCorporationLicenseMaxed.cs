public class uScript_IsCorporationLicenseMaxed : uScriptLogic
{
	private bool m_Maxed;

	public bool True => m_Maxed;

	public bool False => !m_Maxed;

	public void In(FactionSubTypes corporation)
	{
		m_Maxed = false;
		FactionLicense license = Singleton.Manager<ManLicenses>.inst.GetLicense(corporation);
		if (license != null)
		{
			m_Maxed = license.HasReachedMaxLevel;
		}
	}
}
