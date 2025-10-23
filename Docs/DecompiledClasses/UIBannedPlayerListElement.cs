using System;
using TMPro;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBannedPlayerListElement : MonoBehaviour
{
	[SerializeField]
	protected TextMeshProUGUI m_PlayerNameLabel;

	[SerializeField]
	protected Button m_UnbanButton;

	private TTNetworkID m_PlayerNetworkID;

	private Action<TTNetworkID> m_UnbanCallback;

	public Button UnbanButton => m_UnbanButton;

	public void Set(TTNetworkID playerNetworkID, string playerDisplayName, Action<TTNetworkID> onUnbanCallback)
	{
		m_PlayerNetworkID = playerNetworkID;
		m_UnbanCallback = onUnbanCallback;
		m_PlayerNameLabel.text = playerDisplayName;
	}

	public void SetSelected()
	{
		EventSystem.current.SetSelectedGameObject(m_UnbanButton.gameObject);
	}

	private void OnUnbanButtonClicked()
	{
		m_UnbanCallback?.Invoke(m_PlayerNetworkID);
	}

	private void OnPool()
	{
		m_UnbanButton.onClick.AddListener(OnUnbanButtonClicked);
	}
}
