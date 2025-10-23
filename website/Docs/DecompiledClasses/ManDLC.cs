#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.Serialization;

public class ManDLC : Singleton.Manager<ManDLC>
{
	[Serializable]
	public class DLC
	{
		[SerializeField]
		private DLCType m_DLCType;

		[SerializeField]
		private List<BlockTypes> m_RestrictedBlocks;

		[SerializeField]
		[FormerlySerializedAs("m_AssetBundleName")]
		private string m_DlcName;

		[SerializeField]
		private int m_SteamAppID;

		[SerializeField]
		public string m_EOSDlc_Audience_ID;

		[SerializeField]
		private string m_NetEaseContentID;

		[SerializeField]
		private string m_XboxContentID;

		[SerializeField]
		private int m_NintendoSwitchContentID;

		[SerializeField]
		private List<string> m_PS4EntitlementIDs = new List<string>();

		[SerializeField]
		private List<SkinLock> m_CustomSkinIDs;

		[SerializeField]
		private ExclusiveContentTypes m_ExclusiveContent;

		public bool Active { get; set; }

		public DLCType DLCType => m_DLCType;

		public ExclusiveContentTypes ExclusiveContentType => m_ExclusiveContent;

		public List<BlockTypes> RestrictedBlocks => m_RestrictedBlocks;

		public string DlcName => m_DlcName;

		public int SteamAppID => m_SteamAppID;

		public string NetEaseContentID => m_NetEaseContentID;

		public string EOSDlc_Audience_ID => m_EOSDlc_Audience_ID;

		public string XboxContentID
		{
			get
			{
				return m_XboxContentID;
			}
			set
			{
				m_XboxContentID = value;
			}
		}

		public List<string> PS4EntitlementIDs => m_PS4EntitlementIDs;

		public int SwitchContentID => m_NintendoSwitchContentID;

		public List<SkinLock> GetSkinID()
		{
			return m_CustomSkinIDs;
		}
	}

	[Serializable]
	public class SkinLock
	{
		public int SkinID;

		public FactionSubTypes SkinCorp;
	}

	public enum DLCType
	{
		RandD,
		YearOnePayload,
		CustomSkin
	}

	[SerializeField]
	public DLCTable m_DLCTable;

	public EventNoParams OnDLCChanged;

	private List<SkinLock> m_LockedDLCSkins;

	private HashSet<BlockTypes> m_RandDRestrictedBlocks;

	private Dictionary<int, bool> m_HasAnyDLCOfType = new Dictionary<int, bool>();

	private static readonly HashSet<ManGameMode.GameType> s_ModesWithRnDBlocks = new HashSet<ManGameMode.GameType>
	{
		ManGameMode.GameType.RaD,
		ManGameMode.GameType.Creative,
		ManGameMode.GameType.Misc
	};

	public bool HasDLCEntitlement(DLC dlc)
	{
		bool flag = false;
		if ((bool)Singleton.Manager<DebugUtil>.inst && Singleton.Manager<DebugUtil>.inst.m_Settings.m_OverridePlatformDLCs)
		{
			return Singleton.Manager<DebugUtil>.inst.DebugHasDLC(dlc, m_DLCTable);
		}
		if (SKU.IsSteam)
		{
			return Singleton.Manager<ManSteamworks>.inst.Inited && SteamApps.BIsDlcInstalled(new AppId_t((uint)dlc.SteamAppID));
		}
		if (SKU.IsEpicGS)
		{
			return Singleton.Manager<ManEOS>.inst.IsDLCInfoLoaded && Singleton.Manager<ManEOS>.inst.HasDLCEntitlement(dlc.EOSDlc_Audience_ID);
		}
		if (SKU.IsNetEase)
		{
			return ManNetEase.HasDlcEntitlement(dlc.NetEaseContentID);
		}
		d.LogWarning("DLC Entitlement check not implemented for this platform in ManDLC::HasDLCEntitlement!");
		return false;
	}

