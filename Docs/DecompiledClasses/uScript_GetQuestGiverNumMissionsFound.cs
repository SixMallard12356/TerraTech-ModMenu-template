public class uScript_GetQuestGiverNumMissionsFound : uScriptLogic
{
	private bool m_FinishedSearching;

	public bool Out => true;

	public bool SearchingForMissions => !m_FinishedSearching;

	public bool FinishedSearching => m_FinishedSearching;

	public int GetNumMissionsOnBoard(Tank questGiverTech)
	{
		m_FinishedSearching = false;
		int result = 0;
		if ((bool)questGiverTech)
		{
			m_FinishedSearching = !questGiverTech.QuestGiver.IsBusyScanning;
			result = questGiverTech.QuestGiver.NumMissionsDisplayed;
		}
		return result;
	}
}
