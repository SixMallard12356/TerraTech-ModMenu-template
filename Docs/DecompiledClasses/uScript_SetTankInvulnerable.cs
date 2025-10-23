[FriendlyName("Set tank invulnerable")]
public class uScript_SetTankInvulnerable : uScriptLogic
{
	private bool m_True;

	private Tank.WeakReference m_PreviousTank = new Tank.WeakReference();

	public bool Out => true;

	public void In(bool invulnerable, Tank tank)
	{
		if ((!m_True || tank != m_PreviousTank.Get()) && (bool)tank)
		{
			tank.SetInvulnerable(invulnerable, forever: true);
			m_True = true;
			m_PreviousTank.Set(tank);
		}
	}

	public void OnDisable()
	{
		m_True = false;
		m_PreviousTank.Set(null);
	}
}
