using UnityEngine;

public class EventRelay : MonoBehaviour
{
	public EventNoParams UpdateEvent;

	public EventNoParams FixedUpdateEvent;

	public Event<Collision> CollisionEnterEvent;

	public Event<Collision> CollisonStayEvent;

	public Event<Collision> CollisionExitEvent;

	public Event<Collider> TriggerEnterEvent;

	public Event<Collider> TriggerStayEvent;

	public Event<Collider> TriggerExitEvent;

	private void Update()
	{
		UpdateEvent.Send();
	}

	private void FixedUpdate()
	{
		FixedUpdateEvent.Send();
	}

	private void OnCollisionEnter(Collision collision)
	{
		CollisionEnterEvent.Send(collision);
	}

	private void OnCollisionStay(Collision collision)
	{
		CollisonStayEvent.Send(collision);
	}

	private void OnCollisionExit(Collision collision)
	{
		CollisionExitEvent.Send(collision);
	}

	private void OnTriggerEnter(Collider collider)
	{
		TriggerEnterEvent.Send(collider);
	}

	private void OnTriggerStay(Collider collider)
	{
		TriggerStayEvent.Send(collider);
	}

	private void OnTriggerExit(Collider collider)
	{
		TriggerExitEvent.Send(collider);
	}
}
