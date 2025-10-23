[NodeToolTip("Locks an individual block's specified functions")]
[NodePath("TerraTech/Actions/Blocks")]
[FriendlyName("Lock Block Functionality")]
public class uScript_LockBlock : uScriptLogic
{
	public bool Out => true;

	public void In(TankBlock block, Visible.LockTimerTypes functionalityToLock)
	{
		if (block != null)
		{
			block.visible.SetLockTimout(functionalityToLock);
		}
	}
}
