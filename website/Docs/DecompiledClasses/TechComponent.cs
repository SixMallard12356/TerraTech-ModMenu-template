using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class TechComponent : MonoBehaviour
{
	[Serializable]
	public abstract class SerialData
	{
	}

	[Serializable]
	public abstract class SerialData<T> : SerialData where T : SerialData
	{
		private static readonly int k_TypeHash = HashCodeUtility.GetPersistentHashCode(typeof(T).ToString());

		public void Store(Dictionary<int, SerialData> dict)
		{
			dict[k_TypeHash] = this;
		}

		public static void Remove(Dictionary<int, SerialData> dict)
		{
			dict.Remove(k_TypeHash);
		}

		public static T Retrieve(Dictionary<int, SerialData> dict)
		{
			SerialData value = null;
			dict.TryGetValue(k_TypeHash, out value);
			return value as T;
		}
	}

	[SerializeField]
	[HideInInspector]
	private Tank _tech;

	public Tank Tech => _tech;

	private void PrePool()
	{
		_tech = GetComponent<Tank>();
	}

	private void DePool()
	{
		_tech = null;
	}
}
