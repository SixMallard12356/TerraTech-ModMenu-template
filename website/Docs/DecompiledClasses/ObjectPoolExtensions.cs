public static class ObjectPoolExtensions
{
	public static void InitPool<T>(int initialSize = 0) where T : class, new()
	{
		Singleton.Manager<ObjectPool>.inst.InitPool<T>(initialSize);
	}

	public static T SpawnPooled<T>() where T : class, new()
	{
		return Singleton.Manager<ObjectPool>.inst.Spawn<T>();
	}

	public static void RecyclePooled<T>(this T obj, bool recursed = false) where T : class, new()
	{
		Singleton.Manager<ObjectPool>.inst.Recycle(obj, recursed);
	}

	public static int CountFreePooled<T>() where T : class, new()
	{
		return Singleton.Manager<ObjectPool>.inst.Count<T>();
	}
}
