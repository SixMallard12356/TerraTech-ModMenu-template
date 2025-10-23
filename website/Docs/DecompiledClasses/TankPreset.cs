#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class TankPreset : ScriptableObject, ISerializationCallbackReceiver
{
	[Serializable]
	public struct BlockSpec
	{
		public IntVector3 position;

		public int orthoRotation;

		public string block;

		public BlockTypes m_BlockType;

		public int m_VisibleID;

		[FormerlySerializedAs("m_SkinIndex")]
		public byte m_SkinID;

		public Dictionary<int, Module.SerialData> saveState;

		public List<string> textSerialData;

		[JsonExtensionData]
		private IDictionary<string, JToken> m_AdditionalJsonData;

		public BlockTypes GetBlockType()
		{
			if (Singleton.Manager<ManMods>.inst.IsModdedBlock(m_BlockType))
			{
				return (BlockTypes)Singleton.Manager<ManMods>.inst.GetBlockID(block);
			}
			return m_BlockType;
		}

		public void InitFromBlockState(TankBlock block, bool saveRuntimeState)
		{
			position = new IntVector3(block.trans.localPosition);
			orthoRotation = block.cachedLocalRotation;
			this.block = block.name;
			m_BlockType = (BlockTypes)block.visible.ItemType;
			m_SkinID = Singleton.Manager<ManCustomSkins>.inst.SkinIndexToID(block.GetSkinIndex(), Singleton.Manager<ManSpawn>.inst.GetCorporation(GetBlockType()));
			saveState = new Dictionary<int, Module.SerialData>();
			textSerialData = new List<string>();
			if (saveRuntimeState)
			{
				block.Serialize(saving: true, this);
			}
			else
			{
				block.SerializeToText(saving: true, this);
			}
		}

		public static BlockSpec GetBlockConfigState(TankBlock block)
		{
			BlockSpec blockSpec = new BlockSpec
			{
				textSerialData = new List<string>()
			};
			block.SerializeToText(saving: true, blockSpec, onTech: false);
			return blockSpec;
		}

		public BlockSpec SetSaveState(Dictionary<int, Module.SerialData> overrideSaveData)
		{
			return new BlockSpec
			{
				position = position,
				orthoRotation = orthoRotation,
				block = block,
				m_BlockType = m_BlockType,
				m_SkinID = m_SkinID,
				saveState = overrideSaveData,
				textSerialData = new List<string>()
			};
		}

		public BlockSpec ClearTextSerialData()
		{
			return new BlockSpec
			{
				position = position,
				orthoRotation = orthoRotation,
				block = block,
				m_BlockType = m_BlockType,
				m_SkinID = m_SkinID,
				saveState = saveState,
				textSerialData = new List<string>()
			};
		}

		public void Store(Type t, string key, string value)
		{
			textSerialData.Add($"{t} {key} {value}");
		}

		public bool TryRetrieve(Type t, string key, out string outValue)
		{
			outValue = Retrieve(t, key, allowMissing: true);
			return !outValue.NullOrEmpty();
		}

		public string Retrieve(Type t, string key)
		{
			return Retrieve(t, key, allowMissing: false);
		}

		private string Retrieve(Type t, string key, bool allowMissing)
		{
			string prefix = $"{t} {key} ";
			string text = null;
			try
			{
				text = textSerialData.SingleOrDefault((string s) => s.StartsWith(prefix));
			}
			catch
			{
				if (!allowMissing)
				{
					d.LogError("BlockSPec retrieve failed");
				}
			}
			if (text == null)
			{
				t = BlockSerialisation.LookupAliasType(t);
				if (t != null)
				{
					prefix = $"{t} {key} ";
					text = textSerialData.SingleOrDefault((string s) => s.StartsWith(prefix));
				}
			}
			if (text == null)
			{
				return "";
			}
			return text.Substring(prefix.Length);
		}

		public long GetSpecHash(IntVector3 rootOffset)
		{
			return BlockManager.CalculateBlockHash(position + rootOffset, orthoRotation, (int)GetBlockType(), m_SkinID);
		}

		public void NetSerialize(NetworkWriter writer)
		{
			writer.Write(position.x);
			writer.Write(position.y);
			writer.Write(position.z);
			writer.Write(orthoRotation);
			writer.Write(block);
			writer.Write((int)m_BlockType);
			writer.Write(m_SkinID);
			string value = JsonConvert.SerializeObject(saveState, s_SerializationSettings);
			writer.Write(value);
			writer.Write(textSerialData.Count);
			for (int i = 0; i < textSerialData.Count; i++)
			{
				writer.Write(textSerialData[i]);
			}
		}

		public void NetDeserialize(NetworkReader reader)
		{
			position.x = reader.ReadInt32();
			position.y = reader.ReadInt32();
			position.z = reader.ReadInt32();
			orthoRotation = reader.ReadInt32();
			block = reader.ReadString();
			m_BlockType = (BlockTypes)reader.ReadInt32();
			m_SkinID = reader.ReadByte();
			saveState = JsonConvert.DeserializeObject<Dictionary<int, Module.SerialData>>(reader.ReadString(), s_SerializationSettings);
			int num = reader.ReadInt32();
			textSerialData = new List<string>();
			for (int i = 0; i < num; i++)
			{
				textSerialData.Add(reader.ReadString());
			}
			m_VisibleID = 0;
		}

		public bool CheckIsAnchored()
		{
			string text = Retrieve(typeof(ModuleAnchor), "anchored");
			bool result = false;
			ModuleAnchor.SerialData serialData;
			if (!text.NullOrEmpty())
			{
				result = text == "true";
			}
			else if (saveState != null && (serialData = Module.SerialData<ModuleAnchor.SerialData>.Retrieve(saveState)) != null)
			{
				result = serialData.anchored;
			}
			return result;
		}

		[OnDeserialized]
		private void OnDeserialize(StreamingContext context)
		{
			if (m_AdditionalJsonData != null && m_AdditionalJsonData.TryGetValue("m_SkinIndex", out var value))
			{
				m_SkinID = (byte)value.ToObject<int>();
			}
		}
	}

	[Serializable]
	public class UserData
	{
		public string m_Name;

		public DateTime? m_UserCreatedDate;

		[JsonConstructor]
		public UserData()
		{
		}

		public UserData(ManProfile.Profile profile)
		{
			m_Name = profile.m_Name;
			m_UserCreatedDate = profile.m_CreationDate;
		}

		public UserData(UserData userData)
		{
			m_Name = userData.m_Name;
			m_UserCreatedDate = userData.m_UserCreatedDate;
		}

		public static bool operator ==(UserData a, UserData b)
		{
			if ((object)a == b)
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			return a.Equals(b);
		}

		public static bool operator !=(UserData a, UserData b)
		{
			return !(a == b);
		}

		public bool Equals(UserData otherItem)
		{
			if (otherItem == null)
			{
				return false;
			}
			if (m_Name == otherItem.m_Name)
			{
				DateTime? userCreatedDate = m_UserCreatedDate;
				DateTime? userCreatedDate2 = otherItem.m_UserCreatedDate;
				if (userCreatedDate.HasValue != userCreatedDate2.HasValue)
				{
					return false;
				}
				if (!userCreatedDate.HasValue)
				{
					return true;
				}
				return userCreatedDate.GetValueOrDefault() == userCreatedDate2.GetValueOrDefault();
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (!(obj is UserData otherItem))
			{
				return false;
			}
			return Equals(otherItem);
		}

		public override int GetHashCode()
		{
			return m_Name.GetHashCode() ^ m_UserCreatedDate.GetHashCode();
		}
	}

	[SerializeField]
	private TechData m_TechData;

	[SerializeField]
	private ControlSchemeCategory[] m_DefaultControlSchemes;

	public int m_LimiterCost;

	public int m_NumUniqueBlockTypes;

	public static JsonSerializerSettings s_SerializationSettings = new JsonSerializerSettings
	{
		TypeNameHandling = TypeNameHandling.Auto,
		TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
		Formatting = Formatting.None,
		Error = delegate(object sender, ErrorEventArgs args)
		{
			d.LogError("JSON Error: " + args.ErrorContext.Error.Message + "  Path=" + args.ErrorContext.Path + "  FullException=" + args.ErrorContext.Error.ToString());
		}
	};

	public float Radius => Mathf.Max(m_TechData.m_BoundsExtents.x, m_TechData.m_BoundsExtents.z) + 0.5f;

	public IntVector3 Bounds => m_TechData.m_BoundsExtents;

	public TechData GetTechDataFormatted()
	{
		bool flag = SKU.IsNetEase && !m_TechData.HasLocalisedName;
		bool flag2 = m_DefaultControlSchemes != null && m_DefaultControlSchemes.Length != 0;
		if (m_TechData != null && (flag || flag2))
		{
			TechData shallowClonedCopy = m_TechData.GetShallowClonedCopy();
			if (flag2)
			{
				shallowClonedCopy.MakeSaveStateDictionaryUnique();
				shallowClonedCopy.SetControlSchemesFromPreset(m_DefaultControlSchemes);
			}
			if (flag)
			{
				shallowClonedCopy.Name = Singleton.Manager<Localisation>.inst.GetFallbackNetEaseNameForTech(m_TechData.Name.GetHashCode());
			}
			return shallowClonedCopy;
		}
		return m_TechData;
	}

	public TechData GetTechDataRaw()
	{
		return m_TechData;
	}

	public string GetName()
	{
		if (SKU.IsNetEase && !m_TechData.HasLocalisedName)
		{
			return Singleton.Manager<Localisation>.inst.GetFallbackNetEaseNameForTech(m_TechData.Name.GetHashCode());
		}
		return m_TechData.Name;
	}

	public int GetValue()
	{
		return m_TechData.GetValue();
	}

	public void SaveTank(Tank tank, bool saveRuntimeState = false, bool saveMetaData = true)
	{
		m_TechData.SaveTech(tank, saveRuntimeState, saveMetaData);
		m_NumUniqueBlockTypes = 0;
		HashSet<int> hashSet = new HashSet<int>();
		foreach (BlockSpec blockSpec in m_TechData.m_BlockSpecs)
		{
			if (!hashSet.Contains((int)blockSpec.m_BlockType))
			{
				hashSet.Add((int)blockSpec.m_BlockType);
				m_NumUniqueBlockTypes++;
			}
		}
	}

	public void OnAfterDeserialize()
	{
		m_TechData.UpdateFromDeprecatedBounds();
	}

	public void OnBeforeSerialize()
	{
	}

	public static TankPreset CreateInstance()
	{
		TankPreset tankPreset = ScriptableObject.CreateInstance<TankPreset>();
		tankPreset.m_TechData = new TechData();
		return tankPreset;
	}

	public static bool CheckBlockSpecListsMatch(BlockSpec[] specA, BlockSpec[] specB)
	{
		int num = ((specA != null) ? specA.Length : 0);
		int num2 = ((specB != null) ? specB.Length : 0);
		bool result;
		if (num == num2)
		{
			result = true;
			IntVector3 rootOffset = new IntVector3(Vector3.one * BlockManager.DefaultBlockLimit * 0.5f);
			for (int i = 0; i < num; i++)
			{
				if (specA[i].GetSpecHash(rootOffset) != specB[i].GetSpecHash(rootOffset))
				{
					result = false;
					break;
				}
			}
		}
		else
		{
			result = false;
		}
		return result;
	}

	public void NetSerialize(NetworkWriter writer)
	{
		m_TechData.NetSerialize(writer);
	}

	public void NetDeserialize(NetworkReader reader)
	{
		m_TechData.NetDeserialize(reader);
	}
}
