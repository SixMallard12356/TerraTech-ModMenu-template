using UnityEngine;

public class uScript_GetStartPosition : uScriptLogic
{
	public bool Out => true;

	public PositionWithFacing In()
	{
		return new PositionWithFacing(Mode<ModeMain>.inst.StartPositionScene, Vector3.zero);
	}
}
