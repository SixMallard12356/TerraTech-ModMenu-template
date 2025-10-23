#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace TerraTech.Network;

public abstract class Lobby
{
	public enum MemberLobbyStateMask
	{
		MLS_Entered = 1,
		MLS_Left = 2,
		MLS_Disconnected = 4,
		MLS_Kicked = 8,
		MLS_Banned = 0x10
	}

	public enum ChatMessageType
	{
		ChatMessage,
		StartGame,
		PlayerKicked,
		RequestSetColour,
		JIP,
		SendUserID,
		UpdateLobbyData
	}

	public enum LobbyVisibility
	{
		Private,
		FriendsOnly,
		Public
	}

	public Event<TTNetworkID, Sprite> OnPlayerSpriteLoaded;

	private const float DELAYED_JIP_TIMEOUT = 25f;

	private LobbyData m_Data;

	private LobbySystem m_System;

	private bool m_WasLobbyOwner;

	private float m_DelayedJIPCountdown;

	protected TTNetworkID m_DelayedJIPRemoteHostID = TTNetworkID.Invalid;

	private List<TTNetworkID> m_LateJIPPlayerIDs = new List<TTNetworkID>(4);

	private TTNetworkID m_RemoteHostExpectedToConnectTo = TTNetworkID.Invalid;

	private bool m_LastLobbyIsInProgress;

	private Dictionary<TTNetworkID, Sprite> m_SpriteLookup = new Dictionary<TTNetworkID, Sprite>();

	private byte[] m_MemBuffer;

	private MemoryStream m_MemStream;

	private BinaryWriter m_MemWriter;

	private List<LobbyPlayerData> m_Players = new List<LobbyPlayerData>(16);

	private Dictionary<TTNetworkID, Color32> m_PlayerColours = new Dictionary<TTNetworkID, Color32>(16);

	private List<Color32> m_AvailableColoursCache = new List<Color32>(16);

	private Dictionary<TTNetworkID, int> m_PlayerTeams = new Dictionary<TTNetworkID, int>(16);

	private Dictionary<TTNetworkID, LobbyPlayerData> m_ClientConfigs = new Dictionary<TTNetworkID, LobbyPlayerData>(16);

	public const int kImageIDInvalid = -1;

	public LobbyData Data => m_Data;

	public TTNetworkID ID => Data.m_IDLobby;

	public string Name => Data.m_LobbyName;

	public TTNetworkID RemoteHostExpectedToConnectTo => m_RemoteHostExpectedToConnectTo;

	public Dictionary<TTNetworkID, int> PlayerTeams => m_PlayerTeams;

	public Dictionary<TTNetworkID, Color32> PlayerColours => m_PlayerColours;

	public LobbySystem LobbySystem => m_System;

	public bool HasNoPendingClientConfigs()
	{
		return m_ClientConfigs.Count == 0;
	}

	public abstract string GetLocalPlayerName();

	public abstract bool IsLobbyOwner();

	public abstract void RemoveClientConnectionFromServer(TTNetworkID deadNetworkId);

	public abstract TTNetworkID GetLobbyOwner();

	protected abstract void SetLobbyVisibility(LobbyVisibility visibility);

	protected abstract void SendLobbyChatMsg(byte[] memBuffer, int numBytesToWrite);

	protected abstract int GetLargeFriendAvatarImageID(TTNetworkID playerID);

	protected abstract Sprite GetSpriteFromImageID(int imageID, int imageWidthHeight);

	protected abstract void CleanUpPreviousSession();

	protected abstract TTNetworkConnection CreateConnectionToHost(TTNetworkID hostID);

	protected abstract void Update();

	protected abstract void SendTeamData();

	protected abstract void UpdateUsedColoursLobbyData();

	protected abstract void SetLobbyData(string keyName, string value);

	protected virtual void OnPlayerJoined(TTNetworkID playerID)
	{
		LobbySystem.RecentPlayers.Add(playerID);
	}

	public abstract TTNetworkConnection.NetworkStats GetNetworkStats();

	public Lobby(LobbySystem system, LobbyData data, int memBufferSize = 65536)
	{
		m_System = system;
		m_Data = data;
		Singleton.Manager<ManNetwork>.inst.OnPlayerChangedTeam.Subscribe(OnPlayerTeamsUpdated);
		m_MemBuffer = new byte[memBufferSize];
		m_MemStream = new MemoryStream(m_MemBuffer);
		m_MemWriter = new BinaryWriter(m_MemStream);
	}

	public virtual void Shutdown()
	{
		Singleton.Manager<ManNetwork>.inst.OnPlayerChangedTeam.Unsubscribe(OnPlayerTeamsUpdated);
	}

	public TTNetworkID LocalPlayerNetworkID()
	{
		return LobbySystem.GetLocalPlayerID();
	}

	public bool HasClientConfig(TTNetworkID playerID)
	{
		return m_ClientConfigs.ContainsKey(playerID);
	}

	public bool GetClientConfig(TTNetworkID playerID, out LobbyPlayerData playerConfig)
	{
		return m_ClientConfigs.TryGetValue(playerID, out playerConfig);
	}

	public void RemoveClientConfig(TTNetworkID playerID)
	{
		m_ClientConfigs.Remove(playerID);
		if (m_ClientConfigs.Count == 0)
		{
			LobbySystem.SendEventAllClientsConnected();
		}
	}

	public void SetModList(string modsString)
	{
		SetLobbyData("workshopIds", modsString);
	}

