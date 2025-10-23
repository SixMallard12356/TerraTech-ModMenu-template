namespace Payload.UI.Saving;

public class Conditions
{
	public static bool CheckFileExists(SaveOperationData data)
	{
		return ManSaveGame.SaveExists(data.m_GameType, data.m_Name);
	}

	public static bool CheckReservedWord(SaveOperationData data)
	{
		if (Singleton.Manager<ManSaveGame>.inst.IsSaveNameAutoSave(data.m_Name))
		{
			return true;
		}
		return false;
	}
}
