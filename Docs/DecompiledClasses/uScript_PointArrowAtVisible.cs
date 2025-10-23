#define UNITY_EDITOR
using UnityEngine;

public class uScript_PointArrowAtVisible : uScriptLogic
{
	public bool Out => true;

	public void In(object targetObject, float timeToShowFor, Vector3 offset = default(Vector3))
	{
		Visible visible = null;
		if (targetObject != null)
		{
			visible = Singleton.Manager<ManVisible>.inst.GetVisibleFromObject(targetObject);
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
				d.LogError("uScript_PointArrowAtVisible - Failed to find visible for object '" + targetObject.ToString() + "'");
			}
		}
		else
		{
			d.LogError("uScript_PointArrowAtVisible - Null object passed in!");
		}
	}
}
