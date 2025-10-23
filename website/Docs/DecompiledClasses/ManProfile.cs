#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEngine;

public class ManProfile : Singleton.Manager<ManProfile>
{
	public class Profile
	{
		public string m_Name;

		public string m_SaveName = string.Empty;

		public string m_LastUsedSaveName = string.Empty;

		public ManGameMode.GameType m_LastUsedSaveType;

		public WorldGenVersionData m_LastUsedSave_WorldGenVersionData;

		public int m_PlayerId;

		public int m_CumulativePlayTimeMinutes;

		public LocalisationEnums.Languages m_CurrentLanguage;

		public GameplaySettings m_GameplaySettings = new GameplaySettings();

		public GamepadSettings m_GamepadSettings = new GamepadSettings();

		public GraphicsSettings m_GraphicsSettings = new GraphicsSettings();

		public SoundSettings m_SoundSettings = new SoundSettings();

		public ControllerSettings m_ControllerSettings = new ControllerSettings();

		public CameraSettings m_CameraSettings = new CameraSettings();

		public TwitterAPI.TwitterSettings m_TwitterSettings = new TwitterAPI.TwitterSettings();

		public TutorialSkipSettings m_TutorialSkipSettings = new TutorialSkipSettings();

		public DateTime? m_CreationDate;

		public string m_LastSavedVersion;

		public HashSet<string> m_PlayerNotifications = new HashSet<string>();

		[Obsolete("The old way of storing favourites, now using m_SnapshotMetaData[n].IsFavourite", false)]
		public List<string> m_FavouritedSnapshotUIDs = new List<string>();

		public List<Snapshot.MetaData> m_SnapshotMetaData = new List<Snapshot.MetaData>();

		public ControlSchemeLibrary m_ControlSchemeLibrary = new ControlSchemeLibrary();

		[JsonIgnore]
		public Dictionary<EnumString, int> m_SeenHints = new Dictionary<EnumString, int>();

		[JsonProperty]
		private EnumString[] m_SeenHintKeys;

		[JsonProperty]
		private int[] m_SeenHintValues;

		private Dictionary<int, int> m_RuntimeSeenHints = new Dictionary<int, int>();

		public List<ItemTypeInfo> m_DiscoveredItemRecipes = new List<ItemTypeInfo>();

		public DateTime m_LastCumulativePlaytimeUpdateTime = DateTime.Now;

		[JsonConstructor]
		public Profile()
		{
		}

		public Profile(string name, string saveName)
		{
			m_Name = name;
			m_SaveName = saveName;
			m_CurrentLanguage = Singleton.Manager<Localisation>.inst.CurrentLanguage;
			m_ControlSchemeLibrary.AddDefaultSchemes(this);
			m_GraphicsSettings.m_QualityLevel = QualitySettings.GetQualityLevel();
			m_GraphicsSettings.m_HBAO = QualitySettingsExtended.DefaultHBAOEnabled;
			m_GraphicsSettings.m_DOF = QualitySettingsExtended.DefaultDOFEnabled;
			m_GraphicsSettings.m_HDR = QualitySettingsExtended.DefaultHDREnabled;
			m_GraphicsSettings.m_AA = QualitySettingsExtended.DefaultAntialiasingEnabled;
			m_GraphicsSettings.m_DrawDist = QualitySettingsExtended.ViewDistanceRange.InverseLerp(QualitySettingsExtended.DefaultDrawDistance);
			m_GraphicsSettings.m_DetailDist = QualitySettingsExtended.DetailDistanceRange.InverseLerp(QualitySettingsExtended.DefaultDetailDistance);
			m_GraphicsSettings.m_ShadowDist = QualitySettingsExtended.ShadowDistanceRange.InverseLerp(QualitySettingsExtended.DefaultShadowDistance);
			m_GraphicsSettings.m_VSyncEnabled = QualitySettings.vSyncCount > 0;
			m_GraphicsSettings.m_MaxFramerate = 60;
		}

		public Profile(string name)
			: this(name, GetValidFreeFileName(name))
		{
		}

		public void DiscoverRecipe(ItemTypeInfo item, bool saveImmediate = true)
		{
			if (!(item == null) && !m_DiscoveredItemRecipes.Contains(item))
			{
				m_DiscoveredItemRecipes.Add(item);
				m_DiscoveredItemRecipes = m_DiscoveredItemRecipes.Distinct().ToList();
				Singleton.Manager<ManProfile>.inst.OnRecipeDiscovered.Send(item);
				if (saveImmediate)
				{
					Singleton.Manager<ManProfile>.inst.Save();
				}
			}
		}

