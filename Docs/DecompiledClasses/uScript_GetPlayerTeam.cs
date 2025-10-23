[FriendlyName("AI/Get player team")]
public class uScript_GetPlayerTeam : uScriptLogic
{
	public bool Out => true;

	public int In()
	{
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.CoOpCampaign)
		{
			return 1073741824;
		}
		return 0;
	}
}
