public class uScript_SetPlayerTechControlScheme : uScriptLogic
{
	public bool Out => true;

	public void In(ControlSchemeCategory category)
	{
		if (Singleton.playerTank != null)
		{
			ControlScheme controlScheme = Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_ControlSchemeLibrary.GetControlScheme(category);
			if (controlScheme != null)
			{
				Singleton.playerTank.control.SetActiveScheme(controlScheme);
			}
		}
	}
}
