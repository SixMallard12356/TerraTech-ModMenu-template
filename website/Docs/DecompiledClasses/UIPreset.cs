using UnityEngine;
using UnityEngine.UI;

public class UIPreset : MonoBehaviour
{
	[SerializeField]
	private Text m_VehicleName;

	[SerializeField]
	private Image m_SpriteImage;

	[SerializeField]
	private GameObject m_UnavailablePanel;

	[SerializeField]
	private Text m_Creator;

	[SerializeField]
	private RectTransform m_ReferenceSizeTransform;

	private Snapshot m_MyCap;

	private Vector2 m_InitialDeltaSize;

	public void SetData(Snapshot decodedCap, bool isAvailable = true)
	{
		m_MyCap = decodedCap;
		decodedCap.ResolveThumbnail(OnResolveThumbnail);
		if (m_VehicleName != null && decodedCap.techData != null)
		{
			m_VehicleName.text = decodedCap.techData.Name;
		}
		if (m_Creator != null)
		{
			m_Creator.text = GetCreator();
		}
		if (m_UnavailablePanel != null)
		{
			m_UnavailablePanel.SetActive(isAvailable);
		}
	}

	public Snapshot GetData()
	{
		return m_MyCap;
	}

	public void Clear()
	{
		m_MyCap = null;
		m_SpriteImage.sprite = null;
		if (m_VehicleName != null)
		{
			m_VehicleName.text = string.Empty;
		}
		if (m_Creator != null)
		{
			m_Creator.text = string.Empty;
		}
	}

	private string GetCreator()
	{
		if (m_MyCap != null && !m_MyCap.creator.NullOrEmpty())
		{
			return "@" + m_MyCap.creator;
		}
		return string.Empty;
	}

	private void OnResolveThumbnail(Sprite sprite)
	{
		m_SpriteImage.sprite = sprite;
	}

	private void Update()
	{
		if (m_MyCap == null || !(m_ReferenceSizeTransform != null) || !(m_InitialDeltaSize != m_ReferenceSizeTransform.rect.size))
		{
			return;
		}
		m_InitialDeltaSize = m_ReferenceSizeTransform.rect.size;
		m_MyCap.ResolveThumbnail(delegate(Sprite sprite)
		{
			float num = Mathf.Min(m_InitialDeltaSize.x / (float)sprite.texture.width, m_InitialDeltaSize.y / (float)sprite.texture.height);
			if (sprite != null)
			{
				m_SpriteImage.rectTransform.sizeDelta = new Vector2((float)sprite.texture.width * num, (float)sprite.texture.height * num);
				m_SpriteImage.rectTransform.position = m_ReferenceSizeTransform.position;
			}
		});
	}

	private void OnDisable()
	{
		m_InitialDeltaSize = Vector2.zero;
	}
}
