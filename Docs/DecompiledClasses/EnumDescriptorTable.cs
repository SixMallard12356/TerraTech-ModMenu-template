using System;
using System.Collections.Generic;

public class EnumDescriptorTable
{
	private struct Descriptor
	{
		public Type categoryType;

		public int categoryValue;
	}

	private Dictionary<int, Descriptor[]> m_DescriptorLookup = new Dictionary<int, Descriptor[]>();

	private static bool DescriptorComparator<T>(Descriptor t1)
	{
		return t1.categoryType == typeof(T);
	}

	public bool IsDescriptorFlag(int hash, Type descriptorType, int descriptorValue)
	{
		if (m_DescriptorLookup.TryGetValue(hash, out var value))
		{
			Descriptor[] array = value;
			for (int i = 0; i < array.Length; i++)
			{
				Descriptor descriptor = array[i];
				if (descriptor.categoryType == descriptorType)
				{
					return (descriptor.categoryValue & descriptorValue) != 0;
				}
			}
		}
		return false;
	}

	public bool TryGetDescriptor<T>(int hash, out int enumIntValue)
	{
		if (m_DescriptorLookup.TryGetValue(hash, out var value))
		{
			Descriptor[] array = value;
			for (int i = 0; i < array.Length; i++)
			{
				Descriptor descriptor = array[i];
				if (descriptor.categoryType == typeof(T))
				{
					enumIntValue = descriptor.categoryValue;
					return true;
				}
			}
		}
		enumIntValue = 0;
		return false;
	}

	public void SetDescriptor<T>(int hash, T descriptor)
	{
		int num;
		if (m_DescriptorLookup.TryGetValue(hash, out var value))
		{
			num = Array.FindIndex(value, DescriptorComparator<T>);
			if (num == -1)
			{
				Array.Resize(ref value, value.Length + 1);
				num = value.Length - 1;
			}
		}
		else
		{
			value = new Descriptor[1];
			num = 0;
		}
		value[num] = new Descriptor
		{
			categoryType = typeof(T),
			categoryValue = Convert.ToInt32(descriptor)
		};
		m_DescriptorLookup[hash] = value;
	}

	public TEnum[] GetDescriptorFlagsArray<TEnum>(int hash) where TEnum : struct, IConvertible, IComparable, IFormattable
	{
		return Bitfield.GetFlags<TEnum>(GetDescriptorFlags<TEnum>(hash));
	}

	public int GetDescriptorFlags<TEnum>(int hash) where TEnum : struct, IConvertible, IComparable, IFormattable
	{
		if (TryGetDescriptor<TEnum>(hash, out var enumIntValue))
		{
			return enumIntValue;
		}
		return 0;
	}

	public void SetDescriptorFlags<TEnum>(int hash, int flags) where TEnum : struct, IConvertible, IComparable, IFormattable
	{
		int num;
		if (m_DescriptorLookup.TryGetValue(hash, out var value))
		{
			num = Array.FindIndex(value, (Descriptor descriptor) => descriptor.categoryType == typeof(TEnum));
			if (num == -1)
			{
				Array.Resize(ref value, value.Length + 1);
				num = value.Length - 1;
			}
		}
		else
		{
			value = new Descriptor[1];
			num = 0;
		}
		int categoryValue = Bitfield.AddFlags(value[num].categoryValue, flags);
		value[num] = new Descriptor
		{
			categoryType = typeof(TEnum),
			categoryValue = categoryValue
		};
		m_DescriptorLookup[hash] = value;
	}

	public void RemoveDescriptors(int hash)
	{
		m_DescriptorLookup.Remove(hash);
	}
}
