public static class MultiplayerModeTypeExtensions
{
	public static bool IsCoOp(this MultiplayerModeType e)
	{
		if (e != MultiplayerModeType.CoOpCampaign)
		{
			return e == MultiplayerModeType.CoOpCreative;
		}
		return true;
	}
}
