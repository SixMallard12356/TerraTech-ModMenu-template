[NodePath("TerraTech/Actions/Camera")]
[FriendlyName("Get Tank Camera Distance Max", "Get current tank camera max zoom amount")]
public class uScript_GetTankCameraDistanceMax : uScriptLogic
{
	public bool Out => true;

	public float In()
	{
		return TankCamera.inst.GetDistanceMax();
	}
}
