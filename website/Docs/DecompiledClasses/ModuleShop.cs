using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleShop : Module, ManPointer.OpenMenuEventConsumer
{
	private new class SerialData : SerialData<SerialData>
	{
		public WarningHolder warning;
	}

	[SerializeField]
	private bool m_CanLaunchByDefault = true;

	[SerializeField]
	private FactionSubTypes m_SingleCorpToShow;

	[SerializeField]
	private BlockEjector m_Ejector;

	[SerializeField]
	private Transform m_EjectorLocator;

	public Event<Module> MouseInteractEvent;

	private WarningHolder m_Warning;

	private bool m_CanLaunch;

	private ModuleAnimator m_Animator;

	private AnimatorBool m_ReadyBool = new AnimatorBool("Ready");

	private ModuleShopInventory m_ShopInventory;

	public bool CanLaunch
	{
		set
		{
			m_CanLaunch = value;
		}
	}

	public bool CanOpenMenu(bool isRadial)
	{
		bool result = false;
		if (!isRadial)
		{
			result = m_CanLaunch && base.block.tank != null && !base.block.tank.IsEnemy() && base.block.tank.IsAnchored;
		}
		return result;
	}

	public bool OnOpenMenuEvent(OpenMenuEventData openMenu)
	{
		if (openMenu.m_AllowNonRadialMenu && !base.block.tank.IsEnemy())
		{
			MouseInteractEvent.Send(this);
			if (m_CanLaunch)
			{
				if (base.block.tank != null && base.block.tank.IsAnchored)
				{
					Singleton.Manager<ManPurchases>.inst.ShowShop(this, m_ShopInventory.IsNotNull() ? m_ShopInventory.GetInventory() : null, m_SingleCorpToShow);
					return true;
				}
				m_Warning.TryRegisterWarning(LocalisationEnums.Warnings.warningTitleGeneratorsNeedAnchors, LocalisationEnums.Warnings.warningMsgGeneratorsNeedAnchors, 8);
				return true;
			}
		}
		return false;
	}

	public bool PurchaseBlock(BlockTypes blockType)
	{
		IInventory<BlockTypes> inventory = (m_ShopInventory.IsNotNull() ? m_ShopInventory.GetInventory() : null);
		int blockBuyPrice = Singleton.Manager<RecipeManager>.inst.GetBlockBuyPrice(blockType);
		bool flag = inventory?.CanConsumeItem(-1, blockType) ?? true;
		if (flag && Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCampaign() && Singleton.Manager<ManPlayer>.inst.GetCurrentMoney() < blockBuyPrice)
		{
			flag = false;
		}
		if ((flag && (!Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInLaunchedConfig(blockType) || !Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInCurrentGameMode(blockType))) || Singleton.Manager<ManSpawn>.inst.IsBlockUsageRestrictedInGameMode(blockType))
		{
			flag = false;
		}
		if (flag)
		{
			TankBlock paramA = EjectPurchasedBlock(blockType);
			inventory?.HostConsumeItem(-1, blockType);
			Singleton.Manager<ManPlayer>.inst.PayMoney(blockBuyPrice);
			Singleton.Manager<ManStats>.inst.BlockPurchased(blockType);
			Singleton.Manager<ManPurchases>.inst.OnBlockPurchased.Send(paramA);
		}
		return flag;
	}

	public TankBlock EjectPurchasedBlock(BlockTypes blockType)
	{
		TankBlock tankBlock = null;
		if (m_EjectorLocator != null)
		{
			tankBlock = m_Ejector.Eject(m_EjectorLocator, blockType);
		}
		if (tankBlock.IsNull())
		{
			Vector3 pos = base.block.trans.position + Vector3.up * 10f + base.block.trans.forward * 10f;
			tankBlock = Singleton.Manager<ManLooseBlocks>.inst.HostSpawnBlock(blockType, pos, Quaternion.identity);
		}
		return tankBlock;
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.warning = m_Warning;
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null && serialData2.warning != null)
		{
			m_Warning.Restore(serialData2.warning);
		}
	}

	private void OnAttached()
	{
	}

	private void OnDetaching()
	{
		m_Warning.Reset();
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		m_Animator = GetComponent<ModuleAnimator>();
		m_ShopInventory = GetComponent<ModuleShopInventory>();
		base.block.serializeEvent.Subscribe(OnSerialize);
		m_Warning = new WarningHolder(base.block.visible, WarningHolder.WarningType.Anchored);
	}

	private void OnSpawn()
	{
		m_CanLaunch = m_CanLaunchByDefault;
	}

	private void OnRecycle()
	{
		m_Ejector.StopEjecting();
	}

	private void OnUpdate()
	{
		if ((bool)m_Animator)
		{
			bool value = m_CanLaunch && base.block.tank != null && base.block.tank.IsAnchored;
			m_Animator.Set(m_ReadyBool, value);
		}
		m_Ejector.Update();
	}
}
