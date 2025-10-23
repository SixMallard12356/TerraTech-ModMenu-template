using System.Collections.Generic;
using UnityEngine;

public class ManGravity : Singleton.Manager<ManGravity>
{
	public static bool ManipulatorsEnabled = true;

	private List<IGravityManipulator> m_GravityManipulators;

	private List<IGravityAdjustmentTarget> m_AdjustmentTargetsToReset;

	private List<IGravityAdjustmentTarget> m_AdjustmentTargets;

	private List<IGravityApplicationTarget> m_ApplicationTargets;

	private List<IGravityApplicationTarget> m_ApplicationTargetsToReset;

	public void RegisterGravityManipulator(IGravityManipulator manipulator)
	{
		m_GravityManipulators.Add(manipulator);
	}

	public void UnRegisterGravityManipulator(IGravityManipulator manipulator)
	{
		m_GravityManipulators.Remove(manipulator);
	}

	private void Start()
	{
		m_GravityManipulators = new List<IGravityManipulator>();
		m_AdjustmentTargets = new List<IGravityAdjustmentTarget>();
		m_AdjustmentTargetsToReset = new List<IGravityAdjustmentTarget>();
		m_ApplicationTargets = new List<IGravityApplicationTarget>();
		m_ApplicationTargetsToReset = new List<IGravityApplicationTarget>();
	}

	private void Update()
	{
		if (Singleton.Manager<ManPauseGame>.inst.IsPaused)
		{
			return;
		}
		List<IGravityAdjustmentTarget> adjustmentTargetsToReset = m_AdjustmentTargetsToReset;
		m_AdjustmentTargetsToReset = m_AdjustmentTargets;
		m_AdjustmentTargets = adjustmentTargetsToReset;
		m_AdjustmentTargets.Clear();
		List<IGravityApplicationTarget> applicationTargetsToReset = m_ApplicationTargetsToReset;
		m_ApplicationTargetsToReset = m_ApplicationTargets;
		m_ApplicationTargets = applicationTargetsToReset;
		m_ApplicationTargets.Clear();
		foreach (IGravityManipulator gravityManipulator in m_GravityManipulators)
		{
			gravityManipulator.UpdateGravityAdjustmentTargets();
		}
		if (ManipulatorsEnabled)
		{
			foreach (IGravityManipulator gravityManipulator2 in m_GravityManipulators)
			{
				List<IGravityAdjustmentTarget> gravityAdjustmentTargets = gravityManipulator2.GetGravityAdjustmentTargets();
				float num = 0f;
				GravityManipulationZone gravityManipulationZone = gravityManipulator2.GetGravityManipulationZone();
				foreach (IGravityAdjustmentTarget item in gravityAdjustmentTargets)
				{
					if (!item.HasAdjustmentBeenTouched())
					{
						item.PrimeForGravityAdjustment();
						item.SetAdjustmentTouched(touched: true);
						m_AdjustmentTargets.Add(item);
						IGravityApplicationTarget gravityApplicationTarget = item.GetGravityApplicationTarget();
						if (!gravityApplicationTarget.HasApplicationBeenTouched())
						{
							m_ApplicationTargets.Add(gravityApplicationTarget);
							gravityApplicationTarget.SetApplicationTouched(touched: true);
						}
					}
					num += item.AdjustGravity(gravityManipulationZone);
				}
				gravityManipulator2.SetGravityDelta(num);
			}
		}
		foreach (IGravityAdjustmentTarget item2 in m_AdjustmentTargetsToReset)
		{
			if (!item2.HasAdjustmentBeenTouched())
			{
				item2.ResetGravityAdjustment();
			}
		}
		m_AdjustmentTargetsToReset.Clear();
		foreach (IGravityApplicationTarget item3 in m_ApplicationTargetsToReset)
		{
			if (!item3.HasApplicationBeenTouched() && item3.CanApplyGravity())
			{
				Rigidbody applicationRigidbody = item3.GetApplicationRigidbody();
				if (applicationRigidbody != null)
				{
					applicationRigidbody.useGravity = true;
				}
			}
		}
		m_ApplicationTargetsToReset.Clear();
		foreach (IGravityAdjustmentTarget adjustmentTarget in m_AdjustmentTargets)
		{
			adjustmentTarget.FinaliseGravityAdjustment();
			adjustmentTarget.SetAdjustmentTouched(touched: false);
		}
		foreach (IGravityApplicationTarget applicationTarget in m_ApplicationTargets)
		{
			applicationTarget.SetApplicationTouched(touched: false);
		}
	}

	private void FixedUpdate()
	{
		float y = Physics.gravity.y;
		foreach (IGravityApplicationTarget applicationTarget in m_ApplicationTargets)
		{
			if (!applicationTarget.CanApplyGravity())
			{
				continue;
			}
			Rigidbody applicationRigidbody = applicationTarget.GetApplicationRigidbody();
			if (!applicationRigidbody.IsNotNull())
			{
				continue;
			}
			float gravityScale = applicationTarget.GetGravityScale();
			if (!(applicationRigidbody.useGravity = Mathf.Approximately(gravityScale, 1f)))
			{
				float num = gravityScale * y * applicationRigidbody.mass;
				if (num != 0f)
				{
					applicationRigidbody.AddForceAtPosition(new Vector3(0f, num, 0f), applicationTarget.GetWorldCentreOfGravity());
				}
			}
		}
	}
}
