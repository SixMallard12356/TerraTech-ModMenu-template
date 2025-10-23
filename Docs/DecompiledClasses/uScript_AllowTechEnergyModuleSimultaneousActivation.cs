[FriendlyName("uScript_AllowTechEnergyModuleSimultaneousActivation", "Allow tech to simultaneous charge and activate all energy modules of a specific type if it has enough energy. Normal behaviour is sequential charge/activation")]
[NodePath("TerraTech/Actions/Techs")]
public class uScript_AllowTechEnergyModuleSimultaneousActivation : uScriptLogic
{
	public bool Out => true;

	public void In(Tank tech, TechSequencer.ChainType chainType)
	{
		if (tech != null && tech.Sequencer != null)
		{
			tech.Sequencer.ForceAllChainItemsReady(chainType);
		}
	}
}
