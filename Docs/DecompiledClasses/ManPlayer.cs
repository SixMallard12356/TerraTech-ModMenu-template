#define UNITY_EDITOR
using System.Collections.Generic;
using Newtonsoft.Json;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.Networking;

public class ManPlayer : Singleton.Manager<ManPlayer>, Mode.IManagerModeEvents
{
	private struct TrackedHeartTech
	{
		public TrackedVisible m_TrackedVisible;

		public int m_HeartCount;
	}

	private class SaveData
	{
		public bool m_PaletteUnlocked;

		public int m_Money;

		[JsonConverter(typeof(InventoryJsonConverter))]
		public IInventory<BlockTypes> m_Inventory = new SingleplayerInventory();

		public List<int> m_TrackedIDs = new List<int>();

		public HotswapMap m_HotswapMap;

		public List<WorldPosition> m_PlayerDeathLocations;

		public bool m_PlayerIndestructible;

		public bool m_SkipPowerupSequencing;

		public bool m_HasEnabledCheatCommands;

		public List<PlayerSaveData> m_Players = new List<PlayerSaveData>(4);

		public HashSet<int> m_MPClientTechData = new HashSet<int>();
	}

	private class PlayerSaveData
	{
		[JsonConverter(typeof(TTNetworkIDConverter))]
		public TTNetworkID m_NetID;

		public int m_TrackedVisibleIDOfLastTech;
	}

	public struct HotswapBinding
	{
		public TechData m_TechData;

		public string m_SnapshotFilePath;
	}

	public class HotswapMap
	{
		[JsonProperty]
		private Dictionary<int, HotswapBinding> m_LookupTech = new Dictionary<int, HotswapBinding>();

		[JsonProperty]
		private Dictionary<string, int> m_LookupSlotByFilepath = new Dictionary<string, int>();

		public void AddOrReplace(SnapshotDisk snapshotDisk, int slot)
		{
			TechData techData = snapshotDisk.techData;
			string snapName = snapshotDisk.snapName;
			AddOrReplace(techData, snapName, slot);
		}

		public void AddOrReplace(TechData techData, string filePath, int slot)
		{
			Remove(slot);
			m_LookupTech[slot] = new HotswapBinding
			{
				m_TechData = techData,
				m_SnapshotFilePath = filePath
			};
			if (filePath != null)
			{
				m_LookupSlotByFilepath[filePath] = slot;
			}
			else
			{
				d.LogError("ManPlayer.HotswapMap.AddOrReplace: null filename");
			}
		}

		public void Remove(SnapshotDisk snapshotDisk)
		{
			if (snapshotDisk != null)
			{
				string snapName = snapshotDisk.snapName;
				Remove(snapName);
			}
		}

		private void Remove(string filePath)
		{
			if (filePath != null && m_LookupSlotByFilepath.ContainsKey(filePath))
			{
				int key = m_LookupSlotByFilepath[filePath];
				m_LookupTech.Remove(key);
				m_LookupSlotByFilepath.Remove(filePath);
			}
		}

		public void Remove(int slot)
		{
			if (m_LookupTech.ContainsKey(slot))
			{
				HotswapBinding hotswapBinding = m_LookupTech[slot];
				m_LookupTech.Remove(slot);
				string snapshotFilePath = hotswapBinding.m_SnapshotFilePath;
				if (snapshotFilePath != null)
				{
					m_LookupSlotByFilepath.Remove(snapshotFilePath);
				}
			}
		}

		public void Clear()
		{
			m_LookupSlotByFilepath.Clear();
			m_LookupTech.Clear();
		}

		public bool TryGetBinding(int slot, out HotswapBinding binding)
		{
			if (m_LookupTech.TryGetValue(slot, out binding))
			{
				return true;
			}
			return false;
		}

		public bool TryGetSlot(SnapshotDisk snapshotDisk, out int slot)
		{
			string snapName = snapshotDisk.snapName;
			if (m_LookupSlotByFilepath.TryGetValue(snapName, out slot))
			{
				return true;
			}
			slot = 0;
			return false;
		}
	}

