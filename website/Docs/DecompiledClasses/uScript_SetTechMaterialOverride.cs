#define UNITY_EDITOR
[NodePath("TerraTech/Actions/Techs")]
[FriendlyName("uScript_SetTechMaterialOverride", "Set a material override for all blocks on this tech")]
public class uScript_SetTechMaterialOverride : uScriptLogic
{
	public bool Out => true;

	public void In(Tank tech, bool enable, ManTechMaterialSwap.MatType customMaterialType)
	{
		if (tech != null)
		{
			tech.SetCustomMaterialOverride(enable, customMaterialType);
		}
		else
		{
			d.LogWarning("uScript_SetTechMaterialOverride - unable to set material as it was tech arg was null");
		}
	}
}
