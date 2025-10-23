#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

internal class UIElementCache<T> where T : MonoBehaviour
{
	private T m_Prefab;

	private List<T> m_Entries;

	private int m_NumUsed;

	public UIElementCache(T prefab)
	{
		m_Prefab = prefab;
		m_Entries = new List<T>();
		m_NumUsed = 0;
	}

	public void SetNoneUsed()
	{
		m_NumUsed = 0;
	}

	public T Alloc(Transform parent)
	{
		T val;
		if (m_NumUsed >= m_Entries.Count)
		{
			val = m_Prefab.Spawn();
			bool worldPositionStays = false;
			val.transform.SetParent(parent, worldPositionStays);
			m_Entries.Add(val);
		}
		else
		{
			val = m_Entries[m_NumUsed];
			d.Assert(parent == val.transform.parent, "Broken assumption that parent remains the same");
			val.transform.SetAsLastSibling();
		}
		m_NumUsed++;
		return val;
	}

	public void FreeUnused()
	{
		for (int num = m_Entries.Count - 1; num >= m_NumUsed; num--)
		{
			T val = m_Entries[num];
			bool flag = false;
			val.transform.SetParent(null, flag);
			val.Recycle(flag);
		}
		m_Entries.RemoveRange(m_NumUsed, m_Entries.Count - m_NumUsed);
	}

	public void FreeAll()
	{
		SetNoneUsed();
		FreeUnused();
	}

	public void Free(T entry)
	{
		d.Assert(m_Entries.Contains(entry), "Can't free entry not in pool");
		entry.transform.SetParent(null, worldPositionStays: false);
		entry.Recycle(worldPosStays: false);
		m_Entries.Remove(entry);
		m_NumUsed--;
	}
}
