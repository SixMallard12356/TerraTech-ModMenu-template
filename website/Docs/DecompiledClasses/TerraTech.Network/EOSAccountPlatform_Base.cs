#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Epic.OnlineServices;
using Epic.OnlineServices.Connect;
using PlayEveryWare.EpicOnlineServices;
using UnityEngine;

namespace TerraTech.Network;

public abstract class EOSAccountPlatform_Base
{
	private struct PlatformInfo
	{
		public ExternalAccountType PlatformType;

		public TTNetworkID PlayerID;
	}

	private struct RequestUserInfoCallbackContext
	{
		public TTNetworkID PlayerID;
	}

	public delegate void AvatarImageLoadedCallback(TTNetworkID platformID, int imageID, int width, int height);

	public delegate void MirrorLobbyCreateCallback(bool createSuccess, TTNetworkID eosLobbyID, TTNetworkID platformLobbyID);

	private Dictionary<TTNetworkID, PlatformInfo> m_PlayerPlatformMapping = new Dictionary<TTNetworkID, PlatformInfo>();

	private Dictionary<TTNetworkID, TTNetworkID> m_PlayerPlatformReverseLookup = new Dictionary<TTNetworkID, TTNetworkID>();

	private Dictionary<TTNetworkID, Event<TTNetworkID>> m_RequestedPlayerPlatformMapping = new Dictionary<TTNetworkID, Event<TTNetworkID>>();

	private static ProductUserId[] s_productUserIds = new ProductUserId[1];

	public AvatarImageLoadedCallback AvatarImageLoadedEvent;

	public abstract ExternalAccountType PlatformType { get; }

	public abstract bool SupportsAvatars { get; }

	public abstract bool MirrorEOSLobbyToPlatform { get; }

	public TTNetworkID CurrentMirrorLobbyPlatformID { get; set; }

	public bool TryGetPlatformID(TTNetworkID playerID, out TTNetworkID platformID, bool requestIfMissing = true, bool errorOnFail = true)
	{
		PlatformInfo value;
		bool num = m_PlayerPlatformMapping.TryGetValue(playerID, out value);
		if (num)
		{
			platformID = value.PlayerID;
			return num;
		}
		platformID = TTNetworkID.Invalid;
		if (requestIfMissing)
		{
			RequestUserInformation(playerID, null);
		}
		return num;
	}

	public bool TryGetPlatformType(TTNetworkID playerID, out ExternalAccountType platformType, bool requestIfMissing = true, bool errorOnFail = true)
	{
		PlatformInfo value;
		bool num = m_PlayerPlatformMapping.TryGetValue(playerID, out value);
		if (num)
		{
			platformType = value.PlatformType;
			return num;
		}
		platformType = (ExternalAccountType)(-1);
		if (requestIfMissing)
		{
			RequestUserInformation(playerID, null);
		}
		return num;
	}

	public bool TryGetEOSID(TTNetworkID platformID, out TTNetworkID eosID)
	{
		bool num = m_PlayerPlatformReverseLookup.TryGetValue(platformID, out eosID);
		if (!num)
		{
			eosID = TTNetworkID.Invalid;
		}
		return num;
	}

	public bool HasUserInformation(TTNetworkID playerID)
	{
		return m_PlayerPlatformMapping.ContainsKey(playerID);
	}

	public void RequestUserInformation(TTNetworkID playerID, Action<TTNetworkID> onUserInfoRetrieved)
	{
		if (!m_PlayerPlatformMapping.ContainsKey(playerID))
		{
			Event<TTNetworkID> value;
			bool num = m_RequestedPlayerPlatformMapping.TryGetValue(playerID, out value);
			if (onUserInfoRetrieved != null)
			{
				value.Subscribe(onUserInfoRetrieved);
			}
			m_RequestedPlayerPlatformMapping[playerID] = value;
			if (!num)
			{
				s_productUserIds[0] = playerID.ToEOSProductUserID();
				QueryProductUserIdMappingsOptions options = new QueryProductUserIdMappingsOptions
				{
					LocalUserId = EOSManager.Instance.GetProductUserId(),
					ProductUserIds = s_productUserIds
				};
				RequestUserInfoCallbackContext requestUserInfoCallbackContext = new RequestUserInfoCallbackContext
				{
					PlayerID = playerID
				};
				EOSManager.Instance.GetEOSConnectInterface().QueryProductUserIdMappings(ref options, requestUserInfoCallbackContext, OnQueryProductUserIdMappingCallback);
			}
		}
	}

