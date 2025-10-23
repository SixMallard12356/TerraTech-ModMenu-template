#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMissionBoard : UIHUDElement, IMoveHandler, IEventSystemHandler, ISubmitHandler, ICancelHandler
{
	public struct Context
	{
		public TechQuestGiver questGiver;
	}

	[SerializeField]
	private UIMissionList m_MissionList;

	[SerializeField]
	private UIMissionDetails m_MissionDetails;

	[SerializeField]
	private Button m_AcceptMissionButton;

	[SerializeField]
	private Button m_AcceptMissionCantCancelButton;

	[SerializeField]
	private RectTransform m_ScanningForMissionsTrans;

	[SerializeField]
	private RectTransform m_NoMissionsFoundTrans;

	[Tooltip("The transform to display when missions are not available at all (eg Creative mode)")]
	[SerializeField]
	private RectTransform m_MissionsNotAvailableTrans;

	public Event<EncounterToSpawn> OnEncounterStarted;

	private EncounterDisplayData m_SelectedEncounter;

	private TechQuestGiver m_QuestGiver;

	public int NumMissions => m_MissionList.EntryCount;

	public override void Show(object context)
	{
		d.Assert(context != null, "UIMissionBoard.Show was called without context! Needs context to determine mission display params!");
		if (!base.IsVisible && context != null)
		{
			base.Show(context);
			SelectEncounter(null);
			Redraw(((Context)context).questGiver);
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.UIMissionLog);
			Singleton.Manager<ManLicenses>.inst.ShowAllXpBarsEvent.Send(paramA: true);
		}
	}

	public override void Hide(object context)
	{
		m_QuestGiver.OnLocalEncounterFoundEvent.Unsubscribe(OnLocalEncounterFound);
		m_QuestGiver.OnFinishedCollectingLocalEncountersEvent.Unsubscribe(OnFinishedCollectingLocalEncounters);
		m_QuestGiver = null;
		SelectEncounter(null);
		m_MissionList.Clear();
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.UIMissionLog);
		Singleton.Manager<ManLicenses>.inst.ShowAllXpBarsEvent.Send(paramA: false);
		base.Hide(context);
	}

	public void ForceRefresh()
	{
		if (m_QuestGiver != null)
		{
			m_QuestGiver.Refresh();
		}
		Redraw(m_QuestGiver);
	}

	public void StartSelectedEncounter()
	{
		if (m_SelectedEncounter != null)
		{
			Singleton.Manager<ManEncounter>.inst.RequestStartEncounter(m_SelectedEncounter.EncounterToSpawn);
		}
	}

	public void CloseMissionBoard()
	{
		HideSelf();
	}

	public void ForceUpdateUI()
	{
		UpdateUI();
	}

	private void Redraw(TechQuestGiver questGiver)
	{
		m_MissionList.Clear();
		d.Assert(questGiver != null, "UIMissionBoard.Show was called without valid questGiver context param! Needed to determine missions to display!");
		m_QuestGiver = questGiver;
		m_QuestGiver.OnLocalEncounterFoundEvent.Unsubscribe(OnLocalEncounterFound);
		m_QuestGiver.OnFinishedCollectingLocalEncountersEvent.Unsubscribe(OnFinishedCollectingLocalEncounters);
		m_QuestGiver.OnLocalEncounterFoundEvent.Subscribe(OnLocalEncounterFound);
		m_QuestGiver.OnFinishedCollectingLocalEncountersEvent.Subscribe(OnFinishedCollectingLocalEncounters);
		UpdateUI();
	}

	private void UpdateUI()
	{
		bool flag = ManNetwork.IsNetworked || Singleton.Manager<ManEncounter>.inst.UpdateEnabled;
		if (m_MissionsNotAvailableTrans != null)
		{
			m_MissionsNotAvailableTrans.gameObject.SetActive(!flag);
		}
		if (m_ScanningForMissionsTrans != null)
		{
			m_ScanningForMissionsTrans.gameObject.SetActive(flag && m_QuestGiver.IsBusyScanning);
		}
		if (m_NoMissionsFoundTrans != null)
		{
			bool active = flag && !m_QuestGiver.IsBusyScanning && m_MissionList.EntryCount == 0;
			m_NoMissionsFoundTrans.gameObject.SetActive(active);
		}
		ShowHideButtonPrompt();
	}

	private void ShowHideButtonPrompt()
	{
		if (base.IsVisible && Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			if (m_MissionList.EntryCount > 0)
			{
				Singleton.Manager<ManBtnPrompt>.inst.UpdateCurrentHUDPrompt(ManBtnPrompt.PromptType.ContextScroll);
			}
			else
			{
				Singleton.Manager<ManBtnPrompt>.inst.HidePromptType(ManBtnPrompt.PromptType.ContextScroll);
			}
		}
	}

	private void SelectEncounter(EncounterDisplayData encounterDisplayData, bool scrollToEncounter = true)
	{
		m_SelectedEncounter = encounterDisplayData;
		if (encounterDisplayData != null)
		{
			m_MissionList.SelectEncounter(encounterDisplayData, scrollToEncounter);
			m_MissionDetails.SetEncounter(encounterDisplayData);
		}
		else
		{
			m_MissionList.SelectEncounter(null);
			m_MissionDetails.SetEncounter(null);
		}
		bool interactable = false;
		bool flag = true;
		if (m_SelectedEncounter != null)
		{
			UIMissionLogElement missionLogElement = m_MissionList.GetMissionLogElement(m_SelectedEncounter.Identifier);
			if (missionLogElement != null)
			{
				interactable = !missionLogElement.IsShownAsRemoved();
				flag = m_SelectedEncounter.CanBeCancelled;
			}
		}
		m_AcceptMissionButton.interactable = interactable;
		m_AcceptMissionCantCancelButton.gameObject.SetActive(!flag);
	}

	private void AddEncounter(EncounterDisplayData encounterDisplay)
	{
		UIMissionLogElement uIMissionLogElement = m_MissionList.AddEntry(encounterDisplay);
		uIMissionLogElement.OnItemSelected.Clear();
		uIMissionLogElement.OnItemSelected.Subscribe(OnItemSelected);
		if (m_SelectedEncounter == null)
		{
			SelectEncounter(encounterDisplay);
		}
		UpdateUI();
	}

	private void OnItemSelected(EncounterDisplayData encounterDisplayData)
	{
		SelectEncounter(encounterDisplayData);
	}

	private void OnLocalEncounterFound(EncounterDisplayData foundEncounter)
	{
		if (base.IsVisible)
		{
			AddEncounter(foundEncounter);
		}
	}

	private void OnFinishedCollectingLocalEncounters()
	{
		if (base.IsVisible)
		{
			UpdateUI();
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

	public void OnSubmit(BaseEventData eventData)
	{
		StartSelectedEncounter();
		eventData.Use();
		ShowHideButtonPrompt();
	}

	public void OnCancel(BaseEventData eventData)
	{
		HideSelf();
		eventData.Use();
	}

	private void OnClientMissionStarted(EncounterToSpawn encounterToSpawn, NetPlayer fromPlayer, bool setTracked)
	{
		OnEncounterStarted.Send(encounterToSpawn);
		UIMissionLogElement missionLogElement = m_MissionList.GetMissionLogElement(encounterToSpawn.m_EncounterDef);
		if (!(missionLogElement != null))
		{
			return;
		}
		missionLogElement.ShowAsRemoved(UIMissionLogElement.RemoveType.Accepted);
		if (missionLogElement.Data == m_SelectedEncounter)
		{
			EncounterDisplayData nextNonRemovedEntry = m_MissionList.GetNextNonRemovedEntry(missionLogElement.Data);
			if (nextNonRemovedEntry != null)
			{
				SelectEncounter(nextNonRemovedEntry);
			}
			else
			{
				m_AcceptMissionButton.interactable = false;
			}
		}
	}

	private void OnSpawn()
	{
		AddElementToGroup(ManHUD.HUDGroup.Main);
		AddElementToGroup(ManHUD.HUDGroup.GamepadCursorHidingElements);
		AddElementToGroup(ManHUD.HUDGroup.GamepadQuickMenuHUDElements);
		Singleton.Manager<ManEncounter>.inst.ClientEncounterStartedEvent.Subscribe(OnClientMissionStarted);
	}

	private void OnRecycle()
	{
		d.Assert(m_QuestGiver == null, "UIMissionBoard was recylced without being hidden first!");
		Singleton.Manager<ManEncounter>.inst.ClientEncounterStartedEvent.Unsubscribe(OnClientMissionStarted);
	}
}
