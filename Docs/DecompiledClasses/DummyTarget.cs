using UnityEngine;

public class DummyTarget : MonoBehaviour
{
	public float m_ResetDuration = 1f;

	private float m_AccumulatedDamage;

	private float m_Timer;

	private DigitalDisplay m_Display;

	private int m_ShownDamage;

	private TankBlock m_Block;

	private bool OnRejectDamage(ManDamage.DamageInfo info, bool actuallyDealDamage)
	{
		if (actuallyDealDamage)
		{
			if (m_Block != null)
			{
				m_Block.StartMaterialPulse(ManTechMaterialSwap.MaterialTypes.Damage, ManTechMaterialSwap.MaterialColour.Damage);
			}
			m_AccumulatedDamage += info.Damage;
		}
		return true;
	}

	private void OnPool()
	{
		Damageable component = GetComponent<Damageable>();
		if (component != null)
		{
			component.SetRejectDamageHandler(OnRejectDamage);
		}
	}

	private void OnSpawn()
	{
		m_Block = GetComponentInParent<TankBlock>();
	}

	private void FindDisplay()
	{
		if (!(m_Display == null))
		{
			return;
		}
		DigitalDisplay[] array = Object.FindObjectsOfType<DigitalDisplay>();
		float num = float.MaxValue;
		DigitalDisplay[] array2 = array;
		foreach (DigitalDisplay digitalDisplay in array2)
		{
			float sqrMagnitude = (digitalDisplay.transform.position - base.transform.position).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				num = sqrMagnitude;
				m_Display = digitalDisplay;
			}
		}
	}

	private void OnRecycle()
	{
		m_Timer = 0f;
		m_AccumulatedDamage = 0f;
		m_Display = null;
		m_Block = null;
	}

	private void Update()
	{
		FindDisplay();
		if (!(m_AccumulatedDamage > 0f))
		{
			return;
		}
		m_Timer += Time.deltaTime;
		if (m_Timer >= m_ResetDuration)
		{
			m_Timer -= m_ResetDuration;
			m_ShownDamage = (int)m_AccumulatedDamage;
			m_AccumulatedDamage = 0f;
			if (m_Display != null && m_ShownDamage > 0)
			{
				m_Display.SetValue(m_ShownDamage);
			}
		}
	}
}
