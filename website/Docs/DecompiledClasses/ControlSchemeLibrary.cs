#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class ControlSchemeLibrary
{
	[SerializeField]
	[JsonProperty]
	private List<ControlScheme> m_Schemes = new List<ControlScheme>();

	private const int k_MaxSchemesPerTech = 5;

	private const int k_MaxSchemeNameLength = 18;

	private const int k_MaxSchemesInLibrary = 99;

	[JsonIgnore]
	public List<ControlScheme> Schemes => m_Schemes;

	public ControlSchemeLibrary CloneLibrary()
	{
		ControlSchemeLibrary controlSchemeLibrary = new ControlSchemeLibrary();
		controlSchemeLibrary.m_Schemes = new List<ControlScheme>(m_Schemes.Count);
		foreach (ControlScheme scheme in m_Schemes)
		{
			ControlScheme item = scheme.CreateCopy();
			controlSchemeLibrary.m_Schemes.Add(item);
		}
		return controlSchemeLibrary;
	}

	public ControlScheme LookupScheme(ControlScheme l)
	{
		if (l != null)
		{
			return GetControlSchemeByID(l.ID);
		}
		return null;
	}

	public int GetMaxSchemesPerTech()
	{
		return 5;
	}

	public static int GetMaxNameLength()
	{
		return 18;
	}

	public List<ControlScheme> LookupSchemes(List<ControlScheme> l)
	{
		List<ControlScheme> list = new List<ControlScheme>(l.Count);
		foreach (ControlScheme item in l)
		{
			ControlScheme controlSchemeByID = GetControlSchemeByID(item.ID);
			if (controlSchemeByID != null)
			{
				list.Add(controlSchemeByID);
			}
		}
		return list;
	}

	public int GetDisplaySortOrder(ControlScheme s)
	{
		return m_Schemes.IndexOf(s);
	}

	public void ApplyLibraryChangesFrom(ControlSchemeLibrary modified)
	{
		List<ControlScheme> list = new List<ControlScheme>(modified.m_Schemes.Count);
		foreach (ControlScheme scheme in modified.m_Schemes)
		{
			ControlScheme controlSchemeByID = GetControlSchemeByID(scheme.ID);
			if (controlSchemeByID != null)
			{
				controlSchemeByID.CopyDataFrom(scheme);
				list.Add(controlSchemeByID);
			}
			else
			{
				d.Assert(scheme.IsCustom, "Unexpected non-custom scheme when applying library changes");
				list.Add(scheme);
			}
		}
		m_Schemes = list;
		UpdateUnknownControlSchemesOnTechs();
	}

	public bool Equals(ControlSchemeLibrary other)
	{
		if (m_Schemes.Count != other.m_Schemes.Count)
		{
			return false;
		}
		for (int i = 0; i < m_Schemes.Count; i++)
		{
			if (!m_Schemes[i].Equals(other.m_Schemes[i]))
			{
				return false;
			}
		}
		return true;
	}

	private void UpdateUnknownControlSchemesOnTechs()
	{
		foreach (Tank item in Singleton.Manager<ManTechs>.inst.IterateTechs())
		{
			TankControl control = item.control;
			List<ControlScheme> schemes = control.Schemes;
			if (schemes == null || schemes.Count <= 0)
			{
				continue;
			}
			for (int num = schemes.Count - 1; num >= 0; num--)
			{
				if (!m_Schemes.Contains(schemes[num]))
				{
					ControlScheme controlSchemeByID = GetControlSchemeByID(schemes[num].ID);
					if (controlSchemeByID != null)
					{
						d.Log("Updating control scheme " + schemes[num].GetName() + " on tech " + item.name, item);
						schemes[num] = controlSchemeByID;
					}
					else
					{
						d.Log("Removing deleted control scheme " + schemes[num].GetName() + " from tech " + item.name, item);
						schemes.RemoveAt(num);
					}
				}
			}
			if (schemes.Count == 0)
			{
				d.Log("Removed all control schemes from tech (adding defaults) " + item.name, item);
				AssignDefaultControlSchemes(item);
			}
			else if (!schemes.Contains(control.ActiveScheme))
			{
				control.SetActiveScheme(schemes[0]);
			}
		}
	}

	public void AddDefaultSchemes(ManProfile.Profile profile)
	{
		m_Schemes.RemoveAll((ControlScheme s) => !s.IsCustom && GetControlScheme(s.Category) != s);
		List<ControlScheme> schemes = Singleton.Manager<ManInput>.inst.DefaultControlSchemes.m_Schemes;
		d.Assert(schemes != null && schemes.Count > 0, "No default control schemes found");
		foreach (ControlScheme item in schemes)
		{
			d.Assert(!item.IsCustom, "Unexpected custom scheme found in default scheme list " + item.GetName());
			ControlScheme controlScheme = GetControlScheme(item.Category);
			if (controlScheme == null)
			{
				controlScheme = new ControlScheme();
				controlScheme.CopyDataFrom(item);
				m_Schemes.Add(controlScheme);
			}
			else if (item.FormatVersion > controlScheme.FormatVersion)
			{
				controlScheme.CopyDataFrom(item);
			}
			controlScheme.Icon = item.Icon;
			controlScheme.IconColour = item.IconColour;
		}
	}

	public ControlScheme GetDefaultSettingsFor(ControlScheme s)
	{
		List<ControlScheme> schemes = Singleton.Manager<ManInput>.inst.DefaultControlSchemes.m_Schemes;
		d.Assert(!s.IsCustom, "Custom schemes do not have default settings");
		ControlScheme result = null;
		foreach (ControlScheme item in schemes)
		{
			if (item.Category == s.Category)
			{
				result = item;
				break;
			}
		}
		return result;
	}

	public void ResetToDefault(ControlScheme s)
	{
		d.Assert(!s.IsCustom, "Cannot restore custom schemes to default");
		ControlScheme defaultSettingsFor = GetDefaultSettingsFor(s);
		d.Assert(defaultSettingsFor != null, "Unable to find default version of scheme " + s.Category);
		if (defaultSettingsFor != null)
		{
			s.CopyDataFrom(defaultSettingsFor);
		}
	}

	public void AssignDefaultControlSchemes(Tank t)
	{
		AutoDefaultSchemeSelector autoDefaultSchemeSelector = new AutoDefaultSchemeSelector(this);
		autoDefaultSchemeSelector.UpdateFromTech(t);
		t.control.Schemes = autoDefaultSchemeSelector.Schemes;
	}

	public ControlScheme GetControlSchemeByName(string name)
	{
		foreach (ControlScheme scheme in m_Schemes)
		{
			if (scheme.GetName() == name)
			{
				return scheme;
			}
		}
		return null;
	}

	public ControlScheme GetControlScheme(ControlSchemeCategory id)
	{
		d.Assert(id != ControlSchemeCategory.Custom, "Calling GetControlScheme(ControlSchemeCategory) for a custom scheme is inappropriate.");
		foreach (ControlScheme scheme in m_Schemes)
		{
			if (scheme.Category == id)
			{
				return scheme;
			}
		}
		return null;
	}

	public ControlScheme GetControlSchemeByID(int id)
	{
		foreach (ControlScheme scheme in m_Schemes)
		{
			if (scheme.ID == id)
			{
				return scheme;
			}
		}
		return null;
	}

	public void UpdateControlSchemesForTech(List<ControlScheme> schemes, string techName, bool createMissing)
	{
		if (schemes == null)
		{
			return;
		}
		for (int i = 0; i < schemes.Count; i++)
		{
			if (!m_Schemes.Contains(schemes[i]))
			{
				ControlScheme controlScheme = null;
				if (!schemes[i].IsCustom)
				{
					controlScheme = GetControlScheme(schemes[i].Category);
				}
				if (controlScheme == null)
				{
					controlScheme = GetControlSchemeFromLoadedScheme(schemes[i]);
				}
				if (controlScheme == null && createMissing)
				{
					controlScheme = CreateSchemeFromLoaded(schemes[i], techName);
				}
				int num = schemes.IndexOf(controlScheme);
				bool flag = num < i && num != -1;
				schemes[i] = (flag ? null : controlScheme);
			}
		}
		schemes.RemoveAll((ControlScheme controlScheme2) => controlScheme2 == null);
	}

	public void RemoveControlScheme(ControlScheme scheme)
	{
		if (m_Schemes.Contains(scheme))
		{
			m_Schemes.Remove(scheme);
		}
		else
		{
			d.LogError("Attempting to remove unknown control scheme from library");
		}
	}

	public ControlScheme CreateNewCustomScheme(ControlScheme from, string oldName)
	{
		if (m_Schemes.Count >= 99)
		{
			return null;
		}
		oldName = oldName.Trim();
		int num = oldName.LastIndexOf('#');
		string text;
		if (num > 0 && int.TryParse(oldName.Substring(num + 1), out var result))
		{
			text = oldName.Substring(0, num);
		}
		else
		{
			text = oldName;
			result = 1;
		}
		string text2 = oldName;
		while (GetControlSchemeByName(text2) != null)
		{
			result++;
			text2 = text + "#" + result;
			while (text2.Length > GetMaxNameLength() && text.Length > 0)
			{
				text = text.Substring(0, text.Length - 1);
				text = text.Substring(0, text.Length - 1);
				text2 = text + "#" + result;
			}
		}
		ControlScheme controlScheme = from.CreateCopyAsNewCustom(text2);
		m_Schemes.Add(controlScheme);
		return controlScheme;
	}

	private ControlScheme CreateSchemeFromLoaded(ControlScheme loadedScheme, string techName)
	{
		string text = loadedScheme.GetName().Trim();
		if (text.Contains("("))
		{
			text = text.Substring(0, text.IndexOf("(")).Trim();
		}
		if (text.Length + techName.Length + 2 > 18)
		{
			int num = Mathf.Max(9, 16 - techName.Length);
			if (text.Length > num)
			{
				text = text.Substring(0, num);
			}
		}
		string text2 = text + "(" + techName + ")";
		if (text2.Length > 18)
		{
			text2 = text2.Substring(0, 15) + "...";
		}
		return CreateNewCustomScheme(loadedScheme, text2);
	}

	private ControlScheme GetControlSchemeFromLoadedScheme(ControlScheme loadedScheme)
	{
		foreach (ControlScheme scheme in m_Schemes)
		{
			if (scheme.Equals(loadedScheme))
			{
				return scheme;
			}
		}
		ControlScheme controlSchemeByID = GetControlSchemeByID(loadedScheme.ID);
		if (controlSchemeByID != null && controlSchemeByID.Category == loadedScheme.Category)
		{
			return controlSchemeByID;
		}
		foreach (ControlScheme scheme2 in m_Schemes)
		{
			if (scheme2.Equals(loadedScheme, requireNameMatch: false))
			{
				return scheme2;
			}
		}
		return null;
	}
}
