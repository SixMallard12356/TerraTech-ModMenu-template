#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;
using Epic.OnlineServices.P2P;
using PlayEveryWare.EpicOnlineServices;
using UnityEngine;
using UnityEngine.Networking;

namespace TerraTech.Network;

public class PlatformLobby_EOS : Lobby
{
	public class LobbyMember
	{
		public ProductUserId ProductId;

		public const string kDisplayNameKey = "DISPLAYNAME";

		public Dictionary<string, LobbyAttribute> MemberAttributes = new Dictionary<string, LobbyAttribute>();

		public string DisplayName
		{
			get
			{
				MemberAttributes.TryGetValue("DISPLAYNAME", out var value);
				return value?.AsString ?? string.Empty;
			}
		}
	}

	public class LobbyAttribute
	{
		public LobbyAttributeVisibility Visibility;

		public AttributeType ValueType = AttributeType.String;

		public string Key;

		public long? AsInt64 = 0L;

		public double? AsDouble = 0.0;

		public bool? AsBool = false;

		public string AsString;

		public AttributeData AsAttribute
		{
			get
			{
				AttributeData result = new AttributeData
				{
					Key = Key,
					Value = default(AttributeDataValue)
				};
				switch (ValueType)
				{
				case AttributeType.String:
					result.Value = AsString;
					break;
				case AttributeType.Int64:
					result.Value = AsInt64.Value;
					break;
				case AttributeType.Double:
					result.Value = AsDouble.Value;
					break;
				case AttributeType.Boolean:
					result.Value = AsBool.Value;
					break;
				}
				return result;
			}
		}

