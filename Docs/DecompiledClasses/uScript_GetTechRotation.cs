#define UNITY_EDITOR
using UnityEngine;

public class uScript_GetTechRotation : uScriptLogic
{
	public bool Out => true;

	public Quaternion In(Tank tech)
	{
		Quaternion result = Quaternion.identity;
		if (tech != null)
		{
			result = tech.transform.rotation;
		}
		else
		{
			d.Log("uScript_GetTechRotation - tech is null");
		}
		return result;
	}
}
