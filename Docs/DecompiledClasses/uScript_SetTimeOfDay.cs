[FriendlyName("Set Time of Day", "Sets the Time of Day. If a Tank tech is passed, time is only set for the owners client")]
[NodePath("TerraTech/Environment")]
public class uScript_SetTimeOfDay : uScriptLogic
{
	public bool Out => true;

	public void In(int hour, Tank tech)
	{
		if (tech != null && tech.netTech.IsNotNull() && tech.netTech.NetPlayer.IsNotNull())
		{
			if (tech.netTech.NetPlayer.IsHostPlayer)
			{
				Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay(hour, 0, 0, serverOnly: true);
				return;
			}
			TimeOfDayUpdateMessage timeOfDayUpdateMessage = Singleton.Manager<ManTimeOfDay>.inst.GetTimeOfDayUpdateMessage();
			timeOfDayUpdateMessage.m_Hour = hour;
			int connectionId = tech.netTech.NetPlayer.connectionToClient.connectionId;
			Singleton.Manager<ManNetwork>.inst.SendToClient(connectionId, TTMsgType.TimeOfDayUpdate, timeOfDayUpdateMessage);
		}
		else
		{
			Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay(hour, 0, 0);
		}
	}
}
