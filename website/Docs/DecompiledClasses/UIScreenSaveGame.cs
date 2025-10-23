#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Payload.UI.Saving;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIScreenSaveGame : UIScreen, IMoveHandler, IEventSystemHandler, ISubmitHandler, ICancelHandler, IUIExtraButtonHandler1, IUIExtraButtonHandler2
{
	[SerializeField]
	private RectTransform m_ContentHolder;

	[SerializeField]
	private UISave m_SavePrefab;

	[SerializeField]
	private ScrollRect m_ScrollRect;

	[SerializeField]
	private UISave m_AddSavePrefab;

	[SerializeField]
	private RectTransform m_LoadingGraphic;

	[SerializeField]
	private UnityEvent m_SaveDoubleClickAction;

	[SerializeField]
	private Button m_SaveButton;

	[SerializeField]
	private Button m_DeleteButton;

	[SerializeField]
	private Button m_RenameButton;

	[SerializeField]
	private LocalisedString m_SaveSuccessPrompt;

	[SerializeField]
	private LocalisedString m_ContinueLabel;

	[SerializeField]
	private LocalisedString m_CancelLabel;

	[SerializeField]
	private LocalisedString m_OverwritePrompt;

	[SerializeField]
	private LocalisedString m_OverwriteConfirmLabel;

	[SerializeField]
	private LocalisedString m_SaveNameErrorPrompt;

	[SerializeField]
	private LocalisedString m_DeletePrompt;

	[SerializeField]
	private LocalisedString m_DeleteConfirmLabel;

	[SerializeField]
	private GameObject m_PCButtonsPanel;

	private List<UISave> m_SaveSlotUIElements = new List<UISave>();

	private List<ManSaveGame.SaveFileSlot> m_SavesToLoad;

	private SaveOperation m_SaveOperation;

	private SaveOperation m_CreateOperation;

	private SaveOperation m_DeleteOperation;

	private SaveOperation m_RenameOperation;

	private ManGameMode.GameType m_GameTypeOfSaves = ManGameMode.GameType.MainGame;

	private int m_SavesToLoadProcessIndex;

	private int m_SavesSuccessfullyLoadedCount;

	private bool m_BusyLoadingSaves;

	private ToggleGroup m_ToggleGroup;

	private UISave m_ActiveSaveItem;

	private int m_CurrentSelectedIndex;

	private UISave m_AddNewSaveItem;

	private DoubleClickListener m_DoubleClickListener;

	private UIAutoScrollItemController m_AutoScrollController;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		ManGameMode.GameType currentGameType = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType();
		ShowSavesForType(currentGameType);
		Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(base.gameObject);
		if (SKU.ConsoleUI)
		{
			m_ExitButton.gameObject.SetActive(value: false);
			m_PCButtonsPanel.gameObject.SetActive(value: false);
			Singleton.Manager<ManUI>.inst.ToggleBtnPrompt(!ManSaveGame.UseFixedSlots, ManBtnPrompt.PromptType.SaveGameScreenRenameOption);
		}
	}

	public override void Hide()
	{
		Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(base.gameObject);
		ClearDisplayedSaves();
		base.Hide();
	}

	public void SaveGame()
	{
		if (Singleton.Manager<ManGameMode>.inst.GetIsModeSteamWorkshopSave())
		{
			UIScreenNotifications uIScreenNotifications = (UIScreenNotifications)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen);
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 23);
			uIScreenNotifications.Set(localisedString, delegate
			{
				Singleton.Manager<ManUI>.inst.RemovePopup();
				DoSaveGame();
			}, Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4));
			Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
		}
		else
		{
			DoSaveGame();
		}
	}

	public void DoSaveGame()
	{
		ManGameMode.GameType currentGameType = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType();
		if (m_ActiveSaveItem == null || m_ActiveSaveItem == m_AddNewSaveItem || m_ActiveSaveItem.SaveInfo == null)
		{
			string text = Singleton.Manager<ManSaveGame>.inst.GetCurrentSaveName(resolveAutosaveAncestor: true);
			if (text != null)
			{
				text = ManSaveGame.GetNextAvailableSaveName(currentGameType, text);
			}
			SaveOperationData data = new SaveOperationData
			{
				m_Name = null,
				m_PrevName = text,
				m_GameType = currentGameType
			};
			if (ManSaveGame.UseFixedSlots && m_ActiveSaveItem != null)
			{
				data.m_Name = m_ActiveSaveItem.GetSaveFileName();
			}
			m_CreateOperation.Execute(data);
		}
		else
		{
			SaveOperationData data2 = new SaveOperationData
			{
				m_Name = m_ActiveSaveItem.SaveInfo.m_SaveName,
				m_PrevName = null,
				m_GameType = currentGameType
			};
			m_SaveOperation.Execute(data2);
		}
	}

	public void DeleteGame()
	{
		if (m_ActiveSaveItem != null && m_ActiveSaveItem.SaveInfo != null)
		{
			m_ActiveSaveItem.AskDelete();
		}
	}

	public void RenameGame()
	{
		if (m_ActiveSaveItem != null && m_ActiveSaveItem.SaveInfo != null && !ManSaveGame.UseFixedSlots)
		{
			SaveOperationData data = new SaveOperationData
			{
				m_Name = null,
				m_PrevName = m_ActiveSaveItem.SaveInfo.m_SaveName,
				m_GameType = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType()
			};
			m_RenameOperation.Execute(data);
		}
	}

	private void AddToToggleGroup(UISave save)
	{
		Toggle toggleComponent = save.ToggleComponent;
		d.Assert(toggleComponent != null, "UIScreenSaveGame " + base.name + " has toggle group but UISave elements do not contain a Toggle ccomponent!");
		if (!(toggleComponent != null))
		{
			return;
		}
		toggleComponent.isOn = false;
		toggleComponent.group = m_ToggleGroup;
		toggleComponent.onValueChanged.AddListener(delegate(bool selected)
		{
			if (selected)
			{
				OnClickHandler(save);
			}
		});
	}

	private void RemoveFromToggleGroup(UISave save)
	{
		if (m_ToggleGroup != null)
		{
			Toggle toggleComponent = save.ToggleComponent;
			toggleComponent.group = null;
			toggleComponent.onValueChanged.RemoveAllListeners();
		}
	}

	private void SetSelectedSave(UISave item)
	{
		bool flag = false;
		m_ActiveSaveItem = item;
		m_CurrentSelectedIndex = -1;
		if (m_ActiveSaveItem != null)
		{
			m_ActiveSaveItem.ToggleComponent.isOn = true;
			m_CurrentSelectedIndex = m_SaveSlotUIElements.IndexOf(m_ActiveSaveItem);
			flag = true;
		}
		else if (m_ToggleGroup != null)
		{
			m_ToggleGroup.SetAllTogglesOff();
		}
		bool flag2 = m_ActiveSaveItem != m_AddNewSaveItem && m_ActiveSaveItem != null && m_ActiveSaveItem.SaveInfo != null;
		if (m_SaveButton != null)
		{
			m_SaveButton.interactable = flag;
		}
		if (m_DeleteButton != null)
		{
			m_DeleteButton.interactable = flag && flag2;
		}
		if (m_RenameButton != null)
		{
			m_RenameButton.interactable = flag && flag2 && !ManSaveGame.UseFixedSlots;
		}
		if (m_AutoScrollController != null && item != null)
		{
			m_AutoScrollController.ScrollToItem(item.GetComponent<RectTransform>());
		}
	}

	private void ShowSavesForType(ManGameMode.GameType gameType)
	{
		ClearDisplayedSaves();
		LoadUserSaveGames(gameType);
	}

	private void ClearDisplayedSaves()
	{
		for (int num = m_SaveSlotUIElements.Count - 1; num >= 0; num--)
		{
			if (m_ToggleGroup != null)
			{
				m_ToggleGroup.SetAllTogglesOff();
			}
			RemoveFromToggleGroup(m_SaveSlotUIElements[num]);
			m_SaveSlotUIElements[num].transform.SetParent(null, worldPositionStays: false);
			m_SaveSlotUIElements[num].Recycle();
			m_SaveSlotUIElements.RemoveAt(num);
		}
	}

	private void LoadUserSaveGames(ManGameMode.GameType gameType)
	{
		m_GameTypeOfSaves = gameType;
		m_SavesToLoad = ManSaveGame.GetSavesInFolder(gameType, includeUnusedSlots: true);
		if (m_SavesToLoad != null)
		{
			m_SavesToLoad.Sort(UIScreenLoadSave.SaveSortComparer);
		}
		m_SavesToLoadProcessIndex = 0;
		m_SavesSuccessfullyLoadedCount = 0;
		m_BusyLoadingSaves = m_SavesToLoad != null && m_SavesToLoad.Count > 0;
		ShowLoadingGraphic(m_BusyLoadingSaves);
		SetSelectedSave(null);
	}

	private void ShowLoadingGraphic(bool show)
	{
		if (m_LoadingGraphic != null)
		{
			m_LoadingGraphic.gameObject.SetActive(show);
		}
	}

	private void SetSavesInViewportActive()
	{
		foreach (UISave saveSlotUIElement in m_SaveSlotUIElements)
		{
			saveSlotUIElement.SetActiveIfInViewportRect(m_ScrollRect.viewport.WorldRect());
		}
	}

	private void SetupSaveDataComponent(string saveName, bool isEmptySlot, ManSaveGame.SaveInfo saveInfo)
	{
		if ((saveInfo?.CanLoadSave() ?? false) || isEmptySlot)
		{
			m_SavesSuccessfullyLoadedCount++;
			if (saveInfo == null || !saveInfo.IsAutoSave)
			{
				UISave uISave = m_SavePrefab.Spawn(Vector3.zero);
				uISave.GetComponent<RectTransform>().SetParent(m_ContentHolder, worldPositionStays: false);
				if (m_ToggleGroup != null)
				{
					AddToToggleGroup(uISave);
				}
				uISave.Setup(saveName, saveInfo, isEmptySlot);
				m_SaveSlotUIElements.Add(uISave);
				if (saveName == Singleton.Manager<ManSaveGame>.inst.GetCurrentSaveName(resolveAutosaveAncestor: true))
				{
					LayoutRebuilder.ForceRebuildLayoutImmediate(m_ContentHolder);
					SetSelectedSave(uISave);
				}
			}
		}
		else
		{
			d.LogErrorFormat("Failed to Setup SaveData Component for save: {0}, save data null? {1}, save can load? {2}", saveName, saveInfo == null, (saveInfo != null) ? saveInfo.CanLoadSave().ToString() : "N/A");
		}
	}

	private void StopPreloadingGames()
	{
	}

	private void RefreshSavesOnOperationComplete(SaveOperationData data)
	{
		ShowSavesForType(data.m_GameType);
	}

	public void UI_OnBarScrolled(Vector2 _)
	{
		SetSavesInViewportActive();
	}

	private void OnClickHandler(UISave item)
	{
		bool flag = false;
		if (m_SaveDoubleClickAction != null)
		{
			bool doubleClickConditionPassed = m_ActiveSaveItem != null && m_ActiveSaveItem == item;
			flag = m_DoubleClickListener.WasClickEventDoubleClick(doubleClickConditionPassed);
		}
		if (flag && !SKU.ConsoleUI)
		{
			m_SaveDoubleClickAction.Invoke();
			return;
		}
		SetSelectedSave(item);
		EventSystem.current.SetSelectedGameObject(base.gameObject);
	}

	public void OnMove(AxisEventData eventData)
	{
		if (m_SaveSlotUIElements.Count != 0)
		{
			int num = m_CurrentSelectedIndex;
			if (eventData.moveDir == MoveDirection.Up)
			{
				num--;
			}
			else if (eventData.moveDir == MoveDirection.Down)
			{
				num++;
			}
			num = ((!(m_AddNewSaveItem != null)) ? Mathf.Clamp(num, 0, m_SaveSlotUIElements.Count - 1) : Mathf.Clamp(num, -1, m_SaveSlotUIElements.Count - 1));
			if (num >= 0 && num != m_CurrentSelectedIndex)
			{
				m_SaveSlotUIElements[num].SetSelected();
			}
			else if (num == -1)
			{
				m_AddNewSaveItem.SetSelected();
			}
			eventData.Use();
		}
	}

	public void OnSubmit(BaseEventData eventData)
	{
		SaveGame();
	}

	public void OnCancel(BaseEventData eventData)
	{
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	public void OnUIExtraButton1(BaseEventData eventData)
	{
		RenameGame();
	}

	public void OnUIExtraButton2(BaseEventData eventData)
	{
		DeleteGame();
	}

	private void Awake()
	{
		m_ToggleGroup = m_ContentHolder.GetComponent<ToggleGroup>();
		m_DoubleClickListener = new DoubleClickListener();
		m_AutoScrollController = m_ContentHolder.GetComponentInParents<UIAutoScrollItemController>();
		if (!ManSaveGame.UseFixedSlots)
		{
			m_AddNewSaveItem = m_AddSavePrefab.Spawn(Vector3.zero);
			m_AddNewSaveItem.GetComponent<RectTransform>().SetParent(m_ContentHolder, worldPositionStays: false);
			m_AddNewSaveItem.Setup(null, null, useAsSlot: false);
			if (m_ToggleGroup != null)
			{
				AddToToggleGroup(m_AddNewSaveItem);
			}
		}
		m_CreateOperation = new SaveOperation();
		ShowNameInputCommand command = new ShowNameInputCommand();
		ShowErrorCommand showErrorCommand = new ShowErrorCommand();
		DoSaveCommand command2 = new DoSaveCommand();
		ShowConfirmationCommand showConfirmationCommand = new ShowConfirmationCommand();
		ShowAlertCommand showAlertCommand = new ShowAlertCommand();
		ResumeGameCommand command3 = new ResumeGameCommand();
		showErrorCommand.Setup(m_SaveNameErrorPrompt.Value, m_ContinueLabel.Value);
		showConfirmationCommand.Setup(m_OverwritePrompt.Value, m_OverwriteConfirmLabel.Value, m_CancelLabel.Value, usePrevName: false);
		showAlertCommand.Setup(m_SaveSuccessPrompt.Value, m_ContinueLabel.Value);
		if (!ManSaveGame.UseFixedSlots)
		{
			m_CreateOperation.Add(command);
			m_CreateOperation.AddConditional(Conditions.CheckReservedWord, showErrorCommand);
		}
		m_CreateOperation.AddConditional(Conditions.CheckFileExists, showConfirmationCommand);
		m_CreateOperation.Add(command2);
		m_CreateOperation.Add(showAlertCommand);
		m_CreateOperation.Add(command3);
		m_SaveOperation = new SaveOperation();
		ShowConfirmationCommand showConfirmationCommand2 = new ShowConfirmationCommand();
		DoSaveCommand command4 = new DoSaveCommand();
		ShowAlertCommand showAlertCommand2 = new ShowAlertCommand();
		ResumeGameCommand command5 = new ResumeGameCommand();
		showConfirmationCommand2.Setup(m_OverwritePrompt.Value, m_OverwriteConfirmLabel.Value, m_CancelLabel.Value, usePrevName: false);
		showAlertCommand2.Setup(m_SaveSuccessPrompt.Value, m_ContinueLabel.Value);
		m_SaveOperation.Add(showConfirmationCommand2);
		m_SaveOperation.Add(command4);
		m_SaveOperation.Add(showAlertCommand2);
		m_SaveOperation.Add(command5);
		m_RenameOperation = new SaveOperation();
		ShowNameInputCommand command6 = new ShowNameInputCommand();
		ShowErrorCommand showErrorCommand2 = new ShowErrorCommand();
		ShowConfirmationCommand showConfirmationCommand3 = new ShowConfirmationCommand();
		DoCopyCommaned command7 = new DoCopyCommaned();
		DoDeleteCommand command8 = new DoDeleteCommand();
		showErrorCommand2.Setup(m_SaveNameErrorPrompt.Value, m_ContinueLabel.Value);
		showConfirmationCommand3.Setup(m_OverwritePrompt.Value, m_OverwriteConfirmLabel.Value, m_CancelLabel.Value, usePrevName: false);
		m_RenameOperation.Add(command6);
		m_RenameOperation.AddConditional(Conditions.CheckReservedWord, showErrorCommand2);
		m_RenameOperation.AddConditional(Conditions.CheckFileExists, showConfirmationCommand3);
		m_RenameOperation.Add(command7);
		m_RenameOperation.Add(command8);
		SaveOperation renameOperation = m_RenameOperation;
		renameOperation.Completed = (Action<SaveOperationData>)Delegate.Combine(renameOperation.Completed, new Action<SaveOperationData>(RefreshSavesOnOperationComplete));
		m_DeleteOperation = new SaveOperation();
		ShowConfirmationCommand showConfirmationCommand4 = new ShowConfirmationCommand();
		DoDeleteCommand command9 = new DoDeleteCommand();
		showConfirmationCommand4.Setup(m_DeletePrompt.Value, m_DeleteConfirmLabel.Value, m_CancelLabel.Value, usePrevName: true);
		m_DeleteOperation.Add(showConfirmationCommand4);
		m_DeleteOperation.Add(command9);
		SaveOperation deleteOperation = m_DeleteOperation;
		deleteOperation.Completed = (Action<SaveOperationData>)Delegate.Combine(deleteOperation.Completed, new Action<SaveOperationData>(RefreshSavesOnOperationComplete));
	}

	private void Update()
	{
		if (!m_BusyLoadingSaves)
		{
			return;
		}
		if (m_SavesToLoad != null && m_SavesToLoad.Count > 0 && m_SavesToLoadProcessIndex < m_SavesToLoad.Count)
		{
			ManSaveGame.SaveFileSlot save = m_SavesToLoad[m_SavesToLoadProcessIndex];
			if (save.isEmptySlot)
			{
				SetupSaveDataComponent(save.name, save.isEmptySlot, null);
			}
			else
			{
				ManSaveGame.LoadSaveDataInfoAsync(m_GameTypeOfSaves, save.name, delegate(ManSaveGame.SaveInfo saveInfo)
				{
					SetupSaveDataComponent(save.name, save.isEmptySlot, saveInfo);
				});
			}
			m_SavesToLoadProcessIndex++;
			return;
		}
		if (m_SavesSuccessfullyLoadedCount != m_SavesToLoadProcessIndex)
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 22);
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 17);
			string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 18);
			localisedString2 = UIHyperlink.ConvertLinkToTMProLinkCode(localisedString2, localisedString3);
			localisedString = string.Format(localisedString, localisedString2);
			d.LogError("Failed to load one or more save infos in UIScreenSaveGame");
			Singleton.Manager<ManUI>.inst.ShowErrorPopup(localisedString);
		}
		m_BusyLoadingSaves = false;
		m_SavesToLoad = null;
		ShowLoadingGraphic(show: false);
		SetSavesInViewportActive();
		if (m_ActiveSaveItem == null)
		{
			if (m_AddNewSaveItem != null)
			{
				SetSelectedSave(m_AddNewSaveItem);
				m_AddNewSaveItem.SetSelected();
			}
			else if (m_SaveSlotUIElements.Count > 0)
			{
				SetSelectedSave(m_SaveSlotUIElements[0]);
				m_SaveSlotUIElements[0].SetSelected();
			}
		}
	}
}
