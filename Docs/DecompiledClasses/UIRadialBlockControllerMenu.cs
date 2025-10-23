#define UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIRadialBlockControllerMenu : UIHUDElement
{
	[SerializeField]
	private RadialMenu m_RadialMenu;

	[SerializeField]
	private UIRadialBlockControllerMenuElement m_ListElementPrefab;

	[SerializeField]
	private LocalisedString m_TooltipWarningCircuitControlledSingle;

	[SerializeField]
	private LocalisedString m_TooltipWarningCircuitControlledSome;

	[SerializeField]
	private LocalisedString m_TooltipWarningCircuitControlledAll;

	private TankBlock m_TargetBlock;

	private ModuleBlockStateController m_BlockControllerModule;

	private GameObject m_CategoriesListUI;

	private bool[] m_CategoryStates = new bool[EnumValuesIterator<ModuleControlCategory>.Count];

	private List<UIRadialBlockControllerMenuElement> m_SubMenuElements;

	private bool m_Init;

	private IRadialInputController m_RadialController;

	private Button m_DefaultCategoryFocus;

	private const int CHOICE_OFF = 1;

	private const int CHOICE_ON = 2;

	public override void Show(object context)
	{
		OpenMenuEventData openMenuEventData = (OpenMenuEventData)context;
		m_TargetBlock = openMenuEventData.m_TargetTankBlock;
		m_BlockControllerModule = m_TargetBlock.GetComponent<ModuleBlockStateController>();
		if (!IsMenuAvailableForTech(m_TargetBlock.tank) || !openMenuEventData.m_AllowRadialMenu || !(m_BlockControllerModule != null))
		{
			return;
		}
		for (int i = 0; i < m_CategoryStates.Length; i++)
		{
			m_CategoryStates[i] = false;
		}
		foreach (ModuleControlCategory controlledCategory in m_BlockControllerModule.ControlledCategories)
		{
			m_CategoryStates[(int)controlledCategory] = true;
		}
		if (m_CategoriesListUI != null)
		{
			m_CategoriesListUI.gameObject.SetActive(value: false);
			m_DefaultCategoryFocus = null;
			for (int j = 0; j < m_CategoryStates.Length; j++)
			{
				m_SubMenuElements[j].gameObject.SetActive(value: false);
				m_SubMenuElements[j].SetControlledByCircuits(state: false);
			}
			foreach (ModuleControlCategory selectableCategory in m_BlockControllerModule.SelectableCategories)
			{
				int num = (int)selectableCategory;
				m_SubMenuElements[num].gameObject.SetActive(value: true);
				m_SubMenuElements[num].SetControlState(m_CategoryStates[num]);
				RefreshCategoryCircuitControlledIndicators(selectableCategory);
				Button button;
				if (m_DefaultCategoryFocus == null && (object)(button = m_SubMenuElements[num].Button) != null)
				{
					m_DefaultCategoryFocus = button;
				}
			}
		}
		m_RadialController = Singleton.Manager<ManInput>.inst.GetRadialInputController(openMenuEventData.m_RadialInputController);
		base.Show(context);
		m_RadialMenu.Show(openMenuEventData.m_RadialInputController, m_TargetBlock.tank != Singleton.playerTank);
		RefreshRadialOptionsAvailable();
	}

	private void RefreshCategoryCircuitControlledIndicators(ModuleControlCategory controlCategory)
	{
		bool controlledByCircuits = m_TargetBlock.tank.BlockStateController.IsCategoryCircuitControlled(controlCategory);
		m_SubMenuElements[(int)controlCategory].SetControlledByCircuits(controlledByCircuits);
	}

	private void RefreshRadialOptionsAvailable()
	{
		bool flag = m_BlockControllerModule.SelectableCategories.Count() > 1;
		bool flag2 = true;
		bool flag3 = false;
		foreach (ModuleControlCategory controlledCategory in m_BlockControllerModule.ControlledCategories)
		{
			if (!m_TargetBlock.tank.BlockStateController.IsCategoryCircuitControlled(controlledCategory))
			{
				flag3 = true;
			}
			else
			{
				flag2 = false;
			}
		}
		for (int num = 0; num < m_RadialMenu.GetOptionsCount(); num++)
		{
			UIRadialMenuOptionWithWarning uIRadialMenuOptionWithWarning = m_RadialMenu.GetOption(num) as UIRadialMenuOptionWithWarning;
			int optionIdx = GetOptionIdx(num);
			int num2;
			int num3;
			if (optionIdx != 2)
			{
				num2 = ((optionIdx == 1) ? 1 : 0);
				if (num2 == 0)
				{
					num3 = 0;
					goto IL_00a3;
				}
			}
			else
			{
				num2 = 1;
			}
			num3 = ((!flag3) ? 1 : 0);
			goto IL_00a3;
			IL_00a3:
			bool isAllowed = num3 == 0;
			uIRadialMenuOptionWithWarning.SetIsAllowed(isAllowed);
			if (num2 != 0 && !flag2)
			{
				LocalisedString localisedString = (flag3 ? m_TooltipWarningCircuitControlledSome : (flag ? m_TooltipWarningCircuitControlledAll : m_TooltipWarningCircuitControlledSingle));
				uIRadialMenuOptionWithWarning.TooltipComponent.SetText(localisedString.Value);
				uIRadialMenuOptionWithWarning.TooltipComponent.SetMode(UITooltipOptions.Warning);
			}
		}
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		m_RadialMenu.Hide();
		m_TargetBlock = null;
	}

	private int GetOptionIdx(int rawIdx)
	{
		if (!m_CategoriesListUI)
		{
			return rawIdx + 1;
		}
		return rawIdx;
	}

	private void OnOptionSelected(int option)
	{
		option = GetOptionIdx(option);
		if (option == 0 && !m_BlockControllerModule.HasFixedCategories)
		{
			if (m_RadialController.IsGamePad())
			{
				m_CategoriesListUI.gameObject.SetActive(value: true);
				m_RadialMenu.SetModal(modal: true, option);
				Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(m_DefaultCategoryFocus.gameObject);
			}
			else if (m_CategoriesListUI.gameObject.activeInHierarchy)
			{
				HideSelf();
			}
			else
			{
				m_CategoriesListUI.gameObject.SetActive(value: true);
				m_RadialMenu.SetModal(modal: true, option);
			}
			return;
		}
		switch (option)
		{
		case 1:
			m_BlockControllerModule.SetControlledCategoriesActive(active: false);
			HideSelf();
			break;
		case 2:
			m_BlockControllerModule.SetControlledCategoriesActive(active: true);
			HideSelf();
			break;
		}
	}

	private bool IsMenuAvailableForTech(Tank targetTank)
	{
		bool num = !Singleton.Manager<ManGameMode>.inst.LockPlayerControls && Singleton.playerTank != null;
		bool flag = targetTank != null && (targetTank == Singleton.playerTank || !m_TargetBlock.ContextMenuForPlayerTechOnly) && targetTank.Team == Singleton.Manager<ManPlayer>.inst.PlayerTeam && targetTank.visible.IsInteractible;
		bool flag2 = false;
		bool flag3 = Singleton.Manager<ManPointer>.inst.DraggingItem != null;
		bool isInteractionBlocked = Singleton.Manager<ManPointer>.inst.IsInteractionBlocked;
		if (num && flag && !flag2 && !isInteractionBlocked)
		{
			return !flag3;
		}
		return false;
	}

	private void SetControllerCategoryEnabled(int categoryIndex, bool enabled)
	{
		m_CategoryStates[categoryIndex] = enabled;
		if (m_BlockControllerModule != null)
		{
			m_BlockControllerModule.RequestModifyControlledCategory((ModuleControlCategory)categoryIndex, enabled);
			m_SubMenuElements[categoryIndex].SetControlState(enabled);
			RefreshCategoryCircuitControlledIndicators((ModuleControlCategory)categoryIndex);
		}
		RefreshRadialOptionsAvailable();
	}

	private void Awake()
	{
		d.Assert(m_RadialMenu != null);
		d.Assert(m_ListElementPrefab != null);
		m_Init = false;
		m_CategoriesListUI = base.transform.Find("BlockControlSwitch_List")?.gameObject;
		int count = EnumValuesIterator<ModuleControlCategory>.Count;
		m_SubMenuElements = new List<UIRadialBlockControllerMenuElement>(count);
		if ((bool)m_CategoriesListUI)
		{
			foreach (Transform item in m_CategoriesListUI.transform)
			{
				Object.Destroy(item.gameObject);
			}
		}
		EnumValuesIterator<ModuleControlCategory> enumerator2 = EnumIterator<ModuleControlCategory>.Values().GetEnumerator();
		while (enumerator2.MoveNext())
		{
			ModuleControlCategory current = enumerator2.Current;
			if ((bool)m_CategoriesListUI)
			{
				UIRadialBlockControllerMenuElement uIRadialBlockControllerMenuElement = Object.Instantiate(m_ListElementPrefab, m_CategoriesListUI.transform, worldPositionStays: false);
				uIRadialBlockControllerMenuElement.SetCategory(current, SetControllerCategoryEnabled);
				m_SubMenuElements.Add(uIRadialBlockControllerMenuElement);
				if (uIRadialBlockControllerMenuElement.Button != null && uIRadialBlockControllerMenuElement.CategoryIndex == 0)
				{
					m_DefaultCategoryFocus = uIRadialBlockControllerMenuElement.Button;
				}
			}
		}
		m_RadialMenu.OnOptionSelected.Subscribe(OnOptionSelected);
		m_RadialMenu.OnClose.Subscribe(base.HideSelf);
		m_Init = true;
	}

	private void Update()
	{
		if (m_RadialMenu.IsModal() && m_RadialController.DidCancel())
		{
			m_CategoriesListUI?.gameObject?.SetActive(value: false);
			m_RadialMenu.SetModal(modal: false);
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(m_DefaultCategoryFocus.gameObject);
		}
	}
}
