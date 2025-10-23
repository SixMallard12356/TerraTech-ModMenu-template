#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

public class ManStats : Singleton.Manager<ManStats>, Mode.IManagerModeEvents
{
	[Serializable]
	private class SaveData
	{
		public IntStatList m_ResourcesHarvested = new IntStatList(typeof(ChunkTypes));

		public IntStatList m_ResourcesSold = new IntStatList(typeof(ChunkTypes));

		public IntStatList m_ResourcesRefined = new IntStatList(typeof(ChunkTypes));

		public IntStat m_MoneyEarned = new IntStat();

		public IntStat m_MoneySpent = new IntStat();

		public IntStatList m_MoneyFromResourceSales = new IntStatList(typeof(ChunkTypes));

		public IntStatList m_BlocksPurchased = new IntStatList(typeof(BlockTypes));

		public IntStatList m_BlocksCrafted = new IntStatList(typeof(BlockTypes));

		public IntStatList m_BlocksScrapped = new IntStatList(typeof(BlockTypes));

		public IntStatList m_BlocksSold = new IntStatList(typeof(BlockTypes));

		public IntStatList m_MoneyFromBlockSales = new IntStatList(typeof(BlockTypes));

		public IntStatList m_BlocksScavenged = new IntStatList(typeof(BlockTypes));

		public IntStatList m_BlocksAttached = new IntStatList(typeof(BlockTypes));

		public IntStat m_SnapshotsDeployed = new IntStat();

		public IntStat m_PlayerTechsDestroyed = new IntStat();

		public IntStatList m_EnemyTechsDestroyed = new IntStatList(typeof(FactionSubTypes));

		public IntStat m_InvadersDestroyed = new IntStat();

		public DoubleStat m_DamageAbsorbedByShields = new DoubleStat();

		public DoubleStat m_PlayerBlockHealAmount = new DoubleStat();

		public DoubleStat m_EnergyGenerated = new DoubleStat();

		public IntStatList m_ResourceGiversDestroyed = new IntStatList(typeof(SceneryTypes));
	}

	public delegate void BlockCraftingEventDelegate(TankBlock crafter, TankBlock block, int blockTypeIdx, int blockTypeTotal, int craftedTotal);

	public delegate void IntStatChangeEventWithSubTypeDelegate(int subTypeIdx, int newSubTypeAmount, int newStatTotal);

	public delegate void IntStatChangeEventDelegate(int newStatTotal);

	public delegate void DoubleStatChangeEventDelegate(double newStatTotal);

	[Serializable]
	public class IntStat
	{
		[JsonProperty]
		[SerializeField]
		private int m_StatTotal;

		public int Total => m_StatTotal;

		public void Add(int amount)
		{
			m_StatTotal += amount;
		}
	}

	[Serializable]
	public class DoubleStat
	{
		[JsonProperty]
		[SerializeField]
		private double m_StatTotal;

		public double Total => m_StatTotal;

		public void Add(double amount)
		{
			m_StatTotal += amount;
		}
	}

	[Serializable]
	public class IntStatList
	{
		[JsonProperty]
		[SerializeField]
		private int m_StatOverallTotal;

		[NonSerialized]
		private Dictionary<int, int> m_StatPerType;

		[JsonProperty]
		private Dictionary<string, int> m_StatPerTypeSerialized;

		[NonSerialized]
		private Type m_EnumType;

		public int Total => m_StatOverallTotal;

		public IEnumerable<KeyValuePair<int, int>> CollectedStats => m_StatPerType;

		public IntStatList(Type enumType)
		{
			m_EnumType = enumType;
			m_StatOverallTotal = 0;
			int capacity = (int)((float)Enum.GetNames(m_EnumType).Length * 0.5f + 0.5f);
			m_StatPerType = new Dictionary<int, int>(capacity);
		}

		public int GetStatForType(int enumIdx)
		{
			if (!m_StatPerType.TryGetValue(enumIdx, out var value))
			{
				return 0;
			}
			return value;
		}

