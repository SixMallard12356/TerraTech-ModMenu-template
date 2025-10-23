using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerTimer : MonoBehaviour
{
	[SerializeField]
	private RectTransform m_Arrow;

	[SerializeField]
	private Image m_PlayerIcon;

	[SerializeField]
	private Text m_Timer;

	private RectTransform rectTransform;

	private Transform m_Target;

	private float m_Time;

	private float m_CurrentTime;

	public void Initialize(Transform target, Color playerColor)
	{
		rectTransform = base.transform as RectTransform;
		rectTransform.localScale = Vector3.one;
		rectTransform.localPosition = Vector3.zero;
		m_Target = target;
		m_PlayerIcon.color = playerColor;
		Update();
	}

	public void StartTimer(float time)
	{
		m_CurrentTime = (m_Time = time);
		base.gameObject.SetActive(value: true);
	}

	private void UpdateTimer()
	{
		m_CurrentTime = Mathf.Clamp(m_CurrentTime - Time.deltaTime, 0f, m_Time);
		m_Timer.text = Mathf.CeilToInt(m_CurrentTime).ToString();
		m_PlayerIcon.fillAmount = m_CurrentTime / m_Time;
	}

	private void OnEnable()
	{
		if (m_Target != null)
		{
			Update();
		}
	}

	private void Update()
	{
		UpdateTimer();
		Vector3 position = Singleton.camera.WorldToScreenPoint(m_Target.position + Vector3.up * 2f);
		Vector2 anchorMin;
		if (position.z > 0f && position.x > 0f && position.y > 0f && position.x < (float)Screen.width && position.y < (float)Screen.height)
		{
			Vector3 vector = Singleton.camera.ScreenToViewportPoint(position);
			RectTransform obj = rectTransform;
			anchorMin = (rectTransform.anchorMax = vector);
			obj.anchorMin = anchorMin;
			m_Arrow.rotation = Quaternion.Euler(0f, 0f, 180f);
			return;
		}
		if (position.z < 0f)
		{
			position *= -1f;
		}
		Vector3 vector3 = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
		position -= vector3;
		float num = Mathf.Atan2(position.y, position.x) - (float)Math.PI / 2f;
		float num2 = Mathf.Cos(num);
		float num3 = 0f - Mathf.Sin(num);
		position = vector3 + new Vector3(num3 * 150f, num2 * 150f, 0f);
		Vector3 vector4 = vector3 * 0.9f;
		float num4 = num2 / num3;
		position = ((!(num2 > 0f)) ? new Vector3((0f - vector4.y) / num4, 0f - vector4.y, 0f) : new Vector3(vector4.y / num4, vector4.y, 0f));
		if (position.x > vector4.x)
		{
			position = new Vector3(vector4.x, vector4.x * num4, 0f);
		}
		else if (position.x < 0f - vector4.x)
		{
			position = new Vector3(0f - vector4.x, (0f - vector4.x) * num4, 0f);
		}
		position += vector3;
		Vector3 vector5 = Singleton.camera.ScreenToViewportPoint(position);
		RectTransform obj2 = rectTransform;
		anchorMin = (rectTransform.anchorMax = vector5);
		obj2.anchorMin = anchorMin;
		m_Arrow.rotation = Quaternion.Euler(0f, 0f, num * 57.29578f);
	}
}
