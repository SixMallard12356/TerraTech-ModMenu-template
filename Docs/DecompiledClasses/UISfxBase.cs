#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UISfxBase : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, ISubmitHandler
{
	private Selectable m_BuddySelectable;

	protected bool IsInteractable
	{
		get
		{
			if (!m_BuddySelectable)
			{
				return false;
			}
			return m_BuddySelectable.interactable;
		}
	}

	protected abstract ManSFX.UISfxType GetSfxType();

	private void Awake()
	{
		m_BuddySelectable = GetComponent<Selectable>();
		if (!m_BuddySelectable)
		{
			string text = "";
			Transform parent = base.transform;
			while (parent != null)
			{
				text = text + "." + parent.name;
				parent = parent.parent;
			}
			d.LogError($"UISfx missing buddy Selectable: {text}");
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (IsInteractable)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(GetSfxType());
		}
	}

	public void OnSubmit(BaseEventData eventData)
	{
		if (IsInteractable)
		{
			Singleton.Manager<ManSFX>.inst.PlayUISFX(GetSfxType());
		}
	}
}
