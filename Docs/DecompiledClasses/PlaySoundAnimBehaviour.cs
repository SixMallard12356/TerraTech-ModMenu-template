#define UNITY_EDITOR
using UnityEngine;

public class PlaySoundAnimBehaviour : ActionInStateAnimBehaviour
{
	public enum EndActionType
	{
		DontStop,
		TriggerCue,
		StopImmediate,
		NotSet
	}

	[SerializeField]
	private FMODEvent m_SFXEvent;

	[SerializeField]
	private bool m_IsLoopingSound;

	[SerializeField]
	private EndActionType m_NodeExitAction = EndActionType.NotSet;

	[HideInInspector]
	[SerializeField]
	private bool m_KillSoundsOnStateExit = true;

	private FMODEventInstance m_SFXInstance;

	protected override void BeginAction(Transform parentVisible)
	{
		if (parentVisible != null && m_SFXEvent.IsValid() && !m_SFXInstance.IsInited)
		{
			m_SFXInstance = m_SFXEvent.PlayEvent(parentVisible.position);
		}
	}

	protected override void EndAction()
	{
		if (m_SFXInstance.IsInited)
		{
			EndActionType endActionType = m_NodeExitAction;
			if (endActionType == EndActionType.NotSet && m_KillSoundsOnStateExit)
			{
				endActionType = EndActionType.StopImmediate;
			}
			bool flag = false;
			switch (endActionType)
			{
			case EndActionType.TriggerCue:
				m_SFXInstance.triggerCue();
				flag = true;
				break;
			case EndActionType.StopImmediate:
				m_SFXInstance.stop();
				flag = true;
				break;
			default:
				d.LogError("PlaySoundAnimBehaviour.EndAction - exit mode '" + endActionType.ToString() + "' not implemented!");
				break;
			case EndActionType.DontStop:
			case EndActionType.NotSet:
				break;
			}
			if (flag || !m_IsLoopingSound)
			{
				m_SFXInstance.release();
			}
		}
	}

	protected override void OnRecycled()
	{
		if (m_SFXInstance.IsInited)
		{
			m_SFXInstance.StopAndRelease();
		}
	}
}
