using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UILayoutElementFromChildren : UIBehaviour, ILayoutElement
{
	[SerializeField]
	private bool m_MinWidth;

	[SerializeField]
	private bool m_MinHeight;

	[SerializeField]
	private bool m_PreferredWidth;

	[SerializeField]
	private bool m_PreferredHeight;

	[SerializeField]
	private bool m_FlexibleWidth;

	[SerializeField]
	private bool m_FlexibleHeight;

	[SerializeField]
	private bool m_LayoutPriority;

	private static List<ILayoutElement> s_LayoutElemCache = new List<ILayoutElement>();

	public float minWidth { get; private set; }

	public float preferredWidth { get; private set; }

	public float flexibleWidth { get; private set; }

	public float minHeight { get; private set; }

	public float preferredHeight { get; private set; }

	public float flexibleHeight { get; private set; }

	public int layoutPriority { get; private set; }

	public void ForceRefresh()
	{
		CalculateLayoutInputHorizontal();
		CalculateLayoutInputVertical();
		LayoutRebuilder.MarkLayoutForRebuild(base.transform as RectTransform);
	}

	public void CalculateLayoutInputHorizontal()
	{
		minWidth = 0f;
		preferredWidth = 0f;
		flexibleWidth = 0f;
		layoutPriority = 0;
		int num = int.MinValue;
		foreach (Transform item in base.transform)
		{
			item.GetComponents(s_LayoutElemCache);
			foreach (ILayoutElement item2 in s_LayoutElemCache)
			{
				int num2 = item2.layoutPriority;
				if (num2 >= num)
				{
					num = num2;
					if (m_MinWidth)
					{
						minWidth = Mathf.Max(minWidth, item2.minWidth);
					}
					if (m_PreferredWidth)
					{
						preferredWidth = Mathf.Max(preferredWidth, item2.preferredWidth);
					}
					if (m_FlexibleWidth)
					{
						flexibleWidth = Mathf.Max(flexibleWidth, item2.flexibleWidth);
					}
					if (m_LayoutPriority)
					{
						layoutPriority = Mathf.Max(layoutPriority, item2.layoutPriority);
					}
				}
			}
		}
		s_LayoutElemCache.Clear();
	}

	public void CalculateLayoutInputVertical()
	{
		minHeight = 0f;
		preferredHeight = 0f;
		flexibleHeight = 0f;
		layoutPriority = 0;
		int num = int.MinValue;
		foreach (Transform item in base.transform)
		{
			item.GetComponents(s_LayoutElemCache);
			foreach (ILayoutElement item2 in s_LayoutElemCache)
			{
				int num2 = item2.layoutPriority;
				if (num2 >= num)
				{
					num = num2;
					if (m_MinHeight)
					{
						minHeight = Mathf.Max(minHeight, item2.minHeight);
					}
					if (m_PreferredHeight)
					{
						preferredHeight = Mathf.Max(preferredHeight, item2.preferredHeight);
					}
					if (m_FlexibleHeight)
					{
						flexibleHeight = Mathf.Max(flexibleHeight, item2.flexibleHeight);
					}
					if (m_LayoutPriority)
					{
						layoutPriority = Mathf.Max(layoutPriority, item2.layoutPriority);
					}
				}
			}
		}
		s_LayoutElemCache.Clear();
	}
}
