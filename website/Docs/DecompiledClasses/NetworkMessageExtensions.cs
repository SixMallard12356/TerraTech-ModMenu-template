using UnityEngine.Networking;

public static class NetworkMessageExtensions
{
	public static NetPlayer GetSender(this NetworkMessage netMsg)
	{
		return Singleton.Manager<ManNetwork>.inst.FindPlayerByConnection(netMsg.conn);
	}
}
