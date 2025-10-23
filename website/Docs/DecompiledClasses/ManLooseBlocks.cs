#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ManLooseBlocks : Singleton.Manager<ManLooseBlocks>, Mode.IManagerModeEvents
{
	internal abstract class BlockMessageBrokerBase
	{
		protected short msgType;

		public void SendMessage(MessageBase message)
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer((TTMsgType)msgType, message);
		}
	}

	internal class BlockMessageBroker<TBlockMsg> : BlockMessageBrokerBase where TBlockMsg : MessageBase, IBlockMessage, new()
	{
		public BlockMessageBroker(TTMsgType msgType)
		{
			base.msgType = (short)msgType;
			Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(msgType, OnServerMessageReceived);
			Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(msgType, OnClientMessageReceived);
		}

		public void OnServerMessageReceived(NetworkMessage netMsg)
		{
			TBlockMsg val = netMsg.ReadMessage<TBlockMsg>();
			TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(val.BlockPoolID);
			if (tankBlock.IsNotNull())
			{
				tankBlock.OnBlockMessageReceived(msgType, val);
			}
			else
			{
				d.LogError($"Failed to find block with ID {val.BlockPoolID} for message {(TTMsgType)netMsg.msgType} with datatype {typeof(TBlockMsg)}");
			}
		}

		public void OnClientMessageReceived(NetworkMessage netMsg)
		{
			TBlockMsg val = netMsg.ReadMessage<TBlockMsg>();
			TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(val.BlockPoolID);
			if (tankBlock.IsNotNull())
			{
				tankBlock.OnBlockMessageReceived(msgType, val);
			}
			else
			{
				d.LogError($"Failed to find block with ID {val.BlockPoolID} for message {(TTMsgType)netMsg.msgType} with datatype {typeof(TBlockMsg)}");
			}
		}
	}

	private Dictionary<uint, TankBlock> m_TankBlockByPoolIDLookup = new Dictionary<uint, TankBlock>();

	private Dictionary<uint, ResourcePickup> m_ChunkByPoolIDLookup = new Dictionary<uint, ResourcePickup>();

	private NetPlayer m_PlayerDoingDetach;

	private uint m_NextSingleplayerBlockPoolID = 1u;

	private List<NetBlockChunk> m_PendingAttach = new List<NetBlockChunk>();

	private Dictionary<TTMsgType, BlockMessageBrokerBase> m_BlockMessageBrokers = new Dictionary<TTMsgType, BlockMessageBrokerBase>(new TTMsgTypeComparer());

	private List<TankBlock> s_TempBlockCache = new List<TankBlock>();

	private bool isNetworked => Singleton.Manager<ManNetwork>.inst.IsMultiplayer();

	private bool isHost
	{
		get
		{
			if (isNetworked)
			{
				return Singleton.Manager<ManNetwork>.inst.IsServer;
			}
			return true;
		}
	}

	public TankBlock RequestSpawnPaintingBlock(BlockTypes type, Vector3 pos, Quaternion rot)
	{
		bool initNew = true;
		uint blockPoolID = GenerateBlockPoolID();
		TankBlock tankBlock = null;
		if (TankBlock.IsBlockPoolIDValid(blockPoolID))
		{
			if (isNetworked)
			{
				bool flag = true;
				if (Singleton.Manager<ManNetwork>.inst.MyPlayer.IsNotNull() && Singleton.Manager<ManNetwork>.inst.MyPlayer.Inventory.IsNotNull())
				{
					flag = flag && Singleton.Manager<ManNetwork>.inst.MyPlayer.Inventory.CanReserveItem(Singleton.Manager<ManNetwork>.inst.MyPlayer.PlayerID, type);
				}
				if (flag)
				{
					if (!isHost)
					{
						tankBlock = DoSpawnTankBlock(blockPoolID, type, pos, rot, initNew);
					}
					SendSpawnPaintingBlockRequestToServer(type, pos, rot, blockPoolID);
					if (isHost)
					{
						tankBlock = FindTankBlock(blockPoolID);
						d.Assert(tankBlock != null, "ManSpawn.SpawnNewBlockAsHost - Spawned block as host, but couldn't find it afterwards");
					}
				}
			}
			else
			{
				tankBlock = DoSpawnTankBlock(blockPoolID, type, pos, rot, initNew);
			}
		}
		return tankBlock;
	}

	public void RequestDebugSpawnItem(ObjectTypes objectType, int itemType, Vector3 scenePos, Quaternion rot)
	{
		uint blockPoolID = GenerateBlockPoolID();
		if (isNetworked)
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.DebugSpawnItem, new SpawnItemMessage
			{
				m_ObjectType = objectType,
				m_ItemType = itemType,
				m_Pos = WorldPosition.FromScenePosition(in scenePos),
				m_Rot = rot,
				m_BlockPoolID = blockPoolID
			});
			return;
		}
		switch (objectType)
		{
		case ObjectTypes.Block:
			DoSpawnTankBlock(blockPoolID, (BlockTypes)itemType, scenePos, rot);
			break;
		case ObjectTypes.Chunk:
			DoSpawnResourcePickup(blockPoolID, (ChunkTypes)itemType, scenePos, rot);
			break;
		default:
			d.LogErrorFormat("RequestDebugSpawnItem does not support object of type {0}", objectType);
			break;
		}
	}

	public TankBlock HostSpawnBlock(BlockTypes type, Vector3 pos, Quaternion rot, bool initNew = true)
	{
		d.Assert(isHost, "ManLooseBlocks - Can't call HostSpawnBlock on client");
		uint blockPoolID = GenerateBlockPoolID();
		TankBlock tankBlock = DoSpawnTankBlock(blockPoolID, type, pos, rot, initNew);
		d.Assert(tankBlock != null, "ManLooseBlocks.HostSpawnBlock - Failed to spawn TankBlock");
		if (isNetworked)
		{
			NetBlock netBlock = DoSpawnNetBlock(null, tankBlock, 0u, clientAuthority: false);
			d.Assert(netBlock != null, "ManLooseBlocks.OnServerSpawnPaintingBlockRequest - Failed to spawn NetBlock");
			d.Assert(netBlock.block == tankBlock, "ManLooseBlocks.HostSpawnBlock - TankBlock and NetBlock did not pair");
		}
		return tankBlock;
	}

	public ResourcePickup HostSpawnChunk(ChunkTypes type, Vector3 pos, Quaternion rot, bool initNew = true, Vector3 vel = default(Vector3), Vector3 rotVel = default(Vector3))
	{
		d.Assert(isHost, "ManLooseBlocks - Can't call HostSpawnBlock on client");
		uint blockPoolID = GenerateBlockPoolID();
		ResourcePickup resourcePickup = DoSpawnResourcePickup(blockPoolID, type, pos, rot, initNew, vel, rotVel);
		d.Assert(resourcePickup != null, "ManLooseBlocks.HostSpawnBlock - Failed to spawn TankBlock");
		if (isNetworked)
		{
			NetChunk netChunk = DoSpawnNetChunk(null, resourcePickup);
			d.Assert(netChunk != null, "ManLooseBlocks.HostSpawnChunk - Failed to spawn NetChunk");
			d.Assert(netChunk.chunk == resourcePickup, "ManLooseBlocks.HostSpawnBlock - TankBlock and NetBlock did not pair");
		}
		return resourcePickup;
	}

	public TankBlock FindOrSpawnBlockForTech(BlockTypes type, uint blockPoolID)
	{
		TankBlock tankBlock = FindTankBlock(blockPoolID);
		if (tankBlock != null)
		{
			if (tankBlock.tank != null)
			{
				DoDetach(tankBlock, allowHeadlessTech: true);
			}
		}
		else
		{
			tankBlock = DoSpawnTankBlock(blockPoolID, type, Vector3.zero, Quaternion.identity);
		}
		return tankBlock;
	}

	public TankBlock SpawnNonNetworkedBlock(BlockTypes type, Vector3 pos, Quaternion rot, uint blockPoolID = uint.MaxValue)
	{
		if (blockPoolID == uint.MaxValue)
		{
			blockPoolID = GenerateBlockPoolID();
		}
		TankBlock tankBlock = DoSpawnTankBlock(blockPoolID, type, pos, rot);
		d.Assert(tankBlock.IsNotNull(), "ManLooseBlocks.SpawnNonNetworkedBlock - Failed to spawn TankBlock");
		return tankBlock;
	}

	public void HostConvertToNetBlock(TankBlock tankBlock, uint initialSpawnShieldID = 0u, NetPlayer owner = null, ManNetwork.AuthorityReason authReason = ManNetwork.AuthorityReason.HeldVisible)
	{
		d.Assert(isHost, "ManLooseBlocks - Can't call HostConvertToNetBlock on client");
		DoSpawnNetBlock(owner, tankBlock, initialSpawnShieldID, owner != null, authReason);
	}

	public void GenerateBlockPoolIDs(ref uint[] techDataBlockPoolIDs)
	{
		for (int i = 0; i < techDataBlockPoolIDs.Length; i++)
		{
			techDataBlockPoolIDs[i] = GenerateBlockPoolID();
		}
	}

	public void RegisterBlockPoolIDsFromTank(Tank tech)
	{
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tech.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current = enumerator.Current;
			if (current.HasValidBlockPoolID())
			{
				if (!m_TankBlockByPoolIDLookup.ContainsKey(current.blockPoolID))
				{
					m_TankBlockByPoolIDLookup.Add(current.blockPoolID, current);
				}
				else if (current != m_TankBlockByPoolIDLookup[current.blockPoolID])
				{
					d.LogWarning("Other block with ID " + current.blockPoolID + " already present in block pool ID lookup");
				}
			}
		}
	}

	public bool RequestAttachBlock(Tank tech, TankBlock block, IntVector3 pos, OrthoRotation rot = default(OrthoRotation))
	{
		bool result = false;
		if (isNetworked)
		{
			if (!isHost)
			{
				if (block.netBlock != null)
				{
					block.netBlock.ClientPresumptiveDisconnect();
				}
				result = DoAttach(tech, block, pos, rot, Singleton.Manager<ManNetwork>.inst.MyPlayer.name);
			}
			BlockAttachedMessage message = new BlockAttachedMessage
			{
				m_TechNetId = tech.netTech.netId,
				m_BlockPosition = pos,
				m_BlockOrthoRotation = rot,
				m_BlockPoolID = block.blockPoolID
			};
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.BlockAttach, message);
			if (isHost)
			{
				result = block.tank == tech;
			}
		}
		else
		{
			result = DoAttach(tech, block, pos, rot);
		}
		return result;
	}

	public void AddPendingStackItemForTech(NetBlockChunk netBlockChunk)
	{
		m_PendingAttach.Add(netBlockChunk);
	}

	public void AttachPendingVisiblesToTech(NetTech netTech)
	{
		for (int num = m_PendingAttach.Count - 1; num >= 0; num--)
		{
			NetBlockChunk netBlockChunk = m_PendingAttach[num];
			if (netBlockChunk.HoldingTechNetworkID == netTech.netId)
			{
				if ((bool)netBlockChunk.visible && netBlockChunk.visible.gameObject.activeInHierarchy)
				{
					netBlockChunk.ApplyHolderState(netTech.tech);
				}
				m_PendingAttach.RemoveAt(num);
			}
		}
	}

	public void RequestDetachBlock(TankBlock block, bool allowHeadlessTech, bool manualRemove = false)
	{
		d.Assert(block.tank != null, "BlockManager.RequestRemoveBlock. Trying to remove a block that doesn't belong to this tech");
		Tank tank = block.tank;
		BlockManager blockman = tank.blockman;
		bool playerInitiatedRemoveBlock = blockman.PlayerInitiatedRemoveBlock;
		blockman.PlayerInitiatedRemoveBlock = manualRemove;
		if (isNetworked)
		{
			if (!isHost)
			{
				DoDetach(block, allowHeadlessTech, propagate: false, null, manualRemove ? Singleton.Manager<ManNetwork>.inst.MyPlayer.name : "");
			}
			d.LogFormat("Request Detach Block from Tech Name={0}, TechID={1}, BlockPoolID={2}, was allowHeadless={3}", tank.name, tank.netTech.netId, block.blockPoolID, allowHeadlessTech);
			BlockDetachMessage message = new BlockDetachMessage
			{
				m_blockPoolID = block.blockPoolID,
				m_RemovalSeed = tank.blockman.BlockRemovalSeed,
				m_AllowHeadlessTech = allowHeadlessTech,
				m_TechNetId = tank.netTech.netId,
				m_ManuallyRemoved = blockman.PlayerInitiatedRemoveBlock
			};
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.BlockDetach, message);
		}
		else
		{
			DoDetach(block, allowHeadlessTech, propagate: true);
		}
		blockman.PlayerInitiatedRemoveBlock = playerInitiatedRemoveBlock;
	}

	public void HostDetachBlock(TankBlock block, bool allowHeadlessTech, bool propagate)
	{
		d.Assert(isHost, "HostDetachBlock - Can't call this function on clients");
		d.Assert(block.tank != null, "HostDetachBlock - Can't call this function on a block that isn't attached to a tech");
		if (isNetworked)
		{
			BlockDetachMessage message = new BlockDetachMessage
			{
				m_blockPoolID = block.blockPoolID,
				m_RemovalSeed = block.tank.blockman.BlockRemovalSeed,
				m_AllowHeadlessTech = allowHeadlessTech,
				m_TechNetId = block.tank.netTech.netId,
				m_ManuallyRemoved = block.tank.blockman.PlayerInitiatedRemoveBlock
			};
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.BlockDetach, message);
		}
		else
		{
			DoDetach(block, allowHeadlessTech, propagate);
		}
	}

	public void UnnetworkedSeparateCall(TankBlock block, bool allowHeadlessTech, bool propagate)
	{
		d.Assert(!isNetworked, "Calling UnnetworkedSeparateCall in MP");
		DoDetach(block, allowHeadlessTech, propagate);
	}

	public void RequestDespawnBlock(TankBlock block, DespawnReason reason)
	{
		uint blockPoolID = block.blockPoolID;
		if (isNetworked)
		{
			if (!isHost)
			{
				if (block.netBlock.IsNotNull())
				{
					block.netBlock.ClientPresumptiveDisconnect();
				}
				DoDestroyTankBlock(block);
			}
			RemoveBlockMessage message = new RemoveBlockMessage
			{
				m_BlockPoolID = blockPoolID,
				m_Reason = reason
			};
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.DespawnPaintingBlock, message);
		}
		else
		{
			DoDestroyTankBlock(block);
		}
	}

	public void HostDestroyChunk(ResourcePickup chunk)
	{
		uint blockPoolID = chunk.blockPoolID;
		if (isNetworked)
		{
			if (!isHost)
			{
				if (chunk.netChunk.IsNotNull())
				{
					chunk.netChunk.Disconnect();
				}
				DoDestroyChunk(chunk);
			}
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.DebugDespawnChunk, new EmptyBlockMessage
			{
				m_BlockPoolID = blockPoolID
			});
		}
		else
		{
			DoDestroyChunk(chunk);
		}
	}

	public void HostDestroyBlock(TankBlock block, bool errorOnClientCall = true)
	{
		d.Assert(!errorOnClientCall || isHost, "HostDestroyBlock - Can't call this on a client");
		RequestDespawnBlock(block, DespawnReason.Host);
	}

	public void HostDestroyNetBlock(NetBlock netBlock)
	{
		d.Assert(isHost, "HostDestroyBlock - Can't call this on a client");
		DoDestroyNetBlock(netBlock);
	}

	public void HostReplaceBlock(TankBlock[] blocksToRemove, TankPreset.BlockSpec[] replacementSpecs)
	{
		Tank tank = blocksToRemove[0].tank;
		bool isAnchored = tank.IsAnchored;
		TankBlock[] array = blocksToRemove;
		foreach (TankBlock block in array)
		{
			tank.blockman.DetachSingleBlock(block, isPropagating: false, rootTransfer: true, cleanup: false);
			if (tank.netTech.IsNotNull())
			{
				tank.netTech.OnBlockDetached(block);
			}
		}
		s_TempBlockCache.Clear();
		using (ManSpawn.PopulateTechHelper populateTechHelper = new ManSpawn.PopulateTechHelper(tank, spawningNew: true, recycleFailedAdds: false, null, allowHeadlessTech: false, tryDeployAnchors: false, reportFailure: true, allowAttachBlocksWithoutLinks: true))
		{
			for (int i = 0; i < replacementSpecs.Length; i++)
			{
				TankPreset.BlockSpec serialData = replacementSpecs[i];
				Visible.DisableAddToTileOnSpawn = true;
				TankBlock tankBlock = SpawnNonNetworkedBlock(serialData.GetBlockType(), Vector3.zero, Quaternion.identity);
				Visible.DisableAddToTileOnSpawn = false;
				populateTechHelper.AddBlock(tankBlock, serialData, alreadyAttached: false);
				s_TempBlockCache.Add(tankBlock);
			}
		}
		bool rootTransfer = true;
		array = blocksToRemove;
		foreach (TankBlock tankBlock2 in array)
		{
			tank.blockman.RestructureFollowingBlockDetach(tankBlock2, rootTransfer, isAnchored);
			rootTransfer = false;
			tankBlock2.CleanupBlocksByAP();
			HostDestroyBlock(tankBlock2, errorOnClientCall: false);
		}
		foreach (TankBlock item in s_TempBlockCache)
		{
			if (item.IsAttached && (item.NumConnectedAPs == 0 || !item.CanReachRoot()))
			{
				tank.blockman.CleanupInvalidBlockOnTech(item);
			}
		}
		s_TempBlockCache.Clear();
	}

	public void OnClientBlockDeserialize(NetBlock netBlock)
	{
		TankBlock tankBlock = null;
		tankBlock = FindTankBlock(netBlock.BlockPoolID);
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			d.Assert(tankBlock != null, "Called OnStartClient on the server on a NetBlock but there is no corresponding TankBlock. We should already have one, as we are the server.");
			netBlock.ConnectTo(tankBlock);
		}
		else if (tankBlock != null)
		{
			netBlock.ConnectTo(tankBlock);
		}
		else
		{
			tankBlock = DoSpawnTankBlock(netBlock.BlockPoolID, netBlock.BlockType, netBlock.transform.position, netBlock.transform.rotation, initNew: false);
			netBlock.ConnectTo(tankBlock);
		}
	}

	public void OnClientChunkDeserialize(NetChunk netChunk)
	{
		d.Assert(!ManNetwork.IsHost, "OnClientChunkDeserialize should not be called on the host");
		d.AssertFormat(!FindChunk(netChunk.BlockPoolID), "OnClientChunkDeserialize called for a chunk which already exists (ID={0})", netChunk.BlockPoolID);
		ResourcePickup to = DoSpawnResourcePickup(netChunk.BlockPoolID, netChunk.ChunkType, netChunk.transform.position, netChunk.transform.rotation, initNew: false);
		netChunk.ConnectTo(to);
	}

	private void OnServerDetachBlockFromPropagation(Tank tech, TankBlock block)
	{
		if (isHost)
		{
			BlockDetachMessage message = new BlockDetachMessage
			{
				m_blockPoolID = block.blockPoolID,
				m_RemovalSeed = tech.blockman.BlockRemovalSeed,
				m_AllowHeadlessTech = true,
				m_TechNetId = tech.netTech.netId,
				m_ManuallyRemoved = tech.blockman.PlayerInitiatedRemoveBlock
			};
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.BlockDetach, message);
			d.Assert(DoSpawnNetBlock(m_PlayerDoingDetach, block, tech.netTech.InitialSpawnShieldID, clientAuthority: false).block == block, "OnServerDetachBlockFromPropagation - Did not connect to TankBlock when spawning NetBlock");
		}
	}

	public void OnTankBlockRecycle(TankBlock block)
	{
		m_TankBlockByPoolIDLookup.Remove(block.blockPoolID);
	}

	public void OnChunkRecycle(ResourcePickup resourcePickup)
	{
		m_ChunkByPoolIDLookup.Remove(resourcePickup.blockPoolID);
	}

	private void RegisterMessageHandlers()
	{
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.SpawnPaintingBlock, OnServerSpawnPaintingBlockRequest);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.BlockAttach, OnServerAttachBlockRequest);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.BlockDetach, OnServerDetachBlockRequest);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.DebugSpawnItem, OnServerDebugSpawnItem);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.DespawnPaintingBlock, OnServerDespawnBlockMessage);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.DebugDespawnChunk, OnServerDespawnChunkMessage);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.BlockAttach, OnClientAttachBlockMessage);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.BlockAttachFailed, OnClientAttachBlockFailed);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.BlockDetach, OnClientDetachBlock);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.BlockDetachFailed, OnClientDetachFailed);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.BlockRemoveFromGame, OnClientBlockDestroyMessage);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.ShowExplosiveBoltsFX, OnClientShowExplosiveBoltAnimation);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.SpawnPaintingBlockFailed, OnClientSpawnPaintingBlockFailed);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.BlockAnimationChange, OnClientBlockAnimationChange);
	}

	private void OnServerSpawnPaintingBlockRequest(NetworkMessage netMsg)
	{
		SpawnBlockMessage spawnBlockMessage = netMsg.ReadMessage<SpawnBlockMessage>();
		NetPlayer sender = netMsg.GetSender();
		bool flag = true;
		if (sender.Inventory.IsNotNull())
		{
			flag = flag && sender.Inventory.CanReserveItem(sender.PlayerID, spawnBlockMessage.m_BlockType);
		}
		if (flag)
		{
			if (sender.Inventory.IsNotNull())
			{
				sender.Inventory.HostReserveItem(sender.PlayerID, spawnBlockMessage.m_BlockType);
			}
			TankBlock tankBlock = DoSpawnTankBlock(spawnBlockMessage.m_BlockPoolID, spawnBlockMessage.m_BlockType, spawnBlockMessage.m_Pos.ScenePosition, spawnBlockMessage.m_Rot);
			d.Assert(tankBlock != null, "ManLooseBlocks.OnServerSpawnPaintingBlockRequest - Failed to spawn TankBlock");
			NetBlock netBlock = DoSpawnNetBlock(netMsg.GetSender(), tankBlock, spawnBlockMessage.m_InitialSpawnShieldID, clientAuthority: true);
			d.Assert(netBlock != null, "ManLooseBlocks.OnServerSpawnPaintingBlockRequest - Failed to spawn NetBlock");
			d.Assert(netBlock.block == tankBlock, "ManLooseBlocks.OnServerSpawnPaintingBlockRequest - TankBlock and NetBlock did not pair");
		}
		else
		{
			Singleton.Manager<ManNetwork>.inst.SendToClient(netMsg.conn.connectionId, TTMsgType.SpawnPaintingBlockFailed, new EmptyMessage());
		}
	}

	private void OnServerDebugSpawnItem(NetworkMessage netMsg)
	{
		SpawnItemMessage spawnItemMessage = netMsg.ReadMessage<SpawnItemMessage>();
		switch (spawnItemMessage.m_ObjectType)
		{
		case ObjectTypes.Block:
		{
			TankBlock tankBlock = DoSpawnTankBlock(spawnItemMessage.m_BlockPoolID, (BlockTypes)spawnItemMessage.m_ItemType, spawnItemMessage.m_Pos.ScenePosition, spawnItemMessage.m_Rot);
			d.AssertFormat(tankBlock != null, "ManLooseBlocks.OnServerDebugSpawnItem - Failed to spawn TankBlock {0}", (BlockTypes)spawnItemMessage.m_ItemType);
			DoSpawnNetBlock(null, tankBlock, 0u, clientAuthority: false);
			break;
		}
		case ObjectTypes.Chunk:
		{
			ResourcePickup resourcePickup = DoSpawnResourcePickup(spawnItemMessage.m_BlockPoolID, (ChunkTypes)spawnItemMessage.m_ItemType, spawnItemMessage.m_Pos.ScenePosition, spawnItemMessage.m_Rot);
			d.AssertFormat(resourcePickup != null, "ManLooseBlocks.OnServerDebugSpawnItem - Failed to spawn ResourcePickup {0}", (ChunkTypes)spawnItemMessage.m_ItemType);
			DoSpawnNetChunk(null, resourcePickup);
			break;
		}
		default:
			d.LogErrorFormat("DebugSpawnItem does not support object of type {0}", spawnItemMessage.m_ObjectType);
			break;
		}
	}

	private void OnClientSpawnPaintingBlockFailed(NetworkMessage netMsg)
	{
		d.Assert(Singleton.Manager<ManPointer>.inst.DraggingItem.IsNotNull() && Singleton.Manager<ManPointer>.inst.DraggingItem.block.IsNotNull(), "Server reported that we failed to spawn a painting block, but we don't seem to have done so");
		Singleton.Manager<ManPointer>.inst.RemovePaintingBlock();
	}

	private void OnServerAttachBlockRequest(NetworkMessage netMsg)
	{
		BlockAttachedMessage blockAttachedMessage = netMsg.ReadMessage<BlockAttachedMessage>();
		bool flag = false;
		NetTech component = NetworkServer.FindLocalObject(blockAttachedMessage.m_TechNetId).GetComponent<NetTech>();
		TankBlock tankBlock = FindTankBlock(blockAttachedMessage.m_BlockPoolID);
		NetPlayer sender = netMsg.GetSender();
		d.Assert(tankBlock != null, "OnServerAttachBlockRequest - Block could not be found");
		d.Assert(component != null, "OnServerAttachBlockRequest - Tech could not be found");
		d.Assert(sender.CurrentHeldBlockID == blockAttachedMessage.m_BlockPoolID, "OnServerAttachBlockRequest - Player is not holding the block they are trying to attach");
		if (component != null && tankBlock != null)
		{
			Tank tech = component.tech;
			NetBlock netBlock = tankBlock.netBlock;
			bool flag2 = sender.HasInventoryAndIsPainting(tankBlock.BlockType);
			bool flag3 = sender.CanAffordToPaint(tankBlock.BlockType);
			d.Assert(flag3, "OnServerAttachBlockRequest - Player is trying to paint a block they can't afford");
			d.Assert(sender.CurrentHeldBlockID == blockAttachedMessage.m_BlockPoolID, "OnServerAttachBlockRequest - Player asked to attach a block they aren't currently holding. Could be out of order messages or cheating.");
			d.Assert(netBlock.IsNotNull(), "OnServerAttachBlockRequest - Player asked to attach a block with no NetBlock");
			if (netBlock.IsNotNull() && sender.CurrentHeldBlockID == blockAttachedMessage.m_BlockPoolID && flag3)
			{
				if (component.CanPlayerModify(sender, sender.CurTech == null && tankBlock.IsController))
				{
					if (flag2)
					{
						sender.Inventory.HostConsumeItem(sender.PlayerID, tankBlock.BlockType);
					}
					sender.OnServerSetCurrentHeldBlock(null);
					flag = DoAttach(tech, tankBlock, blockAttachedMessage.m_BlockPosition, new OrthoRotation(blockAttachedMessage.m_BlockOrthoRotation), sender.name);
					if (flag)
					{
						Singleton.Manager<ManNetwork>.inst.ServerNetBlockAttachedToTech.Send(tech, netBlock, tankBlock);
						Singleton.Manager<ManBlockLimiter>.inst.TagAsInteresting(tech);
						tech.netTech.SaveTechData();
						Singleton.Manager<ManNetwork>.inst.SendToAllExceptClient(netMsg.conn.connectionId, TTMsgType.BlockAttach, blockAttachedMessage, alsoExcludeHost: true);
						DoDestroyNetBlock(netBlock);
					}
				}
				else
				{
					flag = false;
				}
			}
		}
		if (!flag)
		{
			BlockAttachFailMessage message = new BlockAttachFailMessage
			{
				m_BlockPoolID = blockAttachedMessage.m_BlockPoolID,
				m_TechNetId = blockAttachedMessage.m_TechNetId
			};
			Singleton.Manager<ManNetwork>.inst.SendToClient(netMsg.conn.connectionId, TTMsgType.BlockAttachFailed, message);
		}
	}

	private void OnClientAttachBlockMessage(NetworkMessage netMsg)
	{
		BlockAttachedMessage blockAttachedMessage = netMsg.ReadMessage<BlockAttachedMessage>();
		NetTech component = ClientScene.FindLocalObject(blockAttachedMessage.m_TechNetId).GetComponent<NetTech>();
		d.Assert(component != null, "OnClientBlockAttachMessage - Tech could not be found");
		if (!(component != null))
		{
			return;
		}
		Tank tech = component.tech;
		TankBlock tankBlock = FindTankBlock(blockAttachedMessage.m_BlockPoolID);
		d.Assert(tankBlock != null, "OnClientBlockAttachMessage - Block could not be found");
		if (tankBlock != null)
		{
			bool num = tankBlock.tank == tech;
			if (tankBlock.netBlock != null)
			{
				tankBlock.netBlock.Disconnect();
			}
			if (num)
			{
				d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer || component.NetPlayer == Singleton.Manager<ManNetwork>.inst.MyPlayer, "TankBlock already attached, but we are neither the server nor the instigator of the attach");
			}
			if (!num && !DoAttach(tech, tankBlock, blockAttachedMessage.m_BlockPosition, new OrthoRotation(blockAttachedMessage.m_BlockOrthoRotation)))
			{
				d.LogError("We tried to attach a block to a tech as per a message from the server, but we failed. We will now be out of sync");
			}
		}
		else
		{
			d.LogErrorFormat("Could not find block with block pool ID {0} in OnClientBlockAttached", blockAttachedMessage.m_BlockPoolID);
		}
	}

	private void OnClientAttachBlockFailed(NetworkMessage netMsg)
	{
		BlockAttachFailMessage blockAttachFailMessage = netMsg.ReadMessage<BlockAttachFailMessage>();
		TankBlock tankBlock = FindTankBlock(blockAttachFailMessage.m_BlockPoolID);
		if (tankBlock.IsNotNull())
		{
			d.AssertFormat(tankBlock.tank.IsNotNull(), "Server informed us that we failed to attach block with ID {0}, but it doesn't seem to be attached", blockAttachFailMessage.m_BlockPoolID);
			DoDetach(tankBlock, allowHeadlessTech: false);
		}
		else
		{
			d.LogErrorFormat("Server informed us that we failed to attach block with ID {0}, but it doesn't seem to be attached", blockAttachFailMessage.m_BlockPoolID);
		}
	}

	private void OnServerDetachBlockRequest(NetworkMessage netMsg)
	{
		BlockDetachMessage blockDetachMessage = netMsg.ReadMessage<BlockDetachMessage>();
		GameObject gameObject = NetworkServer.FindLocalObject(blockDetachMessage.m_TechNetId);
		NetTech netTech = ((gameObject != null) ? gameObject.GetComponent<NetTech>() : null);
		TankBlock tankBlock = FindTankBlock(blockDetachMessage.m_blockPoolID);
		bool flag = false;
		if (netTech != null && tankBlock != null)
		{
			if (!netMsg.GetSender().IsHostPlayer && !netTech.CanPlayerModify(netMsg.GetSender(), tankBlock.IsController))
			{
				d.LogWarning("Player requested a block removal from a tech, but they do not have permission");
				flag = true;
			}
			else if (tankBlock.tank == netTech.tech)
			{
				m_PlayerDoingDetach = netMsg.GetSender();
				Tank tank = tankBlock.tank;
				bool playerInitiatedRemoveBlock = tankBlock.tank.blockman.PlayerInitiatedRemoveBlock;
				tank.blockman.PlayerInitiatedRemoveBlock = blockDetachMessage.m_ManuallyRemoved;
				if (m_PlayerDoingDetach != Singleton.Manager<ManNetwork>.inst.MyPlayer && tankBlock.visible == Singleton.Manager<ManPointer>.inst.DraggingItem && tankBlock.tank != null)
				{
					Singleton.Manager<ManNetwork>.inst.ReleaseDraggedItemWithoutSendingCommand();
				}
				DoDetach(tankBlock, blockDetachMessage.m_AllowHeadlessTech, propagate: true, OnServerDetachBlockFromPropagation, blockDetachMessage.m_ManuallyRemoved ? netMsg.GetSender().name : "");
				if (tank.visible.isActive)
				{
					netTech.QueueSaveTechData();
				}
				d.Assert(tankBlock.netBlock != null, "We should've spawned a NetBlock at this point in the detach flow");
				if (tankBlock.netBlock != null && blockDetachMessage.m_AllowHeadlessTech)
				{
					bool isOwnerLoadoutBlock = netMsg.GetSender().IsLoadoutBlock(tankBlock.blockPoolID);
					tankBlock.netBlock.OnServerSetDetachInfo(tankBlock.blockPoolID, netMsg.GetSender().netId, isOwnerLoadoutBlock);
					tankBlock.netBlock.OnServerSetHeld(held: true);
					netMsg.GetSender().OnServerSetCurrentHeldBlock(tankBlock.netBlock);
					tankBlock.netBlock.AssignClientAuthority(ManNetwork.AuthorityReason.HeldVisible, netMsg.conn);
				}
				m_PlayerDoingDetach = null;
				tank.blockman.PlayerInitiatedRemoveBlock = playerInitiatedRemoveBlock;
			}
			else
			{
				d.LogWarning("Player requested a block removal from a tech, but that block is not on that tech anymore. Tell them to cancel");
				flag = true;
			}
		}
		else
		{
			d.LogWarning("Player requested a block removal, but either the block or the tech wasn't there");
			flag = true;
		}
		if (flag)
		{
			Singleton.Manager<ManNetwork>.inst.SendToClient(netMsg.conn.connectionId, TTMsgType.BlockDetachFailed, blockDetachMessage);
		}
	}

	private void OnClientDetachBlock(NetworkMessage netMsg)
	{
		BlockDetachMessage blockDetachMessage = netMsg.ReadMessage<BlockDetachMessage>();
		TankBlock tankBlock = FindTankBlock(blockDetachMessage.m_blockPoolID);
		if (!(tankBlock != null))
		{
			return;
		}
		if (tankBlock.visible == Singleton.Manager<ManPointer>.inst.DraggingItem && tankBlock.tank != null)
		{
			Singleton.Manager<ManNetwork>.inst.ReleaseDraggedItemWithoutSendingCommand();
		}
		if (isHost)
		{
			return;
		}
		d.Log("Detaching block with id: " + tankBlock.blockPoolID + " from tech " + blockDetachMessage.m_TechNetId);
		GameObject gameObject = ClientScene.FindLocalObject(blockDetachMessage.m_TechNetId);
		if (gameObject.IsNotNull())
		{
			NetTech component = gameObject.GetComponent<NetTech>();
			if (tankBlock.tank != null && tankBlock.tank == component.tech)
			{
				DoDetach(tankBlock, allowHeadlessTech: true);
			}
		}
		else
		{
			DoDetach(tankBlock, allowHeadlessTech: true);
		}
	}

	private void OnClientDetachFailed(NetworkMessage netMsg)
	{
		BlockDetachMessage blockDetachMessage = netMsg.ReadMessage<BlockDetachMessage>();
		TankBlock tankBlock = FindTankBlock(blockDetachMessage.m_blockPoolID);
		GameObject gameObject = NetworkServer.FindLocalObject(blockDetachMessage.m_TechNetId);
		NetTech netTech = ((gameObject != null) ? gameObject.GetComponent<NetTech>() : null);
		if (!(tankBlock != null) || !(netTech != null))
		{
			d.LogError("OnClientDetachFailed - Couldn't find block or tech that we supposedly failed to detach");
		}
	}

	private void OnServerDespawnBlockMessage(NetworkMessage netMsg)
	{
		RemoveBlockMessage removeBlockMessage = netMsg.ReadMessage<RemoveBlockMessage>();
		NetPlayer sender = netMsg.GetSender();
		if (sender.IsNotNull())
		{
			TankBlock tankBlock = FindTankBlock(removeBlockMessage.m_BlockPoolID);
			if (tankBlock.IsNotNull())
			{
				if (tankBlock.blockPoolID == sender.CurrentHeldBlockID)
				{
					sender.OnServerSetCurrentHeldBlock(null);
				}
				if (removeBlockMessage.m_Reason == DespawnReason.PaintingBlock)
				{
					d.Assert(tankBlock.tank.IsNull(), "Requesting despawning a painting block, but it already exists and is attached to a tech");
					if (sender.Inventory.IsNotNull())
					{
						d.Assert(sender.Inventory.CancelReserveItem(sender.PlayerID, tankBlock.BlockType), "We just despawned a painting block that we never reserved.");
					}
				}
				else if (removeBlockMessage.m_Reason == DespawnReason.ReturnToInventory && sender.Inventory.IsNotNull())
				{
					sender.Inventory.HostAddItem(tankBlock.BlockType);
				}
				if (tankBlock.netBlock.IsNotNull())
				{
					if (removeBlockMessage.m_Reason == DespawnReason.ScavengeBlock)
					{
						NetPlayer netPlayer = Singleton.Manager<ManNetwork>.inst.FindPlayerById(tankBlock.netBlock.OwnerId.Value);
						sender.CollideScavengeBlock(tankBlock.BlockType, tankBlock.netBlock.OwnerId, (netPlayer != null) ? netPlayer.TechTeamID : 0, tankBlock.netBlock.IsOwnerLoadoutBlock);
						BlockScavengedMessage message = new BlockScavengedMessage
						{
							m_BlockType = tankBlock.BlockType,
							m_Position = WorldPosition.FromScenePosition(tankBlock.transform.position),
							m_Rotation = tankBlock.transform.rotation,
							m_OriginalOwnerId = tankBlock.netBlock.OwnerId,
							m_OriginalTeamId = ((netPlayer != null) ? netPlayer.TechTeamID : 0),
							m_IsLoadoutBlock = tankBlock.netBlock.IsOwnerLoadoutBlock
						};
						Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.BlockScavenged, message, sender.netId);
					}
					DoDestroyNetBlock(tankBlock.netBlock);
					DoDestroyTankBlock(tankBlock);
				}
				else if (tankBlock.tank.IsNotNull())
				{
					Tank tank = tankBlock.tank;
					DoDestroyTankBlock(tankBlock);
					if (tank.visible.isActive)
					{
						tank.netTech.SaveTechData();
					}
				}
				else
				{
					d.LogError("TankBlock is neither on a tech or paired with a NetBlock. This should be impossible");
				}
			}
			else
			{
				d.LogError("Could not find player's painting block that they are trying to despawn");
			}
		}
		else
		{
			d.LogError("Server received RemovePaintingBlockMessage from noone. Should be fine if this is firing during network shutdown.");
		}
	}

	private void OnClientBlockDestroyMessage(NetworkMessage netMsg)
	{
		BlockRemovedFromGameMessage blockRemovedFromGameMessage = netMsg.ReadMessage<BlockRemovedFromGameMessage>();
		TankBlock tankBlock = FindTankBlock(blockRemovedFromGameMessage.m_BlockPoolID);
		if (tankBlock != null)
		{
			if (tankBlock.tank != null)
			{
				_ = tankBlock.tank.netTech;
			}
			DoDestroyTankBlock(tankBlock);
		}
		else
		{
			d.LogError("OnClientBlockDestroyMessage - Could not find block to destroy");
		}
	}

	private void OnServerDespawnChunkMessage(NetworkMessage netMsg)
	{
		EmptyBlockMessage emptyBlockMessage = netMsg.ReadMessage<EmptyBlockMessage>();
		ResourcePickup resourcePickup = FindChunk(emptyBlockMessage.m_BlockPoolID);
		if (resourcePickup.IsNotNull())
		{
			uint blockPoolID = resourcePickup.blockPoolID;
			for (int i = 0; i < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); i++)
			{
				NetPlayer player = Singleton.Manager<ManNetwork>.inst.GetPlayer(i);
				if (blockPoolID == player.CurrentHeldBlockID)
				{
					player.OnServerSetCurrentHeldBlock(null);
					break;
				}
			}
			if (resourcePickup.netChunk.IsNotNull())
			{
				DoDestroyNetChunk(resourcePickup.netChunk);
			}
			else
			{
				d.LogError("ResourcePickup isn't paired with a NetBlock. This should be impossible");
			}
			DoDestroyChunk(resourcePickup);
		}
		else
		{
			d.LogError("Server received DebugDespawnChunk Message but chunk was long gone.");
		}
	}

	private void OnClientShowExplosiveBoltAnimation(NetworkMessage netMsg)
	{
		ShowExplosiveBoltsFXMessage showExplosiveBoltsFXMessage = netMsg.ReadMessage<ShowExplosiveBoltsFXMessage>();
		TankBlock tankBlock = FindTankBlock(showExplosiveBoltsFXMessage.m_BlockId);
		if (tankBlock.IsNotNull())
		{
			ModuleDetachableLink component = tankBlock.GetComponent<ModuleDetachableLink>();
			if (component.IsNotNull())
			{
				component.PlayExplosionFX();
			}
			else
			{
				d.LogError("Block does not have detachable link module: " + tankBlock);
			}
		}
		else
		{
			d.LogError("Cannot find detachable block with id: " + showExplosiveBoltsFXMessage.m_BlockId);
		}
	}

	private void OnClientBlockAnimationChange(NetworkMessage netMsg)
	{
		BlockAnimationChange blockAnimationChange = netMsg.ReadMessage<BlockAnimationChange>();
		TankBlock tankBlock = FindTankBlock(blockAnimationChange.m_BlockPoolID);
		if (tankBlock.IsNotNull())
		{
			ModuleAnimator component = tankBlock.gameObject.GetComponent<ModuleAnimator>();
			if ((bool)component)
			{
				bool flag = false;
				switch (blockAnimationChange.m_ParameterType)
				{
				case AnimatorControllerParameterType.Float:
				{
					AnimatorFloat param4 = new AnimatorFloat(blockAnimationChange.m_ParameterName);
					component.Set(param4, BitConverter.ToSingle(blockAnimationChange.m_ParameterData, 0));
					break;
				}
				case AnimatorControllerParameterType.Int:
				{
					AnimatorInt param3 = new AnimatorInt(blockAnimationChange.m_ParameterName);
					component.Set(param3, BitConverter.ToInt32(blockAnimationChange.m_ParameterData, 0));
					break;
				}
				case AnimatorControllerParameterType.Bool:
				{
					AnimatorBool param2 = new AnimatorBool(blockAnimationChange.m_ParameterName);
					component.Set(param2, BitConverter.ToBoolean(blockAnimationChange.m_ParameterData, 0));
					break;
				}
				case AnimatorControllerParameterType.Trigger:
				{
					AnimatorTrigger param = new AnimatorTrigger(blockAnimationChange.m_ParameterName);
					flag = component.Set(param);
					break;
				}
				}
				if (!flag)
				{
					d.LogError("OnClientBlockAnimationChange msg has an invalid animation name (" + blockAnimationChange.m_ParameterName + ")");
				}
			}
			else
			{
				d.LogError("OnClientBlockAnimationChange is being called on a block without a ModuleAnimator component (" + tankBlock.name + ")");
			}
		}
		else
		{
			d.LogError("OnClientBlockAnimationChange is being called on an invalid block");
		}
	}

	public void RegisterBlockMessage<TBlockMsg>(TTMsgType msgType) where TBlockMsg : MessageBase, IBlockMessage, new()
	{
		if (!m_BlockMessageBrokers.ContainsKey(msgType))
		{
			m_BlockMessageBrokers.Add(msgType, new BlockMessageBroker<TBlockMsg>(msgType));
		}
	}

	public void SendBlockMessageToServer<TBlockMsg>(TankBlock block, TTMsgType msgType, TBlockMsg message) where TBlockMsg : MessageBase, IBlockMessage, new()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			message.BlockPoolID = block.blockPoolID;
			if (m_BlockMessageBrokers.TryGetValue(msgType, out var value))
			{
				value.SendMessage(message);
			}
		}
	}

	public TankBlock FindTankBlock(uint blockPoolID)
	{
		m_TankBlockByPoolIDLookup.TryGetValue(blockPoolID, out var value);
		if (value != null)
		{
			d.AssertFormat(value.blockPoolID == blockPoolID, "TankBlockLookup out of sync - Block at pos {0} has ID {1}", blockPoolID, value.blockPoolID);
			d.AssertFormat(value.isActiveAndEnabled, "TankBlockLookup out of sync - Returned dead block at pos {0} with type {1}", blockPoolID, value.BlockType);
		}
		return value;
	}

	public ResourcePickup FindChunk(uint blockPoolID)
	{
		m_ChunkByPoolIDLookup.TryGetValue(blockPoolID, out var value);
		if (value != null)
		{
			d.AssertFormat(value.blockPoolID == blockPoolID, "FindChunk out of sync - Chunk at pos {0} has ID {1}", blockPoolID, value.blockPoolID);
			d.AssertFormat(value.isActiveAndEnabled, "FindChunk out of sync - Returned dead chunk at pos {0} with type {1}", blockPoolID, value.ChunkType);
		}
		return value;
	}

	public Visible FindVisible(uint blockPoolID)
	{
		TankBlock tankBlock = FindTankBlock(blockPoolID);
		if (tankBlock.IsNotNull())
		{
			return tankBlock.visible;
		}
		ResourcePickup resourcePickup = FindChunk(blockPoolID);
		if (resourcePickup.IsNotNull())
		{
			return resourcePickup.visible;
		}
		return null;
	}

	private uint GenerateBlockPoolID()
	{
		uint num = uint.MaxValue;
		if (isNetworked)
		{
			if (isHost)
			{
				num = Singleton.Manager<ManNetwork>.inst.GetNextHostBlockPoolID();
			}
			else if (Singleton.Manager<ManNetwork>.inst.MyPlayer != null)
			{
				num = Singleton.Manager<ManNetwork>.inst.MyPlayer.GetNextBlockPoolID();
			}
			else
			{
				d.LogError("SpawnNewBlock - We are not host, but MyPlayer is null");
			}
		}
		else
		{
			num = m_NextSingleplayerBlockPoolID++;
		}
		d.Assert(num != uint.MaxValue, "Failed to assign block pool ID");
		return num;
	}

	private void SendSpawnPaintingBlockRequestToServer(BlockTypes type, Vector3 pos, Quaternion rot, uint blockPoolID)
	{
		uint initialSpawnShieldID = 0u;
		if (Singleton.Manager<ManNetwork>.inst.MyPlayer != null && Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech != null)
		{
			initialSpawnShieldID = Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech.InitialSpawnShieldID;
		}
		Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.SpawnPaintingBlock, new SpawnBlockMessage
		{
			m_BlockType = type,
			m_Pos = WorldPosition.FromScenePosition(in pos),
			m_Rot = rot,
			m_BlockPoolID = blockPoolID,
			m_InitialSpawnShieldID = initialSpawnShieldID
		}, NetworkInstanceId.Invalid);
	}

	private TankBlock DoSpawnTankBlock(uint blockPoolID, BlockTypes type, Vector3 pos, Quaternion rot, bool initNew = true)
	{
		TankBlock tankBlock = Singleton.Manager<ManSpawn>.inst.SpawnBlock(type, pos, rot);
		if (tankBlock != null)
		{
			tankBlock.SetBlockPoolID(blockPoolID);
			if (TankBlock.IsBlockPoolIDValid(blockPoolID))
			{
				if (m_TankBlockByPoolIDLookup.ContainsKey(blockPoolID))
				{
					d.LogError("Trying to spawn block with ID " + blockPoolID + ", but one already exists");
				}
				else
				{
					m_TankBlockByPoolIDLookup.Add(blockPoolID, tankBlock);
				}
			}
			if (initNew)
			{
				tankBlock.InitNew();
			}
		}
		else
		{
			d.LogError("Failed to spawn block in DoSpawnTankBlock type:" + type);
		}
		return tankBlock;
	}

	private NetBlock DoSpawnNetBlock(NetPlayer owner, TankBlock tankBlock, uint initialSpawnShieldID, bool clientAuthority, ManNetwork.AuthorityReason authReason = ManNetwork.AuthorityReason.HeldVisible)
	{
		NetBlock netBlock = Singleton.Manager<ManSpawn>.inst.SpawnNetBlock(tankBlock.blockPoolID, tankBlock.BlockType, tankBlock.trans.position, tankBlock.trans.rotation, tankBlock.GetSkinIndex());
		netBlock.SetScavengeTimer();
		OnClientBlockDeserialize(netBlock);
		netBlock.InitialSpawnShieldID = initialSpawnShieldID;
		if (clientAuthority)
		{
			d.Assert(owner != null, "ManLooseBlocks - Can't spawn with client authority if we don't have a client in mind");
			bool isOwnerLoadoutBlock = owner.IsLoadoutBlock(tankBlock.blockPoolID);
			netBlock.OnServerSetDetachInfo(tankBlock.blockPoolID, owner.netId, isOwnerLoadoutBlock);
			netBlock.OnServerSetHeld(held: true);
			owner.OnServerSetCurrentHeldBlock(netBlock);
			netBlock.OnServerSetAuthorityReason(authReason);
			d.Assert(netBlock.HasValidBlockPoolID(), "OnServerSpawnBlock - assigning client authority but blockPoolID is invalid");
			_ = owner.IsActuallyLocalPlayer;
			NetworkServer.SpawnWithClientAuthority(netBlock.gameObject, owner.connectionToClient);
			Singleton.Manager<ManNetwork>.inst.SetAuthorityReason(netBlock.netId, authReason);
		}
		else
		{
			NetworkServer.Spawn(netBlock.gameObject);
		}
		return netBlock;
	}

	private ResourcePickup DoSpawnResourcePickup(uint blockPoolID, ChunkTypes type, Vector3 pos, Quaternion rot, bool initNew = true, Vector3 vel = default(Vector3), Vector3 rotVel = default(Vector3))
	{
		ResourcePickup resourcePickup = Singleton.Manager<ResourceManager>.inst.SpawnResource(type, pos, rot);
		if (resourcePickup != null)
		{
			resourcePickup.setBlockPoolID(blockPoolID);
			if (TankBlock.IsBlockPoolIDValid(blockPoolID))
			{
				if (m_ChunkByPoolIDLookup.ContainsKey(blockPoolID))
				{
					d.LogError("Trying to spawn chunk with ID " + blockPoolID + ", but one already exists");
				}
				else
				{
					m_ChunkByPoolIDLookup.Add(blockPoolID, resourcePickup);
				}
			}
			resourcePickup.trans.SetParent(Singleton.dynamicContainer);
			if (initNew)
			{
				resourcePickup.InitNew(vel, rotVel);
			}
		}
		else
		{
			d.LogError("Failed to spawn chunk in DoSpawnResourcePickup type:" + type);
		}
		return resourcePickup;
	}

	private NetChunk DoSpawnNetChunk(NetPlayer owner, ResourcePickup chunk)
	{
		NetChunk netChunk = Singleton.Manager<ManSpawn>.inst.SpawnNetChunk(chunk.blockPoolID, chunk.ChunkType, chunk.trans.position, chunk.trans.rotation);
		if ((bool)netChunk)
		{
			netChunk.ConnectTo(chunk);
			NetworkServer.Spawn(netChunk.gameObject);
		}
		return netChunk;
	}

	private bool DoAttach(Tank tech, TankBlock block, IntVector3 pos, OrthoRotation rot = default(OrthoRotation), string attacher = "")
	{
		bool num = tech.blockman.AddBlockToTech(block, pos, rot);
		if (num)
		{
			d.Assert(block.tank.IsNotNull(), "Block attach registered as success, but we aren't attached to a tech");
			if (block.tank.netTech != null)
			{
				block.tank.netTech.OnBlockAttached(block);
				if (attacher != "")
				{
					block.tank.netTech.SetAuthorMultiplayerSafe(attacher);
				}
			}
		}
		return num;
	}

	private void DoDetach(TankBlock block, bool allowHeadlessTech, bool propagate = false, Action<Tank, TankBlock> detachCallback = null, string detacher = "")
	{
		Tank tank = block.tank;
		d.Assert(tank.IsNotNull(), "DoDetach - Block has no tech to detach from");
		if (!tank.IsNotNull())
		{
			return;
		}
		bool rootTransfer = true;
		tank.blockman.Detach(block, allowHeadlessTech, rootTransfer, propagate, detachCallback);
		if (tank.netTech.IsNotNull())
		{
			tank.netTech.OnBlockDetached(block);
			if (detacher != "")
			{
				tank.netTech.SetAuthorMultiplayerSafe(detacher);
			}
		}
	}

	private void DoDestroyTankBlock(TankBlock block)
	{
		if (block.tank != null)
		{
			if (ManNetwork.IsNetworked)
			{
				DoDetach(block, allowHeadlessTech: false, ManNetwork.IsHost, OnServerDetachBlockFromPropagation);
				if (block.netBlock != null)
				{
					DoDestroyNetBlock(block.netBlock);
				}
			}
			else
			{
				DoDetach(block, allowHeadlessTech: false, propagate: true);
			}
		}
		m_TankBlockByPoolIDLookup.Remove(block.blockPoolID);
		block.visible.RemoveFromGame();
	}

	private void DoDestroyNetBlock(NetBlock netBlock)
	{
		if (netBlock.block != null)
		{
			netBlock.Disconnect();
		}
		if (isHost)
		{
			netBlock.RemoveClientAuthority();
			NetworkServer.UnSpawn(netBlock.gameObject);
		}
		m_PendingAttach.Remove(netBlock);
		netBlock.transform.Recycle(worldPosStays: false);
	}

	private void DoDestroyChunk(ResourcePickup chunk)
	{
		m_ChunkByPoolIDLookup.Remove(chunk.blockPoolID);
		chunk.visible.RemoveFromGame();
	}

	private void DoDestroyNetChunk(NetChunk netChunk)
	{
		if (netChunk.chunk != null)
		{
			netChunk.Disconnect();
		}
		if (isHost)
		{
			netChunk.RemoveClientAuthority();
			NetworkServer.UnSpawn(netChunk.gameObject);
		}
		m_PendingAttach.Remove(netChunk);
		netChunk.transform.Recycle(worldPosStays: false);
	}

	private void Start()
	{
		RegisterMessageHandlers();
	}

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
	}

	public void Save(ManSaveGame.State saveState)
	{
	}

	public void ModeExit()
	{
	}
}
