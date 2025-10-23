public class uScript_RestrictBlockPurchase : uScriptLogic
{
	public bool Out => true;

	public void In(BlockTypes[] allowedBlockTypes)
	{
		Singleton.Manager<ManPurchases>.inst.RestrictBlockPurchase(allowedBlockTypes);
	}
}
