[FriendlyName("uScript_SetTechExplodeDetachingBlocks", "Set tech blocks falling off to explode instead of standard detach")]
[NodePath("TerraTech/Actions/Techs")]
public class uScript_SetTechExplodeDetachingBlocks : uScriptLogic
{
	public bool Out => true;

	public void In(Tank tech, bool explodeDetachingBlocks, float explodeDelay)
	{
		if (tech != null)
		{
			tech.ShouldExplodeDetachingBlocks = explodeDetachingBlocks;
			tech.ExplodeDetachingBlocksDelay = explodeDelay;
		}
	}
}
