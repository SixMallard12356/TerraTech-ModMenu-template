using UnityEngine;

public class Weapons : ScriptableObject
{
	public void FireAtTarget(Tank tank, Vector3 targetPositionWorld, float targetRadiusWorld)
	{
		AimAtTarget(tank, targetPositionWorld, targetRadiusWorld);
		FireWeapons(tank);
	}

	public void AimAtTarget(Tank tank, Vector3 targetPositionWorld, float targetRadiusWorld)
	{
		tank.control.TargetPositionWorld = targetPositionWorld;
		tank.control.TargetRadiusWorld = targetRadiusWorld;
	}

	public void FireWeapons(Tank tank)
	{
		tank.control.FireControl = true;
	}
}
