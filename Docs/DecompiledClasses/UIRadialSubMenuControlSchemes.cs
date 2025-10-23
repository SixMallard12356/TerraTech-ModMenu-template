using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RadialMenuSubmenu))]
public class UIRadialSubMenuControlSchemes : MonoBehaviour
{
	private struct ControlSchemeSorted
	{
		public ControlScheme m_Scheme;

		public int m_SortOrder;

		public int m_RecentlyUsedOrder;
	}

	private class ControlSchemeSorter : IComparer<ControlSchemeSorted>
	{
		public int Compare(ControlSchemeSorted x, ControlSchemeSorted y)
		{
			return x.m_SortOrder - y.m_SortOrder;
		}
	}

	[SerializeField]
	private SchemaSubmenuListItem m_SchemaListElementPrefab;

	private List<ControlSchemeSorted> m_ControlSchemes = new List<ControlSchemeSorted>();

	private static ControlSchemeSorter m_ControlSchemeSorter = new ControlSchemeSorter();

	private RadialMenuSubmenu m_SubMenu;

	public void SetUpSchemaMenu()
	{
		m_ControlSchemes.Clear();
		Singleton.playerTank.control.CheckSchemeIsSet();
		List<ControlScheme> schemes = Singleton.playerTank.control.Schemes;
		ControlScheme activeScheme = Singleton.playerTank.control.ActiveScheme;
		ControlSchemeLibrary controlSchemeLibrary = Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_ControlSchemeLibrary;
		int num = 0;
		foreach (ControlScheme item in schemes)
		{
			int displaySortOrder = controlSchemeLibrary.GetDisplaySortOrder(item);
			m_ControlSchemes.Add(new ControlSchemeSorted
			{
				m_Scheme = item,
				m_SortOrder = displaySortOrder,
				m_RecentlyUsedOrder = num
			});
			num++;
		}
		m_ControlSchemes.Sort(m_ControlSchemeSorter);
		foreach (ControlSchemeSorted controlScheme in m_ControlSchemes)
		{
			ControlScheme scheme = controlScheme.m_Scheme;
			bool isLastUsed = controlScheme.m_RecentlyUsedOrder == 1;
			AddSchemaOption().SetControlScheme(scheme, isLastUsed, scheme == activeScheme);
		}
	}

	private SchemaSubmenuListItem AddSchemaOption()
	{
		SchemaSubmenuListItem component = Object.Instantiate(m_SchemaListElementPrefab.gameObject).GetComponent<SchemaSubmenuListItem>();
		m_SubMenu.AddAt(component, m_SubMenu.GetElementsCount() - 1);
		return component;
	}

	public void CleanSchemaMenu()
	{
		while (m_SubMenu.GetElementsCount() > 1)
		{
			m_SubMenu.RemoveAt(0);
		}
	}

	public void OnSubmenuItemSelected(int index)
	{
		if (index == m_SubMenu.GetElementsCount() - 1)
		{
			UISchemaMenu uISchemaMenu = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.ControlSchema) as UISchemaMenu;
			if (uISchemaMenu != null)
			{
				Singleton.Manager<ManUI>.inst.PushScreen(uISchemaMenu, ManUI.PauseType.Pause);
			}
		}
		else
		{
			if (index < 0)
			{
				return;
			}
			SchemaSubmenuListItem schemaSubmenuListItem = m_SubMenu.GetOption(index) as SchemaSubmenuListItem;
			if ((bool)Singleton.playerTank && schemaSubmenuListItem != null)
			{
				TankControl control = Singleton.playerTank.control;
				if (control.Schemes.Contains(schemaSubmenuListItem.ControlScheme))
				{
					control.SetActiveScheme(schemaSubmenuListItem.ControlScheme);
				}
			}
		}
	}

	private void OnPool()
	{
		m_SubMenu = GetComponent<RadialMenuSubmenu>();
	}
}
