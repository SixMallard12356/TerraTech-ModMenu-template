using System;
using System.Security.Cryptography;
using System.Text;

public static class HashCodeUtility
{
	private const uint k_FNVHashBasis = 2166136261u;

	private const uint k_FNVHashPrime = 16777619u;

	public static int CombineHashCodes(int hashCode1)
	{
		uint num = 3735928563u;
		uint b = num;
		uint c = num;
		num += (uint)hashCode1;
		FinalizeHash(ref num, ref b, ref c);
		return (int)c;
	}

	public static int CombineHashCodes(int hashCode1, int hashCode2)
	{
		uint num = 3735928567u;
		uint num2 = num;
		uint c = num;
		num += (uint)hashCode1;
		num2 += (uint)hashCode2;
		FinalizeHash(ref num, ref num2, ref c);
		return (int)c;
	}

	public static int CombineHashCodes(int hashCode1, int hashCode2, int hashCode3)
	{
		uint num = 3735928571u;
		uint num2 = num;
		uint num3 = num;
		num += (uint)hashCode1;
		num2 += (uint)hashCode2;
		num3 += (uint)hashCode3;
		FinalizeHash(ref num, ref num2, ref num3);
		return (int)num3;
	}

	public static int CombineHashCodes(int hashCode1, int hashCode2, int hashCode3, int hashCode4)
	{
		uint num = 3735928575u;
		uint num2 = num;
		uint num3 = num;
		num += (uint)hashCode1;
		num2 += (uint)hashCode2;
		num3 += (uint)hashCode3;
		MixHash(ref num, ref num2, ref num3);
		num += (uint)hashCode4;
		FinalizeHash(ref num, ref num2, ref num3);
		return (int)num3;
	}

	public static int CombineHashCodes(params int[] hashCodes)
	{
		if (hashCodes == null)
		{
			return 224428569;
		}
		int num = hashCodes.Length;
		uint a = (uint)(-559038737 + (num << 2));
		uint b = a;
		uint c = a;
		int i;
		for (i = 0; num - i > 3; i += 3)
		{
			a += (uint)hashCodes[i];
			b += (uint)hashCodes[i + 1];
			c += (uint)hashCodes[i + 2];
			MixHash(ref a, ref b, ref c);
		}
		if (num - i > 2)
		{
			c += (uint)hashCodes[i + 2];
		}
		if (num - i > 1)
		{
			b += (uint)hashCodes[i + 1];
		}
		if (num - i > 0)
		{
			a += (uint)hashCodes[i];
			FinalizeHash(ref a, ref b, ref c);
		}
		return (int)c;
	}

	public static int GetPersistentHashCode(bool value)
	{
		if (!value)
		{
			return 1800329511;
		}
		return -1266253386;
	}

	public static uint QuickHash(uint value)
	{
		value = ((value >> 16) ^ value) * 73244475;
		value = ((value >> 16) ^ value) * 73244475;
		value = (value >> 16) ^ value;
		return value;
	}

	public static ulong QuickHash(ulong x)
	{
		x = (x ^ (x >> 30)) * 13787848793156543929uL;
		x = (x ^ (x >> 27)) * 10723151780598845931uL;
		x ^= x >> 31;
		return x;
	}

	public static uint FNVHash(uint value1)
	{
		return ((((((0x50C5D1F ^ (value1 & 0xFF)) * 16777619) ^ ((value1 >> 8) & 0xFF)) * 16777619) ^ ((value1 >> 16) & 0xFF)) * 16777619) ^ ((value1 >> 24) & 0xFF);
	}

	public static uint FNVHash(uint value1, uint value2)
	{
		return ((((((((((((((0x50C5D1F ^ (value1 & 0xFF)) * 16777619) ^ ((value1 >> 8) & 0xFF)) * 16777619) ^ ((value1 >> 16) & 0xFF)) * 16777619) ^ ((value1 >> 24) & 0xFF)) * 16777619) ^ (value2 & 0xFF)) * 16777619) ^ ((value2 >> 8) & 0xFF)) * 16777619) ^ ((value2 >> 16) & 0xFF)) * 16777619) ^ ((value2 >> 24) & 0xFF);
	}

	public static uint FNVHash(uint value1, uint value2, uint value3)
	{
		return ((((((((((((((((((((((0x50C5D1F ^ (value1 & 0xFF)) * 16777619) ^ ((value1 >> 8) & 0xFF)) * 16777619) ^ ((value1 >> 16) & 0xFF)) * 16777619) ^ ((value1 >> 24) & 0xFF)) * 16777619) ^ (value2 & 0xFF)) * 16777619) ^ ((value2 >> 8) & 0xFF)) * 16777619) ^ ((value2 >> 16) & 0xFF)) * 16777619) ^ ((value2 >> 24) & 0xFF)) * 16777619) ^ (value3 & 0xFF)) * 16777619) ^ ((value3 >> 8) & 0xFF)) * 16777619) ^ ((value3 >> 16) & 0xFF)) * 16777619) ^ ((value3 >> 24) & 0xFF);
	}

	public static uint FNVHashCombine(uint previous, uint nextValue)
	{
		return (((((((previous * 16777619) ^ (nextValue & 0xFF)) * 16777619) ^ ((nextValue >> 8) & 0xFF)) * 16777619) ^ ((nextValue >> 16) & 0xFF)) * 16777619) ^ ((nextValue >> 24) & 0xFF);
	}

