#define UNITY_EDITOR
using UnityEngine;

public class uScript_SetTechBlocksHealth : uScriptLogic
{
	public bool Out => true;

	public void In(Tank tech, [DefaultValue(100f)] float healthPercentage)
	{
		if (tech != null)
		{
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tech.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				Damageable damageable = enumerator.Current.visible.damageable;
				float h = damageable.MaxHealth * Mathf.Clamp(healthPercentage, 0f, 100f) * 0.01f;
				damageable.InitHealth(h);
			}
		}
		else
		{
			d.LogError("uScript_SetTechBlocksHealth - tech is null");
		}
	}
}
