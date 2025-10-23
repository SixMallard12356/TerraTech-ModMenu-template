public class uScript_ClearOnScreenMessagesWithTag : uScriptLogic
{
	public bool Out => true;

	public void In(string tag, bool clearCurrent)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			Singleton.Manager<ManOnScreenMessagesNetworked>.inst.ClearMessagesWithTag(tag, clearCurrent);
		}
		else
		{
			Singleton.Manager<ManOnScreenMessages>.inst.ClearMessagesWithTag(tag, clearCurrent);
		}
	}
}
