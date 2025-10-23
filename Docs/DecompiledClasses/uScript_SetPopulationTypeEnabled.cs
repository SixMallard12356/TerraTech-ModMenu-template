public class uScript_SetPopulationTypeEnabled : uScriptLogic
{
	public bool Out => true;

	public void SetEnabled(string popTypeName, bool active)
	{
		Singleton.Manager<ManPop>.inst.SetPopTypeEnabled(popTypeName, active);
	}
}
