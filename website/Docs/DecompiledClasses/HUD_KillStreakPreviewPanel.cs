#define UNITY_EDITOR
using UnityEngine;

public class HUD_KillStreakPreviewPanel : MonoBehaviour
{
	public GameObject[] m_RewardItemContainers;

	public GameObject m_RewardHighlight;

	public GameObject m_RewardItemPrefab;

	public Sprite m_CrossIcon;

	private HUD_KillStreakRewardItem[] _rewardItems;

	private bool _isDirty = true;

	private UIMPKillStreakClaimRewardHUD _pClaimRewardHUD;

	private int _lastCorporationType = -1;

	public void InitPreviewPanel()
	{
		d.Assert(m_RewardItemContainers != null);
		d.Assert(m_RewardItemContainers.Length == 6);
		d.Assert(m_RewardHighlight != null);
		d.Assert(m_RewardItemPrefab != null);
		if (_rewardItems == null)
		{
			int num = m_RewardItemContainers.Length;
			_rewardItems = new HUD_KillStreakRewardItem[num];
			for (int i = 0; i < num; i++)
			{
				GameObject obj = Object.Instantiate(m_RewardItemPrefab);
				obj.transform.SetParent(m_RewardItemContainers[i].gameObject.transform);
				obj.transform.localPosition = Vector3.zero;
				obj.transform.localRotation = Quaternion.identity;
				obj.transform.localScale = Vector3.one;
				HUD_KillStreakRewardItem component = obj.GetComponent<HUD_KillStreakRewardItem>();
				d.Assert(component != null);
				_rewardItems[i] = component;
			}
		}
		_pClaimRewardHUD = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MPKillStreakClaimReward) as UIMPKillStreakClaimRewardHUD;
		d.Assert(_pClaimRewardHUD != null);
		_isDirty = true;
	}

	public void MakeDirty()
	{
		_isDirty = true;
	}

	public void Update()
	{
		if (_pClaimRewardHUD != null && _pClaimRewardHUD.CurrentFaction() != _lastCorporationType)
		{
			MakeDirty();
		}
		if (_isDirty)
		{
			_makeClean();
		}
		_updateKillStreakHighlight();
	}

	private void _makeClean()
	{
		MultiplayerKillStreakRewardAsset multiplayerKillStreakRewardAsset = _pClaimRewardHUD.CurrentRewards();
		if (multiplayerKillStreakRewardAsset == null)
		{
			d.LogWarning("KillStreakPreview unable to display rewards as there aren't any (yet)!");
			return;
		}
		_lastCorporationType = _pClaimRewardHUD.CurrentFaction();
		int num = ManNetwork.KillStreakKillThresholdMultiplierOptions[Singleton.Manager<ManNetwork>.inst.KillStreakKillThresholdMultiplierIndex];
		for (int i = 0; i < _rewardItems.Length; i++)
		{
			HUD_KillStreakRewardItem obj = _rewardItems[i];
			int killStreak = 0;
			int qty = 0;
			bool isMaxed = false;
			Sprite blockIcon = null;
			string blockName = "";
			if (i == 0)
			{
				killStreak = 0;
				isMaxed = false;
				blockIcon = m_CrossIcon;
				blockName = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 84);
			}
			else if (i - 1 < multiplayerKillStreakRewardAsset.m_RewardLevels.Length)
			{
				MultiplayerKillStreakRewardLevel multiplayerKillStreakRewardLevel = multiplayerKillStreakRewardAsset.m_RewardLevels[i - 1];
				killStreak = multiplayerKillStreakRewardLevel.m_KillsRequired * num;
				isMaxed = i == multiplayerKillStreakRewardAsset.m_RewardLevels.Length;
				blockIcon = Singleton.Manager<ManUI>.inst.GetSprite(ObjectTypes.Block, (int)multiplayerKillStreakRewardLevel.m_BlockReward.m_BlockType);
				blockName = StringLookup.GetItemName(ObjectTypes.Block, (int)multiplayerKillStreakRewardLevel.m_BlockReward.m_BlockType);
				qty = multiplayerKillStreakRewardLevel.m_BlockReward.m_Quantity;
			}
			obj.InitKillStreakRewardItem(killStreak, isMaxed, blockIcon, blockName, qty);
		}
		_isDirty = false;
	}

	private void _updateKillStreakHighlight()
	{
		MultiplayerKillStreakRewardAsset multiplayerKillStreakRewardAsset = _pClaimRewardHUD.CurrentRewards();
		if (!(multiplayerKillStreakRewardAsset != null))
		{
			return;
		}
		int killStreak = Singleton.Manager<ManNetwork>.inst.MyPlayer.Score.KillStreak;
		int num = ManNetwork.KillStreakKillThresholdMultiplierOptions[Singleton.Manager<ManNetwork>.inst.KillStreakKillThresholdMultiplierIndex];
		if (killStreak < multiplayerKillStreakRewardAsset.m_RewardLevels[0].m_KillsRequired * num)
		{
			m_RewardHighlight.transform.localPosition = m_RewardItemContainers[0].transform.localPosition;
			return;
		}
		int num2 = 0;
		for (int i = 0; i < multiplayerKillStreakRewardAsset.m_RewardLevels.Length; i++)
		{
			int num3 = multiplayerKillStreakRewardAsset.m_RewardLevels[i].m_KillsRequired * num;
			if (killStreak < num3)
			{
				break;
			}
			num2 = i;
		}
		m_RewardHighlight.transform.localPosition = m_RewardItemContainers[num2 + 1].transform.localPosition;
	}
}
