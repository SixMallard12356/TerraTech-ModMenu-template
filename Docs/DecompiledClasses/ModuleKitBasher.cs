#define UNITY_EDITOR
using System;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleKitBasher : Module, INetworkedModule
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public KitBashPanelSpawner.PanelSpawnData[] panelSpawnData;
	}

	[SerializeField]
	protected GameObject m_PreviewObject;

	[SerializeField]
	[Tooltip("The bash pool preset to use for this block, if this is used then the specific bashPool will not be used")]
	protected KitBashPanelPoolPreset m_BashPoolPreset;

	[SerializeField]
	[Tooltip("The bash pool to use specifically for this block")]
	protected KitBashPanelPool m_SpecificBashPool;

	protected KitBashPanelSpawner[] m_Spawners;

	private NetworkedProperty<ModuleKitBasherPanelSyncMessage> net_PanelSpawnData;

	private bool m_DoBashFlag;

	private void Bash()
	{
		KitBashPanelPool bashPool = ((m_BashPoolPreset != null) ? m_BashPoolPreset.Pool : m_SpecificBashPool);
		DRNG randomizer = new DRNG(base.block.blockPoolID);
		for (int i = 0; i < m_Spawners.Length; i++)
		{
			m_Spawners[i].Bash(bashPool, randomizer);
		}
		UpdatePanelsChanged();
	}

	private void SetBashFromSpawnData(KitBashPanelSpawner.PanelSpawnData[] spawnData)
	{
		if (spawnData != null)
		{
			d.AssertFormat(spawnData.Length == m_Spawners.Length, "Error Loading Kit Bash Spawn Data for '{0}', save data panel count [{1}] did not match available panel spawner count [{2}]", base.gameObject.name, spawnData.Length, m_Spawners.Length);
			for (int i = 0; i < spawnData.Length; i++)
			{
				m_Spawners[i].SetPanel(spawnData[i]);
			}
			UpdatePanelsChanged();
		}
	}

	private void UpdatePanelsChanged()
	{
		base.block.visible.RefreshMeshRendererList();
		SyncMP();
	}

	private void SyncMP()
	{
		if (ManNetwork.IsHost)
		{
			net_PanelSpawnData.Data.panelSpawnData = GetCurrentSpawnData();
			net_PanelSpawnData.Sync();
		}
	}

	private void OnAttached()
	{
	}

	private void OnDetaching()
	{
	}

	private void OnUniqueIDAssigned(uint id)
	{
		if (m_DoBashFlag)
		{
			m_DoBashFlag = false;
			Bash();
		}
	}

	private void OnMPSync(ModuleKitBasherPanelSyncMessage msg)
	{
		if (!ManNetwork.IsHost)
		{
			SetBashFromSpawnData(msg.panelSpawnData);
		}
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		SerialData serialData;
		if (saving)
		{
			serialData = new SerialData
			{
				panelSpawnData = GetCurrentSpawnData()
			};
			serialData.Store(blockSpec.saveState);
			return;
		}
		serialData = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData != null)
		{
			SetBashFromSpawnData(serialData.panelSpawnData);
		}
	}

	private void OnSerializeText(bool saving, TankPreset.BlockSpec context, bool onTech)
	{
		if (saving)
		{
			string[] value = (from r in GetCurrentSpawnData()
				select r.ToString()).ToArray();
			context.Store(GetType(), "moduleKitBasher_PanelSpawnData", string.Join(','.ToString(), value));
		}
		else
		{
			if (!onTech)
			{
				return;
			}
			string[] array = context.Retrieve(GetType(), "moduleKitBasher_PanelSpawnData").Split(',');
			if (array != null)
			{
				KitBashPanelSpawner.PanelSpawnData result;
				KitBashPanelSpawner.PanelSpawnData[] array2 = array.Select((string r) => (!KitBashPanelSpawner.PanelSpawnData.TryParse(r, out result)) ? KitBashPanelSpawner.PanelSpawnData.invalid : result).ToArray();
				if (array2.Any((KitBashPanelSpawner.PanelSpawnData r) => r.IsInvalid))
				{
					d.LogErrorFormat("Invalid panel spawn data in ModuleKitBasher on '{0}'. Aborting deserializeText, block will NOT be loaded! Has something corrupted? Changed versions? Call a coder.", base.gameObject.transform.GetTransformHeirarchyPath());
				}
				else
				{
					SetBashFromSpawnData(array2);
				}
			}
		}
	}

	private KitBashPanelSpawner.PanelSpawnData[] GetCurrentSpawnData()
	{
		KitBashPanelSpawner.PanelSpawnData[] array = new KitBashPanelSpawner.PanelSpawnData[m_Spawners.Length];
		for (int i = 0; i < m_Spawners.Length; i++)
		{
			m_Spawners[i].TryGetPanelSpawnData(out array[i]);
		}
		return array;
	}

	TankBlock INetworkedModule.GetBlock()
	{
		return base.block;
	}

	NetworkedModuleID INetworkedModule.GetModuleID()
	{
		return NetworkedModuleID.ModuleKitBasher;
	}

	void INetworkedModule.OnSerialize(NetworkWriter writer)
	{
		net_PanelSpawnData.Serialise(writer);
	}

	void INetworkedModule.OnDeserialize(NetworkReader reader)
	{
		net_PanelSpawnData.Deserialise(reader);
	}

	private void PrePool()
	{
		if (m_PreviewObject != null)
		{
			UnityEngine.Object.DestroyImmediate(m_PreviewObject);
			m_PreviewObject = null;
		}
	}

	private void OnPool()
	{
		m_Spawners = GetComponentsInChildren<KitBashPanelSpawner>();
		net_PanelSpawnData = new NetworkedProperty<ModuleKitBasherPanelSyncMessage>(this, TTMsgType.ModuleKitBasherPanelSync, OnMPSync);
		UpdatePanelsChanged();
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.serializeTextEvent.Subscribe(OnSerializeText);
		base.block.UniqueIDAssignedEvent.Subscribe(OnUniqueIDAssigned);
	}

	private void OnDepool()
	{
		base.block.AttachedEvent.Unsubscribe(OnAttached);
		base.block.DetachingEvent.Unsubscribe(OnDetaching);
		base.block.serializeEvent.Unsubscribe(OnSerialize);
		base.block.serializeTextEvent.Unsubscribe(OnSerializeText);
		base.block.UniqueIDAssignedEvent.Unsubscribe(OnUniqueIDAssigned);
	}

	private void OnSpawn()
	{
		m_DoBashFlag = true;
	}

	private void OnRecycle()
	{
	}
}
