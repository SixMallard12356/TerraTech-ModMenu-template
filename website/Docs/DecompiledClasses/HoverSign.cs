using UnityEngine;

public class HoverSign : MonoBehaviour
{
	[SerializeField]
	private float HoverRange;

	[SerializeField]
	private float HoverSpeed;

	private Vector3 originalPos;

	private Vector3 newPos;

	private Transform m_Transform;

	private void Start()
	{
		m_Transform = base.transform;
		originalPos = m_Transform.position;
	}

	private void Update()
	{
		newPos = m_Transform.position;
		newPos.y = originalPos.y + Mathf.Sin(Time.time * HoverSpeed) * HoverRange;
		m_Transform.position = newPos;
	}
}
