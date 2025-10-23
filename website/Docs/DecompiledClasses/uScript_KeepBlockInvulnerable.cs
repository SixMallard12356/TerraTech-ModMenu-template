public class uScript_KeepBlockInvulnerable : uScriptLogic
{
	public bool Out => true;

	public void In(TankBlock block)
	{
		if ((bool)block)
		{
			block.visible.damageable.SetInvulnerable(invulnerable: true, unlimitedInvulnerability: false);
		}
	}
}
