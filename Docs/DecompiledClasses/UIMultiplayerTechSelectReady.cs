using UnityEngine;
using UnityEngine.UI;

public class UIMultiplayerTechSelectReady : MonoBehaviour
{
	[SerializeField]
	private Button m_Button;

	public EventNoParams Triggered;

	public void Show()
	{
		base.gameObject.SetActive(value: true);
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(base.gameObject);
		}
	}

	public void Hide()
	{
		Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(base.gameObject);
		base.gameObject.SetActive(value: false);
	}

	private void OnButtonClick()
	{
		Triggered.Send();
	}

	private void Awake()
	{
		m_Button.onClick.AddListener(OnButtonClick);
	}
}
