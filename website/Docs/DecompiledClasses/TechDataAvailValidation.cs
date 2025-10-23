#define UNITY_EDITOR
using System.Collections.Generic;
using Binding;
using UnityEngine;

public class TechDataAvailValidation
{
	public class BlockTypeAvailability
	{
		public int numRequired;

		public BlockAvailableState availability;

		public int numInInventory;

		public int numOnPlayerTech;
	}

	public enum BlockAvailableState
	{
		NotSet,
		NotAvailableInGame,
		NotAvailableInMode,
		NotAvailableInProgression,
		InsufficientQuantityForSwap,
		InsufficientQuantityForPlace,
		Available
	}

	public Bindable<bool> m_UnavailableSwap = new Bindable<bool>(value: false);

	public Bindable<bool> m_UnavailablePlace = new Bindable<bool>(value: false);

	private bool m_HasBeenValidated;

	private bool m_AvailableInGame;

	private BlockAvailableState m_BlockAvailabilityOverall;

	private Dictionary<BlockTypes, BlockTypeAvailability> m_BlockAvailability = new Dictionary<BlockTypes, BlockTypeAvailability>(new BlockTypesComparer());

	private Dictionary<BlockTypes, bool> m_RootBlocks = new Dictionary<BlockTypes, bool>(new BlockTypesComparer());

	public TechData m_Tech { get; private set; }

	public int m_BlockBBCost { get; private set; }

	public int m_LimiterCost { get; private set; }

	public bool m_ExceedsBlockLimitPlace { get; private set; }

	public bool m_ExceedsBlockLimitSwap { get; private set; }

	public bool m_ExceedsBuildSizeLimit { get; private set; }

	public bool m_HasPlayerCab { get; private set; }

	public bool HasMissingBlocksSwap
	{
		get
		{
			d.Assert(m_HasBeenValidated, "TechDataAvailValidation.HasMissingBlocksSwap was called before the data had been validated! Please run UpdateAvailability before accessing any of the validation data!");
			return m_BlockAvailabilityOverall <= BlockAvailableState.InsufficientQuantityForSwap;
		}
	}

	public bool HasMissingBlocksPlace
	{
		get
		{
			d.Assert(m_HasBeenValidated, "TechDataAvailValidation.HasMissingBlocksPlace was called before the data had been validated! Please run UpdateAvailability before accessing any of the validation data!");
			return m_BlockAvailabilityOverall < BlockAvailableState.Available;
		}
	}

	public bool UnavailableSwap
	{
		get
		{
			d.Assert(m_HasBeenValidated, "TechDataAvailValidation.UnavailableSwap was called before the data had been validated! Please run UpdateAvailability before accessing any of the validation data!");
			return m_UnavailableSwap.Value;
		}
	}

	public bool UnavailablePlace
	{
		get
		{
			d.Assert(m_HasBeenValidated, "TechDataAvailValidation.UnavailablePlace was called before the data had been validated! Please run UpdateAvailability before accessing any of the validation data!");
			return m_UnavailablePlace.Value;
		}
	}

	public Dictionary<BlockTypes, BlockTypeAvailability> BlockAvailability
	{
		get
		{
			d.Assert(m_HasBeenValidated, "TechDataAvailValidation.BlockAvailability was called before the data had been validated! Please run UpdateAvailability before accessing any of the validation data!");
			return m_BlockAvailability;
		}
	}

	public void RecordBlockDataAndValidate(TechData tech, InventoryMetaData inventoryData, Dictionary<BlockTypes, int> playerBlockData = null)
	{
		RecordBlockData(tech);
		UpdateAvailability(inventoryData, playerBlockData);
	}

	public void RecordBlockData(TechData tech)
	{
		m_Tech = tech;
		m_BlockAvailability.Clear();
		m_AvailableInGame = true;
		m_HasPlayerCab = false;
		m_HasBeenValidated = false;
		m_UnavailableSwap.Value = false;
		m_UnavailablePlace.Value = false;
		for (int i = 0; i < m_Tech.m_BlockSpecs.Count; i++)
		{
			TankPreset.BlockSpec blockSpec = m_Tech.m_BlockSpecs[i];
			BlockTypes blockType = blockSpec.GetBlockType();
			if (!m_BlockAvailability.TryGetValue(blockType, out var value))
			{
				value = new BlockTypeAvailability
				{
					availability = BlockAvailableState.NotSet,
					numRequired = 0
				};
				if (!Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInLaunchedConfig(blockType))
				{
					value.availability = BlockAvailableState.NotAvailableInGame;
					m_AvailableInGame = false;
				}
				bool flag = Singleton.Manager<ManSpawn>.inst.HasBlockDescriptorEnum(blockType, typeof(BlockAttributes), 12);
				bool flag2 = Singleton.Manager<ManSpawn>.inst.HasBlockDescriptorEnum(blockType, typeof(BlockAttributes), 8);
				bool flag3 = Singleton.Manager<ManSpawn>.inst.HasBlockDescriptorEnum(blockType, typeof(BlockAttributes), 0) && blockSpec.CheckIsAnchored();
				m_HasPlayerCab |= flag;
				if (flag || flag2 || flag3)
				{
					m_RootBlocks[blockType] = flag;
				}
				m_BlockAvailability.Add(blockType, value);
			}
			value.numRequired++;
		}
		m_BlockBBCost = Singleton.Manager<RecipeManager>.inst.GetTechPrice(tech, silentFail: true);
		m_LimiterCost = Singleton.Manager<ManBlockLimiter>.inst.GetTechCost(tech, out var _);
		Vector3 vector = m_Tech.m_BoundsExtents * 2f;
		int blockLimit = Singleton.Manager<ManSpawn>.inst.BlockLimit;
		m_ExceedsBuildSizeLimit = vector.x > (float)blockLimit || vector.y > (float)blockLimit || vector.z > (float)blockLimit;
	}

