#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreHUD : UIHUDElement
{
	[SerializeField]
	private Text m_ScoreTitle;

	[SerializeField]
	private Text m_ScoreName;

	[SerializeField]
	private Text m_ScoreText;

	[SerializeField]
	private Image m_ScoreImage;

	[SerializeField]
	private Text m_NotableName;

	[SerializeField]
	private Text m_NotableScoreText;

	[SerializeField]
	private Image m_NoteableScoreImage;

	[SerializeField]
	private GameObject m_NotablePlayerUI;

	[SerializeField]
	private GameObject m_LifeCounter;

	[SerializeField]
	private Image m_HeartPrefab;

	[SerializeField]
	private Sprite m_FullHeart;

	[SerializeField]
	private Sprite m_EmptyHeart;

	[SerializeField]
	private Button m_KillStreakPreviewButton;

	[SerializeField]
	private HUD_KillStreakPreviewPanel m_KillStreakPreviewPanel;

	private NetController.ScorePolicy m_ScorePolicy;

	private List<Image> m_Lives = new List<Image>();

	private UIElementCache<Image> m_HeartPool;

	public void InitScore(NetController.ScorePolicy policy)
	{
		m_ScorePolicy = policy;
		m_ScoreTitle.text = GetScoreTitle();
		InitScoreString();
		_setupKillStreakPanel();
		m_NotablePlayerUI.SetActive(value: false);
	}

	public void InitLives(int startingLives)
	{
		m_HeartPool.SetNoneUsed();
		for (int i = 0; i < startingLives; i++)
		{
			Image image = m_HeartPool.Alloc(m_LifeCounter.transform);
			image.sprite = m_FullHeart;
			m_Lives.Add(image);
		}
		m_HeartPool.FreeUnused();
	}

	public void SetLives(int numLives)
	{
		for (int i = 0; i < m_Lives.Count; i++)
		{
			if (i < numLives)
			{
				m_Lives[i].sprite = m_FullHeart;
			}
			else
			{
				m_Lives[i].sprite = m_EmptyHeart;
			}
		}
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		m_HeartPool.FreeAll();
	}

	public void SetPlayerScore(NetPlayer netPlayer)
	{
		m_ScoreText.text = netPlayer.Score.EvaluateToString(Singleton.Manager<ManNetwork>.inst.NetController.CurrentScorePolicy);
		m_ScoreImage.sprite = netPlayer.Sprite;
		m_ScoreName.text = netPlayer.name;
		m_ScoreName.color = netPlayer.Colour;
	}

	public void SetNotablePlayer(NetPlayer netPlayer)
	{
		if (netPlayer != null && netPlayer != Singleton.Manager<ManNetwork>.inst.MyPlayer)
		{
			m_NotableName.text = netPlayer.name;
			m_NotableName.color = netPlayer.Colour;
			m_NotableScoreText.text = netPlayer.Score.EvaluateToString(Singleton.Manager<ManNetwork>.inst.NetController.CurrentScorePolicy);
			m_NoteableScoreImage.sprite = netPlayer.Sprite;
			m_NotablePlayerUI.SetActive(value: true);
		}
		else
		{
			m_NotablePlayerUI.SetActive(value: false);
		}
	}

	private void InitScoreString()
	{
		float num = 0f;
		switch (m_ScorePolicy)
		{
		case NetController.ScorePolicy.SetTime:
			num = (true ? Singleton.Manager<ManNetwork>.inst.NetController.ScoreToWin : 0f);
			break;
		default:
			d.LogError(string.Concat("UIScoreHUD.InitScoreString - Score Policy ", m_ScorePolicy, " is unsupported"));
			break;
		case NetController.ScorePolicy.GameTime:
		case NetController.ScorePolicy.Kills:
		case NetController.ScorePolicy.KillMinusDeath:
		case NetController.ScorePolicy.NumWaves:
			break;
		}
		_ = 0f;
	}

	private string GetScoreTitle()
	{
		string result = null;
		switch (m_ScorePolicy)
		{
		case NetController.ScorePolicy.GameTime:
			result = "Game Time";
			break;
		case NetController.ScorePolicy.Kills:
			result = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 52);
			break;
		case NetController.ScorePolicy.SetTime:
			result = "Time";
			break;
		case NetController.ScorePolicy.KillMinusDeath:
			result = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 52);
			break;
		case NetController.ScorePolicy.NumWaves:
			result = "";
			break;
		default:
			d.LogError(string.Concat("UIScoreHUD.GetScoreTitle - Score Policy ", m_ScorePolicy, " is unsupported"));
			break;
		}
		return result;
	}

	private void _setupKillStreakPanel()
	{
		d.Assert(m_KillStreakPreviewButton != null);
		m_KillStreakPreviewButton.onClick.AddListener(_handleKillStreakPreviewButton);
		m_KillStreakPreviewButton.gameObject.GetComponent<TooltipComponent>().SetText(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 88));
		d.Assert(m_KillStreakPreviewPanel != null);
		m_KillStreakPreviewPanel.InitPreviewPanel();
		m_KillStreakPreviewPanel.gameObject.SetActive(value: false);
		m_KillStreakPreviewButton.gameObject.SetActive(Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetwork>.inst.KillStreakRewardsEnabled);
	}

	private void _handleKillStreakPreviewButton()
	{
		m_KillStreakPreviewPanel.gameObject.SetActive(!m_KillStreakPreviewPanel.gameObject.activeSelf);
	}

	private void OnPool()
	{
		m_HeartPool = new UIElementCache<Image>(m_HeartPrefab);
	}
}
