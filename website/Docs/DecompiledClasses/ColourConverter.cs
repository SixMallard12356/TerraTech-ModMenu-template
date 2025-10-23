using System.Globalization;
using UnityEngine;

public static class ColourConverter
{
	public static string ColourToString(Color32 color)
	{
		uint num = (uint)((((color.r << 8) + color.g << 8) + color.b << 8) + color.a);
		return $"{num:X8}";
	}

	public static string RecolourRichText(Color32 color, string str)
	{
		return $"<color=#{ColourToString(color)}>{str}</color>";
	}

	public static bool TryParseColourString(string colStr, out Color32 col)
	{
		col = Color.white;
		if (uint.TryParse(colStr, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out var result))
		{
			byte r = (byte)((result >> 24) & 0xFF);
			byte g = (byte)((result >> 16) & 0xFF);
			byte b = (byte)((result >> 8) & 0xFF);
			byte a = (byte)(result & 0xFF);
			col = new Color32(r, g, b, a);
			return true;
		}
		return false;
	}
}