	private void OnQueryProductUserIdMappingCallback(ref QueryProductUserIdMappingsCallbackInfo data)
	{
		TTNetworkID playerID = ((RequestUserInfoCallbackContext)data.ClientData).PlayerID;
		d.AssertFormat(m_RequestedPlayerPlatformMapping.TryGetValue(playerID, out var value), "Failed to find completion event param in OnQueryProductUserIdMappingCallback for playerId {0}", playerID);
		m_RequestedPlayerPlatformMapping.Remove(playerID);
		if (data.ResultCode != Result.Success)
		{
			d.LogErrorFormat("Failed to RequestUserInformation for player {0}, error code {1}", playerID, data.ResultCode);
			return;
		}
		PlatformInfo value2 = default(PlatformInfo);
		GetProductUserExternalAccountCountOptions options = new GetProductUserExternalAccountCountOptions
		{
			TargetUserId = playerID.ToEOSProductUserID()
		};
		uint productUserExternalAccountCount = EOSManager.Instance.GetEOSConnectInterface().GetProductUserExternalAccountCount(ref options);
		d.AssertFormat(productUserExternalAccountCount <= 1, "Multiple({0}) platform mappings exist for the requested account {1}! We're not handling this yet..", productUserExternalAccountCount, playerID.ToEOSProductUserID());
		for (uint num = 0u; num < productUserExternalAccountCount; num++)
		{
			CopyProductUserExternalAccountByIndexOptions options2 = new CopyProductUserExternalAccountByIndexOptions
			{
				TargetUserId = playerID.ToEOSProductUserID(),
				ExternalAccountInfoIndex = num
			};
			ExternalAccountInfo? outExternalAccountInfo;
			Result result = EOSManager.Instance.GetEOSConnectInterface().CopyProductUserExternalAccountByIndex(ref options2, out outExternalAccountInfo);
			d.AssertFormat(result == Result.Success && outExternalAccountInfo.HasValue, "Failed to CopyProductUserExternalAccountByIndex for player {0} index {1}, error code {2}", playerID, num, result);
			if (result == Result.Success)
			{
				d.Log($"\n\t\t{playerID.ToEOSProductUserID()} > \t{outExternalAccountInfo.Value.AccountId} ({outExternalAccountInfo.Value.AccountIdType})");
				if (value2.PlayerID.IsNull())
				{
					value2.PlatformType = outExternalAccountInfo.Value.AccountIdType;
					value2.PlayerID = new TTNetworkID(outExternalAccountInfo.Value.AccountId);
					m_PlayerPlatformReverseLookup.Add(value2.PlayerID, playerID);
				}
			}
		}
		m_PlayerPlatformMapping[playerID] = value2;
		value.Send(playerID);
	}

	public virtual void Shutdown()
	{
	}

	public virtual bool TryGetUserName(TTNetworkID eosPlayerID, out string userName)
	{
		CopyProductUserInfoOptions options = new CopyProductUserInfoOptions
		{
			TargetUserId = eosPlayerID.ToEOSProductUserID()
		};
		ExternalAccountInfo? outExternalAccountInfo;
		bool flag = EOSManager.Instance.GetEOSConnectInterface().CopyProductUserInfo(ref options, out outExternalAccountInfo) == Result.Success && outExternalAccountInfo.HasValue;
		userName = (flag ? outExternalAccountInfo.Value.DisplayName : null);
		return flag;
	}

	public virtual void RequestUserNameAsync(TTNetworkID eosPlayerID, Action<TTNetworkID, string> onRequestUserNameComplete)
	{
		RequestUserInformation(eosPlayerID, delegate(TTNetworkID requestedID)
		{
			d.AssertFormat(TryGetUserName(requestedID, out var userName), "Failed to Get username for user {0}", requestedID);
			onRequestUserNameComplete?.Invoke(requestedID, userName);
		});
	}

	public abstract int GetLargeFriendAvatarImageID(TTNetworkID platformID);

	public abstract Sprite GetSpriteFromImageID(int imageID, int imageWidthHeight);

	public abstract bool IsFriend(TTNetworkID platformID);

	public abstract void OpenFriendInviteScreen(TTNetworkID eosLobbyIDToInviteTo);

	public virtual void CreateMirrorLobby(TTNetworkID eosLobbyID, Lobby.LobbyVisibility visibility, int maxLobbyMembers, MirrorLobbyCreateCallback creationCallback)
	{
	}

	public virtual void JoinMirrorLobby(TTNetworkID eosLobbyID, TTNetworkID platformLobbyID)
	{
	}

	public virtual bool LeaveMirrorLobby(TTNetworkID eosLobbyID, TTNetworkID platformLobbyID, bool persistLobby)
	{
		return false;
	}

	public virtual void DestroyMirrorLobby(TTNetworkID eosLobbyID, TTNetworkID platformLobbyID)
	{
	}
}
