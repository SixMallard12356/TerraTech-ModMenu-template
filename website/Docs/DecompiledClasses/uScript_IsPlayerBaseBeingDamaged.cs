using System;

public class uScript_IsPlayerBaseBeingDamaged : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	public event uScriptEventHandler OnPlayerBaseDamaged;

	public event uScriptEventHandler OnPlayerHeartBaseDamaged;

	private void OnTankDamaged(Tank tank, ManDamage.DamageInfo dmg)
	{
		if (!ManSpawn.IsPlayerTeam(tank.Team) || !tank.IsAnchored)
		{
			return;
		}
		ManPlayer inst = Singleton.Manager<ManPlayer>.inst;
		int num;
		if ((bool)tank && (bool)tank.visible && (bool)inst && inst.HasHeartBlock)
		{
			num = (inst.DoesTechHavePlayerHeartBlock(tank) ? 1 : 0);
			if (num != 0)
			{
				goto IL_0066;
			}
		}
		else
		{
			num = 0;
		}
		if (this.OnPlayerBaseDamaged != null)
		{
			this.OnPlayerBaseDamaged(this, new EventArgs());
		}
		goto IL_0066;
		IL_0066:
		if (num != 0 && this.OnPlayerHeartBaseDamaged != null)
		{
			this.OnPlayerHeartBaseDamaged(this, new EventArgs());
		}
	}

	public void OnEnable()
	{
		Singleton.Manager<ManTechs>.inst.TankDamagedEvent.Subscribe(OnTankDamaged);
	}

	public void OnDisable()
	{
		Singleton.Manager<ManTechs>.inst.TankDamagedEvent.Unsubscribe(OnTankDamaged);
	}
}
