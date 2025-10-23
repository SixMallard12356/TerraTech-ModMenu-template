public interface IGravityAdjustmentTarget
{
	void PrimeForGravityAdjustment();

	void ResetGravityAdjustment();

	float AdjustGravity(GravityManipulationZone zone);

	void FinaliseGravityAdjustment();

	IGravityApplicationTarget GetGravityApplicationTarget();

	void SetAdjustmentTouched(bool touched);

	bool HasAdjustmentBeenTouched();
}
