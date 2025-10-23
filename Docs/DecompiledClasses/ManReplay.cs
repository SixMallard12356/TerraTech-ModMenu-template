using System;
using System.Collections.Generic;
using UnityEngine;

public class ManReplay : Singleton.Manager<ManReplay>
{
	private class ReplayData
	{
		public GhostData m_GhostData;

		public float m_ReplayTime;
	}

	private class TankChangeTracker
	{
		public bool Changed;

		private Tank m_AttachedTo;

		public bool Attached => m_AttachedTo != null;

		public void Attach(Tank tech)
		{
			if (m_AttachedTo != null)
			{
				Detach();
			}
			m_AttachedTo = tech;
			if (tech != null)
			{
				tech.TankRecycledEvent.Subscribe(TankRecycled);
				tech.AttachEvent.Subscribe(AttachHandler);
				tech.DetachEvent.Subscribe(AttachHandler);
			}
		}

		private void AttachHandler(TankBlock block, Tank tank)
		{
			Changed = true;
		}

		private void TankRecycled(Tank tank)
		{
			Detach();
		}

		private void Detach()
		{
			if ((bool)m_AttachedTo)
			{
				m_AttachedTo.TankRecycledEvent.Unsubscribe(TankRecycled);
				m_AttachedTo.AttachEvent.Unsubscribe(AttachHandler);
				m_AttachedTo.DetachEvent.Unsubscribe(AttachHandler);
				m_AttachedTo = null;
				Changed = false;
			}
		}
	}

	private class RecordingData
	{
		public GhostData m_GhostData = new GhostData();

		public float m_GhostDataTimer;

		public float m_SampleTimer;

		public bool m_SnapToPos;

		public Event<GhostData> m_RecordingFinishedEvent;

		private Tank.WeakReference m_TankReference = new Tank.WeakReference();

		private TankChangeTracker m_ChangeTracker = new TankChangeTracker();

		public bool TechChanged
		{
			get
			{
				return m_ChangeTracker.Changed;
			}
			set
			{
				m_ChangeTracker.Changed = value;
			}
		}

		public void SetTech(Tank tech)
		{
			m_TankReference.Set(tech);
			m_ChangeTracker.Attach(tech);
		}

		public Tank GetTech()
		{
			return m_TankReference.Get();
		}
	}

	public struct ReplayTimingData
	{
		public GhostData m_GhostData;

		public float m_StartTime;
	}

	[SerializeField]
	[Tooltip("Sample Rate in Seconds")]
	private float m_GhostSampleRate = 0.333f;

	private GhostData m_GhostData = new GhostData();

	private bool m_RecordGhostData;

	private TankChangeTracker m_PlayerChangeTracker = new TankChangeTracker();

	private ReplayData m_DebugGhostData;

	private float m_GhostDataTimer;

	private float m_SampleTimer;

	private bool m_SnapToPos;

	private const int kMaxReplaySecondsAllowed = 600;

	private bool m_ReplayTooLong;

	private List<ReplayData> m_ReplayList = new List<ReplayData>();

	private List<RecordingData> m_RecordingList = new List<RecordingData>();

	public bool HasValidRun()
	{
		return !m_ReplayTooLong;
	}

	public void RecordTech()
	{
		StartRecording();
	}

	public GhostData StopAndGetGhostData()
	{
		StopRecording();
		return m_GhostData;
	}

	public void ReplayGhost(GhostData ghostToPlay, bool ghostEffect, float startTime = 0f)
	{
		if (ghostToPlay != null)
		{
			StartReplay(ghostToPlay, ghostEffect, startTime);
		}
	}

	public void StopAllReplays()
	{
		for (int num = m_ReplayList.Count - 1; num >= 0; num--)
		{
			ClearReplayTech(m_ReplayList[num]);
			m_ReplayList[num] = null;
			m_ReplayList.RemoveAt(num);
		}
	}