	public bool SupportsStore()
	{
		if (SKU.IsSteam)
		{
			return true;
		}
		if (SKU.IsEpicGS)
		{
			return true;
		}
		if (SKU.IsNetEase)
		{
			return false;
		}
		_ = SKU.IsTeyon;
		return false;
	}

	public void OpenStoreToDLCPage()
	{
		if (SKU.IsSteam && Singleton.Manager<ManSteamworks>.inst.Inited)
		{
			Singleton.Manager<ManSteamworks>.inst.OpenOverlayStore(Singleton.Manager<ManSteamworks>.inst.AppID);
		}
		else if (SKU.IsEpicGS)
		{
			Singleton.Manager<ManEOS>.inst.OpenDLCShop();
		}
	}

	public void OpenStoreToDLCPageWithNotification()
	{
		if (SupportsStore())
		{
			UIScreenNotifications uIScreenNotifications = (UIScreenNotifications)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen);
			string notification = ((!SKU.SwitchUI) ? string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 101)) : string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 102)));
			uIScreenNotifications.Set(notification, delegate
			{
				Singleton.Manager<ManUI>.inst.RemovePopup();
				Singleton.Manager<ManDLC>.inst.OpenStoreToDLCPage();
			}, delegate
			{
				Singleton.Manager<ManUI>.inst.RemovePopup();
			}, Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29), Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30));
			uIScreenNotifications.SetUseNewInputHandler(useNewInputHandler: true);
			Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
		}
		else
		{
			d.LogWarning("ManDLC.OpenStoreToDLCPageWithNotification called on platform without store support");
		}
	}

	public bool HasAnyDLCOfType(DLCType dlcType)
	{
		if (m_HasAnyDLCOfType.TryGetValue((int)dlcType, out var value))
		{
			return value;
		}
		value = false;
		for (int i = 0; i < m_DLCTable.DLCPacks.Count; i++)
		{
			if (m_DLCTable.DLCPacks[i].DLCType == dlcType && m_DLCTable.DLCPacks[i].Active)
			{
				value = true;
				break;
			}
		}
		m_HasAnyDLCOfType[(int)dlcType] = value;
		return value;
	}

	public bool IsBlockDLC(BlockTypes block, out DLCType dlcType)
	{
		bool result = false;
		dlcType = DLCType.RandD;
		for (int i = 0; i < m_DLCTable.DLCPacks.Count; i++)
		{
			if (m_DLCTable.DLCPacks[i].RestrictedBlocks.Contains(block))
			{
				result = true;
				dlcType = m_DLCTable.DLCPacks[i].DLCType;
				break;
			}
		}
		return result;
	}

	public bool IsBlockRandDRestricted(BlockTypes block)
	{
		return m_RandDRestrictedBlocks.Contains(block);
	}

	public bool AreRandDBlocksAllowedInGameType(ManGameMode.GameType gameType)
	{
		return s_ModesWithRnDBlocks.Contains(gameType);
	}

	public bool HasLimitedAccessDLC(ExclusiveContentTypes contentType)
	{
		foreach (DLC dLCPack in m_DLCTable.DLCPacks)
		{
			if (dLCPack.ExclusiveContentType == contentType && HasDLCEntitlement(dLCPack))
			{
				return true;
			}
		}
		return false;
	}

	public static bool HasLimitedAccessContent(ExclusiveContentTypes contentType)
	{
		if (contentType == ExclusiveContentTypes.None)
		{
			return true;
		}
		if (Singleton.Manager<ManDLC>.inst != null)
		{
			return Singleton.Manager<ManDLC>.inst.HasLimitedAccessDLC(contentType);
		}
		return false;
	}

	public bool IsSkinDLC(int skinID, FactionSubTypes corp)
	{
		foreach (DLC dLCPack in m_DLCTable.DLCPacks)
		{
			foreach (SkinLock item in dLCPack.GetSkinID())
			{
				if (item.SkinID == skinID && item.SkinCorp == corp)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool IsSkinLocked(int skinID, FactionSubTypes corp)
	{
		foreach (SkinLock lockedDLCSkin in m_LockedDLCSkins)
		{
			if (lockedDLCSkin.SkinID == skinID && lockedDLCSkin.SkinCorp == corp)
			{
				return true;
			}
		}
		return false;
	}

	public void PlatformDLCChanged()
	{
		RebuildDLCAvailability();
		OnDLCChanged.Send();
	}

	private void Awake()
	{
		m_RandDRestrictedBlocks = new HashSet<BlockTypes>();
		for (int i = 0; i < m_DLCTable.DLCPacks.Count; i++)
		{
			DLC dLC = m_DLCTable.DLCPacks[i];
			if (dLC.DLCType == DLCType.RandD)
			{
				for (int j = 0; j < dLC.RestrictedBlocks.Count; j++)
				{
					m_RandDRestrictedBlocks.Add(dLC.RestrictedBlocks[j]);
				}
			}
		}
		RebuildDLCAvailability();
	}

	private void RebuildDLCAvailability()
	{
		m_HasAnyDLCOfType.Clear();
		for (int i = 0; i < m_DLCTable.DLCPacks.Count; i++)
		{
			DLC dLC = m_DLCTable.DLCPacks[i];
			dLC.Active = HasDLCEntitlement(dLC);
		}
		m_LockedDLCSkins = new List<SkinLock>();
		foreach (DLC dLCPack in m_DLCTable.DLCPacks)
		{
			switch (dLCPack.DLCType)
			{
			case DLCType.CustomSkin:
				if (!HasDLCEntitlement(dLCPack))
				{
					m_LockedDLCSkins.AddRange(dLCPack.GetSkinID());
				}
				break;
			}
		}
	}

	private void Start()
	{
		PlatformAssetBundles platformAssetBundles = Globals.inst.m_PlatformAssetBundles;
		if (1 == 0)
		{
			return;
		}
		foreach (string requiredAssetBundle in platformAssetBundles.BuildTypes[(int)SKU.CurrentBuildType].RequiredAssetBundles)
		{
			d.LogFormat("Loading asset bundle {0}", Application.streamingAssetsPath + "/" + requiredAssetBundle);
			AssetBundle assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + requiredAssetBundle);
			if ((bool)assetBundle)
			{
				CorporationSkinInfo[] array = assetBundle.LoadAllAssets<CorporationSkinInfo>();
				d.LogFormat("\tFound {0} skinInfos", array.Length);
				CorporationSkinInfo[] array2 = array;
				foreach (CorporationSkinInfo corporationSkinInfo in array2)
				{
					d.LogFormat("\tAdding skinInfo {0}", corporationSkinInfo.name);
					Singleton.Manager<ManCustomSkins>.inst.AddSkinToCorp(corporationSkinInfo);
				}
				GameObject[] array3 = assetBundle.LoadAllAssets<GameObject>();
				d.LogFormat("\tFound {0} game objects", array3.Length);
				GameObject[] array4 = array3;
				foreach (GameObject gameObject in array4)
				{
					d.LogFormat("\tTrying to unpack object {0} as a block", gameObject.name);
					TryUnpackAssetAsBlock(gameObject);
				}
				d.LogFormat("Finished loading asset bundle {0}", Application.streamingAssetsPath + "/" + requiredAssetBundle);
				assetBundle.Unload(unloadAllLoadedObjects: false);
			}
		}
	}

	private void TryUnpackAssetAsBlock(UnityEngine.Object asset)
	{
		GameObject gameObject = asset as GameObject;
		if ((bool)gameObject)
		{
			Singleton.Manager<ManSpawn>.inst.AddBlockToDictionary(gameObject);
		}
	}
}
