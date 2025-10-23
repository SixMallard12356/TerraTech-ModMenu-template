#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.Networking;

public class ModeKingAnton : ModePVP<ModeKingAnton>
{
	private TankBlock m_BlockToTrack;

	private Tank m_TechToUpdateTimer;

	private float m_LastAttachTime;

	private float m_NotableBlockHeldTime;

	private ModuleItemDispenser m_DispenserBlock;

	public override MultiplayerModeType GetMultiplayerGameType()
	{
		return MultiplayerModeType.KingAnton;
	}

	public override string GetGameMode()
	{
		return "ModeKingAnton";
	}

	public override ManGameMode.GameType GetGameType()
	{
		return ManGameMode.GameType.Deathmatch;
	}

	protected override void GameModeInit()
	{
		m_BlockToTrack = null;
		m_TechToUpdateTimer = null;
		m_LastAttachTime = 0f;
		m_NotableBlockHeldTime = 0f;
		m_DispenserBlock = null;
	}

	protected override void OnClientPhaseEnter(NetController.Phase phase)
	{
		base.OnClientPhaseEnter(phase);
	}

	protected override void OnServerPhaseEnter(NetController.Phase phase)
	{
		base.OnServerPhaseEnter(phase);
		if (phase == NetController.Phase.Intro)
		{
			SpawnDispensers();
			Singleton.Manager<ManNetwork>.inst.ServerNetBlockAttachedToTech.Subscribe(OnServerBlockAttachedToTech);
			Singleton.Manager<ManNetwork>.inst.ServerBlockRemovedFromTechTech.Subscribe(OnServerBlockRemovedFromTech);
		}
	}

	protected override void ServerUpdateGameMode()
	{
		if (m_TechToUpdateTimer != null && (bool)m_TechToUpdateTimer.netTech.NetPlayer)
		{
			float addValue = Time.time - m_LastAttachTime;
			m_TechToUpdateTimer.netTech.NetPlayer.OnServerAddPoints(addValue);
			m_LastAttachTime = Time.time;
		}
		UpdateNotableBlockHoggerWatchdog();
	}

	protected override void ClientUpdateGameMode()
	{
		bool flag = false;
		if (Singleton.Manager<ManNetwork>.inst.MyPlayer != null)
		{
			NetPlayer netPlayer = null;
			NetTech notableTech = Singleton.Manager<ManNetwork>.inst.NetController.GetNotableTech();
			if (notableTech != null)
			{
				netPlayer = notableTech.NetPlayer;
				if (notableTech.tech == Singleton.playerTank)
				{
					flag = true;
				}
			}
			Singleton.Manager<ManNetwork>.inst.MyPlayer.UpdateNotablePlayerHUD(netPlayer);
		}
		if (flag)
		{
			Singleton.Manager<ManPurchases>.inst.ShowPalette(show: false);
		}
		else
		{
			Singleton.Manager<ManPurchases>.inst.ShowPalette(show: true);
		}
	}

	protected override void OnServerPlayerOutOfBounds()
	{
		if (m_BlockToTrack != null && m_BlockToTrack.tank != null && m_BlockToTrack.tank == Singleton.playerTank)
		{
			Singleton.Manager<ManLooseBlocks>.inst.HostDetachBlock(m_BlockToTrack, allowHeadlessTech: true, propagate: true);
			ReturnBlockToDispenser(m_BlockToTrack);
		}
	}

	protected override void GameModeExit()
	{
		Singleton.Manager<ManNetwork>.inst.ServerNetBlockAttachedToTech.Unsubscribe(OnServerBlockAttachedToTech);
		Singleton.Manager<ManNetwork>.inst.ServerBlockRemovedFromTechTech.Unsubscribe(OnServerBlockRemovedFromTech);
	}

	private void ReturnBlockToDispenser(TankBlock m_BlockToTrack)
	{
		if (m_DispenserBlock != null)
		{
			m_DispenserBlock.ReturnItem(m_BlockToTrack.visible);
		}
	}

