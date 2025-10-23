using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class UISteamTagToggle<T> : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, ISubmitHandler
{
	public Event<T, bool> OnSelected;

	private Toggle m_Toggle;

	private bool m_Selected;

	public T SteamTag { get; set; }

	public void SetSelected(bool isSelected)
	{
		m_Toggle.isOn = isSelected;
		if (m_Selected != isSelected)
		{
			m_Selected = isSelected;
			OnSelected.Send(SteamTag, m_Selected);
		}
	}

	private void OnToggleChanged(bool isOn)
	{
		SetSelected(isOn);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.CheckBox);
	}

	public void OnSubmit(BaseEventData eventData)
	{
		Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.CheckBox);
	}

	private void OnPool()
	{
		m_Toggle = GetComponent<Toggle>();
		m_Toggle.onValueChanged.AddListener(OnToggleChanged);
		SetSelected(m_Selected);
	}
}