	public void SetVisibility(LobbyVisibility visibility)
	{
		Data.m_Visibility = visibility;
		SetLobbyVisibility(visibility);
		SetLobbyData("lobbyPublic", visibility.ToString());
	}

	public void RefreshPlayerConfigs()
	{
		TTNetworkID tTNetworkID = LocalPlayerNetworkID();
		for (int i = 0; i < m_Players.Count; i++)
		{
			LobbyPlayerData value = m_Players[i];
			if (value.m_PlayerID != tTNetworkID)
			{
				if (HasClientConfig(value.m_PlayerID))
				{
					m_ClientConfigs.Remove(value.m_PlayerID);
				}
				m_ClientConfigs.Add(value.m_PlayerID, value);
			}
		}
	}

	public void TriggerGameStart()
	{
		bool num = LobbySystem.QueryGameState.AlreadyRunning();
		d.AssertFormat(!num, "[Lobby] TriggerGameStart called when Multiplayer session already configured or running (will be ignored)");
		if (num)
		{
			return;
		}
		m_ClientConfigs.Clear();
		CleanUpPreviousSession();
		TTNetworkID tTNetworkID = LocalPlayerNetworkID();
		for (int i = 0; i < m_Players.Count; i++)
		{
			LobbyPlayerData lobbyPlayerData = m_Players[i];
			if (lobbyPlayerData.m_PlayerID == tTNetworkID)
			{
				LobbySystem.SendPlayerDataToGameEvent.Send(lobbyPlayerData);
			}
			else
			{
				m_ClientConfigs.Add(lobbyPlayerData.m_PlayerID, lobbyPlayerData);
			}
			RemoveFromLateJIPPlayers(lobbyPlayerData.m_PlayerID);
		}
		StorePlayerSprites();
		if (SKU.SupportsMods)
		{
			SetModList(Singleton.Manager<ManMods>.inst.GetModsInNetworkedSession());
		}
		LobbySystem.SendEventTriggerPreGameStart(this);
		SendStartGameMessage();
		LobbySystem.SendEventTriggerGameStart(this);
	}

	public byte[] GetStartGameMessage(out int bufferSize)
	{
		m_MemWriter.Seek(0, SeekOrigin.Begin);
		m_MemWriter.Write((byte)1);
		WriteStartGameSettings(m_MemWriter);
		bufferSize = (int)m_MemWriter.BaseStream.Position;
		return m_MemBuffer;
	}

	protected virtual void SendStartGameMessage()
	{
		int bufferSize;
		byte[] startGameMessage = GetStartGameMessage(out bufferSize);
		SendLobbyChatMsg(startGameMessage, bufferSize);
	}

	public virtual void KickPlayer(TTNetworkID playerID)
	{
		m_MemWriter.Seek(0, SeekOrigin.Begin);
		m_MemWriter.Write((byte)2);
		playerID.WriteTo(m_MemWriter);
		SendLobbyChatMsg(m_MemBuffer, (int)m_MemWriter.BaseStream.Position);
	}

