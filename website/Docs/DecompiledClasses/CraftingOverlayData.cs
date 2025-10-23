#define UNITY_EDITOR
using UnityEngine;

public class CraftingOverlayData : WorldObjectOverlayData
{
	public override Vector3 GetTargetPosition(object context)
	{
		ModuleItemConsume moduleItemConsume = context as ModuleItemConsume;
		d.Assert(context == null || moduleItemConsume != null, "CraftingOverlayData - Failed to cast context to ModuleItemConsume");
		if (!moduleItemConsume)
		{
			return Vector3.zero;
		}
		return moduleItemConsume.block.trans.position;
	}

	public override bool ShouldDisplay(object context)
	{
		ModuleItemConsume moduleItemConsume = context as ModuleItemConsume;
		d.Assert(context == null || moduleItemConsume != null, "CraftingOverlayData - Failed to cast context to ModuleItemConsume");
		if (moduleItemConsume != null && moduleItemConsume.block.tank != null)
		{
			return moduleItemConsume.Recipe != null;
		}
		return false;
	}
}
