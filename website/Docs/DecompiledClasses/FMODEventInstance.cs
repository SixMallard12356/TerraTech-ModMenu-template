#define UNITY_EDITOR
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public struct FMODEventInstance
{
	public string m_EventPath;

	public EventInstance m_EventInstance;

	private static Dictionary<string, Dictionary<string, int>> m_ParamDatabase = new Dictionary<string, Dictionary<string, int>>();

	public bool IsInited => m_EventInstance.isValid();

	public FMODEventInstance(string evtPath, EventInstance evtInstance)
	{
		m_EventPath = evtPath;
		m_EventInstance = evtInstance;
	}

	public void start()
	{
		if (m_EventInstance.isValid())
		{
			m_EventInstance.start();
		}
		else
		{
			d.LogError("Start can't be called on an empty or recyled event. EventPath: " + m_EventPath);
		}
	}

	public bool getPaused()
	{
		if (m_EventInstance.isValid())
		{
			m_EventInstance.getPaused(out var paused);
			return paused;
		}
		return false;
	}

	public void setPaused(bool paused)
	{
		if (m_EventInstance.isValid())
		{
			m_EventInstance.setPaused(paused);
		}
	}

	public void stop(FMOD.Studio.STOP_MODE stopMode = FMOD.Studio.STOP_MODE.IMMEDIATE)
	{
		if (m_EventInstance.isValid())
		{
			m_EventInstance.stop(stopMode);
		}
	}

	public void StopAndRelease(FMOD.Studio.STOP_MODE stopMode = FMOD.Studio.STOP_MODE.IMMEDIATE)
	{
		if (m_EventInstance.isValid())
		{
			m_EventInstance.stop(stopMode);
			m_EventInstance.release();
			m_EventInstance.clearHandle();
		}
	}

	public void release()
	{
		if (m_EventInstance.isValid())
		{
			m_EventInstance.release();
			m_EventInstance.clearHandle();
		}
	}

	public void set3DAttributes(Vector3 position)
	{
		if (m_EventInstance.isValid())
		{
			m_EventInstance.set3DAttributes(position.To3DAttributes());
		}
	}

	public void set3DAttributes(Transform trans, Rigidbody rbody)
	{
		if (m_EventInstance.isValid())
		{
			ATTRIBUTES_3D attributes = new ATTRIBUTES_3D
			{
				forward = trans.forward.ToFMODVector(),
				up = trans.up.ToFMODVector()
			};
			if (rbody != null)
			{
				attributes.position = rbody.position.ToFMODVector();
				attributes.velocity = rbody.velocity.ToFMODVector();
			}
			else
			{
				attributes.position = trans.position.ToFMODVector();
			}
			m_EventInstance.set3DAttributes(attributes);
		}
	}

	public void ApplyParam(FMODEvent.FMODParams param)
	{
		if (m_EventInstance.isValid())
		{
			setParameterValue(param.m_ParamName, param.m_Value);
		}
	}

	public void ApplyParams(FMODEvent.FMODParams[] paramList)
	{
		if (m_EventInstance.isValid() && paramList != null)
		{
			for (int i = 0; i < paramList.Length; i++)
			{
				setParameterValue(paramList[i].m_ParamName, paramList[i].m_Value);
			}
		}
	}

	public void setParameterValue(string name, float value)
	{
		if (!m_EventInstance.isValid())
		{
			return;
		}
		int value2 = 0;
		if (!m_ParamDatabase.TryGetValue(m_EventPath, out var value3))
		{
			value3 = new Dictionary<string, int>();
			m_EventInstance.getParameterCount(out var count);
			for (int i = 0; i < count; i++)
			{
				m_EventInstance.getParameterByIndex(i, out var instance);
				instance.getDescription(out var description);
				value3.Add(description.name, i);
			}
			m_ParamDatabase.Add(m_EventPath, value3);
		}
		if (value3.TryGetValue(name, out value2))
		{
			m_EventInstance.setParameterValueByIndex(value2, value);
		}
	}

	public void triggerCue()
	{
		if (m_EventInstance.isValid())
		{
			m_EventInstance.triggerCue();
		}
	}

	public bool CheckPlaybackState(PLAYBACK_STATE playbackState)
	{
		bool result = false;
		if (m_EventInstance.isValid())
		{
			PLAYBACK_STATE state;
			RESULT playbackState2 = m_EventInstance.getPlaybackState(out state);
			if (playbackState2 == RESULT.OK)
			{
				if (state == playbackState)
				{
					result = true;
				}
			}
			else
			{
				d.LogWarning("Failed to get playback state for event instance at " + m_EventPath + " FMOD Result=" + playbackState2);
			}
		}
		return result;
	}
}
