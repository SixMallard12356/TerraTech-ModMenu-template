using UnityEngine;
using UnityEngine.UI;

public class UIShopTechItem : MonoBehaviour
{
	[SerializeField]
	private Image m_Image;

	[SerializeField]
	private Text m_TechName;

	[SerializeField]
	private Text m_Cost;

	[SerializeField]
	private Dropdown m_Dropdown;

	public Event<Snapshot> Selected;

	public Event<Snapshot, int> HotswapChanged;

	private Snapshot m_Snapshot;

	private Toggle m_ToggleButton;

	private Button m_Button;

	private RectTransform rectTrans;

	public Toggle ToggleButton => m_ToggleButton;

	public bool Used { get; set; }

	public Snapshot Snapshot => m_Snapshot;

	public RectTransform RectTransform
	{
		get
		{
			if (rectTrans == null)
			{
				rectTrans = GetComponent<RectTransform>();
			}
			return rectTrans;
		}
	}

	public void SetupTech(Snapshot snapshot)
	{
		m_Snapshot = snapshot;
		m_Snapshot.ResolveThumbnail(OnResolveThumbnail);
		if (m_Snapshot is SnapshotDisk snapshotDisk)
		{
			m_TechName.text = snapshotDisk.GetFileName();
		}
		else
		{
			m_TechName.text = m_Snapshot.techData.Name;
		}
		m_Cost.gameObject.SetActive(value: false);
		m_Dropdown.gameObject.SetActive(Singleton.Manager<ManTechs>.inst.HotswapEnbled);
	}

	public void SetHotswapDropdown(int hotswap)
	{
		m_Dropdown.onValueChanged.RemoveListener(OnDropdownChanged);
		m_Dropdown.value = hotswap;
		m_Dropdown.onValueChanged.AddListener(OnDropdownChanged);
	}

	public void UpdateDropdownOptions(int maxSlots)
	{
		int i = m_Dropdown.options.Count;
		int num = maxSlots + 1;
		if (i > num)
		{
			m_Dropdown.ClearOptions();
			i = 0;
		}
		for (; i < num; i++)
		{
			string text = ((i == 0) ? "-" : i.ToString());
			m_Dropdown.options.Add(new Dropdown.OptionData(text));
		}
	}

	private void OnToggleChanged(bool param)
	{
		Selected.Send(m_Snapshot);
	}

	private void OnButtonClick()
	{
		Selected.Send(m_Snapshot);
	}

	private void OnDropdownChanged(int option)
	{
		HotswapChanged.Send(m_Snapshot, option);
	}

	private void OnResolveThumbnail(Sprite sprite)
	{
		m_Image.sprite = sprite;
	}

	private void OnSpawn()
	{
		m_ToggleButton = GetComponentInChildren<Toggle>();
		m_Button = GetComponent<Button>();
		m_Dropdown.onValueChanged.AddListener(OnDropdownChanged);
		if ((bool)m_ToggleButton)
		{
			m_ToggleButton.onValueChanged.RemoveAllListeners();
			m_ToggleButton.onValueChanged.AddListener(OnToggleChanged);
		}
		else
		{
			m_Button.onClick.RemoveAllListeners();
			m_Button.onClick.AddListener(OnButtonClick);
		}
	}

	private void OnRecycle()
	{
		if ((bool)m_ToggleButton)
		{
			m_ToggleButton.isOn = false;
			m_ToggleButton.group = null;
			m_ToggleButton.onValueChanged.RemoveAllListeners();
		}
		if ((bool)m_Button)
		{
			m_Button.onClick.RemoveAllListeners();
		}
		m_Dropdown.onValueChanged.RemoveListener(OnDropdownChanged);
		m_Snapshot = null;
	}
}
