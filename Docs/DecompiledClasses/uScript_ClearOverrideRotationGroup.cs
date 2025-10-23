#define UNITY_EDITOR
[NodePath("TerraTech/Actions/Tutorial")]
public class uScript_ClearOverrideRotationGroup : uScriptLogic
{
	public bool Out => true;

	public void In(TankBlock theBlock)
	{
		if (theBlock != null)
		{
			Singleton.Manager<ManTechBuilder>.inst.ClearBlockRotationOverride(theBlock);
		}
		else
		{
			d.LogError("ERROR: uScript_ClearOverrideRotationGroup given null block");
		}
	}
}
