using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ModuleCircuitDispensor))]
public class ModuleCircuit_Sensor_Damage : Module, ICircuitDispensor
{
	private class DamageEvent
	{
		public int TicksRemaining;

		public float Damage;

		public DamageEvent(int tickDuration, float damage)
		{
			TicksRemaining = tickDuration;
			Damage = damage;
		}
	}

	[SerializeField]
	[Tooltip("How many ticks should this sensor output a signal for after the tech has been damaged?")]
	protected int m_NumOutputTicksOnDamaged = 10;

	[SerializeField]
	[Tooltip("Whether or not the output signal scales to the damage value. If false, outputs 1")]
	protected bool m_OutputDamageNumberSignal;

	private Queue<DamageEvent> m_DamageEvents = new Queue<DamageEvent>();

	private void RegisterDamageEvent(float damage)
	{
		if (DamageNumberToOutputSignal(damage) > 0)
		{
			m_DamageEvents.Enqueue(new DamageEvent(m_NumOutputTicksOnDamaged, damage));
		}
	}

	private int DoOutputTick()
	{
		float num = 0f;
		foreach (DamageEvent damageEvent in m_DamageEvents)
		{
			damageEvent.TicksRemaining--;
			num = Mathf.Max(num, damageEvent.Damage);
		}
		while (m_DamageEvents.Count > 0 && m_DamageEvents.Peek().TicksRemaining <= 0)
		{
			m_DamageEvents.Dequeue();
		}
		if (!m_OutputDamageNumberSignal)
		{
			if (num != 0f)
			{
				return base.block.CircuitNode.Dispensor.DefaultChargeStrength;
			}
			return 0;
		}
		return DamageNumberToOutputSignal(num);
	}

	private int DamageNumberToOutputSignal(float damageValue)
	{
		return (int)Mathf.Floor(damageValue);
	}

	private void Reset()
	{
		m_DamageEvents.Clear();
	}

	int ICircuitDispensor.GetDispensableCharge(Vector3 ap)
	{
		return DoOutputTick();
	}

	private void OnAttached()
	{
		base.block.tank.DamageEvent.Subscribe(OnDamaged);
	}

	private void OnDetaching()
	{
		base.block.tank.DamageEvent.Unsubscribe(OnDamaged);
		Reset();
	}

	private void OnDamaged(ManDamage.DamageInfo damageInfo)
	{
		RegisterDamageEvent(damageInfo.Damage);
	}

	private void PrePool()
	{
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
	}

	private void OnDepool()
	{
		base.block.AttachedEvent.Unsubscribe(OnAttached);
		base.block.DetachingEvent.Unsubscribe(OnDetaching);
	}

	private void OnSpawn()
	{
	}

	private void OnRecycle()
	{
		Reset();
	}
}
