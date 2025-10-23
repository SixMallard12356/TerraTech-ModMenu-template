using UnityEngine;
using UnityEngine.UI;

public class UIQuestMarker : MonoBehaviour
{
	[SerializeField]
	private Image m_Blip;

	[SerializeField]
	private Image m_Circle;

	private RectTransform m_RectTrans;

	private GameObject m_BlipObj;

	private GameObject m_CircleObj;

	public bool BlipVisible
	{
		set
		{
			if ((bool)m_BlipObj && m_BlipObj.activeSelf != value)
			{
				m_BlipObj.SetActive(value);
			}
		}
	}

	public bool CircleVisible
	{
		set
		{
			if ((bool)m_CircleObj && m_CircleObj.activeSelf != value)
			{
				m_CircleObj.SetActive(value);
			}
		}
	}

	public Vector3 LocalPosition
	{
		set
		{
			if ((bool)m_RectTrans)
			{
				m_RectTrans.localPosition = value;
			}
		}
	}

	public Quaternion LocalRotation
	{
		set
		{
			if ((bool)m_RectTrans)
			{
				m_RectTrans.localRotation = value;
			}
		}
	}

	public void SetParent(RectTransform parent, bool worldPositionStays)
	{
		if ((bool)m_RectTrans)
		{
			m_RectTrans.SetParent(parent, worldPositionStays);
		}
	}

	private void OnPool()
	{
		m_RectTrans = base.transform as RectTransform;
		m_BlipObj = (m_Blip ? m_Blip.gameObject : null);
		m_CircleObj = (m_Circle ? m_Circle.gameObject : null);
	}

	private void OnSpawn()
	{
		BlipVisible = false;
		CircleVisible = false;
		LocalPosition = Vector3.zero;
		LocalRotation = Quaternion.identity;
	}
}
