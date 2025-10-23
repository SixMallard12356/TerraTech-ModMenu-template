#define UNITY_EDITOR
public class uScript_CloseMissionBoard : uScriptLogic
{
	private bool m_Init;

	private UIMissionBoard m_MissionBoard;

	public bool Out => true;

	public void In()
	{
		if (!m_Init)
		{
			m_MissionBoard = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MissionBoard) as UIMissionBoard;
			m_Init = true;
		}
		if (m_Init && m_MissionBoard != null)
		{
			m_MissionBoard.CloseMissionBoard();
		}
		else
		{
			d.LogError("uScript_CloseMissionBoard - MissionBoard is null");
		}
	}

	public void OnEnable()
	{
		m_Init = false;
	}
}
