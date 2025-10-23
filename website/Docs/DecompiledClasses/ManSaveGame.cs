#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Steamworks;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.Networking;
using cakeslice;

public class ManSaveGame : Singleton.Manager<ManSaveGame>
{
	public enum PlayerTier
	{
		None,
		KSBacker,
		BetaBacker,
		Crew,
		Foreman,
		VP,
		CEO
	}

	public enum SaveDataJSONType
	{
		ManVisible,
		ManPlayer,
		ManTimeOfDay,
		ManWorld,
		ManPurchases,
		ManStats,
		ManFTUE,
		ManFTUENew,
		ManEncounter,
		ManInvasion,
		ManPop,
		ManLicenses,
		ManQuestLog,
		ManChallenge,
		ManTechs,
		ModeData,
		ManBlockLimiter,
		NetworkInventory,
		ManMods,
		ManMap,
		ManSpawn
	}

	public struct SaveFileSlot
	{
		public string name;

		public bool isEmptySlot;

		public long lastWriteTime;
	}

	public class SaveData
	{
		private SaveInfo m_SaveInfo;

		private State m_State;

		private static SaveData s_InvalidSaveData;

		[JsonIgnore]
		public SaveInfo SaveInfo => m_SaveInfo;

		[JsonIgnore]
		public State State => m_State;

		public static SaveData CreateNew()
		{
			SaveData obj = new SaveData
			{
				m_SaveInfo = new SaveInfo(),
				m_State = new State()
			};
			SKU.ParseChangeListVersionNumberToInt(SKU.ChangelistVersion, out var versionInt);
			obj.m_State.m_SaveVersion = versionInt;
			WorldGenVersionData currentModeLatestMapVersion = Singleton.Manager<ManGameMode>.inst.GetCurrentModeLatestMapVersion();
			obj.m_State.m_WorldGenVersion = currentModeLatestMapVersion.m_VersionID;
			obj.m_State.m_WorldGenVersioningType = currentModeLatestMapVersion.m_VersioningType;
			return obj;
		}

		public static SaveData CreateInvalid()
		{
			if (s_InvalidSaveData == null)
			{
				SaveData saveData = new SaveData();
				saveData.m_SaveInfo = new SaveInfo();
				saveData.m_State = new State();
				saveData.m_State.m_SaveVersion = int.MinValue;
				saveData.m_State.m_WorldGenVersion = WorldGenVersionData.kUninitialised.m_VersionID;
				saveData.m_State.m_WorldGenVersioningType = WorldGenVersionData.kUninitialised.m_VersioningType;
				s_InvalidSaveData = saveData;
			}
			return s_InvalidSaveData;
		}

		public void GatherSerializableData(List<string> dataStrings)
		{
			d.Assert(dataStrings.Count == 0, "GatherSerializableData called while already appear to have queued up save data! Cannot do this!");
			m_SaveInfo.UpdateSaveInfo(m_State);
			SKU.ParseChangeListVersionNumberToInt(SKU.ChangelistVersion, out m_State.m_SaveVersion);
			string item = SaveObjectToRawJson(m_SaveInfo);
			dataStrings.Add(item);
			string item2 = SaveObjectToRawJson(m_State);
			dataStrings.Add(item2);
			foreach (KeyValuePair<IntVector2, StoredTile> storedTile in m_State.m_StoredTiles)
			{
				IntVector2 coord = storedTile.Key;
				string item3 = coord.ToString();
				dataStrings.Add(item3);
				StoredTile value = storedTile.Value;
				string text = SaveObjectToRawJson(value);
				dataStrings.Add(text);
				WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in coord, allowEmpty: true);
				if (worldTile == null || worldTile.m_LoadStep == WorldTile.LoadStep.Created)
				{
					value.Reset();
					Singleton.Manager<ManSaveGame>.inst.storedTilePool.Push(value);
					worldTile?.ReleaseSaveState(moveSaveDataToJSON: false);
					s_StoredTileJSONToReturn.Add(coord, text);
				}
			}
			if (m_State.m_StoredTilesJSON != null)
			{
				foreach (KeyValuePair<IntVector2, string> item4 in m_State.m_StoredTilesJSON)
				{
					d.Assert(item4.Key != IntVector2.invalid, "Writing out invalid tile co-ordinates in save game");
					d.Assert(!m_State.m_StoredTiles.ContainsKey(item4.Key), "Found tile data in JSON that isn't present in the world at " + item4.Key);
					dataStrings.Add(item4.Key.ToString());
					dataStrings.Add(item4.Value);
				}
			}
			if (s_StoredTileJSONToReturn.Count > 0)
			{
				if (m_State.m_StoredTilesJSON == null)
				{
					m_State.m_StoredTilesJSON = new Dictionary<IntVector2, string>(Singleton.Manager<ManSaveGame>.inst.m_InitialNumberOfStoredTiles);
				}
				foreach (KeyValuePair<IntVector2, string> item5 in s_StoredTileJSONToReturn)
				{
					m_State.m_StoredTilesJSON.Add(item5.Key, item5.Value);
					m_State.m_StoredTiles.Remove(item5.Key);
				}
			}
			s_StoredTileJSONToReturn.Clear();
		}

