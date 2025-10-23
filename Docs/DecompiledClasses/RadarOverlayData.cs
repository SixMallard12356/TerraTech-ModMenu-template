#define UNITY_EDITOR
using UnityEngine;

public class RadarOverlayData : WorldObjectOverlayData
{
	public override Vector3 GetTargetPosition(object context)
	{
		ModuleRadar moduleRadar = context as ModuleRadar;
		d.Assert(context == null || moduleRadar != null, "RadarOverlayData - Failed to cast context to ModuleRadar");
		if (!moduleRadar)
		{
			return Vector3.zero;
		}
		return moduleRadar.block.trans.position;
	}

	public override bool ShouldDisplay(object context)
	{
		ModuleRadar moduleRadar = context as ModuleRadar;
		d.Assert(context == null || moduleRadar != null, "RadarOverlayData - Failed to cast context to ModuleRadar");
		if (moduleRadar != null && moduleRadar.block.tank != null && moduleRadar.ScansResources)
		{
			return moduleRadar.ResourceType != ChunkTypes.Null;
		}
		return false;
	}
}
