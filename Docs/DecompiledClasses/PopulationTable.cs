using System;
using System.Collections.Generic;
using UnityEngine;

public class PopulationTable : ScriptableObject
{
	[Serializable]
	public struct SearchFolders
	{
		public string m_FolderName;

		public bool m_IncludeSubfolders;
	}

	[Serializable]
	public struct FolderTechList
	{
		public string m_FolderName;

		public List<TankPreset> m_Presets;
	}

	[SerializeField]
	private SearchFolders[] m_SearchFolders;

	public FolderTechList[] m_FolderTechs;

	public TankPreset[] m_OfflineInvaderPresets;

	[HideInInspector]
	public bool m_Changed;

	public int GetPresetCount()
	{
		int num = 0;
		FolderTechList[] folderTechs = m_FolderTechs;
		for (int i = 0; i < folderTechs.Length; i++)
		{
			FolderTechList folderTechList = folderTechs[i];
			num += folderTechList.m_Presets.Count;
		}
		return num;
	}
}