	public void ConfirmBanPlayer(TTNetworkID playerID)
	{
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		string userName = LobbySystem.GetUserName(playerID);
		string notification = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.MenuMultiplayer.BanConfirmPrompt), userName);
		UIButtonData accept = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.MenuMultiplayer.BanAccept),
			m_Callback = delegate
			{
				BanPlayer(playerID);
				Singleton.Manager<ManUI>.inst.PopScreen();
			},
			m_RewiredAction = 21
		};
		UIButtonData decline = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.NewMenuMain.menuCancel),
			m_Callback = delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			},
			m_RewiredAction = 22
		};
		uIScreenNotifications.Set(notification, accept, decline);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
	}

	public virtual void BanPlayer(TTNetworkID playerID)
	{
		PersistentPlayerID persistentPlayerID = LobbySystem.GetPersistentPlayerID(playerID);
		KickPlayer(playerID);
		LobbySystem.BanList.SetPlayerBanned(persistentPlayerID, banned: true);
		SetLobbyData("bannedPlayers", LobbySystem.BanList.BannedPlayers.ToString());
	}

	public virtual void SendChat(string text, int teamChannel, uint netId)
	{
		m_MemWriter.Seek(0, SeekOrigin.Begin);
		m_MemWriter.Write((byte)0);
		m_MemWriter.Write(teamChannel);
		m_MemWriter.Write(netId);
		m_MemWriter.Write(text);
		SendLobbyChatMsg(m_MemBuffer, (int)m_MemWriter.BaseStream.Position);
	}

	public static string ChoicesToString(int[] choices)
	{
		d.Assert(choices.Length == 34);
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < choices.Length; i++)
		{
			if (i > 0)
			{
				stringBuilder.Append(",");
			}
			stringBuilder.Append(choices[i]);
		}
		return stringBuilder.ToString();
	}

	public void SetLobbyChoice(LobbyData.EnumLobbyChoiceIndex choiceIndex, int value)
	{
		m_Data.m_Choices[(int)choiceIndex] = value;
		SetLobbyChoices(m_Data.m_Choices);
	}

	public void SetLobbyChoices(int[] choices)
	{
		d.Assert(choices.Length == 34, "[Lobby] ASSERT - Number of choices is wrong! Length=" + choices.Length);
		d.Assert(IsLobbyOwner(), "[Lobby] ASSERT - We're not the lobby owner!");
		string value = ChoicesToString(choices);
		SetLobbyData("choices", value);
	}

	public List<LobbyPlayerData> GetPlayerList()
	{
		return m_Players;
	}

	private void SetPlayerTeam(TTNetworkID playerID, int team)
	{
		if (!m_PlayerTeams.TryGetValue(playerID, out var value) || team != value)
		{
			m_PlayerTeams[playerID] = team;
			m_PlayerColours.Remove(playerID);
		}
		SendTeamData();
	}

	public void ReassignPlayerTeams(int teamCount)
	{
		if (IsLobbyOwner())
		{
			d.Assert(teamCount == 0 || teamCount == 2, "ReassignPlayerTeams - Not expecting a team count other than 0 or 2! Got " + teamCount);
			m_PlayerTeams.Clear();
			SendTeamData();
		}
	}

	private bool GetPlayerData(TTNetworkID playerID, out LobbyPlayerData playerData)
	{
		bool result = false;
		playerData = default(LobbyPlayerData);
		for (int i = 0; i < m_Players.Count; i++)
		{
			if (m_Players[i].m_PlayerID == playerID)
			{
				playerData = m_Players[i];
				result = true;
				break;
			}
		}
		return result;
	}

	protected void StorePlayerSprites()
	{
		for (int i = 0; i < m_Players.Count; i++)
		{
			LobbyPlayerData lobbyPlayerData = m_Players[i];
			if (lobbyPlayerData.m_Sprite != null)
			{
				StorePlayerSprite(lobbyPlayerData.m_PlayerID, lobbyPlayerData.m_Sprite);
			}
		}
	}

	public virtual void CheckForStartGame()
	{
	}

	public virtual void CheckForBeingKicked()
	{
	}

	public void HandleChatMessage(BinaryReader reader, int numBytes, TTNetworkID sender)
	{
		ChatMessageType chatMessageType = (ChatMessageType)reader.ReadByte();
		TTNetworkID lobbyOwner = GetLobbyOwner();
		TTNetworkID tTNetworkID = LocalPlayerNetworkID();
		switch (chatMessageType)
		{
		case ChatMessageType.ChatMessage:
		{
			int teamChannel = reader.ReadInt32();
			uint netId = reader.ReadUInt32();
			string message = reader.ReadString();
			DispatchTextChatMessage(message, sender, netId, teamChannel);
			break;
		}
		case ChatMessageType.StartGame:
			ReadStartGameSettings(reader);
			if (tTNetworkID != lobbyOwner)
			{
				ClientStartGame();
			}
			break;
		case ChatMessageType.JIP:
		{
			TTNetworkID tTNetworkID2 = TTNetworkID.ReadFrom(reader);
			ReadStartGameSettings(reader);
			LobbyPlayerData playerData;
			if (tTNetworkID == tTNetworkID2)
			{
				ClientStartGame();
			}
			else if (GetPlayerData(tTNetworkID2, out playerData) && playerData.m_Sprite != null)
			{
				StorePlayerSprite(tTNetworkID2, playerData.m_Sprite);
			}
			break;
		}
		case ChatMessageType.PlayerKicked:
		{
			TTNetworkID tTNetworkID3 = TTNetworkID.ReadFrom(reader);
			string userName = LobbySystem.GetUserName(tTNetworkID3);
			bool flag = sender == lobbyOwner;
			bool flag2 = tTNetworkID == tTNetworkID3;
			if (flag2 && flag)
			{
				LobbySystem.LeaveLobby();
			}
			LobbySystem.SendEventLobbyKicked(tTNetworkID3, userName, flag2);
			break;
		}
		case ChatMessageType.RequestSetColour:
		{
			if (lobbyOwner == tTNetworkID && ColourConverter.TryParseColourString(reader.ReadString(), out var col))
			{
				HostSetPlayerColour(sender, col);
			}
			break;
		}
		case ChatMessageType.SendUserID:
		{
			ulong id = reader.ReadUInt64();
			TTNetworkID sender2 = new TTNetworkID(reader.ReadUInt64());
			HandleUserIDMessage(id, sender2);
			break;
		}
		case ChatMessageType.UpdateLobbyData:
		{
			int num = reader.ReadPackedInt32();
			byte[] data = reader.ReadBytes(num);
			HandleLobbyDataMessage(data, num);
			break;
		}
		}
		_ = numBytes;
		_ = reader.BaseStream.Position;
	}

	protected virtual void HandleUserIDMessage(ulong id, TTNetworkID sender)
	{
		d.LogError("[Lobby] Received UserID message on platform that does not support them");
	}

	protected virtual void HandleLobbyDataMessage(byte[] data, int len)
	{
		d.LogError("[Lobby] Received LobbyDataMessage message on platform that does not support them");
	}

	protected void SendUserIDMessage(ulong id, TTNetworkID user)
	{
		d.Log($"[Lobby] Sending UserID [{user}:{id}]");
		m_MemWriter.Seek(0, SeekOrigin.Begin);
		m_MemWriter.Write((byte)5);
		m_MemWriter.Write(id);
		m_MemWriter.Write(user.m_NetworkID);
		SendLobbyChatMsg(m_MemBuffer, (int)m_MemWriter.BaseStream.Position);
	}

	public void DispatchTextChatMessage(string message, TTNetworkID sender, uint netId, int teamChannel)
	{
		if (!GetPlayerData(sender, out var playerData))
		{
			playerData.m_PlayerID = TTNetworkID.Invalid;
		}
		if (message.Contains("<") || message.Contains(">") || message.Contains("\\"))
		{
			message = message.Replace("<", "<\u200b");
			message = message.Replace(">", ">\u200b");
			message = message.Replace("\\", "\\\u200b");
		}
		LobbySystem.ChatMessageEvent.Send(playerData, netId, teamChannel, message);
	}

	protected void ClientLoadLobbyChoices(string lobbyChoices, ManNetwork.MapSizeOption mapSize, string worldSeed, string biomeChoice, WorldPosition tetherPoint, List<ManWorld.SavedSetPiece> setPiecePlacements, WorldGenVersionData worldGen)
	{
		int[] array = LobbySystem.ParseLobbyChoicesFromString(lobbyChoices);
		if (array != null && array.Length == 34)
		{
			m_Data.m_Choices = array;
		}
		ClientLoadLobbyChoices(mapSize, worldSeed, biomeChoice, tetherPoint, setPiecePlacements, worldGen);
	}

	protected void ClientLoadLobbyChoices(ManNetwork.MapSizeOption mapSize, string worldSeed, string biomeChoice, WorldPosition tetherPoint, List<ManWorld.SavedSetPiece> setPiecePlacements, WorldGenVersionData worldGen)
	{
		if (!LobbySystem.IsServer)
		{
			Singleton.Manager<ManNetwork>.inst.WorldSeed = worldSeed;
			Singleton.Manager<ManNetwork>.inst.BiomeChoice = biomeChoice;
			Singleton.Manager<ManNetwork>.inst.WorldGenVersionID = worldGen.m_VersionID;
			Singleton.Manager<ManNetwork>.inst.WorldGenVersionType = (int)worldGen.m_VersioningType;
			Singleton.Manager<ManNetwork>.inst.SetMapCenter(tetherPoint);
			Singleton.Manager<ManNetwork>.inst.SetMapSize(mapSize);
			Singleton.Manager<ManNetwork>.inst.SetChosenLevelId(Data.LevelDataChoice);
			Singleton.Manager<ManNetwork>.inst.SetPiecePlacements = setPiecePlacements;
			LobbySystem.SendEventLobbyDataToGame(this);
		}
	}

	public bool IsUserInLobby(TTNetworkID userID)
	{
		bool result = false;
		int numLobbyMembers = LobbySystem.GetNumLobbyMembers(ID);
		for (int i = 0; i < numLobbyMembers; i++)
		{
			TTNetworkID lobbyMemberByIndex = LobbySystem.GetLobbyMemberByIndex(ID, i);
			if (userID == lobbyMemberByIndex)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	private LobbyPlayerData SetupPlayerDataAndCheckTeam(TTNetworkID playerId)
	{
		LobbyPlayerData result = new LobbyPlayerData
		{
			m_PlayerID = playerId,
			m_Name = LobbySystem.GetUserName(playerId),
			m_Sprite = LookupPlayerSprite(playerId)
		};
		if (result.m_Sprite == null)
		{
			int largeFriendAvatarImageID = GetLargeFriendAvatarImageID(playerId);
			if (largeFriendAvatarImageID != -1)
			{
				int imageWidthHeight = 184;
				result.m_Sprite = GetSpriteFromImageID(largeFriendAvatarImageID, imageWidthHeight);
				if (result.m_Sprite != null)
				{
					StorePlayerSprite(playerId, result.m_Sprite);
				}
			}
		}
		if (!m_PlayerTeams.TryGetValue(playerId, out result.m_TeamID))
		{
			result.m_TeamID = -1;
			if (Data.m_Choices != null)
			{
				switch (Data.GameTypeChoice)
				{
				case MultiplayerModeType.CoOpCreative:
				case MultiplayerModeType.CoOpCampaign:
				{
					int num = 0;
					SetPlayerTeam(playerId, num);
					result.m_TeamID = num;
					result.m_Colour = LobbySystem.CoOpColours[num];
					break;
				}
				case MultiplayerModeType.Deathmatch:
					if (Data.TeamMatchChoice == 1)
					{
						int recommendedTeamForNewPlayer = GetRecommendedTeamForNewPlayer();
						if (recommendedTeamForNewPlayer > -1)
						{
							SetPlayerTeam(playerId, recommendedTeamForNewPlayer);
							result.m_TeamID = recommendedTeamForNewPlayer;
							result.m_Colour = LobbySystem.DeathmatchColours[recommendedTeamForNewPlayer];
						}
					}
					break;
				default:
					d.LogError("Unsupported game type in lobby");
					break;
				}
			}
		}
		else
		{
			d.Assert(result.m_TeamID != -1, "Unexpected value in m_PlayerTeams dictionary. Was not expecting -1 as cached team ID");
		}
		if (result.m_TeamID == -1)
		{
			if (!m_PlayerColours.TryGetValue(playerId, out result.m_Colour))
			{
				result.m_Colour = AssignPlayerDefaultColour(playerId);
				if (playerId == LocalPlayerNetworkID())
				{
					RequestSetColour(result.m_Colour);
				}
			}
			if (playerId == LocalPlayerNetworkID())
			{
				GetTemporaryColourOverride(ref result.m_Colour);
			}
		}
		else
		{
			switch (Data.GameTypeChoice)
			{
			case MultiplayerModeType.CoOpCreative:
			case MultiplayerModeType.CoOpCampaign:
				result.m_Colour = LobbySystem.CoOpColours[result.m_TeamID];
				break;
			case MultiplayerModeType.Deathmatch:
				result.m_Colour = LobbySystem.DeathmatchColours[result.m_TeamID];
				break;
			default:
				d.LogError("Unsupported gametype in lobby");
				break;
			}
		}
		return result;
	}

	public virtual void HandleBecomingOwner()
	{
		string localPlayerName = GetLocalPlayerName();
		SetLobbyData("name", localPlayerName);
		SetLobbyData("protocolVersion", LobbySystem.PROTOCOL_VERSION.ToString());
		SetLobbyData("language", ((int)Singleton.Manager<Localisation>.inst.CurrentLanguage).ToString());
		MultiplayerModeType multiplayerModeType = (MultiplayerModeType)Data.m_Choices[0];
		SetLobbyData("gameModeIndex", multiplayerModeType.ToString());
		SetLobbyData("ownerID", LocalPlayerNetworkID().ToString());
		SetLobbyData("bannedPlayers", LobbySystem.BanList.BannedPlayers.ToString());
		SetLobbyData("hasMods", (SKU.SupportsMods && Singleton.Manager<ManMods>.inst.GetNumModsInCurrentSession() > 0) ? "true" : "false");
		UpdateLobbyGameStatus(forceUpdate: true);
		try
		{
			ParseUsedColoursFromLobbyData();
		}
		catch (Exception)
		{
		}
		List<TTNetworkID> list = new List<TTNetworkID>(16);
		foreach (KeyValuePair<TTNetworkID, Color32> playerColour in m_PlayerColours)
		{
			TTNetworkID key = playerColour.Key;
			if (!IsUserInLobby(key))
			{
				list.Add(key);
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			m_PlayerColours.Remove(list[i]);
		}
		list = null;
		int numLobbyMembers = LobbySystem.GetNumLobbyMembers(ID);
		for (int j = 0; j < numLobbyMembers; j++)
		{
			TTNetworkID lobbyMemberByIndex = LobbySystem.GetLobbyMemberByIndex(ID, j);
			if (lobbyMemberByIndex.IsValid() && !m_PlayerColours.ContainsKey(lobbyMemberByIndex))
			{
				AssignPlayerDefaultColour(lobbyMemberByIndex);
			}
		}
		UpdateUsedColoursLobbyData();
		m_WasLobbyOwner = true;
		try
		{
			LobbySystem.BecameOwnerEvent.Send(this);
		}
		catch (Exception)
		{
		}
	}

	private void UpdateLobbyGameStatus(bool forceUpdate)
	{
		bool flag = LobbySystem.QueryGameState.IsInProgress();
		if (forceUpdate || flag != m_LastLobbyIsInProgress)
		{
			SetLobbyData("gameInProgress", flag ? "yes" : "no");
			m_LastLobbyIsInProgress = flag;
		}
	}

	public virtual void RequestSetColour(Color32 colour)
	{
		if (IsLobbyOwner())
		{
			TTNetworkID playerID = LocalPlayerNetworkID();
			HostSetPlayerColour(playerID, colour);
			return;
		}
		m_MemWriter.Seek(0, SeekOrigin.Begin);
		m_MemWriter.Write((byte)3);
		m_MemWriter.Write(ColourConverter.ColourToString(colour));
		SendLobbyChatMsg(m_MemBuffer, (int)m_MemWriter.BaseStream.Position);
	}

	protected virtual bool GetTemporaryColourOverride(ref Color32 theColour)
	{
		return false;
	}

	private bool IsColourUsed(Color32 colour)
	{
		bool result = false;
		foreach (KeyValuePair<TTNetworkID, Color32> playerColour in m_PlayerColours)
		{
			Color32 value = playerColour.Value;
			if (value.a == colour.a && value.r == colour.r && value.g == colour.g && value.b == colour.b)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	private bool FindFirstUnusedColour(out Color32 colour)
	{
		List<Color32> availableColoursList = GetAvailableColoursList();
		if (availableColoursList.Count > 0)
		{
			colour = availableColoursList[0];
			return true;
		}
		colour = Color.black;
		return false;
	}

	public Color32 AssignPlayerDefaultColour(TTNetworkID playerId)
	{
		if (!m_PlayerColours.TryGetValue(playerId, out var value))
		{
			if (!FindFirstUnusedColour(out value))
			{
				return Color.black;
			}
			m_PlayerColours.Add(playerId, value);
		}
		return value;
	}

	public void ParseUsedColoursFromLobbyData()
	{
		m_PlayerColours.Clear();
		string lobbyData = LobbySystem.GetLobbyData(ID, "usedColours");
		if (lobbyData.NullOrEmpty())
		{
			return;
		}
		string[] array = lobbyData.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split('=');
			if (array2.Length == 2)
			{
				TTNetworkID tTNetworkID = new TTNetworkID(array2[0]);
				if (tTNetworkID != TTNetworkID.Invalid && ColourConverter.TryParseColourString(array2[1], out var col) && !m_PlayerColours.ContainsKey(tTNetworkID))
				{
					m_PlayerColours.Add(tTNetworkID, col);
				}
			}
		}
	}

	public List<Color32> GetAvailableColoursList()
	{
		m_AvailableColoursCache.Clear();
		Color32[] deathmatchColours = LobbySystem.DeathmatchColours;
		foreach (Color32 color in deathmatchColours)
		{
			if (!IsColourUsed(color))
			{
				m_AvailableColoursCache.Add(color);
			}
		}
		return m_AvailableColoursCache;
	}

	protected string ConvertColoursUsedToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		bool flag = true;
		foreach (KeyValuePair<TTNetworkID, Color32> playerColour in m_PlayerColours)
		{
			stringBuilder.AppendFormat("{0}{1}={2}", flag ? "" : ",", playerColour.Key.ToString(), ColourConverter.ColourToString(playerColour.Value));
			flag = false;
		}
		return stringBuilder.ToString();
	}

	protected void HostSetPlayerColour(TTNetworkID playerID, Color colour)
	{
		d.Assert(IsLobbyOwner(), "[Lobby] ASSERT - We're not the Lobby Owner!");
		if (!IsColourUsed(colour))
		{
			m_PlayerColours[playerID] = colour;
			for (int i = 0; i < m_Players.Count; i++)
			{
				LobbyPlayerData value = m_Players[i];
				if (value.m_PlayerID == playerID)
				{
					value.m_Colour = colour;
					m_Players[i] = value;
				}
			}
		}
		UpdateUsedColoursLobbyData();
		RefreshPlayerConfigs();
	}

	public virtual void HandleLobbyDataUpdated(bool wasSuccessful)
	{
		if (wasSuccessful && IsUserInLobby(LocalPlayerNetworkID()))
		{
			if (!IsLobbyOwner())
			{
				ParseUsedColoursFromLobbyData();
			}
			m_Data = LobbySystem.SetupLobbyData(ID);
			SetupLobbyFromData();
			LobbySystem.CurrentLobbyUpdatedEvent.PrintSubscribers("CurrentLobbyUpdatedEvent");
			LobbySystem.CurrentLobbyUpdatedEvent.Send(this);
		}
	}

	public void HandleLobbyStateUpdated(TTNetworkID changedPlayerID, MemberLobbyStateMask msk)
	{
		bool flag = IsLobbyOwner();
		if (!m_WasLobbyOwner && flag)
		{
			HandleBecomingOwner();
		}
		m_WasLobbyOwner = flag;
		if (flag)
		{
			if ((msk & MemberLobbyStateMask.MLS_Entered) != 0)
			{
				if (!m_PlayerColours.ContainsKey(changedPlayerID))
				{
					AssignPlayerDefaultColour(changedPlayerID);
				}
				UpdateUsedColoursLobbyData();
			}
			if ((msk & (MemberLobbyStateMask)30) != 0)
			{
				if (m_PlayerColours.Remove(changedPlayerID))
				{
					UpdateUsedColoursLobbyData();
				}
				RemoveFromLateJIPPlayers(changedPlayerID);
			}
		}
		if ((msk & MemberLobbyStateMask.MLS_Entered) != 0)
		{
			OnPlayerJoined(changedPlayerID);
		}
		m_Data = LobbySystem.SetupLobbyData(Data.ID);
		SetupLobbyFromData();
		LobbySystem.CurrentLobbyUpdatedEvent.Send(this);
		if (!flag)
		{
			return;
		}
		if ((msk & MemberLobbyStateMask.MLS_Entered) != 0)
		{
			bool flag2 = false;
			if (LobbySystem.QueryGameState.IsNetControllerNull())
			{
				flag2 = true;
			}
			else if (HandleJIPPlayerJoined(changedPlayerID))
			{
				RemoveFromLateJIPPlayers(changedPlayerID);
			}
			else
			{
				flag2 = true;
			}
			if (flag2 && !HasLateJIPPlayer(changedPlayerID))
			{
				m_LateJIPPlayerIDs.Add(changedPlayerID);
			}
		}
		if ((msk & MemberLobbyStateMask.MLS_Entered) != 0)
		{
			PersistentPlayerID persistentPlayerID = LobbySystem.GetPersistentPlayerID(changedPlayerID);
			if (LobbySystem.BanList.IsPlayerBanned(persistentPlayerID))
			{
				KickPlayer(changedPlayerID);
			}
		}
	}

	public void HandleAvatarImageLoaded(TTNetworkID playerID, int imageID, int width, int height)
	{
		if (!IsUserInLobby(LocalPlayerNetworkID()) || !IsUserInLobby(playerID))
		{
			return;
		}
		for (int i = 0; i < m_Players.Count; i++)
		{
			if (m_Players[i].m_PlayerID == playerID)
			{
				LobbyPlayerData value = m_Players[i];
				value.m_Sprite = GetSpriteFromImageID(imageID, width);
				if (value.m_Sprite != null)
				{
					StorePlayerSprite(playerID, value.m_Sprite);
					OnPlayerSpriteLoaded.Send(playerID, value.m_Sprite);
				}
				m_Players[i] = value;
			}
		}
		if (Data.m_LobbyName != null)
		{
			LobbySystem.CurrentLobbyUpdatedEvent.Send(this);
		}
	}

	public Sprite LookupPlayerSprite(TTNetworkID playerId)
	{
		Sprite value = null;
		if (m_SpriteLookup.TryGetValue(playerId, out value))
		{
			d.AssertFormat(value != null, "[Lobby] LookupPlayerSprite has returned null for playerId {0}", playerId);
		}
		return value;
	}

	private void StorePlayerSprite(TTNetworkID playerId, Sprite sprite)
	{
		d.Assert(sprite != null);
		d.Assert(playerId.IsValid());
		m_SpriteLookup[playerId] = sprite;
	}

	private int GetRecommendedTeamForNewPlayer()
	{
		int[] array = new int[2];
		foreach (LobbyPlayerData player in m_Players)
		{
			if (m_PlayerTeams.TryGetValue(player.m_PlayerID, out var value) && value >= 0 && value < array.Length)
			{
				array[value]++;
			}
		}
		if (array[1] >= array[0])
		{
			return 0;
		}
		return 1;
	}

	protected void ClientStartGame()
	{
		bool num = LobbySystem.QueryGameState.AlreadyRunning();
		d.AssertFormat(!num, "[Lobby] HandleChatMessage: StartGame called when Multiplayer session already configured or running (will be ignored)");
		if (!num)
		{
			StorePlayerSprites();
			DeferJIPPlayerStart(GetLobbyOwner());
			LobbySystem.PreJoinEvent.Send(this);
		}
	}

	private void DeferJIPPlayerStart(TTNetworkID remoteHostID)
	{
		d.Assert(remoteHostID.IsValid(), "[Lobby] ASSERT: DeferJIPPlayerStart RemoteHostID is NOT valid!");
		m_DelayedJIPRemoteHostID = remoteHostID;
		m_DelayedJIPCountdown = Time.unscaledTime + 25f;
	}

	private void PerformDelayedJIPPlayerStart()
	{
		bool flag = false;
		if (!m_DelayedJIPRemoteHostID.IsValid() || LobbySystem.IsLobbySetupDelayed || flag)
		{
			return;
		}
		if (GetLobbyOwner() == m_DelayedJIPRemoteHostID)
		{
			d.Assert(m_DelayedJIPCountdown > 0f);
			m_DelayedJIPCountdown -= Time.unscaledDeltaTime;
			bool flag2 = LobbySystem.QueryGameState.IsTerrainGenerated();
			if (m_DelayedJIPCountdown <= 0f || flag2)
			{
				LobbySystem.SendEventPreJIP(this);
				ConnectToRemoteHost(m_DelayedJIPRemoteHostID);
				LobbySystem.SendEventJIP(this);
				m_DelayedJIPRemoteHostID = TTNetworkID.Invalid;
				m_DelayedJIPCountdown = 0f;
			}
		}
		else
		{
			m_DelayedJIPRemoteHostID = TTNetworkID.Invalid;
			m_DelayedJIPCountdown = 0f;
		}
	}

	private void ConnectToRemoteHost(TTNetworkID remoteHostID)
	{
		m_RemoteHostExpectedToConnectTo = remoteHostID;
		TTNetworkConnection conn = CreateConnectionToHost(remoteHostID);
		LobbySystem.SendEventStartAsClient(conn);
	}

	private bool HasLateJIPPlayer(TTNetworkID playerID)
	{
		d.Assert(playerID.IsValid(), "[Lobby] ASSERT - HasLateJIPPlayer given an invalid PlayerID!");
		bool result = false;
		for (int i = 0; i < m_LateJIPPlayerIDs.Count; i++)
		{
			if (m_LateJIPPlayerIDs[i].Equals(playerID))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	private void RemoveFromLateJIPPlayers(TTNetworkID playerID)
	{
		d.Assert(playerID.IsValid(), "[Lobby] ASSERT - RemoveFromLateJIPPlayers given an invalid PlayerID!");
		for (int i = 0; i < m_LateJIPPlayerIDs.Count; i++)
		{
			if (m_LateJIPPlayerIDs[i].Equals(playerID))
			{
				m_LateJIPPlayerIDs.RemoveAt(i);
				break;
			}
		}
		d.Assert(!HasLateJIPPlayer(playerID), "[Lobby] ASSERT - RemoveFromLateJIPPlayers still has Late JIP Player PlayerID=" + playerID);
	}

	private void HandleLateJIPPlayers()
	{
		d.Assert(LobbySystem.IsServer, "[Lobby] ASSERT - Not the server!");
		if (LobbySystem.QueryGameState.IsNetControllerNull() || m_LateJIPPlayerIDs.Count <= 0)
		{
			return;
		}
		for (int num = m_LateJIPPlayerIDs.Count - 1; num >= 0; num--)
		{
			bool flag = HandleJIPPlayerJoined(m_LateJIPPlayerIDs[num]);
			d.AssertFormat(flag, "Tried to join Late JIP player {0} but failed!", m_LateJIPPlayerIDs[num]);
			if (flag)
			{
				m_LateJIPPlayerIDs.RemoveAt(num);
			}
		}
		d.Assert(m_LateJIPPlayerIDs.Count == 0, "[Lobby] ASSERT - Didn't add all JIP players!");
	}

	private bool HandleJIPPlayerJoined(TTNetworkID changedPlayerID)
	{
		if (GetPlayerData(changedPlayerID, out var playerData))
		{
			int teamMatchChoice = Data.TeamMatchChoice;
			switch (Data.GameTypeChoice)
			{
			case MultiplayerModeType.Deathmatch:
				if (playerData.m_TeamID == -1 && teamMatchChoice == 1)
				{
					int recommendedTeamForNewPlayer = GetRecommendedTeamForNewPlayer();
					if (recommendedTeamForNewPlayer > -1)
					{
						SetPlayerTeam(changedPlayerID, recommendedTeamForNewPlayer);
						playerData.m_TeamID = recommendedTeamForNewPlayer;
						playerData.m_Colour = LobbySystem.DeathmatchColours[recommendedTeamForNewPlayer];
					}
				}
				break;
			case MultiplayerModeType.CoOpCreative:
			case MultiplayerModeType.CoOpCampaign:
				SetPlayerTeam(changedPlayerID, 0);
				playerData.m_TeamID = 0;
				playerData.m_Colour = LobbySystem.CoOpColours[0];
				break;
			default:
				d.LogError("Unsupported gametype in HandleJIP");
				break;
			}
			if (HasClientConfig(changedPlayerID))
			{
				m_ClientConfigs.Remove(changedPlayerID);
			}
			m_ClientConfigs.Add(changedPlayerID, playerData);
			if (playerData.m_Sprite != null)
			{
				StorePlayerSprite(changedPlayerID, playerData.m_Sprite);
			}
			SendLobbyStateToJIPPlayer(changedPlayerID);
			return true;
		}
		return false;
	}

	protected string GetSetPiecePlacementAsString()
	{
		string text = ManSaveGame.SaveObjectToRawJson(Singleton.Manager<ManWorld>.inst.GetSetPiecePlacement());
		d.Log("Lobby.GetSetPiecePlacementAsString: " + text);
		return text;
	}

	protected List<ManWorld.SavedSetPiece> SetPiecePlacementFromString(string s)
	{
		List<ManWorld.SavedSetPiece> objectToLoad = null;
		if (!string.IsNullOrEmpty(s))
		{
			d.Log("Lobby.SetPiecePlacementFromString: " + s);
			ManSaveGame.LoadObjectFromRawJson(ref objectToLoad, s);
		}
		if (objectToLoad != null)
		{
			foreach (ManWorld.SavedSetPiece item in objectToLoad)
			{
				d.Log($"Lobby.SetPiecePlacementFromString: {item.m_Name} @ {item.m_WorldPosition}");
			}
		}
		return objectToLoad;
	}

	public void WriteStartGameSettings(BinaryWriter writer)
	{
		string value = ChoicesToString(Data.m_Choices);
		writer.Write(value);
		writer.Write((int)Singleton.Manager<ManNetwork>.inst.MapSize);
		writer.Write((Singleton.Manager<ManNetwork>.inst.WorldSeed == null) ? "" : Singleton.Manager<ManNetwork>.inst.WorldSeed);
		writer.Write((Singleton.Manager<ManNetwork>.inst.BiomeChoice == null) ? "" : Singleton.Manager<ManNetwork>.inst.BiomeChoice);
		writer.Write(Singleton.Manager<ManNetwork>.inst.WorldGenVersionID);
		writer.Write(Singleton.Manager<ManNetwork>.inst.WorldGenVersionType);
		writer.Write(Singleton.Manager<ManNetwork>.inst.MapCenter);
		List<ManWorld.SavedSetPiece> setPiecePlacement = Singleton.Manager<ManWorld>.inst.GetSetPiecePlacement();
		writer.WritePackedUInt32((uint)setPiecePlacement.Count);
		for (int i = 0; i < setPiecePlacement.Count; i++)
		{
			setPiecePlacement[i].Write(writer);
		}
		Singleton.Manager<ManMods>.inst.WriteLobbySession(writer);
	}

	public void ReadStartGameSettings(BinaryReader reader)
	{
		string lobbyChoices = reader.ReadString();
		ManNetwork.MapSizeOption mapSize = (ManNetwork.MapSizeOption)reader.ReadInt32();
		string worldSeed = reader.ReadString();
		string biomeChoice = reader.ReadString();
		WorldGenVersionData worldGen = new WorldGenVersionData(reader.ReadInt32(), (BiomeMap.WorldGenVersioningType)reader.ReadInt32());
		WorldPosition tetherPoint = reader.ReadWorldPosition();
		int num = (int)reader.ReadPackedUInt32();
		List<ManWorld.SavedSetPiece> list = new List<ManWorld.SavedSetPiece>(num);
		for (int i = 0; i < num; i++)
		{
			list.Add(new ManWorld.SavedSetPiece(reader));
		}
		ClientLoadLobbyChoices(lobbyChoices, mapSize, worldSeed, biomeChoice, tetherPoint, list, worldGen);
		Singleton.Manager<ManMods>.inst.ReadLobbySession(reader);
	}

	protected virtual void SendLobbyStateToJIPPlayer(TTNetworkID changedPlayerID)
	{
		m_MemWriter.Seek(0, SeekOrigin.Begin);
		m_MemWriter.Write((byte)4);
		changedPlayerID.WriteTo(m_MemWriter);
		WriteStartGameSettings(m_MemWriter);
		SendLobbyChatMsg(m_MemBuffer, (int)m_MemWriter.BaseStream.Position);
	}

	public void SetupLobbyFromData()
	{
		m_PlayerTeams.Clear();
		m_Players.Clear();
		m_Data.m_NumFriends = 0;
		string lobbyData = LobbySystem.GetLobbyData(ID, "playerTeams");
		if (!string.IsNullOrEmpty(lobbyData))
		{
			ManSaveGame.LoadObjectFromRawJson(ref m_PlayerTeams, lobbyData);
		}
		for (int i = 0; i < LobbySystem.GetNumLobbyMembers(ID); i++)
		{
			TTNetworkID lobbyMemberByIndex = LobbySystem.GetLobbyMemberByIndex(ID, i);
			if (lobbyMemberByIndex.IsValid())
			{
				LobbyPlayerData item = SetupPlayerDataAndCheckTeam(lobbyMemberByIndex);
				m_Players.Add(item);
				if (LobbySystem.HasFriend(lobbyMemberByIndex))
				{
					m_Data.m_NumFriends++;
				}
			}
		}
	}

	public void OnLocalPlayerLeave()
	{
		m_WasLobbyOwner = false;
	}

	public void OnPlayerTeamsUpdated(NetPlayer player)
	{
		for (int i = 0; i < m_Players.Count; i++)
		{
			if (m_Players[i].m_PlayerID == player.GetPlayerIDInLobby())
			{
				LobbyPlayerData value = m_Players[i];
				value.m_TeamID = player.LobbyTeamID;
				m_Players[i] = value;
				break;
			}
		}
	}

	public virtual void UpdateLobby()
	{
		Update();
		if (IsLobbyOwner())
		{
			UpdateLobbyGameStatus(forceUpdate: false);
			if (LobbySystem.IsServer)
			{
				HandleLateJIPPlayers();
			}
		}
		PerformDelayedJIPPlayerStart();
	}
}
