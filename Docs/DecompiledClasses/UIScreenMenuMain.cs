#define UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScreenMenuMain : UIScreen
{
	[SerializeField]
	private GameObject m_ProfileSelectConsole;

	[SerializeField]
	private Button m_ContinueGame;

	[SerializeField]
	private Text m_ContinueGameTypeLabel;

	[SerializeField]
	private Text m_ContinueGameLastPlayedLabel;

	[SerializeField]
	private LocalisedString m_ContinueGameLastPlayedFormat;

	[SerializeField]
	private Button m_LoadGameButton;

	[SerializeField]
	private Button m_NewGameButton;

	[SerializeField]
	private Text m_UserName;

	[SerializeField]
	private Button m_MultiplayerButton;

	[SerializeField]
	private Button m_StoreButton;

	[SerializeField]
	private GameObject[] m_HiddenUIOnConsole;

	[SerializeField]
	private GameObject m_AnnouncerPanel;

	[SerializeField]
	[EnumArray(typeof(ExclusiveContentTypes))]
	private GameObject[] m_ExclusiveContentIcons;

	[SerializeField]
	private Text m_BlockLimiterText;

	[SerializeField]
	private AdvertisingPanel m_AdvertisingPanel;

	private bool m_ShowButtonsEnabled;

	private GameObject m_NavEntryPoint;

	public override void Show(bool fromStackPop)
	{
		if (base.state != State.Show)
		{
			Singleton.Manager<ManProfile>.inst.OnUserChanged.Subscribe(InitialiseForCurrentUser);
		}
		base.Show(fromStackPop);
		if (SKU.ConsoleUI)
		{
			GameObject[] hiddenUIOnConsole = m_HiddenUIOnConsole;
			for (int i = 0; i < hiddenUIOnConsole.Length; i++)
			{
				hiddenUIOnConsole[i].gameObject.SetActive(value: false);
			}
		}
		m_NewGameButton.gameObject.SetActive(value: true);
		InitialiseForCurrentUser();
		ExclusiveContentTypes exclusiveContentTypes = ExclusiveContentTypes.None;
		int num = 0;
		EnumValuesIterator<ExclusiveContentTypes> enumerator = EnumIterator<ExclusiveContentTypes>.Values().GetEnumerator();
		while (enumerator.MoveNext())
		{
			ExclusiveContentTypes current = enumerator.Current;
			if (ManDLC.HasLimitedAccessContent(current) && m_ExclusiveContentIcons[num] != null)
			{
				exclusiveContentTypes = current;
			}
			num++;
		}
		for (int j = 0; j < m_ExclusiveContentIcons.Length; j++)
		{
			if (m_ExclusiveContentIcons[j] != null)
			{
				m_ExclusiveContentIcons[j].SetActive(j == (int)exclusiveContentTypes);
			}
		}
		if ((bool)m_AdvertisingPanel)
		{
			m_AdvertisingPanel.Init();
		}
		if (SKU.SwitchUI)
		{
			m_ProfileSelectConsole?.SetActive(value: false);
		}
		if (!SKU.AnnouncerEnabled && m_AnnouncerPanel != null)
		{
			m_AnnouncerPanel.SetActive(value: false);
		}
	}

	public override void Hide()
	{
		Singleton.Manager<ManProfile>.inst.OnUserChanged.Unsubscribe(InitialiseForCurrentUser);
		if (m_AdvertisingPanel != null)
		{
			m_AdvertisingPanel.DeInit();
		}
		base.Hide();
	}

	public void OnStoreButtonClicked()
	{
		Singleton.Manager<ManDLC>.inst.OpenStoreToDLCPageWithNotification();
	}

	private void UpdateUserNamePanel()
	{
		if ((object)m_UserName != null)
		{
			ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
			string text = ((currentUser != null) ? currentUser.m_Name : "");
			if (m_UserName.text != text)
			{
				m_UserName.text = text;
			}
		}
	}

	private void InitialiseForCurrentUser(ManProfile.Profile userProfile = null)
	{
		ManProfile.Profile currentUserProfile = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		bool flag = currentUserProfile != null;
		bool canContinueGame = flag && !currentUserProfile.m_LastUsedSaveName.NullOrEmpty() && ManSaveGame.SaveExists(currentUserProfile.m_LastUsedSaveType, currentUserProfile.m_LastUsedSaveName);
		UpdateUserNamePanel();
		try
		{
			d.Log($"[UIScreenMenuMain] InitialiseForCurrentUser: {currentUserProfile} last type: {currentUserProfile?.m_LastUsedSaveType} can continue: {canContinueGame}");
			if (currentUserProfile != null && currentUserProfile.m_LastUsedSaveType != ManGameMode.GameType.Attract)
			{
				if (m_ContinueGameTypeLabel != null)
				{
					m_ContinueGameTypeLabel.text = "";
				}
				if (m_ContinueGameLastPlayedLabel != null)
				{
					m_ContinueGameLastPlayedLabel.text = "";
				}
				ManSaveGame.LoadSaveDataInfoAsync(currentUserProfile.m_LastUsedSaveType, currentUserProfile.m_LastUsedSaveName, delegate(ManSaveGame.SaveInfo saveInfo)
				{
					SetupUi(saveInfo, currentUserProfile, canContinueGame);
				});
			}
			else
			{
				d.LogWarning("[UIScreenMenuMain]:InitialiseForCurrentUser:1, Fallback to default SetupUI as currentUserProfile.m_LastUsedSaveType == ManGameMode.GameType.Attract");
				SetupUi(null, currentUserProfile, canContinueGame);
			}
		}
		catch (Exception)
		{
			d.Log("[UIScreenMenuMain]:InitialiseForCurrentUser:2, Fallback to default SetupUI");
			SetupUi(null, currentUserProfile, canContinueGame);
		}
	}

	private void SetupUi(ManSaveGame.SaveInfo saveInfo, ManProfile.Profile currentUserProfile, bool continueGame)
	{
		d.Log("[UIScreenMenuMain]:SetupUI");
		bool flag = (saveInfo?.CanLoadSave() ?? false) && continueGame;
		if (flag)
		{
			if (m_ContinueGameTypeLabel != null)
			{
				m_ContinueGameTypeLabel.text = StringLookup.GetGameTypeName(currentUserProfile.m_LastUsedSaveType);
			}
			if (m_ContinueGameLastPlayedLabel != null)
			{
				string dateString = Singleton.Manager<Localisation>.inst.GetDateString(saveInfo.m_LastPlayed);
				string text = ((m_ContinueGameLastPlayedFormat.Value == null) ? dateString : string.Format(m_ContinueGameLastPlayedFormat.Value, dateString));
				m_ContinueGameLastPlayedLabel.text = text;
			}
		}
		m_ContinueGame.gameObject.SetActive(flag);
		m_NavEntryPoint = (flag ? m_ContinueGame.gameObject : m_NewGameButton.gameObject);
		GameObject navEntryPoint = m_NavEntryPoint;
		if (m_LoadGameButton != null)
		{
			bool active = true;
			if (SKU.ConsoleUI)
			{
				active = currentUserProfile != null && (flag || ManSaveGame.HasAnySavesInFolder(ManGameMode.GameType.MainGame) || ManSaveGame.HasAnySavesInFolder(ManGameMode.GameType.Creative) || ManSaveGame.HasAnySavesInFolder(ManGameMode.GameType.RaD) || ManSaveGame.HasAnySavesInFolder(ManGameMode.GameType.CoOpCampaign) || ManSaveGame.HasAnySavesInFolder(ManGameMode.GameType.CoOpCreative));
			}
			m_LoadGameButton.gameObject.SetActive(active);
		}
		if (m_ContinueGame.IsActive())
		{
			navEntryPoint = m_ContinueGame.gameObject;
		}
		else if (m_LoadGameButton.IsActive())
		{
			navEntryPoint = m_LoadGameButton.gameObject;
		}
		else if (m_NewGameButton.IsActive())
		{
			navEntryPoint = m_NewGameButton.gameObject;
		}
		RefreshMultiplayerButtonInternal();
		if ((bool)m_StoreButton && !Singleton.Manager<ManDLC>.inst.SupportsStore())
		{
			m_StoreButton.gameObject.SetActive(value: false);
		}
		d.Assert(navEntryPoint != null, "UIScreenMenuMain: ASSERT Button To Select is NULL!");
		d.Assert(navEntryPoint.activeSelf, "UIScreenMenuMain: ASSERT Button to select is NOT active!");
		d.Log("UIScreenMenuMain:  Setting Button:" + navEntryPoint.name + " as SELECTED!");
		SelectButton(navEntryPoint);
		d.Assert(m_NavEntryPoint != null, "MainMenu: ASSERT- m_NavEntryPoint is NULL!");
		d.Assert(m_NavEntryPoint.activeSelf, "MainMenu: ASSERT - m_NavEntryPoint Name=" + m_NavEntryPoint.name + " is not active!");
		AllowButtonInterraction();
	}

	private void AllowButtonInterraction()
	{
		bool flag = Singleton.Manager<ManSnapshots>.inst.ServiceDisk.HasSnapshotCollectionDisk();
		if (m_LoadGameButton != null)
		{
			m_LoadGameButton.interactable = flag;
		}
		if (m_NewGameButton != null)
		{
			m_NewGameButton.interactable = flag;
		}
		if (m_ContinueGame != null)
		{
			m_ContinueGame.interactable = flag;
		}
		if (m_MultiplayerButton != null)
		{
			m_MultiplayerButton.interactable = flag && Singleton.Manager<ManNetwork>.inst.IsMultiplayerAvailable();
		}
	}

	public void OpenURL(string url)
	{
		Application.OpenURL(url);
	}

	private void RefreshMultiplayerButtonInternal()
	{
		if (m_MultiplayerButton != null)
		{
			m_MultiplayerButton.gameObject.SetActive(SKU.SupportsMultiplayer);
		}
	}

	public void OnButtonClickedUser()
	{
	}

	private void Update()
	{
		EventSystem current = EventSystem.current;
		if ((object)m_NavEntryPoint != null && (object)current.currentSelectedGameObject == null && !current.alreadySelecting)
		{
			d.Log("UIScreenMenuMain: Update forcing selection of default Nav Entry Point due to nothing being selected");
			current.SetSelectedGameObject(m_NavEntryPoint);
		}
		RefreshMultiplayerButtonInternal();
		AllowButtonInterraction();
		UpdateUserNamePanel();
	}
}
