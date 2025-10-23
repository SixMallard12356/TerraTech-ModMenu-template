using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class IntVecDictionaryKeyConverter<T> : JsonConverter
{
	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		Dictionary<IntVector2, T> dictionary = new Dictionary<IntVector2, T>();
		while (reader.Read())
		{
			if (reader.TokenType == JsonToken.PropertyName)
			{
				IntVector2 key = IntVector2.ConvertFromString(reader.Value.ToString());
				if (reader.Read())
				{
					T value = serializer.Deserialize<T>(reader);
					dictionary.Add(key, value);
				}
			}
			else if (reader.TokenType == JsonToken.EndObject)
			{
				break;
			}
		}
		return dictionary;
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		serializer.Serialize(writer, value);
	}

	public override bool CanConvert(Type objectType)
	{
		return true;
	}
}
