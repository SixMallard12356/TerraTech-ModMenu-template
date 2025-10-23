#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMultiplayerBlocksButton : MonoBehaviour, IMoveHandler, IEventSystemHandler, ISelectHandler, IDeselectHandler
{
	[SerializeField]
	private Button m_Button;

	[SerializeField]
	private UIMultiplayerTechSelectBlockItem[] m_BlockPaletteItems;

	[SerializeField]
	private CanvasGroup m_CanvasGroup;

	public Event<int> OnClicked;

	public Event<int> OnPageChanged;

	public Event<int, bool> OnHighlightChanged;

	private int m_PaletteIndex = -1;

	public bool Highlighted { get; set; }

	public void SetBlockPaletteIndex(int paletteIndex)
	{
		m_PaletteIndex = paletteIndex;
	}

	public void SetData(BlockCount[] blockInventory)
	{
		for (int i = 0; i < m_BlockPaletteItems.Length; i++)
		{
			UIMultiplayerTechSelectBlockItem uIMultiplayerTechSelectBlockItem = m_BlockPaletteItems[i];
			if (i < blockInventory.Length)
			{
				uIMultiplayerTechSelectBlockItem.SetData(blockInventory[i]);
			}
			else
			{
				uIMultiplayerTechSelectBlockItem.SetData(null);
			}
		}
	}

	public void SetSelected(bool isSelected)
	{
		if (isSelected && !EventSystem.current.alreadySelecting)
		{
			EventSystem.current.SetSelectedGameObject(m_Button.gameObject);
		}
	}

	public void SetEnabled(bool isEnabled)
	{
		m_Button.enabled = isEnabled;
	}

	public void SetFade(float fade)
	{
		m_CanvasGroup.alpha = fade;
	}

	public void OnMove(AxisEventData eventData)
	{
		if (eventData.moveDir == MoveDirection.Up)
		{
			OnPageChanged.Send(1);
			eventData.Use();
		}
		else if (eventData.moveDir == MoveDirection.Down)
		{
			OnPageChanged.Send(-1);
			eventData.Use();
		}
	}

	public void OnSelect(BaseEventData eventData)
	{
		Highlighted = true;
		OnHighlightChanged.Send(m_PaletteIndex, paramB: true);
	}

	public void OnDeselect(BaseEventData eventData)
	{
		Highlighted = false;
		OnHighlightChanged.Send(m_PaletteIndex, paramB: false);
	}

	private void OnButtonClicked()
	{
		d.Assert(m_PaletteIndex >= 0, "UIMultiplayerBlocksButton m_PaletteIndex was never set");
		OnClicked.Send(m_PaletteIndex);
	}

	private void OnPool()
	{
		m_Button.onClick.AddListener(OnButtonClicked);
	}
}