		public bool HasSeenHint(GameHints.HintID hintId)
		{
			return m_RuntimeSeenHints.ContainsKey((int)hintId);
		}

		public void SetHintSeen(GameHints.HintID hintId)
		{
			int value = 1;
			EnumString key = new EnumString(typeof(GameHints.HintID), (int)hintId);
			m_SeenHints[key] = value;
			m_RuntimeSeenHints[(int)hintId] = value;
		}

		public void ResetHintsSeen()
		{
			m_SeenHints.Clear();
			m_RuntimeSeenHints.Clear();
		}

		public void GeneratePlayerID()
		{
			m_PlayerId = (int)DateTime.Now.Ticks;
		}

		public void UpdateCumulativePlaytime()
		{
			DateTime now = DateTime.Now;
			m_CumulativePlayTimeMinutes += (now - m_LastCumulativePlaytimeUpdateTime).Minutes;
			m_LastCumulativePlaytimeUpdateTime = now;
		}

		[OnSerializing]
		private void OnSerialising(StreamingContext context)
		{
			int count = m_SeenHints.Count;
			m_SeenHintKeys = new EnumString[count];
			m_SeenHintValues = new int[count];
			int num = 0;
			foreach (KeyValuePair<EnumString, int> seenHint in m_SeenHints)
			{
				m_SeenHintKeys[num] = seenHint.Key;
				m_SeenHintValues[num] = seenHint.Value;
				num++;
			}
		}

		[OnSerialized]
		private void OnSerialised(StreamingContext context)
		{
			m_SeenHintKeys = null;
			m_SeenHintValues = null;
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (m_GameplaySettings == null)
			{
				m_GameplaySettings = new GameplaySettings();
			}
			if (m_GamepadSettings == null)
			{
				m_GamepadSettings = new GamepadSettings();
			}
			bool flag = true;
			if (!m_LastSavedVersion.NullOrEmpty())
			{
				flag = false;
			}
			if (flag)
			{
				m_LastSavedVersion = SKU.ChangelistVersion;
				m_SoundSettings.m_MasterVolume = 1f;
			}
			m_ControlSchemeLibrary.AddDefaultSchemes(this);
			m_SeenHints.Clear();
			m_RuntimeSeenHints.Clear();
			if (m_SeenHintKeys != null)
			{
				for (int i = 0; i < m_SeenHintKeys.Length; i++)
				{
					EnumString enumString = m_SeenHintKeys[i];
					int value = m_SeenHintValues[i];
					m_SeenHints.Add(enumString, value);
					m_RuntimeSeenHints.Add(enumString.GetValue(), value);
				}
			}
			m_SeenHintKeys = null;
			m_SeenHintValues = null;
		}
	}

	public class GameplaySettings
	{
		public bool m_PauseOnFocusLost = true;

		public bool m_ShowGameplayHints = true;

		public bool m_DisplayExpandedInventoryBlockInfo = true;

		public bool m_UseForceGizmosInBuildBeam;
	}

	public class GamepadSettings
	{
		public float m_GamepadCursorSpeed = 0.6f;

		public bool m_EnableControllerVibration = true;
	}

	public class GraphicsSettings
	{
		public int m_QualityLevel = -1;

		public float m_DrawDist;

		public float m_DetailDist;

		public float m_ShadowDist;

		public bool m_HBAO;

		public bool m_DOF;

		public bool m_HDR;

		public bool m_AA;

		public bool m_VSyncEnabled;

		public int m_MaxFramerate = int.MinValue;

		public float m_Gamma = 1f;

		public bool m_DamageFeedbackEffect = true;

		public float m_ScreenSize = 1f;

		public int m_MousePointerSize;

		public bool m_ConfineMouseToScreen;
	}

	public class SoundSettings
	{
		public float m_MasterVolume = 1f;

		public float m_MusicVolume = 1f;

		public float m_SFXVolume = 1f;
	}

	public class ControllerSettings
	{
		public bool m_DisableControllers;

		[Obsolete("Use ControlScheme.ReverseSteering instead")]
		public bool m_ReverseInverseSteering;

		public bool m_ConsoleStyleJoypad;

		public string m_ControllerMapping;
	}

