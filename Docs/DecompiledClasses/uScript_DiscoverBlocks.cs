public class uScript_DiscoverBlocks : uScriptLogic
{
	public bool Out => true;

	public void In(BlockTypes[] blockTypes)
	{
		for (int i = 0; i < blockTypes.Length; i++)
		{
			Singleton.Manager<ManLicenses>.inst.DiscoverBlock(blockTypes[i]);
		}
	}
}
