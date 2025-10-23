public class uScript_HasAcceptedAnyMissionFromBoard : uScriptLogic
{
	private bool m_HasAcceptedAnyMission;

	private bool m_HasSubscribed;

	public bool Out => true;

	public bool WaitingForAccept => !m_HasAcceptedAnyMission;

	public bool AnyMissionAccepted => m_HasAcceptedAnyMission;

	public void In()
	{
		if (!m_HasSubscribed)
		{
			(Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MissionBoard) as UIMissionBoard).OnEncounterStarted.Subscribe(OnEncounterStarted);
			m_HasSubscribed = true;
		}
	}

	public void OnDisable()
	{
		m_HasAcceptedAnyMission = false;
		if (m_HasSubscribed)
		{
			(Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MissionBoard) as UIMissionBoard).OnEncounterStarted.Unsubscribe(OnEncounterStarted);
		}
		m_HasSubscribed = false;
	}

	private void OnEncounterStarted(EncounterToSpawn encounterToSpawn)
	{
		m_HasAcceptedAnyMission = true;
	}
}
