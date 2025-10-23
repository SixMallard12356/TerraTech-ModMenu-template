#define UNITY_EDITOR
using System.Collections.Generic;

public class ModContainer
{
	public enum State
	{
		INVALID,
		FAILED,
		LOADED
	}

	private State m_State;

	private Dictionary<string, ModdedAsset> m_AssetLookup = new Dictionary<string, ModdedAsset>();

	public bool IsLoaded => m_State == State.LOADED;

	public bool InjectedEarlyHooks { get; private set; }

	public ModBase Script => Contents?.Script;

	public bool HasValidID => !ModID.NullOrEmpty();

	public string ModID { get; private set; }

	public string PublishedID { get; private set; }

	public bool IsRemote { get; set; }

	public string AssetBundlePath { get; private set; }

	public bool Local { get; private set; }

	public ModContents Contents { get; private set; }

	public ModContainer(string modName, string assetBundlePath, bool isLocal, string publishedID)
	{
		ModID = modName;
		AssetBundlePath = assetBundlePath;
		Local = isLocal;
		PublishedID = publishedID;
	}

	public void EarlyInit()
	{
		d.AssertFormat(m_State == State.LOADED, "Calling EarlyInit on a mod that did not successfully complete loading! ID:{0}", ModID);
		d.Assert(!InjectedEarlyHooks, "Trying to inject early mod hooks twice");
		if (!InjectedEarlyHooks)
		{
			if (Script != null && Script.HasEarlyInit())
			{
				Script.EarlyInit();
			}
			InjectedEarlyHooks = true;
		}
	}

	public void OnReloadRequested()
	{
		Contents = null;
		m_AssetLookup.Clear();
		m_State = State.INVALID;
	}

	public void OnLoadFailed()
	{
		d.AssertFormat(m_State == State.INVALID, "MOD state has already been set to {0} by another process! Completion should only happen once!", m_State);
		Contents = null;
		m_AssetLookup.Clear();
		m_State = State.FAILED;
	}

	public void OnLoadComplete(ModContents contents)
	{
		d.AssertFormat(m_State == State.INVALID, "MOD state has already been set to {0} by another process! Completion should only happen once!", m_State);
		Contents = contents;
		m_State = State.LOADED;
	}

	public void RegisterAsset(ModdedAsset asset)
	{
		if (m_AssetLookup.ContainsKey(asset.name))
		{
			d.LogError($"[Mods] Could not register asset {asset.name} of type {asset.GetType()} because asset of type {m_AssetLookup[asset.name].GetType()} is already registered with mod {ModID}");
		}
		else
		{
			m_AssetLookup.Add(asset.name, asset);
			d.Log("[Mods] Registered asset {" + asset.name + "}");
		}
	}

	public T FindAsset<T>(string assetId, bool errorIfMissing = true) where T : ModdedAsset
	{
		ModdedAsset value = null;
		if (m_AssetLookup.TryGetValue(assetId, out value))
		{
			d.Assert(value is T, $"[Mods] Found asset {assetId} in mod {ModID}, but type was {value.GetType()} instead of {typeof(T)}");
			if (value is T)
			{
				return (T)value;
			}
		}
		else if (errorIfMissing)
		{
			d.LogError("[Mods] Failed to lookup asset " + assetId + " in mod " + ModID);
		}
		return null;
	}
}
