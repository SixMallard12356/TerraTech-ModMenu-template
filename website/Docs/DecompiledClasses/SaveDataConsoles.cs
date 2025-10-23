#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class SaveDataConsoles
{
	private abstract class PlatformWrapper
	{
		public abstract void SaveData(string fileName, string directory, byte[] data, int dataLength, int maxExpectedCompressedSize, Action<bool, byte[]> onSaveDataCallback);

		public abstract void LoadData(string fileName, string directory, Action<bool, byte[]> onDataLoadedCallback);

		public abstract void DeleteData(string fileName, string directory, ManGameMode.GameType gameType, Action<bool, byte[]> onDeleteCallback);

		public abstract List<string> FindAllFilesInDirectory(string directory);

		public abstract bool DataExists(string fileName, string directory);

		public abstract bool HasPendingSaveOperations();
	}

	public class PS4_NameEncoder
	{
		private const int MAX_FILE_NAME_LENGTH = 63;

		private static string EncodeChar(char c)
		{
			string text = Convert.ToString(c, 16);
			while (text.Length < 4)
			{
				text = "0" + text;
			}
			if (text.Length == 4)
			{
				return text + "_";
			}
			return text.Substring(text.Length - 4, 4) + "_";
		}

		private static string DecodeChar(string hex)
		{
			return ((char)Convert.ToInt32(hex, 16)).ToString();
		}

		public static string EncodeFilename(string original)
		{
			string text = Regex.Replace(original, "[^0-9a-zA-Z.@-]", (Match match) => EncodeChar(match.Value[0]));
			while (text.Length > 63)
			{
				text = ((text[text.Length - 1] != '_') ? text.Substring(0, text.Length - 1) : text.Substring(0, text.Length - 5));
			}
			d.Log("PS4_NameEncoder encode '" + original + "' => '" + text + "'");
			return text;
		}

		public static string DecodeFilename(string encoded)
		{
			string text = Regex.Replace(encoded, "[0-9a-f][0-9a-f][0-9a-f][0-9a-f]_", (Match match) => DecodeChar(match.Value.Substring(0, 4)));
			d.Log("PS4_NameEncoder decode '" + encoded + "' => '" + text + "'");
			return text;
		}
	}

	private const int kMB = 1048576;

	public const int MaxProfileSaveCompressedSize = 2097152;

	public const int MaxSaveSaveCompressedSize = 3145728;

	public const int MaxGauntletSaveCompressedSize = 3145728;

	public const int MaxSnapshotSaveCompressedSize = 4194304;

	public const int MaxTextureSaveCompressedSize = 4194304;

	public const int MaxCreativeSaveCompressedSize = 3145728;

	public const int MaxCampaignSaveCompressedSize = 3145728;

	private static readonly PlatformWrapper s_Impl;

	public static int GetSaveDirectorySize(string folderName)
	{
		int num = 0;
		return folderName switch
		{
			"Snapshots" => num + 4194304, 
			"Cache" => num + 4194304, 
			"PROFILE" => num + 2097152, 
			"Gauntlet" => num + 3145728, 
			"Creative" => num + 3145728, 
			"Campaign" => num + 3145728, 
			_ => num + 4194304, 
		};
	}

	static SaveDataConsoles()
	{
	}

	public static void SaveData(string fileName, string directory, byte[] data, int dataLength, int maxExpectedCompressedSize, Action<bool, byte[]> onSaveDataCallback)
	{
		d.Log($"SaveDataConsoles.SaveData called with filename = '{fileName}' path = '{directory}'; {dataLength} bytes with maxExpectedCompressedSize = {maxExpectedCompressedSize}, with callback = {onSaveDataCallback}");
		s_Impl.SaveData(fileName, directory, data, dataLength, maxExpectedCompressedSize, onSaveDataCallback);
	}

	public static void LoadData(string fileName, string directory, Action<bool, byte[]> onDataLoadedCallback)
	{
		d.Log($"SaveDataConsoles.LoadData called with filename = '{fileName}' path = '{directory}' with callback = {onDataLoadedCallback}");
		s_Impl.LoadData(fileName, directory, onDataLoadedCallback);
	}

	public static void DeleteData(string fileName, string directory, ManGameMode.GameType gameType, Action<bool, byte[]> onDeleteCallback)
	{
		d.Log($"SaveDataConsoles.DeleteData called with filename = '{fileName}' path = '{directory}' for gameType {gameType} with callback = {onDeleteCallback}");
		s_Impl.DeleteData(fileName, directory, gameType, onDeleteCallback);
	}

	public static bool DataExists(string fileName, string directory)
	{
		return s_Impl.DataExists(fileName, directory);
	}

	public static List<string> FindAllFilesInDirectory(string directory)
	{
		return s_Impl.FindAllFilesInDirectory(directory);
	}

	public static bool HasPendingSaveOperations()
	{
		return s_Impl.HasPendingSaveOperations();
	}

	public static void ShowSavePopupNotification()
	{
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Save, 11);
		uIScreenNotifications.Set(localisedString, null, null);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
	}

	public static bool IsShowingSavePopupNotification()
	{
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		if (uIScreenNotifications != null && uIScreenNotifications.state == UIScreen.State.Show)
		{
			return true;
		}
		return false;
	}

	public static void RemoveSavePopupNotification()
	{
		if (IsShowingSavePopupNotification())
		{
			Singleton.Manager<ManUI>.inst.RemovePopup();
		}
	}
}
