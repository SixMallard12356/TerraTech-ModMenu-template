using System.Collections.Generic;
using UnityEngine;

public class UIFilterCategorySubmenu : RadialMenuSubmenu
{
	private struct CategoryChoice
	{
		public QueryableSelectable selectable;

		public ChunkCategory category;
	}

	[EnumArray(typeof(ChunkCategory))]
	[SerializeField]
	private QueryableSelectable[] m_ChunkCategorySelectables;

	public Event<ChunkCategory> OnCategorySelected;

	private List<CategoryChoice> m_CategoryObjects = new List<CategoryChoice>();

	private int m_LastSelectedIndex;

	public QueryableSelectable GetChunkCategoryButton(ChunkCategory category)
	{
		QueryableSelectable result = null;
		for (int i = 0; i < m_CategoryObjects.Count; i++)
		{
			if (m_CategoryObjects[i].category == category)
			{
				result = m_CategoryObjects[i].selectable;
				break;
			}
		}
		return result;
	}

	private void CheckForSelectedCategory()
	{
		ChunkCategory chunkCategory = ChunkCategory.Null;
		for (int i = 0; i < m_CategoryObjects.Count; i++)
		{
			if (m_CategoryObjects[i].selectable.IsHighlighted)
			{
				chunkCategory = m_CategoryObjects[i].category;
				break;
			}
		}
		if (chunkCategory != ChunkCategory.Null)
		{
			OnCategorySelected.Send(chunkCategory);
		}
	}

	protected override void OnOpen()
	{
		m_LastSelectedIndex = -1;
		if (base.m_Controller.IsGamePad())
		{
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(m_CategoryObjects[0].selectable.gameObject);
		}
	}

	protected override void OnClose()
	{
		Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(m_CategoryObjects[0].selectable.gameObject);
	}

	protected override void OnUpdate()
	{
		if (!base.m_Controller.IsGamePad())
		{
			return;
		}
		int num = -1;
		for (int i = 0; i < m_CategoryObjects.Count; i++)
		{
			if (m_CategoryObjects[i].selectable.IsHighlighted)
			{
				num = i;
				break;
			}
		}
		if (num != m_LastSelectedIndex)
		{
			if (m_LastSelectedIndex != -1)
			{
				m_CategoryObjects[m_LastSelectedIndex].selectable.GetComponent<TooltipComponent>().OnPointerExit(null);
			}
			if (num != -1)
			{
				m_CategoryObjects[num].selectable.GetComponent<TooltipComponent>().OnPointerEnter(null);
			}
			m_LastSelectedIndex = num;
		}
	}

	protected override void OnOptionSelected(QueryableSelectable option)
	{
		CheckForSelectedCategory();
		base.OnOptionSelected(option);
	}

	private void OnPool()
	{
		for (int i = 0; i < m_ChunkCategorySelectables.Length; i++)
		{
			if (m_ChunkCategorySelectables[i] != null)
			{
				CategoryChoice item = new CategoryChoice
				{
					selectable = m_ChunkCategorySelectables[i],
					category = ((i > 0) ? ((ChunkCategory)(1 << i - 1)) : ChunkCategory.Null)
				};
				m_CategoryObjects.Add(item);
			}
		}
	}
}
