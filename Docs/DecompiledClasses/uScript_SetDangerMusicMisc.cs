[NodePath("TerraTech/Actions/Audio")]
[FriendlyName("Set Danger Music Misc", "Override danger music with a misc track. If a Tank tech ref is provided, the change will be sent to the controlling client only")]
public class uScript_SetDangerMusicMisc : uScriptLogic
{
	public bool Out => true;

	public void In(ManMusic.MiscDangerMusicType miscDangerMusicType, Tank tech)
	{
		int connectionId = -1;
		bool flag = true;
		if (tech != null)
		{
			if ((bool)tech.netTech && (bool)tech.netTech.NetPlayer)
			{
				if (!tech.netTech.NetPlayer.IsHostPlayer)
				{
					flag = false;
					connectionId = tech.netTech.NetPlayer.connectionToClient.connectionId;
				}
			}
			else if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && tech.WasPlayerControlledAtFatalDamageTime)
			{
				flag = false;
				connectionId = tech.ConnectionIdOnFatalDamage;
			}
		}
		if (flag)
		{
			Singleton.Manager<ManMusic>.inst.SetDangerMusicOverride(miscDangerMusicType);
			return;
		}
		OverrideDangerMusicRequest message = new OverrideDangerMusicRequest
		{
			m_DangerMusicType = miscDangerMusicType
		};
		Singleton.Manager<ManNetwork>.inst.SendToClient(connectionId, TTMsgType.OverrideDangerMusicRequest, message);
	}
}
