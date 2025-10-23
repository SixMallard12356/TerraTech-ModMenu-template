using System.Collections.Generic;

internal class ItemPipeEnd : ItemPipe
{
	private ItemBuildContext m_Context = new ItemBuildContext();

	public void TryBuildNeededItems(TechHolders holders, List<ItemTypeInfo> neededItems, HashSet<ItemTypeInfo> missingItems)
	{
		ItemPipe.s_ItemCollector.TankHolders = holders;
		PrioritySortChildrenRecursive();
		if (!TryDoBuildPass(Pass.BuildAllSimultaneously, neededItems, missingItems))
		{
			missingItems.Clear();
			TryDoBuildPass(Pass.BuildWhatPossible, neededItems, missingItems);
		}
		ItemPipe.s_ItemCollector.TankHolders = null;
	}

	private bool TryDoBuildPass(Pass pass, List<ItemTypeInfo> neededItems, HashSet<ItemTypeInfo> missingItems)
	{
		bool flag = true;
		for (int i = 0; i < neededItems.Count; i++)
		{
			ItemTypeInfo item = neededItems[i];
			bool hasMissingInputs = false;
			bool flag2 = TrySourceItemLocally(item, m_Context);
			if (!flag2)
			{
				foreach (ItemPipe child in m_Children)
				{
					if (child.TryBuildItem(item, m_Context, pass, out hasMissingInputs))
					{
						flag2 = true;
						break;
					}
				}
			}
			if (!flag2 || hasMissingInputs)
			{
				missingItems.Add(item);
			}
			if (!flag2 && pass == Pass.BuildAllSimultaneously)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			m_Context.ExecuteBuilding();
		}
		else
		{
			for (int j = 0; j < neededItems.Count; j++)
			{
				missingItems.Add(neededItems[j]);
			}
		}
		m_Context.Clear();
		return flag;
	}

	public override bool TryBuildItem(ItemTypeInfo item, ItemBuildContext context, Pass pass, out bool hasMissingInputs)
	{
		hasMissingInputs = false;
		return false;
	}

	public override void DetermineBuildableTypes()
	{
		foreach (ItemPipe child in m_Children)
		{
			child.DetermineBuildableTypes();
			HashSet<ItemTypeInfo>.Enumerator buildableTypes = child.BuildableTypes;
			while (buildableTypes.MoveNext())
			{
				AddBuildableType(buildableTypes.Current);
			}
		}
	}
}
