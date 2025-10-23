public class ModeCoOpCreative : ModeCoOp<ModeCoOpCreative>
{
	public override string GetGameMode()
	{
		return "ModeCoOpCreative";
	}

	public override ManGameMode.GameType GetGameType()
	{
		return ManGameMode.GameType.CoOpCreative;
	}

	public override bool ModePreInit(InitSettings initSettings)
	{
		base.TechManagerShowsOnlyPlayerTeam = false;
		return true;
	}

	protected override void EnterGenerateTerrain(InitSettings initSettings)
	{
		base.EnterGenerateTerrain(initSettings);
		Singleton.Manager<ManWorld>.inst.VendorSpawner.Enabled = true;
		Singleton.Manager<ManWorld>.inst.Vendors.SetAllActive(active: true);
		Singleton.Manager<ManWorld>.inst.Vendors.SetMissionBoardActive(active: false);
		Singleton.Manager<ManWorld>.inst.Vendors.SetVisibleOnRadar(visible: true);
	}

	protected override void EnterPreModeImpl(InitSettings initSettings)
	{
		base.EnterPreModeImpl(initSettings);
		Singleton.Manager<ManTimeOfDay>.inst.EnableSkyDome(enable: true);
		Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay(11, 0, 0);
		Singleton.Manager<ManTimeOfDay>.inst.SetDate(2700, 2, 22);
		Singleton.Manager<ManTimeOfDay>.inst.EnableTimeProgression(enable: false);
		Singleton.Manager<ManPlayer>.inst.SetPlayerInventoryToUnrestricted();
		Singleton.Manager<ManPlayer>.inst.EnablePalette(enable: true);
		if (TryLoadSetting<int>(initSettings, "EnemyDifficulty", out var outValue))
		{
			Singleton.Manager<ManPop>.inst.SetCreativePopulationDifficulty((ManPop.CreativeModePopulationDifficulty)outValue);
		}
	}
}
