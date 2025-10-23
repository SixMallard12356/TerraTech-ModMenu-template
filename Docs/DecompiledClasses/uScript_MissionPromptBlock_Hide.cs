#define UNITY_EDITOR
[NodePath("TerraTech/Actions/Missions")]
[FriendlyName("Hide Mission Prompt on Block", "Turn off the prompt on a specific block with a ModuleMissionPrompt.")]
public class uScript_MissionPromptBlock_Hide : uScriptLogic
{
	public bool Out => true;

	public void In(TankBlock targetBlock)
	{
		d.Assert(ManNetwork.IsHost, "Shouldn't be running uScript on clients");
		d.Assert(targetBlock != null, "Can't offer a block prompt on no block");
		ModuleMissionPrompt component = targetBlock.GetComponent<ModuleMissionPrompt>();
		if (component != null)
		{
			component.ClearAvailableMissionPrompt();
			if (targetBlock.tank != null && targetBlock.tank.netTech != null)
			{
				targetBlock.tank.netTech.SetModuleDirty(component);
			}
		}
		else
		{
			d.LogError("Target block does not have a mission prompt module on it");
		}
	}
}
