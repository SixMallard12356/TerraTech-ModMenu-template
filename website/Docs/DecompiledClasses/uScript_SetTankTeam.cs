[FriendlyName("Set Single Tech Team")]
[NodeToolTip("Set a tanks team to the given team index")]
[NodePath("TerraTech/Techs/Set Single Tech Team")]
public class uScript_SetTankTeam : uScriptLogic
{
	private int m_TeamID = int.MaxValue;

	public bool Out => true;

	public void In(Tank tank, int team)
	{
		if (m_TeamID == int.MaxValue)
		{
			m_TeamID = Singleton.Manager<ManSpawn>.inst.GenerateAutomaticTeamID(team);
		}
		Tank.SetTechTeamMultiplayerSafe(tank, m_TeamID);
	}

	public void OnDisable()
	{
		m_TeamID = int.MaxValue;
	}
}
