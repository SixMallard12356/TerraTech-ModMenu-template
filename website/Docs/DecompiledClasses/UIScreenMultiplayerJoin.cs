using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenMultiplayerJoin : UIScreen
{
	[SerializeField]
	private GameObject m_ServerRowPrefab;

	[SerializeField]
	private ToggleGroup m_RowContainer;

	[SerializeField]
	private GameObject m_Popup;

	[SerializeField]
	private Text m_PopupHostLabel;

	[SerializeField]
	private GameObject m_PopupCloseButton;

	private List<UIServerRow> m_Rows = new List<UIServerRow>();

	public void ConnectToServer()
	{
		if (new List<Toggle>(m_RowContainer.ActiveToggles()).Count > 0)
		{
			m_Popup.SetActive(value: true);
			m_PopupCloseButton.SetActive(value: false);
			m_PopupHostLabel.text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuJoinMultiplayer, 3);
		}
	}

	public void RefreshServerList()
	{
	}

	public void ClosePopup()
	{
		m_Popup.SetActive(value: false);
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		RefreshServerList();
		InvokeRepeating("UpdateList", 0f, 1f);
	}

	public override void Hide()
	{
		base.Hide();
		CancelInvoke("UpdateList");
		for (int i = 0; i < m_Rows.Count; i++)
		{
			UnityEngine.Object.Destroy(m_Rows[i].gameObject);
		}
		m_Rows.Clear();
	}

	private void UpdateList()
	{
	}

	private void OnServerModeReady(string gameMode)
	{
		ClosePopup();
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
		Singleton.Manager<ManGameMode>.inst.NextModeSetting = new ManGameMode.ModeSettings();
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_SwitchAction = delegate
		{
			Singleton.Manager<ManGameMode>.inst.TriggerSwitch(System.Type.GetType(gameMode));
		};
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.SwitchToMode();
	}
}
