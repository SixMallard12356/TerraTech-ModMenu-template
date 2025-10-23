#define UNITY_EDITOR
using UnityEngine;

public class FilterInfoOverlayData : WorldObjectOverlayData
{
	public override Vector3 GetTargetPosition(object context)
	{
		ModuleItemFilter moduleItemFilter = context as ModuleItemFilter;
		d.Assert(context == null || moduleItemFilter != null, "FilterInfoOverlayData - Failed to cast context to ModuleItemFilter");
		if (!moduleItemFilter)
		{
			return Vector3.zero;
		}
		return moduleItemFilter.block.trans.position;
	}

	public override bool ShouldDisplay(object context)
	{
		ModuleItemFilter moduleItemFilter = context as ModuleItemFilter;
		d.Assert(context == null || moduleItemFilter != null, "FilterInfoOverlayData - Failed to cast context to ModuleItemFilter");
		if (moduleItemFilter != null && moduleItemFilter.block.tank != null)
		{
			return !Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.FilterMenu);
		}
		return false;
	}
}
