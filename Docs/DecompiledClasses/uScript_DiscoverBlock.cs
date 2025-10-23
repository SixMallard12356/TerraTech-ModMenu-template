public class uScript_DiscoverBlock : uScriptLogic
{
	public bool Out => true;

	public void In(BlockTypes blockType)
	{
		Singleton.Manager<ManLicenses>.inst.DiscoverBlock(blockType);
	}
}
