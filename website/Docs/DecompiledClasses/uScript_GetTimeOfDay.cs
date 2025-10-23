[FriendlyName("Get Time of Day", "Return the Time of Day in hours")]
[NodePath("TerraTech/Environment")]
public class uScript_GetTimeOfDay : uScriptLogic
{
	public bool Out => true;

	public int In()
	{
		return Singleton.Manager<ManTimeOfDay>.inst.TimeOfDay;
	}
}