		public void Deserialize(StreamReader streamReader, bool loadInfoOnly, bool assertOnFail, bool validate)
		{
			if (m_SaveInfo != null)
			{
				m_SaveInfo.DestroyTexture();
			}
			string rawJson = streamReader.ReadLine();
			LoadObjectFromRawJson(ref m_SaveInfo, rawJson, assertOnFail, validate);
			bool flag = m_SaveInfo.m_SaveFormatVersion <= 1;
			State objectToLoad = null;
			if (!loadInfoOnly || flag)
			{
				rawJson = streamReader.ReadLine();
				LoadObjectFromRawJson(ref objectToLoad, rawJson, assertOnFail, validate);
				if (flag)
				{
					m_SaveInfo.m_WorldGenVersionID = objectToLoad.m_WorldGenVersion;
					m_SaveInfo.m_WorldGenVersioningType = (int)objectToLoad.m_WorldGenVersioningType;
				}
			}
			if (loadInfoOnly)
			{
				return;
			}
			m_State = objectToLoad;
			m_State.m_StoredTilesJSON = new Dictionary<IntVector2, string>(Singleton.Manager<ManSaveGame>.inst.m_InitialNumberOfStoredTiles);
			while (true)
			{
				rawJson = streamReader.ReadLine();
				string text = streamReader.ReadLine();
				if (streamReader.EndOfStream)
				{
					break;
				}
				IntVector2 intVector = IntVector2.ConvertFromString(rawJson);
				d.Assert(intVector != IntVector2.invalid, "Loading a tile at invalid co-ordinates from save file.");
				if (!string.IsNullOrEmpty(text) && text != "null")
				{
					m_State.m_StoredTilesJSON.Add(intVector, text);
				}
				else
				{
					d.LogError("ManSaveGame.Deserialize - Found tileCoord but missing tileDataJSON!? Did we save garbage, or get corrupted?");
				}
			}
			d.Assert(rawJson != null, "Failed to read file checksum data!");
			m_State.m_StoredTiles = new Dictionary<IntVector2, StoredTile>(m_State.m_StoredTilesJSON.Count);
		}
	}

	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct OverrideNextVisibleIDHelper : IDisposable
	{
		public OverrideNextVisibleIDHelper(int nextVisibleID)
		{
			Singleton.Manager<ManSaveGame>.inst.CurrentState.OverrideNextVisibleID(nextVisibleID);
		}

		public void Dispose()
		{
			Singleton.Manager<ManSaveGame>.inst.CurrentState.EnsureNextVisibleIDOverrideIsCleared();
		}
	}

	public class SaveInfo
	{
		public string m_SaveName;

		public string m_SaveNameAncestor;

		public int m_SaveFormatVersion;

		public string m_ChangelistVersion;

		public string m_DisplayVersionNr;

		public int m_WorldGenVersionID;

		public int m_WorldGenVersioningType;

		public ManGameMode.GameType m_GameType;

		public TimeSpan m_TimePlayed;

		public DateTime m_LastPlayed;

		public byte[] m_LastScreenshotData;

		public string m_WorldSeed;

		public string m_BiomeChoice;

		public int m_Money;

		public int[] m_CorpLicenceLevels;

		public Lobby.LobbyVisibility m_LobbyVisibility;

		private const int kCurrentSaveFormatVersion = 2;

		public const int kPreWorldGenBiomeIterationVerChangeVersion = 1;

		public string m_ModNames;

		private Texture2D m_LastScreenshot;

		private int m_LastScreenshotID = -1;

		private static int m_NextScreenshotID1 = 100001;

		private static int m_NextScreenshotID2 = 500001;

		private static int m_NextScreenshotID3 = 900001;

		public bool IsAutoSave => Singleton.Manager<ManSaveGame>.inst.IsSaveNameAutoSave(m_SaveName);

		public bool IsWorkshopSave { get; set; }

		public bool HasMods { get; set; }

		[JsonIgnore]
		public WorldGenVersionData WorldGenVersion => new WorldGenVersionData(m_WorldGenVersionID, (BiomeMap.WorldGenVersioningType)m_WorldGenVersioningType);

		[JsonIgnore]
		public string FullFilePath { get; set; }

		[JsonIgnore]
		public Texture2D LastScreenshot
		{
			get
			{
				if (m_LastScreenshot == null)
				{
					LoadScreenshotData();
				}
				return m_LastScreenshot;
			}
			set
			{
				DestroyTexture();
				d.Assert(m_LastScreenshot == null, "ASSERT - m_LastScreenshot is not NULL!");
				m_LastScreenshot = value;
				m_LastScreenshotID = m_NextScreenshotID3++;
			}
		}

		public void UpdateSaveInfo(State gameState)
		{
			SaveScreenshotData();
			m_GameType = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType();
			m_TimePlayed = TimeSpan.FromSeconds(gameState.m_RunningTime);
			m_LastPlayed = DateTime.Now;
			m_SaveFormatVersion = 2;
			m_ChangelistVersion = SKU.ChangelistVersion;
			m_WorldGenVersionID = gameState.m_WorldGenVersion;
			m_WorldGenVersioningType = (int)gameState.m_WorldGenVersioningType;
			m_DisplayVersionNr = SKU.DisplayVersion;
			m_WorldSeed = gameState.m_WorldSeed;
			m_BiomeChoice = gameState.m_BiomeChoice;
			m_Money = Singleton.Manager<ManPlayer>.inst.GetCurrentMoney();
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
			{
				m_LobbyVisibility = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetLobbyVisibility(Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.ID);
			}
			else
			{
				m_LobbyVisibility = Lobby.LobbyVisibility.Private;
			}
			int count = EnumValuesIterator<FactionSubTypes>.Count;
			m_CorpLicenceLevels = new int[count];
			for (int i = 0; i < count; i++)
			{
				int num = -1;
				FactionSubTypes corporation = (FactionSubTypes)i;
				if (Singleton.Manager<ManLicenses>.inst.IsLicenseSupported(corporation) && Singleton.Manager<ManLicenses>.inst.IsLicenseDiscovered(corporation))
				{
					num = Singleton.Manager<ManLicenses>.inst.GetCurrentLevel(corporation);
				}
				m_CorpLicenceLevels[i] = num;
			}
			HasMods = Singleton.Manager<ManMods>.inst.GetNumModsInCurrentSession() > 0;
			m_ModNames = Singleton.Manager<ManMods>.inst.GetModsInCurrentSession();
		}

		public bool IsFormatSupported()
		{
			return m_SaveFormatVersion >= 1;
		}

		public bool WasSavedInNewerVersion()
		{
			SKU.ParseChangeListVersionNumberToInt(m_ChangelistVersion, out var versionInt);
			SKU.ParseChangeListVersionNumberToInt(SKU.ChangelistVersion, out var versionInt2);
			if (versionInt2 != -1)
			{
				return versionInt > versionInt2;
			}
			return false;
		}

		public bool CanLoadSave()
		{
			if (IsFormatSupported() && (DebugUtil.DebugAllowAllSaves || !WasSavedInNewerVersion()))
			{
				if (m_GameType == ManGameMode.GameType.RaD)
				{
					return Singleton.Manager<ManDLC>.inst.HasAnyDLCOfType(ManDLC.DLCType.RandD);
				}
				return true;
			}
			return false;
		}

		public void DestroyTexture()
		{
			if (m_LastScreenshot != null)
			{
				UnityEngine.Object.Destroy(m_LastScreenshot);
				m_LastScreenshot = null;
			}
			d.Assert(m_LastScreenshot == null, "ASSERT - m_LastScreenshot is not NULL!");
		}

		private void SaveScreenshotData()
		{
			bool isGraphicOptionEnabled = Singleton.Manager<CameraManager>.inst.GetIsGraphicOptionEnabled(CameraManager.GraphicOption.DOF);
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.DOF, enabled: false);
			bool isGraphicOptionEnabled2 = Singleton.Manager<CameraManager>.inst.GetIsGraphicOptionEnabled(CameraManager.GraphicOption.ScreenBlur);
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.ScreenBlur, enabled: false);
			RenderTexture targetTexture = Singleton.camera.targetTexture;
			OutlineEffect outlineEffect = Singleton.Manager<CameraManager>.inst.OutlineEffect;
			bool enabled = outlineEffect.enabled;
			outlineEffect.enabled = false;
			RenderTexture temporary = RenderTexture.GetTemporary(GameScreenshotSize, GameScreenshotSize, 24, RenderTextureFormat.ARGB32);
			Singleton.camera.targetTexture = temporary;
			Singleton.camera.Render();
			Singleton.camera.targetTexture = targetTexture;
			if (m_LastScreenshot == null)
			{
				m_LastScreenshot = new Texture2D(GameScreenshotSize, GameScreenshotSize, TextureFormat.ARGB32, mipChain: false);
				m_LastScreenshotID = m_NextScreenshotID1++;
			}
			RenderTexture.active = temporary;
			m_LastScreenshot.ReadPixels(new Rect(0f, 0f, GameScreenshotSize, GameScreenshotSize), 0, 0, recalculateMipMaps: false);
			m_LastScreenshot.Apply(updateMipmaps: false, makeNoLongerReadable: false);
			RenderTexture.active = null;
			RenderTexture.ReleaseTemporary(temporary);
			if (m_LastScreenshot != null)
			{
				m_LastScreenshotData = m_LastScreenshot.EncodeToJPG(70);
			}
			else
			{
				d.LogError("Game screenshot is null, how did this happen?");
			}
			outlineEffect.enabled = enabled;
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.DOF, isGraphicOptionEnabled);
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.ScreenBlur, isGraphicOptionEnabled2);
		}

		private void LoadScreenshotData()
		{
			DestroyTexture();
			if (m_LastScreenshotData != null && m_LastScreenshotData.Length != 0)
			{
				d.Assert(m_LastScreenshot == null, "ASSERT - m_LastScreenshot is not NULL!");
				m_LastScreenshot = new Texture2D(GameScreenshotSize, GameScreenshotSize);
				m_LastScreenshot.LoadImage(m_LastScreenshotData);
				m_LastScreenshotID = m_NextScreenshotID2++;
			}
			else
			{
				m_LastScreenshotData = null;
			}
		}
	}

	public class State
	{
		private class UIDBucketTilePair
		{
			public UIDBucket uidBucket;

			public WorldTile tile;
		}

		public string m_WorldSeed;

		public string m_BiomeChoice;

		public int m_WorldGenVersion;

		public BiomeMap.WorldGenVersioningType m_WorldGenVersioningType;

		public int m_SaveVersion;

		public int m_VisibleIDCounter;

		public UIDBucket m_VisibleIDBucket;

		public uint m_NextTrackableObjectID;

		public float m_RunningTime;

		public CameraPosition m_CameraPos;

		[JsonProperty]
		private Dictionary<string, string> m_SaveDataJSON = new Dictionary<string, string>();

		public bool m_FileHasBeenTamperedWith;

		public UIDBucket m_SpawnedPlayerNetTechCount = new UIDBucket(0, int.MaxValue, warnAboutLooping: false);

		[JsonIgnore]
		public Dictionary<IntVector2, string> m_StoredTilesJSON;

		[JsonIgnore]
		public Dictionary<IntVector2, StoredTile> m_StoredTiles;

		[JsonIgnore]
		private UIDBucketTilePair[] m_SceneryIDBuckets;

		public List<StoredVisible> m_VisiblesFailedToRestore;

		private int m_OverrideNextVisibleID = -1;

		[JsonIgnore]
		public WorldGenVersionData WorldGenVersionData => new WorldGenVersionData(m_WorldGenVersion, m_WorldGenVersioningType);

		public State()
		{
			int num = 400;
			m_SceneryIDBuckets = new UIDBucketTilePair[num];
			for (int i = 0; i < num; i++)
			{
				int num2 = int.MaxValue - (i + 1) * 2500;
				m_SceneryIDBuckets[i] = new UIDBucketTilePair
				{
					uidBucket = new UIDBucket(num2, num2 + 2500),
					tile = null
				};
			}
			m_VisibleIDBucket = new UIDBucket(1, int.MaxValue - num * 2500);
			m_NextTrackableObjectID = 0u;
			m_StoredTiles = new Dictionary<IntVector2, StoredTile>(Singleton.Manager<ManSaveGame>.inst.m_InitialNumberOfStoredTiles);
		}

		public void AddSaveData<T>(SaveDataJSONType saveDataType, T saveData)
		{
			d.Assert(saveData != null, "saveData is null!");
			try
			{
				string text = JsonConvert.SerializeObject(saveData, s_JSONSerialisationSettings);
				d.Assert(text != null, "saveDataJSON is null!");
				d.Assert(text.Length > 0, "saveDataJSON is empty!");
				d.Assert(text != "null", "saveDataJSON is the actual string 'null'!");
				string text2 = saveDataType.ToString();
				d.Assert(text2 != null, "saveDataName is null!");
				d.Assert(text2.Length > 0, "saveDataName is empty!");
				d.Assert(text2 != "null", "saveDataName is the actual string 'null'!");
				m_SaveDataJSON[text2] = text;
			}
			catch (Exception ex)
			{
				d.LogError("AddSaveData<T> Exception=" + ex.Message + " SaveDataType=" + saveData.GetType().Name);
			}
		}

		public bool GetSaveData<T>(SaveDataJSONType saveDataType, out T saveData)
		{
			bool result = false;
			string key = saveDataType.ToString();
			if (m_SaveDataJSON.TryGetValue(key, out var value) && !value.NullOrEmpty())
			{
				try
				{
					saveData = JsonConvert.DeserializeObject<T>(value, s_JSONSerialisationSettings);
					result = true;
				}
				catch (Exception ex)
				{
					saveData = default(T);
					d.LogError("GetSaveData<T> Exception=" + ex.Message + " saveDataType=" + typeof(T).Name + " JSON='" + value + "'");
				}
			}
			else
			{
				saveData = default(T);
			}
			return result;
		}

		public bool CheckHasSaveData(SaveDataJSONType saveDataType)
		{
			return m_SaveDataJSON.ContainsKey(saveDataType.ToString());
		}

		public int GetSaveDataSize(SaveDataJSONType saveDataType, bool compressed)
		{
			int result = 0;
			if (m_SaveDataJSON.TryGetValue(saveDataType.ToString(), out var value) && !value.NullOrEmpty())
			{
				int byteCount = Encoding.UTF8.GetByteCount(value);
				if (compressed)
				{
					using MemoryStream memoryStream = new MemoryStream(byteCount);
					using (GZipStream stream = new GZipStream(memoryStream, CompressionMode.Compress, leaveOpen: true))
					{
						using StreamWriter streamWriter = new StreamWriter(stream);
						streamWriter.WriteLine(value);
					}
					result = (int)memoryStream.Length;
				}
				else
				{
					result = byteCount;
				}
			}
			return result;
		}

		public void OverrideNextVisibleID(int nextID)
		{
			bool flag = nextID <= m_VisibleIDBucket.GetCurrent();
			d.Assert(flag, "Trying to override a next visible id to one that will be allocated in future.  NextID=" + nextID + " CurrentID=" + m_VisibleIDBucket.GetCurrent());
			d.AssertFormat(m_OverrideNextVisibleID == -1, "OverrideNextVisibleID - Already have a pending override visible ID set, trying to set {0}! ID of first overridden Visible {1} will be lost! (CALL-A-CODER)", nextID, m_OverrideNextVisibleID);
			if (flag)
			{
				m_OverrideNextVisibleID = nextID;
			}
		}

		public void EnsureNextVisibleIDOverrideIsCleared()
		{
			d.Assert(m_OverrideNextVisibleID == -1, "EnsureNextVisibleIDOverrideIsCleared found that OverrideNextVisibleID has not been consumed! This will create the next visible with an incorrect ID!");
			m_OverrideNextVisibleID = -1;
		}

		public int GetCurrentHighestVisibleID(ObjectTypes visibleType)
		{
			d.Assert(visibleType != ObjectTypes.Scenery, "GetCurrentHighestVisibleID - Trying to get <generic> visible ID for type Scenery, but looking in the wrong bucket!");
			return m_VisibleIDBucket.GetCurrent();
		}

		public int GetNextVisibleID(ObjectTypes visibleType)
		{
			d.Assert(visibleType != ObjectTypes.Scenery, "GetNextVisibleID - Trying to get <generic> visible ID for type Scenery, but should be using GetNextSceneryVisibleID!");
			int num = -1;
			if (m_OverrideNextVisibleID != -1)
			{
				num = m_OverrideNextVisibleID;
				m_OverrideNextVisibleID = -1;
			}
			else
			{
				num = m_VisibleIDBucket.GetNextAndIncrement();
			}
			return num;
		}

		public int GetNextSceneryVisibleID(IntVector2 tileCoord)
		{
			int num = -1;
			for (int i = 0; i < m_SceneryIDBuckets.Length; i++)
			{
				if (m_SceneryIDBuckets[i].tile != null && m_SceneryIDBuckets[i].tile.Coord == tileCoord)
				{
					num = m_SceneryIDBuckets[i].uidBucket.GetNextAndIncrement();
					break;
				}
			}
			d.AssertFormat(num != -1, "GetNextSceneryVisibleID - Failed to find bucket for scenery in tile {0}!", tileCoord);
			return num;
		}

		public uint GetNextTrackableObjectID()
		{
			uint nextTrackableObjectID = m_NextTrackableObjectID;
			m_NextTrackableObjectID++;
			if (m_NextTrackableObjectID == uint.MaxValue)
			{
				d.LogError("GetNextTrackableObjectID - Ran out of unique ID space for TrackableObject!");
			}
			return nextTrackableObjectID;
		}

		public StoredTile GetNewStoredTile(IntVector2 coord)
		{
			if (Singleton.Manager<ManSaveGame>.inst.storedTilePool == null)
			{
				Singleton.Manager<ManSaveGame>.inst.storedTilePool = new Stack<StoredTile>(Singleton.Manager<ManSaveGame>.inst.m_InitialNumberOfStoredTiles);
				for (int i = 0; i < Singleton.Manager<ManSaveGame>.inst.m_InitialNumberOfStoredTiles; i++)
				{
					StoredTile item = StoredTile.CreateNew(Singleton.Manager<ManSaveGame>.inst.m_InitialStoredTileListSize);
					Singleton.Manager<ManSaveGame>.inst.storedTilePool.Push(item);
				}
			}
			StoredTile storedTile = ((Singleton.Manager<ManSaveGame>.inst.storedTilePool.Count <= 0) ? StoredTile.CreateNew(Singleton.Manager<ManSaveGame>.inst.m_InitialStoredTileListSize) : Singleton.Manager<ManSaveGame>.inst.storedTilePool.Pop());
			storedTile.coord = coord;
			m_StoredTiles[coord] = storedTile;
			return storedTile;
		}

		public void AssignSceneryUIDBucketToTile(WorldTile tile)
		{
			bool flag = false;
			for (int i = 0; i < m_SceneryIDBuckets.Length; i++)
			{
				if (m_SceneryIDBuckets[i].tile == null)
				{
					m_SceneryIDBuckets[i].tile = tile;
					m_SceneryIDBuckets[i].uidBucket.Reset();
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				string text = "";
				for (int j = 0; j < m_SceneryIDBuckets.Length; j++)
				{
					WorldTile tile2 = m_SceneryIDBuckets[j].tile;
					text += $"[{tile2.Coord} - \tcur:{tile2.m_LoadStep} \treq:{tile2.m_RequestState}], ";
				}
				d.LogErrorFormat("Failed to find an empty scenery ID bucket to assign to tile {0}. All buckets ({1}) were taken! \n Tiles: {2}", tile.Coord, m_SceneryIDBuckets.Length, text);
			}
		}

		public void ClearTileSceneryUIDBucket(WorldTile tile)
		{
			bool flag = false;
			for (int i = 0; i < m_SceneryIDBuckets.Length; i++)
			{
				if (m_SceneryIDBuckets[i].tile == tile)
				{
					m_SceneryIDBuckets[i].tile = null;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				d.LogErrorFormat("OnTileDepopulated - Failed to find matching bucket for tile {0}. The tile didn't have a UID bucket assigned!?", tile.Coord);
			}
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (m_VisibleIDCounter > 0)
			{
				m_VisibleIDBucket.SetCurrent(m_VisibleIDCounter);
				m_VisibleIDCounter = -1;
			}
		}
	}

	public class CameraPosition
	{
		public V3Serial m_Position { get; set; }

		public WorldPosition m_WorldPosition { get; set; }

		public V3Serial m_Forward { get; set; }

		public Vector3 GetBackwardsCompatiblePosition()
		{
			Vector3 result = m_Position + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
			if (m_WorldPosition != default(WorldPosition) && m_Position == Vector3.zero)
			{
				result = m_WorldPosition.ScenePosition;
			}
			return result;
		}
	}

	public abstract class StoredVisible
	{
		public V3Serial m_Position;

		public WorldPosition m_WorldPosition;

		public QuatSerial m_Rotation;

		public V3Serial m_Velocity;

		public V3Serial m_AngularVelocity;

		public float m_ExpireTimeout;

		public Visible.AutoExpireState m_ExpireState;

		public bool m_InSafeArea;

		public bool m_Asleep;

		public int m_ID;

		public int m_TrackStates;

		public bool m_HasBeenSpawned;

		public bool m_WorldSpaceComponentInactive;

		public IntVector2 m_TileOverlapDirection;

		[NonSerialized]
		[JsonIgnore]
		public Func<StoredVisible, Visible> CustomRestoreBehaviour;

		public abstract void Store(Visible visible);

		public abstract Visible SpawnAndRestore();

		public abstract bool CanRestore();

		protected bool CanRestore(ObjectTypes type)
		{
			bool result;
			switch (type)
			{
			case ObjectTypes.Block:
			case ObjectTypes.Chunk:
			{
				result = true;
				float currentModeRunningTime = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
				if (m_ExpireTimeout != -1f && currentModeRunningTime > m_ExpireTimeout && !m_InSafeArea)
				{
					result = false;
				}
				break;
			}
			case ObjectTypes.Vehicle:
			case ObjectTypes.Waypoint:
			case ObjectTypes.Crate:
				result = true;
				break;
			default:
				result = true;
				d.LogError("StoredVisibleNew.CanRestore doesn't cover the case with ObjectType" + type);
				break;
			}
			return result;
		}

		public Vector3 GetBackwardsCompatiblePosition()
		{
			Vector3 result = m_Position + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
			if (m_WorldPosition != default(WorldPosition) && m_Position == Vector3.zero)
			{
				result = m_WorldPosition.ScenePosition;
			}
			return result;
		}
	}

	public class StoredTech : StoredVisible
	{
		public TechData m_TechData = new TechData();

		public int m_TeamID;

		public bool m_IsPlayerFocus;

		public bool m_Grounded;

		public bool m_IsPopulation;

		public bool m_HasRewardValue = true;

		public int[] m_BlockIds;

		public List<StoredVisible> m_StoredHeldVisibles;

		public bool m_ShouldExplodeDetachingBlocks;

		public float m_ExplodeDetachingBlocksDelay;

		public override void Store(Visible visible)
		{
			visible.SaveForStorage(this);
			Tank tank = visible.tank;
			bool saveRuntimeState = true;
			m_TechData.SaveTech(tank, saveRuntimeState);
			m_HasRewardValue = tank.OriginalValue > 0f;
			m_TeamID = tank.Team;
			m_IsPlayerFocus = tank == Singleton.playerTank;
			m_Grounded = tank.grounded;
			m_IsPopulation = tank.IsPopulation;
			m_BlockIds = new int[tank.blockman.blockCount];
			for (int i = 0; i < tank.blockman.blockCount; i++)
			{
				m_BlockIds[i] = tank.blockman.GetBlockWithIndex(i).visible.ID;
			}
			m_StoredHeldVisibles = new List<StoredVisible>();
			TechHolders.HolderIterator enumerator = tank.Holders.GetEnumerator();
			while (enumerator.MoveNext())
			{
				ModuleItemHolder.Stack.ItemIterator enumerator2 = enumerator.Current.Contents.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					StoredVisible item = CreateStoredVisible(enumerator2.Current);
					m_StoredHeldVisibles.Add(item);
				}
			}
			m_ShouldExplodeDetachingBlocks = tank.ShouldExplodeDetachingBlocks;
			m_ExplodeDetachingBlocksDelay = tank.ExplodeDetachingBlocksDelay;
		}

		public override Visible SpawnAndRestore()
		{
			Tank tank = null;
			if (m_TechData.m_BlockSpecs != null && m_TechData.m_BlockSpecs.Count > 0)
			{
				if (m_StoredHeldVisibles != null)
				{
					for (int i = 0; i < m_StoredHeldVisibles.Count; i++)
					{
						Visible.DisableAddToTileOnSpawn = true;
						RestoreVisible(m_StoredHeldVisibles[i]);
						Visible.DisableAddToTileOnSpawn = false;
					}
				}
				ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
				{
					techData = m_TechData,
					blockIDs = m_BlockIds,
					teamID = m_TeamID,
					position = GetBackwardsCompatiblePosition(),
					rotation = m_Rotation,
					grounded = false,
					isPopulation = m_IsPopulation,
					forceSpawn = true,
					placement = ManSpawn.TankSpawnParams.Placement.BoundsCentredAtPosition,
					hasRewardValue = m_HasRewardValue,
					shouldExplodeDetachingBlocks = m_ShouldExplodeDetachingBlocks,
					explodeDetachingBlocksDelay = m_ExplodeDetachingBlocksDelay
				};
				using (new OverrideNextVisibleIDHelper(m_ID))
				{
					tank = Singleton.Manager<ManSpawn>.inst.SpawnTank(param, addToObjectManager: false);
				}
				if ((bool)tank)
				{
					bool hasBeenSpawned = m_HasBeenSpawned;
					tank.visible.RestoreSaved(this, hasBeenSpawned);
					if (m_IsPlayerFocus)
					{
						UpdateNetPlayerTeamFromTech(tank);
						Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(tank);
					}
					Singleton.Manager<ManSaveGame>.inst.m_SerializedVisibleIDLookup[tank.visible.ID] = tank.visible;
					for (int j = 0; j < tank.blockman.blockCount; j++)
					{
						Visible visible = tank.blockman.GetBlockWithIndex(j).visible;
						Singleton.Manager<ManSaveGame>.inst.m_SerializedVisibleIDLookup[visible.ID] = visible;
					}
				}
				else
				{
					d.LogError("ManSaveGame.Restore - Spawned Tank is null");
				}
			}
			else
			{
				d.LogError("ManSaveGame.Restore - Tank has invalid blockSpec");
			}
			if (!tank)
			{
				return null;
			}
			return tank.visible;
		}

		public override bool CanRestore()
		{
			return CanRestore(ObjectTypes.Vehicle);
		}

		private void UpdateNetPlayerTeamFromTech(Tank tech)
		{
			NetPlayer myPlayer = Singleton.Manager<ManNetwork>.inst.MyPlayer;
			if ((bool)tech.netTech && (bool)myPlayer && myPlayer.TechTeamID != tech.Team)
			{
				int num = ManSpawn.LobbyTeamIDFromTechTeamID(tech.Team);
				d.Assert(num != int.MaxValue, $"Unexpected Team 0x{tech.Team:X} for player tech {tech.name} when loading multiplayer", tech);
				if (num != int.MaxValue)
				{
					myPlayer.OnServerSetTeamID(num);
				}
			}
		}
	}

	public class StoredBlock : StoredVisible
	{
		public BlockTypes m_BlockType;

		public float m_Health;

		public int m_LastTechTeam;

		public override void Store(Visible visible)
		{
			visible.SaveForStorage(this);
			m_BlockType = visible.block.BlockType;
			m_Health = visible.damageable.Health;
			m_LastTechTeam = visible.block.LastTechTeam;
		}

		public override Visible SpawnAndRestore()
		{
			Visible visible;
			using (new OverrideNextVisibleIDHelper(m_ID))
			{
				visible = Singleton.Manager<ManSpawn>.inst.SpawnItem(new ItemTypeInfo(ObjectTypes.Block, (int)m_BlockType), GetBackwardsCompatiblePosition(), m_Rotation, addToObjectManager: false, forceSpawn: true, initNew: false);
			}
			if ((bool)visible)
			{
				bool restoreTransform = true;
				visible.RestoreSaved(this, restoreTransform);
				if (visible.isActive)
				{
					visible.block.RestoreSaved(m_Health, m_LastTechTeam);
					Singleton.Manager<ManSaveGame>.inst.m_SerializedVisibleIDLookup[visible.ID] = visible;
				}
			}
			else
			{
				d.LogError("ManSaveGame.Restore - Block spawned was null");
			}
			return visible;
		}

		public override bool CanRestore()
		{
			return CanRestore(ObjectTypes.Block);
		}
	}

	public class StoredChunk : StoredVisible
	{
		public ChunkTypes m_ChunkType;

		public float m_Health;

		public override void Store(Visible visible)
		{
			visible.SaveForStorage(this);
			m_ChunkType = (ChunkTypes)visible.ItemType;
			m_Health = visible.damageable.Health;
		}

		public override Visible SpawnAndRestore()
		{
			Visible visible;
			using (new OverrideNextVisibleIDHelper(m_ID))
			{
				visible = Singleton.Manager<ManSpawn>.inst.SpawnItem(new ItemTypeInfo(ObjectTypes.Chunk, (int)m_ChunkType), GetBackwardsCompatiblePosition(), m_Rotation, addToObjectManager: false, forceSpawn: true, initNew: false);
			}
			if ((bool)visible)
			{
				bool restoreTransform = true;
				visible.RestoreSaved(this, restoreTransform);
				if (visible.isActive)
				{
					visible.pickup.RestoreSaved(m_Health);
					Singleton.Manager<ManSaveGame>.inst.m_SerializedVisibleIDLookup[visible.ID] = visible;
				}
			}
			else
			{
				d.LogError("ManSaveGame.Restore - Block spawned was null");
			}
			return visible;
		}

		public override bool CanRestore()
		{
			return CanRestore(ObjectTypes.Chunk);
		}
	}

	public class StoredCrate : StoredVisible
	{
		public string name;

		public Crate.Definition definition;

		public Crate.SaveData saveData;

		public FactionSubTypes corpType;

		public override void Store(Visible visible)
		{
			visible.SaveForStorage(this);
			name = visible.crate.name;
			definition = visible.crate.Def;
			saveData = visible.crate.Save;
			corpType = visible.crate.CorpType;
		}

		public override Visible SpawnAndRestore()
		{
			bool grounded = false;
			bool forceSpawn = true;
			TrackedVisible trackedVisible;
			using (new OverrideNextVisibleIDHelper(m_ID))
			{
				trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnEmptyCrateDef(name, definition, GetBackwardsCompatiblePosition(), m_Rotation, grounded, Singleton.Manager<ManNetwork>.inst.IsServer, forceSpawn, corpType);
			}
			Visible visible = trackedVisible.visible;
			Crate crate = ((visible != null) ? visible.crate : null);
			if (crate != null)
			{
				bool restoreTransform = false;
				visible.RestoreSaved(this, restoreTransform);
				crate.SetSaveData(saveData);
				Singleton.Manager<ManSaveGame>.inst.m_SerializedVisibleIDLookup[crate.visible.ID] = crate.visible;
				if (Singleton.Manager<ManNetwork>.inst.IsServer)
				{
					NetworkServer.Spawn(crate.gameObject);
				}
			}
			else
			{
				d.LogError("ManSaveGame.Restore - failed to spawn saved crate");
			}
			return visible;
		}

		public override bool CanRestore()
		{
			return CanRestore(ObjectTypes.Crate);
		}
	}

	public class StoredWaypoint : StoredVisible
	{
		public override void Store(Visible visible)
		{
			visible.SaveForStorage(this);
		}

		public override Visible SpawnAndRestore()
		{
			Visible visible;
			using (new OverrideNextVisibleIDHelper(m_ID))
			{
				visible = Singleton.Manager<ManSpawn>.inst.SpawnItem(new ItemTypeInfo(ObjectTypes.Waypoint, 0), GetBackwardsCompatiblePosition(), m_Rotation, addToObjectManager: false, forceSpawn: true, initNew: false);
			}
			if ((bool)visible)
			{
				visible.RestoreSaved(this, restoreTransform: false);
				if (visible.isActive)
				{
					Singleton.Manager<ManSaveGame>.inst.m_SerializedVisibleIDLookup[visible.ID] = visible;
				}
			}
			else
			{
				d.LogError("ManSaveGame.Restore - Waypoint spawned was null");
			}
			return visible;
		}

		public override bool CanRestore()
		{
			return CanRestore(ObjectTypes.Waypoint);
		}
	}

	public struct StoredTerrainObject
	{
		public string m_PrefabGUID;

		public string m_PrefabName;

		public V3Serial m_Position;

		public WorldPosition m_WorldPosition;

		public QuatSerial m_Rotation;

		public bool m_IsTrackedObject;

		public uint m_TrackedObjectID;

		public StoredTile.StoredSceneryState m_StoredSceneryState;
	}

	public class StoredTile
	{
		public class StoredSceneryState
		{
			public ResourceDispenser.PersistentState state;

			public SceneryTypes sceneryType;
		}

		public IntVector2 coord = IntVector2.invalid;

		private static int s_NumVisibleTypes = Enum.GetNames(typeof(ObjectTypes)).Length;

		public Dictionary<int, List<StoredVisible>> m_StoredVisibles = new Dictionary<int, List<StoredVisible>>(s_NumVisibleTypes);

		public List<StoredTerrainObject> m_StoredTerrainObjects;

		[JsonConverter(typeof(IntVecDictionaryKeyConverter<StoredSceneryState>))]
		public Dictionary<IntVector2, StoredSceneryState> m_Scenery;

		public bool m_HasBeenSavedBefore;

		public int m_TileSavedVersion;

		private const int k_CurrentTileVersion = 3;

		private static List<Visible> s_VisibleRecycleList = new List<Visible>(1024);

		private static Stopwatch s_DespawnStopwatch = new Stopwatch();

		public static StoredTile CreateNew(int initListSize)
		{
			return new StoredTile
			{
				m_HasBeenSavedBefore = false,
				m_Scenery = new Dictionary<IntVector2, StoredSceneryState>(initListSize),
				m_TileSavedVersion = 3
			};
		}

		public void Reset()
		{
			coord = IntVector2.invalid;
			m_HasBeenSavedBefore = false;
			m_TileSavedVersion = 3;
			ClearStoredVisiblesExceptScenery();
			if (m_StoredTerrainObjects != null)
			{
				m_StoredTerrainObjects.Clear();
			}
			m_Scenery.Clear();
		}

		public void ClearStoredVisiblesExceptScenery()
		{
			m_StoredVisibles.Clear();
		}

		public void StoreVisibles(Dictionary<int, Visible>[] visiblesOnTile, List<StoredVisible> storedVisiblesOnTile = null, bool despawn = true)
		{
			if (ShouldStore)
			{
				Storing = true;
				ClearStoredVisiblesExceptScenery();
				for (int i = 0; i < k_StoreOrder.Count; i++)
				{
					int num = k_StoreOrder[i];
					Dictionary<int, Visible> dictionary = visiblesOnTile[num];
					if (dictionary == null)
					{
						continue;
					}
					bool flag = num == 1;
					List<StoredVisible> list = new List<StoredVisible>();
					foreach (Visible value2 in dictionary.Values)
					{
						d.Assert(!despawn || !value2.tank.IsNotNull() || (object)value2.tank != Singleton.playerTank || Singleton.Manager<ManGameMode>.inst.IsSwitchingMode, "ManSaveGame.Store - Player Tech is being stored in despawned Tile");
						bool flag2 = true;
						if (flag && ShouldStoreTechSeparatelyHandler != null && ShouldStoreTechSeparatelyHandler(value2.ID))
						{
							DoStoreTechSeparatelyHandler(value2);
							flag2 = false;
						}
						if (flag2)
						{
							StoredVisible storedVisible = CreateStoredVisible(value2);
							list.Add(storedVisible);
							if (flag && storedVisible is StoredTech storedTech)
							{
								_ = storedTech.m_IsPlayerFocus;
							}
						}
						if (despawn)
						{
							s_VisibleRecycleList.Add(value2);
						}
					}
					m_StoredVisibles.Add(num, list);
				}
				if (storedVisiblesOnTile != null)
				{
					int key = 1;
					if (!m_StoredVisibles.TryGetValue(key, out var value) || value == null)
					{
						value = new List<StoredVisible>();
						m_StoredVisibles[key] = value;
					}
					value.AddRange(storedVisiblesOnTile);
					for (int j = 0; j < storedVisiblesOnTile.Count; j++)
					{
						if (storedVisiblesOnTile[j] is StoredTech storedTech2)
						{
							_ = storedTech2.m_IsPlayerFocus;
						}
					}
				}
			}
			else if (despawn)
			{
				for (int k = 0; k < k_StoreOrder.Count; k++)
				{
					int num2 = k_StoreOrder[k];
					Dictionary<int, Visible> dictionary2 = visiblesOnTile[num2];
					if (dictionary2 == null)
					{
						continue;
					}
					foreach (Visible value3 in dictionary2.Values)
					{
						s_VisibleRecycleList.Add(value3);
					}
				}
			}
			if (ManNetwork.IsHost)
			{
				bool flag3 = Singleton.Manager<ManNetwork>.inst.IsMultiplayer();
				int count = s_VisibleRecycleList.Count;
				for (int l = 0; l < count; l++)
				{
					Visible visible = s_VisibleRecycleList[l];
					d.Assert(visible.gameObject.activeInHierarchy, $"StoreVisibles'- Trying to recycle visible {visible} but object was not active in hierarchy!?");
					if (flag3)
					{
						visible.ServerDestroy();
					}
					else
					{
						visible.trans.Recycle();
					}
				}
			}
			else
			{
				d.Assert(s_VisibleRecycleList.Count == 0, "ManSaveGame.StoreVisibles - Client trying to recycle object during Multiplayer Game (non-host)!");
			}
			s_VisibleRecycleList.Clear();
			m_HasBeenSavedBefore = true;
			Storing = false;
		}

		public void StoreScenery(Dictionary<int, Visible>[] visibles)
		{
			if (!ShouldStore)
			{
				return;
			}
			Storing = true;
			m_Scenery.Clear();
			foreach (Visible value in visibles[3].Values)
			{
				if ((bool)value.damageable && (bool)value.resdisp && (!value.isActive || value.damageable.Health != value.damageable.MaxHealth) && !value.resdisp.WasPlacedManually)
				{
					StoredSceneryState storedSceneryState = new StoredSceneryState();
					storedSceneryState.sceneryType = (SceneryTypes)value.ItemType;
					storedSceneryState.state = value.resdisp.Store();
					m_Scenery[value.resdisp.cellCoord] = storedSceneryState;
				}
			}
			Storing = false;
		}

		public bool DespawnScenery(Dictionary<int, Visible>[] visibles, int maxMilliseconds = int.MaxValue)
		{
			bool result = true;
			s_DespawnStopwatch.Restart();
			foreach (Visible value in visibles[3].Values)
			{
				s_VisibleRecycleList.Add(value);
			}
			int count = s_VisibleRecycleList.Count;
			for (int i = 0; i < count; i++)
			{
				s_VisibleRecycleList[i].trans.Recycle();
				if (s_DespawnStopwatch.ElapsedMilliseconds > maxMilliseconds)
				{
					result = false;
					break;
				}
			}
			s_DespawnStopwatch.Stop();
			s_VisibleRecycleList.Clear();
			return result;
		}

		public void SetSceneryAwake(Dictionary<int, Visible>[] visibles, bool awake)
		{
			foreach (Visible value in visibles[3].Values)
			{
				value.resdisp.SetAwake(awake);
			}
		}

		public void StorePersistentTerrainObjects(HashSet<TerrainObject> terrainObjects)
		{
			Storing = true;
			if (m_StoredTerrainObjects != null)
			{
				m_StoredTerrainObjects.Clear();
			}
			if (terrainObjects != null && terrainObjects.Count > 0)
			{
				if (m_StoredTerrainObjects == null)
				{
					m_StoredTerrainObjects = new List<StoredTerrainObject>(terrainObjects.Count);
				}
				foreach (TerrainObject terrainObject in terrainObjects)
				{
					if (!terrainObject.PrefabGUID.NullOrEmpty())
					{
						StoredSceneryState storedSceneryState = null;
						Visible visible = terrainObject.visible;
						if (visible != null && visible.resdisp != null)
						{
							storedSceneryState = new StoredSceneryState
							{
								sceneryType = (SceneryTypes)visible.ItemType,
								state = visible.resdisp.Store()
							};
						}
						Transform transform = terrainObject.transform;
						StoredTerrainObject item = new StoredTerrainObject
						{
							m_WorldPosition = WorldPosition.FromScenePosition(transform.position),
							m_Rotation = transform.rotation,
							m_PrefabGUID = terrainObject.PrefabGUID,
							m_PrefabName = terrainObject.name,
							m_IsTrackedObject = terrainObject.IsTracked,
							m_TrackedObjectID = terrainObject.TrackedId,
							m_StoredSceneryState = storedSceneryState
						};
						m_StoredTerrainObjects.Add(item);
					}
					else
					{
						d.LogError("StorePersistentTerrainObjects - TerrainObject '" + terrainObject.name + "' could not be saved as the PrefabGUID is not set! It's probably not in the TerrainObjectTable!");
					}
				}
			}
			Storing = false;
		}

		public void DespawnPersistentTerrainObjects(HashSet<TerrainObject> terrainObjects)
		{
			if (terrainObjects == null || terrainObjects.Count <= 0)
			{
				return;
			}
			List<Transform> list = new List<Transform>();
			foreach (TerrainObject terrainObject in terrainObjects)
			{
				if (terrainObject.visible != null && terrainObject.visible.resdisp != null)
				{
					terrainObject.visible.resdisp.SetAwake(awake: false);
				}
				list.Add(terrainObject.transform);
			}
			foreach (Transform item in list)
			{
				if (item.gameObject.activeInHierarchy)
				{
					item.Recycle();
				}
			}
		}

		public void AddSavedTech(TechData techData, Vector3 position, Quaternion rotation, int id, int team, int[] blockIds, bool grounded = false, bool isPopulation = false, bool hasRewardValue = false, bool shouldExplodeDetachingBlocks = false, float explodeDetachingBlocksDelay = 0f, bool checkAgainstScenery = false)
		{
			d.Assert(techData != null, "ManSpawn.AddSavedTech - TechData must not be null");
			if (grounded)
			{
				position = Singleton.Manager<ManWorld>.inst.ProjectToGround(position, checkAgainstScenery) + Vector3.up;
			}
			StoredTech storedTech = new StoredTech();
			storedTech.m_TechData = techData;
			storedTech.m_TeamID = team;
			storedTech.m_IsPlayerFocus = false;
			storedTech.m_Grounded = grounded;
			storedTech.m_BlockIds = blockIds;
			storedTech.m_IsPopulation = isPopulation;
			storedTech.m_HasRewardValue = hasRewardValue;
			storedTech.m_ShouldExplodeDetachingBlocks = shouldExplodeDetachingBlocks;
			storedTech.m_ExplodeDetachingBlocksDelay = explodeDetachingBlocksDelay;
			Visible.InitStored(storedTech, position, rotation, techData.Radius);
			storedTech.m_ID = id;
			AddStoredVisibleToTile(storedTech, ObjectTypes.Vehicle);
		}

		public void AddSavedCrate(string name, Crate.Definition definition, Crate.SaveData saveData, Vector3 position, Quaternion rotation, int id, FactionSubTypes m_CorpType)
		{
			StoredCrate storedCrate = new StoredCrate();
			storedCrate.name = name;
			storedCrate.definition = definition;
			storedCrate.saveData = saveData;
			storedCrate.corpType = m_CorpType;
			Visible.InitStored(storedCrate, position, rotation);
			storedCrate.m_ID = id;
			AddStoredVisibleToTile(storedCrate, ObjectTypes.Crate);
		}

		public void AddSavedVisible(ItemTypeInfo typeInfo, Vector3 position, Quaternion rotation, int id)
		{
			switch (typeInfo.ObjectType)
			{
			case ObjectTypes.Chunk:
			{
				StoredChunk storedChunk = new StoredChunk();
				storedChunk.m_ChunkType = (ChunkTypes)typeInfo.ItemType;
				storedChunk.m_Health = -1337f;
				Visible.InitStored(storedChunk, position, rotation);
				storedChunk.m_ID = id;
				AddStoredVisibleToTile(storedChunk, ObjectTypes.Chunk);
				break;
			}
			case ObjectTypes.Block:
			{
				StoredBlock storedBlock = new StoredBlock();
				storedBlock.m_BlockType = (BlockTypes)typeInfo.ItemType;
				storedBlock.m_Health = -1337f;
				Visible.InitStored(storedBlock, position, rotation);
				storedBlock.m_ID = id;
				AddStoredVisibleToTile(storedBlock, ObjectTypes.Block);
				break;
			}
			case ObjectTypes.Scenery:
				d.Assert(condition: false, "ManSaveGame.AddSavedVisible Type Scenery: UNIMPLEMENTED");
				break;
			case ObjectTypes.Waypoint:
			{
				StoredWaypoint storedWaypoint = new StoredWaypoint();
				Visible.InitStored(storedWaypoint, position, rotation);
				storedWaypoint.m_ID = id;
				AddStoredVisibleToTile(storedWaypoint, ObjectTypes.Waypoint);
				break;
			}
			default:
				d.Assert(condition: false, "ManSaveGame.AddSavedVisible Type " + typeInfo.ObjectType.ToString() + " UNIMPLEMENTED");
				break;
			}
		}

		public void StoreLoadedVisible(Visible vis)
		{
			StoredVisible storedVisible = CreateStoredVisible(vis);
			if (storedVisible != null)
			{
				AddStoredVisibleToTile(storedVisible, vis.type);
				return;
			}
			d.LogErrorFormat("ERROR - StoreLoadedVisible Trying to store NULL stored object for visible {0}! That's bad!", (vis != null) ? vis.name : "<null>");
		}

		private void AddStoredVisibleToTile(StoredVisible storedVisible, ObjectTypes objectType)
		{
			if (!m_StoredVisibles.TryGetValue((int)objectType, out var value))
			{
				value = new List<StoredVisible>();
				m_StoredVisibles.Add((int)objectType, value);
			}
			value.Add(storedVisible);
		}

		public void AddPersistentTerrainObjectToSaveData(TerrainObject terrainObjectPrefab, Vector3 position, Quaternion rotation, bool isTracked = false, uint trackedID = 0u)
		{
			if (m_StoredTerrainObjects == null)
			{
				m_StoredTerrainObjects = new List<StoredTerrainObject>();
			}
			if (!terrainObjectPrefab.PrefabGUID.NullOrEmpty())
			{
				StoredTerrainObject item = new StoredTerrainObject
				{
					m_WorldPosition = WorldPosition.FromScenePosition(in position),
					m_Rotation = rotation,
					m_PrefabGUID = terrainObjectPrefab.PrefabGUID,
					m_PrefabName = terrainObjectPrefab.name,
					m_IsTrackedObject = isTracked,
					m_TrackedObjectID = (isTracked ? trackedID : uint.MaxValue),
					m_StoredSceneryState = null
				};
				m_StoredTerrainObjects.Add(item);
			}
			else
			{
				d.LogError("AddPersistentTerrainObjectToSaveData - TerrainObject '" + terrainObjectPrefab.name + "' could not be saved as the PrefabGUID is not set! It's probably not in the TerrainObjectTable!");
			}
		}

		public void RemoveSavedWaypoint(int id)
		{
			bool condition = false;
			int key = 5;
			if (m_StoredVisibles.TryGetValue(key, out var value))
			{
				for (int i = 0; i < value.Count; i++)
				{
					if (value[i].m_ID == id)
					{
						value.RemoveAt(i);
						condition = true;
						break;
					}
				}
			}
			d.Assert(condition, "ManSaveGame.RemoveSavedWaypoint: Failed to find waypoint with id " + id);
		}

		public void RemoveSavedVisible(ObjectTypes visibleType, int visibleID)
		{
			bool condition = false;
			if (m_StoredVisibles.TryGetValue((int)visibleType, out var value))
			{
				for (int i = 0; i < value.Count; i++)
				{
					if (value[i].m_ID == visibleID)
					{
						_ = value[i];
						value.RemoveAt(i);
						condition = true;
						break;
					}
				}
			}
			d.AssertFormat(condition, "ManSaveGame.RemoveSavedVisible: Failed to find stored visible with id {0} in stored tile {1}", visibleID.ToString(), coord);
		}

		public void RemoveSavedTrackedObject(uint trackedObjectID)
		{
			bool condition = false;
			if (m_StoredTerrainObjects != null)
			{
				for (int i = 0; i < m_StoredTerrainObjects.Count; i++)
				{
					if (m_StoredTerrainObjects[i].m_IsTrackedObject && m_StoredTerrainObjects[i].m_TrackedObjectID == trackedObjectID)
					{
						m_StoredTerrainObjects.RemoveAt(i);
						condition = true;
						break;
					}
				}
			}
			d.AssertFormat(condition, "ManSaveGame.RemoveSavedTrackedObject: Failed to find stored terrain object with id {0} in stored tile {1}", trackedObjectID.ToString(), coord);
		}

		public bool TryChangeSavedTechInfo(int visibleID, Action<StoredTech> change, string actionDebugName)
		{
			bool flag = false;
			if (m_StoredVisibles.TryGetValue(1, out var value))
			{
				for (int i = 0; i < value.Count; i++)
				{
					if (value[i].m_ID == visibleID)
					{
						StoredTech obj = value[i] as StoredTech;
						change(obj);
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				d.LogErrorFormat("<b>'{0}</b> failed to find tech {1}", actionDebugName, visibleID);
			}
			return flag;
		}

		public void SetSavedTechAIType(int visibleID, AITreeType aiType)
		{
			bool flag = false;
			if (m_StoredVisibles.TryGetValue(1, out var value))
			{
				for (int i = 0; i < value.Count; i++)
				{
					if (value[i].m_ID == visibleID)
					{
						StoredTech storedTech = value[i] as StoredTech;
						TechAI.SerialData serialData = new TechAI.SerialData();
						serialData.m_AIType = aiType;
						serialData.m_AIVariables = null;
						serialData.m_PathfindingTargetID = -1;
						serialData.Store(storedTech.m_TechData.m_TechSaveState);
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				d.LogErrorFormat("SetSavedTechAIType failed to find tech {0} to rename", visibleID);
			}
		}

		public void RestoreVisibles()
		{
			Tank tank = null;
			bool flag = m_TileSavedVersion < 3;
			for (int i = 0; i < k_RestoreOrder.Count; i++)
			{
				int num = k_RestoreOrder[i];
				if (!m_StoredVisibles.TryGetValue(num, out var value) || value == null)
				{
					continue;
				}
				for (int j = 0; j < value.Count; j++)
				{
					StoredVisible storedVisible = value[j];
					if (flag && num == 1 && storedVisible is StoredTech { m_TeamID: -2 } storedTech)
					{
						TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(storedTech.m_ID);
						if (trackedVisible != null && trackedVisible.RadarType == RadarTypes.Vendor)
						{
							storedVisible.CustomRestoreBehaviour = VendorSpawner.ReplaceVendor;
						}
					}
					RestoreOrDeferLoadingVisible(storedVisible, coord);
				}
			}
			m_TileSavedVersion = 3;
			foreach (int key in Singleton.Manager<ManSaveGame>.inst.m_SerializedVisibleIDLookup.Keys)
			{
				Singleton.Manager<ManVisible>.inst.GetTrackedVisible(key)?.OnRespawn();
			}
			Singleton.Manager<ManSaveGame>.inst.m_SerializedVisibleIDLookup.Clear();
			if ((bool)tank)
			{
				Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(tank);
			}
		}

		public void RestorePersistentTerrainObjects()
		{
			if (m_StoredTerrainObjects == null)
			{
				return;
			}
			foreach (StoredTerrainObject storedTerrainObject in m_StoredTerrainObjects)
			{
				TerrainObject terrainObjectPrefabFromGUID = Singleton.Manager<ManSpawn>.inst.GetTerrainObjectPrefabFromGUID(storedTerrainObject.m_PrefabGUID);
				if (terrainObjectPrefabFromGUID != null)
				{
					Vector3 vector = storedTerrainObject.m_Position + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
					if (storedTerrainObject.m_WorldPosition != default(WorldPosition) && storedTerrainObject.m_Position == Vector3.zero)
					{
						vector = storedTerrainObject.m_WorldPosition.ScenePosition;
					}
					TerrainObject terrainObject = terrainObjectPrefabFromGUID.SpawnFromPrefabAndAddToSaveData(vector, storedTerrainObject.m_Rotation).TerrainObject;
					d.AssertFormat(terrainObject != null, "RestorePersistentTerrainObjects - Failed to spawn TerrainObject {0} at {1} (scene)", terrainObjectPrefabFromGUID.name, vector);
					if (!(terrainObject != null))
					{
						continue;
					}
					if (storedTerrainObject.m_IsTrackedObject)
					{
						TrackedObjectReference trackedObject = Singleton.Manager<ManVisible>.inst.GetTrackedObject(storedTerrainObject.m_TrackedObjectID);
						if (trackedObject != null)
						{
							trackedObject.OnObjectRespawn(terrainObject);
						}
						else
						{
							d.LogErrorFormat("ManSaveGame.RestorePersistentTerrainObjects: Restoring Tracked TerrainObject {0} but ID:{1} isn't in ManVisible", terrainObject.name, storedTerrainObject.m_TrackedObjectID);
						}
					}
					if (storedTerrainObject.m_StoredSceneryState != null)
					{
						Visible visible = terrainObject.visible;
						d.Assert(visible != null && visible.resdisp != null, "Null Visible or ResourceDispenser when restoring saved Persistent Terrain Object with non-null m_StoredSceneryState!");
						if (visible != null && visible.resdisp != null)
						{
							d.Assert(storedTerrainObject.m_StoredSceneryState.sceneryType == (SceneryTypes)visible.ItemType, string.Concat("RestorePersistentTerrainObjects - Stored scenery save data type ('", storedTerrainObject.m_StoredSceneryState.sceneryType, "') does not match spawned scenery type ('", visible.m_ItemType.name, "')!"));
							visible.resdisp.Restore(storedTerrainObject.m_StoredSceneryState.state);
						}
					}
					if (terrainObject.visible.IsNotNull() && terrainObject.visible.resdisp.IsNotNull())
					{
						terrainObject.visible.resdisp.SetAwake(awake: true);
					}
				}
				else
				{
					d.LogError("RestorePersistentTerrainObjects - Failed to find TerrainObject prefab '" + storedTerrainObject.m_PrefabName + "' with GUID '" + storedTerrainObject.m_PrefabGUID + "' while restoring persistent scenery.");
				}
			}
		}
	}

	private class SaveDumper
	{
		private struct Rec
		{
			public int id;

			public string line;
		}

		private StreamWriter m_Writer;

		private List<Rec> m_Recs = new List<Rec>();

		public SaveDumper(StreamWriter writer)
		{
			m_Writer = writer;
		}

		public void StartSection(string title)
		{
			m_Writer.WriteLine(title);
		}

		public void AddVisible(int id, string name, string radarType, Vector3 pos, IntVector2 coord)
		{
			m_Recs.Add(new Rec
			{
				id = id,
				line = id + "\t" + name + "\t" + radarType + "\t" + coord.x + "\t" + coord.y + "\t" + pos.x.ToString("n2") + "\t" + pos.y.ToString("n2") + "\t" + pos.z.ToString("n2")
			});
		}

		public void AddTank(StoredTech tank, string radarType, IntVector2 coord)
		{
			AddVisible(tank.m_ID, tank.m_TechData.Name, radarType, tank.m_WorldPosition.GameWorldPosition, coord);
		}

		public void EndSection()
		{
			if (m_Recs.Count > 0)
			{
				m_Writer.WriteLine("id\tname\tradar type\tx coord\tycoord\tposX\tposY\tposZ");
				m_Recs.Sort((Rec x, Rec y) => x.id.CompareTo(y.id));
				foreach (Rec rec in m_Recs)
				{
					m_Writer.WriteLine(rec.line);
				}
				m_Recs.Clear();
			}
			else
			{
				m_Writer.WriteLine("NONE");
			}
			m_Writer.WriteLine("");
		}
	}

	public PlayerTier m_PlayerTier;

	public int m_InitialNumberOfStoredTiles = 20;

	public int m_InitialStoredTileListSize = 100;

	public int m_TankPresetPoolSize = 100;

	public int m_DefaultPresetBlockListSize = 100;

	public static Func<int, bool> ShouldStoreTechSeparatelyHandler;

	public static Action<Visible> DoStoreTechSeparatelyHandler;

	public const string kFixedSlotNamePrefix = "Slot ";

	private SaveData m_SaveData;

	private string m_LastSavedName;

	private Stack<StoredTile> storedTilePool;

	private static List<StoredVisible> m_MultiplayerVisiblesToSpawn = new List<StoredVisible>();

	private const string k_SaveFileExtension = "sav";

	public const string k_SaveExtension = ".sav";

	private const float kAutosaveBackupIntervalSeconds = 600f;

	private const int kMaxAutosaveBackupupHistoryCount = 3;

	private DateTime m_LastAutosaveBackupTime;

	private HashSet<StoredTech> m_ReportedMismatchTech = new HashSet<StoredTech>();

	private static Dictionary<IntVector2, string> s_StoredTileJSONToReturn = new Dictionary<IntVector2, string>();

	public const string k_SteamMetaDataName = "SteamVersion";

	public const string kGameSavePathBase = "/Saves/";

	private static string s_CachedSaveDataFolderPathRoot = null;

	private static string s_CachedSaveDataFolderPath = null;

	public const int m_GameScreenshotSizePC = 256;

	public const int m_GameScreenshotSizeConsole = 128;

	public const int m_ScreenshotQuality = 70;

	public const string m_EncryptPassword = "ctvyuaidbqwi298384!84dxw21";

	private Dictionary<int, Visible> m_SerializedVisibleIDLookup = new Dictionary<int, Visible>(500);

	public static readonly List<int> k_RestoreOrder = new List<int> { 2, 4, 1, 6, 5 };

	public static readonly List<int> k_StoreOrder = new List<int> { 4, 1, 6, 2, 5 };

	private static JsonSerializerSettings s_JSONSerialisationSettings = new JsonSerializerSettings
	{
		TypeNameHandling = TypeNameHandling.Auto,
		TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
		Formatting = Formatting.None,
		Error = delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
		{
			d.LogErrorFormat("JSON Error: {0}  Path={1}   FullException= {2}", args.ErrorContext.Error.Message, args.ErrorContext.Path, args.ErrorContext.Error);
		}
	};

	private static char[] s_TrimChars = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

	private static List<string> s_SerializableData = new List<string>(32);

	private static Task s_LastSaveOpInProgress = null;

	private static StringBuilder s_HashStringBuilder = new StringBuilder();

	public const string deprecatedSaveDataPathPrefix = "/../";

	public static bool Storing { get; set; }

	public static bool ShouldStore { get; set; } = true;

	public State CurrentState
	{
		get
		{
			if (m_SaveData != null)
			{
				return m_SaveData.State;
			}
			d.LogError("ManSaveGame.CurrentState - trying to access game state on non-existant save data. Investigate race condition");
			return null;
		}
	}

	public static WorldGenVersionData CurrentSavedWorldGenVersion => Singleton.Manager<ManSaveGame>.inst.CurrentState.WorldGenVersionData;

	public static bool DeferLoadingForMP
	{
		get
		{
			if (Singleton.Manager<ManGameMode>.inst.IsCurrentModeMultiplayer())
			{
				return !NetworkServer.active;
			}
			return false;
		}
	}

	public static bool HasQueuedSpawns => m_MultiplayerVisiblesToSpawn.Count > 0;

	public static bool UseFixedSlots => SKU.SwitchUI;

	public static int NumFixedSlots => 3;

	public static string AutoSaveName
	{
		get
		{
			if (!SKU.IsNetEase)
			{
				return "Autosave";
			}
			return "自动存档";
		}
	}

	public static int GameScreenshotSize
	{
		get
		{
			if (!SKU.ConsoleUI)
			{
				return 256;
			}
			return 128;
		}
	}

	private void OnTileStartPopulating(WorldTile tile)
	{
		if (CurrentState != null)
		{
			CurrentState.AssignSceneryUIDBucketToTile(tile);
		}
	}

	private void OnTileDepopulated(WorldTile tile)
	{
		if (CurrentState != null)
		{
			CurrentState.ClearTileSceneryUIDBucket(tile);
		}
	}

	private void OnModeSwitch()
	{
		ShouldStore = true;
		ShouldStoreTechSeparatelyHandler = null;
		DoStoreTechSeparatelyHandler = null;
		m_LastAutosaveBackupTime = DateTime.Now;
	}

	public static StoredVisible CreateStoredVisible(Visible visible)
	{
		StoredVisible storedVisible = null;
		switch (visible.m_ItemType.ObjectType)
		{
		case ObjectTypes.Vehicle:
			storedVisible = new StoredTech();
			break;
		case ObjectTypes.Block:
			storedVisible = new StoredBlock();
			break;
		case ObjectTypes.Chunk:
			storedVisible = new StoredChunk();
			break;
		case ObjectTypes.Waypoint:
			storedVisible = new StoredWaypoint();
			break;
		case ObjectTypes.Crate:
			storedVisible = new StoredCrate();
			break;
		default:
			d.LogError(string.Concat("ManSaveGame.CreateStoredVisible: Visible ", visible.name, " with type ", visible.m_ItemType.ObjectType, " is not currently supported"));
			break;
		}
		storedVisible?.Store(visible);
		return storedVisible;
	}

	private static Visible RestoreVisible(StoredVisible storedVisible)
	{
		Visible visible = null;
		if (storedVisible.CanRestore() || Singleton.Manager<ManVisible>.inst.GetTrackedVisible(storedVisible.m_ID) != null)
		{
			Vector3 v = storedVisible.GetBackwardsCompatiblePosition();
			bool flag = v.IsNaN();
			if (!flag)
			{
				flag = v.y < Globals.inst.m_VisibleEmergencyKillHeight || v.y > Globals.inst.m_VisibleEmergencyKillMaxHeight;
			}
			if (!flag)
			{
				TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(storedVisible.m_ID);
				if ((trackedVisible != null && trackedVisible.visible != null) || Singleton.Manager<ManSaveGame>.inst.m_SerializedVisibleIDLookup.ContainsKey(storedVisible.m_ID))
				{
					d.LogErrorFormat("StoredTech.SpawnAndRestore - Trying to restore Visible with ID {0} type {2}, but already have an object with the same ID in the world {1}!", storedVisible.m_ID, (trackedVisible != null && trackedVisible.visible != null) ? trackedVisible.visible.name : Singleton.Manager<ManSaveGame>.inst.m_SerializedVisibleIDLookup[storedVisible.m_ID].name, storedVisible.GetType());
				}
				else
				{
					visible = ((storedVisible.CustomRestoreBehaviour == null) ? storedVisible.SpawnAndRestore() : storedVisible.CustomRestoreBehaviour(storedVisible));
					if (visible != null)
					{
						d.AssertFormat(visible.ID == storedVisible.m_ID, "ManSaveGame.RestoreVisible - Visible {0} should have been restored with ID {1}, but has ID {2}!", visible.name, storedVisible.m_ID, visible.ID);
					}
				}
			}
			if (visible == null)
			{
				d.LogError("ManSaveGame.RestoreVisible - Failed to restore Visible (ID " + storedVisible.m_ID + ")");
				if (Singleton.Manager<ManSaveGame>.inst.CurrentState.m_VisiblesFailedToRestore == null)
				{
					Singleton.Manager<ManSaveGame>.inst.CurrentState.m_VisiblesFailedToRestore = new List<StoredVisible>();
				}
				Singleton.Manager<ManSaveGame>.inst.CurrentState.m_VisiblesFailedToRestore.Add(storedVisible);
			}
		}
		return visible;
	}

	private static void RestoreOrDeferLoadingVisible(StoredVisible storedVisible, IntVector2 tileCoord)
	{
		bool flag = false;
		if (storedVisible.m_TileOverlapDirection != IntVector2.zero)
		{
			flag = !Singleton.Manager<ManWorld>.inst.TileManager.CheckAllOverlappedNeighboursLoaded(tileCoord, storedVisible.m_TileOverlapDirection, testCentreTileLoadState: false);
		}
		if (flag)
		{
			WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in tileCoord);
			if (worldTile != null)
			{
				worldTile.AddStoredVisibleWaitingToLoad(storedVisible);
				if (storedVisible is StoredTech storedTech)
				{
					Singleton.Manager<ManTechs>.inst.AddOverlappingTechData(storedTech);
				}
			}
			else
			{
				d.LogErrorFormat("RestoreOrDeferLoadingVisible - Trying to defer loading of visible on tile {0}, but did not find a valid tile at that coord!?", tileCoord);
			}
		}
		else if (DeferLoadingForMP)
		{
			AddDeferredLoadForMultiplayerVisible(storedVisible);
		}
		else
		{
			RestoreVisible(storedVisible);
		}
	}

	public static void TryRestoreStoredVisibles(List<StoredVisible> storedVisiblesToRestore, IntVector2 tileCoord)
	{
		bool flag = false;
		for (int num = storedVisiblesToRestore.Count - 1; num >= 0; num--)
		{
			StoredVisible storedVisible = storedVisiblesToRestore[num];
			if (Singleton.Manager<ManWorld>.inst.TileManager.CheckAllOverlappedNeighboursLoaded(tileCoord, storedVisible.m_TileOverlapDirection))
			{
				if (DeferLoadingForMP)
				{
					AddDeferredLoadForMultiplayerVisible(storedVisible);
				}
				else
				{
					Visible visible = RestoreVisible(storedVisible);
					flag = flag || visible != null;
				}
				storedVisiblesToRestore.RemoveAt(num);
			}
		}
		if (!flag)
		{
			return;
		}
		foreach (int key in Singleton.Manager<ManSaveGame>.inst.m_SerializedVisibleIDLookup.Keys)
		{
			Singleton.Manager<ManVisible>.inst.GetTrackedVisible(key)?.OnRespawn();
		}
		Singleton.Manager<ManSaveGame>.inst.m_SerializedVisibleIDLookup.Clear();
	}

	private static void AddDeferredLoadForMultiplayerVisible(StoredVisible storedVisible)
	{
		m_MultiplayerVisiblesToSpawn.Add(storedVisible);
		if (storedVisible is StoredTech storedTech)
		{
			Singleton.Manager<ManTechs>.inst.AddOverlappingTechData(storedTech);
		}
	}

	public static void SetCustomTechStoreHandlers(Func<int, bool> shouldStoreFunc, Action<Visible> performStorageHandler)
	{
		ShouldStoreTechSeparatelyHandler = shouldStoreFunc;
		DoStoreTechSeparatelyHandler = performStorageHandler;
	}

	public static string GetSaveDataFolder()
	{
		return GetSaveDataFolderInternal(getRoot: false);
	}

	public static string GetSaveDataFolderRoot()
	{
		return GetSaveDataFolderInternal(getRoot: true);
	}

	public static string GetDeprecatedSaveDataFolder()
	{
		return GetSaveDataFolderInternal(getRoot: true);
	}

	private static string GetSaveDataFolderInternal(bool getRoot)
	{
		if (s_CachedSaveDataFolderPathRoot == null)
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			s_CachedSaveDataFolderPathRoot = folderPath + "/My Games/TerraTech";
			if (folderPath.Length == 0)
			{
				if (Singleton.Manager<Localisation>.inst != null)
				{
					string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 20);
					string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 17);
					string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 18);
					localisedString2 = UIHyperlink.ConvertLinkToTMProLinkCode(localisedString2, localisedString3);
					localisedString = string.Format(localisedString, localisedString2);
					Singleton.Manager<ManUI>.inst.ShowErrorPopup(localisedString);
				}
				else
				{
					d.LogWarning("Tried to show a user error before Localisation manager is initialised skipping");
				}
				d.LogError("Error, My Documents path is empty, is your documents folder blocked by anti virus ?\nSaving to: \"" + s_CachedSaveDataFolderPathRoot + "\" instead");
			}
		}
		if (!getRoot && s_CachedSaveDataFolderPathRoot != null && s_CachedSaveDataFolderPath == null)
		{
			if (SKU.IsSteam && Singleton.Manager<ManSteamworks>.inst != null && Singleton.Manager<ManSteamworks>.inst.Inited)
			{
				s_CachedSaveDataFolderPath = Path.Combine(s_CachedSaveDataFolderPathRoot, SteamUser.GetSteamID().ToString());
			}
			else if (SKU.IsEpicGS && Singleton.Manager<ManEOS>.inst != null && Singleton.Manager<ManEOS>.inst.HasPlayerID)
			{
				s_CachedSaveDataFolderPath = Path.Combine(s_CachedSaveDataFolderPathRoot, Singleton.Manager<ManEOS>.inst.PlayerIDString);
			}
			else
			{
				s_CachedSaveDataFolderPath = Path.Combine(s_CachedSaveDataFolderPathRoot, "Guest");
			}
		}
		if (!getRoot)
		{
			return s_CachedSaveDataFolderPath;
		}
		return s_CachedSaveDataFolderPathRoot;
	}

	public static string GetCurrentUserGameSaveDir(ManGameMode.GameType gameType)
	{
		return GetUserGameSaveDir(Singleton.Manager<ManProfile>.inst.GetCurrentUser(), gameType);
	}

	public static string GetUserGameSaveDir(ManProfile.Profile user, ManGameMode.GameType gameType)
	{
		d.Assert(user != null, "ManSaveGame.GetUserSaveDir - User was Null!");
		string text = GetSaveDataFolder() + "/Saves/";
		if (user != null)
		{
			text = Path.Combine(text, user.m_SaveName);
			string gameModeString = GetGameModeString(gameType);
			if (gameModeString != null)
			{
				text = Path.Combine(text, gameModeString);
			}
		}
		try
		{
			Directory.CreateDirectory(text);
		}
		catch (Exception ex)
		{
			d.LogError("Failed to create user save game directory in ManSaveGame::GetUserGameSaveDir: " + ex);
		}
		return text;
	}

	public static string GetUserGameSaveDir(ManProfile.Profile user)
	{
		d.Assert(user != null, "ManSaveGame.GetUserSaveDir - User was Null!");
		string text = GetSaveDataFolder() + "/Saves/";
		if (user != null)
		{
			text = Path.Combine(text, user.m_SaveName);
		}
		try
		{
			Directory.CreateDirectory(text);
		}
		catch (Exception ex)
		{
			d.LogError("Failed to create user save game directory in ManSaveGame::GetUserGameSaveDir: " + ex);
		}
		return text;
	}

	public static string GetProfileSaveDir()
	{
		string text = GetSaveDataFolder() + "/Saves/";
		try
		{
			Directory.CreateDirectory(text);
			return text;
		}
		catch (Exception ex)
		{
			if (Singleton.Manager<Localisation>.inst != null)
			{
				string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 21);
				string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 17);
				string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 18);
				localisedString2 = UIHyperlink.ConvertLinkToTMProLinkCode(localisedString2, localisedString3);
				localisedString = string.Format(localisedString, localisedString2);
				Singleton.Manager<ManUI>.inst.ShowErrorPopup(localisedString);
			}
			else
			{
				d.LogWarning("Tried to show a user error before Localisation manager is initialised skipping");
			}
			d.LogError("ManSaveGame::GetProfileSaveDir() Failed to create save game directory: " + ex);
			return "";
		}
	}

	public static string GetDefaultSaveName(ManGameMode.GameType gameType)
	{
		LocalisationEnums.NewMenuMain stringID;
		switch (gameType)
		{
		case ManGameMode.GameType.MainGame:
			stringID = LocalisationEnums.NewMenuMain.CampaignSave;
			break;
		case ManGameMode.GameType.RaD:
			stringID = LocalisationEnums.NewMenuMain.RandDSave;
			break;
		case ManGameMode.GameType.Creative:
			stringID = LocalisationEnums.NewMenuMain.CreativeSave;
			break;
		case ManGameMode.GameType.CoOpCreative:
			stringID = LocalisationEnums.NewMenuMain.CoopCreativeSave;
			break;
		case ManGameMode.GameType.CoOpCampaign:
			stringID = LocalisationEnums.NewMenuMain.CoopCampaignSave;
			break;
		default:
			stringID = LocalisationEnums.NewMenuMain.RandDSave;
			d.LogError(string.Concat("ManSaveGame.GetDefaultSaveName - The specified GameType (", gameType, ") does not map to a valid directory to save to!"));
			break;
		}
		return string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, (int)stringID), "");
	}

	public static string GetGameModeString(ManGameMode.GameType gameType, bool ignoreError = false)
	{
		string text = null;
		switch (gameType)
		{
		case ManGameMode.GameType.MainGame:
			text = "Campaign";
			break;
		case ManGameMode.GameType.RaD:
			text = "RandD";
			break;
		case ManGameMode.GameType.Creative:
			text = "Creative";
			break;
		case ManGameMode.GameType.CoOpCreative:
			text = "CoopCreative";
			break;
		case ManGameMode.GameType.CoOpCampaign:
			text = "CoopCampaign";
			break;
		default:
			text = "Default";
			if (!ignoreError)
			{
				d.LogError(string.Concat("ManSaveGame.GetSaveSubFolder - The specified GameType (", gameType, ") does not map to a valid directory to save to!"));
			}
			break;
		}
		return text;
	}

	public static string GetNextAvailableSaveName(ManGameMode.GameType gameType, string saveName)
	{
		return GetNextAvailableFileName(saveName, (string fileName) => CreateGameSaveFilePath(gameType, fileName));
	}

	public static string GetNextAvailableFileName(string saveName, Func<string, string> filePathCreator)
	{
		string text = saveName;
		int num = 0;
		string arg = saveName.TrimEnd(s_TrimChars);
		string path = filePathCreator(saveName);
		while (File.Exists(path))
		{
			num++;
			text = $"{arg}{num}";
			path = filePathCreator(text);
		}
		return text;
	}

	public static string CreateGameSaveFilePath(ManGameMode.GameType gameType, string saveName)
	{
		return CreateFilePath(GetCurrentUserGameSaveDir(gameType), saveName);
	}

	public static string CreateFilePath(string folderPath, string fileName, string fileExtension = "sav")
	{
		string text = Path.Combine(folderPath, fileName);
		if (!fileExtension.NullOrEmpty())
		{
			text = text + "." + fileExtension;
		}
		return text;
	}

	public bool Save(ManGameMode.GameType gameType, string saveName, bool async = false)
	{
		bool result = false;
		if (m_SaveData != null && m_SaveData.SaveInfo != null)
		{
			m_SaveData.SaveInfo.DestroyTexture();
		}
		string path = CreateGameSaveFilePath(gameType, saveName);
		m_SaveData.SaveInfo.m_SaveName = saveName;
		if (IsSaveNameAutoSave(saveName))
		{
			m_SaveData.SaveInfo.m_SaveNameAncestor = m_LastSavedName;
		}
		else
		{
			m_LastSavedName = saveName;
			m_SaveData.SaveInfo.m_SaveNameAncestor = null;
		}
		Task task = s_LastSaveOpInProgress;
		if (async)
		{
			Task task2 = Task.Run(delegate
			{
				CreateRequiredBackups(saveName, path);
			});
			task = ((s_LastSaveOpInProgress != null) ? Task.WhenAll(s_LastSaveOpInProgress, task2) : task2);
		}
		else
		{
			CreateRequiredBackups(saveName, path);
		}
		if (task != null)
		{
			Task.WaitAll(task);
		}
		try
		{
			SaveSaveData(m_SaveData, path, async);
			result = true;
		}
		catch (Exception ex)
		{
			d.LogErrorFormat("ERROR in ManSaveGame.Save: {0}", ex.Message);
			DisplayFailedToSaveErrorPopup();
		}
		return result;
	}

	private static void DisplayFailedToSaveErrorPopup()
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 21);
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 17);
		string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 18);
		localisedString2 = UIHyperlink.ConvertLinkToTMProLinkCode(localisedString2, localisedString3);
		localisedString = string.Format(localisedString, localisedString2);
		Singleton.Manager<ManUI>.inst.ShowErrorPopup(localisedString);
	}

	public void CheckTrackedVisiblesExistOnce()
	{
		Dictionary<int, IntVector2> dictionary = new Dictionary<int, IntVector2>();
		foreach (IntVector2 key in m_SaveData.State.m_StoredTiles.Keys)
		{
			CheckTileForTrackedVisibles(key, m_SaveData.State.m_StoredTiles[key], dictionary);
		}
		if (m_SaveData.State.m_StoredTilesJSON != null)
		{
			foreach (IntVector2 key2 in m_SaveData.State.m_StoredTilesJSON.Keys)
			{
				string rawJson = m_SaveData.State.m_StoredTilesJSON[key2];
				StoredTile objectToLoad = null;
				bool assertOnFail = false;
				bool validate = false;
				LoadObjectFromRawJson(ref objectToLoad, rawJson, assertOnFail, validate);
				CheckTileForTrackedVisibles(key2, objectToLoad, dictionary);
			}
		}
		foreach (TrackedVisible allTrackedVisible in Singleton.Manager<ManVisible>.inst.AllTrackedVisibles)
		{
			if (allTrackedVisible.ObjectType == ObjectTypes.Vehicle && !dictionary.ContainsKey(allTrackedVisible.ID) && (ShouldStoreTechSeparatelyHandler == null || !ShouldStoreTechSeparatelyHandler(allTrackedVisible.ID)))
			{
				if (IsSavedTrackedVisible(allTrackedVisible))
				{
					d.LogErrorFormat("ManSaveGame.ChecktrackedVisiblesExistOnce didn't find a tracked visible with ID {0} in any tile.{1}", allTrackedVisible.ID, (allTrackedVisible.visible != null) ? (" Visible name: " + allTrackedVisible.visible.name) : string.Empty);
				}
				else
				{
					d.LogErrorFormat("ManSaveGame.ChecktrackedVisiblesExistOnce found a tech that is marked as 'not saved' with ID {0}{1}. Tech should always be tracked and saved!", allTrackedVisible.ID, (allTrackedVisible.visible != null) ? (" name: " + allTrackedVisible.visible.name) : string.Empty);
				}
			}
		}
	}

	private bool IsSavedTrackedVisible(TrackedVisible tv)
	{
		bool result = true;
		foreach (int unsavedTrackedVisibleID in Singleton.Manager<ManVisible>.inst.UnsavedTrackedVisibleIDs)
		{
			if (tv.ID == unsavedTrackedVisibleID)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private void CheckTileForTrackedVisibles(IntVector2 coord, StoredTile storedTile, Dictionary<int, IntVector2> seenTracked)
	{
		foreach (List<StoredVisible> value in storedTile.m_StoredVisibles.Values)
		{
			foreach (StoredVisible item in value)
			{
				int iD = item.m_ID;
				if (Singleton.Manager<ManVisible>.inst.GetTrackedVisible(iD) != null)
				{
					if (!seenTracked.ContainsKey(iD))
					{
						seenTracked.Add(iD, coord);
						continue;
					}
					d.LogErrorFormat("ManSaveGame.CheckTileForTrackedVisibles found visible with ID {0} in tiles {1} and {2}", iD, coord, seenTracked[iD]);
				}
			}
		}
	}

	public static void SaveSaveData(SaveData saveData, string filePath, bool asyncFileIO = false)
	{
		saveData.GatherSerializableData(s_SerializableData);
		if (asyncFileIO)
		{
			s_LastSaveOpInProgress = Task.Run(delegate
			{
				try
				{
					WriteSaveDataToDisk(s_SerializableData, filePath);
				}
				catch (Exception ex)
				{
					d.LogErrorFormat("ERROR in ManSaveGame.Save: {0}", ex);
					Singleton.instance.DoOnNextFrame(DisplayFailedToSaveErrorPopup);
				}
				finally
				{
					s_LastSaveOpInProgress = null;
				}
			});
		}
		else
		{
			WriteSaveDataToDisk(s_SerializableData, filePath);
		}
	}

	private static void WriteSaveDataToDisk(List<string> data, string filePath)
	{
		string text = null;
		try
		{
			do
			{
				text = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".tmp");
			}
			while (File.Exists(text));
			using (FileStream stream = File.Create(text))
			{
				using GZipStream stream2 = new GZipStream(stream, CompressionMode.Compress);
				using StreamWriter streamWriter = new StreamWriter(stream2);
				foreach (string datum in data)
				{
					streamWriter.WriteLine(datum);
				}
			}
			File.Copy(text, filePath, overwrite: true);
		}
		finally
		{
			s_SerializableData.Clear();
			try
			{
				File.Delete(text);
			}
			catch (Exception ex)
			{
				d.LogErrorFormat("ERROR in ManSaveGame.Save while deleting: {0}", ex.Message);
			}
		}
	}

	private void CreateRequiredBackups(string saveName, string filePath)
	{
		if (DebugUtil.DebugSavesEnabled && File.Exists(filePath))
		{
			BackupSaveFileVisibleIngame(filePath);
		}
		if (IsSaveNameAutoSave(saveName))
		{
			DateTime now = DateTime.Now;
			if ((float)(now - m_LastAutosaveBackupTime).TotalSeconds > 600f)
			{
				BackupSaveFile(filePath, 3);
				m_LastAutosaveBackupTime = now;
			}
		}
	}

	private void BackupSaveFile(string fullPathWithFilename, int maxHistoryCount)
	{
		try
		{
			string directoryName = Path.GetDirectoryName(fullPathWithFilename);
			string extension = Path.GetExtension(fullPathWithFilename);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullPathWithFilename);
			int num = maxHistoryCount;
			for (int i = 1; maxHistoryCount == -1 || i <= maxHistoryCount; i++)
			{
				if (!File.Exists(GetBackupFilePath(directoryName, fileNameWithoutExtension, extension, i)))
				{
					num = i - 1;
					break;
				}
			}
			for (int num2 = num; num2 >= 1; num2--)
			{
				string text = GetBackupFilePath(directoryName, fileNameWithoutExtension, extension, num2);
				if (!File.Exists(text))
				{
					break;
				}
				string destFileName = GetBackupFilePath(directoryName, fileNameWithoutExtension, extension, num2 + 1);
				File.Move(text, destFileName);
			}
			string destFileName2 = GetBackupFilePath(directoryName, fileNameWithoutExtension, extension, 1);
			File.Copy(fullPathWithFilename, destFileName2);
			if (maxHistoryCount != -1)
			{
				string path = GetBackupFilePath(directoryName, fileNameWithoutExtension, extension, maxHistoryCount + 1);
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
		}
		catch (Exception message)
		{
			d.LogError(message);
		}
		static string GetBackupFilePath(string dir, string file, string ex, int backupId)
		{
			return dir + Path.DirectorySeparatorChar + file + $"{ex}.bak_{backupId}";
		}
	}

	private void BackupSaveFileVisibleIngame(string fullPathWithFilename)
	{
		try
		{
			string directoryName = Path.GetDirectoryName(fullPathWithFilename);
			string extension = Path.GetExtension(fullPathWithFilename);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullPathWithFilename);
			bool flag = false;
			int num = 1;
			string text = "";
			while (!flag)
			{
				text = directoryName + Path.DirectorySeparatorChar + fileNameWithoutExtension + "_h" + num + extension;
				flag = !File.Exists(text);
				num++;
			}
			File.Copy(fullPathWithFilename, text);
		}
		catch (Exception message)
		{
			d.LogError(message);
		}
	}

	public static bool RenameUserFolder(ManProfile.Profile profile)
	{
		string text = GetSaveDataFolder() + "/Saves/";
		string text2 = GetSaveDataFolder() + "/Saves/";
		if (profile != null)
		{
			text = text + profile.m_Name + "/";
			text2 = text2 + profile.m_SaveName + "/";
			try
			{
				Directory.Move(text, text2);
			}
			catch
			{
				return false;
			}
			return true;
		}
		return false;
	}

	public static bool SaveObject(object objectToSave, string filePath)
	{
		d.Log("[ManSaveGame] Trying to save object at path = " + filePath);
		bool result = false;
		string text = SaveObjectToRawJson(objectToSave);
		if (text != null)
		{
			string text2 = null;
			try
			{
				string text3 = null;
				do
				{
					text3 = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".tmp");
				}
				while (File.Exists(text3));
				using (FileStream stream = File.Create(text3))
				{
					text2 = text3;
					using GZipStream stream2 = new GZipStream(stream, CompressionMode.Compress);
					using StreamWriter streamWriter = new StreamWriter(stream2);
					streamWriter.Write(text);
				}
				File.Copy(text2, filePath, overwrite: true);
				result = true;
			}
			catch (Exception ex)
			{
				d.LogError("ERROR in ManSaveGame.SaveObject: " + ex.Message);
			}
			if (!text2.NullOrEmpty())
			{
				try
				{
					File.Delete(text2);
				}
				catch (Exception ex2)
				{
					d.LogError("Error deleting temporary file in ManSaveGame.SaveObject. The file will still be saved and copied (granted no previous exceptions), but have left an orphaned temp file!. " + ex2.Message);
				}
			}
		}
		return result;
	}

	public static byte[] SaveObjectToBytes(object objectToSave, int initialCapacitySizeHint)
	{
		byte[] array = null;
		string value = SaveObjectToRawJson(objectToSave);
		using MemoryStream memoryStream = new MemoryStream((initialCapacitySizeHint > 0) ? initialCapacitySizeHint : 1048576);
		Stream stream = memoryStream;
		using (stream = new GZipStream(memoryStream, CompressionMode.Compress))
		{
			using BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(value);
		}
		return memoryStream.ToArray();
	}

	public static string SaveObjectToRawJson(object objectToSave)
	{
		string text = null;
		try
		{
			text = JsonConvert.SerializeObject(objectToSave, s_JSONSerialisationSettings);
		}
		catch (Exception ex)
		{
			d.LogError("SaveObjectToRawJson: Exception=" + ex.Message + " objectToSaveType=" + objectToSave.GetType().Name);
			text = null;
		}
		if (text != null && text == "null")
		{
			text = null;
		}
		return text;
	}

	public static bool SavedInVersionPriorTo(int oldVersion)
	{
		if (Singleton.Manager<ManSaveGame>.inst != null && Singleton.Manager<ManSaveGame>.inst.CurrentState.m_SaveVersion != 0 && Singleton.Manager<ManSaveGame>.inst.CurrentState.m_SaveVersion != -1)
		{
			return Singleton.Manager<ManSaveGame>.inst.CurrentState.m_SaveVersion < oldVersion;
		}
		return false;
	}

	public bool Load(ManGameMode.GameType gameType, string saveName, string saveWorkshopPath = null)
	{
		bool result = false;
		SaveData saveData = LoadSaveData((saveWorkshopPath != null) ? saveWorkshopPath : CreateGameSaveFilePath(gameType, saveName), loadInfoOnly: false, assertOnFail: true, validate: true);
		if (saveData != null && saveData.SaveInfo != null && saveData.State != null && saveData.SaveInfo.CanLoadSave())
		{
			result = true;
			CompleteLoad(saveData);
		}
		return result;
	}

	public void LoadAsync(ManGameMode.GameType gameType, string saveName, Action<bool> callback)
	{
		d.Log("[ManSaveGame] LoadAsync gameType " + gameType.ToString() + " saveName " + saveName);
		string directory = CreateGameSaveFilePath(gameType, saveName);
		Action<SaveData> callback2 = delegate(SaveData loadedData)
		{
			bool flag = loadedData != null && loadedData.SaveInfo != null && loadedData.State != null;
			if (flag)
			{
				CompleteLoad(loadedData);
			}
			callback?.Invoke(flag);
		};
		LoadSaveDataAsync(directory, saveName, loadInfoOnly: false, callback2);
	}

	private void CompleteLoad(SaveData loadedData)
	{
		if (m_SaveData != null && m_SaveData.SaveInfo != null)
		{
			m_SaveData.SaveInfo.DestroyTexture();
		}
		m_SaveData = loadedData;
		if (!IsSaveNameAutoSave(m_SaveData.SaveInfo.m_SaveName))
		{
			m_LastSavedName = m_SaveData.SaveInfo.m_SaveName;
		}
		else
		{
			m_LastSavedName = m_SaveData.SaveInfo.m_SaveNameAncestor;
		}
	}

	public static SaveInfo LoadWorkshopSaveDataInfo(FileInfo file, ManGameMode.GameType gameType)
	{
		SaveData saveData = LoadSaveData(file.FullName, loadInfoOnly: true, assertOnFail: false);
		if (saveData != null)
		{
			saveData.SaveInfo.IsWorkshopSave = true;
		}
		if (saveData == null || saveData.SaveInfo.m_GameType != gameType)
		{
			return null;
		}
		return saveData.SaveInfo;
	}

	public static void LoadSaveDataInfoAsync(ManGameMode.GameType gameType, string saveName, Action<SaveInfo> callback)
	{
		if (callback == null)
		{
			d.LogError("[ManSaveGame] LoadSaveDataInfoAsync - Function not supplied with a valid callback - Exiting out!");
		}
		else
		{
			callback(LoadSaveData(CreateGameSaveFilePath(gameType, saveName), loadInfoOnly: true)?.SaveInfo);
		}
	}

	public static SaveData LoadSaveData(string path, bool loadInfoOnly = false, bool assertOnFail = true, bool validate = false)
	{
		d.Log("[ManSaveGame] LoadSaveData path " + path);
		SaveData saveData = new SaveData();
		try
		{
			using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
			{
				using GZipStream stream2 = new GZipStream(stream, CompressionMode.Decompress);
				using StreamReader streamReader = new StreamReader(stream2);
				saveData.Deserialize(streamReader, loadInfoOnly, assertOnFail, validate: false);
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
			saveData.SaveInfo.m_SaveName = fileNameWithoutExtension;
			saveData.SaveInfo.FullFilePath = path;
			if (!Singleton.Manager<ManSaveGame>.inst.IsSaveNameAutoSave(fileNameWithoutExtension))
			{
				saveData.SaveInfo.m_SaveNameAncestor = null;
			}
		}
		catch (Exception ex)
		{
			d.LogError("ManSaveGame.LoadSaveData: Error loading save file \"" + path + "\" - " + ex.ToString());
			saveData = null;
		}
		return saveData;
	}

	public static void LoadSaveDataAsync(string directory, string saveName, bool loadInfoOnly, Action<SaveData> callback)
	{
		SaveDataConsoles.LoadData(saveName, directory, delegate(bool success, byte[] result)
		{
			SaveData saveData = null;
			if (result != null)
			{
				using MemoryStream memoryStream = new MemoryStream(result, writable: false);
				Stream stream = memoryStream;
				using (stream = new GZipStream(memoryStream, CompressionMode.Decompress))
				{
					using StreamReader streamReader = new StreamReader(stream);
					saveData = new SaveData();
					saveData.Deserialize(streamReader, loadInfoOnly, assertOnFail: false, validate: false);
				}
			}
			if (callback != null)
			{
				if (saveData != null && saveData.SaveInfo != null)
				{
					saveData.SaveInfo.m_SaveName = saveName;
				}
				callback(saveData);
			}
			else
			{
				d.LogError("[ManSaveGame] LoadSaveDataAsync, no callback?");
			}
		});
	}

	public static bool LoadObject<T>(ref T objectToLoad, string path, bool assertOnFail = true, bool validate = false)
	{
		bool result = false;
		if (File.Exists(path))
		{
			try
			{
				using FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
				using GZipStream stream2 = new GZipStream(stream, CompressionMode.Decompress);
				using StreamReader streamReader = new StreamReader(stream2);
				string rawJson = streamReader.ReadToEnd();
				LoadObjectFromRawJson(ref objectToLoad, rawJson, assertOnFail, validate);
				result = true;
			}
			catch (Exception arg)
			{
				d.LogError($"ManSaveGame.LoadObject Caught Exception: {arg}");
				result = false;
			}
		}
		else
		{
			d.Assert(!assertOnFail);
		}
		return result;
	}

	public static void LoadObjectFromBytes<T>(ref T objectToLoad, byte[] bytes, bool assertOnFail = true, bool validate = false)
	{
		string text = string.Empty;
		try
		{
			using MemoryStream memoryStream = new MemoryStream(bytes, writable: false);
			Stream stream = memoryStream;
			using (stream = new GZipStream(memoryStream, CompressionMode.Decompress))
			{
				using BinaryReader binaryReader = new BinaryReader(stream);
				text = binaryReader.ReadString();
			}
		}
		catch (Exception ex)
		{
			d.LogErrorFormat("[ManSaveGame]:LoadObjectFromBytes, Caught exception: {0}", ex.Message);
		}
		if (text != null && text.Length > 0)
		{
			LoadObjectFromRawJson(ref objectToLoad, text, assertOnFail, validate);
		}
		else
		{
			d.LogError("[ManSaveGame]:LoadObjectFromBytes, jasonData is NULL or empty");
		}
	}

	public static void LoadObjectFromRawJson<T>(ref T objectToLoad, string rawJson, bool assertOnFail = true, bool validate = false)
	{
		bool flag = true;
		if (validate)
		{
			d.LogError("[ManSaveGame]:LoadObjectFromRawJson - JSON validation not currently supported!");
		}
		try
		{
			T val = JsonConvert.DeserializeObject<T>(rawJson, s_JSONSerialisationSettings);
			if (val != null)
			{
				objectToLoad = val;
			}
			else
			{
				d.LogError("[ManSaveGame]:LoadObjectFromRawJson, newObject is NULL  RawJSON=" + rawJson);
				flag = false;
			}
		}
		catch (Exception ex)
		{
			d.LogError("LoadobjectFromRawJson:  Exception=" + ex.Message + " ExceptionString=" + ex.ToString() + " RawJson=" + ((rawJson == null) ? "NULL" : rawJson));
		}
		if (!flag)
		{
			throw new InvalidCastException("Json object not vaild for " + objectToLoad.GetType());
		}
	}

	public static string GetMD5Hash(MD5 hasher, string rawSource)
	{
		byte[] array = hasher.ComputeHash(Encoding.UTF8.GetBytes(rawSource));
		for (int i = 0; i < array.Length; i++)
		{
			s_HashStringBuilder.Append(array[i].ToString("x2"));
		}
		string result = s_HashStringBuilder.ToString();
		s_HashStringBuilder.Length = 0;
		return result;
	}

	public void Clear(bool createInvalidState = false)
	{
		if (m_SaveData != null && m_SaveData.SaveInfo != null)
		{
			m_SaveData.SaveInfo.DestroyTexture();
		}
		m_SaveData = (createInvalidState ? SaveData.CreateInvalid() : SaveData.CreateNew());
		m_LastSavedName = null;
		m_ReportedMismatchTech.Clear();
	}

	public Visible LookupSerializedVisible(int id)
	{
		Visible value = null;
		m_SerializedVisibleIDLookup.TryGetValue(id, out value);
		return value;
	}

	public StoredTile GetStoredTile(IntVector2 coord, bool createNewIfNotFound = true)
	{
		StoredTile value = null;
		bool flag = false;
		bool flag2 = false;
		if (coord == IntVector2.invalid)
		{
			d.LogError("ManSaveGame.GetStoredTile - Passed invalid tile coord!");
		}
		else if (!CurrentState.m_StoredTiles.TryGetValue(coord, out value))
		{
			string value2 = null;
			if (CurrentState.m_StoredTilesJSON != null && CurrentState.m_StoredTilesJSON.TryGetValue(coord, out value2))
			{
				if (!string.IsNullOrEmpty(value2) && value2 != "null")
				{
					LoadObjectFromRawJson(ref value, value2);
				}
				else
				{
					d.LogError("ManSaveGame.GetStoredTile - Added, but null tileDataJSON!?");
				}
				if (value != null)
				{
					CurrentState.m_StoredTiles.Add(coord, value);
					flag = true;
				}
				CurrentState.m_StoredTilesJSON.Remove(coord);
			}
			else if (createNewIfNotFound)
			{
				value = CurrentState.GetNewStoredTile(coord);
				flag2 = true;
			}
		}
		_ = flag2 || flag;
		return value;
	}

	public StoredTech GetStoredTech(TrackedVisible tank)
	{
		IntVector2 tileCoord = tank.GetWorldPosition().TileCoord;
		StoredTile storedTile = GetStoredTile(tileCoord, createNewIfNotFound: false);
		if (storedTile == null)
		{
			return null;
		}
		if (!storedTile.m_StoredVisibles.TryGetValue(1, out var value))
		{
			return null;
		}
		StoredTech result = null;
		foreach (StoredVisible item in value)
		{
			if (item.m_ID == tank.ID)
			{
				result = item as StoredTech;
				break;
			}
		}
		return result;
	}

	public TechData GetStoredTechData(TrackedVisible tank)
	{
		StoredTech storedTech = GetStoredTech(tank);
		if (storedTech == null)
		{
			return null;
		}
		if (storedTech.m_TeamID != tank.TeamID && !m_ReportedMismatchTech.Contains(storedTech))
		{
			d.LogErrorFormat("GetStoredTech found that storedTech {0} has a mismatching team ID with its tracked visible ({1} vs {2})", storedTech?.m_TechData?.Name, storedTech.m_TeamID, tank.TeamID);
			m_ReportedMismatchTech.Add(storedTech);
		}
		return storedTech.m_TechData;
	}

	public void CleanupStoredTile(StoredTile tile)
	{
		d.Assert(tile.coord != IntVector2.invalid, "Trying to clean up tile that no longer has a valid coordinate! Won't succeed in propagating save data to JSON!");
		if (tile != null && CurrentState.m_StoredTiles.Remove(tile.coord))
		{
			if (ShouldStore)
			{
				if (CurrentState.m_StoredTilesJSON == null)
				{
					CurrentState.m_StoredTilesJSON = new Dictionary<IntVector2, string>(Singleton.Manager<ManSaveGame>.inst.m_InitialNumberOfStoredTiles);
				}
				string text = SaveObjectToRawJson(tile);
				if (!string.IsNullOrEmpty(text) && text != "null")
				{
					CurrentState.m_StoredTilesJSON.Add(tile.coord, text);
				}
			}
			tile.Reset();
			Singleton.Manager<ManSaveGame>.inst.storedTilePool.Push(tile);
		}
		else
		{
			d.LogError("ManSaveGame.CleanupStoredTile called with either null Tile, or Tile was not found in the m_StoredTiles list!? " + ((tile != null) ? tile.coord.ToString() : ""));
		}
	}

	public static List<SaveFileSlot> GetSavesInFolder(ManGameMode.GameType gameType, bool includeUnusedSlots)
	{
		string currentUserGameSaveDir = GetCurrentUserGameSaveDir(gameType);
		string[] array = null;
		try
		{
			if (Directory.Exists(currentUserGameSaveDir))
			{
				array = (from fn in Directory.EnumerateFiles(currentUserGameSaveDir, "*.sav")
					where fn.EndsWith(".sav")
					select fn).ToArray();
			}
		}
		catch (Exception ex)
		{
			array = null;
			d.LogErrorFormat("Exception thrown while trying to GetSavesInFolder: ", ex);
		}
		List<SaveFileSlot> list = new List<SaveFileSlot>();
		if (array != null)
		{
			string[] array2 = array;
			for (int num = 0; num < array2.Length; num++)
			{
				string text2;
				string text = (text2 = array2[num]);
				long lastWriteTime = 0L;
				string path = text;
				text2 = Path.GetFileNameWithoutExtension(path);
				if (!SKU.ConsoleUI)
				{
					lastWriteTime = File.GetLastWriteTimeUtc(path).ToFileTimeUtc();
				}
				list.Add(new SaveFileSlot
				{
					name = text2,
					isEmptySlot = false,
					lastWriteTime = lastWriteTime
				});
			}
		}
		if (UseFixedSlots && includeUnusedSlots)
		{
			for (int num2 = 0; num2 < NumFixedSlots; num2++)
			{
				string slotName = "Slot " + (num2 + 1);
				if (!list.Any((SaveFileSlot file) => file.name.Contains(slotName)))
				{
					list.Add(new SaveFileSlot
					{
						name = slotName,
						isEmptySlot = true
					});
				}
			}
		}
		return list;
	}

	public string GetCurrentSaveName(bool resolveAutosaveAncestor)
	{
		if (m_SaveData != null && m_SaveData.SaveInfo != null)
		{
			string result = m_SaveData.SaveInfo.m_SaveName;
			if (resolveAutosaveAncestor && m_SaveData.SaveInfo.IsAutoSave)
			{
				result = m_SaveData.SaveInfo.m_SaveNameAncestor;
			}
			return result;
		}
		return null;
	}

	public static bool HasAnySavesInFolder(ManGameMode.GameType gameType)
	{
		List<SaveFileSlot> savesInFolder = GetSavesInFolder(gameType, includeUnusedSlots: false);
		if (savesInFolder != null)
		{
			return savesInFolder.Count > 0;
		}
		return false;
	}

	public static bool SaveExists(ManGameMode.GameType gameType, string saveName)
	{
		return File.Exists(CreateGameSaveFilePath(gameType, saveName));
	}

	public bool IsSaveNameAutoSave(string saveName)
	{
		if (saveName == null)
		{
			return false;
		}
		return saveName.ToLowerInvariant() == AutoSaveName.ToLowerInvariant();
	}

	private void CreateSaveDebugDump(ManGameMode.GameType gameType, string saveName)
	{
		StreamWriter streamWriter = File.CreateText(CreateFilePath(GetCurrentUserGameSaveDir(gameType), saveName, "txt"));
		SaveDumper saveDumper = new SaveDumper(streamWriter);
		State state = m_SaveData.State;
		Dictionary<IntVector2, StoredTile> storedTiles = state.m_StoredTiles;
		List<StoredTile> sleepingStoredTiles = null;
		if (state.m_StoredTilesJSON != null)
		{
			sleepingStoredTiles = new List<StoredTile>(state.m_StoredTilesJSON.Count);
			foreach (KeyValuePair<IntVector2, string> item in state.m_StoredTilesJSON)
			{
				StoredTile objectToLoad = null;
				LoadObjectFromRawJson(ref objectToLoad, item.Value, assertOnFail: false);
				sleepingStoredTiles.Add(objectToLoad);
			}
		}
		int activeTileCount = storedTiles.Count;
		int num = activeTileCount + ((sleepingStoredTiles != null) ? sleepingStoredTiles.Count : 0);
		Func<int, StoredTile> func = (int idx) => (idx < activeTileCount) ? state.m_StoredTiles.ElementAt(idx).Value : sleepingStoredTiles[idx - activeTileCount];
		List<KeyValuePair<IntVector2, StoredTech>> list = new List<KeyValuePair<IntVector2, StoredTech>>();
		Dictionary<int, bool> dictionary = new Dictionary<int, bool>();
		saveDumper.StartSection("Healthy Tanks");
		for (int num2 = 0; num2 < num; num2++)
		{
			StoredTile storedTile = func(num2);
			IntVector2 coord = storedTile.coord;
			if (!storedTile.m_StoredVisibles.ContainsKey(1))
			{
				continue;
			}
			List<StoredVisible> list2 = storedTile.m_StoredVisibles[1];
			for (int num3 = 0; num3 < list2.Count; num3++)
			{
				if (list2[num3] is StoredTech storedTech)
				{
					TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(storedTech.m_ID);
					if (trackedVisible != null)
					{
						dictionary.Add(storedTech.m_ID, value: true);
						saveDumper.AddTank(storedTech, trackedVisible.RadarType.ToString(), coord);
					}
					else
					{
						list.Add(new KeyValuePair<IntVector2, StoredTech>(coord, storedTech));
					}
				}
			}
		}
		saveDumper.EndSection();
		saveDumper.StartSection("Tanks which are not tracked");
		if (list.Count > 0)
		{
			for (int num4 = 0; num4 < list.Count; num4++)
			{
				saveDumper.AddTank(list[num4].Value, "(unknown)", list[num4].Key);
			}
		}
		saveDumper.EndSection();
		saveDumper.StartSection("Tanks which tracked but not in world");
		foreach (TrackedVisible allTrackedVisible in Singleton.Manager<ManVisible>.inst.AllTrackedVisibles)
		{
			if (allTrackedVisible.ObjectType == ObjectTypes.Vehicle && !dictionary.ContainsKey(allTrackedVisible.ID))
			{
				IntVector2 coord2 = Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(allTrackedVisible.Position);
				saveDumper.AddVisible(allTrackedVisible.ID, "?", allTrackedVisible.RadarType.ToString(), allTrackedVisible.Position + Singleton.Manager<ManWorld>.inst.SceneToGameWorld, coord2);
			}
		}
		saveDumper.EndSection();
		saveDumper.StartSection("Tracked Blocks");
		for (int num5 = 0; num5 < num; num5++)
		{
			StoredTile storedTile2 = func(num5);
			IntVector2 coord3 = storedTile2.coord;
			if (!storedTile2.m_StoredVisibles.ContainsKey(2))
			{
				continue;
			}
			List<StoredVisible> list3 = storedTile2.m_StoredVisibles[2];
			for (int num6 = 0; num6 < list3.Count; num6++)
			{
				if (list3[num6] is StoredBlock storedBlock)
				{
					TrackedVisible trackedVisible2 = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(storedBlock.m_ID);
					if (trackedVisible2 != null)
					{
						saveDumper.AddVisible(storedBlock.m_ID, storedBlock.m_BlockType.ToString(), trackedVisible2.RadarType.ToString(), storedBlock.m_WorldPosition.GameWorldPosition, coord3);
					}
				}
			}
		}
		saveDumper.EndSection();
		saveDumper.StartSection("Tracked Waypoints");
		Dictionary<int, bool> dictionary2 = new Dictionary<int, bool>();
		for (int num7 = 0; num7 < num; num7++)
		{
			StoredTile storedTile3 = func(num7);
			IntVector2 coord4 = storedTile3.coord;
			if (!storedTile3.m_StoredVisibles.ContainsKey(2))
			{
				continue;
			}
			List<StoredVisible> list4 = storedTile3.m_StoredVisibles[5];
			for (int num8 = 0; num8 < list4.Count; num8++)
			{
				StoredVisible storedVisible = list4[num8] as StoredWaypoint;
				if (storedVisible != null)
				{
					dictionary2.Add(storedVisible.m_ID, value: true);
					TrackedVisible trackedVisible3 = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(storedVisible.m_ID);
					if (trackedVisible3 != null)
					{
						saveDumper.AddVisible(storedVisible.m_ID, trackedVisible3.visible ? trackedVisible3.visible.name : "Waypoint", trackedVisible3.RadarType.ToString(), storedVisible.m_WorldPosition.GameWorldPosition, coord4);
					}
				}
			}
		}
		saveDumper.EndSection();
		saveDumper.StartSection("Waypoints which tracked but not in world");
		foreach (TrackedVisible allTrackedVisible2 in Singleton.Manager<ManVisible>.inst.AllTrackedVisibles)
		{
			if (allTrackedVisible2.ObjectType == ObjectTypes.Waypoint && !dictionary2.ContainsKey(allTrackedVisible2.ID))
			{
				IntVector2 coord5 = Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(allTrackedVisible2.Position);
				saveDumper.AddVisible(allTrackedVisible2.ID, "?", allTrackedVisible2.RadarType.ToString(), allTrackedVisible2.Position + Singleton.Manager<ManWorld>.inst.SceneToGameWorld, coord5);
			}
		}
		saveDumper.EndSection();
		saveDumper.StartSection("Crates");
		for (int num9 = 0; num9 < num; num9++)
		{
			StoredTile storedTile4 = func(num9);
			IntVector2 coord6 = storedTile4.coord;
			if (!storedTile4.m_StoredVisibles.ContainsKey(6))
			{
				continue;
			}
			List<StoredVisible> list5 = storedTile4.m_StoredVisibles[6];
			for (int num10 = 0; num10 < list5.Count; num10++)
			{
				StoredCrate storedCrate = list5[num10] as StoredCrate;
				TrackedVisible trackedVisible4 = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(storedCrate.m_ID);
				if (trackedVisible4 != null)
				{
					saveDumper.AddVisible(storedCrate.m_ID, storedCrate.name, trackedVisible4.RadarType.ToString(), storedCrate.m_WorldPosition.GameWorldPosition, coord6);
				}
			}
		}
		saveDumper.EndSection();
		streamWriter.Close();
	}

	private void MigrateSaveDataLocations()
	{
		string fromPath = Application.dataPath + "/../";
		string fromPath2 = Application.persistentDataPath + "/";
		string fromPath3 = GetDeprecatedSaveDataFolder() + "/";
		string toPath = GetSaveDataFolder() + "/";
		MoveGameSaveData(fromPath, toPath);
		MoveGameSaveData(fromPath2, toPath);
		MoveGameSaveData(fromPath3, toPath);
	}

	private void MoveGameSaveData(string fromPath, string toPath)
	{
		if (!(fromPath == toPath))
		{
			string fullPath = Path.GetFullPath(fromPath + "/Saves/");
			MoveFiles(fullPath, Path.GetFullPath(toPath + "/Saves/"));
			string fullPath2 = Path.GetFullPath(fromPath + "Snapshots");
			MoveFiles(fullPath2, Path.GetFullPath(toPath + "Snapshots"));
			string fullPath3 = Path.GetFullPath(fromPath + "Cache");
			Func<FileInfo, bool> fileValidationFunc = (FileInfo fInfo) => ManSnapshots.Debug_LoadTechData(fInfo.FullName) != null;
			MoveFiles(fromPath, Path.GetFullPath(toPath + "Snapshots/"), "*.png", SearchOption.TopDirectoryOnly, fileValidationFunc);
			RemoveDirectoryIfEmpty(fullPath);
			RemoveDirectoryIfEmpty(fullPath2);
			RemoveDirectoryIfEmpty(fullPath3);
		}
	}

	private void MoveFiles(string sourcePath, string destPath, string fileSearchPattern = null, SearchOption dirSearchOption = SearchOption.AllDirectories, Func<FileInfo, bool> fileValidationFunc = null)
	{
		try
		{
			if (!Directory.Exists(sourcePath))
			{
				return;
			}
			string[] files = Directory.GetFiles(sourcePath, fileSearchPattern ?? "*", dirSearchOption);
			int length = sourcePath.Length;
			string[] array = files;
			foreach (string text in array)
			{
				FileInfo fileInfo = new FileInfo(text);
				if (fileValidationFunc != null && !fileValidationFunc(fileInfo))
				{
					continue;
				}
				string text2 = text.Remove(0, length);
				string text3 = Path.GetFullPath(destPath + "/" + text2);
				string directoryName = Path.GetDirectoryName(text3);
				if (File.Exists(text3))
				{
					int num = 0;
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.Name);
					do
					{
						string text4 = $"/{fileNameWithoutExtension}{num}{fileInfo.Extension}";
						text3 = directoryName + text4;
						num++;
					}
					while (File.Exists(text3));
				}
				else
				{
					Directory.CreateDirectory(directoryName);
				}
				fileInfo.MoveTo(text3);
			}
		}
		catch (Exception ex)
		{
			d.LogError("MoveFiles - Failed to move files - " + ex);
		}
	}

	private void RemoveDirectoryIfEmpty(string dirPath, bool recursive = true)
	{
		try
		{
			if (!Directory.Exists(dirPath))
			{
				return;
			}
			if (recursive)
			{
				string[] directories = Directory.GetDirectories(dirPath);
				foreach (string dirPath2 in directories)
				{
					RemoveDirectoryIfEmpty(dirPath2);
				}
			}
			if (Directory.GetFiles(dirPath).Length == 0 && Directory.GetDirectories(dirPath).Length == 0)
			{
				Directory.Delete(dirPath, recursive: false);
			}
		}
		catch (Exception ex)
		{
			d.LogError("RemoveDirectoryIfEmpty - Directory could not be removed - " + ex);
		}
	}

	private void Awake()
	{
		if (SKU.IsSteam || SKU.IsLAN_MP)
		{
			MigrateSaveDataLocations();
		}
	}

	private void Start()
	{
		Clear(createInvalidState: true);
		Singleton.Manager<ManWorld>.inst.TileManager.TileStartPopulatingEvent.Subscribe(OnTileStartPopulating);
		Singleton.Manager<ManWorld>.inst.TileManager.TileDepopulatedEvent.Subscribe(OnTileDepopulated);
		Singleton.Manager<ManGameMode>.inst.ModeSwitchEvent.Subscribe(OnModeSwitch);
	}

	private void Update()
	{
		if (m_MultiplayerVisiblesToSpawn.Count <= 0 || !(Singleton.Manager<ManNetwork>.inst.MyPlayer != null) || !Singleton.Manager<ManNetwork>.inst.MyPlayer.IsHostPlayer)
		{
			return;
		}
		d.Log("[ManSaveGame] Spawning m_MultiplayerVisiblesToSpawn : " + m_MultiplayerVisiblesToSpawn.Count);
		bool flag = false;
		foreach (StoredVisible item in m_MultiplayerVisiblesToSpawn)
		{
			Visible visible = RestoreVisible(item);
			flag = flag || visible != null;
		}
		m_MultiplayerVisiblesToSpawn.Clear();
		if (flag)
		{
			foreach (int key in Singleton.Manager<ManSaveGame>.inst.m_SerializedVisibleIDLookup.Keys)
			{
				Singleton.Manager<ManVisible>.inst.GetTrackedVisible(key)?.OnRespawn();
			}
			Singleton.Manager<ManSaveGame>.inst.m_SerializedVisibleIDLookup.Clear();
		}
		Singleton.Manager<ManEncounter>.inst.FixupLateSpawnedVisibles();
	}

	[Conditional("CONSOLE_ONLY_LOG")]
	public static void LogOnConsoleOnly(string message)
	{
		d.Log(message);
	}
}