	public static uint FNVHashCombine(uint previous, int nextValue)
	{
		return (((((((previous * 16777619) ^ (uint)(nextValue & 0xFF)) * 16777619) ^ (uint)((nextValue >>> 8) & 0xFF)) * 16777619) ^ (uint)((nextValue >>> 16) & 0xFF)) * 16777619) ^ (uint)((nextValue >>> 24) & 0xFF);
	}

	public static uint FNVHash(float value1)
	{
		uint num = value1.ToUIntBitwise();
		return ((((((0x50C5D1F ^ (num & 0xFF)) * 16777619) ^ ((num >> 8) & 0xFF)) * 16777619) ^ ((num >> 16) & 0xFF)) * 16777619) ^ ((num >> 24) & 0xFF);
	}

	public static uint FNVHash(float value1, float value2)
	{
		uint num = value1.ToUIntBitwise();
		uint num2 = ((((((0x50C5D1F ^ (num & 0xFF)) * 16777619) ^ ((num >> 8) & 0xFF)) * 16777619) ^ ((num >> 16) & 0xFF)) * 16777619) ^ ((num >> 24) & 0xFF);
		num = value2.ToUIntBitwise();
		return (((((((num2 * 16777619) ^ (num & 0xFF)) * 16777619) ^ ((num >> 8) & 0xFF)) * 16777619) ^ ((num >> 16) & 0xFF)) * 16777619) ^ ((num >> 24) & 0xFF);
	}

	public static uint FNVHash(float value1, float value2, float value3)
	{
		uint num = value1.ToUIntBitwise();
		uint num2 = ((((((0x50C5D1F ^ (num & 0xFF)) * 16777619) ^ ((num >> 8) & 0xFF)) * 16777619) ^ ((num >> 16) & 0xFF)) * 16777619) ^ ((num >> 24) & 0xFF);
		num = value2.ToUIntBitwise();
		uint num3 = (((((((num2 * 16777619) ^ (num & 0xFF)) * 16777619) ^ ((num >> 8) & 0xFF)) * 16777619) ^ ((num >> 16) & 0xFF)) * 16777619) ^ ((num >> 24) & 0xFF);
		num = value3.ToUIntBitwise();
		return (((((((num3 * 16777619) ^ (num & 0xFF)) * 16777619) ^ ((num >> 8) & 0xFF)) * 16777619) ^ ((num >> 16) & 0xFF)) * 16777619) ^ ((num >> 24) & 0xFF);
	}

	public static uint FNVHashCombine(uint previous, float nextValue)
	{
		uint num = nextValue.ToUIntBitwise();
		return (((((((previous * 16777619) ^ (num & 0xFF)) * 16777619) ^ ((num >> 8) & 0xFF)) * 16777619) ^ ((num >> 16) & 0xFF)) * 16777619) ^ ((num >> 24) & 0xFF);
	}

	public static int GetPersistentHashCode(int value)
	{
		uint num = (uint)value;
		num = num + 2127912214 + (num << 12);
		num = num ^ 0xC761C23Cu ^ (num >> 19);
		num = num + 374761393 + (num << 5);
		num = (uint)((int)num + -744332180) ^ (num << 9);
		num = (uint)((int)num + -42973499) + (num << 3);
		return (int)(num ^ 0xB55A4F09u ^ (num >> 16));
	}

	public static int GetPersistentHashCode(long value)
	{
		ulong num = (ulong)value;
		num = ~num + (num << 18);
		num ^= num >> 31;
		num *= 21;
		num ^= num >> 11;
		num += num << 6;
		num ^= num >> 22;
		return (int)num;
	}

	public static int GetPersistentHashCode(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return 0;
		}
		int length = value.Length;
		uint num = (uint)length;
		int num2 = length & 1;
		length >>= 1;
		int num3 = 0;
		while (length > 0)
		{
			num += value[num3];
			uint num4 = ((uint)value[num3 + 1] << 11) ^ num;
			num = (num << 16) ^ num4;
			num3 += 2;
			num += num >> 11;
			length--;
		}
		if (num2 == 1)
		{
			num += value[num3];
			num ^= num << 11;
			num += num >> 17;
		}
		num ^= num << 3;
		num += num >> 5;
		num ^= num << 4;
		num += num >> 17;
		num ^= num << 25;
		return (int)(num + (num >> 6));
	}

	private static uint Rotate(uint x, int k)
	{
		return (x << k) | (x >> 32 - k);
	}

	private static void MixHash(ref uint a, ref uint b, ref uint c)
	{
		a -= c;
		a ^= Rotate(c, 4);
		c += b;
		b -= a;
		b ^= Rotate(a, 6);
		a += c;
		c -= b;
		c ^= Rotate(b, 8);
		b += a;
		a -= c;
		a ^= Rotate(c, 16);
		c += b;
		b -= a;
		b ^= Rotate(a, 19);
		a += c;
		c -= b;
		c ^= Rotate(b, 4);
		b += a;
	}

	private static void FinalizeHash(ref uint a, ref uint b, ref uint c)
	{
		c ^= b;
		c -= Rotate(b, 14);
		a ^= c;
		a -= Rotate(c, 11);
		b ^= a;
		b -= Rotate(a, 25);
		c ^= b;
		c -= Rotate(b, 16);
		a ^= c;
		a -= Rotate(c, 4);
		b ^= a;
		b -= Rotate(a, 14);
		c ^= b;
		c -= Rotate(b, 24);
	}

	public static string GetSecureHash(string text, string salt)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(text + salt);
		byte[] inArray = null;
		using (SHA256 sHA = SHA256.Create())
		{
			inArray = sHA.ComputeHash(bytes);
		}
		return Convert.ToBase64String(inArray);
	}
}
