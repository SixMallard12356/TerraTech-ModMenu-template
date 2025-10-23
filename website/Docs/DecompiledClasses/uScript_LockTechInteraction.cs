using System.Linq;

[NodePath("TerraTech/Actions/Techs")]
[FriendlyName("Lock Tech Interaction")]
[NodeToolTip("Locks out interaction on all blocks of a tech: no right clicks on blocks to activate their function. (This does not prevent blocks from being grabbed/removed!)")]
public class uScript_LockTechInteraction : uScriptLogic
{
	public bool Out => true;

	public void In(Tank tech, [FriendlyName("excludedBlockTypes")] BlockTypes[] excludedBlocks, [DefaultValue(null)] TankBlock[] excludedUniqueBlocks = null)
	{
		if (tech == null)
		{
			return;
		}
		tech.visible.SetLockTimout(Visible.LockTimerTypes.Interactible);
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tech.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock block = enumerator.Current;
			if ((excludedUniqueBlocks == null || !excludedUniqueBlocks.Any((TankBlock r) => r.blockPoolID == block.blockPoolID)) && (excludedBlocks == null || !excludedBlocks.Contains(block.BlockType)))
			{
				block.visible.SetLockTimout(Visible.LockTimerTypes.Interactible);
			}
		}
	}
}
