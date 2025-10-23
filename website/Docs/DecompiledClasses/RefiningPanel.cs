#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class RefiningPanel : OverlayPanel
{
	[SerializeField]
	private UIItemDisplay m_OutputPrefab;

	[SerializeField]
	private Transform m_OutputItemsBox;

	private ModuleItemConsume m_Consumer;

	private List<Transform> m_Elements = new List<Transform>();

	private HashSet<ItemTypeInfo> m_RequestedOutputItems = new HashSet<ItemTypeInfo>();

	public override void SetContext(object context)
	{
		m_Consumer = context as ModuleItemConsume;
		d.Assert(context == null || m_Consumer != null, "RefiningPanel - Failed to cast context to ModuleItemConsume");
		SetupPanel();
	}

	private void SetupPanel()
	{
		RecycleElements();
		m_RequestedOutputItems.Clear();
		if (!(m_Consumer != null) || !m_Consumer.CanHonourRequests || !m_Consumer.HasWantedItems || !(m_OutputPrefab != null))
		{
			return;
		}
		foreach (ItemTypeInfo wantedItem in m_Consumer.WantedItems)
		{
			Transform parent = m_OutputItemsBox ?? base.transform;
			AddIngredientToDisplay(wantedItem, string.Empty, m_OutputPrefab, parent);
			m_RequestedOutputItems.Add(wantedItem);
		}
	}

	private void AddIngredientToDisplay(ItemTypeInfo itemSpec, string countStr, UIItemDisplay itemPrefab, Transform parent)
	{
		Transform transform = itemPrefab.transform.Spawn();
		m_Elements.Add(transform);
		transform.GetComponent<UIItemDisplay>().Setup(itemSpec, countStr);
		transform.SetParent(parent, worldPositionStays: false);
	}

	private void RecycleElements()
	{
		for (int i = 0; i < m_Elements.Count; i++)
		{
			m_Elements[i].SetParent(null, worldPositionStays: false);
			m_Elements[i].Recycle();
		}
		m_Elements.Clear();
	}

	private void Update()
	{
		if (!m_Consumer)
		{
			return;
		}
		bool flag = (m_RequestedOutputItems.Count > 0 && !m_Consumer.HasWantedItems) || m_RequestedOutputItems.Count != m_Consumer.WantedItems.Count;
		if (!flag)
		{
			foreach (ItemTypeInfo wantedItem in m_Consumer.WantedItems)
			{
				if (!m_RequestedOutputItems.Contains(wantedItem))
				{
					flag = true;
					break;
				}
			}
		}
		if (flag)
		{
			SetupPanel();
		}
	}
}
