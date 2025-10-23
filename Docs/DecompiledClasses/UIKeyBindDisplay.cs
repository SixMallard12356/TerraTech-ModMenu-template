using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyBindDisplay : MonoBehaviour
{
	public Text m_TextField;

	public int m_Index;

	[Tooltip("See RewiredActions.cs for action IDs")]
	public int m_ActionID;

	public Pole m_AxisContribution;

	public Event<UIKeyBindDisplay> OnKeyBindChanged;

	private bool m_Disabled;

	private bool m_Initialised;

	protected ActionElementMap m_AssignedActionMap;

	public bool IsAssignedToAKey => IsValidElementMap(m_AssignedActionMap);

	public void Setup()
	{
		Init();
		SetButtonText();
	}

	public void Refresh()
	{
		m_AssignedActionMap = GetAssignedMap();
		SetButtonText();
	}

	protected virtual void OnControlMapAssignedCallback(int index, int actionId, Pole axisContribution, ActionElementMap newMap)
	{
		if (index == m_Index && actionId == m_ActionID && axisContribution == m_AxisContribution)
		{
			if (newMap != m_AssignedActionMap)
			{
				Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.Select);
				m_AssignedActionMap = newMap;
				SetButtonText();
				OnKeyBindChanged.Send(this);
			}
			else
			{
				SetButtonText();
			}
		}
	}

	private void OnControlMapRemovedCallback(ActionElementMap map)
	{
		if (map == m_AssignedActionMap && map != null)
		{
			m_AssignedActionMap = null;
			SetButtonText();
			OnKeyBindChanged.Send(this);
		}
	}

	private ActionElementMap GetAssignedMap()
	{
		int num = 0;
		foreach (ControllerMap item in (IEnumerable<ControllerMap>)Singleton.Manager<ManInput>.inst.GetAllRebindableMaps())
		{
			foreach (ActionElementMap allMap in item.AllMaps)
			{
				if (allMap.actionId == m_ActionID && allMap.axisContribution == m_AxisContribution && IsValidElementMap(allMap))
				{
					if (num == m_Index)
					{
						return allMap;
					}
					num++;
				}
			}
		}
		return null;
	}

	private static bool IsValidElementMap(ActionElementMap map)
	{
		if (map == null)
		{
			return false;
		}
		if (map.elementIdentifierId < 0)
		{
			return false;
		}
		if (map.controllerMap.controllerType == ControllerType.Keyboard && map.keyCode == KeyCode.None)
		{
			return false;
		}
		return true;
	}

	protected void SetButtonText()
	{
		if (!m_Disabled)
		{
			string text = null;
			if (m_AssignedActionMap != null)
			{
				text = Singleton.Manager<Localisation>.inst.ActionElementMapToString(m_AssignedActionMap);
			}
			m_TextField.text = (string.IsNullOrEmpty(text) ? " " : text);
		}
	}

	private void Init()
	{
		if (!m_Initialised)
		{
			Button component = GetComponent<Button>();
			if ((bool)component && !component.enabled)
			{
				m_Disabled = true;
			}
			Singleton.Manager<ManInput>.inst.OnControlMapRemoved.Subscribe(OnControlMapRemovedCallback);
			Singleton.Manager<ManInput>.inst.OnControlMapAssigned.Subscribe(OnControlMapAssignedCallback);
			m_Initialised = true;
		}
		m_AssignedActionMap = GetAssignedMap();
	}

	private void OnSpawn()
	{
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Subscribe(SetButtonText);
	}

	private void OnRecycle()
	{
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Unsubscribe(SetButtonText);
	}
}
