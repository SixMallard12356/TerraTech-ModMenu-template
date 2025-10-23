#define UNITY_EDITOR
using System.Collections.Generic;

public sealed class ObjectPool : Singleton.Manager<ObjectPool>
{
	public class Pool
	{
		public string name;

		public int numAllocated;

		public List<object> freeList;
	}

	private Dictionary<int, Pool> poolLookup = new Dictionary<int, Pool>();

	private object m_ObjectBeingRecycled;

	private const int k_RecycleQueueSize = 2;

	private int m_RecycleQueueIndex;

	private List<object>[] m_DeferredRecycleQueue = new List<object>[2];

	public IEnumerable<string> EnumeratePoolUsage()
	{
		if (poolLookup == null)
		{
			yield return null;
		}
		foreach (KeyValuePair<int, Pool> item in poolLookup)
		{
			yield return $"{item.Value.name}: {item.Value.freeList.Count} / {item.Value.numAllocated}";
		}
	}

	public void Clear()
	{
		Singleton.Manager<ObjectPool>.inst.poolLookup.Clear();
	}

	public Pool InitPool<T>(int initialSize = 0) where T : class, new()
	{
		int hashCode = typeof(T).GetHashCode();
		if (!Singleton.Manager<ObjectPool>.inst.poolLookup.ContainsKey(hashCode))
		{
			string arg = typeof(T).Name;
			d.Log($"initialising pool of {arg} at size {initialSize}");
			List<object> list = new List<object>();
			for (int i = 0; i < initialSize; i++)
			{
				list.Add(new T());
			}
			Pool pool = new Pool
			{
				name = arg,
				numAllocated = initialSize,
				freeList = list
			};
			Singleton.Manager<ObjectPool>.inst.poolLookup.Add(hashCode, pool);
			return pool;
		}
		return null;
	}

	public T Spawn<T>() where T : class, new()
	{
		int hashCode = typeof(T).GetHashCode();
		if (!Singleton.Manager<ObjectPool>.inst.poolLookup.TryGetValue(hashCode, out var value))
		{
			value = InitPool<T>(1);
		}
		if (value.freeList.Count == 0)
		{
			value.numAllocated++;
			return new T();
		}
		T obj = value.freeList[value.freeList.Count - 1] as T;
		value.freeList.RemoveAt(value.freeList.Count - 1);
		d.Assert(obj != null);
		return obj;
	}

	public void Recycle<T>(T obj, bool recursed = false) where T : class, new()
	{
		if (m_ObjectBeingRecycled != null)
		{
			m_DeferredRecycleQueue[m_RecycleQueueIndex].Add(obj);
			return;
		}
		m_ObjectBeingRecycled = obj;
		int hashCode = typeof(T).GetHashCode();
		if (!Singleton.Manager<ObjectPool>.inst.poolLookup.TryGetValue(hashCode, out var value))
		{
			d.LogError($"Recycling non-pooled object {obj} ({typeof(T)}) failed");
			m_ObjectBeingRecycled = null;
			return;
		}
		value.freeList.Add(obj);
		m_ObjectBeingRecycled = null;
		if (recursed)
		{
			return;
		}
		int num = 0;
		while (m_DeferredRecycleQueue[m_RecycleQueueIndex].Count != 0)
		{
			List<object> list = m_DeferredRecycleQueue[m_RecycleQueueIndex];
			m_RecycleQueueIndex = (m_RecycleQueueIndex + 1) % 2;
			foreach (object item in list)
			{
				Recycle(item, recursed: true);
			}
			list.Clear();
			num++;
		}
	}

	public int Count<T>() where T : class, new()
	{
		if (Singleton.Manager<ObjectPool>.inst.poolLookup.TryGetValue(typeof(T).GetHashCode(), out var value))
		{
			return value.freeList.Count;
		}
		return 0;
	}
}
