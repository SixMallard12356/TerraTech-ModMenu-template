using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public sealed class Vec3DictionaryKeyConverter<T> : JsonConverter
{
	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		Vector3 v = Vector2.zero;
		Dictionary<Vector3, T> dictionary = new Dictionary<Vector3, T>();
		do
		{
			if (reader.TokenType == JsonToken.StartObject)
			{
				reader.Read();
				string source = reader.Value.ToString();
				Util.Vector3Utils.TryParseValueFromSerializedString(out v, source, '^');
				if (reader.Read())
				{
					T value = serializer.Deserialize<T>(reader);
					dictionary.Add(v, value);
				}
			}
			else
			{
				if (reader.TokenType == JsonToken.Null)
				{
					dictionary = null;
					break;
				}
				if (reader.TokenType == JsonToken.EndArray)
				{
					break;
				}
			}
		}
		while (reader.Read());
		return dictionary;
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		Dictionary<Vector3, T> dictionary = (Dictionary<Vector3, T>)value;
		if (dictionary != null)
		{
			writer.WriteStartArray();
			foreach (KeyValuePair<Vector3, T> item in dictionary)
			{
				writer.WriteStartObject();
				writer.WritePropertyName(Util.Vector3Utils.ToSerializedString(item.Key, '^'));
				writer.WriteValue(item.Value);
				writer.WriteEndObject();
			}
			writer.WriteEndArray();
		}
		else
		{
			writer.WriteNull();
		}
	}

	public override bool CanConvert(Type objectType)
	{
		return true;
	}
}
