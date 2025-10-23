using UnityEngine;
using UnityEngine.UI;

public class UIItemInfo : MonoBehaviour
{
	public Text m_PanelTitle;

	public Text m_ItemName;

	public Text m_ItemDescription;

	public void Show(string title, string name, string description)
	{
		m_PanelTitle.text = title;
		m_ItemName.text = name;
		m_ItemDescription.text = description;
		base.gameObject.SetActive(value: true);
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
	}
}
