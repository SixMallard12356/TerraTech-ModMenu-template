using System.Collections.Generic;
using UnityEngine;

public class ZeroPointProjectile : ZoneProjectile, IGravityManipulator
{
	[SerializeField]
	private float m_Strength;

	private List<IGravityAdjustmentTarget> m_CurrentAdjustmentTargets;

	private float m_GravityDelta;

	private static Dictionary<Collider, IGravityAdjustmentTarget> s_CachedTargetLookup = new Dictionary<Collider, IGravityAdjustmentTarget>();

	public void UpdateGravityAdjustmentTargets()
	{
		m_CurrentAdjustmentTargets.Clear();
		if (base.Deployed)
		{
			foreach (Collider item in PhysicsUtils.OverlapSphereAllNonAlloc(GetZonePosition(), base.ZoneRadius, m_LayersToAffect.value))
			{
				if ((object)item.gameObject == base.gameObject)
				{
					continue;
				}
				if (!s_CachedTargetLookup.TryGetValue(item, out var value) || value == null)
				{
					Visible visible = Singleton.Manager<ManVisible>.inst.FindVisible(item);
					value = (((bool)visible && visible.type == ObjectTypes.Block) ? visible.block : ((!visible || visible.type != ObjectTypes.Chunk) ? item.GetComponentInParent<IGravityAdjustmentTarget>() : visible.pickup));
					if (value != null)
					{
						s_CachedTargetLookup.Add(item, value);
					}
				}
				if (value != null && !value.HasAdjustmentBeenTouched())
				{
					value.SetAdjustmentTouched(touched: true);
					m_CurrentAdjustmentTargets.Add(value);
				}
			}
		}
		foreach (IGravityAdjustmentTarget currentAdjustmentTarget in m_CurrentAdjustmentTargets)
		{
			currentAdjustmentTarget.SetAdjustmentTouched(touched: false);
		}
	}

	public List<IGravityAdjustmentTarget> GetGravityAdjustmentTargets()
	{
		return m_CurrentAdjustmentTargets;
	}

	public GravityManipulationZone GetGravityManipulationZone()
	{
		return new GravityManipulationZone
		{
			m_Position = GetZonePosition(),
			m_Radius = base.ZoneRadius,
			m_ManipulationAmount = m_Strength
		};
	}

	public void SetGravityDelta(float delta)
	{
		m_GravityDelta = delta;
	}

	private void OnPool()
	{
		m_CurrentAdjustmentTargets = new List<IGravityAdjustmentTarget>();
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManGravity>.inst.RegisterGravityManipulator(this);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManGravity>.inst.UnRegisterGravityManipulator(this);
	}
}
