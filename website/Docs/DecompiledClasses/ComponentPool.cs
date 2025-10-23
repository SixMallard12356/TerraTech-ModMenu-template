#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using DevCommands;
using UnityEngine;

public sealed class ComponentPool : Singleton.Manager<ComponentPool>
{
	public interface IPoolMetrics
	{
		int nSpawned { get; }

		int nFree { get; }

		int nHighWater { get; }
	}

	public class Pool : IPoolMetrics
	{
		public struct Poolable
		{
			public Component component;

			public Component prefab;

			public event Action OnSpawn;

			public event Action OnRecycle;

			public event Action OnDepool;

			public void SpawnEvent()
			{
				if (this.OnSpawn != null)
				{
					this.OnSpawn();
				}
			}

			public void RecycleEvent()
			{
				if (this.OnRecycle != null)
				{
					this.OnRecycle();
				}
			}

			public void DepoolEvent()
			{
				if (this.OnDepool != null)
				{
					this.OnDepool();
				}
			}
		}

		public int numSpawned;

		public int highWaterMark;

		public Component template;

		public Component originalPrefab;

		public List<Poolable> freeList;

		public PoolInitTable.PoolSpec poolSpec;

		public int memoryPerInstance;

		public int heapPos;

		public float depoolHeapPriority;

		public float instanciationTime;

		public int CategoryID { get; private set; }

		public int nSpawned => numSpawned;

		public int nFree => freeList.Count;

		public int nHighWater => highWaterMark;

		public float Utilisation
		{
			get
			{
				if ((float)nFree != 0f)
				{
					return (float)nSpawned / ((float)nFree + (float)nSpawned);
				}
				return 0f;
			}
		}

		public float MemoryUsage => memoryPerInstance * numSpawned;

		public float MemoryAllocated => memoryPerInstance * (nFree + numSpawned);

		public Pool(PoolInitTable.PoolSpec spec, Component templatePrefab, Component originalPrefab, int initialSize)
		{
			numSpawned = 0;
			highWaterMark = 0;
			freeList = new List<Poolable>((initialSize <= 0) ? spec.size : initialSize);
			poolSpec = spec;
			poolSpec.poolMetrics = this;
			CategoryID = originalPrefab.gameObject.layer;
			template = templatePrefab;
			this.originalPrefab = originalPrefab;
			memoryPerInstance = EstimateMemoryForPrefab(templatePrefab.gameObject);
		}

		public void UpdateFreePriority()
		{
			if (numSpawned == 0)
			{
				depoolHeapPriority = freeList.Count * memoryPerInstance;
			}
			else
			{
				depoolHeapPriority = (float)freeList.Count / (float)numSpawned;
			}
		}
	}

	private struct PoolFuncs
	{
		public Action<Component>[] prePool;

		public Action<Component>[] onPool;

		public Action<Component>[] onDepool;

		public Action<Component>[] onSpawn;

		public Action<Component>[] onRecycle;
	}

	private class ItemToReturnToPool
	{
		public Pool.Poolable m_Poolable;

		public int m_ItemHash;
	}

	private class PrefabMapping
	{
		private Dictionary<Component, Component> m_AssetToRuntime = new Dictionary<Component, Component>();

		private Dictionary<Component, Component> m_RuntimeToAsset = new Dictionary<Component, Component>();

		public void Add(Component runtime, Component asset)
		{
			if (runtime == null)
			{
				d.LogError("ERROR: PrefabMapping.Add given null runtime prefab");
				return;
			}
			if (asset == null)
			{
				d.LogError("ERROR: PrefabMapping.Add given null asset prefab");
				return;
			}
			if (m_AssetToRuntime.ContainsKey(asset))
			{
				d.LogError($"ERROR: PrefabMapping.Add failed to add prefab {asset.name} because it is already registered");
				return;
			}
			m_AssetToRuntime[asset] = runtime;
			m_RuntimeToAsset[runtime] = asset;
		}

		public Component LookupAsset(Component runtime)
		{
			m_RuntimeToAsset.TryGetValue(runtime, out var value);
			return value;
		}

		public Component LookupRuntime(Component asset)
		{
			m_AssetToRuntime.TryGetValue(asset, out var value);
			return value;
		}
	}

	[SerializeField]
	private PoolInitTable initTable;

	[SerializeField]
	private PoolInitTable initTableConsoles;

	[SerializeField]
	private int m_MaximumPoolMemory;

	[SerializeField]
	private int m_AutoTrimPoolsThreshold;

	[SerializeField]
	private int m_AutoTrimPoolsMaxDepoolPerFrame;

