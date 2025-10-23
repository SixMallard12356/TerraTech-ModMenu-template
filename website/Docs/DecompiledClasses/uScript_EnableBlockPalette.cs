public class uScript_EnableBlockPalette : uScriptLogic
{
	public bool Out => true;

	public void In()
	{
		Singleton.Manager<ManPlayer>.inst.EnablePalette(enable: true);
	}
}
