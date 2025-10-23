using System.Collections.Generic;

public interface IGravityManipulator
{
	void UpdateGravityAdjustmentTargets();

	List<IGravityAdjustmentTarget> GetGravityAdjustmentTargets();

	GravityManipulationZone GetGravityManipulationZone();

	void SetGravityDelta(float delta);
}
