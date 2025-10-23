public class uScript_GetBlockPrice : uScriptLogic
{
	public bool Out => true;

	public int In(BlockTypes blockType)
	{
		return Singleton.Manager<RecipeManager>.inst.GetBlockBuyPrice(blockType);
	}
}
