public static class GameModeExtensions
{
	public static bool IsCoOp(this ManGameMode.GameType e)
	{
		if (e != ManGameMode.GameType.CoOpCampaign)
		{
			return e == ManGameMode.GameType.CoOpCreative;
		}
		return true;
	}

	public static bool IsCreative(this ManGameMode.GameType e)
	{
		if (e != ManGameMode.GameType.Creative)
		{
			return e == ManGameMode.GameType.CoOpCreative;
		}
		return true;
	}

	public static bool IsCampaign(this ManGameMode.GameType e)
	{
		if (e != ManGameMode.GameType.MainGame)
		{
			return e == ManGameMode.GameType.CoOpCampaign;
		}
		return true;
	}
}
