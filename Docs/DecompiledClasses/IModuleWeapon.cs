using UnityEngine;

public interface IModuleWeapon
{
	bool UpdateDeployment(bool deploy);

	bool PrepareFiring(bool prepareFiring);

	int ProcessFiring(bool firing);

	bool ReadyToFire();

	bool FiringObstructed();

	bool IsAimingAtFloor(float limitedAngle);

	float GetVelocity();

	float GetRange();

	bool AimWithTrajectory();

	Transform GetFireTransform();

	float GetFireRateFraction();
}