	public const int InvalidNetPlayerID = -1;

	public Event<int> MoneyAmountChanged;

	public EventNoParams OnPaletteUnlocked;

	public Event<HotswapMap> HotswapBindingChanged;

	public Event<int> HotswapMaxSlotsChanged;

	public EventNoParams OnPlayerFailedToAnchorWithEnemiesNearby;

	private SaveData m_SaveData = new SaveData();

	private bool m_ManagerActive;

	private Dictionary<int, TrackedHeartTech> m_TechsWithHeartBlocks = new Dictionary<int, TrackedHeartTech>();

	private const int kHotswapEmptySlot = 0;

	private int m_PrevMaxHotswapSlots;

	private HotswapMap m_HotswapMap = new HotswapMap();

	private const float kMinDistToClearGrave = 90f;

	private const float kMinDistToClearGraveSqr = 8100f;

	private Tank m_LastPlayerTank;

	private float m_TimeUntilNextDeathMarkerClear;

	private List<WorldPosition> m_PlayerDeathLocations = new List<WorldPosition>();

	private const float kDeathMarkerClearInterval = 1f;

	public IInventory<BlockTypes> PlayerInventory => m_SaveData.m_Inventory;

	public bool InventoryIsUnrestricted => m_SaveData.m_Inventory == null;

	public bool HasHeartBlock => m_TechsWithHeartBlocks.Count > 0;

	public IEnumerable<WorldPosition> PlayerDeathLocations => m_PlayerDeathLocations;

	public bool PaletteUnlocked => m_SaveData.m_PaletteUnlocked;

	public bool PlayerIndestructible
	{
		get
		{
			return m_SaveData.m_PlayerIndestructible;
		}
		set
		{
			m_SaveData.m_PlayerIndestructible = value;
		}
	}

	public bool SkipPowerupSequencing
	{
		get
		{
			return m_SaveData.m_SkipPowerupSequencing;
		}
		set
		{
			m_SaveData.m_SkipPowerupSequencing = value;
		}
	}

