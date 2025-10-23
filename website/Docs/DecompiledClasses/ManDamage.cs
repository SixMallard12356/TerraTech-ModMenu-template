#define UNITY_EDITOR
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ManDamage : Singleton.Manager<ManDamage>
{
	public enum DamageType
	{
		Standard,
		Ballistic,
		Energy,
		Blast,
		Impact,
		Fire,
		Cutting,
		Plasma,
		Electric
	}

	public enum DamageableType
	{
		Standard,
		Armour,
		Rubber,
		Volatile,
		Shield,
		Wood,
		Rock,
		Compound
	}

	public struct BlockDamageInfo
	{
		public DamageType DamageType;

		public bool HasDamageType;

		public DamageableType DamageableType;

		public static readonly BlockDamageInfo standard = new BlockDamageInfo(DamageType.Standard, DamageableType.Standard, HasDamageType: false);

		public BlockDamageInfo(DamageType DamageType, DamageableType DamageableType, bool HasDamageType = true)
		{
			this.DamageableType = DamageableType;
			this.DamageType = DamageType;
			this.HasDamageType = HasDamageType;
		}
	}

	private struct PendingDamage
	{
		public DamageInfo m_Info;

		public Damageable m_Target;
	}

	public struct DamageInfo
	{
		public float Damage { get; private set; }

		public DamageType DamageType { get; private set; }

		public float OverrideDetachDamage { get; private set; }

		public Component Source { get; private set; }

		public Tank SourceTank { get; private set; }

		public int SourceTeamID { get; private set; }

		public Vector3 HitPosition { get; private set; }

		public Vector3 DamageDirection { get; private set; }

		public float KickbackStrength { get; private set; }

		public float KickbackDuration { get; private set; }

		public DamageInfo(float damage, DamageType damageType, Component source, Tank sourceTank = null, Vector3 hitPosition = default(Vector3), Vector3 damageDirection = default(Vector3), float kickbackStrength = 0f, float kickbackDuration = 0f)
		{
			this = default(DamageInfo);
			Damage = damage;
			DamageType = damageType;
			Source = source;
			SourceTank = sourceTank;
			SourceTeamID = ((sourceTank == null) ? int.MaxValue : ((sourceTank.netTech != null) ? sourceTank.netTech.Team : sourceTank.Team));
			HitPosition = hitPosition;
			DamageDirection = damageDirection.normalized;
			KickbackStrength = kickbackStrength;
			KickbackDuration = kickbackDuration;
		}

		public DamageInfo Clone()
		{
			return this;
		}

		public static DamageInfo CreateNull()
		{
			return new DamageInfo(0f, DamageType.Impact, null);
		}

		public void ApplyDamageMultiplier(float multiplier)
		{
			Damage *= multiplier;
		}

		public void ReduceDamage(float sub)
		{
			Damage -= sub;
		}

		public void ApplyDetachDamageOverride(float factor)
		{
			OverrideDetachDamage = Damage * factor;
			Damage = Mathf.Max(0f, Damage - OverrideDetachDamage);
		}

		public void SetDetachDamageOverride(float detachDamageApplied)
		{
			d.Assert(detachDamageApplied > 0f, "Currently cannot be set to 0, as that will result in the value being ignored. Use a very small value in this case");
			OverrideDetachDamage = detachDamageApplied;
		}

		public void NetSerialize(NetworkWriter writer)
		{
			writer.Write(Damage);
			writer.Write((int)DamageType);
			writer.Write(OverrideDetachDamage);
			uint value = 0u;
			if (SourceTank != null && SourceTank.netTech != null)
			{
				value = SourceTank.netTech.netId.Value;
			}
			writer.Write(value);
			writer.Write(SourceTeamID);
			writer.Write(HitPosition);
			writer.Write(DamageDirection);
			writer.Write(KickbackStrength);
			writer.Write(KickbackDuration);
		}

		public void NetDeserialize(NetworkReader reader)
		{
			Damage = reader.ReadSingle();
			DamageType = (DamageType)reader.ReadInt32();
			OverrideDetachDamage = reader.ReadSingle();
			SourceTank = null;
			uint num = reader.ReadUInt32();
			if (num != 0)
			{
				GameObject gameObject = ClientScene.FindLocalObject(new NetworkInstanceId(num));
				if (gameObject != null)
				{
					SourceTank = gameObject.GetComponent<Tank>();
				}
			}
			SourceTeamID = reader.ReadInt32();
			HitPosition = reader.ReadVector3();
			DamageDirection = reader.ReadVector3();
			KickbackStrength = reader.ReadSingle();
			KickbackDuration = reader.ReadSingle();
		}
	}

	[SerializeField]
	private DamageMultiplierTable m_DamageMultiplierTable;

	[SerializeField]
	private float m_CoOpCampaignEnemyExtraHealthFractionPerClient = 0.25f;

	[SerializeField]
	private float m_CoOpCampaignPlayerExtraDamageFractionPerClient;

	[SerializeField]
	[Range(0f, 1f)]
	private float m_CabDamageDissipationFactor;

	[Range(0f, 1f)]
	[SerializeField]
	private float m_CabDamageDissipationDetachFactor;

	private List<PendingDamage> m_PendingDamage = new List<PendingDamage>();

	private Dictionary<BlockTypes, BlockDamageInfo> m_BlockDamageInfo = new Dictionary<BlockTypes, BlockDamageInfo>();

	private static List<TankBlock> s_AdjacentBlocks = new List<TankBlock>();

	private static TankBlock _s_BlockPrefab;

	private static Damageable _s_BlockDamageable;

	private static IModuleDamager _s_BlockDamager;

	public static int NumDamageTypes { get; } = EnumValuesIterator<DamageType>.Count;

	public static int NumDamageableTypes { get; } = EnumValuesIterator<DamageableType>.Count;

	public float CoOpCampaignEnemyExtraHealthFractionPerClient => m_CoOpCampaignEnemyExtraHealthFractionPerClient;

	public float CoOpCampaignPlayerExtraDamageFractionPerClient => m_CoOpCampaignPlayerExtraDamageFractionPerClient;

	public float DealDamage(Damageable damageTarget, float damage, DamageType damageType, Component source, Tank sourceTank = null, Vector3 hitPosition = default(Vector3), Vector3 damageDirection = default(Vector3), float kickbackStrength = 0f, float kickbackDuration = 0f)
	{
		DamageInfo damageInfo = new DamageInfo(damage, damageType, source, sourceTank, hitPosition, damageDirection, kickbackStrength, kickbackDuration);
		return DealDamage(damageInfo, damageTarget);
	}

	public float DealImpactDamage(Damageable damageTarget, float damage, Component source, Tank sourceTank = null, Vector3 hitPosition = default(Vector3), Vector3 damageDirection = default(Vector3))
	{
		return DealDamage(damageTarget, damage, DamageType.Impact, source, sourceTank, hitPosition, damageDirection);
	}

	public float GetDamageMultiplier(DamageType damageType, DamageableType damageableType)
	{
		return m_DamageMultiplierTable.GetDamageMultiplier(damageType, damageableType);
	}

	public float DealDamage(DamageInfo damageInfo, Damageable damageableTarget)
	{
		if (m_DamageMultiplierTable != null)
		{
			float damageMultiplier = GetDamageMultiplier(damageInfo.DamageType, damageableTarget.DamageableType);
			if (damageMultiplier != 1f)
			{
				damageInfo.ApplyDamageMultiplier(damageMultiplier);
			}
		}
		if (m_CabDamageDissipationFactor > 0f && damageableTarget.Block.IsNotNull() && damageableTarget.Block.IsController)
		{
			TankBlock[] connectedBlocksByAP = damageableTarget.Block.ConnectedBlocksByAP;
			foreach (TankBlock tankBlock in connectedBlocksByAP)
			{
				if (tankBlock != null)
				{
					s_AdjacentBlocks.Add(tankBlock);
				}
			}
		}
		if (s_AdjacentBlocks.Count > 0)
		{
			DamageInfo info = damageInfo.Clone();
			DamageInfo info2 = damageInfo.Clone();
			float num = m_CabDamageDissipationFactor / (float)(s_AdjacentBlocks.Count + 1);
			info2.ApplyDamageMultiplier(num);
			info.ApplyDamageMultiplier(1f - num * (float)s_AdjacentBlocks.Count);
			info2.ApplyDetachDamageOverride(m_CabDamageDissipationDetachFactor);
			foreach (TankBlock s_AdjacentBlock in s_AdjacentBlocks)
			{
				m_PendingDamage.Add(new PendingDamage
				{
					m_Info = info2,
					m_Target = s_AdjacentBlock.visible.damageable
				});
			}
			m_PendingDamage.Add(new PendingDamage
			{
				m_Info = info,
				m_Target = damageableTarget
			});
			s_AdjacentBlocks.Clear();
		}
		else
		{
			m_PendingDamage.Add(new PendingDamage
			{
				m_Info = damageInfo,
				m_Target = damageableTarget
			});
		}
		return damageableTarget.TryToDamage(damageInfo, actuallyDealDamage: false);
	}

	private void ProcessPendingDamage()
	{
		foreach (PendingDamage item in m_PendingDamage)
		{
			if (item.m_Target.IsNotNull() && item.m_Target.gameObject.activeSelf)
			{
				item.m_Target.TryToDamage(item.m_Info, actuallyDealDamage: true);
			}
		}
		m_PendingDamage.Clear();
	}

	public DamageableType GetBlockDamageableType(BlockTypes blockType)
	{
		return GetBlockDamageInfo(blockType).DamageableType;
	}

	public bool TryGetBlockDamageType(BlockTypes blockType, out DamageType damageType)
	{
		BlockDamageInfo blockDamageInfo = GetBlockDamageInfo(blockType);
		damageType = (blockDamageInfo.HasDamageType ? blockDamageInfo.DamageType : DamageType.Standard);
		return blockDamageInfo.HasDamageType;
	}

	public BlockDamageInfo GetBlockDamageInfo(BlockTypes blockType)
	{
		if (m_BlockDamageInfo.TryGetValue(blockType, out var value))
		{
			return value;
		}
		_s_BlockPrefab = Singleton.Manager<ManSpawn>.inst.GetBlockPrefab(blockType);
		if (_s_BlockPrefab == null)
		{
			d.LogError("Attempted to get damage type for block with ID: '" + blockType.ToString() + "' but no such block prefab exists! NEEDS FIXING! Returning default...");
			return BlockDamageInfo.standard;
		}
		_s_BlockDamageable = _s_BlockPrefab.GetComponent<Damageable>();
		_s_BlockDamager = _s_BlockPrefab.GetComponent<IModuleDamager>();
		DamageableType damageableType = ((_s_BlockDamageable != null) ? _s_BlockDamageable.DamageableType : DamageableType.Standard);
		bool flag = _s_BlockDamager != null;
		DamageType damageType = (flag ? _s_BlockDamager.DamageType : DamageType.Standard);
		m_BlockDamageInfo[blockType] = new BlockDamageInfo(damageType, damageableType, flag);
		return m_BlockDamageInfo[blockType];
	}

	private void Start()
	{
		Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.FixedUpdate, ManUpdate.Order.Last, ProcessPendingDamage);
		Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.Update, ManUpdate.Order.Last, ProcessPendingDamage);
	}
}
