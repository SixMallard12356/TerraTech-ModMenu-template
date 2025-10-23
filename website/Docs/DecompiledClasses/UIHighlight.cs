#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UIHighlight : MonoBehaviour
{
	[SerializeField]
	private Image m_Image;

	[SerializeField]
	private Gradient m_Gradient;

	[SerializeField]
	private AnimationCurve m_AnimationCurve;

	[SerializeField]
	private float m_MaxScale;

	[SerializeField]
	private float m_TimeScale;

	private Rect m_Rect = kInvalidRect;

	private float m_Time;

	private static readonly Rect kInvalidRect = new Rect(float.MinValue, float.MinValue, float.MinValue, float.MinValue);

	public void SetSize(Rect rect)
	{
		m_Rect = rect;
	}

	private void Update()
	{
		d.Assert(m_Rect != kInvalidRect, "UIHighlight.SetSize() should be called before UIHighlight.Update() otherwise highlight does not know the expected size.");
		d.Assert(m_Image != null, "Image should not be null.  Is the prefab configured correctly?");
		m_Time += Time.deltaTime;
		float time = m_Time * m_TimeScale % 1f;
		m_Image.color = m_Gradient.Evaluate(time);
		if (m_Rect != kInvalidRect)
		{
			time = m_AnimationCurve.Evaluate(time);
			float size = Mathf.Lerp(m_Rect.width, m_Rect.width * m_MaxScale, time);
			float size2 = Mathf.Lerp(m_Rect.height, m_Rect.height * m_MaxScale, time);
			RectTransform obj = (RectTransform)base.transform;
			obj.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size2);
			obj.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
		}
	}
}
