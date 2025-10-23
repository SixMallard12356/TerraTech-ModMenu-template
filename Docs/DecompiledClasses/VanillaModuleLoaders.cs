public static class VanillaModuleLoaders
{
	public static void RegisterVanillaModules()
	{
		JSONBlockLoader.RegisterModuleLoader(new JSONRecipeLoader());
		JSONBlockLoader.RegisterModuleLoader(new JSONWheelLoader());
		JSONBlockLoader.RegisterModuleLoader(new JSONGunLoader());
		JSONBlockLoader.RegisterModuleLoader(new JSONTechControllerLoader());
		JSONBlockLoader.RegisterModuleLoader(new JSONFuelTankLoader());
		JSONBlockLoader.RegisterModuleLoader(new JSONAnchorLoader());
		JSONBlockLoader.RegisterModuleLoader(new JSONWingLoader());
		JSONBlockLoader.RegisterModuleLoader(new JSONWheelLoaderLegacy());
	}
}
