using TMPro;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.UI;

public class UIVoiceIndicatorComponent : MonoBehaviour
{
	[SerializeField]
	private Image m_AvatarImage;

	[SerializeField]
	private TextMeshProUGUI m_NameText;

	private TTNetworkID m_NetID;

	public void SetData(Sprite avatar, string name, TTNetworkID netID)
	{
		m_AvatarImage.sprite = avatar;
		m_NameText.text = name;
		m_NetID = netID;
	}

	public TTNetworkID GetNetID()
	{
		return m_NetID;
	}

	public string GetName()
	{
		return m_NameText.text;
	}
}
