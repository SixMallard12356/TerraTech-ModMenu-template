using UnityEngine.Networking;

public class LANNetworkDiscovery : NetworkDiscovery
{
	public Event<string, string> OnBroadcastReceived;

	public void StartBroadcastingServer(string newBroadcastData)
	{
		if (!base.running && base.hostId == -1)
		{
			Initialize();
		}
		SetBroadcastData(newBroadcastData);
		StartAsServer();
	}

	public void SetBroadcastData(string newBroadcastData)
	{
		base.broadcastData = newBroadcastData;
	}

	public void StopBroadcastingServer()
	{
		StopBroadcast();
	}

	public void StartSearchingForServer()
	{
		if (!base.running && base.hostId == -1)
		{
			Initialize();
		}
		StartAsClient();
	}

	public void StopSearchingForServer()
	{
		StopBroadcast();
	}

	public override void OnReceivedBroadcast(string fromAddress, string data)
	{
		OnBroadcastReceived.Send(fromAddress, data);
	}
}
