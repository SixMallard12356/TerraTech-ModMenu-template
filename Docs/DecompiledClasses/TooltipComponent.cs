using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipComponent : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IInteractionCursorEnterHandler, IInteractionCursorExitHandler
{
	[SerializeField]
	private UITooltipOptions m_Mode;

	[SerializeField]
	private UITooltipAlignment m_Alignment = UITooltipAlignment.BottomLeft;

	[SerializeField]
	public Vector3 m_Offset = new Vector3(0f, 0f, 0f);

	[SerializeField]
	private bool m_PointerEnabled = true;

	[Tooltip("Show when highlighting the element using Navigation (eg menu buttons)")]
	[SerializeField]
	private bool m_ShowOnGamepadHover;

	[Tooltip("Show when the emulated cursor is over the element (Interaction Mode)")]
	[SerializeField]
	private bool m_SupportEmulatedCursor;

	[SerializeField]
	[HideInInspector]
	private bool m_ManuallySetText;

	[SerializeField]
	[HideInInspector]
	private LocalisedString m_LocalisedString;

	public Event<TooltipComponent> Changed;

	private bool m_Active;

	private bool m_ForceDisplay;

	public string Text { get; private set; }

	public UITooltipAlignment Alignment => m_Alignment;

	public Vector3 Offset => m_Offset;

	public UITooltipOptions Mode
	{
		get
		{
			return m_Mode;
		}
		set
		{
			m_Mode = value;
		}
	}

	public RectTransform RectTrans => base.transform as RectTransform;

	public void SetText(string text)
	{
		Text = text;
		Changed.Send(this);
	}

	public void SetMode(UITooltipOptions mode)
	{
		m_Mode = mode;
		Changed.Send(this);
	}

	public void SetActive(bool active)
	{
		SetActiveInternal(active);
	}

	public void SetForceDisplay(bool active)
	{
		m_ForceDisplay = active;
		SetActiveInternal(m_ForceDisplay);
	}

	public void SetPointerEnabled(bool enabled)
	{
		m_PointerEnabled = enabled;
	}

	private void UpdateText()
	{
		if (m_ManuallySetText)
		{
			SetText(m_LocalisedString.Value);
		}
	}

	private void SetActiveInternal(bool active)
	{
		if (m_ForceDisplay)
		{
			active = true;
		}
		if (m_Active == active)
		{
			return;
		}
		if (active)
		{
			if (m_ManuallySetText)
			{
				UpdateText();
				Singleton.Manager<Localisation>.inst.OnLanguageChanged.Subscribe(UpdateText);
			}
			Singleton.Manager<ManOverlay>.inst.AddToolTip(this);
		}
		else
		{
			Singleton.Manager<ManOverlay>.inst.RemoveToolTip(this);
			Singleton.Manager<Localisation>.inst.OnLanguageChanged.Unsubscribe(UpdateText);
		}
		m_Active = active;
	}

	private void OnRecycle()
	{
		SetActiveInternal(active: false);
	}

	public void OnPointerEnter(PointerEventData dataName)
	{
		if (m_PointerEnabled)
		{
			SetActiveInternal(active: true);
		}
	}

	public void OnPointerExit(PointerEventData dataName)
	{
		if (m_PointerEnabled)
		{
			SetActiveInternal(active: false);
		}
	}

	public void OnSelect(BaseEventData data)
	{
		if (m_ShowOnGamepadHover && Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			SetActiveInternal(active: true);
		}
	}

	public void OnDeselect(BaseEventData data)
	{
		if (m_ShowOnGamepadHover && Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			SetActiveInternal(active: false);
		}
	}

	public void OnInteractionCursorEnter(PointerEventData eventData)
	{
		if (m_SupportEmulatedCursor)
		{
			OnPointerEnter(eventData);
		}
	}

	public void OnInteractionCursorExit(PointerEventData eventData)
	{
		if (m_SupportEmulatedCursor)
		{
			OnPointerExit(eventData);
		}
	}

	private void OnDisable()
	{
		m_ForceDisplay = false;
		SetActiveInternal(active: false);
	}
}
