public class uScript_SetDangerMusic : uScriptLogic
{
	public bool Out => true;

	public void In(FactionSubTypes corp)
	{
		if (corp != FactionSubTypes.NULL)
		{
			Singleton.Manager<ManMusic>.inst.SetDanger(ManMusic.DangerContext.Circumstance.SetPiece, corp);
		}
	}
}
