public interface IModuleDamager
{
	ManDamage.DamageType DamageType { get; }

	float GetHitDamage();

	float GetHitsPerSec();
}
