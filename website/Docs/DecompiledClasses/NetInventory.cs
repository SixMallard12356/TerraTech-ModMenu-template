#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class NetInventory : NetworkBehaviour, IInventory<BlockTypes>
{
	[NonSerialized]
	public Event<BlockTypes, int> InventoryChanged;

	private Dictionary<int, BlockTypes> m_BlockReservations = new Dictionary<int, BlockTypes>();

	private List<int> m_Players = new List<int>();

	[JsonProperty]
	private BlockCountList m_BlockCounts = new BlockCountList();

	private BlockCountList m_DirtyBlockCounts = new BlockCountList();

	private const uint kSer_PlayerList = 1u;

	private const uint kSer_BlockReservations = 2u;

	private const uint kSer_BlockCounts = 4u;

	private const uint kSer_IsSharedInventory = 8u;

	private const uint kSer_AllFlagMask = uint.MaxValue;

	public bool IsSharedInventory { get; private set; }

	[Server]
	public void OnServerRegisterUser(NetPlayer player)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetInventory::OnServerRegisterUser(NetPlayer)' called on client");
			return;
		}
		m_Players.Add(player.PlayerID);
		SetDirtyBit(1u);
	}

	[Server]
	public void OnServerUnregisterUser(NetPlayer player)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetInventory::OnServerUnregisterUser(NetPlayer)' called on client");
			return;
		}
		m_Players.Remove(player.PlayerID);
		SetDirtyBit(1u);
	}

	[Server]
	public void ServerSetIsSharedInventory(bool set)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetInventory::ServerSetIsSharedInventory(System.Boolean)' called on client");
		}
		else
		{
			IsSharedInventory = set;
		}
	}

	public IEnumerator<KeyValuePair<BlockTypes, int>> GetEnumerator()
	{
		return m_BlockCounts.GetEnumerator();
	}

	public int GetQuantity(BlockTypes blockType)
	{
		return m_BlockCounts.GetQuantity(blockType);
	}

	public int GetUnreservedQuantity(BlockTypes blockType)
	{
		int quantity = GetQuantity(blockType);
		if (quantity != -1)
		{
			return quantity - GetNumReserved(blockType);
		}
		return quantity;
	}

	public bool GetIsDeathStreakReward(BlockTypes blockType)
	{
		if (Singleton.Manager<ManNetwork>.inst.DeathStreakEnabled && Singleton.Manager<ManNetwork>.inst.MyPlayer != null)
		{
			return Singleton.Manager<ManNetwork>.inst.MyPlayer.IsBlockDeathStreakReward(blockType);
		}
		return false;
	}

	public BlockCountList GetBlockCountsForSaving()
	{
		return m_BlockCounts;
	}

	public void LoadFrom(BlockCountList savedBlockCounts)
	{
		Clear();
		foreach (KeyValuePair<BlockTypes, int> savedBlockCount in savedBlockCounts)
		{
			if (savedBlockCount.Value != 0)
			{
				m_BlockCounts.SetQuantity(savedBlockCount.Key, savedBlockCount.Value);
				m_DirtyBlockCounts.SetQuantity(savedBlockCount.Key, savedBlockCount.Value, keepZeroes: true);
			}
		}
	}

	public void SubscribeToInventoryChanged(Action<BlockTypes, int> _delegate)
	{
		InventoryChanged.Subscribe(_delegate);
	}

	public void UnsubscribeToInventoryChanged(Action<BlockTypes, int> _delegate)
	{
		InventoryChanged.Unsubscribe(_delegate);
	}

	public bool IsAvailableToLocalPlayer(BlockTypes blockType)
	{
		if (HasReservedItem(Singleton.Manager<ManNetwork>.inst.MyPlayer.PlayerID, blockType))
		{
			return true;
		}
		if (GetQuantity(blockType) != -1)
		{
			return GetUnreservedQuantity(blockType) > 0;
		}
		return true;
	}

	public int GetNumReserved(BlockTypes blockType)
	{
		int num = 0;
		foreach (BlockTypes value in m_BlockReservations.Values)
		{
			if (value == blockType)
			{
				num++;
			}
		}
		return num;
	}

	public bool CanReserveItem(int netPlayerID, BlockTypes blockType)
	{
		return GetUnreservedQuantity(blockType) > 0;
	}

	public bool HostReserveItem(int netPlayerID, BlockTypes blockType)
	{
		d.Assert(ManNetwork.IsHost, "Can't call authoritative methods on NetInventory from clients");
		d.Assert(m_Players.Contains(netPlayerID), "This player doesn't have access to this inventory");
		SetDirtyBit(2u);
		if (m_BlockReservations.TryGetValue(netPlayerID, out var value))
		{
			d.LogError(string.Concat("Player ", netPlayerID, " tried to reserve more than one block type. Already reserved: ", value, ", Attempting to reserve: ", blockType));
			return false;
		}
		if (CanReserveItem(netPlayerID, blockType))
		{
			m_BlockReservations.Add(netPlayerID, blockType);
			InventoryChanged.Send((BlockTypes)(-1), 0);
			return true;
		}
		return false;
	}

	public bool CancelReserveItem(int netPlayerID, BlockTypes blockType)
	{
		SetDirtyBit(2u);
		if (m_BlockReservations.TryGetValue(netPlayerID, out var value))
		{
			d.Assert(value == blockType, string.Concat("Player ", netPlayerID, " unreserved their block, but got forgot _which_ block. Reserved: ", value, ", Requested: ", blockType));
			m_BlockReservations.Remove(netPlayerID);
			InventoryChanged.Send((BlockTypes)(-1), 0);
			return true;
		}
		d.LogError(string.Concat("Player ", netPlayerID, " tried to cancel a block reservation for ", blockType, " that they don't have"));
		return false;
	}

	public bool HasReservedItem(int netPlayerID, BlockTypes blockType)
	{
		if (m_BlockReservations.TryGetValue(netPlayerID, out var value))
		{
			return value == blockType;
		}
		return false;
	}

	public bool CanConsumeItem(int netPlayerID, BlockTypes blockType)
	{
		int quantity = m_BlockCounts.GetQuantity(blockType);
		if (netPlayerID == -1)
		{
			if (quantity != -1)
			{
				return quantity > 0;
			}
			return true;
		}
		if (m_BlockReservations.TryGetValue(netPlayerID, out var value) && value == blockType)
		{
			if (quantity != -1)
			{
				return quantity > 0;
			}
			return true;
		}
		return false;
	}

	public int HostConsumeItem(int netPlayerID, BlockTypes blockType, int count = 1)
	{
		d.Assert(ManNetwork.IsHost, "Can't call authoritative methods on NetInventory from clients");
		d.Assert(netPlayerID == -1 || m_Players.Contains(netPlayerID), "This player doesn't have access to this inventory");
		if (CanConsumeItem(netPlayerID, blockType))
		{
			int num = m_BlockCounts.ConsumeItem(blockType, count);
			if (netPlayerID != -1)
			{
				CancelReserveItem(netPlayerID, blockType);
			}
			InventoryQuantityChanged(new BlockCount(blockType, num));
			return num;
		}
		return 0;
	}

	public void HostAddItem(BlockTypes blockType, int count = 1)
	{
		d.Assert(ManNetwork.IsHost, "Can't call authoritative methods on NetInventory from clients");
		int quantity = m_BlockCounts.AddItem(blockType, count);
		InventoryQuantityChanged(new BlockCount(blockType, quantity));
	}

	public void HostStoreTech(TechData techData)
	{
		foreach (TankPreset.BlockSpec blockSpec in techData.m_BlockSpecs)
		{
			HostAddItem(blockSpec.GetBlockType());
		}
	}

	public bool HasItemsToSpawnTech(TechData techData)
	{
		return HasItemsToSpawnTech(new BlockCountList(techData));
	}

	public bool HasItemsToSpawnTech(BlockCountList counts)
	{
		foreach (KeyValuePair<BlockTypes, int> count in counts)
		{
			BlockTypes key = count.Key;
			int value = count.Value;
			int unreservedQuantity = GetUnreservedQuantity(key);
			if (unreservedQuantity != -1 && unreservedQuantity < value)
			{
				return false;
			}
		}
		return true;
	}

	public void SetBlockCount(BlockTypes blockType, int count)
	{
		m_BlockCounts.SetQuantity(blockType, count);
		InventoryQuantityChanged(new BlockCount(blockType, count));
	}

	public void Clear()
	{
		m_DirtyBlockCounts.CreateZeroedCopyOf(ref m_BlockCounts);
		SetDirtyBit(4u);
		m_BlockCounts.Clear();
	}

	public void FillTo(InventoryBlockList list)
	{
		if (list == null)
		{
			return;
		}
		for (int i = 0; i < list.m_BlockList.Length; i++)
		{
			BlockCount blockCount = list.m_BlockList[i];
			int quantity = GetQuantity(blockCount.m_BlockType);
			if (quantity != -1)
			{
				if (blockCount.m_Quantity == -1)
				{
					HostAddItem(blockCount.m_BlockType, -1);
				}
				else if (blockCount.m_Quantity > quantity)
				{
					HostAddItem(blockCount.m_BlockType, blockCount.m_Quantity - quantity);
				}
			}
		}
	}

	public override bool OnSerialize(NetworkWriter writer, bool initialState)
	{
		uint num = (initialState ? uint.MaxValue : base.syncVarDirtyBits);
		writer.Write(num);
		if ((num & 1) != 0)
		{
			writer.Write(m_Players.Count);
			for (int i = 0; i < m_Players.Count; i++)
			{
				writer.Write(m_Players[i]);
			}
		}
		if ((num & 2) != 0)
		{
			writer.Write(m_BlockReservations.Count);
			foreach (int key in m_BlockReservations.Keys)
			{
				writer.Write(key);
				writer.Write((int)m_BlockReservations[key]);
			}
		}
		if ((num & 4) != 0)
		{
			if (initialState)
			{
				writer.Write(ref m_BlockCounts);
			}
			else
			{
				writer.Write(ref m_DirtyBlockCounts);
			}
		}
		if ((num & 8) != 0)
		{
			writer.Write(IsSharedInventory);
		}
		if (!initialState)
		{
			m_DirtyBlockCounts.Clear();
		}
		return num != 0;
	}

	public override void OnDeserialize(NetworkReader reader, bool initialState)
	{
		uint num = reader.ReadUInt32();
		if ((num & 1) != 0)
		{
			m_Players.Clear();
			int num2 = reader.ReadInt32();
			for (int i = 0; i < num2; i++)
			{
				m_Players.Add(reader.ReadInt32());
			}
			UpdateSubscribedPlayers();
		}
		if ((num & 2) != 0)
		{
			m_BlockReservations.Clear();
			int num3 = reader.ReadInt32();
			for (int j = 0; j < num3; j++)
			{
				m_BlockReservations.Add(reader.ReadInt32(), (BlockTypes)reader.ReadInt32());
			}
			InventoryChanged.Send((BlockTypes)(-1), 0);
		}
		if ((num & 4) != 0)
		{
			if (initialState)
			{
				reader.Read(ref m_BlockCounts);
			}
			else
			{
				reader.Read(ref m_DirtyBlockCounts);
				foreach (KeyValuePair<BlockTypes, int> dirtyBlockCount in m_DirtyBlockCounts)
				{
					InventoryQuantityChanged(new BlockCount(dirtyBlockCount.Key, dirtyBlockCount.Value));
				}
				m_BlockCounts.UpdateCountsFrom(m_DirtyBlockCounts);
			}
		}
		if ((num & 8) != 0)
		{
			IsSharedInventory = reader.ReadBoolean();
		}
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		if (!Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			if (Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeCoOpCampaign>() && IsSharedInventory)
			{
				Mode<ModeCoOpCampaign>.inst.SetSharedInventory(this);
			}
			UpdateSubscribedPlayers();
		}
	}

	private void UpdateSubscribedPlayers()
	{
		foreach (int player in m_Players)
		{
			NetPlayer netPlayer = Singleton.Manager<ManNetwork>.inst.FindPlayerByPlayerID(player);
			if (netPlayer != null)
			{
				netPlayer.SetInventory(this);
			}
		}
	}

	private void OnRecycle()
	{
		m_Players.Clear();
		m_BlockReservations.Clear();
		m_BlockCounts.Clear();
		m_DirtyBlockCounts.Clear();
		IsSharedInventory = false;
	}

	protected void InventoryQuantityChanged(BlockCount blockCount)
	{
		Singleton.Manager<ManStats>.inst.InventoryQuantityUpdated(blockCount.m_BlockType, blockCount.m_Quantity);
		InventoryChanged.Send(blockCount.m_BlockType, blockCount.m_Quantity);
		if (base.isServer)
		{
			SetDirtyBit(4u);
			m_DirtyBlockCounts.SetQuantity(blockCount.m_BlockType, blockCount.m_Quantity, keepZeroes: true);
		}
	}

	private void UNetVersion()
	{
	}
}