	public class CameraSettings
	{
		public float m_Horizontal = 1f;

		public float m_Vertical = -1f;

		public float m_SpringLookup = TankCamera.inst.m_InitialCurveValue;

		public float m_InterpolationSpeed = 1f;

		public bool m_InvertY;
	}

	public class TutorialSkipSettings
	{
		public bool m_CompletedTutorial;

		public bool m_WantsSkip;
	}

	public class GlobalSave
	{
		public int m_LastUserID;

		public List<Profile> m_Users = new List<Profile>();
	}

	[SerializeField]
	private string[] m_IgnoredProfileFolders;

	public Event<ItemTypeInfo> OnRecipeDiscovered;

	public Event<Profile> OnUserAdded;

	public Event<Profile> OnUserChanged;

	public Event<Profile> OnUserRemoved;

	public Event<Profile> OnBeforeProfileSave;

	public Event<Profile> OnProfileSaved;

	private Profile m_CurrentUser;

	private GlobalSave m_ProfileSave;

	public static readonly string kInvalidSaveMessgae_0_7_2 = "InvalidSave_0.7.2";

	public static readonly string kRandDForum = "RandDForumMessageShown";

	public static readonly string kCreativeModeExplanation = "CreativeModeExplanationShown";

	public static readonly string kSchemeUINotification = "SchemeNotification";

	public GameplaySettings m_DefaultGameplaySettings = new GameplaySettings();

	public GamepadSettings m_DefaultGamepadSettings = new GamepadSettings();

	public GraphicsSettings m_DefaultGraphicsSettings = new GraphicsSettings();

	public SoundSettings m_DefaultSoundSettings = new SoundSettings();

	public ControllerSettings m_DefaultControllerSettings = new ControllerSettings();

	private CameraSettings m_DefaultCamSettings;

	private const string kConsoleProfileDataFileName = "ProfileData";

	private const string kConsoleProfileDataPath = "PROFILE";

	public CameraSettings m_DefaultCameraSettings
	{
		get
		{
			if (m_DefaultCamSettings == null)
			{
				m_DefaultCamSettings = new CameraSettings();
			}
			return m_DefaultCamSettings;
		}
	}

	public Profile GetCurrentUser()
	{
		return m_CurrentUser;
	}

	public bool SetCurrentUser(int userIndex)
	{
		d.Log("[ManProfile] SetCurrentUser(" + userIndex + ")");
		Profile profile = m_ProfileSave.m_Users[userIndex];
		if (m_CurrentUser != profile)
		{
			m_CurrentUser = profile;
			m_ProfileSave.m_LastUserID = userIndex;
			profile.m_ControlSchemeLibrary.AddDefaultSchemes(profile);
			profile.m_LastCumulativePlaytimeUpdateTime = DateTime.Now;
			OnUserChanged.Send(profile);
			return Save();
		}
		return true;
	}

	public int GetUserIndexFromSaveName(string saveName)
	{
		for (int i = 0; i < m_ProfileSave.m_Users.Count; i++)
		{
			if (m_ProfileSave.m_Users[i].m_SaveName == saveName)
			{
				return i;
			}
		}
		return -1;
	}

	public bool AddUser(Profile profile, bool setCurrent)
	{
		bool flag = true;
		profile.m_CreationDate = DateTime.Now;
		if (m_ProfileSave != null)
		{
			m_ProfileSave.m_Users.Add(profile);
			if (setCurrent)
			{
				flag = SetCurrentUser(m_ProfileSave.m_Users.Count - 1);
			}
			if (!flag)
			{
				return flag;
			}
			OnUserAdded.Send(profile);
		}
		else
		{
			d.LogError("m_ProfileSave == null\n");
			flag = false;
		}
		if (!setCurrent)
		{
			return Save();
		}
		return flag;
	}

	public List<Profile> GetProfiles()
	{
		List<Profile> result = new List<Profile>();
		if (m_ProfileSave != null)
		{
			result = m_ProfileSave.m_Users;
		}
		return result;
	}

