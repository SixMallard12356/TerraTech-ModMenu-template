#define UNITY_EDITOR
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace TerraTech.Network;

public class PlatformLobby_LAN : Lobby
{
	private bool m_LobbyDataUpdated;

	private HostTopology m_HostTopology;

	private static byte[] s_ChatBuffer = new byte[1024];

	private PlatformLobbySystem_LAN PlatformLobbySystem => base.LobbySystem as PlatformLobbySystem_LAN;

	private PlatformLobbySystem_LAN.PlatformLobbyData PlatformLobbyData => PlatformLobbySystem.GetPlatformLobby(this);

	public void HostSetPlayerColour(TTNetworkID playerId, Color32 colour)
	{
		base.HostSetPlayerColour(playerId, (Color)colour);
	}

	public PlatformLobby_LAN(LobbySystem system, LobbyData data, ConnectionConfig config)
		: base(system, data)
	{
		m_HostTopology = new HostTopology(config, 16);
	}

	public override string GetLocalPlayerName()
	{
		return SystemInfo.deviceName;
	}

	public override bool IsLobbyOwner()
	{
		return base.LobbySystem.GetLocalPlayerID() == GetLobbyOwner();
	}

	public override void RemoveClientConnectionFromServer(TTNetworkID deadNetworkId)
	{
		d.LogFormat("PlatformLobby_LAN.RemoveClientConnectionFromServer called with deadNetworkId={0}", deadNetworkId);
		PlatformLobbySystem.RemoveConnection(deadNetworkId);
	}

	protected override void SetLobbyVisibility(LobbyVisibility visibility)
	{
	}

	protected override void SendLobbyChatMsg(byte[] memBuffer, int numBytesToWrite)
	{
		if (numBytesToWrite + 20 > s_ChatBuffer.Length)
		{
			s_ChatBuffer = new byte[numBytesToWrite + 20];
		}
		using MemoryStream memoryStream = new MemoryStream(s_ChatBuffer);
		using BinaryWriter binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8);
		binaryWriter.Write("chat:" + base.LobbySystem.GetLocalPlayerID().ToString());
		binaryWriter.Write(memBuffer, 0, numBytesToWrite);
		if (GetLobbyOwner() == PlatformLobbySystem.LocalPlayerNetworkID)
		{
			PlatformLobbySystem.SendToAllClients(s_ChatBuffer, 0, (int)memoryStream.Position, alsoServer: true);
		}
		else
		{
			PlatformLobbyData.m_ClientConnection.Send(s_ChatBuffer, 0, (int)memoryStream.Position);
		}
	}

	public override TTNetworkID GetLobbyOwner()
	{
		if (PlatformLobbyData == null)
		{
			return TTNetworkID.Invalid;
		}
		return PlatformLobbyData.m_HostId;
	}

	protected override int GetLargeFriendAvatarImageID(TTNetworkID playerID)
	{
		int hashCode = playerID.m_NetworkID.GetHashCode();
		if (hashCode != -1)
		{
			return hashCode;
		}
		return 123;
	}

	protected override Sprite GetSpriteFromImageID(int imageID, int imageWidthHeight)
	{
		byte[] array = new byte[4 * imageWidthHeight * imageWidthHeight];
		int num = 0;
		uint[] array2 = new uint[14]
		{
			16744544u, 15777920u, 14717008u, 16728256u, 16760896u, 12628160u, 6320256u, 4206624u, 8454080u, 12648320u,
			8438015u, 12615935u, 4215024u, 4255824u
		};
		int num2 = array2.Length;
		uint[] array3 = new uint[4]
		{
			array2[(uint)imageID % num2],
			array2[(uint)imageID / num2 % num2],
			array2[(uint)imageID / num2 / num2 % num2],
			array2[(uint)imageID / num2 / num2 / num2 % num2]
		};
		for (int i = 0; i < imageWidthHeight; i++)
		{
			for (int j = 0; j < imageWidthHeight; j++)
			{
				uint num3 = array3[j / (imageWidthHeight / 4) % 2 + i / (imageWidthHeight / 4) % 2 * 2];
				array[num++] = (byte)((num3 >> 16) & 0xFF);
				array[num++] = (byte)((num3 >> 8) & 0xFF);
				array[num++] = (byte)(num3 & 0xFF);
				array[num++] = byte.MaxValue;
			}
		}
		Texture2D texture2D = new Texture2D(imageWidthHeight, imageWidthHeight, TextureFormat.RGBA32, mipChain: false);
		texture2D.wrapMode = TextureWrapMode.Clamp;
		texture2D.LoadRawTextureData(array);
		texture2D.Apply();
		return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
	}

	protected override void CleanUpPreviousSession()
	{
	}

	private TTNetworkConnection CreateTTNetworkConnection(TTNetworkID hostNetworkID, int connectionID)
	{
		TTNetworkConnection tTNetworkConnection = new TTNetworkConnection(m_HostTopology, hostNetworkID);
		string networkAddress = hostNetworkID.ToString();
		int networkHostId = -1;
		tTNetworkConnection.NetworkPort = 7777;
		tTNetworkConnection.NetworkAddress = PlatformLobbyData.m_Data["networkAddress"];
		tTNetworkConnection.Initialize(networkAddress, networkHostId, connectionID, m_HostTopology);
		return tTNetworkConnection;
	}

	protected override TTNetworkConnection CreateConnectionToHost(TTNetworkID hostNetworkID)
	{
		return CreateTTNetworkConnection(hostNetworkID, 0);
	}

	protected override void Update()
	{
		if (m_LobbyDataUpdated)
		{
			m_LobbyDataUpdated = false;
			PlatformLobbySystem.SendToAllClients("lobbydata:" + PlatformLobbyData.GetInLobbyStringData(), alsoServer: false);
			HandleLobbyDataUpdated(wasSuccessful: true);
		}
		if (PlatformLobbyData.m_ClientConnection != null)
		{
			PlatformLobbyData.m_ClientConnection.Update();
		}
	}

	protected override void SetLobbyData(string keyName, string value)
	{
		if (PlatformLobbyData != null && (!PlatformLobbyData.m_Data.ContainsKey(keyName) || !(PlatformLobbyData.m_Data[keyName] == value)))
		{
			PlatformLobbyData.m_Data[keyName] = value;
			m_LobbyDataUpdated = true;
		}
	}

	protected override void SendTeamData()
	{
		if (IsLobbyOwner())
		{
			string value = ManSaveGame.SaveObjectToRawJson(base.PlayerTeams);
			SetLobbyData("playerTeams", value);
		}
	}

	protected override void UpdateUsedColoursLobbyData()
	{
		string value = ConvertColoursUsedToString();
		SetLobbyData("usedColours", value);
	}

	public override TTNetworkConnection.NetworkStats GetNetworkStats()
	{
		return default(TTNetworkConnection.NetworkStats);
	}
}
