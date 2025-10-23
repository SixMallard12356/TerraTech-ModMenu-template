using Newtonsoft.Json.Linq;

public class JSONLandingGearLoader : JSONModuleLoader
{
	public override string GetModuleKey()
	{
		return "LandingGear";
	}

	public override bool CreateModuleForBlock(int blockID, ModdedBlockDefinition def, TankBlock block, JToken jToken)
	{
		if (jToken.Type == JTokenType.Object)
		{
			JObject obj = (JObject)jToken;
			ModuleLandingGear orAddComponent = GetOrAddComponent<ModuleLandingGear>(block);
			orAddComponent.m_DeployBelowAltitude = TryParse(obj, "DeployBelowAltitude", 20f);
			orAddComponent.m_IsRetractedByDefault = TryParse(obj, "IsRetractedByDefault", defaultValue: true);
			return true;
		}
		return false;
	}
}
