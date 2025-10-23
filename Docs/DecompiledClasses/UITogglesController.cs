#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITogglesController
{
	private class ToggleState
	{
		public bool m_Interactable;

		public bool m_Active;
	}

	private struct ToggleEntry
	{
		public ToggleWrapper m_Toggle;

		public int m_SelectionIndex;
	}

	public EventNoParams OnChanged;

	private ToggleWrapper m_AllToggle;

	private List<ToggleEntry> m_Entries = new List<ToggleEntry>();

	private Dictionary<int, ToggleState> m_Fields = new Dictionary<int, ToggleState>();

	public List<int> Selection
	{
		get
		{
			List<int> list = new List<int>(m_Fields.Count);
			foreach (KeyValuePair<int, ToggleState> field in m_Fields)
			{
				if (field.Value.m_Active)
				{
					list.Add(field.Key);
				}
			}
			return list;
		}
	}

	public bool AllSelected
	{
		get
		{
			foreach (ToggleState value in m_Fields.Values)
			{
				if (value.m_Interactable && !value.m_Active)
				{
					return false;
				}
			}
			return true;
		}
	}

	private bool MultiFiltersEnabled => Input.GetKey(KeyCode.LeftControl);

	public void AddToggle(Toggle toggle, int corpIndex)
	{
		toggle.isOn = false;
		ToggleWrapper toggleWrapper = new ToggleWrapper(toggle);
		ToggleEntry item = new ToggleEntry
		{
			m_Toggle = toggleWrapper,
			m_SelectionIndex = corpIndex
		};
		item.m_Toggle.OnToggled.Subscribe(delegate(bool enabled)
		{
			OnToggle(toggleWrapper, corpIndex, enabled);
		});
		m_Entries.Add(item);
		m_Fields.Add(corpIndex, new ToggleState
		{
			m_Active = false,
			m_Interactable = true
		});
	}

	public void SetAllToggle(Toggle toggle)
	{
		d.Assert(m_AllToggle == null, "UIFilterToggles.SetAllToggle already has an m_AllToggle");
		m_AllToggle = ((toggle != null) ? new ToggleWrapper(toggle) : null);
		if (m_AllToggle != null)
		{
			m_AllToggle.isOn = false;
			m_AllToggle.OnToggled.Subscribe(ToggleAll);
		}
	}

	public void SetToggleSelected(int selectionInd, bool selected)
	{
		ToggleWrapper toggle = GetToggle(selectionInd);
		if (toggle != null)
		{
			toggle.isOn = selected;
		}
	}

	public void SetToggleInteractable(int selectionIndex, bool interactable)
	{
		ToggleWrapper toggle = GetToggle(selectionIndex);
		if (toggle == null || toggle.interactable == interactable)
		{
			return;
		}
		bool flag = interactable && m_AllToggle != null && m_AllToggle.isOn;
		toggle.SetOnWithoutCallback(flag);
		if (m_Fields.TryGetValue(selectionIndex, out var value))
		{
			bool num = value.m_Active != flag;
			if (num)
			{
				value.m_Active = flag;
			}
			value.m_Interactable = interactable;
			toggle.interactable = interactable;
			if (num)
			{
				OnChanged.Send();
			}
		}
		else
		{
			d.LogWarning($"Could not get toggle with index {selectionIndex}");
		}
	}

	public ToggleWrapper GetToggle(int selectionInd)
	{
		ToggleWrapper result = null;
		for (int i = 0; i < m_Entries.Count; i++)
		{
			ToggleEntry toggleEntry = m_Entries[i];
			if (toggleEntry.m_SelectionIndex == selectionInd)
			{
				result = toggleEntry.m_Toggle;
				break;
			}
		}
		return result;
	}

	public ToggleWrapper GetAllToggle()
	{
		return m_AllToggle;
	}

	public void Clear()
	{
		for (int i = 0; i < m_Entries.Count; i++)
		{
			m_Entries[i].m_Toggle.OnToggled.Clear();
		}
		m_Entries.Clear();
		if (m_AllToggle != null)
		{
			m_AllToggle.OnToggled.Clear();
			m_AllToggle = null;
		}
		m_Fields.Clear();
	}

	public void SetAllToggleSelected(bool selected)
	{
		if (m_AllToggle != null)
		{
			m_AllToggle.isOn = selected;
		}
	}

	public void CycleSingleToggle(bool forward = true)
	{
		ToggleWrapper toggleWrapper = null;
		int num = ((!forward) ? (m_Entries.Count - 1) : 0);
		int num2 = (forward ? m_Entries.Count : (-1));
		int num3 = (forward ? 1 : (-1));
		bool flag = m_AllToggle != null && m_AllToggle.interactable && m_AllToggle.transform.gameObject.activeInHierarchy;
		if (flag && m_AllToggle.isOn)
		{
			m_AllToggle.SetOnWithoutCallback(enabled: false);
			for (int i = num; i != num2; i += num3)
			{
				if (m_Entries[i].m_Toggle != null && m_Entries[i].m_Toggle.interactable && m_Entries[i].m_Toggle.transform.gameObject.activeInHierarchy)
				{
					toggleWrapper = m_Entries[i].m_Toggle;
					break;
				}
			}
		}
		else
		{
			int num4 = -1;
			int num5 = -1;
			for (int j = num; j != num2; j += num3)
			{
				if (m_Entries[j].m_Toggle == null || !m_Entries[j].m_Toggle.interactable || !m_Entries[j].m_Toggle.transform.gameObject.activeInHierarchy)
				{
					continue;
				}
				if (m_Entries[j].m_Toggle.isOn)
				{
					num5 = j;
					continue;
				}
				if (num5 >= 0)
				{
					num4 = j;
					break;
				}
				if (num4 == -1)
				{
					num4 = j;
				}
			}
			if (flag && (forward ? (num4 < num5) : (num4 > num5)))
			{
				ToggleAll(enabled: true);
			}
			else if (num4 >= 0)
			{
				toggleWrapper = m_Entries[num4].m_Toggle;
			}
		}
		toggleWrapper?.InvokeToggleHandler(enable: true, allowMultiFilter: false);
	}

	private void OnToggle(ToggleWrapper toggleWrapper, int fieldIndex, bool toggleOn, bool allowMultiFilter = true)
	{
		allowMultiFilter = allowMultiFilter && toggleWrapper.AllowMultiFilter;
		if (allowMultiFilter && MultiFiltersEnabled)
		{
			if (m_Fields.TryGetValue(fieldIndex, out var value))
			{
				bool allSelected = AllSelected;
				value.m_Active = toggleOn;
				if (m_AllToggle != null)
				{
					if (AllSelected)
					{
						m_AllToggle.SetOnWithoutCallback(enabled: true);
					}
					else if (allSelected)
					{
						m_AllToggle.SetOnWithoutCallback(enabled: false);
					}
				}
			}
			else
			{
				d.LogWarning($"Could not find UI Toggle with index {fieldIndex}");
			}
		}
		else
		{
			toggleOn = true;
			for (int i = 0; i < m_Entries.Count; i++)
			{
				ToggleEntry toggleEntry = m_Entries[i];
				if (toggleEntry.m_Toggle.interactable)
				{
					bool flag = toggleEntry.m_SelectionIndex == fieldIndex;
					toggleEntry.m_Toggle.SetOnWithoutCallback(flag);
					if (m_Fields.TryGetValue(toggleEntry.m_SelectionIndex, out var value2))
					{
						value2.m_Active = flag;
					}
					else
					{
						d.LogWarning($"Could not find UI Toggle with index {toggleEntry.m_SelectionIndex}");
					}
				}
			}
			if (m_AllToggle != null)
			{
				m_AllToggle.SetOnWithoutCallback(enabled: false);
			}
		}
		OnChanged.Send();
	}

	private void ToggleAll(bool enabled)
	{
		enabled = true;
		m_AllToggle.SetOnWithoutCallback(enabled);
		for (int i = 0; i < m_Entries.Count; i++)
		{
			ToggleEntry toggleEntry = m_Entries[i];
			ToggleWrapper toggle = toggleEntry.m_Toggle;
			if (toggle != null && toggle.interactable && toggle.isOn != enabled)
			{
				toggle.SetOnWithoutCallback(enabled);
				if (m_Fields.TryGetValue(toggleEntry.m_SelectionIndex, out var value))
				{
					value.m_Active = enabled;
				}
				else
				{
					d.LogWarning($"Could not find UI Toggle with index {toggleEntry.m_SelectionIndex}");
				}
			}
		}
		OnChanged.Send();
	}
}
