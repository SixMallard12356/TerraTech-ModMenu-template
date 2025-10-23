using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WorldPushbackBarrierTrigger : MonoBehaviour
{
	public Event<Visible> OnRigidbodyEnter;

	public Event<Visible> OnRigidbodyExit;

	private BoxCollider m_Collider;

	private void OnTriggerEnter(Collider collider)
	{
		Visible visibleOfInterest = GetVisibleOfInterest(collider);
		if (IsInFilter(visibleOfInterest, addingBody: true))
		{
			OnRigidbodyEnter.Send(visibleOfInterest);
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		Visible visibleOfInterest = GetVisibleOfInterest(collider);
		if (IsInFilter(visibleOfInterest, addingBody: false))
		{
			OnRigidbodyExit.Send(visibleOfInterest);
		}
	}

	private bool IsInFilter(Visible visible, bool addingBody)
	{
		if (visible == null || (visible.tank == null && visible.block == null))
		{
			return false;
		}
		return true;
	}

	private Visible GetVisibleOfInterest(Collider collider)
	{
		Visible visible = Singleton.Manager<ManVisible>.inst.FindVisible(collider);
		if (visible != null && (visible.tank != null || visible.block != null))
		{
			Tank tank = ((visible.block != null) ? visible.block.tank : visible.tank);
			if (tank != null)
			{
				return tank.visible;
			}
		}
		return visible;
	}

	private void Awake()
	{
		m_Collider = GetComponent<BoxCollider>();
		m_Collider.isTrigger = true;
	}
}
