using System;
using Newtonsoft.Json;
using UnityEngine;

public sealed class JsonColourConverter : JsonConverter
{
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(Color);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		Color color = Color.white;
		if (reader.TokenType == JsonToken.String && ColourConverter.TryParseColourString(reader.Value.ToString(), out var col))
		{
			color = col;
		}
		return color;
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		Color color = (Color)value;
		writer.WriteValue(ColourConverter.ColourToString(color));
	}
}
