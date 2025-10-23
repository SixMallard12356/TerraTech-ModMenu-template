using UnityEngine.Networking;

public class ChallengeTimerMessage : MessageBase
{
	public EncounterIdentifier m_EncounterId;

	public bool m_IsRunning;

	public bool m_IsShowUI;

	public bool m_IsShowBestTimeUI;

	public float m_TimeElapsed;

	public float m_StartTimeRemaining;

	public float m_BestTime;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_EncounterId);
		writer.Write(m_IsRunning);
		writer.Write(m_IsShowUI);
		writer.Write(m_IsShowBestTimeUI);
		writer.Write(m_TimeElapsed);
		writer.Write(m_StartTimeRemaining);
		writer.Write(m_BestTime);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_EncounterId = reader.ReadEncounterID();
		m_IsRunning = reader.ReadBoolean();
		m_IsShowUI = reader.ReadBoolean();
		m_IsShowBestTimeUI = reader.ReadBoolean();
		m_TimeElapsed = reader.ReadSingle();
		m_StartTimeRemaining = reader.ReadSingle();
		m_BestTime = reader.ReadSingle();
	}
}
