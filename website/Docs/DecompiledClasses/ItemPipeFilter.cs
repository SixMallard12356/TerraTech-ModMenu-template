using System.Collections.Generic;

internal class ItemPipeFilter : ItemPipe
{
	private ItemSearchFilter m_Filter;

	public void Setup(ItemSearchFilter filter, ItemPipe parent)
	{
		m_Filter = filter;
		SetParent(parent);
	}

	public override void OfferItem(OfferedItem item)
	{
		if (m_Filter.AcceptsType(item.GetTypeInfo()))
		{
			base.OfferItem(item);
		}
	}

	public override bool TryBuildItem(ItemTypeInfo item, ItemBuildContext context, Pass pass, out bool hasMissingInputs)
	{
		bool flag = false;
		hasMissingInputs = false;
		if (m_Filter.AcceptsType(item))
		{
			flag = TrySourceItemLocally(item, context);
			if (!flag)
			{
				foreach (ItemPipe child in m_Children)
				{
					if (child.TryBuildItem(item, context, pass, out hasMissingInputs))
					{
						flag = true;
						break;
					}
				}
			}
		}
		return flag;
	}

	protected override void Destroy()
	{
		base.Destroy();
		m_Filter = null;
	}

	public override void DetermineBuildableTypes()
	{
		foreach (ItemPipe child in m_Children)
		{
			child.DetermineBuildableTypes();
			HashSet<ItemTypeInfo>.Enumerator buildableTypes = child.BuildableTypes;
			while (buildableTypes.MoveNext())
			{
				if (m_Filter.AcceptsType(buildableTypes.Current))
				{
					AddBuildableType(buildableTypes.Current);
				}
			}
		}
	}
}
