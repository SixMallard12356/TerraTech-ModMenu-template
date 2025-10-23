#define UNITY_EDITOR
public class uScript_IsHeartBlockOnline : uScriptLogic
{
	private bool m_IsOnline;

	private ModuleHeart m_HeartModule;

	public bool True => m_IsOnline;

	public bool False => !m_IsOnline;

	public void In(TankBlock heartBlock)
	{
		m_IsOnline = false;
		if (heartBlock != null)
		{
			if (m_HeartModule == null || m_HeartModule.block != heartBlock)
			{
				m_HeartModule = heartBlock.GetComponent<ModuleHeart>();
			}
			if ((bool)m_HeartModule)
			{
				m_IsOnline = m_HeartModule.IsOnline;
			}
			else
			{
				d.LogError("uScript_IsHeartBlockOnline - TankBlock has no Heart Module");
			}
		}
		else
		{
			d.LogError("uScript_IsHeartBlockOnline - TankBlock is null");
		}
	}
}
