#define UNITY_EDITOR
public class uScript_DamageBlocks : uScriptLogic
{
	public bool Out => true;

	public void In(TankBlock[] blocks, [DefaultValue(0f)] float damagePercentage)
	{
		if (blocks != null)
		{
			float num = damagePercentage / 100f;
			for (int i = 0; i < blocks.Length; i++)
			{
				if (blocks[i] != null)
				{
					Damageable damageable = blocks[i].visible.damageable;
					Singleton.Manager<ManDamage>.inst.DealDamage(damageable, damageable.MaxHealth * num, ManDamage.DamageType.Standard, null);
				}
				else
				{
					d.LogError("uScript_DamageBlocks - block is null");
				}
			}
		}
		else
		{
			d.LogError("uScript_DamageBlocks - array of blocks is null");
		}
	}
}
