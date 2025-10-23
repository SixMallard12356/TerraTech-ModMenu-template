using UnityEngine;

public class UIPartialFade : MonoBehaviour
{
	public RectTransform[] m_FadeCanvases;

	public RectTransform m_SizeRect;

	private Vector2 m_ReferenceResolution = new Vector2(1920f, 1080f);

	private Rect m_CropRect;

	public void SetCropRect(Rect cropRect)
	{
		m_CropRect = cropRect;
		UpdateRect();
	}

	private void UpdateRect()
	{
		m_ReferenceResolution = new Vector2(m_SizeRect.rect.width, m_SizeRect.rect.height);
		float num = m_CropRect.yMin / (float)Screen.height;
		float num2 = ((float)Screen.height - m_CropRect.yMax) / (float)Screen.height;
		float num3 = m_CropRect.xMin / (float)Screen.width;
		float num4 = ((float)Screen.width - m_CropRect.xMax) / (float)Screen.width;
		float y = m_CropRect.height / (float)Screen.height * m_ReferenceResolution.y;
		m_FadeCanvases[0].sizeDelta = new Vector2(m_ReferenceResolution.x, num * m_ReferenceResolution.y);
		m_FadeCanvases[0].anchoredPosition = new Vector2(0f, m_ReferenceResolution.y - num * m_ReferenceResolution.y);
		m_FadeCanvases[1].sizeDelta = new Vector2(m_ReferenceResolution.x, num2 * m_ReferenceResolution.y);
		m_FadeCanvases[1].anchoredPosition = new Vector2(0f, 0f);
		m_FadeCanvases[2].sizeDelta = new Vector2(num3 * m_ReferenceResolution.x, y);
		m_FadeCanvases[2].anchoredPosition = new Vector2(0f, num2 * m_ReferenceResolution.y);
		m_FadeCanvases[3].sizeDelta = new Vector2(num4 * m_ReferenceResolution.x, y);
		m_FadeCanvases[3].anchoredPosition = new Vector2(m_ReferenceResolution.x - num3 * m_ReferenceResolution.x, num2 * m_ReferenceResolution.y);
	}

	private void Update()
	{
		UpdateRect();
	}
}
