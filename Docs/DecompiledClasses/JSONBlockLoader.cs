#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class JSONBlockLoader
{
	private static Dictionary<string, JSONModuleLoader> sLoaders;

	static JSONBlockLoader()
	{
		sLoaders = new Dictionary<string, JSONModuleLoader>();
		VanillaModuleLoaders.RegisterVanillaModules();
	}

	public static void RegisterModuleLoader(JSONModuleLoader loader)
	{
		if (sLoaders.ContainsKey(loader.GetModuleKey()))
		{
			d.LogError($"Duplicate ModuleLoader {loader.GetType()} with key {loader.GetModuleKey()}");
		}
		else
		{
			sLoaders.Add(loader.GetModuleKey(), loader);
		}
	}

	public static void Inject(int blockID, ModdedBlockDefinition def)
	{
		TextAsset json = def.m_Json;
		if (!(json != null))
		{
			return;
		}
		try
		{
			foreach (KeyValuePair<string, JToken> item in JObject.Parse(json.text))
			{
				if (sLoaders.TryGetValue(item.Key, out var value))
				{
					if (!value.InjectBlock(blockID, def, item.Value))
					{
						d.LogError($"Failed to parse module {item.Key} in JSON for {def}");
					}
				}
				else
				{
					d.LogError($"Could not parse module {item.Key} in JSON for {def}");
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public static void Load(ModContainer mod, int blockID, ModdedBlockDefinition def, TankBlock block)
	{
		if (!(def != null))
		{
			return;
		}
		JObject jObject = null;
		try
		{
			if (Singleton.Manager<ManMods>.inst.ShouldReadFromRawJSON)
			{
				string text = mod.AssetBundlePath.Substring(0, mod.AssetBundlePath.LastIndexOf('/')) + "/BlockJSON/" + def.name + ".json";
				if (File.Exists(text))
				{
					jObject = JObject.Parse(File.ReadAllText(text));
					d.Log("[Mods] Read JSON from " + text + " as an override");
				}
				else
				{
					d.Log("[Mods] Block " + def.name + " could not find a JSON override at " + text);
				}
			}
			if (jObject == null)
			{
				jObject = JObject.Parse(def.m_Json.text);
				d.Log("[Mods] Read JSON from asset bundle for " + def.name);
			}
			if (jObject == null)
			{
				return;
			}
			foreach (KeyValuePair<string, JToken> item in jObject)
			{
				if (sLoaders.TryGetValue(item.Key, out var value))
				{
					if (!value.CreateModuleForBlock(blockID, def, block, item.Value))
					{
						d.LogError($"Failed to parse module {item.Key} in JSON for {def}");
					}
				}
				else
				{
					d.LogError($"Could not parse module {item.Key} in JSON for {def}");
				}
			}
		}
		catch (Exception)
		{
		}
	}
}
