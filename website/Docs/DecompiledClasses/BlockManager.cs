#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Threading;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class BlockManager : MonoBehaviour
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	private class TechSplitBuffer
	{
		private TankBlock newRootBlock;

		private List<TankBlock> blocks = new List<TankBlock>();

		private List<TankBlock> blocksToRetry = new List<TankBlock>();

		private IntVector3 rootOffset;

		private bool wasAnchored;

		public void Clear()
		{
			newRootBlock = null;
			rootOffset = IntVector3.zero;
			blocks.Clear();
			blocksToRetry.Clear();
			wasAnchored = false;
		}

		public void SetWasAnchored()
		{
			wasAnchored = true;
		}

		public void AddBlock(TankBlock block)
		{
			rootOffset += block.cachedLocalPosition;
			if ((block.IsController && (!newRootBlock || !newRootBlock.IsController)) || ((bool)block.Anchor && block.Anchor.IsAnchored && !newRootBlock))
			{
				if ((bool)newRootBlock)
				{
					blocks.Add(newRootBlock);
				}
				newRootBlock = block;
			}
			else
			{
				blocks.Add(block);
			}
		}

		public bool HasRoot()
		{
			return newRootBlock != null;
		}

		public void SpawnTech(string name, Tank previousTech)
		{
			d.Assert(newRootBlock, "TechSplitBuffer.SpawnTech - Called for " + name + " without a valid newRootBlock!");
			IntVector3 intVector = rootOffset / (blocks.Count + 1);
			ModuleAnchor.OnAttachBehaviour anchorOnAttach = ModuleAnchor.AnchorOnAttach;
			bool anchorOnAttachSnap = ModuleAnchor.AnchorOnAttachSnap;
			if (wasAnchored)
			{
				ModuleAnchor.AnchorOnAttach = ModuleAnchor.OnAttachBehaviour.AlwaysTry;
			}
			ModuleAnchor.AnchorOnAttachSnap = false;
			Vector3 position = previousTech.trans.TransformPoint(intVector);
			Tank tank = Singleton.Manager<ManSpawn>.inst.SpawnEmptyTechRef(previousTech.Team, position, previousTech.trans.rotation, grounded: false, !Singleton.Manager<ManNetwork>.inst.IsMultiplayer(), name).visible.tank;
			tank.MainCorps = previousTech.MainCorps;
			tank.control.CopySchemesFrom(previousTech.control);
			float num = Singleton.Manager<RecipeManager>.inst.GetBlockBuyPrice(newRootBlock.BlockType);
			Vector3 vector = newRootBlock.cachedLocalPosition - intVector;
			tank.blockman.AddBlockToTech(newRootBlock, vector, newRootBlock.cachedLocalRotation);
			int num2;
			do
			{
				num2 = 0;
				foreach (TankBlock block in blocks)
				{
					IntVector3 intVector2 = block.cachedLocalPosition - intVector;
					intVector2 += new IntVector3(newRootBlock.cachedLocalPosition) - vector;
					if (tank.blockman.AddBlockToTech(block, intVector2, block.cachedLocalRotation))
					{
						num2++;
						num += (float)Singleton.Manager<RecipeManager>.inst.GetBlockBuyPrice(block.BlockType);
					}
					else
					{
						blocksToRetry.Add(block);
					}
				}
				List<TankBlock> list = blocks;
				blocks = blocksToRetry;
				blocksToRetry = list;
				blocksToRetry.Clear();
			}
			while (num2 != 0 && blocks.Count != 0);
			tank.OriginalValue = num;
			previousTech.OriginalValue = Mathf.Max(0f, previousTech.OriginalValue - num);
			ModuleAnchor.AnchorOnAttachSnap = true;
			AITreeType.AITypes aiType;
			if (Singleton.playerTank != null && previousTech.visible.ID == Singleton.playerTank.visible.ID)
			{
				AITreeType.AITypes behaviorType = ((!tank.IsAnchored) ? AITreeType.AITypes.Escort : AITreeType.AITypes.Guard);
				tank.AI.SetBehaviorType(behaviorType);
			}
			else if (previousTech.AI.TryGetCurrentAIType(out aiType))
			{
				tank.AI.SetBehaviorType(aiType);
			}
			ModuleAnchor.AnchorOnAttach = anchorOnAttach;
			ModuleAnchor.AnchorOnAttachSnap = anchorOnAttachSnap;
			tank.blockman.CheckChangedAndReset();
			tank.ResetPhysics(SendEventUpdate: true);
			tank.PostSpawnEvent.Send();
			tank.rbody.velocity = previousTech.rbody.velocity;
			tank.rbody.angularVelocity = previousTech.rbody.angularVelocity;
			tank.ShouldExplodeDetachingBlocks = previousTech.ShouldExplodeDetachingBlocks;
			tank.ExplodeDetachingBlocksDelay = previousTech.ExplodeDetachingBlocksDelay;
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				TankPreset tankPreset = TankPreset.CreateInstance();
				uint[] array = new uint[tank.blockman.blockCount];
				for (int i = 0; i < tank.blockman.blockCount; i++)
				{
					array[i] = tank.blockman.allBlocks[i].blockPoolID;
				}
				tankPreset.SaveTank(tank);
				TrackedVisible trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnNetworkedTechRef(tankPreset.GetTechDataRaw(), array, tank.Team, tank.trans.position, tank.trans.rotation, null, grounded: false, tank.IsPopulation, tank.ShouldExplodeDetachingBlocks, tank.ExplodeDetachingBlocksDelay);
				if (trackedVisible.visible.IsNotNull())
				{
					NetworkServer.Spawn(trackedVisible.visible.gameObject);
				}
				tank.visible.RemoveFromGame();
			}
		}

		public void CheckDestructionOnRemainingBlocks(Tank fromTech)
		{
			foreach (TankBlock block in blocks)
			{
				block.CheckLooseDestruction(fromTech);
				block.InitRigidbody();
				block.rbody.AddRandomVelocity(Singleton.instance.globals.blockKickBasicVelocity, Singleton.instance.globals.blockKickRandomVelocity, Singleton.instance.globals.blockKickRandomAngVel);
				fromTech.blockman.m_DetachCallback?.Invoke(fromTech, block);
			}
		}
	}

	public struct BlockAttachment
	{
		public TankBlock other;

		public IntVector3 apPosLocal;

		public static int CompareBlockAttachments(BlockAttachment a, BlockAttachment b)
		{
			return a.other.visible.ID - b.other.visible.ID;
		}
	}

	public struct BlockIterator<T> where T : MonoBehaviour
	{
		public struct Enumerator
		{
			private int index;

			private BlockManager manager;

			private bool noFilter;

			private int componentIndex;

			private int tankBlockCount;

			public T Current { get; private set; }

			public Enumerator(BlockManager manager)
			{
				this = default(Enumerator);
				index = -1;
				Current = null;
				this.manager = manager;
				noFilter = typeof(T) == typeof(TankBlock);
				componentIndex = TankBlock.LookupComponentIndex<T>();
				tankBlockCount = manager.blockCount;
				if (componentIndex == int.MaxValue)
				{
					index = manager.allBlocks.Count - 1;
				}
			}

			[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
			[Il2CppSetOption(Option.NullChecks, false)]
			public bool MoveNext()
			{
				do
				{
					index++;
					if (index >= tankBlockCount)
					{
						Current = null;
						return false;
					}
					TankBlock tankBlock = manager.allBlocks[index];
					if (tankBlock.tank.IsNull())
					{
						d.Assert(manager.m_RemoveBlockRecursionCounter != 0, "Detached blocks only expected in list during recursive block removal");
						continue;
					}
					if (noFilter)
					{
						Current = tankBlock as T;
						return true;
					}
					Current = tankBlock.LookupComponentByIndex(componentIndex) as T;
				}
				while (Current.IsNull());
				return true;
			}
		}

		private BlockManager manager;

		public BlockIterator(BlockManager manager)
		{
			this.manager = manager;
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public Enumerator GetEnumerator()
		{
			return new Enumerator(manager);
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public int Count()
		{
			int num = 0;
			Enumerator enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				if ((bool)enumerator.Current)
				{
					num++;
				}
			}
			return num;
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public bool Any(Func<T, bool> predicate)
		{
			Enumerator enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				T current = enumerator.Current;
				if (predicate(current))
				{
					return true;
				}
			}
			return false;
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public T FirstOrDefault()
		{
			Enumerator enumerator = GetEnumerator();
			if (enumerator.MoveNext())
			{
				return enumerator.Current;
			}
			return null;
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public T FirstOrDefault(Func<T, bool> predicate)
		{
			Enumerator enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				T current = enumerator.Current;
				if (predicate(current))
				{
					return current;
				}
			}
			return null;
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public List<T> ToList()
		{
			List<T> list = new List<T>();
			Enumerator enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				T current = enumerator.Current;
				list.Add(current);
			}
			return list;
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public T[] ToArray()
		{
			List<T> list = new List<T>();
			Enumerator enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				T current = enumerator.Current;
				list.Add(current);
			}
			return list.ToArray();
		}
	}

	public struct TableCache
	{
		public IntVector3 blockTableCentre;

		public TankBlock[,,] blockTable;

		public byte[,,] apTable;

		public int size;

		public IntBounds bounds;
	}

	public enum RemoveAllAction
	{
		ApplyPhysicsKick,
		Recycle,
		HandOff
	}

	public EventNoParams BlockTableRecentreEvent;

	private int blockTableSize;

	private Tank tank;

	private List<TankBlock> allBlocks = new List<TankBlock>(20);

	private TankBlock rootBlock;

	private TankBlock[,,] blockTable;

	private byte[,,] m_APbitfield;

	private IntVector3 m_BlockTableCentre;

	private Bounds _blockCentreBounds;

	private static readonly Bounds k_InvalidBounds = new Bounds(Vector3.zero, Vector3.one * float.MinValue);

	private static List<BlockAttachment> s_BlockAttachmentsCache = new List<BlockAttachment>(32);

	private int m_RemoveBlockRecursionCounter;

	private TechSplitBuffer m_TechSplitBuffer = new TechSplitBuffer();

	private Action<Tank, TankBlock> m_DetachCallback;

	private static BlockManager s_ActiveMgr = null;

	private static List<TankBlock> s_LastRemovedBlocks = new List<TankBlock>();

	public int blockCount
	{
		get
		{
			d.Assert(m_RemoveBlockRecursionCounter == 0);
			return allBlocks.Count;
		}
	}

	public Bounds blockCentreBounds
	{
		get
		{
			d.Assert(m_RemoveBlockRecursionCounter == 0);
			return CheckRecalcBlockBounds();
		}
	}

	public long BlockHash { get; private set; }

	public bool changed { get; private set; }

	public bool locked { get; set; }

	public bool PlayerInitiatedRemoveBlock { get; set; }

	public int BlockRemovalSeed { get; set; }

	public bool BlockSeedLocked { get; set; }

	public List<TankBlock> LastRemovedBlocks
	{
		get
		{
			if (s_ActiveMgr != this)
			{
				s_LastRemovedBlocks.Clear();
				s_ActiveMgr = this;
			}
			return s_LastRemovedBlocks;
		}
	}

	public int IdOfFirstRemovedBlock { get; private set; }

	public static int DefaultBlockLimit
	{
		get
		{
			if (!SKU.SwitchUI)
			{
				return 64;
			}
			return 16;
		}
	}

	public static int MaxBlockLimit
	{
		get
		{
			if (!SKU.SwitchUI)
			{
				if (!Singleton.Manager<ManDLC>.inst.HasAnyDLCOfType(ManDLC.DLCType.RandD))
				{
					return 64;
				}
				return 256;
			}
			return 16;
		}
	}

	public bool IsRootBlock(TankBlock block)
	{
		return block == rootBlock;
	}

	public TankBlock GetRootBlock()
	{
		return rootBlock;
	}

	public void SetRootBlock(TankBlock block)
	{
		d.Assert(!block || block.tank == tank, "SetRootBlock fail: " + (block ? block.name : "null"));
		rootBlock = block;
	}

	public void Disintegrate(bool applyPhysicsKick = true, bool allowEmpty = false)
	{
		RemoveAllAction option = ((!applyPhysicsKick) ? RemoveAllAction.HandOff : RemoveAllAction.ApplyPhysicsKick);
		HostRemoveAllBlocks(option);
		if (!allowEmpty)
		{
			d.Assert(!Singleton.Manager<ManNetwork>.inst.IsMultiplayer(), "BlockManager.Disintegrate not supported in Multiplayer");
			if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				tank.visible.RemoveFromGame();
			}
		}
	}

	public void RecycleAll()
	{
		if (tank.netTech != null && Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			HostRemoveAllBlocks(RemoveAllAction.Recycle);
		}
		else
		{
			RemoveAllBlocks(RemoveAllAction.Recycle);
		}
	}

	public void ClearLastRemovedBlocks()
	{
		if (s_ActiveMgr == this)
		{
			s_LastRemovedBlocks.Clear();
			s_ActiveMgr = null;
		}
	}

	private void HostRemoveAllBlocks(RemoveAllAction option)
	{
		d.Assert(ManNetwork.IsHost, "Can't call HostRemoveAllBlocks on client");
		if (ManNetwork.IsNetworked && tank.netTech != null)
		{
			RemoveAllBlocksMessage message = new RemoveAllBlocksMessage
			{
				m_Action = option
			};
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptClient(-1, TTMsgType.RemoveAllBlocksFromTech, message, tank.netTech.netId, alsoExcludeHost: true);
			RemoveAllBlocks(option);
		}
		else
		{
			RemoveAllBlocks(option);
		}
	}

	public void OnClientRemoveAllBlocksMessage(NetworkMessage netMsg)
	{
		RemoveAllBlocksMessage removeAllBlocksMessage = netMsg.ReadMessage<RemoveAllBlocksMessage>();
		RemoveAllBlocks(removeAllBlocksMessage.m_Action);
	}

	private void RemoveAllBlocks(RemoveAllAction option)
	{
		if (tank.rbody.isKinematic)
		{
			tank.rbody.isKinematic = false;
		}
		m_RemoveBlockRecursionCounter++;
		bool flag = tank.visible.ManagedByTile && option != RemoveAllAction.Recycle;
		Singleton.Manager<ManSpawn>.inst.IsRemovingAllBlocks = true;
		foreach (TankBlock allBlock in allBlocks)
		{
			allBlock.OnDetach(tank, flag, flag);
			tank.NotifyBlock(allBlock, attached: false);
			allBlock.RemoveLinks();
			m_DetachCallback?.Invoke(tank, allBlock);
			allBlock.CleanupBlocksByAP();
			switch (option)
			{
			case RemoveAllAction.ApplyPhysicsKick:
				allBlock.trans.parent = null;
				AddBlockRandomVelocity(allBlock, Singleton.instance.globals.blockKickBasicVelocity, Singleton.instance.globals.blockKickRandomVelocity, Singleton.instance.globals.blockKickRandomAngVel);
				break;
			case RemoveAllAction.Recycle:
				allBlock.trans.Recycle();
				break;
			case RemoveAllAction.HandOff:
				allBlock.trans.parent = null;
				break;
			default:
				d.LogError("BlockManager.RemoveAllBlocks - No remove action found for " + option);
				break;
			}
		}
		m_RemoveBlockRecursionCounter--;
		Singleton.Manager<ManSpawn>.inst.IsRemovingAllBlocks = false;
		allBlocks.Clear();
		Array.Clear(blockTable, 0, blockTable.Length);
		Array.Clear(m_APbitfield, 0, m_APbitfield.Length);
		changed = false;
		rootBlock = null;
		_blockCentreBounds = k_InvalidBounds;
	}

	public TankBlock GetBlockWithID(uint blockPoolID)
	{
		foreach (TankBlock allBlock in allBlocks)
		{
			if (allBlock.blockPoolID == blockPoolID)
			{
				return allBlock;
			}
		}
		return null;
	}

	public TankBlock GetBlockAtPosition(IntVector3 posTankLocal)
	{
		IntVector3 intVector = posTankLocal + m_BlockTableCentre;
		if (intVector.x < 0 || intVector.x >= blockTableSize || intVector.y < 0 || intVector.y >= blockTableSize || intVector.z < 0 || intVector.z >= blockTableSize)
		{
			return null;
		}
		return blockTable[intVector.x, intVector.y, intVector.z];
	}

	public bool IsPositionValid(IntVector3 posTankLocal)
	{
		IntVector3 intVector = posTankLocal + m_BlockTableCentre;
		if (intVector.x < 0 || intVector.x >= blockTableSize || intVector.y < 0 || intVector.y >= blockTableSize || intVector.z < 0 || intVector.z >= blockTableSize)
		{
			return false;
		}
		return true;
	}

	public TankBlock GetBlockWithIndex(int index)
	{
		if (index < blockCount)
		{
			return allBlocks[index];
		}
		d.Assert(condition: false, "BlockManager: trying to access block with invalid index " + index);
		return null;
	}

	public static long CalculateBlockHash(IntVector3 indexPos, int rot, int blockID, byte skinIndex)
	{
		d.AssertFormat(indexPos.x >= 0 && indexPos.y >= 0 && indexPos.z >= 0, "CalculateBlockHash on negative position {0}", indexPos);
		d.AssertFormat(indexPos.x < 256 && indexPos.y < 256 && indexPos.z < 256, "CalculateBlockHash on position outside coord range {0}", indexPos);
		return ((17 * 31 + indexPos.GetHashCode()) * 31 + rot.GetHashCode()) * 31 + ((rot << 9) | blockID | skinIndex);
	}

	public static byte OppositeAPBit(byte apBit)
	{
		return (byte)(((apBit >>> 1) & 0x15) | ((apBit << 1) & 0x2A));
	}

	public bool RequestAttachBlock(TankBlock block, IntVector3 localPos, OrthoRotation rot = default(OrthoRotation))
	{
		bool num = Singleton.Manager<ManNetwork>.inst.IsMultiplayer();
		bool isServer = Singleton.Manager<ManNetwork>.inst.IsServer;
		bool result = false;
		uint blockPoolID = block.blockPoolID;
		if (num)
		{
			if (!isServer)
			{
				result = AddBlockToTech(block, localPos, rot);
			}
			BlockAttachedMessage message = new BlockAttachedMessage
			{
				m_TechNetId = tank.netTech.netId,
				m_BlockPosition = localPos,
				m_BlockOrthoRotation = rot,
				m_BlockPoolID = block.blockPoolID
			};
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.BlockAttach, message, tank.netTech.OwnerNetId);
			if (isServer)
			{
				result = GetBlockWithID(blockPoolID) != null;
				d.Assert(block.tank == tank, "Host called RequestAttachBlock, but the requested attachment had not happened by the end of the function");
			}
		}
		else
		{
			result = AddBlockToTech(block, localPos, rot);
		}
		return result;
	}

	public bool OnServerAttachBlock(TankBlock block, IntVector3 localPos, OrthoRotation rot = default(OrthoRotation))
	{
		d.Assert(block != null && block.netBlock != null, "TankBlock or NetBlock null");
		NetBlock netBlock = block.netBlock;
		bool num = AddBlockToTech(block, localPos, rot);
		if (num)
		{
			Singleton.Manager<ManNetwork>.inst.ServerNetBlockAttachedToTech.Send(tank, netBlock, block);
			NetworkServer.UnSpawn(netBlock.gameObject);
			netBlock.Recycle(worldPosStays: false);
			tank.netTech.SaveTechData();
		}
		return num;
	}

	public bool OnClientAttachBlock(TankBlock block, IntVector3 localPos, OrthoRotation rot = default(OrthoRotation))
	{
		bool num = GetBlockWithID(block.blockPoolID) != null;
		d.Assert(block.netBlock == null, "The NetBlock Unspawn call should have arrived by this point? Unless UNET UnSpawn is non-guaranteed");
		if (num)
		{
			d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer || tank.netTech.NetPlayer == Singleton.Manager<ManNetwork>.inst.MyPlayer, "TankBlock already attached, but we are neither the server nor the instigator of the attach");
		}
		bool flag = false;
		if (!num)
		{
			flag = AddBlockToTech(block, localPos, rot);
			if (!flag)
			{
				d.LogError("We tried to attach a block to a tech as per a message from the server, but we failed. We will now be out of sync");
			}
		}
		return flag;
	}

	public void OnClientAttachBlockFailed(uint blockPoolID)
	{
		TankBlock blockWithID = GetBlockWithID(blockPoolID);
		if (blockWithID != null)
		{
			DetachSingleBlock(blockWithID, isPropagating: false, rootTransfer: false);
		}
		else
		{
			d.LogError("Server informed us that we failed to attach block with ID " + blockPoolID + ", but it doesn't seem to be attached");
		}
	}

	public bool AddBlockToTech(TankBlock block, IntVector3 localPos, OrthoRotation rot = default(OrthoRotation), bool allowMissingAPLinks = false)
	{
		bool flag = false;
		s_BlockAttachmentsCache.Clear();
		if (blockCount != 0)
		{
			bool num = TryGetBlockAttachments(block, localPos, rot, s_BlockAttachmentsCache);
			bool flag2 = s_BlockAttachmentsCache.Count > 0;
			if (!num || !(flag2 || allowMissingAPLinks))
			{
				return false;
			}
		}
		IntVector3 intVector = IntVector3.one * (blockTableSize - 1);
		IntVector3 intVector2 = IntVector3.zero;
		IntVector3[] filledCells;
		for (int i = 0; i < 2; i++)
		{
			IntVector3 intVector3 = IntVector3.zero;
			IntVector3 intVector4 = IntVector3.zero;
			filledCells = block.filledCells;
			foreach (IntVector3 intVector5 in filledCells)
			{
				IntVector3 intVector6 = localPos + m_BlockTableCentre + rot * intVector5;
				intVector3 = IntVector3.Max(intVector3, intVector6 - intVector);
				intVector4 = IntVector3.Min(intVector4, intVector6);
			}
			if (!(intVector3 != IntVector3.zero) && !(intVector4 != IntVector3.zero))
			{
				break;
			}
			if (i == 0)
			{
				intVector2 = -blockCentreBounds.center;
				IntVector3 intVector7 = -(intVector3 + intVector4);
				for (int k = 0; k < 3; k++)
				{
					if (intVector7[k] > 0)
					{
						intVector2[k] = Mathf.Max(intVector2[k], intVector7[k]);
					}
					if (intVector7[k] < 0)
					{
						intVector2[k] = Mathf.Min(intVector2[k], intVector7[k]);
					}
				}
				RecentreBlockTable(intVector2);
				localPos += intVector2;
				continue;
			}
			d.LogError(block.name + ": out of bounds failure");
			return false;
		}
		flag = true;
		IntVector3 intVector8 = localPos + m_BlockTableCentre;
		filledCells = block.filledCells;
		foreach (IntVector3 intVector9 in filledCells)
		{
			IntVector3 intVector10 = localPos + rot * intVector9;
			_blockCentreBounds.Encapsulate(intVector10);
			IntVector3 intVector11 = m_BlockTableCentre + intVector10;
			blockTable[intVector11.x, intVector11.y, intVector11.z] = block;
		}
		for (int l = 0; l < block.attachPoints.Length; l++)
		{
			IntVector3 filledCellForAPIndex = block.GetFilledCellForAPIndex(l);
			IntVector3 intVector12 = (IntVector3)(block.attachPoints[l] * 2f) - filledCellForAPIndex - filledCellForAPIndex;
			IntVector3 intVector13 = intVector8 + rot * filledCellForAPIndex;
			byte b = (rot * intVector12).APHalfBits();
			m_APbitfield[intVector13.x, intVector13.y, intVector13.z] |= b;
		}
		BlockHash ^= CalculateBlockHash(intVector8, rot, block.visible.ItemType, Singleton.Manager<ManCustomSkins>.inst.SkinIndexToID(block.GetSkinIndex(), Singleton.Manager<ManSpawn>.inst.GetCorporation(block.BlockType)));
		block.trans.localRotation = rot;
		block.trans.localPosition = localPos;
		block.trans.SetParent(tank.trans, worldPositionStays: false);
		allBlocks.Add(block);
		block.LinkTo(s_BlockAttachmentsCache, rot, intVector2 * 2);
		Vector3 centerOfMass = tank.rbody.centerOfMass;
		if (rootBlock == null)
		{
			rootBlock = block;
		}
		changed = true;
		if (tank.rbody.isKinematic)
		{
			tank.rbody.isKinematic = false;
		}
		if (tank.netTech != null)
		{
			block.HasValidBlockPoolID();
		}
		block.OnAttach(tank);
		tank.CheckKinematic();
		tank.rbody.centerOfMass = centerOfMass;
		tank.NotifyBlock(block, attached: true);
		return flag;
	}

	public TableCache GetTableCacheForPlacementCollection()
	{
		IntVector3 intVector = new IntVector3(blockCentreBounds.min);
		IntBounds bounds = new IntBounds(intVector, intVector + (IntVector3)blockCentreBounds.size + IntVector3.one);
		return new TableCache
		{
			blockTable = blockTable,
			apTable = m_APbitfield,
			blockTableCentre = m_BlockTableCentre,
			size = blockTableSize,
			bounds = bounds
		};
	}

	public bool TryGetBlockAttachments(TankBlock block, IntVector3 localPos, OrthoRotation rot, List<BlockAttachment> outAttachments)
	{
		outAttachments.Clear();
		IntVector3 intVector = localPos + m_BlockTableCentre;
		Bounds bounds = rot * block.BlockCellBounds;
		IntBounds other = new IntBounds(bounds.min, bounds.max + IntVector3.one).Translate(intVector);
		IntVector3 intVector2 = new IntVector3(Mathf.CeilToInt(blockCentreBounds.min.x), Mathf.CeilToInt(blockCentreBounds.min.y), Mathf.CeilToInt(blockCentreBounds.min.z));
		IntBounds intBounds = new IntBounds(intVector2, intVector2 + (IntVector3)blockCentreBounds.size + IntVector3.one).Translate(m_BlockTableCentre).Union(other);
		if (intBounds.size.x > blockTableSize || intBounds.size.y > blockTableSize || intBounds.size.z > blockTableSize)
		{
			return false;
		}
		for (int i = 0; i < block.filledCells.Length; i++)
		{
			IntVector3 intVector3 = intVector + rot * block.filledCells[i];
			if (intVector3.x >= 0 && intVector3.x < blockTableSize && intVector3.y >= 0 && intVector3.y < blockTableSize && intVector3.z >= 0 && intVector3.z < blockTableSize && blockTable[intVector3.x, intVector3.y, intVector3.z] != null)
			{
				return false;
			}
		}
		for (int j = 0; j < block.attachPoints.Length; j++)
		{
			IntVector3 filledCellForAPIndex = block.GetFilledCellForAPIndex(j);
			IntVector3 intVector4 = block.attachPoints[j] * 2f;
			IntVector3 intVector5 = intVector4 - filledCellForAPIndex - filledCellForAPIndex;
			IntVector3 intVector6 = intVector + rot * (filledCellForAPIndex + intVector5);
			if (intVector6.x < 0 || intVector6.x >= blockTableSize || intVector6.y < 0 || intVector6.y >= blockTableSize || intVector6.z < 0 || intVector6.z >= blockTableSize)
			{
				continue;
			}
			byte b = m_APbitfield[intVector6.x, intVector6.y, intVector6.z];
			if (b != 0)
			{
				byte apBit = (rot * intVector5).APHalfBits();
				if ((b & OppositeAPBit(apBit)) != 0)
				{
					outAttachments.Add(new BlockAttachment
					{
						other = blockTable[intVector6.x, intVector6.y, intVector6.z],
						apPosLocal = localPos + localPos + rot * intVector4
					});
				}
			}
		}
		return true;
	}

	public void PrepareNetRemoval(int localBlockVisibleIdToRemove)
	{
		s_LastRemovedBlocks.Clear();
		s_ActiveMgr = this;
		IdOfFirstRemovedBlock = localBlockVisibleIdToRemove;
		if (!BlockSeedLocked)
		{
			BlockRemovalSeed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
		}
	}

	public void Detach(TankBlock block, bool allowHeadlessTech, bool rootTransfer, bool propagate, Action<Tank, TankBlock> detachCallback = null)
	{
		d.Assert(m_DetachCallback == null, "Setting a new detach callback in BlockManager when we already have one");
		m_DetachCallback = detachCallback;
		if (propagate)
		{
			DetachBlockAndRestructure(block, rootTransfer, allowHeadlessTech);
		}
		else
		{
			DetachSingleBlock(block, isPropagating: false, rootTransfer);
		}
		m_DetachCallback = null;
	}

	public void DetachSingleBlock(TankBlock block, bool isPropagating, bool rootTransfer, bool cleanup = true)
	{
		d.Assert(block.tank == tank, "BlockManager.RemoveBlock. Trying to remove a block that doesn't belong to this tech");
		m_RemoveBlockRecursionCounter++;
		if (tank.rbody.isKinematic)
		{
			tank.rbody.isKinematic = false;
		}
		block.OnDetach(tank, !isPropagating, resumeTileManagement: true);
		AddLastRemovedBlock(block);
		IntVector3 intVector = new IntVector3(block.cachedLocalPosition) + m_BlockTableCentre;
		IntVector3[] filledCells = block.filledCells;
		foreach (IntVector3 intVector2 in filledCells)
		{
			IntVector3 intVector3 = intVector + new IntVector3(block.cachedLocalRotation * intVector2);
			d.Assert(blockTable[intVector3.x, intVector3.y, intVector3.z] == block, $"Block {block.name} not found at {intVector3}");
			blockTable[intVector3.x, intVector3.y, intVector3.z] = null;
			m_APbitfield[intVector3.x, intVector3.y, intVector3.z] = 0;
		}
		if (rootBlock == block && !rootTransfer)
		{
			rootBlock = null;
		}
		changed = true;
		_blockCentreBounds = k_InvalidBounds;
		BlockHash ^= CalculateBlockHash(intVector, block.cachedLocalRotation, block.visible.ItemType, Singleton.Manager<ManCustomSkins>.inst.SkinIndexToID(block.GetSkinIndex(), Singleton.Manager<ManSpawn>.inst.GetCorporation(block.BlockType)));
		tank.NotifyBlock(block, attached: false);
		if (!Singleton.Manager<ComponentPool>.inst.IsShuttingDown)
		{
			block.trans.parent = null;
		}
		block.RemoveLinks();
		FixupBlockRefs();
		if (!isPropagating)
		{
			m_DetachCallback?.Invoke(tank, block);
			block.DetachedEvent.Send();
		}
		m_RemoveBlockRecursionCounter--;
		if (cleanup)
		{
			block.CleanupBlocksByAP();
		}
	}

	private void DetachBlockAndRestructure(TankBlock block, bool rootTransfer, bool allowHeadlessTech = false)
	{
		bool isAnchored = tank.IsAnchored;
		DetachSingleBlock(block, isPropagating: false, rootTransfer, cleanup: false);
		RestructureFollowingBlockDetach(block, rootTransfer, isAnchored);
		block.CleanupBlocksByAP();
		FixupAfterRemovingBlocks(allowHeadlessTech);
		RemoveTechIfEmpty();
		if (rootBlock != null && (bool)block.rbody)
		{
			block.rbody.AddRandomVelocity(Singleton.instance.globals.blockKickBasicVelocity, Singleton.instance.globals.blockKickRandomVelocity, Singleton.instance.globals.blockKickRandomAngVel);
		}
	}

	public void RestructureFollowingBlockDetach(TankBlock block, bool rootTransfer, bool techWasAnchored)
	{
		if (rootTransfer && IsRootBlock(block))
		{
			if (tank.control.HasController)
			{
				SetRootBlock(tank.control.FirstController.block);
			}
			else
			{
				ModuleAnchor firstAnchored = tank.Anchors.FirstAnchored;
				if ((bool)firstAnchored)
				{
					SetRootBlock(firstAnchored.block);
				}
				else
				{
					SetRootBlock(null);
				}
			}
		}
		block.TransferRoot(rootTransfer ? tank : null);
		TechSplitNamer techSplitNamer = null;
		TankBlock[] connectedBlocksByAP = block.ConnectedBlocksByAP;
		foreach (TankBlock tankBlock in connectedBlocksByAP)
		{
			if ((bool)tankBlock && (bool)tankBlock.tank && !tankBlock.CanReachRoot())
			{
				CleanupInvalidBlockOnTech(tankBlock, ref techSplitNamer, techWasAnchored);
			}
		}
	}

	public void CleanupInvalidTechBlocks()
	{
		bool isAnchored = tank.IsAnchored;
		TechSplitNamer techSplitNamer = null;
		bool flag;
		do
		{
			flag = false;
			int num = allBlocks.FindIndex((TankBlock b) => b.NumConnectedAPs == 0 || !b.CanReachRoot());
			if (num >= 0)
			{
				CleanupInvalidBlockOnTech(allBlocks[num], ref techSplitNamer, isAnchored);
				flag = true;
			}
		}
		while (flag);
		FixupAfterRemovingBlocks(allowHeadlessTech: false);
	}

	public void CleanupInvalidBlockOnTech(TankBlock block, bool techIsAnchored = false)
	{
		TechSplitNamer techSplitNamer = null;
		CleanupInvalidBlockOnTech(block, ref techSplitNamer, techIsAnchored);
	}

	public void CleanupInvalidBlockOnTech(TankBlock block, ref TechSplitNamer techSplitNamer, bool techIsAnchored)
	{
		m_TechSplitBuffer.Clear();
		if (techIsAnchored)
		{
			m_TechSplitBuffer.SetWasAnchored();
		}
		block.DetachRecursively(PreRecurseActionRemove, PostRecurseActionClearConnectedBlocks);
		if (m_TechSplitBuffer.HasRoot())
		{
			if (techSplitNamer == null)
			{
				techSplitNamer = new TechSplitNamer(tank);
			}
			string text = techSplitNamer.CreateNextName();
			m_TechSplitBuffer.SpawnTech(text, tank);
		}
		m_TechSplitBuffer.CheckDestructionOnRemainingBlocks(tank);
	}

	private void PreRecurseActionRemove(TankBlock block)
	{
		m_TechSplitBuffer.AddBlock(block);
		DetachSingleBlock(block, isPropagating: true, rootTransfer: false, cleanup: false);
	}

	private void PostRecurseActionClearConnectedBlocks(TankBlock block)
	{
		block.CleanupBlocksByAP();
	}

	private void FixupBlockRefs()
	{
		int num = 0;
		for (int i = 0; i < allBlocks.Count; i++)
		{
			if (!(allBlocks[i].tank != tank))
			{
				if (i != num)
				{
					allBlocks[num] = allBlocks[i];
				}
				num++;
			}
		}
		if (num != allBlocks.Count)
		{
			allBlocks.RemoveRange(num, allBlocks.Count - num);
		}
	}

	public void FixupAfterRemovingBlocks(bool allowHeadlessTech)
	{
		FixupBlockRefs();
		if (!allowHeadlessTech && !tank.control.HasController && !tank.IsAnchored && tank.Anchors.NumIsAnchored <= 0)
		{
			HostRemoveAllBlocks(RemoveAllAction.ApplyPhysicsKick);
		}
		if (blockCount == 0)
		{
			d.Assert(tank.blockman.blockCount == 0);
			tank.EnableGravity = false;
		}
	}

	public void RemoveTechIfEmpty()
	{
		if (blockCount == 0)
		{
			bool flag = Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && !Singleton.Manager<ManNetwork>.inst.IsServer;
			d.Log($"BlockManager.RemoveTechIfEmpty name={tank.name} visibleId={tank.visible.ID} netId={(tank.netTech ? tank.netTech.netId.Value : 0u)} isMultiplayerClient={flag}");
			if (!flag)
			{
				tank.FatalDamage = tank.DamageInEffect;
				tank.WasPlayerControlledAtFatalDamageTime = tank.IsPlayer || (tank.netTech != null && tank.netTech.NetPlayer != null);
				if (tank.WasPlayerControlledAtFatalDamageTime && tank.netTech != null && tank.netTech.NetPlayer != null)
				{
					tank.ConnectionIdOnFatalDamage = tank.netTech.NetPlayer.connectionToClient.connectionId;
				}
				tank.visible.RemoveFromGame();
			}
			else
			{
				Singleton.Manager<ManNetwork>.inst.AddToDisposalContainer(tank.trans);
				if (Singleton.playerTank == tank)
				{
					Singleton.Manager<ManTechs>.inst.SetPlayerTankLocally(null);
				}
			}
			return;
		}
		tank.CheckKinematic();
		if (tank.ControllableByAnyPlayer || PlayerInitiatedRemoveBlock)
		{
			return;
		}
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			if (tank.netTech.IsNotNull() && tank.netTech.NetPlayer.IsNotNull())
			{
				Singleton.Manager<ManNetTechs>.inst.HostMakePlayerControlTech(tank.netTech.NetPlayer, null);
			}
		}
		else if (tank == Singleton.playerTank)
		{
			Singleton.Manager<ManTechs>.inst.SetPlayerTankLocally(null);
		}
	}

	public void FixupOnRelease()
	{
		if (!tank.control.HasController && !tank.IsAnchored && allBlocks.Count > 0)
		{
			Singleton.Manager<ManLooseBlocks>.inst.RequestDetachBlock(allBlocks[0], allowHeadlessTech: false, manualRemove: true);
		}
		if (tank.netTech.IsNotNull() && tank.netTech.NetPlayer.IsNotNull() && tank.netTech.NetPlayer.IsActuallyLocalPlayer && !tank.ControllableByAnyPlayer)
		{
			Singleton.Manager<ManNetTechs>.inst.RequestControlTech(null);
		}
	}

	public Bounds CheckRecalcBlockBounds()
	{
		d.Assert(Thread.CurrentThread.ManagedThreadId == Singleton.instance.MainThreadID, "can't recalc block bounds off main thread");
		if (_blockCentreBounds == k_InvalidBounds)
		{
			if (allBlocks.Count == 0)
			{
				return default(Bounds);
			}
			foreach (TankBlock allBlock in allBlocks)
			{
				IntVector3[] filledCells = allBlock.filledCells;
				foreach (IntVector3 intVector in filledCells)
				{
					_blockCentreBounds.Encapsulate(allBlock.cachedLocalPosition + allBlock.cachedLocalRotation * intVector);
				}
			}
		}
		return _blockCentreBounds;
	}

	private void CascadeBlockTable(IntVector3 coordOffset, TankBlock[,,] tableSource, TankBlock[,,] tableDest, byte[,,] apSource, byte[,,] apDest)
	{
		Bounds bounds = blockCentreBounds;
		if (bounds == k_InvalidBounds)
		{
			d.Assert(condition: false, "BlockManager.CascadeBlockTable has invalid bounds");
		}
		else
		{
			if (blockCount == 0)
			{
				return;
			}
			bounds.center += Vector3.one * blockTableSize * 0.5f;
			bounds.min = Vector3.Max(bounds.min, Vector3.zero);
			bounds.max = Vector3.Min(bounds.max, Vector3.one * (blockTableSize - 1));
			IntVector3 intVector = default(IntVector3);
			intVector.x = ((coordOffset.x < 0) ? ((int)bounds.min.x) : ((int)bounds.max.x));
			intVector.y = ((coordOffset.y < 0) ? ((int)bounds.min.y) : ((int)bounds.max.y));
			intVector.z = ((coordOffset.z < 0) ? ((int)bounds.min.z) : ((int)bounds.max.z));
			IntVector3 intVector2 = default(IntVector3);
			intVector2.x = ((coordOffset.x < 0) ? ((int)bounds.max.x + 1) : ((int)bounds.min.x - 1));
			intVector2.y = ((coordOffset.y < 0) ? ((int)bounds.max.y + 1) : ((int)bounds.min.y - 1));
			intVector2.z = ((coordOffset.z < 0) ? ((int)bounds.max.z + 1) : ((int)bounds.min.z - 1));
			IntVector3 intVector3 = default(IntVector3);
			intVector3.x = ((coordOffset.x < 0) ? 1 : (-1));
			intVector3.y = ((coordOffset.y < 0) ? 1 : (-1));
			intVector3.z = ((coordOffset.z < 0) ? 1 : (-1));
			IntVector3 intVector4 = default(IntVector3);
			intVector4.x = intVector.x;
			while (intVector4.x != intVector2.x)
			{
				intVector4.y = intVector.y;
				while (intVector4.y != intVector2.y)
				{
					intVector4.z = intVector.z;
					while (intVector4.z != intVector2.z)
					{
						IntVector3 intVector5 = intVector4 + coordOffset;
						tableDest[intVector5.x, intVector5.y, intVector5.z] = tableSource[intVector4.x, intVector4.y, intVector4.z];
						tableSource[intVector4.x, intVector4.y, intVector4.z] = null;
						apDest[intVector5.x, intVector5.y, intVector5.z] = apSource[intVector4.x, intVector4.y, intVector4.z];
						apSource[intVector4.x, intVector4.y, intVector4.z] = 0;
						intVector4.z += intVector3.z;
					}
					intVector4.y += intVector3.y;
				}
				intVector4.x += intVector3.x;
			}
		}
	}

	private void RecentreBlockTable(IntVector3 coordOffset)
	{
		RecentreAndCascadeBlockTable(coordOffset, blockTable, blockTable, m_APbitfield, m_APbitfield);
	}

	private void RecentreAndCascadeBlockTable(IntVector3 coordOffset, TankBlock[,,] tableSource, TankBlock[,,] tableDest, byte[,,] apSource, byte[,,] apDest)
	{
		if (!(coordOffset == IntVector3.zero))
		{
			Vector3 vector = coordOffset;
			BlockIterator<TankBlock>.Enumerator enumerator = IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.MoveLocalPositionWhileAttached(vector);
			}
			CascadeBlockTable(coordOffset, tableSource, tableDest, apSource, apDest);
			_blockCentreBounds.center += vector;
			tank.trans.localPosition -= tank.trans.TransformDirection(vector);
			BlockTableRecentreEvent.Send();
		}
	}

	public bool SetTableSize(int newSize)
	{
		d.Assert(newSize <= MaxBlockLimit, $"Unexpected block size ({newSize}) that is greater than the maximum allowed block size set ({MaxBlockLimit})!");
		if (newSize == blockTableSize)
		{
			return false;
		}
		TankBlock[,,] tableDest;
		byte[,,] array;
		if (newSize > blockTable.GetLength(0))
		{
			d.LogWarning(base.name + ": reallocating block table (POSSIBLE OPTIM?)");
			tableDest = new TankBlock[newSize, newSize, newSize];
			array = new byte[newSize, newSize, newSize];
		}
		else
		{
			tableDest = blockTable;
			array = m_APbitfield;
		}
		bool result = false;
		if (blockCount > 0)
		{
			IntVector3 intVector = -blockCentreBounds.center + Vector3.one * 0.5f;
			IntVector3 intVector2 = -IntVector3.one * (blockTableSize - newSize) / 2;
			if (newSize < blockTableSize)
			{
				RecentreBlockTable(intVector);
				int num = (blockTableSize - newSize) / 2;
				int num2 = num + newSize;
				HashSet<TankBlock> hashSet = null;
				IntVector3 intVector3 = default(IntVector3);
				intVector3.x = 0;
				while (intVector3.x < blockTableSize)
				{
					intVector3.y = 0;
					while (intVector3.y < blockTableSize)
					{
						intVector3.z = 0;
						while (intVector3.z < blockTableSize)
						{
							if (intVector3.x < num || intVector3.x >= num2 || intVector3.y < num || intVector3.y >= num2 || intVector3.z < num || intVector3.z >= num2)
							{
								TankBlock tankBlock = blockTable[intVector3.x, intVector3.y, intVector3.z];
								if (tankBlock.IsNotNull())
								{
									if (hashSet == null)
									{
										hashSet = new HashSet<TankBlock>();
									}
									hashSet.Add(tankBlock);
								}
							}
							intVector3.z++;
						}
						intVector3.y++;
					}
					intVector3.x++;
				}
				if (hashSet != null)
				{
					foreach (TankBlock item in hashSet)
					{
						DetachSingleBlock(item, isPropagating: false, rootTransfer: false);
						result = true;
					}
				}
				CascadeBlockTable(intVector2, blockTable, tableDest, m_APbitfield, array);
			}
			else
			{
				RecentreAndCascadeBlockTable(intVector + intVector2, blockTable, tableDest, m_APbitfield, array);
			}
		}
		blockTable = tableDest;
		blockTableSize = newSize;
		m_APbitfield = array;
		m_BlockTableCentre = new IntVector3(blockTableSize, blockTableSize, blockTableSize) / 2;
		return result;
	}

	public bool CheckChangedAndReset()
	{
		if (changed)
		{
			changed = false;
			return true;
		}
		return false;
	}

	public BlockIterator<TankBlock> IterateBlocks()
	{
		return new BlockIterator<TankBlock>(this);
	}

	public BlockIterator<T> IterateBlockComponents<T>() where T : MonoBehaviour
	{
		return new BlockIterator<T>(this);
	}

	private void AddBlockRandomVelocity(TankBlock block, Vector3 baseVel, Vector3 randomVel, float randomAngVel)
	{
		UnityEngine.Random.State state = default(UnityEngine.Random.State);
		bool num = Singleton.Manager<ManNetwork>.inst.IsMultiplayer();
		if (num)
		{
			state = UnityEngine.Random.state;
			UnityEngine.Random.InitState(BlockRemovalSeed);
		}
		block.rbody.AddRandomVelocity(Singleton.instance.globals.blockKickBasicVelocity, Singleton.instance.globals.blockKickRandomVelocity, Singleton.instance.globals.blockKickRandomAngVel);
		if (num)
		{
			UnityEngine.Random.state = state;
		}
	}

	public IEnumerable<IntVector3> GetLowestOccupiedCells()
	{
		int x = 0;
		while (x < blockTableSize)
		{
			int num;
			for (int z = 0; z < blockTableSize; z = num)
			{
				for (int i = 0; i < blockTableSize; i++)
				{
					if (blockTable[x, i, z].IsNotNull())
					{
						yield return new IntVector3(x, i, z);
						break;
					}
				}
				num = z + 1;
			}
			num = x + 1;
			x = num;
		}
	}

	private void AddLastRemovedBlock(TankBlock removed)
	{
		if (s_ActiveMgr != this)
		{
			s_LastRemovedBlocks.Clear();
			s_ActiveMgr = this;
		}
		s_LastRemovedBlocks.Add(removed);
	}

	private void OnPool()
	{
		blockTableSize = DefaultBlockLimit;
		blockTable = new TankBlock[blockTableSize, blockTableSize, blockTableSize];
		m_BlockTableCentre = new IntVector3(blockTableSize, blockTableSize, blockTableSize) / 2;
		m_APbitfield = new byte[blockTableSize, blockTableSize, blockTableSize];
		tank = GetComponent<Tank>();
	}

	private void OnSpawn()
	{
		allBlocks.Clear();
		BlockHash = 0L;
		rootBlock = null;
		changed = false;
		locked = false;
		m_RemoveBlockRecursionCounter = 0;
		_blockCentreBounds = k_InvalidBounds;
		SetTableSize(Singleton.Manager<ManSpawn>.inst.BlockLimit);
		Array.Clear(blockTable, 0, blockTable.Length);
	}

	private void OnDrawGizmos()
	{
		if (!Application.IsPlaying(base.gameObject) || !base.gameObject.EditorSelectedSingle())
		{
			return;
		}
		Gizmos.color = new Color(1f, 0f, 1f, 0.3f);
		Gizmos.matrix = tank.trans.localToWorldMatrix;
		for (int i = 0; i < blockTableSize; i++)
		{
			for (int j = 0; j < blockTableSize; j++)
			{
				for (int k = 0; k < blockTableSize; k++)
				{
					if (blockTable[i, j, k] != null)
					{
						Gizmos.DrawCube(new Vector3(i, j, k) - m_BlockTableCentre, Vector3.one * 0.95f);
					}
				}
			}
		}
	}
}
