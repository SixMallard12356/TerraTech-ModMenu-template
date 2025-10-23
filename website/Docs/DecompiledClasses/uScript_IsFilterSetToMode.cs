#define UNITY_EDITOR
public class uScript_IsFilterSetToMode : uScriptLogic
{
	private bool m_MatchingModeSet;

	private ModuleItemFilter m_FilterModule;

	public bool True => m_MatchingModeSet;

	public bool False => !m_MatchingModeSet;

	public void In(TankBlock filterBlock, ModuleItemFilter.AcceptMode desiredMode)
	{
		m_MatchingModeSet = false;
		if (filterBlock != null)
		{
			if (!m_FilterModule)
			{
				m_FilterModule = filterBlock.GetComponent<ModuleItemFilter>();
			}
			if (m_FilterModule != null)
			{
				m_MatchingModeSet = m_FilterModule.FilterMode == desiredMode;
			}
			else
			{
				d.LogError("uScript_IsFilterSetToMode: block doesn't have a filter module");
			}
		}
		else
		{
			d.LogError("uScript_IsFilterSetToMode: filter block is null");
		}
	}

	public void OnEnable()
	{
		m_MatchingModeSet = false;
	}
}
