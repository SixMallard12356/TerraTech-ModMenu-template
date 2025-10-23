#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using Ionic.Zlib;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class TechData
{
	public class CreationData
	{
		public string mode = string.Empty;

		public string subMode = string.Empty;

		public string userData = string.Empty;

		public string m_Creator = string.Empty;

		public TankPreset.UserData m_UserProfile;

		public CreationData()
		{
		}

		public CreationData(CreationData copyTarget)
		{
			mode = copyTarget.mode;
			subMode = copyTarget.subMode;
			userData = copyTarget.userData;
			m_Creator = copyTarget.m_Creator;
			m_UserProfile = ((copyTarget.m_UserProfile != null) ? new TankPreset.UserData(copyTarget.m_UserProfile) : null);
		}

		public void NetSerialize(NetworkWriter writer)
		{
			writer.Write(mode);
			writer.Write(subMode);
			writer.Write(userData);
			writer.Write(m_Creator);
		}

		public void NetDeserialize(NetworkReader reader)
		{
			mode = reader.ReadString();
			subMode = reader.ReadString();
			userData = reader.ReadString();
			m_Creator = reader.ReadString();
			m_UserProfile = null;
		}
	}

	public struct SerializedSnapshotData
	{
		public string m_Name;

		public CreationData m_CreationData;

		public List<TankPreset.BlockSpec> m_BlockSpecs;

		public IntVector3 m_Bounds;

		public Dictionary<uint, string> m_SkinIndices;

		public List<ControlScheme> m_ControlSchemes;

		public int m_EnabledModuleCategoriesFlags;

		public SerializedSnapshotData(TechData techData)
		{
			m_Name = techData.Name;
			m_CreationData = techData.m_CreationData;
			m_BlockSpecs = techData.m_BlockSpecs;
			m_Bounds = techData.m_BoundsDoubleExtents;
			m_ControlSchemes = techData.GetControlSchemes();
			m_SkinIndices = techData.GetSkinIndices();
			m_EnabledModuleCategoriesFlags = techData.GetEnabledModuleCategories();
		}

		public TechData CreateTechData()
		{
			TechData techData = null;
			if (m_BlockSpecs != null)
			{
				techData = new TechData();
				techData.Name = m_Name;
				techData.m_BoundsDoubleExtents = m_Bounds;
				techData.m_BlockSpecs = new List<TankPreset.BlockSpec>(m_BlockSpecs);
				techData.m_CreationData = new CreationData
				{
					mode = m_CreationData.mode,
					subMode = m_CreationData.subMode,
					userData = m_CreationData.userData,
					m_Creator = m_CreationData.m_Creator,
					m_UserProfile = ((m_CreationData.m_UserProfile != null) ? new TankPreset.UserData(m_CreationData.m_UserProfile) : new TankPreset.UserData())
				};
				techData.SetControlSchemesFromSnapshot(m_ControlSchemes);
				techData.SetEnabledModuleCategories(m_EnabledModuleCategoriesFlags);
				if (m_SkinIndices != null)
				{
					techData.m_SkinMapping = new Dictionary<uint, string>(m_SkinIndices);
				}
				else
				{
					techData.m_SkinMapping = new Dictionary<uint, string>();
				}
			}
			return techData;
		}

		public void SerializeToStream(Stream serializationStream, IFormatter formatter)
		{
			formatter.Serialize(serializationStream, m_Name);
			formatter.Serialize(serializationStream, m_CreationData.m_UserProfile);
			formatter.Serialize(serializationStream, m_BlockSpecs);
			formatter.Serialize(serializationStream, m_CreationData.mode);
			formatter.Serialize(serializationStream, m_CreationData.subMode);
			formatter.Serialize(serializationStream, m_CreationData.userData);
			formatter.Serialize(serializationStream, m_Bounds);
			if (m_ControlSchemes != null)
			{
				formatter.Serialize(serializationStream, true);
				formatter.Serialize(serializationStream, m_ControlSchemes);
			}
			else
			{
				formatter.Serialize(serializationStream, false);
			}
			formatter.Serialize(serializationStream, m_EnabledModuleCategoriesFlags);
			if (m_SkinIndices != null)
			{
				formatter.Serialize(serializationStream, m_SkinIndices);
			}
		}
	}

	[JsonProperty]
	[SerializeField]
	private string m_Name;

	[JsonProperty]
	[SerializeField]
	private LocalisedString m_LocalisedName;

	[JsonProperty]
	[SerializeField]
	private int m_NameIndex = -1;

	[JsonProperty]
	[SerializeField]
	private string m_Author;

	[SerializeField]
	[JsonProperty]
	private int m_RadarMarkerIcon;

	[SerializeField]
	[JsonProperty]
	private int m_RadarMarkerColor;

	[JsonProperty]
	[SerializeField]
	private bool m_RadarMarkerUsed;

	[JsonProperty]
	[SerializeField]
	private IntVector3 m_BoundsDoubleExtents;

	public IntVector3 m_Bounds;

	public Dictionary<uint, string> m_SkinMapping;

	public List<TankPreset.BlockSpec> m_BlockSpecs;

	public CreationData m_CreationData = new CreationData();

	public Dictionary<int, TechComponent.SerialData> m_TechSaveState = new Dictionary<int, TechComponent.SerialData>();

	[JsonIgnore]
	public string Name
	{
		get
		{
			if (!HasLocalisedName)
			{
				return m_Name;
			}
			return CreateNameWithIndex(m_LocalisedName, m_NameIndex);
		}
		set
		{
			m_LocalisedName = null;
			m_NameIndex = -1;
			m_Name = value;
		}
	}

	[JsonIgnore]
	public bool HasLocalisedName
	{
		get
		{
			if (m_LocalisedName != null && !m_LocalisedName.m_Bank.NullOrEmpty())
			{
				return !m_LocalisedName.m_Id.NullOrEmpty();
			}
			return false;
		}
	}

	[JsonIgnore]
	public LocalisedString LocalisedName
	{
		get
		{
			return m_LocalisedName;
		}
		set
		{
			m_LocalisedName = value;
			m_Name = "";
		}
	}

	[JsonIgnore]
	public int NameIndex
	{
		get
		{
			return m_NameIndex;
		}
		set
		{
			m_NameIndex = value;
		}
	}

	[JsonIgnore]
	public RadarMarker RadarMarkerConfig
	{
		get
		{
			if (!m_RadarMarkerUsed)
			{
				return RadarMarker.DefaultMarker_Disabled;
			}
			return new RadarMarker((ManRadar.IconType)m_RadarMarkerIcon, (ManRadar.RadarMarkerColorType)m_RadarMarkerColor, m_RadarMarkerUsed);
		}
		set
		{
			m_RadarMarkerIcon = (int)value.Icon;
			m_RadarMarkerColor = (int)value.Color;
			m_RadarMarkerUsed = value.IsUsed;
		}
	}

	[JsonIgnore]
	public string Author
	{
		get
		{
			return m_Author;
		}
		set
		{
			m_Author = value;
		}
	}

	public Vector3 m_BoundsExtents => (Vector3)m_BoundsDoubleExtents * 0.5f;

	public float Radius => Mathf.Max(m_BoundsExtents.x, m_BoundsExtents.z) + 0.5f;

	public void SaveTech(Tank tech, bool saveRuntimeState = false, bool saveMetaData = true)
	{
		if (tech == null)
		{
			d.LogError("TechData.SaveTech was passed a Null tech !");
			return;
		}
		if (tech.blockman.blockCount == 0)
		{
			throw new Exception("Saving tech " + tech.name + " with no blocks");
		}
		SaveTechName(tech);
		SaveTechRadarMarkerInfo(tech.RadarMarker.RadarMarkerConfig);
		SaveTechAuthorInfo(tech);
		m_BoundsDoubleExtents = tech.blockBounds.size;
		if (m_BlockSpecs == null)
		{
			m_BlockSpecs = new List<TankPreset.BlockSpec>();
		}
		m_BlockSpecs.Clear();
		if (m_SkinMapping == null)
		{
			m_SkinMapping = new Dictionary<uint, string>();
		}
		m_SkinMapping.Clear();
		int num = 0;
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tech.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current = enumerator.Current;
			TankPreset.BlockSpec item = default(TankPreset.BlockSpec);
			item.InitFromBlockState(current, saveRuntimeState);
			FactionSubTypes corporation = Singleton.Manager<ManSpawn>.inst.GetCorporation(item.m_BlockType);
			uint key = CombineCorpAndSkinID(corporation, item.m_SkinID);
			if (!m_SkinMapping.ContainsKey(key))
			{
				m_SkinMapping.Add(key, Singleton.Manager<ManCustomSkins>.inst.GetSkinNameForSnapshot(corporation, item.m_SkinID));
			}
			m_BlockSpecs.Add(item);
			num++;
		}
		m_CreationData = new CreationData();
		if (saveMetaData)
		{
			m_CreationData.mode = Singleton.Manager<ManGameMode>.inst.GetCurrentGameMode();
			m_CreationData.subMode = Singleton.Manager<ManGameMode>.inst.GetCurrentGameSubmode();
			if (Singleton.Manager<ManProfile>.inst.GetCurrentUser() != null)
			{
				m_CreationData.m_UserProfile = new TankPreset.UserData(Singleton.Manager<ManProfile>.inst.GetCurrentUser());
			}
		}
		m_TechSaveState.Clear();
		tech.SerializeEvent.Send(paramA: true, m_TechSaveState);
	}

	public void SaveTechName(Tank tech)
	{
		m_Name = tech.name;
		m_LocalisedName = tech.GetLocalisedName();
		m_NameIndex = tech.GetNameIndex();
	}

	public void SaveTechRadarMarkerInfo(RadarMarker info)
	{
		RadarMarkerConfig = info;
	}

	public void SaveTechAuthorInfo(Tank tech)
	{
		Author = tech.Author;
	}

	public void RestoreTechName(Tank tech)
	{
		if (HasLocalisedName)
		{
			tech.SetLocalisedName(m_LocalisedName, m_NameIndex);
		}
		else
		{
			tech.SetName(m_Name);
		}
	}

	public void RestoreTechRadarMarker(Tank tech)
	{
		tech.RadarMarker.RadarMarkerConfig = RadarMarkerConfig;
	}

	public void RestoreTechAuthor(Tank tech)
	{
		tech.Author = Author;
	}

	public void SetNameToPlayerTechCount()
	{
		m_NameIndex = Singleton.Manager<ManSaveGame>.inst.CurrentState.m_SpawnedPlayerNetTechCount.GetNextAndIncrement();
		m_LocalisedName = new LocalisedString
		{
			m_Bank = LocalisationEnums.StringBanks.NewMenuMain.ToString(),
			m_Id = LocalisationEnums.NewMenuMain.PlayerTech.ToString()
		};
		m_Name = CreateNameWithIndex(m_LocalisedName, m_NameIndex);
	}

	public TechData GetShallowClonedCopy()
	{
		return (TechData)MemberwiseClone();
	}

	[Conditional("USE_ANALYTICS")]
	public void SendSnapshotAnalyticEvent(string eventName)
	{
		Dictionary<int, int> blockCounts = new Dictionary<int, int>();
		GetBlockUsageCount(out var numBaseGame, out var numDLC, out var numModded, out var numBaseSkins, out var numDLCSkins, out var numModdedSkins, blockCounts);
		new Dictionary<string, object>
		{
			{
				"game_mode",
				Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().ToString()
			},
			{ "name", m_Name },
			{ "blocks_base", numBaseGame },
			{ "blocks_dlc", numDLC },
			{ "blocks_modded", numModded },
			{ "skins_base", numBaseSkins },
			{ "skins_dlc", numDLCSkins },
			{ "skins_modded", numModdedSkins }
		};
	}

	public void ValidateBlockSkins()
	{
		for (int i = 0; i < m_BlockSpecs.Count; i++)
		{
			TankPreset.BlockSpec value = m_BlockSpecs[i];
			if (!Singleton.Manager<ManCustomSkins>.inst.CanUseSkinWithUniqueID(GetSkinID(i), value.GetBlockType()))
			{
				value.m_SkinID = Singleton.Manager<ManCustomSkins>.inst.SkinIndexToID(0, Singleton.Manager<ManSpawn>.inst.GetCorporation(value.GetBlockType()));
				m_BlockSpecs[i] = value;
			}
		}
	}

	public bool SaveTechIfChanged(Tank tank, bool saveRuntimeState = false, bool saveMetaData = true)
	{
		if (tank == null)
		{
			d.LogError("TechData.SaveTech was passed a Null tech !");
			return false;
		}
		TankPreset.BlockSpec[] array = ((m_BlockSpecs != null) ? m_BlockSpecs.ToArray() : null);
		SaveTech(tank, saveRuntimeState, saveMetaData);
		if (array != null)
		{
			return !TankPreset.CheckBlockSpecListsMatch(array, m_BlockSpecs.ToArray());
		}
		return true;
	}

	public void Copy(TechData techData)
	{
		m_Name = techData.m_Name;
		m_LocalisedName = techData.m_LocalisedName;
		m_NameIndex = techData.m_NameIndex;
		m_Author = techData.m_Author;
		m_RadarMarkerIcon = techData.m_RadarMarkerIcon;
		m_RadarMarkerColor = techData.m_RadarMarkerColor;
		m_RadarMarkerUsed = techData.m_RadarMarkerUsed;
		m_BoundsDoubleExtents = techData.m_BoundsDoubleExtents;
		if (m_BlockSpecs == null)
		{
			m_BlockSpecs = new List<TankPreset.BlockSpec>();
		}
		m_BlockSpecs.Clear();
		m_BlockSpecs.AddRange(techData.m_BlockSpecs);
		m_CreationData = new CreationData(techData.m_CreationData);
		m_TechSaveState.Clear();
		foreach (KeyValuePair<int, TechComponent.SerialData> item in techData.m_TechSaveState)
		{
			m_TechSaveState.Add(item.Key, item.Value);
		}
	}

	public void CopyWithoutSaveData(TechData techData)
	{
		m_Name = techData.m_Name;
		m_LocalisedName = techData.m_LocalisedName;
		m_NameIndex = techData.m_NameIndex;
		m_Author = techData.m_Author;
		m_RadarMarkerIcon = techData.m_RadarMarkerIcon;
		m_RadarMarkerColor = techData.m_RadarMarkerColor;
		m_RadarMarkerUsed = techData.m_RadarMarkerUsed;
		if (m_BlockSpecs == null)
		{
			m_BlockSpecs = new List<TankPreset.BlockSpec>();
		}
		m_BlockSpecs.Clear();
		m_BlockSpecs.AddRange(techData.m_BlockSpecs);
		m_BoundsDoubleExtents = techData.m_BoundsDoubleExtents;
		m_CreationData = new CreationData(techData.m_CreationData);
		m_TechSaveState.Clear();
	}

	public int GetValue()
	{
		int num = 0;
		if ((bool)Singleton.Manager<ManSpawn>.inst)
		{
			for (int i = 0; i < m_BlockSpecs.Count; i++)
			{
				BlockTypes blockType = m_BlockSpecs[i].GetBlockType();
				num += Singleton.Manager<RecipeManager>.inst.GetBlockBuyPrice(blockType);
			}
		}
		return num;
	}

	public List<FactionSubTypes> GetMainCorporations()
	{
		Dictionary<int, int> filledCellsPerCorporation = GetFilledCellsPerCorporation();
		int num = 0;
		List<FactionSubTypes> list = new List<FactionSubTypes>();
		foreach (KeyValuePair<int, int> item in filledCellsPerCorporation)
		{
			if (item.Value >= num)
			{
				if (item.Value > num)
				{
					list.Clear();
				}
				list.Add((FactionSubTypes)item.Key);
				num = item.Value;
			}
		}
		return list;
	}

	public Dictionary<int, float> GetCorporationBlockRatios()
	{
		Dictionary<int, int> filledCellsPerCorporation = GetFilledCellsPerCorporation();
		Dictionary<int, float> dictionary = new Dictionary<int, float>();
		int totalFilledCells = GetTotalFilledCells();
		foreach (KeyValuePair<int, int> item in filledCellsPerCorporation)
		{
			dictionary.Add(item.Key, (float)item.Value / (float)totalFilledCells);
		}
		return dictionary;
	}

	public Bounds CalculateBlockBounds()
	{
		Bounds result = new Bounds(Vector3.zero, Vector3.one * float.MinValue);
		foreach (TankPreset.BlockSpec blockSpec in m_BlockSpecs)
		{
			TankBlock blockPrefab = Singleton.Manager<ManSpawn>.inst.GetBlockPrefab(blockSpec.GetBlockType());
			if (blockPrefab != null)
			{
				OrthoRotation orthoRotation = new OrthoRotation(blockSpec.orthoRotation);
				IntVector3[] filledCells = blockPrefab.filledCells;
				foreach (IntVector3 intVector in filledCells)
				{
					result.Encapsulate(blockSpec.position + orthoRotation * intVector);
				}
			}
		}
		if (result.size.x < 0f)
		{
			return default(Bounds);
		}
		return result;
	}

	public bool CheckIsAnchored()
	{
		bool result = false;
		for (int i = 0; i < m_BlockSpecs.Count; i++)
		{
			if (m_BlockSpecs[i].CheckIsAnchored())
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public void NetSerialize(NetworkWriter writerSupplied)
	{
		d.Log("[TankPreset] NetSerialize");
		NetworkWriter networkWriter = new NetworkWriter();
		networkWriter.SeekZero();
		networkWriter.Write(Name);
		networkWriter.Write(RadarMarkerConfig);
		networkWriter.Write(m_Author);
		networkWriter.Write(m_BlockSpecs.Count);
		for (int i = 0; i < m_BlockSpecs.Count; i++)
		{
			m_BlockSpecs[i].NetSerialize(networkWriter);
		}
		networkWriter.Write(m_BoundsDoubleExtents.x);
		networkWriter.Write(m_BoundsDoubleExtents.y);
		networkWriter.Write(m_BoundsDoubleExtents.z);
		m_CreationData.NetSerialize(networkWriter);
		string value = JsonConvert.SerializeObject(m_TechSaveState, TankPreset.s_SerializationSettings);
		networkWriter.Write(value);
		byte[] array = networkWriter.ToArray();
		int num = array.Length;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		byte[] array2 = GZipStream.CompressBuffer(array);
		float num2 = Time.realtimeSinceStartup - realtimeSinceStartup;
		int num3 = array2.Length;
		float num4 = ((num > 0) ? ((float)num3 * 100f / (float)num) : 0f);
		if (num2 > 0.15f)
		{
			d.LogWarning("TankPreset Compression Time Threshold Exceeded Warning: OrigBytes=" + num + " Compressed=" + num3 + " CompressionTime=" + num2.ToString("F4") + " PercentageOfOriginalSize=" + num4.ToString("F2") + "%");
		}
		writerSupplied.WriteBytesWithSize32(array2);
	}

	public void NetDeserialize(NetworkReader readerSupplied)
	{
		d.Log("[TankPreset] NetDeserialize");
		byte[] array = readerSupplied.ReadBytesWithSize32();
		int num = array.Length;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		byte[] array2 = GZipStream.UncompressBuffer(array);
		float num2 = Time.realtimeSinceStartup - realtimeSinceStartup;
		int num3 = array2.Length;
		if (num2 > 0.1f)
		{
			d.Log("TankPreset: CompressedBytes=" + num + " DecompressedBytes=" + num3 + " DecompressionTime=" + num2.ToString("F4"));
		}
		NetworkReader networkReader = new NetworkReader(array2);
		Name = networkReader.ReadString();
		RadarMarkerConfig = networkReader.ReadRadarMarker();
		Author = networkReader.ReadString();
		int num4 = networkReader.ReadInt32();
		m_BlockSpecs = new List<TankPreset.BlockSpec>(num4);
		for (int i = 0; i < num4; i++)
		{
			TankPreset.BlockSpec item = default(TankPreset.BlockSpec);
			item.NetDeserialize(networkReader);
			m_BlockSpecs.Add(item);
		}
		m_BoundsDoubleExtents.x = networkReader.ReadInt32();
		m_BoundsDoubleExtents.y = networkReader.ReadInt32();
		m_BoundsDoubleExtents.z = networkReader.ReadInt32();
		m_CreationData.NetDeserialize(networkReader);
		m_TechSaveState = JsonConvert.DeserializeObject<Dictionary<int, TechComponent.SerialData>>(networkReader.ReadString(), TankPreset.s_SerializationSettings);
	}

	public void UpdateFromDeprecatedBounds(bool bError = false)
	{
		if (m_Bounds != IntVector3.zero && m_BoundsDoubleExtents == IntVector3.zero)
		{
			if (bError)
			{
				d.LogErrorFormat("TechData - m_BoundsDoubleExtents was not initialised through OnDeserialized on '{0}'. Figure out why this is possible with ScriptableObjects..", Name);
			}
			m_BoundsDoubleExtents = m_Bounds * 2 + Vector3.one;
			m_Bounds = IntVector3.zero;
		}
	}

	public static string CreateNameWithIndex(LocalisedString locString, int nameIndex)
	{
		if (nameIndex < 0)
		{
			return locString.Value;
		}
		return string.Format(locString.Value, nameIndex);
	}

	public void SetControlSchemesFromSnapshot(List<ControlScheme> schemes)
	{
		SetControlSchemes(schemes, fromSnapshot: true);
	}

	public void SetEnabledModuleCategories(int activeModulesBitFlags)
	{
		TechBlockStateController.SerialData serialData = new TechBlockStateController.SerialData();
		serialData.activeModulesBitFlags = activeModulesBitFlags;
		serialData.Store(m_TechSaveState);
	}

	public void SetControlSchemesFromPreset(ControlSchemeCategory[] schemes)
	{
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		List<ControlScheme> list = null;
		if (currentUser != null && schemes != null && schemes.Length != 0)
		{
			list = new List<ControlScheme>(schemes.Length);
			foreach (ControlSchemeCategory controlSchemeCategory in schemes)
			{
				if (controlSchemeCategory != ControlSchemeCategory.Custom)
				{
					ControlScheme controlScheme = currentUser.m_ControlSchemeLibrary.GetControlScheme(controlSchemeCategory);
					if (controlScheme != null && !list.Contains(controlScheme))
					{
						list.Add(controlScheme);
					}
				}
			}
		}
		SetControlSchemes(list, fromSnapshot: false);
	}

	public byte GetSkinID(int blockSpecIndex)
	{
		BlockTypes blockType = m_BlockSpecs[blockSpecIndex].m_BlockType;
		byte skinID = m_BlockSpecs[blockSpecIndex].m_SkinID;
		if (skinID >= 32)
		{
			string modSkinName = GetModSkinName(blockType, skinID);
			if (modSkinName != "")
			{
				return (byte)Singleton.Manager<ManCustomSkins>.inst.GetSkinIDFromSnapshot(Singleton.Manager<ManSpawn>.inst.GetCorporation(blockType), modSkinName);
			}
		}
		return skinID;
	}

	public Dictionary<uint, string> GetSkinIndices()
	{
		if (m_SkinMapping == null)
		{
			m_SkinMapping = new Dictionary<uint, string>();
		}
		return m_SkinMapping;
	}

	public string GetModSkinName(BlockTypes blockID, uint skinID)
	{
		if (m_SkinMapping != null)
		{
			FactionSubTypes corporation = Singleton.Manager<ManSpawn>.inst.GetCorporation(blockID);
			uint key = CombineCorpAndSkinID(corporation, skinID);
			string value = "";
			m_SkinMapping.TryGetValue(key, out value);
			return value;
		}
		return "";
	}

	public List<ControlScheme> GetControlSchemes()
	{
		TankControl.SerialData serialData = TechComponent.SerialData<TankControl.SerialData>.Retrieve(m_TechSaveState);
		List<ControlScheme> result = null;
		if (serialData != null && serialData.m_Schemes != null)
		{
			result = serialData.m_Schemes;
		}
		return result;
	}

	public int GetEnabledModuleCategories()
	{
		TechBlockStateController.SerialData serialData = TechComponent.SerialData<TechBlockStateController.SerialData>.Retrieve(m_TechSaveState);
		int result = -1;
		if (serialData != null)
		{
			result = serialData.activeModulesBitFlags;
		}
		return result;
	}

	public void MakeSaveStateDictionaryUnique()
	{
		m_TechSaveState = new Dictionary<int, TechComponent.SerialData>(m_TechSaveState);
	}

	private void SetControlSchemes(List<ControlScheme> schemes, bool fromSnapshot)
	{
		if (schemes != null && schemes.Count > 0)
		{
			TankControl.SerialData serialData = new TankControl.SerialData();
			serialData.m_Schemes = schemes;
			serialData.m_FromSnapshot = fromSnapshot;
			serialData.Store(m_TechSaveState);
		}
		else
		{
			TechComponent.SerialData<TankControl.SerialData>.Remove(m_TechSaveState);
		}
	}

	private int GetBlockUsageCount(out int numBaseGame, out int numDLC, out int numModded, out int numBaseSkins, out int numDLCSkins, out int numModdedSkins, Dictionary<int, int> blockCounts = null)
	{
		numBaseGame = 0;
		numDLC = 0;
		numModded = 0;
		numBaseSkins = 0;
		numDLCSkins = 0;
		numModdedSkins = 0;
		int num = 0;
		for (int i = 0; i < m_BlockSpecs.Count; i++)
		{
			TankPreset.BlockSpec blockSpec = m_BlockSpecs[i];
			ManDLC.DLCType dlcType;
			if (Singleton.Manager<ManMods>.inst.IsModdedBlock(blockSpec.m_BlockType))
			{
				numModded++;
			}
			else if (Singleton.Manager<ManDLC>.inst.IsBlockDLC(blockSpec.m_BlockType, out dlcType))
			{
				numDLC++;
			}
			else
			{
				numBaseGame++;
			}
			if (blockSpec.m_SkinID >= 32)
			{
				numModdedSkins++;
			}
			else if (Singleton.Manager<ManDLC>.inst.IsSkinDLC(blockSpec.m_SkinID, Singleton.Manager<ManSpawn>.inst.GetCorporation(blockSpec.m_BlockType)))
			{
				numDLCSkins++;
			}
			else
			{
				numBaseSkins++;
			}
			if (blockCounts != null)
			{
				if (!blockCounts.TryGetValue((int)blockSpec.m_BlockType, out var value))
				{
					value = 0;
				}
				blockCounts[(int)blockSpec.m_BlockType] = value + 1;
			}
			num++;
		}
		return num;
	}

	[OnDeserialized]
	private void OnDeserialized(StreamingContext context)
	{
		UpdateFromDeprecatedBounds();
	}

	private uint CombineCorpAndSkinID(FactionSubTypes corp, uint skinID)
	{
		return (uint)((int)corp << 16) ^ skinID;
	}

	private Dictionary<int, int> GetFilledCellsPerCorporation()
	{
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < m_BlockSpecs.Count; i++)
		{
			int corporation = (int)Singleton.Manager<ManSpawn>.inst.GetCorporation(m_BlockSpecs[i].GetBlockType());
			TankBlock blockPrefab = Singleton.Manager<ManSpawn>.inst.GetBlockPrefab(m_BlockSpecs[i].GetBlockType());
			if (blockPrefab != null)
			{
				int num = blockPrefab.filledCells.Length;
				if (!dictionary.ContainsKey(corporation))
				{
					dictionary.Add(corporation, 0);
				}
				dictionary[corporation] += num;
			}
		}
		return dictionary;
	}

	private int GetTotalFilledCells()
	{
		int num = 0;
		for (int i = 0; i < m_BlockSpecs.Count; i++)
		{
			TankBlock blockPrefab = Singleton.Manager<ManSpawn>.inst.GetBlockPrefab(m_BlockSpecs[i].GetBlockType());
			if (blockPrefab != null)
			{
				num += blockPrefab.filledCells.Length;
			}
		}
		return num;
	}
}
