#define UNITY_EDITOR
using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class ItemTypeInfo : IEquatable<ItemTypeInfo>
{
	[SerializeField]
	private ObjectTypes m_ObjectType;

	[SerializeField]
	private int m_ItemType;

	public ObjectTypes ObjectType => m_ObjectType;

	public int ItemType => m_ItemType;

	public string name
	{
		get
		{
			switch (m_ObjectType)
			{
			case ObjectTypes.Block:
			{
				BlockTypes itemType3 = (BlockTypes)m_ItemType;
				return itemType3.ToString();
			}
			case ObjectTypes.Chunk:
			{
				ChunkTypes itemType2 = (ChunkTypes)m_ItemType;
				return itemType2.ToString();
			}
			case ObjectTypes.Scenery:
			{
				SceneryTypes itemType = (SceneryTypes)m_ItemType;
				return itemType.ToString();
			}
			default:
				return m_ObjectType.ToString();
			}
		}
	}

	static ItemTypeInfo()
	{
		d.Assert(Enum.GetValues(typeof(ObjectTypes)).Length <= 32, "ItemTypeInfo.GetHashCode() implementation assumes at most 32 values in ObjectTypes enum");
	}

	public ItemTypeInfo()
	{
	}

	[JsonConstructor]
	public ItemTypeInfo(ObjectTypes objectType, int itemType)
	{
		m_ObjectType = objectType;
		m_ItemType = itemType;
	}

	public int GetItemValue()
	{
		int result = 0;
		switch (m_ObjectType)
		{
		case ObjectTypes.Block:
			result = Singleton.Manager<RecipeManager>.inst.GetBlockBuyPrice((BlockTypes)m_ItemType);
			break;
		case ObjectTypes.Chunk:
			result = Singleton.Manager<RecipeManager>.inst.GetChunkPrice((ChunkTypes)m_ItemType);
			break;
		default:
			d.LogError("ItemTypeInfo.GetItemValue - Value for ObjectType '" + m_ObjectType.ToString() + "' is not supported!");
			break;
		}
		return result;
	}

	public void Set(ItemTypeInfo other)
	{
		m_ObjectType = other.m_ObjectType;
		m_ItemType = other.m_ItemType;
	}

	public void Set(ObjectTypes otype, int itype)
	{
		m_ObjectType = otype;
		m_ItemType = itype;
	}

	public static bool operator ==(ItemTypeInfo a, ItemTypeInfo b)
	{
		if ((object)a == b)
		{
			return true;
		}
		if ((object)a == null || (object)b == null)
		{
			return false;
		}
		return a.Equals(b);
	}

	public static bool operator !=(ItemTypeInfo a, ItemTypeInfo b)
	{
		return !(a == b);
	}

	public bool Equals(ItemTypeInfo otherItem)
	{
		if (otherItem == null)
		{
			return false;
		}
		if (ObjectType == otherItem.ObjectType)
		{
			return ItemType == otherItem.ItemType;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		if (!(obj is ItemTypeInfo itemTypeInfo))
		{
			return false;
		}
		if (ObjectType == itemTypeInfo.ObjectType)
		{
			return ItemType == itemTypeInfo.ItemType;
		}
		return false;
	}

	public int CompareTo(ItemTypeInfo other)
	{
		if (ObjectType != other.ObjectType)
		{
			return ObjectType.CompareTo(other.ObjectType);
		}
		return ItemType.CompareTo(other.ItemType);
	}

	public override int GetHashCode()
	{
		return (m_ItemType << 5) | (int)m_ObjectType;
	}

	public override string ToString()
	{
		return m_ObjectType.ToString() + " " + m_ItemType;
	}

	public static int GetHashCode(ObjectTypes objectType, int itemType)
	{
		return (itemType << 5) | (int)objectType;
	}

	public static ItemTypeInfo Parse(string s)
	{
		string[] array = s.Split(' ');
		if (array.Length == 2)
		{
			return new ItemTypeInfo((ObjectTypes)Enum.Parse(typeof(ObjectTypes), array[0]), int.Parse(array[1]));
		}
		return new ItemTypeInfo(ObjectTypes.Null, 0);
	}

	public static Type GetItemType(ObjectTypes objectType)
	{
		Type result = null;
		switch (objectType)
		{
		case ObjectTypes.Block:
			result = typeof(BlockTypes);
			break;
		case ObjectTypes.Scenery:
			result = typeof(SceneryTypes);
			break;
		case ObjectTypes.Chunk:
			result = typeof(ChunkTypes);
			break;
		}
		return result;
	}

	public static string GetEditorName(ObjectTypes objectType, int itemType)
	{
		Type itemType2 = GetItemType(objectType);
		string text = "";
		if (itemType2 != null)
		{
			return Enum.GetName(itemType2, itemType);
		}
		return (objectType == ObjectTypes.Vehicle) ? "Vehicle" : "NULL";
	}

	public void SetSubtype(int t)
	{
		m_ItemType = t;
	}
}
