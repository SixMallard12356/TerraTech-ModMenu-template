#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UIMultiplayerTechSelectItem : MonoBehaviour
{
	[SerializeField]
	private Image m_TechImage;

	[SerializeField]
	private IntVector2 m_TechImageSize = new IntVector2(358, 230);

	[SerializeField]
	private Text m_SkinName;

	[SerializeField]
	private Image m_Bg;

	[SerializeField]
	private float m_SelectedScale;

	[SerializeField]
	private GameObject m_SkinSelector;

	[SerializeField]
	private UIMultiplayerBlocksButton m_BlockPaletteA;

	[SerializeField]
	private UIMultiplayerBlocksButton m_BlockPaletteB;

	[SerializeField]
	private CanvasGroup m_CanvasGroup;

	[SerializeField]
	private float m_AlphaFade;

	public Event<int, int> OnBlockPaletteHighlighted;

	public Event<int, int> OnBlockPaletteTriggered;

	private UIMultiplayerBlocksButton[] m_BlockPalettes;

	private FactionSubTypes m_Corp;

	private int m_CorpIndex;

	private MultiplayerTechSelectPresetAsset m_Preset;

	private int m_SkinIndex = -1;

	private int m_SkinTotal = -1;

	public void SetPresetData(MultiplayerTechSelectPresetAsset preset)
	{
		if (!(m_Preset == preset))
		{
			m_Preset = preset;
			m_SkinIndex = 0;
			m_SkinTotal = Singleton.Manager<ManCustomSkins>.inst.GetNumSkinsInCorp(m_Corp);
			SetSkin(m_Preset);
			SetTechImage();
			SetPaletteData(m_Preset);
			m_SkinSelector.gameObject.SetActive(value: true);
			m_SkinName.text = preset.m_TankName.Value;
		}
	}

	public void SetSkin(MultiplayerTechSelectPresetAsset preset)
	{
		for (int i = 0; i < preset.m_TankPreset.GetTechDataFormatted().m_BlockSpecs.Count; i++)
		{
			TankPreset.BlockSpec value = preset.m_TankPreset.GetTechDataFormatted().m_BlockSpecs[i];
			value.m_SkinID = Singleton.Manager<ManCustomSkins>.inst.SkinIndexToID((byte)m_SkinIndex, Singleton.Manager<ManSpawn>.inst.GetCorporation(value.GetBlockType()));
			preset.m_TankPreset.GetTechDataFormatted().m_BlockSpecs[i] = value;
		}
		Singleton.Manager<ManCustomSkins>.inst.SetSelectedSkinForCorp(m_SkinIndex, m_Corp);
	}

	public void SetTechImage()
	{
		Singleton.Manager<ManScreenshot>.inst.RenderTechImage(m_Preset.m_TankPreset.GetTechDataFormatted(), m_TechImageSize, encodeTechData: false, delegate(TechData techData, Texture2D tex)
		{
			d.Assert(tex != null);
			Rect rect = new Rect(Vector2.zero, new Vector2(tex.width, tex.height));
			m_TechImage.sprite = Sprite.Create(tex, rect, Vector2.zero);
		});
	}

	public void SetPaletteData(MultiplayerTechSelectPresetAsset preset)
	{
		d.Assert(m_BlockPalettes.Length == 2, "UIMultiplayerTechSelectItem.SetBlockPaletteItems - m_BlockPlattes does not contain exactly two items. Current length: " + m_BlockPalettes);
		for (int i = 0; i < m_BlockPalettes.Length; i++)
		{
			BlockCount[] data = ((i == 0) ? preset.m_InventoryBlockList1.Blocks : preset.m_InventoryBlockList2.Blocks);
			m_BlockPalettes[i].SetData(data);
		}
	}

	public void SelectBlockPalette(int paletteIndex, bool isSelected)
	{
		d.Assert(paletteIndex >= 0 && paletteIndex < m_BlockPalettes.Length, "UIMultiplayerTechSelectItem.SelectBlockPalette - array index is out of range. Index: " + paletteIndex);
		base.transform.localScale = Vector3.one * ((isSelected && Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad()) ? m_SelectedScale : 1f);
		m_BlockPalettes[paletteIndex].SetSelected(isSelected);
	}

	public void SetCorpIndex(int i, FactionSubTypes corp)
	{
		m_CorpIndex = i;
		m_Corp = corp;
	}

	public void SetEnabled(bool isEnabled)
	{
		for (int i = 0; i < m_BlockPalettes.Length; i++)
		{
			m_BlockPalettes[i].SetEnabled(isEnabled);
		}
	}

	public void SetFocused(int selectedCorp, int selectedPalette, bool showFocus)
	{
		if (showFocus)
		{
			bool flag = m_CorpIndex == selectedCorp;
			m_CanvasGroup.alpha = (flag ? 1f : m_AlphaFade);
			for (int i = 0; i < m_BlockPalettes.Length; i++)
			{
				bool flag2 = i == selectedPalette;
				if (flag)
				{
					if (flag2)
					{
						m_BlockPalettes[i].SetFade(1f);
					}
					else
					{
						m_BlockPalettes[i].SetFade(m_AlphaFade);
					}
				}
				else
				{
					m_BlockPalettes[i].SetFade(1f);
				}
			}
		}
		else
		{
			m_CanvasGroup.alpha = 1f;
			for (int j = 0; j < m_BlockPalettes.Length; j++)
			{
				m_BlockPalettes[j].SetFade(1f);
			}
		}
	}

	private void OnBlockPaletteClicked(int paletteIndex)
	{
		d.Assert(m_CorpIndex >= 0, "UIMultiplayerTechSelectItem m_CorpIndex was never set");
		OnBlockPaletteTriggered.Send(m_CorpIndex, paletteIndex);
	}

	public void OnPage(int direction)
	{
		int num = m_SkinIndex;
		for (int i = 0; i < m_SkinTotal; i++)
		{
			num = (num + direction + m_SkinTotal) % m_SkinTotal;
			if (Singleton.Manager<ManCustomSkins>.inst.CanUseSkin(m_Corp, num))
			{
				break;
			}
		}
		if (num != m_SkinIndex)
		{
			m_SkinIndex = num;
			SetSkin(m_Preset);
			SetTechImage();
		}
	}

	private void OnHighlightChanged(int paletteIndex, bool isHighlighted)
	{
		if (isHighlighted)
		{
			OnBlockPaletteHighlighted.Send(m_CorpIndex, paletteIndex);
		}
	}

	private void OnPool()
	{
		m_BlockPalettes = new UIMultiplayerBlocksButton[2] { m_BlockPaletteA, m_BlockPaletteB };
		for (int i = 0; i < m_BlockPalettes.Length; i++)
		{
			m_BlockPalettes[i].SetBlockPaletteIndex(i);
			m_BlockPalettes[i].OnClicked.Subscribe(OnBlockPaletteClicked);
			m_BlockPalettes[i].OnPageChanged.Subscribe(OnPage);
			m_BlockPalettes[i].OnHighlightChanged.Subscribe(OnHighlightChanged);
		}
	}
}
