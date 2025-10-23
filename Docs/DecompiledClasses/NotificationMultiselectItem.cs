using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class NotificationMultiselectItem : MonoBehaviour
{
	[SerializeField]
	protected Image IconDisplay;

	[SerializeField]
	protected TextMeshProUGUI TitleDisplay;

	public Event<int> ToggledEvent;

	public int Index { get; private set; } = -1;

	public Toggle Toggle { get; private set; }

	public UIScreenNotificationMultiselect.ItemConfig ItemConfig { get; private set; }

	public void Set(int index, UIScreenNotificationMultiselect.ItemConfig config, ToggleGroup toggleGroup)
	{
		ItemConfig = config;
		Index = index;
		TitleDisplay.text = ItemConfig.Title;
		IconDisplay.sprite = ItemConfig.Icon;
		Toggle.group = toggleGroup;
		Toggle.isOn = ItemConfig.PreSelected;
	}

	private void OnToggledEvent(bool state)
	{
		ToggledEvent.Send(Index);
	}

	private void OnPool()
	{
		Toggle = GetComponent<Toggle>();
		Toggle.onValueChanged.AddListener(OnToggledEvent);
	}

	private void OnSpawn()
	{
		Toggle.isOn = false;
	}

	private void OnRecycle()
	{
		ToggledEvent.EnsureNoSubscribers();
		Toggle.isOn = false;
		Toggle.group = null;
	}
}
