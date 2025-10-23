using UnityEngine;

public class uScript_GetStartPositionAsVector3 : uScriptLogic
{
	public bool Out => true;

	public Vector3 In()
	{
		Vector3 scenePos = Mode<ModeMain>.inst.StartPositionScene + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
		return Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos);
	}
}
