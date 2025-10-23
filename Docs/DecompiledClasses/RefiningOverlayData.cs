#define UNITY_EDITOR
using UnityEngine;

public class RefiningOverlayData : WorldObjectOverlayData
{
	public override Vector3 GetTargetPosition(object context)
	{
		ModuleItemConsume moduleItemConsume = context as ModuleItemConsume;
		d.Assert(context == null || moduleItemConsume != null, "RefiningOverlayData - Failed to cast context to ModuleItemConsume");
		if (!moduleItemConsume)
		{
			return Vector3.zero;
		}
		return moduleItemConsume.block.trans.position;
	}

	public override bool ShouldDisplay(object context)
	{
		ModuleItemConsume moduleItemConsume = context as ModuleItemConsume;
		d.Assert(context == null || moduleItemConsume != null, "RefiningOverlayData - Failed to cast context to ModuleItemConsume");
		if (moduleItemConsume != null && moduleItemConsume.block.tank != null && moduleItemConsume.CanHonourRequests)
		{
			return moduleItemConsume.HasWantedItems;
		}
		return false;
	}
}