	public Event<Component> OnPoolTemplatePrepared;

	private static PoolingAnalysisTool s_PoolingAnalyser = null;

	private Transform m_PoolContainer;

	private Transform m_TemplateContainer;

	private int m_PoolMemoryUsed;

	private int m_PoolMemoryUsedForFreeLists;

	private Dictionary<int, Pool> m_PoolLookup = new Dictionary<int, Pool>();

	private Dictionary<int, Pool.Poolable> m_SpawnedItemLookup = new Dictionary<int, Pool.Poolable>();

	private List<Pool> m_DepoolHeap = new List<Pool>();

	private const int k_RecycleQueueSize = 2;

	private int m_RecycleQueueIndex;

	private List<Component>[] m_DeferredRecycleQueue = new List<Component>[2];

	private const int k_ReturnToPoolSize = 10;

	private int m_ReturnToPoolIndex;

	private List<ItemToReturnToPool> m_ItemsToReturnToPool = new List<ItemToReturnToPool>(10);

	private Component m_CurrentPrefabDuringInitPool;

	private int m_ItemBeingSpawnedId;

	private bool m_SerializeChanges;

	private float m_poolInstanciationTimer;

	private static MethodInfo s_genericCreateDelegateHelper;

	private static List<MethodInfo> s_PoolInheritanceChainWorking = new List<MethodInfo>();

	private static Queue<List<Action>> s_DelegatesWorkingListPool = new Queue<List<Action>>(4);

	private static Dictionary<int, PoolFuncs> s_PoolFuncsPerType = new Dictionary<int, PoolFuncs>();

	private PrefabMapping m_StdPrefabMap = new PrefabMapping();

	private PrefabMapping m_NetPrefabMap = new PrefabMapping();

	private static List<Component> s_NoAllocGetComponents = new List<Component>(64);

	public Component ComponentBeingRecycled { get; private set; }

	public bool IsShuttingDown { get; private set; }

	public bool DisableInitPools { get; set; }

	public PoolInitTable InitTable => initTable;

	public static PoolingAnalysisTool PoolingAnalyser => s_PoolingAnalyser;

	public Dictionary<int, Pool> PoolLookup => m_PoolLookup;

	public static void GeneratePoolingAnalysisTool()
	{
		if (s_PoolingAnalyser == null)
		{
			s_PoolingAnalyser = Singleton.Manager<ComponentPool>.inst.gameObject.AddComponent(typeof(PoolingAnalysisTool)) as PoolingAnalysisTool;
		}
	}

	public static void DestroyPoolingAnalysisTool()
	{
		if (s_PoolingAnalyser != null)
		{
			UnityEngine.Object.Destroy(s_PoolingAnalyser);
			s_PoolingAnalyser = null;
		}
	}

	private static int EstimateMemoryForComponent(Component c)
	{
		if (c is Transform)
		{
			if (!(c is RectTransform))
			{
				return 840;
			}
			return 900;
		}
		if (c is MeshFilter)
		{
			return 250;
		}
		if (c is Renderer)
		{
			return 700;
		}
		if (c is Rigidbody)
		{
			return 700;
		}
		if (c is Collider)
		{
			return 200;
		}
		if (c is ParticleSystem)
		{
			return 11000;
		}
		if (c is Light)
		{
			return 1100;
		}
		return 350;
	}

	private static int EstimateMemoryForGameObject(GameObject g)
	{
		int num = 0;
		g.GetComponentsInChildren(includeInactive: true, s_NoAllocGetComponents);
		foreach (Component s_NoAllocGetComponent in s_NoAllocGetComponents)
		{
			num += EstimateMemoryForComponent(s_NoAllocGetComponent);
		}
		s_NoAllocGetComponents.Clear();
		return num;
	}

	public static int EstimateMemoryForPrefab(GameObject g)
	{
		return EstimateMemoryForGameObject(g) * 2;
	}

	public IEnumerator InitPoolsFromTable(Func<bool> ShouldYieldFn)
	{
		_ = Time.frameCount;
		_ = Time.realtimeSinceStartup;
		if (!DisableInitPools)
		{
			bool isNetSpecList = false;
			IEnumerator initSpecListEnum = InitPoolsFromSpecList(isNetSpecList, ShouldYieldFn);
			while (initSpecListEnum.MoveNext())
			{
				yield return null;
			}
			isNetSpecList = true;
			initSpecListEnum = InitPoolsFromSpecList(isNetSpecList, ShouldYieldFn);
			while (initSpecListEnum.MoveNext())
			{
				yield return null;
			}
		}
	}