		public int Add(int enumIdx, int amount)
		{
			int value = 0;
			int num = amount;
			if (m_StatPerType.TryGetValue(enumIdx, out value))
			{
				num += value;
				m_StatPerType[enumIdx] = num;
			}
			else
			{
				m_StatPerType.Add(enumIdx, num);
			}
			m_StatOverallTotal += amount;
			return num;
		}

		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			m_StatPerTypeSerialized = new Dictionary<string, int>(m_StatPerType.Count);
			foreach (KeyValuePair<int, int> item in m_StatPerType)
			{
				if (Enum.IsDefined(m_EnumType, item.Key))
				{
					m_StatPerTypeSerialized.Add(Enum.GetName(m_EnumType, item.Key), item.Value);
				}
				else if (m_EnumType == typeof(BlockTypes) && Singleton.Manager<ManMods>.inst.IsModdedBlock((BlockTypes)item.Key))
				{
					m_StatPerTypeSerialized.Add(Singleton.Manager<ManMods>.inst.FindBlockName(item.Key), item.Value);
				}
				else
				{
					d.LogError($"Trying to save stat data for object of type {m_EnumType} but specified value was outside the expected range. Could this be a modded object?");
				}
			}
		}

		[OnSerialized]
		private void OnSerialized(StreamingContext context)
		{
			m_StatPerTypeSerialized = null;
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (m_StatPerTypeSerialized == null)
			{
				return;
			}
			m_StatPerType = new Dictionary<int, int>(m_StatPerTypeSerialized.Count);
			foreach (KeyValuePair<string, int> item in m_StatPerTypeSerialized)
			{
				string key = item.Key;
				int num = -1;
				if (key != null)
				{
					if (Enum.IsDefined(m_EnumType, key))
					{
						num = (int)Enum.Parse(m_EnumType, key);
					}
					else if (m_EnumType == typeof(BlockTypes))
					{
						int blockID = Singleton.Manager<ManMods>.inst.GetBlockID(key);
						if (Singleton.Manager<ManMods>.inst.IsModdedBlock((BlockTypes)blockID))
						{
							num = blockID;
						}
					}
				}
				if (num != -1)
				{
					m_StatPerType.Add(num, item.Value);
				}
				else
				{
					d.LogError("ManStats.OnPostDeserialize - Failed to find enum match for '" + key);
				}
			}
			m_StatPerTypeSerialized = null;
		}
	}

	[SerializeField]
	private SaveData m_SaveData = new SaveData();

	public int TotalMoneyEarned => m_SaveData.m_MoneyEarned.Total;

	public event IntStatChangeEventWithSubTypeDelegate ResourceHarvestedEvent;

	public event IntStatChangeEventWithSubTypeDelegate ResourceSoldEvent;

	public event IntStatChangeEventWithSubTypeDelegate MoneyFromResourceSaleEvent;

	public event IntStatChangeEventWithSubTypeDelegate ResourceRefinedEvent;

	public event BlockCraftingEventDelegate BlockCraftedEvent;

	public event BlockCraftingEventDelegate BlockScrappedEvent;

	public event IntStatChangeEventWithSubTypeDelegate MoneyFromBlockSaleEvent;

	public event IntStatChangeEventWithSubTypeDelegate BlockInventoryQuantityChangedEvent;

	public event IntStatChangeEventWithSubTypeDelegate SceneryObjectDestroyedEvent;

	public event IntStatChangeEventWithSubTypeDelegate EnemyTechDestroyedEvent;

	public event IntStatChangeEventDelegate InvaderDestroyedEvent;

	private void Start()
	{
		Singleton.Manager<ManGameMode>.inst.ModeSwitchEvent.Subscribe(Clear);
	}

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		if (optionalLoadState != null && optionalLoadState.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManStats, out var saveData) && saveData != null)
		{
			m_SaveData = saveData;
			EnsureIntStatList<BlockTypes>(ref m_SaveData.m_BlocksScavenged);
			EnsureIntStatList<BlockTypes>(ref m_SaveData.m_BlocksSold);
			EnsureIntStatList<BlockTypes>(ref m_SaveData.m_BlocksAttached);
			EnsureIntStat(ref m_SaveData.m_SnapshotsDeployed);
		}
		static void EnsureIntStat(ref IntStat statRef)
		{
			if (statRef == null)
			{
				statRef = new IntStat();
			}
		}
		static void EnsureIntStatList<T>(ref IntStatList listRef)
		{
			if (listRef == null)
			{
				listRef = new IntStatList(typeof(T));
			}
		}
	}

	public void Save(ManSaveGame.State saveState)
	{
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManStats, m_SaveData);
	}

	public void ModeExit()
	{
	}

	public void Clear()
	{
		m_SaveData = new SaveData();
	}

	public int GetTotalNumResourcesHarvested()
	{
		return m_SaveData.m_ResourcesHarvested.Total;
	}

	public int GetNumResourcesHarvested(ChunkTypes resourceType)
	{
		return m_SaveData.m_ResourcesHarvested.GetStatForType((int)resourceType);
	}

	public int GetTotalNumResourcesSold()
	{
		return m_SaveData.m_ResourcesSold.Total;
	}

	public int GetNumResourcesSold(ChunkTypes resourceType)
	{
		return m_SaveData.m_ResourcesSold.GetStatForType((int)resourceType);
	}

	public int GetTotalMoneyFromResourceSales()
	{
		return m_SaveData.m_MoneyFromResourceSales.Total;
	}

	public int GetMoneyFromResourceSales(ChunkTypes resourceType)
	{
		return m_SaveData.m_MoneyFromResourceSales.GetStatForType((int)resourceType);
	}

	public int GetTotalNumResourcesRefined()
	{
		return m_SaveData.m_ResourcesRefined.Total;
	}

	public int GetNumResourcesRefined(ChunkTypes resourceType)
	{
		return m_SaveData.m_ResourcesRefined.GetStatForType((int)resourceType);
	}

	public int GetTotalNumBlocksCrafted()
	{
		return m_SaveData.m_BlocksCrafted.Total;
	}

	public int GetNumBlocksCrafted(BlockTypes blockType)
	{
		return m_SaveData.m_BlocksCrafted.GetStatForType((int)blockType);
	}

	public int GetTotalNumBlocksScrapped()
	{
		return m_SaveData.m_BlocksScrapped.Total;
	}

	public int GetNumBlocksScrapped(BlockTypes blockType)
	{
		return m_SaveData.m_BlocksScrapped.GetStatForType((int)blockType);
	}

	public int GetTotalNumBlocksAttached()
	{
		return m_SaveData.m_BlocksAttached.Total;
	}

	public int GetNumBlocksAttached(BlockTypes blockType)
	{
		return m_SaveData.m_BlocksAttached.GetStatForType((int)blockType);
	}

	public IEnumerable<KeyValuePair<int, int>> AllBlocksAttached()
	{
		return m_SaveData.m_BlocksAttached.CollectedStats;
	}

	public int GetTotalMoneyFromBlockSales()
	{
		return m_SaveData.m_MoneyFromBlockSales.Total;
	}

	public int GetMoneyFromBlockSales(BlockTypes blockType)
	{
		return m_SaveData.m_MoneyFromBlockSales.GetStatForType((int)blockType);
	}

	public int GetNumBlocksInInventory(BlockTypes blockType)
	{
		int result = -1;
		if (!Singleton.Manager<ManPlayer>.inst.InventoryIsUnrestricted)
		{
			result = Singleton.Manager<ManPlayer>.inst.PlayerInventory.GetQuantity(blockType);
		}
		return result;
	}

	public int GetNumBlocksPurchased(BlockTypes blockType)
	{
		return m_SaveData.m_BlocksPurchased.GetStatForType((int)blockType);
	}

	public int GetTotalNumSceneryObjectsDestroyed()
	{
		return m_SaveData.m_ResourceGiversDestroyed.Total;
	}

	public int GetNumSceneryObjectsDestroyed(SceneryTypes sceneryType)
	{
		return m_SaveData.m_ResourceGiversDestroyed.GetStatForType((int)sceneryType);
	}

	public int GetTotalNumEnemyTechsDestroyed()
	{
		return m_SaveData.m_EnemyTechsDestroyed.Total;
	}

	public int GetNumEnemyTechsDestroyedByFaction(FactionSubTypes faction)
	{
		return m_SaveData.m_EnemyTechsDestroyed.GetStatForType((int)faction);
	}

	public int GetTotalNumInvadersDestroyed()
	{
		return m_SaveData.m_InvadersDestroyed.Total;
	}

	public void ResourceHarvested(TankBlock harvester, ChunkTypes resourceType)
	{
		int newSubTypeAmount = m_SaveData.m_ResourcesHarvested.Add((int)resourceType, 1);
		if (this.ResourceHarvestedEvent != null)
		{
			this.ResourceHarvestedEvent((int)resourceType, newSubTypeAmount, m_SaveData.m_ResourcesHarvested.Total);
		}
	}

	public void ItemSold(TankBlock sellingBlock, ItemTypeInfo soldItem, int salePrice)
	{
		switch (soldItem.ObjectType)
		{
		case ObjectTypes.Chunk:
		{
			int itemType2 = soldItem.ItemType;
			int newSubTypeAmount2 = m_SaveData.m_ResourcesSold.Add(itemType2, 1);
			int newSubTypeAmount3 = m_SaveData.m_MoneyFromResourceSales.Add(itemType2, salePrice);
			if (this.ResourceSoldEvent != null)
			{
				this.ResourceSoldEvent(itemType2, newSubTypeAmount2, m_SaveData.m_ResourcesSold.Total);
			}
			if (this.MoneyFromResourceSaleEvent != null)
			{
				this.MoneyFromResourceSaleEvent(itemType2, newSubTypeAmount3, m_SaveData.m_MoneyFromResourceSales.Total);
			}
			break;
		}
		case ObjectTypes.Block:
		{
			int itemType = soldItem.ItemType;
			m_SaveData.m_BlocksSold.Add(itemType, 1);
			int newSubTypeAmount = m_SaveData.m_MoneyFromBlockSales.Add(itemType, salePrice);
			if (this.MoneyFromBlockSaleEvent != null)
			{
				this.MoneyFromBlockSaleEvent(itemType, newSubTypeAmount, m_SaveData.m_MoneyFromBlockSales.Total);
			}
			break;
		}
		default:
			d.LogError("ManStats.ItemSold - Unexpected item type ('" + soldItem.ObjectType.ToString() + "') encountered, we don't support handling of this type yet!");
			break;
		}
	}

	public void ItemProduced(TankBlock producingBlock, Visible productVis)
	{
		ItemTypeInfo itemType = productVis.m_ItemType;
		switch (itemType.ObjectType)
		{
		case ObjectTypes.Chunk:
		{
			int itemType3 = itemType.ItemType;
			int newSubTypeAmount = m_SaveData.m_ResourcesRefined.Add(itemType3, 1);
			if (this.ResourceRefinedEvent != null)
			{
				this.ResourceRefinedEvent(itemType3, newSubTypeAmount, m_SaveData.m_ResourcesRefined.Total);
			}
			break;
		}
		case ObjectTypes.Block:
		{
			int itemType2 = itemType.ItemType;
			int blockTypeTotal = m_SaveData.m_BlocksCrafted.Add(itemType2, 1);
			this.BlockCraftedEvent?.Invoke(producingBlock, productVis.block, itemType2, blockTypeTotal, m_SaveData.m_BlocksCrafted.Total);
			break;
		}
		default:
			d.LogError("ManStats.ItemProduced - Unexpected item type ('" + itemType.ObjectType.ToString() + "') encountered, we don't support handling of this type yet!");
			break;
		}
	}

	public void ItemConsumed(TankBlock producingBlock, Visible consumedItemVisible)
	{
		ItemTypeInfo itemType = consumedItemVisible.m_ItemType;
		switch (itemType.ObjectType)
		{
		case ObjectTypes.Block:
		{
			int itemType2 = itemType.ItemType;
			int blockTypeTotal = m_SaveData.m_BlocksScrapped.Add(itemType2, 1);
			this.BlockScrappedEvent?.Invoke(producingBlock, consumedItemVisible.block, itemType2, blockTypeTotal, m_SaveData.m_BlocksScrapped.Total);
			break;
		}
		default:
			d.LogError("ManStats.ItemConsumed - Unexpected item type ('" + itemType.ObjectType.ToString() + "') encountered, we don't support handling of this type yet!");
			break;
		case ObjectTypes.Chunk:
			break;
		}
	}

	public void InventoryQuantityUpdated(BlockTypes blockType, int quantityOfType)
	{
		if (this.BlockInventoryQuantityChangedEvent != null)
		{
			this.BlockInventoryQuantityChangedEvent((int)blockType, quantityOfType, 0);
		}
	}

	public void BlockPurchased(BlockTypes purchasedBlockType)
	{
		m_SaveData.m_BlocksPurchased.Add((int)purchasedBlockType, 1);
	}

	public void BlockScavenged(BlockTypes blockType)
	{
		m_SaveData.m_BlocksScavenged.Add((int)blockType, 1);
	}

	public void BlockAttached(BlockTypes blockType)
	{
		m_SaveData.m_BlocksAttached.Add((int)blockType, 1);
	}

	public void MoneyEarned(int moneyAmount)
	{
		m_SaveData.m_MoneyEarned.Add(moneyAmount);
	}

	public void MoneySpent(int moneyAmount)
	{
		m_SaveData.m_MoneySpent.Add(moneyAmount);
	}

	public void EnergyGenerated(TankBlock energyGenerator, float amount)
	{
		m_SaveData.m_EnergyGenerated.Add(amount);
	}

	public void SnapshotDeployed(TechData techData)
	{
		m_SaveData.m_SnapshotsDeployed.Add(1);
	}

	public void PlayerTechDestroyed(Tank tank, ManDamage.DamageInfo killingBlow)
	{
		m_SaveData.m_PlayerTechsDestroyed.Add(1);
	}

	public void EnemyTechDestroyed(Tank tank, FactionSubTypes techAffiliation, ManDamage.DamageInfo killingBlow)
	{
		int newSubTypeAmount = m_SaveData.m_EnemyTechsDestroyed.Add((int)techAffiliation, 1);
		if (this.EnemyTechDestroyedEvent != null)
		{
			this.EnemyTechDestroyedEvent((int)techAffiliation, newSubTypeAmount, m_SaveData.m_EnemyTechsDestroyed.Total);
		}
	}

	public void InvaderDestroyed(ManInvasion.Invader invader, ManDamage.DamageInfo killingBlow)
	{
		m_SaveData.m_InvadersDestroyed.Add(1);
		if (this.InvaderDestroyedEvent != null)
		{
			this.InvaderDestroyedEvent(m_SaveData.m_InvadersDestroyed.Total);
		}
	}

	public void ResourceGiverDestroyed(ResourceDispenser dispenser, ManDamage.DamageInfo killingBlow)
	{
		d.AssertFormat(dispenser.visible.type == ObjectTypes.Scenery, "ResourceGiverDestroyed called with non-scenery object! Got {0}", dispenser.visible.type);
		int itemType = dispenser.visible.ItemType;
		int newSubTypeAmount = m_SaveData.m_ResourceGiversDestroyed.Add(itemType, 1);
		if (this.SceneryObjectDestroyedEvent != null)
		{
			this.SceneryObjectDestroyedEvent(itemType, newSubTypeAmount, m_SaveData.m_ResourceGiversDestroyed.Total);
		}
	}

	public void PlayerBlockHealed(ModuleShieldGenerator healer, Visible blockReceivingHeal, float healAmount)
	{
		m_SaveData.m_PlayerBlockHealAmount.Add(healAmount);
	}

	public void DamageAbsorbedByShield(ModuleShieldGenerator shield, ManDamage.DamageInfo damageInfo)
	{
		m_SaveData.m_DamageAbsorbedByShields.Add(damageInfo.Damage);
	}
}
