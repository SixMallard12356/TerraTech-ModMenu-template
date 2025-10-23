#define UNITY_EDITOR
public class uScript_IsShieldBlockPowered : uScriptLogic
{
	private bool m_IsPowered;

	private ModuleShieldGenerator m_ShieldGenerator;

	public bool True => m_IsPowered;

	public bool False => !m_IsPowered;

	public void In(TankBlock shieldBlock)
	{
		m_IsPowered = false;
		if (shieldBlock != null)
		{
			if (m_ShieldGenerator == null || m_ShieldGenerator.block != shieldBlock)
			{
				m_ShieldGenerator = shieldBlock.GetComponent<ModuleShieldGenerator>();
			}
			if ((bool)m_ShieldGenerator)
			{
				m_IsPowered = m_ShieldGenerator.IsPowered;
			}
			else
			{
				d.LogError("uScript_IsShieldBlockPowered - TankBlock has no shield generator");
			}
		}
		else
		{
			d.LogError("uScript_IsShieldBlockPowered - TankBlock is null");
		}
	}
}
