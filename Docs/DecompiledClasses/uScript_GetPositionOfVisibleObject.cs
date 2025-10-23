using UnityEngine;

public class uScript_GetPositionOfVisibleObject : uScriptLogic
{
	public bool Out => true;

	public Vector3 In(object visibleObject)
	{
		Vector3 result = Vector3.zero;
		if (visibleObject != null)
		{
			result = Singleton.Manager<ManVisible>.inst.GetVisibleFromObject(visibleObject).centrePosition;
		}
		return result;
	}
}
