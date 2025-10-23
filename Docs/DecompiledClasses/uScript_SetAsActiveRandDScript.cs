using UnityEngine;

public class uScript_SetAsActiveRandDScript : uScriptLogic
{
	public bool Out => true;

	public void In(GameObject owner)
	{
		Singleton.Manager<ManRandDScript>.inst.ActiveScriptObject = owner;
	}
}
