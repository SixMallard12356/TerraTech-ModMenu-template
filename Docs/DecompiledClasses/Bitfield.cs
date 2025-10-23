using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class Bitfield<TEnum> where TEnum : struct, IConvertible, IComparable, IFormattable
{
	[JsonProperty]
	[SerializeField]
	private int m_BitField;

	public static readonly Bitfield<TEnum> Null = new Bitfield<TEnum>();

	[JsonIgnore]
	public int Field => m_BitField;

	[JsonIgnore]
	public bool IsNull => m_BitField == 0;

	[JsonIgnore]
	public bool AnySet => m_BitField != 0;

	[JsonConstructor]
	public Bitfield(TEnum[] types = null)
	{
		m_BitField = 0;
		if (types != null)
		{
			foreach (TEnum val in types)
			{
				m_BitField |= 1 << Convert.ToInt32(val);
			}
		}
	}

	public Bitfield(int typeAsInt)
	{
		m_BitField = 1 << typeAsInt;
	}

	public void Clear()
	{
		m_BitField = 0;
	}

	public void Add(int index)
	{
		m_BitField = Bitfield.Add(m_BitField, index);
	}

	public void Remove(int index)
	{
		m_BitField = Bitfield.Remove(m_BitField, index);
	}

	public void Set(int index, bool enabled)
	{
		if (enabled)
		{
			Add(index);
		}
		else
		{
			Remove(index);
		}
	}

	public void SetFlags(int flags)
	{
		m_BitField = flags;
	}

	public void AddFlags(int flags)
	{
		m_BitField = Bitfield.AddFlags(m_BitField, flags);
	}

	public bool Contains(int typeAsInt)
	{
		return Bitfield.Contains(m_BitField, typeAsInt);
	}

	public bool Contains(Bitfield<TEnum> other)
	{
		return (m_BitField & other.m_BitField) == other.m_BitField;
	}

	public static bool operator ==(Bitfield<TEnum> a, Bitfield<TEnum> b)
	{
		if ((object)a == b)
		{
			return true;
		}
		if ((object)a == null || (object)b == null)
		{
			return false;
		}
		return a.m_BitField == b.m_BitField;
	}

	public static bool operator !=(Bitfield<TEnum> a, Bitfield<TEnum> b)
	{
		return !(a == b);
	}

	public override bool Equals(object obj)
	{
		return obj as Bitfield<TEnum> == this;
	}

	public override int GetHashCode()
	{
		return m_BitField;
	}

	public void Assign(Bitfield<TEnum> other)
	{
		m_BitField = other.m_BitField;
	}
}
public static class Bitfield
{
	public static bool Contains(int flags, int typeAsInt)
	{
		return (flags & (1 << typeAsInt)) != 0;
	}

	public static int Add(int flags, int index)
	{
		flags |= 1 << index;
		return flags;
	}

	public static int AddFlags(int flags, int newFlags)
	{
		return flags |= newFlags;
	}

	public static int Remove(int flags, int typeAsInt)
	{
		return flags &= ~(1 << typeAsInt);
	}

	public static int Set(int flags, int index, bool set)
	{
		if (set)
		{
			return Add(flags, index);
		}
		return Remove(flags, index);
	}

	public static TEnum[] GetFlags<TEnum>(int flags) where TEnum : struct, IConvertible, IComparable, IFormattable
	{
		TEnum[] values = EnumValuesIterator<TEnum>.Values;
		TEnum[] array = new TEnum[values.Length];
		int newSize = 0;
		for (int i = 0; i < values.Length; i++)
		{
			int typeAsInt = (int)values.GetValue(i);
			if (Contains(flags, typeAsInt))
			{
				array[newSize++] = (TEnum)values.GetValue(i);
			}
		}
		Array.Resize(ref array, newSize);
		return array;
	}
}
