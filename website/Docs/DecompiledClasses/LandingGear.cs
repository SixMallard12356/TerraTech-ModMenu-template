using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public class LandingGear : MonoBehaviour
{
	private BoxCollider m_Trigger;

	private int m_DeployLandingGear;

	private List<ModuleLandingGear> m_LandingGear = new List<ModuleLandingGear>(4);

	public bool IsDeployed => m_DeployLandingGear > 0;

	public int Count => m_LandingGear.Count;

	public void Add(ModuleLandingGear gear)
	{
		m_LandingGear.Add(gear);
	}

	public void Remove(ModuleLandingGear gear)
	{
		m_LandingGear.Remove(gear);
	}

	public void Recalculate(Bounds techBounds)
	{
		if (m_LandingGear.Count <= 0)
		{
			return;
		}
		float num = 0f;
		foreach (ModuleLandingGear item in m_LandingGear)
		{
			num = Mathf.Max(num, item.DeployBelowAltitude);
		}
		if (m_Trigger == null)
		{
			m_Trigger = base.gameObject.AddComponent<BoxCollider>();
			base.gameObject.layer = Globals.inst.layerTerrainOnly;
			m_Trigger.isTrigger = true;
		}
		m_Trigger.enabled = false;
		Bounds bounds = techBounds;
		bounds.min -= new Vector3(0f, num, 0f);
		m_Trigger.center = bounds.center;
		m_Trigger.size = bounds.size;
		m_DeployLandingGear = 0;
		m_Trigger.enabled = true;
	}

	private static bool ShouldTrigger(GameObject go)
	{
		return go.IsTerrain();
	}

	private void OnTriggerEnter(Collider otherCollider)
	{
		if (!ShouldTrigger(otherCollider.gameObject))
		{
			return;
		}
		if (m_DeployLandingGear == 0)
		{
			foreach (ModuleLandingGear item in m_LandingGear)
			{
				item.OnLandingGearEvent(deploy: true);
			}
		}
		m_DeployLandingGear++;
	}

	private void OnTriggerExit(Collider otherCollider)
	{
		if (!ShouldTrigger(otherCollider.gameObject))
		{
			return;
		}
		m_DeployLandingGear--;
		if (m_DeployLandingGear != 0)
		{
			return;
		}
		foreach (ModuleLandingGear item in m_LandingGear)
		{
			item.OnLandingGearEvent(deploy: false);
		}
	}

	private void OnDrawGizmos()
	{
		if (base.gameObject.EditorSelectedSingle())
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireCube(m_Trigger.center, m_Trigger.size);
		}
	}
}
