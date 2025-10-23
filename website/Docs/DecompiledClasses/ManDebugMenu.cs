#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class ManDebugMenu : Singleton.Manager<ManDebugMenu>
{
	public enum DebugMenuType
	{
		Folder,
		Toggle,
		FloatSlider,
		IntSlider,
		Button
	}

	public class DebugMenu
	{
		public string m_ParentMenu;

		public List<DebugMenuObject> m_MenuElements = new List<DebugMenuObject>();

		public DebugMenu(string parentMenu)
		{
			m_ParentMenu = parentMenu;
		}
	}

	[SerializeField]
	[EnumArray(typeof(DebugMenuType))]
	private DebugMenuUI[] m_DebugMenuPrefabs;

	[SerializeField]
	private RectTransform m_MenuParentTransform;

	private Dictionary<string, DebugMenu> m_MenuTree = new Dictionary<string, DebugMenu>();

	private List<DebugMenuUI> m_CurrentMenuItems = new List<DebugMenuUI>();

	private string m_CurrentMenu;

	private static readonly string k_RootMenuName = "Root";

	public void ShowMenu(string menuName)
	{
		DebugMenu value = null;
		if (menuName.NullOrEmpty())
		{
			menuName = k_RootMenuName;
		}
		m_MenuTree.TryGetValue(menuName, out value);
		ClearOldMenu();
		bool addBackButton = menuName != k_RootMenuName;
		ShowMenu(value, addBackButton);
		m_CurrentMenu = menuName;
	}

	public void HideMenu()
	{
		ClearOldMenu();
	}

	public void AddFolder(string parentMenu, string folderName)
	{
		DebugMenuFolder menuItem = new DebugMenuFolder(DebugMenuFolder.k_Prefix + folderName, folderName);
		AddMenuItem(parentMenu, menuItem);
		if (parentMenu == null)
		{
			parentMenu = k_RootMenuName;
		}
		DebugMenu value = new DebugMenu(parentMenu);
		m_MenuTree.Add(folderName, value);
	}

	public void AddToggle(string parentMenu, string toggleName, Func<bool> isToggledFunction, Func<bool> toggleFunction)
	{
		DebugMenuToggle menuItem = new DebugMenuToggle(toggleName, isToggledFunction, toggleFunction);
		AddMenuItem(parentMenu, menuItem);
	}

	public void AddButton(string parentMenu, string buttonName, Action buttonAction)
	{
		DebugMenuButton menuItem = new DebugMenuButton(buttonName, buttonAction);
		AddMenuItem(parentMenu, menuItem);
	}

	public void AddFloatSlider(string parentMenu, string name, float min, float max, bool wholeNumbers, Func<float> getSliderValFunction, Action<float> updateSliderAction)
	{
		DebugMenuFloatSlider menuItem = new DebugMenuFloatSlider(name, min, max, wholeNumbers, getSliderValFunction, updateSliderAction);
		AddMenuItem(parentMenu, menuItem);
	}

	private void AddMenuItem(string parentMenu, DebugMenuObject menuItem)
	{
		if (parentMenu == null)
		{
			parentMenu = k_RootMenuName;
		}
		if (m_MenuTree.TryGetValue(parentMenu, out var value))
		{
			m_MenuTree.Remove(parentMenu);
		}
		else if (parentMenu != k_RootMenuName)
		{
			d.LogWarning("ManDebugMenu - Can't find parent menu " + parentMenu + " adding to ROOT menu");
			if (m_MenuTree.TryGetValue(k_RootMenuName, out var value2))
			{
				m_MenuTree.Remove(k_RootMenuName);
			}
			else
			{
				value2 = new DebugMenu(k_RootMenuName);
			}
			DebugMenuFolder item = new DebugMenuFolder(DebugMenuFolder.k_Prefix + parentMenu, parentMenu);
			value2.m_MenuElements.Add(item);
			m_MenuTree.Add(k_RootMenuName, value2);
			value = new DebugMenu(k_RootMenuName);
		}
		else
		{
			value = new DebugMenu(parentMenu);
		}
		value.m_MenuElements.Add(menuItem);
		m_MenuTree.Add(parentMenu, value);
	}

	private void ClearOldMenu()
	{
		for (int i = 0; i < m_CurrentMenuItems.Count; i++)
		{
			m_CurrentMenuItems[i].transform.SetParent(null, worldPositionStays: false);
			m_CurrentMenuItems[i].Recycle();
		}
		m_CurrentMenuItems.Clear();
	}

	private void ShowMenu(DebugMenu menu, bool addBackButton)
	{
		DebugMenuButton data = new DebugMenuButton("Collapse Menu", CollapseMenu);
		AddElement(DebugMenuType.Button, data);
		if (addBackButton)
		{
			string text = ((menu != null) ? menu.m_ParentMenu : k_RootMenuName);
			DebugMenuFolder data2 = new DebugMenuFolder("Back to " + text, text);
			AddElement(DebugMenuType.Folder, data2);
		}
		if (menu != null)
		{
			for (int i = 0; i < menu.m_MenuElements.Count; i++)
			{
				AddElement(menu.m_MenuElements[i].MenuType(), menu.m_MenuElements[i]);
			}
		}
	}

	private void AddElement(DebugMenuType menuType, DebugMenuObject data)
	{
		DebugMenuUI debugMenuUI = GetUIElement(menuType).Spawn(m_MenuParentTransform);
		debugMenuUI.transform.localScale = Vector3.one;
		debugMenuUI.SetMenuObject(data);
		debugMenuUI.Show();
		m_CurrentMenuItems.Add(debugMenuUI);
	}

	private void CollapseMenu()
	{
		ClearOldMenu();
		DebugMenuButton data = new DebugMenuButton("Expand Menu", ExpandMenu);
		AddElement(DebugMenuType.Button, data);
	}

	private void ExpandMenu()
	{
		if (m_CurrentMenu.NullOrEmpty())
		{
			m_CurrentMenu = k_RootMenuName;
		}
		ShowMenu(m_CurrentMenu);
	}

	private DebugMenuUI GetUIElement(DebugMenuType menuType)
	{
		return m_DebugMenuPrefabs[(int)menuType];
	}
}
