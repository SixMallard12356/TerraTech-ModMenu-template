#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class TerrainObjectTable : ScriptableObject
{
	[Serializable]
	private struct PrefabWithGUID
	{
		public TerrainObject prefab;

		public string prefabName;

		public string guid;

		public bool isLegacy;
	}

	[SerializeField]
	private ResourceFileList[] m_TerrainObjectFolders;

	[Tooltip("Auto Generated - Do not Edit!")]
	[SerializeField]
	private List<PrefabWithGUID> m_TerrainObjects = new List<PrefabWithGUID>();

	[NonSerialized]
	private Dictionary<string, TerrainObject> m_GUIDToPrefabLookup;

	public TerrainObject GetPrefabFromSavedGUID(string guid)
	{
		TerrainObject value = null;
		if (!m_GUIDToPrefabLookup.TryGetValue(guid, out value))
		{
			d.LogError("TerrainObjectTable.GetPrefabFromSavedGUID - Saved GUID " + guid + " did not resolve to a valid prefab! Was the GUID changed, or prefab removed?");
		}
		return value;
	}

	public List<Visible> _DebugGetSceneryPrefabs()
	{
		List<Visible> list = new List<Visible>();
		foreach (PrefabWithGUID terrainObject in m_TerrainObjects)
		{
			if (!terrainObject.isLegacy)
			{
				Visible component = terrainObject.prefab.GetComponent<Visible>();
				if (component != null && component.type == ObjectTypes.Scenery)
				{
					list.Add(component);
				}
			}
		}
		return list;
	}

	public void InitLookupTable()
	{
		m_GUIDToPrefabLookup = new Dictionary<string, TerrainObject>(m_TerrainObjects.Count);
		foreach (PrefabWithGUID terrainObject in m_TerrainObjects)
		{
			m_GUIDToPrefabLookup.Add(terrainObject.guid, terrainObject.prefab);
		}
	}
}