	private void SpawnDispensers()
	{
		ModePVP<ModeDeathmatch>.DispenserSpawn[] dispenserSpawns = Singleton.Manager<ManNetwork>.inst.Options.m_DispenserSpawns;
		for (int i = 0; i < dispenserSpawns.Length; i++)
		{
			ItemTypeInfo itemToSpawn = new ItemTypeInfo(ObjectTypes.Block, (int)dispenserSpawns[i].blockType);
			int quantity = ((dispenserSpawns[i].quantity > 0) ? dispenserSpawns[i].quantity : (-1));
			TrackedVisible trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnDispenser(dispenserSpawns[i].pos, Quaternion.identity, itemToSpawn, quantity);
			if (true && trackedVisible != null && trackedVisible.visible != null)
			{
				trackedVisible.RadarType = RadarTypes.Hidden;
				BlockManager.BlockIterator<ModuleItemDispenser>.Enumerator enumerator = trackedVisible.visible.tank.blockman.IterateBlockComponents<ModuleItemDispenser>().GetEnumerator();
				while (enumerator.MoveNext())
				{
					(m_DispenserBlock = enumerator.Current).VisibleSpawnedEvent.Subscribe(OnServerVisibleSpawnedFromDispenser);
				}
			}
		}
	}

	private void UpdateNotableBlockHoggerWatchdog()
	{
		NetController netController = Singleton.Manager<ManNetwork>.inst.NetController;
		if (!(netController != null))
		{
			return;
		}
		NetBlock notableBlock = netController.GetNotableBlock();
		if (notableBlock != null && Singleton.Manager<ManNetwork>.inst.GetAuthorityReason(notableBlock.netId) == ManNetwork.AuthorityReason.HeldVisible)
		{
			if (m_NotableBlockHeldTime >= 5f)
			{
				m_NotableBlockHeldTime = 0f;
				NetworkConnection clientAuthorityOwner = notableBlock.NetIdentity.clientAuthorityOwner;
				NetPlayer netPlayer = ((clientAuthorityOwner != null) ? Singleton.Manager<ManNetwork>.inst.FindPlayerByConnection(clientAuthorityOwner) : null);
				if (netPlayer != null)
				{
					netPlayer.OnServerForceDropNotableBlock();
					return;
				}
				if (clientAuthorityOwner == null)
				{
					d.LogError("Trying to force notable block hogger to drop, but nobody has authority");
					return;
				}
				d.LogErrorFormat("Trying to force notable block hogger to drop, but no player for connection ID {0}", clientAuthorityOwner.connectionId);
			}
			else
			{
				m_NotableBlockHeldTime += Time.deltaTime;
			}
		}
		else
		{
			m_NotableBlockHeldTime = 0f;
		}
	}

	private void OnServerVisibleSpawnedFromDispenser(Visible visible)
	{
		if ((bool)visible.block)
		{
			if (m_BlockToTrack != null)
			{
				m_BlockToTrack.visible.damageable.SetInvulnerable(invulnerable: false, unlimitedInvulnerability: false);
			}
			m_BlockToTrack = visible.block;
			visible.damageable.SetInvulnerable(invulnerable: true, unlimitedInvulnerability: true);
			visible.EnableAutoExpire(enable: false);
			Singleton.Manager<ManNetwork>.inst.NetController.SetNotableBlock(visible.block.netBlock);
		}
	}

	private void OnServerBlockAttachedToTech(Tank tech, NetBlock netBlock, TankBlock stdBlock)
	{
		if (m_BlockToTrack == netBlock.block)
		{
			m_TechToUpdateTimer = tech;
			m_BlockToTrack = stdBlock;
			m_LastAttachTime = Time.time;
			m_BlockToTrack.visible.damageable.SetInvulnerable(invulnerable: true, unlimitedInvulnerability: true);
			NetPlayer netPlayer = tech.netTech.NetPlayer;
			Singleton.Manager<ManNetwork>.inst.NetController.SetNotableTech(tech.netTech);
			Singleton.Manager<ManNetwork>.inst.NetController.SetNotableBlock(null);
			if (netPlayer == null)
			{
				d.LogError("ModeMultiplayer.OnServerBlockAttachedToTech - Tech " + tech.name + " has no NetPlayer");
			}
		}
	}

	private void OnServerBlockRemovedFromTech(Tank tech, TankBlock stdBlock, NetBlock netBlock)
	{
		if (m_BlockToTrack == stdBlock)
		{
			m_TechToUpdateTimer = null;
			m_BlockToTrack = netBlock.block;
			m_LastAttachTime = 0f;
			Singleton.Manager<ManNetwork>.inst.NetController.SetNotableTech(null);
			Singleton.Manager<ManNetwork>.inst.NetController.SetNotableBlock(netBlock);
			netBlock.block.visible.EnableAutoExpire(enable: false);
		}
	}
}