		public override bool Equals(object other)
		{
			LobbyAttribute lobbyAttribute = (LobbyAttribute)other;
			if (ValueType == lobbyAttribute.ValueType && AsInt64 == lobbyAttribute.AsInt64 && AsDouble == lobbyAttribute.AsDouble && AsBool == lobbyAttribute.AsBool && AsString == lobbyAttribute.AsString && Key == lobbyAttribute.Key)
			{
				return Visibility == lobbyAttribute.Visibility;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public LobbyAttribute()
		{
		}

		public LobbyAttribute(string key, string value, LobbyAttributeVisibility visibility = LobbyAttributeVisibility.Public)
		{
			Key = key.ToUpper();
			Visibility = visibility;
			ValueType = AttributeType.String;
			SetValue(value ?? string.Empty);
		}

		public void InitFromAttribute(Epic.OnlineServices.Lobby.Attribute? attributeParam)
		{
			AttributeData value = (attributeParam?.Data).Value;
			Key = value.Key;
			ValueType = value.Value.ValueType;
			switch (value.Value.ValueType)
			{
			case AttributeType.Boolean:
				AsBool = value.Value.AsBool;
				break;
			case AttributeType.Int64:
				AsInt64 = value.Value.AsInt64;
				break;
			case AttributeType.Double:
				AsDouble = value.Value.AsDouble;
				break;
			case AttributeType.String:
				AsString = value.Value.AsUtf8;
				break;
			}
		}

		public void SetValue(AttributeDataValue value, bool changeValueType = false)
		{
			d.AssertFormat(changeValueType || ValueType == value.ValueType, "Value type mismatch {0} vs {1}. Changing value type is not supported", ValueType, value.ValueType);
			if (changeValueType)
			{
				ValueType = value.ValueType;
			}
			switch (ValueType)
			{
			case AttributeType.Boolean:
				AsBool = value.AsBool;
				break;
			case AttributeType.Int64:
				AsInt64 = value.AsInt64;
				break;
			case AttributeType.Double:
				AsDouble = value.AsDouble;
				break;
			case AttributeType.String:
			{
				Utf8String asUtf = value.AsUtf8;
				AsString = ((asUtf.Length == 0) ? ((Utf8String)string.Empty) : asUtf);
				break;
			}
			}
		}

		public static bool Set(List<LobbyAttribute> attributesList, string key, string value)
		{
			d.AssertFormat(value != null, "Lobby data does not support null string data; please specify a valid value for key:{0}", key);
			string keyInUpper = key.ToUpper();
			LobbyAttribute lobbyAttribute = attributesList.Find((LobbyAttribute l) => l.Key == keyInUpper);
			bool result;
			if (lobbyAttribute == null)
			{
				LobbyAttributeVisibility visibility = LobbyAttributeVisibility.Public;
				lobbyAttribute = new LobbyAttribute(keyInUpper, value, visibility);
				attributesList.Add(lobbyAttribute);
				result = true;
			}
			else
			{
				string text = value ?? string.Empty;
				result = lobbyAttribute.AsString != text;
				lobbyAttribute.SetValue(text);
			}
			return result;
		}
	}

	public struct LobbyParams
	{
		public string BucketId;

		public uint MaxNumLobbyMembers;

		public LobbyVisibility LobbyVisibility;

		public bool AllowInvites;

		public List<LobbyAttribute> Attributes;
	}

	public class LobbyData_EOS
	{
		public Utf8String Id;

		public ProductUserId LobbyOwner = new ProductUserId();

		public LobbyDetails LobbyDetailsHandle;

		public string BucketId;

		public uint MaxNumLobbyMembers;

		public LobbyVisibility LobbyVisibility;

		public bool AllowInvites = true;

		public List<LobbyAttribute> Attributes = new List<LobbyAttribute>();

		public int NumLobbyMembers;

		public bool IsLobbyMember;

		public List<LobbyMember> Members = new List<LobbyMember>();

		public bool _SearchResult;

		public bool _BeingCreated;

		public bool TryInitFromLobbyHandle(Utf8String lobbyId)
		{
			d.AssertFormat(IsLobbyMember, "Trying to initialise lobby details for {0} without first being a member of that lobby. This is illegal and will fail!", lobbyId);
			if (string.IsNullOrEmpty(lobbyId))
			{
				return false;
			}
			Id = lobbyId;
			CopyLobbyDetailsHandleOptions options = new CopyLobbyDetailsHandleOptions
			{
				LocalUserId = EOSManager.Instance.GetProductUserId(),
				LobbyId = lobbyId
			};
			LobbyDetails outLobbyDetailsHandle;
			Result result = EOSManager.Instance.GetEOSLobbyInterface().CopyLobbyDetailsHandle(ref options, out outLobbyDetailsHandle);
			if (outLobbyDetailsHandle == null)
			{
				d.LogError("Lobbies (InitFromLobbyHandle): can't get lobby info handle. outLobbyDetailsHandle is null");
				return false;
			}
			if (result != Result.Success)
			{
				d.LogErrorFormat("Lobbies (InitFromLobbyHandle): can't get lobby info handle. Error code: {0}", result);
				outLobbyDetailsHandle.Release();
				return false;
			}
			InitFromLobbyDetails(outLobbyDetailsHandle);
			return true;
		}

		public void InitFromLobbyDetails(LobbyDetails lobbyDetailsHandle)
		{
			LobbyDetailsHandle = lobbyDetailsHandle;
			LobbyDetailsGetLobbyOwnerOptions options = default(LobbyDetailsGetLobbyOwnerOptions);
			ProductUserId lobbyOwner = lobbyDetailsHandle.GetLobbyOwner(ref options);
			if (lobbyOwner != LobbyOwner)
			{
				LobbyOwner = lobbyOwner;
			}
			LobbyDetailsCopyInfoOptions options2 = default(LobbyDetailsCopyInfoOptions);
			LobbyDetailsInfo? outLobbyDetailsInfo;
			Result result = lobbyDetailsHandle.CopyInfo(ref options2, out outLobbyDetailsInfo);
			if (result != Result.Success)
			{
				d.LogErrorFormat("Lobbies (InitFromLobbyDetails): can't copy lobby info. Error code: {0}", result);
				return;
			}
			if (!outLobbyDetailsInfo.HasValue)
			{
				d.LogError("Lobbies: (InitFromLobbyDetails) could not copy info: outLobbyDetailsInfo is null.");
				return;
			}
			Id = outLobbyDetailsInfo?.LobbyId;
			MaxNumLobbyMembers = (outLobbyDetailsInfo?.MaxMembers).Value;
			LobbyVisibility = (outLobbyDetailsInfo?.PermissionLevel.AsVisibility()).Value;
			AllowInvites = (outLobbyDetailsInfo?.AllowInvites).Value;
			NumLobbyMembers = (int)(outLobbyDetailsInfo?.MaxMembers - outLobbyDetailsInfo?.AvailableSlots).Value;
			BucketId = outLobbyDetailsInfo?.BucketId;
			Attributes.Clear();
			LobbyDetailsGetAttributeCountOptions options3 = default(LobbyDetailsGetAttributeCountOptions);
			uint attributeCount = lobbyDetailsHandle.GetAttributeCount(ref options3);
			for (uint num = 0u; num < attributeCount; num++)
			{
				LobbyDetailsCopyAttributeByIndexOptions options4 = new LobbyDetailsCopyAttributeByIndexOptions
				{
					AttrIndex = num
				};
				if (lobbyDetailsHandle.CopyAttributeByIndex(ref options4, out var outAttribute) == Result.Success && outAttribute.HasValue && outAttribute.HasValue && outAttribute.GetValueOrDefault().Data.HasValue)
				{
					LobbyAttribute lobbyAttribute = new LobbyAttribute();
					lobbyAttribute.InitFromAttribute(outAttribute);
					lobbyAttribute.Key = lobbyAttribute.Key.ToUpper();
					Attributes.Add(lobbyAttribute);
				}
			}
			Members.Clear();
			LobbyDetailsGetMemberCountOptions options5 = default(LobbyDetailsGetMemberCountOptions);
			uint memberCount = lobbyDetailsHandle.GetMemberCount(ref options5);
			if (!IsLobbyMember)
			{
				return;
			}
			for (int i = 0; i < memberCount; i++)
			{
				LobbyDetailsGetMemberByIndexOptions options6 = new LobbyDetailsGetMemberByIndexOptions
				{
					MemberIndex = (uint)i
				};
				ProductUserId memberByIndex = lobbyDetailsHandle.GetMemberByIndex(ref options6);
				bool flag = memberByIndex != null && memberByIndex.IsValid();
				d.AssertFormat(flag, "Invalid member returned at index {0}!", i);
				if (!flag)
				{
					continue;
				}
				Members.Insert(i, new LobbyMember
				{
					ProductId = memberByIndex
				});
				LobbyDetailsGetMemberAttributeCountOptions options7 = new LobbyDetailsGetMemberAttributeCountOptions
				{
					TargetUserId = memberByIndex
				};
				int memberAttributeCount = (int)lobbyDetailsHandle.GetMemberAttributeCount(ref options7);
				for (int j = 0; j < memberAttributeCount; j++)
				{
					LobbyDetailsCopyMemberAttributeByIndexOptions options8 = new LobbyDetailsCopyMemberAttributeByIndexOptions
					{
						AttrIndex = (uint)j,
						TargetUserId = memberByIndex
					};
					Epic.OnlineServices.Lobby.Attribute? outAttribute2;
					Result result2 = lobbyDetailsHandle.CopyMemberAttributeByIndex(ref options8, out outAttribute2);
					if (result2 != Result.Success)
					{
						d.LogFormat("Lobbies (InitFromLobbyDetails): can't copy member attribute. Error code: {0}", result2);
					}
					else
					{
						LobbyAttribute lobbyAttribute2 = new LobbyAttribute();
						lobbyAttribute2.InitFromAttribute(outAttribute2);
						Members[i].MemberAttributes.Add(lobbyAttribute2.Key, lobbyAttribute2);
					}
				}
			}
		}

		public string GetLobbyAttribute(string key)
		{
			string keyInLookup = key.ToUpper();
			return Attributes.Find((LobbyAttribute l) => l.Key == keyInLookup)?.AsString;
		}

		public bool IsValid()
		{
			if (Id != null)
			{
				return Id.Length > 0;
			}
			return false;
		}

		public bool IsOwner(ProductUserId userProductId)
		{
			return userProductId == LobbyOwner;
		}

		public LobbyParams GetParams()
		{
			List<LobbyAttribute> list = new List<LobbyAttribute>(Attributes.Count);
			foreach (LobbyAttribute attribute in Attributes)
			{
				LobbyAttribute lobbyAttribute = new LobbyAttribute();
				lobbyAttribute.Key = attribute.Key;
				lobbyAttribute.Visibility = attribute.Visibility;
				lobbyAttribute.SetValue(attribute.AsAttribute.Value, changeValueType: true);
				list.Add(lobbyAttribute);
			}
			return new LobbyParams
			{
				BucketId = BucketId,
				MaxNumLobbyMembers = MaxNumLobbyMembers,
				LobbyVisibility = LobbyVisibility,
				AllowInvites = AllowInvites,
				Attributes = list
			};
		}

		public void Clear()
		{
			Id = string.Empty;
			LobbyOwner = new ProductUserId();
			LobbyDetailsHandle?.Release();
			LobbyDetailsHandle = null;
			Attributes.Clear();
			Members.Clear();
			IsLobbyMember = false;
			_BeingCreated = false;
		}
	}

	private LobbyData_EOS PlatformLobbyData;

	private List<TTNetworkID> m_DeadConnections = new List<TTNetworkID>(16);

	private Dictionary<string, string> m_LobbyModifications = new Dictionary<string, string>();

	private Dictionary<TTNetworkID, TTNetworkConnection> m_NetConnection = new Dictionary<TTNetworkID, TTNetworkConnection>();

	private int m_NextConnectionID = 1;

	private HostTopology m_HostTopology;

	private const string kMirrorLobbyKeyFormat = "mirror_{0}";

	private const int kImageIDUseDefault = 0;

	private PlatformLobbySystem_EOS LobbySystem_Platform => base.LobbySystem as PlatformLobbySystem_EOS;

	public PlatformLobby_EOS(LobbySystem system, LobbyData data, ConnectionConfig config, MultiplayerModeType gameType)
		: base(system, data)
	{
		PlatformLobbyData = LobbySystem_Platform.GetPlatformLobbyData(data.m_IDLobby.m_NetworkID);
		d.Assert(PlatformLobbyData != null, "Creating lobby object while platform data isn't present yet?!");
		m_HostTopology = new HostTopology(config, base.LobbySystem.GetMaxPlayerCount(gameType));
	}

	public override void Shutdown()
	{
		base.Shutdown();
		foreach (TTNetworkID key in m_NetConnection.Keys)
		{
			if (!m_DeadConnections.Contains(key))
			{
				m_DeadConnections.Add(key);
			}
		}
		foreach (TTNetworkID deadConnection in m_DeadConnections)
		{
			if (m_NetConnection.ContainsKey(deadConnection))
			{
				OnConnectionDisposed(m_NetConnection[deadConnection]);
			}
		}
		m_DeadConnections.Clear();
		m_NetConnection.Clear();
	}

	public override string GetLocalPlayerName()
	{
		return LobbySystem_Platform.GetUserName(base.LobbySystem.GetLocalPlayerID());
	}

	public override TTNetworkID GetLobbyOwner()
	{
		return PlatformLobbyData.LobbyOwner.ToTTID();
	}

	public override bool IsLobbyOwner()
	{
		return base.LobbySystem.GetLocalPlayerID() == GetLobbyOwner();
	}

	public override void RemoveClientConnectionFromServer(TTNetworkID deadNetworkId)
	{
		m_DeadConnections.Add(deadNetworkId);
	}

	public override void KickPlayer(TTNetworkID playerID)
	{
		d.Assert(playerID.IsValid(), "Lobbies (KickMember): playerID is invalid!");
		KickMemberOptions options = new KickMemberOptions
		{
			LocalUserId = base.LobbySystem.GetLocalPlayerID().ToEOSProductUserID(),
			LobbyId = PlatformLobbyData.Id,
			TargetUserId = playerID.ToEOSProductUserID()
		};
		EOSManager.Instance.GetEOSLobbyInterface().KickMember(ref options, null, OnKickMemberCompleted);
		base.KickPlayer(playerID);
	}

	public override void BanPlayer(TTNetworkID playerID)
	{
		base.BanPlayer(playerID);
	}

	public string GetMirrorLobbyKey(ExternalAccountType accountPlatformType)
	{
		return $"mirror_{accountPlatformType}";
	}

	public TTNetworkID GetMirrorLobbyID(EOSAccountPlatform_Base accountPlatform)
	{
		string mirrorLobbyKey = GetMirrorLobbyKey(accountPlatform.PlatformType);
		string lobbyData = base.LobbySystem.GetLobbyData(base.ID, mirrorLobbyKey);
		if (!lobbyData.NullOrEmpty())
		{
			return new TTNetworkID(lobbyData);
		}
		return TTNetworkID.Invalid;
	}

	public void SetMirrorLobbyID(EOSAccountPlatform_Base accountPlatform, TTNetworkID platformLobbyID)
	{
		string mirrorLobbyKey = GetMirrorLobbyKey(accountPlatform.PlatformType);
		string value = platformLobbyID.ToString();
		SetLobbyData(mirrorLobbyKey, value);
	}

	protected override void SetLobbyVisibility(LobbyVisibility visibility)
	{
		if (visibility != PlatformLobbyData.LobbyVisibility)
		{
			LobbyParams lobbyParamUpdates = PlatformLobbyData.GetParams();
			lobbyParamUpdates.LobbyVisibility = visibility;
			LobbySystem_Platform.ModifyLobby(lobbyParamUpdates, null);
		}
	}

	protected override void SendLobbyChatMsg(byte[] memBuffer, int numBytesToWrite)
	{
		LobbySystem_Platform.SendPacketToAll(new ArraySegment<byte>(memBuffer, 0, numBytesToWrite), LobbySystem_Platform.m_ChannelLobbyChat, PacketReliability.ReliableOrdered, "Chat", errorOnFail: true, sendToSelf: true);
	}

	protected override int GetLargeFriendAvatarImageID(TTNetworkID eosPlayerID)
	{
		int num = -1;
		EOSAccountPlatform_Base nativeAccountPlatform = LobbySystem_Platform.NativeAccountPlatform;
		if (nativeAccountPlatform.HasUserInformation(eosPlayerID))
		{
			if (nativeAccountPlatform.TryGetPlatformType(eosPlayerID, out var platformType))
			{
				switch (platformType)
				{
				case ExternalAccountType.Steam:
				{
					EOSAccountPlatform_Base accountPlatform = LobbySystem_Platform.GetAccountPlatform(platformType);
					if (accountPlatform != null && accountPlatform.SupportsAvatars)
					{
						nativeAccountPlatform.TryGetPlatformID(eosPlayerID, out var platformID);
						num = accountPlatform.GetLargeFriendAvatarImageID(platformID);
						d.Assert(num != 0, "Steam imageId is that of our own configured Epic override.");
					}
					else
					{
						num = 0;
					}
					break;
				}
				case ExternalAccountType.Epic:
					num = 0;
					break;
				default:
					d.LogErrorFormat("GetLargeFriendAvatarImageID - Non implemented platform {0}", SKU.CurrentBuildType);
					break;
				}
			}
			else
			{
				d.LogErrorFormat("GetLargeFriendAvatarImageID - Failed to resolve external platform mapping for user {0}!", eosPlayerID);
			}
		}
		return num;
	}

	protected override Sprite GetSpriteFromImageID(int imageID, int imageWidthHeight)
	{
		if (imageID == 0)
		{
			return Singleton.Manager<ManEOS>.inst.GetDefaultUserSprite();
		}
		d.AssertFormat(SKU.IsSteam, "GetSpriteFromImageID - Expected user sprite to be for Steam, but found a valid sprite imageId {0} on platform {1}", imageID, SKU.CurrentBuildType);
		return LobbySystem_Platform.GetAccountPlatform(ExternalAccountType.Steam)?.GetSpriteFromImageID(imageID, imageWidthHeight);
	}

	protected override void CleanUpPreviousSession()
	{
		d.AssertFormat(m_NetConnection.Count == 0, "[PlatformLobby_EOS] CleanUpPreviousSession already is connected to {0} endpoints - disconnecting", m_NetConnection.Count);
		foreach (KeyValuePair<TTNetworkID, TTNetworkConnection> item in m_NetConnection)
		{
			if (NetworkServer.active)
			{
				NetworkServer.RemoveExternalConnection(item.Value.connectionId);
			}
		}
		m_NetConnection.Clear();
		m_DeadConnections.Clear();
		LobbySystem_Platform.ReadAllNetworkData(forceAll: true);
		ApplyLobbyModifications();
		m_LobbyModifications.Clear();
	}

	protected override TTNetworkConnection CreateConnectionToHost(TTNetworkID hostNetworkID)
	{
		CleanUpPreviousSession();
		TTNetworkConnection tTNetworkConnection = CreateTTNetworkConnection(hostNetworkID, 0);
		m_NetConnection.Add(hostNetworkID, tTNetworkConnection);
		return tTNetworkConnection;
	}

	public TTNetworkConnection GetTTNetworkConnection(TTNetworkID playerID)
	{
		m_NetConnection.TryGetValue(playerID, out var value);
		return value;
	}

	protected override void Update()
	{
		ApplyLobbyModifications();
		FlushNetworkData();
		if (m_DeadConnections.Count > 0)
		{
			for (int i = 0; i < m_DeadConnections.Count; i++)
			{
				TTNetworkID key = m_DeadConnections[i];
				if (m_NetConnection.ContainsKey(key))
				{
					TTNetworkConnection conn = m_NetConnection[key];
					OnConnectionDisposed(conn);
				}
			}
		}
		m_DeadConnections.Clear();
	}

	protected override void SetLobbyData(string keyName, string value)
	{
		m_LobbyModifications[keyName] = value;
	}

	private void ApplyLobbyModifications()
	{
		if (m_LobbyModifications.Count == 0)
		{
			return;
		}
		LobbyParams lobbyParamUpdates = PlatformLobbyData.GetParams();
		bool flag = false;
		foreach (KeyValuePair<string, string> lobbyModification in m_LobbyModifications)
		{
			string key = lobbyModification.Key;
			string value = lobbyModification.Value;
			if (key == "lobbyPublic")
			{
				if (!Enum.TryParse<LobbyVisibility>(value, out lobbyParamUpdates.LobbyVisibility))
				{
					d.LogErrorFormat("Failed to parse {0} as LobbyVisibility", value);
					lobbyParamUpdates.LobbyVisibility = LobbyVisibility.Private;
				}
				flag = true;
			}
			else if (key == "ownerID")
			{
				PlatformLobbyData.LobbyOwner = ProductUserId.FromString(value);
			}
			else
			{
				LobbyAttribute.Set(lobbyParamUpdates.Attributes, key, value);
				flag = true;
			}
		}
		if (flag)
		{
			LobbySystem_Platform.ModifyLobby(lobbyParamUpdates, null);
		}
		m_LobbyModifications.Clear();
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
		TTNetworkConnection.NetworkStats result = default(TTNetworkConnection.NetworkStats);
		foreach (KeyValuePair<TTNetworkID, TTNetworkConnection> item in m_NetConnection)
		{
			result.accummulate(item.Value.GetNetworkStats());
		}
		return result;
	}

	private TTNetworkConnection CreateTTNetworkConnection(TTNetworkID hostNetworkID, int connectionID)
	{
		TTNetworkConnection tTNetworkConnection = new TTNetworkConnection(m_HostTopology, hostNetworkID);
		string networkAddress = hostNetworkID.ToString();
		int networkHostId = -1;
		tTNetworkConnection.Initialize(networkAddress, networkHostId, connectionID, m_HostTopology);
		tTNetworkConnection.OnDisposed.Subscribe(OnConnectionDisposed);
		return tTNetworkConnection;
	}

	private void FlushNetworkData()
	{
		foreach (TTNetworkConnection value in m_NetConnection.Values)
		{
			value.Flush();
		}
	}

	private void OnKickMemberCompleted(ref KickMemberCallbackInfo data)
	{
		if (data.ResultCode != Result.Success)
		{
			d.LogErrorFormat("Lobbies (OnKickMemberFinished): error code: {0}", data.ResultCode);
		}
	}

	private void AddServerConnectionToClient(TTNetworkID clientNetworkId, LobbyPlayerData config)
	{
		TTNetworkConnection tTNetworkConnection = CreateTTNetworkConnection(clientNetworkId, m_NextConnectionID);
		m_NextConnectionID++;
		base.LobbySystem.SendEventStorePlayerConfig(tTNetworkConnection, config);
		m_NetConnection.Add(clientNetworkId, tTNetworkConnection);
		NetworkServer.AddExternalConnection(tTNetworkConnection);
	}

	public bool ReadNetworkData(byte[] packetData, uint packetSize, int packetChannelId, ProductUserId remoteClientId)
	{
		TTNetworkID tTNetworkID = remoteClientId.ToTTID();
		bool flag = m_NetConnection.ContainsKey(tTNetworkID);
		if (!flag && GetClientConfig(tTNetworkID, out var playerConfig))
		{
			RemoveClientConfig(tTNetworkID);
			AddServerConnectionToClient(tTNetworkID, playerConfig);
			flag = true;
		}
		if (flag)
		{
			int num = Convert.ToInt32(packetSize);
			if (num > 0)
			{
				m_NetConnection[tTNetworkID].TransportReceive(packetData, num, packetChannelId);
			}
		}
		return flag;
	}

	private void OnConnectionDisposed(TTNetworkConnection conn)
	{
		d.LogFormat("[PlatformLobby_EOS] Connection disposed {0}", conn.RemoteClientID.m_NetworkID);
		if (NetworkServer.active)
		{
			NetworkServer.RemoveExternalConnection(conn.connectionId);
		}
		m_NetConnection.Remove(conn.RemoteClientID);
		LobbySystem_Platform.CloseP2PSessionAfterDelay(conn.RemoteClientID);
	}
}
