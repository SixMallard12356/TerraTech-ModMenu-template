public class uScript_HasTechBeenDamagedByPlayer : uScriptLogic
{
	private bool m_Damaged;

	public bool Damaged => m_Damaged;

	public bool NotDamaged => !m_Damaged;

	public void In(Tank tech)
	{
		m_Damaged = false;
		if (tech != null)
		{
			m_Damaged = tech.DamagedByPlayer;
		}
	}

	public void OnDisable()
	{
		m_Damaged = false;
	}
}
