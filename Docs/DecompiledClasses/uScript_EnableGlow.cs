#define UNITY_EDITOR
using cakeslice;

public class uScript_EnableGlow : uScriptLogic
{
	public bool Out => true;

	public void In(object targetObject, bool enable)
	{
		Visible visible = null;
		if (targetObject != null)
		{
			visible = Singleton.Manager<ManVisible>.inst.GetVisibleFromObject(targetObject);
			if (visible != null)
			{
				visible.EnableOutlineGlow(enable, Outline.OutlineEnableReason.ScriptHighlight);
				return;
			}
			d.LogError("uScript_EnableGlow (" + enable + ") - Failed to find visible for object '" + targetObject.ToString() + "'");
		}
		else
		{
			d.LogError("uScript_EnableGlow (" + enable + ") - Null object passed in!");
		}
	}
}
