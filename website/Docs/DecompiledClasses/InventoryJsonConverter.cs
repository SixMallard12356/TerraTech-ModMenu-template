#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class InventoryJsonConverter : JsonConverter
{
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(IInventory<BlockTypes>);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.StartObject)
		{
			IInventory<BlockTypes> inventory = (IInventory<BlockTypes>)existingValue;
			inventory.Clear();
			JObject jObject = JObject.Load(reader);
			d.Assert(jObject?.HasValues ?? false, "Inventory JSON is incorrect");
			{
				foreach (JObject item2 in (IEnumerable<JToken>)jObject["m_InventoryList"])
				{
					BlockTypes item = item2["m_BlockType"].ToObject<BlockTypes>();
					int count = item2["m_Quantity"].ToObject<int>();
					inventory.SetBlockCount(item, count);
				}
				return inventory;
			}
		}
		return null;
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		IInventory<BlockTypes> inventory = (IInventory<BlockTypes>)value;
		if (inventory == null)
		{
			return;
		}
		writer.WriteStartObject();
		writer.WritePropertyName("m_InventoryList");
		writer.WriteStartArray();
		foreach (KeyValuePair<BlockTypes, int> item in inventory)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("m_BlockType");
			writer.WriteValue((int)item.Key);
			writer.WritePropertyName("m_Quantity");
			writer.WriteValue(item.Value);
			writer.WriteEndObject();
		}
		writer.WriteEndArray();
		writer.WriteEndObject();
	}
}
