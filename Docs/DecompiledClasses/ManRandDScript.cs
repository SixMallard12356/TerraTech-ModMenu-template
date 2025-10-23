#define UNITY_EDITOR
using UnityEngine;

public class ManRandDScript : Singleton.Manager<ManRandDScript>
{
	private uScript_RandDScriptEvent m_CurActive;

	public GameObject ActiveScriptObject
	{
		get
		{
			if (!(m_CurActive != null))
			{
				return null;
			}
			return m_CurActive.gameObject;
		}
		set
		{
			if (m_CurActive != null)
			{
				m_CurActive.Deactivate();
			}
			if (value != null)
			{
				m_CurActive = value.GetComponent<uScript_RandDScriptEvent>();
				d.Assert(m_CurActive, "ManRandDScript: setting an object which doesn't have a uScript_RandDScriptEvent attached");
			}
			else
			{
				m_CurActive = null;
			}
			if (m_CurActive != null)
			{
				m_CurActive.Activate();
			}
		}
	}

	private void OnModeExit(Mode newMode)
	{
		if (m_CurActive != null)
		{
			m_CurActive.Deactivate();
			m_CurActive = null;
		}
	}

	public void Start()
	{
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(OnModeExit);
	}
}
