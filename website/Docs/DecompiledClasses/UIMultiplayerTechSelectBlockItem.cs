using UnityEngine;
using UnityEngine.UI;

public class UIMultiplayerTechSelectBlockItem : MonoBehaviour
{
	[SerializeField]
	private Image m_BlockPaletteImage;

	[SerializeField]
	private Text m_PaletteQuantityLabel;

	public void SetData(BlockCount blockCount)
	{
		if (blockCount != null)
		{
			Sprite sprite = Singleton.Manager<ManUI>.inst.GetSprite(ObjectTypes.Block, (int)blockCount.m_BlockType);
			base.gameObject.SetActive(value: true);
			m_BlockPaletteImage.sprite = sprite;
			m_PaletteQuantityLabel.text = HUD_KillStreakRewardItem.FormatKillStreakRewardQuantity(blockCount.m_Quantity);
		}
		else
		{
			base.gameObject.SetActive(value: false);
			m_BlockPaletteImage.sprite = null;
			m_PaletteQuantityLabel.text = string.Empty;
		}
	}
}
