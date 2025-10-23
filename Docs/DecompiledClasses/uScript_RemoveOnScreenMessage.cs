public class uScript_RemoveOnScreenMessage : uScriptLogic
{
	public bool Out => true;

	public void In(ManOnScreenMessages.OnScreenMessage onScreenMessage, [SocketState(false, false)] bool instant)
	{
		if (onScreenMessage != null)
		{
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				Singleton.Manager<ManOnScreenMessagesNetworked>.inst.RemoveMessage(onScreenMessage, instant);
			}
			else
			{
				Singleton.Manager<ManOnScreenMessages>.inst.RemoveMessage(onScreenMessage, instant);
			}
		}
	}
}
