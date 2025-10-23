public class TechSplitNamer
{
	private string m_BaseName;

	private int m_NextID;

	private LocalisedString m_LocName;

	private bool m_NameIndexed;

	public TechSplitNamer(Tank baseTank)
	{
		m_NameIndexed = baseTank.HasNameIndex;
		if (!m_NameIndexed)
		{
			string text = (m_BaseName = baseTank.name);
			m_NextID = 2;
			int num = text.LastIndexOf(" #");
			if (num >= 0 && int.TryParse(text.Substring(num + 2), out var result))
			{
				m_BaseName = text.Substring(0, num);
				m_NextID = result + 1;
			}
		}
		else
		{
			m_LocName = baseTank.GetLocalisedName();
		}
	}

	public string CreateNextName()
	{
		if (!m_NameIndexed)
		{
			string result = $"{m_BaseName} #{m_NextID}";
			m_NextID++;
			return result;
		}
		int nextAndIncrement = Singleton.Manager<ManSaveGame>.inst.CurrentState.m_SpawnedPlayerNetTechCount.GetNextAndIncrement();
		return TechData.CreateNameWithIndex(m_LocName, nextAndIncrement);
	}
}
