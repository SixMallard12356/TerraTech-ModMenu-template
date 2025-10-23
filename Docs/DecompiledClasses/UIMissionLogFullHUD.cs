#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMissionLogFullHUD : UIHUDElement, IMoveHandler, IEventSystemHandler, ISubmitHandler, ICancelHandler, IUIExtraButtonHandler2
{
	public struct MissionLogShowContext
	{
		public EncounterDisplayData encounterDisplayData;
	}

	[SerializeField]
	private UIMissionList m_MissionList;

	[SerializeField]
	private UIMissionDetails m_MissionDetails;

	[SerializeField]
	private Button m_CancelMissionButton;

	[SerializeField]
	private Button m_CantCancelMissionButton;

	[SerializeField]
	private LocalisedString m_CancelMissionTooltipTextNormal;

	[SerializeField]
	private LocalisedString m_CancelMissionTooltipTextTooClose;

	[SerializeField]
	private LocalisedString m_CancelMissionConfirmation;

	[SerializeField]
	private Button m_ShowMissionMarkerButton;

	[SerializeField]
	private Button m_ClaimRewardButton;

	private EncounterDisplayData m_SelectedEncounter;

	private DoubleClickListener m_DoubleClickListener = new DoubleClickListener();

	private TooltipComponent m_CancelMissionButtonTooltip;

	public override void Show(object context)
	{
		CleanupEntriesShownAsRemoved();
		base.Show(context);
		EncounterDisplayData encounterDisplayData = null;
		if (context != null)
		{
			encounterDisplayData = ((MissionLogShowContext)context).encounterDisplayData;
		}
		if (encounterDisplayData == null && Singleton.Manager<ManQuestLog>.inst.HasTrackedEncounter)
		{
			encounterDisplayData = Singleton.Manager<ManQuestLog>.inst.GetTrackedEncounterDisplayData();
		}
		bool scrollToEncounter = encounterDisplayData != null;
		SelectEncounter(encounterDisplayData, scrollToEncounter);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.UIMissionLog);
		Singleton.Manager<ManLicenses>.inst.ShowAllXpBarsEvent.Send(paramA: true);
	}

	public void TrackSelectedEncounterOnHUD()
	{
		if (m_SelectedEncounter != null && m_SelectedEncounter.Identifier != Singleton.Manager<ManQuestLog>.inst.TrackedEncounterId)
		{
			Singleton.Manager<ManQuestLog>.inst.SetTrackedEncounter(m_SelectedEncounter.Identifier);
			UpdateMissionButton();
		}
	}

	public void CancelSelectedEncounter()
	{
		if (!(m_SelectedEncounter != null))
		{
			return;
		}
		UIScreenNotifications uIScreenNotifications = (UIScreenNotifications)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen);
		uIScreenNotifications.Set(m_CancelMissionConfirmation.Value, delegate
		{
			if (CanCancelMission())
			{
				Singleton.Manager<ManQuestLog>.inst.CancelEncounter(m_SelectedEncounter.Identifier);
			}
			Singleton.Manager<ManUI>.inst.RemovePopup();
			EventSystem.current.SetSelectedGameObject(base.gameObject);
		}, delegate
		{
			Singleton.Manager<ManUI>.inst.RemovePopup();
			EventSystem.current.SetSelectedGameObject(base.gameObject);
		}, Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29), Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30));
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
	}

	private void CleanupEntriesShownAsRemoved()
	{
		while (m_MissionList.GetLastRemovedEntry() != null)
		{
			UIMissionLogElement lastRemovedEntry = m_MissionList.GetLastRemovedEntry();
			if (m_SelectedEncounter == lastRemovedEntry.Data)
			{
				m_SelectedEncounter = null;
			}
			lastRemovedEntry.OnItemSelected.Unsubscribe(OnItemSelected);
			m_MissionList.RemoveEntry(lastRemovedEntry.Data);
		}
	}

	public override void Hide(object context)
	{
		CleanupEntriesShownAsRemoved();
		base.Hide(context);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.UIMissionLog);
		Singleton.Manager<ManLicenses>.inst.ShowAllXpBarsEvent.Send(paramA: false);
	}

	public void CloseQuestLog()
	{
		HideSelf();
	}

	private void SelectEncounter(EncounterDisplayData encounter, bool scrollToEncounter = true)
	{
		m_SelectedEncounter = encounter;
		m_MissionList.SelectEncounter(encounter, scrollToEncounter);
		if ((bool)m_CancelMissionButtonTooltip)
		{
			m_CancelMissionButtonTooltip.SetForceDisplay(active: false);
		}
		m_MissionDetails.SetEncounter(encounter);
		TrackSelectedEncounterOnHUD();
		UpdateMissionButton();
		if (encounter != null)
		{
			Singleton.Manager<ManQuestLog>.inst.ClearRecentlyUpdatedEncounter(encounter.Identifier);
		}
	}

	private void UpdateMissionButton()
	{
		UpdateCancelMissionButton();
	}

	public bool CanCancelMission()
	{
		bool flag = false;
		if (m_SelectedEncounter != null && m_SelectedEncounter.CanBeCancelled)
		{
			if (m_SelectedEncounter.HasPosition)
			{
				flag = true;
				foreach (Tank allPlayerTech in Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs())
				{
					float sqrMagnitude = (m_SelectedEncounter.ScenePosition - allPlayerTech.boundsCentreWorld).ToVector2XZ().sqrMagnitude;
					float minEncounterCancelDistance = Singleton.Manager<ManEncounter>.inst.MinEncounterCancelDistance;
					if (sqrMagnitude < minEncounterCancelDistance * minEncounterCancelDistance)
					{
						flag = false;
						break;
					}
				}
			}
			else
			{
				flag = true;
			}
		}
		if (flag && m_SelectedEncounter != null)
		{
			UIMissionLogElement missionLogElement = m_MissionList.GetMissionLogElement(m_SelectedEncounter.Identifier);
			if ((bool)missionLogElement && missionLogElement.IsShownAsRemoved())
			{
				flag = false;
			}
		}
		return flag;
	}

	private void UpdateCancelMissionButton()
	{
		if (m_CancelMissionButton != null)
		{
			bool flag = m_SelectedEncounter != null && m_SelectedEncounter.CanBeCancelled;
			if (flag)
			{
				UIMissionLogElement missionLogElement = m_MissionList.GetMissionLogElement(m_SelectedEncounter.Identifier);
				if ((bool)missionLogElement && missionLogElement.IsShownAsRemoved())
				{
					flag = false;
				}
			}
			m_CancelMissionButton.gameObject.SetActive(flag);
			if (flag)
			{
				bool flag2 = CanCancelMission();
				m_CancelMissionButton.interactable = flag2;
				if (m_CancelMissionButtonTooltip != null)
				{
					LocalisedString localisedString = (flag2 ? m_CancelMissionTooltipTextNormal : m_CancelMissionTooltipTextTooClose);
					m_CancelMissionButtonTooltip.SetText(localisedString.Value);
					m_CancelMissionButtonTooltip.SetMode((!flag2) ? UITooltipOptions.Warning : UITooltipOptions.Default);
				}
			}
		}
		if (m_CantCancelMissionButton != null)
		{
			bool active = m_SelectedEncounter != null && !m_SelectedEncounter.CanBeCancelled;
			m_CantCancelMissionButton.gameObject.SetActive(active);
		}
	}

	private void OnItemSelected(EncounterDisplayData encounterDisplayData)
	{
		bool doubleClickConditionPassed = m_SelectedEncounter != null && m_SelectedEncounter == encounterDisplayData;
		if (m_DoubleClickListener.WasClickEventDoubleClick(doubleClickConditionPassed))
		{
			d.Assert(m_MissionDetails.Data == m_SelectedEncounter, "UIMissionLogFullHUD.OnItemSelected - Encounter in list was double clicked on, but details pane was never updated to show this encounter !?");
			TrackSelectedEncounterOnHUD();
		}
		else
		{
			SelectEncounter(encounterDisplayData);
		}
	}

	private void OnMissionAddedToLog(EncounterDisplayData encounterDisplayData, bool restoredFromSaveData)
	{
		UIMissionLogElement uIMissionLogElement = m_MissionList.AddEntry(encounterDisplayData);
		uIMissionLogElement.OnItemSelected.Clear();
		uIMissionLogElement.OnItemSelected.Subscribe(OnItemSelected);
		if (m_SelectedEncounter == encounterDisplayData)
		{
			SelectEncounter(m_SelectedEncounter);
		}
	}

	private void OnMissionRemoveFromLog(EncounterIdentifier encounterId, ManEncounter.FinishState state)
	{
		UIMissionLogElement missionLogElement = m_MissionList.GetMissionLogElement(encounterId);
		if (missionLogElement != null)
		{
			missionLogElement.ShowAsRemoved((state != ManEncounter.FinishState.Completed) ? UIMissionLogElement.RemoveType.Cancelled : UIMissionLogElement.RemoveType.Complete);
			if (m_SelectedEncounter != null && encounterId == m_SelectedEncounter.Identifier)
			{
				EncounterDisplayData trackedEncounterDisplayData = Singleton.Manager<ManQuestLog>.inst.GetTrackedEncounterDisplayData();
				SelectEncounter(trackedEncounterDisplayData);
			}
			else
			{
				UpdateCancelMissionButton();
			}
		}
	}

	public void OnMove(AxisEventData eventData)
	{
		EncounterDisplayData encounterDisplayData = null;
		if (eventData.moveDir == MoveDirection.Up)
		{
			encounterDisplayData = m_MissionList.GetNextEncounter(-1);
		}
		if (eventData.moveDir == MoveDirection.Down)
		{
			encounterDisplayData = m_MissionList.GetNextEncounter(1);
		}
		if (encounterDisplayData != null)
		{
			SelectEncounter(encounterDisplayData);
		}
		eventData.Use();
	}

	public void OnUIExtraButton2(BaseEventData eventData)
	{
		if (CanCancelMission())
		{
			CancelSelectedEncounter();
		}
		else if ((bool)m_CancelMissionButtonTooltip && m_CancelMissionButton.isActiveAndEnabled)
		{
			m_CancelMissionButtonTooltip.SetForceDisplay(active: true);
		}
		eventData.Use();
	}

	public void OnSubmit(BaseEventData eventData)
	{
	}

	public void OnCancel(BaseEventData eventData)
	{
		HideSelf();
		eventData.Use();
	}

	private void OnPool()
	{
		if (m_CancelMissionButton != null)
		{
			m_CancelMissionButtonTooltip = m_CancelMissionButton.GetComponentInChildren<TooltipComponent>();
		}
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManQuestLog>.inst.OnEncounterAdded.Subscribe(OnMissionAddedToLog);
		Singleton.Manager<ManQuestLog>.inst.OnEncounterRemoved.Subscribe(OnMissionRemoveFromLog);
		AddElementToGroup(ManHUD.HUDGroup.Main);
		AddElementToGroup(ManHUD.HUDGroup.GamepadCursorHidingElements);
		AddElementToGroup(ManHUD.HUDGroup.GamepadQuickMenuHUDElements);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManQuestLog>.inst.OnEncounterAdded.Unsubscribe(OnMissionAddedToLog);
		Singleton.Manager<ManQuestLog>.inst.OnEncounterRemoved.Unsubscribe(OnMissionRemoveFromLog);
	}

	private void Update()
	{
		if (base.IsVisible && m_SelectedEncounter != null && m_SelectedEncounter.CanBeCancelled)
		{
			UpdateCancelMissionButton();
		}
	}
}
