namespace DevCommands;

public class GameTypeAttribute : EnumParamAttribute
{
	public GameTypeAttribute()
		: base(typeof(ManGameMode.GameType))
	{
		ExcludeValue(2);
		ExcludeValue(3);
		ExcludeValue(7);
		DisplayValueAs(6, "Quick");
		DisplayValueAs(5, "RnD");
	}
}
