#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UIShowHUDElementButton : MonoBehaviour
{
	[SerializeField]
	public ManHUD.HUDElementType m_HUDElementToShow;

	private Button m_Button;

	private void OnSpawn()
	{
		m_Button = GetComponent<Button>();
		if ((bool)m_Button)
		{
			m_Button.onClick.AddListener(OnButtonClicked);
		}
		else
		{
			d.LogError("No button attached to object with component UIShowHUDElementButton");
		}
	}

	private void OnRecycle()
	{
		if ((bool)m_Button)
		{
			m_Button.onClick.RemoveListener(OnButtonClicked);
		}
		else
		{
			d.LogError("No button attached to object with component UIShowHUDElementButton");
		}
	}

	private void OnButtonClicked()
	{
		Singleton.Manager<ManHUD>.inst.ShowHudElement(m_HUDElementToShow);
	}
}
