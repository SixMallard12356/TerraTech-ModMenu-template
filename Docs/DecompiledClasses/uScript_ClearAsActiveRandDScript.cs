using UnityEngine;

public class uScript_ClearAsActiveRandDScript : uScriptLogic
{
	public bool Out => true;

	public void In(GameObject owner)
	{
		if (Singleton.Manager<ManRandDScript>.inst.ActiveScriptObject == owner)
		{
			Singleton.Manager<ManRandDScript>.inst.ActiveScriptObject = null;
		}
	}
}
