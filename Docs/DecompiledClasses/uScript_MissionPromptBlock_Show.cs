#define UNITY_EDITOR
[NodePath("TerraTech/Actions/Missions")]
[FriendlyName("Show Mission Prompt on Block", "Offer a mission prompt on a specific block with a ModuleMissionPrompt.")]
public class uScript_MissionPromptBlock_Show : uScriptLogic
{
	public bool Out => true;

	public void In(LocalisedString bodyText, LocalisedString acceptButtonText, LocalisedString rejectButtonText, TankBlock targetBlock, bool highlightBlock = true, bool singleUse = false)
	{
		d.Assert(ManNetwork.IsHost, "Shouldn't be running uScript on clients");
		d.Assert(targetBlock != null, "Can't offer a block prompt on no block");
		ModuleMissionPrompt component = targetBlock.GetComponent<ModuleMissionPrompt>();
		if (component != null)
		{
			component.SetAvailableMissionPrompt(bodyText, acceptButtonText, rejectButtonText, highlightBlock, singleUse);
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
