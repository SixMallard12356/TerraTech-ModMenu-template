[NodePath("TerraTech/Actions/Audio")]
[FriendlyName("Play Misc SFX", "Plays misc sound effect at a location")]
public class uScript_PlayMiscSFX : uScriptLogic
{
	public bool Out => true;

	public void In(ManSFX.MiscSfxType miscSFXType)
	{
		if (miscSFXType != ManSFX.MiscSfxType.None)
		{
			Singleton.Manager<ManSFX>.inst.PlayMiscSFX(miscSFXType, Singleton.cameraTrans.position);
		}
	}
}
