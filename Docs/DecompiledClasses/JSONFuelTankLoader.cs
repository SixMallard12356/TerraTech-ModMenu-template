using Newtonsoft.Json.Linq;

public class JSONFuelTankLoader : JSONModuleLoader
{
	public override string GetModuleKey()
	{
		return "FuelTank";
	}

	public override bool CreateModuleForBlock(int blockID, ModdedBlockDefinition def, TankBlock block, JToken jToken)
	{
		if (jToken.Type == JTokenType.Object)
		{
			JObject obj = (JObject)jToken;
			ModuleFuelTank orAddComponent = GetOrAddComponent<ModuleFuelTank>(block);
			ModuleFuelTank component = Singleton.Manager<ManSpawn>.inst.GetBlockPrefab(BlockTypes.GSOCockpit_111).GetComponent<ModuleFuelTank>();
			orAddComponent.m_Gauge = new Gauge();
			orAddComponent.m_Gauge.m_GaugeProperties = component.m_Gauge.m_GaugeProperties;
			orAddComponent.m_Capacity = TryParse(obj, "Capacity", 40f);
			orAddComponent.m_RefillRate = TryParse(obj, "RefillRate", 1f);
			orAddComponent.m_LedMaterialColorName = TryParse(obj, "LEDMaterialColourName", "_MainColor");
			orAddComponent.m_LedMaterialFloatName = TryParse(obj, "LEDMaterialBlinkName", "_Blink");
			return true;
		}
		return false;
	}
}
