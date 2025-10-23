#define UNITY_EDITOR
using System.Collections;
using System.IO;
using Netease.Oddish.Ingame.Sdk;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameScene : MonoBehaviour
{
	[SerializeField]
	private string[] m_StartupBundleNames;

	[SerializeField]
	private string m_StartupSceneName;

	[SerializeField]
	public bool m_LoadAll;

	[SerializeField]
	public Image[] m_FadeOut;

	[SerializeField]
	public DLCTable m_DlcTable;

	private void Start()
	{
		d.Log("[LoadGameScene:Start()] Time: " + Time.realtimeSinceStartup);
		StartCoroutine(LoadGameSceneAsync());
	}

	public IEnumerator LoadGameSceneAsync()
	{
		d.Log("[LoadGameScene:LoadGameSceneAsync()] Begin loading, time: " + Time.realtimeSinceStartup);
		yield return null;
		d.Assert(m_DlcTable != null, "[LoadGameScene] No dlc table");
		yield return ManNetEase.LoadDLCEntitlements_Coroutine(m_DlcTable);
		string[] startupBundleNames = m_StartupBundleNames;
		foreach (string bundleName in startupBundleNames)
		{
			d.Log("[LoadGameScene:LoadGameSceneAsync()] Load AssetBundle " + bundleName + ", time: " + Time.realtimeSinceStartup);
			AssetBundleCreateRequest bundleCreateRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, bundleName));
			yield return bundleCreateRequest;
			AssetBundle assetBundle = bundleCreateRequest.assetBundle;
			d.Log("[LoadGameScene:LoadGameSceneAsync()] Loaded AssetBundle " + bundleName + ", time: " + Time.realtimeSinceStartup);
			if (m_LoadAll && assetBundle != null && !assetBundle.isStreamedSceneAssetBundle)
			{
				yield return assetBundle.LoadAllAssetsAsync();
				d.Log("[LoadGameScene:LoadGameSceneAsync()] Loaded assets from " + bundleName + ", time: " + Time.realtimeSinceStartup);
			}
		}
		d.Log("[LoadGameScene:LoadGameSceneAsync()] LoadSceneAsync, time: " + Time.realtimeSinceStartup);
		AsyncOperation async = SceneManager.LoadSceneAsync(m_StartupSceneName, LoadSceneMode.Single);
		async.allowSceneActivation = false;
		while (!async.isDone)
		{
			if (async.progress >= 0.9f)
			{
				if (!async.allowSceneActivation)
				{
					d.Log("[LoadGameScene:LoadGameSceneAsync()] Loading complete, time: " + Time.realtimeSinceStartup);
					async.allowSceneActivation = true;
				}
				Image[] fadeOut = m_FadeOut;
				foreach (Image image in fadeOut)
				{
					if ((bool)image)
					{
						Color color = image.color;
						color.a = Mathf.Clamp01(color.a - Time.deltaTime * 2f);
						image.color = color;
					}
				}
			}
			yield return null;
		}
	}

	private void OnDestroy()
	{
		d.Log("[LoadGameScene:OnDestroy()] Time: " + Time.realtimeSinceStartup);
	}

	public void OnApplicationQuit()
	{
		if (SKU.IsNetEase)
		{
			OddishSdk.Dispose();
		}
	}
}
