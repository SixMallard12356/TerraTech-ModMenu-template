using System;
using UnityEngine;
using UnityEngine.UI;

public class UIThrottle : UIHUDElement
{
	[Serializable]
	public struct Axis
	{
		public RectTransform m_NumberLocation;

		public Text m_NumericDisplay;

		[NonSerialized]
		public float m_CurrentNumericValue;

		[NonSerialized]
		public Vector2 m_CurrentNumberPos;
	}

	[SerializeField]
	private Axis m_AxisX;

	[SerializeField]
	private Axis m_AxisY;

	[SerializeField]
	private Axis m_AxisZ;

	[SerializeField]
	private UIThrottle3D m_3DAxes;

	[SerializeField]
	private float m_TextDistance;

	private TankControl m_TankControl;

	private static UIThrottle s_ActiveInstance;

	public static void SetEnabled(bool enable)
	{
		if (s_ActiveInstance != null && (bool)s_ActiveInstance.m_3DAxes)
		{
			s_ActiveInstance.m_3DAxes.gameObject.SetActive(enable);
		}
	}

	public override void Show(object context)
	{
		base.Show(context);
		TankControl tankControl = context as TankControl;
		if ((bool)tankControl)
		{
			m_TankControl = tankControl;
		}
		m_AxisX.m_CurrentNumericValue = float.PositiveInfinity;
		m_AxisY.m_CurrentNumericValue = float.PositiveInfinity;
		m_AxisZ.m_CurrentNumericValue = float.PositiveInfinity;
		s_ActiveInstance = this;
		if ((bool)m_3DAxes)
		{
			m_3DAxes.gameObject.SetActive(Singleton.Manager<ManHUD>.inst.IsVisible);
		}
	}

	public override void Hide(object context)
	{
		if (context as TankControl == m_TankControl)
		{
			m_TankControl = null;
		}
		base.Hide(context);
		if (s_ActiveInstance == this)
		{
			s_ActiveInstance = null;
		}
	}

	private void UpdateAxis(ref Axis axis, int axisIndex, Vector3 direction)
	{
		float throttle2;
		bool throttle = m_TankControl.GetThrottle(axisIndex, out throttle2);
		if (!axis.m_NumericDisplay)
		{
			return;
		}
		bool flag = false;
		if (throttle)
		{
			float num = Mathf.Round(throttle2 * 1000f);
			flag = num != 0f;
			if (flag)
			{
				if (axis.m_CurrentNumericValue != num)
				{
					axis.m_CurrentNumericValue = num;
					axis.m_NumericDisplay.text = (num * 0.1f).ToString("+0.0;-0.0");
				}
				Vector2 vector = new Vector2(direction.x, direction.y).normalized;
				float textDistance = m_TextDistance;
				textDistance += Mathf.Lerp(axis.m_NumericDisplay.preferredHeight, axis.m_NumericDisplay.preferredWidth, Mathf.Abs(vector.x)) * 0.5f;
				vector *= textDistance;
				if (throttle2 < 0f)
				{
					vector = -vector;
				}
				vector.x = Mathf.Round(vector.x);
				vector.y = Mathf.Round(vector.y);
				if ((bool)axis.m_NumberLocation && axis.m_CurrentNumberPos != vector)
				{
					axis.m_CurrentNumberPos = vector;
					axis.m_NumberLocation.anchoredPosition = vector;
				}
			}
		}
		axis.m_NumericDisplay.gameObject.SetActive(flag);
	}

	private void LateUpdate()
	{
		if ((bool)m_TankControl)
		{
			Quaternion quaternion = Quaternion.Inverse(Singleton.cameraTrans.rotation) * m_TankControl.Tech.trans.rotation;
			if ((bool)m_3DAxes)
			{
				m_3DAxes.transform.localRotation = quaternion;
				m_3DAxes.UpdateDisplay(m_TankControl);
			}
			UpdateAxis(ref m_AxisX, 0, quaternion * Vector3.right);
			UpdateAxis(ref m_AxisY, 1, quaternion * Vector3.up);
			UpdateAxis(ref m_AxisZ, 2, quaternion * Vector3.forward);
		}
	}

	private void OnPool()
	{
		RegisterObscuredBy(ManHUD.HUDElementType.WorldMap);
	}
}
