using System.ComponentModel;

namespace UnityEngine.Networking;

[EditorBrowsable(EditorBrowsableState.Never)]
[RequireComponent(typeof(NetworkManager))]
public class MyNetworkManagerHUD : MonoBehaviour
{
	public NetworkManager manager;

	[SerializeField]
	public bool showGUI = true;

	[SerializeField]
	public int offsetX;

	[SerializeField]
	public int offsetY;

	private bool m_ShowServer;

	private void Awake()
	{
		manager = GetComponent<NetworkManager>();
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
		if (!showGUI)
		{
			return;
		}
		if (!manager.IsClientConnected() && !NetworkServer.active && manager.matchMaker == null)
		{
			if (Application.platform != RuntimePlatform.WebGLPlayer)
			{
				if (Input.GetKeyDown(KeyCode.S))
				{
					manager.StartServer();
				}
				if (Input.GetKeyDown(KeyCode.H))
				{
					manager.StartHost();
				}
			}
			if (Input.GetKeyDown(KeyCode.C))
			{
				manager.StartClient();
			}
		}
		if (NetworkServer.active && manager.IsClientConnected() && Input.GetKeyDown(KeyCode.X))
		{
			manager.StopHost();
		}
	}
}
