using System.Collections;
using UnityEngine;

public static class ComponentPoolExtensions
{
	public static void CreatePool<T>(this T prefab, int initialSize) where T : Component
	{
		PoolInitTable.PoolSpec poolSpec = new PoolInitTable.PoolSpec(prefab, prefab.name, initialSize);
		IEnumerator enumerator = Singleton.Manager<ComponentPool>.inst.InitPool(prefab, poolSpec, null, initialSize);
		while (enumerator.MoveNext())
		{
		}
	}

	public static void DeletePool<T>(this T prefab) where T : Component
	{
		Singleton.Manager<ComponentPool>.inst.RemovePool(prefab);
	}

	public static T Spawn<T>(this T prefab) where T : Component
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, null, Vector3.zero, Quaternion.identity) as T;
	}

	public static T Spawn<T>(this T prefab, Vector3 position) where T : Component
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, null, position, Quaternion.identity) as T;
	}

	public static T Spawn<T>(this T prefab, Vector3 position, Quaternion rotation) where T : Component
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, null, position, rotation) as T;
	}

	public static T Spawn<T>(this T prefab, Vector3 position, Quaternion rotation, float scale) where T : Component
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, null, position, rotation, scale) as T;
	}

	public static T Spawn<T>(this T prefab, Transform parent) where T : Component
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, parent, Vector3.zero, Quaternion.identity) as T;
	}

	public static T Spawn<T>(this T prefab, Transform parent, Vector3 position) where T : Component
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, parent, position, Quaternion.identity) as T;
	}

	public static T Spawn<T>(this T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : Component
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, parent, position, rotation) as T;
	}

	public static T Spawn<T>(this T prefab, Transform parent, Vector3 position, Quaternion rotation, float scale) where T : Component
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, parent, position, rotation, scale) as T;
	}

	public static T Spawn<T>(this T prefab, Transform parent, Vector3 position, Quaternion rotation, float scale, bool worldPositionStays) where T : Component
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, parent, position, rotation, scale, worldPositionStays) as T;
	}

	public static Transform Spawn(this Transform prefab)
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, null, Vector3.zero, Quaternion.identity);
	}

	public static Transform Spawn(this Transform prefab, Vector3 position)
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, null, position, Quaternion.identity);
	}

	public static Transform Spawn(this Transform prefab, Vector3 position, Quaternion rotation)
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, null, position, rotation);
	}

	public static Transform Spawn(this Transform prefab, Transform parent)
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, parent, Vector3.zero, Quaternion.identity);
	}

	public static Transform Spawn(this Transform prefab, Transform parent, Vector3 position)
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, parent, position, Quaternion.identity);
	}

	public static Transform Spawn(this Transform prefab, Transform parent, Vector3 position, Quaternion rotation)
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, parent, position, rotation);
	}

	public static Transform Spawn(this Transform prefab, Transform parent, Vector3 position, Quaternion rotation, float scale)
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, parent, position, rotation, scale);
	}

	public static Transform SpawnWithLocalTransform(this Transform prefab, Transform parent, Vector3 localPosition, Quaternion localRotation)
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, parent, localPosition, localRotation, 1f, worldPositionStays: false);
	}

	public static Transform SpawnWithLocalTransform(this Transform prefab, Transform parent)
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, parent, Vector3.zero, Quaternion.identity, 1f, worldPositionStays: false);
	}

	public static T SpawnWithLocalTransform<T>(this T prefab, Transform parent) where T : Component
	{
		return Singleton.Manager<ComponentPool>.inst.Spawn(prefab, parent, Vector3.zero, Quaternion.identity, 1f, worldPositionStays: false) as T;
	}

	public static void Recycle<T>(this T obj, bool worldPosStays = true) where T : Component
	{
		Singleton.Manager<ComponentPool>.inst.Recycle(obj, worldPosStays);
	}

	public static bool IsBeingRecycled<T>(this T obj) where T : Component
	{
		return Singleton.Manager<ComponentPool>.inst.ComponentBeingRecycled == obj;
	}

	public static T GetOriginalPrefab<T>(this T obj) where T : Component
	{
		return Singleton.Manager<ComponentPool>.inst.GetOriginalPrefab(obj);
	}

	public static T UnpooledSpawn<T>(this T prefab, Transform parent = null, bool worldPosStays = true) where T : Component
	{
		return Singleton.Manager<ComponentPool>.inst.UnpooledSpawn(prefab, parent, worldPosStays);
	}

	public static T UnpooledSpawn<T>(this T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : Component
	{
		return Singleton.Manager<ComponentPool>.inst.UnpooledSpawn(prefab, parent, position, rotation);
	}

	public static Transform UnpooledSpawnWithLocalTransform(this Transform prefab, Transform parent, Vector3 localPosition, Quaternion localRotation)
	{
		return Singleton.Manager<ComponentPool>.inst.UnpooledSpawn(prefab, parent, localPosition, localRotation, worldPosStays: false);
	}

	public static T GetPoolTemplate<T>(this T prefab) where T : Component
	{
		ComponentPool.Pool pool = Singleton.Manager<ComponentPool>.inst.LookupPool(prefab);
		if (pool != null)
		{
			return pool.template as T;
		}
		return null;
	}
}
