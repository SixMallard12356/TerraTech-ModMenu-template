using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class ModContents : ScriptableObject
{
	public string ModName = "";

	public ModBase Script;

	public PublishedFileId_t m_WorkshopId = PublishedFileId_t.Invalid;

	public List<ModdedSkinDefinition> m_Skins = new List<ModdedSkinDefinition>();

	public List<ModdedCorpDefinition> m_Corps = new List<ModdedCorpDefinition>();

	public List<ModdedBlockDefinition> m_Blocks = new List<ModdedBlockDefinition>();

	public List<Object> m_AdditionalAssets = new List<Object>();

	public override string ToString()
	{
		return ModName;
	}

	public IEnumerable<Object> FindAllAssets(string id)
	{
		int num = id.LastIndexOf('.');
		string strippedExt = ((num == -1) ? id : id.Substring(0, num));
		if (m_AdditionalAssets == null)
		{
			yield break;
		}
		foreach (Object additionalAsset in m_AdditionalAssets)
		{
			if (additionalAsset.name == id || additionalAsset.name == strippedExt)
			{
				yield return additionalAsset;
			}
		}
	}

	public Object FindAsset(string id)
	{
		int num = id.LastIndexOf('.');
		string text = ((num == -1) ? id : id.Substring(0, num));
		if (m_AdditionalAssets != null)
		{
			foreach (Object additionalAsset in m_AdditionalAssets)
			{
				if (additionalAsset.name == id || additionalAsset.name == text)
				{
					return additionalAsset;
				}
			}
		}
		return null;
	}
}
