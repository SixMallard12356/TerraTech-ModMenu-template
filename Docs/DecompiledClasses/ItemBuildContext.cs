using System.Collections.Generic;

public class ItemBuildContext
{
	public struct Marker
	{
		public int numUsedItems;

		public int numBuildRecs;
	}

	public struct ConversionRec
	{
		public RecipeTable.Recipe recipe;

		public ItemSearchConverter converter;

		public Bitfield<int> inputWarnings;
	}

	private List<OfferedItem> usedItems = new List<OfferedItem>();

	private List<ConversionRec> buildRecs = new List<ConversionRec>();

	public Marker CreateMarker()
	{
		return new Marker
		{
			numBuildRecs = buildRecs.Count,
			numUsedItems = usedItems.Count
		};
	}

	public void RestoreMarker(Marker m)
	{
		if (usedItems.Count - m.numUsedItems > 0)
		{
			usedItems.RemoveRange(m.numUsedItems, usedItems.Count - m.numUsedItems);
		}
		if (buildRecs.Count - m.numBuildRecs > 0)
		{
			buildRecs.RemoveRange(m.numBuildRecs, buildRecs.Count - m.numBuildRecs);
		}
	}

	public bool IsItemUsed(OfferedItem item)
	{
		bool result = false;
		for (int i = 0; i < usedItems.Count; i++)
		{
			if (usedItems[i].IsSameObjectAs(item))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public bool IsConverterUsed(ItemSearchConverter converter)
	{
		bool result = false;
		for (int i = 0; i < buildRecs.Count; i++)
		{
			if (buildRecs[i].converter == converter)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public void AddUsedItem(OfferedItem item)
	{
		usedItems.Add(item);
	}

	public void AddBuildRec(ConversionRec rec)
	{
		buildRecs.Add(rec);
	}

	public void Clear()
	{
		usedItems.Clear();
		buildRecs.Clear();
	}

	public void ExecuteBuilding()
	{
		for (int i = 0; i < usedItems.Count; i++)
		{
			usedItems[i].SetRequested();
		}
		for (int j = 0; j < buildRecs.Count; j++)
		{
			ConversionRec conversionRec = buildRecs[j];
			conversionRec.converter.MakeRecipeRequest(conversionRec.recipe, conversionRec.inputWarnings);
		}
	}
}
