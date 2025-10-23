#define UNITY_EDITOR
[NodeToolTip("Prevent block from being attached to another tech")]
public class uScript_LockBlockAttach : uScriptLogic
{
	public bool Out => true;

	public void In(TankBlock block)
	{
		if (block != null)
		{
			block.LockBlockAttach();
		}
		else
		{
			d.LogError("uScript_LockBlockAttach - NULL parameter passed in for block");
		}
	}
}
