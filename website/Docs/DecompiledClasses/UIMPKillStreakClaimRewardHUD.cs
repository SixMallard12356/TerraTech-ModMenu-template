#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UIMPKillStreakClaimRewardHUD : UIHUDElement
{
	[SerializeField]
	private Text m_KillCountLabel;

	[SerializeField]
	private Text m_QuantityLabel;

	[SerializeField]
	private Text m_MaxedLabel;

	[SerializeField]
	private Image m_BlockIcon;

	private bool m_isDirty = true;

	private bool m_isOperational;

	private int m_lastRewardLevel = -1;

	private int m_lastKillStreak = -1;

	private int m_corporationLoadout = -1;

	private MultiplayerKillStreakRewardAsset m_rewardsToUse;

	private TooltipComponent m_toolTip;

	private Button m_button;

	private string m_lastTipText;

	private string m_enemiesNearbyTip;

	public bool RewardAvailable => m_isOperational;

	public string TooltipText => m_toolTip.Text;

	private void Awake()
	{
		m_button = base.gameObject.GetComponent<Button>();
		d.Assert(m_button != null);
		m_button.onClick.AddListener(_handleButtonClick);
		m_toolTip = base.gameObject.GetComponent<TooltipComponent>();
		d.Assert(m_toolTip != null);
	}

	public void InitKillStreakReward()
	{
		m_enemiesNearbyTip = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Networking, 2);
		m_isDirty = true;
	}

	public void MarkAsDirty()
	{
		m_isDirty = true;
	}

	public MultiplayerKillStreakRewardAsset CurrentRewards()
	{
		if (m_rewardsToUse == null)
		{
			_makeClean();
		}
		return m_rewardsToUse;
	}

	public int MinKillsRequired()
	{
		if (m_rewardsToUse == null || m_isDirty)
		{
			_makeClean();
		}
		if (m_rewardsToUse != null)
		{
			int num = ManNetwork.KillStreakKillThresholdMultiplierOptions[Singleton.Manager<ManNetwork>.inst.KillStreakKillThresholdMultiplierIndex];
			return m_rewardsToUse.m_RewardLevels[0].m_KillsRequired * num;
		}
		return int.MaxValue;
	}

	public int CurrentFaction()
	{
		if (m_isDirty)
		{
			_makeClean();
		}
		return m_corporationLoadout;
	}

	public override void Hide(object context)
	{
		base.Hide(context);
	}

	private void Update()
	{
		if (Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase == NetController.Phase.Playing)
		{
			if (m_isDirty)
			{
				_makeClean();
			}
			NetPlayer myPlayer = Singleton.Manager<ManNetwork>.inst.MyPlayer;
			bool flag = myPlayer.CurTech != null;
			if (flag && Singleton.Manager<ManNetwork>.inst.KillStreakPreventClaimNearDangerEnabled)
			{
				float num = ManNetwork.KillStreakClaimDangerRanges[Singleton.Manager<ManNetwork>.inst.KillStreakClaimDangerRangeIndex];
				float num2 = num * num;
				for (int i = 0; i < Singleton.Manager<ManNetTechs>.inst.GetNumTechs(); i++)
				{
					NetTech tech = Singleton.Manager<ManNetTechs>.inst.GetTech(i);
					if (Tank.IsEnemy(myPlayer.CurTech.Team, tech.Team) && (myPlayer.CurTech.transform.position - tech.transform.position).sqrMagnitude <= num2)
					{
						flag = false;
						break;
					}
				}
			}
			int num3 = ManNetwork.KillStreakKillThresholdMultiplierOptions[Singleton.Manager<ManNetwork>.inst.KillStreakKillThresholdMultiplierIndex];
			int killStreak = myPlayer.Score.KillStreak;
			int qty = 0;
			int num4 = m_rewardsToUse.m_RewardLevels.Length;
			int num5 = m_rewardsToUse.m_RewardLevels[num4 - 1].m_KillsRequired * num3;
			int num6 = -1;
			for (int j = 0; j < m_rewardsToUse.m_RewardLevels.Length; j++)
			{
				int num7 = m_rewardsToUse.m_RewardLevels[j].m_KillsRequired * num3;
				if (killStreak < num7)
				{
					break;
				}
				num6 = j;
				qty = m_rewardsToUse.m_RewardLevels[j].m_BlockReward.m_Quantity;
			}
			d.Assert(num6 >= 0);
			if (killStreak != m_lastKillStreak)
			{
				m_lastKillStreak = killStreak;
				m_KillCountLabel.text = killStreak.ToString();
				m_QuantityLabel.text = HUD_KillStreakRewardItem.FormatKillStreakRewardQuantity(qty);
				m_MaxedLabel.gameObject.SetActive(killStreak >= num5);
			}
			if (num6 != m_lastRewardLevel)
			{
				BlockTypes blockType = m_rewardsToUse.m_RewardLevels[num6].m_BlockReward.m_BlockType;
				m_lastRewardLevel = num6;
				m_BlockIcon.sprite = Singleton.Manager<ManUI>.inst.GetSprite(ObjectTypes.Block, (int)blockType);
				m_lastTipText = StringLookup.GetItemName(ObjectTypes.Block, (int)blockType);
				if (Singleton.Manager<ManNetwork>.inst.KillStreakMaxedAutoClaimRewardEnabled && m_MaxedLabel.gameObject.activeSelf && flag)
				{
					ClaimReward();
				}
			}
			if (flag)
			{
				m_toolTip.SetText(m_lastTipText);
			}
			else
			{
				m_toolTip.SetText(m_enemiesNearbyTip);
			}
			m_isOperational = flag;
			m_button.interactable = flag;
		}
		else
		{
			m_isOperational = false;
			m_button.interactable = false;
			HideSelf();
			MarkAsDirty();
		}
	}

	private void _makeClean()
	{
		m_corporationLoadout = Singleton.Manager<ManNetwork>.inst.StartingTechLoadoutCorp;
		m_rewardsToUse = Singleton.Manager<ManNetwork>.inst.KillStreakRewards;
		m_lastRewardLevel = -1;
		m_lastKillStreak = -1;
		m_isDirty = m_corporationLoadout == -1 || m_rewardsToUse == null;
	}

	private void _handleButtonClick()
	{
		ClaimReward();
	}

	public void ClaimReward()
	{
		if (m_isOperational)
		{
			if (m_lastRewardLevel >= 0 && m_lastRewardLevel < m_rewardsToUse.m_RewardLevels.Length)
			{
				int quantity = m_rewardsToUse.m_RewardLevels[m_lastRewardLevel].m_BlockReward.m_Quantity;
				_ = m_rewardsToUse.m_RewardLevels[m_lastRewardLevel].m_BlockReward.m_BlockType;
				Sprite sprite = m_BlockIcon.sprite;
				UIMPKillStreakClaimRewardBeingClaimedHUD obj = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MPKillStreakClaimRewardBeingClaimed) as UIMPKillStreakClaimRewardBeingClaimedHUD;
				d.Assert(obj != null);
				Vector3 localPosition = base.transform.localPosition;
				UIHUDElement hudElement = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.BlockMenuSelection);
				Vector3 endLocalPos = obj.transform.parent.InverseTransformPoint(hudElement.transform.position);
				obj.InitKillStreakRewardBeingClaimed(localPosition, endLocalPos, m_lastKillStreak, m_MaxedLabel.gameObject.activeSelf, sprite, quantity);
				NetPlayer myPlayer = Singleton.Manager<ManNetwork>.inst.MyPlayer;
				Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.ClaimKillStreak, new ClaimKillstreakMessage
				{
					m_Level = m_lastRewardLevel
				}, myPlayer.netId);
			}
			else
			{
				d.LogError("Failed to claim Kill Streak Reward!  LastRewardLevel=" + m_lastRewardLevel);
			}
			m_isOperational = false;
			HideSelf();
			MarkAsDirty();
		}
	}
}
