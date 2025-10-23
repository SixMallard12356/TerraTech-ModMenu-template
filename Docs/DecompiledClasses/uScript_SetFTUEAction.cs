public class uScript_SetFTUEAction : uScriptLogic
{
	public bool Out => true;

	public void In(FTUEEnumType enumType)
	{
		Singleton.Manager<ManNewFTUE>.inst.SetEvent(enumType);
	}
}
