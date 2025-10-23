[NodePath("TerraTech/Actions/Camera")]
[FriendlyName("Override Tank Camera Zoom Distance Max", "Override Tank Camera max zoom distance. If tank arg is provided, the override will be set for the controlling player only")]
public class uScript_OverrideTankCameraDistanceMax : uScriptLogic
{
	public bool Out => true;

	public void In(bool enable, float newDistanceMax, Tank tech)
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
			if (enable)
			{
				TankCamera.inst.DistanceMaxOverride(newDistanceMax);
			}
			else
			{
				TankCamera.inst.DistanceMaxOverrideClear();
			}
		}
		else
		{
			TankCameraDistanceOverrideRequest message = new TankCameraDistanceOverrideRequest
			{
				enable = enable,
				distance = newDistanceMax
			};
			Singleton.Manager<ManNetwork>.inst.SendToClient(connectionId, TTMsgType.TankCameraDistanceOverrideRequest, message);
		}
	}
}
