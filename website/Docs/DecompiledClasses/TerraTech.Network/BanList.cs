#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TerraTech.Network;

public class BanList
{
	private readonly BannedPlayers m_BannedPlayers = new BannedPlayers();

	private bool m_DataDirty;

	private ManTimedEvents.ManagedEvent m_DeferredSaveEvent;

	private const float kSaveDelay = 5f;

	private const string kFileName = "banlist";

	private const string kConsoleBanDataPath = "BANS";

	public BannedPlayers BannedPlayers => m_BannedPlayers;

	public void SetPlayerBanned(PersistentPlayerID playerID, bool banned)
	{
		d.Assert(SKU.BansEnabled, "Trying to ban a player on a SKU that does not support bans! How did we get here?");
		if (m_BannedPlayers.Set(playerID, banned))
		{
			m_DataDirty = true;
			if (m_DeferredSaveEvent == null)
			{
				m_DeferredSaveEvent = new ManTimedEvents.ManagedEvent(OnDeferredSave);
				Singleton.ApplicationQuitEvent.Subscribe(OnApplicationQuit);
			}
			if (m_DeferredSaveEvent.IsSet)
			{
				m_DeferredSaveEvent.Reset(5f);
			}
			else
			{
				m_DeferredSaveEvent.Set(5f);
			}
		}
	}

	public bool IsPlayerBanned(PersistentPlayerID playerID)
	{
		return m_BannedPlayers.Contains(playerID);
	}

	public void Load()
	{
		if (!SKU.BansEnabled)
		{
			return;
		}
		if (m_DataDirty)
		{
			m_DeferredSaveEvent.Clear();
			Save();
		}
		string bannedPlayersStr = null;
		string saveDataFolder = ManSaveGame.GetSaveDataFolder();
		if (!saveDataFolder.NullOrEmpty())
		{
			string path = Path.Combine(saveDataFolder, "banlist.txt");
			if (File.Exists(path))
			{
				try
				{
					using FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
					using StreamReader streamReader = new StreamReader(stream);
					bannedPlayersStr = streamReader.ReadToEnd();
				}
				catch (Exception arg)
				{
					d.LogError($"Failed to read from BanList file: {arg}");
				}
			}
		}
		m_BannedPlayers.TryParse(bannedPlayersStr);
		m_DataDirty = false;
		if (!SKU.IsSteam || !SKU.UsesEOS)
		{
			return;
		}
		Singleton.Manager<ManEOS>.inst.DoAsSoonAsConnected(delegate
		{
			IEnumerable<string> steamIDstrings = from id in m_BannedPlayers
				select id.ToString() into id
				where id.Length == 17 && id.StartsWith("765")
				select id;
			Singleton.Manager<ManEOS>.inst.TryConvertSteamIDsToEOS(steamIDstrings, delegate(List<(string steamID, string eosID)> convertedIDs)
			{
				foreach (var convertedID in convertedIDs)
				{
					SetPlayerBanned(new PersistentPlayerID(convertedID.steamID), banned: false);
					SetPlayerBanned(new PersistentPlayerID(convertedID.eosID), banned: true);
				}
			});
		});
	}

	private void Save()
	{
		if (!SKU.BansEnabled || !m_DataDirty)
		{
			return;
		}
		string value = m_BannedPlayers.ToString();
		string saveDataFolder = ManSaveGame.GetSaveDataFolder();
		if (!saveDataFolder.NullOrEmpty())
		{
			string path = Path.Combine(saveDataFolder, "banlist.txt");
			try
			{
				using FileStream stream = File.Create(path);
				using StreamWriter streamWriter = new StreamWriter(stream);
				streamWriter.Write(value);
			}
			catch (Exception arg)
			{
				d.LogError($"Failed to write to BanList file: {arg}");
			}
		}
		m_DataDirty = false;
	}

	private void OnDeferredSave()
	{
		Save();
	}

	private void OnApplicationQuit()
	{
		if (m_DeferredSaveEvent.IsSet)
		{
			Save();
			m_DeferredSaveEvent.Clear();
		}
	}
}
