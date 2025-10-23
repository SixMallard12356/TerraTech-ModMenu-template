#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class Damageable : MonoBehaviour, ManVisible.StateVisualiser.Provider
{
	[FormerlySerializedAs("maxHealth")]
	[SerializeField]
	private float m_OrigMaxHealth = 100f;

	[SerializeField]
	public ManDamage.DamageableType m_DamageableType;

	[SerializeField]
	private Transform m_HealingOrigin;

	[SerializeField]
	[Tooltip("Percentage of AoE (explosion) damage that will not pass beyond hitting this block.")]
	[Range(0f, 1f)]
	private float m_AoEDamageBlockPercent = 0.8f;

	public bool destroyOnDeath = true;

	public const float kRefillHealth = -1337f;

	public Event<ManDamage.DamageInfo> damageEvent;

	public Event<Damageable, ManDamage.DamageInfo> deathEvent;

	public Event<Damageable, ManDamage.DamageInfo> multiplayerDamageVisualOnlyEvent;

	public Event<float> HealEvent;

	private Func<ManDamage.DamageInfo, bool, bool> rejectDamageEvent;

	private float m_NextThreshold;

	private float m_InvulnerableEndTime = -1f;

	private bool m_Invulnerable;

	private int m_HealthFixed;

	private int m_MaxHealthFixed;

	[HideInInspector]
	[SerializeField]
	private TankBlock m_Block;

	private static readonly Vector2 k_BarSize = new Vector2(32f, 7f);

	private static readonly Color k_BarColBG = Color.red.ScaleRGB(0.5f).SetAlpha(0.6f);

	private static readonly Color k_BarColFG = Color.green.ScaleRGB(0.7f).SetAlpha(0.6f);

	private static readonly Color k_TextCol = Color.red.ScaleRGB(0.3f).SetAlpha(0.5f);

	private static List<TankBlock> s_TmpList = new List<TankBlock>();

	public static bool DamageEnabled { get; set; } = true;

	public float Health => (float)m_HealthFixed / 4096f;

	public float MaxHealth => (float)m_MaxHealthFixed / 4096f;

	public bool IsAtFullHealth => m_HealthFixed >= m_MaxHealthFixed;

	public bool UnlimitedInvulnerable
	{
		get
		{
			if (m_Invulnerable)
			{
				return m_InvulnerableEndTime == -1f;
			}
			return false;
		}
	}

	public ManDamage.DamageableType DamageableType
	{
		get
		{
			return m_DamageableType;
		}
		set
		{
			m_DamageableType = value;
		}
	}

	public float AoEDamageBlockPercent => m_AoEDamageBlockPercent;

	public TankBlock Block => m_Block;

	public bool Invulnerable
	{
		get
		{
			if (m_Invulnerable && m_InvulnerableEndTime != -1f && m_InvulnerableEndTime <= Time.time)
			{
				m_Invulnerable = false;
			}
			return m_Invulnerable;
		}
	}

	public event Func<ManDamage.DamageInfo, bool> damageThresholdEvent;

	public void SetInvulnerable(bool invulnerable, bool unlimitedInvulnerability)
	{
		m_InvulnerableEndTime = (unlimitedInvulnerability ? (-1f) : (Time.time + 1f));
		m_Invulnerable = invulnerable;
	}

	public void SetDamageThreshold(float thresholdValue)
	{
		m_NextThreshold = thresholdValue;
	}

	public void MultiplayerDamageOwnTech(ManDamage.DamageInfo info)
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst.IsMultiplayer(), "Multiplayer only function");
		if (info.SourceTank.IsNotNull() && info.SourceTank.netTech.IsNotNull() && info.SourceTank.netTech.NetPlayer.IsNotNull() && info.SourceTank.netTech.NetPlayer == Singleton.Manager<ManNetwork>.inst.MyPlayer)
		{
			Damage(info);
		}
	}

	public float TryToDamage(ManDamage.DamageInfo info, bool actuallyDealDamage, bool d_ignoreDamageDisables = false)
	{
		if (!d_ignoreDamageDisables)
		{
			if (!DamageEnabled)
			{
				return 0f;
			}
			if (Singleton.Manager<DebugUtil>.inst.m_Settings.m_DisableAllDamage)
			{
				return 0f;
			}
			if (Singleton.Manager<ManPlayer>.inst.PlayerIndestructible && Block.IsNotNull() && Block.tank.IsNotNull() && ManSpawn.IsPlayerTeam(Block.tank.Team))
			{
				return 0f;
			}
		}
		if (Health > 0f)
		{
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCoOp())
				{
					int numPlayers = Singleton.Manager<ManNetwork>.inst.GetNumPlayers();
					if (numPlayers > 1 && Block.IsNotNull() && Block.tank.IsNotNull())
					{
						if (Block.tank.IsFriendly() && Tank.IsEnemy(info.SourceTeamID, Singleton.Manager<ManPlayer>.inst.PlayerTeam))
						{
							info.ApplyDamageMultiplier(1f + Singleton.Manager<ManDamage>.inst.CoOpCampaignPlayerExtraDamageFractionPerClient * (float)(numPlayers - 1));
						}
						else if (Block.tank.IsEnemy() && Tank.IsFriendly(info.SourceTeamID, Singleton.Manager<ManPlayer>.inst.PlayerTeam))
						{
							info.ApplyDamageMultiplier(1f / (1f + Singleton.Manager<ManDamage>.inst.CoOpCampaignEnemyExtraHealthFractionPerClient * (float)(numPlayers - 1)));
						}
					}
				}
				if (!Singleton.Manager<ManNetwork>.inst.IsServer)
				{
					multiplayerDamageVisualOnlyEvent.Send(this, info);
					return Damage(info, actuallyDealDamage: false);
				}
				if (Block.IsNotNull() && Block.tank.IsNotNull())
				{
					if (Block.tank.Team == 1073741828)
					{
						return 0f;
					}
					if (info.SourceTeamID == 1073741828)
					{
						return 0f;
					}
				}
			}
			else if (Block.IsNotNull() && Block.tank.IsNotNull() && Block.tank.IsPlayer)
			{
				info.ApplyDamageMultiplier(SKU.ConsoleUI ? Globals.inst.playerDamageReceivedMultiplierConsole : Globals.inst.playerDamageReceivedMultiplierPC);
			}
			if (Invulnerable)
			{
				return 0f;
			}
			if (rejectDamageEvent != null && rejectDamageEvent(info, actuallyDealDamage))
			{
				return 0f;
			}
			return Damage(info, actuallyDealDamage);
		}
		return 0f;
	}

	public void ApplyDamageOnly(ManDamage.DamageInfo info)
	{
		ModifyHealth(0f - info.Damage);
	}

	private float Damage(ManDamage.DamageInfo info, bool actuallyDealDamage = true)
	{
		if (!actuallyDealDamage)
		{
			float num = Health - info.Damage;
			if (num <= 0f)
			{
				return (0f - num) / info.Damage;
			}
			return 0f;
		}
		if (info.KickbackStrength > 0f)
		{
			Vector3 vector = info.DamageDirection * info.KickbackStrength;
			if (Block != null)
			{
				if (Block.IsAttached)
				{
					Block.tank.ApplyForceOverTime(vector, info.HitPosition, info.KickbackDuration);
				}
				else
				{
					Rigidbody rbody = Block.visible.rbody;
					if (rbody.IsNotNull())
					{
						rbody.AddForceAtPosition(vector, info.HitPosition, ForceMode.Impulse);
					}
				}
			}
			else
			{
				Rigidbody component = GetComponent<Rigidbody>();
				if (component != null)
				{
					component.AddForceAtPosition(vector, info.HitPosition, ForceMode.Impulse);
				}
			}
		}
		bool flag = true;
		float num2 = info.Damage;
		do
		{
			float num3 = ((m_NextThreshold > 0f) ? Mathf.Clamp(num2, 0f, Health - m_NextThreshold) : num2);
			ModifyHealth(0f - num3);
			num2 -= num3;
			if (this.damageThresholdEvent != null && Health <= m_NextThreshold)
			{
				flag = CallDamageThresholdEvent(info);
			}
		}
		while (flag && num2 > Mathf.Epsilon);
		if (num2 > 0f)
		{
			info.ReduceDamage(num2);
		}
		damageEvent.Send(info);
		if (Health <= 0f && !Invulnerable)
		{
			deathEvent.Send(this, info);
			if (destroyOnDeath)
			{
				base.transform.Recycle();
			}
			float result = (0f - Health) / info.Damage;
			m_HealthFixed = 0;
			return result;
		}
		return 0f;
	}

	public bool CallDamageThresholdEvent(ManDamage.DamageInfo info)
	{
		return this.damageThresholdEvent(info);
	}

	public void Repair(float amount, bool sendEvent = true)
	{
		ModifyHealth(amount);
		if (sendEvent)
		{
			HealEvent.Send(amount);
		}
	}

	public void SetMaxHealth(float maxHealth)
	{
		m_MaxHealthFixed = (int)(maxHealth * 4096f);
		m_HealthFixed = Mathf.Min(m_HealthFixed, m_MaxHealthFixed);
	}

	public void InitHealth(float h)
	{
		if (h == -1337f)
		{
			m_HealthFixed = m_MaxHealthFixed;
		}
		else
		{
			SetHealth(h);
		}
	}

	public Vector3 GetHealingOriginWorld()
	{
		if ((bool)m_HealingOrigin)
		{
			return m_HealingOrigin.position;
		}
		if ((bool)Block)
		{
			return Block.centreOfMassWorld;
		}
		return base.transform.position;
	}

	public void SetRejectDamageHandler(Func<ManDamage.DamageInfo, bool, bool> handler)
	{
		d.Assert(rejectDamageEvent == null || handler == null, "SetRejectDamageHandler - Setting handler while already have a handler set!");
		rejectDamageEvent = handler;
	}

	private void SetHealth(float amount)
	{
		m_HealthFixed = Mathf.Min((int)(amount * 4096f), m_MaxHealthFixed);
	}

	private void ModifyHealth(float delta)
	{
		int num = Mathf.RoundToInt(delta * 4096f);
		m_HealthFixed = Mathf.Min(m_HealthFixed + num, m_MaxHealthFixed);
	}

	void ManVisible.StateVisualiser.Provider.Draw(Vector2 screenPos, Bitfield<DebugSettings.VisibleDebugFlags> flags)
	{
		if (flags.Contains(0) && m_HealthFixed != m_MaxHealthFixed)
		{
			DebugGui.BarScreen(k_BarSize, 1f, k_BarColBG, screenPos);
			DebugGui.BarScreen(k_BarSize, (float)m_HealthFixed / (float)m_MaxHealthFixed, k_BarColFG, screenPos);
			DebugGui.LabelScreen(((int)MaxHealth).ToString(), k_TextCol, screenPos, DebugGui.BGMode.None);
		}
	}

	private void OnRecycle()
	{
		m_Invulnerable = false;
	}

	private void PrePool()
	{
		GetComponentsInParent(includeInactive: true, s_TmpList);
		m_Block = ((s_TmpList.Count > 0) ? s_TmpList[0] : null);
	}

	private void OnPool()
	{
		m_MaxHealthFixed = (int)(m_OrigMaxHealth * 4096f);
		m_HealthFixed = m_MaxHealthFixed;
	}
}
