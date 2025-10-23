using UnityEngine;
using UnityEngine.UI;

public class UIOutOfBoundsHUD : UIHUDElement
{
	[SerializeField]
	private Image m_FadeImage;

	[SerializeField]
	private RectTransform m_DirectionMarker;

	public void SetOutOfBoundsPercentage(float percentageOutOfBounds)
	{
		Color color = m_FadeImage.color;
		float a = percentageOutOfBounds * 0.5f;
		color.a = a;
		m_FadeImage.color = color;
	}

	public void SetOutOfBoundsDirection(Vector3 directionToCenter, Vector3 camFwd, Vector3 camRight)
	{
		Vector3 vector = directionToCenter.SetY(0f);
		Vector3 vector2 = camFwd.SetY(0f);
		Vector3 vector3 = camRight.SetY(0f);
		float f = Vector3.Dot(vector.normalized, vector2.normalized);
		float num = Vector3.Dot(vector.normalized, vector3.normalized);
		float num2 = Mathf.Acos(f) * 57.29578f;
		if (num >= 0f)
		{
			num2 = 0f - num2;
		}
		m_DirectionMarker.rotation = Quaternion.Euler(0f, 0f, num2);
	}
}
