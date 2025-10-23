internal class GameChat2Data
{
	public static bool ChatEnabled;

	public static int SelectedUser;

	public static bool SendChatData;

	static GameChat2Data()
	{
		SelectedUser = 0;
		ChatEnabled = true;
		SendChatData = true;
	}
}
