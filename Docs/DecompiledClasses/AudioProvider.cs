using System;
using UnityEngine;

[Serializable]
public class AudioProvider : TechAudio.IModuleAudioProvider
{
	private enum State
	{
		Off,
		Attack,
		Sustain,
		Release
	}

	[SearchableEnum(SortedEnum.EnumSortType.AlphabeticalAscending, false)]
	[SerializeField]
	private TechAudio.SFXType m_SFXType;

	[SerializeField]
	private float m_AttackTime;

	[SerializeField]
	private float m_ReleaseTime;

	private float m_Adsr01;

	private State m_State;

	private bool m_NoteOn;

	private Module m_RequestedByModule;

	public TechAudio.SFXType SFXType => m_SFXType;

	public bool NoteOn
	{
		get
		{
			return m_NoteOn;
		}
		set
		{
			m_NoteOn = value;
		}
	}

	public FMODEvent.FMODParams AdditionalParams { get; set; }

	public event Action<TechAudio.AudioTickData, FMODEvent.FMODParams> OnAudioTickUpdate;

	public void SetParent(Module module)
	{
		m_RequestedByModule = module;
	}

	public void Update()
	{
		switch (m_State)
		{
		case State.Off:
			m_Adsr01 = 0f;
			if (m_NoteOn)
			{
				m_State = State.Attack;
			}
			break;
		case State.Attack:
			if (!m_NoteOn)
			{
				m_State = State.Release;
				break;
			}
			if (m_AttackTime > 0f)
			{
				m_Adsr01 += m_AttackTime * Time.deltaTime;
			}
			else
			{
				m_Adsr01 = 1f;
			}
			if (m_Adsr01 >= 1f)
			{
				m_State = State.Sustain;
			}
			break;
		case State.Sustain:
			m_Adsr01 = 1f;
			if (!m_NoteOn)
			{
				m_State = State.Release;
			}
			break;
		case State.Release:
			if (m_ReleaseTime > 0f)
			{
				m_Adsr01 -= m_ReleaseTime * Time.deltaTime;
			}
			else
			{
				m_Adsr01 = 0f;
			}
			if (NoteOn)
			{
				m_State = State.Attack;
			}
			else if (m_Adsr01 <= 0f)
			{
				m_State = State.Off;
			}
			break;
		}
		if (this.OnAudioTickUpdate != null)
		{
			TechAudio.AudioTickData arg = TechAudio.AudioTickData.ConfigureLoopedADSR(this, m_RequestedByModule.block, SFXType, m_NoteOn, m_Adsr01);
			this.OnAudioTickUpdate(arg, AdditionalParams);
		}
	}
}
