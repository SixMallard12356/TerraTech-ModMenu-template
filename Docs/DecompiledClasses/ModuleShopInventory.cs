#define UNITY_EDITOR
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleShopInventory : Module, INetworkedModule
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public InventorySupplier.SaveData supplierSaveData;
	}

	[SerializeField]
	private InventorySupplier m_InventorySupplier;

	[SerializeField]
	private NetInventory m_NetInventoryPrefab;

	private NetInventory m_NetInventory;

	public IInventory<BlockTypes> GetInventory()
	{
		return m_InventorySupplier.GetInventory();
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			m_InventorySupplier.Save(ref serialData.supplierSaveData);
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			m_InventorySupplier.Load(serialData2.supplierSaveData);
		}
	}

	private void OnPool()
	{
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.BlockUpdate.Subscribe(OnUpdate);
	}

	private void OnSpawn()
	{
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			if (!(m_InventorySupplier.GetInventory() is SingleplayerInventory))
			{
				m_InventorySupplier.SetInventory(new SingleplayerInventory());
			}
		}
		else if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			m_NetInventory = m_NetInventoryPrefab.transform.Spawn().GetComponent<NetInventory>();
			d.Assert(m_NetInventory.IsNotNull(), "Net inventory failed to spawn");
			m_InventorySupplier.SetInventory(m_NetInventory);
			m_NetInventory.name = "Shop Inventory";
			NetworkServer.Spawn(m_NetInventory.gameObject);
		}
		else
		{
			m_InventorySupplier.SetInventory(null);
		}
	}

	private void OnRecycle()
	{
		if (m_InventorySupplier.GetInventory() is NetInventory)
		{
			m_InventorySupplier.SetInventory(null);
		}
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			d.Assert(m_NetInventory.IsNotNull(), "Net inventory not present when cleaning up shop");
			NetworkServer.UnSpawn(m_NetInventory.gameObject);
			m_NetInventory.transform.Recycle();
		}
		m_NetInventory = null;
		m_InventorySupplier.Clear();
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleShopInventory;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		writer.Write(m_NetInventory.netId);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		NetworkInstanceId networkInstanceId = reader.ReadNetworkId();
		if (networkInstanceId != NetworkInstanceId.Invalid)
		{
			GameObject gameObject = ClientScene.FindLocalObject(networkInstanceId);
			if (gameObject.IsNotNull())
			{
				m_NetInventory = gameObject.GetComponent<NetInventory>();
				m_InventorySupplier.SetInventory(m_NetInventory);
			}
			d.Assert(m_NetInventory != null, "ModuleShopInventory (Networked) - failed to find our inventory");
		}
	}

	private void OnUpdate()
	{
		m_InventorySupplier.UpdateInventoryIfResetPeriodPassed();
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}
}
