#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.Networking;

public class ModeCoOpCampaign : ModeCoOp<ModeCoOpCampaign>
{
	[SerializeField]
	private TankPreset[] m_RespawnPlayerTankPresets;

	private NetInventory m_SharedInventory;

	private BlockCountList m_LoadedInventoryBlocks;

	public IInventory<BlockTypes> GetSharedInventory()
	{
		return m_SharedInventory;
	}

	public override string GetGameMode()
	{
		return "ModeCoOpCampaign";
	}

	public override ManGameMode.GameType GetGameType()
	{
		return ManGameMode.GameType.CoOpCampaign;
	}

	protected override bool IsAutoSaveEnabled()
	{
		if (m_SupportsAutoSave)
		{
			return Singleton.Manager<ManNetwork>.inst.IsServer;
		}
		return false;
	}

	protected override void EnterPreModeImpl(InitSettings initSettings)
	{
		base.EnterPreModeImpl(initSettings);
		Singleton.Manager<ManProgression>.inst.SetEncounterList(ManProgression.EncounterListType.CoOp);
		Singleton.Manager<ManPlayer>.inst.EnablePalette(enable: true);
		Singleton.Manager<ManTimeOfDay>.inst.EnableSkyDome(enable: true);
		if (!IsLoadedFromSaveGame())
		{
			Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay(11, 0, 0);
			Singleton.Manager<ManTimeOfDay>.inst.SetDate(2700, 2, 22);
			Singleton.Manager<ManTimeOfDay>.inst.EnableTimeProgression(enable: false);
		}
	}

