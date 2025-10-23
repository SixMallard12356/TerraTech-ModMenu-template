#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
internal class BlockFilterComponentWhiteList : BlockFilterLookup.IFilterBuilder, ISerializationCallbackReceiver
{
	[SerializeField]
	private string[] m_AllowedModuleTypeNames;

	private HashSet<Type> m_AllowedModuleTypes;

	[SerializeField]
	private BlockTypes[] m_AllowedBlockTypes;

	private HashSet<BlockTypes> m_AllowedBlockTypesSet;

	private static List<Module> s_ModuleList = new List<Module>();

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
		m_AllowedModuleTypes = null;
		m_AllowedBlockTypesSet = null;
	}

	public bool CheckBlockAllowed(BlockTypes blockType, TankBlock prefab, bool logReason)
	{
		if (m_AllowedModuleTypes == null)
		{
			RebuildAllowedModuleTypes();
		}
		if (m_AllowedBlockTypesSet == null)
		{
			m_AllowedBlockTypesSet = new HashSet<BlockTypes>(m_AllowedBlockTypes, new BlockTypesComparer());
		}
		if (m_AllowedBlockTypesSet.Contains(blockType))
		{
			return true;
		}
		if (prefab == null)
		{
			d.LogFormat("Filter BlockFilterComponentWhiteList has blocked type {0} because it does not have a prefab in the Block table (hint: is it R&D?)", blockType);
			return false;
		}
		bool flag = false;
		prefab.GetComponentsInChildren(includeInactive: true, s_ModuleList);
		foreach (Module s_Module in s_ModuleList)
		{
			Type type = s_Module.GetType();
			if (!m_AllowedModuleTypes.Contains(type))
			{
				flag = true;
				if (logReason)
				{
					d.LogFormat("Filter BlockFilterComponentWhiteList has blocked type {0} because it contains illegal module {1}", blockType, type);
				}
				break;
			}
		}
		s_ModuleList.Clear();
		return !flag;
	}

	public List<Type> ListAllowedTypes()
	{
		if (m_AllowedModuleTypes == null)
		{
			RebuildAllowedModuleTypes();
		}
		List<Type> list = new List<Type>();
		foreach (Type allowedModuleType in m_AllowedModuleTypes)
		{
			list.Add(allowedModuleType);
		}
		return list;
	}

	private void RebuildAllowedModuleTypes()
	{
		m_AllowedModuleTypes = new HashSet<Type>();
		for (int i = 0; i < m_AllowedModuleTypeNames.Length; i++)
		{
			string text = m_AllowedModuleTypeNames[i];
			Type type = Type.GetType(text);
			if (type != null)
			{
				m_AllowedModuleTypes.Add(type);
				continue;
			}
			d.LogErrorFormat("Unable to find type from typename {0}", text);
		}
	}
}
