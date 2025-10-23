using UnityEngine.Networking;

public static class RadarMarkerExtentions
{
	public static void Write(this NetworkWriter stream, RadarMarker radarMarkerConfig)
	{
		stream.Write(radarMarkerConfig.IsUsed);
		if (radarMarkerConfig.IsUsed)
		{
			stream.WritePackedInt32((int)radarMarkerConfig.Icon);
			stream.WritePackedInt32((int)radarMarkerConfig.Color);
		}
	}

	public static RadarMarker ReadRadarMarker(this NetworkReader stream)
	{
		bool flag = stream.ReadBoolean();
		if (flag)
		{
			int icon = stream.ReadPackedInt32();
			ManRadar.RadarMarkerColorType color = (ManRadar.RadarMarkerColorType)stream.ReadPackedInt32();
			return new RadarMarker((ManRadar.IconType)icon, color, flag);
		}
		return RadarMarker.DefaultMarker_Disabled;
	}
}
