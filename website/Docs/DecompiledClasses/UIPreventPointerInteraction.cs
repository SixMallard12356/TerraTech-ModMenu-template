using UnityEngine;
using UnityEngine.EventSystems;

public class UIPreventPointerInteraction : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	[SerializeField]
	private bool m_PreventInteraction;

	[SerializeField]
	private bool m_PreventPainting;

	private bool m_PointerOverUI;

	private void PointerEnter()
	{
		if (m_PreventInteraction)
		{
			Singleton.Manager<ManPointer>.inst.PreventInteraction(ManPointer.PreventChannel.HUD, preventInteraction: true);
		}
		if (m_PreventPainting)
		{
			Singleton.Manager<ManPointer>.inst.PreventPainting(ManPointer.PreventChannel.HUD, prevent: true);
		}
		m_PointerOverUI = true;
	}

	private void PointerExit()
	{
		if (m_PreventInteraction)
		{
			Singleton.Manager<ManPointer>.inst.PreventInteraction(ManPointer.PreventChannel.HUD, preventInteraction: false);
		}
		if (m_PreventPainting)
		{
			Singleton.Manager<ManPointer>.inst.PreventPainting(ManPointer.PreventChannel.HUD, prevent: false);
		}
		m_PointerOverUI = false;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		PointerEnter();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		PointerExit();
	}

	private void OnDisable()
	{
		if (m_PointerOverUI)
		{
			PointerExit();
		}
	}
}
