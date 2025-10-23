using UnityEngine;
using UnityEngine.UI;

public class FloatingTextPanel : MonoBehaviour, ManHUD.Focussable
{
	[SerializeField]
	private CanvasGroup m_CanvasGroup;

	[SerializeField]
	private Text m_AmountText;

	private RectTransform m_Rect;

	private Vector3 m_InitialScale;

	private float m_scaler;

	public string Text
	{
		set
		{
			if ((bool)m_AmountText)
			{
				m_AmountText.text = value;
			}
		}
	}

	public float Alpha
	{
		get
		{
			if (!(m_CanvasGroup != null))
			{
				return 1f;
			}
			return m_CanvasGroup.alpha;
		}
		set
		{
			if ((bool)m_CanvasGroup)
			{
				m_CanvasGroup.alpha = value;
			}
		}
	}

	public float LocalScale
	{
		get
		{
			return m_scaler;
		}
		set
		{
			m_scaler = value;
			m_Rect.localScale = m_scaler * m_InitialScale;
		}
	}

	public Vector3 LocalPosition
	{
		get
		{
			return m_Rect.localPosition;
		}
		set
		{
			m_Rect.localPosition = value;
		}
	}

	private void OnPool()
	{
		m_Rect = GetComponent<RectTransform>();
		m_InitialScale = m_Rect.localScale;
	}

	private void OnSpawn()
	{
		Text = "";
		Alpha = 1f;
		LocalScale = 1f;
		Singleton.Manager<ManHUD>.inst.AddOverlay(this);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManHUD>.inst.RemoveOverlay(this);
	}

	public void SetFocusLevel(ManHUD.FocusLevel level)
	{
	}

	public Transform GetTransform()
	{
		return m_Rect;
	}
}
