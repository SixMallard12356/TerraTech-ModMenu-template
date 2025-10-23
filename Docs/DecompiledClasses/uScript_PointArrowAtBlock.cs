#define UNITY_EDITOR
using UnityEngine;

public class uScript_PointArrowAtBlock : uScriptLogic
{
	public bool Out => true;

	public void In(TankBlock block, float timeToShowFor, Vector3 offset = default(Vector3))
	{
		Visible visible = null;
		if (block != null)
		{
			visible = Singleton.Manager<ManVisible>.inst.GetVisibleFromObject(block);
		}
		if (visible != null)
		{
			UIBouncingArrow.BouncingArrowContext bouncingArrowContext = new UIBouncingArrow.BouncingArrowContext
			{
				targetTransform = visible.trans,
				targetOffset = offset,
				forTime = timeToShowFor
			};
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.BouncingArrow, bouncingArrowContext);
		}
		else
		{
			d.LogError("uScript_PointArrowAtBlock - Failed to find visible for object '" + ((block != null) ? block.ToString() : "null object") + "'");
		}
	}
}
