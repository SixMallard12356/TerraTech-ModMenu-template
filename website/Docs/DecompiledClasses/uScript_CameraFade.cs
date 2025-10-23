[NodePath("TerraTech/Actions/Camera")]
[FriendlyName("uScript_CameraFade", "Fade the camera on or off")]
public class uScript_CameraFade : uScriptLogic
{
	public bool Out => true;

	public void In(bool enableFade)
	{
		if (enableFade)
		{
			Singleton.Manager<ManUI>.inst.FadeToBlack(3f, forceFront: true);
		}
		else
		{
			Singleton.Manager<ManUI>.inst.ClearFade(3f);
		}
	}
}
