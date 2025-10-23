using UnityEngine;

[RequireComponent(typeof(Visible))]
public class Waypoint : MonoBehaviour
{
	public Transform trans { get; private set; }

	public Visible visible { get; private set; }

	public NetWaypoint netWaypoint { get; private set; }

	private void OnPool()
	{
		trans = base.transform;
		visible = GetComponent<Visible>();
		netWaypoint = GetComponent<NetWaypoint>();
	}
}
