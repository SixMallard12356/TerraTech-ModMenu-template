#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIAutoToggleItem : MonoBehaviour, ISelectHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	private bool m_ShouldToggle = true;

	private Toggle m_Toggle;

	public void OnSelect(BaseEventData eventData)
	{
		if ((bool)m_Toggle && m_ShouldToggle)
		{
			m_Toggle.isOn = true;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		m_ShouldToggle = false;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		m_ShouldToggle = true;
	}

	private void PrePool()
	{
	}

	private void OnPool()
	{
	}

	private void OnSpawn()
	{
		m_Toggle = GetComponent<Toggle>();
		d.AssertFormat(m_Toggle != null, "Item {0} must have a sibling toggle", base.name);
	}

	private void OnRecycle()
	{
		m_ShouldToggle = true;
		m_Toggle = null;
	}
}
