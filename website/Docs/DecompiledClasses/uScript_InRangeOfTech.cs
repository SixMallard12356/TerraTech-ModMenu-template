public class uScript_InRangeOfTech : uScriptLogic
{
	private bool m_InRange;

	public bool Out => true;

	public bool InRange => m_InRange;

	public bool OutOfRange => !m_InRange;

	public void In(Tank tank, float range)
	{
		if (!(tank != null))
		{
			return;
		}
		m_InRange = false;
		foreach (Tank allPlayerTech in Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs())
		{
			if ((tank.trans.position - allPlayerTech.trans.position).magnitude <= range)
			{
				m_InRange = true;
				break;
			}
		}
	}

	public void OnDisable()
	{
		m_InRange = false;
	}
}
