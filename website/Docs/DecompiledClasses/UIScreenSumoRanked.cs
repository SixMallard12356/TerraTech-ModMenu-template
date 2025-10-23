#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UIScreenSumoRanked : UIScreen
{
	[SerializeField]
	private UISumoPreset m_PlayerChoice;

	[SerializeField]
	private GameObject m_TweetInfo;

	[SerializeField]
	private Text m_TweetInfoText;

	[SerializeField]
	private Button m_FightButton;

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		if (!fromStackPop)
		{
			m_PlayerChoice.ClearContestant();
			IInventory<BlockTypes> inventory = new SingleplayerInventory();
			if (Singleton.Manager<ManGameMode>.inst.NextModeSetting.GetModeInitSetting("Inventory", out var settingData) && settingData != null)
			{
				(settingData as InventoryAsset).BuildInventory(inventory);
			}
			m_PlayerChoice.SetAvailableInventory(inventory);
		}
		UpdateStartButton();
	}

	public override void Hide()
	{
		base.Hide();
		if (m_PlayerChoice.State == UISumoPreset.ContestantState.Selecting)
		{
			m_PlayerChoice.ClearContestant();
		}
	}

	public void AcceptFighter()
	{
		Singleton.Manager<TwitterAuthenticationUIHandler>.inst.TryAuthenticateAsync(OnTwitterAuthenticateComplete);
	}

	private void StartRankedLadder()
	{
		Mode<ModeSumo>.inst.ClearRankedSettings();
		Singleton.Manager<ManUI>.inst.PopScreen(showPrev: false);
		UIScreenSumoLoadContestants uIScreenSumoLoadContestants = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.SumoRankedEnemies) as UIScreenSumoLoadContestants;
		if (!uIScreenSumoLoadContestants)
		{
			return;
		}
		SnapshotTwitter snapshotTwitter = m_PlayerChoice.Snapshot as SnapshotTwitter;
		if (snapshotTwitter == null)
		{
			if (m_PlayerChoice.Snapshot is SnapshotDisk snapshotDisk)
			{
				snapshotTwitter = SnapshotTwitter.ConvertFromDisk(snapshotDisk);
			}
			else
			{
				d.LogError("UIScreenSumoRanked.StartRankedLadder - could not parse the provided snapshot: " + m_PlayerChoice.Snapshot);
			}
		}
		uIScreenSumoLoadContestants.SetPlayer(snapshotTwitter);
		m_PlayerChoice.ClearContestant();
		Singleton.Manager<ManUI>.inst.PushScreen(uIScreenSumoLoadContestants);
	}

	private void UpdateStartButton()
	{
		bool flag = m_PlayerChoice.State == UISumoPreset.ContestantState.Ready;
		m_FightButton.gameObject.SetActive(flag);
		m_FightButton.interactable = flag;
	}

	private void UpdateTweetInfo()
	{
		bool flag = m_PlayerChoice.State == UISumoPreset.ContestantState.Ready;
		m_TweetInfo.SetActive(flag);
		if (flag)
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 9);
			m_TweetInfoText.text = string.Format(localisedString, m_PlayerChoice.Snapshot.techData.Name, Mode<ModeSumo>.inst.RankedHashtag);
		}
	}

	private void OnContestantStateChanged(UISumoPreset preset, UISumoPreset.ContestantState newState)
	{
		UpdateStartButton();
		UpdateTweetInfo();
	}

	private void OnTwitterAuthenticateComplete(bool loggedIn)
	{
		if (loggedIn)
		{
			StartRankedLadder();
		}
	}

	private void Awake()
	{
		m_PlayerChoice.OnContestantStateChanged.Subscribe(OnContestantStateChanged);
	}
}
