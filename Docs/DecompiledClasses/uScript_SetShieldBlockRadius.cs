#define UNITY_EDITOR
[NodePath("TerraTech")]
[FriendlyName("uScript_SetShieldBlockRadius", "Set the size of the shield on target visible")]
public class uScript_SetShieldBlockRadius : uScriptLogic
{
	private ModuleShieldGenerator m_ShieldGenerator;

	public bool Out => true;

	public void In(TankBlock shieldBlock, float radius)
	{
		if (shieldBlock != null)
		{
			if (m_ShieldGenerator == null || m_ShieldGenerator.block != shieldBlock)
			{
				m_ShieldGenerator = shieldBlock.GetComponent<ModuleShieldGenerator>();
			}
			if ((bool)m_ShieldGenerator)
			{
				m_ShieldGenerator.m_Radius = radius;
			}
			else
			{
				d.LogError("uScript_SetShieldBlockRadius - TankBlock has no shield generator");
			}
		}
		else
		{
			d.LogError("uScript_SetShieldBlockRadius - TankBlock is null");
		}
	}
}