	public int PlayerTeam
	{
		get
		{
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				if (!Singleton.Manager<ManNetwork>.inst.MyPlayer)
				{
					return 0;
				}
				return Singleton.Manager<ManNetwork>.inst.MyPlayer.TechTeamID;
			}
			return 0;
		}
	}

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		m_ManagerActive = true;
		m_TechsWithHeartBlocks.Clear();
		m_PlayerDeathLocations.Clear();
		if (optionalLoadState != null && optionalLoadState.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManPlayer, out var saveData) && saveData != null)
		{
			m_SaveData = saveData;
			for (int i = 0; i < m_SaveData.m_TrackedIDs.Count; i++)
			{
				TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(m_SaveData.m_TrackedIDs[i]);
				if (trackedVisible != null)
				{
					m_TechsWithHeartBlocks.Add(trackedVisible.ID, new TrackedHeartTech
					{
						m_TrackedVisible = trackedVisible
					});
					_ = m_SaveData.m_TrackedIDs[i];
				}
			}
			m_SaveData.m_TrackedIDs.Clear();
			Singleton.Manager<ManPurchases>.inst.SetInventory(PlayerInventory);
			if (m_SaveData.m_PaletteUnlocked)
			{
				EnablePalette(enable: true);
			}
			if (m_SaveData.m_HotswapMap != null)
			{
				m_HotswapMap = m_SaveData.m_HotswapMap;
				m_SaveData.m_HotswapMap = null;
			}
			if (m_SaveData.m_PlayerDeathLocations != null)
			{
				m_PlayerDeathLocations.AddRange(m_SaveData.m_PlayerDeathLocations);
			}
		}
		else
		{
			m_SaveData = new SaveData();
		}
	}

	public void Save(ManSaveGame.State saveState)
	{
		m_SaveData.m_TrackedIDs.Clear();
		foreach (int key in m_TechsWithHeartBlocks.Keys)
		{
			m_SaveData.m_TrackedIDs.Add(key);
		}
		m_SaveData.m_HotswapMap = m_HotswapMap;
		m_SaveData.m_PlayerDeathLocations?.Clear();
		if (m_PlayerDeathLocations.Count > 0)
		{
			if (m_SaveData.m_PlayerDeathLocations == null)
			{
				m_SaveData.m_PlayerDeathLocations = new List<WorldPosition>(m_PlayerDeathLocations);
			}
			else
			{
				m_SaveData.m_PlayerDeathLocations.AddRange(m_PlayerDeathLocations);
			}
		}
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManPlayer, m_SaveData);
	}

	public void ModeExit()
	{
		Reset();
	}

	public void Reset()
	{
		m_SaveData = new SaveData();
		m_ManagerActive = false;
		m_TechsWithHeartBlocks.Clear();
		m_LastPlayerTank = null;
		m_PlayerDeathLocations.Clear();
		m_HotswapMap.Clear();
		EnablePalette(enable: false);
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TechLoader);
	}

	public void TrackTechWithHeart(Tank tech)
	{
		if (!m_ManagerActive || !tech)
		{
			return;
		}
		TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(tech.visible.ID);
		d.Assert(trackedVisible != null, "ManPlayer.SetTrackedTech can't get a tracked visible for ID " + tech.visible.ID);
		if (trackedVisible != null)
		{
			if (!m_TechsWithHeartBlocks.TryGetValue(trackedVisible.ID, out var value))
			{
				value = new TrackedHeartTech
				{
					m_TrackedVisible = trackedVisible
				};
			}
			value.m_HeartCount++;
			m_TechsWithHeartBlocks[trackedVisible.ID] = value;
		}
	}

	public void TrackedTechHeartRemoved(Tank tech, bool retainTechTracking)
	{
		if (!m_ManagerActive || !tech)
		{
			return;
		}
		TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(tech.visible.ID);
		if (trackedVisible != null)
		{
			if (m_TechsWithHeartBlocks.TryGetValue(trackedVisible.ID, out var value))
			{
				value.m_HeartCount--;
				if (value.m_HeartCount <= 0 && !retainTechTracking)
				{
					m_TechsWithHeartBlocks.Remove(trackedVisible.ID);
				}
				else
				{
					m_TechsWithHeartBlocks[trackedVisible.ID] = value;
				}
			}
		}
		else
		{
			d.LogError($"ManPlayer.ClearTrackedTech: Could not find Tracked Visible for tech. Name {tech.name} visibleID {tech.visible.ID}");
		}
	}

	public TrackedVisible GetNearestHeartBlock(Vector3 position)
	{
		TrackedVisible result = null;
		float num = float.MaxValue;
		foreach (KeyValuePair<int, TrackedHeartTech> techsWithHeartBlock in m_TechsWithHeartBlocks)
		{
			TrackedVisible trackedVisible = techsWithHeartBlock.Value.m_TrackedVisible;
			float sqrMagnitude = (trackedVisible.Position.SetY(0f) - position.SetY(0f)).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				result = trackedVisible;
				num = sqrMagnitude;
			}
		}
		return result;
	}

	public bool DoesTechHavePlayerHeartBlock(TrackedVisible tv)
	{
		if (tv != null)
		{
			return m_TechsWithHeartBlocks.ContainsKey(tv.ID);
		}
		return false;
	}

	public bool DoesTechHavePlayerHeartBlock(Tank tech)
	{
		if (tech != null)
		{
			return m_TechsWithHeartBlocks.ContainsKey(tech.visible.ID);
		}
		return false;
	}

	public TechData GetHotswapTech(int slot)
	{
		if (m_HotswapMap.TryGetBinding(slot, out var binding))
		{
			return binding.m_TechData;
		}
		return null;
	}

	public int GetHotswapSlot(SnapshotDisk snapshotDisk)
	{
		if (m_HotswapMap.TryGetSlot(snapshotDisk, out var slot))
		{
			return slot;
		}
		return 0;
	}

	public void SetHotswapBinding(SnapshotDisk snapshot, int slot)
	{
		if (slot == 0)
		{
			m_HotswapMap.Remove(snapshot);
		}
		else
		{
			m_HotswapMap.AddOrReplace(snapshot, slot);
		}
		HotswapBindingChanged.Send(m_HotswapMap);
	}

	public void SetPlayerInventoryToUnrestricted()
	{
		m_SaveData.m_Inventory = null;
	}

	public void SetPlayerHasEnabledCheatCommands()
	{
		m_SaveData.m_HasEnabledCheatCommands = true;
	}

	public void AddBlockToInventory(BlockTypes blockType)
	{
		if (Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeMain>())
		{
			Singleton.Manager<ManLicenses>.inst.DiscoverBlock(blockType);
		}
		Singleton.Manager<ManPurchases>.inst.GetInventory()?.HostAddItem(blockType);
	}

	public void EnablePalette(bool enable, bool usePlayerInventory = true)
	{
		if (enable)
		{
			Singleton.Manager<ManPurchases>.inst.ShowPalette(show: true);
			IInventory<BlockTypes> inventory = (usePlayerInventory ? PlayerInventory : null);
			Singleton.Manager<ManPurchases>.inst.SetInventory(inventory);
			if (!m_SaveData.m_PaletteUnlocked)
			{
				m_SaveData.m_PaletteUnlocked = true;
				OnPaletteUnlocked.Send();
			}
		}
		else
		{
			Singleton.Manager<ManPurchases>.inst.ShowPalette(show: false);
			Singleton.Manager<ManPurchases>.inst.SetInventory(null);
		}
	}

	public void PayMoney(int amount)
	{
		if (m_SaveData.m_Money >= amount)
		{
			m_SaveData.m_Money -= amount;
			Singleton.Manager<ManStats>.inst.MoneySpent(amount);
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.Buy);
			MoneyAmountChanged.Send(m_SaveData.m_Money);
		}
	}

	public bool CanAfford(int amount)
	{
		bool result = true;
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCampaign())
		{
			result = amount < m_SaveData.m_Money;
		}
		return result;
	}

	public void AddMoney(int amount)
	{
		if (ManNetwork.IsHost)
		{
			m_SaveData.m_Money += amount;
			MoneyAmountChanged.Send(m_SaveData.m_Money);
			Singleton.Manager<ManStats>.inst.MoneyEarned(amount);
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.EarnMoney);
		}
	}

	public int GetCurrentMoney()
	{
		return m_SaveData.m_Money;
	}

	public void Debug_SetMoney(int newMoney)
	{
		if (ManNetwork.IsHost)
		{
			int money = m_SaveData.m_Money;
			m_SaveData.m_Money = newMoney;
			MoneyAmountChanged.Send(m_SaveData.m_Money);
			Singleton.Manager<ManSFX>.inst.PlayUISFX((newMoney > money) ? ManSFX.UISfxType.EarnMoney : ManSFX.UISfxType.Buy);
		}
	}

	public void SendMoneyLevelToClient(NetPlayer targetPlayer)
	{
		Singleton.Manager<ManNetwork>.inst.SendToClient(targetPlayer.connectionToClient.connectionId, TTMsgType.MoneyChanged, new MoneyChangedMessage
		{
			m_Money = GetCurrentMoney()
		});
	}

	public void SetClientLastUsedTech(NetPlayer player, NetTech tech)
	{
		if (player.IsHostPlayer)
		{
			return;
		}
		int num = ((tech != null) ? tech.HostID : 0);
		bool flag = false;
		foreach (PlayerSaveData player2 in m_SaveData.m_Players)
		{
			if (player2.m_NetID == player.GetPlayerIDInLobby())
			{
				m_SaveData.m_MPClientTechData.Remove(player2.m_TrackedVisibleIDOfLastTech);
				player2.m_TrackedVisibleIDOfLastTech = num;
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			m_SaveData.m_Players.Add(new PlayerSaveData
			{
				m_NetID = player.GetPlayerIDInLobby(),
				m_TrackedVisibleIDOfLastTech = num
			});
		}
		m_SaveData.m_MPClientTechData.Add(num);
	}

	public bool IsMPClientTech(int trackedVisID)
	{
		return m_SaveData.m_MPClientTechData.Contains(trackedVisID);
	}

	public int GetLastUsedTrackedVisibleID(NetPlayer player)
	{
		foreach (PlayerSaveData player2 in m_SaveData.m_Players)
		{
			if (player2.m_NetID == player.GetPlayerIDInLobby())
			{
				return player2.m_TrackedVisibleIDOfLastTech;
			}
		}
		return 0;
	}

	public void AddDeathLocation(Vector3 position)
	{
		m_PlayerDeathLocations.Add(WorldPosition.FromScenePosition(in position));
		m_TimeUntilNextDeathMarkerClear = 1f;
	}

	private void OnClientMoneyUpdate(NetworkMessage netMsg)
	{
		MoneyChangedMessage moneyChangedMessage = netMsg.ReadMessage<MoneyChangedMessage>();
		if (!ManNetwork.IsHost && m_SaveData.m_Money != moneyChangedMessage.m_Money)
		{
			d.Log($"OnClientMoneyUpdate: {moneyChangedMessage.m_Money}");
			m_SaveData.m_Money = moneyChangedMessage.m_Money;
			MoneyAmountChanged.Send(m_SaveData.m_Money);
		}
	}

	private void OnPlayerTankChanged(Tank tank, bool willBePlayerControlled)
	{
		if (willBePlayerControlled && tank.IsNotNull())
		{
			m_LastPlayerTank = tank;
		}
	}

	private void OnTankDestroyed(Tank tank, ManDamage.DamageInfo info)
	{
		if (m_LastPlayerTank.IsNotNull() && (object)tank == m_LastPlayerTank && !Singleton.Manager<ManPointer>.inst.IsDraggingController)
		{
			AddDeathLocation(tank.boundsCentreWorld.SetY(0f));
			m_LastPlayerTank = null;
		}
	}

	private void Start()
	{
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.MoneyChanged, OnClientMoneyUpdate);
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTankChanged);
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Subscribe(OnTankDestroyed);
	}

	private void Update()
	{
		if (m_ManagerActive && !m_SaveData.m_PaletteUnlocked && Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeMain>())
		{
			bool flag = false;
			Dictionary<int, TrackedHeartTech>.Enumerator enumerator = m_TechsWithHeartBlocks.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Visible visible = enumerator.Current.Value.m_TrackedVisible.visible;
				if (!visible.IsNotNull() || !visible.tank.IsNotNull())
				{
					continue;
				}
				BlockManager.BlockIterator<ModuleHeart>.Enumerator enumerator2 = visible.tank.blockman.IterateBlockComponents<ModuleHeart>().GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.IsOnline)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				EnablePalette(enable: true);
			}
		}
		if (!Singleton.playerTank.IsNotNull())
		{
			return;
		}
		if (m_TimeUntilNextDeathMarkerClear > 0f)
		{
			m_TimeUntilNextDeathMarkerClear -= Time.deltaTime;
		}
		else
		{
			if (m_PlayerDeathLocations.Count <= 0)
			{
				return;
			}
			Vector3 posScene = Singleton.playerPos.SetY(0f);
			IntVector2 intVector = Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(in posScene);
			for (int num = m_PlayerDeathLocations.Count - 1; num >= 0; num--)
			{
				WorldPosition worldPosition = m_PlayerDeathLocations[num];
				IntVector2 intVector2 = IntVector2.Abs(intVector - worldPosition.TileCoord);
				if (intVector2.x <= 1 && intVector2.y <= 1 && (worldPosition.ScenePosition - posScene).sqrMagnitude < 8100f)
				{
					m_PlayerDeathLocations.RemoveAt(num);
				}
			}
			if (m_PlayerDeathLocations.Count > 0)
			{
				m_TimeUntilNextDeathMarkerClear = 1f;
			}
		}
	}
}
