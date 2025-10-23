#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleAnimator : Module
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public int restoreState;
	}

	public EventNoParams OnRecycled;

	[SerializeField]
	[HideInInspector]
	private Animator m_ControlledAnimator;

	private AnimatorTrigger m_SpawnedTrigger = new AnimatorTrigger("Spawned");

	private AnimatorInt m_RestoreStateInt = new AnimatorInt("RestoreState");

	private AnimatorBool m_AttachedBool = new AnimatorBool("Attached");

	private AnimatorBool m_HasConnectedAPsBool = new AnimatorBool("HasConnectedAPs");

	private int m_RestoreState;

	private bool m_HasConnectedAPs;

	private Dictionary<int, AnimatorControllerParameterType> m_ParameterLookup;

	public Animator Animator => m_ControlledAnimator;

	public bool Inited => m_ParameterLookup != null;

	public bool HasConnectedAPs
	{
		get
		{
			d.Assert(ParamExists(m_HasConnectedAPsBool), "ModuleAnimator.HasConnectedAPs property is not valid unless m_HasConnectedAPsBool is active");
			return m_HasConnectedAPs;
		}
	}

	public bool Set(AnimatorTrigger param)
	{
		d.Assert(m_ParameterLookup != null, "ModuleAnimator.Set called before OnSpawn");
		if (m_ParameterLookup != null)
		{
			return param.SetOnAnimator(m_ControlledAnimator, m_ParameterLookup);
		}
		return false;
	}

	public void Reset(AnimatorTrigger param)
	{
		d.Assert(m_ParameterLookup != null, "ModuleAnimator.Reset called before OnSpawn");
		if (m_ParameterLookup != null)
		{
			param.ResetOnAnimator(m_ControlledAnimator, m_ParameterLookup);
		}
	}

	public bool Set(AnimatorInt param, int value)
	{
		d.Assert(m_ParameterLookup != null, "ModuleAnimator.Set called before OnSpawn");
		if (m_ParameterLookup != null)
		{
			return param.SetOnAnimator(m_ControlledAnimator, m_ParameterLookup, value);
		}
		return false;
	}

	public bool Set(AnimatorBool param, bool value)
	{
		d.Assert(m_ParameterLookup != null, "ModuleAnimator.Set called before OnSpawn");
		if (m_ParameterLookup != null)
		{
			return param.SetOnAnimator(m_ControlledAnimator, m_ParameterLookup, value);
		}
		return false;
	}

	public float Get(AnimatorFloat param)
	{
		float result = 0f;
		d.Assert(m_ParameterLookup != null, "ModuleAnimator.Get called before OnSpawn");
		if (m_ParameterLookup != null)
		{
			result = param.GetFromAnimator(m_ControlledAnimator, m_ParameterLookup);
		}
		return result;
	}

	public bool Get(AnimatorBool param)
	{
		bool result = false;
		d.Assert(m_ParameterLookup != null, "ModuleAnimator.Get called before OnSpawn");
		if (m_ParameterLookup != null)
		{
			result = param.GetFromAnimator(m_ControlledAnimator, m_ParameterLookup);
		}
		return result;
	}

	public bool Set(AnimatorFloat param, float value)
	{
		d.Assert(m_ParameterLookup != null, "ModuleAnimator.Set called before OnSpawn");
		if (m_ParameterLookup != null)
		{
			return param.SetOnAnimator(m_ControlledAnimator, m_ParameterLookup, value);
		}
		return false;
	}

	public bool ParamExists(AnimatorParameter param)
	{
		bool result = false;
		d.Assert(m_ParameterLookup != null, "ModuleAnimator.ParamExists called before OnSpawn");
		if (m_ParameterLookup != null)
		{
			result = param.Exists(m_ControlledAnimator, m_ParameterLookup);
		}
		return result;
	}

	public void SetRestoreState(int value)
	{
		m_RestoreState = value;
		Set(m_RestoreStateInt, value);
	}

	public void ResetAllTriggers()
	{
		if (!(m_ControlledAnimator != null) || m_ParameterLookup == null)
		{
			return;
		}
		Dictionary<int, AnimatorControllerParameterType>.KeyCollection.Enumerator enumerator = m_ParameterLookup.Keys.GetEnumerator();
		while (enumerator.MoveNext())
		{
			int current = enumerator.Current;
			if (m_ParameterLookup[current] == AnimatorControllerParameterType.Trigger)
			{
				m_ControlledAnimator.ResetTrigger(current);
			}
		}
	}

	public void SetSpawnedTrigger()
	{
		if (m_ParameterLookup != null)
		{
			Set(m_SpawnedTrigger);
		}
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.restoreState = m_RestoreState;
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			SetRestoreState(serialData2.restoreState);
		}
	}

	private void OnAttached()
	{
		Set(m_SpawnedTrigger);
		Set(m_RestoreStateInt, m_RestoreState);
		Set(m_AttachedBool, value: true);
		if (ParamExists(m_HasConnectedAPsBool))
		{
			m_HasConnectedAPs = base.block.NumConnectedAPs > 0;
			Set(m_HasConnectedAPsBool, m_HasConnectedAPs);
		}
	}

	private void OnDetaching()
	{
		Set(m_AttachedBool, value: false);
		if (ParamExists(m_HasConnectedAPsBool))
		{
			m_HasConnectedAPs = false;
			Set(m_HasConnectedAPsBool, value: false);
		}
	}

	private void OnNeighbourAttachedOrDetached(TankBlock neighbour)
	{
		m_HasConnectedAPs = base.block.NumConnectedAPs > 0;
		Set(m_HasConnectedAPsBool, m_HasConnectedAPs);
	}

	private void PrePool()
	{
		bool includeInactive = true;
		Animator[] componentsInChildren = GetComponentsInChildren<Animator>(includeInactive);
		if (componentsInChildren.Length != 0)
		{
			m_ControlledAnimator = componentsInChildren[0];
		}
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialize);
	}

	private void OnSpawn()
	{
		if (m_ParameterLookup == null)
		{
			m_ParameterLookup = new Dictionary<int, AnimatorControllerParameterType>();
			if (m_ControlledAnimator != null)
			{
				for (int i = 0; i < m_ControlledAnimator.parameterCount; i++)
				{
					AnimatorControllerParameter animatorControllerParameter = m_ControlledAnimator.parameters[i];
					m_ParameterLookup[animatorControllerParameter.nameHash] = animatorControllerParameter.type;
				}
			}
			if (ParamExists(m_HasConnectedAPsBool))
			{
				base.block.NeighbourAttachedEvent.Subscribe(OnNeighbourAttachedOrDetached);
				base.block.NeighbourDetachedEvent.Subscribe(OnNeighbourAttachedOrDetached);
			}
		}
		ResetAllTriggers();
		if ((bool)m_ControlledAnimator && base.enabled)
		{
			Set(m_SpawnedTrigger);
		}
		SetRestoreState(0);
	}

	private void OnRecycle()
	{
		OnRecycled.Send();
	}

	private void Start()
	{
	}
}
