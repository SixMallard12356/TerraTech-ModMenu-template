public class uScript_SetMissionsVisibleInHud : uScriptLogic
{
	public bool Out => true;

	public void In(bool visible)
	{
		Singleton.Manager<ManEncounter>.inst.SetEncountersVisibleInHud(visible);
	}
}
