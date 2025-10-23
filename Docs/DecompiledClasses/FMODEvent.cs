#define UNITY_EDITOR
using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct FMODEvent
{
	[Serializable]
	public struct FMODParams : IEquatable<FMODParams>
	{
		public string m_ParamName;

		public float m_Value;

		public static readonly FMODParams empty = new FMODParams(null, 0f);

		public FMODParams(string paramName, float paramValue)
		{
			m_ParamName = paramName;
			m_Value = paramValue;
		}

		public bool Equals(FMODParams other)
		{
			if (m_Value == other.m_Value)
			{
				return m_ParamName == other.m_ParamName;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is FMODParams other)
			{
				return Equals(other);
			}
			return false;
		}

		public static bool operator ==(FMODParams a, FMODParams b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(FMODParams a, FMODParams b)
		{
			return !(a == b);
		}

		public override int GetHashCode()
		{
			return (m_ParamName, m_Value).GetHashCode();
		}

		public override string ToString()
		{
			return $"{m_ParamName}: {m_Value}";
		}
	}

	[FormerlySerializedAs("Event")]
	[EventRef]
	[SerializeField]
	private string m_EventPath;

	public string EventPath => m_EventPath;

	public FMODEvent(string eventPath)
	{
		m_EventPath = eventPath;
		d.Assert(IsValid(), "FMODEvent - Constructor was passed invalid event argument (" + eventPath + ")");
	}

	public bool IsValid()
	{
		return !m_EventPath.NullOrEmpty();
	}

	public FMODEventInstance PlayEvent()
	{
		return PlayEvent(FMODParams.empty);
	}

	public FMODEventInstance PlayEvent(FMODParams singleParam)
	{
		EventInstance evtInstance = RuntimeManager.CreateInstance(m_EventPath);
		FMODEventInstance result = new FMODEventInstance(m_EventPath, evtInstance);
		if (singleParam != FMODParams.empty)
		{
			result.setParameterValue(singleParam.m_ParamName, singleParam.m_Value);
		}
		result.start();
		return result;
	}

	public FMODEventInstance PlayEvent(Vector3 position)
	{
		return PlayEvent(position, FMODParams.empty);
	}

	public FMODEventInstance PlayEvent(Vector3 position, FMODParams singleParam)
	{
		FMODEventInstance fMODEventInstance = PlayEvent(singleParam);
		Singleton.Manager<ManSFX>.inst.AttachInstanceToPosition(fMODEventInstance, position);
		return fMODEventInstance;
	}

	public FMODEventInstance PlayEventTrackedObject(Transform transform, Rigidbody rigidbody)
	{
		return PlayEventTrackedObject(transform, rigidbody, FMODParams.empty);
	}

	public FMODEventInstance PlayEventTrackedObject(Transform transform, Rigidbody rigidbody, FMODParams singleParam)
	{
		FMODEventInstance fMODEventInstance = PlayEvent(singleParam);
		Singleton.Manager<ManSFX>.inst.AttachInstanceToTransform(fMODEventInstance, transform, rigidbody);
		return fMODEventInstance;
	}

	public void PlayOneShot()
	{
		PlayOneShot(FMODParams.empty);
	}

	public void PlayOneShot(FMODParams singleParam)
	{
		PlayEvent(singleParam).release();
	}

	public void PlayOneShot(Vector3 position)
	{
		PlayOneShot(position, FMODParams.empty);
	}

	public void PlayOneShot(Vector3 position, FMODParams singleParam)
	{
		FMODEventInstance instance = PlayEvent(singleParam);
		Singleton.Manager<ManSFX>.inst.AttachInstanceToPosition(instance, position);
		instance.release();
	}

	public void PlayOneShot(Transform transformToFollow)
	{
		PlayOneShot(transformToFollow, FMODParams.empty);
	}

	public void PlayOneShot(Transform transformToFollow, FMODParams singleParam)
	{
		FMODEventInstance instance = PlayEvent(singleParam);
		Singleton.Manager<ManSFX>.inst.AttachInstanceToTransform(instance, transformToFollow, null);
		instance.release();
	}

	public FMODEventInstance PlayEvent(FMODParams[] paramList)
	{
		EventInstance evtInstance = RuntimeManager.CreateInstance(m_EventPath);
		FMODEventInstance result = new FMODEventInstance(m_EventPath, evtInstance);
		if (paramList != null)
		{
			for (int i = 0; i < paramList.Length; i++)
			{
				result.setParameterValue(paramList[i].m_ParamName, paramList[i].m_Value);
			}
		}
		result.start();
		return result;
	}

	public FMODEventInstance PlayEventTrackedObject(Transform transform, Rigidbody rigidbody, FMODParams[] paramList)
	{
		FMODEventInstance fMODEventInstance = PlayEvent(paramList);
		Singleton.Manager<ManSFX>.inst.AttachInstanceToTransform(fMODEventInstance, transform, rigidbody);
		return fMODEventInstance;
	}

	public void PlayOneShot(Vector3 position, params FMODParams[] paramList)
	{
		FMODEventInstance instance = PlayEvent(paramList);
		Singleton.Manager<ManSFX>.inst.AttachInstanceToPosition(instance, position);
		instance.release();
	}
}
