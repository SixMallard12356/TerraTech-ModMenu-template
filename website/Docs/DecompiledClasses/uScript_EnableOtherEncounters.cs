[NodePath("TerraTech/Actions/Encounters")]
[FriendlyName("uScript_EnableOtherEncounters", "Enable other, Non Core, Encounters")]
public class uScript_EnableOtherEncounters : uScriptLogic
{
	public bool Out => true;

	public void In()
	{
		Singleton.Manager<ManProgression>.inst.EnableOtherEncounters();
	}
}
