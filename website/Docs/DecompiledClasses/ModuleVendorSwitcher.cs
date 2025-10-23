using UnityEngine;

public class ModuleVendorSwitcher : Module
{
	[SerializeField]
	private LocalisedString m_DisabledInteractNotificationMessage;

	private bool m_ShopActive;

	private bool m_MissionBoardActive;

	private bool m_SellingActive;

	private bool m_ScuActive;

	private bool m_RemoteChargingActive;

	private ModuleShop m_Shop;

	private ModuleQuestGiver m_QuestGiver;

	private void StateSwitched(bool enableShop, bool enableMissionBoard, bool enableSelling, bool enableScu)
	{
		if ((bool)m_Shop)
		{
			m_Shop.CanLaunch = enableShop;
		}
		if ((bool)m_QuestGiver)
		{
			m_QuestGiver.CanLaunch = enableMissionBoard;
		}
		m_ShopActive = enableShop;
		m_MissionBoardActive = enableMissionBoard;
		m_SellingActive = enableSelling;
		m_ScuActive = enableScu;
		if (m_RemoteChargingActive != Singleton.Manager<ManWorld>.inst.Vendors.RemoteChargingActive)
		{
			UpdateRemoteChargingStatus();
		}
	}

	private void UpdateRemoteChargingStatus()
	{
		m_RemoteChargingActive = Singleton.Manager<ManWorld>.inst.Vendors.RemoteChargingActive;
		if (!(base.block.tank != null))
		{
			return;
		}
		Debug.Log("Iterating over blocks for Vendor status update");
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = base.block.tank.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			ModuleRemoteCharger component = enumerator.Current.GetComponent<ModuleRemoteCharger>();
			if ((bool)component)
			{
				component.SetChargerStatus(m_RemoteChargingActive);
			}
		}
	}

	private void ShowVendorNotEnabledWarning()
	{
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
		uIScreenNotifications.Set(m_DisabledInteractNotificationMessage.Value, delegate
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
		}, localisedString);
		Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenNotifications);
	}

	private bool CanAcceptItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack, ModuleItemHolder.PassType passType)
	{
		bool num = passType == (ModuleItemHolder.PassType.Drop | ModuleItemHolder.PassType.Test);
		if (!m_SellingActive && passType == ModuleItemHolder.PassType.Drop)
		{
			ShowVendorNotEnabledWarning();
		}
		if (!num)
		{
			return m_SellingActive;
		}
		return true;
	}

	private void OnMouseInteract(Module interactedModule)
	{
		if (!m_ShopActive && !m_MissionBoardActive && !m_SellingActive && !m_ScuActive)
		{
			ShowVendorNotEnabledWarning();
		}
	}

	private void OnPool()
	{
		ModuleItemHolder component = GetComponent<ModuleItemHolder>();
		if ((bool)component)
		{
			component.SetAcceptFilterCallback(CanAcceptItem);
		}
		m_Shop = GetComponent<ModuleShop>();
		if (m_Shop != null)
		{
			m_Shop.MouseInteractEvent.Subscribe(OnMouseInteract);
		}
		m_QuestGiver = GetComponent<ModuleQuestGiver>();
		if (m_QuestGiver != null)
		{
			m_QuestGiver.MouseInteractEvent.Subscribe(OnMouseInteract);
		}
	}

	private void OnSpawn()
	{
		UpdateRemoteChargingStatus();
		StateSwitched(Singleton.Manager<ManWorld>.inst.Vendors.ShopActive, Singleton.Manager<ManWorld>.inst.Vendors.MissionBoardActive, Singleton.Manager<ManWorld>.inst.Vendors.SellingActive, Singleton.Manager<ManWorld>.inst.Vendors.ScuActive);
		Singleton.Manager<ManWorld>.inst.Vendors.OnActiveChanged.Subscribe(StateSwitched);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManWorld>.inst.Vendors.OnActiveChanged.Unsubscribe(StateSwitched);
	}
}
