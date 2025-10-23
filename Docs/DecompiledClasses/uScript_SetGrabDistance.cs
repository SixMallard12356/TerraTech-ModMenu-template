[NodeDescription("The set distance is only used for the Set function")]
public class uScript_SetGrabDistance : uScriptLogic
{
	public bool Out => true;

	public void SetMainDefault(float dist)
	{
		Mode<ModeMain>.inst.SetOverridePickupRange();
	}

	public void SetDefault(float dist)
	{
		Singleton.Manager<ManPointer>.inst.ResetPickupRange();
	}

	public void Set(float dist)
	{
		Singleton.Manager<ManPointer>.inst.SetPickupRange(dist);
	}
}
