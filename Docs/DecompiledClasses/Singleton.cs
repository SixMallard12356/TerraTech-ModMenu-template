#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Threading;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class Singleton : MonoBehaviour
{
	public abstract class Manager : MonoBehaviour
	{
		public virtual void ForceOverrideSingletonInstance()
		{
		}
	}

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	public class Manager<T> : Manager where T : class
	{
		public static T inst;

		public Manager()
		{
			if ((bool)_instance)
			{
				d.Assert(typeof(T) == GetType(), $"Singleton manager type incorrect: {GetType()}/{typeof(T)}");
				if (inst == null)
				{
					inst = this as T;
				}
				else
				{
					d.LogWarning("Attempt to reinitialise singleton: {typeof(T)}");
				}
			}
		}

		public override void ForceOverrideSingletonInstance()
		{
			inst = this as T;
		}
	}

	private static Singleton _instance = null;

	public bool Reset;

	public Globals globals;

	public QualitySettingsExtended qualitySettingsExtended;

	private static Tank _playerTank;

	private static Vector3 s_LastPlayerPos = Vector3.zero;

	private Transform _static;

	private Transform _terrain;

	public static EventNoParams DestroyedEvent;

	public static EventNoParams ApplicationQuitEvent;

	private static bool started = false;

	private List<Action> m_NextFrameActions = new List<Action>();

	public static Singleton instance => _instance;

	public int MainThreadID { get; private set; }

	public static bool IsCalledOnMainThread
	{
		get
		{
			if (instance != null)
			{
				return Thread.CurrentThread.ManagedThreadId == instance.MainThreadID;
			}
			return false;
		}
	}

	public static Tank playerTank
	{
		get
		{
			if ((bool)_playerTank && !_playerTank.gameObject.activeSelf)
			{
				throw new Exception("Singleton.playerTank.get - PlayerTank is set to a recycled tech! This should never happen as the tech.OnRecycle call should clear out PlayerTank.");
			}
			return _playerTank;
		}
	}

	public static Vector3 playerPos
	{
		get
		{
			Tank tank = playerTank;
			if (!(tank != null))
			{
				return s_LastPlayerPos;
			}
			return tank.rbody.position;
		}
	}

	public static Camera camera { get; private set; }

	public static Transform cameraTrans { get; private set; }

	public static Transform dynamicContainer => null;

	public static Transform staticContainer
	{
		get
		{
			if (!started)
			{
				return null;
			}
			return InitObjRef(ref _instance._static, "_staticobjects");
		}
	}

	public static Transform terrainContainer
	{
		get
		{
			if (!started)
			{
				return null;
			}
			return InitObjRef(ref _instance._terrain, "_terraintiles");
		}
	}

	public int FixedFrameCount { get; private set; }

	private static event Action m_PostStartEvent;

	public static Tank GetPlayerTankInternal()
	{
		return _playerTank;
	}

	public static void SetPlayerTankInternal(Tank newPlayerTech)
	{
		if (newPlayerTech == null && _playerTank != null)
		{
			s_LastPlayerPos = _playerTank.rbody.position;
		}
		_ = _playerTank != newPlayerTech;
		_playerTank = newPlayerTech;
	}

	private static Transform InitObjRef(ref Transform transRef, string name, bool create = true)
	{
		if (transRef == null)
		{
			GameObject gameObject = GameObject.Find(name);
			if (!gameObject && create)
			{
				gameObject = new GameObject(name);
				gameObject.layer = Globals.inst.layerContainer;
				gameObject.transform.parent = _instance.transform;
			}
			transRef = gameObject.transform;
		}
		return transRef;
	}

	private void Awake()
	{
		if (_instance != null)
		{
			d.LogError("singleton initialised twice");
		}
		_instance = this;
		camera = Camera.main;
		if (camera != null)
		{
			cameraTrans = camera.transform;
		}
		MainThreadID = Thread.CurrentThread.ManagedThreadId;
		d.Log("Main thread ID: " + MainThreadID);
	}

	private void Start()
	{
		FixedFrameCount = 0;
		started = true;
		if (Singleton.m_PostStartEvent != null)
		{
			Singleton.m_PostStartEvent();
		}
	}

	public static void DoOnceAfterStart(Action action)
	{
		if (started)
		{
			action();
		}
		else
		{
			m_PostStartEvent += action;
		}
	}

	public void DoOnNextFrame(Action action)
	{
		Monitor.Enter(m_NextFrameActions);
		m_NextFrameActions.Add(action);
		Monitor.Exit(m_NextFrameActions);
	}

	private void Update()
	{
		if (_instance == null || Reset)
		{
			base.gameObject.SetActive(value: false);
			base.gameObject.SetActive(value: true);
			Reset = false;
		}
		Monitor.Enter(m_NextFrameActions);
		foreach (Action nextFrameAction in m_NextFrameActions)
		{
			nextFrameAction();
		}
		m_NextFrameActions.Clear();
		Monitor.Exit(m_NextFrameActions);
	}

	private void FixedUpdate()
	{
		FixedFrameCount++;
	}

	private void OnDestroy()
	{
		DestroyedEvent.Send();
	}

	private void OnApplicationQuit()
	{
		ApplicationQuitEvent.Send();
	}
}
