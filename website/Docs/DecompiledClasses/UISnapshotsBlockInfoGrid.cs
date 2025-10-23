#define UNITY_EDITOR
using System;
using System.Collections.Generic;

public class UISnapshotsBlockInfoGrid : UIItemSelectGrid
{
	public Action<List<ItemTypeInfo>> ItemFilterCallback { get; set; }

	protected override void GetFilteredItemTypes(List<ItemTypeInfo> itemTypes)
	{
		if (ItemFilterCallback != null)
		{
			ItemFilterCallback(itemTypes);
		}
		else
		{
			d.LogErrorFormat("UISnapshotBlockInfoGrid - ItemFilterCallback has not be set up, grid will not be populated");
		}
	}
}
