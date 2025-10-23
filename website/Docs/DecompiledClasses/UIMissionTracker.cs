using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMissionTracker : UIHUDElement
{
	private class NewMissionElement
	{
		public Transform transform;

		public float timeRemaining;

		public UIMissionLogElement uiElement;
	}

	[SerializeField]
	private RectTransform m_TrackedMissionPanel;

	[SerializeField]
	private UIMissionLogElement m_TrackedMissionDetails;

	[SerializeField]
	private RectTransform m_NewMissionListVLG;

	[SerializeField]
	private UIMissionLogElement m_NewMissionPrefab;

	[SerializeField]
	private int m_MaxMissionCount = 15;

	[SerializeField]
	private float m_NewMissionDisplayTime = 10f;

	[SerializeField]
	private RectTransform m_UpdatedMissionsObject;

	[SerializeField]
	private Text m_UpdatedMissionsCountField;

	private List<NewMissionElement> m_NewMissionElements = new List<NewMissionElement>();

	public override void Show(object context)
	{
		if (!base.IsVisible)
		{
			base.Show(context);
			SetupForEncounter(Singleton.Manager<ManQuestLog>.inst.GetTrackedEncounterDisplayData());
			UpdateRecentMissionUpdatesIndicator();
		}
	}

	private void UpdateRecentMissionUpdatesIndicator()
	{
		int numRecentEncounterUpdates = Singleton.Manager<ManQuestLog>.inst.NumRecentEncounterUpdates;
		if (m_UpdatedMissionsObject != null)
		{
			m_UpdatedMissionsObject.gameObject.SetActive(numRecentEncounterUpdates > 0);
		}
		if (m_UpdatedMissionsCountField != null)
		{
			m_UpdatedMissionsCountField.text = numRecentEncounterUpdates.ToString();
		}
	}

	private void RemoveNewMissionElement(int idx)
	{
		m_NewMissionElements[idx].uiElement.OnItemSelected.Unsubscribe(OnNewMissionClicked);
		m_NewMissionElements[idx].transform.SetParent(null, worldPositionStays: false);
		m_NewMissionElements[idx].transform.Recycle();
		m_NewMissionElements.RemoveAt(idx);
	}

	private void SetupForEncounter(EncounterDisplayData encounterDisplay)
	{
		m_TrackedMissionPanel.gameObject.SetActive(encounterDisplay != null);
		m_TrackedMissionDetails.Init(encounterDisplay);
	}

	private void OnTrackedEncounterChanged()
	{
		SetupForEncounter(Singleton.Manager<ManQuestLog>.inst.GetTrackedEncounterDisplayData());
	}

	private void OnMissionAddedToLog(EncounterDisplayData encounterDisplay, bool restoredFromSaveData)
	{
		if (m_NewMissionListVLG != null && m_NewMissionPrefab != null && !restoredFromSaveData && m_NewMissionListVLG.childCount < m_MaxMissionCount)
		{
			Transform transform = m_NewMissionPrefab.transform.Spawn();
			transform.SetParent(m_NewMissionListVLG, worldPositionStays: false);
			UIMissionLogElement component = transform.GetComponent<UIMissionLogElement>();
			component.Init(encounterDisplay);
			component.OnItemSelected.Subscribe(OnNewMissionClicked);
			NewMissionElement item = new NewMissionElement
			{
				transform = transform,
				uiElement = component,
				timeRemaining = m_NewMissionDisplayTime
			};
			m_NewMissionElements.Add(item);
		}
		UpdateRecentMissionUpdatesIndicator();
	}

	private void OnNewMissionClicked(EncounterDisplayData encounterDisplayData)
	{
		for (int i = 0; i < m_NewMissionElements.Count; i++)
		{
			if (m_NewMissionElements[i].uiElement.Data == encounterDisplayData)
			{
				RemoveNewMissionElement(i);
				break;
			}
		}
	}

	private void OnMissionRemovedFromLog(EncounterIdentifier encounterId, ManEncounter.FinishState state)
	{
		for (int i = 0; i < m_NewMissionElements.Count; i++)
		{
			if (m_NewMissionElements[i].uiElement.Data.Identifier == encounterId)
			{
				RemoveNewMissionElement(i);
				break;
			}
		}
		UpdateRecentMissionUpdatesIndicator();
	}

	private void OnRecentEncountersUpdated(EncounterIdentifier id, bool addedUnused)
	{
		UpdateRecentMissionUpdatesIndicator();
	}

	private void OnPool()
	{
		RegisterObscuredBy(ManHUD.HUDElementType.SkinsPalette);
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManQuestLog>.inst.OnEncounterAdded.Subscribe(OnMissionAddedToLog);
		Singleton.Manager<ManQuestLog>.inst.OnEncounterRemoved.Subscribe(OnMissionRemovedFromLog);
		Singleton.Manager<ManQuestLog>.inst.OnRecentEncountersUpdated.Subscribe(OnRecentEncountersUpdated);
		Singleton.Manager<ManQuestLog>.inst.OnTrackedEncounterChanged.Subscribe(OnTrackedEncounterChanged);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManQuestLog>.inst.OnEncounterAdded.Unsubscribe(OnMissionAddedToLog);
		Singleton.Manager<ManQuestLog>.inst.OnEncounterRemoved.Unsubscribe(OnMissionRemovedFromLog);
		Singleton.Manager<ManQuestLog>.inst.OnRecentEncountersUpdated.Unsubscribe(OnRecentEncountersUpdated);
		Singleton.Manager<ManQuestLog>.inst.OnTrackedEncounterChanged.Unsubscribe(OnTrackedEncounterChanged);
	}

	private void Update()
	{
		for (int num = m_NewMissionElements.Count - 1; num >= 0; num--)
		{
			m_NewMissionElements[num].timeRemaining -= Time.deltaTime;
			if (m_NewMissionElements[num].timeRemaining <= 0f)
			{
				RemoveNewMissionElement(num);
			}
		}
	}
}
