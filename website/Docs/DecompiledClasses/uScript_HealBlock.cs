#define UNITY_EDITOR
public class uScript_HealBlock : uScriptLogic
{
	public bool Out => true;

	public void In(TankBlock block, float healAmount)
	{
		if (block != null)
		{
			block.visible.damageable.Repair(healAmount, sendEvent: false);
		}
		else
		{
			d.LogError("uScript_HealBlock - block is null");
		}
	}
}
