#define UNITY_EDITOR
using System;
using Newtonsoft.Json;

namespace TerraTech.Network;

public class TTNetworkIDConverter : JsonConverter
{
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(TTNetworkID);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.StartObject)
		{
			reader.Read();
			string nid = reader.ReadAsString();
			reader.Read();
			return new TTNetworkID(nid);
		}
		d.LogError("JSON m_NetworkID corrupt");
		return existingValue;
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		TTNetworkID tTNetworkID = (TTNetworkID)value;
		writer.WriteStartObject();
		writer.WritePropertyName("m_NetworkID");
		writer.WriteValue(tTNetworkID.m_NetworkID);
		writer.WriteEndObject();
	}
}
