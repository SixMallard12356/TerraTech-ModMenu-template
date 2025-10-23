#define UNITY_EDITOR
[NodePath("TerraTech/Actions/Tutorial")]
public class uScript_OverrideRotationGroup : uScriptLogic
{
	public bool Out => true;

	public void In(TankBlock theBlock, BlockRotationTable.RotationGroup rotation)
	{
		if (theBlock != null && rotation != null)
		{
			Singleton.Manager<ManTechBuilder>.inst.OverrideBlockRotationGroup(theBlock, rotation);
		}
		else
		{
			d.LogError("ERROR: uScript_OverrideRotationGroup given null block or rotation");
		}
	}
}