	public void UpdateAvailability(InventoryMetaData inventoryData, Dictionary<BlockTypes, int> playerTechBlocks)
	{
		m_BlockAvailabilityOverall = BlockAvailableState.Available;
		int num = 0;
		bool flag = false;
		bool flag2 = false;
		foreach (KeyValuePair<BlockTypes, BlockTypeAvailability> item in m_BlockAvailability)
		{
			BlockTypes key = item.Key;
			BlockTypeAvailability value = item.Value;
			int inventoryBlockCount = inventoryData.GetInventoryBlockCount(key);
			int num2 = ((inventoryBlockCount == -1) ? value.numRequired : inventoryBlockCount);
			int value2 = 0;
			playerTechBlocks?.TryGetValue(key, out value2);
			value.numInInventory = inventoryBlockCount;
			value.numOnPlayerTech = value2;
			if (value.availability == BlockAvailableState.NotSet && (!Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInCurrentGameMode(key) || Singleton.Manager<ManSpawn>.inst.IsBlockUsageRestrictedInGameMode(key)))
			{
				value.availability = BlockAvailableState.NotAvailableInMode;
			}
			if (value.availability == BlockAvailableState.NotSet || value.availability > BlockAvailableState.NotAvailableInMode)
			{
				if (!FilterDiscovered(key))
				{
					value.availability = BlockAvailableState.NotAvailableInProgression;
				}
				else
				{
					if (value.numRequired > num2 + value2)
					{
						value.availability = BlockAvailableState.InsufficientQuantityForSwap;
					}
					else if (value.numRequired > num2)
					{
						value.availability = BlockAvailableState.InsufficientQuantityForPlace;
						num += num2;
					}
					else
					{
						value.availability = BlockAvailableState.Available;
						num += value.numRequired;
					}
					if (m_RootBlocks.TryGetValue(key, out var value3))
					{
						flag = flag || (value3 && num2 + value2 > 0);
						flag2 = flag2 || num2 > 0;
					}
				}
			}
			if (value.availability < m_BlockAvailabilityOverall)
			{
				m_BlockAvailabilityOverall = value.availability;
			}
		}
		if (inventoryData.IsLocked && m_BlockAvailabilityOverall > BlockAvailableState.NotAvailableInProgression)
		{
			m_BlockAvailabilityOverall = BlockAvailableState.NotAvailableInProgression;
			num = 0;
		}
		int replacedTechCost = 0;
		if (Singleton.playerTank != null && Singleton.Manager<ManBlockLimiter>.inst.LimiterActive)
		{
			replacedTechCost = Singleton.Manager<ManBlockLimiter>.inst.GetTrackedTechCost(Singleton.playerTank, includeHeldItems: true);
		}
		m_ExceedsBlockLimitSwap = !Singleton.Manager<ManBlockLimiter>.inst.AllowCreatePlayerTech(m_LimiterCost, replacedTechCost);
		m_ExceedsBlockLimitPlace = !Singleton.Manager<ManBlockLimiter>.inst.AllowCreatePlayerTech(m_LimiterCost);
		m_HasBeenValidated = true;
		m_UnavailableSwap.Value = !m_AvailableInGame || m_ExceedsBlockLimitSwap || m_ExceedsBuildSizeLimit || HasMissingBlocksSwap || !m_HasPlayerCab || !flag;
		m_UnavailablePlace.Value = m_ExceedsBlockLimitPlace || m_ExceedsBuildSizeLimit || num == 0 || !flag2 || (!Globals.inst.m_AllowPlaceTechWithMissingBlocks && (!m_AvailableInGame || HasMissingBlocksPlace));
		if (Singleton.Manager<DebugUtil>.inst.RemoveTechLoaderRestrictions || Singleton.Manager<DebugUtil>.inst.AllBlocksInInventory)
		{
			m_UnavailableSwap.Value = false;
			m_UnavailablePlace.Value = false;
		}
	}

	public void ResetModeAvailability()
	{
		foreach (BlockTypeAvailability value in m_BlockAvailability.Values)
		{
			if (value.availability == BlockAvailableState.NotAvailableInMode)
			{
				value.availability = BlockAvailableState.NotSet;
			}
		}
		m_UnavailableSwap.Value = false;
		m_UnavailablePlace.Value = false;
		m_HasBeenValidated = false;
	}

	private bool FilterDiscovered(BlockTypes blockType)
	{
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() != ManGameMode.GameType.MainGame)
		{
			return true;
		}
		if (Singleton.Manager<ManLicenses>.inst.GetBlockState(blockType) == ManLicenses.BlockState.Discovered)
		{
			return true;
		}
		return false;
	}
}
