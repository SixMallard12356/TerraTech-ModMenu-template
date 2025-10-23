using System.Collections.Generic;
using System.IO;

public class ModSessionInfo
{
	public static readonly ModSessionInfo VanillaSession = new ModSessionInfo();

	public static readonly ModSessionInfo VanillaMPClientSession = new ModSessionInfo
	{
		m_Multiplayer = true,
		m_Authoritative = false
	};

	public bool m_Multiplayer;

	public bool m_Authoritative = true;

	public Dictionary<int, Dictionary<int, string>> SkinIDsByCorp = new Dictionary<int, Dictionary<int, string>>();

	public Dictionary<string, ulong> Mods { get; } = new Dictionary<string, ulong>();

	public Dictionary<int, string> CorpIDs { get; } = new Dictionary<int, string>();

	public Dictionary<int, string> SkinIDs { get; set; }

	public Dictionary<int, string> BlockIDs { get; } = new Dictionary<int, string>();

	public IEnumerator<ModContainer> GetEnumerator()
	{
		foreach (KeyValuePair<string, ulong> mod in Mods)
		{
			if (Singleton.Manager<ManMods>.inst.ModExists(mod.Key))
			{
				yield return Singleton.Manager<ManMods>.inst.FindMod(mod.Key);
			}
		}
	}

	public static bool UsesSameMods(ModSessionInfo a, ModSessionInfo b)
	{
		if (a == null && b == null)
		{
			return true;
		}
		if (a == null != (b == null))
		{
			return false;
		}
		if (a.Mods.Count != b.Mods.Count)
		{
			return false;
		}
		foreach (KeyValuePair<string, ulong> mod in a.Mods)
		{
			if (!b.Mods.TryGetValue(mod.Key, out var value) || mod.Value != value)
			{
				return false;
			}
		}
		if (a.CorpIDs.Count != b.CorpIDs.Count)
		{
			return false;
		}
		foreach (KeyValuePair<int, string> corpID in a.CorpIDs)
		{
			if (!b.CorpIDs.TryGetValue(corpID.Key, out var value2) || corpID.Value != value2)
			{
				return false;
			}
		}
		if ((a.SkinIDs == null || a.SkinIDs.Count == 0) != (b.SkinIDs == null || b.SkinIDs.Count == 0))
		{
			return false;
		}
		if (a.SkinIDs != null && b.SkinIDs != null)
		{
			foreach (KeyValuePair<int, string> skinID in a.SkinIDs)
			{
				if (!b.SkinIDs.TryGetValue(skinID.Key, out var value3) || skinID.Value != value3)
				{
					return false;
				}
			}
		}
		if (a.SkinIDsByCorp.Count != b.SkinIDsByCorp.Count)
		{
			return false;
		}
		foreach (KeyValuePair<int, Dictionary<int, string>> item in a.SkinIDsByCorp)
		{
			if (!b.SkinIDsByCorp.TryGetValue(item.Key, out var value4))
			{
				return false;
			}
			foreach (KeyValuePair<int, string> item2 in item.Value)
			{
				if (!value4.TryGetValue(item.Key, out var value5) || item2.Value != value5)
				{
					return false;
				}
			}
		}
		if (a.BlockIDs.Count != b.BlockIDs.Count)
		{
			return false;
		}
		foreach (KeyValuePair<int, string> blockID in a.BlockIDs)
		{
			if (!b.BlockIDs.TryGetValue(blockID.Key, out var value6) || blockID.Value != value6)
			{
				return false;
			}
		}
		return true;
	}

	public bool AddSkinToCorp(int skinID, string skinCompoundID, string corpID)
	{
		int corpIndex = (int)Singleton.Manager<ManMods>.inst.GetCorpIndex(corpID, this);
		if (corpIndex != 0)
		{
			if (!SkinIDsByCorp.ContainsKey(corpIndex))
			{
				SkinIDsByCorp[corpIndex] = new Dictionary<int, string>();
			}
			SkinIDsByCorp[corpIndex][skinID] = skinCompoundID;
			return true;
		}
		return false;
	}

	public IEnumerable<int> GetAllCorpIDs()
	{
		foreach (KeyValuePair<int, string> corpID in CorpIDs)
		{
			yield return corpID.Key;
		}
	}

	public void Write(BinaryWriter writer)
	{
		writer.Write(Mods.Count);
		writer.Write(CorpIDs.Count);
		writer.Write(SkinIDsByCorp.Count);
		writer.Write(BlockIDs.Count);
		foreach (KeyValuePair<string, ulong> mod in Mods)
		{
			writer.Write(mod.Key);
			writer.Write(mod.Value);
		}
		foreach (KeyValuePair<int, string> corpID in CorpIDs)
		{
			writer.Write(corpID.Key);
			writer.Write(corpID.Value);
		}
		foreach (KeyValuePair<int, Dictionary<int, string>> item in SkinIDsByCorp)
		{
			writer.Write(item.Key);
			writer.Write(item.Value.Count);
			foreach (KeyValuePair<int, string> item2 in item.Value)
			{
				writer.Write(item2.Key);
				writer.Write(item2.Value);
			}
		}
		foreach (KeyValuePair<int, string> blockID in BlockIDs)
		{
			writer.Write(blockID.Key);
			writer.Write(blockID.Value);
		}
	}

	public void Read(BinaryReader reader)
	{
		int num = reader.ReadInt32();
		int num2 = reader.ReadInt32();
		int num3 = reader.ReadInt32();
		int num4 = reader.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			Mods.Add(reader.ReadString(), reader.ReadUInt64());
		}
		for (int j = 0; j < num2; j++)
		{
			CorpIDs.Add(reader.ReadInt32(), reader.ReadString());
		}
		for (int k = 0; k < num3; k++)
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			SkinIDsByCorp.Add(reader.ReadInt32(), dictionary);
			int num5 = reader.ReadInt32();
			for (int l = 0; l < num5; l++)
			{
				dictionary.Add(reader.ReadInt32(), reader.ReadString());
			}
		}
		for (int m = 0; m < num4; m++)
		{
			BlockIDs.Add(reader.ReadInt32(), reader.ReadString());
		}
		m_Authoritative = false;
		m_Multiplayer = true;
	}
}
