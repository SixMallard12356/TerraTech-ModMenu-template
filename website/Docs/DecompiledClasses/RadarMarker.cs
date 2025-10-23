using System;

[Serializable]
public struct RadarMarker : IEquatable<RadarMarker>
{
	public ManRadar.IconType Icon;

	public ManRadar.RadarMarkerColorType Color;

	public bool IsUsed;

	public static RadarMarker DefaultMarker => new RadarMarker(ManRadar.IconType.rm_Flag, ManRadar.RadarMarkerColorType.White);

	public static RadarMarker DefaultMarker_Disabled => new RadarMarker(ManRadar.IconType.rm_Flag, ManRadar.RadarMarkerColorType.White, IsUsed: false);

	public RadarMarker(ManRadar.IconType Icon, ManRadar.RadarMarkerColorType Color, bool IsUsed = true)
	{
		this.Icon = Icon;
		this.Color = Color;
		this.IsUsed = IsUsed;
	}

	public RadarMarker(RadarMarker copyTarget)
	{
		Icon = copyTarget.Icon;
		Color = copyTarget.Color;
		IsUsed = copyTarget.IsUsed;
	}

	public static bool operator ==(RadarMarker info, RadarMarker otherInfo)
	{
		if (info.Icon == otherInfo.Icon && info.Color == otherInfo.Color)
		{
			return info.IsUsed == otherInfo.IsUsed;
		}
		return false;
	}

	public static bool operator !=(RadarMarker info, RadarMarker otherInfo)
	{
		return !(info == otherInfo);
	}

	public bool Equals(RadarMarker other)
	{
		return this == other;
	}

	public override bool Equals(object obj)
	{
		if (obj is RadarMarker other)
		{
			return Equals(other);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return ((17 * 31 + Icon.GetHashCode()) * 31 + Color.GetHashCode()) * 31 + IsUsed.GetHashCode();
	}

	public override string ToString()
	{
		return string.Format("Marker '{0}' - '{1}': {2}", Icon, Color, IsUsed ? "Used" : "NOT Used");
	}
}
