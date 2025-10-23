using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISfxHelper : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, ISubmitHandler, ICancelHandler, IScrollHandler
{
	[Serializable]
	public struct UISfxHelperItem
	{
		public bool m_Enabled;

		public ManSFX.UISfxType m_SFX;
	}

	[SerializeField]
	private UISfxHelperItem m_SelectSFX;

	[SerializeField]
	private UISfxHelperItem m_DeselectSFX;

	[SerializeField]
	private UISfxHelperItem m_PointerDownSFX;

	[SerializeField]
	private UISfxHelperItem m_PointerUpSFX;

	[SerializeField]
	private UISfxHelperItem m_PointerEnterSFX;

	[SerializeField]
	private UISfxHelperItem m_PointerExitSFX;

	[SerializeField]
	private UISfxHelperItem m_SubmitSFX;

	[SerializeField]
	private UISfxHelperItem m_CancelSFX;

	[SerializeField]
	private UISfxHelperItem m_ScrollSFX;

	protected bool m_Selected;

	protected bool m_Over;

	protected bool m_Down;

	public void OnSelect(BaseEventData eventData)
	{
		m_Selected = true;
		if (m_SelectSFX.m_Enabled)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(m_SelectSFX.m_SFX);
		}
	}

	public void OnDeselect(BaseEventData eventData)
	{
		m_Selected = false;
		if (m_DeselectSFX.m_Enabled)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(m_DeselectSFX.m_SFX);
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		m_Down = true;
		if (m_PointerDownSFX.m_Enabled)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(m_PointerDownSFX.m_SFX);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		m_Down = false;
		if (m_PointerUpSFX.m_Enabled)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(m_PointerUpSFX.m_SFX);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		m_Over = true;
		if (m_PointerEnterSFX.m_Enabled)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(m_PointerEnterSFX.m_SFX);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		m_Over = false;
		if (m_PointerExitSFX.m_Enabled)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(m_PointerExitSFX.m_SFX);
		}
	}

	public void OnSubmit(BaseEventData eventData)
	{
		if (m_SubmitSFX.m_Enabled)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(m_SubmitSFX.m_SFX);
		}
	}

	public void OnCancel(BaseEventData eventData)
	{
		if (m_CancelSFX.m_Enabled)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(m_CancelSFX.m_SFX);
		}
	}

	public void OnScroll(PointerEventData eventData)
	{
		if (m_ScrollSFX.m_Enabled)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(m_ScrollSFX.m_SFX);
		}
	}
}
