public class uScript_IsResourceReservoir : uScriptLogic
{
	private bool m_IsResourceReservoir;

	public bool True => m_IsResourceReservoir;

	public bool False => !m_IsResourceReservoir;

	public void In(ResourceDispenser resourceDispenser)
	{
		m_IsResourceReservoir = resourceDispenser != null && resourceDispenser.IsResourceReservoir;
	}
}
