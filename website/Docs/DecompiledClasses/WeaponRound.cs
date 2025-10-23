using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public abstract class WeaponRound : MonoBehaviour
{
	[SerializeField]
	protected ManDamage.DamageType m_DamageType;

	[SerializeField]
	protected int m_Damage = 1;

	public int ShortlivedUID { get; set; } = int.MinValue;

	public ManDamage.DamageType DamageType => m_DamageType;

	public int Damage => m_Damage;

	public abstract void Fire(Vector3 fireDirection, Transform firingOrigin, FireData fireData, ModuleWeapon weapon, Tank shooter = null, bool seekingRounds = false, bool replayRounds = false);

	public abstract void GetVariationParameters(out Vector3 fireDirection, out Vector3 fireSpin);

	public abstract void SetVariationParameters(Vector3 fireDirection, Vector3 fireSpin);

	private void PrePool()
	{
		base.gameObject.AddComponent<WorldSpaceObject>();
	}

	private void OnRecycle()
	{
		ManCombat.Projectiles.UnregisterWeaponRound(this);
	}
}
