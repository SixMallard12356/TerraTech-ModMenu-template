using System;
using UnityEngine;
using UnityEngine.UI;

public class UISumoPreset : MonoBehaviour
{
	public enum ContestantState
	{
		Inactive,
		Selecting,
		Ready
	}

	[SerializeField]
	private GameObject m_InactivePlayerRoot;

	[SerializeField]
	private UITechSelector m_TechSelector;

	[SerializeField]
	private GameObject m_ReadiedPlayerRoot;

	[SerializeField]
	private Image m_PresetImage;

	[SerializeField]
	private Text m_PresetName;

	[SerializeField]
	private Text m_CreatorName;

	public Event<UISumoPreset, ContestantState> OnContestantStateChanged;

	private Snapshot m_SelectedCapture;

	private ContestantState m_State;

	public ContestantState State => m_State;

	public Snapshot Snapshot => m_SelectedCapture;

	public void SetAvailableInventory(IInventory<BlockTypes> inventory)
	{
		m_TechSelector.SetInventory(inventory);
	}

	public void ClearContestant()
	{
		Clear();
		m_InactivePlayerRoot.SetActive(value: true);
		m_ReadiedPlayerRoot.SetActive(value: false);
		m_TechSelector.Hide();
		SetState(ContestantState.Inactive);
	}

	public void ShowTechSelect()
	{
		Clear();
		m_InactivePlayerRoot.SetActive(value: false);
		m_ReadiedPlayerRoot.SetActive(value: false);
		m_TechSelector.Show();
		SetState(ContestantState.Selecting);
	}

	public void PickSelectedPreset()
	{
		ConfirmPreset(m_SelectedCapture);
	}

	private void SelectPreset(Snapshot cap)
	{
		m_SelectedCapture = cap;
		if (m_PresetName != null)
		{
			m_PresetName.text = cap.techData.Name;
		}
	}

	private void ConfirmPreset(Snapshot capture)
	{
		ConfirmSelection(capture);
		m_InactivePlayerRoot.SetActive(value: false);
		m_ReadiedPlayerRoot.SetActive(value: true);
		m_TechSelector.Hide();
		SetState(ContestantState.Ready);
	}

	private void ConfirmSelection(Snapshot cap)
	{
		m_SelectedCapture = cap;
		m_SelectedCapture.ResolveThumbnail(delegate(Sprite s)
		{
			m_PresetImage.sprite = s;
		});
		if (m_PresetName != null)
		{
			m_PresetName.text = cap.techData.Name;
		}
		if (m_CreatorName != null)
		{
			string empty = string.Empty;
			empty = (cap.techData.m_CreationData.m_Creator.NullOrEmpty() ? ((cap.techData.m_CreationData.m_UserProfile != null) ? cap.techData.m_CreationData.m_UserProfile.m_Name : cap.creator) : cap.techData.m_CreationData.m_Creator);
			m_CreatorName.text = empty;
		}
	}

	private void Clear()
	{
		m_SelectedCapture = null;
		m_PresetImage.sprite = null;
		if (m_PresetName != null)
		{
			m_PresetName.text = string.Empty;
		}
		if (m_CreatorName != null)
		{
			m_CreatorName.text = string.Empty;
		}
	}

	private void SetState(ContestantState newState)
	{
		if (m_State != newState)
		{
			m_State = newState;
			OnContestantStateChanged.Send(this, m_State);
		}
	}

	private void OnListSelectEvent(Snapshot capture, int costUnused)
	{
		SelectPreset(capture);
	}

	private void OnTechPresetChosen(Snapshot capture)
	{
		ConfirmPreset(capture);
	}

	private void OnPool()
	{
		if ((bool)m_TechSelector)
		{
			UITechSelector techSelector = m_TechSelector;
			techSelector.OnListSelectEvent = (UITechSelector.SelectionEvent)Delegate.Combine(techSelector.OnListSelectEvent, new UITechSelector.SelectionEvent(OnListSelectEvent));
			m_TechSelector.OnSelectionAcceptedEvent.Subscribe(OnTechPresetChosen);
			m_TechSelector.SetCommunitySource(UITechSelector.CommunityTagGroup.Sumo);
			m_TechSelector.SetDisplayAvailableOnly(displayAvailableOnly: true);
		}
	}

	private void OnSpawn()
	{
		if ((bool)m_TechSelector)
		{
			ClearContestant();
		}
	}

	private void OnRecycle()
	{
		ClearContestant();
	}
}
