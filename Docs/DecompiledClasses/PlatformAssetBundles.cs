using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlatformAssetBundles", menuName = "Asset/PlatformAssetBundles")]
public class PlatformAssetBundles : ScriptableObject
{
	[SerializeField]
	private string m_AssetBundleBuildPath;

	[SerializeField]
	private string m_AssetBundleMovePath;

	[SerializeField]
	private List<string> m_CommonAssetBundles;

	[EnumArray(typeof(SKU.BuildType))]
	[SerializeField]
	private List<AssetBundleInformation> m_BuildTypes;

	public List<AssetBundleInformation> BuildTypes => m_BuildTypes;

	public string AssetBundleBuildPath => m_AssetBundleBuildPath;

	public string AssetBundleMovePath => m_AssetBundleMovePath;

	public List<string> CommonAssetBundles => m_CommonAssetBundles;
}
