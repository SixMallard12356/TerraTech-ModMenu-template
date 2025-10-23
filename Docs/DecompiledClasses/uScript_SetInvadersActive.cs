public class uScript_SetInvadersActive : uScriptLogic
{
	public bool Out => true;

	public void Enable()
	{
		Singleton.Manager<ManInvasion>.inst.SetActive(active: true);
	}

	public void Disable()
	{
		Singleton.Manager<ManInvasion>.inst.SetActive(active: false);
	}
}
