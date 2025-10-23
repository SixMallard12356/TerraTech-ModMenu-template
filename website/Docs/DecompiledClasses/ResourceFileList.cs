using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourceFileList
{
	public string m_Location;

	public string m_Suffix;

	public string m_SubFolderFilter;

	public bool m_IncludeChildren;

	public List<UnityEngine.Object> objects = new List<UnityEngine.Object>();

	public ResourceFileList(string location, string suffix, string filter = null)
	{
		m_Location = location;
		m_Suffix = suffix;
		m_SubFolderFilter = filter;
	}

	public void Rebuild(Type resourceType, string overrideFolders = null)
	{
		ClearFileList();
		ResourceFileSelection resourceFileSelection = default(ResourceFileSelection);
		resourceFileSelection.m_Location = m_Location;
		resourceFileSelection.m_Suffix = m_Suffix;
		resourceFileSelection.m_SubFolderFilter = m_SubFolderFilter;
		resourceFileSelection.m_IncludeChildren = m_IncludeChildren;
		resourceFileSelection.BuildList(objects, resourceType, overrideFolders);
	}

	public void ClearFileList()
	{
		objects.Clear();
	}
}
