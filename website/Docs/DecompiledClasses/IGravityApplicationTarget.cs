using UnityEngine;

public interface IGravityApplicationTarget
{
	float GetGravityScale();

	Rigidbody GetApplicationRigidbody();

	Vector3 GetWorldCentreOfGravity();

	bool CanApplyGravity();

	void SetApplicationTouched(bool touched);

	bool HasApplicationBeenTouched();
}