	public bool Save()
	{
		if (m_CurrentUser != null)
		{
			m_CurrentUser.m_LastSavedVersion = SKU.ChangelistVersion;
			if (m_CurrentUser.m_PlayerId == 0)
			{
				m_CurrentUser.GeneratePlayerID();
			}
			OnBeforeProfileSave.Send(m_CurrentUser);
		}
		string profileSaveDir = ManSaveGame.GetProfileSaveDir();
		string text = "";
		bool flag;
		if (profileSaveDir == null || profileSaveDir.Length == 0)
		{
			flag = false;
		}
		else
		{
			text = profileSaveDir + "UserData.sav";
			flag = ManSaveGame.SaveObject(m_ProfileSave, text);
		}
		if (flag)
		{
			OnProfileSaved.Send(m_CurrentUser);
		}
		else
		{
			d.LogError("Failed to save user profile to: " + text);
			Singleton.DoOnceAfterStart(delegate
			{
				LogProfileSaveError();
			});
		}
		return flag;
	}

	public void DoDesktopFirstLoadStep()
	{
		d.Log("[ManProfile]: Attempting First Load Step");
		try
		{
			Load();
		}
		catch (Exception)
		{
		}
		if (m_ProfileSave == null)
		{
			m_ProfileSave = new GlobalSave();
		}
		bool flag = false;
		for (int i = 0; i < m_ProfileSave.m_Users.Count; i++)
		{
			Profile profile = m_ProfileSave.m_Users[i];
			if (profile.m_SaveName.NullOrEmpty())
			{
				profile.m_SaveName = GetValidName(profile.m_Name);
				if (profile.m_SaveName != profile.m_Name)
				{
					profile.m_SaveName = GetValidFreeFileName(profile.m_Name);
					if (ManSaveGame.RenameUserFolder(profile))
					{
					}
				}
			}
			else if (!profile.m_SaveName.Equals(profile.m_Name))
			{
				profile.m_Name = profile.m_SaveName;
				flag = true;
			}
			if (!profile.m_CreationDate.HasValue)
			{
				profile.m_CreationDate = DateTime.Now;
				flag = true;
			}
			if (profile.m_SnapshotMetaData != null && profile.m_SnapshotMetaData.Count != 0)
			{
				continue;
			}
			profile.m_SnapshotMetaData = new List<Snapshot.MetaData>();
			if (profile.m_FavouritedSnapshotUIDs != null && profile.m_FavouritedSnapshotUIDs.Count > 0)
			{
				for (int j = 0; j < profile.m_FavouritedSnapshotUIDs.Count; j++)
				{
					Snapshot.MetaData item = new Snapshot.MetaData(profile.m_FavouritedSnapshotUIDs[j]);
					item.IsFavourite = true;
					profile.m_SnapshotMetaData.Add(item);
				}
				profile.m_FavouritedSnapshotUIDs = null;
			}
		}
		string text = ManSaveGame.GetSaveDataFolder() + "/Saves/";
		string[] directories = Directory.GetDirectories(text);
		int length = text.Length;
		string[] array = directories;
		for (int k = 0; k < array.Length; k++)
		{
			string saveName = array[k].Substring(length);
			if (IsUniqueProfileName(saveName) && !IsIgnoredFolder(saveName))
			{
				Profile profile2 = new Profile(saveName, saveName);
				Singleton.Manager<ManProfile>.inst.AddUser(profile2, setCurrent: false);
				flag = true;
			}
		}
		if (flag)
		{
			Save();
		}
		int num = Mathf.Min(m_ProfileSave.m_LastUserID, m_ProfileSave.m_Users.Count - 1);
		if (num >= 0)
		{
			m_CurrentUser = m_ProfileSave.m_Users[num];
			m_CurrentUser.m_ControlSchemeLibrary.AddDefaultSchemes(m_CurrentUser);
		}
	}

