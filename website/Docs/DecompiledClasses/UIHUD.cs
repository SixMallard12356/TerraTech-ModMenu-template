#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIHUD : MonoBehaviour
{
	public enum ShowAction
	{
		Show,
		Expand
	}

	private struct TypeAction : IEquatable<TypeAction>
	{
		public ManHUD.HUDElementType elemType;

		public ShowAction showAction;

		public override bool Equals(object other)
		{
			if (other is TypeAction)
			{
				return this == (TypeAction)other;
			}
			return false;
		}

		public bool Equals(TypeAction other)
		{
			return this == other;
		}

		public override int GetHashCode()
		{
			return (int)(elemType + ((int)showAction << 16));
		}

		public static bool operator ==(TypeAction a, TypeAction b)
		{
			if (a.elemType == b.elemType)
			{
				return a.showAction == b.showAction;
			}
			return false;
		}

		public static bool operator !=(TypeAction a, TypeAction b)
		{
			return !(a == b);
		}
	}

	public struct ObscuredData
	{
		public bool showWhenUnobscured;

		public List<object> contexts;
	}

	[SerializeField]
	private PrefabSpawner m_HudPrefabSpawner;

	private Dictionary<int, UIHUDElement> m_HUDElements = new Dictionary<int, UIHUDElement>();

	private Dictionary<int, List<TypeAction>> m_ExclusiveHUDGroups = new Dictionary<int, List<TypeAction>>();

	private Dictionary<TypeAction, List<ManHUD.HUDGroup>> m_ExclusiveHUDGroupReverseLookup = new Dictionary<TypeAction, List<ManHUD.HUDGroup>>();

	private Dictionary<int, Bitfield<ManHUD.GroupRules>> m_HUDGroupRules = new Dictionary<int, Bitfield<ManHUD.GroupRules>>();

	private ObscuredData[] m_ObscuredData;

	public bool IsSettingUp
	{
		get
		{
			if ((bool)m_HudPrefabSpawner)
			{
				return m_HudPrefabSpawner.IsSettingUp;
			}
			return false;
		}
	}

	public void InitialiseHudElement(ManHUD.HUDElementType hudElemType, object context = null)
	{
		UIHUDElement hudElement = GetHudElement(hudElemType);
		if (hudElement != null)
		{
			hudElement.Init(context);
		}
	}

	public void DeInitialiseHudElement(ManHUD.HUDElementType hudElemType, object context = null)
	{
		UIHUDElement hudElement = GetHudElement(hudElemType);
		if (hudElement != null)
		{
			hudElement.DeInit(context);
		}
	}

	public void ShowHudElement(ManHUD.HUDElementType hudElemType, object context = null)
	{
		if (CheckShowActionAllowed(hudElemType, ShowAction.Show))
		{
			UIHUDElement hudElement = GetHudElement(hudElemType);
			if (!(hudElement != null))
			{
				return;
			}
			if (hudElement.Obscurers == null)
			{
				DoShowElement(hudElemType, hudElement, context);
				return;
			}
			m_ObscuredData[(int)hudElemType].showWhenUnobscured = true;
			if (context != null)
			{
				if (m_ObscuredData[(int)hudElemType].contexts == null)
				{
					m_ObscuredData[(int)hudElemType].contexts = new List<object>(1);
				}
				m_ObscuredData[(int)hudElemType].contexts.Add(context);
			}
		}
		else
		{
			d.LogWarningFormat("UIHUD.ShowElement cannot show element {0} because its HUD group is locked.  Suggest checking this before showing this element.", hudElemType);
		}
	}

	public void HideHudElement(ManHUD.HUDElementType hudElemType, object context = null)
	{
		UIHUDElement hudElement = GetHudElement(hudElemType);
		if (hudElement != null)
		{
			if (IsHudElementVisible(hudElemType))
			{
				DoHideElement(hudElemType, hudElement, context);
			}
			m_ObscuredData[(int)hudElemType] = default(ObscuredData);
		}
	}

	public bool IsHudElementVisible(ManHUD.HUDElementType hudElemType)
	{
		bool result = false;
		UIHUDElement hudElement = GetHudElement(hudElemType);
		if (hudElement != null)
		{
			result = hudElement.IsVisible;
		}
		return result;
	}

	public void ExpandHudElement(ManHUD.HUDElementType hudElemType, object context = null)
	{
		if (CheckShowActionAllowed(hudElemType, ShowAction.Expand))
		{
			UIHUDElement hudElement = GetHudElement(hudElemType);
			if (hudElement != null && hudElement.Expand(context))
			{
				UpdateExclusiveGroups(hudElemType, ShowAction.Expand);
				Singleton.Manager<ManHUD>.inst.OnExpandHUDElementEvent.Send(hudElement);
			}
		}
		else
		{
			d.LogWarningFormat("UIHUD.ShowElement cannot expand element {0} because its HUD group is locked. Suggest checking this before showing this element.", hudElemType);
		}
	}

	public void CollapseHudElement(ManHUD.HUDElementType hudElemType, object context = null)
	{
		UIHUDElement hudElement = GetHudElement(hudElemType);
		if (hudElement != null && hudElement.Collapse(context))
		{
			Singleton.Manager<ManHUD>.inst.OnCollapseHUDElementEvent.Send(hudElement);
		}
	}

	public bool IsHudElementExpanded(ManHUD.HUDElementType hudElemType)
	{
		bool result = false;
		UIHUDElement hudElement = GetHudElement(hudElemType);
		if (hudElement != null)
		{
			result = hudElement.IsExpanded;
		}
		return result;
	}

	public UIHUDElement GetHudElement(ManHUD.HUDElementType hudElemType)
	{
		if (!m_HUDElements.TryGetValue((int)hudElemType, out var value))
		{
			return null;
		}
		return value;
	}

	public void SetGroupRule(ManHUD.HUDGroup hudGroup, ManHUD.GroupRules rule, bool enabled)
	{
		if (!m_HUDGroupRules.TryGetValue((int)hudGroup, out var value))
		{
			value = new Bitfield<ManHUD.GroupRules>();
			m_HUDGroupRules.Add((int)hudGroup, value);
		}
		value.Set((int)rule, enabled);
	}

	public bool IsGroupRuleSet(ManHUD.HUDGroup hudGroup, ManHUD.GroupRules rule)
	{
		if (m_HUDGroupRules.TryGetValue((int)hudGroup, out var value) && value != null)
		{
			return value.Contains((int)rule);
		}
		return false;
	}

	public void AddElementToGroup(ManHUD.HUDGroup hudGroup, ManHUD.HUDElementType elementType, ShowAction showAction)
	{
		TypeAction typeAction = new TypeAction
		{
			elemType = elementType,
			showAction = showAction
		};
		List<TypeAction> value = null;
		bool num = m_ExclusiveHUDGroups.TryGetValue((int)hudGroup, out value);
		if (value == null)
		{
			value = new List<TypeAction>();
		}
		value.Add(typeAction);
		if (!num)
		{
			m_ExclusiveHUDGroups.Add((int)hudGroup, value);
		}
		List<ManHUD.HUDGroup> value2 = null;
		bool num2 = m_ExclusiveHUDGroupReverseLookup.TryGetValue(typeAction, out value2);
		if (value2 == null)
		{
			value2 = new List<ManHUD.HUDGroup>();
		}
		value2.Add(hudGroup);
		if (!num2)
		{
			m_ExclusiveHUDGroupReverseLookup.Add(typeAction, value2);
		}
	}

	public void RemoveElementFromGroup(ManHUD.HUDGroup hudGroup, ManHUD.HUDElementType elementType, ShowAction showAction)
	{
		TypeAction typeAction = new TypeAction
		{
			elemType = elementType,
			showAction = showAction
		};
		List<TypeAction> value = null;
		if (m_ExclusiveHUDGroups.TryGetValue((int)hudGroup, out value))
		{
			value?.Remove(typeAction);
		}
		if (m_ExclusiveHUDGroupReverseLookup.TryGetValue(typeAction, out var value2))
		{
			value2?.Remove(hudGroup);
		}
	}

	public bool IsAnyElementInGroupVisible(ManHUD.HUDGroup hudGroup)
	{
		bool flag = false;
		List<TypeAction> value = null;
		if (m_ExclusiveHUDGroups.TryGetValue((int)hudGroup, out value) && value != null)
		{
			for (int i = 0; i < value.Count; i++)
			{
				if (flag)
				{
					break;
				}
				TypeAction typeAction = value[i];
				switch (typeAction.showAction)
				{
				case ShowAction.Show:
					flag = IsHudElementVisible(typeAction.elemType);
					break;
				case ShowAction.Expand:
					flag = IsHudElementExpanded(typeAction.elemType);
					break;
				default:
					d.LogError("UIHUD.IsAnyElementInExclusiveGroupVisible unhandled showAction");
					break;
				}
			}
		}
		return flag;
	}

	public void AddElement(UIHUDElement hudElement)
	{
		m_HUDElements.Add((int)hudElement.HudElementType, hudElement);
		hudElement.SetParentHUD(this);
		hudElement.gameObject.SetActive(value: false);
	}

	public bool HandleEscapeKey()
	{
		bool flag = false;
		foreach (int key in m_HUDElements.Keys)
		{
			UIHUDElement uIHUDElement = m_HUDElements[key];
			switch (uIHUDElement.GetEscapeKeyAction())
			{
			case UIHUDElement.EscapeKeyActionType.Hide:
				if (uIHUDElement.IsVisible)
				{
					HideHudElement((ManHUD.HUDElementType)key);
					flag = (byte)((flag ? 1u : 0u) | 1u) != 0;
				}
				break;
			case UIHUDElement.EscapeKeyActionType.Collapse:
				if (uIHUDElement.IsExpanded)
				{
					CollapseHudElement((ManHUD.HUDElementType)key);
					flag = (byte)((flag ? 1u : 0u) | 1u) != 0;
				}
				break;
			case UIHUDElement.EscapeKeyActionType.Custom:
				flag |= uIHUDElement.HandleCustomEscapeKeyAction();
				break;
			default:
			{
				string text = uIHUDElement.GetEscapeKeyAction().ToString();
				ManHUD.HUDElementType hUDElementType = (ManHUD.HUDElementType)key;
				d.Assert(condition: false, "UIHUD.Update - invalid escape key action of " + text + " on hud type " + hUDElementType);
				break;
			}
			case UIHUDElement.EscapeKeyActionType.None:
				break;
			}
		}
		return flag;
	}

	public bool CheckShowActionAllowed(ManHUD.HUDElementType shownElementType, ShowAction showAction)
	{
		bool result = true;
		TypeAction key = new TypeAction
		{
			elemType = shownElementType,
			showAction = showAction
		};
		if (m_ExclusiveHUDGroupReverseLookup.TryGetValue(key, out var value) && value != null)
		{
			for (int i = 0; i < value.Count; i++)
			{
				if (IsGroupRuleSet(value[i], ManHUD.GroupRules.Locked))
				{
					result = false;
					break;
				}
			}
		}
		return result;
	}

	public void UpdateObscuredElements()
	{
		int num = 10;
		bool flag = true;
		while (flag && num > 0)
		{
			num--;
			flag = false;
			int num2 = m_ObscuredData.Length;
			for (int i = 0; i < num2; i++)
			{
				if (!m_ObscuredData[i].showWhenUnobscured)
				{
					continue;
				}
				ManHUD.HUDElementType hudElemType = (ManHUD.HUDElementType)i;
				UIHUDElement hudElement = GetHudElement(hudElemType);
				if (!(hudElement != null))
				{
					continue;
				}
				bool flag2 = false;
				List<ManHUD.HUDElementType> obscurers = hudElement.Obscurers;
				int num3 = obscurers?.Count ?? 0;
				for (int j = 0; j < num3; j++)
				{
					ManHUD.HUDElementType hudElemType2 = obscurers[j];
					if (IsHudElementVisible(hudElemType2))
					{
						flag2 = true;
						break;
					}
				}
				bool flag3 = !flag2;
				if (flag3 == hudElement.IsVisible)
				{
					continue;
				}
				if (flag3)
				{
					if (m_ObscuredData[i].contexts == null)
					{
						DoShowElement(hudElemType, hudElement, null);
					}
					else
					{
						for (int k = 0; k < m_ObscuredData[i].contexts.Count; k++)
						{
							DoShowElement(hudElemType, hudElement, m_ObscuredData[i].contexts[k]);
						}
						m_ObscuredData[i].contexts = null;
					}
				}
				else
				{
					DoHideElement(hudElemType, hudElement, null);
				}
				flag = true;
			}
		}
		if (num <= 0)
		{
			d.LogErrorFormat("UIHUD.UpdateObscuredElements is using up all of its allowed loops");
		}
	}

	private void UpdateExclusiveGroups(ManHUD.HUDElementType shownElementType, ShowAction showAction)
	{
		TypeAction key = new TypeAction
		{
			elemType = shownElementType,
			showAction = showAction
		};
		if (!m_ExclusiveHUDGroupReverseLookup.TryGetValue(key, out var value) || value == null)
		{
			return;
		}
		for (int i = 0; i < value.Count; i++)
		{
			if (IsGroupRuleSet(value[i], ManHUD.GroupRules.ExclusiveVisibility))
			{
				CloseElementsInExclusiveGroup(value[i], shownElementType);
			}
		}
	}

	private void CloseElementsInExclusiveGroup(ManHUD.HUDGroup hudGroup, ManHUD.HUDElementType excludedElementType)
	{
		if (!m_ExclusiveHUDGroups.TryGetValue((int)hudGroup, out var value) || value == null)
		{
			return;
		}
		for (int i = 0; i < value.Count; i++)
		{
			TypeAction typeAction = value[i];
			if (typeAction.elemType == excludedElementType)
			{
				continue;
			}
			switch (typeAction.showAction)
			{
			case ShowAction.Show:
				if (IsHudElementVisible(typeAction.elemType))
				{
					HideHudElement(typeAction.elemType);
				}
				break;
			case ShowAction.Expand:
				if (IsHudElementExpanded(typeAction.elemType))
				{
					CollapseHudElement(typeAction.elemType);
				}
				break;
			default:
				d.LogError("UIHUD.CloseElementsInExclusiveGroup unhandled showAction");
				break;
			}
		}
	}

	private void DoShowElement(ManHUD.HUDElementType hudElemType, UIHUDElement uiElement, object context)
	{
		uiElement.Show(context);
		UpdateExclusiveGroups(hudElemType, ShowAction.Show);
		Singleton.Manager<ManHUD>.inst.OnShowHUDElementEvent.Send(uiElement);
	}

	private void DoHideElement(ManHUD.HUDElementType hudElemType, UIHUDElement uiElement, object context)
	{
		uiElement.Hide(context);
		Singleton.Manager<ManHUD>.inst.OnHideHUDElementEvent.Send(uiElement);
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManHUD>.inst.SetCurrentSpawnHUD(this);
		SetGroupRule(ManHUD.HUDGroup.Main, ManHUD.GroupRules.ExclusiveVisibility, enabled: true);
		SetGroupRule(ManHUD.HUDGroup.GamepadQuickMenuHUDElements, ManHUD.GroupRules.ExclusiveVisibility, enabled: true);
		int count = EnumValuesIterator<ManHUD.HUDElementType>.Count;
		m_ObscuredData = new ObscuredData[count];
	}

	private void OnRecycle()
	{
		foreach (int key in m_HUDElements.Keys)
		{
			UIHUDElement uIHUDElement = m_HUDElements[key];
			if (uIHUDElement != null && uIHUDElement.IsShowing)
			{
				DoHideElement((ManHUD.HUDElementType)key, uIHUDElement, null);
			}
		}
		m_HUDElements.Clear();
		m_ExclusiveHUDGroups.Clear();
		m_ExclusiveHUDGroupReverseLookup.Clear();
		m_HUDGroupRules.Clear();
	}
}
