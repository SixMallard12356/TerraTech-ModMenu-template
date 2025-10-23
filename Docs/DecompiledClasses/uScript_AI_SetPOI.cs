using UnityEngine;

public class uScript_AI_SetPOI : uScriptLogic
{
	public bool Out => true;

	public void In(Tank tank, bool usePOI, Vector3 position, float distance)
	{
		if ((bool)tank)
		{
			tank.AI.SetPOI(usePOI, position, distance);
		}
	}
}
