[FriendlyName("uScript_GetNumInvadersDestroyed", "Get number of invaders we have destroyed to this point")]
[NodePath("TerraTech/Progression/Stats")]
public class uScript_GetNumInvadersDestroyed : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("In", "Get the number of invaders destroyed")]
	public int In()
	{
		return Singleton.Manager<ManStats>.inst.GetTotalNumInvadersDestroyed();
	}
}
