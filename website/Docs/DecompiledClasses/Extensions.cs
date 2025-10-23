#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public static class Extensions
{
	[StructLayout(LayoutKind.Explicit)]
	private struct UIntFloatUnion
	{
		[FieldOffset(0)]
		public float floatValue;

		[FieldOffset(0)]
		public int intValue;

		[FieldOffset(0)]
		public uint uintValue;
	}

	[StructLayout(LayoutKind.Explicit)]
	private struct ULongDoubleUnion
	{
		[FieldOffset(0)]
		public double doubleValue;

		[FieldOffset(0)]
		public long longValue;

		[FieldOffset(0)]
		public ulong ulongValue;
	}

	private static StringBuilder s_SharedStringBuilder = new StringBuilder(256);

	private static Vector3[] _s_WrldRct_fourCornersArrray = null;

	public static IEnumerable<T> Zip<A, B, T>(this IEnumerable<A> seqA, IEnumerable<B> seqB, Func<A, B, T> func)
	{
		if (seqA == null)
		{
			throw new ArgumentNullException("seqA");
		}
		if (seqB == null)
		{
			throw new ArgumentNullException("seqB");
		}
		return seqA.Zip35Deferred(seqB, func);
	}

	private static IEnumerable<T> Zip35Deferred<A, B, T>(this IEnumerable<A> seqA, IEnumerable<B> seqB, Func<A, B, T> func)
	{
		using IEnumerator<A> iteratorA = seqA.GetEnumerator();
		using IEnumerator<B> iteratorB = seqB.GetEnumerator();
		while (iteratorA.MoveNext() && iteratorB.MoveNext())
		{
			yield return func(iteratorA.Current, iteratorB.Current);
		}
	}

	public static T Lowest<T>(this IEnumerable<T> sequence, Func<T, float> selector)
	{
		T result = default(T);
		float num = float.MaxValue;
		foreach (T item in sequence)
		{
			float num2 = selector(item);
			if (num2 < num)
			{
				result = item;
				num = num2;
			}
		}
		return result;
	}

	public static T Highest<T>(this IEnumerable<T> sequence, Func<T, float> selector)
	{
		T result = default(T);
		float num = float.MinValue;
		foreach (T item in sequence)
		{
			float num2 = selector(item);
			if (num2 > num)
			{
				result = item;
				num = num2;
			}
		}
		return result;
	}

	public static IEnumerable<T> UnionDistinct<T>(this IEnumerable<T> seqA, IEnumerable<T> seqB, Func<T, T, bool> comparer)
	{
		bool[] used = new bool[seqB.Count()];
		foreach (T a in seqA)
		{
			yield return a;
			for (int i = 0; i < seqB.Count(); i++)
			{
				T arg = seqB.ElementAt(i);
				if (!used[i] && comparer(a, arg))
				{
					used[i] = true;
					break;
				}
			}
		}
		for (int j = 0; j < seqB.Count(); j++)
		{
			if (!used[j])
			{
				yield return seqB.ElementAt(j);
			}
		}
	}

	public static bool SetContains<T>(this IEnumerable<T> innerSet, IEnumerable<T> outerSet) where T : class
	{
		IEnumerator<T> enumerator = innerSet.GetEnumerator();
		IEnumerator<T> enumerator2 = outerSet.GetEnumerator();
		if (!enumerator.MoveNext() || !enumerator2.MoveNext())
		{
			return false;
		}
		while (true)
		{
			if (enumerator.Current.Equals(enumerator2.Current))
			{
				if (!enumerator.MoveNext())
				{
					return true;
				}
				if (!enumerator2.MoveNext())
				{
					return false;
				}
			}
			else if (!enumerator2.MoveNext())
			{
				break;
			}
		}
		return false;
	}

	public static void Resize<T>(this List<T> list, int size, T value)
	{
		int count = list.Count;
		if (size < count)
		{
			list.RemoveRange(size, count - size);
		}
		else if (size > count)
		{
			if (size > list.Capacity)
			{
				list.Capacity = size;
			}
			for (int i = count; i < size; i++)
			{
				list.Add(value);
			}
		}
	}

	public static void Resize<T>(this List<T> list, int size) where T : new()
	{
		list.Resize(size, new T());
	}

	public static void Shuffle<T>(this IList<T> list)
	{
		System.Random random = new System.Random();
		int num = list.Count;
		while (num > 1)
		{
			num--;
			int index = random.Next(num + 1);
			T value = list[index];
			list[index] = list[num];
			list[num] = value;
		}
	}

	public static T GetRandomEntry<T>(this IList<T> list)
	{
		int num = -1;
		if (list.Count > 0)
		{
			num = UnityEngine.Random.Range(0, list.Count);
		}
		if (num < 0)
		{
			return default(T);
		}
		return list[num];
	}

	public static void InsertionSort<T>(this IList<T> list, Func<T, int> elemValuePredicate)
	{
		int count = list.Count;
		for (int i = 1; i < count; i++)
		{
			T value = list[i];
			int num = elemValuePredicate(list[i]);
			int num2 = i - 1;
			while (num2 >= 0 && elemValuePredicate(list[num2]) > num)
			{
				list[num2 + 1] = list[num2];
				num2--;
			}
			list[num2 + 1] = value;
		}
	}

	public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current)
	{
		return current.Next ?? current.List.First;
	}

	public static LinkedListNode<T> PreviousOrLast<T>(this LinkedListNode<T> current)
	{
		return current.Previous ?? current.List.Last;
	}

	public static IEnumerable<LinkedListNode<T>> EnumerateCircular<T>(this LinkedListNode<T> start)
	{
		LinkedListNode<T> current = start;
		do
		{
			yield return current;
			current = current.NextOrFirst();
		}
		while (current != start);
	}

	public static void Shuffle<T>(this T[] array)
	{
		for (int num = array.Length; num > 1; num--)
		{
			int num2 = UnityEngine.Random.Range(0, num);
			T val = array[num2];
			array[num2] = array[num - 1];
			array[num - 1] = val;
		}
	}

	public static T PickRandom<T>(this T[] array)
	{
		if (array == null || array.Length == 0)
		{
			return default(T);
		}
		return array[UnityEngine.Random.Range(0, array.Length)];
	}

	public static int GetDecimalPlaceCount(this float input)
	{
		input -= (float)(int)input;
		int num = 0;
		while (input > 0f)
		{
			num++;
			input *= 10f;
			input -= (float)(int)input;
		}
		return num;
	}

	public static float RandomVariance(this float input, float percentVariance)
	{
		return input * (1f + percentVariance * (UnityEngine.Random.value * 2f - 1f));
	}

	public static bool Approximately(this float input, float other, float epsilon = 0.001f)
	{
		return Mathf.Abs(input - other) < epsilon;
	}

	public static bool ApproximatelyDivisibleBy(this float input, float other, float epsilon = 0.001f)
	{
		float num = input % other;
		if (num >= other / 2f)
		{
			num = other - num;
		}
		return num.Approximately(0f, epsilon);
	}

	public static int ToIntBitwise(this float value)
	{
		UIntFloatUnion uIntFloatUnion = new UIntFloatUnion
		{
			floatValue = value
		};
		return uIntFloatUnion.intValue;
	}

	public static uint ToUIntBitwise(this float value)
	{
		UIntFloatUnion uIntFloatUnion = new UIntFloatUnion
		{
			floatValue = value
		};
		return uIntFloatUnion.uintValue;
	}

	public static float ToFloatBitwise(this int value)
	{
		UIntFloatUnion uIntFloatUnion = new UIntFloatUnion
		{
			intValue = value
		};
		return uIntFloatUnion.floatValue;
	}

	public static float ToFloatBitwise(this uint value)
	{
		UIntFloatUnion uIntFloatUnion = new UIntFloatUnion
		{
			uintValue = value
		};
		return uIntFloatUnion.floatValue;
	}

	public static ulong ToULongBitwise(this long value)
	{
		ULongDoubleUnion uLongDoubleUnion = new ULongDoubleUnion
		{
			longValue = value
		};
		return uLongDoubleUnion.ulongValue;
	}

	public static long ToLongBitwise(this ulong value)
	{
		ULongDoubleUnion uLongDoubleUnion = new ULongDoubleUnion
		{
			ulongValue = value
		};
		return uLongDoubleUnion.longValue;
	}

	public static string GetTimeValueString_SecondsToMMSSmS(this float value, int maxMinutes = 99)
	{
		int num = 60000;
		int num2 = maxMinutes * num + 59000 + 999;
		string text = ((value < 0f) ? "-" : "");
		value = Mathf.Abs(Mathf.FloorToInt(value * 1000f));
		value = Mathf.Min(num2, value);
		int num3 = Mathf.FloorToInt(value / (float)num);
		text = text + ((num3 < 10) ? "0" : "") + num3 + ":";
		value -= (float)(num3 * num);
		num3 = Mathf.FloorToInt(value / 1000f);
		text = text + ((num3 < 10) ? "0" : "") + num3 + ":";
		value -= (float)(num3 * 1000);
		num3 = Mathf.FloorToInt(value);
		return text + ((num3 < 100) ? "0" : "") + ((num3 < 10) ? "0" : "") + num3;
	}

	public static string GetTransformHeirarchyPath(this Transform transform)
	{
		string text = transform.name;
		while (transform.parent != null)
		{
			transform = transform.parent;
			text = transform.name + "/" + text;
		}
		return text;
	}

	public static IEnumerable<GameObject> EnumerateHierarchy(this GameObject go, bool thisObjectFirst = true, bool thisObjectLast = false)
	{
		if (thisObjectFirst)
		{
			yield return go;
			thisObjectLast = false;
		}
		for (int i = 0; i < go.transform.childCount; i++)
		{
			GameObject child = go.transform.GetChild(i).gameObject;
			yield return child;
			IEnumerable<GameObject> enumerable = child.EnumerateHierarchy(thisObjectFirst: false);
			foreach (GameObject item in enumerable)
			{
				yield return item;
			}
		}
		if (thisObjectLast)
		{
			yield return go;
		}
	}

	public static IEnumerable<GameObject> EnumerateParents(this GameObject go, bool thisObjectFirst = true)
	{
		if (thisObjectFirst)
		{
			yield return go;
		}
		while (go.transform.parent != null)
		{
			go = go.transform.parent.gameObject;
			yield return go;
		}
	}

	public static T GetComponentInParents<T>(this GameObject go, bool thisObjectFirst = false) where T : Component
	{
		return go.transform.GetComponentInParents<T>(thisObjectFirst);
	}

	public static bool IsTerrain(this GameObject go)
	{
		return go.layer == (int)Globals.inst.layerTerrain;
	}

	public static bool IsTerrain(this Collider collider)
	{
		if (!(collider is TerrainCollider))
		{
			return collider.gameObject.IsTerrain();
		}
		return true;
	}

	public static bool EditorSelectedSingle(this GameObject go)
	{
		return EditorHooks.SelectedObject == go;
	}

	public static bool EditorParentSelected(this GameObject go)
	{
		if (EditorHooks.SelectedObject == null)
		{
			return false;
		}
		return go.EnumerateParents().Contains(EditorHooks.SelectedObject);
	}

	public static bool EditorSelected(this GameObject go)
	{
		return EditorHooks.SelectionContains(go);
	}

	public static IEnumerable<Transform> EnumerateParents(this Transform t, bool thisObjectFirst = true)
	{
		if (thisObjectFirst)
		{
			yield return t;
		}
		while (t.parent != null)
		{
			t = t.parent;
			yield return t;
		}
	}

	public static Transform GetLowestCommonParent(this IEnumerable<Transform> transforms)
	{
		IEnumerable<Transform> enumerable = null;
		foreach (Transform transform in transforms)
		{
			enumerable = ((enumerable != null) ? enumerable.Where((Transform t) => transform.EnumerateParents().Contains(t)) : transform.EnumerateParents());
		}
		return enumerable.First();
	}

	public static void SetLocalPositionIfChanged(this Transform t, Vector3 newLocalPos, string profileTag = null, float epsilon = 0.01f)
	{
		Vector3 vector = t.localPosition - newLocalPos;
		if (!(Mathf.Abs(vector.x) + Mathf.Abs(vector.y) + Mathf.Abs(vector.z) < epsilon))
		{
			t.localPosition = newLocalPos;
		}
	}

	public static bool SetPositionIfChanged(this Transform t, Vector3 newWorldPos, string profileTag = null, float epsilon = 0.01f)
	{
		Vector3 vector = t.position - newWorldPos;
		if (Mathf.Abs(vector.x) + Mathf.Abs(vector.y) + Mathf.Abs(vector.z) >= epsilon)
		{
			t.position = newWorldPos;
			return true;
		}
		return false;
	}

	public static void SetLocalEulersIfChanged(this Transform t, Vector3 newLocalEulers, string profileTag = null, float epsilon = 0.0015f)
	{
		Vector3 vector = (t.localEulerAngles - newLocalEulers) * ((float)Math.PI / 180f) * 0.5f;
		if (!(Mathf.Abs(Mathf.Sin(vector.x) + Mathf.Sin(vector.y) + Mathf.Sin(vector.z)) < epsilon * 0.5f))
		{
			t.localRotation = Quaternion.Euler(newLocalEulers);
		}
	}

	public static void SetLocalRotationIfChanged(this Transform t, Quaternion newLocalRot, string profileTag = null, float epsilon = 0.001f)
	{
		Quaternion localRotation = t.localRotation;
		if (!(Mathf.Abs(localRotation.w - newLocalRot.w) + Mathf.Abs(localRotation.x - newLocalRot.x) + Mathf.Abs(localRotation.y - newLocalRot.y) + Mathf.Abs(localRotation.z - newLocalRot.z) < epsilon))
		{
			t.localRotation = newLocalRot;
		}
	}

	public static void SetEulersIfChanged(this Transform t, Vector3 newWorldEulers, string profileTag = null, float epsilon = 0.0015f)
	{
		Vector3 vector = (t.eulerAngles - newWorldEulers) * ((float)Math.PI / 180f) * 0.5f;
		if (!(Mathf.Abs(Mathf.Sin(vector.x) + Mathf.Sin(vector.y) + Mathf.Sin(vector.z)) < epsilon * 0.5f))
		{
			t.rotation = Quaternion.Euler(newWorldEulers);
		}
	}

	public static void SetRotationIfChanged(this Transform t, Quaternion newWorldRot, string profileTag = null, float epsilon = 0.001f)
	{
		Quaternion rotation = t.rotation;
		if (!(Mathf.Abs(rotation.w - newWorldRot.w) + Mathf.Abs(rotation.x - newWorldRot.x) + Mathf.Abs(rotation.y - newWorldRot.y) + Mathf.Abs(rotation.z - newWorldRot.z) < epsilon))
		{
			t.rotation = newWorldRot;
		}
	}

	public static bool SetPositionAndRotationIfChanged(this Transform t, Vector3 newWorldPos, Quaternion newWorldRot, string profileTag = null, float epsilon = 0.01f)
	{
		Vector3 vector = t.position - newWorldPos;
		bool flag = Mathf.Abs(vector.x) + Mathf.Abs(vector.y) + Mathf.Abs(vector.z) >= epsilon;
		Quaternion rotation = t.rotation;
		bool flag2 = Mathf.Abs(rotation.w - newWorldRot.w) + Mathf.Abs(rotation.x - newWorldRot.x) + Mathf.Abs(rotation.y - newWorldRot.y) + Mathf.Abs(rotation.z - newWorldRot.z) >= epsilon;
		if (flag || flag2)
		{
			if (!flag)
			{
				t.rotation = newWorldRot;
			}
			else if (!flag2)
			{
				t.position = newWorldPos;
			}
			else
			{
				t.SetPositionAndRotation(newWorldPos, newWorldRot);
			}
			return true;
		}
		return false;
	}

	public static void SetLocalScaleIfChanged(this Transform t, Vector3 newLocalScale, string profileTag = null, float epsilon = 0.001f)
	{
		Vector3 vector = t.localScale - newLocalScale;
		if (!(Mathf.Abs(vector.x) + Mathf.Abs(vector.y) + Mathf.Abs(vector.z) < epsilon))
		{
			t.localScale = newLocalScale;
		}
	}

	public static Quaternion InverseTransformRotation(this Transform transform, Quaternion worldRotation)
	{
		return Quaternion.Inverse(transform.rotation) * worldRotation;
	}

	public static Quaternion TransformRotation(this Transform transform, Quaternion localRotation)
	{
		return ((transform.parent != null) ? transform.parent.rotation : Quaternion.identity) * localRotation;
	}

	public static IEnumerable<T> GetComponentsInParents<T>(this Component c, bool thisFirst = true) where T : Component
	{
		Transform xform = (thisFirst ? c.transform : c.transform.parent);
		while ((bool)xform)
		{
			T component = xform.GetComponent<T>();
			if ((bool)component)
			{
				yield return component;
			}
			xform = xform.parent;
		}
	}

	public static IEnumerable<Component> AllComponentsIncludingNewlyAdded(this Component c)
	{
		Component[] initialComponents = c.GetComponents<Component>();
		Component[] array = initialComponents;
		for (int i = 0; i < array.Length; i++)
		{
			yield return array[i];
		}
		while (true)
		{
			array = c.GetComponents<Component>();
			if (array.Length <= initialComponents.Where((Component comp) => comp != null).Count())
			{
				break;
			}
			foreach (Component item in array.Where((Component comp) => !initialComponents.Contains(comp)))
			{
				yield return item;
			}
			initialComponents = array;
		}
		int childCount = c.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = c.transform.GetChild(i);
			foreach (Component item2 in child.AllComponentsIncludingNewlyAdded())
			{
				yield return item2;
			}
		}
	}

	public static Component MatchTagInThisOrParents(this Component c, string tag)
	{
		if (c.CompareTag(tag))
		{
			return c;
		}
		Transform parent = c.transform.parent;
		while ((bool)parent)
		{
			if (parent.CompareTag(tag))
			{
				return parent;
			}
			parent = parent.parent;
		}
		return null;
	}

	public static T GetComponentInParents<T>(this Component c, bool thisObjectFirst = false) where T : Component
	{
		Transform transform = (thisObjectFirst ? c.transform : c.transform.parent);
		while ((bool)transform)
		{
			T component = transform.GetComponent<T>();
			if ((bool)component)
			{
				return component;
			}
			transform = transform.parent;
		}
		return null;
	}

	public static Transform MatchTagInParents(this Transform t, string tag)
	{
		Transform transform = t;
		while ((bool)transform)
		{
			if (transform.CompareTag(tag))
			{
				return transform;
			}
			transform = transform.parent;
		}
		return null;
	}

	public static Transform GetTopParent(this Transform t)
	{
		Transform transform = t;
		while ((bool)transform.parent && transform.parent.gameObject.layer != (int)Globals.inst.layerContainer)
		{
			transform = transform.parent;
		}
		return transform;
	}

	public static IEnumerable<T> GetInterfaces<T>(this GameObject obj) where T : class
	{
		return obj.GetComponents<Component>().OfType<T>();
	}

	public static void CopyToClipboard(this string val)
	{
		GUIUtility.systemCopyBuffer = val;
	}

	public static bool EqualsNoCase(this string a, string b)
	{
		return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
	}

	public static bool NullOrEmpty(this string s)
	{
		return string.IsNullOrEmpty(s);
	}

	public static string SplitCamelCase(this string input)
	{
		return Regex.Replace(input, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
	}

	public static string JoinStrings(this string separator, IEnumerable<string> strings)
	{
		s_SharedStringBuilder.Length = 0;
		foreach (string @string in strings)
		{
			if (s_SharedStringBuilder.Length == 0)
			{
				s_SharedStringBuilder.Append(@string);
			}
			else
			{
				s_SharedStringBuilder.Append(separator).Append(@string);
			}
		}
		return s_SharedStringBuilder.ToString();
	}

	public static string JoinStrings(this string separator, IEnumerable<object> objects)
	{
		s_SharedStringBuilder.Length = 0;
		foreach (object @object in objects)
		{
			if (s_SharedStringBuilder.Length == 0)
			{
				s_SharedStringBuilder.Append((@object != null) ? @object.ToString() : "null");
			}
			else
			{
				s_SharedStringBuilder.Append(separator).Append((@object != null) ? @object.ToString() : "null");
			}
		}
		return s_SharedStringBuilder.ToString();
	}

	public static int GetDotNet3HashCode(this string str)
	{
		int num = 0;
		int num2 = str.Length - 1;
		int i;
		for (i = 0; i < num2; i += 2)
		{
			int num3 = (num << 5) - num + str[i];
			num = (num3 << 5) - num3 + str[i + 1];
		}
		if (i < str.Length)
		{
			num = (num << 5) - num + str[i];
		}
		return num;
	}

	public static Vector3 RandomizeSignedRange(this Vector3 v)
	{
		return new Vector3(UnityEngine.Random.Range(0f - v.x, v.x), UnityEngine.Random.Range(0f - v.y, v.y), UnityEngine.Random.Range(0f - v.z, v.z));
	}

	public static bool EqualsEpsilon(this Vector3 a, Vector3 b, float epsilon = 0.001f)
	{
		return Vector3.SqrMagnitude(a - b) < epsilon;
	}

	public static Vector3 FixPrecision(this Vector3 v)
	{
		return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
	}

	public static Vector3 SetX(this Vector3 input, float newX)
	{
		return new Vector3(newX, input.y, input.z);
	}

	public static Vector3 SetY(this Vector3 input, float newY)
	{
		return new Vector3(input.x, newY, input.z);
	}

	public static Vector3 SetZ(this Vector3 input, float newZ)
	{
		return new Vector3(input.x, input.y, newZ);
	}

	public static Vector3 Clamp(this Vector3 input, Vector3 min, Vector3 max)
	{
		return new Vector3(Mathf.Clamp(input.x, min.x, max.x), Mathf.Clamp(input.y, min.y, max.y), Mathf.Clamp(input.z, min.z, max.z));
	}

	public static Vector3 Clamp01(this Vector3 input)
	{
		return new Vector3(Mathf.Clamp01(input.x), Mathf.Clamp01(input.y), Mathf.Clamp01(input.z));
	}

	public static float Dot(this Vector3 a, Vector3 b)
	{
		return Vector3.Dot(a, b);
	}

	public static Vector3 Cross(this Vector3 a, Vector3 b)
	{
		return Vector3.Cross(a, b);
	}

	public static float PerpDotHoriz(this Vector3 a, Vector3 b)
	{
		return Vector3.Dot(a, new Vector3(b.z, b.y, 0f - b.x));
	}

	public static Vector2 FixPrecision(this Vector2 v)
	{
		return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
	}

	public static float Lerp(this Vector2 v, float t)
	{
		return Mathf.Lerp(v.x, v.y, t);
	}

	public static bool ContainsValueInRange(this Vector2 v, float value)
	{
		if (v.x <= v.y)
		{
			if (value >= v.x)
			{
				return value <= v.y;
			}
			return false;
		}
		if (value >= v.y)
		{
			return value <= v.x;
		}
		return false;
	}

	public static bool ContainsValueInRange(this Vector2 v, float value, out float closestValueInRange)
	{
		closestValueInRange = value;
		if (v.x <= v.y)
		{
			if (value >= v.x && value <= v.y)
			{
				return true;
			}
			closestValueInRange = ((value < v.x) ? v.x : v.y);
			return false;
		}
		if (value >= v.y && value <= v.x)
		{
			return true;
		}
		closestValueInRange = ((value < v.y) ? v.y : v.x);
		return false;
	}

	public static float InverseLerp(this Vector2 v, float val)
	{
		return Mathf.InverseLerp(v.x, v.y, val);
	}

	public static float Lerp(this Vector2Int v, float t)
	{
		return Mathf.Lerp(v.x, v.y, t);
	}

	public static float InverseLerp(this Vector2Int v, float val)
	{
		return Mathf.InverseLerp(v.x, v.y, val);
	}

	public static Vector2 InvertY(this Vector2 input, float yMax)
	{
		return new Vector2(input.x, yMax - input.y);
	}

	public static Vector2 ToScreenProportional(this Vector2 input)
	{
		return new Vector2(input.x / (float)Screen.width, input.y / (float)Screen.height);
	}

	public static Vector2 FromScreenProportional(this Vector2 input)
	{
		return new Vector2(input.x * (float)Screen.width, input.y * (float)Screen.height);
	}

	public static Vector2 Clamp(this Vector2 input, Vector2 min, Vector2 max)
	{
		return new Vector2(Mathf.Clamp(input.x, min.x, max.x), Mathf.Clamp(input.y, min.y, max.y));
	}

	public static bool IsInCircle(this Vector3 input, Vector3 point, float pointRadius, float thisRadius = 0f, bool ClampY = false)
	{
		Vector3 input2 = point - input;
		if (ClampY)
		{
			input2 = input2.SetY(0f);
		}
		float num = thisRadius + pointRadius;
		if (input2.sqrMagnitude < num * num)
		{
			return true;
		}
		return false;
	}

	public static float HorizontalAngle(this Vector3 v)
	{
		Vector3 normalized = new Vector3(v.x, 0f, v.z).normalized;
		float num = Mathf.Acos(Mathf.Clamp(Vector3.Dot(normalized, Vector3.forward), -1f, 1f)) * 57.29578f;
		if (Vector3.Dot(normalized, Vector3.right) < 0f)
		{
			num = 0f - num;
		}
		return num;
	}

	public static float NormalizedAngle(this float angle)
	{
		return angle - (Mathf.Ceil((angle + 180f) / 360f) - 1f) * 360f;
	}

	public static float DotClamped(this Vector3 v, Vector3 w)
	{
		return Mathf.Clamp(Vector3.Dot(v, w), -1f, 1f);
	}

	public static Vector3 RandomVariancePerAxis(this Vector3 v, float axisVarianceFactor)
	{
		return new Vector3(v.x + v.x * UnityEngine.Random.Range(0f - axisVarianceFactor, axisVarianceFactor), v.y + v.y * UnityEngine.Random.Range(0f - axisVarianceFactor, axisVarianceFactor), v.z + v.z * UnityEngine.Random.Range(0f - axisVarianceFactor, axisVarianceFactor));
	}

	public static Vector3 RandomVariancePerAxisProportional(this Vector3 v, float varianceFactor)
	{
		float magnitude = v.magnitude;
		return new Vector3(v.x + magnitude * UnityEngine.Random.Range(0f - varianceFactor, varianceFactor), v.y + magnitude * UnityEngine.Random.Range(0f - varianceFactor, varianceFactor), v.z + magnitude * UnityEngine.Random.Range(0f - varianceFactor, varianceFactor));
	}

	public static Vector3 RandomAngleOffset(this Vector3 v, float maxAngleDegrees)
	{
		Vector3 onUnitSphere = UnityEngine.Random.onUnitSphere;
		return Quaternion.AngleAxis(UnityEngine.Random.Range(0f - maxAngleDegrees, maxAngleDegrees), onUnitSphere) * v;
	}

	public static Vector3 RotatePointAroundPivot(this Vector3 point, Vector3 pivot, Vector3 angles)
	{
		Vector3 vector = point - pivot;
		vector = Quaternion.Euler(angles) * vector;
		point = vector + pivot;
		return point;
	}

	private static Vector3 NearestAxis(Vector3 v)
	{
		if (Mathf.Abs(v.x) > Mathf.Abs(v.y))
		{
			v.y = 0f;
			if (Mathf.Abs(v.x) > Mathf.Abs(v.z))
			{
				v.z = 0f;
				v.x = Mathf.Sign(v.x);
			}
			else
			{
				v.x = 0f;
				v.z = Mathf.Sign(v.z);
			}
		}
		else
		{
			v.x = 0f;
			if (Mathf.Abs(v.y) > Mathf.Abs(v.z))
			{
				v.z = 0f;
				v.y = Mathf.Sign(v.y);
			}
			else
			{
				v.y = 0f;
				v.z = Mathf.Sign(v.z);
			}
		}
		return v;
	}

	public static Vector3 AlignToAxis(this Vector3 vecToAlign, bool preserveLength = false)
	{
		float num = 0f;
		if (preserveLength)
		{
			num = vecToAlign.magnitude;
		}
		Vector3 result = NearestAxis(vecToAlign);
		if (preserveLength)
		{
			result *= num;
		}
		return result;
	}

	public static Vector3 AlignToAxis(this Vector3 vecToAlign, Quaternion refFrame, bool preserveLength = false)
	{
		float num = 0f;
		if (preserveLength)
		{
			num = vecToAlign.magnitude;
		}
		Vector3 vector = NearestAxis(Quaternion.Inverse(refFrame) * vecToAlign);
		Vector3 result = refFrame * vector;
		if (preserveLength)
		{
			result *= num;
		}
		return result;
	}

	public static Quaternion AlignToAxis(this Quaternion toAlign)
	{
		Vector3 forward = (toAlign * Vector3.forward).AlignToAxis();
		Vector3 upwards = (toAlign * Vector3.up).AlignToAxis();
		return Quaternion.LookRotation(forward, upwards);
	}

	public static Quaternion AlignToAxis(this Quaternion toAlign, Quaternion refFrame)
	{
		Vector3 forward = (toAlign * Vector3.forward).AlignToAxis(refFrame);
		Vector3 upwards = (toAlign * Vector3.up).AlignToAxis(refFrame);
		return Quaternion.LookRotation(forward, upwards);
	}

	public static Vector2 ToVector2XY(this in Vector3 coord)
	{
		return new Vector2(coord.x, coord.y);
	}

	public static Vector2 ToVector2XZ(this in Vector3 coord)
	{
		return new Vector2(coord.x, coord.z);
	}

	public static Vector3 ToVector3XZ(this in Vector2 coord, float y = 0f)
	{
		return new Vector3(coord.x, y, coord.y);
	}

	public static Vector3 ToVector3XY(this in Vector2 coord, float z = 0f)
	{
		return new Vector3(coord.x, coord.y, z);
	}

	public static IntVector2 ToVector2XZ(this in IntVector3 coord)
	{
		return new IntVector2(coord.x, coord.z);
	}

	public static IntVector3 ToVector3XZ(this in IntVector2 coord, int y = 0)
	{
		return new IntVector3(coord.x, y, coord.y);
	}

	public static bool IsNaN(this in Vector3 v)
	{
		if (!float.IsNaN(v.x) && !float.IsNaN(v.y))
		{
			return float.IsNaN(v.z);
		}
		return true;
	}

	public static bool IsNaN(this in Vector2 v)
	{
		if (!float.IsNaN(v.x))
		{
			return float.IsNaN(v.y);
		}
		return true;
	}

	public static bool Approximately(this in Vector3 v, in Vector3 other, float epsilon = 0.001f)
	{
		if (v.x.Approximately(other.x, epsilon) && v.y.Approximately(other.y, epsilon))
		{
			return v.z.Approximately(other.z, epsilon);
		}
		return false;
	}

	public static bool ApproxZero(this in Vector3 v)
	{
		if (Mathf.Approximately(v.x, 0f) && Mathf.Approximately(v.y, 0f))
		{
			return Mathf.Approximately(v.z, 0f);
		}
		return false;
	}

	public static bool IsZeroEpsilon(this in Vector3 v, float epsilon = 0.001f)
	{
		if (Mathf.Abs(v.x) < epsilon && Mathf.Abs(v.y) < epsilon)
		{
			return Mathf.Abs(v.z) < epsilon;
		}
		return false;
	}

	public static float Average(this in Vector3 v)
	{
		return (v.x + v.y + v.z) * (1f / 3f);
	}

	public static Quaternion ProjectToPlane(this Quaternion rotation, Vector3 planeNormal)
	{
		planeNormal.Normalize();
		return Quaternion.LookRotation(Vector3.ProjectOnPlane(rotation * Vector3.forward, planeNormal).normalized);
	}

	public static bool Overlaps(this RectTransform a, RectTransform b)
	{
		return a.WorldRect().Overlaps(b.WorldRect());
	}

	public static bool Overlaps(this RectTransform a, RectTransform b, bool allowInverse)
	{
		return a.WorldRect().Overlaps(b.WorldRect(), allowInverse);
	}

	public static Rect WorldRect(this RectTransform rectTransform)
	{
		if (_s_WrldRct_fourCornersArrray == null)
		{
			_s_WrldRct_fourCornersArrray = new Vector3[4];
		}
		rectTransform.GetWorldCorners(_s_WrldRct_fourCornersArrray);
		Vector2 vector = _s_WrldRct_fourCornersArrray[0];
		Vector2 vector2 = _s_WrldRct_fourCornersArrray[1];
		Vector2 vector3 = _s_WrldRct_fourCornersArrray[3];
		return new Rect(vector.x, vector.y, vector3.x - vector2.x, vector2.y - vector3.y);
	}

	public static Rect AlignXMin(this Rect rect, float x)
	{
		return new Rect(x, rect.yMin, rect.width, rect.height);
	}

	public static Rect AlignYMin(this Rect rect, float y)
	{
		return new Rect(rect.xMin, y, rect.width, rect.height);
	}

	public static Rect AlignXMax(this Rect rect, float x)
	{
		return new Rect(x - rect.width, rect.yMin, rect.width, rect.height);
	}

	public static Rect AlignYMax(this Rect rect, float y)
	{
		return new Rect(rect.xMin, y - rect.height, rect.width, rect.height);
	}

	public static Rect SplitLeft(this Rect rect, float absoluteWidth = 0f)
	{
		float width = ((absoluteWidth == 0f) ? (rect.width * 0.5f) : absoluteWidth);
		return new Rect(rect.x, rect.y, width, rect.height);
	}

	public static Rect SplitRight(this Rect rect, float absoluteWidth = 0f)
	{
		float num = ((absoluteWidth == 0f) ? (rect.width * 0.5f) : absoluteWidth);
		return new Rect(rect.x + (rect.width - num), rect.y, num, rect.height);
	}

	public static Rect SplitLeftRel(this Rect rect, float relativeWidth)
	{
		float width = rect.width * relativeWidth;
		return new Rect(rect.x, rect.y, width, rect.height);
	}

	public static Rect SplitRightRel(this Rect rect, float relativeWidth)
	{
		float num = rect.width * relativeWidth;
		return new Rect(rect.x + (rect.width - num), rect.y, num, rect.height);
	}

	public static Rect SplitTop(this Rect rect, float absoluteHeight = 0f)
	{
		float height = ((absoluteHeight == 0f) ? (rect.height * 0.5f) : absoluteHeight);
		return new Rect(rect.x, rect.y, rect.width, height);
	}

	public static Rect SplitBottom(this Rect rect, float absoluteHeight = 0f)
	{
		float num = ((absoluteHeight == 0f) ? (rect.height * 0.5f) : absoluteHeight);
		return new Rect(rect.x, rect.y + (rect.height - num), rect.width, num);
	}

	public static Rect SplitPartsHorizontal(this Rect rect, int numParts, int partIndex)
	{
		float num = rect.width / (float)numParts;
		return new Rect(rect.x + num * (float)partIndex, rect.y, num, rect.height);
	}

	public static Rect SplitPartsVertical(this Rect rect, int numParts, int partIndex)
	{
		float num = rect.height / (float)numParts;
		return new Rect(rect.x, rect.y + num * (float)partIndex, rect.width, num);
	}

	public static Rect Trim(this Rect rect, float trim)
	{
		return new Rect(rect.x + trim, rect.y, rect.width - trim - trim, rect.height);
	}

	public static Rect TrimFourSides(this Rect rect, float trim)
	{
		return new Rect(rect.x + trim, rect.y + trim, rect.width - trim - trim, rect.height - trim - trim);
	}

	public static Rect TrimLeft(this Rect rect, float trim)
	{
		return new Rect(rect.x + trim, rect.y, rect.width - trim, rect.height);
	}

	public static Rect TrimRight(this Rect rect, float trim)
	{
		return new Rect(rect.x, rect.y, rect.width - trim, rect.height);
	}

	public static Rect TrimTop(this Rect rect, float trim)
	{
		return new Rect(rect.x, rect.y + trim, rect.width, rect.height - trim);
	}

	public static Rect TrimBottom(this Rect rect, float trim)
	{
		return new Rect(rect.x, rect.y, rect.width, rect.height - trim);
	}

	public static Rect NudgeLeft(this Rect rect, float nudge)
	{
		return new Rect(rect.x - nudge, rect.y, rect.width, rect.height);
	}

	public static Rect NudgeRight(this Rect rect, float nudge)
	{
		return new Rect(rect.x + nudge, rect.y, rect.width, rect.height);
	}

	public static Rect NudgeUp(this Rect rect, float nudge)
	{
		return new Rect(rect.x, rect.y - nudge, rect.width, rect.height);
	}

	public static Rect NudgeDown(this Rect rect, float nudge)
	{
		return new Rect(rect.x, rect.y + nudge, rect.width, rect.height);
	}

	public static Vector2 Clamp(this Rect rect, Vector2 pos)
	{
		return new Vector2(Mathf.Clamp(pos.x, rect.xMin, rect.xMax), Mathf.Clamp(pos.y, rect.yMin, rect.yMax));
	}

	public static void Encapsulate(this ref Rect rect, Vector2 pos)
	{
		rect.xMin = Mathf.Min(rect.xMin, pos.x);
		rect.xMax = Mathf.Max(rect.xMax, pos.x);
		rect.yMin = Mathf.Min(rect.yMin, pos.y);
		rect.yMax = Mathf.Max(rect.yMax, pos.y);
	}

	public static float GetRadiusXZWorld(this Bounds bounds, Transform transform)
	{
		return new Vector2(bounds.extents.x, bounds.extents.z).magnitude * (transform.lossyScale.x + transform.lossyScale.y + transform.lossyScale.z) / 3f;
	}

	public static bool IntersectSphere(this Bounds bounds, Vector3 centre, float radius)
	{
		float num = radius * radius;
		if (centre.x < bounds.min.x)
		{
			num -= (centre.x - bounds.min.x) * (centre.x - bounds.min.x);
		}
		else if (centre.x > bounds.max.x)
		{
			num -= (centre.x - bounds.max.x) * (centre.x - bounds.max.x);
		}
		if (centre.y < bounds.min.y)
		{
			num -= (centre.y - bounds.min.y) * (centre.y - bounds.min.y);
		}
		else if (centre.y > bounds.max.y)
		{
			num -= (centre.y - bounds.max.y) * (centre.y - bounds.max.y);
		}
		if (centre.z < bounds.min.z)
		{
			num -= (centre.z - bounds.min.z) * (centre.z - bounds.min.z);
		}
		else if (centre.z > bounds.max.z)
		{
			num -= (centre.z - bounds.max.z) * (centre.z - bounds.max.z);
		}
		return num > 0f;
	}

	public static bool Contains(this Collider collider, Vector3 posWorld)
	{
		Type type = collider.GetType();
		if (type == typeof(SphereCollider))
		{
			return ((SphereCollider)collider).ContainsPoint(posWorld);
		}
		if (type == typeof(BoxCollider))
		{
			return ((BoxCollider)collider).ContainsPoint(posWorld);
		}
		Vector3 direction = collider.bounds.center - posWorld;
		float magnitude = direction.magnitude;
		RaycastHit hitInfo;
		return !collider.Raycast(new Ray(posWorld, direction), out hitInfo, magnitude);
	}

	public static bool OverlapsSphere(this Collider collider, Vector3 posWorld, float radius)
	{
		if (collider.GetType() == typeof(SphereCollider))
		{
			SphereCollider sphereCollider = (SphereCollider)collider;
			return Maths.SphereSphereOverlap(sphereCollider.transform.TransformPoint(sphereCollider.center), sphereCollider.radius, posWorld, radius);
		}
		return SphereBoxIntersection(posWorld, radius, collider);
	}

	public static bool SphereBoxIntersection(Vector3 posWorld, float radius, Collider collider)
	{
		Vector3 vector = collider.transform.InverseTransformPoint(posWorld);
		Vector3 vector2 = default(Vector3);
		vector2.x = ((vector.x < collider.bounds.min.x) ? collider.bounds.min.x : ((vector.x > collider.bounds.max.x) ? collider.bounds.max.x : vector.x));
		vector2.y = ((vector.y < collider.bounds.min.y) ? collider.bounds.min.y : ((vector.y > collider.bounds.max.y) ? collider.bounds.max.y : vector.y));
		vector2.z = ((vector.z < collider.bounds.min.z) ? collider.bounds.min.z : ((vector.z > collider.bounds.max.z) ? collider.bounds.max.z : vector.z));
		return (vector - vector2).sqrMagnitude < radius * radius;
	}

	public static bool ContainsPoint(this SphereCollider collider, Vector3 posWorld)
	{
		float num = collider.transform.lossyScale.x * collider.radius;
		return (collider.transform.position - posWorld).sqrMagnitude < num * num;
	}

	public static bool ContainsPoint(this BoxCollider collider, Vector3 posWorld)
	{
		Vector3 vector = collider.transform.InverseTransformPoint(posWorld);
		Vector3 vector2 = collider.size * 0.5f;
		Vector3 vector3 = collider.center - vector2;
		Vector3 vector4 = collider.center + vector2;
		if (vector.x > vector3.x && vector.x < vector4.x && vector.y > vector3.y && vector.y < vector4.y)
		{
			if (vector.z > vector3.z)
			{
				return vector.z < vector4.z;
			}
			return false;
		}
		return false;
	}

	public static float RadiusWorld(this SphereCollider collider)
	{
		return collider.transform.lossyScale.x * collider.radius;
	}

	public static float LengthToClosestPoint(this Ray ray, Vector3 point)
	{
		d.Assert(Mathf.Abs(ray.direction.magnitude - 1f) < 0.001f, "ray direction must be unit vector");
		return Vector3.Dot(point - ray.origin, ray.direction);
	}

	public static Vector3 ClosestPoint(this Ray ray, Vector3 point)
	{
		d.Assert(Mathf.Abs(ray.direction.magnitude - 1f) < 0.001f, "ray direction must be unit vector");
		return Vector3.Dot(point - ray.origin, ray.direction) * ray.direction + ray.origin;
	}

	public static float ClosestDistance(this Ray ray, Vector3 point)
	{
		d.Assert(Mathf.Abs(ray.direction.magnitude - 1f) < 0.001f, "ray direction must be unit vector");
		Vector3 vector = Vector3.Dot(point - ray.origin, ray.direction) * ray.direction + ray.origin;
		return (point - vector).magnitude;
	}

	public static Vector3 SampleNormal(this Terrain terrain, Vector3 worldPos)
	{
		d.Assert(condition: false, "Terrain.SampleNormal extension is not yet tested");
		Transform transform = terrain.transform;
		float num = (worldPos.x - transform.position.x) / terrain.terrainData.size.x;
		if (num < 0f || num > 1f)
		{
			return Vector3.zero;
		}
		float num2 = (worldPos.z - transform.position.z) / terrain.terrainData.size.y;
		if (num2 < 0f || num2 > 1f)
		{
			return Vector3.zero;
		}
		return terrain.terrainData.GetInterpolatedNormal(num, num2);
	}

	public static Color SetAlpha(this Color color, float alpha)
	{
		Color result = color;
		result.a = alpha;
		return result;
	}

	public static Color ScaleRGB(this Color color, float factor)
	{
		return new Color(color.r * factor, color.g * factor, color.b * factor, color.a);
	}

	public static Color ScaleRGBA(this Color color, float factor)
	{
		return new Color(color.r * factor, color.g * factor, color.b * factor, color.a * factor);
	}

	public static Color EqualiseRGB(this Color color, float percentage)
	{
		float b = (color.r + color.g + color.b) / 3f;
		return new Color(Mathf.Lerp(color.r, b, percentage), Mathf.Lerp(color.g, b, percentage), Mathf.Lerp(color.b, b, percentage), color.a);
	}

	public static Color Add(this Color color, Color other)
	{
		return new Color(color.r + other.r, color.g + other.g, color.b + other.b, color.a + other.a);
	}

	public static bool Equals(this Color col0, Color col1)
	{
		if (col0.a == col1.a && col0.r == col1.r && col0.g == col1.g)
		{
			return col0.b == col1.b;
		}
		return false;
	}

	public static void AddRandomVelocity(this Rigidbody rbody, Vector3 baseVelocity, Vector3 randomVelocity, float randomAngVel)
	{
		if ((bool)rbody)
		{
			rbody.velocity += baseVelocity + new Vector3(randomVelocity.x * (UnityEngine.Random.value * 2f - 1f), randomVelocity.y * (UnityEngine.Random.value * 2f - 1f), randomVelocity.z * (UnityEngine.Random.value * 2f - 1f));
			rbody.angularVelocity += new Vector3(randomAngVel * (UnityEngine.Random.value * 2f - 1f), randomAngVel * (UnityEngine.Random.value * 2f - 1f), randomAngVel * (UnityEngine.Random.value * 2f - 1f));
		}
	}

	public static void AddRandomForceAtPoint(this Rigidbody rbody, Vector3 baseVelocity, Vector3 randomVelocity, float randomAngVel, Vector3 position)
	{
		if ((bool)rbody)
		{
			Vector3 force = baseVelocity + new Vector3(randomVelocity.x * (UnityEngine.Random.value * 2f - 1f), randomVelocity.y * (UnityEngine.Random.value * 2f - 1f), randomVelocity.z * (UnityEngine.Random.value * 2f - 1f));
			Vector3 torque = new Vector3(randomAngVel * (UnityEngine.Random.value * 2f - 1f), randomAngVel * (UnityEngine.Random.value * 2f - 1f), randomAngVel * (UnityEngine.Random.value * 2f - 1f));
			rbody.AddForceAtPosition(force, position, ForceMode.VelocityChange);
			rbody.AddTorque(torque, ForceMode.VelocityChange);
		}
	}

	public static void AddRandomForce(this Rigidbody rbody, Vector3 baseVelocity, Vector3 randomVelocity, float randomAngVel)
	{
		if ((bool)rbody)
		{
			Vector3 force = baseVelocity + new Vector3(randomVelocity.x * (UnityEngine.Random.value * 2f - 1f), randomVelocity.y * (UnityEngine.Random.value * 2f - 1f), randomVelocity.z * (UnityEngine.Random.value * 2f - 1f));
			Vector3 torque = new Vector3(randomAngVel * (UnityEngine.Random.value * 2f - 1f), randomAngVel * (UnityEngine.Random.value * 2f - 1f), randomAngVel * (UnityEngine.Random.value * 2f - 1f));
			rbody.AddForce(force, ForceMode.Impulse);
			rbody.AddTorque(torque, ForceMode.Impulse);
		}
	}

	public static RangeFloat InputRange(this AnimationCurve curve)
	{
		return new RangeFloat(curve.keys.First().time, curve.keys.Last().time);
	}

	public static float FindNearestT(this AnimationCurve curve, float value, int iterations = 50)
	{
		float num = 1f / (float)iterations;
		float result = 0f;
		float num2 = float.MaxValue;
		for (float num3 = 0f; num3 < 1f; num3 += num)
		{
			float num4 = Mathf.Abs(curve.Evaluate(num3) - value);
			if (num4 == 0f)
			{
				result = num3;
				break;
			}
			if (num4 < num2)
			{
				result = num3;
				num2 = num4;
			}
		}
		return result;
	}

	public static void SetEmissionEnabled(this ParticleSystem particleSystem, bool enabled)
	{
		ParticleSystem.EmissionModule emission = particleSystem.emission;
		emission.enabled = enabled;
	}

	public static float GetConstantEmissionRate(this ParticleSystem particleSystem)
	{
		return particleSystem.emission.rateOverTime.constantMax;
	}

	public static void SetConstantEmissionRate(this ParticleSystem particleSystem, float emissionRate)
	{
		ParticleSystem.EmissionModule emission = particleSystem.emission;
		ParticleSystem.MinMaxCurve rateOverTime = emission.rateOverTime;
		rateOverTime.mode = ParticleSystemCurveMode.Constant;
		float constantMax = (rateOverTime.constantMin = emissionRate);
		rateOverTime.constantMax = constantMax;
		emission.rateOverTime = rateOverTime;
	}

	public static T GetDataForEncounter<T>(GameObject owner, string dataId)
	{
		if (owner == null)
		{
			return default(T);
		}
		Encounter component = owner.GetComponent<Encounter>();
		if (!component)
		{
			return default(T);
		}
		string encounterData = component.GetEncounterData(dataId);
		if (encounterData.NullOrEmpty())
		{
			return default(T);
		}
		T result;
		try
		{
			return JsonConvert.DeserializeObject<T>(encounterData);
		}
		catch (Exception ex)
		{
			d.LogError("Error loading encounter data '" + dataId + "' - " + ex);
			result = default(T);
		}
		return result;
	}

	public static void ArgumentNotNullOrEmpty(string value, string parameterName)
	{
		if (value == null)
		{
			throw new ArgumentNullException(parameterName);
		}
		if (value.Length == 0)
		{
			throw new ArgumentException($"'{parameterName}' cannot be empty.");
		}
	}

	public static void ArgumentTypeIsEnum(Type enumType, string parameterName)
	{
		ArgumentNotNull(enumType, "enumType");
		if (!enumType.IsEnum)
		{
			throw new ArgumentException($"'{parameterName}' cannot be empty.");
		}
	}

	public static void ArgumentNotNull(object value, string parameterName)
	{
		if (value == null)
		{
			throw new ArgumentNullException(parameterName);
		}
	}

	public static void Send<T>(this Action<T> action, T value)
	{
		action?.Invoke(value);
	}

	public static void Send<T, U>(this Action<T, U> action, T value1, U value2)
	{
		action?.Invoke(value1, value2);
	}

	public static void Send<T, U, V>(this Action<T, U, V> action, T value1, U value2, V value3)
	{
		action?.Invoke(value1, value2, value3);
	}

	public static bool IsNull(this UnityEngine.Object obj)
	{
		return (object)obj == null;
	}

	public static bool IsNotNull(this UnityEngine.Object obj)
	{
		return !obj.IsNull();
	}

	public static bool HasEffectiveAuthority(this NetworkIdentity netIdentity)
	{
		bool flag = netIdentity.hasAuthority;
		if (!flag && Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			flag = netIdentity.clientAuthorityOwner == null;
		}
		return flag;
	}

	public static void SetNavigationMode(this Selectable selectable, Navigation.Mode navigationMode)
	{
		Navigation navigation = selectable.navigation;
		navigation.mode = navigationMode;
		selectable.navigation = navigation;
	}

	public static void SetNavigationUp(this Selectable selectable, Selectable navTarget)
	{
		Navigation navigation = selectable.navigation;
		navigation.selectOnUp = navTarget;
		selectable.navigation = navigation;
	}

	public static void SetNavigationDown(this Selectable selectable, Selectable navTarget)
	{
		Navigation navigation = selectable.navigation;
		navigation.selectOnDown = navTarget;
		selectable.navigation = navigation;
	}

	public static void SetNavigationLeft(this Selectable selectable, Selectable navTarget)
	{
		Navigation navigation = selectable.navigation;
		navigation.selectOnLeft = navTarget;
		selectable.navigation = navigation;
	}

	public static void SetNavigationRight(this Selectable selectable, Selectable navTarget)
	{
		Navigation navigation = selectable.navigation;
		navigation.selectOnRight = navTarget;
		selectable.navigation = navigation;
	}

	public static Selectable GetNavigationUp(this Selectable selectable)
	{
		return selectable.navigation.selectOnUp;
	}

	public static Selectable GetNavigationDown(this Selectable selectable)
	{
		return selectable.navigation.selectOnDown;
	}

	public static Selectable GetNavigationLeft(this Selectable selectable)
	{
		return selectable.navigation.selectOnLeft;
	}

	public static Selectable GetNavigationRight(this Selectable selectable)
	{
		return selectable.navigation.selectOnRight;
	}

	public static string TryParseString(this JObject jObject, string propertyName, ref bool success)
	{
		if (jObject.TryGetValue(propertyName, out var value))
		{
			return value.ToObject<string>();
		}
		d.LogWarning("[Mods] Could not parse key " + propertyName + " from JSON object.");
		success = false;
		return null;
	}

	public static ModuleMeleeWeapon.FrameCollisionInfo TriggerToFrameCollisionInfo(this Collider otherCollider, Vector3 point)
	{
		Visible visible = Visible.FindVisibleUpwards(otherCollider);
		if (visible == null)
		{
			return ModuleMeleeWeapon.FrameCollisionInfo.clear;
		}
		return new ModuleMeleeWeapon.FrameCollisionInfo
		{
			TargetVisible = visible,
			Point = point,
			Normal = otherCollider.transform.position,
			OtherCol = otherCollider,
			Tank = ((visible.block != null) ? visible.block.tank : null),
			ImpulseMagnitude = 0f,
			WasStartOfCollision = false
		};
	}

	public static void CopyHashSetValues<T>(this HashSet<T> hashsetToPasteTo, in HashSet<T> hashsetToCopyFrom)
	{
		hashsetToPasteTo.Clear();
		foreach (T item in hashsetToCopyFrom)
		{
			hashsetToPasteTo.Add(item);
		}
	}

	public static void CopyKeyValuePairsFrom<T1, T2>(this Dictionary<T1, T2> dictToPasteTo, in Dictionary<T1, T2> dictToCopyFrom) where T2 : new()
	{
		dictToPasteTo.Clear();
		foreach (T1 key in dictToCopyFrom.Keys)
		{
			dictToPasteTo[key] = dictToCopyFrom[key];
		}
	}

	public static void WriteToBinaryFile<T>(this T obj, string filePath, bool append = false)
	{
		try
		{
			if (File.Exists(filePath))
			{
				File.Create(filePath).Dispose();
			}
			using Stream serializationStream = File.Open(filePath, append ? FileMode.Append : FileMode.Create);
			new BinaryFormatter().Serialize(serializationStream, obj);
		}
		catch (Exception arg)
		{
			d.LogError($"Failed to write to file at [{filePath}]\n{arg}");
		}
	}

	public static T ReadFromBinaryFile<T>(this T obj, string filePath)
	{
		try
		{
			if (File.Exists(filePath))
			{
				using Stream serializationStream = File.Open(filePath, FileMode.Open);
				obj = (T)new BinaryFormatter().Deserialize(serializationStream);
			}
			else
			{
				d.Log("Attempted to read file at [" + filePath + "] but no such file exists, aborting...");
				obj = default(T);
			}
		}
		catch (Exception arg)
		{
			d.LogError($"Failed to read from file at [{filePath}]\n{arg}");
			obj = default(T);
		}
		return obj;
	}
}
