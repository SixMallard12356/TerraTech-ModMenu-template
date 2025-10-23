#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ManPurchases : Singleton.Manager<ManPurchases>, Mode.IManagerModeEvents
{
	private enum AutoTechOperation
	{
		Load,
		Store,
		Hotswap
	}

	[Serializable]
	public class OrderedBlockList
	{
		public List<BlockTypes> m_Blocks = new List<BlockTypes>();
	}

	[SerializeField]
	private List<FactionSubTypes> m_AvailableCorporations;

	[SerializeField]
	private float m_ShopHideDist = 30f;

	private bool m_ShopShowing;

	private ModuleShop m_LocallyOpenedShopModule;

	private IInventory<BlockTypes> m_BlockPaletteInventory;

	private UndoSendTechToInventory m_UndoCommand;

	private List<AutoTechOperation> m_AutoTechOperations = new List<AutoTechOperation>();

	public Event<int> OnNeedUpdate;

	public Event<TankBlock> OnBlockPurchased;

	public Event<BlockTypes, int> OnInventoryChanged;

	public Event<Tank> StoreTechToInventoryEvent;

	private List<BlockTypes> m_RestrictedBlocksAllowed = new List<BlockTypes>();

	public List<FactionSubTypes> AvailableCorporations => m_AvailableCorporations;

	public float ShopHideDistance => m_ShopHideDist;

	public bool IsLoadingTechs => m_AutoTechOperations.Contains(AutoTechOperation.Load);

	public bool IsStoringTechs => m_AutoTechOperations.Contains(AutoTechOperation.Store);

	public bool IsHotswappingTechs => m_AutoTechOperations.Contains(AutoTechOperation.Hotswap);

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		UIPaletteBlockSelect uIPaletteBlockSelect = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.BlockPalette) as UIPaletteBlockSelect;
		if (uIPaletteBlockSelect != null)
		{
			uIPaletteBlockSelect.OnModeChange();
		}
		UIShopBlockSelect uIShopBlockSelect = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.BlockShop) as UIShopBlockSelect;
		if (uIShopBlockSelect != null)
		{
			uIShopBlockSelect.OnModeChange();
		}
	}

	public void Save(ManSaveGame.State saveState)
	{
	}

	public void ModeExit()
	{
	}

	public void AddCustomCorp(int corpIndex)
	{
		m_AvailableCorporations.Add((FactionSubTypes)corpIndex);
	}

	public void ClearCustomCorps()
	{
		for (int num = m_AvailableCorporations.Count - 1; num >= 0; num--)
		{
			if (Singleton.Manager<ManMods>.inst.IsModdedCorp(m_AvailableCorporations[num]))
			{
				m_AvailableCorporations.RemoveAt(num);
			}
		}
	}

	public void Clear()
	{
		m_LocallyOpenedShopModule = null;
		m_ShopShowing = false;
	}

	public void ShowShop(ModuleShop shopModule, IInventory<BlockTypes> inventory = null, FactionSubTypes corpToShow = FactionSubTypes.NULL)
	{
		d.Assert(shopModule.IsNotNull(), "Trying to show shop for an invalid module");
		m_LocallyOpenedShopModule = shopModule;
		m_ShopShowing = true;
		UIBlockMenuSelection.Context context = new UIBlockMenuSelection.Context
		{
			showBlockContext = new UIShopBlockSelect.ShowContext
			{
				singleCorpToShow = corpToShow,
				inventory = inventory,
				inventorySpecified = true,
				shopBlockPoolID = shopModule.block.blockPoolID
			},
			targetMode = UIBlockMenuSelection.ModeMask.BlockShop
		};
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.BlockMenuSelection, context);
	}

	public void HideShop()
	{
		m_LocallyOpenedShopModule = null;
		m_ShopShowing = false;
		UIBlockMenuSelection.Context context = new UIBlockMenuSelection.Context
		{
			targetMode = UIBlockMenuSelection.ModeMask.BlockShop
		};
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.BlockMenuSelection, context);
	}

	public bool IsPaletteAvailable()
	{
		return Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.BlockPalette);
	}

	public bool IsPaletteExpanded()
	{
		return Singleton.Manager<ManHUD>.inst.IsHudElementExpanded(ManHUD.HUDElementType.BlockPalette);
	}

	public void ExpandPalette(bool expand, UIShopBlockSelect.ExpandReason reason, bool forceClose = false)
	{
		if (!IsPaletteAvailable())
		{
			return;
		}
		UIShopBlockSelect.ExpandContext context = new UIShopBlockSelect.ExpandContext
		{
			expandReason = reason,
			forceClose = forceClose
		};
		if (expand)
		{
			Singleton.Manager<ManHUD>.inst.ExpandHudElement(ManHUD.HUDElementType.BlockPalette, context);
			if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled() && Singleton.Manager<ManPointer>.inst.IsInteractionModeEnabled)
			{
				Singleton.Manager<ManPointer>.inst.EnableInteractionMode(enable: false);
			}
		}
		else
		{
			Singleton.Manager<ManHUD>.inst.CollapseHudElement(ManHUD.HUDElementType.BlockPalette, context);
		}
	}

	public void TogglePalette()
	{
		bool expand = !IsPaletteExpanded();
		ExpandPalette(expand, UIShopBlockSelect.ExpandReason.Button, forceClose: true);
	}

	public IInventory<BlockTypes> GetInventory()
	{
		if (Singleton.Manager<DebugUtil>.inst != null && (Singleton.Manager<DebugUtil>.inst.UnlimitedShopBlocks || Singleton.Manager<DebugUtil>.inst.AllBlocksInInventory))
		{
			return null;
		}
		return m_BlockPaletteInventory;
	}

	public void SetInventory(IInventory<BlockTypes> inventory)
	{
		if (m_BlockPaletteInventory != null)
		{
			m_BlockPaletteInventory.UnsubscribeToInventoryChanged(OnInventoryChangedListener);
		}
		m_BlockPaletteInventory = inventory;
		if (m_BlockPaletteInventory != null)
		{
			m_BlockPaletteInventory.SubscribeToInventoryChanged(OnInventoryChangedListener);
		}
	}

	private void OnHotswapOperationComplete()
	{
		m_AutoTechOperations.Remove(AutoTechOperation.Hotswap);
	}

	private void OnLoadOperationComplete()
	{
		m_AutoTechOperations.Remove(AutoTechOperation.Load);
	}

	private void OnStoreOperationComplete()
	{
		m_AutoTechOperations.Remove(AutoTechOperation.Store);
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && m_UndoCommand != null)
		{
			Singleton.Manager<ManUndo>.inst.AddCommand(m_UndoCommand);
			m_UndoCommand = null;
		}
	}

	public void LoadTechFromInventoryAtPosition(TechData techData, Vector3 position, Quaternion rotation)
	{
		ManTechSwapper.Operation newOperation = Singleton.Manager<ManTechSwapper>.inst.GetNewOperation();
		techData.ValidateBlockSkins();
		newOperation.InitSpawnTech(position, rotation, techData, Singleton.Manager<ManGameMode>.inst.GetReferenceInventory());
		newOperation.SubscribeToCompletionCallback(OnLoadOperationComplete);
		m_AutoTechOperations.Add(AutoTechOperation.Load);
		Singleton.Manager<ManStats>.inst.SnapshotDeployed(techData);
	}

	public void HotswapTechs(Tank currentTech, TechData targetTechData)
	{
		if (!Singleton.Manager<ManTechSwapper>.inst.CheckOperationInProgress())
		{
			if (currentTech == null || targetTechData == null)
			{
				d.LogErrorFormat("ManPurchases.HotswapTech trying to hotswap while player tech [{0}] or target tech [{1}] is null", currentTech, targetTechData);
				return;
			}
			ManTechSwapper.Operation newOperation = Singleton.Manager<ManTechSwapper>.inst.GetNewOperation();
			targetTechData.ValidateBlockSkins();
			newOperation.InitSwapTech(currentTech, targetTechData, Singleton.Manager<ManGameMode>.inst.GetReferenceInventory());
			newOperation.SubscribeToCompletionCallback(OnHotswapOperationComplete);
			m_AutoTechOperations.Add(AutoTechOperation.Hotswap);
			Singleton.Manager<ManStats>.inst.SnapshotDeployed(targetTechData);
		}
	}

	public void StoreTechToInventory(TrackedVisible tv, bool supportUndo)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.UnspawnTech, new UnspawnTechMessage
			{
				m_HostID = tv.HostID
			});
			return;
		}
		if (supportUndo)
		{
			d.Assert(m_UndoCommand == null);
			m_UndoCommand = Singleton.Manager<ManUndo>.inst.GetUndoSendTechToInventory();
		}
		Tank tank = (tv.visible.IsNotNull() ? tv.visible.tank : null);
		TechData techData;
		Quaternion rotation;
		if (tank.IsNotNull())
		{
			techData = new TechData();
			techData.SaveTech(tank, saveRuntimeState: true);
			tank.visible.StopManagingVisible();
			StoreTechToInventoryEvent.Send(tank);
			if (supportUndo)
			{
				m_UndoCommand.Initialize(techData, tank);
			}
		}
		else if (Singleton.Manager<ManVisible>.inst.TryGetStoredTechData(tv, out techData, out rotation))
		{
			if (supportUndo)
			{
				bool isAnchored = techData.CheckIsAnchored();
				Bounds bounds = techData.CalculateBlockBounds();
				Vector3 scenePos = tv.Position + rotation * bounds.center;
				m_UndoCommand.Initialize(techData, scenePos, rotation, isAnchored);
			}
		}
		else
		{
			d.LogError("Send to SCU but the tech requested wasn't there!?");
			m_UndoCommand = null;
		}
		InventoryMetaData referenceInventory = Singleton.Manager<ManGameMode>.inst.GetReferenceInventory();
		if (referenceInventory.TakesAndStoresBlocks && techData != null)
		{
			referenceInventory.m_Inventory.HostStoreTech(techData);
		}
		m_AutoTechOperations.Add(AutoTechOperation.Store);
		if (tank.IsNotNull())
		{
			ManTechSwapper.Operation newOperation = Singleton.Manager<ManTechSwapper>.inst.GetNewOperation();
			newOperation.InitDespawnTech(tank, InventoryMetaData.kUnrestrictedIntenvory);
			newOperation.SubscribeToCompletionCallback(OnStoreOperationComplete);
		}
		else
		{
			Singleton.Manager<ManVisible>.inst.ObliterateTrackedVisibleFromWorld(tv.ID);
			OnStoreOperationComplete();
		}
	}

	public void ShowPalette(bool show)
	{
		UIBlockMenuSelection.ModeMask targetMode = UIBlockMenuSelection.ModeMask.BlockPaletteAndTechs;
		ManGameMode.GameType currentGameType = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType();
		if (currentGameType == ManGameMode.GameType.SumoShowdown || currentGameType == ManGameMode.GameType.Gauntlet)
		{
			targetMode = UIBlockMenuSelection.ModeMask.BlockPalette;
		}
		UIBlockMenuSelection.Context context = new UIBlockMenuSelection.Context
		{
			targetMode = targetMode
		};
		if (show)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.BlockMenuSelection, context);
		}
		else
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.BlockMenuSelection, context);
		}
	}

	public void RequestPurchaseBlock(uint shopBlockPoolID, BlockTypes blockType, int count = 1)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.BuyBlock, new BuyBlockMessage
			{
				m_BlockType = blockType,
				m_Count = count,
				m_ShopBlockPoolID = shopBlockPoolID
			});
		}
		else
		{
			DoPurchaseBlock(shopBlockPoolID, blockType);
		}
	}

	private void OnServerBuyBlockMessage(NetworkMessage netMsg)
	{
		BuyBlockMessage buyBlockMessage = netMsg.ReadMessage<BuyBlockMessage>();
		DoPurchaseBlock(buyBlockMessage.m_ShopBlockPoolID, buyBlockMessage.m_BlockType, buyBlockMessage.m_Count);
	}

	public void DoPurchaseBlock(uint shopBlockPoolID, BlockTypes blockType, int count = 1)
	{
		d.Assert(ManNetwork.IsHost, "Can't purchase a block on a client");
		TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(shopBlockPoolID);
		if (tankBlock.IsNotNull())
		{
			ModuleShop component = tankBlock.GetComponent<ModuleShop>();
			if (component.IsNotNull())
			{
				for (int i = 0; i < count; i++)
				{
					d.Assert(component.PurchaseBlock(blockType), "DoPurchaseBlock - Shop would not sell us a " + blockType);
				}
			}
			else
			{
				d.LogError("DoPurchaseBlock - We found the block the player is trying to buy from, but its not a shop");
			}
		}
		else
		{
			d.LogError("DoPurchaseBlock - We could not find the block the player is trying to buy from");
		}
	}

	public void TrySpawnPurchase(TechData techData, WorldPosition spawnPos, ObjectSpawner spawner, ManSpawn.SpawnVisualType spawnVisualType = ManSpawn.SpawnVisualType.Bomb, bool takeFromInventory = false)
	{
		float num = 0f;
		num = ((techData.m_BoundsExtents.sqrMagnitude != 0f) ? Mathf.Sqrt(techData.m_BoundsExtents.sqrMagnitude) : Mathf.Sqrt(techData.m_BlockSpecs.Max((TankPreset.BlockSpec x) => Extensions.SetY(x.position, 0f).sqrMagnitude)));
		ManFreeSpace.FreeSpaceParams freeSpaceParams = new ManFreeSpace.FreeSpaceParams
		{
			m_ObjectsToAvoid = ManSpawn.AvoidSceneryVehiclesCrates,
			m_CircleRadius = num,
			m_CenterPosWorld = spawnPos,
			m_CircleIndex = 0,
			m_CameraSpawnConditions = ManSpawn.CameraSpawnConditions.Anywhere,
			m_CheckSafeArea = false,
			m_RejectFunc = null
		};
		ManSpawn.TechSpawnParams objectSpawnParams = new ManSpawn.TechSpawnParams
		{
			m_TechToSpawn = techData,
			m_Team = 0,
			m_Rotation = Quaternion.identity,
			m_Grounded = true,
			m_SpawnVisualType = spawnVisualType,
			m_TakeBlocksFromPlayerInventory = takeFromInventory
		};
		bool autoRetry = false;
		spawner.TrySpawn(objectSpawnParams, freeSpaceParams, null, techData.Name, autoRetry);
	}

	private void Init()
	{
	}

	public void RestrictBlockPurchase(BlockTypes[] blocksAllowed)
	{
		if (blocksAllowed == null)
		{
			return;
		}
		for (int i = 0; i < blocksAllowed.Length; i++)
		{
			if (!m_RestrictedBlocksAllowed.Contains(blocksAllowed[i]))
			{
				m_RestrictedBlocksAllowed.Add(blocksAllowed[i]);
			}
		}
	}

	public void ClearRestrictedBlocks()
	{
		m_RestrictedBlocksAllowed.Clear();
	}

	public bool IsBlockRestricted(BlockTypes block)
	{
		bool result = true;
		if (m_RestrictedBlocksAllowed.Count == 0 || m_RestrictedBlocksAllowed.Contains(block))
		{
			result = false;
		}
		return result;
	}

	private void OnInventoryChangedListener(BlockTypes blockType, int quanity)
	{
		OnInventoryChanged.Send(blockType, quanity);
	}

	private void Start()
	{
		Singleton.DoOnceAfterStart(Init);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.BuyBlock, OnServerBuyBlockMessage);
	}

	private void Update()
	{
		if (m_ShopShowing && (bool)m_LocallyOpenedShopModule)
		{
			bool flag = false;
			if (m_LocallyOpenedShopModule.block.tank == null || Singleton.playerTank == null)
			{
				flag = true;
			}
			else if (!m_LocallyOpenedShopModule.block.tank.IsAnchored)
			{
				flag = true;
			}
			else if ((Singleton.playerPos - m_LocallyOpenedShopModule.block.tank.boundsCentreWorld).SetY(0f).sqrMagnitude > m_ShopHideDist * m_ShopHideDist)
			{
				flag = true;
			}
			if (flag)
			{
				HideShop();
			}
		}
	}
}
