#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ManNetTechs : Singleton.Manager<ManNetTechs>
{
	public Event<NetTech> OnBeforeServerSpawnTechWithAuthorityEvent;

	public Event<NetTech> OnServerTechSpawnedEvent;

	public Event<NetTech> OnServerTechUnspawnedEvent;

	public Event<NetTech> OnTechSpawnedEvent;

	public Event<NetTech> OnTechDespawnedEvent;

	private List<NetTech> m_Techs = new List<NetTech>();

	public int GetNumTechs()
	{
		return m_Techs.Count;
	}

	public NetTech GetTech(int idx)
	{
		return m_Techs[idx];
	}

	public void OnTechAdded(NetTech netTech)
	{
		d.Log($"ManNetTechs.OnTechAdded id={netTech.netId.Value} HostID={netTech.HostID} name={netTech.name}");
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			_purgeEmptyTechs();
		}
		OnTechSpawnedEvent.Send(netTech);
		m_Techs.Add(netTech);
	}

	public void OnTechRemoved(NetTech netTech)
	{
		d.Log($"ManNetTechs.OnTechRemoved Removing tech id={netTech.netId.Value} HostID={netTech.HostID} name={netTech.name}");
		m_Techs.Remove(netTech);
		OnTechDespawnedEvent.Send(netTech);
		_purgeEmptyTechs();
	}

	public void RequestControlTech(Tank tech)
	{
		d.Log("ManNetTechs.RequestControlTech - Requesting control of " + ((tech == null) ? "nothing" : tech.name));
		if (tech == null)
		{
			RequestControlTechMessage message = new RequestControlTechMessage
			{
				m_TechId = NetworkInstanceId.Invalid
			};
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.RequestTechControl, message);
		}
		else
		{
			d.Assert(tech.netTech != null, "RequestControlTech - Trying to take control of a non-networked tech in MP.");
			d.Assert(tech.Team == Singleton.Manager<ManNetwork>.inst.MyPlayer.TechTeamID, "RequestControlTech - Trying to take control of a tech on a different team");
			RequestControlTechMessage message2 = new RequestControlTechMessage
			{
				m_TechId = tech.netTech.netId
			};
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.RequestTechControl, message2);
		}
	}

	public void HostMakePlayerControlTech(NetPlayer netPlayer, NetTech netTech)
	{
		if (netTech != null)
		{
			d.Assert(netTech.NetPlayer == null || netTech.NetPlayer == netPlayer, "HostMakePlayerControlTech - Tech is already controlled by someone else");
		}
		ServerSetPlayerControllingTech_Internal(netPlayer, netTech);
	}

	private void OnServerRequestControlTech(NetworkMessage netMsg)
	{
		NetPlayer sender = netMsg.GetSender();
		RequestControlTechMessage requestControlTechMessage = netMsg.ReadMessage<RequestControlTechMessage>();
		NetTech netTech = FindTech(requestControlTechMessage.m_TechId.Value);
		d.Assert(requestControlTechMessage.m_TechId == NetworkInstanceId.Invalid || netTech != null, $"OnServerRequestControlTech - Had valid tech ID {requestControlTechMessage.m_TechId.Value} but found no tech");
		if (sender != null)
		{
			if (netTech != null)
			{
				if (netTech.NetPlayer == null)
				{
					ServerSetPlayerControllingTech_Internal(sender, netTech);
				}
				else if (netTech.NetPlayer == sender)
				{
					d.LogWarning("OnServerRequestControlTech - Player requested tech control on their current tech");
				}
				else
				{
					d.LogError("OnServerRequestControlTech - Player requested tech control on tech with a player in");
				}
			}
			else
			{
				ServerSetPlayerControllingTech_Internal(sender, null);
			}
		}
		else
		{
			d.LogWarning("OnServerRequestControlTech - Could not find player. Only valid if we are currently leaving multiplayer");
			if (netTech != null)
			{
				netTech.ServerSetOwner(null);
			}
		}
	}

	private void ServerSetPlayerControllingTech_Internal(NetPlayer netPlayer, NetTech netTech)
	{
		netPlayer.ServerSetTech(netTech, isBeingDestroyed: false);
		Singleton.Manager<ManPlayer>.inst.SetClientLastUsedTech(netPlayer, netTech);
	}

	public void OnPlayerDisconnect(NetPlayer player)
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer);
		bool flag = true;
		if (player != null && player.CurTech != null)
		{
			NetTech curTech = player.CurTech;
			if (flag)
			{
				d.Log($"ManNetTechs.OnPlayerDisconnect despawning tech ID={curTech.netId.Value} with HostID={curTech.HostID} name={curTech.name}");
				StorePlayerTech(player, despawn: true);
			}
			else
			{
				d.Log($"ManNetTechs.OnPlayerDisconnect removing authority tech ID={curTech.netId.Value} with HostID={curTech.HostID} name={curTech.name}");
				curTech.NetIdentity.RemoveClientAuthority(curTech.NetPlayer.connectionToClient);
				curTech.ServerSetOwner(null);
			}
		}
	}

	public void StorePlayerTech(NetPlayer player, bool despawn)
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer);
		if (player.CurTech.IsNotNull())
		{
			NetTech curTech = player.CurTech;
			int hostID = curTech.HostID;
			if (!Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeDeathmatch>() && ManSaveGame.ShouldStoreTechSeparatelyHandler != null && ManSaveGame.ShouldStoreTechSeparatelyHandler(hostID))
			{
				ManSaveGame.DoStoreTechSeparatelyHandler(curTech.tech.visible);
			}
			if (despawn)
			{
				bool cheatBypassInventory = Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeDeathmatch>();
				ServerUnspawnTech(hostID, cheatBypassInventory);
			}
		}
	}

	public bool CanSwitchToTech(Tank targetTank)
	{
		bool result = false;
		if (targetTank != null && targetTank != Singleton.playerTank && targetTank.Team == Singleton.Manager<ManPlayer>.inst.PlayerTeam && targetTank.visible.IsInteractible && (!(targetTank != null) || !(targetTank.netTech != null) || !(targetTank.netTech.NetPlayer != null) || !(targetTank.netTech.NetPlayer != Singleton.Manager<ManNetwork>.inst.MyPlayer)) && Singleton.Manager<ManGameMode>.inst.CanPlayerChangeTech(Singleton.Manager<ManPointer>.inst.targetTank))
		{
			result = true;
		}
		return result;
	}

	public NetTech FindTech(uint netIdValue)
	{
		for (int i = 0; i < m_Techs.Count; i++)
		{
			if (m_Techs[i].netId.Value == netIdValue)
			{
				return m_Techs[i];
			}
		}
		return null;
	}

	private void _purgeEmptyTechs()
	{
		for (int num = m_Techs.Count - 1; num >= 0; num--)
		{
			NetTech netTech = m_Techs[num];
			if (netTech.tech.blockman.blockCount == 0)
			{
				d.Log($"ManNetTechs._purgeEmptyTechs Removing tech id={netTech.netId.Value} HostID={netTech.HostID} name={netTech.name}");
				netTech.tech.blockman.RemoveTechIfEmpty();
				m_Techs.Remove(netTech);
			}
		}
	}

	private void BeforeServerSpawnNetTech(NetTech netTech)
	{
		OnBeforeServerSpawnTechWithAuthorityEvent.Send(netTech);
	}

	private void OnServerSpawnTech(NetworkMessage nm)
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer);
		SpawnTechMessage spawnTechMessage = nm.ReadMessage<SpawnTechMessage>();
		if (SKU.OverrideMpTechNames)
		{
			spawnTechMessage.m_TechData.SetNameToPlayerTechCount();
		}
		bool useInventory = !Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) || !spawnTechMessage.m_CheatBypassInventory;
		ServerSpawnTech(spawnTechMessage.m_TechData, spawnTechMessage.m_Position, spawnTechMessage.m_Rotation, spawnTechMessage.m_Team, spawnTechMessage.m_PlayerNetID, useInventory, spawnTechMessage.m_PlayerWhoCalledSpawn, spawnTechMessage.m_IsSpawnedByPlayer, spawnTechMessage.m_SpawnTechWithUnavailableBlocksMissing);
	}

	private void OnServerSwapTech(NetworkMessage nm)
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer);
		SpawnTechMessage spawnTechMessage = nm.ReadMessage<SpawnTechMessage>();
		NetPlayer sender = nm.GetSender();
		NetTech curTech = sender.CurTech;
		bool flag = !Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) || !spawnTechMessage.m_CheatBypassInventory;
		bool flag2 = true;
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCampaign() && flag)
		{
			if (curTech.IsNotNull())
			{
				flag2 = Mode<ModeCoOpCampaign>.inst.GetSharedInventory().HasItemsToSpawnTech(GetBlocksRequiredToSwap(curTech.tech, spawnTechMessage.m_TechData));
			}
			else
			{
				d.LogError("Player tried to swap tech while their current tech was null. We'll treat it as a spawn request, but it should not really happen");
				flag2 = Mode<ModeCoOpCampaign>.inst.GetSharedInventory().HasItemsToSpawnTech(spawnTechMessage.m_TechData);
			}
		}
		if (flag2)
		{
			d.Log("Swapping tech for client " + sender.name);
			if (curTech.IsNotNull())
			{
				ServerUnspawnTech(curTech.HostID, spawnTechMessage.m_CheatBypassInventory);
			}
			if (SKU.OverrideMpTechNames)
			{
				spawnTechMessage.m_TechData.SetNameToPlayerTechCount();
			}
			ServerSpawnTech(spawnTechMessage.m_TechData, spawnTechMessage.m_Position, spawnTechMessage.m_Rotation, spawnTechMessage.m_Team, spawnTechMessage.m_PlayerNetID, flag, sender.IsNotNull() ? sender.netId : spawnTechMessage.m_PlayerWhoCalledSpawn, spawnTechMessage.m_SpawnTechWithUnavailableBlocksMissing);
		}
		else
		{
			d.LogError("Client " + sender.name + " requested an invalid tech swap");
			Singleton.Manager<ManNetwork>.inst.SendToClient(sender.connectionToClient.connectionId, TTMsgType.TechSwapRejected, new EmptyMessage(), sender.netId);
		}
	}

	private BlockCountList GetBlocksRequiredToSwap(Tank swapFrom, TechData swapTo)
	{
		BlockCountList blockCountList = new BlockCountList();
		foreach (TankPreset.BlockSpec blockSpec in swapTo.m_BlockSpecs)
		{
			blockCountList.AddItem(blockSpec.GetBlockType(), 1);
		}
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator2 = swapFrom.blockman.IterateBlocks().GetEnumerator();
		while (enumerator2.MoveNext())
		{
			TankBlock current = enumerator2.Current;
			if (blockCountList.GetQuantity(current.BlockType) != 0)
			{
				blockCountList.ConsumeItem(current.BlockType, 1);
			}
		}
		return blockCountList;
	}

	public void ServerSpawnTech(TechData techData, WorldPosition pos, Quaternion rot, int team, NetworkInstanceId playerID, bool useInventory, NetworkInstanceId playerWhoCalledSpawnID, bool playerCalledSpawn = true, bool allowTechToSpawnWithMissingBlocks = false)
	{
		NetPlayer netPlayer = Singleton.Manager<ManNetwork>.inst.FindPlayerById(playerID.Value);
		NetPlayer netPlayer2 = Singleton.Manager<ManNetwork>.inst.FindPlayerById(playerWhoCalledSpawnID.Value);
		IInventory<BlockTypes> inventory = null;
		if (useInventory && Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.CoOpCampaign)
		{
			inventory = Mode<ModeCoOpCampaign>.inst.GetSharedInventory();
		}
		if (allowTechToSpawnWithMissingBlocks || inventory == null || inventory.HasItemsToSpawnTech(techData))
		{
			if (techData != null)
			{
				if (playerCalledSpawn)
				{
					techData.Author = "";
				}
				else if (techData.Author == "")
				{
					techData.Author = (netPlayer.IsNotNull() ? netPlayer.name : netPlayer2.name);
				}
			}
			if (allowTechToSpawnWithMissingBlocks)
			{
				TechData techData2 = new TechData();
				techData2.Copy(techData);
				InventoryMetaData inventoryMeta = new InventoryMetaData(inventory);
				Dictionary<BlockTypes, int> remainingBlocks = null;
				techData2.m_BlockSpecs.RemoveAll((TankPreset.BlockSpec bs) => !HasBlock(bs.GetBlockType(), inventoryMeta, ref remainingBlocks));
				techData = techData2;
			}
			TrackedVisible trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnNetworkedTechRef(techData, pos.ScenePosition, rot, team, isPopulation: false, netPlayer, BeforeServerSpawnNetTech, netPlayer2, !allowTechToSpawnWithMissingBlocks);
			NetTech netTech = trackedVisible.visible.tank.netTech;
			OnServerTechSpawnedEvent.Send(netTech);
			if (ManSpawn.IsPlayerTeam(team))
			{
				Singleton.Manager<ManBlockLimiter>.inst.TagAsInteresting(trackedVisible.visible.tank);
			}
			if (netPlayer != null && netTech.NetPlayer != netPlayer)
			{
				d.LogError("OnServerSpawnTech - We should not need to do this fixup operation!");
			}
			if (inventory == null)
			{
				return;
			}
			{
				foreach (TankPreset.BlockSpec blockSpec in techData.m_BlockSpecs)
				{
					inventory.HostConsumeItem(-1, blockSpec.GetBlockType());
				}
				return;
			}
		}
		d.LogError("Could not spawn player in tech " + techData.Name + " because inventory was not sufficiently full");
		static bool HasBlock(BlockTypes blockType, InventoryMetaData inventoryData, ref Dictionary<BlockTypes, int> blocksRemainingOfType)
		{
			if (!Singleton.Manager<ManSpawn>.inst.IsBlockAvailableForTechSpawn(blockType, inventoryData))
			{
				return false;
			}
			bool result = true;
			if (inventoryData.TakesAndStoresBlocks)
			{
				if (blocksRemainingOfType == null)
				{
					blocksRemainingOfType = new Dictionary<BlockTypes, int>();
				}
				if (!blocksRemainingOfType.TryGetValue(blockType, out var value))
				{
					value = inventoryData.GetInventoryBlockCount(blockType);
				}
				result = value == -1 || value > 0;
				blocksRemainingOfType[blockType] = ((value > 0) ? (value - 1) : value);
			}
			return result;
		}
	}

	private void OnServerUnspawnTech(NetworkMessage nm)
	{
		UnspawnTechMessage unspawnTechMessage = nm.ReadMessage<UnspawnTechMessage>();
		d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer);
		ServerUnspawnTech(unspawnTechMessage.m_HostID, unspawnTechMessage.m_CheatBypassInventory);
	}

	private void ServerUnspawnTech(int hostVisibleID, bool cheatBypassInventory)
	{
		TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(hostVisibleID);
		if (trackedVisible == null)
		{
			return;
		}
		Tank tank = (trackedVisible.visible.IsNotNull() ? trackedVisible.visible.tank : null);
		NetTech netTech = (tank.IsNotNull() ? tank.netTech : null);
		if (netTech != null)
		{
			if ((bool)netTech.NetPlayer)
			{
				netTech.NetIdentity.RemoveClientAuthority(netTech.NetPlayer.connectionToClient);
			}
			OnServerTechUnspawnedEvent.Send(netTech);
			if (netTech.NetPlayer != null)
			{
				HostMakePlayerControlTech(netTech.NetPlayer, null);
			}
			if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.CoOpCampaign && !cheatBypassInventory)
			{
				TechData techData = new TechData();
				techData.SaveTech(tank);
				Mode<ModeCoOpCampaign>.inst.GetSharedInventory().HostStoreTech(techData);
			}
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.ForceDropAll, new TargetTechMessage
			{
				m_TechId = netTech.netId
			});
			NetworkServer.UnSpawn(netTech.gameObject);
			netTech.tech.visible.RemoveFromGame();
			return;
		}
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.CoOpCampaign && !cheatBypassInventory)
		{
			TechData storedTechData = Singleton.Manager<ManVisible>.inst.GetStoredTechData(trackedVisible);
			if (storedTechData != null)
			{
				Mode<ModeCoOpCampaign>.inst.GetSharedInventory().HostStoreTech(storedTechData);
			}
		}
		Singleton.Manager<ManVisible>.inst.ObliterateTrackedVisibleFromWorld(hostVisibleID);
	}

	private void OnServerRenameTech(NetworkMessage netMsg)
	{
		SetTechNameMessage setTechNameMessage = netMsg.ReadMessage<SetTechNameMessage>();
		TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(setTechNameMessage.m_HostId);
		if (trackedVisible != null && trackedVisible.ObjectType == ObjectTypes.Vehicle)
		{
			UIScreenRenameTech.DoRenameTech(trackedVisible, setTechNameMessage.m_NewName);
			if (trackedVisible.visible.IsNotNull())
			{
				if (trackedVisible.visible.tank.IsNotNull() && trackedVisible.visible.tank.netTech.IsNotNull())
				{
					trackedVisible.visible.tank.netTech.OnServerSetName(setTechNameMessage.m_NewName);
					return;
				}
				d.LogErrorFormat("Serious error on rename: tracked visible {0} that is not a tech or not a nettech", setTechNameMessage.m_HostId);
			}
			else
			{
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.RenameTech, setTechNameMessage);
			}
		}
		else
		{
			d.LogErrorFormat("Server ignoring request to rename tech {0} because it could not be found", setTechNameMessage.m_HostId);
		}
	}

	private void OnClientRenameTech(NetworkMessage netMsg)
	{
		SetTechNameMessage setTechNameMessage = netMsg.ReadMessage<SetTechNameMessage>();
		TrackedVisible trackedVisibleByHostID = Singleton.Manager<ManVisible>.inst.GetTrackedVisibleByHostID(setTechNameMessage.m_HostId);
		if (trackedVisibleByHostID != null)
		{
			TechData storedTechData = Singleton.Manager<ManVisible>.inst.GetStoredTechData(trackedVisibleByHostID);
			if (storedTechData != null)
			{
				storedTechData.Name = setTechNameMessage.m_NewName;
				Singleton.Manager<ManTechs>.inst.TankNameChangedEvent.Send(null, trackedVisibleByHostID);
			}
			else
			{
				d.LogErrorFormat("OnClientRename tech: tracked visible {0} does not have tech data to rename", setTechNameMessage.m_HostId);
			}
		}
		else
		{
			d.LogErrorFormat("OnClientRenameTech unable to find tracked visible with ID {0}", setTechNameMessage.m_HostId);
		}
	}

	private void OnServerChangeTechRadarMarker(NetworkMessage netMsg)
	{
		SetTechRadarMarkerConfigMessage setTechRadarMarkerConfigMessage = netMsg.ReadMessage<SetTechRadarMarkerConfigMessage>();
		TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(setTechRadarMarkerConfigMessage.m_HostId);
		if (trackedVisible != null && trackedVisible.ObjectType == ObjectTypes.Vehicle)
		{
			UIScreenRenameTech.DoChangeTechRadarMarker(trackedVisible, (ManRadar.RadarMarkerColorType)setTechRadarMarkerConfigMessage.m_NewColor, (ManRadar.IconType)setTechRadarMarkerConfigMessage.m_NewIcon);
			if (trackedVisible.visible.IsNotNull())
			{
				if (trackedVisible.visible.tank.IsNotNull() && trackedVisible.visible.tank.netTech.IsNotNull())
				{
					trackedVisible.visible.tank.netTech.OnServerSetRadarMarker(new RadarMarker((ManRadar.IconType)setTechRadarMarkerConfigMessage.m_NewIcon, (ManRadar.RadarMarkerColorType)setTechRadarMarkerConfigMessage.m_NewColor));
					return;
				}
				d.LogErrorFormat("Serious error on change radar marker config: tracked visible {0} that is not a tech or not a nettech", setTechRadarMarkerConfigMessage.m_HostId);
			}
			else
			{
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.ChangeTechRadarMarker, setTechRadarMarkerConfigMessage);
			}
		}
		else
		{
			d.LogErrorFormat("Server ignoring request to change tech radar marker config {0} because it could not be found", setTechRadarMarkerConfigMessage.m_HostId);
		}
	}

	private void OnClientChangeTechRadarMarker(NetworkMessage netMsg)
	{
		SetTechRadarMarkerConfigMessage setTechRadarMarkerConfigMessage = netMsg.ReadMessage<SetTechRadarMarkerConfigMessage>();
		TrackedVisible trackedVisibleByHostID = Singleton.Manager<ManVisible>.inst.GetTrackedVisibleByHostID(setTechRadarMarkerConfigMessage.m_HostId);
		if (trackedVisibleByHostID != null)
		{
			TechData storedTechData = Singleton.Manager<ManVisible>.inst.GetStoredTechData(trackedVisibleByHostID);
			if (storedTechData != null)
			{
				storedTechData.RadarMarkerConfig = new RadarMarker((ManRadar.IconType)setTechRadarMarkerConfigMessage.m_NewIcon, (ManRadar.RadarMarkerColorType)setTechRadarMarkerConfigMessage.m_NewColor);
				return;
			}
			d.LogErrorFormat("OnClientChangeTechRadarMarker tech: tracked visible {0} does not have tech data to change radar marker config on", setTechRadarMarkerConfigMessage.m_HostId);
		}
		else
		{
			d.LogErrorFormat("OnClientChangeTechRadarMarker: unable to find tracked visible with ID {0}", setTechRadarMarkerConfigMessage.m_HostId);
		}
	}

	private void OnClientForceDropAll(NetworkMessage netMsg)
	{
		TargetTechMessage targetTechMessage = netMsg.ReadMessage<TargetTechMessage>();
		TechHolders.HolderIterator enumerator = FindTech(targetTechMessage.m_TechId.Value).tech.Holders.GetEnumerator();
		while (enumerator.MoveNext())
		{
			enumerator.Current.DropAll();
		}
	}

	private void OnClientTechDestroyed(NetworkMessage netMsg)
	{
		TargetTechMessage targetTechMessage = netMsg.ReadMessage<TargetTechMessage>();
		NetTech netTech = FindTech(targetTechMessage.m_TechId.Value);
		d.Assert(netTech.isLocalPlayer, "Expecting tech being sent through to still be the local player for this instant, until it is destroyed shortly after!");
		Singleton.Manager<ManPlayer>.inst.AddDeathLocation(netTech.tech.boundsCentreWorld.SetY(0f));
	}

	private void OnFlyTechRequest(NetworkMessage message)
	{
		FlyTechAwayRequest flyTechAwayRequest = message.ReadMessage<FlyTechAwayRequest>();
		NetTech netTech = FindTech(flyTechAwayRequest.m_TargetTechId.Value);
		if (netTech != null)
		{
			d.Log($"ManNetTechs.OnFlyTechRequest - Triggered request to show Particles at {netTech.tech.name} / {netTech.tech.boundsCentreWorld} - expected position is {flyTechAwayRequest.m_ExpectedPositionOfSmoke}");
			FlyTechAway.InitiateTakeOff(netTech.tech, flyTechAwayRequest.m_MaxLifetime, flyTechAwayRequest.m_TargetHeightWorld, null, flyTechAwayRequest.m_UseParticles ? Singleton.Manager<ManNetwork>.inst.DefaultClientFlyDespawnEffect : null);
		}
		else
		{
			d.LogWarning($"ManNetTechs.OnFlyTechRequest - Unrecognised tech ID {flyTechAwayRequest.m_TargetTechId.Value}");
		}
	}

	private void Awake()
	{
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.SpawnTech, OnServerSpawnTech);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.SwapTech, OnServerSwapTech);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.UnspawnTech, OnServerUnspawnTech);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.RenameTech, OnServerRenameTech);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.RenameTech, OnClientRenameTech);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.ChangeTechRadarMarker, OnServerChangeTechRadarMarker);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.ChangeTechRadarMarker, OnClientChangeTechRadarMarker);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.RequestTechControl, OnServerRequestControlTech);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.ForceDropAll, OnClientForceDropAll);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.TechDestroyed, OnClientTechDestroyed);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.FlyTechAwayRequest, OnFlyTechRequest);
	}
}
