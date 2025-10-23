using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TerraTech/Table/ReferenceList")]
public class ReferenceList : ScriptableObject
{
	public List<GameObject> mVanillaEntries = new List<GameObject>();

	public string m_Type = "";

	private Dictionary<string, GameObject> mRuntimeDictionary = new Dictionary<string, GameObject>();

	public void Init()
	{
		mRuntimeDictionary.Clear();
		foreach (GameObject mVanillaEntry in mVanillaEntries)
		{
			mRuntimeDictionary.Add(mVanillaEntry.name, mVanillaEntry);
		}
	}

	public T Find<T>(string key) where T : Component
	{
		if (mRuntimeDictionary.TryGetValue(key, out var value))
		{
			return value.GetComponent<T>();
		}
		return null;
	}
}
