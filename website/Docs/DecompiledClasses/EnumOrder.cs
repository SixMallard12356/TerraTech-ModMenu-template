#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnumOrder : ISerializationCallbackReceiver
{
	[SerializeField]
	private List<string> m_StringOrder;

	private Type m_Type;

	private Dictionary<int, int> m_Lookup;

	public EnumOrder(Type type)
	{
		d.AssertFormat(type.IsEnum, "EnumOrder constructed with non enum type {0}", type);
		m_Type = type;
		m_StringOrder = new List<string>();
		m_StringOrder.AddRange(Enum.GetNames(m_Type));
		m_Lookup = new Dictionary<int, int>();
		RebuildLookup();
	}

	public bool LookupOrder(int enumVal, out int order)
	{
		bool result = false;
		order = 0;
		if (m_Lookup.TryGetValue(enumVal, out order))
		{
			result = true;
		}
		return result;
	}

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		string[] names = Enum.GetNames(m_Type);
		for (int num = m_StringOrder.Count - 1; num >= 0; num--)
		{
			try
			{
				Enum.Parse(m_Type, m_StringOrder[num]);
			}
			catch (ArgumentException)
			{
				m_StringOrder.RemoveAt(num);
			}
		}
		for (int i = 0; i < names.Length; i++)
		{
			bool flag = false;
			for (int j = 0; j < m_StringOrder.Count; j++)
			{
				if (m_StringOrder[j] == names[i])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				m_StringOrder.Add(names[i]);
			}
		}
		RebuildLookup();
	}

	private void RebuildLookup()
	{
		m_Lookup.Clear();
		for (int i = 0; i < m_StringOrder.Count; i++)
		{
			try
			{
				object obj = Enum.Parse(m_Type, m_StringOrder[i]);
				m_Lookup[(int)obj] = i;
			}
			catch (ArgumentException)
			{
				d.AssertFormat(false, "Unexpected value {0} not in type {1}", m_StringOrder[i], m_Type);
			}
		}
	}
}
