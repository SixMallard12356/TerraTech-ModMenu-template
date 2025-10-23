#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UIBlockMenuSelection : UIHUDElement
{
	public class Context
	{
		public ModeMask targetMode;

		public UIShopBlockSelect.ShowContext showBlockContext;
	}

	public enum ModeMask
	{
		Null,
		BlockPalette,
		BlockPaletteAndTechs,
		BlockShop
	}

	[SerializeField]
	private RectTransform m_ButtonGroup;

	[SerializeField]
	private Selectable m_BlockPaletteButton;

	[SerializeField]
	private Selectable m_CloseBlockPaletteButton;

	[SerializeField]
	private Selectable m_BlockShopButton;

	[SerializeField]
	private Selectable m_TechShopButton;

	[SerializeField]
	private Selectable m_CloseShopButton;

	private UIHUDElement m_ExpandedPane;

	private Bitfield<ModeMask> m_EnabledModes = new Bitfield<ModeMask>(0);

	public override void Show(object context)
	{
		UIShopBlockSelect.ShowContext context2 = null;
		if (context is Context context3)
		{
			context2 = context3.showBlockContext;
			m_EnabledModes.Add((int)context3.targetMode);
		}
		else
		{
			d.LogError("UIBlockMenuSelection.Show - attempting to show without a valid targetMode set in context");
			m_EnabledModes.Add(3);
		}
		bool flag = m_EnabledModes.Contains(1) || m_EnabledModes.Contains(2);
		bool flag2 = m_EnabledModes.Contains(2);
		bool num = m_EnabledModes.Contains(3);
		if (flag)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.BlockPalette);
		}
		if (num)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.BlockShop, context2);
		}
		if (flag2)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechShop);
		}
		if (num)
		{
			ShowBlockShopTab();
		}
		RefreshButtons(m_EnabledModes);
		base.Show(context);
	}

	public override void Hide(object context)
	{
		Context context2 = context as Context;
		if (context != null)
		{
			m_EnabledModes.Remove((int)context2.targetMode);
		}
		else
		{
			d.LogError("UIBlockMenuSelection.Hide - attempting to hide without a valid targetMode set in context");
			m_EnabledModes.Remove(3);
		}
		bool flag = m_EnabledModes.Contains(1) || m_EnabledModes.Contains(2);
		bool num = m_EnabledModes.Contains(2);
		bool num2 = m_EnabledModes.Contains(3);
		if (!flag)
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.BlockPalette);
		}
		if (!num2)
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.BlockShop);
		}
		if (!num)
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TechShop);
		}
		RefreshButtons(m_EnabledModes);
		base.Hide(context);
	}

	public void ShowPaletteTab()
	{
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayerAndNotPlaying())
		{
			UIShopBlockSelect.ExpandContext context = new UIShopBlockSelect.ExpandContext
			{
				expandReason = UIShopBlockSelect.ExpandReason.Button
			};
			Singleton.Manager<ManHUD>.inst.ExpandHudElement(ManHUD.HUDElementType.BlockPalette, context);
		}
	}

	public void HidePaletteTab()
	{
		UIShopBlockSelect.ExpandContext context = new UIShopBlockSelect.ExpandContext
		{
			expandReason = UIShopBlockSelect.ExpandReason.Button,
			forceClose = true
		};
		Singleton.Manager<ManHUD>.inst.CollapseHudElement(ManHUD.HUDElementType.BlockPalette, context);
	}

	public void ShowBlockShopTab()
	{
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayerAndNotPlaying())
		{
			UIShopBlockSelect.ExpandContext context = new UIShopBlockSelect.ExpandContext
			{
				expandReason = UIShopBlockSelect.ExpandReason.Button
			};
			Singleton.Manager<ManHUD>.inst.ExpandHudElement(ManHUD.HUDElementType.BlockShop, context);
		}
	}

	public void ShowTechShopTab()
	{
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayerAndNotPlaying())
		{
			Singleton.Manager<ManHUD>.inst.ExpandHudElement(ManHUD.HUDElementType.TechShop);
		}
	}

	public void CloseAllTabs()
	{
		Singleton.Manager<ManHUD>.inst.CollapseHudElement(ManHUD.HUDElementType.BlockPalette);
		Singleton.Manager<ManHUD>.inst.CollapseHudElement(ManHUD.HUDElementType.BlockShop);
		Singleton.Manager<ManHUD>.inst.CollapseHudElement(ManHUD.HUDElementType.TechShop);
	}

	private bool IsHUDElementExpanded(ManHUD.HUDElementType elementType)
	{
		bool result = false;
		UIHUDElement hudElement = Singleton.Manager<ManHUD>.inst.GetHudElement(elementType);
		if ((bool)hudElement)
		{
			result = hudElement.IsVisible && hudElement.IsExpanded;
		}
		return result;
	}

	private void ResetButtonGroupPositionIfAllTabsClosed(UIHUDElement element)
	{
		if ((bool)m_ExpandedPane && !(IsHUDElementExpanded(ManHUD.HUDElementType.BlockPalette) | IsHUDElementExpanded(ManHUD.HUDElementType.BlockShop) | IsHUDElementExpanded(ManHUD.HUDElementType.TechShop)))
		{
			d.Assert(element == m_ExpandedPane, "UIBlockMenuSelection - on last pane closed expected tab to be the same as cached ExpandedPane");
			RectTransform rectTransform = m_ExpandedPane.transform as RectTransform;
			m_ButtonGroup.anchoredPosition -= new Vector2(rectTransform.rect.width, 0f);
			m_ExpandedPane = null;
			RefreshButtons(m_EnabledModes);
		}
	}

	private void Update()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetwork>.inst.NetController != null && Singleton.Manager<ManNetwork>.inst.NetController.GameModeType == MultiplayerModeType.Deathmatch)
		{
			if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayerAndInvulnerable())
			{
				if (!Singleton.Manager<ManNetwork>.inst.InventoryAvailable)
				{
					CloseAllTabs();
					m_BlockPaletteButton.gameObject.SetActive(value: false);
					m_BlockShopButton.gameObject.SetActive(value: false);
					m_TechShopButton.gameObject.SetActive(value: false);
					return;
				}
				if (Singleton.Manager<ManHUD>.inst.IsHudElementExpanded(ManHUD.HUDElementType.BlockShop))
				{
					Singleton.Manager<ManHUD>.inst.CollapseHudElement(ManHUD.HUDElementType.BlockShop);
				}
				if (Singleton.Manager<ManHUD>.inst.IsHudElementExpanded(ManHUD.HUDElementType.TechShop))
				{
					Singleton.Manager<ManHUD>.inst.CollapseHudElement(ManHUD.HUDElementType.TechShop);
				}
				m_BlockShopButton.gameObject.SetActive(value: false);
				m_TechShopButton.gameObject.SetActive(value: false);
			}
			else if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && !Singleton.Manager<ManNetwork>.inst.InventoryAvailable)
			{
				CloseAllTabs();
				m_BlockPaletteButton.gameObject.SetActive(value: false);
				m_BlockShopButton.gameObject.SetActive(value: false);
				m_TechShopButton.gameObject.SetActive(value: false);
			}
			else
			{
				RefreshButtons(m_EnabledModes);
				m_TechShopButton.gameObject.SetActive(value: false);
			}
		}
		else if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			m_TechShopButton.gameObject.SetActive(value: false);
		}
	}

	private void RefreshButtons(Bitfield<ModeMask> enabledModes)
	{
		bool flag = IsHUDElementExpanded(ManHUD.HUDElementType.BlockPalette);
		bool flag2 = IsHUDElementExpanded(ManHUD.HUDElementType.BlockShop);
		bool flag3 = IsHUDElementExpanded(ManHUD.HUDElementType.TechShop);
		bool num = flag || flag2 || flag3;
		m_BlockPaletteButton.interactable = !flag;
		m_CloseBlockPaletteButton.interactable = flag;
		m_BlockShopButton.interactable = !flag2;
		m_TechShopButton.interactable = false;
		bool active = (enabledModes.Contains(1) || enabledModes.Contains(2)) && !flag && !flag2;
		bool active2 = false;
		bool active3 = enabledModes.Contains(3) && !flag2;
		bool active4 = num;
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			active = false;
			active2 = false;
			active3 = false;
			active4 = false;
		}
		m_BlockPaletteButton.gameObject.SetActive(active);
		m_CloseBlockPaletteButton.gameObject.SetActive(value: false);
		m_BlockShopButton.gameObject.SetActive(active3);
		m_TechShopButton.gameObject.SetActive(active2);
		m_CloseShopButton.gameObject.SetActive(active4);
		LayoutRebuilder.MarkLayoutForRebuild(m_ButtonGroup);
	}

	protected override void OnInputModeChanged(ManInput.InputMode inputMode)
	{
		base.OnInputModeChanged(inputMode);
		if (base.IsVisible)
		{
			RefreshButtons(m_EnabledModes);
		}
	}

	private void OnHUDElementShown(UIHUDElement element)
	{
		if (element.HudElementType == ManHUD.HUDElementType.BlockPalette || element.HudElementType == ManHUD.HUDElementType.BlockShop || element.HudElementType == ManHUD.HUDElementType.TechShop)
		{
			RefreshButtons(m_EnabledModes);
		}
	}

	private void OnHUDElementHidden(UIHUDElement element)
	{
		if (element.HudElementType == ManHUD.HUDElementType.BlockPalette || element.HudElementType == ManHUD.HUDElementType.BlockShop || element.HudElementType == ManHUD.HUDElementType.TechShop)
		{
			ResetButtonGroupPositionIfAllTabsClosed(element);
			RefreshButtons(m_EnabledModes);
		}
	}

	private void OnHUDElementExpanded(UIHUDElement element)
	{
		if (element.HudElementType != ManHUD.HUDElementType.BlockPalette && element.HudElementType != ManHUD.HUDElementType.BlockShop && element.HudElementType != ManHUD.HUDElementType.TechShop)
		{
			return;
		}
		if (element != m_ExpandedPane)
		{
			float num = 0f;
			if ((bool)m_ExpandedPane)
			{
				num = 0f - (m_ExpandedPane.transform as RectTransform).rect.width;
			}
			num += (element.transform as RectTransform).rect.width;
			m_ButtonGroup.anchoredPosition += new Vector2(num, 0f);
			m_ExpandedPane = element;
		}
		RefreshButtons(m_EnabledModes);
	}

	private void OnHUDElementCollapsed(UIHUDElement element)
	{
		if (element.HudElementType == ManHUD.HUDElementType.BlockPalette || element.HudElementType == ManHUD.HUDElementType.BlockShop || element.HudElementType == ManHUD.HUDElementType.TechShop)
		{
			ResetButtonGroupPositionIfAllTabsClosed(element);
			RefreshButtons(m_EnabledModes);
		}
	}

	private void OnSpawn()
	{
		m_EnabledModes.Clear();
		RefreshButtons(m_EnabledModes);
		Singleton.Manager<ManHUD>.inst.OnShowHUDElementEvent.Subscribe(OnHUDElementShown);
		Singleton.Manager<ManHUD>.inst.OnHideHUDElementEvent.Subscribe(OnHUDElementHidden);
		Singleton.Manager<ManHUD>.inst.OnExpandHUDElementEvent.Subscribe(OnHUDElementExpanded);
		Singleton.Manager<ManHUD>.inst.OnCollapseHUDElementEvent.Subscribe(OnHUDElementCollapsed);
	}

	private void OnRecycle()
	{
		if ((bool)m_ExpandedPane)
		{
			RectTransform rectTransform = m_ExpandedPane.transform as RectTransform;
			m_ButtonGroup.anchoredPosition -= new Vector2(rectTransform.rect.width, 0f);
			m_ExpandedPane = null;
		}
		Singleton.Manager<ManHUD>.inst.OnShowHUDElementEvent.Unsubscribe(OnHUDElementShown);
		Singleton.Manager<ManHUD>.inst.OnHideHUDElementEvent.Unsubscribe(OnHUDElementHidden);
		Singleton.Manager<ManHUD>.inst.OnExpandHUDElementEvent.Unsubscribe(OnHUDElementExpanded);
		Singleton.Manager<ManHUD>.inst.OnCollapseHUDElementEvent.Unsubscribe(OnHUDElementCollapsed);
	}
}
