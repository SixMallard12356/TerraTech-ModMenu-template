public class uScript_AddMoney : uScriptLogic
{
	public bool Out => true;

	public void In(int amount)
	{
		Singleton.Manager<ManPlayer>.inst.AddMoney(amount);
	}
}
