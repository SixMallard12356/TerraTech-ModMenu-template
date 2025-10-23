#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UICategoryToggles
{
	[SerializeField]
	private Toggle[] m_Toggles;

	[SerializeField]
	private Toggle m_AllToggle;

	public EventNoParams OnChanged;

	private UITogglesController m_Controller = new UITogglesController();

	public List<int> Selection => m_Controller.Selection;

	public int NumToggles => m_Toggles.Length;

	public void SetupAsBlockCategoryToggles(CategoryOrder order)
	{
		m_Controller.OnChanged.Subscribe(OnControllerChanged);
		List<BlockCategories> list = new List<BlockCategories>(EnumValuesIterator<BlockCategories>.Values);
		list.Sort(delegate(BlockCategories itemA, BlockCategories itemB)
		{
			order.Lookup(itemA, out var order2);
			order.Lookup(itemB, out var order3);
			return order2.CompareTo(order3);
		});
		for (int num = 0; num < list.Count; num++)
		{
			BlockCategories blockCategories = list[num];
			if (blockCategories != BlockCategories.Null)
			{
				int num2 = (int)(blockCategories - 1);
				if (num2 >= 0 && num2 < m_Toggles.Length)
				{
					int corpIndex = (int)blockCategories;
					m_Controller.AddToggle(m_Toggles[num2], corpIndex);
				}
				else
				{
					d.LogWarningFormat("Unable to find toggle to represent BlockCategory {0}", blockCategories);
				}
			}
		}
		m_Controller.SetAllToggle(m_AllToggle);
	}

	public void SetupAsComponentTierToggles()
	{
		m_Controller.OnChanged.Subscribe(OnControllerChanged);
		ComponentTier[] values = EnumValuesIterator<ComponentTier>.Values;
		foreach (ComponentTier componentTier in values)
		{
			if (componentTier != ComponentTier.Null)
			{
				int num = (int)(componentTier - 1);
				if (num >= 0 && num < m_Toggles.Length)
				{
					int corpIndex = (int)componentTier;
					m_Controller.AddToggle(m_Toggles[num], corpIndex);
				}
				else
				{
					d.LogWarningFormat("Unable to find toggle to represent ComponentTier {0}", componentTier);
				}
			}
		}
		m_Controller.SetAllToggle(m_AllToggle);
	}

	public void TakeDown()
	{
		m_Controller.Clear();
		m_Controller.OnChanged.Unsubscribe(OnControllerChanged);
	}

	public void ToggleAllOn()
	{
		m_Controller.SetAllToggleSelected(selected: true);
	}

	public void ToggleAllOff()
	{
		m_Controller.SetAllToggleSelected(selected: false);
	}

	public void SetToggleSelected(int selectionIndex, bool selected)
	{
		m_Controller.SetToggleSelected(selectionIndex, selected);
	}

	public ToggleWrapper GetToggle(int selectionIndex)
	{
		return m_Controller.GetToggle(selectionIndex);
	}

	public ToggleWrapper GetAllToggle()
	{
		return m_Controller.GetAllToggle();
	}

	public void SetToggleInteractable(int selectionIndex, bool enabled)
	{
		m_Controller.SetToggleInteractable(selectionIndex, enabled);
	}

	public void CycleSingleToggle(bool forward = true)
	{
		m_Controller.CycleSingleToggle(forward);
	}

	private void OnControllerChanged()
	{
		OnChanged.Send();
	}
}
