#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class ModuleDamage : Module, ManVisible.StateVisualiser.Provider
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public float health;

		public bool unlimitedInvulnerable;

		public float m_ExplodeTime;
	}

	public int maxHealth = 100;

	public float m_DamageDetachFragility = 1f;

	public Transform deathExplosion;

	private float m_ExplodeCountdownTimer;

	private bool m_HealOnDetach;

	private float m_HealOnDetachPercent;

	private float m_DamageDetachMeter;

	private float m_DamageDetachMeterTimestamp;

	[SerializeField]
	private SwitchableUpdater m_SwitchableUpdater;

	private List<ParticleSystemRenderer> s_TempPsr = new List<ParticleSystemRenderer>(16);

	private static readonly Vector2 k_BarSize = new Vector2(32f, 3f);

	private static readonly Color k_BarColBGFull = Color.red.ScaleRGB(0.5f).SetAlpha(0.4f);

	private static readonly Color k_BarColBGBase = Color.cyan.ScaleRGB(0.5f).SetAlpha(0.4f);

	private static readonly Color k_BarColFG = Color.cyan.SetAlpha(0.7f);

	public bool AboutToDie => m_ExplodeCountdownTimer > 0f;

	public void SelfDestruct(float fuseTime)
	{
		if (m_ExplodeCountdownTimer == 0f)
		{
			d.Assert(fuseTime > 0f, "ASSERT - fuseTime is <= 0.0f!");
			m_ExplodeCountdownTimer = fuseTime;
			base.block.PreExplodePulse = true;
			EnableOrDisableUpdater();
			base.block.StartMaterialPulse(ManTechMaterialSwap.MaterialTypes.Damage, ManTechMaterialSwap.MaterialColour.Damage);
		}
	}

	public void AbortSelfDestruct()
	{
		m_ExplodeCountdownTimer = 0f;
		base.block.PreExplodePulse = false;
	}

	public float SelfDestructTimeRemaining()
	{
		d.Assert(m_ExplodeCountdownTimer > 0f, "ASSERT - Self Destruct is NOT currently active!");
		return m_ExplodeCountdownTimer;
	}

	public void SelfDestruct(float fuseTime, ManDamage.DamageInfo damage)
	{
		if (m_ExplodeCountdownTimer == 0f)
		{
			d.Assert(fuseTime > 0f, "ASSERT - fuseTime is <= 0.0f!");
			m_ExplodeCountdownTimer = fuseTime;
			base.block.PreExplodePulse = true;
			base.block.DamageInEffect = new ManDamage.DamageInfo(0f, damage.DamageType, damage.Source, damage.SourceTank);
			EnableOrDisableUpdater();
			base.block.StartMaterialPulse(ManTechMaterialSwap.MaterialTypes.Damage, ManTechMaterialSwap.MaterialColour.Damage);
		}
	}

	public void SetHealOnDetatch(float healPercentage)
	{
		m_HealOnDetach = true;
		m_HealOnDetachPercent = healPercentage;
	}

	public void ResetDetachMeter()
	{
		m_DamageDetachMeter = 0f;
	}

	public float GetDetachPowerFromDamage(float damage)
	{
		bool num = Singleton.Manager<ManNetwork>.inst.IsMultiplayer();
		bool flag = ManSpawn.IsPlayerTeam(base.block.tank.Team);
		float num2 = ((!num && SKU.ConsoleUI) ? Globals.inst.moduleDamageParams.detachMeterFillFactorPlayerConsole : Globals.inst.moduleDamageParams.detachMeterFillFactorPlayerPC);
		float num3 = (flag ? num2 : Globals.inst.moduleDamageParams.detachMeterFillFactor);
		int num4 = Mathf.Max(base.block.NumConnectedAPs, 1);
		float num5 = 1f - Globals.inst.moduleDamageParams.detachMeterConnectedAPBias * (1f - 1f / (float)num4);
		float num6 = 1f - Globals.inst.moduleDamageParams.detachMeterConnectedMassBias * (1f - 1f / base.block.CurrentMass);
		return damage * m_DamageDetachFragility * num3 * num5 * num6;
	}

	public bool CouldDetachFromAdditionalDamage(float damage)
	{
		return CouldDetachFromDamageMeterValue(m_DamageDetachMeter + GetDetachPowerFromDamage(damage));
	}

	public bool CouldDetachFromDamageMeterValue(float damageDetachMeter)
	{
		float num = Globals.inst.moduleDamageParams.detachMeterMaxThreshold - Globals.inst.moduleDamageParams.detachMeterMinThreshold;
		float num2 = (damageDetachMeter - Globals.inst.moduleDamageParams.detachMeterMinThreshold) / num;
		return UnityEngine.Random.value < num2;
	}

	public void Explode(bool withDamage = true)
	{
		Transform transform = deathExplosion.Spawn(Singleton.dynamicContainer, base.block.centreOfMassWorld, base.block.trans.rotation);
		transform.GetComponentsInChildren(s_TempPsr);
		byte skinIndex = base.block.GetSkinIndex();
		foreach (ParticleSystemRenderer item in s_TempPsr)
		{
			if (item.renderMode != ParticleSystemRenderMode.Mesh)
			{
				continue;
			}
			MaterialSwapper.SetMaterialPropertiesOnRenderer(ManTechMaterialSwap.MaterialColour.Normal, 0f, skinIndex, MaterialSwapper.VariableColorOverrides.empty, item);
			bool flag = false;
			if (base.block.IsCustomMaterialOverride())
			{
				ManTechMaterialSwap.MatType materialType = base.block.GetMaterialType();
				Material material = Singleton.Manager<ManTechMaterialSwap>.inst.GetMaterial(materialType, base.block.GetCurrentMaterial());
				if (material != null)
				{
					item.sharedMaterial = material;
					flag = true;
				}
			}
			if (!flag)
			{
				item.sharedMaterial = Singleton.Manager<ManTechMaterialSwap>.inst.GetSharedDefaultMaterial(item.sharedMaterial);
			}
		}
		s_TempPsr.Clear();
		Explosion component = transform.GetComponent<Explosion>();
		if ((bool)component)
		{
			component.SetDamageSource(base.block.DamageInEffect.SourceTank);
			component.DoDamage = withDamage;
			component.SetCorpType(Singleton.Manager<ManSpawn>.inst.GetCorporation((BlockTypes)base.block.visible.ItemType));
		}
	}

	private void ShopBlockHACK(bool detached)
	{
		if (base.block.visible.ItemType == 138)
		{
			base.block.visible.damageable.SetInvulnerable(detached, unlimitedInvulnerability: true);
			if (detached)
			{
				m_HealOnDetach = true;
				m_HealOnDetachPercent = 1f;
			}
		}
	}

	public bool HasMaterialPulse()
	{
		return base.block.HasMaterialPulse();
	}

	private float GetLowHealthThreshold()
	{
		return (float)maxHealth * Globals.inst.moduleDamageParams.lowHealthFlashThreshold;
	}

	private void EnableOrDisableUpdater()
	{
		m_SwitchableUpdater.enabled = m_ExplodeCountdownTimer > 0f;
	}

	private void OnAttached()
	{
		ShopBlockHACK(detached: false);
		m_DamageDetachMeter = 0f;
		m_DamageDetachMeterTimestamp = 0f;
	}

	private void OnDetaching()
	{
		ShopBlockHACK(detached: true);
		if (m_HealOnDetach)
		{
			float num = Mathf.Min((float)maxHealth * m_HealOnDetachPercent, maxHealth);
			if (num > base.block.visible.damageable.Health)
			{
				base.block.visible.damageable.Repair(num - base.block.visible.damageable.Health, sendEvent: false);
			}
			m_HealOnDetach = false;
		}
		m_DamageDetachMeter = 0f;
	}

	public void OnNetDamaged(ManDamage.DamageInfo info, bool detatched, bool isServer)
	{
		base.block.visible.damageable.ApplyDamageOnly(info);
		if (base.block.visible.damageable.Health <= 0f && !base.block.visible.damageable.Invulnerable)
		{
			base.block.PreExplodePulse = true;
		}
		base.block.StartMaterialPulse(ManTechMaterialSwap.MaterialTypes.Damage, ManTechMaterialSwap.MaterialColour.Damage);
		Tank tank = base.block.tank;
		if (!tank)
		{
			return;
		}
		tank.NotifyDamage(info, base.block);
		if (!isServer)
		{
			Vector3 impulse = info.DamageDirection * info.KickbackStrength;
			tank.ApplyForceOverTime(impulse, info.HitPosition, info.KickbackDuration);
		}
		if (detatched && !isServer)
		{
			DetachFromDamage(info);
			List<TankBlock> lastRemovedBlocks = tank.blockman.LastRemovedBlocks;
			for (int i = 0; i < lastRemovedBlocks.Count; i++)
			{
				lastRemovedBlocks[i].trans.Recycle();
			}
		}
	}

	public void NetScavenged()
	{
		base.block.StartMaterialPulse(ManTechMaterialSwap.MaterialTypes.Damage, ManTechMaterialSwap.MaterialColour.Scavenge);
	}

	public bool DisplayOutOfShieldFeedback()
	{
		return Globals.inst.moduleDamageParams.OutOfShieldColour.a > 0f;
	}

	public void SetOutOfShieldFeedback()
	{
		base.block.StartMaterialPulse(ManTechMaterialSwap.MaterialTypes.Damage, ManTechMaterialSwap.MaterialColour.OutOfShield);
	}

	public void MultiplayerFakeDamagePulse()
	{
		base.block.StartMaterialPulse(ManTechMaterialSwap.MaterialTypes.Damage, ManTechMaterialSwap.MaterialColour.Damage);
	}

	public void MultiplayerOutOfBoundsDamageBlock(float damage)
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst.IsMultiplayer(), "Multiplayer only function");
		Tank tank = base.block.tank;
		if ((bool)tank)
		{
			ManDamage.DamageInfo info = new ManDamage.DamageInfo(damage, ManDamage.DamageType.Impact, this, tank);
			tank.NotifyDamage(info, base.block);
			OnDamaged(info);
			base.block.visible.damageable.MultiplayerDamageOwnTech(info);
			OnDamagedMuliplayerTechBlock(info, tank, detached: false);
			base.block.StartMaterialPulse(ManTechMaterialSwap.MaterialTypes.Damage, ManTechMaterialSwap.MaterialColour.Damage);
		}
	}

	public void DebugKillTechBlock()
	{
		Tank tank = base.block.tank;
		if ((bool)tank)
		{
			ManDamage.DamageInfo info = new ManDamage.DamageInfo(base.block.visible.damageable.Health, ManDamage.DamageType.Impact, this);
			tank.NotifyDamage(info, base.block);
			OnFatalDamage(base.block.visible.damageable, info);
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				OnDamagedMuliplayerTechBlock(info, tank, detached: false);
			}
			base.block.StartMaterialPulse(ManTechMaterialSwap.MaterialTypes.Damage, ManTechMaterialSwap.MaterialColour.Damage);
		}
	}

	public void DebugDetatchTechBlock()
	{
		Tank tank = base.block.tank;
		if ((bool)tank)
		{
			ManDamage.DamageInfo info = new ManDamage.DamageInfo(0f, ManDamage.DamageType.Impact, this);
			tank.NotifyDamage(info, base.block);
			DetachFromDamage(info);
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				OnDamagedMuliplayerTechBlock(info, tank, detached: true);
			}
		}
	}

	private void OnDamagedMuliplayerTechBlock(ManDamage.DamageInfo info, Tank tech, bool detached)
	{
		if (!(info.Source != null))
		{
			return;
		}
		List<TankBlock> lastRemovedBlocks = tech.blockman.LastRemovedBlocks;
		NetTech netTech = tech.netTech;
		if ((bool)netTech)
		{
			if (info.SourceTank.IsNotNull() && netTech.OwnerNetId != netTech.netId)
			{
				netTech.LastDamager = info.SourceTank.netTech.NetPlayer;
			}
			Singleton.Manager<ManNetwork>.inst.OnHostDuringPhysicsCallback = true;
			Singleton.Manager<ManNetwork>.inst.MyPlayer.SendTechBlockDamagedMessage(netTech.netId, base.block.blockPoolID, info, detached && lastRemovedBlocks.Count > 0, tech.blockman.IdOfFirstRemovedBlock, tech.blockman.BlockRemovalSeed);
		}
	}

	private bool CheckAndHandleDetatch(ManDamage.DamageInfo info)
	{
		bool result = false;
		if (!base.block.IsController)
		{
			UpdateDetachMeterFromDamage(info);
			if (CouldDetachFromDamageMeterValue(m_DamageDetachMeter))
			{
				DetachFromDamage(info);
				result = true;
			}
		}
		return result;
	}

	private void OnDamaged(ManDamage.DamageInfo info)
	{
		Tank tank = base.block.tank;
		if ((bool)tank)
		{
			tank.NotifyDamage(info, base.block);
			bool detached = CheckAndHandleDetatch(info);
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				OnDamagedMuliplayerTechBlock(info, tank, detached);
			}
		}
		else if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && info.Source != null)
		{
			NetBlock netBlock = base.block.netBlock;
			if ((bool)netBlock)
			{
				Singleton.Manager<ManNetwork>.inst.OnClientBlockDamaged(netBlock, info);
			}
		}
		base.block.StartMaterialPulse(ManTechMaterialSwap.MaterialTypes.Damage, ManTechMaterialSwap.MaterialColour.Damage);
	}

	private void UpdateDetachMeterFromDamage(ManDamage.DamageInfo info)
	{
		float num = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() - m_DamageDetachMeterTimestamp;
		m_DamageDetachMeterTimestamp = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
		m_DamageDetachMeter = Mathf.Max(m_DamageDetachMeter - num * Globals.inst.moduleDamageParams.detachMeterDrainRate, 0f);
		if (info.OverrideDetachDamage > 0f)
		{
			m_DamageDetachMeter += info.OverrideDetachDamage;
		}
		else
		{
			m_DamageDetachMeter += GetDetachPowerFromDamage(info.Damage);
		}
	}

	private void DetachFromDamage(ManDamage.DamageInfo info)
	{
		Tank tank = base.block.tank;
		tank.DamageInEffect = info;
		base.block.DamageInEffect = info;
		Singleton.Manager<ManLooseBlocks>.inst.HostDetachBlock(base.block, allowHeadlessTech: false, propagate: true);
		base.block.DamageInEffect = ManDamage.DamageInfo.CreateNull();
		tank.DamageInEffect = ManDamage.DamageInfo.CreateNull();
	}

	private void OnHealed(float healAmount)
	{
		if (healAmount != 0f && base.block.visible.damageable.Health > 0f && m_ExplodeCountdownTimer > 0f)
		{
			m_ExplodeCountdownTimer = 0f;
			base.block.PreExplodePulse = false;
			EnableOrDisableUpdater();
		}
	}

	private void OnFatalDamage(Damageable damageable, ManDamage.DamageInfo info)
	{
		if (m_ExplodeCountdownTimer != 0f)
		{
			return;
		}
		if (base.block.IsController)
		{
			m_ExplodeCountdownTimer = Globals.inst.moduleDamageParams.zeroHealthCabExplodeAfterTime;
			if (m_ExplodeCountdownTimer <= 0f)
			{
				m_ExplodeCountdownTimer = 0.0001f;
			}
		}
		else
		{
			float zeroHealthExplodeAfterTime = Globals.inst.moduleDamageParams.zeroHealthExplodeAfterTime;
			float zeroHealthExplodeTimeVariance = Globals.inst.moduleDamageParams.zeroHealthExplodeTimeVariance;
			m_ExplodeCountdownTimer = zeroHealthExplodeAfterTime.RandomVariance(zeroHealthExplodeTimeVariance);
		}
		d.Assert(m_ExplodeCountdownTimer > 0f, "ASSERT - ExplodeCountdownTimer must be greater than 0.0f!");
		base.block.PreExplodePulse = true;
		EnableOrDisableUpdater();
		base.block.DamageInEffect = info;
	}

	private bool OnRejectDamage(ManDamage.DamageInfo info, bool actuallyDealDamage)
	{
		if ((bool)base.block.tank && !Singleton.Manager<ManGameMode>.inst.IsFriendlyFireEnabled() && (bool)info.SourceTank && !info.SourceTank.IsEnemy(base.block.tank.Team))
		{
			return true;
		}
		if (actuallyDealDamage && info.Damage == 0f && AboutToDie)
		{
			m_ExplodeCountdownTimer *= Globals.inst.moduleDamageParams.explodeTimerReductionPerAdditionalHit;
		}
		return false;
	}

	private void OnUpdateExplodeTimeout()
	{
		if (m_ExplodeCountdownTimer == 0f)
		{
			return;
		}
		d.Assert(m_ExplodeCountdownTimer > 0f, "ASSERT - m_ExplodeCountdownTimer is < 0.0f!");
		m_ExplodeCountdownTimer -= Time.deltaTime;
		if (!(m_ExplodeCountdownTimer <= 0f))
		{
			return;
		}
		if ((bool)deathExplosion)
		{
			Explode();
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && ((base.block.tank != null && base.block.tank.netTech != null) || (base.block.netBlock != null && base.block.netBlock.hasAuthority)))
			{
				Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.BlockExploded, new BlockExplodedMessage
				{
					m_BlockPoolID = base.block.blockPoolID
				}, Singleton.Manager<ManNetwork>.inst.MyPlayer.netId);
			}
		}
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && base.block.tank != null)
		{
			DetachFromDamage(base.block.DamageInEffect);
		}
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManPointer>.inst.DraggingItem != null && Singleton.Manager<ManPointer>.inst.DraggingItem.block == base.block)
		{
			Singleton.Manager<ManPointer>.inst.ReleaseDraggingItem(applyVelocity: false);
		}
		Singleton.Manager<ManLooseBlocks>.inst.RequestDespawnBlock(base.block, DespawnReason.Host);
		m_ExplodeCountdownTimer = 0f;
		base.block.PreExplodePulse = false;
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.health = base.block.visible.damageable.Health;
			serialData.unlimitedInvulnerable = base.block.visible.damageable.UnlimitedInvulnerable;
			serialData.m_ExplodeTime = Mathf.Max(0f, m_ExplodeCountdownTimer);
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 == null)
		{
			return;
		}
		base.block.visible.damageable.InitHealth(serialData2.health);
		if (serialData2.unlimitedInvulnerable)
		{
			base.block.visible.damageable.SetInvulnerable(invulnerable: true, unlimitedInvulnerability: true);
		}
		if (serialData2.health < GetLowHealthThreshold())
		{
			base.block.StartMaterialPulse(ManTechMaterialSwap.MaterialTypes.Damage, ManTechMaterialSwap.MaterialColour.Damage);
		}
		if (serialData2.health == 0f)
		{
			if (serialData2.m_ExplodeTime > 0f)
			{
				m_ExplodeCountdownTimer = serialData2.m_ExplodeTime;
				base.block.PreExplodePulse = true;
				EnableOrDisableUpdater();
			}
			else
			{
				float fuseTime = 2f;
				SelfDestruct(fuseTime);
			}
		}
	}

	void ManVisible.StateVisualiser.Provider.Draw(Vector2 screenPos, Bitfield<DebugSettings.VisibleDebugFlags> flags)
	{
		if (flags.Contains(0))
		{
			float num = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() - m_DamageDetachMeterTimestamp;
			m_DamageDetachMeterTimestamp = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
			m_DamageDetachMeter = Mathf.Max(m_DamageDetachMeter - num * Globals.inst.moduleDamageParams.detachMeterDrainRate, 0f);
			if (m_DamageDetachMeter != 0f)
			{
				screenPos.y -= 10f;
				DebugGui.BarScreen(k_BarSize, 1f, k_BarColBGFull, screenPos);
				float fullness = Globals.inst.moduleDamageParams.detachMeterMinThreshold / Globals.inst.moduleDamageParams.detachMeterMaxThreshold;
				DebugGui.BarScreen(k_BarSize, fullness, k_BarColBGBase, screenPos);
				fullness = m_DamageDetachMeter / Globals.inst.moduleDamageParams.detachMeterMaxThreshold;
				DebugGui.BarScreen(k_BarSize, fullness, k_BarColFG, screenPos);
			}
		}
	}

	private void PrePool()
	{
		m_SwitchableUpdater = base.gameObject.AddComponent<SwitchableUpdater>();
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.visible.damageable.SetMaxHealth(maxHealth);
		base.block.visible.damageable.destroyOnDeath = false;
		base.block.visible.damageable.deathEvent.Subscribe(OnFatalDamage);
		base.block.visible.damageable.damageEvent.Subscribe(OnDamaged);
		base.block.visible.damageable.SetRejectDamageHandler(OnRejectDamage);
		base.block.visible.damageable.HealEvent.Subscribe(OnHealed);
		m_SwitchableUpdater.UpdateEvent.Subscribe(OnUpdateExplodeTimeout);
	}

	private void OnSpawn()
	{
		m_ExplodeCountdownTimer = 0f;
		m_HealOnDetach = false;
		m_DamageDetachMeter = 0f;
		m_SwitchableUpdater.enabled = false;
		base.block.visible.damageable.SetMaxHealth(maxHealth);
		if (base.block.BlockType == BlockTypes.SPE_Crown_111)
		{
			base.block.visible.damageable.SetInvulnerable(invulnerable: true, unlimitedInvulnerability: true);
		}
	}
}
