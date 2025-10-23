#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Util
{
	public struct BitPacker
	{
		public uint m_Data;

		private int m_Shift;

		public void Write(uint data, int numBits)
		{
			d.Assert(data < 1 << numBits, "Value too big for Packing");
			m_Data |= data << m_Shift;
			m_Shift += numBits;
		}

		public void Write(bool val)
		{
			Write(val ? 1u : 0u, 1);
		}

		public void WriteFloat01(float val, int numBits)
		{
			d.Assert(val >= 0f && val <= 1f, "WriteFloat01 - Float value " + val.ToString("F5") + " not in clamp range");
			uint data = (uint)((float)((1 << numBits) - 1) * Mathf.Clamp(val, 0f, 1f));
			Write(data, numBits);
		}

		public void WriteFloatPlusMinus1(float val, int numBits)
		{
			Write(val >= 0f);
			WriteFloat01(Mathf.Abs(val), numBits - 1);
		}
	}

	public struct BitUnpacker
	{
		private uint m_Data;

		private int m_Shift;

		public BitUnpacker(uint data)
		{
			m_Data = data;
			m_Shift = 0;
		}

		public uint ReadRaw(int numBits)
		{
			uint num = (uint)((1 << numBits) - 1);
			uint result = (m_Data >> m_Shift) & num;
			m_Shift += numBits;
			return result;
		}

		public bool ReadBool()
		{
			return ReadRaw(1) != 0;
		}

		public float ReadFloat01(int numBits)
		{
			uint num = ReadRaw(numBits);
			uint num2 = (uint)((1 << numBits) - 1);
			return (float)num / (float)num2;
		}

		public float ReadFloatPlusMinus1(int numBits)
		{
			bool num = ReadBool();
			float num2 = ReadFloat01(numBits - 1);
			if (!num)
			{
				return 0f - num2;
			}
			return num2;
		}
	}

	public struct CoordIterator
	{
		private int currentX;

		private int currentY;

		private IntVector2 minCoord;

		private IntVector2 maxCoordInclusive;

		public IntVector2 Current { get; private set; }

		public CoordIterator(IntVector2 minCoord, IntVector2 maxCoordInclusive)
		{
			d.Assert(minCoord.x <= maxCoordInclusive.x && minCoord.y <= maxCoordInclusive.y, "Max coord was smaller than min coord! Invalid range to iterate.");
			currentX = minCoord.x - 1;
			currentY = minCoord.y;
			this.minCoord = minCoord;
			this.maxCoordInclusive = maxCoordInclusive;
			Current = default(IntVector2);
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public bool MoveNext()
		{
			currentX++;
			if (currentX > maxCoordInclusive.x)
			{
				currentX = minCoord.x;
				currentY++;
				if (currentY > maxCoordInclusive.y)
				{
					Current = default(IntVector2);
					return false;
				}
			}
			Current = new IntVector2(currentX, currentY);
			return true;
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public CoordIterator GetEnumerator()
		{
			return this;
		}
	}

	public static class Vector3Utils
	{
		public static string ToSerializedString(Vector3 v, char splitter)
		{
			return string.Join(splitter.ToString(), v.x, v.y, v.z);
		}

		public static bool TryParseValueFromSerializedString(out Vector3 v, string source, char splitter)
		{
			v = default(Vector3);
			string[] array = source.Split(splitter);
			if (array == null || array.Length != 3)
			{
				return false;
			}
			float[] array2 = new float[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!float.TryParse(array[i], out array2[i]))
				{
					Debug.LogError("Could not parse Vector3 from serializedString, expected 3 floats separated by " + splitter + "s but got => " + source);
					return false;
				}
			}
			v = new Vector3(array2[0], array2[1], array2[2]);
			return true;
		}
	}

	public static class QuaternionUtils
	{
		public static Quaternion RotateTowardsAroundAxis(Quaternion fromRotation, Quaternion toRotation, Vector3 aroundAxis, float maxDegrees)
		{
			aroundAxis.Normalize();
			Vector3 normalized = Vector3.ProjectOnPlane(fromRotation * Vector3.forward, aroundAxis).normalized;
			Vector3 normalized2 = Vector3.ProjectOnPlane(toRotation * Vector3.forward, aroundAxis).normalized;
			float target = Vector3.SignedAngle(normalized, normalized2, aroundAxis);
			return Quaternion.AngleAxis(Mathf.MoveTowardsAngle(0f, target, maxDegrees), aroundAxis) * fromRotation;
		}
	}

	public static class Encryption
	{
		private static readonly Func<int, byte[]>[] s_AESEncryptionKeyFuncs = new Func<int, byte[]>[8] { Appleseed, Benzema, Chaplin, DunePartTwo, ECorp, Foxtrot, GoogleEnPassant, Hamster };

		private static byte[] Appleseed(int seed)
		{
			int num = BitConverter.ToInt32(new byte[4] { 69, 103, 103, 79 }, 0);
			System.Random random = new System.Random(seed);
			return BitConverter.GetBytes(num + (random.Next() - random.Next() + 253702223));
		}

		private static byte[] Benzema(int seed)
		{
			int num = BitConverter.ToInt32(new byte[4] { 110, 77, 121, 70 }, 0);
			System.Random random = new System.Random(seed);
			return BitConverter.GetBytes((num + (random.Next() - random.Next()) << 2) | 0x11111111);
		}

		private static byte[] Chaplin(int seed)
		{
			int num = BitConverter.ToInt32(new byte[4] { 97, 99, 101, 95 }, 0);
			System.Random random = new System.Random(seed);
			return BitConverter.GetBytes((num + (random.Next() - random.Next()) >> 1) & 0x5DDDDDDD);
		}

		private static byte[] DunePartTwo(int seed)
		{
			int num = BitConverter.ToInt32(new byte[4] { 83, 104, 97, 109 }, 0);
			System.Random random = new System.Random(seed);
			int num2 = num + (random.Next() - random.Next());
			return BitConverter.GetBytes(num2 - num2 % 4);
		}

		private static byte[] ECorp(int seed)
		{
			int num = BitConverter.ToInt32(new byte[4] { 101, 79, 110, 89 }, 0);
			System.Random random = new System.Random(seed);
			return BitConverter.GetBytes(num + (random.Next() - random.Next()) - 267509022);
		}

		private static byte[] Foxtrot(int seed)
		{
			int num = BitConverter.ToInt32(new byte[4] { 111, 117, 114, 115 }, 0);
			System.Random random = new System.Random(seed);
			int num2 = num + (random.Next() - random.Next());
			return BitConverter.GetBytes(num2 + (num2 >> 4) * 2);
		}

		private static byte[] GoogleEnPassant(int seed)
		{
			int num = BitConverter.ToInt32(new byte[4] { 116, 65, 116, 87 }, 0);
			System.Random random = new System.Random(seed);
			num += random.Next() - random.Next();
			num = (num >> 3) | (num * num);
			return BitConverter.GetBytes(num);
		}

		private static byte[] Hamster(int seed)
		{
			int num = BitConverter.ToInt32(new byte[4] { 107, 101, 100, 84 }, 0);
			System.Random random = new System.Random(seed);
			return BitConverter.GetBytes(num + (random.Next() - random.Next()) + "redrum".GetHashCode());
		}

		private static byte[] GetAESEncryptionKey()
		{
			byte[] array = new byte[32];
			System.Random newEncryptionRandom = Globals.NewEncryptionRandom;
			for (int i = 0; i < s_AESEncryptionKeyFuncs.Length; i++)
			{
				Array.Copy(s_AESEncryptionKeyFuncs[i](newEncryptionRandom.Next()), 0, array, i * 4, 4);
			}
			return array;
		}

		public static string EncryptStringAES(string unencryptedString, ushort encryptionIV = 0)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(unencryptedString);
			using SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create();
			symmetricAlgorithm.Key = GetAESEncryptionKey();
			byte[] array = BitConverter.GetBytes(encryptionIV);
			Array.Resize(ref array, 16);
			symmetricAlgorithm.IV = array;
			symmetricAlgorithm.Padding = PaddingMode.PKCS7;
			ICryptoTransform transform = symmetricAlgorithm.CreateEncryptor(symmetricAlgorithm.Key, symmetricAlgorithm.IV);
			using MemoryStream memoryStream = new MemoryStream();
			using CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			return Convert.ToBase64String(memoryStream.ToArray());
		}

		public static string DecryptStringAES(string encryptedString, ushort encryptionIV = 0)
		{
			byte[] buffer = Convert.FromBase64String(encryptedString);
			using SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create();
			symmetricAlgorithm.Key = GetAESEncryptionKey();
			byte[] array = BitConverter.GetBytes(encryptionIV);
			Array.Resize(ref array, 16);
			symmetricAlgorithm.IV = array;
			symmetricAlgorithm.Padding = PaddingMode.PKCS7;
			ICryptoTransform transform = symmetricAlgorithm.CreateDecryptor(symmetricAlgorithm.Key, symmetricAlgorithm.IV);
			using MemoryStream stream = new MemoryStream(buffer);
			using CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
			using StreamReader streamReader = new StreamReader(stream2);
			return streamReader.ReadToEnd();
		}

		public static void TestAESEncryption()
		{
			string text = "eggMan!";
			string text2 = EncryptStringAES(text, 0);
			string text3 = DecryptStringAES(text2, 0);
			d.LogError("Encryption Test Complete [Expand for details]\nInput: " + text + "\nEncrypted: " + text2 + "\nDecrypted: " + text3);
		}
	}

	private static Plane[] s_CalculatedFrustumPlanes = new Plane[6];

	public static IEnumerable<string> EnumDirectoriesRecursive(string basePath)
	{
		yield return basePath;
		string[] directories = Directory.GetDirectories(basePath);
		foreach (string basePath2 in directories)
		{
			foreach (string item in EnumDirectoriesRecursive(basePath2))
			{
				yield return item;
			}
		}
	}

	public static IEnumerable<string> EnumFilesRecursive(string basePath, string extension = null)
	{
		foreach (string item in EnumDirectoriesRecursive(basePath))
		{
			string[] files = Directory.GetFiles(item);
			foreach (string text in files)
			{
				if (extension == null || text.EndsWith(extension))
				{
					yield return text;
				}
			}
		}
	}

	public static bool TryParseFloatInvariant(string numberString, out float value)
	{
		return float.TryParse(numberString.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out value);
	}

	public static string GetTimeString(float timeInSeconds, bool showMinutes, bool showMilliseconds)
	{
		int num = 0;
		if (showMilliseconds)
		{
			num = (int)((timeInSeconds - (float)(int)timeInSeconds) * 1000f);
		}
		string text = null;
		if (showMinutes)
		{
			int num2 = (int)timeInSeconds / 60;
			if (showMilliseconds)
			{
				int num3 = (int)timeInSeconds % 60;
				return $"{num2:00}:{num3:00}:{num:000}";
			}
			int num4 = Mathf.CeilToInt(timeInSeconds);
			return $"{num2:00}:{num4:00}";
		}
		if (showMilliseconds)
		{
			int num5 = Mathf.FloorToInt(timeInSeconds);
			return $"{num5:00}:{num:000}";
		}
		int num6 = Mathf.CeilToInt(timeInSeconds);
		return $"{num6:00}";
	}

	public static string[] GetSearchFilters(string searchString, bool matchCase = false)
	{
		string text = searchString.Trim();
		if (!matchCase)
		{
			text = searchString.ToLowerInvariant();
		}
		return text.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
	}

	public static bool StringPassesSearchFilter(string testString, IEnumerable<string> filterGroups, bool matchCase = false)
	{
		string text = (matchCase ? testString : testString.ToLowerInvariant());
		int num = 0;
		foreach (string filterGroup in filterGroups)
		{
			num = text.IndexOf(filterGroup, num);
			if (num == -1)
			{
				break;
			}
			num += filterGroup.Length;
		}
		return num != -1;
	}

	public static int SeachNextIndex<T>(List<T> list, string searchString, int startingIndex = -1, bool loopAround = false, Func<T, string> elemNameSelector = null)
	{
		string[] invariantSearchStrings = GetSearchFilters(searchString);
		startingIndex = Mathf.Clamp(startingIndex, 0, list.Count - 1);
		int num = list.FindIndex(startingIndex, ElemMatchesSearchString);
		if (loopAround && num == -1 && startingIndex > 0)
		{
			num = list.FindIndex(0, startingIndex + 1, ElemMatchesSearchString);
		}
		return num;
		bool ElemMatchesSearchString(T elem)
		{
			return StringPassesSearchFilter(elemNameSelector?.Invoke(elem) ?? elem.ToString(), invariantSearchStrings);
		}
	}

	public static Plane[] CalculateFrustumPlanes(Camera camera)
	{
		GeometryUtility.CalculateFrustumPlanes(camera, s_CalculatedFrustumPlanes);
		return s_CalculatedFrustumPlanes;
	}

	public static string GetCurrentRAMUsageReadout()
	{
		long totalMemory = GC.GetTotalMemory(forceFullCollection: false);
		int num = 0;
		for (long num2 = totalMemory; num2 > 0; num2 /= 10)
		{
			num++;
		}
		float num3 = totalMemory;
		for (int num4 = 9; num4 >= 0; num4 -= 3)
		{
			if (num > num4)
			{
				return string.Format(num4 switch
				{
					3 => "{0} KB", 
					6 => "{0} MB", 
					9 => "{0} GB", 
					_ => "{0} Bytes", 
				}, (num3 / Mathf.Pow(10f, num4)).ToString(num4 switch
				{
					3 => "F3", 
					0 => "F0", 
					_ => "F6", 
				}));
			}
		}
		d.LogError("Tried to get RAM usage readout but the value was sub-zero??? HOW??");
		return "ERROR";
	}

	public static void RebuildExplicitVerticalUINavigationBetweenElements(IEnumerable<Selectable> selectables, bool includeInactive = false)
	{
		Selectable[] array = (includeInactive ? selectables.ToArray() : selectables.Where((Selectable r) => r.gameObject.activeSelf).ToArray());
		for (int num = 0; num < array.Length; num++)
		{
			Navigation navigation = new Navigation
			{
				mode = Navigation.Mode.Explicit
			};
			int num2 = num - 1;
			int num3 = num + 1;
			if (num2 >= 0)
			{
				navigation.selectOnUp = array[num2];
			}
			if (num3 < array.Length)
			{
				navigation.selectOnDown = array[num3];
			}
			array[num].navigation = navigation;
		}
	}
}
