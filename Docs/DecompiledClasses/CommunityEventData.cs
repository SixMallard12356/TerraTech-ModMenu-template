using System.Reflection;
using UnityEngine;

public class CommunityEventData : ScriptableObject
{
	[SerializeField]
	[EncryptedField(Type = typeof(string), Salt = "8UIYsgi1Jh")]
	private string WorldBiome_Hash;

	[SerializeField]
	[EncryptedField(Type = typeof(string), Salt = "2sDbrjtZq8")]
	private string WorldSeed_Hash;

	[SerializeField]
	[EncryptedField(Type = typeof(Vector3), Salt = "aSioI8oPul3")]
	private string WorldPos_Hash;

	[EncryptedField(Type = typeof(string), Salt = "rbCvQ6md4x")]
	[SerializeField]
	private string Block1_Hash;

	[EncryptedField(Type = typeof(string), Salt = "9yr920nhf")]
	[SerializeField]
	private string Block2_Hash;

	[EncryptedField(Type = typeof(string), Salt = "0a9sfn32gq")]
	[SerializeField]
	private string Block3_Hash;

	[SerializeField]
	[EncryptedField(Type = typeof(TankPreset), Salt = "p6mUnWQTqtP")]
	private string Tech_Hash;

	[SerializeField]
	[EncryptedField(Type = typeof(string), Salt = "mved4kMDd")]
	private string Message_Hash;

	[SerializeField]
	private EncounterData m_Handshake_EncounterData;

	public EncounterData Handshake_EncounterData => m_Handshake_EncounterData;

	public bool CheckBiome(string biome)
	{
		return CheckProp("WorldBiome_Hash", WorldBiome_Hash, biome);
	}

	public bool CheckWorld(string seedStr)
	{
		return CheckProp("WorldSeed_Hash", WorldSeed_Hash, seedStr);
	}

	public bool CheckPos(Vector3 pos)
	{
		return CheckProp("WorldPos_Hash", WorldPos_Hash, pos);
	}

	public bool CheckBlockContainerId(string blockId)
	{
		return CheckProp("Block1_Hash", Block1_Hash, blockId);
	}

	public bool CheckBlockSourceId(string blockId)
	{
		return CheckProp("Block2_Hash", Block2_Hash, blockId);
	}

	public bool CheckBlock(string blockId)
	{
		return CheckProp("Block3_Hash", Block3_Hash, blockId);
	}

	public bool CheckMessage(string messageStr)
	{
		return CheckProp("Message_Hash", Message_Hash, messageStr);
	}

	private bool CheckProp(string propName, string propValue, object testData)
	{
		string text = typeof(CommunityEventData).GetField(propName, BindingFlags.Instance | BindingFlags.NonPublic).GetCustomAttribute<EncryptedFieldAttribute>().Encrypt(testData);
		return propValue == text;
	}
}
