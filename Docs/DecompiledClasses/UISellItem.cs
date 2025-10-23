using UnityEngine;
using UnityEngine.UI;

public class UISellItem : MonoBehaviour
{
	[Header("Content Parameters")]
	public Text m_NameText;

	public Image m_Image;

	public Text m_Value;

	public Button m_Button;

	[Header("Button Images")]
	public Sprite m_AddToQueue;

	public Sprite m_Unlock;

	private ItemTypeInfo mItemInfo;

	private void OnClicked()
	{
	}

	private void Awake()
	{
		if ((bool)m_Button)
		{
			m_Button.onClick.AddListener(OnClicked);
		}
	}
}
