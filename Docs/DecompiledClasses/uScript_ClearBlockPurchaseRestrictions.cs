public class uScript_ClearBlockPurchaseRestrictions : uScriptLogic
{
	public bool Out => true;

	public void In()
	{
		Singleton.Manager<ManPurchases>.inst.ClearRestrictedBlocks();
	}
}
