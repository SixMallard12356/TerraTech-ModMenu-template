#define UNITY_EDITOR
public class uScript_IsBlockGeneratingPower : uScriptLogic
{
	private bool m_Generating;

	private ModuleEnergy m_Energy;

	public bool True => m_Generating;

	public bool False => !m_Generating;

	public void In(TankBlock block)
	{
		m_Generating = false;
		if ((bool)block)
		{
			if ((bool)block.tank)
			{
				if (m_Energy == null || m_Energy.block != block)
				{
					m_Energy = block.GetComponent<ModuleEnergy>();
				}
				if ((bool)m_Energy)
				{
					m_Generating = m_Energy.IsGenerating;
				}
				else
				{
					d.LogError("uScript_IsBlockGeneratingPower - Block " + block.name + " does not have a ModuleEnergy");
				}
			}
			else
			{
				d.LogError("uScript_IsBlockGeneratingPower - Block " + block.name + " has no Tech");
			}
		}
		else
		{
			d.LogError("uScript_IsBlockGeneratingPower null block passed in");
		}
	}

	public void OnDisable()
	{
		m_Generating = false;
		m_Energy = null;
	}
}
