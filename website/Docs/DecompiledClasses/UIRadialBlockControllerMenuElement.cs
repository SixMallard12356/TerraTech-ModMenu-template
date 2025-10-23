using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRadialBlockControllerMenuElement : MonoBehaviour
{
	public delegate void ControlChangedDelegate(int index, bool value);

	[SerializeField]
	private Image m_CircuitsLockIcon;

	[SerializeField]
	[HideInInspector]
	private Toggle m_Toggle;

	[SerializeField]
	[HideInInspector]
	private TextMeshProUGUI m_Text;

	[SerializeField]
	[HideInInspector]
	private TooltipComponent m_TooltipComponent;

	[SerializeField]
	[HideInInspector]
	private Button m_Button;

	protected ControlChangedDelegate m_ControlChangedCallback;

	public Toggle Toggle => m_Toggle;

	public TextMeshProUGUI Text => m_Text;

	public TooltipComponent Tooltip => m_TooltipComponent;

	public Button Button => m_Button;

	public int CategoryIndex { get; private set; } = -1;

	public ModuleControlCategory Category { get; private set; }

	public bool ControlState { get; private set; }

	public void SetCategory(ModuleControlCategory category, ControlChangedDelegate onRequestedChangedCallback)
	{
		Category = category;
		CategoryIndex = (int)Category;
		m_Text.text = StringLookup.GetBlockControlCategoryName(Category);
		m_ControlChangedCallback = onRequestedChangedCallback;
		Button.onClick.AddListener(OnClicked);
		Toggle.onValueChanged.AddListener(OnToggled);
	}

	public void SetControlState(bool state)
	{
		ControlState = state;
		Toggle.SetValue(ControlState);
	}

	public void SetControlledByCircuits(bool state)
	{
		if (m_CircuitsLockIcon != null)
		{
			m_CircuitsLockIcon.gameObject.SetActive(state);
		}
		Tooltip.enabled = state;
	}

	private void OnStateRequestedChanged(bool controlState)
	{
		m_ControlChangedCallback?.Invoke(CategoryIndex, controlState);
	}

	private void OnClicked()
	{
		OnStateRequestedChanged(!ControlState);
	}

	private void OnToggled(bool newValue)
	{
		OnStateRequestedChanged(newValue);
	}

	private void Awake()
	{
		m_Toggle = GetComponentInChildren<Toggle>();
		m_Text = GetComponentInChildren<TextMeshProUGUI>();
		m_TooltipComponent = GetComponentInChildren<TooltipComponent>();
		m_Button = GetComponentInChildren<Button>();
	}
}
