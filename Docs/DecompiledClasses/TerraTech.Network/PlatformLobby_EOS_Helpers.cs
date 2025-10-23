#define UNITY_EDITOR
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;

namespace TerraTech.Network;

public static class PlatformLobby_EOS_Helpers
{
	public static ProductUserId ToEOSProductUserID(this TTNetworkID TTnetworkID)
	{
		if (!TTnetworkID.IsValid())
		{
			return new ProductUserId();
		}
		return ProductUserId.FromString(TTnetworkID.m_NetworkID);
	}

	public static TTNetworkID ToTTID(this ProductUserId productUserId)
	{
		return new TTNetworkID(productUserId?.ToString());
	}

	public static string ToStringShort(this ProductUserId id)
	{
		string text = id.ToString();
		return text.Substring(text.Length - 4);
	}

	public static Utf8String ToEOSLobbyID(this TTNetworkID TTnetworkID)
	{
		return new Utf8String(TTnetworkID.m_NetworkID);
	}

	public static TTNetworkID ToTTID(this Utf8String lobbyId)
	{
		return new TTNetworkID(lobbyId?.ToString());
	}

	public static EpicAccountId ToEpicAccountID(this TTNetworkID TTnetworkID)
	{
		if (!TTnetworkID.IsValid())
		{
			return new EpicAccountId();
		}
		return EpicAccountId.FromString(TTnetworkID.m_NetworkID);
	}

	public static TTNetworkID ToTTID(this EpicAccountId epicAccountId)
	{
		return new TTNetworkID(epicAccountId?.ToString());
	}

	public static LobbyPermissionLevel AsPremissionLevel(this Lobby.LobbyVisibility visibility)
	{
		switch (visibility)
		{
		case Lobby.LobbyVisibility.Private:
			return LobbyPermissionLevel.Inviteonly;
		case Lobby.LobbyVisibility.FriendsOnly:
			return LobbyPermissionLevel.Joinviapresence;
		case Lobby.LobbyVisibility.Public:
			return LobbyPermissionLevel.Publicadvertised;
		default:
			d.LogErrorFormat("Unsupported Lobby visibility type conversion from {0}", visibility);
			return LobbyPermissionLevel.Publicadvertised;
		}
	}

	public static Lobby.LobbyVisibility AsVisibility(this LobbyPermissionLevel permissionLevel)
	{
		switch (permissionLevel)
		{
		case LobbyPermissionLevel.Inviteonly:
			return Lobby.LobbyVisibility.Private;
		case LobbyPermissionLevel.Joinviapresence:
			return Lobby.LobbyVisibility.FriendsOnly;
		case LobbyPermissionLevel.Publicadvertised:
			return Lobby.LobbyVisibility.Public;
		default:
			d.LogErrorFormat("Unsupported Lobby PermissionLevel type conversion from {0}", permissionLevel);
			return Lobby.LobbyVisibility.Private;
		}
	}

	public static bool TryGetMemberLobbyState(LobbyMemberStatus memberStatus, out Lobby.MemberLobbyStateMask outLobbyState)
	{
		switch (memberStatus)
		{
		case LobbyMemberStatus.Joined:
			outLobbyState = Lobby.MemberLobbyStateMask.MLS_Entered;
			return true;
		case LobbyMemberStatus.Left:
			outLobbyState = Lobby.MemberLobbyStateMask.MLS_Left;
			return true;
		case LobbyMemberStatus.Disconnected:
		case LobbyMemberStatus.Closed:
			outLobbyState = Lobby.MemberLobbyStateMask.MLS_Disconnected;
			return true;
		case LobbyMemberStatus.Kicked:
			outLobbyState = Lobby.MemberLobbyStateMask.MLS_Kicked;
			return true;
		default:
			outLobbyState = (Lobby.MemberLobbyStateMask)0;
			return false;
		}
	}

	public static void AddSearchAttribute(this LobbySearch lobbySearchHandle, string key, AttributeDataValue value, ComparisonOp comparison = ComparisonOp.Equal)
	{
		AttributeData value2 = new AttributeData
		{
			Key = key,
			Value = value
		};
		LobbySearchSetParameterOptions options = new LobbySearchSetParameterOptions
		{
			Parameter = value2,
			ComparisonOp = comparison
		};
		lobbySearchHandle.SetParameter(ref options);
	}

	public static void RemoveSearchAttribute(this LobbySearch lobbySearchHandle, string key, ComparisonOp comparison = ComparisonOp.Equal)
	{
		LobbySearchRemoveParameterOptions options = new LobbySearchRemoveParameterOptions
		{
			Key = key,
			ComparisonOp = comparison
		};
		lobbySearchHandle.RemoveParameter(ref options);
	}

	public static LobbyDetailsInfo GetDetailsInfo(this LobbyDetails lobbyDetails)
	{
		LobbyDetailsCopyInfoOptions options = default(LobbyDetailsCopyInfoOptions);
		d.AssertFormat(lobbyDetails.CopyInfo(ref options, out var outLobbyDetailsInfo) == Result.Success && outLobbyDetailsInfo.HasValue, "Failed to Copy lobby details from lobby details!");
		return outLobbyDetailsInfo.Value;
	}
}
