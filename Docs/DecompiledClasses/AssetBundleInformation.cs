using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AssetBundleInformation
{
	[SerializeField]
	private List<string> m_RequiredAssetBundles;

	public List<string> RequiredAssetBundles => m_RequiredAssetBundles;
}
