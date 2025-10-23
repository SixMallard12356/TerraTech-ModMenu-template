#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ResourceFileSelection
{
	public string m_Location;

	public string m_Suffix;

	public string m_SubFolderFilter;

	public bool m_IncludeChildren;

	private static string assetPrefix = "Assets/";

	public void BuildList(List<UnityEngine.Object> outObjects, Type resourceType, string overrideFolders = null)
	{
		d.LogError("Cannot rebuild file list in ResourceFileSelection except in editor");
	}
}
