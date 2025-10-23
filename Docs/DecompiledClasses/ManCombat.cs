#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ManCombat : Singleton.Manager<ManCombat>
{
	public static class Projectiles
	{
		private class ExpirationInfo
		{
			public Projectile Projectile;

			public float ExpirationTime;

			public Action OnExpiredCallback;

			public ExpirationInfo(Projectile projectile, Action onExpiredCallback)
			{
				Projectile = projectile;
				Projectile.m_RecycledEvent.Subscribe(OnProjectileRecycled);
				OnExpiredCallback = onExpiredCallback;
				ExpirationTime = -1f;
			}

			public void SetLifetime(float duration)
			{
				ExpirationTime = Time.time + duration;
			}

			private void OnProjectileRecycled()
			{
				DeregisterPerishableProjectile(this);
			}

			public void Dispose()
			{
				Projectile.m_RecycledEvent.Unsubscribe(OnProjectileRecycled);
			}
		}

		private static UIDBucket s_WeaponRoundUIDBucket;

		private static Dictionary<int, WeaponRound> s_WeaponRoundLookup;

		private static List<ExpirationInfo> s_Perishables;

		public static void Initialise()
		{
			s_Perishables = new List<ExpirationInfo>(256);
			s_WeaponRoundLookup = new Dictionary<int, WeaponRound>(100);
			s_WeaponRoundUIDBucket = new UIDBucket(0, int.MaxValue, warnAboutLooping: false);
			Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.ProjectileStuckSync, OnClientSyncRegisteredProjectileStuck);
		}

		public static void Update()
		{
			int count = s_Perishables.Count;
			if (count <= 0)
			{
				return;
			}
			for (int num = count - 1; num >= 0; num--)
			{
				if (s_Perishables[num].ExpirationTime <= Time.time)
				{
					ExpirationInfo expirationInfo = s_Perishables[num];
					DeregisterPerishableProjectileAt(num);
					expirationInfo.OnExpiredCallback();
				}
			}
		}

		public static void InitWeaponRoundUIDRange(int initial, int range)
		{
			s_WeaponRoundUIDBucket = new UIDBucket(initial, initial + range, warnAboutLooping: false);
		}

		public static void RegisterWeaponRound(WeaponRound round, int projectileUID = int.MinValue)
		{
			round.ShortlivedUID = ((projectileUID != int.MinValue) ? projectileUID : s_WeaponRoundUIDBucket.GetNextAndIncrement());
			s_WeaponRoundLookup[round.ShortlivedUID] = round;
			if (round is Projectile projectile)
			{
				projectile.m_StuckToVisibleEvent.Subscribe(OnRegisteredProjectileStuck);
			}
		}

		public static void UnregisterWeaponRound(WeaponRound round)
		{
			s_WeaponRoundLookup.Remove(round.ShortlivedUID);
			round.ShortlivedUID = int.MinValue;
			if (round is Projectile projectile)
			{
				projectile.m_StuckToVisibleEvent.Unsubscribe(OnRegisteredProjectileStuck);
			}
		}

		public static WeaponRound FindWeaponRound(int projectileID)
		{
			if (!s_WeaponRoundLookup.TryGetValue(projectileID, out var value))
			{
				d.LogWarning("Could not find weapon round when updating");
			}
			return value;
		}

		public static void SendProjectileStuckBlock(int projectileUID, uint blockStuckToUID, Vector3 localPosOnStuckBlock)
		{
			d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer, "Eh?? Why are we trying to send projectile stuck event data as a non-server client? This should be syncing from the server to the client!");
			ProjectileStuckSyncMessage message = new ProjectileStuckSyncMessage
			{
				m_ProjectileUID = projectileUID,
				m_BlockStuckToPoolID = blockStuckToUID,
				m_ProjectileLocalPosition = localPosOnStuckBlock
			};
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.ProjectileStuckSync, message);
		}

		public static void ReceiveProjectileStuckToBlock(int projectileUID, uint blockStuckToUID, Vector3 localPosOnStuckBlock)
		{
			d.Assert(!Singleton.Manager<ManNetwork>.inst.IsServer, "Eh?? We should not be receiving projectile stuck event data as the server, as we don't need to sync our side!");
			TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(blockStuckToUID);
			WeaponRound weaponRound = FindWeaponRound(projectileUID);
			if (tankBlock == null)
			{
				d.LogWarningFormat("Was unable to restick projectile to networked block as we were unable to locate networked block with UID {0} on the client! Aborting sync...", blockStuckToUID.ToString());
				return;
			}
			if (weaponRound == null)
			{
				d.LogWarningFormat("Was unable to restick projectile to networked block as we were unable to locate projectile with UID {0} on the client! Aborting sync...", projectileUID.ToString());
				return;
			}
			Projectile obj = (Projectile)weaponRound;
			Vector3 vector = tankBlock.visible.transform.TransformPoint(localPosOnStuckBlock);
			obj.StickToObjectWithVisuals(tankBlock.transform, tankBlock.visible, vector, allowRestick: true);
			obj.transform.position = vector;
		}

		public static void OnClientSyncRegisteredProjectileStuck(NetworkMessage netMsg)
		{
			ProjectileStuckSyncMessage projectileStuckSyncMessage = netMsg.ReadMessage<ProjectileStuckSyncMessage>();
			ReceiveProjectileStuckToBlock(projectileStuckSyncMessage.m_ProjectileUID, projectileStuckSyncMessage.m_BlockStuckToPoolID, projectileStuckSyncMessage.m_ProjectileLocalPosition);
		}

		public static void OnRegisteredProjectileStuck(bool state, Projectile projectile, Visible visibleStuckTo)
		{
			if (Singleton.Manager<ManNetwork>.inst.IsServer && state && !(visibleStuckTo == null) && visibleStuckTo.type == ObjectTypes.Block)
			{
				Vector3 localPosOnStuckBlock = visibleStuckTo.transform.InverseTransformPoint(projectile.transform.position);
				SendProjectileStuckBlock(projectile.ShortlivedUID, visibleStuckTo.block.blockPoolID, localPosOnStuckBlock);
			}
		}

		public static void RegisterPerishableProjectile(Projectile projectile, float lifetimeDuration, Action onExpiredCallback)
		{
			ExpirationInfo expirationInfo = s_Perishables.Where((ExpirationInfo expiry) => expiry.Projectile == projectile).FirstOrDefault();
			if (expirationInfo == null)
			{
				expirationInfo = new ExpirationInfo(projectile, onExpiredCallback);
				s_Perishables.Add(expirationInfo);
			}
			expirationInfo.SetLifetime(lifetimeDuration);
		}

		private static void DeregisterPerishableProjectileAt(int index)
		{
			s_Perishables[index].Dispose();
			s_Perishables.RemoveAt(index);
		}

		private static void DeregisterPerishableProjectile(ExpirationInfo perishable)
		{
			s_Perishables.Remove(perishable);
			perishable.Dispose();
		}
	}

	private void Update()
	{
		Projectiles.Update();
	}

	private void Awake()
	{
		Projectiles.Initialise();
	}
}
