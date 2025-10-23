#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMissionList : MonoBehaviour
{
	[SerializeField]
	private UIMissionLogElement m_MissionListEntryPrefab;

	[SerializeField]
	private RectTransform m_MissionList;

	[SerializeField]
	private bool m_SortListOnAdd = true;

	private ToggleGroup m_MissionListToggleGroup;

	private UITableScroller m_ListScroller;

	private List<UIMissionLogElement> m_MissionListEntries = new List<UIMissionLogElement>();

	private Comparison<UIMissionLogElement> m_SortComparer;

	public int EntryCount => m_MissionListEntries.Count;

	public UIMissionLogElement AddEntry(EncounterDisplayData encounter)
	{
		UIMissionLogElement uIMissionLogElement = GetMissionLogElement(encounter.Identifier);
		if (!uIMissionLogElement)
		{
			Transform obj = m_MissionListEntryPrefab.transform.Spawn();
			obj.SetParent(m_MissionList, worldPositionStays: false);
			uIMissionLogElement = obj.GetComponent<UIMissionLogElement>();
			m_MissionListEntries.Add(uIMissionLogElement);
		}
		uIMissionLogElement.Init(encounter);
		uIMissionLogElement.SetToggled(toggleOn: false);
		uIMissionLogElement.SetToggleGroup(m_MissionListToggleGroup);
		if (m_SortListOnAdd)
		{
			SortList();
		}
		return uIMissionLogElement;
	}

	public UIMissionLogElement GetMissionLogElement(EncounterIdentifier encounterId)
	{
		UIMissionLogElement result = null;
		for (int i = 0; i < m_MissionListEntries.Count; i++)
		{
			if (m_MissionListEntries[i].Data.Identifier == encounterId)
			{
				result = m_MissionListEntries[i];
				break;
			}
		}
		return result;
	}

	public UIMissionLogElement GetLastRemovedEntry()
	{
		for (int num = m_MissionListEntries.Count - 1; num >= 0; num--)
		{
			if (m_MissionListEntries[num].IsShownAsRemoved())
			{
				return m_MissionListEntries[num];
			}
		}
		return null;
	}

	public void RemoveEntry(EncounterDisplayData encounter)
	{
		for (int i = 0; i < m_MissionListEntries.Count; i++)
		{
			if (m_MissionListEntries[i].Data == encounter)
			{
				m_MissionListEntries[i].SetToggleGroup(null);
				m_MissionListEntries[i].transform.SetParent(null, worldPositionStays: false);
				m_MissionListEntries[i].transform.Recycle();
				m_MissionListEntries.RemoveAt(i);
				break;
			}
		}
	}

	public EncounterDisplayData GetNextNonRemovedEntry(EncounterDisplayData encounter)
	{
		EncounterDisplayData result = null;
		int num = m_MissionListEntries.FindIndex((UIMissionLogElement x) => x.Data == encounter);
		if (num != -1)
		{
			int num2 = ((num + 1 < m_MissionListEntries.Count) ? m_MissionListEntries.FindIndex(num + 1, (UIMissionLogElement x) => !x.IsShownAsRemoved()) : (-1));
			if (num2 == -1 && num > 0)
			{
				num2 = m_MissionListEntries.FindLastIndex(num - 1, (UIMissionLogElement x) => !x.IsShownAsRemoved());
			}
			if (num2 != -1)
			{
				result = m_MissionListEntries[num2].Data;
			}
		}
		else
		{
			d.LogError("GetNextNonRemovedEntry - Failed to find current element " + encounter.Title + " in the list!");
		}
		return result;
	}

	public void Clear()
	{
		for (int i = 0; i < m_MissionListEntries.Count; i++)
		{
			m_MissionListEntries[i].SetToggleGroup(null);
			m_MissionListEntries[i].transform.SetParent(null, worldPositionStays: false);
			m_MissionListEntries[i].transform.Recycle();
		}
		m_MissionListEntries.Clear();
		m_SortComparer = null;
	}

	public EncounterDisplayData GetNextEncounter(int offset)
	{
		bool flag = false;
		for (int i = 0; i < m_MissionListEntries.Count; i++)
		{
			if (m_MissionListEntries[i].IsToggled())
			{
				flag = true;
				if (i + offset >= 0 && i + offset < m_MissionListEntries.Count)
				{
					return m_MissionListEntries[i + offset].Data;
				}
			}
		}
		if (!flag && m_MissionListEntries.Count > 0)
		{
			return m_MissionListEntries[0].Data;
		}
		return null;
	}

	public void SelectEncounter(EncounterDisplayData encounter, bool scrollToEncounter = false)
	{
		UIMissionLogElement uIMissionLogElement = null;
		for (int i = 0; i < m_MissionListEntries.Count; i++)
		{
			if (m_MissionListEntries[i].Data == encounter)
			{
				uIMissionLogElement = m_MissionListEntries[i];
				if (scrollToEncounter && m_ListScroller != null)
				{
					m_ListScroller.ScrollToEntry(i);
				}
				break;
			}
		}
		if (uIMissionLogElement != null)
		{
			uIMissionLogElement.SetToggled(toggleOn: true);
		}
	}

	public void SetSortComparer(Comparison<UIMissionLogElement> sortComparer)
	{
		m_SortComparer = sortComparer;
	}

	private void SortList()
	{
		m_MissionListEntries.Sort(m_SortComparer ?? new Comparison<UIMissionLogElement>(MissionEntrySorter));
		for (int i = 0; i < m_MissionListEntries.Count; i++)
		{
			m_MissionListEntries[i].transform.SetAsLastSibling();
		}
	}

	private static int MissionEntrySorter(UIMissionLogElement a, UIMissionLogElement b)
	{
		int num = 0;
		num = ((int)a.Data.Corp).CompareTo((int)b.Data.Corp);
		if (num == 0)
		{
			num = a.Data.Grade.CompareTo(b.Data.Grade);
			if (num == 0)
			{
				bool isCoreEncounter = a.Data.Identifier.IsCoreEncounter;
				bool isCoreEncounter2 = b.Data.Identifier.IsCoreEncounter;
				if (isCoreEncounter != isCoreEncounter2)
				{
					num = ((!isCoreEncounter) ? 1 : (-1));
				}
			}
		}
		if (num == 0)
		{
			num = a.Data.Identifier.m_Name.CompareTo(b.Data.Identifier.m_Name);
		}
		return num;
	}

	private void OnPool()
	{
		m_MissionListToggleGroup = m_MissionList.GetComponent<ToggleGroup>();
		m_ListScroller = m_MissionList.GetComponent<UITableScroller>();
	}

	private void OnRecycle()
	{
		Clear();
	}
}
