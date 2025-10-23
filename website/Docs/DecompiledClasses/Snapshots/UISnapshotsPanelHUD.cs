#define UNITY_EDITOR
using System;
using UnityEngine;

namespace Snapshots;

public class UISnapshotsPanelHUD : UIHUDElement, ITechLoader
{
	private VMSnapshotPanel m_SnapshotViewModel;

	private TechPlacementHelper m_TechPlacementHelper = new TechPlacementHelper();

	private UITechSelector.PlaceTechHandler m_PlaceTechHandler;

	public override void Show(object context)
	{
		base.Show(context);
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.Open);
		GetTechSelector().Show();
	}

	public override void Hide(object context)
	{
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.Close);
		GetTechSelector().Hide();
		base.Hide(context);
	}

	public override bool HandleCustomEscapeKeyAction()
	{
		bool result = false;
		if (base.IsVisible)
		{
			if (m_SnapshotViewModel.HandleCustomEscapeKey())
			{
				result = true;
			}
			else
			{
				HideSelf();
				result = true;
			}
		}
		return result;
	}

	public void CloseTechLoader()
	{
		HideSelf();
	}

	public void SetupPlaceTechScreenHandler(UITechSelector.PlaceTechHandler placeTechHandler)
	{
		m_PlaceTechHandler = (UITechSelector.PlaceTechHandler)Delegate.Combine(m_PlaceTechHandler, placeTechHandler);
	}

	public void RemovePlaceTechScreenHandler(UITechSelector.PlaceTechHandler placeTechHandler)
	{
		m_PlaceTechHandler = (UITechSelector.PlaceTechHandler)Delegate.Remove(m_PlaceTechHandler, placeTechHandler);
	}

	public void SetupScreenHandlers(Action<Snapshot> selectionAcceptedEvent, UITechSelector.CanAcceptTechCallback selectButtonEnabledCallback = null)
	{
		GetTechSelector().OnSwapTech.Subscribe(selectionAcceptedEvent);
	}

	public void RemoveScreenHandlers(Action<Snapshot> selectionAcceptedEvent, UITechSelector.CanAcceptTechCallback selectButtonEnabledCallback = null)
	{
		GetTechSelector().OnSwapTech.Unsubscribe(selectionAcceptedEvent);
	}

	public void SetInventory(IInventory<BlockTypes> inventory)
	{
		throw new NotImplementedException();
	}

	public VMSnapshotPanel.FocusTarget GetFocusTarget()
	{
		return GetTechSelector().GetFocusTarget();
	}

	public bool GetIsASnapshotCurrentlySelected()
	{
		return m_SnapshotViewModel.m_Selected.Value.m_Snapshot != null;
	}

	private VMSnapshotPanel GetTechSelector()
	{
		if (m_SnapshotViewModel == null)
		{
			bool includeInactive = true;
			m_SnapshotViewModel = GetComponentInChildren<VMSnapshotPanel>(includeInactive);
			d.Assert(m_SnapshotViewModel != null, "UISnapshotPanelHUD - cannot find child VMSnapshotModel component");
			m_SnapshotViewModel.InjectInteractionModel(m_TechPlacementHelper);
		}
		return m_SnapshotViewModel;
	}

	private void ShowTechPlaceConfirmationScreen(Action acceptAction, Action declineAction)
	{
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		string notification = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.TechLoader.DeployWithMissingBlocks), m_SnapshotViewModel.m_SelectedTechName.Value);
		UIButtonData accept = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.TechLoader.DeployWMissingAccept),
			m_Callback = delegate
			{
				acceptAction?.Invoke();
				Singleton.Manager<ManUI>.inst.PopScreen();
			},
			m_RewiredAction = 21
		};
		UIButtonData decline = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.TechLoader.DeployWMissingCancel),
			m_Callback = delegate
			{
				declineAction?.Invoke();
				Singleton.Manager<ManUI>.inst.PopScreen();
			},
			m_RewiredAction = 22
		};
		uIScreenNotifications.Set(notification, accept, decline, m_SnapshotViewModel.m_Selected.Value.m_Snapshot.image);
		Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenNotifications.Type);
	}

	private void OnShowTwitterAuthPopup()
	{
		Singleton.Manager<ManPauseGame>.inst.Pause();
	}

	private void OnHideTwitterAuthPopup()
	{
		Singleton.Manager<ManPauseGame>.inst.Resume();
	}

	private void OnPlaceTechConfirm(TechData techData, Vector3 chosenPosition, Quaternion techOrientation)
	{
		m_SnapshotViewModel.ConfirmPlace();
		if (m_PlaceTechHandler == null)
		{
			return;
		}
		bool hasMissingBlocksPlace = m_SnapshotViewModel.m_Selected.Value.m_ValidData.HasMissingBlocksPlace;
		bool flag = true;
		if (Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) && (Singleton.Manager<DebugUtil>.inst.RemoveTechLoaderRestrictions || Singleton.Manager<DebugUtil>.inst.AllBlocksInInventory))
		{
			flag = false;
		}
		if (hasMissingBlocksPlace && flag)
		{
			ShowTechPlaceConfirmationScreen(delegate
			{
				m_PlaceTechHandler(techData, chosenPosition, techOrientation);
			}, null);
		}
		else
		{
			m_PlaceTechHandler(techData, chosenPosition, techOrientation);
		}
	}

	private void OnPlaceTechCancelled()
	{
		m_SnapshotViewModel.CancelPlace();
	}

	private void OnPool()
	{
		m_TechPlacementHelper.PlaceTechEvent.Subscribe(OnPlaceTechConfirm);
		m_TechPlacementHelper.PlaceTechCancelledEvent.Subscribe(OnPlaceTechCancelled);
	}

	private void OnSpawn()
	{
		AddElementToGroup(ManHUD.HUDGroup.Main);
		AddElementToGroup(ManHUD.HUDGroup.GamepadCursorHidingElements);
		AddElementToGroup(ManHUD.HUDGroup.GamepadQuickMenuHUDElements);
	}

	private void Update()
	{
		m_TechPlacementHelper.UpdateValidation();
	}
}
