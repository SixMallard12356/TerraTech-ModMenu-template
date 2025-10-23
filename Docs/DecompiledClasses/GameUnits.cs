using UnityEngine;

public static class GameUnits
{
	private enum GameUnitDisplay
	{
		Legacy,
		Metric
	}

	private const GameUnitDisplay UsedGameUnits = GameUnitDisplay.Metric;

	private const float kMilesPerKm = 0.621371f;

	private const float kFeetPerMetre = 3.28084f;

	private const float kMstoKmh = 3.6f;

	private const float kMstoMph = 2.2369356f;

	private const float kMphToKmh = 1.6093446f;

	private const float kSeaLevelOffset = 0.5f;

	public static float GetSpeed(float gameUnitsPerSecond)
	{
		return gameUnitsPerSecond * 3.6f;
	}

	public static float GetSpeed_MphToCurrent(float speedMph)
	{
		return speedMph * 1.6093446f;
	}

	public static string GetSpeedText(float gameUnitsPerSecond, string valueFormat = "000")
	{
		float speed = GetSpeed(gameUnitsPerSecond);
		return string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.HUD.HUDKilometersPerHour), speed.ToString(valueFormat));
	}

	public static float GetDistance(float gameUnits, bool forceBaseUnit = false)
	{
		return (gameUnits < 1000f || forceBaseUnit) ? gameUnits : (gameUnits / 1000f);
	}

	public static string GetDistanceText(float gameUnits, bool forceBaseUnit = false)
	{
		float distance = GetDistance(gameUnits, forceBaseUnit);
		if (gameUnits < 1000f || forceBaseUnit)
		{
			return string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.HUD.MissionDistanceMeters), distance);
		}
		return string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.HUD.MissionDistanceKilometers), distance);
	}

	public static float GetAltitude(float gameUnits)
	{
		float f = gameUnits - Singleton.Manager<ManGameMode>.inst.GetCurrentModeSeaLevel() + 0.5f;
		return Mathf.Floor(f);
	}

	public static string GetAltitudeText(float gameUnits, string valueFormat = "000")
	{
		float altitude = GetAltitude(gameUnits);
		return string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.HUD.HUDMetres), altitude.ToString(valueFormat));
	}
}
