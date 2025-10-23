[NodeToolTip("Set multiple tanks team to the given team index")]
[NodePath("TerraTech/Techs/Set Multiple Techs Team")]
[FriendlyName("Set Multiple Techs Team")]
public class uScript_SetTechsTeam : uScriptLogic
{
	private int m_TeamID = int.MaxValue;

	public bool Out => true;

	public void In(ref Tank[] techs, int team)
	{
		if (techs != null)
		{
			if (m_TeamID == int.MaxValue)
			{
				m_TeamID = Singleton.Manager<ManSpawn>.inst.GenerateAutomaticTeamID(team);
			}
			for (int i = 0; i < techs.Length; i++)
			{
				Tank.SetTechTeamMultiplayerSafe(techs[i], m_TeamID);
			}
		}
	}

	public void OnDisable()
	{
		m_TeamID = int.MaxValue;
	}
}
