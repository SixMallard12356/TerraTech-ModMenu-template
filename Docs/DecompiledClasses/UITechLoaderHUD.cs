#define UNITY_EDITOR
using System;
using Snapshots;
using UnityEngine;

public class UITechLoaderHUD : UIHUDElement, ITechLoader
{
	[SerializeField]
	private bool m_HideOnConfirmTech = true;

	private UITechSelector m_TechSelectorComponent;

	public override void Show(object context)
	{
		base.Show(context);
		Singleton.Manager<TwitterAuthenticationUIHandler>.inst.OnShowAuthenticatePopup.Subscribe(OnShowTwitterAuthPopup);
		Singleton.Manager<TwitterAuthenticationUIHandler>.inst.OnHideAuthenticatePopup.Subscribe(OnHideTwitterAuthPopup);
		if (m_HideOnConfirmTech)
		{
			GetTechSelector().OnSelectionAcceptedEvent.Subscribe(OnTechSelected);
		}
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.SumoShowdown)
		{
			GetTechSelector().SetCommunitySource(UITechSelector.CommunityTagGroup.Sumo);
		}
		GetTechSelector().Show();
	}

	public override void Hide(object context)
	{
		GetTechSelector().Hide();
		if (m_HideOnConfirmTech)
		{
			GetTechSelector().OnSelectionAcceptedEvent.Unsubscribe(OnTechSelected);
		}
		Singleton.Manager<TwitterAuthenticationUIHandler>.inst.OnShowAuthenticatePopup.Unsubscribe(OnShowTwitterAuthPopup);
		Singleton.Manager<TwitterAuthenticationUIHandler>.inst.OnHideAuthenticatePopup.Unsubscribe(OnHideTwitterAuthPopup);
		base.Hide(context);
	}

	public void SetupPlaceTechScreenHandler(UITechSelector.PlaceTechHandler placeTechHandler)
	{
		d.LogError("UITechLoaderHUD.SetupPlaceTechScreenHandler - Placing tech is not supported from this obsolete class! Please use UISnapshotsPanelHUD instead");
	}

	public void RemovePlaceTechScreenHandler(UITechSelector.PlaceTechHandler placeTechHandler)
	{
		d.LogError("UITechLoaderHUD.SetupPlaceTechScreenHandler - Placing tech is not supported from this obsolete class! Please use UISnapshotsPanelHUD instead");
	}

	public void SetupScreenHandlers(Action<Snapshot> selectionAcceptedEvent, UITechSelector.CanAcceptTechCallback selectButtonEnabledCallback = null)
	{
		GetTechSelector().OnSelectionAcceptedEvent.Subscribe(selectionAcceptedEvent);
		GetTechSelector().CanSelectTechCallback = selectButtonEnabledCallback;
	}

	public void RemoveScreenHandlers(Action<Snapshot> selectionAcceptedEvent, UITechSelector.CanAcceptTechCallback selectButtonEnabledCallback = null)
	{
		GetTechSelector().OnSelectionAcceptedEvent.Unsubscribe(selectionAcceptedEvent);
		GetTechSelector().CanSelectTechCallback = null;
	}

	public void SetInventory(IInventory<BlockTypes> inventory)
	{
		GetTechSelector().SetInventory(inventory);
	}

	public void CloseTechLoader()
	{
		HideSelf();
	}

	private UITechSelector GetTechSelector()
	{
		if (m_TechSelectorComponent == null)
		{
			bool includeInactive = true;
			m_TechSelectorComponent = GetComponentInChildren<UITechSelector>(includeInactive);
			d.Assert(m_TechSelectorComponent != null, "UITechLoaderHUD - cannot find child tech selector component");
		}
		return m_TechSelectorComponent;
	}

	private void OnShowTwitterAuthPopup()
	{
		Singleton.Manager<ManPauseGame>.inst.Pause();
	}

	private void OnHideTwitterAuthPopup()
	{
		Singleton.Manager<ManPauseGame>.inst.Resume();
	}

	private void OnTechSelected(Snapshot capture)
	{
		HideSelf();
	}

	private void OnSpawn()
	{
		AddElementToGroup(ManHUD.HUDGroup.Main);
		AddElementToGroup(ManHUD.HUDGroup.GamepadCursorHidingElements);
		AddElementToGroup(ManHUD.HUDGroup.GamepadQuickMenuHUDElements);
	}

	private void OnRecycle()
	{
		m_TechSelectorComponent = null;
	}
}
