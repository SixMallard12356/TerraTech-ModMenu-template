using UnityEngine;

public class SpeedTrap : MonoBehaviour
{
	private DigitalDisplay m_Display;

	private int m_CurrentSpeed;

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
		m_CurrentSpeed = 0;
		m_Display = null;
	}

	private void OnTriggerEnter(Collider other)
	{
		FindDisplay();
		if (!(m_Display != null))
		{
			return;
		}
		Rigidbody component = other.gameObject.transform.root.GetComponent<Rigidbody>();
		if (component != null)
		{
			int num = Mathf.RoundToInt(GameUnits.GetSpeed(component.velocity.magnitude));
			if (num > m_CurrentSpeed || m_Display.IsClear)
			{
				m_CurrentSpeed = num;
				m_Display.SetValue(num);
			}
		}
	}
}
