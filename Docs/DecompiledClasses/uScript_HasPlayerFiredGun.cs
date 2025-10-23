[FriendlyName("Has player fired the gun")]
public class uScript_HasPlayerFiredGun : uScriptLogic
{
	private bool m_True;

	public bool True => m_True;

	public bool False => !m_True;

	public void In()
	{
		if ((bool)Singleton.playerTank && !m_True)
		{
			ModuleWeapon firstWeapon = Singleton.playerTank.Weapons.GetFirstWeapon();
			m_True = ((bool)firstWeapon && firstWeapon.FireRequested) || Singleton.Manager<ManInput>.inst.GetButton(2);
		}
	}

	public void OnDisable()
	{
		m_True = false;
	}
}
