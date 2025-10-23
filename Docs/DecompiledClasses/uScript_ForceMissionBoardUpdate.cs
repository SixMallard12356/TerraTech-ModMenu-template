public class uScript_ForceMissionBoardUpdate : uScriptLogic
{
	public bool Out => true;

	public void In()
	{
		UIMissionBoard uIMissionBoard = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MissionBoard) as UIMissionBoard;
		if (uIMissionBoard != null)
		{
			uIMissionBoard.ForceRefresh();
		}
	}
}
