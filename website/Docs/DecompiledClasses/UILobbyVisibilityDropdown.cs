#define UNITY_EDITOR
using System.Collections.Generic;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
internal class UILobbyVisibilityDropdown : MonoBehaviour
{
	private static List<Lobby.LobbyVisibility> s_AllowedTypes;

	private Dropdown m_DropDown;

	public Dropdown DropDown => m_DropDown;

	public Lobby.LobbyVisibility Visibility
	{
		get
		{
			if (m_DropDown != null)
			{
				return DropdownIndexToVisibility(m_DropDown.value);
			}
			d.LogError("Trying to get visibility of dropdown with null dropdown");
			return Lobby.LobbyVisibility.Public;
		}
		set
		{
			if (m_DropDown != null)
			{
				if (VisibilityToDropdownIndex(value, out var index))
				{
					m_DropDown.SetValue(index);
					return;
				}
				d.LogErrorFormat("Unable to set visibility dropdown to unsupported value {0}", value);
			}
			else
			{
				d.LogError("Trying to set visibility of dropdown with null dropdown");
			}
		}
	}

	public static Lobby.LobbyVisibility DropdownIndexToVisibility(int index)
	{
		InitAllowedTypes();
		return s_AllowedTypes[index];
	}

	public static bool VisibilityToDropdownIndex(Lobby.LobbyVisibility vis, out int index)
	{
		InitAllowedTypes();
		for (int i = 0; i < s_AllowedTypes.Count; i++)
		{
			if (s_AllowedTypes[i] == vis)
			{
				index = i;
				return true;
			}
		}
		index = -1;
		return false;
	}

	public static void AddDropdownOptions(Dropdown dropdown)
	{
		InitAllowedTypes();
		List<string> list = new List<string>(s_AllowedTypes.Count);
		foreach (Lobby.LobbyVisibility s_AllowedType in s_AllowedTypes)
		{
			list.Add(Singleton.Manager<Localisation>.inst.GetLobbyVisibilityString(s_AllowedType));
		}
		dropdown.AddOptions(list);
	}

	private void OnSpawn()
	{
		m_DropDown = GetComponent<Dropdown>();
		if (m_DropDown != null && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem != null)
		{
			AddDropdownOptions(m_DropDown);
		}
	}

	private void OnRecycle()
	{
		if (m_DropDown != null)
		{
			m_DropDown.ClearOptions();
		}
	}

	private static void InitAllowedTypes()
	{
		if (s_AllowedTypes != null)
		{
			return;
		}
		s_AllowedTypes = new List<Lobby.LobbyVisibility>();
		EnumValuesIterator<Lobby.LobbyVisibility> enumerator = EnumIterator<Lobby.LobbyVisibility>.Values().GetEnumerator();
		while (enumerator.MoveNext())
		{
			Lobby.LobbyVisibility current = enumerator.Current;
			if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.SupportsVisibilityType(current))
			{
				s_AllowedTypes.Add(current);
			}
		}
	}
}
