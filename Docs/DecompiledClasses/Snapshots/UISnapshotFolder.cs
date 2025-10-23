#define UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Snapshots;

[RequireComponent(typeof(Toggle))]
public class UISnapshotFolder : MonoBehaviour, UISnapshotPanel.ISelectable
{
	[SerializeField]
	protected RectTransform m_SnapshotItemContainer;

	[SerializeField]
	protected TextMeshProUGUI m_FolderTitle;

	[SerializeField]
	protected Toggle m_SelectedToggle;

	[SerializeField]
	protected Toggle m_ExpandToggle;

	[SerializeField]
	protected Sprite m_ExpandedIcon;

	[SerializeField]
	protected Sprite m_CollapsedIcon;

	public Event<string, bool> OnFolderExpandedEvent;

	public Event<SnapshotFolderLiveData> OnToggledTrue;

	protected HashSet<UISnapshotItem> m_CurrentSnapshotItems = new HashSet<UISnapshotItem>();

	protected SnapshotFolderLiveData m_SnapshotFolderData;

	protected RectTransform m_TransformAsRect;

	protected bool m_IsExpanded;

	public void UpdateExpanded()
	{
		OnFolderExpandedEvent.Send(m_SnapshotFolderData.Name, m_ExpandToggle.isOn);
	}

	public void SetExpanded(bool state, bool forceSet = false)
	{
		if (forceSet || m_IsExpanded != state)
		{
			m_IsExpanded = state;
			if (m_ExpandToggle.isOn != m_IsExpanded)
			{
				m_ExpandToggle.isOn = m_IsExpanded;
			}
			m_SnapshotItemContainer.gameObject.SetActive(m_IsExpanded);
			m_ExpandToggle.image.sprite = (m_IsExpanded ? m_ExpandedIcon : m_CollapsedIcon);
		}
	}

	public SnapshotFolderLiveData GetData()
	{
		return m_SnapshotFolderData;
	}

	public void SetData(SnapshotFolderLiveData liveData)
	{
		m_SnapshotFolderData = liveData;
		SetExpanded(liveData.IsExpanded);
		if (m_FolderTitle != null)
		{
			m_FolderTitle.text = m_SnapshotFolderData.Name;
		}
	}

	public void SetAllItems(IEnumerable<UISnapshotItem> items)
	{
		d.Assert(m_CurrentSnapshotItems.Count == 0, "Trying to set snapshot items in snapshot folder that already contains snapshots! If you're trying to add use AddItem instead!");
		RetrieveAndClearAllItems();
		foreach (UISnapshotItem item in items)
		{
			AddItem(item);
		}
	}

	public void AddItem(UISnapshotItem item)
	{
		m_CurrentSnapshotItems.Add(item);
		(item.transform as RectTransform).SetParent(m_SnapshotItemContainer);
	}

	public void RemoveItem(UISnapshotItem item)
	{
		m_CurrentSnapshotItems.Remove(item);
	}

	public UISnapshotItem[] RetrieveAndClearAllItems()
	{
		UISnapshotItem[] array = m_CurrentSnapshotItems.ToArray();
		UISnapshotItem[] array2 = array;
		foreach (UISnapshotItem item in array2)
		{
			RemoveItem(item);
		}
		return array;
	}

	public void ToggleSelected()
	{
		m_ExpandToggle.isOn = !m_ExpandToggle.isOn;
	}

	public void SetSelected(bool isSelected)
	{
		if (m_SelectedToggle != null)
		{
			m_SelectedToggle.onValueChanged.RemoveListener(OnToggleValueChanged);
			m_SelectedToggle.isOn = isSelected;
			m_SelectedToggle.onValueChanged.AddListener(OnToggleValueChanged);
		}
	}

	public void SetButtonGroup(ToggleGroup buttonToggleGroup)
	{
		if (m_SelectedToggle != null)
		{
			m_SelectedToggle.group = buttonToggleGroup;
		}
	}

	public void RefreshLayoutGroup()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(m_TransformAsRect);
	}

	private void OnToggleValueChanged(bool isOn)
	{
		if (isOn)
		{
			OnToggledTrue.Send(m_SnapshotFolderData);
		}
	}

	private void OnPool()
	{
		SetExpanded(state: false, forceSet: true);
		m_TransformAsRect = base.transform as RectTransform;
	}

	private void OnSpawn()
	{
		d.Assert(base.transform.parent != null, "Spawning 'Snapshot Folder' without a parent! This should not be orphaned!");
	}

	private void OnRecycle()
	{
		d.Assert(m_CurrentSnapshotItems.Count == 0, "Recycling Snapshot folder without removing all child snapshot items! This should be handled by the Snapshot Load Menu!!");
	}

	private void OnEnable()
	{
		if (m_SelectedToggle != null)
		{
			m_SelectedToggle.onValueChanged.AddListener(OnToggleValueChanged);
		}
	}

	private void OnDisable()
	{
		if (m_SelectedToggle != null)
		{
			m_SelectedToggle.onValueChanged.RemoveListener(OnToggleValueChanged);
		}
	}
}