	public bool IsReplayRunning(GhostData ghostData)
	{
		bool result = false;
		for (int i = 0; i < m_ReplayList.Count; i++)
		{
			if (m_ReplayList[i].m_GhostData == ghostData)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public void RecordGhost(Tank tech, Action<GhostData> recordingFinishedCallback)
	{
		RecordingData recordingData = new RecordingData();
		recordingData.SetTech(tech);
		recordingData.m_RecordingFinishedEvent.Subscribe(recordingFinishedCallback);
		m_RecordingList.Add(recordingData);
	}

	public void StopRecording(Tank tech)
	{
		if (!(tech != null))
		{
			return;
		}
		for (int num = m_RecordingList.Count - 1; num >= 0; num--)
		{
			if (m_RecordingList[num].GetTech() == tech)
			{
				StopRecording(m_RecordingList[num]);
			}
		}
	}

	public void StopAllRecording()
	{
		for (int i = 0; i < m_RecordingList.Count; i++)
		{
			StopRecording(m_RecordingList[i]);
		}
		m_RecordingList.Clear();
	}

	private void StartRecording()
	{
		m_GhostData = new GhostData();
		m_RecordGhostData = true;
		m_GhostDataTimer = 0f;
		m_SampleTimer = 0f;
		m_ReplayTooLong = false;
	}

	private void StopRecording()
	{
		m_RecordGhostData = false;
	}

	private void StopRecording(RecordingData recording)
	{
		recording.m_RecordingFinishedEvent.Send(recording.m_GhostData);
		recording.m_RecordingFinishedEvent.Clear();
		recording.SetTech(null);
	}

	private ReplayData StartReplay(GhostData ghostToPlay, bool ghostEffect, float startTime)
	{
		ReplayData replayData = new ReplayData();
		replayData.m_GhostData = ghostToPlay;
		replayData.m_GhostData.StartReplay(ghostEffect);
		replayData.m_ReplayTime = startTime;
		m_ReplayList.Add(replayData);
		return replayData;
	}

	private void ClearReplayTech(ReplayData replayData)
	{
		replayData?.m_GhostData.RevertAndRecycleTech();
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
		for (int num = m_RecordingList.Count - 1; num >= 0; num--)
		{
			RecordingData recordingData = m_RecordingList[num];
			if (recordingData.GetTech() != null && !m_ReplayTooLong)
			{
				if (recordingData.m_SampleTimer >= m_GhostSampleRate || recordingData.m_SnapToPos)
				{
					bool techChanged = recordingData.TechChanged;
					recordingData.TechChanged = false;
					recordingData.m_GhostData.AddGhostData(recordingData.GetTech(), recordingData.m_GhostDataTimer, recordingData.m_SnapToPos, techChanged);
					recordingData.m_SampleTimer -= m_GhostSampleRate;
				}
			}
			else
			{
				StopRecording(m_RecordingList[num]);
				m_RecordingList.RemoveAt(num);
			}
			recordingData.m_SampleTimer += Time.deltaTime;
			recordingData.m_GhostDataTimer += Time.deltaTime;
		}
		if (m_RecordGhostData && !m_ReplayTooLong)
		{
			bool checkBlockSpecsChanged = true;
			if ((bool)Singleton.playerTank)
			{
				if (m_SampleTimer >= m_GhostSampleRate || m_SnapToPos)
				{
					if (m_PlayerChangeTracker.Attached)
					{
						checkBlockSpecsChanged = m_PlayerChangeTracker.Changed;
						m_PlayerChangeTracker.Changed = false;
					}
					else
					{
						m_PlayerChangeTracker.Attach(Singleton.playerTank);
					}
					m_GhostData.AddGhostData(Singleton.playerTank, m_GhostDataTimer, m_SnapToPos, checkBlockSpecsChanged);
					m_SampleTimer -= m_GhostSampleRate;
					m_SnapToPos = false;
				}
			}
			else
			{
				if (!m_SnapToPos)
				{
					m_GhostData.AddGhostData(Singleton.playerTank, m_GhostDataTimer, snapToPos: true, checkBlockSpecsChanged);
				}
				m_SnapToPos = true;
			}
			m_SampleTimer += Time.deltaTime;
			m_GhostDataTimer += Time.deltaTime;
		}
		if (m_GhostDataTimer > 600f)
		{
			m_ReplayTooLong = true;
		}
	}

	private void FixedUpdate()
	{
		if (m_ReplayList.Count <= 0)
		{
			return;
		}
		for (int num = m_ReplayList.Count - 1; num >= 0; num--)
		{
			if (m_ReplayList[num].m_GhostData.ReplayData(m_ReplayList[num].m_ReplayTime))
			{
				ClearReplayTech(m_ReplayList[num]);
				m_ReplayList[num] = null;
				m_ReplayList.RemoveAt(num);
			}
			else
			{
				m_ReplayList[num].m_ReplayTime += Time.deltaTime;
			}
		}
	}
}
