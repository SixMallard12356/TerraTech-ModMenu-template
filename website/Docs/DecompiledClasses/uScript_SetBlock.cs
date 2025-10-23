[FriendlyName("Set Block", "Set a block from another block. Just reassigning the reference for uScript working purposes. Similar to uScriptAct_SetGameObject.")]
[NodePath("TerraTech/Actions/Blocks")]
public class uScript_SetBlock : uScriptLogic
{
	public bool Out => true;

	public void In([FriendlyName("Value", "The variable you wish to use to set the target's value.")][DefaultValue(null)] TankBlock Value, [FriendlyName("Target", "The Target variable you wish to set.")] out TankBlock TargetGameObject)
	{
		TargetGameObject = Value;
	}
}
