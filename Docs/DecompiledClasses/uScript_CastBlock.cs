[NodePath("TerraTech/Actions/Blocks")]
[FriendlyName("Cast External param to TankBlock", "Casts the TankBlock passed in from an External param to a strongly typed TankBlock parameter")]
public class uScript_CastBlock : uScriptLogic
{
	public bool Out => true;

	public void In(TankBlock block, out TankBlock outBlock)
	{
		outBlock = block;
	}
}
