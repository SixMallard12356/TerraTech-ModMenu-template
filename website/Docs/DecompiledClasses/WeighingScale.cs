using System.Collections.Generic;
using UnityEngine;

public class WeighingScale : MonoBehaviour
{
	public float m_RefreshDelay = 0.1f;

	private DigitalDisplay m_Display;

	private float m_Timer;

	private static List<Rigidbody> s_Bodies = new List<Rigidbody>();

	private static Collider[] s_Colliders = new Collider[1024];

	private BoxCollider m_TriggerBox;

	private void OnSpawn()
	{
		BoxCollider[] components = GetComponents<BoxCollider>();
		foreach (BoxCollider boxCollider in components)
		{
			if (boxCollider.isTrigger)
			{
				m_TriggerBox = boxCollider;
				break;
			}
		}
	}

	private void FindDisplay()
	{
		if (!(m_Display == null))
		{
			return;
		}
		DigitalDisplay[] array = Object.FindObjectsOfType<DigitalDisplay>();
		float num = float.MaxValue;
		DigitalDisplay[] array2 = array;
		foreach (DigitalDisplay digitalDisplay in array2)
		{
			float sqrMagnitude = (digitalDisplay.transform.position - base.transform.position).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				num = sqrMagnitude;
				m_Display = digitalDisplay;
			}
		}
	}

	private void OnRecycle()
	{
		m_Display = null;
		m_Timer = 0f;
	}

	private void Update()
	{
		m_Timer += Time.deltaTime;
		if (!(m_Timer >= m_RefreshDelay))
		{
			return;
		}
		m_Timer = 0f;
		if (m_TriggerBox != null)
		{
			float num = 0f;
			int num2 = Physics.OverlapBoxNonAlloc(m_TriggerBox.transform.TransformPoint(m_TriggerBox.center), 0.5f * m_TriggerBox.size, s_Colliders, m_TriggerBox.transform.rotation);
			for (int i = 0; i < num2; i++)
			{
				Transform root = s_Colliders[i].gameObject.transform.root;
				if (!(root == base.transform))
				{
					Rigidbody component = root.GetComponent<Rigidbody>();
					s_Colliders[i] = null;
					if (component != null && !s_Bodies.Contains(component))
					{
						num = ((!(root.GetComponent<Tank>() != null)) ? (num + component.mass) : (num + component.mass * 0.5f));
						s_Bodies.Add(component);
					}
				}
			}
			FindDisplay();
			if (m_Display != null && num > 0f)
			{
				m_Display.SetValue(num);
			}
		}
		s_Bodies.Clear();
	}
}