	public void RegisterStandardRuntimePrefab(Component runtimePrefab, Component assetPrefab)
	{
		m_StdPrefabMap.Add(runtimePrefab, assetPrefab);
	}

	public void RegisterNetworkRuntimePrefab(Component runtimePrefab, Component assetPrefab)
	{
		m_NetPrefabMap.Add(runtimePrefab, assetPrefab);
	}

	private IEnumerator InitPoolsFromSpecList(bool isNetSpecList, Func<bool> ShouldYieldFn)
	{
		List<PoolInitTable.PoolSpec> specList = (isNetSpecList ? InitTable.netPoolSpecs : InitTable.poolSpecs);
		int totalPoolSizeWithRequestedSizes = 0;
		List<Pool> newPools = new List<Pool>(specList.Count);
		for (int i = specList.Count - 1; i >= 0; i--)
		{
			PoolInitTable.PoolSpec spec = specList[i];
			Component component = null;
			if (spec.assetPrefab != null)
			{
				if (isNetSpecList)
				{
					component = m_NetPrefabMap.LookupRuntime(spec.assetPrefab);
					if (component == null)
					{
						d.LogError("Unable to find networked prefab for original " + spec.assetPrefab.name);
					}
				}
				else
				{
					component = m_StdPrefabMap.LookupRuntime(spec.assetPrefab) ?? spec.assetPrefab;
				}
			}
			if ((bool)component && spec.size != 0 && !m_PoolLookup.ContainsKey(component.GetInstanceID()))
			{
				Pool pool = CreateEmptyPool(component, spec, spec.size);
				if (ShouldYieldFn != null && ShouldYieldFn())
				{
					yield return null;
				}
				totalPoolSizeWithRequestedSizes += spec.size * pool.memoryPerInstance;
				newPools.Add(pool);
			}
			else
			{
				specList.RemoveAt(i);
			}
		}
		float poolInitSizeMultiplier = 1f;
		if (m_MaximumPoolMemory > 0)
		{
			int num = m_MaximumPoolMemory - m_PoolMemoryUsed - m_AutoTrimPoolsThreshold;
			if (totalPoolSizeWithRequestedSizes > num && totalPoolSizeWithRequestedSizes > 0)
			{
				poolInitSizeMultiplier = Mathf.Max(0f, (float)num / (float)totalPoolSizeWithRequestedSizes);
				d.LogWarning("Requested pool sizes don't fit in memory. Reducing to multiplier of " + poolInitSizeMultiplier);
			}
		}
		for (int i = 0; i < newPools.Count; i++)
		{
			Pool pool2 = newPools[i];
			int initialSize = Mathf.FloorToInt(poolInitSizeMultiplier * (float)pool2.poolSpec.size);
			IEnumerator initPoolEnum = ReservePoolSize(pool2, ShouldYieldFn, initialSize);
			while (initPoolEnum.MoveNext())
			{
				yield return null;
			}
		}
		d.Log("Initial component pool memory = " + m_PoolMemoryUsed + " / " + m_MaximumPoolMemory + " (pool size multiplier = " + poolInitSizeMultiplier + ")");
	}

	public Pool LookupPool(Component prefab)
	{
		Pool value = null;
		if (m_PoolLookup.Count == 0)
		{
			d.LogError("Spawned before initialising Component Pools: " + prefab.name);
		}
		int instanceID = prefab.GetInstanceID();
		if (!m_PoolLookup.TryGetValue(instanceID, out value))
		{
			Component component = m_NetPrefabMap.LookupAsset(prefab);
			List<PoolInitTable.PoolSpec> list;
			if (component != null)
			{
				list = InitTable.netPoolSpecs;
			}
			else
			{
				component = m_StdPrefabMap.LookupAsset(prefab) ?? prefab;
				list = InitTable.poolSpecs;
			}
			PoolInitTable.PoolSpec poolSpec = new PoolInitTable.PoolSpec(component, component.name, 1);
			if (!DisableInitPools)
			{
				list.Add(poolSpec);
			}
			value = CreateEmptyPool(prefab, poolSpec, 1);
		}
		return value;
	}

