using UnityEngine;

public class DamageMultiplierTable : ScriptableObject
{
	[SerializeField]
	private float m_GlobalDamageMultiplier = 1f;

	[SerializeField]
	private float[] m_DamageTypeMultiplierLookup = new float[ManDamage.NumDamageTypes * ManDamage.NumDamageableTypes];

	public float GetDamageMultiplier(ManDamage.DamageType damageType, ManDamage.DamageableType damageableType, bool applyGlobalModifiers = true)
	{
		int num = (int)((int)damageType * ManDamage.NumDamageableTypes + damageableType);
		float num2 = m_DamageTypeMultiplierLookup[num];
		if (applyGlobalModifiers && m_GlobalDamageMultiplier != 1f)
		{
			num2 *= m_GlobalDamageMultiplier;
		}
		return num2;
	}

	public float GetGlobalDamageMultiplier()
	{
		return m_GlobalDamageMultiplier;
	}

	private void OnEnable()
	{
	}
}
