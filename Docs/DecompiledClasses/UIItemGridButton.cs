using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
[RequireComponent(typeof(UIItemDisplay))]
[RequireComponent(typeof(RectTransform))]
public class UIItemGridButton : MonoBehaviour
{
	public struct DisplayParams
	{
		public static DisplayParams kDefault = new DisplayParams
		{
			m_ItemColour = Color.white,
			m_ItemBackgroundColour = new Color(1f, 1f, 1f, 0f),
			m_DisplayQuantity = false,
			m_Quantity = 0,
			m_ShowAsUndiscovered = false,
			m_ShowDeathStreakReward = false,
			m_ShowLocked = false
		};

		public Color m_ItemColour;

		public Color m_ItemBackgroundColour;

		public bool m_DisplayQuantity;

		public int m_Quantity;

		public bool m_ShowAsUndiscovered;

		public bool m_ShowDeathStreakReward;

		public bool m_ShowLocked;
	}

	[SerializeField]
	[Tooltip("Set of components which are disabled when this item isn't displayed")]
	private MonoBehaviour[] m_SwitchedComponents;

	[SerializeField]
	private GameObject m_UndiscoveredImage;

	[SerializeField]
	private GameObject m_DeathStreakRewardImage;

	[SerializeField]
	private GameObject m_LockedImage;

	[SerializeField]
	private GameObject m_ModdedIcon;

	public Event<UIItemGridButton, bool> OnToggled;

	private Toggle m_Toggle;

	private RectTransform m_RectTransform;

	private LayoutElement m_LayoutElement;

	private UIItemDisplay m_ItemDisplay;

	private ItemTypeInfo m_ItemTypeInfo;

	private bool m_Used;

	private bool m_Visible;

	private bool m_ToggledOn;

	public bool IsOn
	{
		get
		{
			return m_ToggledOn;
		}
		set
		{
			if (m_Visible && (bool)m_Toggle)
			{
				m_Toggle.isOn = value;
			}
			else if (m_ToggledOn != value)
			{
				m_ToggledOn = value;
				OnToggled.Send(this, value);
			}
		}
	}

	public ItemTypeInfo ItemTypeInfo => m_ItemTypeInfo;

	public RectTransform RectTransform => m_RectTransform;

	public bool IsVisible => m_Visible;

	public void Init()
	{
		m_Toggle = GetComponent<Toggle>();
		m_ItemDisplay = GetComponent<UIItemDisplay>();
		m_RectTransform = GetComponent<RectTransform>();
		m_LayoutElement = GetComponent<LayoutElement>();
		m_ToggledOn = false;
		m_Used = true;
		SetUsed(used: false);
		m_Visible = true;
		SetVisible(visible: false);
		m_Toggle.onValueChanged.AddListener(OnToggleValueChanged);
	}

	public void SetupItem(ItemTypeInfo itemTypeInfo, DisplayParams dParams)
	{
		m_ItemTypeInfo = itemTypeInfo;
		string quantityText = ((!dParams.m_DisplayQuantity) ? "" : Singleton.Manager<Localisation>.inst.GetInventoryQuantityString(dParams.m_Quantity));
		if (m_UndiscoveredImage != null && m_UndiscoveredImage.activeSelf != dParams.m_ShowAsUndiscovered)
		{
			m_UndiscoveredImage.SetActive(dParams.m_ShowAsUndiscovered);
		}
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetwork>.inst.DeathStreakEnabled)
		{
			if (m_DeathStreakRewardImage != null && m_DeathStreakRewardImage.activeSelf != dParams.m_ShowDeathStreakReward)
			{
				m_DeathStreakRewardImage.SetActive(dParams.m_ShowDeathStreakReward);
			}
		}
		else if (m_DeathStreakRewardImage != null)
		{
			m_DeathStreakRewardImage.SetActive(value: false);
		}
		if (m_LockedImage != null && m_LockedImage.activeSelf != dParams.m_ShowLocked)
		{
			m_LockedImage.SetActive(dParams.m_ShowLocked);
		}
		m_ItemDisplay.Setup(m_ItemTypeInfo, dParams.m_ItemColour, dParams.m_ItemBackgroundColour, quantityText);
	}

	public bool GetUsed()
	{
		return m_Used;
	}

	public void SetUsed(bool used)
	{
		if (m_Used != used)
		{
			m_Used = used;
			if (m_LayoutElement != null && m_LayoutElement.ignoreLayout == used)
			{
				m_LayoutElement.ignoreLayout = !used;
			}
		}
	}

	public void SetVisible(bool visible)
	{
		if (m_Visible == visible)
		{
			return;
		}
		if (!visible)
		{
			m_Visible = false;
			m_Toggle.isOn = false;
			if (m_DeathStreakRewardImage != null)
			{
				m_DeathStreakRewardImage.SetActive(value: false);
			}
			if (m_ModdedIcon != null)
			{
				m_ModdedIcon.SetActive(value: false);
			}
		}
		for (int i = 0; i < m_SwitchedComponents.Length; i++)
		{
			MonoBehaviour monoBehaviour = m_SwitchedComponents[i];
			if (monoBehaviour.enabled != visible)
			{
				monoBehaviour.enabled = visible;
			}
		}
		if (m_Toggle.enabled != visible)
		{
			m_Toggle.enabled = visible;
		}
		if (visible)
		{
			m_Toggle.isOn = m_ToggledOn;
			m_Visible = true;
		}
	}

	public void SetInteractable(bool interactable)
	{
		m_Toggle.interactable = interactable;
	}

	private void OnToggleValueChanged(bool isOn)
	{
		if (m_Visible)
		{
			m_ToggledOn = isOn;
			OnToggled.Send(this, isOn);
		}
	}
}