	private void LogProfileSaveError()
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 19);
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 17);
		string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 18);
		localisedString2 = UIHyperlink.ConvertLinkToTMProLinkCode(localisedString2, localisedString3);
		localisedString = string.Format(localisedString, localisedString2);
		Singleton.Manager<ManUI>.inst.ShowErrorPopup(localisedString);
	}

	public void DeleteProfile(int id)
	{
		Profile currentUser = m_CurrentUser;
		Profile profile = m_ProfileSave.m_Users.ElementAt(id);
		Directory.Delete(ManSaveGame.GetUserGameSaveDir(profile), recursive: true);
		m_ProfileSave.m_Users.RemoveAt(id);
		if (profile == currentUser && m_ProfileSave.m_Users.Count > 0)
		{
			SetCurrentUser(0);
		}
		OnUserRemoved.Send(profile);
		Save();
	}

	public void LoadConsoles_Async(bool loadSnapshots = false, Action callback = null)
	{
	}

	public void LoadDesktop()
	{
		string path = ManSaveGame.GetProfileSaveDir() + "UserData.sav";
		ManSaveGame.LoadObject(ref m_ProfileSave, path, assertOnFail: false);
	}

	private void Load()
	{
		LoadDesktop();
	}

	private static string GetValidName(string fileName)
	{
		if (SKU.IsNetEase)
		{
			string str = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
			fileName = new Regex($"[{Regex.Escape(str)}]").Replace(fileName, "").Trim();
		}
		else
		{
			fileName = new Regex("[^a-zA-Z0-9 ]").Replace(fileName, "").Trim();
		}
		return fileName;
	}

	private static string GetValidFreeFileName(string fileName)
	{
		fileName = GetValidName(fileName);
		int i = 1;
		if (Directory.Exists(ManSaveGame.GetSaveDataFolder() + "/Saves/" + fileName))
		{
			for (fileName += "_{0}"; Directory.Exists(ManSaveGame.GetSaveDataFolder() + "/Saves/" + string.Format(fileName, i)); i++)
			{
			}
			fileName = string.Format(fileName, i);
		}
		return fileName;
	}

	public bool IsUniqueProfileName(string name)
	{
		bool result = true;
		for (int i = 0; i < m_ProfileSave.m_Users.Count; i++)
		{
			if (m_ProfileSave.m_Users[i].m_Name.EqualsNoCase(name))
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private bool IsIgnoredFolder(string name)
	{
		bool result = false;
		if (m_IgnoredProfileFolders != null)
		{
			for (int i = 0; i < m_IgnoredProfileFolders.Length; i++)
			{
				if (m_IgnoredProfileFolders[i].EqualsNoCase(name))
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	[Conditional("USE_ANALYTICS")]
	private void SendPlaytimeAnalyticsEvent()
	{
		new Dictionary<string, object>
		{
			{
				"player_id",
				Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_PlayerId
			},
			{
				"cumulative_playtime_minutes",
				Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_CumulativePlayTimeMinutes
			}
		};
	}

	private void OnProfileDataLoaded_Console(bool success, byte[] result, Action callback = null, bool loadSnapshots = true)
	{
		d.Log($"Data loaded: {success}");
		if (!success || result == null || result.Length == 0)
		{
			d.Log("[ManProfile]:OnProfileDataLoaded_Console, No saved user profile found, or bad data - Generating a new one");
		}
		else
		{
			d.Log("[ManProfile]:OnProfileDataLoaded_Console, User profile found, loading");
			if (m_CurrentUser == null)
			{
				m_CurrentUser = new Profile(GetUserName());
			}
			ManSaveGame.LoadObjectFromBytes(ref m_CurrentUser, result, assertOnFail: false);
			d.Log("[ManProfile]:OnProfileDataLoaded_Console, Profile data loaded and set for the user: " + m_CurrentUser.m_Name + ", with language: " + m_CurrentUser.m_CurrentLanguage);
			m_CurrentUser.m_ControlSchemeLibrary.AddDefaultSchemes(m_CurrentUser);
		}
		OnUserChanged.Send(m_CurrentUser);
		if (loadSnapshots)
		{
			Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(Singleton.Manager<ManSnapshots>.inst.ServiceDisk_Console.UpdateSnapshotCacheOnStartup());
		}
		callback?.Invoke();
	}

	private void OnApplicationQuit()
	{
	}

	public void RequestFirstUserActivation()
	{
		OnUserChanged.Send(m_CurrentUser);
	}

	[Conditional("BUILD_ALL_PLATFORMS")]
	[Conditional("UNITY_CONSOLES")]
	public void GenerateNewProfileConsoles()
	{
		d.Log("[ManProfile] GenerateNewProfileConsoles - Trying to save profile");
		if (m_ProfileSave == null)
		{
			m_ProfileSave = new GlobalSave();
		}
		Profile profile = new Profile(GetUserName(), "SAVE");
		profile.m_CreationDate = DateTime.Now;
		m_CurrentUser = profile;
		m_CurrentUser.m_ControlSchemeLibrary.AddDefaultSchemes(m_CurrentUser);
		d.Log("[ManProfile] GenerateNewProfileConsoles - New Profile name=" + m_CurrentUser.m_Name);
		Save();
	}

	private string GetUserName()
	{
		return "User";
	}
}
