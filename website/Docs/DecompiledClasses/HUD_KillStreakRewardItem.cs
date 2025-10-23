#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class HUD_KillStreakRewardItem : MonoBehaviour
{
	[SerializeField]
	private Text m_KillCountLabel;

	[SerializeField]
	private Text m_QuantityLabel;

	[SerializeField]
	private Text m_MaxedLabel;

	[SerializeField]
	private Image m_BlockIcon;

	public void InitKillStreakRewardItem(int killStreak, bool isMaxed, Sprite blockIcon, string blockName, int qty)
	{
		m_KillCountLabel.text = killStreak.ToString();
		m_QuantityLabel.text = FormatKillStreakRewardQuantity(qty);
		m_MaxedLabel.gameObject.SetActive(isMaxed);
		m_BlockIcon.sprite = blockIcon;
		TooltipComponent component = base.gameObject.GetComponent<TooltipComponent>();
		d.Assert(component != null);
		component.SetText(blockName);
	}

	public static string FormatKillStreakRewardQuantity(int qty)
	{
		if (qty <= 0)
		{
			return "";
		}
		return string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 53), qty.ToString());
	}
}
