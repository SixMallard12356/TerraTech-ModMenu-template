#define UNITY_EDITOR
public class uScript_LockVisibleStackAccept : uScriptLogic
{
	public bool Out => true;

	public void In(object targetObject)
	{
		Visible visible = null;
		if (targetObject != null)
		{
			visible = Singleton.Manager<ManVisible>.inst.GetVisibleFromObject(targetObject);
			if (visible != null)
			{
				visible.SetLockTimout(Visible.LockTimerTypes.StackAccept);
			}
			else
			{
				d.LogError("ERROR - uScript_LockVisibleStackAccept - Failed to find visible for object '" + targetObject.ToString() + "'");
			}
		}
		else
		{
			d.LogError("ERROR - uScript_LockVisibleStackAccept - Null targetObject passed in!");
		}
	}
}
