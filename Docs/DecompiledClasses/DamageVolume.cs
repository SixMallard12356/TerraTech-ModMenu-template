#define UNITY_EDITOR
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class DamageVolume : MonoBehaviour
{
	public enum DamageType
	{
		Kill,
		DPS,
		Heal,
		KeepAlive
	}

	[SerializeField]
	private DamageType m_DamageType;

	[SerializeField]
	private bool m_StartActive;

	public Event<Visible> OnDamageVolumeKillEvent;

	private Collider m_Collider;

	private bool m_InVolume;

	private bool m_Active;

	private List<Collider> m_CollidersToKill = new List<Collider>();

	private List<Collider> m_CollidersToVerifyAndKill = new List<Collider>();

	public void Enable(bool enable)
	{
		m_Active = enable;
	}

	public void SetVolumeRadius(float radius)
	{
		SphereCollider sphereCollider = m_Collider as SphereCollider;
		if ((bool)sphereCollider)
		{
			sphereCollider.radius = radius;
		}
		else
		{
			d.LogError("DamageVolume.SetVolumeRadius - does not have a SphereCollider on it.");
		}
	}

	private void OnContainmentTrigger(TriggerCatcher.Interaction t, Collider other)
	{
		if (!m_Active)
		{
			return;
		}
		switch (t)
		{
		case TriggerCatcher.Interaction.Enter:
			if (m_DamageType == DamageType.Kill)
			{
				m_CollidersToVerifyAndKill.Add(other);
			}
			break;
		case TriggerCatcher.Interaction.Stay:
			if (m_DamageType != DamageType.DPS)
			{
				_ = m_DamageType;
				_ = 2;
			}
			break;
		case TriggerCatcher.Interaction.Exit:
			if (m_DamageType == DamageType.KeepAlive)
			{
				m_CollidersToKill.Add(other);
			}
			break;
		case TriggerCatcher.Interaction.Enter | TriggerCatcher.Interaction.Exit:
			break;
		}
	}

	private void TryKill(Collider other, bool verifyInside)
	{
		if (!other.attachedRigidbody)
		{
			return;
		}
		Visible componentInChildren = other.attachedRigidbody.GetComponentInChildren<Visible>();
		if (componentInChildren == null)
		{
			return;
		}
		if (verifyInside)
		{
			if (!m_Collider.bounds.Intersects(other.GetComponent<Collider>().bounds))
			{
				return;
			}
		}
		else if (m_Collider.Contains(componentInChildren.centrePosition))
		{
			return;
		}
		OnDamageVolumeKillEvent.Send(componentInChildren);
		if ((bool)componentInChildren.tank)
		{
			d.LogFormat("DamageVolume {0} destroying tech {1}", base.name, componentInChildren.tank.name);
			componentInChildren.tank.blockman.Disintegrate();
		}
		else if ((bool)componentInChildren.damageable)
		{
			Singleton.Manager<ManDamage>.inst.DealDamage(componentInChildren.damageable, componentInChildren.damageable.Health, ManDamage.DamageType.Standard, this);
		}
	}

	private void Update()
	{
		for (int i = 0; i < m_CollidersToKill.Count; i++)
		{
			TryKill(m_CollidersToKill[i], verifyInside: false);
		}
		m_CollidersToKill.Clear();
		for (int j = 0; j < m_CollidersToVerifyAndKill.Count; j++)
		{
			TryKill(m_CollidersToVerifyAndKill[j], verifyInside: true);
		}
		m_CollidersToVerifyAndKill.Clear();
	}

	private void OnPool()
	{
		m_Collider = GetComponent<SphereCollider>();
		if (!m_Collider)
		{
			m_Collider = GetComponent<BoxCollider>();
		}
		if ((bool)m_Collider)
		{
			m_Collider.isTrigger = true;
		}
		else
		{
			d.LogError("DamageVolume - Needs a SphereCollider or a BoxCollider on it to function");
		}
	}

	private void OnSpawn()
	{
		TriggerCatcher.Subscribe(m_Collider.gameObject, TriggerCatcher.Interaction.Enter | TriggerCatcher.Interaction.Exit | TriggerCatcher.Interaction.Stay, OnContainmentTrigger);
		if (m_StartActive)
		{
			Enable(enable: true);
		}
	}

	private void OnRecycle()
	{
		Enable(enable: false);
		TriggerCatcher.Unsubscribe(m_Collider.gameObject, TriggerCatcher.Interaction.Enter | TriggerCatcher.Interaction.Exit | TriggerCatcher.Interaction.Stay, OnContainmentTrigger);
		OnDamageVolumeKillEvent.EnsureNoSubscribers();
	}
}