	private void MoveLicenceHUD(bool forCoop)
	{
		UIHUDElement hudElement;
		if ((object)(hudElement = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.FactionLicences)) != null)
		{
			RectTransform component = hudElement.GetComponent<RectTransform>();
			component.anchoredPosition = new Vector2(component.anchoredPosition.x, forCoop ? (-100) : 0);
		}
	}

	protected override void ExitModeImpl()
	{
		base.ExitModeImpl();
		MoveLicenceHUD(forCoop: false);
		m_LoadedInventoryBlocks = null;
	}

	protected override void SetupModeLoadSaveListeners()
	{
		base.SetupModeLoadSaveListeners();
		SubscribeToEvents(Singleton.Manager<ManLicenses>.inst);
		SubscribeToEvents(Singleton.Manager<ManQuestLog>.inst);
		SubscribeToEvents(Singleton.Manager<ManFTUE>.inst);
		SubscribeToEvents(Singleton.Manager<ManNewFTUE>.inst);
		SubscribeToEvents(Singleton.Manager<ManEncounter>.inst);
	}

	protected override void CleanupModeLoadSaveListeners()
	{
		base.CleanupModeLoadSaveListeners();
		UnsubscribeFromEvents(Singleton.Manager<ManLicenses>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManQuestLog>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManFTUE>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManNewFTUE>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManEncounter>.inst);
	}

	protected override void OnServerHostStarted()
	{
		base.OnServerHostStarted();
		Singleton.Manager<ManPlayer>.inst.MoneyAmountChanged.Subscribe(OnHostMoneyChanged);
		Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Subscribe(OnPlayerAdded);
		Singleton.Manager<ManNetwork>.inst.OnPlayerRemoved.Subscribe(OnPlayerRemoved);
		Transform transform = Singleton.Manager<ManNetwork>.inst.NetInventoryPrefab.transform.Spawn(Vector3.zero, Quaternion.identity);
		m_SharedInventory = transform.GetComponent<NetInventory>();
		m_SharedInventory.ServerSetIsSharedInventory(set: true);
		m_SharedInventory.name = "Shared Campaign Inventory";
		if (m_LoadedInventoryBlocks != null)
		{
			m_SharedInventory.LoadFrom(m_LoadedInventoryBlocks);
			m_LoadedInventoryBlocks = null;
		}
		ReturnClientTechBlocks();
		NetworkServer.Spawn(m_SharedInventory.gameObject);
	}

	protected override void OnServerHostStopped()
	{
		Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Unsubscribe(OnPlayerAdded);
		Singleton.Manager<ManNetwork>.inst.OnPlayerRemoved.Unsubscribe(OnPlayerRemoved);
		Singleton.Manager<ManPlayer>.inst.MoneyAmountChanged.Unsubscribe(OnHostMoneyChanged);
		base.OnServerHostStopped();
	}

	public void SetSharedInventory(NetInventory inventory)
	{
		d.Assert(!Singleton.Manager<ManNetwork>.inst.IsServer, "This should be client only");
		m_SharedInventory = inventory;
		if (Singleton.Manager<ManNetwork>.inst.MyPlayer.IsNotNull())
		{
			d.Log("Fixing up player inventory.");
			Singleton.Manager<ManNetwork>.inst.MyPlayer.SetInventory(inventory);
		}
	}

	protected override void SetupPlayerHUD()
	{
		base.SetupPlayerHUD();
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.MoneyCounter);
		Singleton.Manager<ManLicenses>.inst.SetHUDVisible(visible: true);
		MoveLicenceHUD(forCoop: true);
	}

	public override InventoryMetaData GetReferenceInventory()
	{
		return new InventoryMetaData(Singleton.Manager<ManPurchases>.inst.GetInventory(), locked: false, m_AllowedBlocks);
	}

	protected override void Save(ManSaveGame.State saveState)
	{
		base.Save(saveState);
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.NetworkInventory, m_SharedInventory.GetBlockCountsForSaving());
	}

	protected override void Load(ManSaveGame.State saveState)
	{
		base.Load(saveState);
		saveState.GetSaveData<BlockCountList>(ManSaveGame.SaveDataJSONType.NetworkInventory, out m_LoadedInventoryBlocks);
	}

	protected override void EnterGenerateTerrain(InitSettings initSettings)
	{
		base.EnterGenerateTerrain(initSettings);
		Singleton.Manager<ManWorld>.inst.VendorSpawner.Enabled = true;
		Singleton.Manager<ManWorld>.inst.Vendors.SetAllActive(active: true);
		Singleton.Manager<ManWorld>.inst.Vendors.SetVisibleOnRadar(visible: true);
	}

	protected override TechData GetTechDataForPlayerSpawn(NetPlayer player)
	{
		int currentLevel = Singleton.Manager<ManLicenses>.inst.GetCurrentLevel(FactionSubTypes.GSO);
		int num = currentLevel;
		int num2 = Mathf.Min(num, m_RespawnPlayerTankPresets.Length - 1);
		TechData techData = null;
		while (num2 >= 0)
		{
			TankPreset tankPreset = m_RespawnPlayerTankPresets[num2];
			if (tankPreset != null)
			{
				techData = tankPreset.GetTechDataFormatted();
				break;
			}
			num2--;
		}
		if (num2 != num)
		{
			d.LogWarning($"GetRespawnTechForPlayer - Failed to find Starter Tech for techLevel {num + 1} - had to fall back to level {num2 + 1} before finding a valid tech!");
		}
		d.Assert(techData != null, $"GetRespawnTechForPlayer - Failed to find starter tech data for GSO grade {currentLevel}. Falling back to default mode tech data!");
		return techData ?? Singleton.Manager<ManNetwork>.inst.DefaultTechData;
	}

	private void OnPlayerAdded(NetPlayer player)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			player.SetInventory(m_SharedInventory);
			m_SharedInventory.OnServerRegisterUser(player);
		}
		else if (m_SharedInventory != null)
		{
			player.SetInventory(m_SharedInventory);
		}
	}

	private void OnPlayerRemoved(NetPlayer player)
	{
		player.SetInventory(null);
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			m_SharedInventory.OnServerUnregisterUser(player);
		}
	}

	private void OnHostMoneyChanged(int qty)
	{
		d.Log($"OnHostMoneyChanged: {qty}");
		Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.MoneyChanged, new MoneyChangedMessage
		{
			m_Money = qty
		});
	}
}
