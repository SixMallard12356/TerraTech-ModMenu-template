public class uScript_GetCurrentMoneyEarned : uScriptLogic
{
	public bool Out => true;

	public int In()
	{
		return Singleton.Manager<ManPlayer>.inst.GetCurrentMoney();
	}
}
