public class uScript_SetPopulationActive : uScriptLogic
{
	public bool Out => true;

	public void SetActive(bool active)
	{
		Singleton.Manager<ManPop>.inst.SetSpawningEnabled(active);
	}
}
