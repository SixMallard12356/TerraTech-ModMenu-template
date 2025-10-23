#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class ManBlockLimiter : Singleton.Manager<ManBlockLimiter>, Mode.IManagerModeEvents
{
	[Serializable]
	private class ComponentCost
	{
		public string name = "";

		public float cost;

		public bool freeOnCab;
	}

	public enum TechCategory : sbyte
	{
		None = -1,
		Player,
		Population,
		Mission,
		Max
	}

	public struct CostChangeInfo
	{
		public int m_VisibleID;

		public int m_TechCost;

		public int m_CategoryCost;

		public TechCategory m_TechCategory;

		public int m_TeamColour;
	}

	[Serializable]
	private class PerTechBlockUsage
	{
		public int costOfTechBlocks;

		public int costOfHeldItems;

		public bool hidden;

		public TechCategory TechCategory;

		public int TeamColour;

		public PerTechBlockUsage(TechCategory category)
		{
			TechCategory = category;
		}

		public void Reset(TechCategory category)
		{
			costOfTechBlocks = 0;
			costOfHeldItems = 0;
			hidden = false;
			TechCategory = category;
			TeamColour = 0;
		}
	}

	[SerializeField]
	private int m_MaximumPlayerUsage;

	[SerializeField]
	private int m_MaximumPlayerUsage_Switch;

	[SerializeField]
	private int m_PopulationMaximumUsageDefault = 5000;

	[SerializeField]
	private int m_PopulationMaximumUsageDefault_Switch = 3500;

	[SerializeField]
	private int m_ExtraCapacityCreative = 2000;

	[SerializeField]
	private int m_ExtraCapacityCreative_Switch = 1000;

	[SerializeField]
	private int m_ExtraCapacityCoOpCreative = 2000;

	[SerializeField]
	private int m_ExtraCapacityCoOpCreative_Switch = 1000;

	[SerializeField]
	private int m_ExtraCapacityCoOpCampaign = 2000;

	[SerializeField]
	private int m_ExtraCapacityCoOpCampaign_Switch = 1000;

	[SerializeField]
	private int m_ShowUIUsagePercent;

	[SerializeField]
	private int m_PerTechCost;

	[SerializeField]
	private int m_CostOfHeldChunk;

	[SerializeField]
	private float m_CostOfHeldChunkReserved;

	[SerializeField]
	private int m_CostOfHeldBlock;

	[SerializeField]
	private float m_CostOfHeldBlockReserved;

	[SerializeField]
	private string m_SnapshotNamePrefix;

	[SerializeField]
	private ComponentCost[] m_ComponentCosts;

	[SerializeField]
	[EnumArray(typeof(BlockTypes))]
	public float[] m_BlockCostBasis;

	public Event<CostChangeInfo> CostChangedEvent;

	public Event<int, int> IDSwappedEvent;

	private int m_MaximumUsage;

	private int m_PopulationMaximumUsage;

	private bool m_Active;

	private bool m_IsCoOpCreative;

	private bool m_IsCreative;

	private int m_ExtraAllowanceForMode;

	private Dictionary<int, PerTechBlockUsage> m_PerTechBlockUsage = new Dictionary<int, PerTechBlockUsage>();

	private Dictionary<string, ComponentCost> m_ComponentCostDict = new Dictionary<string, ComponentCost>();

	private int[] m_BlockCost;

	private int m_ConfirmRemoveTechID;

	private int[] m_CurrentUsage = new int[3];

	private ManSaveGame.State m_OptionalLoadState;

	public static bool LimiterSupported => false;

	public static bool LimiterToggleSupported => false;

	public bool RequestedOn
	{
		get
		{
			return false;
		}
		set
		{
			d.LogWarning("Toggling of block limiter not allowed in this build - ignored");
		}
	}

	public bool LimiterActive => m_Active;

	public int MaximumUsage => m_MaximumUsage;

	public int PerTechCost => m_PerTechCost;

	public bool IsOverNonPlayerLimit => m_CurrentUsage[2] + m_CurrentUsage[1] > m_PopulationMaximumUsage;

	public int UsableMissionLimit
	{
		get
		{
			if (!SKU.SwitchUI)
			{
				return m_PopulationMaximumUsageDefault;
			}
			return m_PopulationMaximumUsageDefault_Switch;
		}
	}

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		ManGameMode.GameType currentGameType = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType();
		m_IsCreative = currentGameType == ManGameMode.GameType.CoOpCreative || currentGameType == ManGameMode.GameType.Creative;
		m_IsCoOpCreative = currentGameType == ManGameMode.GameType.CoOpCreative;
		switch (currentGameType)
		{
		case ManGameMode.GameType.Creative:
			m_ExtraAllowanceForMode = (SKU.SwitchUI ? m_ExtraCapacityCreative_Switch : m_ExtraCapacityCreative);
			break;
		case ManGameMode.GameType.CoOpCreative:
			m_ExtraAllowanceForMode = (SKU.SwitchUI ? m_ExtraCapacityCoOpCreative_Switch : m_ExtraCapacityCoOpCreative);
			break;
		case ManGameMode.GameType.CoOpCampaign:
			m_ExtraAllowanceForMode = (SKU.SwitchUI ? m_ExtraCapacityCoOpCampaign_Switch : m_ExtraCapacityCoOpCampaign);
			break;
		default:
			m_ExtraAllowanceForMode = 0;
			break;
		}
		bool flag = LimiterSupported && (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer() || Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeCoOpCreative>() || Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeCoOpCampaign>());
		if (flag)
		{
			if (optionalLoadState == null)
			{
				flag = RequestedOn;
			}
			else if (optionalLoadState.CheckHasSaveData(ManSaveGame.SaveDataJSONType.ManBlockLimiter))
			{
				flag = true;
				if (!RequestedOn)
				{
					d.LogWarning("Loaded save contains block limiter data, so limiter enabled regardless of current settings");
				}
			}
			else
			{
				flag = false;
				if (RequestedOn)
				{
					d.LogWarning("Loaded save contains no block limiter data, so limiter disabled regardless of current settings");
				}
			}
		}
		m_Active = flag;
		if (m_Active)
		{
			UpdateMaximum();
			SubscribeToEvents();
		}
		if (m_Active && optionalLoadState != null)
		{
			m_OptionalLoadState = optionalLoadState;
			Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Subscribe(OnModeStart);
		}
	}

	private void OnModeStart(Mode mode)
	{
		Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Unsubscribe(OnModeStart);
		if (!Load(m_OptionalLoadState))
		{
			d.LogError("No ManBlockLimiter data found in save file");
		}
		foreach (KeyValuePair<int, PerTechBlockUsage> item in m_PerTechBlockUsage)
		{
			SendCostChangedEvent(item.Key, item.Value);
		}
		m_OptionalLoadState = null;
	}

	public void Save(ManSaveGame.State saveState)
	{
		if (LimiterActive)
		{
			saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManBlockLimiter, m_PerTechBlockUsage);
		}
	}

	private bool Load(ManSaveGame.State optionalLoadState)
	{
		bool result = false;
		if (optionalLoadState != null && optionalLoadState.GetSaveData<Dictionary<int, PerTechBlockUsage>>(ManSaveGame.SaveDataJSONType.ManBlockLimiter, out var saveData) && saveData != null)
		{
			m_PerTechBlockUsage = saveData;
			List<int> list = new List<int>();
			foreach (int key in m_PerTechBlockUsage.Keys)
			{
				if (Singleton.Manager<ManVisible>.inst.GetTrackedVisible(key) == null)
				{
					d.LogWarningFormat("Removing unknown tech ID {0} (costOfTechBlocks={1}) from block limiter", key, m_PerTechBlockUsage[key].costOfTechBlocks);
					list.Add(key);
				}
			}
			foreach (int item in list)
			{
				m_PerTechBlockUsage.Remove(item);
			}
			for (int i = 0; i < 3; i++)
			{
				m_CurrentUsage[i] = 0;
			}
			foreach (PerTechBlockUsage value in m_PerTechBlockUsage.Values)
			{
				m_CurrentUsage[(int)value.TechCategory] += GetTechCost(value);
			}
			result = true;
		}
		UpdateMaximum();
		return result;
	}

	public void ModeExit()
	{
		if (m_Active)
		{
			UnsubscribeFromEvents();
		}
		m_PerTechBlockUsage.Clear();
		for (int i = 0; i < 3; i++)
		{
			m_CurrentUsage[i] = 0;
		}
		m_Active = false;
	}

	private ManBlockLimiter()
	{
	}

	private void SendCostChangedEvent(int id, PerTechBlockUsage usage, bool techRemoved = false)
	{
		CostChangedEvent.Send(new CostChangeInfo
		{
			m_VisibleID = id,
			m_TechCost = ((!techRemoved && usage != null) ? GetTechCost(usage) : 0),
			m_CategoryCost = m_CurrentUsage[(int)usage.TechCategory],
			m_TechCategory = usage.TechCategory,
			m_TeamColour = usage.TeamColour
		});
	}

	private void SubscribeToEvents()
	{
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Subscribe(OnTankDestroyedEvent);
		Singleton.Manager<ManTechs>.inst.TankPostSpawnEvent.Subscribe(OnTankPostSpawnEvent);
		Singleton.Manager<ManTechs>.inst.TankTeamChangedEvent.Subscribe(OnTechTeamChange);
		TankBeam.OnBeamEnabled.Subscribe(OnTankBeamEnabled);
		Singleton.Manager<ManLicenses>.inst.LevelUpEvent.Subscribe(OnLevelUpEvent);
		Singleton.Manager<ManPurchases>.inst.StoreTechToInventoryEvent.Subscribe(OnStoreTechToInventory);
	}

	private void UnsubscribeFromEvents()
	{
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Unsubscribe(OnTankDestroyedEvent);
		Singleton.Manager<ManTechs>.inst.TankPostSpawnEvent.Unsubscribe(OnTankPostSpawnEvent);
		Singleton.Manager<ManTechs>.inst.TankTeamChangedEvent.Unsubscribe(OnTechTeamChange);
		TankBeam.OnBeamEnabled.Unsubscribe(OnTankBeamEnabled);
		Singleton.Manager<ManLicenses>.inst.LevelUpEvent.Unsubscribe(OnLevelUpEvent);
		Singleton.Manager<ManPurchases>.inst.StoreTechToInventoryEvent.Unsubscribe(OnStoreTechToInventory);
	}

	private int GetTechCost(PerTechBlockUsage usage, bool includeHeldItems = true)
	{
		if (usage.hidden)
		{
			return 0;
		}
		int num = m_PerTechCost + usage.costOfTechBlocks;
		if (includeHeldItems)
		{
			num += usage.costOfHeldItems;
		}
		return num;
	}

	private PerTechBlockUsage AddEmptyTank(int tankID, TechCategory category)
	{
		if (m_PerTechBlockUsage.TryGetValue(tankID, out var value))
		{
			m_CurrentUsage[(int)value.TechCategory] -= GetTechCost(value);
			value.Reset(category);
		}
		else
		{
			value = new PerTechBlockUsage(category);
			m_PerTechBlockUsage.Add(tankID, value);
		}
		return value;
	}

	public void AddTrackedVisibleTech(int tankID, int cost, int teamID, bool isPopulation)
	{
		if (m_Active)
		{
			TechCategory techCategory = CategoriseTech(teamID, isPopulation);
			if (techCategory != TechCategory.None)
			{
				PerTechBlockUsage perTechBlockUsage = AddEmptyTank(tankID, techCategory);
				perTechBlockUsage.costOfTechBlocks = cost - m_PerTechCost;
				m_CurrentUsage[(int)techCategory] += GetTechCost(perTechBlockUsage);
				SendCostChangedEvent(tankID, perTechBlockUsage);
			}
		}
	}

	private void AddTank(Tank tank, TechCategory category, int teamColour)
	{
		int iD = tank.visible.ID;
		int num = m_CurrentUsage[(int)category];
		PerTechBlockUsage perTechBlockUsage = AddEmptyTank(iD, category);
		perTechBlockUsage.TeamColour = teamColour;
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tank.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current = enumerator.Current;
			perTechBlockUsage.costOfTechBlocks += GetBlockCost(current);
		}
		TechHolders.HolderIterator enumerator2 = tank.Holders.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			ModuleItemHolder.Stack.ItemIterator enumerator3 = enumerator2.Current.Contents.GetEnumerator();
			while (enumerator3.MoveNext())
			{
				Visible current2 = enumerator3.Current;
				perTechBlockUsage.costOfHeldItems += GetHeldItemCost(current2);
			}
		}
		m_CurrentUsage[(int)category] += GetTechCost(perTechBlockUsage);
		RemoveCallbacks(tank);
		tank.AttachEvent.Subscribe(OnBlockAttach);
		tank.DetachEvent.Subscribe(OnBlockDetach);
		tank.TankRecycledEvent.Subscribe(OnTankRecycled);
		tank.Holders.ItemPickupEvent.Subscribe(OnTankPickupItem);
		tank.Holders.ItemReleaseEvent.Subscribe(OnTankReleaseItem);
		if (m_CurrentUsage[(int)category] != num)
		{
			SendCostChangedEvent(iD, perTechBlockUsage);
		}
	}

	private void AddStoredTechsFromTile(ManSaveGame.StoredTile tile)
	{
		if (tile == null || !tile.m_StoredVisibles.TryGetValue(1, out var value))
		{
			return;
		}
		foreach (ManSaveGame.StoredVisible item in value)
		{
			if (item is ManSaveGame.StoredTech)
			{
				AddStoredTech((ManSaveGame.StoredTech)item);
			}
		}
	}

	private void AddStoredTech(ManSaveGame.StoredTech tech)
	{
		TechCategory techCategory = CategoriseTech(tech.m_TeamID, tech.m_IsPopulation);
		if (techCategory == TechCategory.None)
		{
			return;
		}
		int iD = tech.m_ID;
		PerTechBlockUsage perTechBlockUsage = AddEmptyTank(iD, techCategory);
		foreach (TankPreset.BlockSpec blockSpec in tech.m_TechData.m_BlockSpecs)
		{
			perTechBlockUsage.costOfTechBlocks += GetBlockCost(blockSpec.GetBlockType());
		}
		if (tech.m_StoredHeldVisibles != null)
		{
			foreach (ManSaveGame.StoredVisible storedHeldVisible in tech.m_StoredHeldVisibles)
			{
				perTechBlockUsage.costOfHeldItems += GetHeldItemCost(storedHeldVisible);
			}
		}
		m_CurrentUsage[(int)techCategory] += GetTechCost(perTechBlockUsage);
		SendCostChangedEvent(iD, perTechBlockUsage);
	}

	private void RemoveCallbacks(Tank tank)
	{
		tank.AttachEvent.Unsubscribe(OnBlockAttach);
		tank.DetachEvent.Unsubscribe(OnBlockDetach);
		tank.TankRecycledEvent.Unsubscribe(OnTankRecycled);
		tank.Holders.ItemPickupEvent.Unsubscribe(OnTankPickupItem);
		tank.Holders.ItemReleaseEvent.Unsubscribe(OnTankReleaseItem);
	}

	private void OnTankRecycled(Tank tank)
	{
		int iD = tank.visible.ID;
		if (m_PerTechBlockUsage.TryGetValue(iD, out var value) && value.TechCategory != TechCategory.Player)
		{
			RemoveTechByID(iD);
		}
		RemoveCallbacks(tank);
	}

	private void OnTankPickupItem(Tank tank, Visible item)
	{
		int iD = tank.visible.ID;
		if (m_PerTechBlockUsage.TryGetValue(iD, out var value))
		{
			int heldItemCost = GetHeldItemCost(item);
			value.costOfHeldItems += heldItemCost;
			if (!value.hidden)
			{
				m_CurrentUsage[(int)value.TechCategory] += heldItemCost;
				SendCostChangedEvent(iD, value);
			}
		}
	}

	private void OnTankReleaseItem(Tank tank, Visible item)
	{
		if (ManSaveGame.Storing)
		{
			return;
		}
		int iD = tank.visible.ID;
		if (m_PerTechBlockUsage.TryGetValue(iD, out var value))
		{
			int heldItemCost = GetHeldItemCost(item);
			value.costOfHeldItems -= heldItemCost;
			if (!value.hidden)
			{
				m_CurrentUsage[(int)value.TechCategory] -= heldItemCost;
				SendCostChangedEvent(iD, value);
			}
		}
	}

	private void OnBlockAttach(TankBlock block, Tank tank)
	{
		int iD = tank.visible.ID;
		if (m_PerTechBlockUsage.TryGetValue(iD, out var value))
		{
			int blockCost = GetBlockCost(block);
			value.costOfTechBlocks += blockCost;
			if (!value.hidden)
			{
				m_CurrentUsage[(int)value.TechCategory] += blockCost;
				SendCostChangedEvent(iD, value);
			}
		}
	}

	private void OnBlockDetach(TankBlock block, Tank tank)
	{
		if (ManSaveGame.Storing)
		{
			return;
		}
		int iD = tank.visible.ID;
		if (m_PerTechBlockUsage.TryGetValue(iD, out var value))
		{
			int blockCost = GetBlockCost(block);
			value.costOfTechBlocks -= blockCost;
			if (!value.hidden)
			{
				m_CurrentUsage[(int)value.TechCategory] -= blockCost;
				SendCostChangedEvent(iD, value);
			}
		}
	}

	private void OnTankDestroyedEvent(Tank tank, ManDamage.DamageInfo damageInfo)
	{
		RemoveTechByID(tank.visible.ID);
	}

	private void OnTankPostSpawnEvent(Tank tank)
	{
		d.Assert(tank.Team != int.MaxValue);
		TechCategory techCategory = CategoriseTech(tank);
		if (techCategory != TechCategory.None)
		{
			AddTank(tank, techCategory, GetTeamColour(tank.Team));
		}
	}

	private int GetTeamColour(int techTeam)
	{
		int num = 0;
		if (m_IsCoOpCreative)
		{
			num = ManSpawn.LobbyTeamIDFromTechTeamID(techTeam);
			if (num == int.MaxValue)
			{
				num = 5;
			}
		}
		return num;
	}

	private TechCategory CategoriseTech(Tank tech)
	{
		return CategoriseTech(tech.Team, tech.IsPopulation);
	}

	private TechCategory CategoriseTech(int team, bool isPopulation)
	{
		TechCategory techCategory = TechCategory.None;
		if (ManSpawn.IsPlayerTeam(team))
		{
			return TechCategory.Player;
		}
		if (isPopulation)
		{
			return TechCategory.Population;
		}
		if (m_IsCoOpCreative)
		{
			return TechCategory.Player;
		}
		return TechCategory.Mission;
	}

	private void OnTechTeamChange(Tank tech, ManTechs.TeamChangeInfo changeInfo)
	{
		if (!m_Active || changeInfo.m_OldTeam == int.MaxValue)
		{
			return;
		}
		TechCategory techCategory = CategoriseTech(changeInfo.m_OldTeam, changeInfo.m_OldIsPopulation);
		TechCategory techCategory2 = CategoriseTech(changeInfo.m_NewTeam, changeInfo.m_NewIsPopulation);
		PerTechBlockUsage value;
		if (techCategory != techCategory2)
		{
			if (techCategory != TechCategory.None)
			{
				RemoveTechByID(tech.visible.ID);
				RemoveCallbacks(tech);
			}
			if (techCategory2 != TechCategory.None)
			{
				AddTank(tech, techCategory2, GetTeamColour(changeInfo.m_NewTeam));
			}
		}
		else if (m_IsCoOpCreative && m_PerTechBlockUsage.TryGetValue(tech.visible.ID, out value))
		{
			int teamColour = GetTeamColour(changeInfo.m_NewTeam);
			if (teamColour != value.TeamColour)
			{
				value.TeamColour = teamColour;
				SendCostChangedEvent(tech.visible.ID, value);
			}
		}
	}

	private void OnTankBeamEnabled(Tank tank, bool enabledBeam)
	{
		if (tank == Singleton.playerTank)
		{
			if (enabledBeam)
			{
				UIBlockLimit.ShowUI(UIBlockLimit.ShowReason.BuildBeam);
			}
			else
			{
				UIBlockLimit.HideUI(UIBlockLimit.ShowReason.BuildBeam);
			}
		}
	}

	private void OnLevelUpEvent(FactionSubTypes corp, int level)
	{
		if (m_Active)
		{
			UpdateMaximum();
		}
	}

	private bool CanRemoveTechPair(KeyValuePair<int, PerTechBlockUsage> techPair)
	{
		if (techPair.Value.hidden)
		{
			return false;
		}
		if (Singleton.playerTank.IsNotNull() && techPair.Key == Singleton.playerTank.visible.ID)
		{
			return false;
		}
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetwork>.inst.IsPlayerTechID(techPair.Key))
		{
			return false;
		}
		return true;
	}

	private int GetFurthestTechID(Vector3 refPos, TechCategory techCategory)
	{
		float num = 0f;
		int result = 0;
		List<int> list = new List<int>();
		foreach (KeyValuePair<int, PerTechBlockUsage> item in m_PerTechBlockUsage)
		{
			if (item.Value.TechCategory != techCategory)
			{
				continue;
			}
			TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(item.Key);
			if (trackedVisible != null)
			{
				float sqrMagnitude = (trackedVisible.Position - refPos).sqrMagnitude;
				if (sqrMagnitude > num)
				{
					num = sqrMagnitude;
					result = item.Key;
				}
			}
			else
			{
				Debug.LogError("ManBlockLimiter: Failed to get furthest Tech ID removing this tech from the list of block uses");
				list.Add(item.Key);
			}
		}
		foreach (int item2 in list)
		{
			m_PerTechBlockUsage.Remove(item2);
		}
		return result;
	}

	private static TechData GetStoredPlayerTech(TrackedVisible tank)
	{
		if (!ManSpawn.IsPlayerTeam(tank.TeamID))
		{
			return null;
		}
		return Singleton.Manager<ManSaveGame>.inst.GetStoredTechData(tank);
	}

	private bool ReturnBlocksToInventory(IInventory<BlockTypes> inventory, TechData techData)
	{
		if (inventory == null)
		{
			return false;
		}
		foreach (TankPreset.BlockSpec blockSpec in techData.m_BlockSpecs)
		{
			inventory.HostAddItem(blockSpec.GetBlockType());
		}
		return true;
	}

	private void DespawnTech(int tankID)
	{
		TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(tankID);
		if (trackedVisible == null || trackedVisible.ObjectType != ObjectTypes.Vehicle)
		{
			d.LogError("No TrackedVisible associated with tech ID #" + tankID + " in ManBlockLimiter");
			RemoveTechByID(tankID);
		}
		else
		{
			Singleton.Manager<ManVisible>.inst.ObliterateTrackedVisibleFromWorld(trackedVisible);
		}
	}

	private void OnStoreTechToInventory(Tank tank)
	{
		RemoveTechByID(tank.visible.ID);
	}

	private ComponentCost GetComponentCost(Type t)
	{
		Type type = t;
		while (type != typeof(object))
		{
			if (m_ComponentCostDict.TryGetValue(type.Name, out var value))
			{
				if (type != t)
				{
					m_ComponentCostDict[t.Name] = value;
				}
				return value;
			}
			type = type.BaseType;
		}
		m_ComponentCostDict[t.Name] = new ComponentCost();
		return null;
	}

	private float CalculateBlockExtraCost(Transform t, bool isCab)
	{
		float num = 0f;
		Component[] components = t.GetComponents<Component>();
		foreach (Component component in components)
		{
			ModuleItemHolder moduleItemHolder = component as ModuleItemHolder;
			if (moduleItemHolder != null && (m_CostOfHeldBlockReserved > 0f || m_CostOfHeldChunkReserved > 0f) && t.GetComponent<ModuleHeart>() == null)
			{
				int totalCapacityForLimiter = moduleItemHolder.GetTotalCapacityForLimiter();
				if ((moduleItemHolder.Acceptance & ModuleItemHolder.AcceptFlags.Blocks) != 0)
				{
					num += (float)Mathf.CeilToInt((float)totalCapacityForLimiter * m_CostOfHeldBlockReserved);
				}
				if ((moduleItemHolder.Acceptance & ModuleItemHolder.AcceptFlags.Chunks) != 0)
				{
					num += (float)Mathf.CeilToInt((float)totalCapacityForLimiter * m_CostOfHeldChunkReserved);
				}
			}
			ComponentCost componentCost = GetComponentCost(component.GetType());
			if (componentCost != null && (!isCab || !componentCost.freeOnCab))
			{
				num += componentCost.cost;
			}
		}
		foreach (Transform item in t)
		{
			num += CalculateBlockExtraCost(item, isCab);
		}
		return num;
	}

	private void UpdateMaximum()
	{
		int maximumUsage = m_MaximumUsage;
		m_MaximumUsage = (SKU.SwitchUI ? m_MaximumPlayerUsage_Switch : m_MaximumPlayerUsage);
		m_PopulationMaximumUsage = m_PopulationMaximumUsageDefault;
		m_MaximumUsage += m_ExtraAllowanceForMode;
		if (m_IsCreative)
		{
			m_PopulationMaximumUsage -= m_ExtraAllowanceForMode;
		}
		if (maximumUsage != m_MaximumUsage)
		{
			CostChangedEvent.Send(new CostChangeInfo
			{
				m_CategoryCost = m_CurrentUsage[0],
				m_TechCategory = TechCategory.Player
			});
		}
	}

	public int GetHeldItemCost(ManSaveGame.StoredVisible item)
	{
		if (item is ManSaveGame.StoredChunk)
		{
			return m_CostOfHeldChunk;
		}
		if (item is ManSaveGame.StoredBlock)
		{
			return m_CostOfHeldBlock;
		}
		return 0;
	}

	public int GetHeldItemCost(Visible item)
	{
		return item.type switch
		{
			ObjectTypes.Chunk => m_CostOfHeldChunk, 
			ObjectTypes.Block => m_CostOfHeldBlock, 
			_ => 0, 
		};
	}

	public int GetBlockCost(BlockTypes type)
	{
		bool hasMissingBlock = false;
		return GetBlockCost(type, ref hasMissingBlock);
	}

	public int GetBlockCost(BlockTypes type, ref bool hasMissingBlock)
	{
		if (m_BlockCost == null)
		{
			hasMissingBlock = true;
			return 1;
		}
		if (type < BlockTypes.GSOAIController_111 || (int)type >= m_BlockCost.Length)
		{
			return 1;
		}
		int num = m_BlockCost[(int)type];
		if (num == 0)
		{
			TankBlock blockPrefab = Singleton.Manager<ManSpawn>.inst.GetBlockPrefab(type);
			if (blockPrefab == null)
			{
				num = 1;
				hasMissingBlock = true;
			}
			else
			{
				bool isCab = blockPrefab.GetComponent<ModuleDriveBot>() != null || blockPrefab.IsController;
				float num2 = (((int)type >= m_BlockCostBasis.Length) ? 0f : m_BlockCostBasis[(int)type]);
				if (num2 <= 0f)
				{
					num2 = 1 + blockPrefab.filledCells.Length / 2;
				}
				num2 += CalculateBlockExtraCost(blockPrefab.transform, isCab);
				num = Mathf.Max(1, Mathf.CeilToInt(num2));
				m_BlockCost[(int)type] = num;
			}
		}
		return num;
	}

	public int GetBlockCost(TankBlock block)
	{
		return GetBlockCost(block.BlockType);
	}

	public int GetTechCost(TechData techData, out bool hasMissingBlock)
	{
		hasMissingBlock = false;
		int num = m_PerTechCost;
		foreach (TankPreset.BlockSpec blockSpec in techData.m_BlockSpecs)
		{
			num += GetBlockCost(blockSpec.GetBlockType(), ref hasMissingBlock);
		}
		return num;
	}

	public int GetTrackedTechCost(int visibleID, bool includeHeldItems)
	{
		if (m_PerTechBlockUsage.TryGetValue(visibleID, out var value))
		{
			return GetTechCost(value, includeHeldItems);
		}
		return 0;
	}

	public int GetTrackedTechCost(Tank tech, bool includeHeldItems)
	{
		if (tech != null && m_PerTechBlockUsage.TryGetValue(tech.visible.ID, out var value))
		{
			return GetTechCost(value, includeHeldItems);
		}
		d.LogErrorFormat("ManBlockLimiter.GetTrackedTechCost - Tank {0} was not tracked in the block limiter! Could not find tank cost value.", tech ? tech.name : "<NULL>");
		return 0;
	}

	public void RemoveTechByID(int tankID)
	{
		if (m_PerTechBlockUsage.TryGetValue(tankID, out var value))
		{
			m_CurrentUsage[(int)value.TechCategory] -= GetTechCost(value);
			m_PerTechBlockUsage.Remove(tankID);
			SendCostChangedEvent(tankID, value, techRemoved: true);
		}
	}

	public void ReassociateID(int oldID, int newID)
	{
		if (m_PerTechBlockUsage.TryGetValue(oldID, out var value) && !m_PerTechBlockUsage.ContainsKey(newID))
		{
			m_PerTechBlockUsage.Remove(oldID);
			m_PerTechBlockUsage.Add(newID, value);
			IDSwappedEvent.Send(oldID, newID);
			return;
		}
		d.LogWarning($"Cannot reassociate BlockLimiter ID {oldID}(exists:{m_PerTechBlockUsage.ContainsKey(oldID)}) to {newID}(exists:{m_PerTechBlockUsage.ContainsKey(newID)}");
		RemoveTechByID(oldID);
	}

	public bool AllowCreatePlayerTech(int cost)
	{
		return AllowCreatePlayerTech(cost, 0);
	}

	public bool AllowCreatePlayerTech(int newTechCost, int replacedTechCost)
	{
		if (m_Active)
		{
			return m_CurrentUsage[0] + newTechCost - replacedTechCost <= m_MaximumUsage;
		}
		return true;
	}

	public bool AllowCreateSimplePlayerTech(TankBlock block)
	{
		return true;
	}

	public bool AllowPickup(TankBlock block, Visible visible)
	{
		if (m_Active && block.IsAttached && CategoriseTech(block.tank) == TechCategory.Player)
		{
			int heldItemCost = GetHeldItemCost(visible);
			if (heldItemCost > 0)
			{
				return m_CurrentUsage[0] + heldItemCost <= m_MaximumUsage;
			}
			return true;
		}
		return true;
	}

	public bool AllowPickupBy(TankBlock block, ModuleItemHolder.AcceptFlags acceptFlags)
	{
		if (m_Active && block.IsAttached && CategoriseTech(block.tank) == TechCategory.Player)
		{
			if (m_CostOfHeldBlock <= 0 && (acceptFlags & ModuleItemHolder.AcceptFlags.Blocks) != 0)
			{
				return true;
			}
			if (m_CostOfHeldChunk <= 0 && (acceptFlags & ModuleItemHolder.AcceptFlags.Chunks) != 0)
			{
				return true;
			}
			return m_CurrentUsage[0] <= m_MaximumUsage;
		}
		return true;
	}

	public bool AllowPlayerAttachBlock(TankBlock block)
	{
		if (!m_Active)
		{
			return true;
		}
		return m_CurrentUsage[0] <= m_MaximumUsage;
	}

	public void TagAsInteresting(Tank tank)
	{
	}

	public void SetTankHidden(Tank tank, bool hide)
	{
		if (!(tank != null))
		{
			return;
		}
		int iD = tank.visible.ID;
		if (m_PerTechBlockUsage.TryGetValue(iD, out var value))
		{
			if (value.hidden != hide)
			{
				m_CurrentUsage[(int)value.TechCategory] -= GetTechCost(value);
				value.hidden = hide;
				m_CurrentUsage[(int)value.TechCategory] += GetTechCost(value);
				SendCostChangedEvent(iD, value);
			}
		}
		else if (hide)
		{
			value = new PerTechBlockUsage(CategoriseTech(tank))
			{
				hidden = hide
			};
			m_PerTechBlockUsage.Add(iD, value);
		}
	}

	public int GetRemainingEnemyLimit()
	{
		return m_PopulationMaximumUsage - m_CurrentUsage[2] - m_CurrentUsage[1];
	}

	private void Start()
	{
		if (LimiterSupported)
		{
			int count = EnumValuesIterator<BlockTypes>.Count;
			m_BlockCost = new int[count];
			ComponentCost[] componentCosts = m_ComponentCosts;
			foreach (ComponentCost componentCost in componentCosts)
			{
				m_ComponentCostDict.Add(componentCost.name, componentCost);
			}
		}
		else
		{
			base.enabled = false;
		}
	}

	private void Update()
	{
		if (!m_Active || Singleton.playerTank == null)
		{
			return;
		}
		if (m_CurrentUsage[0] * 100 >= m_MaximumUsage * m_ShowUIUsagePercent)
		{
			UIBlockLimit.ShowUI(UIBlockLimit.ShowReason.Warning);
		}
		else
		{
			UIBlockLimit.HideUI(UIBlockLimit.ShowReason.Warning);
		}
		if (m_CurrentUsage[0] > m_MaximumUsage && !Singleton.Manager<ManHUD>.inst.IsAnyElementInGroupVisible(ManHUD.HUDGroup.GamepadQuickMenuHUDElements))
		{
			if (!Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.BlockLimiterWarning))
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.BlockLimiterWarning);
			}
		}
		else if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.BlockLimiterWarning))
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.BlockLimiterWarning);
		}
		if (IsOverNonPlayerLimit && m_CurrentUsage[1] > 0 && ManNetwork.IsHost)
		{
			Vector3 refPos = ((Singleton.playerTank != null) ? Singleton.playerTank.rootBlockTrans.position : Singleton.cameraTrans.position);
			int furthestTechID = GetFurthestTechID(refPos, TechCategory.Population);
			if (furthestTechID != 0)
			{
				DespawnTech(furthestTechID);
			}
		}
	}
}
