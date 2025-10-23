using UnityEngine;

public class UIThrottle3D : MonoBehaviour
{
	[SerializeField]
	private Transform m_AxisXPositive;

	[SerializeField]
	private Transform m_AxisXNegative;

	[SerializeField]
	private Transform m_AxisYPositive;

	[SerializeField]
	private Transform m_AxisYNegative;

	[SerializeField]
	private Transform m_AxisZPositive;

	[SerializeField]
	private Transform m_AxisZNegative;

	[SerializeField]
	private float m_MaximumScale;

	private Vector3 m_CurrentValues = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

	private const float kMinVisibleThrottle = 0.0001f;

	public void UpdateDisplay(TankControl tankControl)
	{
		tankControl.GetThrottle(0, out var throttle);
		tankControl.GetThrottle(1, out var throttle2);
		tankControl.GetThrottle(2, out var throttle3);
		UpdateAxis(m_AxisXNegative, m_AxisXPositive, throttle, ref m_CurrentValues.x);
		UpdateAxis(m_AxisYNegative, m_AxisYPositive, throttle2, ref m_CurrentValues.y);
		UpdateAxis(m_AxisZNegative, m_AxisZPositive, throttle3, ref m_CurrentValues.z);
	}

	private void UpdateAxis(Transform negative, Transform positive, float value, ref float currentValue)
	{
		if (currentValue != value)
		{
			bool flag = Mathf.Abs(currentValue) < 0.0001f;
			bool num = Mathf.Abs(value) < 0.0001f;
			bool flag2 = !num;
			if (num != flag)
			{
				positive.gameObject.SetActive(flag2);
				negative.gameObject.SetActive(flag2);
			}
			if (flag2)
			{
				positive.localScale = new Vector3(1f, 1f, 1f + (m_MaximumScale - 1f) * Mathf.Clamp01(value));
				negative.localScale = new Vector3(1f, 1f, 1f + (m_MaximumScale - 1f) * Mathf.Clamp01(0f - value));
			}
			currentValue = value;
		}
	}
}
