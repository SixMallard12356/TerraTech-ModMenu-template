#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(TankBlock))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public abstract class Module : MonoBehaviour
{
	[Serializable]
	public abstract class SerialData
	{
	}

	[Serializable]
	public abstract class SerialData<T> : SerialData where T : SerialData
	{
		private static readonly int k_TypeHash = HashCodeUtility.GetPersistentHashCode(typeof(T).ToString());

		public static int GetTypeHash()
		{
			return k_TypeHash;
		}

		public void Store(Dictionary<int, SerialData> dict)
		{
			dict[k_TypeHash] = this;
		}

		public void Remove(Dictionary<int, SerialData> dict)
		{
			dict.Remove(k_TypeHash);
		}

		public static T Retrieve(Dictionary<int, SerialData> dict)
		{
			SerialData value = null;
			if (!dict.TryGetValue(k_TypeHash, out value))
			{
				value = BlockSerialisation.LookForAlias(k_TypeHash, dict);
			}
			return value as T;
		}
	}

	public class NetworkedProperty<TBlockMsg> where TBlockMsg : MessageBase, IBlockMessage, new()
	{
		public TBlockMsg Data;

		private TankBlock block;

		private INetworkedModule module;

		private short msgType;

		private Action<TBlockMsg> localUpdatedCallback;

		private bool forceUseMessages;

		public NetworkedProperty(Module module, TTMsgType msgType, Action<TBlockMsg> localUpdatedCallback, bool forceUseMessages = false)
		{
			INetworkedModule networkedModule = module as INetworkedModule;
			d.AssertFormat(networkedModule != null || forceUseMessages, "Module {0} does not implement INetworkedModule, nor is it configured to only sync using messages!", module);
			this.module = networkedModule;
			block = module.block;
			this.msgType = (short)msgType;
			this.localUpdatedCallback = localUpdatedCallback;
			this.forceUseMessages = forceUseMessages;
			Data = new TBlockMsg();
			block.RegisterNetworkableProperty<TBlockMsg>(msgType, OnLocalUpdated);
		}

		public void Sync()
		{
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				if (Singleton.Manager<ManNetwork>.inst.IsServer)
				{
					Data.BlockPoolID = block.blockPoolID;
					OnLocalUpdated(Data);
				}
				else
				{
					Singleton.Manager<ManLooseBlocks>.inst.SendBlockMessageToServer(block, (TTMsgType)msgType, Data);
				}
			}
		}

		private void OnLocalUpdated(MessageBase baseMessage)
		{
			Data = baseMessage as TBlockMsg;
			localUpdatedCallback(Data);
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				if (!forceUseMessages && module != null && block.IsAttached && block.tank.netTech.IsNotNull())
				{
					block.tank.netTech.SetModuleDirty(module);
				}
				else
				{
					Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost((TTMsgType)msgType, Data);
				}
			}
		}

		public void Serialise(NetworkWriter writer)
		{
			Data.Serialize(writer);
		}

		public void Deserialise(NetworkReader reader)
		{
			Data.Deserialize(reader);
			localUpdatedCallback(Data);
		}
	}

	[SerializeField]
	[HideInInspector]
	private TankBlock _block;

	protected ModuleControlCategory m_ControlCategoryType;

	public TankBlock block => _block;

	protected bool IsCategoryEnabled()
	{
		if (m_ControlCategoryType == ModuleControlCategory.NotImplemented)
		{
			d.LogError("Asking if Module category is enabled, but module does not implement a category type!");
			return true;
		}
		if (block.IsNull() || block.tank.IsNull() || block.tank.BlockStateController.IsNull())
		{
			return true;
		}
		return block.tank.BlockStateController.IsCategoryActive(m_ControlCategoryType);
	}

	private void PrePool()
	{
		_block = GetComponent<TankBlock>();
	}
}
