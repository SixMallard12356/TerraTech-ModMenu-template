#define UNITY_EDITOR
[FriendlyName("uScript_SetShieldEnabled", "Enable or disable the BubbleShield on target visible")]
[NodePath("TerraTech")]
public class uScript_SetShieldEnabled : uScriptLogic
{
	public bool Out => true;

	public void In(object targetObject, bool enable)
	{
		if (targetObject != null)
		{
			Visible visibleFromObject = Singleton.Manager<ManVisible>.inst.GetVisibleFromObject(targetObject);
			if (visibleFromObject != null)
			{
				ModuleShieldGenerator component = visibleFromObject.GetComponent<ModuleShieldGenerator>();
				BubbleShield componentInChildren = visibleFromObject.GetComponentInChildren<BubbleShield>();
				if ((bool)component)
				{
					component.SetScriptDisabled(!enable);
				}
				else if (componentInChildren != null)
				{
					componentInChildren.SetTargetScale(enable ? 1f : 0f);
				}
				else
				{
					d.LogError("uScript_SetShieldEnabled (" + enable + ") - Failed to find shield for object '" + targetObject.ToString() + "'");
				}
				if (componentInChildren != null)
				{
					Damageable component2 = componentInChildren.transform.GetComponent<Damageable>();
					if (component2 != null)
					{
						component2.SetInvulnerable(invulnerable: true, unlimitedInvulnerability: true);
					}
				}
				if (component != null)
				{
					Damageable component3 = component.transform.GetComponent<Damageable>();
					if (component3 != null)
					{
						component3.SetInvulnerable(enable, enable);
					}
				}
			}
			else
			{
				d.LogError("uScript_SetShieldEnabled (" + enable + ") - Failed to find visible for object '" + targetObject.ToString() + "'");
			}
		}
		else
		{
			d.LogError("uScript_SetShieldEnabled (" + enable + ") - Null object passed in!");
		}
	}
}