	private Action<Component>[] GetDelegates(Type type, string methodName)
	{
		s_PoolInheritanceChainWorking.Clear();
		Type type2 = type;
		do
		{
			MethodInfo method = type2.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (method != null && method.GetParameters().Length == 0 && method.ReturnType == typeof(void))
			{
				s_PoolInheritanceChainWorking.Add(method);
			}
			type2 = type2.BaseType;
		}
		while (type2 != null && type2 != typeof(Component));
		int count = s_PoolInheritanceChainWorking.Count;
		Action<Component>[] array = new Action<Component>[count];
		for (int i = 0; i < count; i++)
		{
			MethodInfo methodInfo = s_PoolInheritanceChainWorking[count - 1 - i];
			if (s_genericCreateDelegateHelper == null)
			{
				s_genericCreateDelegateHelper = typeof(ComponentPool).GetMethod("CreateDelegateHelper", BindingFlags.Static | BindingFlags.NonPublic);
			}
			MethodInfo methodInfo2 = s_genericCreateDelegateHelper.MakeGenericMethod(methodInfo.DeclaringType);
			array[i] = (Action<Component>)methodInfo2.Invoke(null, new object[1] { methodInfo });
		}
		return array;
	}

	private static Action<Component> CreateDelegateHelper<T>(MethodInfo method) where T : class
	{
		Action<T> action = (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), method);
		return delegate(Component component)
		{
			action(component as T);
		};
	}

	private PoolFuncs InterrogatePoolInterface(Component component)
	{
		Type type = component.GetType();
		if (!s_PoolFuncsPerType.TryGetValue(type.GetHashCode(), out var value))
		{
			value.prePool = GetDelegates(type, "PrePool");
			value.onPool = GetDelegates(type, "OnPool");
			value.onDepool = GetDelegates(type, "OnDepool");
			value.onSpawn = GetDelegates(type, "OnSpawn");
			value.onRecycle = GetDelegates(type, "OnRecycle");
			s_PoolFuncsPerType[type.GetHashCode()] = value;
		}
		return value;
	}

	private bool PreparePoolTemplate(Component templatePrefab)
	{
		foreach (Component item in templatePrefab.AllComponentsIncludingNewlyAdded())
		{
			PoolFuncs poolFuncs = InterrogatePoolInterface(item);
			if (poolFuncs.prePool != null)
			{
				Action<Component>[] prePool = poolFuncs.prePool;
				for (int i = 0; i < prePool.Length; i++)
				{
					prePool[i](item);
				}
			}
		}
		return true;
	}

	private Pool.Poolable InitPoolable(Component item, Component prefab)
	{
		Pool.Poolable result = new Pool.Poolable
		{
			component = item,
			prefab = prefab
		};
		if (s_DelegatesWorkingListPool.Count < 2)
		{
			for (int i = s_DelegatesWorkingListPool.Count; i < 2; i++)
			{
				s_DelegatesWorkingListPool.Enqueue(new List<Action>());
			}
		}
		List<Action> list = s_DelegatesWorkingListPool.Dequeue();
		List<Action> list2 = s_DelegatesWorkingListPool.Dequeue();
		m_CurrentPrefabDuringInitPool = prefab;
		Component[] componentsInChildren = item.GetComponentsInChildren<Component>(includeInactive: true);
		foreach (Component component in componentsInChildren)
		{
			PoolFuncs poolFuncs = InterrogatePoolInterface(component);
			Action<Component>[] onPool;
			if (poolFuncs.onPool != null)
			{
				onPool = poolFuncs.onPool;
				for (int k = 0; k < onPool.Length; k++)
				{
					onPool[k](component);
				}
			}
			Component closuredComponent = component;
			onPool = poolFuncs.onSpawn;
			foreach (Action<Component> action in onPool)
			{
				Action<Component> closuredDelegate = action;
				result.OnSpawn += delegate
				{
					closuredDelegate(closuredComponent);
				};
			}
			onPool = poolFuncs.onRecycle;
			foreach (Action<Component> action2 in onPool)
			{
				Action<Component> closuredDelegate2 = action2;
				list.Add(delegate
				{
					closuredDelegate2(closuredComponent);
				});
			}
			onPool = poolFuncs.onDepool;
			foreach (Action<Component> action3 in onPool)
			{
				Action<Component> closuredDelegate3 = action3;
				list2.Add(delegate
				{
					closuredDelegate3(closuredComponent);
				});
			}
		}
		m_CurrentPrefabDuringInitPool = null;
		for (int num = list.Count - 1; num >= 0; num--)
		{
			result.OnRecycle += list[num];
		}
		for (int num2 = list2.Count - 1; num2 >= 0; num2--)
		{
			result.OnDepool += list2[num2];
		}
		list.Clear();
		s_DelegatesWorkingListPool.Enqueue(list);
		list2.Clear();
		s_DelegatesWorkingListPool.Enqueue(list2);
		return result;
	}

	private Pool CreateEmptyPool(Component prefab, PoolInitTable.PoolSpec poolSpec, int initialSize)
	{
		Component component = UnityEngine.Object.Instantiate(prefab);
		component.name = prefab.name;
		m_CurrentPrefabDuringInitPool = prefab;
		if (!PreparePoolTemplate(component))
		{
			return null;
		}
		OnPoolTemplatePrepared.Send(component);
		m_CurrentPrefabDuringInitPool = null;
		component.transform.SetParent(m_TemplateContainer);
		component.gameObject.SetActive(value: false);
		Pool pool = new Pool(poolSpec, component, prefab, initialSize);
		m_PoolLookup.Add(prefab.GetInstanceID(), pool);
		pool.heapPos = m_DepoolHeap.Count;
		m_DepoolHeap.Add(pool);
		m_PoolMemoryUsed += pool.memoryPerInstance;
		return pool;
	}

	public IEnumerator InitPool(Component prefab, PoolInitTable.PoolSpec poolSpec, Func<bool> ShouldYieldFn, int initialSize)
	{
		if (!m_PoolLookup.TryGetValue(prefab.GetInstanceID(), out var value))
		{
			value = CreateEmptyPool(prefab, poolSpec, initialSize);
			if (value == null)
			{
				return null;
			}
		}
		return ReservePoolSize(value, ShouldYieldFn, initialSize);
	}

	public void RemovePool(Component prefab)
	{
		if (!m_PoolLookup.TryGetValue(prefab.GetInstanceID(), out var value))
		{
			return;
		}
		if (value.nSpawned > 0)
		{
			d.LogError("Tried to delete pool with spawned instances in it");
		}
		foreach (Pool.Poolable free in value.freeList)
		{
			free.DepoolEvent();
			UnityEngine.Object.DestroyImmediate(free.component.gameObject);
		}
		m_PoolMemoryUsed -= value.memoryPerInstance * (value.nFree + value.nSpawned + 1);
		int num = m_DepoolHeap.IndexOf(value);
		Pool pool = m_DepoolHeap[m_DepoolHeap.Count - 1];
		pool.heapPos = num;
		m_DepoolHeap[num] = pool;
		m_DepoolHeap.RemoveAt(m_DepoolHeap.Count - 1);
		m_PoolLookup.Remove(prefab.GetInstanceID());
		m_SpawnedItemLookup.Remove(prefab.GetInstanceID());
	}

	[DevCommand(Name = "Util.PurgePools", Access = Access.DevCheat, Users = User.Any)]
	private static void PurgeUnusedPooledItems()
	{
		foreach (KeyValuePair<int, Pool> item in Singleton.Manager<ComponentPool>.inst.m_PoolLookup)
		{
			Pool value = item.Value;
			Singleton.Manager<ComponentPool>.inst.DepoolItems(value, value.nFree);
		}
	}

	private IEnumerator ReservePoolSize(Pool pool, Func<bool> ShouldYieldFn, int initialSize)
	{
		for (int i = pool.freeList.Count + pool.numSpawned; i < initialSize; i++)
		{
			m_poolInstanciationTimer = -1f * Time.realtimeSinceStartup;
			Component component = UnityEngine.Object.Instantiate(pool.template);
			pool.instanciationTime = m_poolInstanciationTimer + Time.realtimeSinceStartup;
			component.transform.SetParent(m_PoolContainer);
			component.name = pool.originalPrefab.name;
			pool.freeList.Add(InitPoolable(component, pool.originalPrefab));
			m_PoolMemoryUsed += pool.memoryPerInstance;
			m_PoolMemoryUsedForFreeLists += pool.memoryPerInstance;
			UpdatePoolHeap(pool);
			if (ShouldYieldFn != null && ShouldYieldFn())
			{
				yield return null;
			}
		}
	}

	private void InitSpawnedItem(Component item, Transform parent, Pool pool, Pool.Poolable poolable)
	{
		item.gameObject.SetActive(value: true);
		int instanceID = item.GetInstanceID();
		if (m_SpawnedItemLookup.ContainsKey(instanceID))
		{
			d.LogError("Spawning item '" + item.name + "' already exists in spawned item lookup: how could this happen?");
		}
		else
		{
			m_SpawnedItemLookup.Add(instanceID, poolable);
		}
		d.AssertFormat(instanceID != 0, "InitSpawned item got a 0 instance ID");
		m_ItemBeingSpawnedId = instanceID;
		poolable.SpawnEvent();
		m_ItemBeingSpawnedId = 0;
		pool.numSpawned++;
		pool.highWaterMark = Mathf.Max(pool.highWaterMark, pool.numSpawned);
		if (pool.numSpawned > pool.poolSpec.size)
		{
			pool.poolSpec.size = pool.numSpawned;
		}
		UpdatePoolHeap(pool);
	}

	public Component Spawn(Component prefab, Transform parent, Vector3 position, Quaternion rotation, float scale = 1f, bool worldPositionStays = true)
	{
		Pool pool = LookupPool(prefab);
		Component component = null;
		Transform transform = null;
		Pool.Poolable poolable;
		if (pool.freeList.Count > 0)
		{
			poolable = pool.freeList[pool.freeList.Count - 1];
			pool.freeList.RemoveAt(pool.freeList.Count - 1);
			m_PoolMemoryUsedForFreeLists -= pool.memoryPerInstance;
			component = poolable.component;
			transform = component.transform;
			transform.localPosition = position;
			transform.localRotation = rotation;
		}
		else
		{
			DepoolForMemory(pool.memoryPerInstance, pool);
			m_poolInstanciationTimer = -1f * Time.realtimeSinceStartup;
			component = UnityEngine.Object.Instantiate(pool.template, position, rotation);
			pool.instanciationTime += m_poolInstanciationTimer + Time.realtimeSinceStartup;
			transform = component.transform;
			component.name = prefab.name;
			poolable = InitPoolable(component, prefab);
			m_PoolMemoryUsed += pool.memoryPerInstance;
		}
		transform.SetParent(parent, worldPositionStays);
		InitSpawnedItem(component, parent, pool, poolable);
		if (scale != 1f)
		{
			transform.localScale = Vector3.one * scale;
		}
		return component;
	}

	public Transform Spawn(Transform prefab, Transform parent, Vector3 position, Quaternion rotation, float scale = 1f, bool worldPositionStays = true)
	{
		Pool pool = LookupPool(prefab);
		Transform transform = null;
		Pool.Poolable poolable;
		if (pool.freeList.Count > 0)
		{
			poolable = pool.freeList[pool.freeList.Count - 1];
			pool.freeList.RemoveAt(pool.freeList.Count - 1);
			m_PoolMemoryUsedForFreeLists -= pool.memoryPerInstance;
			transform = poolable.component as Transform;
			transform.localPosition = position;
			transform.localRotation = rotation;
		}
		else
		{
			DepoolForMemory(pool.memoryPerInstance, pool);
			d.Assert(pool.template is Transform);
			m_poolInstanciationTimer = -1f * Time.realtimeSinceStartup;
			transform = UnityEngine.Object.Instantiate(pool.template, position, rotation) as Transform;
			pool.instanciationTime += m_poolInstanciationTimer + Time.realtimeSinceStartup;
			transform.name = prefab.name;
			poolable = InitPoolable(transform, prefab);
			m_PoolMemoryUsed += pool.memoryPerInstance;
		}
		if (transform.localScale != Vector3.one)
		{
			transform.localScale = Vector3.one;
		}
		transform.SetParent(parent, worldPositionStays);
		InitSpawnedItem(transform, parent, pool, poolable);
		if (scale != 1f)
		{
			transform.localScale *= scale;
		}
		return transform;
	}

	public void Recycle(Component item, bool worldPosStays, bool recursed = false, bool forceReturnNow = false)
	{
		int instanceID = item.GetInstanceID();
		if (ComponentBeingRecycled != null || instanceID == m_ItemBeingSpawnedId)
		{
			if (instanceID == m_ItemBeingSpawnedId)
			{
				d.LogError($"Recycling object as it is being spawned {item.name} ({item.GetType()})");
			}
			m_DeferredRecycleQueue[m_RecycleQueueIndex].Add(item);
		}
		else
		{
			if (!m_SpawnedItemLookup.TryGetValue(instanceID, out var value))
			{
				return;
			}
			ComponentBeingRecycled = item;
			if (forceReturnNow)
			{
				ReturnItemToPool(new ItemToReturnToPool
				{
					m_Poolable = value,
					m_ItemHash = instanceID
				});
			}
			else
			{
				if (m_ReturnToPoolIndex < m_ItemsToReturnToPool.Count)
				{
					m_ItemsToReturnToPool[m_ReturnToPoolIndex].m_Poolable = value;
					m_ItemsToReturnToPool[m_ReturnToPoolIndex].m_ItemHash = instanceID;
				}
				else
				{
					ItemToReturnToPool item2 = new ItemToReturnToPool
					{
						m_Poolable = value,
						m_ItemHash = instanceID
					};
					m_ItemsToReturnToPool.Add(item2);
				}
				m_ReturnToPoolIndex++;
			}
			m_SpawnedItemLookup.Remove(instanceID);
			value.RecycleEvent();
			item.gameObject.SetActive(value: false);
			if (!IsShuttingDown)
			{
				((item as Transform) ?? item.transform).SetParent(m_PoolContainer, worldPosStays);
			}
			ComponentBeingRecycled = null;
			if (recursed)
			{
				return;
			}
			int num = 0;
			while (m_DeferredRecycleQueue[m_RecycleQueueIndex].Count != 0)
			{
				List<Component> list = m_DeferredRecycleQueue[m_RecycleQueueIndex];
				m_RecycleQueueIndex = (m_RecycleQueueIndex + 1) % 2;
				foreach (Component item3 in list)
				{
					Recycle(item3, worldPosStays, recursed: true);
				}
				list.Clear();
				num++;
			}
		}
	}

	[Conditional("UNITY_EDITOR")]
	public void AddChildRecycleException(Component component)
	{
	}

	public T UnpooledSpawn<T>(T prefab, Transform parent = null, bool worldPosStays = true) where T : Component
	{
		T val = UnityEngine.Object.Instantiate(prefab);
		if (parent != null)
		{
			val.transform.SetParent(parent, worldPosStays);
		}
		CallPoolInterfaceOnUnpooledSpawn(val);
		return val;
	}

	public T UnpooledSpawn<T>(T prefab, Transform parent, Vector3 position, Quaternion rotation, bool worldPosStays = true) where T : Component
	{
		T val = UnityEngine.Object.Instantiate(prefab);
		Transform transform = val.transform;
		if (parent != null)
		{
			transform.SetParent(parent, worldPosStays);
		}
		transform.localPosition = position;
		transform.localRotation = rotation;
		CallPoolInterfaceOnUnpooledSpawn(val);
		return val;
	}

	private void CallPoolInterfaceOnUnpooledSpawn(Component spawnedObject)
	{
		Pool.Poolable poolable = default(Pool.Poolable);
		bool spawnUnpooled = PrefabSpawner.SpawnUnpooled;
		PrefabSpawner.SpawnUnpooled = true;
		if (PreparePoolTemplate(spawnedObject))
		{
			Component[] componentsInChildren = spawnedObject.GetComponentsInChildren<Component>(includeInactive: true);
			foreach (Component component in componentsInChildren)
			{
				PoolFuncs poolFuncs = InterrogatePoolInterface(component);
				Action<Component>[] onPool;
				if (poolFuncs.onPool != null)
				{
					onPool = poolFuncs.onPool;
					for (int j = 0; j < onPool.Length; j++)
					{
						onPool[j](component);
					}
				}
				Component closuredComponent = component;
				onPool = poolFuncs.onSpawn;
				foreach (Action<Component> action in onPool)
				{
					Action<Component> closuredDelegate = action;
					poolable.OnSpawn += delegate
					{
						closuredDelegate(closuredComponent);
					};
				}
			}
			poolable.SpawnEvent();
		}
		PrefabSpawner.SpawnUnpooled = spawnUnpooled;
	}

	private void ReturnItemToPool(ItemToReturnToPool itemToReturn)
	{
		Pool pool = m_PoolLookup[itemToReturn.m_Poolable.prefab.GetInstanceID()];
		pool.freeList.Add(itemToReturn.m_Poolable);
		m_PoolMemoryUsedForFreeLists += pool.memoryPerInstance;
		pool.numSpawned--;
		UpdatePoolHeap(pool);
	}

	private int DepoolItems(Pool pool, int count)
	{
		int num = 0;
		while (num < count && pool.freeList.Count > 0)
		{
			Pool.Poolable poolable = pool.freeList[pool.freeList.Count - 1];
			pool.freeList.RemoveAt(pool.freeList.Count - 1);
			poolable.DepoolEvent();
			UnityEngine.Object.Destroy(poolable.component.gameObject);
			num++;
			m_PoolMemoryUsed -= pool.memoryPerInstance;
			m_PoolMemoryUsedForFreeLists -= pool.memoryPerInstance;
		}
		if (num > 0)
		{
			UpdatePoolHeap(pool);
		}
		return num;
	}

	private void UpdatePoolHeap(Pool pool)
	{
		float depoolHeapPriority = pool.depoolHeapPriority;
		pool.UpdateFreePriority();
		if (pool.depoolHeapPriority > depoolHeapPriority)
		{
			SiftHeapUp(pool);
		}
		else if (pool.depoolHeapPriority < depoolHeapPriority)
		{
			SiftHeapDown(pool);
		}
	}

	private void SiftHeapUp(Pool iPool)
	{
		int num = iPool.heapPos;
		while (num > 0)
		{
			int num2 = num - 1 >> 1;
			Pool pool = m_DepoolHeap[num2];
			if (!(iPool.depoolHeapPriority <= pool.depoolHeapPriority))
			{
				m_DepoolHeap[num] = pool;
				pool.heapPos = num;
				m_DepoolHeap[num2] = iPool;
				iPool.heapPos = num2;
				num = num2;
				continue;
			}
			break;
		}
	}

	private void SiftHeapDown(Pool iPool)
	{
		int num = iPool.heapPos;
		int count = m_DepoolHeap.Count;
		while (true)
		{
			int num2 = num * 2 + 1;
			if (num2 >= count)
			{
				break;
			}
			int num3 = num * 2 + 2;
			Pool pool = m_DepoolHeap[num2];
			Pool pool2 = ((num3 >= count) ? pool : m_DepoolHeap[num3]);
			if (iPool.depoolHeapPriority < pool.depoolHeapPriority && pool.depoolHeapPriority >= pool2.depoolHeapPriority)
			{
				m_DepoolHeap[num] = pool;
				pool.heapPos = num;
				m_DepoolHeap[num2] = iPool;
				iPool.heapPos = num2;
				num = num2;
				continue;
			}
			if (iPool.depoolHeapPriority < pool2.depoolHeapPriority)
			{
				m_DepoolHeap[num] = pool2;
				pool2.heapPos = num;
				m_DepoolHeap[num3] = iPool;
				iPool.heapPos = num3;
				num = num3;
				continue;
			}
			break;
		}
	}

	public IEnumerable<string> EnumeratePoolUsage()
	{
		if (m_PoolLookup == null)
		{
			yield return null;
		}
		int counter = 0;
		foreach (KeyValuePair<int, Pool> item in m_PoolLookup)
		{
			yield return $"[{counter++}]{(item.Value.template ? item.Value.template.name : item.Key.ToString())}: {item.Value.freeList.Count} free + {item.Value.numSpawned} active";
		}
	}

	public T GetOriginalPrefab<T>(T component) where T : Component
	{
		if ((bool)m_CurrentPrefabDuringInitPool)
		{
			return m_CurrentPrefabDuringInitPool.GetComponent<T>();
		}
		if (Singleton.Manager<ComponentPool>.inst.m_SpawnedItemLookup.TryGetValue(component.GetInstanceID(), out var value))
		{
			return value.prefab.GetComponent<T>();
		}
		return null;
	}

	private void DepoolForMemory(int required, Pool forPoolAlloc, int maxDepoolCount = -1)
	{
		if (m_MaximumPoolMemory <= 0)
		{
			return;
		}
		int num = m_MaximumPoolMemory - required;
		while (m_PoolMemoryUsed > num && m_PoolMemoryUsedForFreeLists > 0 && maxDepoolCount != 0)
		{
			Pool pool = m_DepoolHeap[0];
			if (!(pool.depoolHeapPriority <= 0f))
			{
				DepoolItems(pool, 1);
				maxDepoolCount--;
				continue;
			}
			break;
		}
	}

	private void Awake()
	{
		m_PoolContainer = new GameObject("_poolcontainer").transform;
		m_PoolContainer.parent = base.transform;
		m_TemplateContainer = new GameObject("_templatecontainer").transform;
		m_TemplateContainer.parent = m_PoolContainer;
		for (int i = 0; i < 2; i++)
		{
			m_DeferredRecycleQueue[i] = new List<Component>();
		}
		for (int j = 0; j < 10; j++)
		{
			m_ItemsToReturnToPool.Add(new ItemToReturnToPool());
		}
		m_ReturnToPoolIndex = 0;
		IsShuttingDown = false;
	}

	private void LateUpdate()
	{
		if (m_AutoTrimPoolsThreshold > 0)
		{
			DepoolForMemory(m_AutoTrimPoolsThreshold, null, m_AutoTrimPoolsMaxDepoolPerFrame);
		}
		for (int i = 0; i < m_ReturnToPoolIndex; i++)
		{
			ReturnItemToPool(m_ItemsToReturnToPool[i]);
		}
		m_ReturnToPoolIndex = 0;
	}

	private void OnApplicationQuit()
	{
		IsShuttingDown = true;
	}
}
